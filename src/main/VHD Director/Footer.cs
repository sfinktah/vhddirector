using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;


// http://stackoverflow.com/questions/2623761/marshal-ptrtostructure-and-back-again-and-generic-solution-for-endianness-swap
// http://weblogs.asp.net/ralfw/archive/2006/03/04/439580.aspx
// http://stackoverflow.com/questions/2871/reading-a-c-c-data-structure-in-c-sharp-from-a-byte-array
// http://stackoverflow.com/questions/2384/read-binary-file-into-a-struct-c-sharp
// http://www.codeproject.com/KB/files/fastbinaryfileinput.aspx
// http://bytes.com/topic/c-sharp/answers/257580-reading-binary-file-struct

namespace VHD_Director
{
    public class MasterBootRecord : VhdStructReader
    {
        private BlockAllocationTable blockAllocationTable;
        public BiosPartitionRecord[] partition;

        public MasterBootRecord(BlockAllocationTable blockAllocationTable)
        {
            this.blockAllocationTable = blockAllocationTable;

            byte[] mbr = blockAllocationTable.GetSector(0);
            mbrGetPartitions(mbr);
        }

        public MasterBootRecord(string filename, int p)
        {
            // TODO: Complete member initialization
            _sFileName = filename;
            byte[] mbr = RandomReadVhd(p, 512);
            mbrGetPartitions(mbr);
        }

        public void mbrGetPartitions(byte[] mbr)
        {
            partition = new BiosPartitionRecord[] {
                new BiosPartitionRecord(mbr, 446, 0, 0),
                new BiosPartitionRecord(mbr, 462, 0, 0),
                new BiosPartitionRecord(mbr, 478, 0, 0),
                new BiosPartitionRecord(mbr, 494, 0, 0)
            };
        }

        // public byte[] RandomReadVhd(long position, int count) {

   

        public BiosPartitionRecord[] getPartitionRecords() {
            BiosPartitionRecord[] bprArray = new BiosPartitionRecord[4];
            for (int i = 0; i < 4; i++)
            {
                bprArray[i] = partition[i];
            }
            return bprArray;
        }
    
        public override void Render(TreeNodeCollection arg)
        {
            PropertyInfo[] Footers = typeof(BiosPartitionRecord).GetProperties();
       

            for (int i = 0; i < 4; i++)
            {
                BiosPartitionRecord bpr = partition[i];
                if (bpr.IsValid)
                {
                    TreeNode tn = arg.Add("MBR Partition " + (i + 1) + " (" + bpr.FriendlyPartitionType + ")");
                    TreeNodeCollection tnc = tn.Nodes;

                    // foreach (System.Reflection.FieldInfo field in typeof(Footer).GetFields())
                    // foreach (PropertyDescriptor field in Footer)
                    foreach (PropertyInfo pi in Footers)
                    {
                        // Footer c = (Footer)pi.GetValue(null, null);
                        // do something here with the Footer

                        // http://msdn.microsoft.com/en-us/library/system.attribute.aspx
                        DescriptionAttribute[] attributes = (DescriptionAttribute[])pi.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        string description = attributes.Length > 0 ? attributes[0].Description : pi.Name;
                        if (description.CompareTo("hide") == 0)
                        {
                            continue;
                        }

                        // Show something like "My integer, 123 (Int32, i)"
                        string s = string.Format("{3}: {1}",
                                description, // DescriptionAttribute
                                pi.GetValue(bpr, null), //  GetValue(this), // value of
                                pi.GetType(),
                                description
                                );
                        tnc.Add(s);
                    }
                }
            }

        }

        public override bool FromFile(string filename)
        {
            return false;
        }
        
    }

    public class MasterBootRecord_ : VhdStructReader
    {
        MasterBootRecordFormat vhdData;

        public override bool FromFile(string filename)
        {
            return false;
        }
        
#region properties

public string BootCode {
	get { return "LONG"; }
	set { /* TODO */ ; }
}

public uint DiskSignature {
	get { return vhdData.DiskSignature; }
	set { vhdData.DiskSignature = value; }
}

public string Nulls {
	get { return "0x" + vhdData.Nulls.ToString("x"); }
    set { /* TODO */ ; }
}

public string Partition1 {
	get { return "0xTODO"; }
    set { /* TODO */ ; }
}

public string Partition2 {
    get { return "0xTODO"; }
    set { /* TODO */ ; }
}

public string Partition3 {
    get { return "0xTODO"; }
    set { /* TODO */ ; }
}

public string Partition4 {
    get { return "0xTODO"; }
    set { /* TODO */ ; }
}

public string Signature {
	get { return "0x" + vhdData.Signature.ToString("x"); }
    set { /* TODO */ ; }
}

#endregion 

[StructLayout(LayoutKind.Sequential, Size = 512, Pack = 1)] 
			private struct MasterBootRecordFormat 
			{ 

#region fields

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 440)]
				public byte[] BootCode;

