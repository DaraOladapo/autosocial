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
    public static class BlogPostFunction
    {
        [FunctionName("BlogPost")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {log.LogInformation("blog post info retrieved.");

            var blogPostFeed = await FeedReader.ReadAsync(Constants.BlogFeedURL);
            var randomIndex = new Random().Next(blogPostFeed.Items.Count) - 1;
            var blgFeedItem = new AutoFeedItem()
            {
                Title = blogPostFeed.Items.ToArray()[randomIndex].Title,
                Description = blogPostFeed.Items.ToArray()[randomIndex].Description,
                Link = blogPostFeed.Items.ToArray()[randomIndex].Link,

            };
            log.LogInformation("blog post info info returned.");
            return new OkObjectResult(blgFeedItem);
        }
    }
}
