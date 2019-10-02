using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HackerNewsScraper.Models;

namespace HackerNewsScraper.Interfaces
{
    /// <summary>
    /// Interface to allow implementations to provide access to the HackerNews API
    /// </summary>
    public interface IHackerNewsService
    {
        /// <summary>
        /// Provide an implementation to derive and return the top posts
        /// on HackerNews
        /// </summary>
        IEnumerable<Post> GetTopPosts(IConsoleStream consoleStream);
    }
}