				[MarshalAs(UnmanagedType.U4)]
				public uint DiskSignature;

				[MarshalAs(UnmanagedType.U2)]
				public ushort Nulls;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				public char[] Partition1;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				public char[] Partition2;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				public char[] Partition3;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
				public char[] Partition4;

				[MarshalAs(UnmanagedType.U2)]
				public ushort Signature;

#endregion
            }
		}

	public class BlockAllocationTable : VhdStructReader
	{
		// It's a little silly using a struct within a class for this 
		BlockAllocationTableStruct vhdData;
		public long tableOffset { get; set; }
		public uint blockSize { get; set; }
		public uint maxTableEntries { get; set; }
		public const int sectorSize = 512;

		// The Block Allocation Table (BAT) is a table of absolute sector
		// offsets into the file backing the hard disk. It is pointed to by the
		// “Table Offset” field of the Dynamic Disk Header.
		// 
		// The size of the BAT is calculated during creation of the hard disk.
		// The number of entries in the BAT is the number of blocks needed to
		// store the contents of the disk when fully expanded. For example, a
		// 2-GB disk image that uses 2 MB blocks requires 1024 BAT entries.
		// Each entry is four bytes long. All unused table entries are
		// initialized to 0xFFFFFFFF.
		//
		// The BAT is always extended to a sector boundary. The “Max Table
		// Entries” field within the Dynamic Disk Header indicates how many
		// entries are valid. 

		// The data block consists of a sector bitmap and data. For dynamic
		// disks, the sector bitmap indicates which sectors contain valid data
		// (1’s) and which sectors have never been modified (0’s). For
		// differencing disks, the sector bitmap indicates which sectors are
		// located within the differencing disk (1’s) and which sectors are in
		// the parent (0’s). The bitmap is padded to a 512-byte sector
		// boundary. 
		//
		// A block is a power-of-two multiple of sectors. By default, the size
		// of a block is 4096 512-byte sectors (2 MB). All blocks within a
		// given image must be the same size. This size is specified in the
		// “Block Size” field of the Dynamic Disk Header.
		//
		// All sectors within a block whose corresponding bits in the bitmap
		// are zero must contain 512 bytes of zero on disk. Software that
		// accesses the disk image may take advantage of this assumption to
		// increase performance.
		//
		// Note: Although the format supports varying block sizes, Microsoft
		// Virtual PC 2004 and Virtual Server 2005 have only been tested with
		// 512K and 2 MB block sizes.
		
		// Mapping a Disk Sector to a Sector in the Block
		// To calculate a block number from a referenced sector number, the
		// following formula is used:

		public uint sectorsPerBlock {
			get {
				return blockSize / sectorSize;
			}
		}

		public uint blockSectorBitmapSize
		{
			get {
				/*
				if (sectorsPerBlock % 8 > 0) {
					throw new Exception("sectorsPerBlock was not a multiple of 8");
				} */
				return sectorsPerBlock / 8;	// 1 bit per sector.
			}
		}






		//
		// BlockNumber = floor(RawSectorNumber / SectorsPerBlock)

		public uint getBlockNumber(uint sector) {
			uint doubleCheckValue = (uint)Math.Floor((double)sector / sectorsPerBlock);
			uint value = sector / sectorsPerBlock;
			if (doubleCheckValue != value) {
				throw new Exception("Need to double check getBlockNumber for handling of integer divides");
			}
			return value;
		}
		
		// SectorInBlock = RawSectorNumber % SectorsPerBlock 

		public uint getBlockSectorNumber(uint sector) {
			return sector % sectorsPerBlock;
		}
		
		// BlockNumber is used as an index into the BAT. The BAT entry contains
		// the absolute sector offset of the beginning of the block’s bitmap
		// followed by the block’s data. The following formula can be used to
		// calculate the location of the data:
		//
		// ActualSectorLocation = BAT [BlockNumber] + BlockBitmapSectorCount + SectorInBlock

		public byte[] GetSector(uint sector) {
			uint nBlock = getBlockNumber(sector);
            uint nSector = getBlockSectorNumber(sector);

            // Using uint is silly.  Will slowly convert all uints to ints.
            int blockAllocation = (int)vhdData.GetBlock(nBlock);
            if (blockAllocation == -1 /* 0xffffffff */)
            {
                throw new Exception("Block is not set");
            }

            long sectorBitmapLocation = blockAllocation * sectorSize;
            // TODO: check if sector is allocated.
            long sectorLocationActual = sectorBitmapLocation + blockSectorBitmapSize + (nSector * sectorSize);
			// SectorBitmap sectorBitmap = GetSectorBitmap(uint nBlock);

			byte[] sectorBuf = RandomReadVhd(sectorLocationActual, sectorSize);
			return sectorBuf;
		}

		public byte[] GetSectorBitmap(uint nBlock) {
			uint blockLocation = vhdData.GetBlock(nBlock);
			byte[] sectorBitmap = RandomReadVhd(blockLocation, sectorSize);
			return sectorBitmap;
		}

		public byte[] GetSector(uint nBlock, uint nSector) {
			// TODO: This is all wrong
			uint blockLocation = vhdData.GetBlock(nBlock);
			byte[] sectorBitmap = RandomReadVhd(blockLocation, sectorSize);
			return sectorBitmap;
		}

		
		// (a 2TB disk would have 4194304 sectors.. could define as int or uint)
		// (a 2TB disk would have 2147483648 actual bytes, which is 1 more byte than an int)
		// (a uint could store absolute locations of up to 4TB - 1byte)

		//
		// In this manner, blocks can be allocated in any order while
		// maintaining their sequencing through the BAT. 
		//
		// When a block is allocated, the image footer must be pushed back to
		// the end of the file. The expanded portion of the file should be
		// zeroed. 
		

		public BlockAllocationTable(long tableOffset, uint blockSize, uint maxTableEntries) {
			if (blockSize != 1<<21) {
				var vrex = new VhdReadException("blockSize should be 2M");
                vrex.Data.Add("blocksize", blockSize);
                throw vrex;
			}
			this.tableOffset = tableOffset;
			this.blockSize = blockSize;
			this.maxTableEntries = maxTableEntries;
		}

		public override bool FromFile(string filename) {
			_sFileName = filename;
            bool rv;
            using (FileStream fs = new FileStream(_sFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fs == null)
                    throw new Exception("FileStream returned null on open");
                fs.Seek(tableOffset, SeekOrigin.Begin);
                using (_br = new BinaryReader(fs))
                {
                    rv = FromBinaryReader(_br);
                }
            }
            return rv;
		}


		// Fast Binary File Reading with C#
		// 		http://www.codeproject.com/KB/files/fastbinaryfileinput.aspx
		// 		By Anthony Baraff | 28 Jun 2005 
        private static byte[] ReverseBytes(byte[] inArray)
        {
            byte temp;
            int highCtr = inArray.Length - 1;

            for (int ctr = 0; ctr < inArray.Length / 2; ctr++)
            {
                temp = inArray[ctr];
                inArray[ctr] = inArray[highCtr];
                inArray[highCtr] = temp;
                highCtr -= 1;
            }
            return inArray;
        }
		public bool FromBinaryReader(BinaryReader br)
		{
			vhdData = new BlockAllocationTableStruct(maxTableEntries);
			uint tableEntriesRead = 0;
            while (tableEntriesRead < maxTableEntries)
            {
                vhdData.SetBlock(tableEntriesRead++, (uint)System.Net.IPAddress.NetworkToHostOrder(br.ReadInt32()));
            }

			return true;
		}

	

		private void SeekArrayExample() {
			// http://www.dotnetperls.com/seek
			// 1. Open file with a BinaryReader.
			using (BinaryReader b = new BinaryReader(File.Open("perls.bin",
							FileMode.Open)))
			{
				// 2. Variables for our position.
				int pos = 50000;
				int required = 2000;

				// 3. Seek to our required position.
				b.BaseStream.Seek(pos, SeekOrigin.Begin);

				// 4. Read the next 2000 bytes.
				byte[] by = b.ReadBytes(required);
			}
		}

		public bool IsBlockAllocated(uint nBlock) {
            return (vhdData.GetBlock(nBlock) != 0xffffffff);
		}

		// [StructLayout(LayoutKind.Sequential, Size = 512, Pack = 1)]
		protected struct BlockAllocationTableStruct
		{
			public BlockAllocationTableStruct(uint nBlocks) {
				Block = new uint[nBlocks];
				this.nBlocks = nBlocks;
			}
			public void SetBlock(uint nBlock, uint value) {
				Block[nBlock] = value;
			}
			public uint GetBlock(uint nBlock) {
                if (nBlock >= nBlocks)
                {
                    throw new Exception("GetBlock attempted to access non-existant block definition");
                }
				return Block[nBlock];
			}
			uint nBlocks;
			uint[] Block;
		}

	}

	public class DynamicHeader : VhdStructReader
	{
		DynamicDiskHeaderFormat vhdData;

#region properties	// {{{

		public string Cookie
		{
			get { return new string(vhdData.Cookie); }
			set { /* TODO */ ; }
		}

		public long DataOffset
		{
			get { return vhdData.DataOffset; }
			set { vhdData.DataOffset = value; }
		}

		public long TableOffset
		{
			get { return vhdData.TableOffset; }
			set { vhdData.TableOffset = value; }
		}

		public string HeaderVersion
		{
			get { return "0x" + vhdData.HeaderVersion.ToString("x"); }
			set { vhdData.HeaderVersion = uint.Parse(value.Substring(2), System.Globalization.NumberStyles.HexNumber); }
		}

		public uint MaxTableEntries
		{
			get { return vhdData.MaxTableEntries; }
			set { vhdData.MaxTableEntries = value; }
		}

		public uint BlockSize
		{
			get { return vhdData.BlockSize; }
			set { vhdData.BlockSize = value; }
		}

		public string Checksum
		{
			get { return "0x" + vhdData.Checksum.ToString("x"); }
			set { vhdData.Checksum = uint.Parse(value.Substring(2), System.Globalization.NumberStyles.HexNumber); }
		}

		public string ParentUniqueID
		{
			get { return new string(vhdData.ParentUniqueID); }
			set { /* TODO */ ; }
		}

		public uint ParentTimeStamp
		{
			get { return vhdData.ParentTimeStamp; }
			set { vhdData.ParentTimeStamp = value; }
		}

		public string ParentUnicodeName
		{
			get { return vhdData.ParentUnicodeName; }
			set { vhdData.ParentUnicodeName = value; }
		}

#endregion // }}}

		[StructLayout(LayoutKind.Sequential, Size = 512, Pack = 1)]
			private struct DynamicDiskHeaderFormat
			{

#region Fields /*{{{*/
				// The Block Allocation Table (BAT) is a table of absolute sector
				// offsets into the file backing the hard disk. It is pointed to by the
				// “Table Offset” field of the Dynamic Disk Header.
				//
				// The size of the BAT is calculated during creation of the hard disk.
				// The number of entries in the BAT is the number of blocks needed to
				// store the contents of the disk when fully expanded. For example, a
				// 2-GB disk image that uses 2 MB blocks requires 1024 BAT entries.
				// Each entry is four bytes long. All unused table entries are
				// initialized to 0xFFFFFFFF.
				//
				// The BAT is always extended to a sector boundary. The “Max Table
				// Entries” field within the Dynamic Disk Header indicates how many
				// entries are valid. 

				// The data block consists of a sector bitmap and data. For dynamic
				// disks, the sector bitmap indicates which sectors contain valid data
				// (1’s) and which sectors have never been modified (0’s). For
				// differencing disks, the sector bitmap indicates which sectors are
				// located within the differencing disk (1’s) and which sectors are in
				// the parent (0’s). The bitmap is padded to a 512-byte sector
				// boundary. 
				//
				// A block is a power-of-two multiple of sectors. By default, the size
				// of a block is 4096 512-byte sectors (2 MB). All blocks within a
				// given image must be the same size. This size is specified in the
				// “Block Size” field of the Dynamic Disk Header.
				//
				// All sectors within a block whose corresponding bits in the bitmap
				// are zero must contain 512 bytes of zero on disk. Software that
				// accesses the disk image may take advantage of this assumption to
				// increase performance.
				//
				// Note: Although the format supports varying block sizes, Microsoft
				// Virtual PC 2004 and Virtual Server 2005 have only been tested with
				// 512K and 2 MB block sizes.
				//
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
					public char[] Cookie; // cxparse

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U8)]
					public long DataOffset;	// 0xFFFFFFFF

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U8)]
					public long TableOffset;	// Table Offset
				// This field stores the absolute byte
				// offset of the Block Allocation Table
				// (BAT) in the file. 

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint HeaderVersion;	// Header Version
				// This field stores the version of the
				// dynamic disk header. The field is
				// divided into Major/Minor version.
				// The least-significant two bytes
				// represent the minor version, and the
				// most-significant two bytes represent
				// the major version. This must match
				// with the file format specification.
				// For this specification, this field
				// must be initialized to 0x00010000. 

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint MaxTableEntries;	// Max Table Entries
				// This field holds the maximum entries
				// present in the BAT. This should be
				// equal to the number of blocks in the
				// disk (that is, the disk size divided
				// by the block size). 

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint BlockSize;		// Block Size
				// A block is a unit of expansion for
				// dynamic and differencing hard disks.
				// It is stored in bytes. This size
				// does not include the size of the
				// block bitmap. It is only the size of
				// the data section of the block. The
				// sectors per block must always be a
				// power of two. The default value is
				// 0x00200000 (indicating a block size
				// of 2 MB).

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint Checksum;

				// This field holds a basic Checksum of the
				// hard disk footer. It is just a one’s
				// complement of the sum of all the bytes
				// in the footer without the Checksum
				// field.

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
					public char[] ParentUniqueID;
				// This field is used for differencing
				// hard disks. A differencing hard disk
				// stores a 128-bit UUID of the parent
				// hard disk. For more information, see
				// “Creating Differencing Hard Disk
				// Images” later in this paper.

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint ParentTimeStamp;

				// This field is used for differencing hard disks. A differencing
				// hard disk stores a 128-bit UUID of the parent hard disk. For
				// more information, see “Creating Differencing Hard Disk Images”
				// later in this paper.

				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint Reserved;

				// This field should be set to zero. 

				[MarshalAs(UnmanagedType.LPWStr, SizeConst = 512)]
					public string ParentUnicodeName;

				// This field contains a Unicode string (UTF-16) of the parent hard disk filename. 


				// Parent Locator Entries
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry1;

				// These entries store an absolute byte offset in the file where
				// the parent locator for a differencing hard disk is stored. This
				// field is used only for differencing disks and should be set to
				// zero for dynamic disks. 

				// The following table describes the fields inside each locator entry.

				// Platform Code				4
				// Platform Data Space			4
				// Platform Data Length			4
				// Reserved						4
				// Platform Data Offset			8

				// Platform Code. The platform code describes which
				// platform-specific format is used for the file locator. For
				// Windows, a file locator is stored as a path (for example.
				// “c:\disksimages\ParentDisk.vhd”). On a Macintosh system, the
				// file locator is a binary large object (blob) that contains an
				// “alias.” The parent locator table is used to support moving hard
				// disk images across platforms.
				//
				// Some current platform codes include the following:
				//
				// Platform Code		Description
				//
				// None (0x0)	
				// Wi2r (0x57693272)	[deprecated]
				// Wi2k (0x5769326B)	[deprecated]
				// W2ru (0x57327275)	Unicode pathname (UTF-16) on Windows relative to the differencing disk pathname.
				// W2ku (0x57326B75)	Absolute Unicode (UTF-16) pathname on Windows.
				// Mac (0x4D616320)		(Mac OS alias stored as a blob)
				// MacX(0x4D616358)		A file URL with UTF-8 encoding conforming to RFC 2396.

				// Platform Data Space. This field stores the number of 512-byte
				// sectors needed to store the parent hard disk locator.
				//
				// Platform Data Length. This field stores the actual length of the
				// parent hard disk locator in bytes.
				//
				// Reserved. This field must be set to zero.
				//
				// Platform Data Offset. This field stores the absolute file offset
				// in bytes where the platform specific file locator data is
				// stored.


				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry2;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry3;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry4;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry5;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry6;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry7;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
					public byte[] ParentLocatorEntry8;

				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
					public byte[] Reserved256;
				// This must be initialized to zeroes.

