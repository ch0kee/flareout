using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Drawing;

namespace FlareOut
{
    static class Options
    {
        public enum Behavior { None, Hide, Colorize }
        static string m_TOCBackColor;
        public static string TocColor { get { return m_TOCBackColor; } set { m_TOCBackColor = value; } }
        static string m_ALIBackColor;
        public static string AliColor { get { return m_ALIBackColor; } set { m_ALIBackColor = value; } }
        static string[] m_TopicStyles;
        static string[] m_Languages;
        static string m_Language;
        public static string Language { get { return m_Language; } set { m_Language = value; } }
        static int[] m_TopicDepths;

        static string m_OPTIONS_FILE;
            //////////////////////////////////////////////////////////////////////////
        static public void LoadOptions()
        {
            m_OPTIONS_FILE = MainForm.ExeDirectory + "options.xml";
            if (!File.Exists(m_OPTIONS_FILE))
                return;
            XmlDocument oxml = new XmlDocument();
            oxml.Load(m_OPTIONS_FILE);
            XmlNode AboveFlare_Options = GetChildNodeByName(oxml, "AboveFlare_Options");
            XmlNode MSWordProcessing = GetChildNodeByName(AboveFlare_Options, "MSWordProcessing");
            XmlNode ProjectSettings = GetChildNodeByName(AboveFlare_Options, "ProjectSettings");
            LoadProjectSettings(ProjectSettings);
            LoadWordSettings(MSWordProcessing);
            LoadBehaviorSettings(AboveFlare_Options);
        }
        static public void SaveOptions()
        {
            if (!File.Exists(m_OPTIONS_FILE))
                return;
            XmlDocument oxml = new XmlDocument();
            oxml.Load(m_OPTIONS_FILE);
            XmlNode AboveFlare_Options = GetChildNodeByName(oxml, "AboveFlare_Options");
            XmlNode MSWordProcessing = GetChildNodeByName(AboveFlare_Options, "MSWordProcessing");
            XmlNode ProjectSettings = GetChildNodeByName(AboveFlare_Options, "ProjectSettings");
            SaveWordSettings(MSWordProcessing);
            SaveProjectSettings(ProjectSettings);
            SaveBehaviorSettings(AboveFlare_Options);
            oxml.Save(m_OPTIONS_FILE);
        }
        //////////////////////////////////////////////////////////////////////////
        static void LoadBehaviorSettings( XmlNode optionsnode )
        {
            XmlNode TocNode = GetChildNodeByName(optionsnode, "TOC");
            m_TOCBehavior = StrToBehavior(TocNode.Attributes["Mode"].Value);
            m_TOCBackColor = TocNode.Attributes["BackColor"].Value;

            XmlNode AliNode = GetChildNodeByName(optionsnode, "ALI");
            m_ALIBehavior = StrToBehavior(AliNode.Attributes["Mode"].Value);
            m_ALIBackColor = AliNode.Attributes["BackColor"].Value;
        }
        static Behavior m_TOCBehavior;
        public static Behavior TocBehavior { get { return m_TOCBehavior; } set { m_TOCBehavior = value; } }
        static Behavior m_ALIBehavior;
        public static Behavior AliBehavior { get { return m_ALIBehavior; } set { m_ALIBehavior = value; } }
        static void SaveBehaviorSettings(XmlNode optionsnode)
        {
            XmlNode TocNode = GetChildNodeByName(optionsnode, "TOC");
            TocNode.Attributes["Mode"].Value = m_TOCBehavior.ToString();
            TocNode.Attributes["BackColor"].Value = m_TOCBackColor;

            XmlNode AliNode = GetChildNodeByName(optionsnode, "ALI");
            AliNode.Attributes["Mode"].Value = m_ALIBehavior.ToString();
            AliNode.Attributes["BackColor"].Value = m_ALIBackColor;
        }
        //////////////////////////////////////////////////////////////////////////

