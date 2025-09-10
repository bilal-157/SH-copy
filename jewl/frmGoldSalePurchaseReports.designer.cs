namespace jewl
{
    partial class frmGoldSalePurchaseReports
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.chkDateTo = new System.Windows.Forms.CheckBox();
            this.chkDateFrom = new System.Windows.Forms.CheckBox();
            this.cbxCustomerName = new System.Windows.Forms.ComboBox();
            this.chkCustomerName = new System.Windows.Forms.CheckBox();
            this.rbtCustomerSummary = new System.Windows.Forms.RadioButton();
            this.rbtCustomer = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rbtDate = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.ChkTo = new System.Windows.Forms.CheckBox();
            this.checFrom = new System.Windows.Forms.CheckBox();
            this.rbtGoldBalances = new System.Windows.Forms.RadioButton();
            this.pnlGold = new System.Windows.Forms.Panel();
            this.dtpGoldDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpGoldDateFrom = new System.Windows.Forms.DateTimePicker();
            this.chkGoldDateTo = new System.Windows.Forms.CheckBox();
            this.chkGoldDateFrom = new System.Windows.Forms.CheckBox();
            this.rbtPurchaseBill = new System.Windows.Forms.RadioButton();
            this.txtVNO = new System.Windows.Forms.TextBox();
            this.rbtSaleBill = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlGold.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(353, 484);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(92, 43);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(193, 484);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(92, 44);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "View";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Controls.Add(this.chkDateTo);
            this.panel1.Controls.Add(this.chkDateFrom);
            this.panel1.Controls.Add(this.cbxCustomerName);
            this.panel1.Controls.Add(this.chkCustomerName);
            this.panel1.Location = new System.Drawing.Point(293, 78);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(412, 113);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Enabled = false;
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(237, 82);
            this.dtpDateTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(159, 22);
            this.dtpDateTo.TabIndex = 14;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Enabled = false;
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(237, 49);
            this.dtpDateFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(159, 22);
            this.dtpDateFrom.TabIndex = 13;
            // 
            // chkDateTo
            // 
            this.chkDateTo.Enabled = false;
            this.chkDateTo.Location = new System.Drawing.Point(23, 79);
            this.chkDateTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDateTo.Name = "chkDateTo";
            this.chkDateTo.Size = new System.Drawing.Size(129, 26);
            this.chkDateTo.TabIndex = 12;
            this.chkDateTo.Text = "Date To";
            this.chkDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDateTo.UseVisualStyleBackColor = true;
            this.chkDateTo.CheckedChanged += new System.EventHandler(this.chkDateTo_CheckedChanged);
            // 
            // chkDateFrom
            // 
            this.chkDateFrom.Enabled = false;
            this.chkDateFrom.Location = new System.Drawing.Point(23, 50);
            this.chkDateFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkDateFrom.Name = "chkDateFrom";
            this.chkDateFrom.Size = new System.Drawing.Size(129, 26);
            this.chkDateFrom.TabIndex = 11;
            this.chkDateFrom.Text = "Date From";
            this.chkDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkDateFrom.UseVisualStyleBackColor = true;
            this.chkDateFrom.CheckedChanged += new System.EventHandler(this.chkDateFrom_CheckedChanged);
            // 
            // cbxCustomerName
            // 
            this.cbxCustomerName.Enabled = false;
            this.cbxCustomerName.FormattingEnabled = true;
            this.cbxCustomerName.Location = new System.Drawing.Point(237, 10);
            this.cbxCustomerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxCustomerName.Name = "cbxCustomerName";
            this.cbxCustomerName.Size = new System.Drawing.Size(160, 24);
            this.cbxCustomerName.TabIndex = 10;
            // 
            // chkCustomerName
            // 
            this.chkCustomerName.Enabled = false;
            this.chkCustomerName.Location = new System.Drawing.Point(23, 10);
            this.chkCustomerName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkCustomerName.Name = "chkCustomerName";
            this.chkCustomerName.Size = new System.Drawing.Size(168, 26);
            this.chkCustomerName.TabIndex = 9;
            this.chkCustomerName.Text = "Customer Name";
            this.chkCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkCustomerName.UseVisualStyleBackColor = true;
            this.chkCustomerName.CheckedChanged += new System.EventHandler(this.chkCustomerName_CheckedChanged);
            // 
            // rbtCustomerSummary
            // 
            this.rbtCustomerSummary.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtCustomerSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCustomerSummary.ForeColor = System.Drawing.Color.Black;
            this.rbtCustomerSummary.Location = new System.Drawing.Point(15, 177);
            this.rbtCustomerSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtCustomerSummary.Name = "rbtCustomerSummary";
            this.rbtCustomerSummary.Size = new System.Drawing.Size(243, 27);
            this.rbtCustomerSummary.TabIndex = 9;
            this.rbtCustomerSummary.TabStop = true;
            this.rbtCustomerSummary.Text = "Customer Summary";
            this.rbtCustomerSummary.UseVisualStyleBackColor = false;
            this.rbtCustomerSummary.CheckedChanged += new System.EventHandler(this.rbtCustomerSummary_CheckedChanged);
            // 
            // rbtCustomer
            // 
            this.rbtCustomer.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCustomer.ForeColor = System.Drawing.Color.Black;
            this.rbtCustomer.Location = new System.Drawing.Point(15, 123);
            this.rbtCustomer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtCustomer.Name = "rbtCustomer";
            this.rbtCustomer.Size = new System.Drawing.Size(243, 28);
            this.rbtCustomer.TabIndex = 8;
            this.rbtCustomer.TabStop = true;
            this.rbtCustomer.Text = "Customer";
            this.rbtCustomer.UseVisualStyleBackColor = false;
            this.rbtCustomer.CheckedChanged += new System.EventHandler(this.rbtCustomer_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(721, 62);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "Gold Sale Purchase Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbtDate
            // 
            this.rbtDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDate.ForeColor = System.Drawing.Color.Black;
            this.rbtDate.Location = new System.Drawing.Point(15, 233);
            this.rbtDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtDate.Name = "rbtDate";
            this.rbtDate.Size = new System.Drawing.Size(243, 26);
            this.rbtDate.TabIndex = 15;
            this.rbtDate.TabStop = true;
            this.rbtDate.Text = "Date ";
            this.rbtDate.UseVisualStyleBackColor = false;
            this.rbtDate.CheckedChanged += new System.EventHandler(this.rbtDate_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpTo);
            this.panel2.Controls.Add(this.dtpFrom);
            this.panel2.Controls.Add(this.ChkTo);
            this.panel2.Controls.Add(this.checFrom);
            this.panel2.Location = new System.Drawing.Point(293, 198);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(412, 89);
            this.panel2.TabIndex = 16;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(237, 50);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(159, 22);
            this.dtpTo.TabIndex = 23;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(237, 11);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(159, 22);
            this.dtpFrom.TabIndex = 22;
            // 
            // ChkTo
            // 
            this.ChkTo.Enabled = false;
            this.ChkTo.Location = new System.Drawing.Point(23, 52);
            this.ChkTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ChkTo.Name = "ChkTo";
            this.ChkTo.Size = new System.Drawing.Size(129, 26);
            this.ChkTo.TabIndex = 21;
            this.ChkTo.Text = "Date To";
            this.ChkTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ChkTo.UseVisualStyleBackColor = true;
            this.ChkTo.CheckedChanged += new System.EventHandler(this.ChkTo_CheckedChanged);
            // 
            // checFrom
            // 
            this.checFrom.Enabled = false;
            this.checFrom.Location = new System.Drawing.Point(23, 12);
            this.checFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checFrom.Name = "checFrom";
            this.checFrom.Size = new System.Drawing.Size(129, 26);
            this.checFrom.TabIndex = 20;
            this.checFrom.Text = "Date From";
            this.checFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checFrom.UseVisualStyleBackColor = true;
            this.checFrom.CheckedChanged += new System.EventHandler(this.checFrom_CheckedChanged);
            // 
            // rbtGoldBalances
            // 
            this.rbtGoldBalances.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtGoldBalances.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtGoldBalances.ForeColor = System.Drawing.Color.Black;
            this.rbtGoldBalances.Location = new System.Drawing.Point(15, 294);
            this.rbtGoldBalances.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtGoldBalances.Name = "rbtGoldBalances";
            this.rbtGoldBalances.Size = new System.Drawing.Size(243, 26);
            this.rbtGoldBalances.TabIndex = 17;
            this.rbtGoldBalances.TabStop = true;
            this.rbtGoldBalances.Text = "Gold Balances";
            this.rbtGoldBalances.UseVisualStyleBackColor = false;
            this.rbtGoldBalances.CheckedChanged += new System.EventHandler(this.rbtGoldBalances_CheckedChanged);
            // 
            // pnlGold
            // 
            this.pnlGold.Controls.Add(this.dtpGoldDateTo);
            this.pnlGold.Controls.Add(this.dtpGoldDateFrom);
            this.pnlGold.Controls.Add(this.chkGoldDateTo);
            this.pnlGold.Controls.Add(this.chkGoldDateFrom);
            this.pnlGold.Location = new System.Drawing.Point(293, 294);
            this.pnlGold.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlGold.Name = "pnlGold";
            this.pnlGold.Size = new System.Drawing.Size(412, 89);
            this.pnlGold.TabIndex = 18;
            this.pnlGold.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlGold_Paint);
            // 
            // dtpGoldDateTo
            // 
            this.dtpGoldDateTo.Enabled = false;
            this.dtpGoldDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGoldDateTo.Location = new System.Drawing.Point(237, 50);
            this.dtpGoldDateTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpGoldDateTo.Name = "dtpGoldDateTo";
            this.dtpGoldDateTo.Size = new System.Drawing.Size(159, 22);
            this.dtpGoldDateTo.TabIndex = 23;
            // 
            // dtpGoldDateFrom
            // 
            this.dtpGoldDateFrom.Enabled = false;
            this.dtpGoldDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpGoldDateFrom.Location = new System.Drawing.Point(237, 11);
            this.dtpGoldDateFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpGoldDateFrom.Name = "dtpGoldDateFrom";
            this.dtpGoldDateFrom.Size = new System.Drawing.Size(159, 22);
            this.dtpGoldDateFrom.TabIndex = 22;
            // 
            // chkGoldDateTo
            // 
            this.chkGoldDateTo.Enabled = false;
            this.chkGoldDateTo.Location = new System.Drawing.Point(23, 52);
            this.chkGoldDateTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkGoldDateTo.Name = "chkGoldDateTo";
            this.chkGoldDateTo.Size = new System.Drawing.Size(129, 26);
            this.chkGoldDateTo.TabIndex = 21;
            this.chkGoldDateTo.Text = "Date To";
            this.chkGoldDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGoldDateTo.UseVisualStyleBackColor = true;
            this.chkGoldDateTo.CheckedChanged += new System.EventHandler(this.chkGoldDateTo_CheckedChanged);
            // 
            // chkGoldDateFrom
            // 
            this.chkGoldDateFrom.Enabled = false;
            this.chkGoldDateFrom.Location = new System.Drawing.Point(23, 12);
            this.chkGoldDateFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkGoldDateFrom.Name = "chkGoldDateFrom";
            this.chkGoldDateFrom.Size = new System.Drawing.Size(129, 26);
            this.chkGoldDateFrom.TabIndex = 20;
            this.chkGoldDateFrom.Text = "Date From";
            this.chkGoldDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkGoldDateFrom.UseVisualStyleBackColor = true;
            this.chkGoldDateFrom.CheckedChanged += new System.EventHandler(this.chkGoldDateFrom_CheckedChanged);
            // 
            // rbtPurchaseBill
            // 
            this.rbtPurchaseBill.BackColor = System.Drawing.Color.LightCoral;
            this.rbtPurchaseBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPurchaseBill.Location = new System.Drawing.Point(15, 353);
            this.rbtPurchaseBill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rbtPurchaseBill.Name = "rbtPurchaseBill";
            this.rbtPurchaseBill.Size = new System.Drawing.Size(205, 30);
            this.rbtPurchaseBill.TabIndex = 20;
            this.rbtPurchaseBill.TabStop = true;
            this.rbtPurchaseBill.Text = "Purchase Bill";
            this.rbtPurchaseBill.UseVisualStyleBackColor = false;
            this.rbtPurchaseBill.CheckedChanged += new System.EventHandler(this.rbtVoucherBill_CheckedChanged);
            // 
            // txtVNO
            // 
            this.txtVNO.Location = new System.Drawing.Point(451, 390);
            this.txtVNO.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVNO.Multiline = true;
            this.txtVNO.Name = "txtVNO";
            this.txtVNO.Size = new System.Drawing.Size(240, 48);
            this.txtVNO.TabIndex = 21;
            // 
            // rbtSaleBill
            // 
            this.rbtSaleBill.BackColor = System.Drawing.Color.LightCoral;
            this.rbtSaleBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSaleBill.Location = new System.Drawing.Point(15, 385);
            this.rbtSaleBill.Margin = new System.Windows.Forms.Padding(4);
            this.rbtSaleBill.Name = "rbtSaleBill";
            this.rbtSaleBill.Size = new System.Drawing.Size(205, 30);
            this.rbtSaleBill.TabIndex = 22;
            this.rbtSaleBill.TabStop = true;
            this.rbtSaleBill.Text = "Sale Bill";
            this.rbtSaleBill.UseVisualStyleBackColor = false;
            this.rbtSaleBill.CheckedChanged += new System.EventHandler(this.rbtSaleBill_CheckedChanged);
            // 
            // frmGoldSalePurchaseReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(721, 542);
            this.Controls.Add(this.rbtSaleBill);
            this.Controls.Add(this.txtVNO);
            this.Controls.Add(this.rbtPurchaseBill);
            this.Controls.Add(this.pnlGold);
            this.Controls.Add(this.rbtGoldBalances);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rbtDate);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtCustomerSummary);
            this.Controls.Add(this.rbtCustomer);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmGoldSalePurchaseReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmGoldSalePurchaseReports";
            this.Load += new System.EventHandler(this.frmGoldSalePurchaseReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmGoldSalePurchaseReports_Paint);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlGold.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.CheckBox chkDateTo;
        private System.Windows.Forms.CheckBox chkDateFrom;
        private System.Windows.Forms.ComboBox cbxCustomerName;
        private System.Windows.Forms.CheckBox chkCustomerName;
        private System.Windows.Forms.RadioButton rbtCustomerSummary;
        private System.Windows.Forms.RadioButton rbtCustomer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.RadioButton rbtDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.CheckBox ChkTo;
        private System.Windows.Forms.CheckBox checFrom;
        private System.Windows.Forms.RadioButton rbtGoldBalances;
        private System.Windows.Forms.Panel pnlGold;
        private System.Windows.Forms.DateTimePicker dtpGoldDateTo;
        private System.Windows.Forms.DateTimePicker dtpGoldDateFrom;
        private System.Windows.Forms.CheckBox chkGoldDateTo;
        private System.Windows.Forms.CheckBox chkGoldDateFrom;
        private System.Windows.Forms.RadioButton rbtPurchaseBill;
        private System.Windows.Forms.TextBox txtVNO;
        private System.Windows.Forms.RadioButton rbtSaleBill;
    }
}