namespace jewl
{
    partial class AccountReports
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbtAccountLedger = new System.Windows.Forms.RadioButton();
            this.rbtChartOfAccount = new System.Windows.Forms.RadioButton();
            this.rbtProfiyAndLoss = new System.Windows.Forms.RadioButton();
            this.rbtTrialBalance = new System.Windows.Forms.RadioButton();
            this.rbtComprehensiveLedger = new System.Windows.Forms.RadioButton();
            this.rbtHeadReport = new System.Windows.Forms.RadioButton();
            this.rbtDayBook = new System.Windows.Forms.RadioButton();
            this.rbtParentReport = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxChildAccount = new System.Windows.Forms.ComboBox();
            this.cbxParentName = new System.Windows.Forms.ComboBox();
            this.cbxHeadAccount = new System.Windows.Forms.ComboBox();
            this.lblParentName = new System.Windows.Forms.Label();
            this.lblChildAccount = new System.Windows.Forms.Label();
            this.lblHeadAccount = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.rbtGoldLedger = new System.Windows.Forms.RadioButton();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.rbtVoucherBill = new System.Windows.Forms.RadioButton();
            this.txtVNO = new System.Windows.Forms.TextBox();
            this.rbtDailyCashGoldInOut = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(550, 54);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Accounts Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(202, 354);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 29);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "View";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(333, 356);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbtAccountLedger
            // 
            this.rbtAccountLedger.BackColor = System.Drawing.Color.LightCoral;
            this.rbtAccountLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAccountLedger.Location = new System.Drawing.Point(7, 129);
            this.rbtAccountLedger.Name = "rbtAccountLedger";
            this.rbtAccountLedger.Size = new System.Drawing.Size(154, 24);
            this.rbtAccountLedger.TabIndex = 3;
            this.rbtAccountLedger.TabStop = true;
            this.rbtAccountLedger.Text = "Account Ledger";
            this.rbtAccountLedger.UseVisualStyleBackColor = false;
            this.rbtAccountLedger.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtAccountLedger_MouseClick);
            // 
            // rbtChartOfAccount
            // 
            this.rbtChartOfAccount.BackColor = System.Drawing.Color.LightCoral;
            this.rbtChartOfAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtChartOfAccount.Location = new System.Drawing.Point(7, 363);
            this.rbtChartOfAccount.Name = "rbtChartOfAccount";
            this.rbtChartOfAccount.Size = new System.Drawing.Size(154, 24);
            this.rbtChartOfAccount.TabIndex = 4;
            this.rbtChartOfAccount.TabStop = true;
            this.rbtChartOfAccount.Text = "Chart Of Account";
            this.rbtChartOfAccount.UseVisualStyleBackColor = false;
            this.rbtChartOfAccount.Visible = false;
            this.rbtChartOfAccount.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtChartOfAccount_MouseClick);
            // 
            // rbtProfiyAndLoss
            // 
            this.rbtProfiyAndLoss.BackColor = System.Drawing.Color.LightCoral;
            this.rbtProfiyAndLoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtProfiyAndLoss.Location = new System.Drawing.Point(7, 304);
            this.rbtProfiyAndLoss.Name = "rbtProfiyAndLoss";
            this.rbtProfiyAndLoss.Size = new System.Drawing.Size(154, 24);
            this.rbtProfiyAndLoss.TabIndex = 5;
            this.rbtProfiyAndLoss.TabStop = true;
            this.rbtProfiyAndLoss.Text = "Profit And Loss";
            this.rbtProfiyAndLoss.UseVisualStyleBackColor = false;
            this.rbtProfiyAndLoss.Visible = false;
            this.rbtProfiyAndLoss.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtProfiyAndLoss_MouseClick);
            // 
            // rbtTrialBalance
            // 
            this.rbtTrialBalance.BackColor = System.Drawing.Color.LightCoral;
            this.rbtTrialBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtTrialBalance.Location = new System.Drawing.Point(7, 275);
            this.rbtTrialBalance.Name = "rbtTrialBalance";
            this.rbtTrialBalance.Size = new System.Drawing.Size(154, 24);
            this.rbtTrialBalance.TabIndex = 6;
            this.rbtTrialBalance.TabStop = true;
            this.rbtTrialBalance.Text = "Trial Balance";
            this.rbtTrialBalance.UseVisualStyleBackColor = false;
            this.rbtTrialBalance.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtTrialBalance_MouseClick);
            // 
            // rbtComprehensiveLedger
            // 
            this.rbtComprehensiveLedger.BackColor = System.Drawing.Color.LightCoral;
            this.rbtComprehensiveLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtComprehensiveLedger.Location = new System.Drawing.Point(7, 246);
            this.rbtComprehensiveLedger.Name = "rbtComprehensiveLedger";
            this.rbtComprehensiveLedger.Size = new System.Drawing.Size(154, 24);
            this.rbtComprehensiveLedger.TabIndex = 7;
            this.rbtComprehensiveLedger.TabStop = true;
            this.rbtComprehensiveLedger.Text = "Comprehensive Ledger";
            this.rbtComprehensiveLedger.UseVisualStyleBackColor = false;
            this.rbtComprehensiveLedger.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtComprehensiveLedger_MouseClick);
            // 
            // rbtHeadReport
            // 
            this.rbtHeadReport.BackColor = System.Drawing.Color.LightCoral;
            this.rbtHeadReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtHeadReport.Location = new System.Drawing.Point(7, 217);
            this.rbtHeadReport.Name = "rbtHeadReport";
            this.rbtHeadReport.Size = new System.Drawing.Size(154, 24);
            this.rbtHeadReport.TabIndex = 8;
            this.rbtHeadReport.TabStop = true;
            this.rbtHeadReport.Text = "Head Report";
            this.rbtHeadReport.UseVisualStyleBackColor = false;
            this.rbtHeadReport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtHeadReport_MouseClick);
            // 
            // rbtDayBook
            // 
            this.rbtDayBook.BackColor = System.Drawing.Color.LightCoral;
            this.rbtDayBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDayBook.Location = new System.Drawing.Point(7, 333);
            this.rbtDayBook.Name = "rbtDayBook";
            this.rbtDayBook.Size = new System.Drawing.Size(154, 24);
            this.rbtDayBook.TabIndex = 10;
            this.rbtDayBook.TabStop = true;
            this.rbtDayBook.Text = "Day Book";
            this.rbtDayBook.UseVisualStyleBackColor = false;
            this.rbtDayBook.Visible = false;
            this.rbtDayBook.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtDayBook_MouseClick);
            // 
            // rbtParentReport
            // 
            this.rbtParentReport.BackColor = System.Drawing.Color.LightCoral;
            this.rbtParentReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtParentReport.Location = new System.Drawing.Point(7, 187);
            this.rbtParentReport.Name = "rbtParentReport";
            this.rbtParentReport.Size = new System.Drawing.Size(154, 25);
            this.rbtParentReport.TabIndex = 11;
            this.rbtParentReport.TabStop = true;
            this.rbtParentReport.Text = "Parent Report";
            this.rbtParentReport.UseVisualStyleBackColor = false;
            this.rbtParentReport.CheckedChanged += new System.EventHandler(this.rbtParentReport_CheckedChanged);
            this.rbtParentReport.MouseClick += new System.Windows.Forms.MouseEventHandler(this.rbtParentReport_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxChildAccount);
            this.panel1.Controls.Add(this.cbxParentName);
            this.panel1.Controls.Add(this.cbxHeadAccount);
            this.panel1.Controls.Add(this.lblParentName);
            this.panel1.Controls.Add(this.lblChildAccount);
            this.panel1.Controls.Add(this.lblHeadAccount);
            this.panel1.Location = new System.Drawing.Point(193, 99);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 154);
            this.panel1.TabIndex = 12;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // cbxChildAccount
            // 
            this.cbxChildAccount.FormattingEnabled = true;
            this.cbxChildAccount.Location = new System.Drawing.Point(149, 107);
            this.cbxChildAccount.Name = "cbxChildAccount";
            this.cbxChildAccount.Size = new System.Drawing.Size(180, 21);
            this.cbxChildAccount.TabIndex = 11;
            this.cbxChildAccount.SelectedIndexChanged += new System.EventHandler(this.cbxChildAccount_SelectedIndexChanged);
            this.cbxChildAccount.SelectionChangeCommitted += new System.EventHandler(this.cbxChildAccount_SelectionChangeCommitted);
            this.cbxChildAccount.Validating += new System.ComponentModel.CancelEventHandler(this.cbxChildAccount_Validating);
            // 
            // cbxParentName
            // 
            this.cbxParentName.FormattingEnabled = true;
            this.cbxParentName.Location = new System.Drawing.Point(149, 57);
            this.cbxParentName.Name = "cbxParentName";
            this.cbxParentName.Size = new System.Drawing.Size(180, 21);
            this.cbxParentName.TabIndex = 10;
            this.cbxParentName.SelectedIndexChanged += new System.EventHandler(this.cbxParentName_SelectedIndexChanged);
            this.cbxParentName.SelectionChangeCommitted += new System.EventHandler(this.cbxParentName_SelectionChangeCommitted);
            this.cbxParentName.Validating += new System.ComponentModel.CancelEventHandler(this.cbxParentName_Validating);
            // 
            // cbxHeadAccount
            // 
            this.cbxHeadAccount.FormattingEnabled = true;
            this.cbxHeadAccount.Items.AddRange(new object[] {
            "Asset",
            "Liability",
            "Expense",
            "Revenue",
            "Capital"});
            this.cbxHeadAccount.Location = new System.Drawing.Point(149, 12);
            this.cbxHeadAccount.Name = "cbxHeadAccount";
            this.cbxHeadAccount.Size = new System.Drawing.Size(180, 21);
            this.cbxHeadAccount.TabIndex = 7;
            this.cbxHeadAccount.SelectedIndexChanged += new System.EventHandler(this.cbxHeadAccount_SelectedIndexChanged);
            this.cbxHeadAccount.SelectionChangeCommitted += new System.EventHandler(this.cbxHeadAccount_SelectionChangeCommitted);
            this.cbxHeadAccount.Validating += new System.ComponentModel.CancelEventHandler(this.cbxHeadAccount_Validating);
            // 
            // lblParentName
            // 
            this.lblParentName.AutoSize = true;
            this.lblParentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentName.Location = new System.Drawing.Point(8, 65);
            this.lblParentName.Name = "lblParentName";
            this.lblParentName.Size = new System.Drawing.Size(80, 13);
            this.lblParentName.TabIndex = 4;
            this.lblParentName.Text = "Parent Name";
            // 
            // lblChildAccount
            // 
            this.lblChildAccount.AutoSize = true;
            this.lblChildAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChildAccount.Location = new System.Drawing.Point(8, 115);
            this.lblChildAccount.Name = "lblChildAccount";
            this.lblChildAccount.Size = new System.Drawing.Size(86, 13);
            this.lblChildAccount.TabIndex = 3;
            this.lblChildAccount.Text = "Child Account";
            // 
            // lblHeadAccount
            // 
            this.lblHeadAccount.AutoSize = true;
            this.lblHeadAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadAccount.Location = new System.Drawing.Point(8, 15);
            this.lblHeadAccount.Name = "lblHeadAccount";
            this.lblHeadAccount.Size = new System.Drawing.Size(88, 13);
            this.lblHeadAccount.TabIndex = 0;
            this.lblHeadAccount.Text = "Head Account";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(6, 16);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(34, 13);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "From";
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(186, 16);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 13);
            this.lblTo.TabIndex = 2;
            this.lblTo.Text = "To";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpTo);
            this.panel2.Controls.Add(this.dtpFrom);
            this.panel2.Controls.Add(this.lblFrom);
            this.panel2.Controls.Add(this.lblTo);
            this.panel2.Location = new System.Drawing.Point(193, 264);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(345, 46);
            this.panel2.TabIndex = 12;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(226, 12);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(103, 20);
            this.dtpTo.TabIndex = 4;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(60, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(103, 20);
            this.dtpFrom.TabIndex = 3;
            // 
            // rbtGoldLedger
            // 
            this.rbtGoldLedger.BackColor = System.Drawing.Color.LightCoral;
            this.rbtGoldLedger.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtGoldLedger.Location = new System.Drawing.Point(7, 158);
            this.rbtGoldLedger.Name = "rbtGoldLedger";
            this.rbtGoldLedger.Size = new System.Drawing.Size(154, 24);
            this.rbtGoldLedger.TabIndex = 13;
            this.rbtGoldLedger.TabStop = true;
            this.rbtGoldLedger.Text = "Gold Ledger";
            this.rbtGoldLedger.UseVisualStyleBackColor = false;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // rbtVoucherBill
            // 
            this.rbtVoucherBill.BackColor = System.Drawing.Color.LightCoral;
            this.rbtVoucherBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtVoucherBill.Location = new System.Drawing.Point(7, 99);
            this.rbtVoucherBill.Name = "rbtVoucherBill";
            this.rbtVoucherBill.Size = new System.Drawing.Size(154, 24);
            this.rbtVoucherBill.TabIndex = 15;
            this.rbtVoucherBill.TabStop = true;
            this.rbtVoucherBill.Text = "Voucher Bill";
            this.rbtVoucherBill.UseVisualStyleBackColor = false;
            this.rbtVoucherBill.CheckedChanged += new System.EventHandler(this.rbtVoucherBill_CheckedChanged);
            // 
            // txtVNO
            // 
            this.txtVNO.Location = new System.Drawing.Point(342, 64);
            this.txtVNO.Name = "txtVNO";
            this.txtVNO.Size = new System.Drawing.Size(180, 20);
            this.txtVNO.TabIndex = 16;
            // 
            // rbtDailyCashGoldInOut
            // 
            this.rbtDailyCashGoldInOut.BackColor = System.Drawing.Color.LightCoral;
            this.rbtDailyCashGoldInOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDailyCashGoldInOut.Location = new System.Drawing.Point(7, 69);
            this.rbtDailyCashGoldInOut.Name = "rbtDailyCashGoldInOut";
            this.rbtDailyCashGoldInOut.Size = new System.Drawing.Size(154, 24);
            this.rbtDailyCashGoldInOut.TabIndex = 17;
            this.rbtDailyCashGoldInOut.TabStop = true;
            this.rbtDailyCashGoldInOut.Text = "DailyCashGoldCard";
            this.rbtDailyCashGoldInOut.UseVisualStyleBackColor = false;
            this.rbtDailyCashGoldInOut.Visible = false;
            // 
            // AccountReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(550, 406);
            this.Controls.Add(this.rbtDailyCashGoldInOut);
            this.Controls.Add(this.txtVNO);
            this.Controls.Add(this.rbtVoucherBill);
            this.Controls.Add(this.rbtGoldLedger);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtParentReport);
            this.Controls.Add(this.rbtDayBook);
            this.Controls.Add(this.rbtHeadReport);
            this.Controls.Add(this.rbtComprehensiveLedger);
            this.Controls.Add(this.rbtTrialBalance);
            this.Controls.Add(this.rbtProfiyAndLoss);
            this.Controls.Add(this.rbtChartOfAccount);
            this.Controls.Add(this.rbtAccountLedger);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTitle);
            this.Name = "AccountReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccountReports";
            this.Load += new System.EventHandler(this.AccountReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmAccountReports_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbtAccountLedger;
        private System.Windows.Forms.RadioButton rbtChartOfAccount;
        private System.Windows.Forms.RadioButton rbtProfiyAndLoss;
        private System.Windows.Forms.RadioButton rbtTrialBalance;
        private System.Windows.Forms.RadioButton rbtComprehensiveLedger;
        private System.Windows.Forms.RadioButton rbtHeadReport;
        private System.Windows.Forms.RadioButton rbtDayBook;
        private System.Windows.Forms.RadioButton rbtParentReport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblParentName;
        private System.Windows.Forms.Label lblChildAccount;
        private System.Windows.Forms.Label lblHeadAccount;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.ComboBox cbxChildAccount;
        private System.Windows.Forms.ComboBox cbxParentName;
        private System.Windows.Forms.ComboBox cbxHeadAccount;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.RadioButton rbtGoldLedger;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.RadioButton rbtVoucherBill;
        private System.Windows.Forms.TextBox txtVNO;
        private System.Windows.Forms.RadioButton rbtDailyCashGoldInOut;
    }
}