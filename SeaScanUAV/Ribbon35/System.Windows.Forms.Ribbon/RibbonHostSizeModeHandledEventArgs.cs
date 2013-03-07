using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace System.Windows.Forms
{
   public class RibbonHostSizeModeHandledEventArgs : HandledEventArgs
   {
      private RibbonElementSizeMode _sizeMode;
      private System.Drawing.Graphics _graphics;
      private Size _Size;

      /// <summary>
      /// Creates a new RibbonElementMeasureSizeEventArgs object
      /// </summary>
      /// <param name="graphics">Device info to draw and measure</param>
      /// <param name="sizeMode">Size mode to measure</param>
      internal RibbonHostSizeModeHandledEventArgs(System.Drawing.Graphics graphics, RibbonElementSizeMode sizeMode)
      {
         _graphics = graphics;
         _sizeMode = sizeMode;
      }

      /// <summary>
      /// Gets the size mode to measure
      /// </summary>
      public RibbonElementSizeMode SizeMode
      {
         get
         {
            return _sizeMode;
         }
      }

      /// <summary>
      /// Gets the device to measure objects
      /// </summary>
      public Graphics Graphics
      {
         get
         {
            return _graphics;
         }
      }
      public Size ControlSize
      {
         get { return _Size; }
         set { _Size = value; }
      }
   }
}
