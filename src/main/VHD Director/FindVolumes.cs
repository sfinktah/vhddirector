using System;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections.Generic;
namespace VHD_Director
{
    public class FindVolumes : IDisposable
    {
        public static void MainFunction()
        {
            try
            {
                GetVolumes();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        //HANDLE WINAPI FindFirstVolume(
        //  __out  LPTSTR lpszVolumeName,
        //  __in   DWORD cchBufferLength
        //);


        [DllImport("kernel32.dll", EntryPoint = "FindFirstVolume", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern int FindFirstVolume(
          StringBuilder lpszVolumeName,
          int cchBufferLength);


        [DllImport("kernel32.dll", EntryPoint = "FindNextVolume", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextVolume(
          int hFindVolume,
          StringBuilder lpszVolumeName,
          int cchBufferLength);

        public static List<string> GetVolumes()
        {

            const int N = 1024;
            StringBuilder cVolumeName = new StringBuilder((int)N);
            List<string> ret = new List<string>();
            int volume_handle = FindFirstVolume(cVolumeName, N);
            do
            {
                ret.Add(cVolumeName.ToString());
                Console.WriteLine(cVolumeName.ToString());
            } while (FindNextVolume(volume_handle, cVolumeName, N));
            return ret;
        }


        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

    }
}