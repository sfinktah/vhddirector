using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace VhdDirectorApp
{
    public partial class MyLabel2 : UserControlLayout, ISharePaintBuffer
    {
		public MyLabel2()
		{
            endOfText = new Point(0, 0);

		}

		public int var1 { get; set; }
		public int var2 { get; set; }
		protected BackBuffer _backbuff;
		protected Rectangle drawingArea;
		protected Point endOfText;
		// protected Bitmap _backbuff;
		protected Size _backbuffSize;
		protected int _clientWidth;
		protected int _clientHeight;
		protected int _lineHeight;
		public bool styleBold = false;
        public int styleAlert = 0;

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

        protected string text = string.Empty;
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (!text.TrimEnd().Equals(value))
                {
                    RedrawControl(value);
                }
            }
        }

        private void RedrawControl(string value)
        {
            Clear();
            string[] lines = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    AddLine(line);
                }
            }
        }


        // TODO: No point doing all the calculation *before* the resize event comes. Should only do so onPaint.
		public void AddText(String text) {
            if (text.Contains("<b>")) {
                System.Console.WriteLine("debug\n");
            }
			string[] words = Regex.Split(text, "(<.*?>|\\s)");
			if (words.Length > 0)
			{
				foreach (string word in words)
				{
					if (word.Equals(" ")) {
						// continue;
					}

                    if (word.Length == 0)
                    {
                        continue;
                    }

                    if (word.StartsWith("<alert"))
                    {
                        styleAlert = 1;
                        continue;
                    }
					if (word.Equals("<b>")) {
						styleBold = true;
						continue;
					}
					if (word.Equals("</b>")) {
						styleBold = false;
						continue;
					}
					AddWord(word);
				}
			}
		}

		public void NewLine() {
			endOfText.X = 0;
			endOfText.Y += lineHeight;
			if (endOfText.Y + lineHeight > clientHeight) {
				this.Size = new Size(clientWidth, endOfText.Y + lineHeight);
				this.Size = new Size(clientWidth, endOfText.Y + lineHeight);
			}
		}

        public bool AtLineStart { get { return endOfText.X == 0; } }

		protected void AddWord(String word) {
            text += word;
			// Rectangle _container = new Rectangle(0,0, clientWidth, clientHeight);
            // Bitmap _working = new Bitmap(_container.Width, _container.Height);
			// Graphics g; // = Graphics.FromImage(_working);

            // g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            // Draw the text in the box as an image. This is ok, because we ONLY
            // call OnPaint when the textbox is disabled.
// OnPaint(e);
            // g.DrawString(word + " ", /* new Font("Tahoma", 11) */ Font, Brushes.Black, new PointF(0, 0));
            // SizeF textSizeF = g.MeasureString(word + " ", Font);
			// Size textSize = textSizeF.ToSize();
			// g.Dispose();

			// If textSize.Width < remaining client width, draw at current position, else new line

		 	
			// CopyTextBitampToCanvas(_working, textSize);
            if (_backbuff == null)
            {
				throw new Exception("Can't add text until label has been sized");
                //_backbuff = new Bitmap(ClientSize.Width, ClientSize.Height);
			}
            
            if (true && word == " " && !AtLineStart)
            {
                int spaceWidth = 1; // 3.99 x 15.98 actually
                if (endOfText.X + spaceWidth > clientWidth)
                {
                    if (this.AutoWrap)
                    {
                        NewLine();
                    }
                }
                else
                {
                    endOfText.X += spaceWidth;
                }

                return;

            }
            
            Graphics g = _backbuff.GetGraphics();
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;
            // g.TextRenderingHint = TextRenderingHint.SystemDefault;
            Font styled = Font;
            if (styleBold)
            {
                styled = new Font(Font, FontStyle.Bold);
                // TODO: Dispose?
            }
            SizeF textSizeF = g.MeasureString(word + "", styled);
            Size textSize = textSizeF.ToSize();

            int endX = endOfText.X + textSize.Width;
            if (endX > clientWidth && !AtLineStart)
            {
				_backbuff.DisposeGraphics();
                NewLine();
				g = _backbuff.GetGraphics();
            } 
			// System.Console.WriteLine("{0}x{1}: {2}\n", endOfText.X, endOfText.Y, word);
			if (styleBold) {
				g.FillRectangle(Brushes.Red,
						endOfText.X,
						endOfText.Y,
						endOfText.X + textSize.Width,
						endOfText.Y + textSize.Height);
				g.DrawString(word, /* new Font("Tahoma", 11) */ styled, Brushes.Yellow, endOfText);
			} else {
				g.DrawString(word, /* new Font("Tahoma", 11) */ styled, Brushes.Black, endOfText);
			}

            _backbuff.DisposeGraphics();
            // g.Dispose();

            endOfText.X += textSize.Width;

            // Invalidate();

		}
        /*
        protected void CopyTextBitampToCanvas(Bitmap bmp, Size textSize)
        {
			Graphics g = Graphics.FromImage(_backbuff);
            g.DrawImage(bmp, 
                new Rectangle(endOfText.X, endOfText.Y, endOfText.X + textSize.Width, endOfText.Y + textSize.Height), 
                new Rectangle(0,0, textSize.Width, textSize.Height), 
                GraphicsUnit.Pixel);	// Will have to be current free position
			// DrawToBitmap(_controlTile, this.ClientRectangle); 
            g.Dispose();                                                     
		}*/

        protected override void OnPaint(PaintEventArgs e)
        {
			
            // e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            // e.Graphics.DrawString(Text, /* new Font("Tahoma", 11) */ Font, Brushes.Black, new PointF(0, 0));
            // SizeF textSize = e.Graphics.MeasureString(Text, Font);
            // this.Size = textSize.ToSize();
			
			if (_backbuff == null) {
				throw new Exception("Trying to paint buffer before it's been drawn");
				return;
			}
			e.Graphics.DrawImageUnscaled(_backbuff.bitmap, 0, 0);
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
            base.InitLayout();
            // System.Console.WriteLine("InitLayout: {0}x{1}", Size.Width, Size.Height);
            if (_backbuff == null)
            {

                _backbuff = new BackBuffer(ParentBitmap);
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            // System.Console.WriteLine("OnLayout: {0}x{1}", Size.Width, Size.Height);
            if (Parent == null)
            {
                return;
            }


            if (_backbuff == null)
            {
                _backbuff = new BackBuffer(ParentBitmap);
            }
            _backbuff.size = Size;

        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            // System.Console.WriteLine("OnClientSizeChanged: {0}x{1}", Size.Width, Size.Height);
            if (Parent != null)
            {
                _backbuff.size = Size;
            }

        }

        // --

        protected bool haveSetBackground;
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // return;
            if (!Transparent)
            {
                base.OnPaintBackground(pevent);
                return;
            }

            // base.OnPaintBackground(pevent);
            if (!NoPaintParentBackground)
            {
                using (new csharp_debug.SectionStopWatch("MyLabel2::PaintParentBackground"))
                {
                    PaintParentBackground(pevent);
                }
            }
        }
        Bitmap _parentBitmap;
        public void InvalidateBuffer(bool invalidateChildren = false)
        {
            if (_parentBitmap != null) {
            _parentBitmap.Dispose();
            _parentBitmap = null;
            }

            // Can a label have children?
        }

        public SharePaintBuffer.TryGetBufferResults TryGetBuffer(ref Bitmap bmp, Rectangle rectangle)
        {
            
            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            // We're going to cheat, and cause a real redraw.  Since it is highly unlikely this method will ever be called on a label, that's okay.
            // And we haven't really tested it either.

            using (Bitmap _tmpbmp = new Bitmap(Width, Height))
            {
                Rectangle rect = new Rectangle(0, 0,
                                               Width, Height);
                using (Graphics dc = Graphics.FromImage(_parentBitmap))
                {
                    // dc.TranslateTransform(-rect.X, -rect.Y);

                    try
                    {
                        using (PaintEventArgs pea = new PaintEventArgs(dc, rect))
                        {
                            pea.Graphics.SetClip(rect);
                            InvokePaintBackground(this, pea);
                            InvokePaint(this, pea);
                        }
                    }
                    finally
                    {
                        // dc.TranslateTransform(rect.X, rect.Y);
                    }
                }



                using (Graphics dc = Graphics.FromImage(bmp))
                {
                    Rectangle dstRect = rectangle;
                    dstRect.X = 0;
                    dstRect.Y = 0;
                    dc.DrawImage(_tmpbmp, dstRect, rectangle, GraphicsUnit.Pixel);
                }
            }

            sw.Stop();
            TimeSpan layoutTime = sw.Elapsed;
            System.Console.WriteLine("TryGetBuffer took: {0} ms", layoutTime.Milliseconds);
            return SharePaintBuffer.TryGetBufferResults.Ok;
        }

        private void PaintParentBackground(PaintEventArgs e)
        {
            if (!Transparent)
            {
                // This probably shouldn't be called in non transparent mode, but we could insert a FillRectangle of bg color
                return;

            }
            if (Parent != null) {
                if  (_parentBitmap == null)
                {
                    _parentBitmap = new Bitmap(Width, Height);
                    Rectangle rect = new Rectangle(Left, Top,
                                                   Width, Height);

                    // Point p = new Point(this.Location.X, this.Location.Y);
                    // Point dp = (Parent as Control).PointToClient(this.PointToScreen(p));

                    if (Parent is ISharePaintBuffer)
                    {
                        if ((Parent as ISharePaintBuffer).TryGetBuffer(ref _parentBitmap, rect) == SharePaintBuffer.TryGetBufferResults.Ok)
                        {
                            // e.Graphics.DrawImageUnscaled(_parentBitmap, 0, 0);
                            (Parent as ISharePaintBuffer).bufferInvalidated += new SharePaintBuffer.BufferInvalidated(parent_bufferInvalidated);
                            return;
                        } 
                    }

                    // If parent isn't ISharePaintBuffer, or it just failed, do it the old fashioned way.
                    using (Graphics dc = Graphics.FromImage(_parentBitmap)) {
                        dc.TranslateTransform(-rect.X, -rect.Y);
                        
                        try
                        {
                            using (PaintEventArgs pea =
                                        new PaintEventArgs(dc, rect))
                            {
                                pea.Graphics.SetClip(rect);
                                InvokePaintBackground(Parent, pea);
                                InvokePaint(Parent, pea);
                            }
                        }
                        finally
                        {
                            dc.TranslateTransform(rect.X, rect.Y);
                        }
                    }
                }

                // Paint cached bitmap, or old fashioned obtained bitmap.  (ISharePaintBuffer has already returned)
                // e.Graphics.DrawImageUnscaled(_parentBitmap, 0, 0);
            }
            else
            {
                using (Graphics dc = Graphics.FromImage(_parentBitmap))
                {
                    dc.FillRectangle(SystemBrushes.Control, ClientRectangle);
                }
                // e.Graphics.FillRectangle(SystemBrushes.Control, ClientRectangle);
            }
        }
        public event SharePaintBuffer.BufferInvalidated bufferInvalidated;
        void parent_bufferInvalidated(object source, object eventArgument)
        {
            if (_parentBitmap != null)
            {
                _parentBitmap.Dispose();
                _parentBitmap = null;
            }

            _parentBitmap = new Bitmap(Width, Height);
            Rectangle srcRect = new Rectangle(Left, Top, Width, Height);
            Rectangle dstRect = new Rectangle(0, 0,      Width, Height);
            using (Graphics dc = Graphics.FromImage(_parentBitmap))
            {
                dc.DrawImage((eventArgument as Bitmap), dstRect, srcRect, GraphicsUnit.Pixel);
            }
            RedrawControl(text);
            // _parentBitmap = new Bitmap((Bitmap)eventArgument);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (_parentBitmap != null)
            {
                _parentBitmap.Dispose();
                _parentBitmap = null;
            }
        }

        public bool Transparent { get; set; }
//        public bool TransparentPaintParentBackground { get; set; }

        public bool NoPaintParentBackground { get; set; }


        internal void Clear()
        {
            text = string.Empty;
            endOfText = new Point(0, 0);
            if (Parent == null)
            {
                return;
            }

            if (_backbuff != null)
            {
                _backbuff.Dispose();
            }
            _backbuff = new BackBuffer(ParentBitmap);
        }

        internal void AddLine(string _line1)
        {
            AddText(_line1);
            if (!AtLineStart) { NewLine(); text += Environment.NewLine; }
        }

        public bool AutoWrap { get; set; }

        protected Bitmap ParentBitmap
        {
            get
            {
                if (_parentBitmap == null)
                {
                    PaintParentBackground(null);
                }
                return _parentBitmap;
            }
        }


        private void MyLabel2_Load(object sender, EventArgs e)
        {
            if (Parent is ISharePaintBuffer)
            {
                (Parent as ISharePaintBuffer).bufferInvalidated += new SharePaintBuffer.BufferInvalidated(parent_bufferInvalidated);
            }
        }
    }
}


