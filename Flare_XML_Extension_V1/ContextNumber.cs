using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FlareOut
{
    class ContextNumber : ListViewItem
    {
        bool m_Visible;
        public bool Visible { get { return m_Visible; } set { m_Visible = value; } }
        Topic m_TopicToCall;
        public ContextNumber(string[] InputStrings)
        {
            string[] mycell = new string[2];
            DefineName = InputStrings[1]; // define neve
            mycell[0] = InputStrings[2]; mycell[1] = " - ";
            this.SubItems.AddRange(mycell);
        }
        public bool IsReserved { get { return m_TopicToCall != null; } }
        public Topic CalledTopic
        {
            set
            {
                if (value != null) // szöveg beállítása
                {
                    m_TopicToCall = value;
                    SubItems[2].Text = m_TopicToCall.Text;
                    m_TopicToCall.AddContextNumber(this);
                    this.ForeColor = System.Drawing.Color.Gray;
//                     Visible = 
                }
                else
                {
                    SubItems[2].Text = "-";
                    m_TopicToCall.RemoveContextNumber(this);
                    m_TopicToCall = null;
                    this.ForeColor = System.Drawing.Color.Black;
                }
            }
            get
            {
                return m_TopicToCall;
            }
        }
        // Xml node létrehozása belõle
        public XmlNode CreateXmlNode(XmlDocument doc)
        {
            XmlElement NewXmlEntry = doc.CreateElement("Map");
            NewXmlEntry.SetAttributeNode("Name", ""); NewXmlEntry.Attributes[0].Value = DefineName;
            NewXmlEntry.SetAttributeNode("Link", ""); NewXmlEntry.Attributes[1].Value = CalledTopic.PointToUrl;
            return NewXmlEntry;
        }

        // Xml nodedal való összehasonlítás
        public bool IsTheSame(XmlNode node)
        {
            return (this.SubItems[0].Text == node.Attributes["Name"].Value);
        }

        // cellák
        public string DefineName
        {
            set { this.Text = value; }
            get { return this.Text; }
        }
        public string DefineNumber
        {
            set { this.SubItems[1].Text = value; }
            get { return this.SubItems[1].Text; }
        }

    }
}
