using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director
{
    class QuickTip
    {
        static public void Add(Control control, string text)
        {
            ToolTip ToolTip1 = new ToolTip();
            ToolTip1.SetToolTip(control, text);
        }
    }
}
