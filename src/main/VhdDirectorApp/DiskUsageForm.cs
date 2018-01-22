using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using Microsoft.Win32;
using System.Collections.Specialized;

namespace VhdDirectorApp
{
    public partial class DiskUsageForm : Form
    {
        private static Form mainForm;
        public static Form MainForm { get { return mainForm; } }

        public DiskUsageForm()
        {
            Error.InstallHandler();
            InitializeComponent();
            mainForm = this;
            flowLayoutPanel1.HorizontalScroll.Visible = false;
            // ResizeRedraw = true;
            //          this.SetStyle(
            //ControlStyles.AllPaintingInWmPaint |
            //ControlStyles.UserPaint |
            //ControlStyles.DoubleBuffer, true);
        }


        internal void AddDisk(VirtualHardDisk vhd)
        {
            using (RotateWaitThreaded.Hold("Generating View for {0}", Path.GetFileNameWithoutExtension(vhd.Filename)))
            {
                if (vhd.vhdReadException != null)
                {
                    // TODO: Handle virtualDIskException

                    VhdPartitionView vhdview = new VhdPartitionView();
                    vhdview.Icon = Icon;

                    vhdview.AddDisk(vhd);
                    vhdview.remove += new VhdPartitionView.RemoveHandler(vhdview_remove);

                    // When vhdview is added, it does a hell of a lot of drawing of greenpartitionlayout for no reason.
                    flowLayoutPanel1.Controls.Add(vhdview);
                }

                else
                {
                    VhdPartitionView vhdview = new VhdPartitionView();
                    vhdview.Icon = Icon;

                    vhdview.AddDisk(vhd);
                    vhdview.remove += new VhdPartitionView.RemoveHandler(vhdview_remove);

                    // When vhdview is added, it does a hell of a lot of drawing of greenpartitionlayout for no reason.
                    flowLayoutPanel1.Controls.Add(vhdview);
                }
            }
        }

        void vhdview_remove(object source, object eventArgument)
        {
            flowLayoutPanel1.Controls.Remove((Control)source);
        }

        private void UnloadCurrentVhd()
        {
            //footer = null;  
            //dynamicHeader = null; 
            //blockAllocationTable = null;
            //masterBootRecord = null;
            /*
                        if (duf != null)
                        {
                            duf.Close();
                            duf = null;
                        }
                        */
            //if (treeView1 != null)
            //{
            //    treeView1.Nodes.Clear();
            //}
        }
        private void flowLayoutPanel1_ControlAdded(object sender, ControlEventArgs e)
        {
            if (flowLayoutPanel1.Controls.Count > 0)
            {
                this.flowLayoutPanel1.BackgroundImage = null;
                // this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            }
            else
            {
                this.flowLayoutPanel1.BackgroundImage = global::VhdDirectorApp.Properties.Resources.dragondrop_1_;
                // this.flowLayoutPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            }
        }

        protected string filename;

        VirtualHardDisk vhd;

        private void ShowDetailedUsage()
        {
            using (RotateWaitThreaded.Hold("Processing MBR for {0}", vhd.Filename))
            {

                // Sorry, I know it looks like there is a MVC pattern in use, but really it's just
                // a giant fucking mess.

                DiskUsageView duv = new DiskUsageView();
                DiskUsageModel dum = new DiskUsageModel(vhd.masterBootRecord.getPartitionRecords());
                DiskUsageController duc = new DiskUsageController();
                // DiskModel dm = new DiskModel(vhd.footer, vhd.dynamicHeader, vhd.blockAllocationTable, vhd.masterBootRecord);

            }
        }
        public bool ReadVhd(string filename)
        {
            try
            {
                if (CSharp.cc.Files.DoubleCheckIfFileIsBeingUsed(filename))
                {
                    List<String> whoLockedIt = FileLockInfo.Handle(filename);
                    string whoString = String.Empty;
                    foreach (var who in whoLockedIt)
                    {
                        whoString += who.ToString() + ", ";
                    }
                    MessageBox.Show(filename + " Locked by " + whoString);
                    return false;
                }
                vhd = new VirtualHardDisk(filename);
                if (vhd.vhdReadException == null)
                {
                    if (!vhd.DiskType.Equals("Fixed"))
                    {
                        ShowUsage();
                    }
                    ShowDetailedUsage();
                }
            }
            //catch (VhdReadException ex)
            //{
            //    vhd.vhdReadException = ex;
            //}
            catch (Exception ex)
            {
                MessageBox.Show(filename + ": " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                this.AddDisk(vhd);
            }
               

            return true;
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] filenames = CSharp.cc.Windows.Forms.OpenFileDialogs.QuickMultipleFileDialog();
            if (filenames != null && filenames.Length > 0) {
                foreach (string f in filenames)
                {
                    if (!ReadVhd(f))
                    {
                        continue;
                    }
                    UnloadCurrentVhd();
                }
                // MessageBox.Show("It may have worked!", "Done", MessageBoxButtons.OK);
            }
#if RELEASE 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception: " + ex.Message, "Unsurprisingly...", MessageBoxButtons.OK);
                throw new Exception(ex.Message, ex);
            }
#endif
        }

        private void ShowUsage()
        {
            RotateWaitThreaded.WriteLine("Getting BAT", vhd.Filename);

            BitArray ba = new BitArray((int)vhd.blockAllocationTable.maxTableEntries);
            // userControlDiskUsageBar1.Maximum = (int)vhd.blockAllocationTable.maxTableEntries;
            for (int i = 0; i < vhd.blockAllocationTable.maxTableEntries; i++)
            {
                if (vhd.blockAllocationTable.IsBlockAllocated((uint)i))
                {
                    ba.Set(i, true);
                }
            }
            vhd.batUsage = ba;
        }


        private void viewMBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] mbr = vhd.blockAllocationTable.GetSector(0);

