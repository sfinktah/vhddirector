using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    namespace BCDConstants
    {
        // [Guid("48ECC24D-FC20-44A7-B616-710B589CD21B")]
        public enum BcdBootMgrElementTypes
        {
            BcdBootMgrBoolean_AttemptResume = 0x26000005,
            BcdBootMgrBoolean_DisplayBootMenu = 0x26000020,
            BcdBootMgrBoolean_NoErrorDisplay = 0x26000021,
            BcdBootMgrDevice_BcdDevice = 0x21000022,
            BcdBootMgrInteger_Timeout = 0x25000004,
            BcdBootMgrObject_DefaultObject = 0x23000003,
            BcdBootMgrObject_ResumeObject = 0x23000006,
            BcdBootMgrObjectList_BootSequence = 0x24000002,
            BcdBootMgrObjectList_CustomActions = 0x27000030,
            BcdBootMgrObjectList_DisplayOrder = 0x24000001,
            BcdBootMgrObjectList_ToolsDisplayOrder = 0x24000010,
            BcdBootMgrString_BcdFilePath = 0x22000023
        }

        // [Guid("8CF31856-D37B-4FD3-80DA-86952054D7A6")]
        public enum BcdDeviceObjectElementTypes
        {
            BcdDeviceBoolean_ExportAsCdRamdiskImageOffset = 0x36000006,
            BcdDeviceDevice_SdiDevice = 0x31000003,
            BcdDeviceInteger_MulticastEnabled = 0x36000009,
            BcdDeviceInteger_MulticastTftpFallback = 0x3600000a,
            BcdDeviceInteger_RamdiskImageLength = 0x35000005,
            BcdDeviceInteger_RamdiskImageOffset = 0x35000001,
            BcdDeviceInteger_TftpBlockSize = 0x35000007,
            BcdDeviceInteger_TftpClientPort = 0x35000002,
            BcdDeviceInteger_TftpWindowSize = 0x35000008,
            BcdDeviceString_SdiPath = 0x32000004
        }

        // [Guid("EC8883F8-90B9-417B-A7A7-4DB599639FE6")]
        public enum BcdLibrary_ConfigAccessPolicy
        {
            ConfigAccessPolicyDefault,
            ConfigAccessPolicyDisallowMmConfig
        }

        // [Guid("515F1C87-65DE-4E4C-953D-0612C9876EDA")]
        public enum BcdLibrary_DebuggerStartPolicy
        {
            DebuggerStartActive,
            DebuggerStartAutoEnable,
            DebuggerStartDisable
        }

        // [Guid("CE902CA0-13A5-48C7-910E-87B399C488B1")]
        public enum BcdLibrary_DebuggerType
        {
            DebuggerSerial,
            Debugger1394,
            DebuggerUsb
        }

        // [Guid("23B9871B-25C4-45D9-8510-62D894C8CD9B")]
        public enum BcdLibrary_FirstMegabytePolicy
        {
            FirstMegabytePolicyUseNone,
            FirstMegabytePolicyUseAll,
            FirstMegabytePolicyUsePrivate
        }

        // [Guid("BF9A555C-5298-4B64-B21F-77D045974D18")]
        public enum BcdLibrary_SiPolicy
        {
            IntegrityServicesDefault,
            IntegrityServicesEnable,
            IntegrityServicesDisable
        }

        // [Guid("384DA285-55B5-414F-8EC7-165E6ABA2655")]
        public enum BcdLibraryElementTypes
        {
            BcdLibraryBoolean_AllowBadMemoryAccess = 0x1600000b,
            BcdLibraryBoolean_AllowPrereleaseSignatures = 0x16000049,
            BcdLibraryBoolean_AttemptNonBcdStart = 0x16000031,
            BcdLibraryBoolean_AutoRecoveryEnabled = 0x16000009,
            BcdLibraryBoolean_ConsoleExtendedInput = 0x16000050,
            BcdLibraryBoolean_DebuggerEnabled = 0x16000010,
            BcdLibraryBoolean_DebuggerIgnoreUsermodeExceptions = 0x16000017,
            BcdLibraryBoolean_DisableIntegrityChecks = 0x16000048,
            BcdLibraryBoolean_DisplayAdvancedOptions = 0x16000040,
            BcdLibraryBoolean_DisplayOptionsEdit = 0x16000041,
            BcdLibraryBoolean_EmsEnabled = 0x16000020,
            BcdLibraryBoolean_GraphicsModeDisabled = 0x16000046,
            BcdLibraryBoolean_TraditionalKsegMappings = 0x1600000f,
            BcdLibraryDevice_ApplicationDevice = 0x11000001,
            BcdLibraryInteger_1394DebuggerChannel = 0x15000015,
            BcdLibraryInteger_AvoidLowPhysicalMemory = 0x1500000e,
            BcdLibraryInteger_ConfigAccessPolicy = 0x15000047,
            BcdLibraryInteger_DebuggerStartPolicy = 0x15000018,
            BcdLibraryInteger_DebuggerType = 0x15000011,
            BcdLibraryInteger_EmsBaudRate = 0x15000023,
            BcdLibraryInteger_EmsPort = 0x15000022,
            BcdLibraryInteger_FirstMegabytePolicy = 0x1500000c,
            BcdLibraryInteger_FVEKeyRingAddress = 0x15000042,
            BcdLibraryInteger_GraphicsResolution = 0x15000052,
            BcdLibraryInteger_InitialConsoleInput = 0x15000051,
            BcdLibraryInteger_RelocatePhysicalMemory = 0x1500000d,
            BcdLibraryInteger_SerialDebuggerBaudRate = 0x15000014,
            BcdLibraryInteger_SerialDebuggerPort = 0x15000013,
            BcdLibraryInteger_SerialDebuggerPortAddress = 0x15000012,
            BcdLibraryInteger_SiPolicy = 0x1500004b,
            BcdLibraryInteger_TruncatePhysicalMemory = 0x15000007,
            BcdLibraryIntegerList_BadMemoryList = 0x1700000a,
            BcdLibraryObjectList_InheritedObjects = 0x14000006,
            BcdLibraryObjectList_RecoverySequence = 0x14000008,
            BcdLibraryString_ApplicationPath = 0x12000002,
            BcdLibraryString_DebuggerBusParameter = 0x12000019,
            BcdLibraryString_Description = 0x12000004,
            BcdLibraryString_FontPath = 0x1200004a,
            BcdLibraryString_LoadOptionsString = 0x12000030,
            BcdLibraryString_PreferredLocale = 0x12000005,
            BcdLibraryString_UsbDebuggerTargetName = 0x12000016
        }

        // [Guid("A792F002-D701-4C93-968E-F105F827E393")]
        public enum BcdMemDiag_TestMix
        {
            TestMixBasic,
            TestMixExtended
        }

        // [Guid("045979A6-B9D1-4809-B8C9-7FEDF7BE8ABA")]
        public enum BcdMemDiag_TesttoFail
        {
            MemtestStride,
            MemtestMats,
            MemtestInverseCoupling,
            MemtestRandomPattern,
            MemtestCheckerboard
        }

        // [Guid("934CE6DA-8242-493E-B843-E781C96BD98D")]
        public enum BcdMemDiagElementTypes
        {
            BcdMemDiagBoolean_Cacheenable = 0x26000005,
            BcdMemDiagInteger_FailureCount = 0x25000003,
            BcdMemDiagInteger_PassCount = 0x25000001,
            BcdMemDiagInteger_TestMix = 0x25000002,
            BcdMemDiagInteger_TestToFail = 0x25000004
        }

        // [Guid("8D2EEEC8-B78F-40BD-99E8-0BCB8883AFA1")]
        public enum BcdOSLoader_BootStatusPolicy
        {
            BootStatusPolicyDisplayAllFailures,
            BootStatusPolicyIgnoreAllFailures,
            BootStatusPolicyIgnoreShutdownFailures,
            BootStatusPolicyIgnoreBootFailures
        }

        // [Guid("958963FA-10B0-4468-BAEC-F62E5511EADB")]
        public enum BcdOSLoader_BootUxPolicy
        {
            BgPolicyDisabled,
            BgPolicyBasic,
            BgPolicyStandard
        }

        // [Guid("593F0515-0A2B-40F2-AFA5-D25882E744D7")]
        public enum BcdOSLoader_DriverLoadFailurePolicy
        {
            DriverLoadFailurePolicyFatal,
            DriverLoadFailurePolicyUseErrorControl
        }

        // [Guid("593AF3B0-445B-11DB-B0DE-0800200C9A66")]
        public enum BcdOSLoader_HypervisorDebuggerType
        {
            HypervisorDebuggerSerial,
            HypervisorDebugger1394
        }

        // [Guid("E4D150F0-445A-11DB-B0DE-0800200C9A66")]
        public enum BcdOSLoader_HypervisorLaunchType
        {
            HypervisorLaunchOff,
            HypervisorLaunchAuto
        }

        // [Guid("F2022D3F-E2CF-4C4A-ABDF-2FF23CEB1384")]
        public enum BcdOSLoader_NxPolicy
        {
            NxPolicyOptIn,
            NxPolicyOptOut,
            NxPolicyAlwaysOff,
            NxPolicyAlwaysOn
        }

        // [Guid("3E0B67FA-9545-4D18-94A0-5F8A10DE957E")]
        public enum BcdOSLoader_PAEPolicy
        {
            PaePolicyDefault,
            PaePolicyForceEnable,
            PaePolicyForceDisable
        }

        // [Guid("C7C65422-E94F-47AC-906B-AB655E1BE5DA")]
        public enum BcdOSLoader_SafeBoot
        {
            SafemodeMinimal,
            SafemodeNetwork,
            SafemodeDsRepair
        }

        // [Guid("58535AB6-31DA-43FE-8D87-3DE008476916")]
        public enum BcdOSLoader_TpmBootEntropyPolicy
        {
            TpmBootEntropyPolicyDefault,
            TpmBootEntropyPolicyForceDisable,
            TpmBootEntropyPolicyForceEnable
        }

        // [Guid("BBDF9516-5FD7-46A3-8BAE-C7BBE13191C8")]
        public enum BcdOSLoader_X2APICPolicy
        {
            X2APICPolicyDefault,
            X2APICPolicyDisable,
            X2APICPolicyEnable
        }

        // [Guid("467FBB0D-AF40-40EA-AB09-E2485A213C74")]
        public enum BcdOSLoaderElementTypes
        {
            BcdOSLoaderBoolean_AllowPrereleaseSignatures = 0x26000027,
            BcdOSLoaderBoolean_BootLogInitialization = 0x26000090,
            BcdOSLoaderBoolean_DebuggerHalBreakpoint = 0x260000a1,
            BcdOSLoaderBoolean_DetectKernelAndHal = 0x26000010,
            BcdOSLoaderBoolean_DisableBootDisplay = 0x26000041,
            BcdOSLoaderBoolean_DisableCodeIntegrityChecks = 0x26000026,
            BcdOSLoaderBoolean_DisableCrashAutoReboot = 0x26000024,
            BcdOSLoaderBoolean_DisableVesaBios = 0x26000042,
            BcdOSLoaderBoolean_EmsEnabled = 0x260000b0,
            BcdOSLoaderBoolean_ForceGroupAwareness = 0x26000065,
            BcdOSLoaderBoolean_ForceMaximumProcessors = 0x26000062,
            BcdOSLoaderBoolean_GroupSize = 0x25000066,
            BcdOSLoaderBoolean_HypervisorDebuggerEnabled = 0x260000f2,
            BcdOSLoaderBoolean_KernelDebuggerEnabled = 0x260000a0,
            BcdOSLoaderBoolean_MaximizeGroupsCreated = 0x26000064,
            BcdOSLoaderBoolean_NoLowMemory = 0x26000030,
            BcdOSLoaderBoolean_ProcessorConfigurationFlags = 0x25000063,
            BcdOSLoaderBoolean_SafeBootAlternateShell = 0x26000081,
            BcdOSLoaderBoolean_StampDisks = 0x26000004,
            BcdOSLoaderBoolean_UseBootProcessorOnly = 0x26000060,
            BcdOSLoaderBoolean_UseLastGoodSettings = 0x26000025,
            BcdOSLoaderBoolean_UseLegacyApicMode = 0x26000054,
            BcdOSLoaderBoolean_UsePhysicalDestination = 0x26000051,
            BcdOSLoaderBoolean_UsePlatformClock = 0x260000a2,
            BcdOSLoaderBoolean_UseVgaDriver = 0x26000040,
            BcdOSLoaderBoolean_VerboseObjectLoadMode = 0x26000091,
            BcdOSLoaderBoolean_WinPEMode = 0x26000022,
            BcdOSLoaderDevice_OSDevice = 0x21000001,
            BcdOSLoaderInteger_BootStatusPolicy = 0x250000e0,
            BcdOSLoaderInteger_BootUxPolicy = 0x250000f7,
            BcdOSLoaderInteger_ClusterModeAddressing = 0x25000050,
            BcdOSLoaderInteger_DriverLoadFailurePolicy = 0x250000c1,
            BcdOSLoaderInteger_ForceFailure = 0x250000c0,
            BcdOSLoaderInteger_HypervisorDebugger1394Channel = 0x250000f6,
            BcdOSLoaderInteger_HypervisorDebuggerBaudrate = 0x250000f5,
            BcdOSLoaderInteger_HypervisorDebuggerPortNumber = 0x250000f4,
            BcdOSLoaderInteger_HypervisorDebuggerType = 0x250000f3,
            BcdOSLoaderInteger_HypervisorLaunchType = 0x250000f0,
            BcdOSLoaderInteger_HypervisorSlatDisabled = 0x260000f8,
            BcdOSLoaderInteger_HypervisorUseLargeVTlb = 0x260000fc,
            BcdOSLoaderInteger_IncreaseUserVa = 0x25000032,
            BcdOSLoaderInteger_NumberOfProcessors = 0x25000061,
            BcdOSLoaderInteger_NxPolicy = 0x25000020,
            BcdOSLoaderInteger_PAEPolicy = 0x25000021,
            BcdOSLoaderInteger_PciExpressPolicy = 0x25000072,
            BcdOSLoaderInteger_PerformaceDataMemory = 0x25000033,
            BcdOSLoaderInteger_RemoveMemory = 0x25000031,
            BcdOSLoaderInteger_RestrictApicCluster = 0x25000052,
            BcdOSLoaderInteger_SafeBoot = 0x25000080,
            BcdOSLoaderInteger_TpmBootEntropyPolicy = 0x25000100,
            BcdOSLoaderInteger_UseFirmwarePciSettings = 0x25000070,
            BcdOSLoaderInteger_X2ApicPolicy = 0x25000055,
            BcdOSLoaderObject_AssociatedResumeObject = 0x23000003,
            BcdOSLoaderString_DbgTransportPath = 0x22000013,
            BcdOSLoaderString_HalPath = 0x22000012,
            BcdOSLoaderString_HypervisorPath = 0x220000f1,
            BcdOSLoaderString_KernelPath = 0x22000011,
            BcdOSLoaderString_OSLoaderTypeEVStore = 0x22000053,
            BcdOSLoaderString_SystemRoot = 0x22000002
        }

        // [Guid("33C36119-64F2-48C0-A867-BD068A78ABCD")]
        public enum BcdResumeElementTypes
        {
            BcdResumeBoolean_DebugOptionEnabled = 0x26000006,
            BcdResumeBoolean_UseCustomSettings = 0x26000003,
            BcdResumeBoolean_x86PaeMode = 0x26000004,
            BcdResumeDevice_AssociatedOsDevice = 0x21000005,
            BcdResumeDevice_HiberFileDevice = 0x21000001,
            BcdResumeInteger_BootUxPolicy = 0x25000007,
            BcdResumeString_HiberFilePath = 0x22000002
        }

        // [Guid("58262605-0332-4775-ADE2-233C712AC121")]
        public enum BcdSetup_DeviceType
        {
            NULL_ENUM_VALUE,
            DeviceBootPartition,
            DeviceWindowsPartition,
            DeviceRamdiskBootPartition,
            DeviceRamdiskWindowsPartition
        }

        // [Guid("3DDDE433-7343-4B67-B5E7-F66742892B6B")]
        public enum BcdSetupTemplateElementTypes
        {
            BcdSetupBoolean_OmitOsLoaderElements = 0x46000004,
            BcdSetupBoolean_RecoveryOs = 0x46000010,
            BcdSetupInteger_DeviceType = 0x45000001,
            BcdSetupString_ApplicationRelativePath = 0x42000002,
            BcdSetupString_RamdiskDeviceRelativePath = 0x42000003
        }
    }

