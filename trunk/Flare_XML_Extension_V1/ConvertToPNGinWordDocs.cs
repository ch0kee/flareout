using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FlareOut
{
    public partial class ConvertToPNGinWordDocs : Form
    {
        public ConvertToPNGinWordDocs()
        {
            InitializeComponent();
            listDocuments_SelectedIndexChanged(this, new EventArgs());
        }

        private void btnAddDocs_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "Microsoft Office Word dokumentumok (*.doc)|*.doc";
            of.Title = "Dokumentumok betöltése";
            of.DefaultExt = "doc";
            of.CheckFileExists = true;
            of.Multiselect = true;
            if (of.ShowDialog() != DialogResult.OK)
                return;
            foreach (string filename in of.FileNames)
            {
                ListViewItem newdoc = new ListViewItem( Path.GetFileName(filename));
                newdoc.SubItems.Add(filename);
                listDocuments.Items.Add(newdoc);
            }
        }

        private void btnRemoveDocs_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listDocuments.SelectedItems)
            {
                listDocuments.Items.Remove(item);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (listDocuments.Items.Count == 0)
            {
                MainForm.Output = "Nem adott hozzá dokumentumokat!";
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }
            MSWordProcessor.Reset();
            // betöltjük a doksikat progress barral
            MSWordProcessor.StartWord();

            foreach (ListViewItem item in listDocuments.Items)
            {
                string path = item.SubItems[1].Text;
                MainForm.Output = Path.GetFileName(path) + " fejezetek kiolvasása...";
                MSWordProcessor.WriteDocument(path);

                MSWordProcessor.ConvertObjectsToPNG(barProgressBar);

                item.BackColor = Color.LightGreen;
            }

            MSWordProcessor.StopWord();
            Close();
        }

        private void listDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnMoveUp.Enabled = btnMoveDown.Enabled = (listDocuments.SelectedItems.Count == 1);
            btnMoveUp.Enabled = (btnMoveUp.Enabled && listDocuments.SelectedIndices[0] > 0);
            btnMoveDown.Enabled = (btnMoveDown.Enabled && listDocuments.SelectedIndices[0] < listDocuments.Items.Count - 1);
            btnRemoveDocs.Enabled = (listDocuments.SelectedItems.Count >= 1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int selected = listDocuments.SelectedIndices[0];
            ListViewItem next = listDocuments.Items[selected+1].Clone() as ListViewItem;
            listDocuments.Items[selected + 1] = listDocuments.SelectedItems[0].Clone() as ListViewItem;
            listDocuments.Items[selected] = next;
            listDocuments.Items[selected + 1].Selected = true;
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int selected = listDocuments.SelectedIndices[0];
            ListViewItem previous = listDocuments.Items[selected - 1].Clone() as ListViewItem;
            listDocuments.Items[selected - 1] = listDocuments.SelectedItems[0].Clone() as ListViewItem;
            listDocuments.Items[selected] = previous;
            listDocuments.Items[selected - 1].Selected = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MSWordProcessor.Reset();
            Close();
        }


    }
}