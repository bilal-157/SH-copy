namespace jewl
{
    partial class ChartOfAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChartOfAccount));
            this.tabPage = new System.Windows.Forms.TabControl();
            this.tbpAsset1 = new System.Windows.Forms.TabPage();
            this.tvAsset = new System.Windows.Forms.TreeView();
            this.tbpLiability2 = new System.Windows.Forms.TabPage();
            this.tvLiability = new System.Windows.Forms.TreeView();
            this.tbpExpense3 = new System.Windows.Forms.TabPage();
            this.tvExpense = new System.Windows.Forms.TreeView();
            this.tbpRevenue4 = new System.Windows.Forms.TabPage();
            this.tvRevenue = new System.Windows.Forms.TreeView();
            this.tbpCapital5 = new System.Windows.Forms.TabPage();
            this.tvCapital = new System.Windows.Forms.TreeView();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.tabPage.SuspendLayout();
            this.tbpAsset1.SuspendLayout();
            this.tbpLiability2.SuspendLayout();
            this.tbpExpense3.SuspendLayout();
            this.tbpRevenue4.SuspendLayout();
            this.tbpCapital5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage
            // 
            this.tabPage.Controls.Add(this.tbpAsset1);
            this.tabPage.Controls.Add(this.tbpLiability2);
            this.tabPage.Controls.Add(this.tbpExpense3);
            this.tabPage.Controls.Add(this.tbpRevenue4);
            this.tabPage.Controls.Add(this.tbpCapital5);
            this.tabPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage.Location = new System.Drawing.Point(7, 66);
            this.tabPage.Name = "tabPage";
            this.tabPage.Padding = new System.Drawing.Point(10, 3);
            this.tabPage.SelectedIndex = 0;
            this.tabPage.Size = new System.Drawing.Size(487, 328);
            this.tabPage.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabPage.TabIndex = 1;
            this.tabPage.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabPage_DrawItem);
            // 
            // tbpAsset1
            // 
            this.tbpAsset1.Controls.Add(this.tvAsset);
            this.tbpAsset1.ForeColor = System.Drawing.Color.DarkCyan;
            this.tbpAsset1.Location = new System.Drawing.Point(4, 22);
            this.tbpAsset1.Name = "tbpAsset1";
            this.tbpAsset1.Padding = new System.Windows.Forms.Padding(3);
            this.tbpAsset1.Size = new System.Drawing.Size(479, 302);
            this.tbpAsset1.TabIndex = 6;
            this.tbpAsset1.Text = "Asset";
            this.tbpAsset1.UseVisualStyleBackColor = true;
            // 
            // tvAsset
            // 
            this.tvAsset.Location = new System.Drawing.Point(3, 3);
            this.tvAsset.Name = "tvAsset";
            this.tvAsset.Size = new System.Drawing.Size(476, 290);
            this.tvAsset.TabIndex = 0;
            this.tvAsset.DoubleClick += new System.EventHandler(this.tvAsset_DoubleClick);
            // 
            // tbpLiability2
            // 
            this.tbpLiability2.Controls.Add(this.tvLiability);
            this.tbpLiability2.Location = new System.Drawing.Point(4, 22);
            this.tbpLiability2.Name = "tbpLiability2";
            this.tbpLiability2.Padding = new System.Windows.Forms.Padding(3);
            this.tbpLiability2.Size = new System.Drawing.Size(479, 302);
            this.tbpLiability2.TabIndex = 7;
            this.tbpLiability2.Text = "Liability";
            this.tbpLiability2.UseVisualStyleBackColor = true;
            // 
            // tvLiability
            // 
            this.tvLiability.Location = new System.Drawing.Point(7, 3);
            this.tvLiability.Name = "tvLiability";
            this.tvLiability.Size = new System.Drawing.Size(464, 284);
            this.tvLiability.TabIndex = 0;
            this.tvLiability.DoubleClick += new System.EventHandler(this.tvLiability_DoubleClick);
            // 
            // tbpExpense3
            // 
            this.tbpExpense3.Controls.Add(this.tvExpense);
            this.tbpExpense3.Location = new System.Drawing.Point(4, 22);
            this.tbpExpense3.Name = "tbpExpense3";
            this.tbpExpense3.Padding = new System.Windows.Forms.Padding(3);
            this.tbpExpense3.Size = new System.Drawing.Size(479, 302);
            this.tbpExpense3.TabIndex = 8;
            this.tbpExpense3.Text = "Expense";
            this.tbpExpense3.UseVisualStyleBackColor = true;
            // 
            // tvExpense
            // 
            this.tvExpense.Location = new System.Drawing.Point(7, 6);
            this.tvExpense.Name = "tvExpense";
            this.tvExpense.Size = new System.Drawing.Size(464, 279);
            this.tvExpense.TabIndex = 0;
            this.tvExpense.DoubleClick += new System.EventHandler(this.tvExpense_DoubleClick);
            // 
            // tbpRevenue4
            // 
            this.tbpRevenue4.Controls.Add(this.tvRevenue);
            this.tbpRevenue4.Location = new System.Drawing.Point(4, 22);
            this.tbpRevenue4.Name = "tbpRevenue4";
            this.tbpRevenue4.Padding = new System.Windows.Forms.Padding(3);
            this.tbpRevenue4.Size = new System.Drawing.Size(479, 302);
            this.tbpRevenue4.TabIndex = 9;
            this.tbpRevenue4.Text = "Revenue";
            this.tbpRevenue4.UseVisualStyleBackColor = true;
            // 
            // tvRevenue
            // 
            this.tvRevenue.Location = new System.Drawing.Point(8, 7);
            this.tvRevenue.Name = "tvRevenue";
            this.tvRevenue.Size = new System.Drawing.Size(463, 278);
            this.tvRevenue.TabIndex = 0;
            this.tvRevenue.DoubleClick += new System.EventHandler(this.tvRevenue_DoubleClick);
            // 
            // tbpCapital5
            // 
            this.tbpCapital5.Controls.Add(this.tvCapital);
            this.tbpCapital5.Location = new System.Drawing.Point(4, 22);
            this.tbpCapital5.Name = "tbpCapital5";
            this.tbpCapital5.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCapital5.Size = new System.Drawing.Size(479, 302);
            this.tbpCapital5.TabIndex = 10;
            this.tbpCapital5.Text = "Capital";
            this.tbpCapital5.UseVisualStyleBackColor = true;
            // 
            // tvCapital
            // 
            this.tvCapital.Location = new System.Drawing.Point(7, 6);
            this.tvCapital.Name = "tvCapital";
            this.tvCapital.Size = new System.Drawing.Size(464, 279);
            this.tvCapital.TabIndex = 0;
            this.tvCapital.DoubleClick += new System.EventHandler(this.tvCapital_DoubleClick);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(501, 63);
            this.lblTitle.TabIndex = 313;
            this.lblTitle.Text = "Chart of Account";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(192, 403);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 37);
            this.btnExit.TabIndex = 319;
            this.btnExit.Text = " E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // ChartOfAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 450);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabPage);
            this.Name = "ChartOfAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::Chart Of Account:::...";
            this.Load += new System.EventHandler(this.ChartOfAccount_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ChartOfAccount_Paint);
            this.tabPage.ResumeLayout(false);
            this.tbpAsset1.ResumeLayout(false);
            this.tbpLiability2.ResumeLayout(false);
            this.tbpExpense3.ResumeLayout(false);
            this.tbpRevenue4.ResumeLayout(false);
            this.tbpCapital5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPage;
        private System.Windows.Forms.TabPage tbpExpense3;
        private System.Windows.Forms.TabPage tbpRevenue4;
        private System.Windows.Forms.TreeView tvExpense;
        private System.Windows.Forms.TreeView tvRevenue;
        private System.Windows.Forms.TabPage tbpAsset1;
        private System.Windows.Forms.TreeView tvAsset;
        private System.Windows.Forms.TabPage tbpLiability2;
        private System.Windows.Forms.TreeView tvLiability;
        private System.Windows.Forms.TabPage tbpCapital5;
        private System.Windows.Forms.TreeView tvCapital;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Button btnExit;
    }
}