            using (BinaryWriter b = new BinaryWriter(File.Open("mbr.bin", FileMode.Create)))
            {
                // 3. Use foreach and write all 12 integers.
                foreach (byte i in mbr)
                {
                    b.Write(i);
                }
            }
            BiosPartitionRecord[] bpr = {
                new BiosPartitionRecord(mbr, 446, 0, 0),
                new BiosPartitionRecord(mbr, 462, 0, 0),
                new BiosPartitionRecord(mbr, 478, 0, 0),
                new BiosPartitionRecord(mbr, 494, 0, 0)
            };

            // DiscUtils.Partitions.BiosPartitionTable bpt = new DiscUtils.Partitions.BiosPartitionTable();

            Be.Windows.Forms.ByteCollection bc = new Be.Windows.Forms.ByteCollection(mbr);
            HextFormTest hexForm = new HextFormTest();
            Be.Windows.Forms.DynamicByteProvider dbp = new Be.Windows.Forms.DynamicByteProvider(bc);
            hexForm.SetHexBox("mbr.bin");
            hexForm.Icon = Icon;
            hexForm.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RotateWaitThreaded.WriteLine("Searching {0}", "C:");

            CSharp.cc.DirSearch dsearch = new CSharp.cc.DirSearch();
            dsearch.found += new CSharp.cc.DirSearch.FoundHandler(dsearch_found);
            dsearch.completed += new CSharp.cc.DirSearch.CompleteHandler(dsearch_completed);
            dsearch.StartDirSearch(@"C:\", ".vhd");
        }

        void dsearch_completed(object source, object eventArgument)
        {
            RotateWaitThreaded.WriteLine("Search Complete");

        }

        void dsearch_found(object source, object eventArgument)
        {
            // treeView1.Nodes.Add((string)eventArgument);
            try
            {
                RotateWaitThreaded.WriteLine("Found {0}", (string)eventArgument);

                ReadVhd((string)eventArgument);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, (string)eventArgument);
            }
        }

