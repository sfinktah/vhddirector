using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director.BcdDirector
{
    public partial class BcdOsDetailViewLine : UserControl
    {
        private BcdOsDetailModel model;
        private string elementType = string.Empty;

        public BcdOsDetailViewLine()
        {
            InitializeComponent();

        }

        public BcdOsDetailViewLine(BcdOsDetailModel _model)
        {
            InitializeComponent();
            this.model = _model;


            foreach (var pair in model.Contents)
            {
                elementType = pair.Key;
                string[] e = pair.Key.Split('_');
                this.label1.Text = e[1];
                this.label2.Text = pair.Value;
                break;
            }

            setTooltips();
        }

        protected string getTooltip()
        {
            string tip;
            if (BcdEnumDescriptions.Descriptions.TryGetValue(elementType, out tip))
            {
                return tip;
            }

            return string.Empty;
        }
        protected void setTooltips()
        {
            string tooltip_text = getTooltip();
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTip1.SetToolTip(this.label1, tooltip_text);
            toolTip1.SetToolTip(this.label2, tooltip_text);
            toolTip1.SetToolTip(this.roundedPanel1, tooltip_text);
        }
        private void roundedPanel1_MouseHover(object sender, EventArgs e)
        {
            //ToolTip tt = new ToolTip();
            //tt.SetToolTip(this, "Tooltip");
            //tt.Show("Tooltip");
        }
    }
}
