using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class BatchScript
    {

        string command = null;
        string param = null;
        int exitCode = 0;

        /// <summary>
        /// Set to the batch command to execute
        /// </summary>
        public string Command { set; get; }
        /// <summary>
        /// Set to the parameter to send to the batch command
        /// </summary>
        public string Param { set; get; }
        /// <summary>
        /// This property is set to the exit code returned
        /// from the batch commmand
        /// </summary>
        public int ExitCode { set; get; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public BatchScript()
        { }
        /// <summary>
        /// A constructor that will set the batch command and parameter
        /// </summary>
        /// <param name="c">The batch command</param>
        /// <param name="p">A parameter to send to the batch command</param>
        public BatchScript(string c, string p)
        {
            command = c;
            Param = p;
        }
        /// <summary>
        /// Executes the given command. Make sure to initialize the
        /// Command and Params properties.
        /// </summary>
        public void Run()
        {
            ProcessStartInfo processInfo;
            Process process;

            try
            {
                processInfo = new ProcessStartInfo("cmd.exe", "/c " + Command + " \"" + Param + "\"");
                processInfo.CreateNoWindow = true;
                processInfo.UseShellExecute = false;
                // *** Redirect the output ***
                processInfo.RedirectStandardError = false;
                processInfo.RedirectStandardOutput = false;
                processInfo.WindowStyle = ProcessWindowStyle.Minimized;
                process = Process.Start(processInfo);
                process.WaitForExit();

                exitCode = process.ExitCode;
                process.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        
        }
    }
}