        static void LoadProjectSettings(XmlNode optionsnode)
        {
            XmlNode ProjDir = GetChildNodeByName(optionsnode, "ProjectFile");
            m_ProjDir = ProjDir.Attributes["Dir"].Value;
        }
        static string m_ProjDir;
        public static string ProjectDir { get { return m_ProjDir; } set { m_ProjDir = value; } }
        static void SaveProjectSettings(XmlNode optionsnode)
        {
            XmlNode ProjDir = GetChildNodeByName(optionsnode, "ProjectFile");
            ProjDir.Attributes["Dir"].Value = m_ProjDir;
        }
        //////////////////////////////////////////////////////////////////////////
        static void LoadWordSettings( XmlNode wordnode )
        {
            m_Language = wordnode.Attributes["Lang"].Value;
            XmlNode TopicStyles = GetChildNodeByName(wordnode, "TopicStyles");
            //////////////////////////////////////////////////////////////////////////
            // nyelvek kigyûjtése
            m_Languages = new string[wordnode.ChildNodes.Count];
            int i = 0;
            for (TopicStyles = GetChildNodeByName(wordnode, "TopicStyles");
                TopicStyles != null; TopicStyles = TopicStyles.NextSibling, ++i)
                m_Languages[i] = TopicStyles.Attributes["Lang"].Value;
            //////////////////////////////////////////////////////////////////////////
            // nyelvspecifikus topik stílusok
            for (TopicStyles = GetChildNodeByName(wordnode, "TopicStyles");
               TopicStyles.Attributes["Lang"].Value != m_Language;
                TopicStyles = TopicStyles.NextSibling) ;

            m_TopicStyles = new string[TopicStyles.ChildNodes.Count];
            m_TopicDepths = new int[TopicStyles.ChildNodes.Count];
            i = 0;
            foreach (XmlNode style in TopicStyles.ChildNodes)
            {
                m_TopicStyles[i] = style.Attributes["Name"].Value;
                m_TopicDepths[i++] = Convert.ToInt16(style.Attributes["Depth"].Value);
            }

        }
        static void SaveWordSettings(XmlNode wordnode)
        {
            wordnode.Attributes["Lang"].Value = m_Language;
            XmlNode TopicStyles = GetChildNodeByName(wordnode, "TopicStyles");
        }
        //////////////////////////////////////////////////////////////////////////
        static public string[] GetHeadingList()
        {
            return m_TopicStyles;
        }
        static public string[] GetLanguageList()
        {
            return m_Languages;
        }
        //////////////////////////////////////////////////////////////////////////
        static public string[] ReadHeadingList(string language)
        {
            XmlDocument oxml = new XmlDocument();
            oxml.Load(m_OPTIONS_FILE);
            XmlNode AboveFlare_Options = GetChildNodeByName(oxml, "AboveFlare_Options");
            XmlNode MSWordProcessing = GetChildNodeByName(AboveFlare_Options, "MSWordProcessing");
            XmlNode TopicStyles = null;
            for (TopicStyles = GetChildNodeByName(MSWordProcessing, "TopicStyles");
                TopicStyles.Attributes["Lang"].Value != language;
                TopicStyles = TopicStyles.NextSibling) ;
            //

            string[] headinglist = new string[TopicStyles.ChildNodes.Count];
            for (int i = 0; i < headinglist.Length; ++i)
                headinglist[i] = TopicStyles.ChildNodes[i].Attributes["Name"].Value;
            return headinglist;

        }
        //////////////////////////////////////////////////////////////////////////
        public static bool IsHeading(string stylename)
        {
            foreach (string s in m_TopicStyles)
                if (s == stylename)
                    return true;
            return false;
        }
        //////////////////////////////////////////////////////////////////////////
        public static int Depth(string stylename)
        {
            int retval = 0;
            foreach (string s in m_TopicStyles)
                if (s == stylename)
                    return m_TopicDepths[retval];
                else
                    ++retval;
            return 0;
        }
        //////////////////////////////////////////////////////////////////////////
        static XmlNode GetChildNodeByName(XmlNode node, string tagname)
        {
            foreach (XmlNode n in node.ChildNodes)
                if (n.Name == tagname)
                    return n;
            return null;
        }
        static Behavior StrToBehavior( string str )
        {
            return (Behavior)Enum.Parse(typeof(Behavior), str, true);
        }

    }
}
