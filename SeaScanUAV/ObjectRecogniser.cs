using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

namespace SeaScanUAV
{
    public class ObjectRecogniser
    {
        protected EigenObjectRecognizer recogniser;
        protected List<Image<Gray, Byte>> images;

        public void AddImage(Image<Gray, Byte> img)
        {

        }


        public void LearnImages()
        {

        }

    }
}
