using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HackerNewsScraper.Interfaces;
using Newtonsoft.Json;

namespace HackerNewsScraper.Tests.TestHelpers
{
    /// <summary>
    /// Implementation pf IConsoleStream to provide a testable ConsoleStream to be used in unit tests
    /// Implements IDisposable to dispose of the JsonWriter when ConsoleStream is disposed
    /// </summary>
    public class TestConsoleStream : IConsoleStream, IDisposable
    {
        private readonly List<string> errors;
        private readonly StringWriter output = new StringWriter();

        public TestConsoleStream()
        {
            errors = new List<string>();
            JsonWriter = new JsonTextWriter(output);
        }

        public List<string> Errors => errors;

        public StringWriter Output => output;
        
        /// <summary>
        /// Provide a JsonWriter writing Json data to a test store (stringwriter)
        /// </summary>
        public JsonWriter JsonWriter { get; }

        /// <summary>
        /// Write error data to a test data store (list<string>)
        /// </summary>
        public void WriteErrorLine(string error)
        {
            errors.Add(error);
        }

        public void Dispose()
        {
            ((IDisposable)JsonWriter).Dispose();
        }
    }
}
