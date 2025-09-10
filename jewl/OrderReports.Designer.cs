namespace jewl
{
    partial class OrderReports
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
            this.rbtOrderEstimate = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.rbtOrderSale = new System.Windows.Forms.RadioButton();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlOrderBalance = new System.Windows.Forms.Panel();
            this.txtBOrderNo = new System.Windows.Forms.TextBox();
            this.txtAdv = new System.Windows.Forms.TextBox();
            this.rbtAdv = new System.Windows.Forms.RadioButton();
            this.pnlOrderNo = new System.Windows.Forms.Panel();
            this.chkTo = new System.Windows.Forms.CheckBox();
            this.chkFrom = new System.Windows.Forms.CheckBox();
            this.txtTOrderNo = new System.Windows.Forms.TextBox();
            this.txtFOrderNo = new System.Windows.Forms.TextBox();
            this.pnlOrderItmWise = new System.Windows.Forms.Panel();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.txtBillBookNo = new System.Windows.Forms.TextBox();
            this.pnlDateWise = new System.Windows.Forms.Panel();
            this.chkDTo = new System.Windows.Forms.CheckBox();
            this.chkDFrom = new System.Windows.Forms.CheckBox();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.rbtBillBookNo = new System.Windows.Forms.RadioButton();
            this.rbtJobCard = new System.Windows.Forms.RadioButton();
            this.rbtSaleDate = new System.Windows.Forms.RadioButton();
            this.rbtDeliveryDate = new System.Windows.Forms.RadioButton();
            this.rbtOrderDate = new System.Windows.Forms.RadioButton();
            this.rbtOrderNo = new System.Windows.Forms.RadioButton();
            this.rbtItemName = new System.Windows.Forms.RadioButton();
            this.rbtCompleteRpt = new System.Windows.Forms.RadioButton();
            this.rbtBill = new System.Windows.Forms.RadioButton();
            this.lblName = new System.Windows.Forms.Label();
            this.rbtPendingOrder = new System.Windows.Forms.RadioButton();
            this.pnlPendingOrder = new System.Windows.Forms.Panel();
            this.rbtPendingONo = new System.Windows.Forms.RadioButton();
            this.rbtPOrderWorkerWise = new System.Windows.Forms.RadioButton();
            this.rbtPendingOrd = new System.Windows.Forms.RadioButton();
            this.cbxWorker = new System.Windows.Forms.ComboBox();
            this.rbtOrderBalance = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlOrderBalance.SuspendLayout();
            this.pnlOrderNo.SuspendLayout();
            this.pnlOrderItmWise.SuspendLayout();
            this.pnlDateWise.SuspendLayout();
            this.pnlPendingOrder.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtOrderEstimate
            // 
            this.rbtOrderEstimate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtOrderEstimate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOrderEstimate.Location = new System.Drawing.Point(18, 70);
            this.rbtOrderEstimate.Name = "rbtOrderEstimate";
            this.rbtOrderEstimate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtOrderEstimate.Size = new System.Drawing.Size(114, 24);
            this.rbtOrderEstimate.TabIndex = 10;
            this.rbtOrderEstimate.TabStop = true;
            this.rbtOrderEstimate.Text = "Order Estimate";
            this.rbtOrderEstimate.UseVisualStyleBackColor = false;
            this.rbtOrderEstimate.CheckedChanged += new System.EventHandler(this.rbtOrderEstimate_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Khaki;
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Location = new System.Drawing.Point(8, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 51);
            this.panel1.TabIndex = 9;
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
            this.lblTitle.Size = new System.Drawing.Size(492, 49);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Order Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(245, 454);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 29);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(117, 454);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 29);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // rbtOrderSale
            // 
            this.rbtOrderSale.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtOrderSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOrderSale.Location = new System.Drawing.Point(134, 70);
            this.rbtOrderSale.Name = "rbtOrderSale";
            this.rbtOrderSale.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtOrderSale.Size = new System.Drawing.Size(114, 24);
            this.rbtOrderSale.TabIndex = 11;
            this.rbtOrderSale.TabStop = true;
            this.rbtOrderSale.Text = "Order Sale";
            this.rbtOrderSale.UseVisualStyleBackColor = false;
            this.rbtOrderSale.CheckedChanged += new System.EventHandler(this.rbtOrderSale_CheckedChanged);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMain.Controls.Add(this.pnlOrderBalance);
            this.pnlMain.Controls.Add(this.txtAdv);
            this.pnlMain.Controls.Add(this.rbtAdv);
            this.pnlMain.Controls.Add(this.pnlOrderNo);
            this.pnlMain.Controls.Add(this.pnlOrderItmWise);
            this.pnlMain.Controls.Add(this.txtBillBookNo);
            this.pnlMain.Controls.Add(this.pnlDateWise);
            this.pnlMain.Controls.Add(this.rbtBillBookNo);
            this.pnlMain.Controls.Add(this.rbtJobCard);
            this.pnlMain.Controls.Add(this.rbtSaleDate);
            this.pnlMain.Controls.Add(this.rbtDeliveryDate);
            this.pnlMain.Controls.Add(this.rbtOrderDate);
            this.pnlMain.Controls.Add(this.rbtOrderNo);
            this.pnlMain.Controls.Add(this.rbtItemName);
            this.pnlMain.Controls.Add(this.rbtCompleteRpt);
            this.pnlMain.Controls.Add(this.rbtBill);
            this.pnlMain.Location = new System.Drawing.Point(12, 127);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(478, 318);
            this.pnlMain.TabIndex = 13;
            this.pnlMain.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMain_Paint);
            // 
            // pnlOrderBalance
            // 
            this.pnlOrderBalance.Controls.Add(this.txtBOrderNo);
            this.pnlOrderBalance.Location = new System.Drawing.Point(165, 18);
            this.pnlOrderBalance.Name = "pnlOrderBalance";
            this.pnlOrderBalance.Size = new System.Drawing.Size(251, 42);
            this.pnlOrderBalance.TabIndex = 34;
            // 
            // txtBOrderNo
            // 
            this.txtBOrderNo.Location = new System.Drawing.Point(19, 10);
            this.txtBOrderNo.Name = "txtBOrderNo";
            this.txtBOrderNo.Size = new System.Drawing.Size(156, 20);
            this.txtBOrderNo.TabIndex = 1;
            // 
            // txtAdv
            // 
            this.txtAdv.Location = new System.Drawing.Point(184, 277);
            this.txtAdv.Name = "txtAdv";
            this.txtAdv.Size = new System.Drawing.Size(156, 20);
            this.txtAdv.TabIndex = 28;
            this.txtAdv.Visible = false;
            // 
            // rbtAdv
            // 
            this.rbtAdv.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtAdv.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtAdv.Location = new System.Drawing.Point(13, 277);
            this.rbtAdv.Name = "rbtAdv";
            this.rbtAdv.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtAdv.Size = new System.Drawing.Size(125, 24);
            this.rbtAdv.TabIndex = 27;
            this.rbtAdv.TabStop = true;
            this.rbtAdv.Text = "Order Advance";
            this.rbtAdv.UseVisualStyleBackColor = false;
            this.rbtAdv.Visible = false;
            this.rbtAdv.CheckedChanged += new System.EventHandler(this.rbtAdv_CheckedChanged);
            // 
            // pnlOrderNo
            // 
            this.pnlOrderNo.Controls.Add(this.chkTo);
            this.pnlOrderNo.Controls.Add(this.chkFrom);
            this.pnlOrderNo.Controls.Add(this.txtTOrderNo);
            this.pnlOrderNo.Controls.Add(this.txtFOrderNo);
            this.pnlOrderNo.Location = new System.Drawing.Point(165, 110);
            this.pnlOrderNo.Name = "pnlOrderNo";
            this.pnlOrderNo.Size = new System.Drawing.Size(251, 32);
            this.pnlOrderNo.TabIndex = 22;
            // 
            // chkTo
            // 
            this.chkTo.AutoSize = true;
            this.chkTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTo.Location = new System.Drawing.Point(127, 10);
            this.chkTo.Name = "chkTo";
            this.chkTo.Size = new System.Drawing.Size(41, 17);
            this.chkTo.TabIndex = 24;
            this.chkTo.Text = "To";
            this.chkTo.UseVisualStyleBackColor = true;
            this.chkTo.CheckedChanged += new System.EventHandler(this.chkTo_CheckedChanged);
            // 
            // chkFrom
            // 
            this.chkFrom.AutoSize = true;
            this.chkFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFrom.Location = new System.Drawing.Point(3, 9);
            this.chkFrom.Name = "chkFrom";
            this.chkFrom.Size = new System.Drawing.Size(53, 17);
            this.chkFrom.TabIndex = 23;
            this.chkFrom.Text = "From";
            this.chkFrom.UseVisualStyleBackColor = true;
            this.chkFrom.CheckedChanged += new System.EventHandler(this.chkFrom_CheckedChanged);
            // 
            // txtTOrderNo
            // 
            this.txtTOrderNo.Enabled = false;
            this.txtTOrderNo.Location = new System.Drawing.Point(175, 7);
            this.txtTOrderNo.Name = "txtTOrderNo";
            this.txtTOrderNo.Size = new System.Drawing.Size(64, 20);
            this.txtTOrderNo.TabIndex = 22;
            // 
            // txtFOrderNo
            // 
            this.txtFOrderNo.Enabled = false;
            this.txtFOrderNo.Location = new System.Drawing.Point(57, 7);
            this.txtFOrderNo.Name = "txtFOrderNo";
            this.txtFOrderNo.Size = new System.Drawing.Size(64, 20);
            this.txtFOrderNo.TabIndex = 21;
            // 
            // pnlOrderItmWise
            // 
            this.pnlOrderItmWise.Controls.Add(this.cbxGroupItem);
            this.pnlOrderItmWise.Location = new System.Drawing.Point(165, 71);
            this.pnlOrderItmWise.Name = "pnlOrderItmWise";
            this.pnlOrderItmWise.Size = new System.Drawing.Size(251, 32);
            this.pnlOrderItmWise.TabIndex = 21;
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(19, 5);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(156, 21);
            this.cbxGroupItem.TabIndex = 5;
            // 
            // txtBillBookNo
            // 
            this.txtBillBookNo.Location = new System.Drawing.Point(186, 241);
            this.txtBillBookNo.Name = "txtBillBookNo";
            this.txtBillBookNo.Size = new System.Drawing.Size(156, 20);
            this.txtBillBookNo.TabIndex = 24;
            this.txtBillBookNo.Visible = false;
            // 
            // pnlDateWise
            // 
            this.pnlDateWise.Controls.Add(this.chkDTo);
            this.pnlDateWise.Controls.Add(this.chkDFrom);
            this.pnlDateWise.Controls.Add(this.dtpTo);
            this.pnlDateWise.Controls.Add(this.dtpFrom);
            this.pnlDateWise.Location = new System.Drawing.Point(165, 146);
            this.pnlDateWise.Name = "pnlDateWise";
            this.pnlDateWise.Size = new System.Drawing.Size(142, 57);
            this.pnlDateWise.TabIndex = 23;
            // 
            // chkDTo
            // 
            this.chkDTo.AutoSize = true;
            this.chkDTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDTo.Location = new System.Drawing.Point(3, 32);
            this.chkDTo.Name = "chkDTo";
            this.chkDTo.Size = new System.Drawing.Size(41, 17);
            this.chkDTo.TabIndex = 25;
            this.chkDTo.Text = "To";
            this.chkDTo.UseVisualStyleBackColor = true;
            this.chkDTo.CheckedChanged += new System.EventHandler(this.chkDTo_CheckedChanged);
            // 
            // chkDFrom
            // 
            this.chkDFrom.AutoSize = true;
            this.chkDFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDFrom.Location = new System.Drawing.Point(3, 9);
            this.chkDFrom.Name = "chkDFrom";
            this.chkDFrom.Size = new System.Drawing.Size(53, 17);
            this.chkDFrom.TabIndex = 24;
            this.chkDFrom.Text = "From";
            this.chkDFrom.UseVisualStyleBackColor = true;
            this.chkDFrom.CheckedChanged += new System.EventHandler(this.chkDFrom_CheckedChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yy";
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(61, 29);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(76, 20);
            this.dtpTo.TabIndex = 10;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yy";
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(62, 5);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(76, 20);
            this.dtpFrom.TabIndex = 9;
            // 
            // rbtBillBookNo
            // 
            this.rbtBillBookNo.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtBillBookNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtBillBookNo.Location = new System.Drawing.Point(13, 247);
            this.rbtBillBookNo.Name = "rbtBillBookNo";
            this.rbtBillBookNo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtBillBookNo.Size = new System.Drawing.Size(125, 24);
            this.rbtBillBookNo.TabIndex = 19;
            this.rbtBillBookNo.TabStop = true;
            this.rbtBillBookNo.Text = "Bill Book No";
            this.rbtBillBookNo.UseVisualStyleBackColor = false;
            this.rbtBillBookNo.CheckedChanged += new System.EventHandler(this.rbtBillBookNo_CheckedChanged);
            // 
            // rbtJobCard
            // 
            this.rbtJobCard.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtJobCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtJobCard.Location = new System.Drawing.Point(13, 218);
            this.rbtJobCard.Name = "rbtJobCard";
            this.rbtJobCard.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtJobCard.Size = new System.Drawing.Size(125, 24);
            this.rbtJobCard.TabIndex = 18;
            this.rbtJobCard.TabStop = true;
            this.rbtJobCard.Text = "Job Card";
            this.rbtJobCard.UseVisualStyleBackColor = false;
            this.rbtJobCard.Visible = false;
            this.rbtJobCard.CheckedChanged += new System.EventHandler(this.rbtJobCard_CheckedChanged);
            // 
            // rbtSaleDate
            // 
            this.rbtSaleDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSaleDate.Enabled = false;
            this.rbtSaleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSaleDate.Location = new System.Drawing.Point(13, 189);
            this.rbtSaleDate.Name = "rbtSaleDate";
            this.rbtSaleDate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtSaleDate.Size = new System.Drawing.Size(125, 24);
            this.rbtSaleDate.TabIndex = 17;
            this.rbtSaleDate.TabStop = true;
            this.rbtSaleDate.Text = "Sale Date";
            this.rbtSaleDate.UseVisualStyleBackColor = false;
            this.rbtSaleDate.CheckedChanged += new System.EventHandler(this.rbtSaleDate_CheckedChanged);
            // 
            // rbtDeliveryDate
            // 
            this.rbtDeliveryDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDeliveryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDeliveryDate.Location = new System.Drawing.Point(13, 160);
            this.rbtDeliveryDate.Name = "rbtDeliveryDate";
            this.rbtDeliveryDate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtDeliveryDate.Size = new System.Drawing.Size(125, 24);
            this.rbtDeliveryDate.TabIndex = 16;
            this.rbtDeliveryDate.TabStop = true;
            this.rbtDeliveryDate.Text = "Delivery Date";
            this.rbtDeliveryDate.UseVisualStyleBackColor = false;
            this.rbtDeliveryDate.CheckedChanged += new System.EventHandler(this.rbtDeliveryDate_CheckedChanged);
            // 
            // rbtOrderDate
            // 
            this.rbtOrderDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtOrderDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOrderDate.Location = new System.Drawing.Point(13, 131);
            this.rbtOrderDate.Name = "rbtOrderDate";
            this.rbtOrderDate.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtOrderDate.Size = new System.Drawing.Size(125, 24);
            this.rbtOrderDate.TabIndex = 15;
            this.rbtOrderDate.TabStop = true;
            this.rbtOrderDate.Text = "Order Date";
            this.rbtOrderDate.UseVisualStyleBackColor = false;
            this.rbtOrderDate.CheckedChanged += new System.EventHandler(this.rbtOrderDate_CheckedChanged);
            // 
            // rbtOrderNo
            // 
            this.rbtOrderNo.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtOrderNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOrderNo.Location = new System.Drawing.Point(13, 102);
            this.rbtOrderNo.Name = "rbtOrderNo";
            this.rbtOrderNo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtOrderNo.Size = new System.Drawing.Size(125, 24);
            this.rbtOrderNo.TabIndex = 14;
            this.rbtOrderNo.TabStop = true;
            this.rbtOrderNo.Text = "Order No";
            this.rbtOrderNo.UseVisualStyleBackColor = false;
            this.rbtOrderNo.CheckedChanged += new System.EventHandler(this.rbtOrderNo_CheckedChanged);
            // 
            // rbtItemName
            // 
            this.rbtItemName.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtItemName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemName.Location = new System.Drawing.Point(13, 73);
            this.rbtItemName.Name = "rbtItemName";
            this.rbtItemName.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtItemName.Size = new System.Drawing.Size(125, 24);
            this.rbtItemName.TabIndex = 13;
            this.rbtItemName.TabStop = true;
            this.rbtItemName.Text = "Item Name";
            this.rbtItemName.UseVisualStyleBackColor = false;
            this.rbtItemName.CheckedChanged += new System.EventHandler(this.rbtItemName_CheckedChanged);
            // 
            // rbtCompleteRpt
            // 
            this.rbtCompleteRpt.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtCompleteRpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCompleteRpt.Location = new System.Drawing.Point(13, 44);
            this.rbtCompleteRpt.Name = "rbtCompleteRpt";
            this.rbtCompleteRpt.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtCompleteRpt.Size = new System.Drawing.Size(125, 24);
            this.rbtCompleteRpt.TabIndex = 12;
            this.rbtCompleteRpt.TabStop = true;
            this.rbtCompleteRpt.Text = "Complete Report";
            this.rbtCompleteRpt.UseVisualStyleBackColor = false;
            // 
            // rbtBill
            // 
            this.rbtBill.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtBill.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtBill.Location = new System.Drawing.Point(13, 15);
            this.rbtBill.Name = "rbtBill";
            this.rbtBill.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtBill.Size = new System.Drawing.Size(125, 24);
            this.rbtBill.TabIndex = 11;
            this.rbtBill.TabStop = true;
            this.rbtBill.Text = "Bill";
            this.rbtBill.UseVisualStyleBackColor = false;
            this.rbtBill.CheckedChanged += new System.EventHandler(this.rbtBill_CheckedChanged);
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Brown;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.3F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(9, 102);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(159, 23);
            this.lblName.TabIndex = 14;
            this.lblName.Text = "Order Estimate Reports";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbtPendingOrder
            // 
            this.rbtPendingOrder.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtPendingOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPendingOrder.Location = new System.Drawing.Point(260, 70);
            this.rbtPendingOrder.Name = "rbtPendingOrder";
            this.rbtPendingOrder.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtPendingOrder.Size = new System.Drawing.Size(114, 24);
            this.rbtPendingOrder.TabIndex = 15;
            this.rbtPendingOrder.TabStop = true;
            this.rbtPendingOrder.Text = "Pending Order";
            this.rbtPendingOrder.UseVisualStyleBackColor = false;
            this.rbtPendingOrder.Visible = false;
            this.rbtPendingOrder.CheckedChanged += new System.EventHandler(this.rbtPendingOrder_CheckedChanged);
            // 
            // pnlPendingOrder
            // 
            this.pnlPendingOrder.Controls.Add(this.rbtPendingONo);
            this.pnlPendingOrder.Controls.Add(this.rbtPOrderWorkerWise);
            this.pnlPendingOrder.Controls.Add(this.rbtPendingOrd);
            this.pnlPendingOrder.Controls.Add(this.cbxWorker);
            this.pnlPendingOrder.Location = new System.Drawing.Point(172, 103);
            this.pnlPendingOrder.Name = "pnlPendingOrder";
            this.pnlPendingOrder.Size = new System.Drawing.Size(319, 260);
            this.pnlPendingOrder.TabIndex = 29;
            this.pnlPendingOrder.Visible = false;
            // 
            // rbtPendingONo
            // 
            this.rbtPendingONo.AutoSize = true;
            this.rbtPendingONo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPendingONo.Location = new System.Drawing.Point(21, 91);
            this.rbtPendingONo.Name = "rbtPendingONo";
            this.rbtPendingONo.Size = new System.Drawing.Size(126, 17);
            this.rbtPendingONo.TabIndex = 32;
            this.rbtPendingONo.TabStop = true;
            this.rbtPendingONo.Text = "Pending Order No";
            this.rbtPendingONo.UseVisualStyleBackColor = true;
            // 
            // rbtPOrderWorkerWise
            // 
            this.rbtPOrderWorkerWise.AutoSize = true;
            this.rbtPOrderWorkerWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPOrderWorkerWise.Location = new System.Drawing.Point(21, 137);
            this.rbtPOrderWorkerWise.Name = "rbtPOrderWorkerWise";
            this.rbtPOrderWorkerWise.Size = new System.Drawing.Size(183, 17);
            this.rbtPOrderWorkerWise.TabIndex = 31;
            this.rbtPOrderWorkerWise.TabStop = true;
            this.rbtPOrderWorkerWise.Text = "Pending Order Worker Wise";
            this.rbtPOrderWorkerWise.UseVisualStyleBackColor = true;
            this.rbtPOrderWorkerWise.CheckedChanged += new System.EventHandler(this.rbtPOrderWorkerWise_CheckedChanged);
            // 
            // rbtPendingOrd
            // 
            this.rbtPendingOrd.AutoSize = true;
            this.rbtPendingOrd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPendingOrd.Location = new System.Drawing.Point(21, 114);
            this.rbtPendingOrd.Name = "rbtPendingOrd";
            this.rbtPendingOrd.Size = new System.Drawing.Size(106, 17);
            this.rbtPendingOrd.TabIndex = 30;
            this.rbtPendingOrd.TabStop = true;
            this.rbtPendingOrd.Text = "Pending Order";
            this.rbtPendingOrd.UseVisualStyleBackColor = true;
            // 
            // cbxWorker
            // 
            this.cbxWorker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWorker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxWorker.FormattingEnabled = true;
            this.cbxWorker.Location = new System.Drawing.Point(21, 168);
            this.cbxWorker.Name = "cbxWorker";
            this.cbxWorker.Size = new System.Drawing.Size(150, 21);
            this.cbxWorker.TabIndex = 28;
            this.cbxWorker.SelectedIndexChanged += new System.EventHandler(this.cbxWorker_SelectedIndexChanged);
            // 
            // rbtOrderBalance
            // 
            this.rbtOrderBalance.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtOrderBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOrderBalance.Location = new System.Drawing.Point(386, 70);
            this.rbtOrderBalance.Name = "rbtOrderBalance";
            this.rbtOrderBalance.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.rbtOrderBalance.Size = new System.Drawing.Size(106, 24);
            this.rbtOrderBalance.TabIndex = 30;
            this.rbtOrderBalance.TabStop = true;
            this.rbtOrderBalance.Text = "Order Balance";
            this.rbtOrderBalance.UseVisualStyleBackColor = false;
            this.rbtOrderBalance.Visible = false;
            this.rbtOrderBalance.CheckedChanged += new System.EventHandler(this.rbtOrderBalance_CheckedChanged);
            // 
            // OrderReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(510, 500);
            this.Controls.Add(this.pnlPendingOrder);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.rbtOrderBalance);
            this.Controls.Add(this.rbtPendingOrder);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.rbtOrderSale);
            this.Controls.Add(this.rbtOrderEstimate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlMain);
            this.Name = "OrderReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmOrderReports";
            this.Load += new System.EventHandler(this.frmOrderReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.OrderReports_Paint);
            this.panel1.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlOrderBalance.ResumeLayout(false);
            this.pnlOrderBalance.PerformLayout();
            this.pnlOrderNo.ResumeLayout(false);
            this.pnlOrderNo.PerformLayout();
            this.pnlOrderItmWise.ResumeLayout(false);
            this.pnlDateWise.ResumeLayout(false);
            this.pnlDateWise.PerformLayout();
            this.pnlPendingOrder.ResumeLayout(false);
            this.pnlPendingOrder.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtOrderEstimate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.RadioButton rbtOrderSale;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.RadioButton rbtJobCard;
        private System.Windows.Forms.RadioButton rbtSaleDate;
        private System.Windows.Forms.RadioButton rbtDeliveryDate;
        private System.Windows.Forms.RadioButton rbtOrderDate;
        private System.Windows.Forms.RadioButton rbtOrderNo;
        private System.Windows.Forms.RadioButton rbtItemName;
        private System.Windows.Forms.RadioButton rbtCompleteRpt;
        private System.Windows.Forms.RadioButton rbtBill;
        private System.Windows.Forms.RadioButton rbtBillBookNo;
        private System.Windows.Forms.Panel pnlOrderItmWise;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Panel pnlOrderNo;
        private System.Windows.Forms.TextBox txtTOrderNo;
        private System.Windows.Forms.TextBox txtFOrderNo;
        private System.Windows.Forms.Panel pnlDateWise;
        private System.Windows.Forms.TextBox txtBillBookNo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.CheckBox chkTo;
        private System.Windows.Forms.CheckBox chkFrom;
        private System.Windows.Forms.CheckBox chkDTo;
        private System.Windows.Forms.CheckBox chkDFrom;
        private System.Windows.Forms.RadioButton rbtPendingOrder;
        private System.Windows.Forms.Panel pnlPendingOrder;
        private System.Windows.Forms.RadioButton rbtPOrderWorkerWise;
        private System.Windows.Forms.RadioButton rbtPendingOrd;
        private System.Windows.Forms.ComboBox cbxWorker;
        private System.Windows.Forms.RadioButton rbtOrderBalance;
        private System.Windows.Forms.Panel pnlOrderBalance;
        private System.Windows.Forms.TextBox txtBOrderNo;
        private System.Windows.Forms.RadioButton rbtPendingONo;
        private System.Windows.Forms.TextBox txtAdv;
        private System.Windows.Forms.RadioButton rbtAdv;
        private System.Windows.Forms.Label lblTitle;
    }
}