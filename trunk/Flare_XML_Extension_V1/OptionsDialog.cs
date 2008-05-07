using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FlareOut
{
    public partial class OptionsDialog : Form
    {

        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            ShowToUser();
        }

        private void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            SaveAll();
            Options.SaveOptions();
            Options.LoadOptions();
            Close();
        }

        private void SaveAll()
        {
            if (radColorALI.Checked)
                Options.AliBehavior = Options.Behavior.Colorize;
            else if (radHideALI.Checked)
                Options.AliBehavior = Options.Behavior.Hide;
            else
                Options.AliBehavior = Options.Behavior.None;
//////////////////////////////////////////////////////////////////////////
            if (radColorTOC.Checked)
                Options.TocBehavior = Options.Behavior.Colorize;
            else if (radHideTOC.Checked)
                Options.TocBehavior = Options.Behavior.Hide;
            else
                Options.TocBehavior = Options.Behavior.None;
//////////////////////////////////////////////////////////////////////////
            Options.Language = m_Language;
            Options.AliColor = m_ALIcolor;
            Options.TocColor = m_TOCcolor;
        }

        private void ShowToUser()
        {
            radColorALI.Checked = radHideALI.Checked = radNoneALI.Checked = false;
            radColorTOC.Checked = radHideTOC.Checked = radNoneTOC.Checked = false;
            switch(Options.AliBehavior)
            {
                case Options.Behavior.Colorize:
                    radColorALI.Checked = true;
                    break;
                case Options.Behavior.Hide:
                    radHideALI.Checked = true;
                    break;
                case Options.Behavior.None:
                    radNoneALI.Checked = true;
                    break;
            }
            switch (Options.TocBehavior)
            {
                case Options.Behavior.Colorize:
                    radColorTOC.Checked = true;
                    break;
                case Options.Behavior.Hide:
                    radHideTOC.Checked = true;
                    break;
                case Options.Behavior.None:
                    radNoneTOC.Checked = true;
                    break;
            }
            m_ALIcolor = Options.AliColor;
            m_TOCcolor = Options.TocColor;
            comboWordLanguage.Items.Clear();
            foreach (string lang in Options.GetLanguageList())
            {
                int new_index = comboWordLanguage.Items.Add(lang);
                if (lang == Options.Language)
                    comboWordLanguage.SelectedIndex = new_index;
            }

            listWordTopics.Items.Clear();
            foreach (string heading in Options.GetHeadingList())
                listWordTopics.Items.Add(heading);
            
        }
        string m_TOCcolor;
        string m_ALIcolor;
        string m_Language;
        private void radColorTOC_Click(object sender, EventArgs e)
        {
/*
            ColorDialog colordlg = new ColorDialog();
            colordlg.AllowFullOpen = false;
            if (colordlg.ShowDialog() == DialogResult.OK)
                m_TOCcolor = colordlg.Color.Name;
*/
        }

        private void radColorALI_Click(object sender, EventArgs e)
        {
/*
            ColorDialog colordlg = new ColorDialog();
            colordlg.SolidColorOnly = true;
            colordlg.AllowFullOpen = false;
            if (colordlg.ShowDialog() == DialogResult.OK)
                m_ALIcolor = colordlg.Color.Name;
*/
        }

        private void comboWordLanguage_SelectedValueChanged(object sender, EventArgs e)
        {
            m_Language = comboWordLanguage.SelectedItem as string;
            listWordTopics.Items.Clear();
            string[] headings = Options.ReadHeadingList(m_Language);
            foreach (string heading in headings)
                listWordTopics.Items.Add(heading);
        }
    }
}