//
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR
//  PURPOSE.
//
//  License: GNU General Public License version 3 (GPLv3)
//
//  Email: pavel_torgashov@mail.ru.
//
//  Copyright (C) Pavel Torgashov, 2011. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourAnalysisNS
{
    public class newProcessor
    {
        //settings
        public bool equalizeHist = false;
        public bool noiseFilter = false;
        public int cannyThreshold = 50; // the max_low threshold
        public bool blur = true;
        public int adaptiveThresholdBlockSize = 4;
        public double adaptiveThresholdParameter = 1.2d;
        public bool addCanny = true;
        public bool filterContoursBySize = true;
        public bool onlyFindContours = false;
        public int minContourLength = 15;
        public int minContourArea = 10;
        public int maxContourArea = 10;
        public int minContourPer = 10;
        public int maxContourPer = 50;
        public double minFormFactor = 0.5;
        public Point amal;
        public Point firstPoint = new Point(-1, -1);
        public Point lastPoint = new Point(-1, -1);
        List<Point> contourCenters = new List<Point>();
        //public List<Contour<Point>> contourListFinal = new List<Contour<Point>>();
        //
        public List<Contour<Point>> contours;
        public Templates templates = new Templates();
        public Templates samples = new Templates();
        public List<FoundTemplateDesc> foundTemplates = new List<FoundTemplateDesc>();
        public TemplateFinder finder = new TemplateFinder();
        public Image<Gray, byte> binarizedFrame;
        private List<NumericUpDown> nQuestionsPerColumnList = new List<NumericUpDown>();
        private int numberOfQ;//number of questions
        private int numberofC;//number of choices
        private int nOfM;//number of master coloumns
        public int averageArea = 0;
        private Dictionary<int, List<Point>> dict = new Dictionary<int, List<Point>>();
        List<Point> tempList = new List<Point>();
        List<int> startKeyOfMasters = new List<int>();
        List<int> minXs = new List<int>();
        List<int> maxXs = new List<int>();
        public int xDiff;
        public int yPad;
        public int IDxDiff;
        public int IDyPad;
        public bool idDetection;

        public void setXdiff(int x)
        {
            xDiff = x;
        }
        public void setYpad(int y)
        {
            yPad = y;
        }
        public void setIDXdiff(int x)
        {
            IDxDiff = x;
        }
        public void setIDYpad(int y)
        {
            IDyPad = y;
        }
        public void setNOFM(int n)
        {
            nOfM = n;
        }

        public void setNumberOfQ(int n)
        {
            numberOfQ = n;
        }
        public void setQperColumnList(List<NumericUpDown> list)
        {
            nQuestionsPerColumnList = list;
        }

        public void setNumberOfC(int n)
        {

            numberofC = n;
        }
        void quickSortX(List<Point> arr, int left, int right)
        {
            int i = left, j = right;
            Point tmp;
            Point pivot = arr[(left + right) / 2];

            /* partition */

            while (i <= j)
            {
                while (arr[i].X < pivot.X)
                    i++;
                while (arr[j].X > pivot.X)
                    j--;
                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    j--;
                }
            };

            /* recursion */

            if (left < j)
                quickSortX(arr, left, j);
            if (i < right)
                quickSortX(arr, i, right);
        }

        public List<Point> getSortedPoints(ref Image<Bgr, byte> imageRef, Image<Gray, byte> image)
        {
            List<Point> returnThis = new List<Point>();
            foreach (var contour in contours)
            {
                int X = contour.BoundingRectangle.Center().X;//abscissa of point
                int Y = contour.BoundingRectangle.Center().Y;//ordinate of point
                if ((X < lastPoint.X) && (Y < lastPoint.Y))
                {
                    if ((X > firstPoint.X) && (Y > firstPoint.Y))
                    {
                        returnThis.Add(new Point(X, Y));
                    }
                }
            }


            returnThis.Sort(new PointComparer());//this here sorts the points ( first X then Y)
            returnThis = returnThis.Distinct().ToList();//remove the repeated points (with no allowed error)
            removeRepitions(returnThis, 10);//remove repeated points with an allowed error

            rotateFinal.setRotationCenter(new Point(image.Width / 2, image.Height / 2));
            float angle = rotateFinal.newGetRotationAngle(returnThis);
            if (Math.Abs(angle) > 1)//maximum of 1 degrees alllowed error
            {
                Bitmap checkImage = rotateFinal.RotateImage(image.ToBitmap(), returnThis, angle);
                image = new Image<Gray, byte>(checkImage);
                //image = new Image<Gray, byte>(rotateFinal.RotateImage(image.ToBitmap(), returnThis));
                ProcessImage(image);
                List<Point> tempList = new List<Point>();
                tempList = getSortedPoints(ref imageRef, image);
                imageRef = image.Convert<Bgr, byte>();//swap the imageRef with new image
                return tempList;
            }
            if (idDetection)
            {
                idBackup backupId = new idBackup();
                backupId.setXdiff(IDxDiff);
                backupId.setYpad(IDyPad);
                returnThis = backupId.fixList(returnThis);
                returnThis.Sort(new PointComparer());//re-sort the list

                List<Point> newlySortedList = new List<Point>();
                //now we have to sort the list in a way compatible with the BAU test ID0
                for (int i = 0; i < 90; i += 9)
                {
                    if ((i + 8) < returnThis.Count)
                        quickSortX(returnThis, i, i + 8);
                }
                for (int i = 0; i < 9; i++)
                {
                    for (int j = i; j < 90; j += 9)
                    {
                        if (j < returnThis.Count)
                            newlySortedList.Add(returnThis[j]);
                    }
                }

                return newlySortedList;
            }
            int nOfColomns = nOfM * numberofC;//number of colomns in the whole page
                
            //backup
            Backup backup = new Backup();
            backup.setXdiff(xDiff);
            backup.setYpad(yPad);
            backup.setNOfChoice(numberofC);
            backup.setNOfM(nOfM);
            backup.setQperColumnList(nQuestionsPerColumnList);
            returnThis = backup.fixList(returnThis);
            returnThis = returnThis.Distinct().ToList();//remove the repeated points (with no allowed error)
            removeRepitions(returnThis, 10);//remove repeated points with an allowed error
            returnThis = returnThis.Distinct().ToList();//remove the repeated points (with no allowed error)
            removeRepitions(returnThis, 10);//remove repeated points with an allowed error
            return returnThis;
        }
       
        public List<Point> getList(ref Image<Bgr, byte> imageRef, Image<Gray, byte> image)
        {
            //calling the getSorted
            return getSortedPoints(ref imageRef, image);
        }
        
        
        void removeRepitions(List<Point> list, int error)//removes repetitons - the noob way
        {
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (Math.Abs(list[j].X - list[i].X) < error && Math.Abs(list[j].Y - list[i].Y) < error)
                    {
                        if (j != i)
                        {
                            list.Remove(list[j]);
                            //break;
                            if (i > 0) i--;
                            if (j > 0) j--;
                        }
                    }
                }
            }
        }

        public void ProcessImage(Image<Bgr, byte> frame)
        {
            lock (frame)
            {
                ProcessImage(frame.Convert<Gray, Byte>());
            }
        }

        public void ProcessImage(Image<Gray, byte> grayFrame)
        {
            if (equalizeHist)
                grayFrame._EqualizeHist();//autocontrast
            //smoothed
            Image<Gray, byte> smoothedGrayFrame = grayFrame.PyrDown();
            smoothedGrayFrame = smoothedGrayFrame.PyrUp();
            //canny

            Image<Gray, byte> cannyFrame = null;
            if (noiseFilter)
                cannyFrame = smoothedGrayFrame.Canny(new Gray(cannyThreshold), new Gray(cannyThreshold));
            //smoothing
            if (blur)
                grayFrame = smoothedGrayFrame;
            //binarize
            CvInvoke.cvAdaptiveThreshold(grayFrame, grayFrame, 255, Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_MEAN_C, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY, adaptiveThresholdBlockSize + adaptiveThresholdBlockSize % 2 + 1, adaptiveThresholdParameter);
            //
            grayFrame._Not();
            //
            if (addCanny)
                if (cannyFrame != null)
                    grayFrame._Or(cannyFrame);
            //
            this.binarizedFrame = grayFrame;

            //dilate canny contours for filtering
            if (cannyFrame != null)
                cannyFrame = cannyFrame.Dilate(3);

            //find contours
            var sourceContours = grayFrame.FindContours(Emgu.CV.CvEnum.CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_NONE, Emgu.CV.CvEnum.RETR_TYPE.CV_RETR_LIST);
            //filter contours
            contours = FilterContours(sourceContours, cannyFrame, grayFrame.Width, grayFrame.Height);
            //find templates
            lock (foundTemplates)
                foundTemplates.Clear();
            samples.Clear();

            lock (templates)
                Parallel.ForEach<Contour<Point>>(contours, (contour) =>
                {
                    var arr = contour.ToArray();
                    Template sample = new Template(arr, contour.Area, samples.templateSize);
                    lock (samples)
                        samples.Add(sample);

                    if (!onlyFindContours)
                    {
                        FoundTemplateDesc desc = finder.FindTemplate(templates, sample);

                        if (desc != null)
                            lock (foundTemplates)
                                foundTemplates.Add(desc);
                    }
                }
                );
            //
            FilterByIntersection(ref foundTemplates);
        }

        private static void FilterByIntersection(ref List<FoundTemplateDesc> templates)
        {
            //sort by area
            templates.Sort(new Comparison<FoundTemplateDesc>((t1, t2) => -t1.sample.contour.SourceBoundingRect.Area().CompareTo(t2.sample.contour.SourceBoundingRect.Area())));
            //exclude templates inside other templates
            HashSet<int> toDel = new HashSet<int>();
            for (int i = 0; i < templates.Count; i++)
            {
                if (toDel.Contains(i))
                    continue;
                Rectangle bigRect = templates[i].sample.contour.SourceBoundingRect;
                int bigArea = templates[i].sample.contour.SourceBoundingRect.Area();
                bigRect.Inflate(4, 4);
                for (int j = i + 1; j < templates.Count; j++)
                {
                    if (bigRect.Contains(templates[j].sample.contour.SourceBoundingRect))
                    {
                        double a = templates[j].sample.contour.SourceBoundingRect.Area();
                        if (a / bigArea > 0.9d)
                        {
                            //choose template by rate
                            if (templates[i].rate > templates[j].rate)
                                toDel.Add(j);
                            else
                                toDel.Add(i);
                        }
                        else//delete tempate
                            toDel.Add(j);
                    }
                }
            }
            List<FoundTemplateDesc> newTemplates = new List<FoundTemplateDesc>();
            for (int i = 0; i < templates.Count; i++)
                if (!toDel.Contains(i))
                    newTemplates.Add(templates[i]);
            templates = newTemplates;
        }

        private List<Contour<Point>> FilterContours(Contour<Point> contours, Image<Gray, byte> cannyFrame, int frameWidth, int frameHeight)
        {
            int maxArea = frameWidth * frameHeight / 5;
            var c = contours;
            List<Contour<Point>> result = new List<Contour<Point>>();
            while (c != null)
            {
                if (filterContoursBySize)
                    if (c.Area < minContourArea || c.Area > maxContourArea)// || c.Perimeter < minContourPer || c.Perimeter > maxContourPer)
                        goto next;

                if (noiseFilter)
                {
                    Point p1 = c[0];
                    Point p2 = c[(c.Total / 2) % c.Total];
                    if (cannyFrame[p1].Intensity <= double.Epsilon && cannyFrame[p2].Intensity <= double.Epsilon)
                        goto next;
                }
                result.Add(c);

            next:
                c = c.HNext;
            }

            return result;
        }
    }
    class PointComparer : IComparer<Point>
    {
        public int Compare(Point first, Point second)
        {
            if (first.Y == second.Y)
            {
                return first.X - second.X;
            }
            else
            {
                return first.Y - second.Y;
            }
        }
    }
}
