using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
namespace detection
{
    class cropClass
    {
        mainForm main;
        public Image<Bgr, byte> crop(Point amal, Point tishreen, Image<Bgr, byte> frame)
        {
            Image<Bgr, byte> frame2 = frame.Clone();//L frame 2 ra7 2e5eda cloned 3an L frame bil awwal, ba3den 7a sawweya bas matra7 ma L user rasam rectangle
            getRectangle getRect = new getRectangle();
            Rectangle kirdaha = new Rectangle();    //FOR WASSIM'S ALGORITHM ©
            kirdaha = getRect.returnRectangle(amal, tishreen);
            Image<Bgr, byte> imgROI = new Image<Bgr, byte>(kirdaha.Width, kirdaha.Height);  //hayda ya3ne L rectangle li rasamo L user(bas 3al soora l2asleyye)
            CvInvoke.cvSetImageROI(frame2, kirdaha);    //hon 3am ba3mil L frame2 tsewe bas L region li 3amallo L user crop
            main.originalPictureBox.Image = null;   //sheel L pictureBox taba3 L C#
            main.originalPictureBox.Width = main.originalPictureBox.Height = 0;   //sheel L pictureBox taba3 L C#
            main.panel3.Controls.Add(main.croppedImageBox); //zidle imageBox L opencv
            main.croppedImageBox.Left = main.panel3.Left + main.panel3.Width / 20; //design
            main.croppedImageBox.Width = frame2.Width;  //wad7a
            main.croppedImageBox.Height = frame2.Height; 
            return frame2;
        }
        public void setMain(mainForm m)
        {
            main = m;
        }
    }
}
