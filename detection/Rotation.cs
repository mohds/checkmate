using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Imaging.Filters;
using AForge.Imaging;
using System.Windows;
using System.Drawing.Drawing2D;

namespace detection
{
    public class rotation
    {
        public Bitmap rotate(Bitmap img)
        {
            ConvertTo8bpp convertor = new ConvertTo8bpp();
            DocumentSkewChecker skewChecker = new DocumentSkewChecker();
            System.Drawing.Image temp = convertor.Convert(img);
            double angle = skewChecker.GetSkewAngle((Bitmap)temp);
            RotateBilinear rotationFilter = new RotateBilinear(-angle);
            rotationFilter.FillColor = Color.White;
            Bitmap rotatedImage = rotationFilter.Apply(img);
            return rotatedImage;
        }
    }
}
