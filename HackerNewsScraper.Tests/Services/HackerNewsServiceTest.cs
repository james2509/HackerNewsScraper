using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using Xunit;
using FluentAssertions;
using HackerNewsScraper.Models;
using HackerNewsScraper.Services;
using HackerNewsScraper.Tests.TestHelpers;
using RichardSzalay.MockHttp;

namespace HackerNewsScraper.Tests.Services
{
    /// <summary>
    /// Test class to test functionality in the HackerNewsService class
    /// This uses mocked HTTPMessageHandlers injected into the HttpClient to fake Hacker News responses
    /// </summary>
    public class HackerNewsServiceTest
    {
        private readonly TestConsoleStream consoleStream;

        public HackerNewsServiceTest()
        {
            consoleStream = new TestConsoleStream();
        }

        [Fact]
        public void GivenAHackerNewsResponse_WhereTwoWellFormedPostsExist_ExpectTwoPostObjectsToBeConstructed()
        {
            //Arrange
            var mockHttp = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259 });
            var client = new HttpClient(mockHttp);
            var service = new HackerNewsService(client);

            //Act
            var posts = service.GetTopPosts(consoleStream).ToList();

            //Assert
            posts.Count.Should().Be(2);
            posts.FirstOrDefault(p => p.id == 21134540).Should().NotBeNull();
            posts.FirstOrDefault(p => p.id == 21135259).Should().NotBeNull();
        }

        [Fact]
        public void GivenAHackerNewsResponse_WhereTwoWellFormedAndOneUnparsablePostExist_ExpectTwoPostObjectsToBeConstructed()
        {
            //Arrange
            var mockHttp = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259, 999 });
            var client = new HttpClient(mockHttp);
            var service = new HackerNewsService(client);

            //Act
            var posts = service.GetTopPosts(consoleStream).ToList();

            //Assert
            posts.Count.Should().Be(2);
            posts.FirstOrDefault(p => p.id == 21134540).Should().NotBeNull();
            posts.FirstOrDefault(p => p.id == 21135259).Should().NotBeNull();
            consoleStream.Errors.Count.Should().Be(2);
        }

        [Fact]
        public void GiveAHackerNewsResponse_WhereTopStoryIdsAreNotAvailable_ExpectNoPosts()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://hacker-news.firebaseio.com/v0/topstories.json")
                .Respond("application/json", $"[]");
            var client = new HttpClient(mockHttp);
            var service = new HackerNewsService(client);

            //Act
            var posts = service.GetTopPosts(consoleStream).ToList();

            //Assert
            posts.Count.Should().Be(0);
        }

        [Fact]
        public void GiveAHackerNewsResponse_WhereTopStoryIdsAreNotParsable_ExpectNoPostsAndErrorToBeReturned()
        {
            //Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("https://hacker-news.firebaseio.com/v0/topstories.json")
                .Respond("application/json", $"['a', 'b', 'c', 'd']");
            var client = new HttpClient(mockHttp);
            var service = new HackerNewsService(client);

            //Act
            var posts = service.GetTopPosts(consoleStream).ToList();

            //Assert
            posts.Count.Should().Be(0);
            consoleStream.Errors.Count.Should().Be(2);
        }
    }
}
