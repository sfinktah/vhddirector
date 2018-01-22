using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for GammaInput.
	/// </summary>
	public class GammaInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox Red;
		private System.Windows.Forms.TextBox Green;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox Blue;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GammaInput()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Cancel = new System.Windows.Forms.Button();
			this.OK = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.Red = new System.Windows.Forms.TextBox();
			this.Green = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.Blue = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(112, 152);
			this.Cancel.Name = "Cancel";
			this.Cancel.TabIndex = 0;
			this.Cancel.Text = "Cancel";
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(24, 152);
			this.OK.Name = "OK";
			this.OK.TabIndex = 1;
			this.OK.Text = "OK";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(168, 24);
			this.label1.TabIndex = 2;
			this.label1.Text = "Enter values between .2 and 5.0";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(32, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Red";
			// 
			// Red
			// 
			this.Red.Location = new System.Drawing.Point(88, 48);
			this.Red.Name = "Red";
			this.Red.TabIndex = 4;
			this.Red.Text = "textBox1";
			// 
			// Green
			// 
			this.Green.Location = new System.Drawing.Point(88, 80);
			this.Green.Name = "Green";
			this.Green.TabIndex = 6;
			this.Green.Text = "textBox2";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(32, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "Green";
			// 
			// Blue
			// 
			this.Blue.Location = new System.Drawing.Point(88, 112);
			this.Blue.Name = "Blue";
			this.Blue.TabIndex = 8;
			this.Blue.Text = "textBox3";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(32, 112);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 16);
			this.label4.TabIndex = 7;
			this.label4.Text = "Blue";
			// 
			// GammaInput
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(208, 181);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.Blue,
																		  this.label4,
																		  this.Green,
																		  this.label3,
																		  this.Red,
																		  this.label2,
																		  this.label1,
																		  this.OK,
																		  this.Cancel});
			this.Name = "GammaInput";
			this.Text = "GammaInput";
			this.Load += new System.EventHandler(this.GammaInput_Load);
			this.CenterToParent();
			this.ResumeLayout(false);

		}
		#endregion

		private void GammaInput_Load(object sender, System.EventArgs e)
		{
		
		}

		public double red
		{
			get 
			{
				return (Convert.ToDouble(Red.Text));
			}
			set{Red.Text = value.ToString();}
		}

		public double green
		{
			get 
			{
				return (Convert.ToDouble(Green.Text));
			}
			set{Green.Text = value.ToString();}
		}

		public double blue
		{
			get 
			{
				return (Convert.ToDouble(Blue.Text));
			}
			set{Blue.Text = value.ToString();}
		}
	}
}
