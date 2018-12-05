using MSMQCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcher
{

    public partial class Form1 : Form
    {
        private Logger logger;
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetForegroundWindow(IntPtr hwnd);
        public Form1()
        {
            logger = new Logger();
            logger.Info("Starting FileWatcher.", "");
            InitializeComponent();
        }

        private void btStart_Click(object sender, EventArgs e)
        {
            // Need to add Exception handling
            // In particular, check that the folder is valid
            try
            {
                FileSystemWatcher watcher = new FileSystemWatcher();
                watcher.Path = tbFoldertoWatch.Text;
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
                // Only watch csv files.
                watcher.Filter = "*.csv";
                // Set up event handler
                watcher.Created += new FileSystemEventHandler(OnChanged);
                // Begin watching.
                watcher.EnableRaisingEvents = true;
                btStart.Enabled = false;
                btBrowse.Enabled = false;
                tbFoldertoWatch.Enabled = false;
                //MessageBox.Show("Folder monitoring started.");
                logger.Info(String.Format("Started monitoring {0} folder for new files.", tbFoldertoWatch.Text));
                InitializeQueue();
            }
            catch (ArgumentException ex)
            {
                logger.Warning(ex.Message);
                MessageBox.Show("Invalid drive entered.");
            }
        }

        /// <summary>
        /// Initializes the queue during start of the application
        /// </summary>
        private void InitializeQueue()
        {
            // Initialize Queue with files read from folder specified in tbFolderWatch.Text
            // Process the list of files found in the directory.

            logger.Info("Initialize queue.");
            string[] fileEntries = null;
            try
            {
                // Enumerate all files in the watched folder
                fileEntries = Directory.GetFiles(tbFoldertoWatch.Text);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }

            Queue myqueue = new Queue();
            try
            {
                // Delete existing queue
                myqueue.DeleteQueue();
                logger.Info("Existing queue deleted.");
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
            logger.Info("Adding files from watch folder to queue.");
            try
            {
                myqueue.WriteMessages(fileEntries);
                foreach (string file in fileEntries)
                {
                    // Need this to avoid cross-thread operation not valid error
                    if (InvokeRequired)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            tbFilesFound.AppendText(file + Environment.NewLine); ;
                        }));
                    }
                    else
                    {
                        tbFilesFound.AppendText(file + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        /// <summary>
        /// Event handler will add new files in watch folder to the queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            Inputfile inputfile = new Inputfile();
            inputfile.File = e.FullPath;
            Queue myqueue = new Queue();
            try
            {
                myqueue.WriteMessage(inputfile);
                logger.Info(String.Format("Added {0} to the queue.", inputfile.File));
                // Need this to avoid cross-thread operation not valid error
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        tbFilesFound.AppendText(e.FullPath.ToString() + Environment.NewLine);
                        btRead.Enabled = true;
                    }));
                }
                else
                {
                    tbFilesFound.AppendText(e.FullPath.ToString() + Environment.NewLine);
                    btRead.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbFoldertoWatch.Text = folderBrowserDialog1.SelectedPath;
            }
            logger.Info(String.Format("User selected {0} folder to watch", tbFoldertoWatch.Text));
        }

        private void btRead_Click(object sender, EventArgs e)
        {
            Queue myqueue = new Queue();
            try
            {
                Inputfile inputfile = myqueue.ReadMessage();
                if (myqueue.Count == 0)
                {
                    btRead.Enabled = false;
                }
                logger.Info(String.Format("Read {0} from queue.", inputfile.File));
                tbRead.AppendText(inputfile.File + Environment.NewLine);
                string fullPath = System.Reflection.Assembly.GetEntryAssembly().Location;
                string fileName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                string fileDir = Path.GetDirectoryName(fullPath);

                Thread thread = new Thread(() => RunBatch(Path.Combine(fileDir, "BatchScript", "run.bat"), inputfile.File));
                thread.Start();

                Thread keepwindowup = new Thread(KeepWindowUp);
                keepwindowup.Start();
                //while (!thread.IsAlive) ; // Wait until the thread is in the background
                //Thread.Sleep(3000);
                //if (!this.Focused)
                //    this.Focus();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        void KeepWindowUp()
        {
            {
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (this.Handle != GetForegroundWindow())
                        {
                            SetForegroundWindow(this.Handle);
                            //Or comment above for your custom stuff here...
                        }
                    }));
                }
                else
                {
                    if (this.Handle != GetForegroundWindow())
                    {
                        SetForegroundWindow(this.Handle);
                        //Or comment above for your custom stuff here...
                    }
                }

            }
          
        }
        void RunBatch(string command, string param)
        {
            //BatchScript batchscript = new BatchScript();
            RunScript batchscript = new RunScript();
            batchscript.Command = command;
            batchscript.Param = param;
            batchscript.WindowState = FormWindowState.Minimized;
            if (File.Exists(batchscript.Command))
            {
                try
                {
                    logger.Info(String.Format("Executing batch file {0}.", batchscript.Param));
                    batchscript.ShowDialog();
                    logger.Info(String.Format("Executing batch file {0} successful.", batchscript.Param));
                }
                catch (Exception ex)
                {
                    logger.Error(String.Format("An error was thrown while executing the batch file {0}. {1}", batchscript.Param, ex.Message));
                }
            }

        }
    }
}
