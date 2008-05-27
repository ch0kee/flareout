using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace FlareOut
{
    class ImageResizer
    {
        Image m_Image;
        string m_Filename;
        ImageFormat m_Format;
        Size m_NewSize;
        //
        public ImageResizer(string filename, int newWidth, int newHeight)
        {
            m_Filename = filename;
            using (FileStream fs = new FileStream(m_Filename, FileMode.Open))
                m_Image = Image.FromStream(fs);
            m_NewSize = new Size(newWidth, newHeight);
        }
        //
        bool SetFormat(string filename)
        {
            string ext = Path.GetExtension(filename);
            bool formatset = true;

            if (ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg")    //JPG
                m_Format = ImageFormat.Jpeg;
            else if (ext.ToLower() == ".png")                           //PNG
                m_Format = ImageFormat.Png;
            else if (ext.ToLower() == ".gif")                           //GIF
                m_Format = ImageFormat.Gif;
            else
                formatset = false;
            return formatset;
        }
        //
        bool Save(string savetofile)
        {
            if (SetFormat(savetofile))
            {
                using (FileStream fs = new FileStream(m_Filename, FileMode.Truncate))
                    m_Image.Save(fs, m_Format);
                return true;
            }
            return false;
        }
        //
        void SetImageSize(int width, int height)
        {
            int originalWidth = m_Image.Width;
            int originalHeight = m_Image.Height;

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
                destX = Convert.ToInt16((width -
                    (originalWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((height -
                              (originalHeight * nPercent)) / 2);
            }
            int destWidth = (int)(originalWidth * nPercent);
            int destHeight = (int)(originalHeight * nPercent);
            //
            using (Bitmap bmPhoto = new Bitmap(width, height, PixelFormat.Format24bppRgb))
            {
                bmPhoto.SetResolution(m_Image.HorizontalResolution, m_Image.VerticalResolution);
                // 
                using (Graphics grPhoto = Graphics.FromImage(bmPhoto))
                {
                    grPhoto.Clear(Color.White);
                    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    grPhoto.DrawImage(m_Image,
                        new Rectangle(destX, destY, destWidth, destHeight),
                        new Rectangle(sourceX, sourceY, originalWidth, originalHeight),
                        GraphicsUnit.Pixel);
                }
                m_Image = bmPhoto.Clone() as Image;
            }
        }

        public void ResizeImage()
        {
            SetImageSize(m_NewSize.Width, m_NewSize.Height);
            Save(m_Filename);
        }
    }
}
