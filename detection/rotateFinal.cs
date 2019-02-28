using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace ContourAnalysisNS
{
    class rotateFinal
    {
        private static Point rotation_center;
        public static void setRotationCenter(Point p)
        {
            rotation_center = p;
        }
        public static Bitmap RotateImage(Bitmap b, List<Point> list, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }

        public static Point rotate_point(Point o , Point p , float theta)
        {
            //rotate point p about point o with angle theta
            Point newP = new Point();
            //convert from degrees to rad:
            theta = (float)((theta / 180) * Math.PI);
            newP.X = (int)((Math.Cos(theta)) * (p.X - o.X) - (Math.Sin(theta)) * (p.Y - o.Y) + o.X);
            newP.Y = (int)((Math.Sin(theta)) * (p.X - o.X) + (Math.Cos(theta)) * (p.Y - o.Y) + o.Y);
            return newP;
        }
        public static List<Point> rotate_list_of_points(List<Point> points, double theta)
        {
            Point temp = new Point();
            List<Point> new_list_of_points = new List<Point>();
            //convert from degrees to rad:
            theta = (double)((theta / 180) * Math.PI);
            foreach (var item in points)
            {
                temp.X = (int)((Math.Cos(theta)) * (item.X - rotation_center.X) - (Math.Sin(theta)) * (item.Y - rotation_center.Y) + rotation_center.X);
                temp.Y = (int)((Math.Sin(theta)) * (item.X - rotation_center.X) + (Math.Cos(theta)) * (item.Y - rotation_center.Y) + rotation_center.Y);
                new_list_of_points.Add(temp);
            }
            return new_list_of_points;
        }
        public static float newGetRotationAngle(List<Point> List)
        {
            float angle_of_rotation;

            int vote;
            int xReference;
            List<Point> tempList = new List<Point>();
            Dictionary<double, int> results = new Dictionary<double, int>();
            for (double angle = -15; angle <= 15; angle+=0.1)
            {
                
                vote = 0;
                tempList = rotate_list_of_points(List, angle);
                xReference = tempList[0].X;//any X can be taken as reference
                foreach (var item in tempList)
                {
                    if (Math.Abs(xReference - item.X) < 5)
                    {
                        vote++;
                    }
                }
                tempList.Clear();
                results.Add(angle, vote);
            }
            angle_of_rotation = (float)get_max_dictionary_value(results);
            return angle_of_rotation;
        }
        private static double get_max_dictionary_value(Dictionary<double, int> dictionary)
        {
            List<double> accepted_values = new List<double>();
            int max = 0;
            double angle = 0;
            /*foreach (var item in dictionary)
            {
                if (item.Value > max)
                {
                    max = item.Value;
                    angle = item.Key;
                }
            }*/
            max = dictionary.Aggregate((l, r) => l.Value > r.Value ? l : r).Value;
            foreach (var item in dictionary)
            {
                if (item.Value == max)
                {
                    accepted_values.Add(item.Key);
                }
            }
            angle = accepted_values.Average();
            
            return angle;
        }
        private static Point get_min_point_in_X(List<Point> L)
        {
            Point min = L[0]; //initial value, ma khassa bil final value of minX
            foreach (var item in L)
            {
                if (item.X < min.X)   //iterate through list la tle2e 2a2alla X
                    min = item;
            }
            return min;
        }
        private static Point get_max_point_in_Y(List<Point> L)
        {
            Point max = L[0]; //initial value, ma khassa bil final value of minX
            foreach (var item in L)
            {
                if (item.Y > max.Y)   //iterate through list la tle2e 2a2alla X
                    max = item;
            }
            return max;
        }
        private static Point get_min_point_in_Y(List<Point> L)
        {
            Point min = L[0]; //initial value, ma khassa bil final value of minX
            foreach (var item in L)
            {
                if (item.Y < min.Y)   //iterate through list la tle2e 2a2alla X
                    min = item;
            }
            return min;
        }
    }
}