using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcher
{
    public partial class RunScript : Form
    {
        int exitCode = 0;
        public RunScript()
        {
            InitializeComponent();
        }
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
        /// Executes the given command. Make sure to initialize the
        /// Command and Params properties.
        /// </summary>
        private void Run(object sender, EventArgs e)
        {
            //Thread thread = new Thread(() => RunBatch(Path.Combine(fileDir, "BatchScript", "run.bat"), inputfile.File));
            Thread thread = new Thread(Start);
            thread.Start();
        }

        private void Start()
        {
            Process process;

            try
            {
                process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c " + Command + " \"" + Param + "\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                //process.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;

                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;

                process.ErrorDataReceived += process_DataReceived;
                process.OutputDataReceived += process_DataReceived;
                process.Start();

                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.WaitForExit();
                exitCode = process.ExitCode;
                process.Close();
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        private void process_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action(() =>
                {
                    textBox1.AppendText(e.Data + Environment.NewLine);
                }));
            }
            else
            {
                textBox1.AppendText(e.Data + Environment.NewLine);
            }
            
        }
    }
}
