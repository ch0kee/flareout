namespace FlareOut
{
    partial class LoadMSWordDocs
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
            this.listDocuments = new System.Windows.Forms.ListView();
            this.filename = new System.Windows.Forms.ColumnHeader();
            this.path = new System.Windows.Forms.ColumnHeader();
            this.btnAddDocs = new System.Windows.Forms.Button();
            this.btnRemoveDocs = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.barProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // listDocuments
            // 
            this.listDocuments.AllowDrop = true;
            this.listDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.filename,
            this.path});
            this.listDocuments.HideSelection = false;
            this.listDocuments.Location = new System.Drawing.Point(46, 37);
            this.listDocuments.Name = "listDocuments";
            this.listDocuments.Size = new System.Drawing.Size(492, 179);
            this.listDocuments.TabIndex = 0;
            this.listDocuments.UseCompatibleStateImageBehavior = false;
            this.listDocuments.View = System.Windows.Forms.View.Details;
            this.listDocuments.SelectedIndexChanged += new System.EventHandler(this.listDocuments_SelectedIndexChanged);
            // 
            // filename
            // 
            this.filename.Text = "Fájlnév";
            this.filename.Width = 391;
            // 
            // path
            // 
            this.path.Text = "Elérési út";
            this.path.Width = 96;
            // 
            // btnAddDocs
            // 
            this.btnAddDocs.Location = new System.Drawing.Point(46, 222);
            this.btnAddDocs.Name = "btnAddDocs";
            this.btnAddDocs.Size = new System.Drawing.Size(75, 23);
            this.btnAddDocs.TabIndex = 1;
            this.btnAddDocs.Text = "Hozzáad...";
            this.btnAddDocs.UseVisualStyleBackColor = true;
            this.btnAddDocs.Click += new System.EventHandler(this.btnAddDocs_Click);
            // 
            // btnRemoveDocs
            // 
            this.btnRemoveDocs.Enabled = false;
            this.btnRemoveDocs.Location = new System.Drawing.Point(141, 222);
            this.btnRemoveDocs.Name = "btnRemoveDocs";
            this.btnRemoveDocs.Size = new System.Drawing.Size(75, 23);
            this.btnRemoveDocs.TabIndex = 2;
            this.btnRemoveDocs.Text = "Eltávolít...";
            this.btnRemoveDocs.UseVisualStyleBackColor = true;
            this.btnRemoveDocs.Click += new System.EventHandler(this.btnRemoveDocs_Click);
            // 
            // btnDone
            // 
            this.btnDone.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnDone.Location = new System.Drawing.Point(374, 222);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Kész";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(463, 222);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Mégsem";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Enabled = false;
            this.btnMoveUp.Location = new System.Drawing.Point(545, 37);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(32, 23);
            this.btnMoveUp.TabIndex = 5;
            this.btnMoveUp.Text = "Fel";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Enabled = false;
            this.btnMoveDown.Location = new System.Drawing.Point(545, 67);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(32, 23);
            this.btnMoveDown.TabIndex = 6;
            this.btnMoveDown.Text = "Le";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // barProgressBar
            // 
            this.barProgressBar.Location = new System.Drawing.Point(-1, 261);
            this.barProgressBar.Name = "barProgressBar";
            this.barProgressBar.Size = new System.Drawing.Size(607, 23);
            this.barProgressBar.TabIndex = 7;
            // 
            // LoadMSWordDocs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 284);
            this.Controls.Add(this.barProgressBar);
            this.Controls.Add(this.btnMoveDown);
            this.Controls.Add(this.btnMoveUp);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.btnRemoveDocs);
            this.Controls.Add(this.btnAddDocs);
            this.Controls.Add(this.listDocuments);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadMSWordDocs";
            this.Text = "LoadMSWordDocs";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listDocuments;
        private System.Windows.Forms.Button btnAddDocs;
        private System.Windows.Forms.Button btnRemoveDocs;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ColumnHeader filename;
        private System.Windows.Forms.ColumnHeader path;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.ProgressBar barProgressBar;
    }
}