using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using HackerNewsScraper.Interfaces;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;

namespace HackerNewsScraper.Models
{
    /// <summary>
    /// Implementation pf IConsoleStream to provide access to the System.Console STDERR and STDOUT streams
    /// Implements IDisposable to dispose of the JsonWriter when ConsoleStream is disposed
    /// </summary>
    public class ConsoleStream : IConsoleStream, IDisposable
    {
        private readonly TextWriter errorStream;
        private readonly TextWriter outputStream;

        public ConsoleStream(TextWriter errorStream, TextWriter outputStream)
        {
            this.errorStream = errorStream;
            this.outputStream = outputStream;
            JsonWriter = new JsonTextWriter(outputStream);
        }

        /// <summary>
        /// Write Error data to the STDERR stream
        /// </summary>
        /// <param name="error"></param>
        public void WriteErrorLine(string error)
        {
            errorStream.WriteLine(error);
            errorStream.Flush();
        }

        /// <summary>
        /// JsonWriter writing to the System.Console STDOUT stream
        /// </summary>
        public JsonWriter JsonWriter { get; }

        public void Dispose()
        {
            ((IDisposable)JsonWriter).Dispose();
        }

    }
}
