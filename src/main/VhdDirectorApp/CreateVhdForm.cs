using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DiscUtils.Partitions;
using DiscUtils.Fat;
using System.Reflection;

// http://www.java2s.com/Open-Source/CSharp/File/discutils/DiscUtils/Combined/CombinedTest.cs.htm

namespace VhdDirectorApp
{

    public partial class CreateVhdForm : Form
    {

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]

        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
           IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private PrivateFontCollection MYpfc = new PrivateFontCollection();
        public CreateVhdForm()
        {
            InitializeComponent();
            try
            {
                unsafe
                {   
                    fixed (byte* pFontData = Properties.Resources.ROYALP)
                    {
                        uint dummy = 0;
                        MYpfc.AddMemoryFont((IntPtr)pFontData, Properties.Resources.ROYALP.Length);
                        AddFontMemResourceEx((IntPtr)pFontData, (uint)Properties.Resources.ROYALP.Length, IntPtr.Zero, ref dummy);
                    }
                }
                textBox1.Font = new Font(MYpfc.Families[0], 36); //Font size is 36
                comboBox1.Font = new Font(MYpfc.Families[0], 36); //Font size is 36
                comboBox2.Font = new Font(MYpfc.Families[0], 36); //Font size is 36
                label1.Font = new Font(MYpfc.Families[0], 36); //Font size is 36

            }
            catch
            {
                MessageBox.Show("Font does not correctly appear");

            }
        }

        bool skipKey = false;
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (isvalid())
            {
                pictureBox1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.burn_green;

            }
            if (skipKey)
            {
                skipKey = false;
                e.Handled = true;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            // Determine whether the keystroke is a number from the top of the keyboard or numberpad, period, or backspace key.

            if ((e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9) && (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9) && e.KeyCode != Keys.Back)
            {
                if (e.KeyCode == Keys.M)
                {
                    label1.Text = "MB";
                }
                else if (e.KeyCode == Keys.G)
                {
                    label1.Text = "GB";
                }

                skipKey = true;
            }
        }

        private void comboBox1_Validating(object sender, CancelEventArgs e)
        {
            if (isvalid())
            {
                pictureBox1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.burn_green;

            }
            if (((ComboBox)sender).SelectedIndex > -1)
            {
                return;
            }
            if (((ComboBox)sender).Text.Length < 1)
            {
                return;
            }

            ((ComboBox)sender).Text = String.Empty;

            // e.Cancel = true;
        }

        private bool isvalid() {
            try
            {
                return (comboBox1.SelectedIndex > -1 && comboBox2.SelectedIndex > -1 && Convert.ToInt32(textBox1.Text) > 0);
            }
            catch
            {
                return false;
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (isvalid())
            {
                pictureBox1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.burn_green;

            }
            else
            {
                pictureBox1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.burn_red;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.burn_yellow;

        }


        public void CreateVhd()
        {
            long diskSize = Convert.ToInt32(textBox1.Text) * 1024 * 1000;
            if (label1.Text == "GB")
            {
                diskSize *= 1000;
            }
            try
            {
                using (Stream vhdStream = File.Create(QuickFileDialog()))
                {
                    DiscUtils.Ownership owner;
                    owner = DiscUtils.Ownership.None;
                    DiscUtils.Vhd.Disk disk;

                    if (comboBox1.SelectedItem.ToString().Equals("dynamic"))
                    {
                        disk = DiscUtils.Vhd.Disk.InitializeDynamic(vhdStream, owner, diskSize);
                    }
                    else if (comboBox1.SelectedItem.ToString().Equals("fixed"))
                    {
                        disk = DiscUtils.Vhd.Disk.InitializeFixed(vhdStream, owner, diskSize);

                    } else {
                        MessageBox.Show("Woops, I don't know what kind of VHD I'm making", "My Bad");
                        throw new Exception("What the last one said");
                    }
                    WellKnownPartitionType pt;

                    if (comboBox2.Text.Equals("linux"))
                    {
                        pt = WellKnownPartitionType.Linux;
                    }
                    else if (comboBox2.Text.Equals("fat32"))
                    {
                        pt = WellKnownPartitionType.WindowsFat;
                    }
                    else if (comboBox2.Text.Equals("ntfs"))
                    {
                        pt = WellKnownPartitionType.WindowsNtfs;
                    }
                    else
                    {
                        throw new Exception("Unknown FS TYPE");
                    }
                    BiosPartitionTable.Initialize(disk, pt);

                    /* using (FatFileSystem fs = FatFileSystem.FormatPartition(disk, 0, null))
                    {
                        fs.CreateDirectory(@"TestDir\CHILD");
                        // do other things with the file system...
                    }
                     * */
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cancelled");
                return;
            }

            MessageBox.Show("It should be done now", "Finished");
        }

        public string QuickFileDialog()
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select file";
            browseFile.InitialDirectory = @"\\pt3\raid1\vhds\";
            browseFile.Filter = "All files (*.*)|*.*|Virtual Hard Disk (*.vhd)|*.vhd";
            browseFile.FilterIndex = 2;
            browseFile.RestoreDirectory = true;
            browseFile.CheckFileExists = false;
            DialogResult result = browseFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                return browseFile.FileName;
            }

            throw new Exception("User cancelled OpenFileDialog");
            //   return string.Empty;
        }

        private void pictureBox1_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (isvalid())
            {
                CreateVhd();
            }

        }
    }
}