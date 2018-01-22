using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for ColorSpaceDlg.
	/// </summary>
	public class ColorSpaceDlg : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox initialRed;
		private System.Windows.Forms.TextBox initialGreen;
		private System.Windows.Forms.TextBox initialBlue;
		private System.Windows.Forms.Label Red;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox color1;
		private System.Windows.Forms.PictureBox color2;
		private System.Windows.Forms.Label HLSValues;
		private System.Windows.Forms.Label RGBValues;
		private System.Windows.Forms.Button ConvertBtn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ColorSpaceDlg()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.initialRed = new System.Windows.Forms.TextBox();
			this.initialGreen = new System.Windows.Forms.TextBox();
			this.initialBlue = new System.Windows.Forms.TextBox();
			this.Red = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ConvertBtn = new System.Windows.Forms.Button();
			this.color1 = new System.Windows.Forms.PictureBox();
			this.color2 = new System.Windows.Forms.PictureBox();
			this.HLSValues = new System.Windows.Forms.Label();
			this.RGBValues = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// initialRed
			// 
			this.initialRed.Location = new System.Drawing.Point(48, 24);
			this.initialRed.Name = "initialRed";
			this.initialRed.Size = new System.Drawing.Size(40, 20);
			this.initialRed.TabIndex = 0;
			this.initialRed.Text = "";
			// 
			// initialGreen
			// 
			this.initialGreen.Location = new System.Drawing.Point(168, 24);
			this.initialGreen.Name = "initialGreen";
			this.initialGreen.Size = new System.Drawing.Size(40, 20);
			this.initialGreen.TabIndex = 1;
			this.initialGreen.Text = "";
			// 
			// initialBlue
			// 
			this.initialBlue.Location = new System.Drawing.Point(288, 24);
			this.initialBlue.Name = "initialBlue";
			this.initialBlue.Size = new System.Drawing.Size(40, 20);
			this.initialBlue.TabIndex = 2;
			this.initialBlue.Text = "";
			// 
			// Red
			// 
			this.Red.Location = new System.Drawing.Point(0, 24);
			this.Red.Name = "Red";
			this.Red.Size = new System.Drawing.Size(48, 23);
			this.Red.TabIndex = 3;
			this.Red.Text = "Red";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(112, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Green";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(232, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Blue";
			// 
			// ConvertBtn
			// 
			this.ConvertBtn.Location = new System.Drawing.Point(152, 72);
			this.ConvertBtn.Name = "ConvertBtn";
			this.ConvertBtn.TabIndex = 6;
			this.ConvertBtn.Text = "Convert";
			this.ConvertBtn.Click += new System.EventHandler(this.Convert_Click);
			// 
			// color1
			// 
			this.color1.BackColor = System.Drawing.SystemColors.Desktop;
			this.color1.Location = new System.Drawing.Point(24, 111);
			this.color1.Name = "color1";
			this.color1.Size = new System.Drawing.Size(56, 50);
			this.color1.TabIndex = 7;
			this.color1.TabStop = false;
			// 
			// color2
			// 
			this.color2.BackColor = System.Drawing.SystemColors.Desktop;
			this.color2.Location = new System.Drawing.Point(152, 111);
			this.color2.Name = "color2";
			this.color2.Size = new System.Drawing.Size(56, 50);
			this.color2.TabIndex = 8;
			this.color2.TabStop = false;
			// 
			// HLSValues
			// 
			this.HLSValues.Location = new System.Drawing.Point(16, 192);
			this.HLSValues.Name = "HLSValues";
			this.HLSValues.Size = new System.Drawing.Size(368, 23);
			this.HLSValues.TabIndex = 10;
			// 
			// RGBValues
			// 
			this.RGBValues.Location = new System.Drawing.Point(16, 232);
			this.RGBValues.Name = "RGBValues";
			this.RGBValues.Size = new System.Drawing.Size(368, 23);
			this.RGBValues.TabIndex = 11;
			// 
			// ColorSpaceDlg
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(416, 350);
			this.Controls.Add(this.RGBValues);
			this.Controls.Add(this.HLSValues);
			this.Controls.Add(this.color2);
			this.Controls.Add(this.color1);
			this.Controls.Add(this.ConvertBtn);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Red);
			this.Controls.Add(this.initialBlue);
			this.Controls.Add(this.initialGreen);
			this.Controls.Add(this.initialRed);
			this.Name = "ColorSpaceDlg";
			this.Text = "ColorSpaceDlg";
			this.Load += new System.EventHandler(this.ColorSpaceDlg_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void ColorSpaceDlg_Load(object sender, System.EventArgs e)
		{
		
		}

		private void Convert_Click(object sender, System.EventArgs e)
		{
			Color c = Color.FromArgb(255, Convert.ToInt32(initialRed.Text), Convert.ToInt32(initialGreen.Text), Convert.ToInt32(initialBlue.Text));

			HSL hls = HSL.FromRGB(c);

			color1.BackColor = c;
			color2.BackColor = hls.RGB;

			HLSValues.Text = String.Format("HLS values are Hue: {0}, Saturation: {1}, Luminance: {2}", hls.Hue.ToString(), hls.Saturation.ToString(), hls.Luminance.ToString());
			RGBValues.Text = String.Format("RGB converted back to Red: {0}, Green: {1}, Blue: {2}", hls.RGB.R.ToString(), hls.RGB.G.ToString(), hls.RGB.B.ToString());

			Invalidate(true);
		}
	}
}
