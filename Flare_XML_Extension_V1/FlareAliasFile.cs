using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace FlareOut
{
    // alias
    class FlareAliasFile : FlareFile
    {
        ListView m_Defines;
        string m_Define_Path;
        public FlareAliasFile(ListView DefineList, string OutPath, string definepath)
            : base(OutPath, OutPath)
        {
            m_Defines = DefineList;
            m_Define_Path = definepath;
        }


        public void FillDefineList()
        {
            m_Defines.Items.Clear();
            using (StreamReader reader = new StreamReader(m_Define_Path)) {
                string line;
                while ((line = reader.ReadLine()) != null) {
                    line.Trim();
                    if (line.Length == 0)
                        continue;

                    string[] delimiters = { " ", "\t" };
                    string[] all_cell = line.Split( delimiters ,StringSplitOptions.RemoveEmptyEntries);
                    if (all_cell.Length < 3)
                        continue;
                    all_cell = new string[3] { all_cell[0], all_cell[1], all_cell[2] };
                    m_Defines.Items.Add(new ContextNumber(all_cell));
                }
            }
            MainForm.Output = System.IO.Path.GetFileName(m_Define_Path)+" betöltve";
        }

        public void LoadContextRelations( FlareTopicFile TocXml )
        {
            XmlDocument document = new XmlDocument();
            document.Load(m_FullPath);
            // inicializálás
            XmlNode tagCatapultAliasFile = document.LastChild;
            XmlNodeList xmlnodes = tagCatapultAliasFile.ChildNodes;
            // fejezetek feldolgozása
            foreach (XmlNode node in xmlnodes)
                if (node.Name == "Map")
                {
                    bool found = false;
                    // kikeressük a listából
                    foreach (ContextNumber ContNum in m_Defines.Items)
                        if (ContNum.IsTheSame(node))
                        {
                            Topic calledtopic = TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic;
                            if (calledtopic == null)
                            {
                                MainForm.Output = "Hiányzó hivatkozás !!"
                                    + "#define név: " + node.Attributes["Name"].Value
                                    + " hiányzó link: " + node.Attributes["Link"].Value;
                            } else
                                ContNum.CalledTopic = TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic;
                            found = true;
                            break;
                        }
                    if (!found)
                    {
                        MainForm.Output = "!! Összeegyeztethetletlen bejegyzés !!"
                            + "#define név: " + node.Attributes["Name"].Value
                            + "; topik: " + (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic).Text;
                        //
                        if (MessageBox.Show(
"Olyan bejegyzést találtam az Összerendelések között, amihez nincsen azonosító a #define fájlban. Meghagyjam a bejegyzést ?\n"
+ "#define név: " + node.Attributes["Name"].Value
                            + "; topik: " + (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic).Text,
"Összeegyeztethetetlen bejegyzés", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
MessageBoxOptions.ServiceNotification) == DialogResult.Yes)
                        {
                            // felvétel a defineok közé
                            string line = "#define " + node.Attributes["Name"].Value + " " + node.Attributes["ResolvedId"].Value;
                            string[] delimiters = { " " };
                            string[] all_cell = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            if (all_cell.Length != 3)
                                continue;
                            ContextNumber NewCNum = new ContextNumber(all_cell);
                            NewCNum.CalledTopic = (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic);
                            m_Defines.Items.Add(NewCNum);
                            // topikkapcsolat beállítása
                            MainForm.Output = "Bejegyzés felvéve";
                        }
                        else
                            MainForm.Output = "Bejegyzés eltávolítva";

                    }
                }
        }

        public void SaveModifiedXml( FlareTopicFile TocXml, bool CreateBackup )
        {
            XmlDocument document = new XmlDocument();
            document.Load(m_FullPath);
            // inicializálás
            XmlNode tagCatapultAliasFile = document.LastChild;
            XmlNodeList xmlnodes = tagCatapultAliasFile.ChildNodes;
            tagCatapultAliasFile.RemoveAll();
            foreach( ContextNumber ContNum in m_Defines.Items)
                if (ContNum.IsReserved) // ki kell írni
                    tagCatapultAliasFile.AppendChild(ContNum.CreateXmlNode(document));
            // kész
            document.Save(new BackupMaker(m_OutFileName, CreateBackup));
        }
    };
}
