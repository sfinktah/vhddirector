using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscUtils.Ntfs;
using System.IO;
using DiscUtils;
using System.Collections;

namespace VhdDirectorApp
{
    public class PartitionController
    {
        public int UsedClusters { get; set; }
        public int FreeClusters { get; set; }
        public long TotalClusters { get; set; }
        public BitArray ClusterBitArray { get; set; }

        public VirtualHardDisk vhd;

        public PartitionController(VirtualHardDisk vhd, int partition)
        {
            // TODO: Complete member initialization
            this.vhd = vhd;
            this.Partition = partition;
            try
            {
                if (GetClusterUsage() != null)
                {
                    // Result is stored in ClusterBitArray as well as being returned
                }
            }
            catch (Exception ex)
            {
                vhd.vhdReadException = new VhdReadException("GetClusterUsage", ex);
                // throw new VhdReadException("GetClusterUsage", ex);
            }
        }

        public string VolumeId = String.Empty;
        public int Partition = 0;
        public NtfsFileSystem fs;
        
        public BitArray GetClusterUsage()
        {
            if (vhd.masterBootRecord.partition[Partition].PartitionType != 7)
            {
                return null;
            }

            try
            {
                DiscUtils.VolumeManager volMgr = new VolumeManager();
                using (VirtualDisk virtualDisk = VirtualDisk.OpenDisk(vhd.footer.FileName, FileAccess.Read))
                {
                    volMgr.AddDisk(virtualDisk);

                    using (Stream partitionStream = volMgr.GetPhysicalVolumes()[Partition].Open())
                    {

                        //if (!string.IsNullOrEmpty(VolumeId))
                        //{
                        //    partitionStream = volMgr.GetVolume(VolumeId).Open();
                        //}
                        //else if (Partition >= 0)
                        //{
                        //    partitionStream = volMgr.GetPhysicalVolumes()[Partition].Open();
                        //}
                        //else
                        //{
                        //    partitionStream = volMgr.GetLogicalVolumes()[0].Open();
                        //}

                        SparseStream cacheStream = SparseStream.FromStream(partitionStream, Ownership.None);
                        cacheStream = new BlockCacheStream(cacheStream, Ownership.None);
                        try
                        {
                            fs = new NtfsFileSystem(cacheStream);
                        }
                        catch
                        {
                            cacheStream.Dispose();
                            return null;
                        }


                        fs.NtfsOptions.HideHiddenFiles = false;
                        fs.NtfsOptions.HideSystemFiles = false;
                        fs.NtfsOptions.HideMetafiles = false;

                        BitArray ba = fs.BuildClusterBitArray();

                        fs.Dispose();
                        cacheStream.Dispose();
                        partitionStream.Dispose();


                        ClusterBitArray = ba;
                    }
                }
            }
            catch (VhdReadException ex)
            {
                this.vhd.vhdReadException = ex;
                return null;
            }

            return ClusterBitArray;
            /*
            ClusterMap clusterMap = null;
            try
            {
                clusterMap = fs.BuildClusterMap();
            }
            catch (IOException ex)
            {
                // TODO: Need to some error notfication
                // MessageBox.Show(ex.Message, "Exception");
                return null;
            }


            BitArray ba = new BitArray((int)fs.TotalClusters);
             */

            //UsedClusters = 0;
            //FreeClusters = 0;
            //TotalClusters = fs.TotalClusters;

            //for (int i = 0; i < TotalClusters; i++)
            //{
            //    if (clusterMap.ClusterToPaths(i).Length > 0)
            //    {
            //        UsedClusters++;
            //        ba.Set(i, true);
            //    }
            //    else
            //    {
            //        FreeClusters++;
            //    }
            //}
            /*
            ClusterForm cf = new ClusterForm();
            cf.Icon = Icon;
            cf.Text = Text + " - Cluster Map";
            cf.clusterBitmaps1.Clusters = ba;
            cf.Show();
             */
            //cf.clusterBitmaps1.GradientStart = Color.WhiteSmoke;
            //cf.clusterBitmaps1.GradientEnd = Color.Black;
            //cf.clusterBitmaps1.GradientSteps = 8;


        }



        public Boolean IsPartitionModel
        {
            get
            {
                return vhd.Partitions != null && vhd.Partitions[Partition] != null;
            }
        }

        public DiskPartitionModel PartitionModel
        {
            get
            {
                return vhd.Partitions[Partition];
            }
        }

        public Boolean IsLogicalPartitionModel
        {
            get
            {
                return vhd.Partitions[Partition].LogicalDiskModel != null;
            }
        }

        public LogicalDiskModel LogicalPartitionModel
        {
            get
            {
                return vhd.Partitions[Partition].LogicalDiskModel;
            }
        }

    }
}

/*

        protected override void OnLayout(LayoutEventArgs e)
        {
            csharp_debug.Debug.DebugWinControl(this);
            if (ba == null)
            {
                ba = controller.GetClusterUsage();

                this.blackProgressBar1.BitArray = ba;
                UsedSpaceBar.Maximum = blackProgressBar1.Maximum;
                UsedSpaceBar.Value = blackProgressBar1.Value;
            }
            // base.OnLayout(e);


        }

*/