#endregion /*}}}*/
			}

		public override bool FromFile(string filename)
		{
			throw new Exception("FromFile() requires seek argument");
		}

		public bool FromFile(string filename, int seek)
		{
			if (FromFileEx(filename, seek, SeekOrigin.Begin, 512))
			{
				vhdData = BytesToStruct<DynamicDiskHeaderFormat>(buffer);
				return true;
			}

			return false;
		}


	}
	public class Footer : VhdStructReader
	{
		VhdFooter vhdData;
		public override bool FromFile(string filename) 
		{
            _sFileName = filename;
            // Probably yet again, the wrong place to put this
            using (FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fs == null)
                {
                    // DOes it ever return NULL?
                    throw new Exception("FileStream returned null on open");
                }

                VhdFileSize = fs.Length;
                fs.Close();
            }

			if (FromFileEx(filename, -512, SeekOrigin.End, 512))
			{
				vhdData = BytesToStruct<VhdFooter>(buffer);
				return true;
			}

			return false;
		}
#region Properties // {{{

        public long VhdFileSize { get; set; }

		public string NameDiskType(uint n)
		{
            if (n > 4) { n = 4; }
			string[] diskTypes = {
				"None",
				"Reserved",
				"Fixed",
				"Dynamic",
				"Differencing"
			};

			return diskTypes[n];
		}

		public string Cookie
		{
			get { return new string(vhdData.Cookie); }
			set
			{ /* TODO 
				 byte[] array = Encoding.ASCII.GetBytes(input);
				 */
			}
		}

		public string CreatorApplication
		{
			get { return new string(vhdData.CreatorApplication); }
			set { /* TODO */ }
		}

		public string CreatorHostOS
		{
			get { return new string(vhdData.CreatorHostOS); }
			set { /* TODO */ }
		}

		public long DataOffset
		{
			get { return vhdData.DataOffset; }
			set { vhdData.DataOffset = value; }
		}

		public ulong OriginalSize
		{
			get { return vhdData.OriginalSize; }
			set { vhdData.OriginalSize = value; }
		}

		public ulong CurrentSize
		{
			get { return vhdData.CurrentSize; }
			set { vhdData.CurrentSize = value; }
		}

		public string UniqueId
		{
			get { return GetBytesToString(vhdData.UniqueId); }
			set { /* TODO */ }
		}

		public bool SavedState
		{
			get { return vhdData.SavedState; }
			set { vhdData.SavedState = value; }
		}

		public string Features
		{
			get { return "0x" + vhdData.Features.ToString("X"); }
			set { vhdData.Features = uint.Parse(value, System.Globalization.NumberStyles.HexNumber); }
		}

		public string FileFormatVersion
		{
			get { return "0x" + vhdData.FileFormatVersion.ToString("X"); }
			set { vhdData.FileFormatVersion = uint.Parse(value, System.Globalization.NumberStyles.HexNumber); }
		}

		public uint TimeStamp
		{
			get { return vhdData.TimeStamp; }
			set { vhdData.TimeStamp = value; }
		}

		public string CreatorVersion
		{
			get { return "0x" + vhdData.CreatorVersion.ToString("X"); }
			set { vhdData.CreatorVersion = uint.Parse(value, System.Globalization.NumberStyles.HexNumber); }
		}

		public ushort DiskCylinders
		{
			get { return vhdData.DiskCylinders; }
			set { vhdData.DiskCylinders = value; }
		}
		public byte DiskHeads
		{
			get { return vhdData.DiskHeads; }
			set { vhdData.DiskHeads = value; }
		}
		public byte DiskSectors
		{
			get { return vhdData.DiskSectors; }
			set { vhdData.DiskSectors = value; }
		}

		public string DiskType
		{
			get { return NameDiskType(vhdData.DiskType); }
			set { /* TODO vhdData.DiskType = value; */ }
		}

		public uint Checksum
		{
			get { return vhdData.Checksum; }
			set { vhdData.Checksum = value; }
		}


