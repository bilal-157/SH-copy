namespace jewl
{
    partial class frmSplit
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
            this.pnlChwD = new System.Windows.Forms.Panel();
            this.txtCHW = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlChwD.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlChwD
            // 
            this.pnlChwD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChwD.Controls.Add(this.txtCHW);
            this.pnlChwD.Controls.Add(this.btnOk);
            this.pnlChwD.Location = new System.Drawing.Point(5, 8);
            this.pnlChwD.Margin = new System.Windows.Forms.Padding(4);
            this.pnlChwD.Name = "pnlChwD";
            this.pnlChwD.Size = new System.Drawing.Size(266, 128);
            this.pnlChwD.TabIndex = 44;
            // 
            // txtCHW
            // 
            this.txtCHW.Location = new System.Drawing.Point(3, 4);
            this.txtCHW.Margin = new System.Windows.Forms.Padding(4);
            this.txtCHW.Multiline = true;
            this.txtCHW.Name = "txtCHW";
            this.txtCHW.Size = new System.Drawing.Size(259, 87);
            this.txtCHW.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(2, 93);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(260, 28);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 142);
            this.Controls.Add(this.pnlChwD);
            this.Name = "frmSplit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplit";
            this.Load += new System.EventHandler(this.frmSplit_Load);
            this.pnlChwD.ResumeLayout(false);
            this.pnlChwD.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlChwD;
        private System.Windows.Forms.TextBox txtCHW;
        private System.Windows.Forms.Button btnOk;
    }
}