using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Text;
using HackerNewsScraper.Interfaces;
using HackerNewsScraper.Model;
using HackerNewsScraper.Models;
using HackerNewsScraper.Services;
using HackerNewsScraper.Validators;
using Newtonsoft.Json;

namespace HackerNewsScraper
{
    /// <summary>
    /// Handler to orchestrate the building and output of a specified number of Posts to the STDOUT stream
    /// </summary>
    public class HackerNewsScraperHandler
    {
        private readonly IHackerNewsService service;

        public HackerNewsScraperHandler(IHackerNewsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Trigger the build and output of Posts to the STDOUT stream
        /// </summary>
        public void Handle(Options options, IConsoleStream consoleStream)
        {
            // Validate that the number of Posts does not exceed 100
            if (options.Posts > 100)
            {
                consoleStream.WriteErrorLine("Number of posts cannot exceed 100");
                return;
            }

            var postSerializer = new JsonSerializer();
            consoleStream.JsonWriter.Formatting = Formatting.Indented;
            int postCounter = 1;
            
            // Write the start of the Json Array to the STDOUT stream
            consoleStream.JsonWriter.WriteStartArray();

            // Write each post to the Array in the STDOUT stream
            // This continues to use Linq Lazy Evaluation to write one valid post to the STDOUT stream
            // at a time
            var posts = service.GetTopPosts(consoleStream)
                .Where(PostValidator.Validate)
                .Where(p => SerializePost(postSerializer, consoleStream, p, postCounter++))
                .Take(options.Posts)
                .ToList();

            // Write the end of the Json Array to the STDOUT stream
            consoleStream.JsonWriter.WriteEndArray();
        }

        /// <summary>
        /// Method to write a Post in the required format to the STDOUT stream
        /// </summary>
        private bool SerializePost(JsonSerializer ser, IConsoleStream consoleStream, Post post, int counter)
        {
            try
            {
                ser.Serialize(consoleStream.JsonWriter, 
                    new
                    {
                        title = post.title,
                        uri = post.url,
                        author = post.by,
                        points = post.score,
                        comments = post.descendants,
                        rank = counter
                    });
            }
            catch (Exception e)
            {
                consoleStream.WriteErrorLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
