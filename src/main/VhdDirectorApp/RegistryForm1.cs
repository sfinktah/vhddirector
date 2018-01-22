using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;

namespace VhdDirectorApp
{
    public partial class RegistryForm1 : Form
    {
        public RegistryForm1()
        {
            InitializeComponent();

            listBox1.DataSource = CSharp.cc.Registry.RegistryTools.GetRegistryTypeList();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            string keyPath = textBox1.Text;
            CSharp.cc.Registry.Key k = new CSharp.cc.Registry.Key();
            
            // HKEY_LOCAL_MACHINE\SOFTWARE\blah\blah\keyname
            // http://www.codeproject.com/KB/system/modifyregistry.aspx

            button2.Enabled = false;
            if (k.TryGetKey(keyPath))
            {
                textBox2.Text = k.KeyValue.ToString();
                textBox3.Text = k.KeyValueKind.ToString().ToUpper();
                button2.Enabled = true;
            }
            else
            {
                textBox2.Text = "Error";
                textBox3.Text = "Error";
            }
            // sk1.SetValue(keyName.ToUpper(), keyValue, keyKind);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string keyPath = textBox1.Text;
            CSharp.cc.Registry.Key k = new CSharp.cc.Registry.Key();

            // HKEY_LOCAL_MACHINE\SOFTWARE\blah\blah\keyname
            // http://www.codeproject.com/KB/system/modifyregistry.aspx

            if (k.TryGetKey(keyPath))
            {
                k.SetKey(textBox2.Text);
            }
            else
            {
                MessageBox.Show("Woops");
            }
        }

    }
}