        // This event occurs when the user drags over the form with 
        // the mouse during a drag drop operation 
        void Form_DragEnter(object sender, DragEventArgs e)
        {
            // Check if the Dataformat of the data can be accepted
            // (we only accept file drops from Explorer, etc.)
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it

        }

        // Occurs when the user releases the mouse over the drop target 
        void Form_DragDrop(object sender, DragEventArgs e)
        {
            // Extract the data from the DataObject-Container into a string list
            string[] FileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            // Do something with the data...

            // For example add all files into a simple label control:
            foreach (string File in FileList)
                ReadVhd(File);
        }

        private void flowLayoutPanel1_Layout(object sender, LayoutEventArgs e)
        {
            foreach (var c in flowLayoutPanel1.Controls)
            {
                if (c is VhdPartitionView)
                {
                    // (c as Control).Dock = DockStyle.Top;
                    (c as Control).Width = flowLayoutPanel1.ClientSize.Width - 24;
                }
            }
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            return;
            foreach (var c in flowLayoutPanel1.Controls)
            {
                if (c is VhdPartitionView)
                {
                    (c as Control).Dock = DockStyle.Top;
                }
            }
        }

        private void DiskUsageForm_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();

            for (int i = 1; i < args.Length; i++)
            {
                ReadVhd(args[i]);
            }

