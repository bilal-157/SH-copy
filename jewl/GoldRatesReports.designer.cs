namespace jewl
{
    partial class GoldRatesReports
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
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlDateWise = new System.Windows.Forms.Panel();
            this.rbtDateRange = new System.Windows.Forms.RadioButton();
            this.rbtDate = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.pnlDateWise.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.CustomFormat = "dd-MM-yy";
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFromDate.Location = new System.Drawing.Point(79, 7);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpFromDate.TabIndex = 14;
            // 
            // dtpToDate
            // 
            this.dtpToDate.CustomFormat = "dd-MM-yy";
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpToDate.Location = new System.Drawing.Point(79, 40);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpToDate.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 45);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "From";
            // 
            // pnlDateWise
            // 
            this.pnlDateWise.Controls.Add(this.dtpFromDate);
            this.pnlDateWise.Controls.Add(this.dtpToDate);
            this.pnlDateWise.Controls.Add(this.label5);
            this.pnlDateWise.Controls.Add(this.label4);
            this.pnlDateWise.Location = new System.Drawing.Point(220, 107);
            this.pnlDateWise.Name = "pnlDateWise";
            this.pnlDateWise.Size = new System.Drawing.Size(192, 78);
            this.pnlDateWise.TabIndex = 32;
            this.pnlDateWise.Visible = false;
            // 
            // rbtDateRange
            // 
            this.rbtDateRange.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDateRange.Font = new System.Drawing.Font("Verdana", 7.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDateRange.Location = new System.Drawing.Point(12, 129);
            this.rbtDateRange.Name = "rbtDateRange";
            this.rbtDateRange.Size = new System.Drawing.Size(198, 29);
            this.rbtDateRange.TabIndex = 28;
            this.rbtDateRange.TabStop = true;
            this.rbtDateRange.Text = "Date Range";
            this.rbtDateRange.UseVisualStyleBackColor = false;
            this.rbtDateRange.CheckedChanged += new System.EventHandler(this.rbtDateRange_CheckedChanged);
            // 
            // rbtDate
            // 
            this.rbtDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDate.Font = new System.Drawing.Font("Verdana", 7.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDate.Location = new System.Drawing.Point(12, 89);
            this.rbtDate.Name = "rbtDate";
            this.rbtDate.Size = new System.Drawing.Size(198, 29);
            this.rbtDate.TabIndex = 27;
            this.rbtDate.TabStop = true;
            this.rbtDate.Text = "Date";
            this.rbtDate.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(220, 206);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(63, 29);
            this.btnExit.TabIndex = 26;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Location = new System.Drawing.Point(96, 206);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(63, 29);
            this.btnView.TabIndex = 25;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(421, 53);
            this.lblTitle.TabIndex = 24;
            this.lblTitle.Text = "Gold Rate Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MM-yy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(285, 79);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(96, 20);
            this.dtpDate.TabIndex = 33;
            // 
            // frmGoldRatesReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(421, 257);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.pnlDateWise);
            this.Controls.Add(this.rbtDateRange);
            this.Controls.Add(this.rbtDate);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmGoldRatesReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGoldRatesReports";
            this.Load += new System.EventHandler(this.frmGoldRatesReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmGoldRatesReports_Paint);
            this.pnlDateWise.ResumeLayout(false);
            this.pnlDateWise.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlDateWise;
        private System.Windows.Forms.RadioButton rbtDateRange;
        private System.Windows.Forms.RadioButton rbtDate;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}