using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp
{
    public class VirtualHardDisk : DiskModel
    {
        public Footer footer;
        public DynamicHeader dynamicHeader;
        public BlockAllocationTable blockAllocationTable;
        public MasterBootRecord masterBootRecord;
        public VhdReadException vhdReadException;

        public string DiskType { get { return footer.DiskType; } }

        public VirtualHardDisk(string filename, VhdReadException ex)
        {
            this.Filename = filename;
            this.vhdReadException = ex;
        }

        public VirtualHardDisk(string filename)
        {
            this.Filename = filename;
            LoadHeaders(filename);
            GetFileMap();
        }

        public void LoadHeaders(string filename)
        {
            try
            {
                this.Filename = filename;
                footer = new Footer();
                footer.FromFile(filename);

                if (footer.DataOffset > 0)
                {
                    dynamicHeader = new DynamicHeader();
                    dynamicHeader.FromFile(filename, (int)footer.DataOffset);
                    blockAllocationTable = new BlockAllocationTable(dynamicHeader.TableOffset, dynamicHeader.BlockSize, dynamicHeader.MaxTableEntries);
                    blockAllocationTable.FromFile(filename);
                    masterBootRecord = new MasterBootRecord(blockAllocationTable);
                    // ShowUsage();
                    // ShowDetailedUsage();
                }
                else
                {
                    masterBootRecord = new MasterBootRecord(filename, 0);
                    // MessageBox.Show("So far all the good stuff involves Dynamic VHDs, so all you get is this pretty box really.", "Fixed VHD", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (VhdReadException ex)
            {
                this.vhdReadException = ex;
            }
        }

        public void GetFileMap()
        {
            int errno;
            try
            {
                // this.ClusterMap = defraglib.IOWrapper.GetFileMap(this.FileName, out errno);

            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                errno = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                if (errno == 50)
                {
                    // ERROR_NOT_SUPPORTED 
                    // Probably a network drive
                    return;
                }
            }
        }


        public System.Collections.BitArray batUsage { get; set; }

        public string MountedAs { get; set; }

        public List<DiskPartitionModel> Partitions { get; set; }

        public string Filename { get; set; }

        public Array ClusterMap { get; set; }
        public bool IsFragmentedFile
        {
            get
            {
                if (ClusterMap == null) return false;
                if (ClusterMap.Length == 2) return false;
                return true;
            }
        }

        public string IOExceptionMessage { get; set; }
    }
}
