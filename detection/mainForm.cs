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
using System.Drawing.Drawing2D;
namespace detection
{
    public partial class mainForm : Form
    {
        int width;  //width: New image width bas 2a3mal zoom in/out
        int height; //New image height bas 2a3mal zoom in/out
        Point startP;   //matra7 ma btikbis L mouse
        Point endP;     //matra7 ma bit5allis rasem
        Rectangle rectangleSelect;  //L rectagnle li rasamto
        Bitmap resizedImage;
        bool selectOn = false;
        Image<Bgr, byte> frame; //L soora l2asleyye
        List<Point> ezio = new List<Point>();   //FOR WASSIM'S ALGORITHM ©
        List<Point> altair = new List<Point>(); //FOR WASSIM'S ALGORITHM ©
        int i, j;   //L i counter lal zoom out wil j counter lal zoom in. ya3ne iza kabaset zoom in, i-- w j++, iza 3milet zoom out, i++ j--
        bool imageCropped = false;  //hayda biseer true lamma tikbis kabsit L crop
        Point amal = new Point();   //startP 3al asleyye
        Point tishreen = new Point();   //endP 3al asleyye
        public ImageProcessor processor;
        Image<Bgr, byte> croppedImage;
        Point referencePoint;//FOR WASSIM'S ALGORITHM ©
        modelAnswer model = new modelAnswer();
        correction correct = new correction();
        public List<int> answerKey = new List<int>();
        public List<Point> contourCenters = new List<Point>();//to save the contour centers in 
        private bool idChosen = false;//hay bet7added eza ne7na bil id aw la2, kirmel ma tfoot el deni bi ba3da
        public mainForm()
        {
            InitializeComponent();
            panel3.Width = originalPictureBox.Width;
            panel3.Height = originalPictureBox.Height;
        }
        private void open_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image|*.bmp;*.png;*.jpg;*.tif;*.jpeg";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                zoomIn.Show();
                zoomOut.Show();
                idChosen = false;//rajje3a false bas tekbos open
                openImageClass open = new openImageClass();
                open.setMain(this);
                frame = open.openButton(ofd);
                width = frame.Width;    //initial width huwwe width lsoora l2asleyye
                height = frame.Height;  //initial height huwwe height lsoora l2asleyye
                i = j = 0;  //bil awwal la sawwet zoom in wala zoom out fa tnayneton msaffareen
                referencePoint = new Point(originalPictureBox.Width / 2, originalPictureBox.Height / 2);
                ezio.Add(referencePoint);   //FOR WASSIM'S ALGORITHM ©
                altair.Add(referencePoint); //FOR WASSIM'S ALGORITHM ©
            }
            else
                ofd.Dispose();
        }

        
        private void originalPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            startP.X = e.X;
            startP.Y = e.Y;
            selectOn = true;
            Console.WriteLine("startP:{0}",startP);
        }
        private void originalPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            selectOn = false;
        }
        private void originalPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            getRectangle getRect = new getRectangle();
            if (selectOn)
            {
                endP.X = e.X;
                endP.Y = e.Y;
                rectangleSelect = getRect.returnRectangle(startP, endP);
                originalPictureBox.Refresh();
            }
        }
        private void originalPictureBox_Paint(object sender, PaintEventArgs e)
        {
                Pen centerPen = new Pen(Color.SkyBlue, 3);                              //manyake
                if (rectangleSelect != null && imageCropped == false)   //iza la sawwet crop w 3inde rectangle
                    e.Graphics.DrawRectangle(new Pen(Color.Red, 1), rectangleSelect);   //rsem L rectangle
                if (contourCenters.Count != 0)//iza list L centerat 3al soora l2asleyye mish fadye
                {
                   foreach (var item in contourCenters)
                    {
                        e.Graphics.DrawEllipse(centerPen, (float)item.X, (float)item.Y, 5, 5);
                    }
                }
                
        }//////////HAY RASMIT L CENTERAT 3AL SOORA L2ASLEYYE LAL TESTING BASS
        private void ProcessFrame2()
        {
            processor.ProcessImage(croppedImage);
            croppedImageBox.Refresh();
        }

        private void cropButton_Click(object sender, EventArgs e)
        {
            Image<Bgr, byte> frame2;//hay bi7aje 2ila la irsom L cropped image mnil soora l2asleyye
            if (rectangleSelect.Width < 10)
                MessageBox.Show("Crop Image First");
            else
            {
                getRectangle getRect = new getRectangle();
                amal.X = rectangleSelect.X;      //2ILNEHA HE
                amal.Y = rectangleSelect.Y;
                tishreen.X = rectangleSelect.X + rectangleSelect.Width;    //2ILNEHA HE 
                tishreen.Y = rectangleSelect.Y + rectangleSelect.Height;
                frame2 = frame.Clone(); //L frame 2 ra7 2e5eda cloned 3an L frame bil awwal, ba3den 7a sawweya bas matra7 ma L user rasam rectangle
                amal = getOriginalPoint(amal);    //FOR WASSIM'S ALGORITHM ©
                tishreen = getOriginalPoint(tishreen);  //FOR WASSIM'S ALGORITHM ©
                Rectangle kirdaha = new Rectangle();    //FOR WASSIM'S ALGORITHM ©
                kirdaha = getRect.returnRectangle(amal, tishreen);
                Image<Bgr, byte> imgROI = new Image<Bgr, byte>(kirdaha.Width, kirdaha.Height);  //hayda ya3ne L rectangle li rasamo L user(bas 3al soora l2asleyye)
                CvInvoke.cvSetImageROI(frame2, kirdaha);    //hon 3am ba3mil L frame2 tsewe bas L region li 3amallo L user crop
                croppedImage = frame2;  //wad7a
                originalPictureBox.Image = null;   //sheel L pictureBox taba3 L C#
                originalPictureBox.Width = originalPictureBox.Height = 0;   //sheel L pictureBox taba3 L C#
                panel3.Controls.Add(croppedImageBox); //zidle imageBox L opencv
                croppedImageBox.Left = panel3.Left + panel3.Width / 20; //design
                croppedImageBox.Image = croppedImage;   //wad7a
                croppedImageBox.Width = kirdaha.Width;  //wad7a
                croppedImageBox.Height = kirdaha.Height;    //wad7a
                imageCropped = true;
                ezio.Clear();   //FOR WASSIM'S ALGORITHM ©
                altair.Clear(); //FOR WASSIM'S ALGORITHM ©
                ApplySettings();
                zoomIn.Hide();
                zoomOut.Hide();
            }
        }



        private void submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (idChosen)//eza 3am neshte8el 3al id
                {
                    processor.idDetection = true;//3malle hayde true
                }
                browse browse = new browse();//create browse object
                newProcessor processorToBrowze = new newProcessor();
                processor.amal = amal;//FOR WASSIM'S ALGORITHM ©
                croppedImageBox.Width = croppedImageBox.Height = 0; //5alas sawwet detection w 3milet submit.7le2le la imageBox L opencv
                croppedImageBox.Image = null;   //5alas sawwet detection w 3milet submit.7le2le la imageBox L opencv
                panel3.Controls.Add(originalPictureBox);        //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
                originalPictureBox.Image = frame.ToBitmap();    //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
                originalPictureBox.Width = frame.Width;         //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
                originalPictureBox.Height = frame.Height;       //rajji3le L pictureBox L taba3 L c# w 7ottolle feya L soora l2asleyye
                imageCropped = false;
                referencePoint = new Point(originalPictureBox.Width / 2, originalPictureBox.Height / 2);    //FOR WASSIM'S ALGORITHM ©
                width = frame.Width;    //L width 3atee width lsoora l2asleyye
                height = frame.Height;  //L height 3atee height lsoora l2asleyye
                contourCenters = processor.getList();//testing 
                processorToBrowze = setNewProcessor();
                processorToBrowze.firstPoint = processor.getFirstPoint(contourCenters);
                processorToBrowze.lastPoint = processor.getLastPoint(contourCenters);

                browse.firstPoint = processor.getFirstPoint(contourCenters);    //testing - just to draw it
                browse.lastPoint = processor.getLastPoint(contourCenters);  //testing    - just to draw it
                if (idChosen)//eza sha8aleen bil id
                {
                    browse.setIdProcessor(processorToBrowze);//3mal set la id processor
                }
                else//eza la2
                {
                    browse.setProcessor(processorToBrowze);//set la processor el 3ade bi alb el browse
                }

                if (!idChosen)//eza mish sho8ol el id
                {
                    model.setProcessor(processorToBrowze);
                    model.setBrowse(browse);
                    model.Show();
                    correct.setWidthHeight(processor.getAverageWidth(), processor.getAverageHeight());
                    browse.setCorrectClass(correct);
                }
                else//eza sha8aleen bil id
                {
                    model.setIdProcessor(processorToBrowze);
                    //model.setIdBrowse(browse);
                    //model.Show();
                    //correct.setWidthHeight(processor.getAverageWidth(), processor.getAverageHeight());
                    //browse.setCorrectClass(correct);
                }
                i = j = 0;  //counters L zoom in w zoom out saffiron
                ezio.Clear();   //FOR WASSIM'S ALGORITHM ©
                altair.Clear(); //FOR WASSIM'S ALGORITHM ©
                ezio.Add(referencePoint);   //3am rastir kill shee
                altair.Add(referencePoint); //3am rastir kill shee
                minimumAreaTrackBar.Value = 600;      //3am rastir kill shee
                maximumAreaTrackBar.Value = 1050;      //INSA LLI FAT W 5ALLEENA NBALLISH SAF7A JDEEDE
                idChosen = false;//rajje3le yeha false
                processor.idDetection = false;
                memory.MemoryManagement.FlushMemory();//kib may warak
                zoomIn.Show();
                zoomOut.Show();
            }
            catch
            {
                MessageBox.Show("Process exam first", "Error");
            }
        }
        public Point getOriginalPoint(Point point)  //WASSIM'S ALGORITHM ©
        {
            Point originalPoint = new Point(); 
            int a, b, w, x;
            
            if (i > 0 && j <= 0)
            {
                a = (point.X * ezio[i - 1].X) / ezio[i].X;
                b = (point.Y * ezio[i - 1].Y) / ezio[i].Y;
                w = Math.Abs(a - point.X);
                x = Math.Abs(b - point.Y);
                originalPoint.X = point.X + (i * w);
                originalPoint.Y = point.Y + (i * x);
            }
            else if (j > 0 && i <= 0)
            {
                a = (point.X * altair[j - 1].X) / altair[j].X;
                b = (point.Y * altair[j - 1].Y) / altair[j].Y;
                w = Math.Abs(a - point.X);
                x = Math.Abs(b - point.Y);
                originalPoint.X = point.X - (j * w);
                originalPoint.Y = point.Y - (j * x);
            }
            if (j == 0 && i == 0)
                return point;
            return originalPoint;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            answerKey = model.getAnswerKey();
            formLoad load = new formLoad();
            load.setMain(this);
            load.load();
            panel3.BackColor = Color.Transparent;
            groupBox1.BackColor = Color.Transparent;
            zoomIn.Hide();
            zoomOut.Hide();

        }

        private void mainForm_Resize(object sender, EventArgs e)   //ho lal design killon. bala ba3basa.
        {
            mainFormResize resize = new mainFormResize();
            resize.setMain(this);
            resize.resize();
            this.Invalidate();
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            if (imageCropped == true)   //hay inno iza 3amil crop ya3ne 3inde imageBox L opencv fa kabset L zoom in wil zoom out baddeesh yehon
                return;
            else if (imageCropped == false) //iza la2 ta3a la 5abbrak
            {
                if (width >= 4000)  //iza kabbarta kteer 5alas hesh
                    return;
                width = width + frame.Width / 5;   //iza la2 zeed L width
                height = height + frame.Height / 5;    //w zeed L height
                i--;    //3am sawwe zoom in fa counter L zoom out nazzlo
                j++;    //w counter L zoom in zeedo
                resizedImage = new Bitmap(frame.ToBitmap(), new Size(width, height));   //L resizedImage heyye L soora l2asleyye resized
                originalPictureBox.Image = resizedImage;    //wad7a
                originalPictureBox.Width = width;   //wad7a
                originalPictureBox.Height = height; //wad7a
                referencePoint.X = originalPictureBox.Width / 2;    //FOR WASSIM'S ALGORITHM ©
                referencePoint.Y = originalPictureBox.Height / 2;   //FOR WASSIM'S ALGORITHM ©
                if (i >= 0) //FOR WASSIM'S ALGORITHM ©
                    ezio.RemoveAt(ezio.Count - 1);  //FOR WASSIM'S ALGORITHM ©
                if (j > 0)  //FOR WASSIM'S ALGORITHM ©
                    altair.Add(referencePoint); //FOR WASSIM'S ALGORITHM ©
                originalPictureBox.Refresh();   
            }
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            if (imageCropped == true)   //hay inno iza 3amil crop ya3ne 3inde imageBox L opencv fa kabset L zoom in wil zoom out baddeesh yehon
                return;

            else if (imageCropped == false) //iza la2 ta3a la 5abbrak
            {
                if (width <= 300)   //iza zagharta kteer 5alas hesh
                    return;
                width = width - frame.Width / 5;   //iza la2 na22is L width
                height = height - frame.Height / 5;    //w na22is L height
                i++;    //3am sawwe zoom out fa counter L zoom out zeedo
                j--;    //w counter L zoom in nazzlo
                resizedImage = new Bitmap(frame.ToBitmap(), new Size(width, height));   //L resizedImage heyye L soora l2asleyye resized
                originalPictureBox.Image = resizedImage;    //wad7a
                originalPictureBox.Width = width;   //wad7a
                originalPictureBox.Height = height; //wad7a
                referencePoint.X = originalPictureBox.Width / 2;    //FOR WASSIM'S ALGORITHM ©
                referencePoint.Y = originalPictureBox.Height / 2;   //FOR WASSIM'S ALGORITHM ©
                if (i > 0)  //FOR WASSIM'S ALGORITHM ©
                    ezio.Add(referencePoint);   //FOR WASSIM'S ALGORITHM ©
                if (j >= 0) //FOR WASSIM'S ALGORITHM ©
                    altair.RemoveAt(altair.Count - 1);  //FOR WASSIM'S ALGORITHM ©
                originalPictureBox.Refresh();  
            }
        }
        private void ApplySettings()
        {
            processor.minContourArea = minimumAreaTrackBar.Value; //kill ma 7arrik bil trackBars update L minContourArea
            processor.maxContourArea = maximumAreaTrackBar.Value; //kill ma 7arrik bil trackBars update L maxContourArea
            ProcessFrame2();    //w 3ja3 3mal process
        }

        private void minimumAreaTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                minAreaLabel.Text = minimumAreaTrackBar.Value.ToString();
                ApplySettings();    //update values L minArea wil maxArea
            }
            catch
            {
                MessageBox.Show("Load and crop image first", "Error");
            }
        }

        private void maximumAreaTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                maxAreaLabel.Text = maximumAreaTrackBar.Value.ToString();
                ApplySettings();    //update values L minArea wil maxArea
            }
            catch
            {
                MessageBox.Show("Load and crop image first", "Error");
            }
        }

        private void croppedImageBox_Paint(object sender, PaintEventArgs e)
        {
            Pen circlePen = new Pen(Color.Red, 2);
            if (processor.contours == null) //iza fesh canateer insa
                return;
            if (croppedImage == null) return;   //iza mish 3amil crop insa.
                foreach (var contour in processor.contours)
                {
                    ////////////////////////////////////HERE CONTOUR IS DRAWING/////////////////////////////////////////////
                    if (contour.Total > 1)
                    {
                        e.Graphics.DrawLines(circlePen, contour.ToArray()); //rsimle canateer
                        e.Graphics.DrawRectangle(circlePen,contour.BoundingRectangle);
                    }
                }
        }
        public void setModelAnswerForm(modelAnswer m)
        {
            model = m;
        }
        public void setProcessor(ImageProcessor p)
        {
            processor = p;
        }
        public newProcessor setNewProcessor()
        {
            newProcessor processorToBrowze = new newProcessor();
            processorToBrowze.setXdiff(processor.getXdiff());
            processorToBrowze.setYpad(processor.getYpad());
            processorToBrowze.setIDXdiff(processor.getIDXdiff());
            processorToBrowze.setIDYpad(processor.getIDYpad());
            processorToBrowze.averageArea = processor.getAverageArea();
            processorToBrowze.minContourArea = minimumAreaTrackBar.Value - 50;
            processorToBrowze.maxContourArea = maximumAreaTrackBar.Value + 50;
            processorToBrowze.setNumberOfQ(processor.getNumberOfQ());
            processorToBrowze.setNumberOfC(processor.getNumberOfC());
            processorToBrowze.setQperColumnList(processor.getQperColumnList());
            processorToBrowze.setNOFM(processor.getNOFM());
            if (idChosen)//eza sha8aleen bil id
            {
                processorToBrowze.idDetection = true;//3malle hayde true
            }
            else//eza la2
            {
                processorToBrowze.idDetection = false;//t2akadle enna false
            }
            return processorToBrowze;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            idChosen = true;
            submit_Click(sender, e);
        }

        private void mainForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
    }
}
