using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director
{
    public partial class VhdProperties : Form
    {
        protected VirtualHardDisk vhd;
        public VhdProperties()
        {
            InitializeComponent();
        }

        public VhdProperties(VirtualHardDisk vhd) : this()
        {
            this.vhd = vhd;
        }

        private void VhdProperties_Load(object sender, EventArgs e)
        {
            TreeNode branch_node = treeView1.Nodes.Add("Hard Disk Footer ");
            TreeNodeCollection branch = (TreeNodeCollection)branch_node.Nodes;
            vhd.footer.Render(branch);
            if (vhd.dynamicHeader != null)
            {
                branch_node = treeView1.Nodes.Add("Dynamic Disk Header");
                branch = (TreeNodeCollection)branch_node.Nodes;
                vhd.dynamicHeader.Render(branch);


                branch_node = treeView1.Nodes.Add("Partitions");
                branch = (TreeNodeCollection)branch_node.Nodes;
                vhd.masterBootRecord.Render(branch);
            }
        }
    }
}
