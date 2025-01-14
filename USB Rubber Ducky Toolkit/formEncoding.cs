﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USB_Rubber_Ducky_Toolkit
{
    public partial class formEncoding : Form
    {
        public formEncoding()
        {
            InitializeComponent();
        }
        //Varibles
        string FilePath = "";
        string savePathLocation = "";
        string outPutFilePath = "";
        //string userCode = MainForm.SendDuckyLocation();
        string directoryPath = "";
        //BUTTONS
        

        private void btnOutputButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                outPutFilePath = folderBrowserDialog1.SelectedPath;
                btnEncode.Enabled = true;
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            EncodeToBin();
        }
        //END OF BUTTONS
        private void EncodeToBin()
        {
            string outputfilename = txtboxFileName.Text;
            //start cmd and run java file passing duckyscript to it
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.RedirectStandardError = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("java -jar duckencode.jar -i \"" + "helloworld.txt" + "\"");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();

            Console.Out.WriteLine($"Output: {cmd.StandardOutput.ReadToEnd()}");
            Console.Out.WriteLine($"Error: {cmd.StandardError.ReadToEnd()}");

            outPutFilePath = Path.Combine(directoryPath, outputfilename);
            Console.WriteLine(outPutFilePath);
            MessageBox.Show(File.Exists("inject.bin") ? "Bin file created sucessfully." : "Error creating file.");
        }
        public void getScriptDirectory(string directoryScriptPath)
        {
            directoryPath = directoryScriptPath;
        }
    }
}
