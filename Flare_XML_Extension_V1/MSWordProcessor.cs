using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using MsWord = Microsoft.Office.Interop.Word;
using System.Drawing;

namespace FlareOut
{
    static class MSWordProcessor
    {
        static private object na = System.Reflection.Missing.Value;
        static MsWord.Application m_WordApp;
        static MsWord.Document m_WordDoc;
        static MSWordProcessor()
        {
            Reset();
        }

        static public void Reset()
        {
            m_Books = new ArrayList();
            m_IsLoaded = false;

        }
        static bool m_IsLoaded;
        static public bool IsLoaded { get { return m_IsLoaded; } }
        static object NO = false;
        static object YES = true;

        static public void OpenDocument(object filename)
        {

            m_WordDoc = m_WordApp.Documents.Open(ref filename/*�tvonal*/, ref na, ref YES, ref na, ref na,
                ref na, ref na, ref na, ref na, ref na, ref na, ref NO/*Megjelenjen?NE!*/, ref na, ref na, ref na, ref na);
            m_WordDoc.Activate();
        }

        static public void WriteDocument(object filename)
        {
            m_WordDoc = m_WordApp.Documents.Open(ref filename/*�tvonal*/, ref na, ref NO, ref na, ref na,
                ref na, ref na, ref na, ref na, ref na, ref na, ref NO/*Megjelenjen?NE!*/, ref na, ref na, ref na, ref na);
            m_WordDoc.Activate();
        }
        
        static public void StartWord()
        {
            m_WordApp = new MsWord.Application();
            m_WordApp.Visible = false;  // h�tt�rben fut

        }
        static public void StopWord()
        {
            m_WordApp.Quit(ref NO, ref na, ref na);
        }
         
        static private string NormalizeTopicString( string tstr )
        {
            for (int i = 0; i < tstr.Length; ++i)
                if (!char.IsLetterOrDigit(tstr[i])) 
                {
                    tstr = tstr.Insert(i, " ");
                    tstr = tstr.Remove(i+1,1);
                }
            // whitespaceek normaliz�l�sa
            tstr = Regex.Replace( tstr, @"\s", " " );
            //c k�zb�ls� sz�k�z�k elt�vol�t�sa
            for(int f = 0; (f = tstr.IndexOf("  ")) != -1; tstr = tstr.Remove(f, 1));
            //c sor eleji, sor v�gi sz�k�z�k elt�vol�t�sa
            return tstr.Trim();
        }


        public static void ConvertObjectsToPNG(ProgressBar pbar)
        {
            pbar.Value = 0;
            pbar.Minimum = 0;
            pbar.Step = 1;
            pbar.Maximum = m_WordDoc.InlineShapes.Count;

            for (int i = 0; i < m_WordDoc.InlineShapes.Count; ++i, pbar.Increment(1))
            {
                Clipboard.Clear();
                MsWord.InlineShape shp = m_WordDoc.InlineShapes[i];
                shp.Select();
                m_WordApp.Selection.Copy();
                if (Clipboard.ContainsImage())
                {
                    Image img = Clipboard.GetImage();
                    img.Save("ch0kee_tmp"+i+".png", System.Drawing.Imaging.ImageFormat.Png);
                    
                    //m_WordApp.Selection.InsertFile("ch0kee_tmp.png", ref na, ref na, ref na, ref na);
                }
                else
                    MessageBox.Show("No Image Object");
            }
            m_WordDoc.Save();
        }

        public static void ReadActiveDocStyles(ProgressBar pbar)
        {
            pbar.Value = 0;
            pbar.Minimum = 0;
            pbar.Step = 1;
            // kigy�jt�s
            //////////////////////////////////////////////////////////////////////////
            MsWord.ListParagraphs list = m_WordDoc.ListParagraphs;
            pbar.Maximum = list.Count;
            MsWord.Paragraph p = m_WordDoc.Paragraphs.First;
            string FirstParagraphsStylename = (p.get_Style() as MsWord.Style).NameLocal;
            if (Options.IsHeading(FirstParagraphsStylename))
                if (p.Range.Start != list[1].Range.Start) // ha nem fogjuk m�gegyszer fedolgozni
                    m_Books.Add(new Book(NormalizeTopicString(p.Range.Text), //c fejezetc�m
                                         Options.Depth(FirstParagraphsStylename))); //c m�lys�g
            for (int i = 1; i <= list.Count; ++i, pbar.Increment(1))
            {
                string ParagraphsStylename = (list[i].get_Style() as MsWord.Style).NameLocal;
                if (Options.IsHeading(ParagraphsStylename))
                    m_Books.Add(new Book(NormalizeTopicString(list[i].Range.Text),
                                         Options.Depth(ParagraphsStylename)));
            }

           // (Microsoft.Office.Interop.Word);
            {
                (m_WordDoc as MsWord._Document).Close(ref NO, ref na, ref na);
            }
            m_IsLoaded = true;

        }

