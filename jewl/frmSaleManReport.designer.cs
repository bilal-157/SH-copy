namespace jewl
{
    partial class frmSaleManReport
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.rbtAdvanceReport = new System.Windows.Forms.RadioButton();
            this.rbtSalesManList = new System.Windows.Forms.RadioButton();
            this.rbtSalaryReport = new System.Windows.Forms.RadioButton();
            this.rbtAttendanceReport = new System.Windows.Forms.RadioButton();
            this.pnlSelection = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cbxSalesMan = new System.Windows.Forms.ComboBox();
            this.chkToDate = new System.Windows.Forms.CheckBox();
            this.chkFromDate = new System.Windows.Forms.CheckBox();
            this.chkSalesMan = new System.Windows.Forms.CheckBox();
            this.pnlSelection.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(575, 59);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Saleman Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(284, 214);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(77, 31);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(155, 214);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(77, 31);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "Veiw";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // rbtAdvanceReport
            // 
            this.rbtAdvanceReport.AutoSize = true;
            this.rbtAdvanceReport.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtAdvanceReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAdvanceReport.Location = new System.Drawing.Point(62, 76);
            this.rbtAdvanceReport.Name = "rbtAdvanceReport";
            this.rbtAdvanceReport.Size = new System.Drawing.Size(123, 17);
            this.rbtAdvanceReport.TabIndex = 2;
            this.rbtAdvanceReport.TabStop = true;
            this.rbtAdvanceReport.Text = "Advance Reports";
            this.rbtAdvanceReport.UseVisualStyleBackColor = false;
            // 
            // rbtSalesManList
            // 
            this.rbtSalesManList.AutoSize = true;
            this.rbtSalesManList.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSalesManList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSalesManList.Location = new System.Drawing.Point(62, 169);
            this.rbtSalesManList.Name = "rbtSalesManList";
            this.rbtSalesManList.Size = new System.Drawing.Size(108, 17);
            this.rbtSalesManList.TabIndex = 3;
            this.rbtSalesManList.TabStop = true;
            this.rbtSalesManList.Text = "Sales Man List";
            this.rbtSalesManList.UseVisualStyleBackColor = false;
            this.rbtSalesManList.CheckedChanged += new System.EventHandler(this.rbtSalesManList_CheckedChanged);
            // 
            // rbtSalaryReport
            // 
            this.rbtSalaryReport.AutoSize = true;
            this.rbtSalaryReport.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSalaryReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSalaryReport.Location = new System.Drawing.Point(62, 138);
            this.rbtSalaryReport.Name = "rbtSalaryReport";
            this.rbtSalaryReport.Size = new System.Drawing.Size(102, 17);
            this.rbtSalaryReport.TabIndex = 4;
            this.rbtSalaryReport.TabStop = true;
            this.rbtSalaryReport.Text = "Salary Report";
            this.rbtSalaryReport.UseVisualStyleBackColor = false;
            // 
            // rbtAttendanceReport
            // 
            this.rbtAttendanceReport.AutoSize = true;
            this.rbtAttendanceReport.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtAttendanceReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAttendanceReport.Location = new System.Drawing.Point(62, 107);
            this.rbtAttendanceReport.Name = "rbtAttendanceReport";
            this.rbtAttendanceReport.Size = new System.Drawing.Size(132, 17);
            this.rbtAttendanceReport.TabIndex = 5;
            this.rbtAttendanceReport.TabStop = true;
            this.rbtAttendanceReport.Text = "Attendance Report";
            this.rbtAttendanceReport.UseVisualStyleBackColor = false;
            // 
            // pnlSelection
            // 
            this.pnlSelection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSelection.Controls.Add(this.dtpTo);
            this.pnlSelection.Controls.Add(this.dtpFrom);
            this.pnlSelection.Controls.Add(this.cbxSalesMan);
            this.pnlSelection.Controls.Add(this.chkToDate);
            this.pnlSelection.Controls.Add(this.chkFromDate);
            this.pnlSelection.Controls.Add(this.chkSalesMan);
            this.pnlSelection.Location = new System.Drawing.Point(234, 64);
            this.pnlSelection.Name = "pnlSelection";
            this.pnlSelection.Size = new System.Drawing.Size(257, 133);
            this.pnlSelection.TabIndex = 6;
            this.pnlSelection.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlSelection_Paint);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yy";
            this.dtpTo.Enabled = false;
            this.dtpTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(106, 89);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(121, 20);
            this.dtpTo.TabIndex = 5;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yy";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(106, 50);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(121, 20);
            this.dtpFrom.TabIndex = 4;
            // 
            // cbxSalesMan
            // 
            this.cbxSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSalesMan.Enabled = false;
            this.cbxSalesMan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSalesMan.FormattingEnabled = true;
            this.cbxSalesMan.Location = new System.Drawing.Point(106, 10);
            this.cbxSalesMan.Name = "cbxSalesMan";
            this.cbxSalesMan.Size = new System.Drawing.Size(121, 21);
            this.cbxSalesMan.TabIndex = 3;
            // 
            // chkToDate
            // 
            this.chkToDate.AutoSize = true;
            this.chkToDate.Location = new System.Drawing.Point(19, 94);
            this.chkToDate.Name = "chkToDate";
            this.chkToDate.Size = new System.Drawing.Size(72, 17);
            this.chkToDate.TabIndex = 2;
            this.chkToDate.Text = "To Date";
            this.chkToDate.UseVisualStyleBackColor = true;
            this.chkToDate.CheckedChanged += new System.EventHandler(this.chkToDate_CheckedChanged);
            // 
            // chkFromDate
            // 
            this.chkFromDate.AutoSize = true;
            this.chkFromDate.Location = new System.Drawing.Point(19, 55);
            this.chkFromDate.Name = "chkFromDate";
            this.chkFromDate.Size = new System.Drawing.Size(84, 17);
            this.chkFromDate.TabIndex = 1;
            this.chkFromDate.Text = "From Date";
            this.chkFromDate.UseVisualStyleBackColor = true;
            this.chkFromDate.CheckedChanged += new System.EventHandler(this.chkFromDate_CheckedChanged);
            // 
            // chkSalesMan
            // 
            this.chkSalesMan.AutoSize = true;
            this.chkSalesMan.Location = new System.Drawing.Point(19, 12);
            this.chkSalesMan.Name = "chkSalesMan";
            this.chkSalesMan.Size = new System.Drawing.Size(81, 17);
            this.chkSalesMan.TabIndex = 0;
            this.chkSalesMan.Text = "SalesMan";
            this.chkSalesMan.UseVisualStyleBackColor = true;
            this.chkSalesMan.CheckedChanged += new System.EventHandler(this.chkSalesMan_CheckedChanged);
            // 
            // frmSaleManReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(575, 266);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.pnlSelection);
            this.Controls.Add(this.rbtAttendanceReport);
            this.Controls.Add(this.rbtSalaryReport);
            this.Controls.Add(this.rbtSalesManList);
            this.Controls.Add(this.rbtAdvanceReport);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmSaleManReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSaleManReport";
            this.Load += new System.EventHandler(this.frmSaleManReport_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSaleManReport_Paint);
            this.pnlSelection.ResumeLayout(false);
            this.pnlSelection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.RadioButton rbtAdvanceReport;
        private System.Windows.Forms.RadioButton rbtSalesManList;
        private System.Windows.Forms.RadioButton rbtSalaryReport;
        private System.Windows.Forms.RadioButton rbtAttendanceReport;
        private System.Windows.Forms.Panel pnlSelection;
        private System.Windows.Forms.CheckBox chkToDate;
        private System.Windows.Forms.CheckBox chkFromDate;
        private System.Windows.Forms.CheckBox chkSalesMan;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cbxSalesMan;
    }
}