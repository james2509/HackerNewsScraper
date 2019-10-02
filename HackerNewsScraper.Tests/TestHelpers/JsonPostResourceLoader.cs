using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace HackerNewsScraper.Tests.TestHelpers
{
    /// <summary>
    /// Test helper class to Load embedded resources from the assembly
    /// </summary>
    public static class JsonPostResourceLoader
    {
        /// <summary>
        /// Load test Post data to be used as HackerNews fake responses
        /// </summary>
        public static string Load(long postId)
        {
            var resourceName = $"HackerNewsScraper.Tests.Data.{postId}.json";
            return LoadResource(resourceName);
        }

        /// <summary>
        /// Load expected test data
        /// </summary>
        public static string Load(string expectedName)
        {
            var resourceName = $"HackerNewsScraper.Tests.Expected.{expectedName}.json";
            return LoadResource(resourceName);
        }

        private static string LoadResource(string resourceName)
        {
            string postJsonString;
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                postJsonString = reader.ReadToEnd();
            }

            return postJsonString;
        }
    }
}
