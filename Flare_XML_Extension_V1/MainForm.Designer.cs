namespace FlareOut
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader3;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tableOfContentsMûveletekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sorszámozásJavításaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.kibontMindentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.becsukMindentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flareMûveletekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flareProjektBetöltéseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azonosítóMûveletekToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.azonosítókeresõToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beállításokToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtEvents = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.treeTOC = new System.Windows.Forms.TreeView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listDefines = new System.Windows.Forms.ListView();
            this.numAliBackup = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchTOC = new System.Windows.Forms.TextBox();
            this.txtSearchALI = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelSelectedTopic = new System.Windows.Forms.Label();
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAliBackup)).BeginInit();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "#define név";
            columnHeader1.Width = 115;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "Azonosító";
            columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader2.Width = 129;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "Topik név";
            columnHeader3.Width = 268;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tableOfContentsMûveletekToolStripMenuItem,
            this.flareMûveletekToolStripMenuItem,
            this.azonosítóMûveletekToolStripMenuItem,
            this.beállításokToolStripMenuItem,
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(939, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tableOfContentsMûveletekToolStripMenuItem
            // 
            this.tableOfContentsMûveletekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sorszámozásJavításaToolStripMenuItem,
            this.toolStripSeparator1,
            this.kibontMindentToolStripMenuItem,
            this.becsukMindentToolStripMenuItem});
            this.tableOfContentsMûveletekToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tableOfContentsMûveletekToolStripMenuItem.Name = "tableOfContentsMûveletekToolStripMenuItem";
            this.tableOfContentsMûveletekToolStripMenuItem.Size = new System.Drawing.Size(164, 20);
            this.tableOfContentsMûveletekToolStripMenuItem.Text = "Tartalomjegyzék mûveletek";
            // 
            // sorszámozásJavításaToolStripMenuItem
            // 
            this.sorszámozásJavításaToolStripMenuItem.Enabled = false;
            this.sorszámozásJavításaToolStripMenuItem.Name = "sorszámozásJavításaToolStripMenuItem";
            this.sorszámozásJavításaToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.sorszámozásJavításaToolStripMenuItem.Text = "Sorszámozás javítása";
            this.sorszámozásJavításaToolStripMenuItem.Click += new System.EventHandler(this.sorszámozásJavításaToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // kibontMindentToolStripMenuItem
            // 
            this.kibontMindentToolStripMenuItem.Name = "kibontMindentToolStripMenuItem";
            this.kibontMindentToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.kibontMindentToolStripMenuItem.Text = "Kibont mindet";
            this.kibontMindentToolStripMenuItem.Click += new System.EventHandler(this.kibontMindentToolStripMenuItem_Click);
            // 
            // becsukMindentToolStripMenuItem
            // 
            this.becsukMindentToolStripMenuItem.Name = "becsukMindentToolStripMenuItem";
            this.becsukMindentToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.becsukMindentToolStripMenuItem.Text = "Becsuk mindet";
            this.becsukMindentToolStripMenuItem.Click += new System.EventHandler(this.becsukMindentToolStripMenuItem_Click);
            // 
            // flareMûveletekToolStripMenuItem
            // 
            this.flareMûveletekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.flareProjektBetöltéseToolStripMenuItem,
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem});
            this.flareMûveletekToolStripMenuItem.Name = "flareMûveletekToolStripMenuItem";
            this.flareMûveletekToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.flareMûveletekToolStripMenuItem.Text = "Flare mûveletek";
            // 
            // flareProjektBetöltéseToolStripMenuItem
            // 
            this.flareProjektBetöltéseToolStripMenuItem.Name = "flareProjektBetöltéseToolStripMenuItem";
            this.flareProjektBetöltéseToolStripMenuItem.Size = new System.Drawing.Size(354, 22);
            this.flareProjektBetöltéseToolStripMenuItem.Text = "Flare projekt betöltése...";
            this.flareProjektBetöltéseToolStripMenuItem.Click += new System.EventHandler(this.flareProjektBetöltéseToolStripMenuItem_Click);
            // 
            // flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem
            // 
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Enabled = false;
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Name = "flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem";
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Size = new System.Drawing.Size(354, 22);
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Text = "Flare tartalomjegyzék rendezése .DOC fájlok alapján...";
            this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem.Click += new System.EventHandler(this.flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem_Click);
            // 
            // azonosítóMûveletekToolStripMenuItem
            // 
            this.azonosítóMûveletekToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.azonosítókeresõToolStripMenuItem,
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem});
            this.azonosítóMûveletekToolStripMenuItem.Name = "azonosítóMûveletekToolStripMenuItem";
            this.azonosítóMûveletekToolStripMenuItem.Size = new System.Drawing.Size(130, 20);
            this.azonosítóMûveletekToolStripMenuItem.Text = "Azonosító mûveletek";
            // 
            // azonosítókeresõToolStripMenuItem
            // 
            this.azonosítókeresõToolStripMenuItem.Name = "azonosítókeresõToolStripMenuItem";
            this.azonosítókeresõToolStripMenuItem.Size = new System.Drawing.Size(386, 22);
            this.azonosítókeresõToolStripMenuItem.Text = "Azonosító-keresõ...";
            this.azonosítókeresõToolStripMenuItem.Click += new System.EventHandler(this.azonosítókeresõToolStripMenuItem_Click);
            // 
            // beállításokToolStripMenuItem
            // 
            this.beállításokToolStripMenuItem.Name = "beállításokToolStripMenuItem";
            this.beállításokToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.beállításokToolStripMenuItem.Text = "Beállítások";
            this.beállításokToolStripMenuItem.Click += new System.EventHandler(this.beállításokToolStripMenuItem_Click);
            // 
            // aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem
            // 
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem.Name = "aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem";
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem.Size = new System.Drawing.Size(200, 20);
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem.Text = "ALAPÉRTELMEZETT BEÁLLÍTÁSOK";
            this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem.Click += new System.EventHandler(this.aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem_Click);
            // 
            // txtEvents
            // 
            this.txtEvents.BackColor = System.Drawing.Color.LightSteelBlue;
            this.txtEvents.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtEvents.ForeColor = System.Drawing.Color.Black;
            this.txtEvents.Location = new System.Drawing.Point(12, 487);
            this.txtEvents.Name = "txtEvents";
            this.txtEvents.ReadOnly = true;
            this.txtEvents.Size = new System.Drawing.Size(918, 193);
            this.txtEvents.TabIndex = 3;
            this.txtEvents.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 10F, System.Drawing.FontStyle.Underline);
            this.label1.Location = new System.Drawing.Point(15, 470);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Kimenet";
            // 
            // treeTOC
            // 
            this.treeTOC.BackColor = System.Drawing.Color.Snow;
            this.treeTOC.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeTOC.ForeColor = System.Drawing.Color.DarkBlue;
            this.treeTOC.FullRowSelect = true;
            this.treeTOC.HideSelection = false;
            this.treeTOC.Indent = 10;
            this.treeTOC.Location = new System.Drawing.Point(12, 80);
            this.treeTOC.Name = "treeTOC";
            this.treeTOC.Size = new System.Drawing.Size(396, 384);
            this.treeTOC.TabIndex = 5;
            this.treeTOC.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeTOC_NodeMouseDoubleClick);
            this.treeTOC.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeTOC_AfterSelect);
            this.treeTOC.MouseMove += new System.Windows.Forms.MouseEventHandler(this.treeTOC_MouseMove);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 10F, System.Drawing.FontStyle.Underline);
            this.label2.Location = new System.Drawing.Point(15, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tartalomjegyzék";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 10F, System.Drawing.FontStyle.Underline);
            this.label3.Location = new System.Drawing.Point(417, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Azonosítók";
            // 
            // listDefines
            // 
            this.listDefines.BackColor = System.Drawing.Color.Snow;
            this.listDefines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3});
            this.listDefines.ForeColor = System.Drawing.Color.Black;
            this.listDefines.FullRowSelect = true;
            this.listDefines.GridLines = true;
            this.listDefines.HideSelection = false;
            this.listDefines.Location = new System.Drawing.Point(414, 80);
            this.listDefines.Name = "listDefines";
            this.listDefines.OwnerDraw = true;
            this.listDefines.Size = new System.Drawing.Size(516, 384);
            this.listDefines.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listDefines.TabIndex = 9;
            this.listDefines.UseCompatibleStateImageBehavior = false;
            this.listDefines.View = System.Windows.Forms.View.Details;
            this.listDefines.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listDefines_DrawColumnHeader);
            this.listDefines.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listDefines_DrawItem);
            this.listDefines.DoubleClick += new System.EventHandler(this.listDefines_DoubleClick);
            this.listDefines.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listDefines_ColumnClick);
            this.listDefines.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listDefines_MouseMove);
            this.listDefines.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listDefines_DrawSubItem);
            // 
            // numAliBackup
            // 
            this.numAliBackup.Location = new System.Drawing.Point(549, 466);
            this.numAliBackup.Name = "numAliBackup";
            this.numAliBackup.Size = new System.Drawing.Size(33, 20);
            this.numAliBackup.TabIndex = 12;
            this.numAliBackup.ValueChanged += new System.EventHandler(this.numAliBackup_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(423, 468);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Biztonsági mentés minden";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(579, 468);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = ". módosítás után";
            // 
            // txtSearchTOC
            // 
            this.txtSearchTOC.Location = new System.Drawing.Point(308, 59);
            this.txtSearchTOC.Name = "txtSearchTOC";
            this.txtSearchTOC.Size = new System.Drawing.Size(100, 20);
            this.txtSearchTOC.TabIndex = 15;
            this.txtSearchTOC.TextChanged += new System.EventHandler(this.txtSearchTOC_TextChanged);
            this.txtSearchTOC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchTOC_KeyPress);
            // 
            // txtSearchALI
            // 
            this.txtSearchALI.Location = new System.Drawing.Point(830, 59);
            this.txtSearchALI.Name = "txtSearchALI";
            this.txtSearchALI.Size = new System.Drawing.Size(100, 20);
            this.txtSearchALI.TabIndex = 16;
            this.txtSearchALI.TextChanged += new System.EventHandler(this.txtSearchALI_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(776, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Keresés";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(256, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Keresés";
            // 
            // labelSelectedTopic
            // 
            this.labelSelectedTopic.AutoSize = true;
            this.labelSelectedTopic.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelSelectedTopic.ForeColor = System.Drawing.Color.ForestGreen;
            this.labelSelectedTopic.Location = new System.Drawing.Point(18, 31);
            this.labelSelectedTopic.Name = "labelSelectedTopic";
            this.labelSelectedTopic.Size = new System.Drawing.Size(87, 13);
            this.labelSelectedTopic.TabIndex = 19;
            this.labelSelectedTopic.Text = "selectedTopic";
            // 
            // defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem
            // 
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem.Name = "defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem";
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem.Size = new System.Drawing.Size(386, 22);
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem.Text = "#define-ok kiírása HTML Help kompatibilis formátumban...";
            this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem.Click += new System.EventHandler(this.defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 682);
            this.Controls.Add(this.labelSelectedTopic);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSearchALI);
            this.Controls.Add(this.txtSearchTOC);
            this.Controls.Add(this.numAliBackup);
            this.Controls.Add(this.listDefines);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.treeTOC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEvents);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Flare-Out v0.2";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAliBackup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tableOfContentsMûveletekToolStripMenuItem;
        private System.Windows.Forms.RichTextBox txtEvents;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem sorszámozásJavításaToolStripMenuItem;
        private System.Windows.Forms.TreeView treeTOC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listDefines;
        private System.Windows.Forms.ToolStripMenuItem flareMûveletekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flareTOCJavításaABetöltöttTartalomjegyzékAlapjánToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beállításokToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flareProjektBetöltéseToolStripMenuItem;
        private System.Windows.Forms.NumericUpDown numAliBackup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem kibontMindentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem becsukMindentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azonosítóMûveletekToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem azonosítókeresõToolStripMenuItem;
        private System.Windows.Forms.TextBox txtSearchTOC;
        private System.Windows.Forms.TextBox txtSearchALI;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelSelectedTopic;
        private System.Windows.Forms.ToolStripMenuItem aLAPÉRTELMEZETTBEÁLLÍTÁSOKToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defineokKiírásaHTMLHelpKompatibilisFormátumbanToolStripMenuItem;
    }
}

