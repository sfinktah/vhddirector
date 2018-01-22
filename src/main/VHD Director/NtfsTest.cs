using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DiscUtils;
using DiscUtils.Ntfs;
using System.Collections;
using CSharp.cc;


namespace VHD_Director
{
    public partial class NtfsTest : Form
    {
        public NtfsTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = Form1.QuickFileDialog();
        }

        public string VolumeId = String.Empty;
        public int Partition = 0;
        public NtfsFileSystem fs;
        private void button2_Click(object sender, EventArgs e)
        {
            DiscUtils.VolumeManager volMgr = new VolumeManager();
                volMgr.AddDisk(VirtualDisk.OpenDisk(textBox1.Text, FileAccess.Read));


            Stream partitionStream = null;
            if (!string.IsNullOrEmpty(VolumeId))
            {
                partitionStream = volMgr.GetVolume(VolumeId).Open();
            }
            else if (Partition >= 0)
            {
                partitionStream = volMgr.GetPhysicalVolumes()[Partition].Open();
            }
            else
            {
                partitionStream = volMgr.GetLogicalVolumes()[0].Open();
            }

            SparseStream cacheStream = SparseStream.FromStream(partitionStream, Ownership.None);
            cacheStream = new BlockCacheStream(cacheStream, Ownership.None);

            fs = new NtfsFileSystem(cacheStream);
            fs.NtfsOptions.HideHiddenFiles = false;
            fs.NtfsOptions.HideSystemFiles = false; 
            fs.NtfsOptions.HideMetafiles = false;

            ClusterMap clusterMap = null;
            try
            {
                 clusterMap = fs.BuildClusterMap();
            }
            catch (IOException ex)
            {
                // DebugConsole.LogException(ex, "Trying to build a clustermap of " + textBox1.Text);
                MessageBox.Show(ex.Message, "Exception");
                // return;
            }
            // string[] files = null;

            
            // string[] sysfiles = fs.GetFileSystemEntries(@"\");
            // string[] files = fs.GetFiles(@"\", "*.*", SearchOption.AllDirectories);
            // fs.Dump(Console.Out, "");

            //QueuedBackgroundWorker.QueueWorkItem(QueuedBackgroundWorker.m_Queue, null,
            //    (args) => { return fs.GetFiles(@"\", "*.*", SearchOption.AllDirectories); },
            //    (args) => { if (args.Result != null && args.Result is string[]) ProcessFiles((string[])args.Result); }
            //);

            BitArray ba = new BitArray((int)fs.TotalClusters);

            for (int i = 0; i < fs.TotalClusters; i++)
            {
                if (clusterMap.ClusterToPaths(i).Length > 0)
                {
                    ba.Set(i, true);
                }
            }
            
            ClusterForm cf = new ClusterForm();
            cf.Icon = Icon;
            cf.Text = Text + " - Cluster Map";
            //cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            //cf.clusterBitmaps1.GradientEnd = Color.Black;
            //cf.clusterBitmaps1.GradientSteps = 8;
            cf.clusterBitmaps1.Clusters = ba;
            cf.Show();

        }

        private void ProcessFiles(string[] files)
        {
            BitArray ba = new BitArray((int)fs.TotalClusters);

            foreach (var file in files)
            {
                try
                {
                    Range<long, long>[] clusters = fs.PathToClusters(file);
                    foreach (var range in clusters)
                    {
                        int end = (int)(range.Offset + range.Count);
                        for (int i = (int)range.Offset; i < end; i++)
                        {
                            ba.Set(i, true);
                        }
                    }
                }
                catch (FileNotFoundException ex)
                {
                    System.Console.WriteLine("FileNotFound: {0}", file);
                }
            }

            // BitArray ba = defraglib.IOWrapper.GetVolumeMap(@"f:");


            ClusterForm cf = new ClusterForm();
            cf.Icon = Icon;
            cf.Text = Text + " - Cluster Map";
            //cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            //cf.clusterBitmaps1.GradientEnd = Color.Black;
            //cf.clusterBitmaps1.GradientSteps = 8;
            cf.clusterBitmaps1.Clusters = ba;
            cf.Show();
        }
    }
}
