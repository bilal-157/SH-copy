namespace jewl
{
    partial class frmRepairingReport
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
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.rbtCompleteRepairing = new System.Windows.Forms.RadioButton();
            this.pnlSampleDateWise = new System.Windows.Forms.Panel();
            this.cbxWorker = new System.Windows.Forms.ComboBox();
            this.chkWorker = new System.Windows.Forms.CheckBox();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.chkFrom = new System.Windows.Forms.CheckBox();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.rbtDelivered = new System.Windows.Forms.RadioButton();
            this.rbtRepairingGivenToWorker = new System.Windows.Forms.RadioButton();
            this.rbtReceiveFromWorker = new System.Windows.Forms.RadioButton();
            this.rbtReapirBill = new System.Windows.Forms.RadioButton();
            this.txtReapirBill = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlBill = new System.Windows.Forms.Panel();
            this.chkDDateFrom = new System.Windows.Forms.CheckBox();
            this.chkDDateTo = new System.Windows.Forms.CheckBox();
            this.dtpDTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDFrom = new System.Windows.Forms.DateTimePicker();
            this.pnlSampleDateWise.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlBill.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yy";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(173, 50);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(170, 22);
            this.dtpTo.TabIndex = 10;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yy";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(173, 20);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(170, 22);
            this.dtpFrom.TabIndex = 8;
            // 
            // rbtCompleteRepairing
            // 
            this.rbtCompleteRepairing.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtCompleteRepairing.Checked = true;
            this.rbtCompleteRepairing.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCompleteRepairing.Location = new System.Drawing.Point(13, 92);
            this.rbtCompleteRepairing.Margin = new System.Windows.Forms.Padding(4);
            this.rbtCompleteRepairing.Name = "rbtCompleteRepairing";
            this.rbtCompleteRepairing.Size = new System.Drawing.Size(207, 30);
            this.rbtCompleteRepairing.TabIndex = 15;
            this.rbtCompleteRepairing.TabStop = true;
            this.rbtCompleteRepairing.Text = "Complete Repairing";
            this.rbtCompleteRepairing.UseVisualStyleBackColor = false;
            // 
            // pnlSampleDateWise
            // 
            this.pnlSampleDateWise.Controls.Add(this.chkDDateFrom);
            this.pnlSampleDateWise.Controls.Add(this.chkDDateTo);
            this.pnlSampleDateWise.Controls.Add(this.dtpDTo);
            this.pnlSampleDateWise.Controls.Add(this.dtpDFrom);
            this.pnlSampleDateWise.Controls.Add(this.cbxWorker);
            this.pnlSampleDateWise.Controls.Add(this.chkWorker);
            this.pnlSampleDateWise.Controls.Add(this.cbxCustomer);
            this.pnlSampleDateWise.Controls.Add(this.chkCustomer);
            this.pnlSampleDateWise.Controls.Add(this.chkFrom);
            this.pnlSampleDateWise.Controls.Add(this.chkTo);
            this.pnlSampleDateWise.Controls.Add(this.dtpTo);
            this.pnlSampleDateWise.Controls.Add(this.dtpFrom);
            this.pnlSampleDateWise.Location = new System.Drawing.Point(227, 95);
            this.pnlSampleDateWise.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSampleDateWise.Name = "pnlSampleDateWise";
            this.pnlSampleDateWise.Size = new System.Drawing.Size(462, 239);
            this.pnlSampleDateWise.TabIndex = 14;
            // 
            // cbxWorker
            // 
            this.cbxWorker.Enabled = false;
            this.cbxWorker.FormattingEnabled = true;
            this.cbxWorker.Location = new System.Drawing.Point(172, 175);
            this.cbxWorker.Name = "cbxWorker";
            this.cbxWorker.Size = new System.Drawing.Size(170, 24);
            this.cbxWorker.TabIndex = 16;
            // 
            // chkWorker
            // 
            this.chkWorker.AutoSize = true;
            this.chkWorker.Location = new System.Drawing.Point(20, 182);
            this.chkWorker.Name = "chkWorker";
            this.chkWorker.Size = new System.Drawing.Size(76, 21);
            this.chkWorker.TabIndex = 15;
            this.chkWorker.Text = "Worker";
            this.chkWorker.UseVisualStyleBackColor = true;
            this.chkWorker.CheckedChanged += new System.EventHandler(this.chkWorker_CheckedChanged);
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.Enabled = false;
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(172, 143);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(170, 24);
            this.cbxCustomer.TabIndex = 14;
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.Location = new System.Drawing.Point(20, 148);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(90, 21);
            this.chkCustomer.TabIndex = 13;
            this.chkCustomer.Text = "Customer";
            this.chkCustomer.UseVisualStyleBackColor = true;
            this.chkCustomer.CheckedChanged += new System.EventHandler(this.chkCustomer_CheckedChanged);
            // 
            // chkFrom
            // 
            this.chkFrom.AutoSize = true;
            this.chkFrom.Location = new System.Drawing.Point(21, 24);
            this.chkFrom.Name = "chkFrom";
            this.chkFrom.Size = new System.Drawing.Size(117, 21);
            this.chkFrom.TabIndex = 12;
            this.chkFrom.Text = "Receive From";
            this.chkFrom.UseVisualStyleBackColor = true;
            this.chkFrom.CheckedChanged += new System.EventHandler(this.chkFrom_CheckedChanged);
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Location = new System.Drawing.Point(21, 54);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(102, 21);
            this.chkTo.TabIndex = 11;
            this.chkTo.Text = "Receive To";
            this.chkTo.UseVisualStyleBackColor = true;
            this.chkTo.CheckedChanged += new System.EventHandler(this.chkTo_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(684, 80);
            this.panel1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(-1, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(459, 70);
            this.label2.TabIndex = 0;
            this.label2.Text = "Repairing Reports";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(571, 5);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 66);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(463, 5);
            this.btnView.Margin = new System.Windows.Forms.Padding(4);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(100, 66);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // rbtDelivered
            // 
            this.rbtDelivered.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDelivered.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDelivered.Location = new System.Drawing.Point(13, 208);
            this.rbtDelivered.Margin = new System.Windows.Forms.Padding(4);
            this.rbtDelivered.Name = "rbtDelivered";
            this.rbtDelivered.Size = new System.Drawing.Size(207, 30);
            this.rbtDelivered.TabIndex = 16;
            this.rbtDelivered.Text = "Delivered Repairing";
            this.rbtDelivered.UseVisualStyleBackColor = false;
            // 
            // rbtRepairingGivenToWorker
            // 
            this.rbtRepairingGivenToWorker.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtRepairingGivenToWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtRepairingGivenToWorker.Location = new System.Drawing.Point(13, 130);
            this.rbtRepairingGivenToWorker.Margin = new System.Windows.Forms.Padding(4);
            this.rbtRepairingGivenToWorker.Name = "rbtRepairingGivenToWorker";
            this.rbtRepairingGivenToWorker.Size = new System.Drawing.Size(207, 30);
            this.rbtRepairingGivenToWorker.TabIndex = 17;
            this.rbtRepairingGivenToWorker.Text = "Given To Worker";
            this.rbtRepairingGivenToWorker.UseVisualStyleBackColor = false;
            // 
            // rbtReceiveFromWorker
            // 
            this.rbtReceiveFromWorker.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtReceiveFromWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtReceiveFromWorker.Location = new System.Drawing.Point(13, 169);
            this.rbtReceiveFromWorker.Margin = new System.Windows.Forms.Padding(4);
            this.rbtReceiveFromWorker.Name = "rbtReceiveFromWorker";
            this.rbtReceiveFromWorker.Size = new System.Drawing.Size(207, 30);
            this.rbtReceiveFromWorker.TabIndex = 18;
            this.rbtReceiveFromWorker.Text = "Receive From Worker";
            this.rbtReceiveFromWorker.UseVisualStyleBackColor = false;
            // 
            // rbtReapirBill
            // 
            this.rbtReapirBill.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtReapirBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtReapirBill.Location = new System.Drawing.Point(13, 246);
            this.rbtReapirBill.Margin = new System.Windows.Forms.Padding(4);
            this.rbtReapirBill.Name = "rbtReapirBill";
            this.rbtReapirBill.Size = new System.Drawing.Size(207, 30);
            this.rbtReapirBill.TabIndex = 19;
            this.rbtReapirBill.Text = "Repair Bill No";
            this.rbtReapirBill.UseVisualStyleBackColor = false;
            this.rbtReapirBill.CheckedChanged += new System.EventHandler(this.rbtReapirBill_CheckedChanged);
            // 
            // txtReapirBill
            // 
            this.txtReapirBill.Location = new System.Drawing.Point(119, 17);
            this.txtReapirBill.Name = "txtReapirBill";
            this.txtReapirBill.Size = new System.Drawing.Size(170, 22);
            this.txtReapirBill.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "RepairNo";
            // 
            // pnlBill
            // 
            this.pnlBill.Controls.Add(this.txtReapirBill);
            this.pnlBill.Controls.Add(this.label1);
            this.pnlBill.Location = new System.Drawing.Point(227, 341);
            this.pnlBill.Name = "pnlBill";
            this.pnlBill.Size = new System.Drawing.Size(462, 56);
            this.pnlBill.TabIndex = 22;
            this.pnlBill.Visible = false;
            // 
            // chkDDateFrom
            // 
            this.chkDDateFrom.AutoSize = true;
            this.chkDDateFrom.Location = new System.Drawing.Point(21, 87);
            this.chkDDateFrom.Name = "chkDDateFrom";
            this.chkDDateFrom.Size = new System.Drawing.Size(117, 21);
            this.chkDDateFrom.TabIndex = 20;
            this.chkDDateFrom.Text = "Delivery From";
            this.chkDDateFrom.UseVisualStyleBackColor = true;
            this.chkDDateFrom.CheckedChanged += new System.EventHandler(this.chkDDateFrom_CheckedChanged);
            // 
            // chkDDateTo
            // 
            this.chkDDateTo.AutoSize = true;
            this.chkDDateTo.Location = new System.Drawing.Point(21, 117);
            this.chkDDateTo.Name = "chkDDateTo";
            this.chkDDateTo.Size = new System.Drawing.Size(102, 21);
            this.chkDDateTo.TabIndex = 19;
            this.chkDDateTo.Text = "Delivery To";
            this.chkDDateTo.UseVisualStyleBackColor = true;
            this.chkDDateTo.CheckedChanged += new System.EventHandler(this.chkDDateTo_CheckedChanged);
            // 
            // dtpDTo
            // 
            this.dtpDTo.CustomFormat = "dd-MMM-yy";
            this.dtpDTo.Enabled = false;
            this.dtpDTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDTo.Location = new System.Drawing.Point(173, 113);
            this.dtpDTo.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDTo.Name = "dtpDTo";
            this.dtpDTo.Size = new System.Drawing.Size(170, 22);
            this.dtpDTo.TabIndex = 18;
            // 
            // dtpDFrom
            // 
            this.dtpDFrom.CustomFormat = "dd-MMM-yy";
            this.dtpDFrom.Enabled = false;
            this.dtpDFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDFrom.Location = new System.Drawing.Point(173, 83);
            this.dtpDFrom.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDFrom.Name = "dtpDFrom";
            this.dtpDFrom.Size = new System.Drawing.Size(170, 22);
            this.dtpDFrom.TabIndex = 17;
            // 
            // frmRepairingReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(695, 416);
            this.Controls.Add(this.pnlBill);
            this.Controls.Add(this.rbtReapirBill);
            this.Controls.Add(this.rbtReceiveFromWorker);
            this.Controls.Add(this.rbtRepairingGivenToWorker);
            this.Controls.Add(this.rbtDelivered);
            this.Controls.Add(this.rbtCompleteRepairing);
            this.Controls.Add(this.pnlSampleDateWise);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmRepairingReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRepairingReport";
            this.Load += new System.EventHandler(this.frmRepairingReport_Load);
            this.pnlSampleDateWise.ResumeLayout(false);
            this.pnlSampleDateWise.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnlBill.ResumeLayout(false);
            this.pnlBill.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.RadioButton rbtCompleteRepairing;
        private System.Windows.Forms.Panel pnlSampleDateWise;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.CheckBox chkFrom;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.RadioButton rbtDelivered;
        private System.Windows.Forms.ComboBox cbxWorker;
        private System.Windows.Forms.CheckBox chkWorker;
        private System.Windows.Forms.RadioButton rbtRepairingGivenToWorker;
        private System.Windows.Forms.RadioButton rbtReceiveFromWorker;
        private System.Windows.Forms.RadioButton rbtReapirBill;
        private System.Windows.Forms.TextBox txtReapirBill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlBill;
        private System.Windows.Forms.CheckBox chkDDateFrom;
        private System.Windows.Forms.CheckBox chkDDateTo;
        private System.Windows.Forms.DateTimePicker dtpDTo;
        private System.Windows.Forms.DateTimePicker dtpDFrom;

    }
}