namespace jewl
{
    partial class frmTagHistory
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlTagHistory = new System.Windows.Forms.Panel();
            this.cbxTagNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxItemName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlTagHistory.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(257, 153);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 33);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(122, 151);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 33);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlTagHistory
            // 
            this.pnlTagHistory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTagHistory.Controls.Add(this.cbxTagNo);
            this.pnlTagHistory.Controls.Add(this.label4);
            this.pnlTagHistory.Controls.Add(this.cbxItemName);
            this.pnlTagHistory.Controls.Add(this.label3);
            this.pnlTagHistory.Location = new System.Drawing.Point(100, 65);
            this.pnlTagHistory.Name = "pnlTagHistory";
            this.pnlTagHistory.Size = new System.Drawing.Size(245, 70);
            this.pnlTagHistory.TabIndex = 16;
            // 
            // cbxTagNo
            // 
            this.cbxTagNo.FormattingEnabled = true;
            this.cbxTagNo.Location = new System.Drawing.Point(110, 35);
            this.cbxTagNo.Name = "cbxTagNo";
            this.cbxTagNo.Size = new System.Drawing.Size(121, 21);
            this.cbxTagNo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Tag No";
            // 
            // cbxItemName
            // 
            this.cbxItemName.FormattingEnabled = true;
            this.cbxItemName.Location = new System.Drawing.Point(110, 8);
            this.cbxItemName.Name = "cbxItemName";
            this.cbxItemName.Size = new System.Drawing.Size(121, 21);
            this.cbxItemName.TabIndex = 1;
            this.cbxItemName.SelectionChangeCommitted += new System.EventHandler(this.cbxItemName_SelectionChangeCommitted);
            this.cbxItemName.SelectedIndexChanged += new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Item Name";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(499, 48);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Tag History";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTagHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(499, 209);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlTagHistory);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Name = "frmTagHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTagHistory";
            this.Load += new System.EventHandler(this.frmTagHistory_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmTagHistory_Paint);
            this.pnlTagHistory.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel pnlTagHistory;
        private System.Windows.Forms.ComboBox cbxTagNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxItemName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTitle;
    }
}