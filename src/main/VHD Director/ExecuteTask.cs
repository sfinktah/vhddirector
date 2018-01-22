using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    public class ExecuteTask
    {
        private string command;
        private string arguments;
        private List<string> required_files;

        public ExecuteTask(string command, string arguments, List<String> required_files)
            : this(command, arguments)
        {
            this.required_files = required_files;
        }

        public ExecuteTask(string command, string arguments, string required_file)
            : this(command, arguments)
        {
            this.required_files = new List<string>();
            this.required_files.Add(required_file);
        }

        public ExecuteTask(string command, string arguments)
        {
            // TODO: Complete member initialization
            this.command = command;
            this.arguments = arguments;
        }
    }
}
