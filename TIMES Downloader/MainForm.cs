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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using System.Threading;
using System.Net;
using System.Diagnostics;

namespace TIMES_Downloader
{
    public partial class MainForm : Form
    {
        public static string error; //static gets the job done for now
        private Boolean loaded = false; //used by webbrowser control
        private Thread thread;

		Boolean killThread = false;

		public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
			btnRefresh.Enabled = false;
            btnDownload.Enabled = false;
			btnRefreshStop.Enabled = false;
			btnSaveLoc.Enabled = false;
			btnLogin.Enabled = true;
			

            License l = new License();
            l.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Application.Exit();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Login(username.Text, password.Text))
            {
                toolStripProgressBar1.Value = 0;
				btnSaveLoc.Enabled = true;
				btnDownload.Enabled = true;
                btnLogin.Enabled = false;
            }
            else
            {
                toolStripProgressBar1.Value = 0;
                btnDownload.Enabled = false;
                btnLogin.Enabled = true;
                MessageBox.Show("Invalid Login");
            }
        }

        private void btnSaveLoc_Click(object sender, EventArgs e)
        {
            string folderPath = "";
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderPath = folderBrowserDialog1.SelectedPath;
            }

            try
            {
                Directory.CreateDirectory(folderPath + "\\timesdownloaderblabla543");
                Directory.Delete(folderPath + "\\timesdownloaderblabla543");
            }
            catch (Exception)
            {
                MessageBox.Show("Try choosing a directory that the program can write to. Documents/Desktop");
                return;
            }

            if (Directory.Exists(folderPath + "\\download"))
            {
                MessageBox.Show("The program found old files in the folder you chose. To resume downloads or update click the Update button.");
				btnRefresh.Enabled = true;
            }
			else
				btnRefresh.Enabled = false;

			saveLoc.Text = folderPath;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            btnSaveLoc.Enabled = false;
            btnDownload.Enabled = false;
            btnPDF.Enabled = false;

            if (DownloadFiles())
            {
                MessageBox.Show("Download Complete.");
                toolStripLabel1.Text = "Download Complete";
            }
            else
            {
                MessageBox.Show("ERROR");
                toolStripLabel1.Text = "Error";
            }

            toolStripProgressBar1.Value = 0;
            
            btnSaveLoc.Enabled = true;
            btnDownload.Enabled = true;
            btnPDF.Enabled = true;
        }

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			btnDownload.Enabled = false;
			btnLogin.Enabled = false;
			btnPDF.Enabled = false;
			btnRefresh.Enabled = false;
			btnSaveLoc.Enabled = false;
			btnRefreshStop.Enabled = true;
			killThread = false;

			string saveLocation = saveLoc.Text + "\\download\\";
			string[] courses = Directory.GetDirectories(saveLocation);

			string url = "https://times.taylors.edu.my/course/view.php?id=";
			
			foreach (var course in courses)
			{
				if (killThread)
					continue;

				string courseID = Path.GetFileName(course).Split('-').Last().Trim();

				if(courseID == null)
				{
					error += "Skipping directory " + course + " as couldn't find courseID \r\n";
					continue;
				}

				Boolean result = RefreshFiles(url + courseID, course);
				if(!result)
					error += "Error updating course " + url + courseID + "  Make sure you still have access to the course. \r\n";

			}

			btnDownload.Enabled = true;
			btnLogin.Enabled = true;
			btnPDF.Enabled = true;
			btnRefresh.Enabled = true;
			btnSaveLoc.Enabled = true;
			btnRefreshStop.Enabled = false;
			killThread = false;

			toolStripProgressBar1.Value = 0;
			toolStripLabel1.Text = "All files updated.";
		}

		private void btnRefreshStop_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure? Click No to continue downloading.", "Stop?", MessageBoxButtons.YesNo);
			if (dialogResult == DialogResult.Yes)
			{
				killThread = true;
			}
			else
			{
				
			}
		}

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            btnBrowser.Enabled = false;
            btnBrowserMinimize.Enabled = true;
            panel1.Dock = DockStyle.Fill;
        }

        private void btnBrowserMinimize_Click(object sender, EventArgs e)
        {
            btnBrowser.Enabled = true;
            btnBrowserMinimize.Enabled = false;
            panel1.Dock = DockStyle.None;
        }

        private void btnLicense_Click(object sender, EventArgs e)
        {
            License l = new License();
            l.ShowDialog();
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This feature isn't stable. I recommend using some other dedicated tool to do this. If you really want to continue, please read the license again. Press OK to continue", "Sure?", MessageBoxButtons.OKCancel);

            if (dialogResult == DialogResult.Cancel)
                return;
            else
            {
                License l = new License();
                l.ShowDialog();
                MessageBox.Show("Atleast restart your computer to clear out orphaned processes once you are done.");
            }
          
            String saveLocation = saveLoc.Text;

            if (saveLocation == "" || !Directory.Exists(saveLocation + "\\download"))
            {
                MessageBox.Show("Choose a directory that contains the download folder.");
                return;
            }

            ToPDF toPDF = new ToPDF(saveLocation);
            toPDF.ShowDialog();
        }

		private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://thetechterminus.com");
        }

        private void btnError_Click(object sender, EventArgs e)
        {
            File.AppendAllText(saveLoc.Text + "\\" + "error.log", error);
            MessageBox.Show("Saved file to" + saveLoc.Text + "\\" + "error.log .");
        }

		private Boolean Login(string username, string password)
        {
            toolStripLabel1.Text = "Logging in... WAIT";
            toolStripProgressBar1.Value = 5;

            GetBodyFromURL("https://times.taylors.edu.my/login/index.php");

            toolStripProgressBar1.Value = 25;
            toolStripLabel1.Text = "Page loaded. Logging in";

            webBrowser1.Document.GetElementById("username").SetAttribute("value", username);
            webBrowser1.Document.GetElementById("password").SetAttribute("value", password);

            toolStripProgressBar1.Value = 35;

            HtmlElement btn = webBrowser1.Document.GetElementById("login");
            btn.InvokeMember("submit");

            toolStripProgressBar1.Value = 65;
            loaded = false;
            LoadPage();

            toolStripLabel1.Text = "Page loaded.";
            toolStripProgressBar1.Value = 99;

            return !(webBrowser1.Document.Body.InnerHtml.Contains("Forgotten your username or password?"));
        }

        private String GetBodyFromURL(string URL)
        {
            loaded = false;
            webBrowser1.Navigate(URL);   
            LoadPage();
            return webBrowser1.Document.Body.InnerHtml;
        }

        private Boolean LoadPage()
        {
            while (!loaded)
            {
                Application.DoEvents();
            }
            return true;
        }

		private Boolean DownloadFiles()
        {
			String saveLocation = saveLoc.Text;
            string folderName = "";
      
            List<DownloadItem> downloadItems = new List<DownloadItem>();

            if (saveLocation == "")
            {
                MessageBox.Show("Please choose a directory.");
                return false;
            }

            toolStripLabel1.Text = "Loading... WAIT";
            toolStripProgressBar1.Value = 5;

            string body = GetBodyFromURL(downloadUrl.Text);
            File.WriteAllText(saveLocation + "/temp.html", body);
            if (!body.Contains("Your progress"))
                return false;

            var doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(saveLocation + "/temp.html");
            
            toolStripProgressBar1.Value = 25;
            toolStripLabel1.Text = "Page loaded. Fetching url";

            foreach(var div in doc.DocumentNode.SelectNodes("//div[@class='page-header-headings']"))
            {
                folderName = div.InnerText.Trim();
            }

            Uri myUri = new Uri(downloadUrl.Text);
            folderName = folderName + " - " + System.Web.HttpUtility.ParseQueryString(myUri.Query).Get("id");
            folderName = folderName.Trim();
            folderName = GetSafeFilename(folderName);
            if (folderName == null || folderName == "") 
                return false; //very bad design?

            if (Directory.Exists(saveLocation + "\\download\\" + folderName))
            {
                MessageBox.Show("Can't download course that was already downloaded. Please delete the old folder to redownload again or choose a different location. " + saveLocation + "\\download\\" + folderName);
                return false;
            }

			if (doc.DocumentNode.SelectNodes("//div[@class='course-content']") != null) {

				foreach (var div in doc.DocumentNode.SelectNodes("//div[@class='course-content']"))
				{
					foreach (var ul in div.Elements("ul"))
					{
						foreach (var section in ul.Elements("li"))
						{
							foreach (var itemLi in section.Descendants("li"))
							{
								foreach (var itemLink in itemLi.Descendants("a"))
								{
									if (!itemLink.GetAttributeValue("href", "nolink").Contains("resource/view.php?id"))
									{
										error += "Skipping Link: " + itemLink.GetAttributeValue("href", "nolink") + "\r\n";
										continue;
									}

									if (itemLink.Element("span").HasClass("instancename"))
									{
										downloadItems.Add(new DownloadItem(itemLink.Element("span").InnerText, section.GetAttributeValue("aria-label", "all"), itemLink.GetAttributeValue("href", "nolink"))); //null changed to nolink
										RichTextBox1.AppendText("Found file: " + itemLink.Element("span").InnerText + " \r\n");
									}

								}
							}
						}
					}
				}
			} else {
				MessageBox.Show("Course type not supported.");
				return false;
			}
            

            if (downloadItems.Count < 1)
			{
				MessageBox.Show("Course type not supported.");
				return false;
			}

			toolStripProgressBar1.Value = 35;
            toolStripLabel1.Text = "All links fetched... Now downloading... This may take a while...";

            var incrementProgress = 65 / downloadItems.Count;
            string cookies = GetCookies();
			string path = saveLocation + "\\download\\" + folderName;

			string historyFile = path +  "\\.download_history";

			List<string> history = new List<string>();
            thread = new Thread(() =>
            {
                foreach (var dlItem in downloadItems)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        toolStripProgressBar1.Value += incrementProgress;
                    });
                    this.Invoke((MethodInvoker)delegate
                    {
                        richTextBox2.AppendText("Downloading file: " + dlItem.name + "\r\n");
                    });
					//getBodyFromURL(dlItem.url);
					history.Add(dlItem.folder + "\\" + dlItem.name);
                    DownloadInit(dlItem, cookies, path);
                }
            });
            thread.IsBackground = true;
            thread.Start();

            while(thread.IsAlive)
            {
                Application.DoEvents();
            }

			using (TextWriter tw = new StreamWriter(historyFile))
			{
				foreach (String s in history)
					tw.WriteLine(s);
			}

			File.Delete(saveLoc.Text + "/temp.html");

			toolStripProgressBar1.Value = 0;
            toolStripLabel1.Text = "Completed. Saved to: " +  saveLocation + "\\download\\" + folderName;
            return true;
        }

		//TODO: Merge DownloadFiles() and RefreshFiles() into one function.
		private Boolean RefreshFiles(string URL, string saveLocation)
		{

			if (killThread)
				return false;

			List<DownloadItem> downloadItems = new List<DownloadItem>();

			toolStripLabel1.Text = "Loading... WAIT";
			toolStripProgressBar1.Value = 5;

			string body = GetBodyFromURL(URL);
			File.WriteAllText(saveLoc.Text + "/temp.html", body);
			if (!body.Contains("Your progress"))
				return false;

			var doc = new HtmlAgilityPack.HtmlDocument();
			doc.Load(saveLoc.Text + "/temp.html");

			toolStripProgressBar1.Value = 25;
			toolStripLabel1.Text = "Page loaded. Fetching url";

			foreach (var div in doc.DocumentNode.SelectNodes("//div[@class='page-header-headings']"))
			{
				//folderName = div.InnerText.Trim(); //TODO change folder name if the course name has changed.
			}

			if (doc.DocumentNode.SelectNodes("//div[@class='course-content']") != null)
			{
				foreach (var div in doc.DocumentNode.SelectNodes("//div[@class='course-content']"))
				{
					foreach (var ul in div.Elements("ul"))
					{
						foreach (var section in ul.Elements("li"))
						{
							foreach (var itemLi in section.Descendants("li"))
							{
								foreach (var itemLink in itemLi.Descendants("a"))
								{
									if (!itemLink.GetAttributeValue("href", "nolink").Contains("resource/view.php?id"))
									{
										error += "Skipping Link: " + itemLink.GetAttributeValue("href", "nolink") + "\r\n";
										continue;
									}

									if (itemLink.Element("span").HasClass("instancename"))
									{
										downloadItems.Add(new DownloadItem(itemLink.Element("span").InnerText, section.GetAttributeValue("aria-label", "all"), itemLink.GetAttributeValue("href", "nolink"))); //null changed to nolink
										RichTextBox1.AppendText("Found file: " + itemLink.Element("span").InnerText + " \r\n");
									}

								}
							}
						}
					}
				}
			}
			else
			{
				error += "Course not supported anymore. " + URL + " \r\n";
				return false;
			}


			if (downloadItems.Count < 1)
				return false;

			toolStripProgressBar1.Value = 35;
			toolStripLabel1.Text = "All links fetched... Now downloading... This may take a while...";

			var incrementProgress = 65 / downloadItems.Count;
			string cookies = GetCookies();

			
			string historyFile = saveLocation + "\\.download_history";
			string[] history = null;
			Boolean validHistory = true;

			if (File.Exists(historyFile))
				 history = File.ReadAllLines(historyFile);
			else
				validHistory = false;

			List<string> newHistory = new List<string>();

			if (validHistory && !(history.Length > 0))
				validHistory = false;

			thread = new Thread(() =>
			{
				foreach (var dlItem in downloadItems)
				{
					if (killThread)
						continue;

					this.Invoke((MethodInvoker)delegate
					{
						toolStripProgressBar1.Value += incrementProgress;
					});
					
					newHistory.Add(dlItem.folder + "\\" + dlItem.name);

					if (validHistory)
					{
						var index = Array.FindIndex(history, x => x == (dlItem.folder + "\\" + dlItem.name));
						if (index > -1)
						{
							continue; // file already downloaded
						}
					}

					this.Invoke((MethodInvoker)delegate
					{
						richTextBox2.AppendText("Downloading file: " + dlItem.name + "\r\n");
					});


					//getBodyFromURL(dlItem.url);
					DownloadInit(dlItem, cookies, saveLocation);
				}
			});
			thread.IsBackground = true;
			thread.Start();

			while (thread.IsAlive)
			{
				Application.DoEvents();
			}

			using (TextWriter tw = new StreamWriter(historyFile))
			{
				foreach (String s in newHistory)
					tw.WriteLine(s);
			}

			File.Delete(saveLoc.Text + "/temp.html");

			return true;
		}

        private string GetCookies()
        {
            if (webBrowser1.InvokeRequired)
            {
                return (string)webBrowser1.Invoke(new Func<string>(() => GetCookies()));
            }
            else
            {
                return webBrowser1.Document.Cookie;
            }
        }

        private Boolean DownloadInit(DownloadItem item, string cookies, string saveLocation)
        {
            string tempNameHolder = "none";
            try
            {
                WebClient wc = new WebClient();
                wc.Headers.Add("Cookie: " + cookies);

                string tmpFile = Path.GetTempFileName();
                string fileName = "";
                wc.DownloadFile(item.url, tmpFile);

                string header = wc.ResponseHeaders["Content-Disposition"] ?? string.Empty;
                const string constFilename = "filename=";
                int index = header.LastIndexOf(constFilename, StringComparison.OrdinalIgnoreCase);
                if (index > -1)
                {
                    fileName = header.Substring(index + constFilename.Length);
                    fileName = fileName.Replace('"', ' ').Trim();
                    tempNameHolder = fileName;
                }

                if(fileName == "")
                {
                    fileName = item.name + ".ext";
                    error += "File Name was empty. Renamed File to: " + fileName + " Check extension. \r\n";
                }

                string path = saveLocation + "\\" + GetSafeFilename(item.folder) + "\\";
                fileName = GetSafeFilename(fileName);
                tempNameHolder = path + fileName;

                //MessageBox.Show(path + fileName);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

				CheckFileName(path, ref fileName);

                File.Move(tmpFile, path + fileName);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please check error file for more detailed error. \r\n " + ex.Message);
                error += "Couldn't download: " + item.name + " --- " + item.url + " ---- " + tempNameHolder + "\r\n";
                return false;
            }
        }

        private string GetSafeFilename(string filename)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '_');
            }
            return filename;
        }

		/**
		 * Check if file exists and if it exists, add a number to the file. 
		 */
		private void CheckFileName(string path, ref string fileName)
		{
			int i = 0;
			string name = Path.GetFileNameWithoutExtension(path + fileName);
			string ext = Path.GetExtension(path + fileName);

			while (File.Exists(path + fileName)) //if file doesn't exist, then no need to modify the fileName
			{
				i++;
				fileName = name + " - " + i.ToString() + ext;
			}
		}
	}
}
