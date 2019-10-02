using System;
using System.Collections.Generic;
using System.Text;
using HackerNewsScraper.Models;
using Newtonsoft.Json;

namespace HackerNewsScraper.Interfaces
{
    /// <summary>
    /// Interface to allow implementations to provide right access to the STDERR and STDOUT streams
    /// or to create a test implementation for testing purposes
    /// </summary>
    public interface IConsoleStream
    {
        /// <summary>
        /// Write Error line to the STDERR Stream
        /// </summary>
        void WriteErrorLine(string error);

        /// <summary>
        /// Provide access to a writer to write JSon data to the STDOUT stream
        /// </summary>
        JsonWriter JsonWriter { get; }
    }
}
