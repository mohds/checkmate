using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace detection
{
    class mainFormResize
    {
        mainForm main;
        public void resize()
        {
            main.panel3.Width = main.Bounds.Width - (main.Bounds.Width) / 4;
            main.panel3.Height = main.Bounds.Height - main.Bounds.Height / 7;
            main.panel3.Top = main.Bounds.Y + main.Bounds.Height / 30;
            main.groupBox1.Top = main.Bounds.Y + main.Bounds.Height / 30;
            main.groupBox1.Width = main.Bounds.Width / 7;
            main.groupBox1.Left = main.panel3.Left + main.panel3.Width + main.panel3.Width / 20;
            main.zoomIn.Left = main.panel3.Left + main.panel3.Width / 2 + main.zoomIn.Width / 10;
            main.zoomIn.Top = 7 * (main.panel3.Top) / 5 + main.panel3.Height;
            main.zoomOut.Left = main.panel3.Left + main.panel3.Width / 2 - main.zoomOut.Width / 10 - main.zoomOut.Width;
            main.zoomOut.Top = 7 * (main.panel3.Top) / 5 + main.panel3.Height;
        }
        public void setMain(mainForm m)
        {
            main = m;
        }
    }
}
