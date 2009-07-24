using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlareOut.Keywords
{
    class DefineLinkerByHeadingStructure : DefineLinker
    {


        public DefineLinkerByHeadingStructure()
        {

        }

        private int FindDepthMismatch()
        {
            for (int i = 0; i < m_SortlistOfRemoteTOC.Count && i < m_SortlistOfLocalTOC.Count; ++i)
                if (m_SortlistOfRemoteTOC.Depth(i) != m_SortlistOfLocalTOC.Depth(i))
                    return i;
            return -1;
        }

        public override bool CheckIntegrity()
        {
            bool lengthOk = m_SortlistOfLocalTOC.Count == m_SortlistOfRemoteTOC.Count;
            if (!lengthOk)
            {
                Logger.Error("Fejezetszám eltérés(Munkalap:"+m_SortlistOfLocalTOC.Count+" | Minta:"+m_SortlistOfRemoteTOC.Count+")");
                return false;
            }
            int depthMatch = FindDepthMismatch();
            if (depthMatch >= 0)
            {
                Logger.Error("Struktúrális eltérés(Munkalap:" + m_SortlistOfLocalTOC.Depth(depthMatch).ToString() + " | "
                                                    +" | Minta:"+m_SortlistOfRemoteTOC.Depth(depthMatch).ToString()+")");
                return false;
            }
            return true;
        }

        public override void MakeAutoLinks()
        {
            int topicsCount = m_SortlistOfRemoteTOC.Count;
            Logger.Message("Fejezetek száma : "+topicsCount);
            for (int i = 0; i < topicsCount; ++i)
            {
                string remoteURLinTOC = m_SortlistOfRemoteTOC[i].Attributes["Link"].Value;
                string localURLinTOC = m_SortlistOfLocalTOC[i].Attributes["Link"].Value;
                foreach (XmlNode ali in m_RemoteAliasList)
                {
                    if (ali.Attributes["Link"].Value == remoteURLinTOC)
                    {
                        XmlElement NewXmlEntry = m_LocalAliases.CreateElement("Map");
                        NewXmlEntry.SetAttributeNode("Name", "");
                        NewXmlEntry.Attributes["Name"].Value = ali.Attributes["Name"].Value;
                        NewXmlEntry.SetAttributeNode("Link", "");
                        NewXmlEntry.Attributes["Link"].Value = localURLinTOC;
                        m_LocalAliases.LastChild.AppendChild(NewXmlEntry);

                    }
                }
            }
            m_LocalAliases.Save(new BackupMaker(FlareProjectMgr.AliasFile.Path, true));
        }
    }
}
