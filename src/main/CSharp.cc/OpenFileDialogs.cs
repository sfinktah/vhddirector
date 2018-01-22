using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSharp.cc.Windows.Forms
{
    public class OpenFileDialogs
    {
        public static string QuickFileDialog()
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select file";
            // browseFile.InitialDirectory = @"\\pt3\raid1\vhds\";
            browseFile.Filter = "All files (*.*)|*.*|Virtual Hard Disks (*.vhd, *.vmdk)|*.vhd;*.vmdk";
            browseFile.FilterIndex = 2;
            browseFile.RestoreDirectory = true;
            browseFile.CheckFileExists = false;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                return browseFile.FileName;
            }

            return String.Empty;
            //   return string.Empty;
        }
        public static string[] QuickMultipleFileDialog()
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select file";
            // browseFile.InitialDirectory = @"\\pt3\raid1\vhds\";
            browseFile.Filter = "All files (*.*)|*.*|Virtual Hard Disks (*.vhd, *.vmdk)|*.vhd;*.vmdk";
            browseFile.FilterIndex = 2;
            browseFile.Multiselect = true;
            browseFile.RestoreDirectory = true;
            browseFile.CheckFileExists = false;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                return browseFile.FileNames;
            }

            return null;
            //   return string.Empty;
        }
        //public void FolderBrowserDialog()
        //{
        //    OpenFileDialog browseFile = new OpenFileDialog();
        //    browseFile.Title = "Select your damned VHD file";
        //    browseFile.InitialDirectory = @"\\pt3\raid1\vhds\xp04.vhd";
        //    browseFile.Filter = "All files (*.*)|*.*|Virtual Hard Disk (*.vhd)|*.vhd";
        //    browseFile.FilterIndex = 2;
        //    browseFile.RestoreDirectory = true;
        //    if (browseFile.ShowDialog() == DialogResult.Cancel)
        //        return;

        //    try
        //    {
        //        filename = browseFile.FileName;
        //    }

        //    catch (Exception)
        //    {

        //        MessageBox.Show("Error opening file", "File Error",

        //        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        //    }
        //}
    }
}
