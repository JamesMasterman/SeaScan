/*
 
 2008 José Manuel Menéndez Poo
 * 
 * Please give me credit if you use this code. It's all I ask.
 * 
 * Contact me for more info: menendezpoo@gmail.com
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace System.Windows.Forms
{
   public class RibbonItemEventArgs : EventArgs
   {
      private RibbonItem _item;

      public RibbonItemEventArgs( RibbonItem item)
         : base()
      {
         Item = item;
      }

      public RibbonItem Item
      {
         get
         {
            return _item;
         }
         set
         {
            _item = value;
         }
      }
   }
}
