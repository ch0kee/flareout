using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FlareOut.Keywords
{
    abstract class DefineLinker
    {

        protected XmlDocument m_RemoteAliases = new XmlDocument();
        protected XmlDocument m_RemoteTOC = new XmlDocument();

        protected XmlNodeList m_RemoteAliasList;

        protected XmlDocument m_LocalAliases = new XmlDocument();
        protected XmlDocument m_LocalTOC = new XmlDocument();


        public abstract void MakeAutoLinks();
        public abstract bool CheckIntegrity();
        public DefineLinker()
        {
            MessageBox.Show("Add meg a mintaként szolgáló Hivatkozás- és Tartalomjegyzéket!");

            // hivatkozásjegyzék betöltése
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Hivatkozásjegyzék betöltése";
            of.Filter = "Flare hivatkozásjegyzék (*.flali)|*.flali";
            of.CheckFileExists = true;
            if (of.ShowDialog() != DialogResult.OK)
                return;
            m_RemoteAliases.Load(of.FileName);
            // minta betöltése
            of.Title = "Tartalomjegyzék betöltése";
            of.Filter = "Flare tartalomjegyzék (*.fltoc)|*.fltoc";
            if (of.ShowDialog() != DialogResult.OK)
                return;

            m_RemoteTOC.Load(of.FileName);
            // módosítandó betöltése
            m_LocalAliases.Load(FlareProjectMgr.AliasFile.Path);
            m_LocalAliases.LastChild.RemoveAll(); // aliasok törlése

            m_LocalTOC.Load(FlareProjectMgr.TopicFile.Path);

            m_RemoteAliasList = m_RemoteAliases.GetElementsByTagName("Map");

            m_SortlistOfRemoteTOC = new XmlDepthSorter(m_RemoteTOC.LastChild);
            m_SortlistOfLocalTOC = new XmlDepthSorter(m_LocalTOC.LastChild);
        }

        protected readonly XmlDepthSorter m_SortlistOfRemoteTOC;
        protected readonly XmlDepthSorter m_SortlistOfLocalTOC;
    }
}
