using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VhdDirectorApp
{
    public partial class UserControlDiskUsageBar : UserControl
    {
        public UserControlDiskUsageBar()
        {
            System.Console.WriteLine("Constructor: {0}x{1}", Size.Width, Size.Height);

            InitializeComponent();
            Maximum = 32384;

        }

        public int var1 { get; set; }
        public int var2 { get; set; }

        protected Bitmap[] bmpProgressBar = new Bitmap[5];
        // protected System.Drawing.Bitmap[] bmpProgressBar = new Object[2]{null, null}; 
        protected BackBuffer _backbuff;
        protected Rectangle drawingArea;
        protected Size _backbuffSize;
        protected int _clientWidth;
        protected int _clientHeight;
        protected int _lineHeight;
        protected bool doneInitLayout = false;
        public int Maximum { get; set; }

        public int lineHeight
        {
            get { return _lineHeight; }
            set { _lineHeight = value; }
        }

        public int clientWidth
        {
            get { return ClientSize.Width; }
            set { _clientWidth = value; }
        }

        public int clientHeight
        {
            get { return ClientSize.Height; }
            set { _clientHeight = value; }
        }

        protected void GetBitmaps()
        {
            ProgressBar c = new ProgressBar();
            c.Size = ClientRectangle.Size;
            c.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            c.Maximum = 10;
            c.Width = Width;

            bmpProgressBar = new Bitmap[2];
            // bmpProgressBar[0].Dispose();
            // bmpProgressBar[1].Dispose();

            bmpProgressBar[0] = new System.Drawing.Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bmpProgressBar[0], c.ClientRectangle);

            c.Increment(10);

            bmpProgressBar[1] = new System.Drawing.Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bmpProgressBar[1], c.ClientRectangle);
        }

        public void NewLine()
        {
            /*
            if (endOfText.Y + lineHeight > clientHeight) {
                    this.Size = new Size(clientWidth, endOfText.Y + lineHeight);
                    this.Size = new Size(clientWidth, endOfText.Y + lineHeight);
            }
            */
        }

        protected void AddWord(String word)
        {

            if (_backbuff == null)
            {
                throw new Exception("Can't add text until label has been sized");
            }

            Graphics g = _backbuff.GetGraphics();

            if (true || false)
            {
                _backbuff.DisposeGraphics();
                NewLine();
                g = _backbuff.GetGraphics();
            }

            if (true || false)
            {
                g.FillRectangle(Brushes.Red,
                                0, 0,
                                100, 23);
                Font styled = Font;
                styled = new Font(Font, FontStyle.Bold);
                g.DrawString(word, /* new Font("Tahoma", 11) */ styled, Brushes.Black, new Point(0, 0));
            }

            _backbuff.DisposeGraphics();

            Invalidate();
        }

        protected int lastPixel = -1;
        public int SetSlice(int state, int start, int count)
        {
            int left = (int)Math.Floor((double)start / Maximum * bmpProgressBar[0].Width);
            if (left == lastPixel)
            {
                return -1;
            }
            else
            {
                lastPixel = left;
            }


            if (_backbuff == null)
            {
                throw new Exception("Can't add text until label has been sized");
            }

            

            int right = + 1; //  +(int)Math.Floor((double)count / Maximum);
            Rectangle rect = new Rectangle(left, 0, right, bmpProgressBar[0].Height);           //Location, this.Size); // Rectangle dst = new Rectangle(e.ClipRectangle.Location, e.ClipRectangle.Size);
            Graphics g = _backbuff.GetGraphics();
         
            g.DrawImage(bmpProgressBar[state], rect, rect, GraphicsUnit.Pixel);

            _backbuff.DisposeGraphics();
            Invalidate();

            return left;

        }
        /*
        protected void CopyTextBitampToCanvas(Bitmap bmp, Size textSize)
        {
                        Graphics g = Graphics.FromImage(_backbuff);
            g.DrawImage(bmp, 
                new Rectangle(endOfText.X, endOfText.Y, endOfText.X + textSize.Width, endOfText.Y + textSize.Height), 
                new Rectangle(0,0, textSize.Width, textSize.Height), 
                GraphicsUnit.Pixel);    // Will have to be current free position
                        // DrawToBitmap(_controlTile, this.ClientRectangle); 
            g.Dispose();                                                     
                }*/

        protected override void OnPaint(PaintEventArgs e)
        {

            // e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            // e.Graphics.DrawString(Text, /* new Font("Tahoma", 11) */ Font, Brushes.Black, new PointF(0, 0));
            // SizeF textSize = e.Graphics.MeasureString(Text, Font);
            // this.Size = textSize.ToSize();

            if (_backbuff != null)
            {
                e.Graphics.DrawImageUnscaled(_backbuff.bitmap, 0, 0);
            }
        }

        // InitLayout: 150x150
        // OnLayout: 164x52
        // OnClientSizeChanged: 164x52
        // OnLayout: 214x52
        // OnClientSizeChanged: 214x52
        // OnLayout: 197x52
        // OnClientSizeChanged: 197x52
        // OnLayout: 197x52
        // OnLayout: 197x25
        // OnClientSizeChanged: 197x25

        protected override void InitLayout()
        {
            System.Console.WriteLine("InitLayout: {0}x{1}", Size.Width, Size.Height);
            base.InitLayout();


  CSharp.cc.WinApi.User32.SendMessage(Handle,
    0x400 + 16, //WM_USER + PBM_SETSTATE
    0x0003, //PBST_PAUSED
    0);
/*
  SendMessage(progressBar3.Handle,
    0x400 + 16, //WM_USER + PBM_SETSTATE
    0x0002, //PBST_ERROR
    0);
 */

//[DllImport("user32.dll", CharSet = CharSet.Unicode)]
//static extern uint SendMessage(IntPtr hWnd,
//  uint Msg,
//  uint wParam,
//  uint lParam);

            
            doneInitLayout = true;
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            System.Console.WriteLine("OnLayout: {0}x{1}", Size.Width, Size.Height);
            base.OnLayout(e);

            if (doneInitLayout)
            {
                _backbuff = new BackBuffer(Size);
                _backbuff.size = Size;

                GetBitmaps();
                Graphics g = _backbuff.GetGraphics();
                g.DrawImageUnscaled(bmpProgressBar[0], 0, 0);
                _backbuff.DisposeGraphics();

            }
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            System.Console.WriteLine("OnClientSizeChange: {0}x{1}", Size.Width, Size.Height);
            base.OnClientSizeChanged(e);
            if (_backbuff != null)
            {
                _backbuff.size = Size;
                GetBitmaps();
            }

        }
    }
}
