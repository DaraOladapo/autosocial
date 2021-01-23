using AutoSocial.Functions.Models;
using CodeHollow.FeedReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutoSocial.Functions
{
    public static class PodcastFunction
    {
        [FunctionName("PodcastFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Podcast info retrieved.");

            var podcastFeed = await FeedReader.ReadAsync(Constants.PodcastFeedURL);
            var randomIndex = new Random().Next(podcastFeed.Items.Count) - 1;
            var pdFeedItem = new AutoFeedItem()
            {
                Title = podcastFeed.Items.ToArray()[randomIndex].Title,
                Description = podcastFeed.Items.ToArray()[randomIndex].Description,
                Link = podcastFeed.Items.ToArray()[randomIndex].Link,

            };
            log.LogInformation("Podcast info returned.");
            return new OkObjectResult(pdFeedItem);
        }
    }
}