#endregion // }}}

		[StructLayout(LayoutKind.Sequential, Size = 512, Pack = 1)]
			protected struct VhdFooter
			{
#region Fields/*{{{*/
				/*

				   -- http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.unmanagedtype%28v=vs.71%29.aspx
				   -- http://msdn.microsoft.com/en-us/library/cbf1574z.aspx

				   Cookie                  8
				   Features                4
				   File Format Version     4
				   Data Offset             8
				   Time Stamp              4
				   Creator Application     4
				   Creator Version         4
				   Creator Host OS         4
				   Original Size           8
				   Current Size            8
				   Disk Geometry           4
				   Disk Type               4
				   Checksum                4
				   Unique Id               16
				   Saved State             1
				   Reserved                427


0000360:                     636f 6e65 6374 6978  ........conectix
0000370: 0000 0002 0001 0000 0000 0000 0000 0200  ................
0000380: 1660 e629 7165 6d75 0005 0003 5769 326b  .`.)qemu....Wi2k
0000390: 0000 0001 006e 0000 0000 0001 006e 0000  .....n.......n..
00003a0: 2090 103f 0000 0003 0000 0000 0000 0000   ..?............
00003b0: 0000 0000 0000 0000 0000 0000 0000 0000  ................
*/
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
					// [FieldOffset(0)]
					public char[] Cookie; // Microsoft uses the “conectix” string 

				// [FieldOffset(8)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint Features;
				// No features enabled  0x00000000
				// Temporary            0x00000001
				// Reserved             0x00000002

				// [FieldOffset(12)]
				[Endian(Endianness.BigEndian)]

					[MarshalAs(UnmanagedType.U4)]
					public uint FileFormatVersion;
				// This field is divided into a major/minor
				// version and matches the version of the
				// specification used in creating the file.
				// The most-significant two bytes are for
				// the major version. The least-significant
				// two bytes are the minor version. This
				// must match the file format
				// specification. For the current
				// specification, this field must be
				// initialized to 0x00010000.

				// [FieldOffset(16)]
				[MarshalAs(UnmanagedType.U8)]
					[Endian(Endianness.BigEndian)]

					public long DataOffset;
				// This field holds the absolute byte
				// offset, from the beginning of the file,
				// to the next structure. This field is
				// used for dynamic disks and differencing
				// disks, but not fixed disks. For fixed
				// disks, this field should be set to
				// 0xFFFFFFFF. 

				// [FieldOffset(24)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint TimeStamp;
				// This field stores the creation time of a
				// hard disk image. This is the number of
				// seconds since January 1, 2000 12:00:00
				// AM in UTC/GMT.

				// [FieldOffset(28)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
					public char[] CreatorApplication;
				// This field is used to document which
				// application created the hard disk. The
				// field is a left-justified text field. It
				// uses a single-byte character set. 
				//
				// If the hard disk is created by Microsoft
				// Virtual PC, "vpc " is written in this
				// field. If the hard disk image is created
				// by Microsoft Virtual Server, then "vs  "
				// is written in this field.
				//
				// Other applications should use their own
				// unique identifiers. 


				// [FieldOffset(32)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint CreatorVersion;
				// This field holds the major/minor version
				// of the application that created the hard
				// disk image.
				//
				// Virtual Server 2004 sets this value to
				// 0x00010000 and Virtual PC 2004 sets this
				// to 0x00050000.


				// [FieldOffset(36)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
					public char[] CreatorHostOS;
				// Windows      0x5769326B (Wi2k)
				// Macintosh    0x4D616320 (Mac )


				// [FieldOffset(40)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U8)]
					public ulong OriginalSize;
				// This field stores the size of the hard
				// disk in bytes, from the perspective of
				// the virtual machine, at creation time.
				// This field is for informational
				// purposes. 

				// [FieldOffset(48)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U8)]
					public ulong CurrentSize;
				// This field stores the current size of
				// the hard disk, in bytes, from the
				// perspective of the virtual machine.
				//
				// This value is same as the original size
				// when the hard disk is created. This
				// value can change depending on whether
				// the hard disk is expanded. 


				// [FieldOffset(56)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U2)]
					public ushort DiskCylinders;
				[MarshalAs(UnmanagedType.U1)]
					public byte DiskHeads;
				[MarshalAs(UnmanagedType.U1)]
					public byte DiskSectors;
				// This field stores the cylinder, heads,
				// and sectors per track value for the hard
				// disk. 
				//
				// Disk Geometry field  Size (bytes)
				// Cylinder                     2
				// Heads                        1
				// Sectors per track/cylinder   1
				//
				//
				// When a hard disk is configured as an ATA
				// hard disk, the CHS values (that is,
				// Cylinder, Heads, Sectors per track) are
				// used by the ATA controller to determine
				// the size of the disk. When the user
				// creates a hard disk of a certain size,
				// the size of the hard disk image in the
				// virtual machine is smaller than that
				// created by the user. This is because CHS
				// value calculated from the hard disk size
				// is rounded down. The pseudo-code for the
				// algorithm used to determine the CHS
				// values can be found in the appendix of
				// this document. 

				// [FieldOffset(60)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint DiskType;
				// None                         0
				// Reserved (deprecated)        1
				// Fixed hard disk              2
				// Dynamic hard disk            3
				// Differencing hard disk       4
				// Reserved (deprecated)        5
				// Reserved (deprecated)        6


				// [FieldOffset(64)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U4)]
					public uint Checksum;
				// This field holds a basic Checksum of the
				// hard disk footer. It is just a one’s
				// complement of the sum of all the bytes
				// in the footer without the Checksum
				// field.

				// [FieldOffset(68)]
				// [Endian(Endianness.BigEndian)]
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
					public byte[] UniqueId;
				// Every hard disk has a unique ID stored
				// in the hard disk. This is used to
				// identify the hard disk. This is a
				// 128-bit universally unique identifier
				// (UUID). This field is used to associate
				// a parent hard disk image with its
				// differencing hard disk image(s).

				// [FieldOffset(84)]
				[Endian(Endianness.BigEndian)]
					[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
					public bool SavedState;
				// This field holds a one-byte flag that
				// describes whether the system is in saved
				// state. If the hard disk is in the saved
				// state the value is set to 1.  Operations
				// such as compaction and expansion cannot
				// be performed on a hard disk in a saved
				// state

				//// [FieldOffset(85)]
				//[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 427)]
				// public string Reserved;
#endregion/*}}}*/
			}

        public string FileName { get { return _sFileName;} }

        public bool Changed { get; set; }

        internal void UpdateFile()
        {
            byte[] buf = StructToBytes<VhdFooter>(vhdData);
            RandomWriteVhd(-512, buf);

            if (vhdData.DiskType == 3)
            {
                // Updating secondary header for Dynamic disks.
                RandomWriteVhd(0, buf);
            }
        }
    }

	abstract public class VhdStructReader
	{
		protected FileStream _fs;
		protected string _sFileName;
		protected BinaryReader _br;
		// protected Object vhdData;
		protected byte[] buffer;

        public byte[] RandomReadVhd(long position, int count)
        {
            CSharp.cc.Files.CheckIfFileIsBeingUsed(_sFileName);
            byte[] by;
            using (BinaryReader b = new BinaryReader(File.Open(_sFileName, FileMode.Open, FileAccess.Read)))
            {
                b.BaseStream.Seek(position, SeekOrigin.Begin);
                by = b.ReadBytes(count);
            }
            CSharp.cc.Files.CheckIfFileIsBeingUsed(_sFileName);
            return by;

        }

        public void RandomWriteVhd(long position, byte[] buffer)
        {
            using (BinaryWriter b = new BinaryWriter(File.Open(_sFileName, FileMode.Open, FileAccess.ReadWrite)))
            {
                b.BaseStream.Seek(position, (position < 0) ? SeekOrigin.End : SeekOrigin.Begin);
                b.Write(buffer);
            }
        }

		abstract public bool FromFile(string filename);
		protected struct vhdDataStruct
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
				// [FieldOffset(0)]
				public char[] Cookie; // Microsoft uses the “conectix” string 
		};

		public byte[] GetStringToBytes(string value)
		{
			SoapHexBinary shb = SoapHexBinary.Parse(value);
			return shb.Value;
		}

		public string GetBytesToString(byte[] value)
		{
			SoapHexBinary shb = new SoapHexBinary(value);
			return shb.ToString();
		}

		public bool FromFileEx(string filename, long offset, SeekOrigin origin, int length)
		{   
			_sFileName = filename;
            int sum = 0;                          // total number of bytes read

            using (FileStream fs = new FileStream(_sFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                if (fs == null)
                    throw new Exception("FileStream returned null on open");
                if (fs.Length < 512)
                    throw new IOException("File is too small to be valid");
                // fs.Seek(-512, SeekOrigin.End);
                fs.Seek(offset, origin);

                // int length = 512;                     // get file length

                try
                {
                    buffer = new byte[length];            // create buffer
                    int count;                            // actual number of bytes read

                    // read until Read method returns 0 (end of the stream has been reached)
                    if ((count = fs.Read(buffer, sum, length - sum)) > 0)
                        sum += count;
                }
                finally
                {
                    fs.Close();
                }
            }
			if (sum == length)
			{

				// vhdData = ByteArrayToVhdFooter(buffer);
				return true;
			}
			return false;
		}

		public void Write(string filename)
		{
            using (_fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None))
            {
                if (_fs == null)
                    throw new Exception("Cannot call write on closed CVhdFooterWriter");
            }
			//byte[] buff = vhdData.ToByteArray();
			//_fs.Write(buff, 0, buff.Length);
			//_fs.Seek(-512, SeekOrigin.End);
			//            if(_fs != null)
			//    _fs.Close();
		}

		[AttributeUsage(AttributeTargets.Field)]
			public class EndianAttribute : Attribute
		{
			public Endianness Endianness { get; private set; }

			public EndianAttribute(Endianness endianness)
			{
				this.Endianness = endianness;
			}
		}

		public enum Endianness
		{
			BigEndian,
				LittleEndian
		}
