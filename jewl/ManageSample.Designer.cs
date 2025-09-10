namespace jewl
{
    partial class ManageSample
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tbSampleGiven = new System.Windows.Forms.TabPage();
            this.tbSampleReturn = new System.Windows.Forms.TabPage();
            this.txtSampleNo = new System.Windows.Forms.TextBox();
            this.lblSampleNo = new System.Windows.Forms.Label();
            this.pbxPicture = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbxWorkerName = new System.Windows.Forms.ComboBox();
            this.txtHTWt = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtHTQty = new System.Windows.Forms.TextBox();
            this.txtSampleWt = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblWorkerName = new System.Windows.Forms.Label();
            this.lblRtmWeight = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSampleQty = new System.Windows.Forms.TextBox();
            this.cbxItemType = new System.Windows.Forms.ComboBox();
            this.lblWeight = new System.Windows.Forms.Label();
            this.lblTagNumber = new System.Windows.Forms.Label();
            this.txtSmStWt = new System.Windows.Forms.TextBox();
            this.lblGroupItem = new System.Windows.Forms.Label();
            this.txtSmStQty = new System.Windows.Forms.TextBox();
            this.txtKarrat = new System.Windows.Forms.TextBox();
            this.txtStockWt = new System.Windows.Forms.TextBox();
            this.lblKarrat = new System.Windows.Forms.Label();
            this.txtStockQty = new System.Windows.Forms.TextBox();
            this.lblQty = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.cbxTagNumber = new System.Windows.Forms.ComboBox();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtBillBookNo = new System.Windows.Forms.TextBox();
            this.lblBillBookNo = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.dgvItemAdded = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCustId = new System.Windows.Forms.Label();
            this.tbSearchRecord = new System.Windows.Forms.TabPage();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.lblCustomerName = new System.Windows.Forms.Label();
            this.dgvSampleNo = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxSearch = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tbSampleReturn.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemAdded)).BeginInit();
            this.tbSearchRecord.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSampleNo)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tbSampleGiven);
            this.tabControl1.Controls.Add(this.tbSampleReturn);
            this.tabControl1.Controls.Add(this.tbSearchRecord);
            this.tabControl1.Location = new System.Drawing.Point(0, 35);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1150, 410);
            this.tabControl1.TabIndex = 0;
            // 
            // tbSampleGiven
            // 
            this.tbSampleGiven.Location = new System.Drawing.Point(4, 22);
            this.tbSampleGiven.Name = "tbSampleGiven";
            this.tbSampleGiven.Padding = new System.Windows.Forms.Padding(3);
            this.tbSampleGiven.Size = new System.Drawing.Size(1142, 661);
            this.tbSampleGiven.TabIndex = 0;
            this.tbSampleGiven.Text = "tabPage1";
            this.tbSampleGiven.UseVisualStyleBackColor = true;
            // 
            // tbSampleReturn
            // 
            this.tbSampleReturn.Controls.Add(this.txtSampleNo);
            this.tbSampleReturn.Controls.Add(this.lblSampleNo);
            this.tbSampleReturn.Controls.Add(this.pbxPicture);
            this.tbSampleReturn.Controls.Add(this.groupBox3);
            this.tbSampleReturn.Controls.Add(this.dtpDate);
            this.tbSampleReturn.Controls.Add(this.txtBillBookNo);
            this.tbSampleReturn.Controls.Add(this.lblBillBookNo);
            this.tbSampleReturn.Controls.Add(this.lblDate);
            this.tbSampleReturn.Controls.Add(this.groupBox6);
            this.tbSampleReturn.Controls.Add(this.lblCustId);
            this.tbSampleReturn.Location = new System.Drawing.Point(4, 22);
            this.tbSampleReturn.Name = "tbSampleReturn";
            this.tbSampleReturn.Padding = new System.Windows.Forms.Padding(3);
            this.tbSampleReturn.Size = new System.Drawing.Size(1142, 384);
            this.tbSampleReturn.TabIndex = 1;
            this.tbSampleReturn.Text = "Sample Return";
            this.tbSampleReturn.UseVisualStyleBackColor = true;
            // 
            // txtSampleNo
            // 
            this.txtSampleNo.Location = new System.Drawing.Point(250, 20);
            this.txtSampleNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleNo.Name = "txtSampleNo";
            this.txtSampleNo.Size = new System.Drawing.Size(104, 20);
            this.txtSampleNo.TabIndex = 127;
            this.txtSampleNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSampleNo_KeyUp);
            // 
            // lblSampleNo
            // 
            this.lblSampleNo.AutoSize = true;
            this.lblSampleNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleNo.Location = new System.Drawing.Point(155, 25);
            this.lblSampleNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSampleNo.Name = "lblSampleNo";
            this.lblSampleNo.Size = new System.Drawing.Size(68, 13);
            this.lblSampleNo.TabIndex = 128;
            this.lblSampleNo.Text = "Sample No";
            // 
            // pbxPicture
            // 
            this.pbxPicture.Location = new System.Drawing.Point(742, 71);
            this.pbxPicture.Name = "pbxPicture";
            this.pbxPicture.Size = new System.Drawing.Size(208, 278);
            this.pbxPicture.TabIndex = 126;
            this.pbxPicture.TabStop = false;
            this.pbxPicture.Text = "Picture";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbxWorkerName);
            this.groupBox3.Controls.Add(this.txtHTWt);
            this.groupBox3.Controls.Add(this.txtDescription);
            this.groupBox3.Controls.Add(this.txtHTQty);
            this.groupBox3.Controls.Add(this.txtSampleWt);
            this.groupBox3.Controls.Add(this.lblDescription);
            this.groupBox3.Controls.Add(this.lblWorkerName);
            this.groupBox3.Controls.Add(this.lblRtmWeight);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtSampleQty);
            this.groupBox3.Controls.Add(this.cbxItemType);
            this.groupBox3.Controls.Add(this.lblWeight);
            this.groupBox3.Controls.Add(this.lblTagNumber);
            this.groupBox3.Controls.Add(this.txtSmStWt);
            this.groupBox3.Controls.Add(this.lblGroupItem);
            this.groupBox3.Controls.Add(this.txtSmStQty);
            this.groupBox3.Controls.Add(this.txtKarrat);
            this.groupBox3.Controls.Add(this.txtStockWt);
            this.groupBox3.Controls.Add(this.lblKarrat);
            this.groupBox3.Controls.Add(this.txtStockQty);
            this.groupBox3.Controls.Add(this.lblQty);
            this.groupBox3.Controls.Add(this.txtQty);
            this.groupBox3.Controls.Add(this.txtWeight);
            this.groupBox3.Controls.Add(this.cbxTagNumber);
            this.groupBox3.Controls.Add(this.cbxGroupItem);
            this.groupBox3.Location = new System.Drawing.Point(22, 71);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(509, 275);
            this.groupBox3.TabIndex = 125;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Item Information";
            // 
            // cbxWorkerName
            // 
            this.cbxWorkerName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxWorkerName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxWorkerName.FormattingEnabled = true;
            this.cbxWorkerName.Location = new System.Drawing.Point(31, 198);
            this.cbxWorkerName.Margin = new System.Windows.Forms.Padding(4);
            this.cbxWorkerName.Name = "cbxWorkerName";
            this.cbxWorkerName.Size = new System.Drawing.Size(204, 22);
            this.cbxWorkerName.TabIndex = 124;
            // 
            // txtHTWt
            // 
            this.txtHTWt.Location = new System.Drawing.Point(276, 227);
            this.txtHTWt.Margin = new System.Windows.Forms.Padding(4);
            this.txtHTWt.Name = "txtHTWt";
            this.txtHTWt.Size = new System.Drawing.Size(72, 20);
            this.txtHTWt.TabIndex = 109;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(265, 200);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(204, 20);
            this.txtDescription.TabIndex = 121;
            // 
            // txtHTQty
            // 
            this.txtHTQty.Location = new System.Drawing.Point(382, 227);
            this.txtHTQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtHTQty.Name = "txtHTQty";
            this.txtHTQty.Size = new System.Drawing.Size(72, 20);
            this.txtHTQty.TabIndex = 110;
            // 
            // txtSampleWt
            // 
            this.txtSampleWt.Location = new System.Drawing.Point(196, 247);
            this.txtSampleWt.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleWt.Name = "txtSampleWt";
            this.txtSampleWt.Size = new System.Drawing.Size(39, 20);
            this.txtSampleWt.TabIndex = 124;
            this.txtSampleWt.Visible = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(262, 179);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 13);
            this.lblDescription.TabIndex = 123;
            this.lblDescription.Text = "Description";
            // 
            // lblWorkerName
            // 
            this.lblWorkerName.AutoSize = true;
            this.lblWorkerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkerName.Location = new System.Drawing.Point(27, 181);
            this.lblWorkerName.Name = "lblWorkerName";
            this.lblWorkerName.Size = new System.Drawing.Size(80, 13);
            this.lblWorkerName.TabIndex = 122;
            this.lblWorkerName.Text = "Worker Name";
            // 
            // lblRtmWeight
            // 
            this.lblRtmWeight.AutoSize = true;
            this.lblRtmWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRtmWeight.ForeColor = System.Drawing.Color.Red;
            this.lblRtmWeight.Location = new System.Drawing.Point(349, 71);
            this.lblRtmWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRtmWeight.Name = "lblRtmWeight";
            this.lblRtmWeight.Size = new System.Drawing.Size(11, 13);
            this.lblRtmWeight.TabIndex = 119;
            this.lblRtmWeight.Text = "-";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(166, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 19);
            this.label3.TabIndex = 118;
            this.label3.Text = "Item Type";
            // 
            // txtSampleQty
            // 
            this.txtSampleQty.Location = new System.Drawing.Point(196, 225);
            this.txtSampleQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtSampleQty.Name = "txtSampleQty";
            this.txtSampleQty.Size = new System.Drawing.Size(39, 20);
            this.txtSampleQty.TabIndex = 123;
            this.txtSampleQty.Visible = false;
            // 
            // cbxItemType
            // 
            this.cbxItemType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxItemType.FormattingEnabled = true;
            this.cbxItemType.IntegralHeight = false;
            this.cbxItemType.Items.AddRange(new object[] {
            "Gold",
            "Diamond",
            "Silver",
            "Pladium",
            "Platinum"});
            this.cbxItemType.Location = new System.Drawing.Point(31, 33);
            this.cbxItemType.Margin = new System.Windows.Forms.Padding(4);
            this.cbxItemType.Name = "cbxItemType";
            this.cbxItemType.Size = new System.Drawing.Size(204, 21);
            this.cbxItemType.TabIndex = 117;
            // 
            // lblWeight
            // 
            this.lblWeight.AutoSize = true;
            this.lblWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeight.Location = new System.Drawing.Point(264, 70);
            this.lblWeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(45, 13);
            this.lblWeight.TabIndex = 106;
            this.lblWeight.Text = "Weight";
            // 
            // lblTagNumber
            // 
            this.lblTagNumber.AutoSize = true;
            this.lblTagNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagNumber.Location = new System.Drawing.Point(29, 68);
            this.lblTagNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTagNumber.Name = "lblTagNumber";
            this.lblTagNumber.Size = new System.Drawing.Size(73, 13);
            this.lblTagNumber.TabIndex = 105;
            this.lblTagNumber.Text = "Tag Number";
            // 
            // txtSmStWt
            // 
            this.txtSmStWt.Location = new System.Drawing.Point(108, 247);
            this.txtSmStWt.Margin = new System.Windows.Forms.Padding(4);
            this.txtSmStWt.Name = "txtSmStWt";
            this.txtSmStWt.Size = new System.Drawing.Size(70, 20);
            this.txtSmStWt.TabIndex = 121;
            this.txtSmStWt.Visible = false;
            // 
            // lblGroupItem
            // 
            this.lblGroupItem.AutoSize = true;
            this.lblGroupItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupItem.Location = new System.Drawing.Point(264, 16);
            this.lblGroupItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupItem.Name = "lblGroupItem";
            this.lblGroupItem.Size = new System.Drawing.Size(69, 13);
            this.lblGroupItem.TabIndex = 103;
            this.lblGroupItem.Text = "Group Item";
            // 
            // txtSmStQty
            // 
            this.txtSmStQty.Location = new System.Drawing.Point(108, 225);
            this.txtSmStQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtSmStQty.Name = "txtSmStQty";
            this.txtSmStQty.Size = new System.Drawing.Size(70, 20);
            this.txtSmStQty.TabIndex = 120;
            this.txtSmStQty.Visible = false;
            // 
            // txtKarrat
            // 
            this.txtKarrat.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtKarrat.Location = new System.Drawing.Point(267, 144);
            this.txtKarrat.Margin = new System.Windows.Forms.Padding(4);
            this.txtKarrat.Multiline = true;
            this.txtKarrat.Name = "txtKarrat";
            this.txtKarrat.ReadOnly = true;
            this.txtKarrat.Size = new System.Drawing.Size(204, 21);
            this.txtKarrat.TabIndex = 94;
            // 
            // txtStockWt
            // 
            this.txtStockWt.Location = new System.Drawing.Point(39, 248);
            this.txtStockWt.Margin = new System.Windows.Forms.Padding(4);
            this.txtStockWt.Name = "txtStockWt";
            this.txtStockWt.Size = new System.Drawing.Size(60, 20);
            this.txtStockWt.TabIndex = 119;
            this.txtStockWt.Visible = false;
            // 
            // lblKarrat
            // 
            this.lblKarrat.AutoSize = true;
            this.lblKarrat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKarrat.Location = new System.Drawing.Point(262, 127);
            this.lblKarrat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKarrat.Name = "lblKarrat";
            this.lblKarrat.Size = new System.Drawing.Size(41, 13);
            this.lblKarrat.TabIndex = 98;
            this.lblKarrat.Text = "Karrat";
            // 
            // txtStockQty
            // 
            this.txtStockQty.Location = new System.Drawing.Point(39, 226);
            this.txtStockQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtStockQty.Name = "txtStockQty";
            this.txtStockQty.Size = new System.Drawing.Size(60, 20);
            this.txtStockQty.TabIndex = 118;
            this.txtStockQty.Visible = false;
            // 
            // lblQty
            // 
            this.lblQty.AutoSize = true;
            this.lblQty.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQty.Location = new System.Drawing.Point(27, 126);
            this.lblQty.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(26, 13);
            this.lblQty.TabIndex = 97;
            this.lblQty.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(30, 143);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Multiline = true;
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(204, 24);
            this.txtQty.TabIndex = 86;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtWeight.Location = new System.Drawing.Point(267, 88);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtWeight.Multiline = true;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(204, 21);
            this.txtWeight.TabIndex = 85;
            // 
            // cbxTagNumber
            // 
            this.cbxTagNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTagNumber.FormattingEnabled = true;
            this.cbxTagNumber.ItemHeight = 13;
            this.cbxTagNumber.Location = new System.Drawing.Point(31, 88);
            this.cbxTagNumber.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTagNumber.Name = "cbxTagNumber";
            this.cbxTagNumber.Size = new System.Drawing.Size(204, 21);
            this.cbxTagNumber.TabIndex = 84;
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.ItemHeight = 13;
            this.cbxGroupItem.Location = new System.Drawing.Point(267, 34);
            this.cbxGroupItem.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(204, 21);
            this.cbxGroupItem.TabIndex = 81;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd-MMM-yy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(710, 20);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(113, 20);
            this.dtpDate.TabIndex = 114;
            // 
            // txtBillBookNo
            // 
            this.txtBillBookNo.Location = new System.Drawing.Point(502, 20);
            this.txtBillBookNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtBillBookNo.Name = "txtBillBookNo";
            this.txtBillBookNo.Size = new System.Drawing.Size(132, 20);
            this.txtBillBookNo.TabIndex = 113;
            this.txtBillBookNo.Visible = false;
            // 
            // lblBillBookNo
            // 
            this.lblBillBookNo.AutoSize = true;
            this.lblBillBookNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillBookNo.Location = new System.Drawing.Point(398, 25);
            this.lblBillBookNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBillBookNo.Name = "lblBillBookNo";
            this.lblBillBookNo.Size = new System.Drawing.Size(77, 13);
            this.lblBillBookNo.TabIndex = 112;
            this.lblBillBookNo.Text = "Bill Book No";
            this.lblBillBookNo.Visible = false;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(652, 24);
            this.lblDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(34, 13);
            this.lblDate.TabIndex = 111;
            this.lblDate.Text = "Date";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnRemove);
            this.groupBox6.Controls.Add(this.dgvItemAdded);
            this.groupBox6.Location = new System.Drawing.Point(553, 71);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(167, 278);
            this.groupBox6.TabIndex = 110;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Selected Items";
            // 
            // btnRemove
            // 
            this.btnRemove.BackgroundImage = global::jewl.Properties.Resources.removeRow;
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(31, 225);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(100, 40);
            this.btnRemove.TabIndex = 48;
            this.btnRemove.Text = "Remove";
            this.btnRemove.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // dgvItemAdded
            // 
            this.dgvItemAdded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItemAdded.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9});
            this.dgvItemAdded.Location = new System.Drawing.Point(9, 27);
            this.dgvItemAdded.Margin = new System.Windows.Forms.Padding(4);
            this.dgvItemAdded.Name = "dgvItemAdded";
            this.dgvItemAdded.Size = new System.Drawing.Size(149, 190);
            this.dgvItemAdded.TabIndex = 40;
            this.dgvItemAdded.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvItemAdded_CellDoubleClick);
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.HeaderText = "";
            this.Column9.Name = "Column9";
            // 
            // lblCustId
            // 
            this.lblCustId.AutoSize = true;
            this.lblCustId.Location = new System.Drawing.Point(-5, 46);
            this.lblCustId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustId.Name = "lblCustId";
            this.lblCustId.Size = new System.Drawing.Size(0, 13);
            this.lblCustId.TabIndex = 105;
            this.lblCustId.Visible = false;
            // 
            // tbSearchRecord
            // 
            this.tbSearchRecord.Controls.Add(this.cbxSearch);
            this.tbSearchRecord.Controls.Add(this.dgvSampleNo);
            this.tbSearchRecord.Controls.Add(this.lblCustomerName);
            this.tbSearchRecord.Location = new System.Drawing.Point(4, 22);
            this.tbSearchRecord.Name = "tbSearchRecord";
            this.tbSearchRecord.Size = new System.Drawing.Size(1142, 384);
            this.tbSearchRecord.TabIndex = 2;
            this.tbSearchRecord.Text = "Search Record";
            this.tbSearchRecord.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnEdit,
            this.btnReset,
            this.btnDelete,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(1161, 33);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 97;
            // 
            // btnNew
            // 
            this.btnNew.CheckOnClick = true;
            this.btnNew.ForeColor = System.Drawing.Color.Black;
            this.btnNew.Image = global::jewl.Properties.Resources.newfile;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(62, 30);
            this.btnNew.Text = "&New";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::jewl.Properties.Resources._1483476843_Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 30);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.CheckOnClick = true;
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Image = global::jewl.Properties.Resources._1483476345_edit_notes;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 30);
            this.btnEdit.Text = "&Edit";
            // 
            // btnReset
            // 
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Image = global::jewl.Properties.Resources.reset;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(68, 30);
            this.btnReset.Text = "&Reset";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = global::jewl.Properties.Resources.mydelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 30);
            this.btnDelete.Text = "&Delete";
            // 
            // btnExit
            // 
            this.btnExit.AutoSize = false;
            this.btnExit.CheckOnClick = true;
            this.btnExit.ForeColor = System.Drawing.Color.Black;
            this.btnExit.Image = global::jewl.Properties.Resources._1483476602_Cancel;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(68, 30);
            this.btnExit.Text = "&Close";
            // 
            // lblCustomerName
            // 
            this.lblCustomerName.AutoSize = true;
            this.lblCustomerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCustomerName.Location = new System.Drawing.Point(147, 179);
            this.lblCustomerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCustomerName.Name = "lblCustomerName";
            this.lblCustomerName.Size = new System.Drawing.Size(47, 13);
            this.lblCustomerName.TabIndex = 54;
            this.lblCustomerName.Text = "Search";
            // 
            // dgvSampleNo
            // 
            this.dgvSampleNo.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgvSampleNo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSampleNo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column8});
            this.dgvSampleNo.Location = new System.Drawing.Point(475, 25);
            this.dgvSampleNo.Name = "dgvSampleNo";
            this.dgvSampleNo.RowHeadersVisible = false;
            this.dgvSampleNo.Size = new System.Drawing.Size(224, 287);
            this.dgvSampleNo.TabIndex = 56;
            this.dgvSampleNo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSampleNo_CellDoubleClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "SampleNo";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "SampleDate";
            this.Column8.Name = "Column8";
            // 
            // cbxSearch
            // 
            this.cbxSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSearch.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cbxSearch.FormattingEnabled = true;
            this.cbxSearch.ItemHeight = 13;
            this.cbxSearch.Location = new System.Drawing.Point(220, 176);
            this.cbxSearch.Margin = new System.Windows.Forms.Padding(4);
            this.cbxSearch.Name = "cbxSearch";
            this.cbxSearch.Size = new System.Drawing.Size(204, 21);
            this.cbxSearch.TabIndex = 82;
            this.cbxSearch.SelectionChangeCommitted += new System.EventHandler(this.cbxSearch_SelectionChangeCommitted);
            this.cbxSearch.SelectedIndexChanged += new System.EventHandler(this.cbxSearch_SelectedIndexChanged);
            this.cbxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxSearch_KeyDown);
            // 
            // ManageSample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1161, 447);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "ManageSample";
            this.Text = "Manage_Sample";
            this.Load += new System.EventHandler(this.ManageSample_Load);
            this.tabControl1.ResumeLayout(false);
            this.tbSampleReturn.ResumeLayout(false);
            this.tbSampleReturn.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItemAdded)).EndInit();
            this.tbSearchRecord.ResumeLayout(false);
            this.tbSearchRecord.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSampleNo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tbSampleGiven;
        private System.Windows.Forms.TabPage tbSampleReturn;
        private System.Windows.Forms.TabPage tbSearchRecord;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.TextBox txtHTWt;
        private System.Windows.Forms.TextBox txtHTQty;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.DataGridView dgvItemAdded;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.TextBox txtSampleWt;
        private System.Windows.Forms.TextBox txtSampleQty;
        private System.Windows.Forms.TextBox txtSmStWt;
        private System.Windows.Forms.TextBox txtSmStQty;
        private System.Windows.Forms.TextBox txtStockWt;
        private System.Windows.Forms.TextBox txtStockQty;
        private System.Windows.Forms.Label lblCustId;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtBillBookNo;
        private System.Windows.Forms.Label lblBillBookNo;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbxWorkerName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblWorkerName;
        private System.Windows.Forms.Label lblRtmWeight;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxItemType;
        private System.Windows.Forms.Label lblWeight;
        private System.Windows.Forms.Label lblTagNumber;
        private System.Windows.Forms.Label lblGroupItem;
        private System.Windows.Forms.TextBox txtKarrat;
        private System.Windows.Forms.Label lblKarrat;
        private System.Windows.Forms.Label lblQty;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.ComboBox cbxTagNumber;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.GroupBox pbxPicture;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtSampleNo;
        private System.Windows.Forms.Label lblSampleNo;
        private System.Windows.Forms.Label lblCustomerName;
        private System.Windows.Forms.DataGridView dgvSampleNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.ComboBox cbxSearch;
    }
}