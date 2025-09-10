using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO;
using System.IO.Ports;
using System.Globalization;

namespace BusinesEntities
{
   public  class JewelPictures
    {
       public int CustId { get; set; }
       public string TagNo { get; set; }
        private byte[] imageMemory;

        public byte[] ImageMemory
        {
            get { return imageMemory; }
            set { imageMemory = value; }
        }
        private byte[] imageMemorySmall;

        public byte[] ImageMemorySmall
        {
            get { return imageMemorySmall; }
            set { imageMemorySmall = value; }
        }
        private byte[] imageMemory1;

        public byte[] ImageMemory1
        {
            get { return imageMemory1; }
            set { imageMemory1 = value; }
        }
        private byte[] imageMemory2;

        public byte[] ImageMemory2
        {
            get { return imageMemory2; }
            set { imageMemory2 = value; }
        }
        private byte[] imageMemory3;

        public byte[] ImageMemory3
        {
            get { return imageMemory3; }
            set { imageMemory3 = value; }
        }

        private byte[] imageMemory4;

        public byte[] ImageMemory4
        {
            get { return imageMemory4; }
            set { imageMemory4 = value; }
        }

        private byte[] imageMemory5;

        public byte[] ImageMemory5
        {
            get { return imageMemory5; }
            set { imageMemory5 = value; }
        }

        private byte[] imageMemory6;

        public byte[] ImageMemory6
        {
            get { return imageMemory6; }
            set { imageMemory6 = value; }
        }
        private byte[] imageMemory7;

        public byte[] ImageMemory7
        {
            get { return imageMemory7; }
            set { imageMemory7 = value; }
        }
        private byte[] imageMemory8;

        public byte[] ImageMemory8
        {
            get { return imageMemory8; }
            set { imageMemory8 = value; }
        }
       //private byte[] imageMemory;

       //public byte[] ImageMemory
       //{
       //    get { return imageMemory; }
       //    set { imageMemory = value; }
       //}

       public Byte[] ConvertImageToBinary(Image imag)
       {
           MemoryStream ms = new MemoryStream();
           //imag.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
           imag.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
           Byte[] bytes = ms.GetBuffer();
           return bytes;
       }
       public Image resizeImage(Image imgToResize, Size size)
       {
           int sourceWidth = imgToResize.Width;
           int sourceHeight = imgToResize.Height;

           decimal nPercent = 0;
           decimal nPercentW = 0;
           decimal nPercentH = 0;

           nPercentW = ((decimal)size.Width / (decimal)sourceWidth);
           nPercentH = ((decimal)size.Height / (decimal)sourceHeight);

           if (nPercentH < nPercentW)
               nPercent = nPercentH;
           else
               nPercent = nPercentW;

           int destWidth = (int)(sourceWidth * nPercent);
           int destHeight = (int)(sourceHeight * nPercent);

           Bitmap b = new Bitmap(destWidth, destHeight);
           Graphics g = Graphics.FromImage((Image)b);
           g.InterpolationMode = InterpolationMode.HighQualityBicubic;

           g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
           g.Dispose();

           return (Image)b;
       }
    }
}
