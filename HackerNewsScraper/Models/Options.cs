using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using CommandLine;

namespace HackerNewsScraper.Model
{
    /// <summary>
    /// Data structure to hold command line options
    /// </summary>
    public class Options
    {
        [Option('p', "posts", Required = true, 
            HelpText = "Please enter the number of posts using a number from 1 to 100")]
        public int Posts { get; set; }
    }
}
