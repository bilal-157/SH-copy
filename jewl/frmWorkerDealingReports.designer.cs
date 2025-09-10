namespace jewl
{
    partial class frmWorkerDealingReports
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
            this.rbtWorker = new System.Windows.Forms.RadioButton();
            this.rbtWorkerSummary = new System.Windows.Forms.RadioButton();
            this.rbtWorkerList = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkStoneDetail = new System.Windows.Forms.CheckBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.chkDateTo = new System.Windows.Forms.CheckBox();
            this.chkDateFrom = new System.Windows.Forms.CheckBox();
            this.cbxWorkerName = new System.Windows.Forms.ComboBox();
            this.chkWorkerName = new System.Windows.Forms.CheckBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbtBillNo = new System.Windows.Forms.RadioButton();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtWorker
            // 
            this.rbtWorker.BackColor = System.Drawing.Color.Maroon;
            this.rbtWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtWorker.ForeColor = System.Drawing.Color.White;
            this.rbtWorker.Location = new System.Drawing.Point(7, 16);
            this.rbtWorker.Name = "rbtWorker";
            this.rbtWorker.Size = new System.Drawing.Size(120, 21);
            this.rbtWorker.TabIndex = 2;
            this.rbtWorker.TabStop = true;
            this.rbtWorker.Text = "Worker";
            this.rbtWorker.UseVisualStyleBackColor = false;
            this.rbtWorker.CheckedChanged += new System.EventHandler(this.rbtWorker_CheckedChanged);
            // 
            // rbtWorkerSummary
            // 
            this.rbtWorkerSummary.BackColor = System.Drawing.Color.Maroon;
            this.rbtWorkerSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtWorkerSummary.ForeColor = System.Drawing.Color.White;
            this.rbtWorkerSummary.Location = new System.Drawing.Point(7, 48);
            this.rbtWorkerSummary.Name = "rbtWorkerSummary";
            this.rbtWorkerSummary.Size = new System.Drawing.Size(120, 21);
            this.rbtWorkerSummary.TabIndex = 3;
            this.rbtWorkerSummary.TabStop = true;
            this.rbtWorkerSummary.Text = "Worker Summary\r\n";
            this.rbtWorkerSummary.UseVisualStyleBackColor = false;
            this.rbtWorkerSummary.CheckedChanged += new System.EventHandler(this.rbtWorkerSummary_CheckedChanged);
            // 
            // rbtWorkerList
            // 
            this.rbtWorkerList.BackColor = System.Drawing.Color.Maroon;
            this.rbtWorkerList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtWorkerList.ForeColor = System.Drawing.Color.White;
            this.rbtWorkerList.Location = new System.Drawing.Point(7, 80);
            this.rbtWorkerList.Name = "rbtWorkerList";
            this.rbtWorkerList.Size = new System.Drawing.Size(120, 21);
            this.rbtWorkerList.TabIndex = 4;
            this.rbtWorkerList.TabStop = true;
            this.rbtWorkerList.Text = "Worker List\r\n";
            this.rbtWorkerList.UseVisualStyleBackColor = false;
            this.rbtWorkerList.CheckedChanged += new System.EventHandler(this.rbtWorkerList_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkStoneDetail);
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Controls.Add(this.chkDateTo);
            this.panel1.Controls.Add(this.chkDateFrom);
            this.panel1.Controls.Add(this.cbxWorkerName);
            this.panel1.Controls.Add(this.chkWorkerName);
            this.panel1.Location = new System.Drawing.Point(149, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(236, 148);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // chkStoneDetail
            // 
            this.chkStoneDetail.Enabled = false;
            this.chkStoneDetail.Location = new System.Drawing.Point(6, 49);
            this.chkStoneDetail.Name = "chkStoneDetail";
            this.chkStoneDetail.Size = new System.Drawing.Size(97, 21);
            this.chkStoneDetail.TabIndex = 15;
            this.chkStoneDetail.Text = "StoneDetail";
            this.chkStoneDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkStoneDetail.UseVisualStyleBackColor = true;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Enabled = false;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(109, 108);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(120, 20);
            this.dtpDateTo.TabIndex = 14;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Enabled = false;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(110, 75);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(120, 20);
            this.dtpDateFrom.TabIndex = 13;
            // 
            // chkDateTo
            // 
            this.chkDateTo.Enabled = false;
            this.chkDateTo.Location = new System.Drawing.Point(6, 108);
            this.chkDateTo.Name = "chkDateTo";
            this.chkDateTo.Size = new System.Drawing.Size(97, 21);
            this.chkDateTo.TabIndex = 12;
            this.chkDateTo.Text = "Date To";
            this.chkDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDateTo.UseVisualStyleBackColor = true;
            this.chkDateTo.CheckedChanged += new System.EventHandler(this.chkDateTo_CheckedChanged);
            // 
            // chkDateFrom
            // 
            this.chkDateFrom.Enabled = false;
            this.chkDateFrom.Location = new System.Drawing.Point(6, 75);
            this.chkDateFrom.Name = "chkDateFrom";
            this.chkDateFrom.Size = new System.Drawing.Size(97, 21);
            this.chkDateFrom.TabIndex = 11;
            this.chkDateFrom.Text = "Date From";
            this.chkDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDateFrom.UseVisualStyleBackColor = true;
            this.chkDateFrom.CheckedChanged += new System.EventHandler(this.chkDateFrom_CheckedChanged_1);
            // 
            // cbxWorkerName
            // 
            this.cbxWorkerName.Enabled = false;
            this.cbxWorkerName.FormattingEnabled = true;
            this.cbxWorkerName.Location = new System.Drawing.Point(109, 17);
            this.cbxWorkerName.Name = "cbxWorkerName";
            this.cbxWorkerName.Size = new System.Drawing.Size(121, 21);
            this.cbxWorkerName.TabIndex = 10;
            // 
            // chkWorkerName
            // 
            this.chkWorkerName.Enabled = false;
            this.chkWorkerName.Location = new System.Drawing.Point(6, 17);
            this.chkWorkerName.Name = "chkWorkerName";
            this.chkWorkerName.Size = new System.Drawing.Size(97, 21);
            this.chkWorkerName.TabIndex = 9;
            this.chkWorkerName.Text = "Worker Name\r\n";
            this.chkWorkerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkWorkerName.UseVisualStyleBackColor = true;
            this.chkWorkerName.CheckedChanged += new System.EventHandler(this.chkWorkerName_CheckedChanged_1);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(94, 256);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(98, 38);
            this.btnPrint.TabIndex = 6;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(210, 256);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 38);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(390, 48);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "Worker Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbtBillNo);
            this.panel2.Controls.Add(this.rbtWorker);
            this.panel2.Controls.Add(this.rbtWorkerSummary);
            this.panel2.Controls.Add(this.rbtWorkerList);
            this.panel2.Location = new System.Drawing.Point(8, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(135, 148);
            this.panel2.TabIndex = 0;
            // 
            // rbtBillNo
            // 
            this.rbtBillNo.BackColor = System.Drawing.Color.Maroon;
            this.rbtBillNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtBillNo.ForeColor = System.Drawing.Color.White;
            this.rbtBillNo.Location = new System.Drawing.Point(7, 112);
            this.rbtBillNo.Name = "rbtBillNo";
            this.rbtBillNo.Size = new System.Drawing.Size(120, 21);
            this.rbtBillNo.TabIndex = 6;
            this.rbtBillNo.TabStop = true;
            this.rbtBillNo.Text = "BillNo";
            this.rbtBillNo.UseVisualStyleBackColor = false;
            this.rbtBillNo.CheckedChanged += new System.EventHandler(this.rbtBillNo_CheckedChanged);
            // 
            // txtBillNo
            // 
            this.txtBillNo.Location = new System.Drawing.Point(258, 213);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(119, 20);
            this.txtBillNo.TabIndex = 10;
            // 
            // frmWorkerDealingReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(390, 305);
            this.Controls.Add(this.txtBillNo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Name = "frmWorkerDealingReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWorkerDealingReports";
            this.Load += new System.EventHandler(this.frmWorkerDealingReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmWorkerDealingReports_Paint);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtWorker;
        private System.Windows.Forms.RadioButton rbtWorkerSummary;
        private System.Windows.Forms.RadioButton rbtWorkerList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.CheckBox chkDateTo;
        private System.Windows.Forms.CheckBox chkDateFrom;
        private System.Windows.Forms.ComboBox cbxWorkerName;
        private System.Windows.Forms.CheckBox chkWorkerName;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.CheckBox chkStoneDetail;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbtBillNo;
        private System.Windows.Forms.TextBox txtBillNo;
    }
}