using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp.BcdDirector
{
    public class GuidItem
    {
        private string name;
        private string description;

        public GuidItem(string name, string description)
        {
            // TODO: Complete member initialization
            this.name = name;
            this.description = description;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
