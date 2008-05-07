namespace FlareOut
{
    partial class OptionsDialog
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radNoneTOC = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radHideTOC = new System.Windows.Forms.RadioButton();
            this.radColorTOC = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radNoneALI = new System.Windows.Forms.RadioButton();
            this.radHideALI = new System.Windows.Forms.RadioButton();
            this.radColorALI = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveAndClose = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listWordTopics = new System.Windows.Forms.ListView();
            this.label3 = new System.Windows.Forms.Label();
            this.comboWordLanguage = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radNoneTOC);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.radHideTOC);
            this.groupBox1.Controls.Add(this.radColorTOC);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tartalomjegyzék";
            // 
            // radNoneTOC
            // 
            this.radNoneTOC.AutoSize = true;
            this.radNoneTOC.Location = new System.Drawing.Point(12, 96);
            this.radNoneTOC.Name = "radNoneTOC";
            this.radNoneTOC.Size = new System.Drawing.Size(52, 17);
            this.radNoneTOC.TabIndex = 3;
            this.radNoneTOC.TabStop = true;
            this.radNoneTOC.Text = "Nincs";
            this.radNoneTOC.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Összerendelt topikok jelölése";
            // 
            // radHideTOC
            // 
            this.radHideTOC.AutoSize = true;
            this.radHideTOC.Location = new System.Drawing.Point(12, 73);
            this.radHideTOC.Name = "radHideTOC";
            this.radHideTOC.Size = new System.Drawing.Size(48, 17);
            this.radHideTOC.TabIndex = 1;
            this.radHideTOC.TabStop = true;
            this.radHideTOC.Text = "Elrejt";
            this.radHideTOC.UseVisualStyleBackColor = true;
            // 
            // radColorTOC
            // 
            this.radColorTOC.AutoSize = true;
            this.radColorTOC.Location = new System.Drawing.Point(12, 50);
            this.radColorTOC.Name = "radColorTOC";
            this.radColorTOC.Size = new System.Drawing.Size(58, 17);
            this.radColorTOC.TabIndex = 0;
            this.radColorTOC.TabStop = true;
            this.radColorTOC.Text = "Színez";
            this.radColorTOC.UseVisualStyleBackColor = true;
            this.radColorTOC.Click += new System.EventHandler(this.radColorTOC_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radNoneALI);
            this.groupBox2.Controls.Add(this.radHideALI);
            this.groupBox2.Controls.Add(this.radColorALI);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(224, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(178, 149);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Azonosítók";
            // 
            // radNoneALI
            // 
            this.radNoneALI.AutoSize = true;
            this.radNoneALI.Location = new System.Drawing.Point(10, 96);
            this.radNoneALI.Name = "radNoneALI";
            this.radNoneALI.Size = new System.Drawing.Size(52, 17);
            this.radNoneALI.TabIndex = 3;
            this.radNoneALI.TabStop = true;
            this.radNoneALI.Text = "Nincs";
            this.radNoneALI.UseVisualStyleBackColor = true;
            // 
            // radHideALI
            // 
            this.radHideALI.AutoSize = true;
            this.radHideALI.Location = new System.Drawing.Point(10, 73);
            this.radHideALI.Name = "radHideALI";
            this.radHideALI.Size = new System.Drawing.Size(48, 17);
            this.radHideALI.TabIndex = 2;
            this.radHideALI.TabStop = true;
            this.radHideALI.Text = "Elrejt";
            this.radHideALI.UseVisualStyleBackColor = true;
            // 
            // radColorALI
            // 
            this.radColorALI.AutoSize = true;
            this.radColorALI.Location = new System.Drawing.Point(10, 50);
            this.radColorALI.Name = "radColorALI";
            this.radColorALI.Size = new System.Drawing.Size(58, 17);
            this.radColorALI.TabIndex = 1;
            this.radColorALI.TabStop = true;
            this.radColorALI.Text = "Színez";
            this.radColorALI.UseVisualStyleBackColor = true;
            this.radColorALI.Click += new System.EventHandler(this.radColorALI_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Összerendelt azonosítók jelölése";
            // 
            // btnSaveAndClose
            // 
            this.btnSaveAndClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSaveAndClose.Location = new System.Drawing.Point(25, 349);
            this.btnSaveAndClose.Name = "btnSaveAndClose";
            this.btnSaveAndClose.Size = new System.Drawing.Size(95, 23);
            this.btnSaveAndClose.TabIndex = 2;
            this.btnSaveAndClose.Text = "Ment és bezár";
            this.btnSaveAndClose.UseVisualStyleBackColor = true;
            this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.listWordTopics);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.comboWordLanguage);
            this.groupBox3.Location = new System.Drawing.Point(13, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 174);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Microsoft Word";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Új topik formátumok";
            // 
            // listWordTopics
            // 
            this.listWordTopics.Location = new System.Drawing.Point(12, 71);
            this.listWordTopics.Name = "listWordTopics";
            this.listWordTopics.Size = new System.Drawing.Size(182, 97);
            this.listWordTopics.TabIndex = 2;
            this.listWordTopics.UseCompatibleStateImageBehavior = false;
            this.listWordTopics.View = System.Windows.Forms.View.List;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Nyelv";
            // 
            // comboWordLanguage
            // 
            this.comboWordLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboWordLanguage.FormattingEnabled = true;
            this.comboWordLanguage.Location = new System.Drawing.Point(74, 19);
            this.comboWordLanguage.Name = "comboWordLanguage";
            this.comboWordLanguage.Size = new System.Drawing.Size(120, 21);
            this.comboWordLanguage.TabIndex = 0;
            this.comboWordLanguage.SelectedValueChanged += new System.EventHandler(this.comboWordLanguage_SelectedValueChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(318, 349);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Mégsem";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 384);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveAndClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "OptionsDialog";
            this.Load += new System.EventHandler(this.OptionsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radHideTOC;
        private System.Windows.Forms.RadioButton radColorTOC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radNoneTOC;
        private System.Windows.Forms.RadioButton radNoneALI;
        private System.Windows.Forms.RadioButton radHideALI;
        private System.Windows.Forms.RadioButton radColorALI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveAndClose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboWordLanguage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listWordTopics;
        private System.Windows.Forms.Button btnCancel;
    }
}