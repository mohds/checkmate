using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace detection
{
    public class openImageClass
    {
        mainForm main;
        public Image<Bgr, byte> openButton(OpenFileDialog ofd)
        {
            Image<Bgr, byte> frame;
            rotation rotateImage = new rotation();
            //OpenFileDialog ofd = new OpenFileDialog();
            //ofd.Filter = "Image|*.bmp;*.png;*.jpg;*.tif;*.jpeg";
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
                frame = new Image<Bgr, byte>((Bitmap)Bitmap.FromFile(ofd.FileName));    //frame = lsoora lfata7a L user
                frame.Bitmap = rotateImage.rotate(frame.ToBitmap());                    //hon 3am naswwe rotation
                //width = frame.Width;    //initial width huwwe width lsoora l2asleyye
                //height = frame.Height;  //initial height huwwe height lsoora l2asleyye
                //resizedImage = new Bitmap(frame.ToBitmap(), new Size(width, height));
                main.originalPictureBox.Image = frame.ToBitmap();   //shkella lsoora l2asleyye
                
                main.originalPictureBox.Height = frame.Height;      //shkella height lsoora l2asleyye
                main.originalPictureBox.Width = frame.Width;        //shkella width lsoora l2asleyye
                return frame;
            //}
            //else
            //{
            //    return null;
            //}
        }
        public void setMain(mainForm m)
        {
            main = m;
        }
    }
}
