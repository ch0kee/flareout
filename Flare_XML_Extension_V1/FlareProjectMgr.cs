using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace FlareOut
{
    class FlareProject
    {
        // public
        public string ProjectPath
        {
            get { return Path.GetDirectoryName(m_ProjectFilePath); }
        }

        public FlareProject(string ProjectPath)
        {

            m_ProjectFilePath = ProjectPath;
            // toc path
            string tocpath = Path.GetDirectoryName(m_ProjectFilePath)+@"\Project\TOCs";
            string[] files = Directory.GetFiles(tocpath);
            foreach (string file in files)
            {
                if (file.EndsWith(".fltoc"))
                {
                    m_TOCPath = file;
                }
            }
            // alias & define path
            string aliaspath = Path.GetDirectoryName(m_ProjectFilePath) + @"\Project\Advanced";
            files = Directory.GetFiles(aliaspath);
            foreach(string file in files)
                if (file.EndsWith(".flali"))
                {
                    m_AliasPath = file;
                }
                else if (file.EndsWith(".h"))
                {
                    m_DefinePath = file;
                }
            HandleErrors();
        }

        private void HandleErrors()
        {
            if (m_TOCPath == null)
            {
                MessageBox.Show("Hiba! Ellenőrizd a topik fájl létezését, majd indítsd újra a programot!");
            }
            if (m_AliasPath == null)
            {
                MessageBox.Show("Hiba! Ellenőrizd az alias fájl létezését, majd indítsd újra a programot!");
            }
            if (m_DefinePath == null)
            {
                MessageBox.Show("Hiba! Ellenőrizd a define fájl létezését, majd indítsd újra a programot!");
            }
        }
        public string TOCPath { get { return m_TOCPath; } }
        public string ALIASPath { get { return m_AliasPath; } }
        public string DEFINEPath { get { return m_DefinePath; } }
        // private
        string  m_ProjectFilePath = null;
        string m_TOCPath = null;
        string m_AliasPath = null;
        string m_DefinePath = null;
    }
    static class FlareProjectMgr
    {
        static bool m_IsLoaded;
        static FlareProject m_Project;
        // public
        public static string ProjectPath
        {
            get { return m_Project.ProjectPath; }
        }


        public static bool LoadFlareProjectFile(string filename)
        {
            OpenFileDialog TocLocator = new OpenFileDialog();
            TocLocator.Title = "Flare projekt betöltése";
            TocLocator.Filter = "Flare projekt fájl (*.flprj)|*.flprj";
            TocLocator.CheckFileExists = true;
            try
            {
                if (filename != null && (m_IsLoaded = File.Exists(filename)))
                    m_Project = new FlareProject(filename);
                else if (m_IsLoaded = TocLocator.ShowDialog() == DialogResult.OK)
                    m_Project = new FlareProject(Options.ProjectDir = TocLocator.FileName);
                else
                    return false;
            }
            catch(Exception)
            {
                MessageBox.Show("Hiba!");
                m_IsLoaded = false;
                return false;
            }
            return true;
        }

        public static void RestructureTOCwithMSWORD()
        {
            if (!m_IsLoaded)
                return;

            XmlDocument Target = new XmlDocument();
            Target.Load(m_Project.TOCPath);
            // az elemszámok ellenőrzése a DepthSorterekben van
            //////////////////////////////////////////////////////////////////////////
            MSWordProcessor.EqualizeXMLDepth(Target.LastChild);
            //////////////////////////////////////////////////////////////////////////
            Target.Save(new BackupMaker(m_Project.TOCPath,true));
        }
        //////////////////////////////////////////////////////////////////////////
        // Topic
        static FlareTopicFile m_TopicFile;
        public static FlareTopicFile TopicFile
        {
            get { return m_TopicFile; }
        }
        static TreeView m_TOC;
        public static void FillTreeWithToc(TreeView treeTOC)
        {
            m_TOC = treeTOC;
            m_TopicFile = new FlareTopicFile(treeTOC, m_Project.TOCPath);
            m_TopicFile.TartalomjegyzékFaFeltöltése();
        }
        public static void RepairNumbering()
        {
            if (!m_IsLoaded)
                return;

            XmlDocument Target = new XmlDocument();
            Target.Load(m_Project.TOCPath);
            ArrayList next_number = new ArrayList();
            //////////////////////////////////////////////////////////////////////////
            XmlDepthSorter xmlSorter = new XmlDepthSorter(Target.LastChild);
            // egyeztetés
            next_number.Add(0);
            for (int i = 0; i < xmlSorter.Count; ++i)
            {
                string prefix = "";
                for (int j = 0; j <= xmlSorter.Depth(i); ++j)
                {
                    if (j >= next_number.Count)
                        next_number.Add(0);
                    if (j == xmlSorter.Depth(i))
                        next_number[j] = GetChildBeforeNum(xmlSorter[i])+1;
                    prefix += ((int)next_number[j]).ToString() + ".";
                }
                if (xmlSorter[i].Attributes["Title"].Value.StartsWith(prefix))
                    MainForm.Output = "Már sorszámozott :" + xmlSorter[i].Attributes["Title"].Value;
                else
                 xmlSorter[i].Attributes["Title"].Value = prefix + " " + xmlSorter[i].Attributes["Title"].Value;
            }
            //////////////////////////////////////////////////////////////////////////
            Target.Save(new BackupMaker(m_Project.TOCPath,true));
        }
        public static int GetChildBeforeNum( XmlNode node )
        {
            int pretendchild = 0;
            while (node.PreviousSibling != null) 
                { node = node.PreviousSibling; ++pretendchild;  }
            return pretendchild;
        }
        //////////////////////////////////////////////////////////////////////////
        // Alias
        static FlareAliasFile m_AliasFile;
        public static FlareAliasFile AliasFile
        {
            get { return m_AliasFile; }
        }
        public static void FillListWithAliases(ListView listDefines)
        {
            m_AliasFile = new FlareAliasFile(listDefines, m_Project.ALIASPath, m_Project.DEFINEPath);
            m_AliasFile.FillDefineList();
            m_AliasFile.LoadContextRelations(m_TopicFile);
        }

        public static void SaveAliases(bool CreateBackup)
        {
            m_AliasFile.SaveModifiedXml(m_TopicFile, CreateBackup);
        }


        public static void SaveDefinesTo(string filename)
        {
            if (MainForm.AliasList.Items.Count == 0) return;
            MainForm.Output = "#define azonosítók kiírása HTML Help Compiler kompatibilis formátumban...";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (ContextNumber cnum in MainForm.AliasList.Items)
                {
                    string line = "#define "+cnum.DefineName+" "+cnum.DefineNumber;
                    writer.WriteLine(line);
                }
            }
            MainForm.Output = "#define azonosítók kiírása kész.";
            
        }

        public static void ResizeAllImageInTopics()
        {            
            XmlDocument doc = new XmlDocument();
            doc.Load(m_Project.TOCPath);
            //////////////////////////////////////////////////////////////////////////
            XmlDepthSorter topics = new XmlDepthSorter(doc.LastChild);
            bool everything_ok = true;
            for (int i = 0; i < topics.Count; ++i)
            {
                XmlNode topic = topics[i];
                string htmfile = ProjectPath + (topic.Attributes["Link"].Value.Replace('/', '\\'));
                HtmProcessor htmproc = new HtmProcessor(htmfile);
                // ha sikerül kigyűjteni az ÖSSZES képet
                if (htmproc.CollectImagesWithSize())
                    // mindet átméretezzük
                    foreach (ImageResizer ir in htmproc.Images)
                        ir.ResizeImage();
                else
                {
                    MainForm.Output = "Hiba a képek méretezése során : "+Path.GetFileName(htmfile);
                    MainForm.Output = htmproc.GetErrorString();
                    everything_ok = false;
                }
            }
            if (everything_ok)
                MainForm.Output = "A képek átméretezése befejeződött.";

        }

        public static void FilterNonEnglishCharacters()
        {
            if (!m_IsLoaded)
                return;

            XmlDocument Target = new XmlDocument();
            Target.Load(m_Project.TOCPath);

            XmlNodeList topics = Target.GetElementsByTagName("TocEntry");
            foreach(XmlNode t in topics)
            {
                string path = t.Attributes["Link"].Value.Replace('/','\\');
                string filename = Path.GetFileName(path);
                string renamed = Path.GetDirectoryName(path)+@"\"+filename.Replace('ù','u').Replace('à','a').Replace('è','e');
                if (renamed != path)
                {
                    MainForm.Output = "Átnevezés - Régi : "+Path.GetFileName(path)+" - Új : "+Path.GetFileName(renamed);
                    try
                    {
                        if (File.Exists(ProjectPath + renamed))
                            File.Delete(ProjectPath + renamed);
                        File.Move(ProjectPath+path, ProjectPath+renamed);
                        t.Attributes["Link"].Value = renamed.Replace('\\', '/');
                    }
                    catch(System.IO.FileNotFoundException e)
                    {
                        MainForm.Output = "Nem található : " + e.FileName;
                    }
                }
            }
            Target.Save(new BackupMaker(m_Project.TOCPath, true));
        }

        public static void MakeAliasesUsingOtherProject()
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Minta aliasok betöltése";
            of.Filter = "Flare alias fájl (*.flali)|*.flali";
            of.CheckFileExists = true;
            if (of.ShowDialog() == DialogResult.OK)
            {
                string pattern_aliasfile = of.FileName;

                of.Title = "Minta tartalomjegyzék betöltése";
                of.Filter = "Flare toc fájl (*.fltoc)|*.fltoc";
                if (of.ShowDialog() == DialogResult.OK)
                {
                    // minta betöltése
                    string pattern_topicfile = of.FileName;
                    XmlDocument pattern_flali = new XmlDocument();
                    pattern_flali.Load(pattern_aliasfile);
                    XmlNodeList pattern_aliases = pattern_flali.GetElementsByTagName("Map");

                    XmlDocument pattern_fltoc = new XmlDocument();
                    pattern_fltoc.Load(pattern_topicfile);
                    XmlDepthSorter pattern_topics = new XmlDepthSorter(pattern_fltoc.LastChild);

                    // módosítandó betöltése
                    XmlDocument this_flali = new XmlDocument();
                    this_flali.Load(m_AliasFile.Path);
                    this_flali.LastChild.RemoveAll(); // aliasok törlése

                    XmlDocument this_fltoc = new XmlDocument();
                    this_fltoc.Load(m_TopicFile.Path);
                    XmlDepthSorter this_topics = new XmlDepthSorter(this_fltoc.LastChild);

                    MainForm.Output = "Aktuális tartalomjegyzékben a fejezetek száma : " + this_topics.Count;
                    MainForm.Output = "Mintaként használt tartalomjegyzékben a fejezetek száma : " + pattern_topics.Count;

                    if (this_topics.Count != pattern_topics.Count)
                    {
                        MainForm.Output = "Megszakítás : Fejezetszám eltérés.";
                        MainForm.Output = "Ellenőrzöm az elcsúszásokat...";
                        for (int i = 0; i < pattern_topics.Count && i < this_topics.Count; ++i)
                        {
                            if (pattern_topics.Depth(i) != this_topics.Depth(i))
                            {
                                MainForm.Output = "Elcsúszás :";
                                MainForm.Output = "Minta   :("+pattern_topics.Depth(i).ToString()+") " + pattern_topics[i].Attributes["Title"].Value;
                                MainForm.Output = "Projekt :(" + this_topics.Depth(i).ToString() + ") " + this_topics[i].Attributes["Title"].Value;
                                return;
                            }
                        }
                            return;
                    }

                    MainForm.Output = "Fejezetszám egyezés. Feldolgozás indul...";
                    MainForm.Output = "A minta alias fájlban a hozzárendelések száma : " + pattern_aliases.Count;
                                        
                    // végigmegyünk egyesével a minta topikokon, és az annyiadiki topikhoz
                    // elvégezzük ugyanazt a hozzárendelést
                    int aliases_added = 0;

                    Logger.Message("Minta fejezetek száma   : " + pattern_topics.Count.ToString());
                    Logger.Message("Projekt fejezetek száma : " + this_topics.Count.ToString());

                    for (int i = 0; i < pattern_topics.Count; ++i)
                    {
                        string PatternLink = pattern_topics[i].Attributes["Link"].Value;
                        string ThisLink = this_topics[i].Attributes["Link"].Value;
                        Logger.Message(i.ToString() + ". minta fejezet url : " + PatternLink);
                        Logger.Message(i.ToString() + ". projekt fejezet url : " + ThisLink);
                        foreach (XmlNode n in pattern_aliases)
                        {
                            if (n.Attributes["Link"].Value == PatternLink)
                            {
                                // felvesszük thislinkkel az új aliasba
                                XmlElement NewXmlEntry = this_flali.CreateElement("Map");
                                NewXmlEntry.SetAttributeNode("Name", "");
                                NewXmlEntry.Attributes["Name"].Value = n.Attributes["Name"].Value;
                                NewXmlEntry.SetAttributeNode("Link", "");
                                NewXmlEntry.Attributes["Link"].Value = ThisLink;
                                this_flali.LastChild.AppendChild(NewXmlEntry);
                                ++aliases_added;
                                Logger.Message(aliases_added.ToString() + ". Egyezés :");
                                Logger.Message("Minta   : ID:" + n.Attributes["Name"].Value + "||LINK:" + n.Attributes["Link"].Value);
                                Logger.Message("Projekt : ID:" + NewXmlEntry.Attributes["Name"].Value + "||LINK:" + NewXmlEntry.Attributes["Link"].Value);

                            }
                        }
                        Logger.Message("--");
                    }
                    this_flali.Save(new BackupMaker(m_AliasFile.Path, true));

                    MainForm.Output = "Összesen " + aliases_added + " hozzárendelést vettem fel a jelenlegi projektbe.";
                }
            }
        }
    }
}
