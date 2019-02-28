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
    public class ImageProcessor
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
        public Templates templates = new Templates();
        public Templates samples = new Templates();
        public List<FoundTemplateDesc> foundTemplates = new List<FoundTemplateDesc>();
        public TemplateFinder finder = new TemplateFinder();
        public Image<Gray, byte> binarizedFrame;
        //above here malnash da3wa
        public Point amal;
        public List<Contour<Point>> contours;
        private List<NumericUpDown> nQuestionsPerColumnList = new List<NumericUpDown>();
        private int numberOfQ;//number of questions
        private int numberofC;//number of choices
        private int nOfM;//number of master coloumns
        public bool idDetection = false;
        public int xDiff;
        public int yPad;
        public int IDxDiff;
        public int IDyPad;
        public int getXdiff()
        {
            return xDiff;
        }
        public int getYpad()
        {
            return yPad;
        }
        public int getIDXdiff()
        {
            return IDxDiff;
        }
        public int getIDYpad()
        {
            return IDyPad;
        }
        public void setNOFM(int n)
        {
            nOfM = n;
        }
        public int getNOFM()
        {
            return nOfM;
        }

        public void setNumberOfQ(int n)
        {
            numberOfQ = n;
        }
        
        public int getNumberOfQ()
        {
            return numberOfQ;
        }

        public void setQperColumnList(List<NumericUpDown> list)
        {
            nQuestionsPerColumnList = list;
        }
        public List<NumericUpDown> getQperColumnList()
        {
            return nQuestionsPerColumnList;
        }
        public void setNumberOfC(int n)
        {
            numberofC = n;
        }
        public int getNumberOfC()
        {
            return numberofC;
        }
        public Point getFirstPoint(List<Point> list)
        {
            int minX = list[0].X;
            int minY = list[0].Y;
            foreach (var item in list)
            {
                if (item.X < minX)
                    minX = item.X;
            }
            foreach (var item in list)
            {
                if (item.Y < minY)
                    minY = item.Y;
            }
            return new Point(minX - 40, minY - 20);
        }
        public Point getLastPoint(List<Point> list)
        {
            int maxX = list[list.Count - 1].X;
            int maxY = list[list.Count - 1].Y;
            foreach (var item in list)
            {
                if (item.X > maxX)
                    maxX = item.X;
            }
            foreach (var item in list)
            {
                if (item.Y > maxY)
                    maxY = item.Y;
            }
            return new Point(maxX + 50, maxY +100);
        }

        void quickSortX(List<Point> arr, int left, int right) 
        {
              int i = left, j = right;
              Point tmp;
              Point pivot = arr[(left + right) / 2];

              /* partition */

              while (i <= j) {
                    while (arr[i].X < pivot.X)
                          i++;
                    while (arr[j].X > pivot.X)
                          j--;
                    if (i <= j) {
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

        public int getAverageWidth()
        {
            int averageWidth = 0;
            foreach (var item in contours)
                averageWidth = averageWidth + item.BoundingRectangle.Width;
            return averageWidth / contours.Count;
        }
        public int getAverageHeight()
        {
            int averageHeight = 0;
            foreach (var item in contours)
                averageHeight = averageHeight + item.BoundingRectangle.Height;
            return averageHeight / contours.Count;
        }
        public int getAverageArea()
        {
            int averageArea = 0;
            foreach (var item in contours)
                averageArea = averageArea + (int)item.Area;
            return averageArea / contours.Count;
        }
        public List<Point> getSortedPoints()
        {
            List<Point> returnThis = new List<Point>();
            foreach (var contour in contours)
            {
                returnThis.Add(new Point(contour.BoundingRectangle.Center().X + amal.X, contour.BoundingRectangle.Center().Y + amal.Y));
            }
            //returnThis.Reverse();
            returnThis.Sort(new PointComparer());//this here sorts the points ( first X then Y)
            returnThis = returnThis.Distinct().ToList();//remove the repeated points (with no allowed error)
            removeRepitions(returnThis, 10);//remove repeated points with an allowed error
            if (idDetection)
            {
                idBackup backupId = new idBackup();
                backupId.setFirstImageTrue();
                returnThis = backupId.fixList(returnThis);
                IDxDiff = backupId.getXdiff();
                IDyPad = backupId.getYpad();
                returnThis.Sort(new PointComparer());

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
            //setQuestionsPerMaster(40);//dummy variable
            //setNumberOfQ(80);
            //setNumberOfC(4);

            int nOfColomns = nOfM * getNumberOfC();//number of colomns in the whole page
            Backup backup = new Backup();
            backup.setFirstImageTrue();
            backup.setNOfChoice(numberofC);
            backup.setNOfM(nOfM);
            backup.setQperColumnList(nQuestionsPerColumnList);
            returnThis = backup.fixList(returnThis);
            xDiff = backup.getXdiff();
            yPad = backup.getYpad();
            returnThis.Sort(new PointComparer());//re-sort the list
            for (int i = 0; i < returnThis.Count; i++)
            {
                if ((i + (nOfColomns - 1)) < returnThis.Count)
                {
                    quickSortX(returnThis, i, i + (nOfColomns - 1));
                    i += (nOfColomns - 1);
                }
            }

            return returnThis;
        }

        public Queue<Point> getQueue()
        {
            Queue<Point> Q = new Queue<Point>();
            List<Point> list = new List<Point>();
            list = getSortedPoints();
            int linecontrol = 0;//moves queuer from line to line
            for (int i = 0 + linecontrol; i < list.Count; i++)
            {
                if (!Q.Contains(list[i]))
                    Q.Enqueue(list[i]);
                if (i != 0)
                    if ((i + 1) % getNumberOfC() == 0)
                    {
                        i += getNumberOfC();
                    }
            }
            linecontrol = 0;//reset line control
            for (int i = getNumberOfC() + linecontrol; i < list.Count; i++)
            {
                if (!Q.Contains(list[i]))
                    Q.Enqueue(list[i]);
                if (i != getNumberOfC())
                    if ((i + 1) % getNumberOfC() == 0)
                    {
                        i += getNumberOfC();
                    }
            }
            return Q;
        }
        private int lastY;
        public List<Point> getList()
        {

            //must be of equal questions per master colomn
            List<Point> L = new List<Point>();
            List<Point> list = new List<Point>();
            list = getSortedPoints();
            if (idDetection)//eza sha8aleen 3al id
            {
                return list;//return lal list de8re
            }
            int masterControl = 1;//controls the variation of the number of master colomns
            int j = 1;
            int temp = 0;

            lastY=(int)nQuestionsPerColumnList[0].Value;
                for (int i =0  ; i < list.Count; i++)
                {
                    if (!L.Contains(list[i]))//return false if L already has list[i]
                        L.Add(list[i]);//add this list[i] to list L
                    if (i != 0)//do not enter for i==0
                        if (masterControl <= nOfM)//true if master control is less than or equal number of master colomns
                        {
                            if ((i + 1) % getNumberOfC() == 0)//if the colomn's number we are at is divisible by the number of choices return true
                            {
                                i += numberofC*(nQuestionsPerColumnList.Count-1);
                            }
                        }
                    if (L.Count == lastY*numberofC)//true when we reach the end of a master colomn
                    {
                        masterControl++;//move to the next colomn
                        if(j<nQuestionsPerColumnList.Count)//check if we are still in bound
                        lastY += (int)nQuestionsPerColumnList[j].Value;//make lastY the number of the last question in the next master colomn
                        j++;//increment nQuestionsPerColumnList
                    }

                    int nOfColomns = nOfM * numberofC;//number of colomns in the whole page

                    if (i + 1 >= list.Count)//true when we reach the end of the list 
                    {
                        temp += numberofC;
                        i = temp - 1;
                        if (temp >= nOfColomns)
                        {
                            goto exit;
                        }
                    }

                }
            exit:
                return L;
        }

        void removeRepitions(List<Point> list , int error)//removes repetitons - the noob way
        {
            for(int j=0 ; j<list.Count;j++)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (Math.Abs(list[j].X - list[i].X) < error && Math.Abs(list[j].Y-list[i].Y) < error)
                    {
                        if (list[j] != list[i])
                        {
                            list.Remove(list[i]);
                            //break;
                            if (i > 0) i--;
                            if (j > 0) j--;
                        }
                    }
                }
            }
        }
        public void ProcessImage(Image<Bgr,byte> frame)
        {
            ProcessImage(frame.Convert<Gray, Byte>());
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
                    if (c.Area < minContourArea || c.Area>maxContourArea)// || c.Perimeter < minContourPer || c.Perimeter > maxContourPer)
                        goto next;
                    if (c.Area < minContourArea || c.Area > maxContourArea)
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
    class PointComparer2 : IComparer<Point>//PointComparer2 instead of PointComparer 
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
