using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace detection
{
    class submitClass
    {
        mainForm main;
        public void afterMath(Image<Bgr,byte> frame)
        {
            main.croppedImageBox.Width = main.croppedImageBox.Height = 0; //5alas sawwet detection w 3milet submit.7le2le la imageBox L opencv
            main.croppedImageBox.Image = null;   //5alas sawwet detection w 3milet submit.7le2le la imageBox L opencv
            main.panel3.Controls.Add(main.originalPictureBox);        //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
            main.originalPictureBox.Image = frame.ToBitmap();    //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
            main.originalPictureBox.Width = frame.Width;         //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
            main.originalPictureBox.Height = frame.Height;       //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
            main.minimumAreaTrackBar.Value = 600;
            main.maximumAreaTrackBar.Value = 4000;
            main.contourCenters.Clear();
            main.processor.contours.Clear();
        }

        public void setMain(mainForm m)
        {
            main = m;
        }
    }
}
