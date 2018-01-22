using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VHD_Director
{
    interface IListPanel
    {
        //
        // Summary:
        //     When overridden in a derived class, gets or sets the zero-based index of
        //     the currently selected item.
        //
        // Returns:
        //     A zero-based index of the currently selected item. A value of negative one
        //     (-1) is returned if no item is selected.
        int SelectedIndex { get; set; }
        //
        // Summary:
        //     Gets or sets the value of the member property specified by the System.Windows.Forms.ListControl.ValueMember
        //     property.
        //
        // Returns:
        //     An object containing the value of the member of the data source specified
        //     by the System.Windows.Forms.ListControl.ValueMember property.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The assigned value is null or the empty string ("").

        object SelectedValue { get; set; }
        //
        // Summary:
        //     Gets or sets the property to use as the actual value for the items in the
        //     System.Windows.Forms.ListControl.
        //
        // Returns:
        //     A System.String representing the name of an object property that is contained
        //     in the collection specified by the System.Windows.Forms.ListControl.DataSource
        //     property. The default is an empty string ("").
        //
        // Exceptions:
        //   System.ArgumentException:
        //     The specified property cannot be found on the object specified by the System.Windows.Forms.ListControl.DataSource
        //     property.
        //
        // Summary:
        //     Occurs when the System.Windows.Forms.ListControl.SelectedValue property changes.
        event EventHandler SelectedIndexChanged;
        //
        // Summary:
        //     Raises the System.Windows.Forms.ListControl.SelectedValueChanged event.
        //
        // Parameters:
        //   e:
        //     An System.EventArgs that contains the event data.
        void OnSelectedIndexChanged(EventArgs e);
    }
}