        static ArrayList m_Books;
        class Book
        {
            int m_Depth;
            string m_Text;
            public Book(string text, int depth) { m_Text = text; m_Depth = depth; }
            public int Depth { get { return m_Depth;} }
            public string Text { get { return m_Text;} }
        }
        //////////////////////////////////////////////////////////////////////////
        // rendez� f�ggv�nyek
        public static int Count { get { return m_Books.Count; } }
        public static int Depth(int index)
        {
            return (m_Books[index] as Book).Depth;
        }
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////


        static public void EqualizeXMLDepth(XmlNode node)
        {
            XmlDepthSorter xmlSorter = new XmlDepthSorter(node);
            MainForm.Output = "DOC c�msorok elemsz�ma : " + Count.ToString()
                + ";C�l XML f�jl elemsz�ma : " + xmlSorter.Count.ToString();

            string flarelogfile = MainForm.ExeDirectory+"TOPICSflare.log";
            string doclogfile = MainForm.ExeDirectory + "TOPICSword.log";
            MainForm.Output = "!Log f�jlok l�trehoz�sa!";
            MainForm.Output = "Flare tartalomjegyz�k fejezetek : " + flarelogfile;
            MainForm.Output = "MS Word fejezetek : " + doclogfile;
            CreateLogFile(flarelogfile, xmlSorter);
            CreateLogFile(doclogfile);
            try
            {
                if (xmlSorter.Count == Count)
                {
                    // egyeztet�s
                    for (int i = 0; i < xmlSorter.Count; )
                    {
                        while (i < xmlSorter.Count && xmlSorter.Depth(i) == Depth(i))
                            ++i;
                        if (i < xmlSorter.Count)
                        {
                            if (xmlSorter.Depth(i) < Depth(i))
                            {
                                xmlSorter.IncDepth(i);
                                i = 0;
                                xmlSorter.Reorder();
                            }
                            else
                            {
                                MainForm.Output = "Hib�s szerkezet";
                                return;
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);

            }
        }

        private static void CreateLogFile(string doclogfile)
        {
            using (StreamWriter writer = new StreamWriter(doclogfile))
            {
                writer.WriteLine("-- Sorsz�m :: M�lys�g - Fejezetc�m --");
                writer.WriteLine("-- FlareOut MS Word file log       --");
                writer.WriteLine("-------------------------------------");

                for (int i = 0; i < m_Books.Count; ++i)
                    writer.WriteLine((i+1).ToString("D4")+" :: "+ (Depth(i)+1).ToString()+" - "+(m_Books[i] as Book).Text);
                writer.WriteLine("-------------------------------------");
                writer.WriteLine("-- FlareOut MS Word file log       --");
            }
        }

        private static void CreateLogFile(string logfile, XmlDepthSorter xmlsorter)
        {
            using (StreamWriter writer = new StreamWriter(logfile))
            {
                writer.WriteLine("-- Sorsz�m :: M�lys�g - Fejezetc�m --");
                writer.WriteLine("-- FlareOut Topic file log         --");
                writer.WriteLine("-------------------------------------");
                for (int i = 0; i < xmlsorter.Count; ++i)
                    writer.WriteLine((i + 1).ToString("D4") + " :: " + (xmlsorter.Depth(i)+1).ToString() + " - " + xmlsorter[i].Attributes["Title"].Value);
                writer.WriteLine("-------------------------------------");
                writer.WriteLine("-- FlareOut Topic file log         --");
            }
        }

    }
}
