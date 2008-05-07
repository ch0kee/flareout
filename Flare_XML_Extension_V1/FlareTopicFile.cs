using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;

namespace FlareOut
{
    // tartalomjegyz�k
    class FlareTopicFile : FlareFile
    {
        TreeView m_Toc;
        public FlareTopicFile(TreeView TocTree, string OutPath)
            : base(OutPath, OutPath)
        {
            m_Toc = TocTree;
        }
        // egy fejezet felt�lt�se a tartalomjegyz�kbe (rekurz�v)
        void ReadNode(TreeNode ParentTreeNode, XmlNode xmlnode)
        {
            if (xmlnode.Name != "TocEntry")
                return;
            Topic new_node = new Topic(xmlnode.Attributes["Title"].Value);
            if (xmlnode.Attributes["Link"] != null)
                new_node.Tag = xmlnode.Attributes["Link"].Value;
            ParentTreeNode.Nodes.Add(new_node);
            if (xmlnode.HasChildNodes) // fejezet, bontsuk ki
                foreach (XmlNode ChildNode in xmlnode.ChildNodes)
                    ReadNode(ParentTreeNode.LastNode, ChildNode);
        }
        // tartalomjegyz�k felt�lt�se
        public void Tartalomjegyz�kFaFelt�lt�se()
        {
            m_Toc.BeginUpdate();
            //
            m_Toc.Nodes.Clear();
            // xml dokumentum bet�lt�se
            XmlDocument document = new XmlDocument();
            document.Load(m_FullPath);
            // inicializ�l�s
            XmlNode tagCatapultToc = document.LastChild;
            XmlNodeList xmlnodes = tagCatapultToc.ChildNodes;
            // Toc.xml gy�k�r
            m_Toc.Nodes.Add(new TreeNode(FileName));
            // fejezetek feldolgoz�sa
            foreach (XmlNode node in xmlnodes)
                ReadNode(m_Toc.Nodes[0], node);
            //
            m_Toc.EndUpdate();
        }
        // rekurz�v p�szt�z�s
        static Topic FejezetCimNode = null;
        bool FindOwnerOfURL(TreeNode ParentTreeNode, string Url)
        {
            if ((ParentTreeNode.Tag as string) == Url)
            {
                FejezetCimNode = ParentTreeNode as Topic;
                return true;
            }

            if (ParentTreeNode.Nodes.Count > 0) // van m�g benne
                foreach (Topic ChildNode in ParentTreeNode.Nodes)
                    if (FindOwnerOfURL(ChildNode, Url))
                        break;
            return false;
        }

        public TreeNode URL_2_Node(string url)
        {
            // v�gigp�szt�zzuk a nodeokat, �s ha megvan a Tag-ban az url, akkor visszaadjuk a fejezetc�met
            FejezetCimNode = null;
            foreach (TreeNode node in m_Toc.Nodes)
                if (FindOwnerOfURL(m_Toc.Nodes[0], url))
                    break;
            if (FejezetCimNode == null)
                return new TreeNode("!<Ismeretlen>!");
            else
                return FejezetCimNode;
        }


    }

    abstract class DepthSorter
    {
        protected object m_StartingNode;
        protected ArrayList m_DepthOrderedNodes = new ArrayList();
        public int Count { get { return m_DepthOrderedNodes.Count; } }
        abstract protected void AddNode(object o);
        public void Reorder()
        {
            m_DepthOrderedNodes.Clear();
            AddNode(m_StartingNode);
        }
        abstract public int Depth(int index);
    }

    class XmlDepthSorter : DepthSorter
    {
        public XmlDepthSorter(XmlNode node)
        {
            m_StartingNode = node;
            Reorder();
        }
        override protected void AddNode(object o)
        {
            XmlNode node = o as XmlNode;

            if (node.Name == "TocEntry")
                m_DepthOrderedNodes.Add(node);
            foreach (XmlNode n in node.ChildNodes)
            {
                AddNode(n);
            }
        }
        public XmlNode this[int index]
        {
            get
            {
                return m_DepthOrderedNodes[index] as XmlNode;
            }
            set
            {
                m_DepthOrderedNodes[index] = value;
            }
        }
        // bejegyz�s m�lys�g�nek megv�ltoztat�sa
        public void IncDepth(int index)
        {
            XmlNode node = m_DepthOrderedNodes[index] as XmlNode;
            node.PreviousSibling.AppendChild(node.Clone());
            node.ParentNode.RemoveChild(node);
        }
        public void DecDepth(int index)
        {
            XmlNode node = m_DepthOrderedNodes[index] as XmlNode;
            if (node == null) return;
            // r�k�vetkez� nodeokat besz�rjuk gyereknek
            for (XmlNode n = node.NextSibling; n != null; n = n.NextSibling)
                node.AppendChild(n);
            // kitessz�k a sz�l� szintj�re a sz�l� ut�n
            node.ParentNode.ParentNode.InsertAfter(node, node.ParentNode);
            // t�r�lj�k eredeti hely�kr�l a nodeokat
            for (XmlNode n = node; n != null; n = n.NextSibling)
                node.ParentNode.RemoveChild(n);
        }
        // visszaadja, hogy milyen szinten van a bejegyz�s
        override public int Depth(int index)
        {
            XmlNode node = m_DepthOrderedNodes[index] as XmlNode;
            int depth = -2;
            for (node = node.ParentNode; node != null; node = node.ParentNode)
                ++depth;
            return depth;
        }
    }


}
