using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using System.IO;
using System.Security;
using Emgu.CV.Structure;
using ContourAnalysisNS;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using AForge.Imaging.Filters;
using System.Collections.Concurrent.Partitioners;
using Microsoft.Win32;
using System.Runtime.InteropServices;
namespace detection
{
    public partial class browse : Form
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ParallelOptions parallel = new ParallelOptions();
        List<Image<Bgr, byte>> exams = new List<Image<Bgr, byte>>();
        rotation rotate = new rotation();
        newProcessor processorMain = new newProcessor();
        newProcessor processorMain2 = new newProcessor();
        List<Point> L = new List<Point>();
        public Point firstPoint = new Point();
        List<Point> newList = new List<Point>();
        public Point lastPoint = new Point();
        public List<Contour<Point>> contours = new List<Contour<Point>>();
        //Image<Bgr, byte> cvImage;
        Pen centerPen = new Pen(Color.SkyBlue, 3);
        Image<Bgr, byte> image;
        correction correct = new correction();
        Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
        List<Bitmap> circles = new List<Bitmap>();
        List<int> answerKey;
        int nOptions;
        int i = 1;
        int x = 0;
        int r = 0;
        int examCount = 0;
        List<int> allStudentsGrades = new List<int>();
        List<double> allStudentsIds = new List<double>();
        List<Image<Bgr, byte>> failedImages = new List<Image<Bgr, byte>>();
        private object lockObject = new object();
        Image<Bgr, byte> cvImage;
        Image<Bgr, byte> clone;

        newProcessor idProcessor = new newProcessor();//processor to be used for exam answering area
        public browse()
        {
            InitializeComponent();
        }
        public void setIdProcessor(newProcessor p)
        {
            processorMain2 = p;
        }
        private void browseBTN_Click(object sender, EventArgs e)
        {
            //newProcessor processor = new newProcessor();
            DialogResult dr = this.ofd.ShowDialog();
            parallel.MaxDegreeOfParallelism = Environment.ProcessorCount - 1;
            if (dr == System.Windows.Forms.DialogResult.OK)
            {
                int activePage = 0;
                int pages;
                /*Parallel.ForEach(ofd.FileNames,
                new ParallelOptions { MaxDegreeOfParallelism = 7 },
                (file) =>
                {
                    //var cvImage = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(file));
                    newProcessor processor = new newProcessor();
                    lock (lockObject)
                    {
                        cvImage = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(file));
                        //cvImage.Bitmap = rotate.rotate(cvImage.ToBitmap());
                        processor = processorMain;
                        processor.ProcessImage(cvImage);
                        newList = processor.getList();
                        MemoryManagement.FlushMemory();
                        //correct.correct(newList, cvImage, answerKey, nOptions);
                    }
                    correct.correct(newList, cvImage, answerKey, nOptions);
                    counter++;
                    MemoryManagement.FlushMemory();
                });
                allStudentsGrades = correct.getAllGrades();*/
                memory.MemoryManagement.FlushMemory();
                SynchronizationContext ctx = SynchronizationContext.Current;
                Parallel.ForEach(ChunkPartitioner.Create(ofd.FileNames, 10), (file) =>
                    {
                        List<Point> id_list_of_points = new List<Point>();
                        int count = 0;
                        Image image = Image.FromFile(file);
                        pages = image.GetFrameCount(FrameDimension.Page);
                        newProcessor examProcessor = new newProcessor();//processor to be used for exam answering area
                        if (pages > 1)
                        {
                            for (int index = 0; index < pages; index++)
                            //Parallel.For(0, pages,new ParallelOptions {MaxDegreeOfParallelism = 7}, (index) =>
                            {
                                lock (lockObject)
                                {
                                    Image image2 = Image.FromFile(file);
                                    activePage = index + 1;
                                    Console.WriteLine("Active Page : {0}", activePage);
                                    image2.SelectActiveFrame(FrameDimension.Page, index);
                                    cvImage = new Image<Bgr, byte>((Bitmap)image2);

                                    examProcessor = processorMain;
                                    examProcessor.ProcessImage(cvImage);
                                    newList = examProcessor.getList(ref cvImage, cvImage.Convert<Gray, byte>());

                                    //getting id
                                    idProcessor = processorMain2;
                                    idProcessor.ProcessImage(cvImage);
                                    id_list_of_points = idProcessor.getList(ref cvImage, cvImage.Convert<Gray, byte>());
                                    MemoryManagement.FlushMemory();
                                    ctx.Post(d => { progressBar1.Value++; }, null);
                                }
                                if (count < pages - 1)
                                {
                                    correct.correct(newList, cvImage, answerKey, nOptions);
                                    //fta7 el id detection
                                    correct.idDetection = true;
                                    //3mal detect lal id
                                    correct.correct(id_list_of_points, cvImage, answerKey, nOptions);
                                    //rja3 sakkerle el id detection
                                    correct.idDetection = false;
                                }

                                

                                if (count == pages - 1)
                                {
                                    clone = cvImage.Clone();
                                    correct.correct(newList, cvImage, answerKey, nOptions);

                                    //fta7 el id detection
                                    correct.idDetection = true;
                                    //3mal detect lal id
                                    correct.correct(id_list_of_points, cvImage, answerKey, nOptions);
                                    //rja3 sakkerle el id detection
                                    correct.idDetection = false;
                                }
                                /*//fta7 el id detection
                                correct.idDetection = true;
                                //3mal detect lal id
                                if(count < pages - 1)
                                correct.correct(id_list_of_points, cvImage, answerKey, nOptions);
                                //rja3 sakkerle el id detection
                                correct.idDetection = false;*/
                                Interlocked.Increment(ref count);
                                Console.WriteLine("{0}", count);
                                MemoryManagement.FlushMemory();
                                //cvImage.Dispose();
                            }
                        }
                        else if (pages == 1)
                        {
                            lock (lockObject)
                            {
                                cvImage = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(file));
                                //cvImage.Bitmap = rotate.rotate(cvImage.ToBitmap());

                                //jib el id:
                                idProcessor = processorMain2;
                                idProcessor.ProcessImage(cvImage);
                                id_list_of_points = idProcessor.getList(ref cvImage, cvImage.Convert<Gray, byte>());

                                examProcessor = processorMain;
                                examProcessor.ProcessImage(cvImage);
                                newList = examProcessor.getList(ref cvImage, cvImage.Convert<Gray, byte>());

                                clone = cvImage.Clone();
                                MemoryManagement.FlushMemory();
                                correct.correct(newList, cvImage, answerKey, nOptions);
                                //fta7 el id detection
                                correct.idDetection = true;
                                //3mal detect lal id
                                correct.correct(id_list_of_points, cvImage, answerKey, nOptions);
                                //rja3 sakkerle el id detection
                                correct.idDetection = false;
                                
                            }
                        }
                        /*correct.correct(newList, cvImage, answerKey, nOptions);
                        //fta7 el id detection
                        correct.idDetection = true;
                        //3mal detect lal id
                        correct.correct(id_list_of_points, cvImage, answerKey, nOptions);
                        //rja3 sakkerle el id detection
                        correct.idDetection = false;*/

                        Interlocked.Increment(ref count);
                        Console.WriteLine("{0}", count);
                        MemoryManagement.FlushMemory();
                    });
                allStudentsGrades = correct.getAllGrades();
                allStudentsIds = correct.getAllIds();
                /*Parallel.ForEach(SingleItemPartitioner.Create(ofd.FileNames), new ParallelOptions { MaxDegreeOfParallelism = 7 }, (file) =>
                {
                    newProcessor processor = new newProcessor();
                    lock (lockObject)
                    {
                        cvImage = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(file));
                        //cvImage.Bitmap = rotate.rotate(cvImage.ToBitmap());
                        processor = processorMain;
                        processor.ProcessImage(cvImage);
                        newList = processor.getList();
                        //correct.correct(newList, cvImage, answerKey, nOptions);
                        MemoryManagement.FlushMemory();
                    }
                    correct.correct(newList, cvImage, answerKey, nOptions);
                    //cvImage.Dispose();
                    //newList.Clear();
                    MemoryManagement.FlushMemory();
                });
                allStudentsGrades = correct.getAllGrades();*/
            }
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
        private void browse_Load(object sender, EventArgs e)
        {
            InitializeOpenFileDialog();
            WindowState = FormWindowState.Maximized;
            panel3.Width = 2 * this.Width / 3;
            panel3.Height = this.Height - this.Height / 15;
            //L = processor.getList();
            //processor.firstPoint = L[0];
            //processor.lastPoint = L[L.Count - 1];

        }
        private void InitializeOpenFileDialog()
        {
            // Set the file dialog to filter for graphics files.
            this.ofd.Filter = "Image|*.bmp;*.png;*.jpg;*.tif;*.jpeg";

            //  Allow the user to select multiple images.
            this.ofd.Multiselect = true;
            //                   ^  ^  ^  ^  ^  ^  ^

            this.ofd.Title = "My Image Browser";
        }

