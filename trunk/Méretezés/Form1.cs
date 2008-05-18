using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
namespace JpegResizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = (int)numWidth.Value;
            int height = (int)numHeight.Value;

            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Jpegek egyelõre (*.jpg)|*.jpg";
            if (of.ShowDialog() == DialogResult.OK)
            {
                SetSize(of.FileName, width, height);
            }
        }

        private void SetSize(string f, int width, int height)
        {
            Image jpg = Image.FromFile(f);
            Image newjpg = SetImageSize(jpg, width, height);
            jpg.Dispose();

            newjpg.Save(f);
        }

        private Image SetImageSize(Image jpg, int width, int height)
        {
            int originalWidth = jpg.Width;
            int originalHeight = jpg.Height;

            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;

            float nPercentW = ((float)width / (float)originalWidth);
            float nPercentH = ((float)height / (float)originalHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((width - (originalWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height -
                              (originalHeight * nPercent)) / 2);
            }

            int destWidth = (int)(originalWidth * nPercent);
            int destHeight = (int)(originalHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(width, height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(jpg.HorizontalResolution,
                             jpg.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.Red);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(jpg,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, originalWidth, originalHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
    }
}