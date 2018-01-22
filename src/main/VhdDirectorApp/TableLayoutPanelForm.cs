using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp
{
    public partial class TableLayoutPanelForm : Form
    {
        public TableLayoutPanelForm()
        {
            InitializeComponent();
        }

        protected int lastRow = 1;

        private void button1_Click(object sender, EventArgs e)
        {
            //string name = type.Name;
            //string fullname = type.FullName;
            //object o = Activator.CreateInstance(type);
            //System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(null, fullname);
            //System.Windows.Forms.Form dynamicForm = (System.Windows.Forms.Form)oh.Unwrap();

            if (comboBox1.SelectedIndex > -1)
            {
                
                // string controltype = "System.Windows.Forms." + comboBox1.SelectedItem.ToString();
                //System.Runtime.Remoting.ObjectHandle oh = Activator.CreateInstance(null, controltype);
                //Control c = (Control)oh.Unwrap();

                string strType = "System.Windows.Forms." + comboBox1.SelectedItem.ToString();
                System.Reflection.Assembly asm = typeof(Form).Assembly;
                Type tp = asm.GetType(strType);
                object obj = Activator.CreateInstance(tp);
                if (obj is Control)
                {
                    Control c = (Control)obj;
                    c.Dock = DockStyle.Fill;
                    c.Text = "text " + lastRow;

                    Label label = new Label();
                    label.Dock = DockStyle.Fill;
                    label.Text = "Control " + lastRow;
                    label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

                    tableLayoutPanel1.Controls.Add(label,   0, lastRow); // , 1, lastRow++);
                    tableLayoutPanel1.Controls.Add(c,       1, lastRow++); // , 1, lastRow++);

                    // tableLayoutPanel1.Controls.Add(label, 0 /* Column Index */, lastRow++ /* Row index */);
                    // this.Controls.Add(label);
                }



            }

        
        }

    }
}

/*
 * http://stackoverflow.com/questions/4885151/advantage-of-activator-createinstance-in-this-scenario
 * The problem with generics is that you can't define a constraint on a complex constructor. The only constraint is the availability of an empty constructor.

public static T CreateInstance<T>() where T : new()
{
  return new T();
}

However, when you want to pass parameter, you'll have to use other methods, such as Activator.CreateInstance. You can also use lambda.

public static T CreateInstance<T>(Func<FormControl, T> builder, FormControl parent)
{
  return builder(parent);
}

But you'll have to provide a specific lambda for constructing your object, for each different object.

MyButton b = CreateInstance<MyButton>(parent => new MyButton(parent), SomeformInstance);

By using reflection, you can make code simpler and automatically use a pre-defined constructor. But by using lambda, you can use class that don't match a given convention and fill other constructor arguments with you own data.

var b2 = CreateInstance<MyOtherButton>(
  parent => new MyOtherButton("name", 42, parent), SomeformInstance
);

*/
