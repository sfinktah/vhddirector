using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VHD_Director
{
    public interface ITransparentControl
    {
        
    // Summary:
    //     Provides the functionality for transparency and an inner fil color
        // Summary:
        //     Gets or sets the control that is active on the container control.
        //
        // Returns:
        //     The System.Windows.Forms.Control that is currently active on the container
        //     control.
        Color RectangleFillColor { get; set; }
        bool Transparent { get; set; }

        // Summary:
        //     Activates a specified control.
        //
        // Parameters:
        //   active:
        //     The System.Windows.Forms.Control being activated.
        //
        // Returns:
        //     true if the control is successfully activated; otherwise, false.

    }
}
