using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for Resize.
	/// </summary>
	public class Resize : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox WidthBox;
		private System.Windows.Forms.TextBox HeightBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox BilinearBox;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public int nWidth
		{
			get 
			{
				return (Convert.ToInt32(WidthBox.Text, 10));
			}
			set{WidthBox.Text = value.ToString();}
		}

		public int nHeight
		{
			get 
			{
				return (Convert.ToInt32(HeightBox.Text, 10));
			}
			set{HeightBox.Text = value.ToString();}
		}

		public bool bBilinear
		{
			get 
			{
				return BilinearBox.Checked;;
			}
			set{BilinearBox.Checked = value;}
		}


		public Resize()
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
			this.label1 = new System.Windows.Forms.Label();
			this.WidthBox = new System.Windows.Forms.TextBox();
			this.HeightBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.BilinearBox = new System.Windows.Forms.CheckBox();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Width";
			// 
			// WidthBox
			// 
			this.WidthBox.Location = new System.Drawing.Point(64, 16);
			this.WidthBox.Name = "WidthBox";
			this.WidthBox.Size = new System.Drawing.Size(56, 20);
			this.WidthBox.TabIndex = 1;
			this.WidthBox.Text = "";
			// 
			// HeightBox
			// 
			this.HeightBox.Location = new System.Drawing.Point(64, 48);
			this.HeightBox.Name = "HeightBox";
			this.HeightBox.Size = new System.Drawing.Size(56, 20);
			this.HeightBox.TabIndex = 3;
			this.HeightBox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(40, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Height";
			// 
			// BilinearBox
			// 
			this.BilinearBox.Location = new System.Drawing.Point(24, 80);
			this.BilinearBox.Name = "BilinearBox";
			this.BilinearBox.Size = new System.Drawing.Size(96, 24);
			this.BilinearBox.TabIndex = 4;
			this.BilinearBox.Text = "Bilinear Filter";
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(16, 128);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(48, 23);
			this.OK.TabIndex = 5;
			this.OK.Text = "OK";
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(80, 128);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(48, 23);
			this.Cancel.TabIndex = 6;
			this.Cancel.Text = "Cancel";
			// 
			// Resize
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(144, 165);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.Cancel,
																		  this.OK,
																		  this.BilinearBox,
																		  this.HeightBox,
																		  this.label2,
																		  this.WidthBox,
																		  this.label1});
			this.Name = "Resize";
			this.Text = "Resize";
			this.Load += new System.EventHandler(this.Resize_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void Resize_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
