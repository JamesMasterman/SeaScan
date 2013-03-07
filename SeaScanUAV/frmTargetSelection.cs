using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace SeaScanUAV
{
    public partial class frmTargetSelection : Form
    {
        protected float xScale = 0.0f;
        protected float yScale = 0.0f;

        public frmTargetSelection(Bitmap image, List<TargetType> targetTypes, int selectedID)
        {
            InitializeComponent();
            xScale = (float)image.Width / (float)imgTargetImage.Width;
            yScale = (float)image.Height / (float)imgTargetImage.Height;

            imgTargetImage.Image = new Image<Bgr, Byte>(image);
          
            foreach (TargetType target in targetTypes)
            {
                if (!target.IsNavigation)//avoid generic mission point type
                {
                    cboTargetTypes.Items.Add(target);
                    if (target.ID == selectedID)
                    {
                        cboTargetTypes.SelectedItem = target;
                    }                  
                }
            }           
        }

        public Bitmap SelectedImage
        {
            get
            {
                Image<Bgr, Byte> img = new Image<Bgr, Byte>(imgTargetImage.Image.Bitmap);
                if (imgTargetImage.HasValidROI)
                {
                    img = img.Copy(imgTargetImage.GetScaledROI(xScale, yScale));                   
                }

                return img.Bitmap;
            }
        }

        public string Annotation
        {
            get
            {
                return txtNotes.Text;
            }
        }

        public TargetType Target { get; set; }
        

        private void cboTargetTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Target = cboTargetTypes.SelectedItem as TargetType;
            }
            catch (Exception ex)
            {
                Target = null;
            }
        }

        private void imgTargetImage_MouseDown(object sender, MouseEventArgs e)
        {
            imgTargetImage.ROIMouseDown(sender, e);
        }

        private void imgTargetImage_MouseMove(object sender, MouseEventArgs e)
        {
            imgTargetImage.ROIMouseMove(sender, e);
        }

        private void imgTargetImage_MouseUp(object sender, MouseEventArgs e)
        {
            imgTargetImage.ROIMouseUp(sender, e);
            imgTargetImage.Invalidate();
        } 
    }
}