        public void setProcessor(newProcessor p)
        {
            processorMain = p;
        }
        public void setAnswerKey(List<int> a)
        {
            answerKey = a;
        }

        private void originalPictureBox_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in newList)
            {
                e.Graphics.DrawEllipse(new Pen(Color.Red, 2), item.X, item.Y, 5, 5);
            }
            e.Graphics.DrawEllipse(new Pen(Color.Green), (float)firstPoint.X, (float)firstPoint.Y, 5, 5);
            e.Graphics.DrawEllipse(new Pen(Color.Green), (float)lastPoint.X, (float)lastPoint.Y, 5, 5);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            /*processor = processorMain;
            //exams[r].Bitmap = rotate.rotate(exams[r].ToBitmap());
            processor.ProcessImage(exams[r]);
            List<Point> newList = new List<Point>();
            newList = processor.getList();
                 = newList;
            contours = processor.getContours();
            //if (newList.Count != 320)
            originalPictureBox.Image = exams[r].ToBitmap();
            correct.correct(newList, exams[r], answerKey, nOptions);
            r++;*/
            //foreach (var exam in exams)
            //Parallel.ForEach(exams, exam =>
            /*for(int i = 0 ; i < exams.Count() ; i++)
            {
                processor = processorMain;
                //exam.Bitmap = rotate.rotate(exam.ToBitmap());
                processor.ProcessImage(exams[i]);
                List<Point> newList = new List<Point>();
                newList = processor.getList();
                //contours = processor.getContours();
                //originalPictureBox.Image = exam.ToBitmap();
                //exam.Dispose();
                correct.correct(newList,exams[i], answerKey, nOptions);
            }*/
            allStudentsGrades = correct.getAllGrades();
            failedImages = correct.getFailedImages();
        }
        public void setCorrectClass(correction c)
        {
            correct = c;
        }
        public void setNOptions(int n)
        {
            nOptions = n;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            originalPictureBox.Image = clone.Bitmap ;
        }

        private void nextBTN_Click(object sender, EventArgs e)
        {
            List<int> tempIDs = new List<int>();
            //allStudentsGrades.Clear();
            excel export = new excel();
            export.setGrades(allStudentsGrades);
            export.setIDs(allStudentsIds);
            memory.MemoryManagement.FlushMemory();
            export.Show();
            this.Hide();
        }
    }
}