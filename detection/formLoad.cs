using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace detection
{
    class formLoad
    {
        mainForm main;
        public void load()
        {
            main.WindowState = FormWindowState.Maximized;    //hay btifta7 L form maximized
            //oldValue = (int)numericUpDown3.Value;
            main.processor.cannyThreshold = 0;   //hode initial values lal processor.KTEER MHEMMEEN
            main.processor.noiseFilter = true;   //hode initial values lal processor.KTEER MHEMMEEN
            main.processor.minContourArea = main.minimumAreaTrackBar.Value; //hode initial values lal processor.KTEER MHEMMEEN
            main.croppedImageBox.Width = main.croppedImageBox.Height = 0; //bil awwal imageBox L openCV 7li2la
        }
        public void setMain(mainForm m)
        {
            main = m;
        }

    }
}
