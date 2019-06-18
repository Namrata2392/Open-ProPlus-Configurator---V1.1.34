using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace recentlyList
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// how many list will save
        /// </summary>
        const int MRUnumber = 6;
        System.Collections.Generic.Queue<string> MRUlist = new Queue<string>();
        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadRecentList();
            foreach (string  item in MRUlist )
            {
                ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);  //create new menu for each item in list
                recentToolStripMenuItem.DropDownItems.Add(fileRecent); //add the menu to "recent" menu
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }
        /// <summary>
        /// store a list to file and refresh list
        /// </summary>
        /// <param name="path"></param>
        private void SaveRecentFile(string path)
        {
            recentToolStripMenuItem.DropDownItems.Clear(); //clear all recent list from menu
            LoadRecentList(); //load list from file
            if (!(MRUlist.Contains(path))) //prevent duplication on recent list
                MRUlist.Enqueue(path); //insert given path into list
            while (MRUlist.Count > MRUnumber ) //keep list number not exceeded given value
            {
                MRUlist.Dequeue();
            }
            foreach (string  item in MRUlist )
            {
                ToolStripMenuItem fileRecent = new ToolStripMenuItem(item, null, RecentFile_click);  //create new menu for each item in list
                recentToolStripMenuItem.DropDownItems.Add(fileRecent); //add the menu to "recent" menu
            }
            //writing menu list to file
            StreamWriter stringToWrite = new StreamWriter(System.Environment.CurrentDirectory + "\\Recent.txt"); //create file called "Recent.txt" located on app folder
            foreach (string  item in MRUlist )
            {
                stringToWrite.WriteLine(item); //write list to stream
            }
            stringToWrite.Flush(); //write stream to file
            stringToWrite.Close(); //close the stream and reclaim memory
        }
        /// <summary>
        /// load recent file list from file
        /// </summary>
        private void LoadRecentList()
        {//try to load file. If file isn't found, do nothing
            MRUlist.Clear();
            try
            {
                StreamReader listToRead = new StreamReader(System.Environment.CurrentDirectory + "\\Recent.txt"); //read file stream
                string line;
                while ((line = listToRead.ReadLine()) != null) //read each line until end of file
                    MRUlist.Enqueue(line); //insert to list
                listToRead.Close(); //close the stream
            }
            catch (Exception)
            {
                
                //throw;
            }

        }
        /// <summary>
        /// click menu handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecentFile_click(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(sender.ToString (), RichTextBoxStreamType.PlainText); //same as open menu
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.LoadFile(openFileDialog1.FileName,RichTextBoxStreamType.PlainText ); //open file
                SaveRecentFile(openFileDialog1.FileName); //insert to list so that opened file will shown on the list
            }
        }
    }
}
