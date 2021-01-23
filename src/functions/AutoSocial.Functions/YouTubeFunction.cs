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
    public static class YouTubeFunction
    {
        [FunctionName("YouTube")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Youtube video info retrieved.");

            var youTubeFeed = await FeedReader.ReadAsync(Constants.YoutubeFeedURL);
            var randomIndex = new Random().Next(youTubeFeed.Items.Count) - 1;
            var ytFeedItem = new AutoFeedItem()
            {
                Title = youTubeFeed.Items.ToArray()[randomIndex].Title,
                Description = youTubeFeed.Items.ToArray()[randomIndex].Description,
                Link = youTubeFeed.Items.ToArray()[randomIndex].Link,

            };
            log.LogInformation("Youtube video info returned.");
            return new OkObjectResult(ytFeedItem);
        }
    }
}
