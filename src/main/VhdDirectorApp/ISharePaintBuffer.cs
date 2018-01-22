using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace VhdDirectorApp
{
    /// <summary>
    /// Provides the functionality to directly copy (what would be) the contents of the OnPaint*() results, from their pre-drawn buffer(s). 
    /// </summary>
    interface ISharePaintBuffer
    {

        void InvalidateBuffer(bool invalidateChildren = false);
        SharePaintBuffer.TryGetBufferResults TryGetBuffer(ref Bitmap  bmp, Rectangle rectangle);
        event SharePaintBuffer.BufferInvalidated bufferInvalidated;
    
    }



    static public class SharePaintBuffer 
    {
        public delegate void BufferInvalidated(object source, object eventArgument);
        public enum TryGetBufferResults
        {
            NotReady = 0,
            Unbuffered,
            OutOfRange,
            Ok
 
        }
    }
}
