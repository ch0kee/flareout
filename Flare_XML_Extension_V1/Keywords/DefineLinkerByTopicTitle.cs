using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FlareOut.Keywords
{
    class DefineLinkerByTopicTitle : DefineLinker
    {
        public override bool CheckIntegrity()
        {
            // nincs integrit�s teszt itt, csak a c�mek egyez�s�t vizsg�ljuk
            return true;
        }

        public override void MakeAutoLinks()
        {
            int topicsCount = m_SortlistOfRemoteTOC.Count;
            Logger.Message("Fejezetek sz�ma : " + topicsCount);
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
