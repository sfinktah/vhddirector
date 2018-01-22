using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp.BcdDirector
{
    public partial class BcdOsDetailView : UserControl
    {
        private List<BcdOsDetailModel> models;
        public List<BcdOsDetailModel> Models
        {
            set
            {
                models = value;
                Redraw();
            }
        }

        private void Redraw()
        {
            SuspendLayout();
            flowPanel1.Controls.Clear();
            foreach (BcdOsDetailModel model in models)
            {
                flowPanel1.Controls.Add(new BcdOsDetailViewLine(model));
            }
            ResumeLayout();
        }
        public BcdOsDetailView()
        {
            InitializeComponent();
        }

        public BcdOsDetailView(List<BcdOsDetailModel> models)
        {
            // TODO: Complete member initialization
            this.models = models;
        }

        internal void AddToPanel(Panel panelDetail)
        {
            Redraw();
            panelDetail.Controls.Add(this);
        }
    }
}
