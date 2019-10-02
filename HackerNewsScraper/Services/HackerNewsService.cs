using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HackerNewsScraper.Interfaces;
using HackerNewsScraper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HackerNewsScraper.Services
{
    /// <summary>
    /// Implementation of IHackerNewsService to call APIs to get the latest stories and to also get each story in detail.
    /// This uses a HttpClient to call HackerNews APIs hosted on Firebase as described here... https://github.com/HackerNews/API
    /// </summary>
    public class HackerNewsService : IHackerNewsService
    {
        private readonly HttpClient client;
        
        public HackerNewsService(HttpClient client = null)
        {
            this.client = client ?? new HttpClient();
        }

        /// <summary>
        /// First gets the list of top story Ids and then calls the API for each Id to get the full story data.
        /// This method takes advantage of LazyEvaluation which only executes the API call when needed. 
        /// </summary>
        public IEnumerable<Post> GetTopPosts(IConsoleStream consoleStream)
        {
            var postIds = GetTopPostIdCollection(consoleStream);

            foreach (var id in postIds)
            {
                Post post = null;

                try
                { 
                    var response = client.GetAsync(new Uri($"https://hacker-news.firebaseio.com/v0/item/{id}.json")).Result;
                    var jsonString = response.Content.ReadAsStringAsync().Result;
                    post = JsonConvert.DeserializeObject<Post>(jsonString);
                }
                catch (Exception e)
                {
                    consoleStream.WriteErrorLine($"Error getting Post for Post Id: {id}");
                    consoleStream.WriteErrorLine(e.Message);
                    continue;
                }

                yield return post;
            }
        }

        /// <summary>
        /// Makes an API call to get the collection of Top Story Ids
        /// </summary>
        private IEnumerable<long> GetTopPostIdCollection(IConsoleStream consoleStream)
        {
            IEnumerable<long> storyIds = new List<long>().AsEnumerable();

            try
            {
                var response = client.GetAsync(new Uri("https://hacker-news.firebaseio.com/v0/topstories.json")).Result;
                var jsonString = response.Content.ReadAsStringAsync().Result;
                storyIds = JsonConvert.DeserializeObject<IEnumerable<long>>(jsonString);
            }
            catch (Exception e)
            {
                consoleStream.WriteErrorLine("Error getting latest Post Ids");
                consoleStream.WriteErrorLine(e.Message);
            }

            return storyIds;
        }
    }
}
