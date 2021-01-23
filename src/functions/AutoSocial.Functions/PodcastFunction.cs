using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CodeHollow.FeedReader;
using AutoSocial.Functions.Models;
using System.Linq;

namespace AutoSocial.Functions
{
    public static class PodcastFunction
    {
        [FunctionName("Podcast")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Podcast info retrieved.");

            var podcastFeed = await FeedReader.ReadAsync(Constants.PodcastFeedURL);
            var randomIndex = new Random().Next(podcastFeed.Items.Count) - 1;
            var pdFeedItem = new AutoFeedItem()
            {
                Title = podcastFeed.Items.ToArray()[randomIndex].Title,
                Link = podcastFeed.Items.ToArray()[randomIndex].Link,

            };
            log.LogInformation("Podcast info returned.");
            return new OkObjectResult(pdFeedItem);
        }
    }
}
