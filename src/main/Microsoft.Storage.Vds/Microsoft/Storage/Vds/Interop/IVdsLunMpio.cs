namespace Microsoft.Storage.Vds.Interop
{
    using Microsoft.Storage.Vds;
    using System;
    using System.Runtime.InteropServices;

    [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("7c5fbae3-333a-48a1-a982-33c15788cde3")]
    public interface IVdsLunMpio
    {
        void GetPathInfo(out IntPtr pathInfoArray, out int numberOfPaths);
        void GetLoadBalancePolicy(out LoadBalancePolicyType policy, out IntPtr pathPolicyArray, out int numberOfPaths);
        void SetLoadBalancePolicy([In] LoadBalancePolicyType policy, [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex=2)] VdsPathPolicy[] pathPolicy, [In] int numberOfPaths);
        void GetSupportedLbPolicies(out LoadBalanceSupportFlags flags);
    }
}

