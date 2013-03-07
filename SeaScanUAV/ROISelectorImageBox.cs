using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SeaScanUAV
{
    public partial class ROISelectorImageBox :  Emgu.CV.UI.ImageBox
    {
        protected Rectangle rectROI = new Rectangle();
        protected Pen penROI = new Pen(Color.Black, 2);
        protected Pen penCompletedROI = new Pen(Color.Black, 4);
        protected bool bSelectingROI = false;

        public ROISelectorImageBox()
        {
            InitializeComponent();
            this.SetStyle(
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.Opaque
          , true);

            penROI.DashCap = System.Drawing.Drawing2D.DashCap.Round;
            penROI.DashPattern = new float[] { 2.0f, 1.0f };
        }

        public bool HasValidROI
        {
            get
            {
                if (rectROI != null && rectROI.Width > 0 && rectROI.Height > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Rectangle ROI
        {
            get
            {
                return new Rectangle(rectROI.Location, rectROI.Size);
            }
        }

        public Rectangle GetScaledROI(float xScaleFactor, float yScaleFactor)
        {
            return new Rectangle((int)(((float)rectROI.Location.X) * xScaleFactor),
                                 (int)(((float)rectROI.Location.Y) * yScaleFactor),
                                 (int)(((float)rectROI.Size.Width) * xScaleFactor),
                                 (int)(((float)rectROI.Size.Height) * yScaleFactor));


        }
        

        protected override void OnPaint(PaintEventArgs p)
        {
            base.OnPaint(p);

            if (bSelectingROI)
            {
                p.Graphics.DrawRectangle(penROI, rectROI);
            }
            else
            {
                p.Graphics.DrawRectangle(penCompletedROI, rectROI);
            }

        }


        public void ROIMouseDown(object sender, MouseEventArgs e)
        {
            bSelectingROI = true;
            rectROI = new Rectangle(e.X, e.Y, 0, 0);

        }

        public void ROIMouseMove(object sender, MouseEventArgs e)
        {
            if (bSelectingROI && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                
                rectROI.Width = e.X - rectROI.X;
                rectROI.Height = e.Y - rectROI.Y;
                this.Invalidate();
            }
        }

        public void ROIMouseUp(object sender, MouseEventArgs e)
        {
            bSelectingROI = false;
        }
        
    }
}
