using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for HLSDemo.
	/// </summary>
	public class HLSDemo : System.Windows.Forms.Form
	{
		private Bitmap bitmap = new Bitmap(720, 720, PixelFormat.Format24bppRgb);
		private System.Windows.Forms.TrackBar Luminance;
		private System.Windows.Forms.Label luminanceValue;
		private System.Windows.Forms.CheckBox checkBox1;
		private bool modSaturation = false;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HLSDemo()
		{
			InitializeComponent();

			DrawHLS();
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
			this.Luminance = new System.Windows.Forms.TrackBar();
			this.luminanceValue = new System.Windows.Forms.Label();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.Luminance)).BeginInit();
			this.SuspendLayout();
			// 
			// Luminance
			// 
			this.Luminance.Location = new System.Drawing.Point(736, 56);
			this.Luminance.Maximum = 100;
			this.Luminance.Name = "Luminance";
			this.Luminance.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.Luminance.Size = new System.Drawing.Size(45, 480);
			this.Luminance.TabIndex = 0;
			this.Luminance.Value = 50;
			this.Luminance.ValueChanged += new System.EventHandler(this.LuminanceChanged);
			// 
			// luminanceValue
			// 
			this.luminanceValue.Location = new System.Drawing.Point(720, 24);
			this.luminanceValue.Name = "luminanceValue";
			this.luminanceValue.TabIndex = 1;
			this.luminanceValue.Text = "Luminance = ";
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(720, 560);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Modify Saturation";
			this.checkBox1.Click += new System.EventHandler(this.OnModSat);
			// 
			// HLSDemo
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(832, 726);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.luminanceValue);
			this.Controls.Add(this.Luminance);
			this.Name = "HLSDemo";
			this.Text = "HLSDemo";
			this.Click += new System.EventHandler(this.OnModSat);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.OnPaint);
			((System.ComponentModel.ISupportInitialize)(this.Luminance)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnPaint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		}

		private void DrawHLS()
		{
			// GDI+ still lies to us - the return format is BGR, NOT RGB.
			BitmapData bmData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

			int stride = bmData.Stride;
			System.IntPtr Scan0 = bmData.Scan0;

			float luminance = Luminance.Value/100.0f;

			luminanceValue.Text = modSaturation ? String.Format("Saturation = {0}", luminance) : String.Format("Luminance = {0}", luminance);

			unsafe
			{
				byte * p = (byte *)(void *)Scan0;

				int nOffset = stride - bitmap.Width*3;
				int nWidth = bitmap.Width;
				int nHeight = bitmap.Height;

				for(int y=0;y<nHeight;++y)
				{
					for(int x=0; x < nWidth; ++x )
					{
						double normalisedX = 2.0*(double)x/(double)nWidth-1.0;
						double normalisedY = 2.0*(double)y/(double)nHeight-1.0;

						double r = Math.Sqrt(normalisedX*normalisedX + normalisedY*normalisedY);
						double phi = Math.Atan2(normalisedY, normalisedX);

						if (r > 1 || r < -1)
						{
							p[0] = p[1] = p[2] = 0;
						}
						else
						{
							phi = (phi+Math.PI)/(2*Math.PI) * 360;
							r = Math.Abs(r)/Math.PI;

							HSL hsl = modSaturation ? new HSL((float)phi, luminance, (float)r) : new HSL((float)phi, (float)r, luminance);
							Color c = hsl.RGB;

							p[0] = c.B;
							p[1] = c.G;
							p[2] = c.R;
						}

						p += 3;
					}

					p += nOffset;
				}
			}

			bitmap.UnlockBits(bmData);
		}

		private void LuminanceChanged(object sender, System.EventArgs e)
		{
			DrawHLS();
			Invalidate();
		}

		private void OnModSat(object sender, System.EventArgs e)
		{
			modSaturation = checkBox1.Checked;
			DrawHLS();
			Invalidate();	
		
		}
	}
}
