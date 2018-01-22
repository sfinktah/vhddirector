namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("daebeef3-8523-47ed-a2b9-05cecce2a1ae")]
    public interface IVdsMaintenance
    {
        void StartMaintenance([In] MaintenanceOperation operation);
        void StopMaintenance([In] MaintenanceOperation operation);
        void PulseMaintenance([In] MaintenanceOperation operation, [In] uint count);
    }
}