#if DOTNET2
		private static void RespectEndianness(Type type, byte[] data)
		{
			foreach (System.Reflection.FieldInfo f in type.GetFields())
			{
				if (f.IsDefined(typeof(EndianAttribute), false))
				{
					EndianAttribute att = (EndianAttribute)f.GetCustomAttributes(typeof(EndianAttribute), false)[0];
					int offset = Marshal.OffsetOf(type, f.Name).ToInt32();
					if ((att.Endianness == Endianness.BigEndian && BitConverter.IsLittleEndian) ||
							(att.Endianness == Endianness.LittleEndian && !BitConverter.IsLittleEndian))
					{
						Array.Reverse(data, offset, Marshal.SizeOf(f.FieldType));
					}
				}
			}
		}
#else

		private static void RespectEndianness(Type type, byte[] data)
		{
			var fields = type.GetFields().Where(f => f.IsDefined(typeof(EndianAttribute), false))
				.Select(f => new
						{
						Field = f,
						Attribute = (EndianAttribute)f.GetCustomAttributes(typeof(EndianAttribute), false)[0],
						Offset = Marshal.OffsetOf(type, f.Name).ToInt32()
						}).ToList();

			foreach (var field in fields)
			{
				if ((field.Attribute.Endianness == Endianness.BigEndian && BitConverter.IsLittleEndian) ||
						(field.Attribute.Endianness == Endianness.LittleEndian && !BitConverter.IsLittleEndian))
				{
					Array.Reverse(data, field.Offset, Marshal.SizeOf(field.Field.FieldType));
				}
			}
		}
