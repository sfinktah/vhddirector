using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace VHD_Director
{
    public class BackBuffer
    {
        protected Bitmap _backBuffer;
        protected Size _size;
        protected Size _newSize;
		protected Graphics _g;
		protected int _state = 0;
        private Bitmap ParentBitmap;

		public Bitmap bitmap 
		{
			get { return _backBuffer; }
		}

		public Size size
		{
			get { return _size; }
			set { ReSizeBackBuffer(value); }
		}

		public BackBuffer()
		{
		}

		public BackBuffer(Size size)
		{
			this.size = size;
		}

        public BackBuffer(Bitmap ParentBitmap)
        {
            this.ParentBitmap = new Bitmap(ParentBitmap);
            this.size = ParentBitmap.Size;
            // TODO: Complete member initialization
        }

        public void Dispose() {
            if (_backBuffer != null)
            {
                _backBuffer.Dispose();
            }
        }
		public Graphics GetGraphics() {
			if (_g == null) {
				_g = Graphics.FromImage(_backBuffer);
				return _g;
			} else {
				throw new Exception("Tried to GetGraphics() when the last handle hasn't been disposed of");
			}
		}

		public void DisposeGraphics() {
			if (_g != null) {
				_g.Dispose();
                _g = null;
			} else {
			}
		}
        /*
		protected void NewBackBuffer() {
			// System.Console.WriteLine("Making new BackBuffer: {0}x{1}", _size.Width, _size.Height);
			_backBuffer = MakeBufferBitmap(_size.Width, _size.Height);
		}
         */

		protected Bitmap MakeBufferBitmap(Int32 width, Int32 height)
		{
            Bitmap tmpbuf;

            if (ParentBitmap == null)
            {
                tmpbuf = new Bitmap(width, height);
            }
            else
            {
                tmpbuf = new Bitmap(ParentBitmap);
            }
			return tmpbuf;
		}
		protected void ReSizeBackBuffer(Size newSize)
		{
			if (_backBuffer == null) {
				// System.Console.WriteLine("ReSizing from unset buffer, making new buffer.\n");
				_backBuffer = MakeBufferBitmap(newSize.Width, newSize.Height);
				_size = newSize;
				return;
			}

			if (newSize == _size) {
				// System.Console.WriteLine("ReSizeBackBuffer(newSize) is the same as old size\n");
				return;
			}
				
			Int32 newWidth = (newSize.Width > _size.Width)
				? newSize.Width
				: _size.Width;
			Int32 newHeight = (newSize.Height > _size.Height)
				? newSize.Height
				: _size.Height;

			if (newWidth == _size.Width && newHeight == _size.Height)
			{
				// System.Console.WriteLine("Tried to resize to a smaller buffer, not making new buffer.\n");
				_size = newSize;
				return;
			}

			// System.Console.WriteLine("Resized buffer\n");
			Bitmap tmpbuf = MakeBufferBitmap(newWidth, newHeight);
			Graphics g = Graphics.FromImage(tmpbuf);
			g.DrawImageUnscaled(_backBuffer, 0, 0);
			g.Dispose();
			_backBuffer.Dispose();
			_backBuffer = tmpbuf;

			_size = newSize;
		}

        
	}


}
