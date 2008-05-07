using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlareOut
{
   
    public partial class DefineFinder : Form
    {
        string m_FileName;
        public DefineFinder(string filename)
        {
            InitializeComponent();
            m_FileName = filename;
        }


        private void DefineFinder_Load(object sender, EventArgs e)
        {
        }
        public void LoadFile()
        {
            txtTextBox.LoadFile(m_FileName, RichTextBoxStreamType.PlainText);
        }

        private bool selecting = false;
        private void txtTextBox_SelectionChanged(object sender, EventArgs e)
        {
            if (selecting)
                return;
            // kijelöljük az egész szót
            selecting = true;
            int selstart = txtTextBox.SelectionStart-1;
            // kijelölés balra
            for (; selstart >= 0 && !char.IsWhiteSpace(txtTextBox.Text[selstart]); --selstart);
            // kijelölés jobbra
            int sellength = txtTextBox.SelectionLength + (txtTextBox.SelectionStart - selstart);
            int selend = selstart + sellength;
            for (; selend < txtTextBox.Text.Length && !char.IsWhiteSpace(txtTextBox.Text[selend]); ++selend) ;

            txtTextBox.Select(selstart + 1, selend-1-selstart);
            selecting = false;
            if (MainForm.AliasList.Items.Count == 0)
                return;
            // aktuális szelekció kikeresése a listából
            string searchstr = txtTextBox.SelectedText.Trim();
            ContextNumber cnum = MainForm.AliasList.FindItemWithText(searchstr, true,0, false) as ContextNumber;
            if (cnum != null && searchstr.Length > 0)
            {
                txtTextBox.ContextMenu = new ContextMenu();

                MainForm.DeselectAllInList();
                cnum.Selected = true;
                cnum.EnsureVisible();
                
                txtTextBox.ContextMenu.MenuItems.Add("Összekapcsol", new EventHandler(MainForm.ConnectSelections));
            }
            else
                txtTextBox.ContextMenu = null;

        }

        private void txtTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            Focus();
        }

    }
}