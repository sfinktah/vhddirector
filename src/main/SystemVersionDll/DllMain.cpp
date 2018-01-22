#include <stdio.h>
#include <Windows.h>
#include <windows.h>
#include <string>
#include <sstream>

typedef void (WINAPI *PGNSI)(LPSYSTEM_INFO);
typedef BOOL (WINAPI *PGPI)(DWORD, DWORD, DWORD, DWORD, PDWORD);
#define PRODUCT_PROFESSIONAL    0x00000030
#define VER_SUITE_WH_SERVER     0x00008000

/*

#define VER_NT_WORKSTATION              0x0000001
#define VER_NT_DOMAIN_CONTROLLER        0x0000002
#define VER_NT_SERVER                   0x0000003

//
// dwPlatformId defines:
//

#define VER_PLATFORM_WIN32s             0
#define VER_PLATFORM_WIN32_WINDOWS      1
#define VER_PLATFORM_WIN32_NT           2



//
// Product types
// This list grows with each OS release.
//
// There is no ordering of values to ensure callers
// do an equality test i.e. greater-than and less-than
// comparisons are not useful.
//
// NOTE: Values in this list should never be deleted.
//       When a product-type 'X' gets dropped from a
//       OS release onwards, the value of 'X' continues
//       to be used in the mapping table of GetProductInfo.
//

#define PRODUCT_UNDEFINED                           0x00000000

#define PRODUCT_ULTIMATE                            0x00000001
#define PRODUCT_HOME_BASIC                          0x00000002
#define PRODUCT_HOME_PREMIUM                        0x00000003
#define PRODUCT_ENTERPRISE                          0x00000004
#define PRODUCT_HOME_BASIC_N                        0x00000005
#define PRODUCT_BUSINESS                            0x00000006
#define PRODUCT_STANDARD_SERVER                     0x00000007
#define PRODUCT_DATACENTER_SERVER                   0x00000008
#define PRODUCT_SMALLBUSINESS_SERVER                0x00000009
#define PRODUCT_ENTERPRISE_SERVER                   0x0000000A
#define PRODUCT_STARTER                             0x0000000B
#define PRODUCT_DATACENTER_SERVER_CORE              0x0000000C
#define PRODUCT_STANDARD_SERVER_CORE                0x0000000D
#define PRODUCT_ENTERPRISE_SERVER_CORE              0x0000000E
#define PRODUCT_ENTERPRISE_SERVER_IA64              0x0000000F
#define PRODUCT_BUSINESS_N                          0x00000010
#define PRODUCT_WEB_SERVER                          0x00000011
#define PRODUCT_CLUSTER_SERVER                      0x00000012
#define PRODUCT_HOME_SERVER                         0x00000013
#define PRODUCT_STORAGE_EXPRESS_SERVER              0x00000014
#define PRODUCT_STORAGE_STANDARD_SERVER             0x00000015
#define PRODUCT_STORAGE_WORKGROUP_SERVER            0x00000016
#define PRODUCT_STORAGE_ENTERPRISE_SERVER           0x00000017
#define PRODUCT_SERVER_FOR_SMALLBUSINESS            0x00000018
#define PRODUCT_SMALLBUSINESS_SERVER_PREMIUM        0x00000019
#define PRODUCT_HOME_PREMIUM_N                      0x0000001A
#define PRODUCT_ENTERPRISE_N                        0x0000001B
#define PRODUCT_ULTIMATE_N                          0x0000001C
#define PRODUCT_WEB_SERVER_CORE                     0x0000001D
#define PRODUCT_MEDIUMBUSINESS_SERVER_MANAGEMENT    0x0000001E
#define PRODUCT_MEDIUMBUSINESS_SERVER_SECURITY      0x0000001F
#define PRODUCT_MEDIUMBUSINESS_SERVER_MESSAGING     0x00000020
#define PRODUCT_SERVER_FOUNDATION                   0x00000021
#define PRODUCT_HOME_PREMIUM_SERVER                 0x00000022
#define PRODUCT_SERVER_FOR_SMALLBUSINESS_V          0x00000023
#define PRODUCT_STANDARD_SERVER_V                   0x00000024
#define PRODUCT_DATACENTER_SERVER_V                 0x00000025
#define PRODUCT_ENTERPRISE_SERVER_V                 0x00000026
#define PRODUCT_DATACENTER_SERVER_CORE_V            0x00000027
#define PRODUCT_STANDARD_SERVER_CORE_V              0x00000028
#define PRODUCT_ENTERPRISE_SERVER_CORE_V            0x00000029
#define PRODUCT_HYPERV                              0x0000002A
#define PRODUCT_STORAGE_EXPRESS_SERVER_CORE         0x0000002B
#define PRODUCT_STORAGE_STANDARD_SERVER_CORE        0x0000002C
#define PRODUCT_STORAGE_WORKGROUP_SERVER_CORE       0x0000002D
#define PRODUCT_STORAGE_ENTERPRISE_SERVER_CORE      0x0000002E
#define PRODUCT_STARTER_N                           0x0000002F
#define PRODUCT_PROFESSIONAL                        0x00000030
#define PRODUCT_PROFESSIONAL_N                      0x00000031
#define PRODUCT_SB_SOLUTION_SERVER                  0x00000032
#define PRODUCT_SERVER_FOR_SB_SOLUTIONS             0x00000033
#define PRODUCT_STANDARD_SERVER_SOLUTIONS           0x00000034
#define PRODUCT_STANDARD_SERVER_SOLUTIONS_CORE      0x00000035
#define PRODUCT_SB_SOLUTION_SERVER_EM               0x00000036
#define PRODUCT_SERVER_FOR_SB_SOLUTIONS_EM          0x00000037
#define PRODUCT_SOLUTION_EMBEDDEDSERVER             0x00000038
#define PRODUCT_SOLUTION_EMBEDDEDSERVER_CORE        0x00000039
#define PRODUCT_SMALLBUSINESS_SERVER_PREMIUM_CORE   0x0000003F
#define PRODUCT_ESSENTIALBUSINESS_SERVER_MGMT       0x0000003B
#define PRODUCT_ESSENTIALBUSINESS_SERVER_ADDL       0x0000003C
#define PRODUCT_ESSENTIALBUSINESS_SERVER_MGMTSVC    0x0000003D
#define PRODUCT_ESSENTIALBUSINESS_SERVER_ADDLSVC    0x0000003E
#define PRODUCT_CLUSTER_SERVER_V                    0x00000040
#define PRODUCT_EMBEDDED                            0x00000041
#define PRODUCT_STARTER_E                           0x00000042
#define PRODUCT_HOME_BASIC_E                        0x00000043
#define PRODUCT_HOME_PREMIUM_E                      0x00000044
#define PRODUCT_PROFESSIONAL_E                      0x00000045
#define PRODUCT_ENTERPRISE_E                        0x00000046
#define PRODUCT_ULTIMATE_E                          0x00000047

#define PRODUCT_UNLICENSED                          0xABCDABCD

*/
bool windowsVersionName(wchar_t* str, int bufferSize){
        OSVERSIONINFOEX osvi;
        SYSTEM_INFO si;
        BOOL bOsVersionInfoEx;
        DWORD dwType; ZeroMemory(&si, sizeof(SYSTEM_INFO));
        ZeroMemory(&osvi, sizeof(OSVERSIONINFOEX)); osvi.dwOSVersionInfoSize = sizeof(OSVERSIONINFOEX);
        bOsVersionInfoEx = GetVersionEx((OSVERSIONINFO*) &osvi); if(bOsVersionInfoEx == 0)
                return false; // Call GetNativeSystemInfo if supported or GetSystemInfo otherwise.
        PGNSI pGNSI = (PGNSI) GetProcAddress(GetModuleHandle(TEXT("kernel32.dll")), "GetNativeSystemInfo");
        if(NULL != pGNSI)
                pGNSI(&si);
        else GetSystemInfo(&si); // Check for unsupported OS
        if (VER_PLATFORM_WIN32_NT != osvi.dwPlatformId || osvi.dwMajorVersion <= 4 ) {
                return false;
        } 
		
		std::wstringstream os;

        os << L"Microsoft "; // Test for the specific product. 
		if ( osvi.dwMajorVersion == 6 )
        {
                if( osvi.dwMinorVersion == 0 )
                {
                        if( osvi.wProductType == VER_NT_WORKSTATION )
                                os << "Windows Vista ";
                        else os << "Windows Server 2008 ";
                }  if ( osvi.dwMinorVersion == 1 )
                {
                        if( osvi.wProductType == VER_NT_WORKSTATION )
                                os << "Windows 7 ";
                        else os << "Windows Server 2008 R2 ";
                }  
				
				PGPI pGPI = (PGPI) GetProcAddress(GetModuleHandle(TEXT("kernel32.dll")), "GetProductInfo");
                pGPI( osvi.dwMajorVersion, osvi.dwMinorVersion, 0, 0, &dwType);  
				switch( dwType )
                {
                        case PRODUCT_ULTIMATE:
                                os << "Ultimate Edition";
                                break;
                        case PRODUCT_PROFESSIONAL:
                                os << "Professional";
                                break;
                        case PRODUCT_HOME_PREMIUM:
                                os << "Home Premium Edition";
                                break;
                        case PRODUCT_HOME_BASIC:
                                os << "Home Basic Edition";
                                break;
                        case PRODUCT_ENTERPRISE:
                                os << "Enterprise Edition";
                                break;
                        case PRODUCT_BUSINESS:
                                os << "Business Edition";
                                break;
                        case PRODUCT_STARTER:
                                os << "Starter Edition";
                                break;
                        case PRODUCT_CLUSTER_SERVER:
                                os << "Cluster Server Edition";
                                break;
                        case PRODUCT_DATACENTER_SERVER:
                                os << "Datacenter Edition";
                                break;
                        case PRODUCT_DATACENTER_SERVER_CORE:
                                os << "Datacenter Edition (core installation)";
                                break;
                        case PRODUCT_ENTERPRISE_SERVER:
                                os << "Enterprise Edition";
                                break;
                        case PRODUCT_ENTERPRISE_SERVER_CORE:
                                os << "Enterprise Edition (core installation)";
                                break;
                        case PRODUCT_ENTERPRISE_SERVER_IA64:
                                os << "Enterprise Edition for Itanium-based Systems";
                                break;
                        case PRODUCT_SMALLBUSINESS_SERVER:
                                os << "Small Business Server";
                                break;
                        case PRODUCT_SMALLBUSINESS_SERVER_PREMIUM:
                                os << "Small Business Server Premium Edition";
                                break;
                        case PRODUCT_STANDARD_SERVER:
                                os << "Standard Edition";
                                break;
                        case PRODUCT_STANDARD_SERVER_CORE:
                                os << "Standard Edition (core installation)";
                                break;
                        case PRODUCT_WEB_SERVER:
                                os << "Web Server Edition";
                                break;
                }
        } if ( osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 2 )
        {
                if( GetSystemMetrics(SM_SERVERR2) )
                        os <<  "Windows Server 2003 R2, ";
                else if ( osvi.wSuiteMask & VER_SUITE_STORAGE_SERVER )
                        os <<  "Windows Storage Server 2003";
                else if ( osvi.wSuiteMask & VER_SUITE_WH_SERVER )
                        os <<  "Windows Home Server";
                else if( osvi.wProductType == VER_NT_WORKSTATION &&
                                si.wProcessorArchitecture==PROCESSOR_ARCHITECTURE_AMD64)
                {
                        os <<  "Windows XP Professional x64 Edition";
                }
                else os << "Windows Server 2003, ";  // Test for the server type.
                if ( osvi.wProductType != VER_NT_WORKSTATION )
                {
                        if ( si.wProcessorArchitecture==PROCESSOR_ARCHITECTURE_IA64 )
                        {
                                if( osvi.wSuiteMask & VER_SUITE_DATACENTER )
                                        os <<  "Datacenter Edition for Itanium-based Systems";
                                else if( osvi.wSuiteMask & VER_SUITE_ENTERPRISE )
                                        os <<  "Enterprise Edition for Itanium-based Systems";
                        }   else if ( si.wProcessorArchitecture==PROCESSOR_ARCHITECTURE_AMD64 )
                        {
                                if( osvi.wSuiteMask & VER_SUITE_DATACENTER )
                                        os <<  "Datacenter x64 Edition";
                                else if( osvi.wSuiteMask & VER_SUITE_ENTERPRISE )
                                        os <<  "Enterprise x64 Edition";
                                else os <<  "Standard x64 Edition";
                        }   else
                        {
                                if ( osvi.wSuiteMask & VER_SUITE_COMPUTE_SERVER )
                                        os <<  "Compute Cluster Edition";
                                else if( osvi.wSuiteMask & VER_SUITE_DATACENTER )
                                        os <<  "Datacenter Edition";
                                else if( osvi.wSuiteMask & VER_SUITE_ENTERPRISE )
                                        os <<  "Enterprise Edition";
                                else if ( osvi.wSuiteMask & VER_SUITE_BLADE )
                                        os <<  "Web Edition";
                                else os <<  "Standard Edition";
                        }
                }
        } if ( osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 1 ) {
                os << "Windows XP ";
                if( osvi.wSuiteMask & VER_SUITE_PERSONAL )
                        os <<  "Home Edition";
                else os <<  "Professional";
        } if ( osvi.dwMajorVersion == 5 && osvi.dwMinorVersion == 0 ) {
                os << "Windows 2000 ";  if ( osvi.wProductType == VER_NT_WORKSTATION )
                {
                        os <<  "Professional";
                }
                else 
                {
                        if( osvi.wSuiteMask & VER_SUITE_DATACENTER )
                                os <<  "Datacenter Server";
                        else if( osvi.wSuiteMask & VER_SUITE_ENTERPRISE )
                                os <<  "Advanced Server";
                        else os <<  "Server";
                }
        } // Include service pack (if any) and build number. 
        if(wcslen(osvi.szCSDVersion) > 0) {
                os << " " << osvi.szCSDVersion;
        } os << L" (build " << osvi.dwBuildNumber << L")"; if ( osvi.dwMajorVersion >= 6 ) {
                if ( si.wProcessorArchitecture==PROCESSOR_ARCHITECTURE_AMD64 )
                        os <<  ", 64-bit";
                else if (si.wProcessorArchitecture==PROCESSOR_ARCHITECTURE_INTEL )
                        os << ", 32-bit";
        } wcscpy_s(str, bufferSize, os.str().c_str());
        return true; 
}

extern "C"
{
	__declspec(dllexport) void DisplayHelloFromDLL()
	{
		printf ("Hello from DLL !\n");
	}


	__declspec(dllexport) UINT GetStringFromDLL(
		__in   HWND hwnd,
		__out  LPTSTR lpszFileName,
		__in   UINT cchFileNameMax
		) 
	{
		// bool windowsVersionName(wchar_t* str, int bufferSize){
		return windowsVersionName(lpszFileName, cchFileNameMax >> 2);
		return wsprintf(lpszFileName, L"%s", "Hello Mr String");
		 
	}
}

/*

UINT WINAPI GetWindowModuleFileName(
  __in   HWND hwnd,
  __out  LPTSTR lpszFileName,
  __in   UINT cchFileNameMax
);

[DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
static extern uint GetWindowModuleFileName(IntPtr hwnd,
StringBuilder lpszFileName, uint cchFileNameMax);

StringBuilder fileName = new StringBuilder(2000);
GetWindowModuleFileName(hwnd, fileName, 2000);
Debug.WriteLine(fileName.ToString());
*/
