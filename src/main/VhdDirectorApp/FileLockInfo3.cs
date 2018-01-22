using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace VhdDirectorApp
{
    public static class FileLockInfo
    {
        static public List<string> Handle(string filename)
        {
            if (!System.IO.File.Exists("handle.exe"))
            {
                return new List<string>{"(handle.exe not installed)"};
            }
            // http://live.sysinternals.com/handle.exe
            Process tool = new Process();
            tool.StartInfo.FileName = "handle.exe";
            tool.StartInfo.Arguments = filename;
            tool.StartInfo.UseShellExecute = false;
            tool.StartInfo.RedirectStandardOutput = true;
            tool.Start();
            tool.WaitForExit();
            string outputTool = tool.StandardOutput.ReadToEnd();

            List<string> list = new List<string>();

            string matchPattern = @"(?<=\s+pid:\s+)\b(\d+)\b(?=\s+)";
            foreach (Match match in Regex.Matches(outputTool, matchPattern))
            {
                Process p = Process.GetProcessById(int.Parse(match.Value));
                string s = String.Format("Process: {0} ({1})", p.ProcessName, p.Id);
                list.Add(s);
                // Process.GetProcessById(int.Parse(match.Value)).Kill();
            }
            return list;
        }
    }
}
