using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace FlareOut
{
    public partial class MainForm : Form
    {
        private ListViewColumnSorter lvwColumnSorter;
        FlareFile[] ArrayOfXMLFiles = new FlareFile[2];
        static RichTextBox m_Display;
        static ListView m_AliasList;
        static TreeView m_TopicView;
        static MainForm m_ActForm;
        public static string Output { get { return m_Display.Text; } set { m_Display.Text = value + "\r\n" + m_Display.Text; } }
        public static ListView AliasList { get { return m_AliasList; } }
        public static TreeView TopicView { get { return m_TopicView; } }
        public static string ExeDirectory { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        const string m_NO_SELECTED_TOPIC = "Nincs kiválasztott fejezet!";
        public MainForm()
        {
            InitializeComponent();
            m_ActForm = this;
            m_Display = txtEvents;
            m_AliasList = listDefines;
            m_TopicView = treeTOC;
            lvwColumnSorter = new ListViewColumnSorter();
            this.listDefines.ListViewItemSorter = lvwColumnSorter;
        }

        int m_AliBackupCtr = 0;


        private void sorszámozásJavításaToolStripMenuItem_Click(object sender, EventArgs e)
        {

            treeTOC.BeginUpdate();
            FlareProjectMgr.RepairNumbering();
            FlareProjectMgr.FillTreeWithToc(treeTOC);
            treeTOC.EndUpdate();

        }

        private void listDefines_DoubleClick(object sender, EventArgs e)
        {
            AssignSelections();
        }

        public static void ConnectSelections(object sender, EventArgs e)
        {
            AssignSelections();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ensure that the view is set to show details.
            listDefines.View = View.Details;
            listDefines.ContextMenu = new ContextMenu();
            listDefines.ContextMenu.MenuItems.Add(new MenuItem("Felold összekapcsolást", RemoveContextNumber));
            labelSelectedTopic.Text = m_NO_SELECTED_TOPIC;
            Options.LoadOptions();
            LoadProject(Options.ProjectDir);
            Options.SaveOptions();
        }
        private void RemoveContextNumber(object sender, EventArgs e)
        {
            // aktuális kiválasztás semlegesítése
            if (listDefines.SelectedItems.Count >= 1)
            {
                foreach ( ListViewItem ListItem in listDefines.SelectedItems)
                {
                    if (!(ListItem is ContextNumber) || !(ListItem as ContextNumber).IsReserved)
                        continue;
                    ContextNumber SelectedCNum = ListItem as ContextNumber;
                    MainForm.Output = "Feloldás : "
                    + SelectedCNum.CalledTopic.Text
                    + " <--X--> " + SelectedCNum.DefineName
                    + ":" + SelectedCNum.DefineNumber;
                    SelectedCNum.CalledTopic = null;
                }
                SaveOrBackupAliases();
            }
        }

        //////////////////////////////////////////////////////////////////////////
        // Options
        //////////////////////////////////////////////////////////////////////////
        private void beállításokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OptionsDialog options = new OptionsDialog();
            if (options.ShowDialog() == DialogResult.OK)
                ;// frissítés
        }
        //////////////////////////////////////////////////////////////////////////
        // Flare
        //////////////////////////////////////////////////////////////////////////


        private void flareProjektBetöltéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadProject("");
        }

        private void LoadProject(string filename)
        {
            treeTOC.BeginUpdate();
            listDefines.BeginUpdate();
            if (!FlareProjectMgr.LoadFlareProjectFile(filename))
                return;
            // projectTree betöltése
            FlareProjectMgr.FillTreeWithToc(treeTOC);
            // aliasok betöltése
            FlareProjectMgr.FillListWithAliases(listDefines);
            listDefines.Sort();
            treeTOC.EndUpdate();
            listDefines.EndUpdate();
            flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Enabled = true;
            flareProjektBetöltéseToolStripMenuItem.Enabled = false;
            sorszámozásJavításaToolStripMenuItem.Enabled = true;
            // define finderek megnyitása
        }

        private void flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadMSWordDocs loaddocsform = new LoadMSWordDocs();
            if (loaddocsform.ShowDialog() == DialogResult.OK)
            {
                treeTOC.BeginUpdate();
                FlareProjectMgr.RestructureTOCwithMSWORD();
                // projectTree betöltése
                FlareProjectMgr.FillTreeWithToc(treeTOC);
                treeTOC.EndUpdate();
            }
        }

        private void treeTOC_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            AssignSelections();
        }

        private static void AssignSelections()
        {
            if (AliasList.SelectedItems.Count >= 1 && TopicView.SelectedNode is Topic)
            {
                foreach (ListViewItem ListItem in AliasList.SelectedItems)
                {
                    if (!(ListItem is ContextNumber))
                        continue;
                    ContextNumber SelectedCNum = ListItem as ContextNumber;
                    SelectedCNum.CalledTopic = TopicView.SelectedNode as Topic;
                    MainForm.Output = "Összerendelés : "
                        + (TopicView.SelectedNode as Topic).Text
                        + " <--=--> " + SelectedCNum.DefineName
                        + ":" + SelectedCNum.DefineNumber;
                }
                SaveOrBackupAliases();
            }
        }

        private void listDefines_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                    lvwColumnSorter.Order = SortOrder.Descending;
                else
                    lvwColumnSorter.Order = SortOrder.Ascending;
            }
            else
            {
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            listDefines.Sort();
        }



        private void kibontMindentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeTOC.ExpandAll();
        }

        private void becsukMindentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeTOC.CollapseAll();
        }

        private void numAliBackup_ValueChanged(object sender, EventArgs e)
        {
            m_AliBackupCtr = 0;
        }

        private static void SaveOrBackupAliases()
        {
            if (m_ActForm.numAliBackup.Value == 0)
                FlareProjectMgr.SaveAliases(false);
            else if (++m_ActForm.m_AliBackupCtr >= m_ActForm.numAliBackup.Value)
                {
                    FlareProjectMgr.SaveAliases(true);
                    m_ActForm.m_AliBackupCtr = 0;
                    //create backup and reset counter
                }
            else // menteni kell, de még nem értük el a lépésközt
            {
                FlareProjectMgr.SaveAliases(false);

            }
        }

        private void azonosítókeresõToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Azonosító-keresõ file megnyitása";
            ofd.Filter = "Azonosító-keresõ file (*.*)|*.*";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
                OpenDefineFinder(ofd.FileName);
        }
        private void OpenDefineFinder(string filename)
        {
            if (!File.Exists(filename))
                return;
            DefineFinder df = new DefineFinder(filename);
            df.Show();
            df.LoadFile();
        }
        public static void DeselectAllInList()
        {
            foreach (ListViewItem i in AliasList.Items)
                i.Selected = false;
        }

        private void txtSearchALI_TextChanged(object sender, EventArgs e)
        {
            DeselectAllInList();
            foreach (ListViewItem it in AliasList.Items)
            {
                if (it.Text.ToLower().Contains(txtSearchALI.Text.ToLower()))
                {
                    it.Selected = true;
                    it.EnsureVisible();
                    break;
                }
            }
        }

        private void txtSearchTOC_TextChanged(object sender, EventArgs e)
        {
            TreeNode found = FindFirstNodeAfter(treeTOC.Nodes, null, txtSearchTOC.Text);
            SearchingDone();
            if (found != null)
            {
                treeTOC.SelectedNode = found;
            }

        }
        private void txtSearchTOC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')  // enter
            {
                if (treeTOC.SelectedNode != null)
                {
                    TreeNode found = FindFirstNodeAfter(treeTOC.Nodes, treeTOC.SelectedNode, txtSearchTOC.Text);
                    SearchingDone();
                    if (found != null)
                    {
                        treeTOC.SelectedNode = found;
                    }
                }

            }
        }

        public void SearchingDone() { m_StartLooking = false; }
        private bool m_StartLooking = false;
        private TreeNode FindFirstNodeAfter(TreeNodeCollection nodes, TreeNode from, string text)
        {
            if (nodes == null || nodes.Count == 0)
                return null;
            if (from == null)
                m_StartLooking = true;

            foreach (TreeNode n in nodes)
            {
                if (m_StartLooking && n.Text.ToLower().Contains(text.ToLower()))
                    return n;
                if (n == from)
                    m_StartLooking = true;
                TreeNode retval = FindFirstNodeAfter(n.Nodes, from, text);
                if (retval != null)
                    return retval;
            }
            return null;
        }


        private void listDefines_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.Item.BackColor = (e.Item.Selected) ? Color.Blue : Color.Transparent;
            e.Item.ForeColor = (e.Item.Selected) ? Color.White : Color.Black;
            e.DrawBackground();
            e.DrawText();
        }

        private void listDefines_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                if (!e.Item.Selected)
                     e.SubItem.ForeColor = Color.Black;
                else
                    e.SubItem.ForeColor = Color.White;
                e.DrawText();
            }
        }

        private void listDefines_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void treeTOC_AfterSelect(object sender, TreeViewEventArgs e)
        {
            labelSelectedTopic.Text = e.Node.Text;
            if (treeTOC.SelectedNode == null)
                labelSelectedTopic.Text = m_NO_SELECTED_TOPIC;
        }

        private void listDefines_MouseMove(object sender, MouseEventArgs e)
        {
            listDefines.Focus();
        }

        private void treeTOC_MouseMove(object sender, MouseEventArgs e)
        {
            treeTOC.Focus();
        }

        private void aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options.ProjectDir = "";
            Options.SaveOptions();
        }

        private void defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "#define azonosítók kimentése";
            sd.Filter = "Új #define fájl (*.h)|*.h";
            if (sd.ShowDialog() == DialogResult.OK)
                FlareProjectMgr.SaveDefinesTo(sd.FileName);
        }



    }

}

            