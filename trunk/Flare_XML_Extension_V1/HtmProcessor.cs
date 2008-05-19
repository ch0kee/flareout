using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Xml;
using System.IO;

namespace FlareOut
{
    class HtmProcessor
    {
        string m_HtmFile;
        List<ImageResizer> m_ImageList;
        public HtmProcessor(string htmfilename)
        {
            m_HtmFile = htmfilename;
        }
        // képek és a tervezett méret kigyûjtése

        public bool CollectImagesWithSize()
        {
            bool OK = true;
            const string m_HTMSTYLE_WPrefix = "width: ";
            const string m_HTMSTYLE_HPrefix = "height: ";

            using (FileStream fs = new FileStream(m_HtmFile, FileMode.Open))
            {
                m_ImageList = new List<ImageResizer>();
                //
                XmlDocument doc = new XmlDocument();
                doc.Load(fs);
                //
                XmlNodeList imagenodes = doc.GetElementsByTagName("img"); // képek
                string projectpath = FlareProjectMgr.ProjectPath;
                foreach (XmlNode img in imagenodes)
                {
                    // style="width: 451px;height: 48px;"
                    string style = img.Attributes["style"].Value;
                    string[] delimiter = { "px;" };
                    string[] WH = style.Split(delimiter, StringSplitOptions.RemoveEmptyEntries);
                    string W = WH[0].Substring(m_HTMSTYLE_WPrefix.Length);
                    string H = WH[1].Substring(m_HTMSTYLE_HPrefix.Length);

                    int newWidth = int.Parse(W, System.Globalization.NumberStyles.Integer);
                    int newHeight = int.Parse(H, System.Globalization.NumberStyles.Integer);
                    //
                    string imagepath = img.Attributes["src"].Value;
                    imagepath = imagepath.Replace('/', '\\');
                    imagepath = imagepath.TrimStart('.');
                    imagepath = projectpath + @"\Content" + imagepath;
                    //
                    if (File.Exists(imagepath))
                    {
                        m_ImageList.Add(new ImageResizer(imagepath, newWidth, newHeight));
                    }
                    else
                        OK = false;
                }
            }
            return OK;
        }
        public List<ImageResizer> Images
        {
            get
            {
                return m_ImageList;
            }
        }
    }
}
