namespace jewl
{
    partial class frmStockCheck
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
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemTotalWeight = new System.Windows.Forms.TextBox();
            this.txtItemTotalNetWeight = new System.Windows.Forms.TextBox();
            this.txtItemTotalWaste = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.txtTotalNetWeight = new System.Windows.Forms.TextBox();
            this.txtNetTotalWeight = new System.Windows.Forms.TextBox();
            this.txtTotalWaste = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtTagNo = new System.Windows.Forms.TextBox();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.txtDate = new System.Windows.Forms.TextBox();
            this.txtWasteInGm = new System.Windows.Forms.TextBox();
            this.txtNetWeight = new System.Windows.Forms.TextBox();
            this.txtKarat = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtWasteInPercent = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTotalWeight = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.btnSelectedItem = new System.Windows.Forms.Button();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.cbxOldScan = new System.Windows.Forms.ComboBox();
            this.txtNewScan = new System.Windows.Forms.TextBox();
            this.rbtNewScan = new System.Windows.Forms.RadioButton();
            this.rbtOldScan = new System.Windows.Forms.RadioButton();
            this.pbxPicture = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnItemPrint = new System.Windows.Forms.ToolStripButton();
            this.btnComplete = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblSessionNo = new System.Windows.Forms.Label();
            this.lblItemId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxPicture)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTitle.Location = new System.Drawing.Point(0, 32);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1214, 50);
            this.lblTitle.TabIndex = 78;
            this.lblTitle.Text = "Stock Check";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBarCode
            // 
            this.txtBarCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBarCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBarCode.Location = new System.Drawing.Point(209, 147);
            this.txtBarCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(128, 22);
            this.txtBarCode.TabIndex = 88;
            this.txtBarCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBarCode_KeyPress);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(51, 146);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 23);
            this.label2.TabIndex = 89;
            this.label2.Text = "Bar Code";
            // 
            // txtItemTotalWeight
            // 
            this.txtItemTotalWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtItemTotalWeight.Location = new System.Drawing.Point(190, 86);
            this.txtItemTotalWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemTotalWeight.Name = "txtItemTotalWeight";
            this.txtItemTotalWeight.Size = new System.Drawing.Size(128, 23);
            this.txtItemTotalWeight.TabIndex = 102;
            // 
            // txtItemTotalNetWeight
            // 
            this.txtItemTotalNetWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtItemTotalNetWeight.Location = new System.Drawing.Point(190, 24);
            this.txtItemTotalNetWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemTotalNetWeight.Name = "txtItemTotalNetWeight";
            this.txtItemTotalNetWeight.Size = new System.Drawing.Size(128, 23);
            this.txtItemTotalNetWeight.TabIndex = 101;
            // 
            // txtItemTotalWaste
            // 
            this.txtItemTotalWaste.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtItemTotalWaste.Location = new System.Drawing.Point(190, 54);
            this.txtItemTotalWaste.Margin = new System.Windows.Forms.Padding(4);
            this.txtItemTotalWaste.Name = "txtItemTotalWaste";
            this.txtItemTotalWaste.Size = new System.Drawing.Size(128, 23);
            this.txtItemTotalWaste.TabIndex = 100;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(26, 27);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(158, 19);
            this.label23.TabIndex = 99;
            this.label23.Text = "Item Total Net Weight";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(26, 90);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(144, 19);
            this.label22.TabIndex = 98;
            this.label22.Text = "Item Total Weight";
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(26, 58);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(133, 19);
            this.label21.TabIndex = 97;
            this.label21.Text = "ItemTotal Waste";
            // 
            // txtTotalNetWeight
            // 
            this.txtTotalNetWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalNetWeight.Location = new System.Drawing.Point(190, 23);
            this.txtTotalNetWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalNetWeight.Name = "txtTotalNetWeight";
            this.txtTotalNetWeight.Size = new System.Drawing.Size(128, 23);
            this.txtTotalNetWeight.TabIndex = 102;
            // 
            // txtNetTotalWeight
            // 
            this.txtNetTotalWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtNetTotalWeight.Location = new System.Drawing.Point(190, 86);
            this.txtNetTotalWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtNetTotalWeight.Name = "txtNetTotalWeight";
            this.txtNetTotalWeight.Size = new System.Drawing.Size(128, 23);
            this.txtNetTotalWeight.TabIndex = 101;
            // 
            // txtTotalWaste
            // 
            this.txtTotalWaste.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalWaste.Location = new System.Drawing.Point(190, 54);
            this.txtTotalWaste.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalWaste.Name = "txtTotalWaste";
            this.txtTotalWaste.Size = new System.Drawing.Size(128, 23);
            this.txtTotalWaste.TabIndex = 100;
            // 
            // label26
            // 
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(26, 27);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(139, 19);
            this.label26.TabIndex = 99;
            this.label26.Text = "Total Net Weight";
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(26, 57);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 19);
            this.label25.TabIndex = 98;
            this.label25.Text = "Total Waste";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(26, 89);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(107, 19);
            this.label24.TabIndex = 97;
            this.label24.Text = "Total Weight";
            // 
            // txtTagNo
            // 
            this.txtTagNo.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTagNo.Location = new System.Drawing.Point(190, 62);
            this.txtTagNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtTagNo.Name = "txtTagNo";
            this.txtTagNo.ReadOnly = true;
            this.txtTagNo.Size = new System.Drawing.Size(128, 23);
            this.txtTagNo.TabIndex = 107;
            // 
            // txtQty
            // 
            this.txtQty.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtQty.Location = new System.Drawing.Point(470, 153);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4);
            this.txtQty.Name = "txtQty";
            this.txtQty.ReadOnly = true;
            this.txtQty.Size = new System.Drawing.Size(128, 23);
            this.txtQty.TabIndex = 106;
            // 
            // txtDate
            // 
            this.txtDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtDate.Location = new System.Drawing.Point(470, 124);
            this.txtDate.Margin = new System.Windows.Forms.Padding(4);
            this.txtDate.Name = "txtDate";
            this.txtDate.ReadOnly = true;
            this.txtDate.Size = new System.Drawing.Size(128, 23);
            this.txtDate.TabIndex = 105;
            // 
            // txtWasteInGm
            // 
            this.txtWasteInGm.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtWasteInGm.Location = new System.Drawing.Point(190, 94);
            this.txtWasteInGm.Margin = new System.Windows.Forms.Padding(4);
            this.txtWasteInGm.Name = "txtWasteInGm";
            this.txtWasteInGm.ReadOnly = true;
            this.txtWasteInGm.Size = new System.Drawing.Size(128, 23);
            this.txtWasteInGm.TabIndex = 104;
            // 
            // txtNetWeight
            // 
            this.txtNetWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtNetWeight.Location = new System.Drawing.Point(470, 64);
            this.txtNetWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtNetWeight.Name = "txtNetWeight";
            this.txtNetWeight.ReadOnly = true;
            this.txtNetWeight.Size = new System.Drawing.Size(128, 23);
            this.txtNetWeight.TabIndex = 103;
            // 
            // txtKarat
            // 
            this.txtKarat.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtKarat.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKarat.Location = new System.Drawing.Point(190, 153);
            this.txtKarat.Margin = new System.Windows.Forms.Padding(4);
            this.txtKarat.Multiline = true;
            this.txtKarat.Name = "txtKarat";
            this.txtKarat.ReadOnly = true;
            this.txtKarat.Size = new System.Drawing.Size(128, 24);
            this.txtKarat.TabIndex = 102;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(26, 157);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 19);
            this.label9.TabIndex = 100;
            this.label9.Text = "Karat";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(26, 188);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 19);
            this.label10.TabIndex = 101;
            this.label10.Text = "Description";
            // 
            // txtWasteInPercent
            // 
            this.txtWasteInPercent.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtWasteInPercent.Location = new System.Drawing.Point(470, 94);
            this.txtWasteInPercent.Margin = new System.Windows.Forms.Padding(4);
            this.txtWasteInPercent.Name = "txtWasteInPercent";
            this.txtWasteInPercent.ReadOnly = true;
            this.txtWasteInPercent.Size = new System.Drawing.Size(128, 23);
            this.txtWasteInPercent.TabIndex = 97;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(363, 98);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 19);
            this.label12.TabIndex = 95;
            this.label12.Text = "Waste%";
            // 
            // txtDescription
            // 
            this.txtDescription.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(190, 183);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(408, 24);
            this.txtDescription.TabIndex = 99;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(26, 127);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 19);
            this.label13.TabIndex = 96;
            this.label13.Text = "Total Weight";
            // 
            // txtTotalWeight
            // 
            this.txtTotalWeight.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalWeight.Location = new System.Drawing.Point(190, 124);
            this.txtTotalWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalWeight.Name = "txtTotalWeight";
            this.txtTotalWeight.ReadOnly = true;
            this.txtTotalWeight.Size = new System.Drawing.Size(128, 23);
            this.txtTotalWeight.TabIndex = 98;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(26, 68);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(101, 19);
            this.label14.TabIndex = 93;
            this.label14.Text = "Tag Number";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(363, 157);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 19);
            this.label15.TabIndex = 74;
            this.label15.Text = "Qty";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(363, 127);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(45, 19);
            this.label17.TabIndex = 73;
            this.label17.Text = "Date";
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(26, 98);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(107, 19);
            this.label18.TabIndex = 72;
            this.label18.Text = "Waste In Gm";
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(362, 68);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 19);
            this.label19.TabIndex = 71;
            this.label19.Text = "Net Weight";
            // 
            // btnSelectedItem
            // 
            this.btnSelectedItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectedItem.Location = new System.Drawing.Point(489, 413);
            this.btnSelectedItem.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectedItem.Name = "btnSelectedItem";
            this.btnSelectedItem.Size = new System.Drawing.Size(128, 23);
            this.btnSelectedItem.TabIndex = 70;
            this.btnSelectedItem.Text = "Selected Item";
            this.btnSelectedItem.UseVisualStyleBackColor = true;
            this.btnSelectedItem.Click += new System.EventHandler(this.btnSelectedItem_Click);
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownHeight = 200;
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.IntegralHeight = false;
            this.cbxGroupItem.Location = new System.Drawing.Point(190, 30);
            this.cbxGroupItem.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(128, 22);
            this.cbxGroupItem.TabIndex = 68;
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxGroupItem.SelectionChangeCommitted += new System.EventHandler(this.cbxGroupItem_SelectionChangeCommitted);
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(26, 36);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(92, 19);
            this.label20.TabIndex = 69;
            this.label20.Text = "Group Item";
            // 
            // cbxOldScan
            // 
            this.cbxOldScan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOldScan.Enabled = false;
            this.cbxOldScan.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxOldScan.FormattingEnabled = true;
            this.cbxOldScan.Location = new System.Drawing.Point(209, 117);
            this.cbxOldScan.Margin = new System.Windows.Forms.Padding(4);
            this.cbxOldScan.Name = "cbxOldScan";
            this.cbxOldScan.Size = new System.Drawing.Size(128, 22);
            this.cbxOldScan.TabIndex = 100;
            this.cbxOldScan.SelectedIndexChanged += new System.EventHandler(this.cbxOldScan_SelectedIndexChanged);
            this.cbxOldScan.SelectionChangeCommitted += new System.EventHandler(this.cbxOldScan_SelectionChangeCommitted);
            // 
            // txtNewScan
            // 
            this.txtNewScan.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtNewScan.Location = new System.Drawing.Point(209, 85);
            this.txtNewScan.Margin = new System.Windows.Forms.Padding(4);
            this.txtNewScan.Name = "txtNewScan";
            this.txtNewScan.Size = new System.Drawing.Size(128, 23);
            this.txtNewScan.TabIndex = 105;
            // 
            // rbtNewScan
            // 
            this.rbtNewScan.Checked = true;
            this.rbtNewScan.Location = new System.Drawing.Point(51, 85);
            this.rbtNewScan.Margin = new System.Windows.Forms.Padding(4);
            this.rbtNewScan.Name = "rbtNewScan";
            this.rbtNewScan.Size = new System.Drawing.Size(100, 24);
            this.rbtNewScan.TabIndex = 106;
            this.rbtNewScan.TabStop = true;
            this.rbtNewScan.Text = "New Scan";
            this.rbtNewScan.UseVisualStyleBackColor = true;
            this.rbtNewScan.CheckedChanged += new System.EventHandler(this.rbtNewScan_CheckedChanged);
            // 
            // rbtOldScan
            // 
            this.rbtOldScan.Location = new System.Drawing.Point(51, 117);
            this.rbtOldScan.Margin = new System.Windows.Forms.Padding(4);
            this.rbtOldScan.Name = "rbtOldScan";
            this.rbtOldScan.Size = new System.Drawing.Size(92, 24);
            this.rbtOldScan.TabIndex = 107;
            this.rbtOldScan.TabStop = true;
            this.rbtOldScan.Text = "Old Scan";
            this.rbtOldScan.UseVisualStyleBackColor = true;
            this.rbtOldScan.CheckedChanged += new System.EventHandler(this.rbtOldScan_CheckedChanged);
            // 
            // pbxPicture
            // 
            this.pbxPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbxPicture.Location = new System.Drawing.Point(3, 19);
            this.pbxPicture.Margin = new System.Windows.Forms.Padding(4);
            this.pbxPicture.Name = "pbxPicture";
            this.pbxPicture.Size = new System.Drawing.Size(370, 312);
            this.pbxPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxPicture.TabIndex = 108;
            this.pbxPicture.TabStop = false;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrint,
            this.btnItemPrint,
            this.btnComplete,
            this.btnExit,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(1214, 33);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 109;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = global::jewl.Properties.Resources.my_print_16;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(63, 30);
            this.btnPrint.Text = "&Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnItemPrint
            // 
            this.btnItemPrint.BackColor = System.Drawing.Color.Transparent;
            this.btnItemPrint.ForeColor = System.Drawing.Color.Black;
            this.btnItemPrint.Image = global::jewl.Properties.Resources.my_print_16;
            this.btnItemPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnItemPrint.Name = "btnItemPrint";
            this.btnItemPrint.Size = new System.Drawing.Size(98, 30);
            this.btnItemPrint.Text = "&Item Print";
            this.btnItemPrint.Visible = false;
            this.btnItemPrint.Click += new System.EventHandler(this.btnItemPrint_Click);
            // 
            // btnComplete
            // 
            this.btnComplete.Image = global::jewl.Properties.Resources.completed;
            this.btnComplete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnComplete.Name = "btnComplete";
            this.btnComplete.Size = new System.Drawing.Size(97, 30);
            this.btnComplete.Text = "Complete";
            this.btnComplete.Click += new System.EventHandler(this.button1_Click);
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
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::jewl.Properties.Resources.reset;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(68, 30);
            this.toolStripButton1.Text = "&Reset";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbxPicture);
            this.groupBox1.Location = new System.Drawing.Point(703, 176);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 334);
            this.groupBox1.TabIndex = 110;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Picture Box";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTagNo);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.txtQty);
            this.groupBox2.Controls.Add(this.cbxGroupItem);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.txtDate);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.txtWasteInGm);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtNetWeight);
            this.groupBox2.Controls.Add(this.txtTotalWeight);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtKarat);
            this.groupBox2.Controls.Add(this.txtDescription);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtWasteInPercent);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(19, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(654, 219);
            this.groupBox2.TabIndex = 111;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Last Item Detail";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTotalNetWeight);
            this.groupBox3.Controls.Add(this.txtTotalWaste);
            this.groupBox3.Controls.Add(this.txtNetTotalWeight);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Location = new System.Drawing.Point(19, 527);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 122);
            this.groupBox3.TabIndex = 103;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Net Total";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtItemTotalWeight);
            this.groupBox4.Controls.Add(this.txtItemTotalWaste);
            this.groupBox4.Controls.Add(this.txtItemTotalNetWeight);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Location = new System.Drawing.Point(19, 401);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(335, 122);
            this.groupBox4.TabIndex = 103;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Item Total";
            // 
            // lblSessionNo
            // 
            this.lblSessionNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSessionNo.Location = new System.Drawing.Point(431, 146);
            this.lblSessionNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSessionNo.Name = "lblSessionNo";
            this.lblSessionNo.Size = new System.Drawing.Size(35, 19);
            this.lblSessionNo.TabIndex = 108;
            this.lblSessionNo.Visible = false;
            // 
            // lblItemId
            // 
            this.lblItemId.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemId.Location = new System.Drawing.Point(486, 146);
            this.lblItemId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(35, 19);
            this.lblItemId.TabIndex = 112;
            this.lblItemId.Visible = false;
            // 
            // frmStockCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1214, 674);
            this.Controls.Add(this.lblItemId);
            this.Controls.Add(this.lblSessionNo);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btnSelectedItem);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.rbtOldScan);
            this.Controls.Add(this.rbtNewScan);
            this.Controls.Add(this.txtNewScan);
            this.Controls.Add(this.cbxOldScan);
            this.Controls.Add(this.txtBarCode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStockCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStockCheck";
            this.Load += new System.EventHandler(this.frmStockCheck_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbxPicture)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemTotalWeight;
        private System.Windows.Forms.TextBox txtItemTotalNetWeight;
        private System.Windows.Forms.TextBox txtItemTotalWaste;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTotalNetWeight;
        private System.Windows.Forms.TextBox txtNetTotalWeight;
        private System.Windows.Forms.TextBox txtTotalWaste;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.TextBox txtDate;
        private System.Windows.Forms.TextBox txtWasteInGm;
        private System.Windows.Forms.TextBox txtNetWeight;
        private System.Windows.Forms.TextBox txtKarat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtWasteInPercent;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTotalWeight;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnSelectedItem;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cbxOldScan;
        private System.Windows.Forms.TextBox txtNewScan;
        private System.Windows.Forms.RadioButton rbtNewScan;
        private System.Windows.Forms.RadioButton rbtOldScan;
        private System.Windows.Forms.TextBox txtTagNo;
        private System.Windows.Forms.PictureBox pbxPicture;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnComplete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ToolStripButton btnItemPrint;
        private System.Windows.Forms.Label lblSessionNo;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}