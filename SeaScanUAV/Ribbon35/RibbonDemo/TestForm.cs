using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RibbonDemo
{
   public partial class TestForm : RibbonForm
   {
      public TestForm()
      {
         InitializeComponent();
      }

      private void TestForm_Load(object sender, EventArgs e)
      {
      }

      private void ribbonCheckBox1_CheckBoxCheckChanging(object sender, CancelEventArgs e)
      {
         if (ribbonCheckBox1.Checked)
            e.Cancel = true;
      }

      private Bitmap ResizeImage(Image Img, int Width, int Height)
      {
         Bitmap result = new Bitmap(Width, Height);
         using (Graphics g = Graphics.FromImage(result))
            g.DrawImage(Img, 0, 0, Width, Height);
         return result;
      }

      private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
      {

      }

      private void button1_Click(object sender, EventArgs e)
      {
         ribbonTextBox1.Visible = !ribbonTextBox1.Visible;
      }

      private void ribbonComboBox1_DropDownItemClicked(object sender, RibbonItemEventArgs e)
      {
         MessageBox.Show("Item Clicked");
      }
   }
}