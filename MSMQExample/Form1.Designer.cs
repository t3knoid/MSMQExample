namespace MSMQExample
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbQueueStatus = new System.Windows.Forms.TextBox();
            this.btReadMessage = new System.Windows.Forms.Button();
            this.btWriteMessage = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbQueueStatus
            // 
            this.tbQueueStatus.Location = new System.Drawing.Point(46, 45);
            this.tbQueueStatus.Multiline = true;
            this.tbQueueStatus.Name = "tbQueueStatus";
            this.tbQueueStatus.Size = new System.Drawing.Size(569, 125);
            this.tbQueueStatus.TabIndex = 0;
            // 
            // btReadMessage
            // 
            this.btReadMessage.Location = new System.Drawing.Point(212, 347);
            this.btReadMessage.Name = "btReadMessage";
            this.btReadMessage.Size = new System.Drawing.Size(75, 23);
            this.btReadMessage.TabIndex = 1;
            this.btReadMessage.Text = "Read";
            this.btReadMessage.UseVisualStyleBackColor = true;
            this.btReadMessage.Click += new System.EventHandler(this.btReadMessage_Click);
            // 
            // btWriteMessage
            // 
            this.btWriteMessage.Location = new System.Drawing.Point(425, 347);
            this.btWriteMessage.Name = "btWriteMessage";
            this.btWriteMessage.Size = new System.Drawing.Size(75, 23);
            this.btWriteMessage.TabIndex = 2;
            this.btWriteMessage.Text = "Write";
            this.btWriteMessage.UseVisualStyleBackColor = true;
            this.btWriteMessage.Click += new System.EventHandler(this.btWriteMessage_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(425, 309);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(274, 20);
            this.tbMessage.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.btWriteMessage);
            this.Controls.Add(this.btReadMessage);
            this.Controls.Add(this.tbQueueStatus);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbQueueStatus;
        private System.Windows.Forms.Button btReadMessage;
        private System.Windows.Forms.Button btWriteMessage;
        private System.Windows.Forms.TextBox tbMessage;
    }
}

