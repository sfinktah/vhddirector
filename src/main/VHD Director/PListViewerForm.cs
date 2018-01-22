using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VHD_Director
{
    public partial class PListViewerForm : Form
    {
        public PListViewerForm()
        {
            InitializeComponent();
        }

        public object o { get; set; }

        private void PListViewerForm_Load(object sender, EventArgs e)
        {
            AddTreeBranchFromObject(treeView1.Nodes.Add("Root").Nodes, o);
            // AddTreeGridBranchFromObject(treeGridView1.Nodes, o);
        }


        private void AddTreeBranchFromObject(TreeNodeCollection branch_node_collection, object o)
        {
            foreach (string key in (o as Hashtable).Keys)
            {
                Console.WriteLine(String.Format("{0}: {1}", key, (o as Hashtable)[key]));
                if ((o as Hashtable)[key] is Hashtable)
                {
                    TreeNode branch_node = branch_node_collection.Add(key);
                    AddTreeBranchFromObject(branch_node.Nodes, (o as Hashtable)[key]);
                }
                else
                {
                    branch_node_collection.Add(key, key + ": " + (o as Hashtable)[key].ToString());
                }
            }
            
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode == null)
            {
                return;
            }
            TreeNode SelectedNode = treeView1.SelectedNode; // SelectedIndex = treeView1.IndexFromPoint(e.Location);
            dataGridView1.DataSource = SelectedNode.Nodes;
        }
    }
}

    
