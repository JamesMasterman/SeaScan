using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace SeaScanUAV
{
    public class HistogramView
    {
        public const int BIN_SIZE = 256;
        public const float BIN_DEPTH = 255;

        protected ImageAnalyser imgAnalyser = new ImageAnalyser();
        protected HistogramBox redChart;
        protected HistogramBox blueChart;
        protected HistogramBox greenChart;


        public HistogramView(HistogramBox redChart, HistogramBox blueChart, HistogramBox greenChart)
        {
            this.redChart = redChart;
            this.blueChart = blueChart;
            this.greenChart = greenChart;
        }

        public void CreateHistogram(Image<Bgr, Byte> img)
        {           
            Image<Gray, Byte>[] channels = img.Split();
            Image<Gray, Byte> blueChannel = channels[0];
            Image<Gray, Byte> greenChannel = channels[1];
            Image<Gray, Byte> redChannel = channels[2];                     
            
            //Red colour channel
            DenseHistogram dhRed = new DenseHistogram(BIN_SIZE, new RangeF(0.0f, BIN_DEPTH));
            dhRed.Calculate<Byte>(new Image<Gray, Byte>[1] { redChannel }, false, null);
            redChart.ClearHistogram();
            redChart.AddHistogram("Red Channel", System.Drawing.Color.Red, dhRed);
            redChart.Refresh();

            //Green colour Channel
            DenseHistogram dhGreen = new DenseHistogram(BIN_SIZE, new RangeF(0.0f, BIN_DEPTH));
            dhGreen.Calculate<Byte>(new Image<Gray, Byte>[1] { greenChannel }, false, null);
            greenChart.ClearHistogram();
            greenChart.AddHistogram("Green Channel", System.Drawing.Color.Green, dhGreen);

            greenChart.Refresh();

            //Blue colour Channel
            DenseHistogram dhBlue = new DenseHistogram(BIN_SIZE, new RangeF(0.0f, BIN_DEPTH));
            dhBlue.Calculate<Byte>(new Image<Gray, Byte>[1] { blueChannel }, false, null);
            blueChart.ClearHistogram();
            blueChart.Show();
            blueChart.AddHistogram("Blue Channel", System.Drawing.Color.Blue, dhBlue);
            blueChart.Refresh();

        }


    }
}
