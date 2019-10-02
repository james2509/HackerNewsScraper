using System;
using System.Collections.Generic;
using System.Text;

using Xunit;
using FluentAssertions;
using HackerNewsScraper.Models;
using HackerNewsScraper.Validators;

namespace HackerNewsScraper.Tests.Validators
{
    /// <summary>
    /// Test class to perform validation on a Post object. Includes a helper method to
    /// create Post objects and override data to setup the test case
    /// </summary>
    public class PostValidatorTest
    {
        [Fact]
        public void GivenAPostObject_WithAllValidData_IsValidReturnsTrue()
        {
            //Arrange
            var post = GetPostObject();

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenAPostObject_WithTitleGreaterThan256Chars_IsValidReturnsFalse()
        {
            //Arrange
            var longTitle = new string('a', 257);
            var post = GetPostObject(title: longTitle);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPostObject_WithByGreaterThan256Chars_IsValidReturnsFalse()
        {
            //Arrange
            var longName = $"{new string('a', 135)} {new string('b', 135)}";
            var post = GetPostObject(by: longName);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("file://c:/directory/filename")]
        [InlineData(@"C:\testsite\posts")]
        [InlineData(@"http:\\\host/path/file")]
        [InlineData(@"www.testsite.com/path/file")]
        [InlineData("2013.05.29_14:33:41")]
        public void GivenAPostObject_WithUrlWithoutScheme_IsValidReturnsFalse(string url)
        {
            //Arrange
            var post = GetPostObject(url: url);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPostObject_WithScoreSetToNegativeInteger_IsValidReturnsFalse()
        {
            //Arrange
            var score = -12;
            var post = GetPostObject(score: score);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPostObject_WithScoreSetToZero_IsValidReturnsTrue()
        {
            //Arrange
            var score = 0;
            var post = GetPostObject(score: score);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenAPostObject_WithDescendantsSetToNegativeInteger_IsValidReturnsFalse()
        {
            //Arrange
            var descendants = -12;
            var post = GetPostObject(descendants: descendants);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPostObject_WithDescendantsSetToZero_IsValidReturnsTrue()
        {
            //Arrange
            var descendants = 0;
            var post = GetPostObject(descendants: descendants);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenAPostObject_WithRankSetToNegativeInteger_IsValidReturnsFalse()
        {
            //Arrange
            var rank = -12;
            var post = GetPostObject(rank: rank);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void GivenAPostObject_WithRankSetToZero_IsValidReturnsTrue()
        {
            //Arrange
            var rank = 0;
            var post = GetPostObject(rank: rank);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void GivenAPostObject_WithTypeSetToJob_IsValidReturnsFalse()
        {
            //Arrange
            var type = "job";
            var post = GetPostObject(type: type);

            //Act
            var result = PostValidator.Validate(post);

            //Assert
            result.Should().BeFalse();

        }

        /// <summary>
        /// Helper method to create a Post object. Includes optional parameters to allow the caller
        /// to override standard data to setup the test case.
        /// </summary>
        private Post GetPostObject(string title = null, string url = null, string by = null, 
            int? score = null, string type = null, int? descendants = null, int? rank = null)
        {
            return new Post
            {
                title = title ?? "This is a valid title",
                url = url ?? "https://www.thisisavalidurl.com/posts",
                by = by ?? "James Smith",
                score = score ?? 536,
                type = type ?? "story",
                descendants = descendants ?? 12,
                rank = rank ?? 13,
                id = 412645, 
            };
        }
    }
}