#endif

		public static T BytesToStruct<T>(byte[] rawData) where T : struct
		{
			T result = default(T);

			RespectEndianness(typeof(T), rawData);

			GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);

			try
			{
				IntPtr rawDataPtr = handle.AddrOfPinnedObject();
				result = (T)Marshal.PtrToStructure(rawDataPtr, typeof(T));
			}
			finally
			{
				handle.Free();
			}

			return result;
		}

		protected static byte[] StructToBytes<T>(T data) where T : struct
		{
			byte[] rawData = new byte[Marshal.SizeOf(data)];
			GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
			try
			{
				IntPtr rawDataPtr = handle.AddrOfPinnedObject();
				Marshal.StructureToPtr(data, rawDataPtr, false);
			}
			finally
			{
				handle.Free();
			}

			RespectEndianness(typeof(T), rawData);

			return rawData;
		}

		// Example usage: myStruct.Render(treeView1.Nodes);
		public virtual void Render(TreeNodeCollection arg)
		{

			// foreach (System.Reflection.FieldInfo field in typeof(Footer).GetFields())
			// foreach (PropertyDescriptor field in Footer)
			PropertyInfo[] Footers = GetType().GetProperties();
			foreach (PropertyInfo pi in Footers)
			{
				// Footer c = (Footer)pi.GetValue(null, null);
				// do something here with the Footer

				DescriptionAttribute[] attributes = (DescriptionAttribute[])pi.GetCustomAttributes(typeof(DescriptionAttribute), false);
				string description = attributes.Length > 0 ? attributes[0].Description : pi.ToString();

				// Show something like "My integer, 123 (Int32, i)"
				string s = string.Format("{3}: {1}",
						description, // DescriptionAttribute
						pi.GetValue(this, null), //  GetValue(this), // value of
						pi.GetType(),
						pi.Name // name of structure member
						);
				arg.Add(s);
			}

		}

		/* vim: set ts=4 sts=0 sw=4 noet: */
	}
}
