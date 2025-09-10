namespace jewl
{
    partial class PurchaseReports
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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cbxSupplierCodeTo = new System.Windows.Forms.ComboBox();
            this.cbxSupplierCodeFrom = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chkSupplierFrom = new System.Windows.Forms.CheckBox();
            this.chkSupplierTo = new System.Windows.Forms.CheckBox();
            this.txtWeightFrom = new System.Windows.Forms.TextBox();
            this.txtWeightTo = new System.Windows.Forms.TextBox();
            this.txtInvoiceTo = new System.Windows.Forms.TextBox();
            this.txtInvoiceFrom = new System.Windows.Forms.TextBox();
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cbxItemCode = new System.Windows.Forms.ComboBox();
            this.cbxKarat = new System.Windows.Forms.ComboBox();
            this.btnView = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 21);
            this.label3.TabIndex = 58;
            this.label3.Text = "Weight Range";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 21);
            this.label2.TabIndex = 57;
            this.label2.Text = "Invoice Range";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(41, 37);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 21);
            this.label8.TabIndex = 56;
            this.label8.Text = "Supplier Code";
            // 
            // cbxSupplierCodeTo
            // 
            this.cbxSupplierCodeTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupplierCodeTo.FormattingEnabled = true;
            this.cbxSupplierCodeTo.Location = new System.Drawing.Point(328, 39);
            this.cbxSupplierCodeTo.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSupplierCodeTo.Name = "cbxSupplierCodeTo";
            this.cbxSupplierCodeTo.Size = new System.Drawing.Size(118, 21);
            this.cbxSupplierCodeTo.TabIndex = 55;
            // 
            // cbxSupplierCodeFrom
            // 
            this.cbxSupplierCodeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSupplierCodeFrom.FormattingEnabled = true;
            this.cbxSupplierCodeFrom.Location = new System.Drawing.Point(177, 37);
            this.cbxSupplierCodeFrom.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSupplierCodeFrom.Name = "cbxSupplierCodeFrom";
            this.cbxSupplierCodeFrom.Size = new System.Drawing.Size(118, 21);
            this.cbxSupplierCodeFrom.TabIndex = 54;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 21);
            this.label1.TabIndex = 53;
            this.label1.Text = "To";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(177, 13);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 21);
            this.label5.TabIndex = 52;
            this.label5.Text = "From";
            // 
            // chkSupplierFrom
            // 
            this.chkSupplierFrom.AutoSize = true;
            this.chkSupplierFrom.Location = new System.Drawing.Point(155, 39);
            this.chkSupplierFrom.Name = "chkSupplierFrom";
            this.chkSupplierFrom.Size = new System.Drawing.Size(15, 14);
            this.chkSupplierFrom.TabIndex = 1;
            this.chkSupplierFrom.UseVisualStyleBackColor = true;
            this.chkSupplierFrom.CheckedChanged += new System.EventHandler(this.chkSupplierFrom_CheckedChanged);
            // 
            // chkSupplierTo
            // 
            this.chkSupplierTo.AutoSize = true;
            this.chkSupplierTo.Location = new System.Drawing.Point(306, 39);
            this.chkSupplierTo.Name = "chkSupplierTo";
            this.chkSupplierTo.Size = new System.Drawing.Size(15, 14);
            this.chkSupplierTo.TabIndex = 2;
            this.chkSupplierTo.UseVisualStyleBackColor = true;
            this.chkSupplierTo.CheckedChanged += new System.EventHandler(this.chkSupplierTo_CheckedChanged);
            // 
            // txtWeightFrom
            // 
            this.txtWeightFrom.Location = new System.Drawing.Point(177, 68);
            this.txtWeightFrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtWeightFrom.Name = "txtWeightFrom";
            this.txtWeightFrom.Size = new System.Drawing.Size(118, 20);
            this.txtWeightFrom.TabIndex = 59;
            // 
            // txtWeightTo
            // 
            this.txtWeightTo.Location = new System.Drawing.Point(328, 69);
            this.txtWeightTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtWeightTo.Name = "txtWeightTo";
            this.txtWeightTo.Size = new System.Drawing.Size(118, 20);
            this.txtWeightTo.TabIndex = 60;
            // 
            // txtInvoiceTo
            // 
            this.txtInvoiceTo.Location = new System.Drawing.Point(328, 98);
            this.txtInvoiceTo.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoiceTo.Name = "txtInvoiceTo";
            this.txtInvoiceTo.Size = new System.Drawing.Size(118, 20);
            this.txtInvoiceTo.TabIndex = 61;
            // 
            // txtInvoiceFrom
            // 
            this.txtInvoiceFrom.Location = new System.Drawing.Point(177, 98);
            this.txtInvoiceFrom.Margin = new System.Windows.Forms.Padding(4);
            this.txtInvoiceFrom.Name = "txtInvoiceFrom";
            this.txtInvoiceFrom.Size = new System.Drawing.Size(118, 20);
            this.txtInvoiceFrom.TabIndex = 62;
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(564, 39);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(118, 20);
            this.dateTimePickerFrom.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(470, 39);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 21);
            this.label4.TabIndex = 64;
            this.label4.Text = "Date From";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(470, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 21);
            this.label6.TabIndex = 65;
            this.label6.Text = "Date To";
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(564, 69);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(118, 20);
            this.dateTimePickerTo.TabIndex = 66;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(710, 39);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 21);
            this.label7.TabIndex = 67;
            this.label7.Text = "Item Code";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(710, 69);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 21);
            this.label9.TabIndex = 68;
            this.label9.Text = "Gold Karat";
            // 
            // cbxItemCode
            // 
            this.cbxItemCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxItemCode.FormattingEnabled = true;
            this.cbxItemCode.Location = new System.Drawing.Point(790, 39);
            this.cbxItemCode.Margin = new System.Windows.Forms.Padding(4);
            this.cbxItemCode.Name = "cbxItemCode";
            this.cbxItemCode.Size = new System.Drawing.Size(118, 21);
            this.cbxItemCode.TabIndex = 69;
            // 
            // cbxKarat
            // 
            this.cbxKarat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKarat.FormattingEnabled = true;
            this.cbxKarat.Items.AddRange(new object[] {
            "24",
            "22",
            "21",
            "20",
            "18",
            "17",
            "16",
            "15"});
            this.cbxKarat.Location = new System.Drawing.Point(790, 66);
            this.cbxKarat.Margin = new System.Windows.Forms.Padding(4);
            this.cbxKarat.Name = "cbxKarat";
            this.cbxKarat.Size = new System.Drawing.Size(118, 21);
            this.cbxKarat.TabIndex = 70;
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(969, 33);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(98, 54);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "QUERY";
            this.btnView.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1073, 33);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 54);
            this.btnClose.TabIndex = 71;
            this.btnClose.Text = "CLOSE";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerFrom);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.txtWeightTo);
            this.groupBox1.Controls.Add(this.btnView);
            this.groupBox1.Controls.Add(this.txtWeightFrom);
            this.groupBox1.Controls.Add(this.cbxItemCode);
            this.groupBox1.Controls.Add(this.txtInvoiceTo);
            this.groupBox1.Controls.Add(this.cbxKarat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkSupplierTo);
            this.groupBox1.Controls.Add(this.txtInvoiceFrom);
            this.groupBox1.Controls.Add(this.chkSupplierFrom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbxSupplierCodeTo);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.dateTimePickerTo);
            this.groupBox1.Controls.Add(this.cbxSupplierCodeFrom);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1234, 130);
            this.groupBox1.TabIndex = 72;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Panel";
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.DisplayGroupTree = false;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 130);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.SelectionFormula = "";
            this.crystalReportViewer1.Size = new System.Drawing.Size(1234, 566);
            this.crystalReportViewer1.TabIndex = 72;
            this.crystalReportViewer1.ViewTimeSelectionFormula = "";
            // 
            // PurchaseReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 696);
            this.Controls.Add(this.crystalReportViewer1);
            this.Controls.Add(this.groupBox1);
            this.Name = "PurchaseReports";
            this.Text = "PurchaseReports";
            this.Load += new System.EventHandler(this.PurchaseReports_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkSupplierFrom;
        private System.Windows.Forms.CheckBox chkSupplierTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxSupplierCodeFrom;
        private System.Windows.Forms.ComboBox cbxSupplierCodeTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWeightFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.TextBox txtInvoiceFrom;
        private System.Windows.Forms.TextBox txtInvoiceTo;
        private System.Windows.Forms.TextBox txtWeightTo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox cbxKarat;
        private System.Windows.Forms.ComboBox cbxItemCode;
        private System.Windows.Forms.GroupBox groupBox1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;

    }
}