using System;
using System.Collections.Generic;
using CommandLine;
using HackerNewsScraper.Model;
using HackerNewsScraper.Services;

namespace HackerNewsScraper
{
    public class Program
    {
        /// <summary>
        /// Entry point to gather input arguments and starts processing
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(ExecuteHandler);
        }

        /// <summary>
        /// Instantiate required objects and kick of processing
        /// </summary>
        static void ExecuteHandler(Options options)
        {
            var consoleStream = new Models.ConsoleStream(Console.Error, Console.Out);

            var handler = new HackerNewsScraperHandler(new HackerNewsService());
            handler.Handle(options, consoleStream);
        }
    }
}
