using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using ContourAnalysisNS;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using AForge.Imaging;
using System.Data;
using System.Linq;
using System.Text;
using System.Security;
using AForge.Imaging.Filters;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
//using AForge.Math.Geometry;//mish mawjoode, 5laiha comment iza ma sar shi benma7iha

namespace detection
{
    public class correction
    {
        List<int> listOfAnswers = new List<int>();//hay e5er shi bterja3lak fiha list la kel el answers
        int NOC;//number of choices
        int averageWidth;
        int averageHeight;
        List<Image<Bgr, byte>> failedImages = new List<Image<Bgr, byte>>();
        List<int> allStudentsGrades = new List<int>();
        List<double> allStudentsIds = new List<double>();
        private object lockObject = new object();
        public bool idDetection = false;
        private double GetBrightness(Color color)//method to calculate the brightness of a color(actually this is the luminiscance equtaion but does the job)
        {
            return (0.299 * color.R + 0.587 * color.G + 0.114 * color.B);
        }
        public void setNOC(int x)//sets NOC (la halla2 bas hay li bethemna min el user, 5alle be2e el data li bi faweton bas ma ta3mel shi fihon)
        {
            NOC = x;
        }
        
        public void setWidthHeight(int w , int h)
        {
            averageWidth = w;
            averageHeight = h;
        }
        public void correct(List<System.Drawing.Point> L, Image<Bgr, byte> frame2, List<int> answerKey, int nOptions)
        {
            //Image<Bgr, byte> originalImage = frame2.Clone();
            try
            {
                List<double> brightnessList = new List<double>();
                getRectangle getRect = new getRectangle();
                int answer;
                int totalGrade = 0;
                List<int> studentAnswers = new List<int>();
                int z = 0;
                for (int i = 0; i < L.Count(); i++)
                {
                    double brightness = 0;
                    Rectangle kirdaha = new Rectangle(L[i].X - averageWidth / 2, L[i].Y - averageHeight / 2, averageWidth, averageHeight);
                    Image<Bgr, byte> imgROI = new Image<Bgr, byte>(kirdaha.Width, kirdaha.Height);  //hayda ya3ne L rectangle li rasamo L user(bas 3al soora l2asleyye)
                    CvInvoke.cvSetImageROI(frame2, kirdaha);    //hon 3am ba3mil L frame2 tsewe bas L region li 3amallo L user crop
                    Bitmap bitmap = frame2.ToBitmap();
                    var colors = new List<Color>();
                    Color color;
                    for (int x = 0; x < bitmap.Size.Width; x++)
                    {
                        for(int y = 0 ; y < bitmap.Size.Height ; y++)
                        {
                            lock (lockObject)
                            {
                                color = bitmap.GetPixel(x, y);
                            }
                            brightness = brightness + GetBrightness(color);
                            //colors.Add(bitmap.GetPixel(x, y));
                        }
                    }
                    //float imageBrightness = colors.Average(color => color.GetBrightness());
                    brightnessList.Add(brightness);
                    imgROI.Dispose();
                    bitmap.Dispose();
                }
                int tempStorage = nOptions;//save the value of nOptions
                int c = 0;//constant c to control switching of z. make c=1 if working with id
                if (idDetection)
                {
                    nOptions = 10;//fixed for BAU ID's
                    c = 1;//if id,let c = 1. to include numbers 0 to 9 when z is switched
                }
                for (int i = 0; i < brightnessList.Count; i = i + nOptions)
                {
                    
                    z = 0;
                    answer = (int)brightnessList.Skip(i).Take(nOptions).Min();
                    
                        while (z < nOptions)
                        {
                            if (Math.Abs(brightnessList[i + z] - answer) < 9000)
                                break;
                            else
                                z++;
                        }
                    switch (z)
                    {
                        case 0:
                            studentAnswers.Add(1 - c);
                            break;
                        case 1:
                            studentAnswers.Add(2 - c);
                            break;
                        case 2:
                            studentAnswers.Add(3 - c);
                            break;
                        case 3:
                            studentAnswers.Add(4 - c);
                            break;
                        case 4:
                            studentAnswers.Add(5 - c);
                            break;
                        case 5:
                            studentAnswers.Add(6 - c);
                            break;
                        case 6:
                            studentAnswers.Add(7 - c);
                            break;
                        case 7:
                            studentAnswers.Add(8 - c);
                            break;
                        case 8:
                            studentAnswers.Add(9 - c);
                            break;
                        case 9:
                            studentAnswers.Add(10 - c);
                            break;
                    }
                    
                }
                c = 0;//return c to nomral
                nOptions = tempStorage;//return nOptions to normal
                if (!idDetection)//eza mish sha8aleen bil id
                {
                    if (studentAnswers.Count() == answerKey.Count())
                    {
                        for (int f = 0; f < studentAnswers.Count; f++)
                        {
                            if (studentAnswers[f] == answerKey[f])
                                totalGrade++;
                        }
                        allStudentsGrades.Add(totalGrade);//add this grade to the grades list
                    }
                    else
                        Console.WriteLine("error page");
                }
                else//eza sha8aleen bil id
                {
                    //changing the list into one number
                    double total = 0;
                    foreach (var entry in studentAnswers)
                    {
                        total = 10 * total + entry;
                    }

                    allStudentsIds.Add(total);
                }
                brightnessList.Clear();
            }
            catch
            {
                Console.WriteLine("error page");
            }
            listOfAnswers.Clear();
            MemoryManagement.FlushMemory();
        }
        public class MemoryManagement
        {
            [DllImportAttribute("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize", ExactSpelling = true, CharSet =
            CharSet.Ansi, SetLastError = true)]

            private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int
            maximumWorkingSetSize);

            public static void FlushMemory()
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
        }
        public List<Image<Bgr, byte>> getFailedImages()
        {
            return failedImages;
        }
        public List<int> getAllGrades()
        {
            return allStudentsGrades;
        }
        public List<double> getAllIds()
        {
            return allStudentsIds;
        }
    }
}