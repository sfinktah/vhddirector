using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CSharpFilters
{
	/// <summary>
	/// Summary description for HSLColorPicker.
	/// </summary>
	public class HSLColorPicker : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TrackBar hueSlider;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox hueText;

		private Bitmap hue;
		private Bitmap saturation;
		private Bitmap luminance;
		private Bitmap RGB;
		private System.Windows.Forms.PictureBox huePicBox;
		private System.Windows.Forms.PictureBox satPicBox;
		private System.Windows.Forms.TextBox satText;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TrackBar satSlider;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.PictureBox lumPicBox;
		private System.Windows.Forms.TextBox lumText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TrackBar lumSlider;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label redLabel;
		private System.Windows.Forms.Label greenLabel;
		private System.Windows.Forms.Label blueLabel;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button button2;

		private bool keyIsDown = false;
		private byte red, green, blue;
		private int initialHue = 180, initialSat = 50, initialLum = 50;
		private System.Windows.Forms.PictureBox RGBPicBox;

		public HSLColorPicker()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			hue = new Bitmap(huePicBox.Width, 360, PixelFormat.Format24bppRgb);
			saturation = new Bitmap(satPicBox.Width, 360, PixelFormat.Format24bppRgb);
			luminance = new Bitmap(lumPicBox.Width, 360, PixelFormat.Format24bppRgb);
			RGB = new Bitmap(RGBPicBox.Width, RGBPicBox.Height, PixelFormat.Format24bppRgb);

			hueSlider.Value = initialHue;
			satSlider.Value = initialSat;
			lumSlider.Value = initialLum;

			Graphics gr = Graphics.FromImage(hue);

			for (int i=0; i < 360; ++i)
			{
				HSL hsl = new HSL((float)i, .5f, .5f);
				gr.DrawLine(new Pen(hsl.RGB), 0, 360-i, huePicBox.Width, 360-i);
			}

			huePicBox.Image = hue;

			GenerateImages();
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
			this.hueSlider = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.hueText = new System.Windows.Forms.TextBox();
			this.huePicBox = new System.Windows.Forms.PictureBox();
			this.satPicBox = new System.Windows.Forms.PictureBox();
			this.satText = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.satSlider = new System.Windows.Forms.TrackBar();
			this.lumPicBox = new System.Windows.Forms.PictureBox();
			this.lumText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lumSlider = new System.Windows.Forms.TrackBar();
			this.RGBPicBox = new System.Windows.Forms.PictureBox();
			this.label4 = new System.Windows.Forms.Label();
			this.redLabel = new System.Windows.Forms.Label();
			this.greenLabel = new System.Windows.Forms.Label();
			this.blueLabel = new System.Windows.Forms.Label();
			this.OK = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.hueSlider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.satSlider)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.lumSlider)).BeginInit();
			this.SuspendLayout();
			// 
			// hueSlider
			// 
			this.hueSlider.Location = new System.Drawing.Point(88, 8);
			this.hueSlider.Maximum = 360;
			this.hueSlider.Name = "hueSlider";
			this.hueSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.hueSlider.Size = new System.Drawing.Size(45, 376);
			this.hueSlider.TabIndex = 0;
			this.hueSlider.TickFrequency = 360;
			this.hueSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.hueSlider.ValueChanged += new System.EventHandler(this.OnChangeHue);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 408);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Hue: ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// hueText
			// 
			this.hueText.Location = new System.Drawing.Point(72, 408);
			this.hueText.Name = "hueText";
			this.hueText.Size = new System.Drawing.Size(56, 20);
			this.hueText.TabIndex = 3;
			this.hueText.Text = "0";
			this.hueText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.hueText_KeyPress);
			// 
			// huePicBox
			// 
			this.huePicBox.Location = new System.Drawing.Point(16, 16);
			this.huePicBox.Name = "huePicBox";
			this.huePicBox.Size = new System.Drawing.Size(56, 360);
			this.huePicBox.TabIndex = 4;
			this.huePicBox.TabStop = false;
			// 
			// satPicBox
			// 
			this.satPicBox.Location = new System.Drawing.Point(160, 16);
			this.satPicBox.Name = "satPicBox";
			this.satPicBox.Size = new System.Drawing.Size(56, 360);
			this.satPicBox.TabIndex = 8;
			this.satPicBox.TabStop = false;
			// 
			// satText
			// 
			this.satText.Location = new System.Drawing.Point(224, 408);
			this.satText.Name = "satText";
			this.satText.Size = new System.Drawing.Size(56, 20);
			this.satText.TabIndex = 7;
			this.satText.Text = "0";
			this.satText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.satText_KeyPress);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(152, 408);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 23);
			this.label2.TabIndex = 6;
			this.label2.Text = "Saturation: ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// satSlider
			// 
			this.satSlider.Location = new System.Drawing.Point(232, 8);
			this.satSlider.Maximum = 100;
			this.satSlider.Name = "satSlider";
			this.satSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.satSlider.Size = new System.Drawing.Size(45, 376);
			this.satSlider.TabIndex = 5;
			this.satSlider.TickFrequency = 360;
			this.satSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.satSlider.ValueChanged += new System.EventHandler(this.OnChangeSat);
			// 
			// lumPicBox
			// 
			this.lumPicBox.Location = new System.Drawing.Point(304, 16);
			this.lumPicBox.Name = "lumPicBox";
			this.lumPicBox.Size = new System.Drawing.Size(56, 360);
			this.lumPicBox.TabIndex = 12;
			this.lumPicBox.TabStop = false;
			// 
			// lumText
			// 
			this.lumText.Location = new System.Drawing.Point(368, 408);
			this.lumText.Name = "lumText";
			this.lumText.Size = new System.Drawing.Size(56, 20);
			this.lumText.TabIndex = 11;
			this.lumText.Text = "0";
			this.lumText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lumText_KeyPress);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(296, 408);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 23);
			this.label3.TabIndex = 10;
			this.label3.Text = "Luminance: ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lumSlider
			// 
			this.lumSlider.Location = new System.Drawing.Point(376, 8);
			this.lumSlider.Maximum = 100;
			this.lumSlider.Name = "lumSlider";
			this.lumSlider.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.lumSlider.Size = new System.Drawing.Size(45, 376);
			this.lumSlider.TabIndex = 9;
			this.lumSlider.TickFrequency = 360;
			this.lumSlider.TickStyle = System.Windows.Forms.TickStyle.Both;
			this.lumSlider.ValueChanged += new System.EventHandler(this.OnChangeLum);
			// 
			// RGBPicBox
			// 
			this.RGBPicBox.Location = new System.Drawing.Point(472, 64);
			this.RGBPicBox.Name = "RGBPicBox";
			this.RGBPicBox.Size = new System.Drawing.Size(104, 112);
			this.RGBPicBox.TabIndex = 13;
			this.RGBPicBox.TabStop = false;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(480, 32);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(112, 23);
			this.label4.TabIndex = 14;
			this.label4.Text = "Selected Color";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// redLabel
			// 
			this.redLabel.Location = new System.Drawing.Point(472, 192);
			this.redLabel.Name = "redLabel";
			this.redLabel.Size = new System.Drawing.Size(112, 23);
			this.redLabel.TabIndex = 15;
			this.redLabel.Text = "Red: ";
			this.redLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// greenLabel
			// 
			this.greenLabel.Location = new System.Drawing.Point(472, 224);
			this.greenLabel.Name = "greenLabel";
			this.greenLabel.Size = new System.Drawing.Size(112, 23);
			this.greenLabel.TabIndex = 16;
			this.greenLabel.Text = "Green: ";
			this.greenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// blueLabel
			// 
			this.blueLabel.Location = new System.Drawing.Point(472, 256);
			this.blueLabel.Name = "blueLabel";
			this.blueLabel.Size = new System.Drawing.Size(112, 23);
			this.blueLabel.TabIndex = 17;
			this.blueLabel.Text = "Blue:";
			this.blueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// OK
			// 
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(480, 320);
			this.OK.Name = "OK";
			this.OK.TabIndex = 18;
			this.OK.Text = "OK";
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(480, 376);
			this.button2.Name = "button2";
			this.button2.TabIndex = 19;
			this.button2.Text = "Cancel";
			// 
			// HSLColorPicker
			// 
			this.AcceptButton = this.OK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.button2;
			this.ClientSize = new System.Drawing.Size(592, 446);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.blueLabel);
			this.Controls.Add(this.greenLabel);
			this.Controls.Add(this.redLabel);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.RGBPicBox);
			this.Controls.Add(this.lumPicBox);
			this.Controls.Add(this.lumText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lumSlider);
			this.Controls.Add(this.satPicBox);
			this.Controls.Add(this.satText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.satSlider);
			this.Controls.Add(this.huePicBox);
			this.Controls.Add(this.hueText);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.hueSlider);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(600, 480);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(600, 480);
			this.Name = "HSLColorPicker";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "HSLColorPicker";
			((System.ComponentModel.ISupportInitialize)(this.hueSlider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.satSlider)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.lumSlider)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void GenerateImages()
		{
			Graphics g = Graphics.FromImage(saturation);

			float h = (float)hueSlider.Value;

			for (int i = 0; i < 360; ++i)
			{
				HSL hsl = new HSL(h, (360-i)/360.0f, .5f);

				g.DrawLine(new Pen(hsl.RGB), 0, i, satPicBox.Width, i); 
			}

			satPicBox.Image = saturation;

			Graphics gl = Graphics.FromImage(luminance);

			float s = (float)satSlider.Value/100.0f;

			for (int i = 0; i < 360; ++i)
			{
				HSL hsl = new HSL(h, s, (360-i)/360.0f);

				gl.DrawLine(new Pen(hsl.RGB), 0, i, lumPicBox.Width, i); 
			}

			lumPicBox.Image = luminance;

			Graphics rgb = Graphics.FromImage(RGB);

			float l = (float)lumSlider.Value/100.0f;

			Color rgbFinal = new HSL(h, s, l).RGB;
			rgb.FillRectangle(new SolidBrush(rgbFinal), 0, 0, RGBPicBox.Width, RGBPicBox.Height);

			RGBPicBox.Image = RGB;

			redLabel.Text = String.Format("Red: {0}", rgbFinal.R.ToString());
			greenLabel.Text = String.Format("Green: {0}", rgbFinal.G.ToString());
			blueLabel.Text = String.Format("Blue: {0}", rgbFinal.B.ToString());

			red = rgbFinal.R;
			green = rgbFinal.G;
			blue = rgbFinal.B;
		}

		private void OnChangeHue(object sender, System.EventArgs e)
		{
			if (!keyIsDown) 
			{
				hueText.Text = hueSlider.Value.ToString();
				GenerateImages();
			}
		}

		private void hueText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (!Char.IsControl(e.KeyChar))
			{
				if (!Char.IsDigit(e.KeyChar))
				{
					e.Handled = true;
					return;
				}

				if (hueText.Text.Length > 0)
				{
					string s = hueText.Text.Substring(0, hueText.SelectionStart);
					s += e.KeyChar;
					int sub = hueText.SelectionStart+hueText.SelectionLength;
					s += hueText.Text.Substring(sub, hueText.Text.Length - sub);

					int n = Convert.ToInt16(s);

					if (n>359)
					{
						e.Handled = true;
						return;
					}

					keyIsDown = true;
					hueSlider.Value = n;
					keyIsDown = false;
				}
			}

			GenerateImages();
		}

		private void OnChangeSat(object sender, System.EventArgs e)
		{
			if (!keyIsDown) 
			{
				satText.Text = satSlider.Value.ToString();
				GenerateImages();
			}
		}

		private void satText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (!Char.IsControl(e.KeyChar))
			{
				if (!Char.IsDigit(e.KeyChar))
				{
					e.Handled = true;
					return;
				}

				if (satText.Text.Length > 0)
				{
					string s = satText.Text.Substring(0, satText.SelectionStart);
					s += e.KeyChar;
					int sub = satText.SelectionStart+satText.SelectionLength;
					s += satText.Text.Substring(sub, satText.Text.Length - sub);

					int n = Convert.ToInt16(s);

					if (n>100)
					{
						e.Handled = true;
						return;
					}

					keyIsDown = true;
					satSlider.Value = n;
					keyIsDown = false;
				}
			}		

			GenerateImages();
		}

		private void OnChangeLum(object sender, System.EventArgs e)
		{
			if (!keyIsDown) 
			{
				lumText.Text = lumSlider.Value.ToString();
				GenerateImages();
			}
		}

		private void lumText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (!Char.IsControl(e.KeyChar))
			{
				if (!Char.IsDigit(e.KeyChar))
				{
					e.Handled = true;
					return;
				}

				if (lumText.Text.Length > 0)
				{
					string s = lumText.Text.Substring(0, lumText.SelectionStart);
					s += e.KeyChar;
					int sub = lumText.SelectionStart+lumText.SelectionLength;
					s += lumText.Text.Substring(lumText.SelectionStart+lumText.SelectionLength, lumText.Text.Length - sub);

					int	n = Convert.ToInt16(s);

					if (n>100)
					{
						e.Handled = true;
						return;
					}

					keyIsDown = true;
					lumSlider.Value = n;
					keyIsDown = false;
				}
			}		

			GenerateImages();
		}

		public Color SelectedColor
		{
			get
			{
				return Color.FromArgb(red, green, blue);
			}
			set
			{
				HSL hsl = HSL.FromRGB(value);

				hueSlider.Value = (int)hsl.Hue;
				satSlider.Value = (int)(hsl.Saturation * 100);
				lumSlider.Value = (int)(hsl.Luminance * 100);
			}
		}
	}
}
