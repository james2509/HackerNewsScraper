using System;
using System.Collections.Generic;
using System.Text;
using RichardSzalay.MockHttp;

namespace HackerNewsScraper.Tests.TestHelpers
{
    /// <summary>
    /// Create a mock HttpMessageHandler with required responses to be able to fake one or more
    /// calls to the HackerNews API
    /// </summary>
    public static class MockPostHttpMessageHandlerCreator
    {
        public static MockHttpMessageHandler Create(List<long> postIds)
        {
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://hacker-news.firebaseio.com/v0/topstories.json")
                .Respond("application/json", $"[{String.Join(",",postIds)}]");

            foreach (var id in postIds)
            {
                mockHttp.When($"https://hacker-news.firebaseio.com/v0/item/{id}.json")
                    .Respond("application/json", JsonPostResourceLoader.Load(id));
            }

            return mockHttp;
        }
    }
}
