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
using Microsoft.Office.Interop.Word;
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
using Microsoft.Office.Interop;
using Microsoft.Office.Core;
using System.Threading;

namespace TIMES_Downloader
{
    public partial class ToPDF : Form
    {
        string saveLocation;
        private const string pdfFolder = "\\pdf-generated"; //just pdf is too generalized and a section name could be called pdf.
        private Thread thread;
        Boolean killThread = false;
        private static ToPDF _instance = null;
        public ToPDF(string saveLocation)
        {
            InitializeComponent();
            this.saveLocation = saveLocation;
            _instance = this;

        }

        private void Start_Click(object sender, EventArgs e)
        {
            Start.Enabled = false;
			btnStop.Enabled = true;
			killThread = false;
            string saveLocation = this.saveLocation + "\\download\\";
            string[] courses = Directory.GetDirectories(saveLocation);
            int courseCount = courses.Length;
            int documentCount = Directory.GetFiles(saveLocation, "*", SearchOption.AllDirectories).Length;
            int progressBarIncrement = 95 / documentCount;
            int fileCount = 0;

            label2.Text = "Found a total of " + documentCount + " files in " + courseCount + " courses.";
            label1.Text = "Starting conversion";

            progressBar1.Value = 4;

            thread = new Thread(() =>
            {
                if (killThread)
                    return;
                foreach (var course in courses)
                {
                    if (killThread)
                        continue;
                    if (Directory.Exists(course + pdfFolder))
                    {
                        MessageBox.Show("A pdf folder already exists in the course. Please delete it if you would like to regenerate pdf's : " + course + pdfFolder);
                        MainForm.error += "Can't convert course - pdf folder already exists: " + course + "\r\n";
                        continue;
                    }
                    foreach (var file in Directory.GetFiles(course, "*", SearchOption.AllDirectories))
                    {
                        if (killThread)
                            continue;
                        AddProgess(progressBarIncrement);
                        fileCount++;

                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string fileExt = Path.GetExtension(file);
                        string path = Path.GetDirectoryName(file);
                        string newPath = course + path.Replace(course, pdfFolder); //just adding pdf after the course folder. Have to do it each time so even if the files are inside nested directories, the structure won't be changed

                        SetLabelText(String.Format("Convert File {0} out of {1} : {2}", fileCount, documentCount, fileName), "");
                        if (!Directory.Exists(newPath))
                            Directory.CreateDirectory(newPath);

                        if (fileExt == ".pdf") //if it's already pdf skip
                        {
                            File.Copy(file, newPath + "\\" + fileName + fileExt);
                            continue;
                        }
                        string newFile = newPath + "\\" + fileName + ".pdf";
                        Boolean result = false;
                        switch (fileExt)
                        {
                            case ".pptx":
                            case ".ppt":
                                result = ConvertPowerpoint(file, newFile);
                                break;
                            case ".docx":
                            case ".doc":
                                result = ConvertWord(file, newFile);
                                break;
                            case ".xlsx":
                            case ".xls":
                                result = ConvertExcel(file, newFile);
                                break;
                            default:
                                MainForm.error += "New type of file? " + fileExt + " \r\n";
                                break;
                        }

                        if (!result)
                        {
                            MainForm.error += "Error when converting file. Continuing to next file. " + file + "  ---   " + newFile;
                        }
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
            
            while(thread.IsAlive)
            {
                System.Windows.Forms.Application.DoEvents();
            }

            Start.Enabled = true;
            btnStop.Enabled = false;
            label1.Text = "Completed";
            label2.Text = "";
            progressBar1.Value = 100;

            //foreach()
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure? You can't continue this process later. Click No to continue converting.", "Stop?", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                killThread = true;
                //this.Close();
            }
            else
            {
                //do Nothing
            }
        }

        private Boolean ConvertWord(string path, string newpath)
        {
            Boolean returnB = true; //so we can set all the objects to null before returning (whether or not there is an error)
            Microsoft.Office.Interop.Word.Application msWordDoc = null;
            Microsoft.Office.Interop.Word.Document doc = null;

            object oMissing = System.Reflection.Missing.Value;

            Object file = (object)path;
            Object newFile = (object)newpath;
            msWordDoc = new Microsoft.Office.Interop.Word.Application
            {
                Visible = false,
                ScreenUpdating = false
            };

            try
            {
                doc = msWordDoc.Documents.Open(ref file, ref oMissing
                                          , ref oMissing, ref oMissing, ref oMissing, ref oMissing
                                          , ref oMissing, ref oMissing, ref oMissing, ref oMissing
                                          , ref oMissing, ref oMissing, ref oMissing, ref oMissing
                                          , ref oMissing, ref oMissing);

                if (doc != null)
                {
                    doc.Activate();
                    // save Document as PDF
                    object fileFormat = WdSaveFormat.wdFormatPDF;
                    doc.SaveAs(ref newFile,
                    ref fileFormat, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing);
                }
                else
                {
                    MainForm.error = String.Format("Filename {0} - Unexpected error at ConvertWord \r\n", Path.GetFileName(path));
                    returnB = false;
                }
            }
            catch(Exception ex)
            {
                MainForm.error = String.Format("Filename {0} - {1} at ConvertWord \r\n", Path.GetFileName(path), ex.Message);
                returnB = false;
            }

            try
            {
                object saveChanges = WdSaveOptions.wdDoNotSaveChanges;
              
                doc.Close(ref saveChanges, ref oMissing, ref oMissing);
                msWordDoc.Quit();
                doc = null;
                msWordDoc = null;
            }
            catch(Exception ex)
            {
                MainForm.error = String.Format("Error when closing. Would recommend you to restart. Filename {0} - {1} at ConvertWord \r\n", Path.GetFileName(path), ex.Message);
                returnB = false;
            }
            return returnB;
        }

        private Boolean ConvertPowerpoint(string path, string newpath)
        {
            Boolean returnB = true;
            Microsoft.Office.Interop.PowerPoint.Application pptApplication = null;
            Microsoft.Office.Interop.PowerPoint.Presentation pptPresentation = null;

            object unknownType = Type.Missing;

            pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            try
            {
                pptPresentation = pptApplication.Presentations.Open(path,
                                Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoTrue,
                                Microsoft.Office.Core.MsoTriState.msoFalse);

                pptPresentation.ExportAsFixedFormat(newpath,
                Microsoft.Office.Interop.PowerPoint.PpFixedFormatType.ppFixedFormatTypePDF,
                Microsoft.Office.Interop.PowerPoint.PpFixedFormatIntent.ppFixedFormatIntentPrint,
                MsoTriState.msoFalse, Microsoft.Office.Interop.PowerPoint.PpPrintHandoutOrder.ppPrintHandoutVerticalFirst,
                Microsoft.Office.Interop.PowerPoint.PpPrintOutputType.ppPrintOutputSlides, MsoTriState.msoFalse, null,
                Microsoft.Office.Interop.PowerPoint.PpPrintRangeType.ppPrintAll, string.Empty, true, true, true,
                true, false, unknownType);

                if (pptPresentation == null)
                {
                    MainForm.error = String.Format("Filename {0} - Unexpected error at ConvertPowerpoint \r\n", Path.GetFileName(path));
                    returnB = false;
                }
            }
            catch (Exception ex)
            {
                MainForm.error = String.Format("Filename {0} - {1} at ConvertPowerpoint \r\n", Path.GetFileName(path), ex.Message);
                returnB = false;
            }
            pptPresentation.Close();
            pptPresentation = null;
            pptApplication.Quit();
            pptApplication = null;
            return returnB;
        }

        private Boolean ConvertExcel(string path, string newpath)
        {
            Boolean returnB = true;
            Microsoft.Office.Interop.Excel.Application excelApplication = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkbook = null;
            object unknownType = Type.Missing;

            excelApplication = new Microsoft.Office.Interop.Excel.Application
            {
                ScreenUpdating = false,
                DisplayAlerts = false
            };

            try
            {
                excelWorkbook = excelApplication.Workbooks.Open(path, unknownType, unknownType,
                                                                unknownType, unknownType, unknownType,
                                                                unknownType, unknownType, unknownType,
                                                                unknownType, unknownType, unknownType,
                                                                unknownType, unknownType, unknownType);

                excelWorkbook.ExportAsFixedFormat(Microsoft.Office.Interop.Excel.XlFixedFormatType.xlTypePDF,
                                                    newpath, unknownType, unknownType, unknownType, unknownType, unknownType,
                                                    unknownType, unknownType);
                if (excelWorkbook == null)
                {
                    MainForm.error = String.Format("Filename {0} - Unexpected error at ConvertPowerpoint \r\n", Path.GetFileName(path));
                    returnB = false;
                }
            }
            catch (Exception ex)
            {
                MainForm.error = String.Format("Filename {0} - {1} at ConvertPowerpoint \r\n", Path.GetFileName(path), ex.Message);
                returnB = false;
            }
            if (excelWorkbook != null)
                excelWorkbook.Close(unknownType, unknownType, unknownType);
            if(excelApplication != null)
                excelApplication.Quit();

            excelWorkbook = null;
            excelApplication = null;
            return returnB;
        }

        private void SetLabelText(string label1S, string label2S)
        {
            if (label1S != "")
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label1.Text = label1S;
                });

            }

            if (label2S != "")
            {
                this.Invoke((MethodInvoker)delegate
                {
                    label2.Text = label2S;
                });
            }
        }

        private void AddProgess(int val)
        {
         
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.Value += val;
            });
        }
    }
}
