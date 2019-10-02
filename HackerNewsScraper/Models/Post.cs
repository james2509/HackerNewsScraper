using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HackerNewsScraper.Attributes;
using Newtonsoft.Json;

namespace HackerNewsScraper.Models
{
    /// <summary>
    /// Holds Post data as returned from HackerNews
    /// Properties are marked up with DataAnnotations which are used to validate that the post is valid
    /// I believe annotating the data makes it clear what makes up a valid Post
    /// </summary>
    public class Post
    {
        [StringLength(256)]
        public string title { get; set; }

        [Required, UrlValid]
        public string url { get; set; }

        [Range(0, long.MaxValue)]
        public long id { get; set; }

        [Required, StringLength(256)]
        public string by { get; set; }

        [Range(0, Int32.MaxValue)]
        public int score { get; set; } //points

        [IsStory]
        public string type { get; set; }

        [Range(0, Int32.MaxValue)]
        public int descendants { get; set; } //comments

        [Range(0, Int32.MaxValue)]
        public int rank { get; set; }
    }
}
