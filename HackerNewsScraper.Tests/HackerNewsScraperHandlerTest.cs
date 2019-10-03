using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

using Xunit;
using FluentAssertions;
using HackerNewsScraper.Model;
using HackerNewsScraper.Services;
using HackerNewsScraper.Tests.TestHelpers;

namespace HackerNewsScraper.Tests
{
    /// <summary>
    /// Test class to test functionality in the HackerNewsScraperHandler class
    /// This uses mocked HTTPMessageHandlers injected into the HttpClient to fake Hacker News responses
    /// </summary>
    public class HackerNewsScraperHandlerTest
    {
        private readonly TestConsoleStream consoleStream;

        public HackerNewsScraperHandlerTest()
        {
            consoleStream = new TestConsoleStream();
        }

        [Fact]
        public void GivenHandleCalled_WithHttpResponseWithTwoValidPost_ExpectFormattedJsonOnOutputStream()
        {
            //Arrange
            var mockHttpHandler = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259 });
            var service = new HackerNewsService(new HttpClient(mockHttpHandler));
            var handler = new HackerNewsScraperHandler(service);
            var expected = JsonPostResourceLoader.Load("WithHttpResponseWithTwoValidPost");

            //Act
            handler.Handle(new Options{Posts = 2}, consoleStream);

            //Assert
            var output = consoleStream.Output.ToString();
            output.Should().Be(expected);
        }

        [Fact]
        public void GivenHandleCalled_WithHttpResponseWithThreePostsOneInvalid_ExpectFormattedJsonOnOutputStreamWithTwoPosts()
        {
            //Arrange
            var mockHttpHandler = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259, 21143102 });
            var service = new HackerNewsService(new HttpClient(mockHttpHandler));
            var handler = new HackerNewsScraperHandler(service);
            var expected = JsonPostResourceLoader.Load("WithHttpResponseWithTwoValidPost");

            //Act
            handler.Handle(new Options { Posts = 2 }, consoleStream);

            //Assert
            var output = consoleStream.Output.ToString();
            output.Should().Be(expected);
        }

        [Fact]
        public void GivenHandleCalled_WithHttpResponseWithTwoValidPostsAndPostsNumberIsOne_ExpectFormattedJsonOnOutputStreamWithOnePost()
        {
            //Arrange
            var mockHttpHandler = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259 });
            var service = new HackerNewsService(new HttpClient(mockHttpHandler));
            var handler = new HackerNewsScraperHandler(service);
            var expected = JsonPostResourceLoader.Load("WithHttpResponseWithTwoValidPostsAndPostsNumberIsOne");

            //Act
            handler.Handle(new Options { Posts = 1 }, consoleStream);

            //Assert
            var output = consoleStream.Output.ToString();
            output.Should().Be(expected);
        }

        [Fact]
        public void GivenHandleCalled_WithHttpResponseWithFiveValidPosts_ExpectFormattedJsonOnOutputWithPostsInOrderOfIdsCollection()
        {
            //Arrange
            var mockHttpHandler = MockPostHttpMessageHandlerCreator.Create(new List<long> { 21134540, 21135259, 21144604, 21138841, 21144098 });
            var service = new HackerNewsService(new HttpClient(mockHttpHandler));
            var handler = new HackerNewsScraperHandler(service);
            var expected = JsonPostResourceLoader.Load("WithHttpResponseWithFiveValidPosts");

            //Act
            handler.Handle(new Options { Posts = 5 }, consoleStream);

            //Assert
            var output = consoleStream.Output.ToString();
            output.Should().Be(expected);
        }

        [Fact]
        public void GivenAnOptionsPassedIntoHandle_WithPostExceedingOneHundred_ExpectErrorInErrorStream()
        {
            //Arrange
            var handler = new HackerNewsScraperHandler(new HackerNewsService());

            //Act
            handler.Handle(new Options{Posts = 102}, consoleStream);

            //Assert
            consoleStream.Errors.Count.Should().Be(1);
            consoleStream.Errors.Any(e => e == "Number of posts must be a positive integer not exceeding 100").Should().BeTrue();
        }

        [Fact]
        public void GivenAnOptionsPassedIntoHandle_WithPostLessThan1_ExpectErrorInErrorStream()
        {
            //Arrange
            var handler = new HackerNewsScraperHandler(new HackerNewsService());

            //Act
            handler.Handle(new Options { Posts = 0 }, consoleStream);

            //Assert
            consoleStream.Errors.Count.Should().Be(1);
            consoleStream.Errors.Any(e => e == "Number of posts must be a positive integer not exceeding 100").Should().BeTrue();
        }
    }
}
