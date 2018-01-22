namespace Microsoft.Storage.Vds.Advanced
{
    using Microsoft.Storage.Vds;
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class AdvancedDisk : Disk, IDisposable
    {
        private IVdsAdvancedDisk advancedDisk;
        private IVdsCreatePartitionEx createPartitionEx;
        private IVdsDiskOnline diskOnline;

        public AdvancedDisk(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.InitializeComInterfaces();
        }

        public Async BeginCreatePartitionMbr(ulong offset, ulong size, uint align, byte mbrPartitionType, bool bootIndicator, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            CreatePartitionParameters parameters = new CreatePartitionParameters();
            this.InitializeComInterfaces();
            parameters.Style = PartitionStyle.Mbr;
            parameters.PartitionInfo.Mbr.BootIndicator = bootIndicator ? 1U : 0U;
            parameters.PartitionInfo.Mbr.PartitionType = mbrPartitionType;
            try
            {
                this.createPartitionEx.CreatePartitionEx(offset, size, align, parameters, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAdvancedDisk::CreatePartition failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Partition EndCreatePartitionMbr(IAsyncResult asyncResult)
        {
            VDS_ASYNC_OUTPUT vds_async_output;
            PartitionProperties properties;
            this.InitializeComInterfaces();
            try
            {
                uint num;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Create partition failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
            try
            {
                this.advancedDisk.GetPartitionProperties(vds_async_output.Info.Cp.Offset, out properties);
            }
            catch (COMException exception2)
            {
                throw new VdsException("The call to IVdsAdvancedDisk::GetPartitionProperties failed.", exception2);
            }
            return new MbrPartition(properties, this.advancedDisk);
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.advancedDisk == null)
            {
                this.advancedDisk = InteropHelpers.QueryInterface<IVdsAdvancedDisk>(base.ComUnknown);
                if (this.advancedDisk == null)
                {
                    throw new VdsException("QueryInterface for IVdsAdvancedDisk failed.");
                }
            }
            if (this.createPartitionEx == null)
            {
                this.createPartitionEx = InteropHelpers.QueryInterface<IVdsCreatePartitionEx>(base.ComUnknown);
                if (this.createPartitionEx == null)
                {
                    throw new VdsException("QueryInterface for IVdsCreatePartitionEx failed.");
                }
            }
            if (this.diskOnline == null)
            {
                this.diskOnline = InteropHelpers.QueryInterface<IVdsDiskOnline>(base.ComUnknown);
            }
        }

        public void Offline()
        {
            this.InitializeComInterfaces();
            if (this.diskOnline == null)
            {
                throw new VdsException("QueryInterface for IVdsDiskOnline failed.");
            }
            try
            {
                this.diskOnline.Offline();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsDiskOnline::Offline failed.", exception);
            }
        }

        public void Online()
        {
            this.InitializeComInterfaces();
            if (this.diskOnline == null)
            {
                throw new VdsException("QueryInterface for IVdsDiskOnline failed.");
            }
            try
            {
                this.diskOnline.Online();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsDiskOnline::Online failed.", exception);
            }
        }

        public List<Partition> Partitions
        {
            get
            {
                List<Partition> list;
                this.InitializeComInterfaces();
                PartitionStyle partitionStyle = base.PartitionStyle;
                try
                {
                    IntPtr ptr2;
                    int num;
                    this.advancedDisk.QueryPartitions(out ptr2, out num);
                    IntPtr ptr = ptr2;
                    list = new List<Partition>(num);
                    try
                    {
                        for (int i = 0; i < num; i++)
                        {
                            PartitionProperties partitionProperties = (PartitionProperties) Marshal.PtrToStructure(ptr, typeof(PartitionProperties));
                            switch (partitionStyle)
                            {
                                case PartitionStyle.Mbr:
                                    list.Add(new MbrPartition(partitionProperties, this.advancedDisk));
                                    break;

                                case PartitionStyle.Gpt:
                                {
                                    int num3 = Marshal.OffsetOf(typeof(PartitionProperties), "Info").ToInt32() + Marshal.OffsetOf(typeof(GptPartitionInfo), "Name").ToInt32();
                                    string partitionName = Marshal.PtrToStringAuto(Utilities.IntPtrAddOffset(ptr, (uint) num3));
                                    list.Add(new GptPartition(partitionProperties, partitionName, this.advancedDisk));
                                    break;
                                }
                                case PartitionStyle.Unknown:
                                    list.Add(new Partition(partitionProperties, this.advancedDisk));
                                    break;
                            }
                            Marshal.DestroyStructure(ptr, typeof(PartitionProperties));
                            ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(PartitionProperties)));
                        }
                    }
                    finally
                    {
                        Marshal.FreeCoTaskMem(ptr2);
                    }
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsAdvancedDisk::QueryPartitions failed.", exception);
                }
                return list;
            }
        }
    }
}

