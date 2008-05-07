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
        public FlareProject(string ProjectPath)
        {
            m_ProjectPath = ProjectPath;
            // toc path
            string tocpath = Path.GetDirectoryName(m_ProjectPath)+@"\Project\TOCs";
            string[] files = Directory.GetFiles(tocpath);
            foreach (string file in files)
            {
                if (file.EndsWith(".fltoc"))
                {
                    m_TOCPath = file;
                }
            }
            // alias & define path
            string aliaspath = Path.GetDirectoryName(m_ProjectPath) + @"\Project\Advanced";
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
                MessageBox.Show("Hiba! Ellen�rizd a topik f�jl l�tez�s�t, majd ind�tsd �jra a programot!");
            }
            if (m_AliasPath == null)
            {
                MessageBox.Show("Hiba! Ellen�rizd az alias f�jl l�tez�s�t, majd ind�tsd �jra a programot!");
            }
            if (m_DefinePath == null)
            {
                MessageBox.Show("Hiba! Ellen�rizd a define f�jl l�tez�s�t, majd ind�tsd �jra a programot!");
            }
        }
        public string TOCPath { get { return m_TOCPath; } }
        public string ALIASPath { get { return m_AliasPath; } }
        public string DEFINEPath { get { return m_DefinePath; } }
        // private
        string  m_ProjectPath = null;
        string m_TOCPath = null;
        string m_AliasPath = null;
        string m_DefinePath = null;
    }
    static class FlareProjectMgr
    {
        static bool m_IsLoaded;
        static FlareProject m_Project;
        public static bool LoadFlareProjectFile(string filename)
        {
            OpenFileDialog TocLocator = new OpenFileDialog();
            TocLocator.Title = "Flare projekt bet�lt�se";
            TocLocator.Filter = "Flare projekt f�jl (*.flprj)|*.flprj";
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
            // az elemsz�mok ellen�rz�se a DepthSorterekben van
            //////////////////////////////////////////////////////////////////////////
            MSWordProcessor.EqualizeXMLDepth(Target.LastChild);
            //////////////////////////////////////////////////////////////////////////
            Target.Save(new BackupMaker(m_Project.TOCPath,true));
        }
        //////////////////////////////////////////////////////////////////////////
        // Topic
        static FlareTopicFile m_TopicFile;
        public static void FillTreeWithToc(TreeView treeTOC)
        {
            m_TopicFile = new FlareTopicFile(treeTOC, m_Project.TOCPath);
            m_TopicFile.Tartalomjegyz�kFaFelt�lt�se();
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
            // egyeztet�s
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
                    MainForm.Output = "M�r sorsz�mozott :" + xmlSorter[i].Attributes["Title"].Value;
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
            MainForm.Output = "#define azonos�t�k ki�r�sa HTML Help Compiler kompatibilis form�tumban...";
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (ContextNumber cnum in MainForm.AliasList.Items)
                {
                    string line = "#define "+cnum.DefineName+" "+cnum.DefineNumber;
                    writer.WriteLine(line);
                }
            }
            MainForm.Output = "#define azonos�t�k ki�r�sa k�sz.";
            
        }
    }
}
