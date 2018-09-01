/********************************************************************************
 * MIT License
 * 
 * Copyright (c) 2018 KKS21199
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 ********************************************************************************/
namespace TIMES_Downloader
{
    partial class MainForm
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
            this.password = new System.Windows.Forms.TextBox();
            this.username = new System.Windows.Forms.TextBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.downloadUrl = new System.Windows.Forms.TextBox();
            this.btnSaveLoc = new System.Windows.Forms.Button();
            this.saveLoc = new System.Windows.Forms.TextBox();
            this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.btnBrowser = new System.Windows.Forms.Button();
            this.btnError = new System.Windows.Forms.Button();
            this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
            this.Label1 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnBrowserMinimize = new System.Windows.Forms.Button();
            this.btnLicense = new System.Windows.Forms.Button();
            this.btnPDF = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // password
            // 
            this.password.AccessibleDescription = "Taylor\'s Student ID";
            this.password.Location = new System.Drawing.Point(242, 11);
            this.password.Name = "password";
            this.password.Size = new System.Drawing.Size(216, 20);
            this.password.TabIndex = 11;
            this.password.Text = "password";
            // 
            // username
            // 
            this.username.AccessibleDescription = "";
            this.username.Location = new System.Drawing.Point(20, 11);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(216, 20);
            this.username.TabIndex = 10;
            this.username.Text = "id";
            // 
            // btnDownload
            // 
            this.btnDownload.Enabled = false;
            this.btnDownload.Location = new System.Drawing.Point(472, 78);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(103, 31);
            this.btnDownload.TabIndex = 9;
            this.btnDownload.Text = "Download!!";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(472, 7);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(103, 27);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // downloadUrl
            // 
            this.downloadUrl.Location = new System.Drawing.Point(20, 83);
            this.downloadUrl.Name = "downloadUrl";
            this.downloadUrl.Size = new System.Drawing.Size(438, 20);
            this.downloadUrl.TabIndex = 7;
            this.downloadUrl.Text = "https://times.taylors.edu.my/course/view.php?id=65978";
            // 
            // btnSaveLoc
            // 
            this.btnSaveLoc.Location = new System.Drawing.Point(472, 41);
            this.btnSaveLoc.Name = "btnSaveLoc";
            this.btnSaveLoc.Size = new System.Drawing.Size(103, 31);
            this.btnSaveLoc.TabIndex = 13;
            this.btnSaveLoc.Text = "Choose Directory";
            this.btnSaveLoc.UseVisualStyleBackColor = true;
            this.btnSaveLoc.Click += new System.EventHandler(this.btnSaveLoc_Click);
            // 
            // saveLoc
            // 
            this.saveLoc.Enabled = false;
            this.saveLoc.Location = new System.Drawing.Point(20, 46);
            this.saveLoc.Name = "saveLoc";
            this.saveLoc.Size = new System.Drawing.Size(438, 20);
            this.saveLoc.TabIndex = 12;
            // 
            // RichTextBox1
            // 
            this.RichTextBox1.Location = new System.Drawing.Point(20, 129);
            this.RichTextBox1.Name = "RichTextBox1";
            this.RichTextBox1.ReadOnly = true;
            this.RichTextBox1.Size = new System.Drawing.Size(272, 145);
            this.RichTextBox1.TabIndex = 14;
            this.RichTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(298, 129);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(277, 145);
            this.richTextBox2.TabIndex = 15;
            this.richTextBox2.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.webBrowser1);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(20, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 179);
            this.panel1.TabIndex = 0;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(7, 8);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(540, 163);
            this.webBrowser1.TabIndex = 0;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // btnBrowser
            // 
            this.btnBrowser.Location = new System.Drawing.Point(472, 280);
            this.btnBrowser.Name = "btnBrowser";
            this.btnBrowser.Size = new System.Drawing.Size(103, 23);
            this.btnBrowser.TabIndex = 16;
            this.btnBrowser.Text = "Maximize Browser";
            this.btnBrowser.UseVisualStyleBackColor = true;
            this.btnBrowser.Click += new System.EventHandler(this.btnBrowser_Click);
            // 
            // btnError
            // 
            this.btnError.Location = new System.Drawing.Point(20, 280);
            this.btnError.Name = "btnError";
            this.btnError.Size = new System.Drawing.Size(103, 31);
            this.btnError.TabIndex = 17;
            this.btnError.Text = "Save error to file";
            this.btnError.UseVisualStyleBackColor = true;
            this.btnError.Click += new System.EventHandler(this.btnError_Click);
            // 
            // LinkLabel1
            // 
            this.LinkLabel1.AutoSize = true;
            this.LinkLabel1.Location = new System.Drawing.Point(469, 495);
            this.LinkLabel1.Name = "LinkLabel1";
            this.LinkLabel1.Size = new System.Drawing.Size(146, 13);
            this.LinkLabel1.TabIndex = 12;
            this.LinkLabel1.TabStop = true;
            this.LinkLabel1.Text = "Check out TheTechTerminus";
            this.LinkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(2, 495);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(105, 13);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Made by Sugavanas";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripProgressBar1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 510);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(610, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(31, 22);
            this.toolStripLabel1.Text = "Start";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 22);
            // 
            // btnBrowserMinimize
            // 
            this.btnBrowserMinimize.Enabled = false;
            this.btnBrowserMinimize.Location = new System.Drawing.Point(363, 280);
            this.btnBrowserMinimize.Name = "btnBrowserMinimize";
            this.btnBrowserMinimize.Size = new System.Drawing.Size(103, 23);
            this.btnBrowserMinimize.TabIndex = 19;
            this.btnBrowserMinimize.Text = "Minimize Browser";
            this.btnBrowserMinimize.UseVisualStyleBackColor = true;
            this.btnBrowserMinimize.Click += new System.EventHandler(this.btnBrowserMinimize_Click);
            // 
            // btnLicense
            // 
            this.btnLicense.Location = new System.Drawing.Point(242, 495);
            this.btnLicense.Name = "btnLicense";
            this.btnLicense.Size = new System.Drawing.Size(75, 23);
            this.btnLicense.TabIndex = 20;
            this.btnLicense.Text = "License";
            this.btnLicense.UseVisualStyleBackColor = true;
            this.btnLicense.Click += new System.EventHandler(this.btnLicense_Click);
            // 
            // btnPDF
            // 
            this.btnPDF.Location = new System.Drawing.Point(133, 284);
            this.btnPDF.Name = "btnPDF";
            this.btnPDF.Size = new System.Drawing.Size(103, 23);
            this.btnPDF.TabIndex = 21;
            this.btnPDF.Text = "PDF Converter";
            this.btnPDF.UseVisualStyleBackColor = true;
            this.btnPDF.Click += new System.EventHandler(this.btnPDF_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 535);
            this.Controls.Add(this.btnLicense);
            this.Controls.Add(this.btnBrowserMinimize);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.LinkLabel1);
            this.Controls.Add(this.btnError);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnBrowser);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.RichTextBox1);
            this.Controls.Add(this.btnSaveLoc);
            this.Controls.Add(this.saveLoc);
            this.Controls.Add(this.password);
            this.Controls.Add(this.username);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.downloadUrl);
            this.Controls.Add(this.btnPDF);
            this.MaximumSize = new System.Drawing.Size(626, 574);
            this.MinimumSize = new System.Drawing.Size(626, 574);
            this.Name = "MainForm";
            this.Text = "Downloader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox password;
        internal System.Windows.Forms.TextBox username;
        internal System.Windows.Forms.Button btnDownload;
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.TextBox downloadUrl;
        internal System.Windows.Forms.Button btnSaveLoc;
        internal System.Windows.Forms.TextBox saveLoc;
        internal System.Windows.Forms.RichTextBox RichTextBox1;
        internal System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnBrowser;
        internal System.Windows.Forms.Button btnError;
        internal System.Windows.Forms.LinkLabel LinkLabel1;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        internal System.Windows.Forms.Button btnBrowserMinimize;
        private System.Windows.Forms.Button btnLicense;
        internal System.Windows.Forms.Button btnPDF;
    }
}

