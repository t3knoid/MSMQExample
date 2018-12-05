﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.IO;

namespace FileWatcher
{
    public class Logger
    {
        public String logfile { set; get; }

        public Logger()
        {
            string fullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string fileName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            string fileDir = Path.GetDirectoryName(fullPath);
            logfile = String.Format("{0}_{1}.log", Path.Combine(fileDir,fileName), DateTime.Now.ToString("yyyyMMddHHmmss"));
            Init();
        }
        public Logger(String file)
        {
            logfile = file;
            Init();
        }

        private void Init()
        {
            // The following stream allows sharing of the log file with an external process
            Stream myFile = new FileStream(logfile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
            /* Create a new text writer using the output stream, and add it to
             * the trace listeners. */
            TextWriterTraceListener myTextListener = new
               TextWriterTraceListener(myFile);
            Trace.Listeners.Add(myTextListener);
            Trace.AutoFlush = true;
        }
        public void Error(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[ERROR]",module);
        }
        public void Error(Exception ex, [CallerMemberName]string module = "")
        {
            WriteEntry(ex.Message, "[ERROR]",module);
        }
        public void Warning(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[WARNING]",module);
        }
        public void Info(string message, [CallerMemberName]string module = "")
        {
            WriteEntry(message, "[INFO]",module);
        }
        private void WriteEntry(string message, string type, string module)
        {
           // Stream myFile = File.OpenWrite(logfile);

            Trace.WriteLine(
                    string.Format("{0},{1},{2},{3}",
                                  DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                  type,
                                  module,
                                  message));
            Trace.Flush();
        }
    }
}