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
            MainForm.Output = System.IO.Path.GetFileName(m_Define_Path)+" bet�ltve";
        }

        public void LoadContextRelations( FlareTopicFile TocXml )
        {
            XmlDocument document = new XmlDocument();
            document.Load(m_FullPath);
            // inicializ�l�s
            XmlNode tagCatapultAliasFile = document.LastChild;
            XmlNodeList xmlnodes = tagCatapultAliasFile.ChildNodes;
            // fejezetek feldolgoz�sa
            foreach (XmlNode node in xmlnodes)
                if (node.Name == "Map")
                {
                    bool found = false;
                    // kikeress�k a list�b�l
                    foreach (ContextNumber ContNum in m_Defines.Items)
                        if (ContNum.IsTheSame(node))
                        {
                            Topic calledtopic = TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic;
                            if (calledtopic == null)
                            {
                                MainForm.Output = "Hi�nyz� hivatkoz�s !!"
                                    + "#define n�v: " + node.Attributes["Name"].Value
                                    + " hi�nyz� link: " + node.Attributes["Link"].Value;
                            } else
                                ContNum.CalledTopic = TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic;
                            found = true;
                            break;
                        }
                    if (!found)
                    {
                        MainForm.Output = "!! �sszeegyeztethetletlen bejegyz�s !!"
                            + "#define n�v: " + node.Attributes["Name"].Value
                            + "; topik: " + (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic).Text;
                        //
                        if (MessageBox.Show(
"Olyan bejegyz�st tal�ltam az �sszerendel�sek k�z�tt, amihez nincsen azonos�t� a #define f�jlban. Meghagyjam a bejegyz�st ?\n"
+ "#define n�v: " + node.Attributes["Name"].Value
                            + "; topik: " + (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic).Text,
"�sszeegyeztethetetlen bejegyz�s", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
MessageBoxOptions.ServiceNotification) == DialogResult.Yes)
                        {
                            // felv�tel a defineok k�z�
                            string line = "#define " + node.Attributes["Name"].Value + " " + node.Attributes["ResolvedId"].Value;
                            string[] delimiters = { " " };
                            string[] all_cell = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            if (all_cell.Length != 3)
                                continue;
                            ContextNumber NewCNum = new ContextNumber(all_cell);
                            NewCNum.CalledTopic = (TocXml.URL_2_Node(node.Attributes["Link"].Value) as Topic);
                            m_Defines.Items.Add(NewCNum);
                            // topikkapcsolat be�ll�t�sa
                            MainForm.Output = "Bejegyz�s felv�ve";
                        }
                        else
                            MainForm.Output = "Bejegyz�s elt�vol�tva";

                    }
                }
        }

        public void SaveModifiedXml( FlareTopicFile TocXml, bool CreateBackup )
        {
            XmlDocument document = new XmlDocument();
            document.Load(m_FullPath);
            // inicializ�l�s
            XmlNode tagCatapultAliasFile = document.LastChild;
            XmlNodeList xmlnodes = tagCatapultAliasFile.ChildNodes;
            tagCatapultAliasFile.RemoveAll();
            foreach( ContextNumber ContNum in m_Defines.Items)
                if (ContNum.IsReserved) // ki kell �rni
                    tagCatapultAliasFile.AppendChild(ContNum.CreateXmlNode(document));
            // k�sz
            document.Save(new BackupMaker(m_OutFileName, CreateBackup));
        }
    };
}
