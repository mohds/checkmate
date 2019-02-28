using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace detection
{
    public class getRectangle
    {
        public Rectangle returnRectangle(Point startP, Point endP)
        {
            Rectangle rectangleSelect = new Rectangle();
            if (endP.X > startP.X && endP.Y > startP.Y)
            {
                rectangleSelect.X = startP.X;
                rectangleSelect.Y = startP.Y;
                rectangleSelect.Width = endP.X - startP.X;
                rectangleSelect.Height = endP.Y - startP.Y;
            }
            else if (endP.X > startP.X && endP.Y < startP.Y)
            {
                rectangleSelect.X = startP.X;
                rectangleSelect.Y = startP.Y - (startP.Y - endP.Y);
                rectangleSelect.Width = endP.X - startP.X;
                rectangleSelect.Height = startP.Y - endP.Y;
            }
            else if (endP.X < startP.X && endP.Y > startP.Y)
            {
                rectangleSelect.X = startP.X - (startP.X - endP.X);
                rectangleSelect.Y = startP.Y;
                rectangleSelect.Width = startP.X - endP.X;
                rectangleSelect.Height = endP.Y - startP.Y;
            }
            else if (endP.X < startP.X && endP.Y < startP.Y)
            {
                rectangleSelect.X = endP.X;
                rectangleSelect.Y = endP.Y;
                rectangleSelect.Width = startP.X - endP.X;
                rectangleSelect.Height = startP.Y - endP.Y;
            }
            return rectangleSelect;
        }
    }
}
