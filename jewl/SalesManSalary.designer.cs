namespace jewl
{
    partial class SalesManSalary
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
            this.chkAlownce = new System.Windows.Forms.CheckBox();
            this.txtAlownce = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxAdvanceNo = new System.Windows.Forms.ComboBox();
            this.pnlIstallment = new System.Windows.Forms.Panel();
            this.txtRemainingSalary = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtRemainingAdvance = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtInstallmentAmount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSalaryOfMonth = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkInstallmentPaid = new System.Windows.Forms.CheckBox();
            this.txtSalaryAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCashInHand = new System.Windows.Forms.TextBox();
            this.dgvPreviousBalance = new System.Windows.Forms.DataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxEmloyeeName = new System.Windows.Forms.ComboBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnClose = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlIstallment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreviousBalance)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkAlownce
            // 
            this.chkAlownce.AutoSize = true;
            this.chkAlownce.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAlownce.Location = new System.Drawing.Point(6, 65);
            this.chkAlownce.Name = "chkAlownce";
            this.chkAlownce.Size = new System.Drawing.Size(74, 17);
            this.chkAlownce.TabIndex = 24;
            this.chkAlownce.Text = "Alownce";
            this.chkAlownce.UseVisualStyleBackColor = true;
            this.chkAlownce.CheckedChanged += new System.EventHandler(this.chkAlownce_CheckedChanged);
            // 
            // txtAlownce
            // 
            this.txtAlownce.Location = new System.Drawing.Point(121, 63);
            this.txtAlownce.Name = "txtAlownce";
            this.txtAlownce.Size = new System.Drawing.Size(98, 20);
            this.txtAlownce.TabIndex = 23;
            this.txtAlownce.Visible = false;
            this.txtAlownce.TextChanged += new System.EventHandler(this.txtAlownce_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(379, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "Advance No.";
            this.label10.Visible = false;
            // 
            // cbxAdvanceNo
            // 
            this.cbxAdvanceNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxAdvanceNo.FormattingEnabled = true;
            this.cbxAdvanceNo.Location = new System.Drawing.Point(540, 63);
            this.cbxAdvanceNo.Name = "cbxAdvanceNo";
            this.cbxAdvanceNo.Size = new System.Drawing.Size(106, 21);
            this.cbxAdvanceNo.TabIndex = 20;
            this.cbxAdvanceNo.Visible = false;
            this.cbxAdvanceNo.SelectedIndexChanged += new System.EventHandler(this.cbxAdvanceNo_SelectedIndexChanged);
            this.cbxAdvanceNo.SelectionChangeCommitted += new System.EventHandler(this.cbxAdvanceNo_SelectionChangeCommitted);
            // 
            // pnlIstallment
            // 
            this.pnlIstallment.Controls.Add(this.txtRemainingSalary);
            this.pnlIstallment.Controls.Add(this.label15);
            this.pnlIstallment.Controls.Add(this.txtRemainingAdvance);
            this.pnlIstallment.Controls.Add(this.label14);
            this.pnlIstallment.Controls.Add(this.txtInstallmentAmount);
            this.pnlIstallment.Controls.Add(this.label13);
            this.pnlIstallment.Location = new System.Drawing.Point(1, 100);
            this.pnlIstallment.Name = "pnlIstallment";
            this.pnlIstallment.Size = new System.Drawing.Size(662, 38);
            this.pnlIstallment.TabIndex = 19;
            this.pnlIstallment.Visible = false;
            // 
            // txtRemainingSalary
            // 
            this.txtRemainingSalary.Location = new System.Drawing.Point(578, 7);
            this.txtRemainingSalary.Name = "txtRemainingSalary";
            this.txtRemainingSalary.ReadOnly = true;
            this.txtRemainingSalary.Size = new System.Drawing.Size(70, 20);
            this.txtRemainingSalary.TabIndex = 22;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(447, 10);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(105, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "Remaining Salary";
            // 
            // txtRemainingAdvance
            // 
            this.txtRemainingAdvance.Location = new System.Drawing.Point(374, 7);
            this.txtRemainingAdvance.Name = "txtRemainingAdvance";
            this.txtRemainingAdvance.ReadOnly = true;
            this.txtRemainingAdvance.Size = new System.Drawing.Size(70, 20);
            this.txtRemainingAdvance.TabIndex = 20;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(222, 10);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(120, 13);
            this.label14.TabIndex = 19;
            this.label14.Text = "Remaining Advance";
            // 
            // txtInstallmentAmount
            // 
            this.txtInstallmentAmount.Location = new System.Drawing.Point(147, 7);
            this.txtInstallmentAmount.Name = "txtInstallmentAmount";
            this.txtInstallmentAmount.Size = new System.Drawing.Size(70, 20);
            this.txtInstallmentAmount.TabIndex = 18;
            this.txtInstallmentAmount.TextChanged += new System.EventHandler(this.txtInstallmentAmount_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(3, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(114, 13);
            this.label13.TabIndex = 17;
            this.label13.Text = "Installment Amount";
            // 
            // txtSalaryOfMonth
            // 
            this.txtSalaryOfMonth.Location = new System.Drawing.Point(540, 26);
            this.txtSalaryOfMonth.Name = "txtSalaryOfMonth";
            this.txtSalaryOfMonth.ReadOnly = true;
            this.txtSalaryOfMonth.Size = new System.Drawing.Size(106, 20);
            this.txtSalaryOfMonth.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(379, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Salary Of This Month";
            // 
            // chkInstallmentPaid
            // 
            this.chkInstallmentPaid.AutoSize = true;
            this.chkInstallmentPaid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkInstallmentPaid.Location = new System.Drawing.Point(249, 65);
            this.chkInstallmentPaid.Name = "chkInstallmentPaid";
            this.chkInstallmentPaid.Size = new System.Drawing.Size(116, 17);
            this.chkInstallmentPaid.TabIndex = 13;
            this.chkInstallmentPaid.Text = "Installment Paid";
            this.chkInstallmentPaid.UseVisualStyleBackColor = true;
            this.chkInstallmentPaid.CheckedChanged += new System.EventHandler(this.chkInstallmentPaid_CheckedChanged);
            // 
            // txtSalaryAmount
            // 
            this.txtSalaryAmount.Location = new System.Drawing.Point(271, 26);
            this.txtSalaryAmount.Name = "txtSalaryAmount";
            this.txtSalaryAmount.ReadOnly = true;
            this.txtSalaryAmount.Size = new System.Drawing.Size(69, 20);
            this.txtSalaryAmount.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(158, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(88, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Salary Amount";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(55, 26);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(87, 20);
            this.dtpDate.TabIndex = 6;
            this.dtpDate.Leave += new System.EventHandler(this.dtpDate_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Date";
            // 
            // txtCashInHand
            // 
            this.txtCashInHand.BackColor = System.Drawing.Color.Black;
            this.txtCashInHand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCashInHand.ForeColor = System.Drawing.Color.MidnightBlue;
            this.txtCashInHand.Location = new System.Drawing.Point(558, 43);
            this.txtCashInHand.Name = "txtCashInHand";
            this.txtCashInHand.Size = new System.Drawing.Size(88, 21);
            this.txtCashInHand.TabIndex = 9;
            // 
            // dgvPreviousBalance
            // 
            this.dgvPreviousBalance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPreviousBalance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.Column1,
            this.Column2,
            this.Column5,
            this.Column3,
            this.Column4});
            this.dgvPreviousBalance.Location = new System.Drawing.Point(6, 24);
            this.dgvPreviousBalance.Name = "dgvPreviousBalance";
            this.dgvPreviousBalance.Size = new System.Drawing.Size(652, 127);
            this.dgvPreviousBalance.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(554, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Cash In Hand";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 27);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Emloyee Name";
            // 
            // cbxEmloyeeName
            // 
            this.cbxEmloyeeName.FormattingEnabled = true;
            this.cbxEmloyeeName.Location = new System.Drawing.Point(126, 24);
            this.cbxEmloyeeName.Name = "cbxEmloyeeName";
            this.cbxEmloyeeName.Size = new System.Drawing.Size(158, 21);
            this.cbxEmloyeeName.TabIndex = 6;
            this.cbxEmloyeeName.SelectedIndexChanged += new System.EventHandler(this.cbxEmloyeeName_SelectedIndexChanged);
            this.cbxEmloyeeName.SelectionChangeCommitted += new System.EventHandler(this.cbxEmloyeeName_SelectionChangeCommitted);
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(384, 24);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(141, 20);
            this.txtContactNo.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(290, 27);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Contact No";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(126, 58);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(399, 20);
            this.txtAddress.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(685, 28);
            this.toolStrip1.TabIndex = 62;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSave.Image = global::jewl.Properties.Resources._1483476843_Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 25);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClose.Image = global::jewl.Properties.Resources._1483476602_Cancel;
            this.btnClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 25);
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCashInHand);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.cbxEmloyeeName);
            this.groupBox1.Controls.Add(this.txtContactNo);
            this.groupBox1.Location = new System.Drawing.Point(10, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(665, 90);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Saleman Information";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvPreviousBalance);
            this.groupBox2.Location = new System.Drawing.Point(10, 140);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(665, 159);
            this.groupBox2.TabIndex = 64;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Advance Detail";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pnlIstallment);
            this.groupBox3.Controls.Add(this.chkAlownce);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtAlownce);
            this.groupBox3.Controls.Add(this.dtpDate);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.cbxAdvanceNo);
            this.groupBox3.Controls.Add(this.txtSalaryAmount);
            this.groupBox3.Controls.Add(this.chkInstallmentPaid);
            this.groupBox3.Controls.Add(this.txtSalaryOfMonth);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(10, 305);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(665, 142);
            this.groupBox3.TabIndex = 65;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Salary Detail";
            // 
            // No
            // 
            this.No.HeaderText = "Date";
            this.No.Name = "No";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Amount";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.FillWeight = 150F;
            this.Column2.HeaderText = "# Of Installment";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column5
            // 
            this.Column5.FillWeight = 120F;
            this.Column5.HeaderText = "InstallmentPaid";
            this.Column5.Name = "Column5";
            this.Column5.Width = 120;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 180F;
            this.Column3.HeaderText = "Installment Amount";
            this.Column3.Name = "Column3";
            this.Column3.Width = 180;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Remaining";
            this.Column4.Name = "Column4";
            // 
            // SalesManSalary
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(685, 454);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SalesManSalary";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SalemanSalary";
            this.Load += new System.EventHandler(this.SalesManSalary_Load);
            this.pnlIstallment.ResumeLayout(false);
            this.pnlIstallment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPreviousBalance)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkInstallmentPaid;
        private System.Windows.Forms.TextBox txtSalaryAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCashInHand;
        private System.Windows.Forms.DataGridView dgvPreviousBalance;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbxEmloyeeName;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtSalaryOfMonth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbxAdvanceNo;
        private System.Windows.Forms.Panel pnlIstallment;
        private System.Windows.Forms.TextBox txtRemainingSalary;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtRemainingAdvance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtInstallmentAmount;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chkAlownce;
        private System.Windows.Forms.TextBox txtAlownce;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}