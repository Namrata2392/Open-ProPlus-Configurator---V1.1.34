
// Created By : Namrata
// Date : 28/12/2017
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;
using System.IO;
namespace OpenProPlusConfigurator
{
    public class MruList
    {
        #region Decalration
        private string ApplicationName;
        private int NumFiles;
        private List<FileInfo> FileInfos;
        private ToolStripMenuItem MyMenu;
        private ToolStripSeparator Separator;
        private ToolStripMenuItem[] MenuItems;
        public delegate void FileSelectedEventHandler(string file_name);
        public event FileSelectedEventHandler FileSelected;
        private Action<object, EventArgs> OnClearRecentFilesClick;
        ToolStripMenuItem tsi = new ToolStripMenuItem();
        #endregion Decalration
        public MruList(string application_name, ToolStripMenuItem menu, int num_files, Action<object, EventArgs> onClearRecentFilesClick = null)
        {
            string strRoutineName = "MruList";
            try
            {
                ApplicationName = application_name;
                MyMenu = menu;
                NumFiles = num_files;
                FileInfos = new List<FileInfo>();
                this.OnClearRecentFilesClick = onClearRecentFilesClick;
                Separator = new ToolStripSeparator();
                Separator.Visible = false;
                this.MyMenu.DropDownItems.Clear();
                MenuItems = new ToolStripMenuItem[NumFiles + 1];
                for (int i = 0; i < NumFiles; i++)
                {
                    MenuItems[i] = new ToolStripMenuItem();
                    MenuItems[i].Visible = false;
                    MyMenu.DropDownItems.Add(MenuItems[i]);
                }
                LoadFiles();
                ShowFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Namrata: 29/01/2018
        // After Save as refresh fileName in RecentList 
        public MruList(string application_name, ToolStripMenuItem menu, int num_files, string FileName, Action<object, EventArgs> onClearRecentFilesClick = null)
        {
            string strRoutineName = "MruList";
            try
            {
                ApplicationName = application_name;
                MyMenu = menu;
                NumFiles = num_files;
                FileName = Utils.XMLUpdatedFileName;
                FileInfos = new List<FileInfo>();
                this.OnClearRecentFilesClick = onClearRecentFilesClick;
                Separator = new ToolStripSeparator();
                Separator.Visible = false;
                this.MyMenu.DropDownItems.Clear();
                MenuItems = new ToolStripMenuItem[NumFiles + 1];
                for (int i = 0; i < NumFiles; i++)
                {
                    MenuItems[i] = new ToolStripMenuItem();
                    MenuItems[i].Visible = false;
                    MyMenu.DropDownItems.Add(MenuItems[i]);
                }
                LoadFilesAfterSaveAs();
                ShowFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Tsi_Click(object sender, EventArgs e)
        {
            string strRoutineName = "Tsi_Click";
            try
            {
                try
                {
                    RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
                    RegistryKey rK = reg_key.CreateSubKey(Application.ProductName);
                    if (rK == null)
                        return;
                    string[] values = rK.GetValueNames();
                    foreach (string valueName in values)
                        rK.DeleteValue(valueName, true);
                    rK.Close();
                    MyMenu.DropDownItems.Clear();
                    MyMenu.Enabled = false;
                    FileInfos.Clear();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                if (OnClearRecentFilesClick != null)
                    this.OnClearRecentFilesClick(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadFiles()
        {
            string strRoutineName = "LoadFiles";
            try
            {
                for (int i = 0; i < NumFiles; i++)
                {
                    string file_name = (string)RegistryTools.GetSetting(ApplicationName, "FilePath" + i.ToString(), "");

                    if (file_name != "")
                    {
                        FileInfos.Add(new FileInfo(file_name));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadFilesAfterSaveAs()
        {
            string strRoutineName = "LoadFilesAfterSaveAs";
            try
            {
                RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
                RegistryKey myKey = reg_key.CreateSubKey(Application.ProductName);

                for (int i = 0; i < NumFiles; i++)
                {
                    string file_name = (string)RegistryTools.GetSetting(ApplicationName, "FilePath" + i.ToString(), "");
                    if (file_name != "")
                    {
                        if (!FileInfos.OfType<FileInfo>().Where(x => x.FullName == Utils.XMLUpdatedFileName).Any())
                        {
                            FileInfos.Add(new FileInfo(Utils.XMLUpdatedFileName));
                            myKey.SetValue("FilePath" + i.ToString(), Utils.XMLUpdatedFileName);
                            //myKey.Close();
                        }
                        else
                        {
                            FileInfos.Add(new FileInfo(file_name));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveFiles()
        {
            string strRoutineName = "SaveFiles";
            try
            {
                for (int i = 0; i < NumFiles; i++)
                {
                    RegistryTools.DeleteSetting(ApplicationName, "FilePath" + i.ToString());
                }
                int index = 0;
                foreach (FileInfo file_info in FileInfos)
                {
                    RegistryTools.SaveSetting(ApplicationName, "FilePath" + index.ToString(), file_info.FullName);
                    index++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void RemoveFileInfo(string file_name)
        {
            string strRoutineName = "RemoveFileInfo";
            try
            {
                for (int i = FileInfos.Count - 1; i >= 0; i--)
                {
                    if (FileInfos[i].FullName == file_name) FileInfos.RemoveAt(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void AddFile(string file_name)
        {
            string strRoutineName = "AddFile";
            try
            {
                RemoveFileInfo(file_name);
                FileInfos.Insert(0, new FileInfo(file_name));
                if (FileInfos.Count > NumFiles) FileInfos.RemoveAt(NumFiles);
                this.MyMenu.DropDownItems.Clear();
                tsi.Text = "Clear List";
                MenuItems = new ToolStripMenuItem[NumFiles + 1];
                if (!tsi.DropDownItems.OfType<ToolStripItem>().Where(x => x.Text == "Clear List").Any())
                {
                    for (int i = 0; i < NumFiles; i++)
                    {
                        MenuItems[i] = new ToolStripMenuItem();
                        MenuItems[i].Visible = false;
                        MyMenu.DropDownItems.Add(MenuItems[i]);
                    }
                    MyMenu.DropDownItems.Add(tsi);
                    tsi.Click += Tsi_Click;
                    MyMenu.Enabled = true;
                }
                ShowFiles();
                SaveFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RemoveFile(string file_name)
        {
            string strRoutineName = "RemoveFile";
            try
            {
                RemoveFileInfo(file_name);
                ShowFiles();
                SaveFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ShowFiles()
        {
            string strRoutineName = "ShowFiles";
            try
            {
                Separator.Visible = (FileInfos.Count > 0);
                for (int i = 0; i < FileInfos.Count; i++)
                {
                    MenuItems[i].Text = string.Format("&{0} {1}", i + 1, FileInfos[i].FullName);
                    MenuItems[i].Visible = true;
                    MenuItems[i].Tag = FileInfos[i];
                    MenuItems[i].Click -= File_Click;
                    MenuItems[i].Click += File_Click;
                }
                for (int i = FileInfos.Count; i < NumFiles; i++)
                {
                    MenuItems[i].Visible = false;
                    MenuItems[i].Click -= File_Click;
                }
                tsi.Text = "Clear List";
                if (!tsi.DropDownItems.OfType<ToolStripItem>().Where(x => x.Text == "Clear List").Any())
                {
                    if (FileInfos.Count > 0)
                    {
                        MyMenu.DropDownItems.Add("-");
                        MyMenu.DropDownItems.Add(tsi);
                        tsi.Click += Tsi_Click;
                        MyMenu.Enabled = true;
                    }
                    else
                    {
                        this.MyMenu.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void File_Click(object sender, EventArgs e)
        {
            string strRoutineName = "File_Click";
            try
            {
                if (FileSelected != null)
                {
                    ToolStripMenuItem menu_item = sender as ToolStripMenuItem;
                    FileInfo file_info = menu_item.Tag as FileInfo;
                    FileSelected(file_info.FullName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(strRoutineName + ": " + "Error: " + ex.Message.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
