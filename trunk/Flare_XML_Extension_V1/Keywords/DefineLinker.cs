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
            MessageBox.Show("Add meg a mintak�nt szolg�l� Hivatkoz�s- �s Tartalomjegyz�ket!");

            // hivatkoz�sjegyz�k bet�lt�se
            OpenFileDialog of = new OpenFileDialog();
            of.Title = "Hivatkoz�sjegyz�k bet�lt�se";
            of.Filter = "Flare hivatkoz�sjegyz�k (*.flali)|*.flali";
            of.CheckFileExists = true;
            if (of.ShowDialog() != DialogResult.OK)
                return;
            m_RemoteAliases.Load(of.FileName);
            // minta bet�lt�se
            of.Title = "Tartalomjegyz�k bet�lt�se";
            of.Filter = "Flare tartalomjegyz�k (*.fltoc)|*.fltoc";
            if (of.ShowDialog() != DialogResult.OK)
                return;

            m_RemoteTOC.Load(of.FileName);
            // m�dos�tand� bet�lt�se
            m_LocalAliases.Load(FlareProjectMgr.AliasFile.Path);
            m_LocalAliases.LastChild.RemoveAll(); // aliasok t�rl�se

            m_LocalTOC.Load(FlareProjectMgr.TopicFile.Path);

            m_RemoteAliasList = m_RemoteAliases.GetElementsByTagName("Map");

            m_SortlistOfRemoteTOC = new XmlDepthSorter(m_RemoteTOC.LastChild);
            m_SortlistOfLocalTOC = new XmlDepthSorter(m_LocalTOC.LastChild);
        }

        protected readonly XmlDepthSorter m_SortlistOfRemoteTOC;
        protected readonly XmlDepthSorter m_SortlistOfLocalTOC;
    }
}
