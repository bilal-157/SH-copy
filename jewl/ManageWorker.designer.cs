namespace jewl
{
    partial class ManageWorker
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
            this.dgvWorkerInformation = new System.Windows.Forms.DataGridView();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtOpGold = new System.Windows.Forms.TextBox();
            this.rbtOpDebit = new System.Windows.Forms.RadioButton();
            this.rbtOpCredit = new System.Windows.Forms.RadioButton();
            this.lblOpeningBalance = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new System.Windows.Forms.TextBox();
            this.rbtCr = new System.Windows.Forms.RadioButton();
            this.rbtDr = new System.Windows.Forms.RadioButton();
            this.txtCheejad = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cbxTransKarrat = new System.Windows.Forms.ComboBox();
            this.txtMaking = new System.Windows.Forms.TextBox();
            this.txtContactNo = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnReports = new System.Windows.Forms.ToolStripButton();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkerInformation)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTitle.BackColor = System.Drawing.Color.Gray;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 36);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1236, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Manage Worker";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvWorkerInformation
            // 
            this.dgvWorkerInformation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkerInformation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.AccountCode});
            this.dgvWorkerInformation.Location = new System.Drawing.Point(3, 19);
            this.dgvWorkerInformation.Margin = new System.Windows.Forms.Padding(4);
            this.dgvWorkerInformation.Name = "dgvWorkerInformation";
            this.dgvWorkerInformation.Size = new System.Drawing.Size(683, 353);
            this.dgvWorkerInformation.TabIndex = 5;
            this.dgvWorkerInformation.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvWorkerInformation_CellClick);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(143, 196);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(204, 26);
            this.txtEmail.TabIndex = 58;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(32, 196);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 59;
            this.label4.Text = "Email";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(143, 96);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(204, 26);
            this.txtAddress.TabIndex = 56;
            this.txtAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAddress_KeyDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 96);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 19);
            this.label6.TabIndex = 57;
            this.label6.Text = "Address";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(32, 65);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(105, 19);
            this.label8.TabIndex = 16;
            this.label8.Text = "Opening Gold";
            // 
            // txtOpGold
            // 
            this.txtOpGold.Location = new System.Drawing.Point(143, 62);
            this.txtOpGold.Margin = new System.Windows.Forms.Padding(4);
            this.txtOpGold.Multiline = true;
            this.txtOpGold.Name = "txtOpGold";
            this.txtOpGold.Size = new System.Drawing.Size(204, 24);
            this.txtOpGold.TabIndex = 17;
            this.txtOpGold.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpGold_KeyDown);
            this.txtOpGold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpGold_KeyPress);
            // 
            // rbtOpDebit
            // 
            this.rbtOpDebit.BackColor = System.Drawing.Color.White;
            this.rbtOpDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOpDebit.Location = new System.Drawing.Point(6, 4);
            this.rbtOpDebit.Margin = new System.Windows.Forms.Padding(4);
            this.rbtOpDebit.Name = "rbtOpDebit";
            this.rbtOpDebit.Size = new System.Drawing.Size(38, 28);
            this.rbtOpDebit.TabIndex = 18;
            this.rbtOpDebit.TabStop = true;
            this.rbtOpDebit.Text = "Dr";
            this.rbtOpDebit.UseVisualStyleBackColor = false;
            this.rbtOpDebit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtOpDebit_KeyDown);
            // 
            // rbtOpCredit
            // 
            this.rbtOpCredit.BackColor = System.Drawing.Color.White;
            this.rbtOpCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOpCredit.Location = new System.Drawing.Point(50, 4);
            this.rbtOpCredit.Margin = new System.Windows.Forms.Padding(4);
            this.rbtOpCredit.Name = "rbtOpCredit";
            this.rbtOpCredit.Size = new System.Drawing.Size(38, 28);
            this.rbtOpCredit.TabIndex = 19;
            this.rbtOpCredit.Text = "Cr";
            this.rbtOpCredit.UseVisualStyleBackColor = false;
            this.rbtOpCredit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtOpCredit_KeyDown);
            // 
            // lblOpeningBalance
            // 
            this.lblOpeningBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpeningBalance.Location = new System.Drawing.Point(32, 31);
            this.lblOpeningBalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOpeningBalance.Name = "lblOpeningBalance";
            this.lblOpeningBalance.Size = new System.Drawing.Size(101, 20);
            this.lblOpeningBalance.TabIndex = 4;
            this.lblOpeningBalance.Text = "Opening Cash";
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.Location = new System.Drawing.Point(143, 27);
            this.txtOpeningBalance.Margin = new System.Windows.Forms.Padding(4);
            this.txtOpeningBalance.Multiline = true;
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(204, 24);
            this.txtOpeningBalance.TabIndex = 8;
            this.txtOpeningBalance.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOpeningBalance_KeyDown);
            this.txtOpeningBalance.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtOpeningBalance_KeyPress);
            // 
            // rbtCr
            // 
            this.rbtCr.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbtCr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCr.Location = new System.Drawing.Point(403, 23);
            this.rbtCr.Margin = new System.Windows.Forms.Padding(4);
            this.rbtCr.Name = "rbtCr";
            this.rbtCr.Size = new System.Drawing.Size(38, 28);
            this.rbtCr.TabIndex = 10;
            this.rbtCr.Text = "Cr";
            this.rbtCr.UseVisualStyleBackColor = false;
            this.rbtCr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtCr_KeyDown);
            // 
            // rbtDr
            // 
            this.rbtDr.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbtDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDr.Location = new System.Drawing.Point(359, 23);
            this.rbtDr.Margin = new System.Windows.Forms.Padding(4);
            this.rbtDr.Name = "rbtDr";
            this.rbtDr.Size = new System.Drawing.Size(38, 28);
            this.rbtDr.TabIndex = 9;
            this.rbtDr.TabStop = true;
            this.rbtDr.Text = "Dr";
            this.rbtDr.UseVisualStyleBackColor = false;
            this.rbtDr.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbtDr_KeyDown);
            // 
            // txtCheejad
            // 
            this.txtCheejad.Location = new System.Drawing.Point(143, 162);
            this.txtCheejad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCheejad.Name = "txtCheejad";
            this.txtCheejad.Size = new System.Drawing.Size(204, 26);
            this.txtCheejad.TabIndex = 6;
            this.txtCheejad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheejad_KeyDown);
            this.txtCheejad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheejad_KeyPress);
            this.txtCheejad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheejad_KeyUp);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(32, 162);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 19);
            this.label10.TabIndex = 51;
            this.label10.Text = "Cheejad";
            // 
            // cbxTransKarrat
            // 
            this.cbxTransKarrat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTransKarrat.FormattingEnabled = true;
            this.cbxTransKarrat.Items.AddRange(new object[] {
            "24",
            "23",
            "22",
            "21",
            "20",
            "19",
            "18",
            "17",
            "16",
            "15",
            "14",
            "13",
            "12"});
            this.cbxTransKarrat.Location = new System.Drawing.Point(143, 231);
            this.cbxTransKarrat.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTransKarrat.Name = "cbxTransKarrat";
            this.cbxTransKarrat.Size = new System.Drawing.Size(204, 28);
            this.cbxTransKarrat.TabIndex = 7;
            this.cbxTransKarrat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbxTransKarrat_KeyDown);
            // 
            // txtMaking
            // 
            this.txtMaking.Location = new System.Drawing.Point(143, 128);
            this.txtMaking.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaking.Name = "txtMaking";
            this.txtMaking.Size = new System.Drawing.Size(204, 26);
            this.txtMaking.TabIndex = 4;
            this.txtMaking.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaking_KeyDown);
            this.txtMaking.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaking_KeyPress);
            this.txtMaking.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMaking_KeyUp);
            this.txtMaking.Leave += new System.EventHandler(this.txtMaking_Leave);
            // 
            // txtContactNo
            // 
            this.txtContactNo.Location = new System.Drawing.Point(143, 64);
            this.txtContactNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtContactNo.Name = "txtContactNo";
            this.txtContactNo.Size = new System.Drawing.Size(204, 26);
            this.txtContactNo.TabIndex = 2;
            this.txtContactNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtContactNo_KeyDown);
            this.txtContactNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContactNo_KeyPress);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(143, 31);
            this.txtName.Margin = new System.Windows.Forms.Padding(4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(204, 26);
            this.txtName.TabIndex = 1;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            this.txtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtName_KeyPress);
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(32, 231);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 19);
            this.label7.TabIndex = 45;
            this.label7.Text = "Trans Karrat";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(32, 128);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 19);
            this.label5.TabIndex = 43;
            this.label5.Text = "Making";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 64);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 19);
            this.label3.TabIndex = 41;
            this.label3.Text = "Contact No";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 40;
            this.label2.Text = "Name";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtContactNo);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtMaking);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cbxTransKarrat);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtCheejad);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(740, 102);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 266);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Personal Info";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.lblOpeningBalance);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtOpeningBalance);
            this.groupBox2.Controls.Add(this.rbtCr);
            this.groupBox2.Controls.Add(this.txtOpGold);
            this.groupBox2.Controls.Add(this.rbtDr);
            this.groupBox2.Location = new System.Drawing.Point(740, 379);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(452, 100);
            this.groupBox2.TabIndex = 60;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Working Detail";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.rbtOpCredit);
            this.panel1.Controls.Add(this.rbtOpDebit);
            this.panel1.Location = new System.Drawing.Point(354, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(92, 39);
            this.panel1.TabIndex = 20;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvWorkerInformation);
            this.groupBox3.Location = new System.Drawing.Point(12, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(689, 375);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Worker Record";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnEdit,
            this.btnReset,
            this.btnDelete,
            this.btnExit,
            this.btnPrint,
            this.btnReports});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(1236, 35);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 61;
            // 
            // btnNew
            // 
            this.btnNew.CheckOnClick = true;
            this.btnNew.ForeColor = System.Drawing.Color.Black;
            this.btnNew.Image = global::jewl.Properties.Resources.newfile;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 32);
            this.btnNew.Text = "&New";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::jewl.Properties.Resources._1483476843_Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(77, 32);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.CheckOnClick = true;
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Image = global::jewl.Properties.Resources._1483476345_edit_notes;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(70, 32);
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.Edit_Click);
            // 
            // btnReset
            // 
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Image = global::jewl.Properties.Resources.reset;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(83, 32);
            this.btnReset.Text = "&Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Image = global::jewl.Properties.Resources.mydelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 32);
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.CheckOnClick = true;
            this.btnPrint.ForeColor = System.Drawing.Color.Black;
            this.btnPrint.Image = global::jewl.Properties.Resources.my_print_16;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(77, 32);
            this.btnPrint.Text = "&Print";
            // 
            // btnReports
            // 
            this.btnReports.Image = global::jewl.Properties.Resources.Reports;
            this.btnReports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(104, 32);
            this.btnReports.Text = "&Reports";
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Enabled = false;
            this.txtAccountCode.Location = new System.Drawing.Point(883, 486);
            this.txtAccountCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.Size = new System.Drawing.Size(204, 26);
            this.txtAccountCode.TabIndex = 62;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(773, 490);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 19);
            this.label1.TabIndex = 63;
            this.label1.Text = "Account Code";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Id";
            this.Column7.Name = "Column7";
            this.Column7.Visible = false;
            this.Column7.Width = 200;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "Making";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            this.Column2.Width = 91;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column3.HeaderText = "Contact No";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 170;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column4.HeaderText = "Address";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 200;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column5.HeaderText = "Email";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column5.Width = 80;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column6.HeaderText = "TKarrat";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            this.Column6.Width = 94;
            // 
            // AccountCode
            // 
            this.AccountCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AccountCode.HeaderText = "Account Code";
            this.AccountCode.Name = "AccountCode";
            this.AccountCode.Visible = false;
            // 
            // ManageWorker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 538);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAccountCode);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ManageWorker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Worker";
            this.Load += new System.EventHandler(this.ManageWorker_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AddWorker_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkerInformation)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvWorkerInformation;
        private System.Windows.Forms.ComboBox cbxTransKarrat;
        private System.Windows.Forms.TextBox txtMaking;
        private System.Windows.Forms.TextBox txtContactNo;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCheejad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtOpGold;
        private System.Windows.Forms.RadioButton rbtOpDebit;
        private System.Windows.Forms.RadioButton rbtOpCredit;
        private System.Windows.Forms.Label lblOpeningBalance;
        private System.Windows.Forms.TextBox txtOpeningBalance;
        private System.Windows.Forms.RadioButton rbtCr;
        private System.Windows.Forms.RadioButton rbtDr;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnReports;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountCode;
    }
}