            AddContextMenuItem(".vhd", "VHD Director", "Open with &VHD Director", "\"" + Application.ExecutablePath + "\" \"%1\"");
        }

        private static bool AddContextMenuItem(string Extension,
          string MenuName, string MenuDescription, string MenuCommand)
        {
            bool ret = false;
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;
            try
            {
                RegistryKey rkey = Registry.ClassesRoot.OpenSubKey(Extension);
                if (rkey != null)
                {
                    string extstring = rkey.GetValue("").ToString();
                    rkey.Close();
                    if (extstring != null)
                    {
                        if (extstring.Length > 0)
                        {
                            rkey = Registry.ClassesRoot.OpenSubKey(extstring, true);
                            if (rkey != null)
                            {
                                string strkey = "shell\\" + MenuName + "\\command";
                                RegistryKey subky = rkey.CreateSubKey(strkey);
                                if (subky != null)
                                {
                                    subky.SetValue("", MenuCommand);
                                    subky.Close();
                                    subky = rkey.OpenSubKey("shell\\" + MenuName, true);
                                    if (subky != null)
                                    {
                                        subky.SetValue("", MenuDescription);
                                        subky.Close();
                                    }
                                    ret = true;
                                }
                                rkey.Close();
                            }
                        }
                    }
                }
                regmenu = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\NewMenuOption");
                if (regmenu != null)
                    regmenu.SetValue("", MenuName + " &Folder");
                regcmd = Registry.ClassesRoot.CreateSubKey(@"Folder\shell\NewMenuOption\command");
                if (regcmd != null)
                    regcmd.SetValue("", MenuCommand);
            }
            catch
            {
                ret = false;
            }
            finally
            {
                if (regmenu != null)
                    regmenu.Close();
                if (regcmd != null)
                    regcmd.Close();
            }


            return ret;
        }

        Timer fadeTimer;
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fadeTimer = new Timer();
            fadeTimer.Interval = 1000 / 30;
            fadeTimer.Tick += new EventHandler(fadeTimer_Tick);
            fadeTimer.Start();

        }

        void fadeTimer_Tick(object sender, EventArgs e)
        {
            double op = Opacity;
            if (op < 0.05)
            {
                Close();
            }
            Opacity = op - 0.05;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Icon = Icon;
            about.ShowDialog();
        }

        private void getVolumeIdsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, NameValueCollection> result = wmi.GetAllVolumes.GetAllVolumeDeviceIDs();
        }

        private void findVolumesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FindVolumes.MainFunction();
        }

        private void getDiskDrivePhysicalMediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateWaitThreaded.WriteLine("WMI: GetDisks");
            GetDisks gdForm = new GetDisks();
            // gdForm.Size = new Size(500, 800);
            gdForm.Name = "WMI Query Tests";
            gdForm.Icon = Icon;
            gdForm.Show();
            RotateWaitThreaded.WriteLine("WMI: GetDiskPhysicalMedia");
            gdForm.GetDiskDrivePhysicalMedia();
            RotateWaitThreaded.WriteLine("WMI: GetDiskDrives");
            gdForm.GetDiskDrives();
            RotateWaitThreaded.WriteLine("WMI: GetLogicalDrives");
            gdForm.GetLogicalDisks();
            RotateWaitThreaded.WriteLine("WMI: GetRelatedPartitions");
            gdForm.GetDiskDrivesAndRelatedPartitions();
            RotateWaitThreaded.WriteLine("WMI: GetDiskDrivePhysicalMediaAndRelatedPartitions");
            gdForm.GetDiskDrivePhysicalMediaAndRelatedPartitions();
            RotateWaitThreaded.WriteLine("WMI: Complete");
        }

        private void downloadContigexeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadUnpackForm.Example();
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BitArray ba = defraglib.IOWrapper.GetVolumeMap(@"c:");
            int nClusters = ba.Count;

            ClusterForm cf = new ClusterForm();
            cf.Icon = Icon;
            cf.Text = Text + " - Cluster Map of C:";
            //cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            //cf.clusterBitmaps1.GradientEnd = Color.Black;
            //cf.clusterBitmaps1.GradientSteps = 8;
            cf.clusterBitmaps1.Clusters = ba;
            cf.Show();
        }

        private void readPlistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var o = PList.Read("plugins/pro.reboot.sfinktah.contig.plist.txt");
            PListViewerForm plform = new PListViewerForm();
            plform.o = o;
            plform.Show();



        }

        private void rotatingDiscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RotateWait waitform = new RotateWait();
            waitform.Show();
        }


        private void greenPartitionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GreenPartitionViewForm gf = new GreenPartitionViewForm();
            gf.Show();
        }

        private void tableLayoutGridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TableLayoutPanelForm tlpf = new TableLayoutPanelForm();
            tlpf.Show();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateVhdForm cf = new CreateVhdForm();
            cf.Show();
        }

        private void throwExceptionFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exception ex = new Exception("Outer Exception", new Exception("innerException"));
            ex.Data.Add("Cause", "Idiocy");
            ex.Source = "You are the source";
            // ex.Message;
            // ex.StackTrace;
            // ex.TargetSite;
            // ex.InnerException;
            throw ex;
        }

        private void screenShotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            UnhandledExceptionManager.TakeScreenshotOfWindow("screenshot.jpeg", MainForm.Handle);
        }

        private Form nwtForm;
        private void nativeWindowTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NativeWindowApplication.Form1 form = new NativeWindowApplication.Form1();
            form.Text = "Test or kak";
            form.Icon = Icon;
            form.Show();
            nwtForm = form;
        }

        private void nativeWindowTestSendToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSharp.cc.WinApi.User32.SetWindowText(nwtForm.Handle, "Amazing!");
            // CSharp.cc.WinApi.User32.SetWindowText(Process.GetCurrentProcess().MainWindowHandle, "Amazing!");
        }

        private void getManifestResourcesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string all = string.Empty;

            foreach (string one in System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames())
                all += one + Environment.NewLine;

            MessageBox.Show(all, "Manifest Resources");
        }

        private void replaceOurselfToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Attempting to replace " + Ourself.FileName(), "Updating");
            // Ourself.Rename();
            new DownloadRotateWait("http://reboot.nt4.com/vhddirector/vhddirector.exe", Ourself.FileName(), "Update");
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckForUpdates updateCheck = new CheckForUpdates("http://manok.me/update/");
            updateCheck.updateRequired += new UpdateEventHandler(updateCheck_updateRequired);
            updateCheck.Start();

        }

        void updateCheck_updateRequired(object sender, EventArgsString e)
        {
            new DownloadRotateWait(e.Target, Ourself.FileName(), "Update");
        }

        private void zipTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            My7Zip.Test();
        }


    }


}
