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
using System.Collections;

// http://www.java2s.com/Open-Source/CSharp/File/discutils/DiscUtils/Combined/CombinedTest.cs.htm

// Microsoft Virtual Server 2005 R2 SP1 - Enterprise Edition
// http://www.microsoft.com/download/en/details.aspx?id=2994
// 32bit - http://download.microsoft.com/download/d/7/2/d7235926-a10d-482c-a2ff-6e0d3130f869/32-BIT/setup.exe
// 64bit - http://download.microsoft.com/download/d/7/2/d7235926-a10d-482c-a2ff-6e0d3130f869/64-BIT/setup.exe

// Microsoft Virtual Server 2005 R2 SP1 - Enterprise Edition
//  http://www.microsoft.com/download/en/details.aspx?id=2994
//  32-bit: http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=bc49c7c8-4840-4e67-8dc4-1e6e218acce4&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fd%2f7%2f2%2fd7235926-a10d-482c-a2ff-6e0d3130f869%2f32-BIT%2fsetup.exe
//  64-bit: http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=bc49c7c8-4840-4e67-8dc4-1e6e218acce4&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fd%2f7%2f2%2fd7235926-a10d-482c-a2ff-6e0d3130f869%2f64-BIT%2fsetup.exe



namespace VHD_Director
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // new CheckUpdates("http://manok.me/update/", UpdateAvailable);
            //start client idle hook


        }


        public void UpdateAvailable(object source, object eventArgument)
        {
            Close();
        }

        private string filename;

        public static string QuickFileDialog()
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
        public void FolderBrowserDialog()
        {
            OpenFileDialog browseFile = new OpenFileDialog();
            browseFile.Title = "Select your damned VHD file";
            browseFile.InitialDirectory = @"\\pt3\raid1\vhds\xp04.vhd";
            browseFile.Filter = "All files (*.*)|*.*|Virtual Hard Disk (*.vhd)|*.vhd";
            browseFile.FilterIndex = 2;
            browseFile.RestoreDirectory = true;
            if (browseFile.ShowDialog() == DialogResult.Cancel)
                return;

            try
            {
                filename = browseFile.FileName;
            }

            catch (Exception)
            {

                MessageBox.Show("Error opening file", "File Error",

                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }
        //private Footer footer;
        //private DynamicHeader dynamicHeader;
        //private BlockAllocationTable blockAllocationTable;
        //private MasterBootRecord masterBootRecord;

        VirtualHardDisk vhd;
        public bool ReadVhd(string filename)
        {
            try
            {
                vhd = new VirtualHardDisk(filename);
                if (!vhd.DiskType.Equals("Fixed"))
                {
                    ShowUsage();
                }
                ShowDetailedUsage();
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
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
            if (treeView1 != null)
            {
                treeView1.Nodes.Clear();
            }
        }
        private DiskUsageForm duf;
        private void ShowDetailedUsage()
        {
            if (duf == null || duf.IsDisposed)
            {
                duf = new DiskUsageForm();
                duf.Icon = Icon;
            }

            // Sorry, I know it looks like there is a MVC pattern in use, but really it's just
            // a giant fucking mess.

            DiskUsageView duv = new DiskUsageView();
            DiskUsageModel dum = new DiskUsageModel(vhd.masterBootRecord.getPartitionRecords());
            DiskUsageController duc = new DiskUsageController();
            // DiskModel dm = new DiskModel(vhd.footer, vhd.dynamicHeader, vhd.blockAllocationTable, vhd.masterBootRecord);

            duf.AddDisk(vhd);
            duf.Show();
            //duf.footer = vhd.footer;
            //duf.dynamicHeader = vhd.dynamicHeader;
            //duf.blockAllocationTable = vhd.blockAllocationTable;
            //duf.masterBootRecord = vhd.masterBootRecord;

            //duf.diskModel = dm;  // A duplication of the above 4.

            //duv.model = dum;
            //duv.controller = duc;
            //duf.AddDisk(duv);
            //duf.Text = filename;
            //duf.Show();

            // duf.AddDisk(vhd);
        }

        private void browseFileButton_Click(object sender, EventArgs e)
        {
#if RELEASE
            try
            {
#endif
            FolderBrowserDialog();
            if (filename.Length > 0)
            {
                if (!ReadVhd(filename))
                {
                    return;
                }
                UnloadCurrentVhd();

                TreeNode branch_node = treeView1.Nodes.Add("Hard Disk Footer ");
                TreeNodeCollection branch = (TreeNodeCollection)branch_node.Nodes;
                vhd.footer.Render(branch);
                if (vhd.dynamicHeader != null)
                {
                    branch_node = treeView1.Nodes.Add("Dynamic Disk Header");
                    branch = (TreeNodeCollection)branch_node.Nodes;
                    vhd.dynamicHeader.Render(branch);


                    branch_node = treeView1.Nodes.Add("Partitions");
                    branch = (TreeNodeCollection)branch_node.Nodes;
                    vhd.masterBootRecord.Render(branch);
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

        private void createVHDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateVhdForm cf = new CreateVhdForm();
            cf.Show();
        }

        private void acronisPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DiskUsageForm af = new DiskUsageForm();
            af.Icon = Icon;
            af.Show();
        }

        private Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            //This handler is called only when the common language runtime tries to bind to the assembly and fails.

            //Retrieve the list of referenced assemblies in an array of AssemblyName.
            Assembly MyAssembly, objExecutingAssemblies;
            string strTempAssmbPath = "";

            objExecutingAssemblies = Assembly.GetExecutingAssembly();
            AssemblyName[] arrReferencedAssmbNames = objExecutingAssemblies.GetReferencedAssemblies();

            //Loop through the array of referenced assembly names.
            foreach (AssemblyName strAssmbName in arrReferencedAssmbNames)
            {
                //Check for the assembly names that have raised the "AssemblyResolve" event.
                if (strAssmbName.FullName.Substring(0, strAssmbName.FullName.IndexOf(",")) == args.Name.Substring(0, args.Name.IndexOf(",")))
                {
                    //Build the path of the assembly from where it has to be loaded.				
                    strTempAssmbPath = "C:\\Myassemblies\\" + args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll";
                    break;
                }

            }
            //Load the assembly from the specified path. 					
            MyAssembly = Assembly.LoadFrom(strTempAssmbPath);

            //Return the loaded assembly.
            return MyAssembly;
        }


        private void bCDDirectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BcdDirector.BcdList bcdList = new BcdDirector.BcdList();
            bcdList.Icon = Icon;
            bcdList.Show();
        }

        #region Private Members
        private ContextMenuStrip listboxContextMenu;
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            // AppDomain currentDomain = AppDomain.CurrentDomain;
            // currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
            //assign a contextmenustrip
            listboxContextMenu = new ContextMenuStrip();
            // listboxContextMenu.Opening += new CancelEventHandler(listboxContextMenu_Opening);
            treeView1.ContextMenuStrip = listboxContextMenu;

            filename = @"C:\CRYSIS-13 - Copy.VHD";
            filename = @"Z:\VHDs\WindowsXpVhdImport.vhd";
            if (filename.Length > 0)
            {
                if (!ReadVhd(filename))
                {
                    return;
                }
                this.Hide();
                UnloadCurrentVhd();

                TreeNode branch_node = treeView1.Nodes.Add("Hard Disk Footer ");
                TreeNodeCollection branch = (TreeNodeCollection)branch_node.Nodes;
                vhd.footer.Render(branch);
                if (vhd.dynamicHeader != null)
                {
                    branch_node = treeView1.Nodes.Add("Dynamic Disk Header");
                    branch = (TreeNodeCollection)branch_node.Nodes;
                    vhd.dynamicHeader.Render(branch);


                    branch_node = treeView1.Nodes.Add("Partitions");
                    branch = (TreeNodeCollection)branch_node.Nodes;
                    vhd.masterBootRecord.Render(branch);
                }

                // MessageBox.Show("It may have worked!", "Done", MessageBoxButtons.OK);
            }


        }

        //private void treeView1_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (e.Button == MouseButtons.Right)
        //    {
        //        //select the item under the mouse pointer
        //        // treeView1.SelectedNode.Index; // SelectedIndex = treeView1.IndexFromPoint(e.Location);
        //        if (treeView1.SelectedIndex != -1)
        //        {
        //            listboxContextMenu.Show();
        //        }
        //    }
        //}

        //private void listboxContextMenu_Opening(object sender, CancelEventArgs e)
        //{
        //    //clear the menu and add custom items
        //    listboxContextMenu.Items.Clear();
        //    listboxContextMenu.Items.Add(string.Format("Edit - {0}", treeView1.SelectedItem.ToString()));
        //}

        private void registryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegistryForm1 rform = new RegistryForm1();
            rform.Icon = Icon;
            rform.Show();
        }

        private void myFontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyFont myfont = new MyFont();
            myfont.Show();
        }

        private void disksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDisks gd = new GetDisks();
            gd.Icon = Icon;
            gd.Show();
        }

        private void vHDMountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VhdMount.Form1 vf = new VhdMount.Form1();
            vf.Icon = Icon;
            vf.Show();
        }

        private void downloadVHDMOUNTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DownloadFileForm df = new DownloadFileForm();
            df.Icon = Icon;
            df.Show();
        }

        private void findVHDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CSharp.cc.DirSearch dsearch = new CSharp.cc.DirSearch();
            dsearch.found += new CSharp.cc.DirSearch.FoundHandler(dsearch_found);
            dsearch.completed += new CSharp.cc.DirSearch.CompleteHandler(dsearch_completed);
            dsearch.StartDirSearch(@"C:\", ".vhd");
        }

        void dsearch_completed(object source, object eventArgument)
        {
            MessageBox.Show("Completed search");
        }

        void dsearch_found(object source, object eventArgument)
        {
            // TreeNode branch_node = treeView1.Nodes.Add((string)eventArgument);
            // TreeNodeCollection branch = (TreeNodeCollection)branch_node.Nodes;
            treeView1.Nodes.Add((string)eventArgument);
        }

        private void vHDTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VDS.LaurentEtiemble.example();
        }

        private void defragTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //-> VDisk Id=38469a5b-1194-4464-a21e-4d3120c18bb3
            //-> VDisk Device Name=\\?\scsi#disk&ven_msft&prod_virtual_disk#2&1f4adffe&0&000001#{53f56307-b6bf-11d0-94f2-00a0c91efb8b}
            //-> VDisk Path=\\pt3\raid1\VHDs\Tiny7.vhd
            //-> Disk Name=\\?\PhysicalDrive1
            //-> Disk Friendly=Msft Virtual Disk SCSI Disk Device
            BitArray ba = defraglib.IOWrapper.GetVolumeMap(@"c:");
            int nClusters = ba.Count;

            ClusterForm cf = new ClusterForm();
            cf.Icon = Icon;
            cf.Text = Text = " - Cluster Map of F:";
            //cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            //cf.clusterBitmaps1.GradientEnd = Color.Black;
            //cf.clusterBitmaps1.GradientSteps = 8;
            cf.clusterBitmaps1.Clusters = ba;
            cf.Show();

            //    static public BitArray GetVolumeMap(string DeviceName)
            // \\?\PhysicalDrive1
        }

        private void clusterViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClusterForm cf = new ClusterForm();
            cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            cf.clusterBitmaps1.GradientEnd = Color.Black;
            cf.Show();
        }

        private void fileClusterVIewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int errno;
            Array map = defraglib.IOWrapper.GetFileMap(@"C:\Users\Administrator\AppData\Local\Temp\VirtualServerSetup.exe", out errno);
            System.Console.WriteLine("Maybe that worked");
            // defraglib.IOWrapper.MoveFile("c:", @"C:\Users\Administrator\AppData\Local\Temp\VirtualServerSetup.exe", 0, LCN, count);

        }

        private void nTFSTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NtfsTest ntform = new NtfsTest();
            ntform.Show();
        }

        protected TreeNode tr;
        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode tr = treeView1.GetNodeAt(e.X, e.Y);
            //X = e.X;
            //Y = e.Y;
        }

        void treeView1MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Select the clicked node
                treeView1.SelectedNode = treeView1.GetNodeAt(e.X, e.Y);

                if (treeView1.SelectedNode != null)
                {
                    if (treeView1.SelectedNode.Nodes.Count == 0)
                    {
                        this.contextMenuStrip1.Show((sender as Control).PointToScreen(e.Location));
                    }
                    // treeView1.ContextMenuStrip.Show(treeView1, e.Location);
                    // myContextMenuStrip.Show(treeView1, e.Location)
                }
            }
            else
            {

            }
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            String name = tr.Name;
            String text = tr.Text;


        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}


    
/* vim: set ts=4 sts=0 sw=4 noet: */
