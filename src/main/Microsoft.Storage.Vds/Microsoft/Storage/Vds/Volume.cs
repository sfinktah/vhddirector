namespace Microsoft.Storage.Vds
{
    using Microsoft.Storage.Vds.Interop;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Volume : Wrapper, IDisposable
    {
        private FileSystemProperties fsProp;
        private bool isMounted;
        private bool refresh;
        private bool refreshFS;
        private VolumeProperties volProp;
        private IVdsVolume volume;
        private IVdsVolumeMF volumeMF;

        public Volume()
        {
            this.refresh = true;
            this.refreshFS = true;
            this.volume = null;
        }

        public Volume(object comUnknown, IVdsService vdsService) : base(comUnknown, vdsService)
        {
            this.refresh = true;
            this.refreshFS = true;
            this.InitializeComInterfaces();
        }

        public void AddAccessPath(string path)
        {
            this.InitializeComInterfaces();
            try
            {
                this.volumeMF.AddAccessPath(path);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumeMF::AddAccessPath failed.", exception);
            }
        }

        public Async BeginExtend(InputDisk[] inputDiskArray, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.volume.Extend(inputDiskArray, (uint) inputDiskArray.Length, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolume::Extend(...) failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public Async BeginFormat(Microsoft.Storage.Vds.FileSystemType type, string label, uint unitAllocationSize, bool force, bool quickFormat, bool enableCompression, AsyncCallback callback, object state)
        {
            IVdsAsync async;
            this.InitializeComInterfaces();
            try
            {
                this.volumeMF.Format(type, label, unitAllocationSize, force ? (uint)1 : (uint)0, quickFormat ? (uint)1 : (uint)0, enableCompression ? (uint)1 : (uint)0, out async);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumeMF::Format failed.", exception);
            }
            Async async2 = new Async(async, callback, state);
            new Thread(new ThreadStart(async2.Wait)).Start();
            return async2;
        }

        public void Delete(bool forceDismount)
        {
            this.InitializeComInterfaces();
            try
            {
                this.volume.Delete(Convert.ToUInt32(forceDismount));
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolume::Delete() failed.", exception);
            }
        }

        public void DeleteAccessPath(string path, bool force)
        {
            this.InitializeComInterfaces();
            try
            {
                this.volumeMF.DeleteAccessPath(path, force ? (uint)1 : (uint)0);
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumeMF::DeleteAccessPath failed.", exception);
            }
        }

        public void Dismount(bool force, bool permanent)
        {
            this.InitializeComInterfaces();
            try
            {
                this.volumeMF.Dismount((uint)(force ? (uint)1 : (uint)0), (uint)(permanent ? (uint)1 : (uint)0));
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumeMF::Dismount failed.", exception);
            }
        }

        public void EndExtend(IAsyncResult asyncResult)
        {
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("IVolume.Extend(...) failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public void EndFormat(IAsyncResult asyncResult)
        {
            try
            {
                uint num;
                VDS_ASYNC_OUTPUT vds_async_output;
                Async async = (Async) asyncResult;
                if (async == null)
                {
                    throw new ArgumentNullException("asyncResult");
                }
                InteropHelpers.QueryInterfaceThrow<IVdsAsync>(async.ComUnknown).Wait(out num, out vds_async_output);
                if (num != 0)
                {
                    throw new VdsException("Format failed with the following error code: " + num);
                }
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsAsync::Wait failed.", exception);
            }
        }

        public override void InitializeComInterfaces()
        {
            base.InitializeComInterfaces();
            if (this.volume == null)
            {
                this.volume = InteropHelpers.QueryInterface<IVdsVolume>(base.ComUnknown);
                if (this.volume == null)
                {
                    throw new VdsException("QueryInterface for IVdsVolume failed.");
                }
            }
            if (this.volumeMF == null)
            {
                this.volumeMF = InteropHelpers.QueryInterface<IVdsVolumeMF>(base.ComUnknown);
                if (this.volumeMF == null)
                {
                    throw new VdsException("QueryInterface for IVdsVolumeMF failed.");
                }
            }
        }

        public void InitializeFileSystemProperties()
        {
            this.InitializeComInterfaces();
            if (this.refreshFS)
            {
                try
                {
                    this.volumeMF.GetFileSystemProperties(out this.fsProp);
                }
                catch (COMException exception)
                {
                    if (exception.ErrorCode == -2147212209)
                    {
                        this.isMounted = false;
                        this.refreshFS = false;
                    }
                    throw new VdsException("The call to IVdsVolumeMF::GetFileSystemProperties failed.", exception);
                }
                this.isMounted = true;
                this.refreshFS = false;
            }
        }

        public override void InitializeProperties()
        {
            this.InitializeComInterfaces();
            if (this.refresh)
            {
                try
                {
                    this.volume.GetProperties(out this.volProp);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolume::GetProperties failed.", exception);
                }
                this.refresh = false;
            }
        }

        public void Mount()
        {
            this.InitializeComInterfaces();
            try
            {
                this.volumeMF.Mount();
            }
            catch (COMException exception)
            {
                throw new VdsException("The call to IVdsVolumeMF::Mount failed.", exception);
            }
        }

        public void Refresh()
        {
            this.refresh = true;
            this.refreshFS = true;
        }

        public List<string> AccessPaths
        {
            get
            {
                IntPtr ptr2;
                int num;
                int num2 = Marshal.SizeOf(typeof(IntPtr));
                this.InitializeComInterfaces();
                try
                {
                    this.volumeMF.QueryAccessPaths(out ptr2, out num);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolumeMF::QueryAccessPaths failed", exception);
                }
                List<string> list = new List<string>(num);
                IntPtr ptr = ptr2;
                for (int i = 0; i < num; i++)
                {
                    IntPtr ptr3 = Marshal.ReadIntPtr(ptr);
                    list.Add(Marshal.PtrToStringAuto(ptr3));
                    Marshal.FreeCoTaskMem(ptr3);
                    ptr = Utilities.IntPtrAddOffset(ptr, (uint) num2);
                }
                Marshal.FreeCoTaskMem(ptr2);
                return list;
            }
        }

        public uint AllocationUnitSize
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.AllocationUnitSize;
            }
        }

        public ulong AvailableAllocationUnits
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.AvailableAllocationUnits;
            }
        }

        public bool Compressed
        {
            get
            {
                this.InitializeFileSystemProperties();
                return (Microsoft.Storage.Vds.FileSystemFlags.Compressed == (this.fsProp.Flags & Microsoft.Storage.Vds.FileSystemFlags.Compressed));
            }
            set
            {
                if (value)
                {
                    try
                    {
                        this.volumeMF.SetFileSystemFlags(Microsoft.Storage.Vds.FileSystemFlags.Compressed);
                        return;
                    }
                    catch (COMException exception)
                    {
                        throw new VdsException("The call to IVdsVolumeMF::SetFileSystemFlags failed.", exception);
                    }
                }
                try
                {
                    this.volumeMF.ClearFileSystemFlags(Microsoft.Storage.Vds.FileSystemFlags.Compressed);
                }
                catch (COMException exception2)
                {
                    throw new VdsException("The call to IVdsVolumeMF::ClearFileSystemFlags failed.", exception2);
                }
            }
        }

        public char DriveLetter
        {
            get
            {
                List<string> accessPaths = this.AccessPaths;
                if ((accessPaths.Count != 0) && accessPaths[0].EndsWith(@":\", StringComparison.OrdinalIgnoreCase))
                {
                    return accessPaths[0][0];
                }
                return ' ';
            }
            set
            {
                if (char.IsLetter(value))
                {
                    string path = value + @":\";
                    this.AddAccessPath(path);
                }
                else
                {
                    if (value != ' ')
                    {
                        throw new ArgumentOutOfRangeException("value", value, "The specified character is not a letter");
                    }
                    List<string> accessPaths = this.AccessPaths;
                    if ((accessPaths.Count != 0) && accessPaths[0].EndsWith(@":\", StringComparison.OrdinalIgnoreCase))
                    {
                        this.DeleteAccessPath(accessPaths[0], false);
                    }
                }
            }
        }

        public Microsoft.Storage.Vds.FileSystemFlags FileSystemFlags
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.Flags;
            }
        }

        public Microsoft.Storage.Vds.FileSystemType FileSystemType
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.Type;
            }
        }

        public VolumeFlags Flags
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Flags;
            }
        }

        public Microsoft.Storage.Vds.Health Health
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Health;
            }
        }

        public Guid Id
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Id;
            }
        }

        public bool IsMounted
        {
            get
            {
                try
                {
                    this.InitializeFileSystemProperties();
                }
                catch (VdsException exception)
                {
                    if (((COMException) exception.InnerException).ErrorCode != -2147212209)
                    {
                        throw exception;
                    }
                }
                return this.isMounted;
            }
        }

        public string Label
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.Label;
            }
        }

        public string Name
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Name;
            }
        }

        public Microsoft.Storage.Vds.Pack Pack
        {
            get
            {
                Microsoft.Storage.Vds.Pack pack;
                this.InitializeComInterfaces();
                try
                {
                    IVdsPack pack2;
                    this.volume.GetPack(out pack2);
                    pack = new Microsoft.Storage.Vds.Pack(pack2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolume::GetPack failed.", exception);
                }
                return pack;
            }
        }

        public Collection<VolumePlex> Plexes
        {
            get
            {
                Collection<VolumePlex> collection;
                try
                {
                    IEnumVdsObject obj2;
                    this.volume.QueryPlexes(out obj2);
                    collection = new Collection<VolumePlex>(obj2, base.VdsService);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolume::QueryPlexes(...) failed.", exception);
                }
                return collection;
            }
        }

        public Microsoft.Storage.Vds.FileSystemType RecommendedFileSystemType
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.RecommendedFileSystemType;
            }
        }

        public List<ReparsePointProperties> ReparsePoints
        {
            get
            {
                IntPtr ptr2;
                int num;
                this.InitializeComInterfaces();
                try
                {
                    this.volumeMF.QueryReparsePoints(out ptr2, out num);
                }
                catch (COMException exception)
                {
                    throw new VdsException("The call to IVdsVolumeMF::QueryReparsePoints failed.", exception);
                }
                List<ReparsePointProperties> list = new List<ReparsePointProperties>(num);
                IntPtr ptr = ptr2;
                for (int i = 0; i < num; i++)
                {
                    ReparsePointProperties item = (ReparsePointProperties) Marshal.PtrToStructure(ptr, typeof(ReparsePointProperties));
                    list.Add(item);
                    Marshal.DestroyStructure(ptr, typeof(ReparsePointProperties));
                    ptr = Utilities.IntPtrAddOffset(ptr, (uint) Marshal.SizeOf(typeof(ReparsePointProperties)));
                }
                Marshal.FreeCoTaskMem(ptr2);
                return list;
            }
        }

        public ulong Size
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Size;
            }
        }

        public VolumeStatus Status
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Status;
            }
        }

        public ulong TotalAllocationUnits
        {
            get
            {
                this.InitializeFileSystemProperties();
                return this.fsProp.TotalAllocationUnits;
            }
        }

        public Microsoft.Storage.Vds.TransitionState TransitionState
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.TransitionState;
            }
        }

        public VolumeType Type
        {
            get
            {
                this.InitializeProperties();
                return this.volProp.Type;
            }
        }
    }
}

