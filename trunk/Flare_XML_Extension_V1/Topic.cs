using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace FlareOut
{
    class Topic : TreeNode
    {


        public Topic( string text )
            : base(text)
        {
            SetContextMenu();
        }
        public string PointToUrl
        {
            get
            {
                return Tag as string;
            }
        }

        ContextNumber[] m_ContextNumber = new ContextNumber[0];


        public void AddContextNumber(ContextNumber cnum)
        {
            if (cnum == null || AssignedTo(cnum)) return;   // már hozzá van rendelve
            ContextNumber[] temp = (ContextNumber[])m_ContextNumber.Clone();
            m_ContextNumber = new ContextNumber[temp.Length + 1];
            for (int i = 0; i < temp.Length; ++i)
                    m_ContextNumber[i] = temp[i];
            m_ContextNumber[m_ContextNumber.Length - 1] = cnum;
            if (Options.TocBehavior == Options.Behavior.Colorize)
            {
                this.BackColor = Color.Linen;
                this.ForeColor = Color.LightBlue;
                this.NodeFont = new Font(this.TreeView.Font.Name, this.TreeView.Font.Size, FontStyle.Italic);
            }
            SetContextMenu();
        }

        public void ReleaseAll()
        {
            while (m_ContextNumber.Length > 0)
                RemoveContextNumber(m_ContextNumber[0]);
        }

        public void RemoveContextNumber(ContextNumber cnum)
        {
            ContextNumber[] temp = (ContextNumber[])m_ContextNumber.Clone();
            m_ContextNumber = new ContextNumber[temp.Length - 1];
            for (int i = 0, j = 0; i < temp.Length; ++i)
                if (temp[i] != cnum)
                    m_ContextNumber[j++] = temp[i];
            if (Options.TocBehavior == Options.Behavior.Colorize 
                && m_ContextNumber.Length == 0)
            {
                this.BackColor = Color.Snow;
                this.ForeColor = Color.DarkBlue;
                this.NodeFont = new Font(this.TreeView.Font.Name, this.TreeView.Font.Size, FontStyle.Bold);
            }
            SetContextMenu();
        }
        public bool AssignedTo(ContextNumber cnum)
        {
            foreach (ContextNumber c in m_ContextNumber)
                if (c == cnum)
                    return true;
            return false;
        }
        private void SetContextMenu()
        {
            this.ContextMenu = new ContextMenu();
            for (int i = 0; i < m_ContextNumber.Length; ++i)
                ContextMenu.MenuItems.Add(m_ContextNumber[i].DefineName, new EventHandler(JumpToAlias));
        }

        public static void JumpToAlias(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            ListViewItem ali = MainForm.AliasList.FindItemWithText(mi.Text);
            if (ali == null)
                MainForm.Output = mi.Text + " nem található.";
            else
            {
                MainForm.DeselectAllInList();
                ali.Selected = true;
                ali.EnsureVisible();
            }            
        }
    }

}
