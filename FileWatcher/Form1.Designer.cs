namespace FileWatcher
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
            this.tbFilesFound = new System.Windows.Forms.TextBox();
            this.btStart = new System.Windows.Forms.Button();
            this.tbFoldertoWatch = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btRead = new System.Windows.Forms.Button();
            this.tbRead = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbRead = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbFilesFound
            // 
            this.tbFilesFound.Location = new System.Drawing.Point(29, 51);
            this.tbFilesFound.Multiline = true;
            this.tbFilesFound.Name = "tbFilesFound";
            this.tbFilesFound.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbFilesFound.Size = new System.Drawing.Size(264, 181);
            this.tbFilesFound.TabIndex = 0;
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(504, 290);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(75, 23);
            this.btStart.TabIndex = 1;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // tbFoldertoWatch
            // 
            this.tbFoldertoWatch.Location = new System.Drawing.Point(29, 293);
            this.tbFoldertoWatch.Name = "tbFoldertoWatch";
            this.tbFoldertoWatch.Size = new System.Drawing.Size(381, 20);
            this.tbFoldertoWatch.TabIndex = 2;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(420, 291);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 23);
            this.btBrowse.TabIndex = 3;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btRead
            // 
            this.btRead.Location = new System.Drawing.Point(504, 243);
            this.btRead.Name = "btRead";
            this.btRead.Size = new System.Drawing.Size(75, 23);
            this.btRead.TabIndex = 4;
            this.btRead.Text = "Read";
            this.btRead.UseVisualStyleBackColor = true;
            this.btRead.Click += new System.EventHandler(this.btRead_Click);
            // 
            // tbRead
            // 
            this.tbRead.Location = new System.Drawing.Point(299, 51);
            this.tbRead.Multiline = true;
            this.tbRead.Name = "tbRead";
            this.tbRead.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbRead.Size = new System.Drawing.Size(280, 181);
            this.tbRead.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Added to Queue";
            // 
            // lbRead
            // 
            this.lbRead.AutoSize = true;
            this.lbRead.Location = new System.Drawing.Point(302, 35);
            this.lbRead.Name = "lbRead";
            this.lbRead.Size = new System.Drawing.Size(91, 13);
            this.lbRead.TabIndex = 7;
            this.lbRead.Text = "Read from Queue";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(611, 342);
            this.Controls.Add(this.lbRead);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRead);
            this.Controls.Add(this.btRead);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.tbFoldertoWatch);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.tbFilesFound);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilesFound;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TextBox tbFoldertoWatch;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btRead;
        private System.Windows.Forms.TextBox tbRead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbRead;
    }
}

