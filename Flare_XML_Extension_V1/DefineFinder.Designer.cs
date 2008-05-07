namespace FlareOut
{
    partial class DefineFinder
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
            this.txtTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtTextBox
            // 
            this.txtTextBox.DetectUrls = false;
            this.txtTextBox.HideSelection = false;
            this.txtTextBox.Location = new System.Drawing.Point(13, 13);
            this.txtTextBox.Name = "txtTextBox";
            this.txtTextBox.ReadOnly = true;
            this.txtTextBox.Size = new System.Drawing.Size(653, 460);
            this.txtTextBox.TabIndex = 0;
            this.txtTextBox.Text = "";
            this.txtTextBox.SelectionChanged += new System.EventHandler(this.txtTextBox_SelectionChanged);
            this.txtTextBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtTextBox_MouseMove);
            // 
            // DefineFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 485);
            this.Controls.Add(this.txtTextBox);
            this.Name = "DefineFinder";
            this.Text = "DefineFinder";
            this.Load += new System.EventHandler(this.DefineFinder_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtTextBox;
    }
}