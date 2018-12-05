using MSMQCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSMQExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btReadMessage_Click(object sender, EventArgs e)
        {
            Queue myqueue = new Queue();
            try
            {
                Inputfile inputfile = myqueue.ReadMessage();
                tbQueueStatus.AppendText(inputfile.File + Environment.NewLine);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btWriteMessage_Click(object sender, EventArgs e)
        {
            Inputfile inputfile = new Inputfile();
            inputfile.File = tbMessage.Text;
            Queue myqueue = new Queue();
            try
            {
                myqueue.WriteMessage(inputfile);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Message written successfully");
            
        }
    }
}
