using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VhdDirectorApp.BcdDirector
{
    public class OS
    {
        public string Name { get; set; }
        public string GUID { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public string HexGuid
        {
            get
            {
                return GUID.Replace("{", "").Replace("}", "").Replace("-", "");
            }
        }

        public string GravatarGuid
        {
            get
            {
                return "http://www.gravatar.com/avatar/" + HexGuid + "?s=40";
            }
        }
    }
}
