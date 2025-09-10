namespace jewl
{
    partial class ManageParent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageParent));
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtCreateParent = new System.Windows.Forms.RadioButton();
            this.rbtCreateChild = new System.Windows.Forms.RadioButton();
            this.rbtDeleteParent = new System.Windows.Forms.RadioButton();
            this.rbtEditParent = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtParentName = new System.Windows.Forms.TextBox();
            this.txtParentCode = new System.Windows.Forms.TextBox();
            this.lblParentName = new System.Windows.Forms.Label();
            this.lblParentCode = new System.Windows.Forms.Label();
            this.lblCreateChild = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOpGold = new System.Windows.Forms.TextBox();
            this.rbtOpDebit = new System.Windows.Forms.RadioButton();
            this.rbtOpCredit = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lblOpeningBalance = new System.Windows.Forms.Label();
            this.txtOpeningBalance = new System.Windows.Forms.TextBox();
            this.rbtCr = new System.Windows.Forms.RadioButton();
            this.rbtDr = new System.Windows.Forms.RadioButton();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.vlrDescription = new System.Windows.Forms.VScrollBar();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.cbxAccountType = new System.Windows.Forms.ComboBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.lblAccountCode = new System.Windows.Forms.Label();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.lblAccountType = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtCreateParent);
            this.panel1.Controls.Add(this.rbtCreateChild);
            this.panel1.Controls.Add(this.rbtDeleteParent);
            this.panel1.Controls.Add(this.rbtEditParent);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 49);
            this.panel1.TabIndex = 1;
            // 
            // rbtCreateParent
            // 
            this.rbtCreateParent.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtCreateParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCreateParent.ForeColor = System.Drawing.Color.Transparent;
            this.rbtCreateParent.Location = new System.Drawing.Point(6, 7);
            this.rbtCreateParent.Name = "rbtCreateParent";
            this.rbtCreateParent.Size = new System.Drawing.Size(117, 33);
            this.rbtCreateParent.TabIndex = 4;
            this.rbtCreateParent.Text = "Create Parent";
            this.rbtCreateParent.UseVisualStyleBackColor = false;
            this.rbtCreateParent.CheckedChanged += new System.EventHandler(this.rbtCreateParent_CheckedChanged);
            // 
            // rbtCreateChild
            // 
            this.rbtCreateChild.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtCreateChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCreateChild.ForeColor = System.Drawing.Color.Transparent;
            this.rbtCreateChild.Location = new System.Drawing.Point(346, 8);
            this.rbtCreateChild.Name = "rbtCreateChild";
            this.rbtCreateChild.Size = new System.Drawing.Size(103, 32);
            this.rbtCreateChild.TabIndex = 3;
            this.rbtCreateChild.Text = "Create Child";
            this.rbtCreateChild.UseVisualStyleBackColor = false;
            this.rbtCreateChild.CheckedChanged += new System.EventHandler(this.rbtCreateChild_CheckedChanged);
            // 
            // rbtDeleteParent
            // 
            this.rbtDeleteParent.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtDeleteParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDeleteParent.ForeColor = System.Drawing.Color.Transparent;
            this.rbtDeleteParent.Location = new System.Drawing.Point(237, 8);
            this.rbtDeleteParent.Name = "rbtDeleteParent";
            this.rbtDeleteParent.Size = new System.Drawing.Size(103, 32);
            this.rbtDeleteParent.TabIndex = 1;
            this.rbtDeleteParent.Text = "Delete Parent";
            this.rbtDeleteParent.UseVisualStyleBackColor = false;
            this.rbtDeleteParent.CheckedChanged += new System.EventHandler(this.rbtDeleteParent_CheckedChanged);
            // 
            // rbtEditParent
            // 
            this.rbtEditParent.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtEditParent.Checked = true;
            this.rbtEditParent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtEditParent.ForeColor = System.Drawing.Color.Transparent;
            this.rbtEditParent.Location = new System.Drawing.Point(129, 8);
            this.rbtEditParent.Name = "rbtEditParent";
            this.rbtEditParent.Size = new System.Drawing.Size(103, 32);
            this.rbtEditParent.TabIndex = 2;
            this.rbtEditParent.TabStop = true;
            this.rbtEditParent.Text = "Edit Parent";
            this.rbtEditParent.UseVisualStyleBackColor = false;
            this.rbtEditParent.CheckedChanged += new System.EventHandler(this.rbtEditParent_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtParentName);
            this.panel2.Controls.Add(this.txtParentCode);
            this.panel2.Controls.Add(this.lblParentName);
            this.panel2.Controls.Add(this.lblParentCode);
            this.panel2.Location = new System.Drawing.Point(8, 74);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(446, 68);
            this.panel2.TabIndex = 2;
            // 
            // txtParentName
            // 
            this.txtParentName.Location = new System.Drawing.Point(189, 38);
            this.txtParentName.Multiline = true;
            this.txtParentName.Name = "txtParentName";
            this.txtParentName.Size = new System.Drawing.Size(253, 23);
            this.txtParentName.TabIndex = 5;
            // 
            // txtParentCode
            // 
            this.txtParentCode.BackColor = System.Drawing.Color.DarkCyan;
            this.txtParentCode.Location = new System.Drawing.Point(189, 9);
            this.txtParentCode.Multiline = true;
            this.txtParentCode.Name = "txtParentCode";
            this.txtParentCode.ReadOnly = true;
            this.txtParentCode.Size = new System.Drawing.Size(253, 23);
            this.txtParentCode.TabIndex = 4;
            // 
            // lblParentName
            // 
            this.lblParentName.BackColor = System.Drawing.Color.Transparent;
            this.lblParentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentName.ForeColor = System.Drawing.Color.Transparent;
            this.lblParentName.Location = new System.Drawing.Point(9, 41);
            this.lblParentName.Name = "lblParentName";
            this.lblParentName.Size = new System.Drawing.Size(116, 19);
            this.lblParentName.TabIndex = 1;
            this.lblParentName.Text = "Parent Name";
            // 
            // lblParentCode
            // 
            this.lblParentCode.BackColor = System.Drawing.Color.Transparent;
            this.lblParentCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParentCode.ForeColor = System.Drawing.Color.Transparent;
            this.lblParentCode.Location = new System.Drawing.Point(9, 12);
            this.lblParentCode.Name = "lblParentCode";
            this.lblParentCode.Size = new System.Drawing.Size(113, 19);
            this.lblParentCode.TabIndex = 0;
            this.lblParentCode.Text = "Parent Code";
            // 
            // lblCreateChild
            // 
            this.lblCreateChild.BackColor = System.Drawing.Color.DarkCyan;
            this.lblCreateChild.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreateChild.ForeColor = System.Drawing.Color.White;
            this.lblCreateChild.Location = new System.Drawing.Point(9, 145);
            this.lblCreateChild.Name = "lblCreateChild";
            this.lblCreateChild.Size = new System.Drawing.Size(446, 23);
            this.lblCreateChild.TabIndex = 3;
            this.lblCreateChild.Text = "Create Child";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.dtpDate);
            this.panel3.Controls.Add(this.vlrDescription);
            this.panel3.Controls.Add(this.txtDescription);
            this.panel3.Controls.Add(this.cbxAccountType);
            this.panel3.Controls.Add(this.txtAccountName);
            this.panel3.Controls.Add(this.txtAccountCode);
            this.panel3.Controls.Add(this.lblAccountCode);
            this.panel3.Controls.Add(this.lblAccountName);
            this.panel3.Controls.Add(this.lblAccountType);
            this.panel3.Controls.Add(this.lblDate);
            this.panel3.Controls.Add(this.lblDescription);
            this.panel3.Location = new System.Drawing.Point(8, 171);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(446, 263);
            this.panel3.TabIndex = 4;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.label1);
            this.panel6.Controls.Add(this.txtOpGold);
            this.panel6.Controls.Add(this.rbtOpDebit);
            this.panel6.Controls.Add(this.rbtOpCredit);
            this.panel6.Location = new System.Drawing.Point(5, 106);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(438, 37);
            this.panel6.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Opening Gold";
            // 
            // txtOpGold
            // 
            this.txtOpGold.Location = new System.Drawing.Point(184, 8);
            this.txtOpGold.Multiline = true;
            this.txtOpGold.Name = "txtOpGold";
            this.txtOpGold.Size = new System.Drawing.Size(121, 23);
            this.txtOpGold.TabIndex = 17;
            // 
            // rbtOpDebit
            // 
            this.rbtOpDebit.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbtOpDebit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOpDebit.Location = new System.Drawing.Point(318, 8);
            this.rbtOpDebit.Name = "rbtOpDebit";
            this.rbtOpDebit.Size = new System.Drawing.Size(48, 23);
            this.rbtOpDebit.TabIndex = 18;
            this.rbtOpDebit.TabStop = true;
            this.rbtOpDebit.Text = "Dr";
            this.rbtOpDebit.UseVisualStyleBackColor = false;
            // 
            // rbtOpCredit
            // 
            this.rbtOpCredit.BackColor = System.Drawing.Color.LemonChiffon;
            this.rbtOpCredit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtOpCredit.Location = new System.Drawing.Point(375, 8);
            this.rbtOpCredit.Name = "rbtOpCredit";
            this.rbtOpCredit.Size = new System.Drawing.Size(48, 23);
            this.rbtOpCredit.TabIndex = 19;
            this.rbtOpCredit.TabStop = true;
            this.rbtOpCredit.Text = "Cr";
            this.rbtOpCredit.UseVisualStyleBackColor = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.lblOpeningBalance);
            this.panel5.Controls.Add(this.txtOpeningBalance);
            this.panel5.Controls.Add(this.rbtCr);
            this.panel5.Controls.Add(this.rbtDr);
            this.panel5.Location = new System.Drawing.Point(5, 65);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(438, 35);
            this.panel5.TabIndex = 20;
            // 
            // lblOpeningBalance
            // 
            this.lblOpeningBalance.BackColor = System.Drawing.Color.Transparent;
            this.lblOpeningBalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpeningBalance.ForeColor = System.Drawing.Color.Transparent;
            this.lblOpeningBalance.Location = new System.Drawing.Point(3, 11);
            this.lblOpeningBalance.Name = "lblOpeningBalance";
            this.lblOpeningBalance.Size = new System.Drawing.Size(122, 19);
            this.lblOpeningBalance.TabIndex = 4;
            this.lblOpeningBalance.Text = "Opening Cash";
            // 
            // txtOpeningBalance
            // 
            this.txtOpeningBalance.Location = new System.Drawing.Point(185, 6);
            this.txtOpeningBalance.Multiline = true;
            this.txtOpeningBalance.Name = "txtOpeningBalance";
            this.txtOpeningBalance.Size = new System.Drawing.Size(120, 23);
            this.txtOpeningBalance.TabIndex = 8;
            // 
            // rbtCr
            // 
            this.rbtCr.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtCr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCr.ForeColor = System.Drawing.Color.Transparent;
            this.rbtCr.Location = new System.Drawing.Point(375, 6);
            this.rbtCr.Name = "rbtCr";
            this.rbtCr.Size = new System.Drawing.Size(48, 23);
            this.rbtCr.TabIndex = 10;
            this.rbtCr.TabStop = true;
            this.rbtCr.Text = "Cr";
            this.rbtCr.UseVisualStyleBackColor = false;
            // 
            // rbtDr
            // 
            this.rbtDr.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDr.ForeColor = System.Drawing.Color.Transparent;
            this.rbtDr.Location = new System.Drawing.Point(317, 6);
            this.rbtDr.Name = "rbtDr";
            this.rbtDr.Size = new System.Drawing.Size(48, 23);
            this.rbtDr.TabIndex = 9;
            this.rbtDr.Text = "Dr";
            this.rbtDr.UseVisualStyleBackColor = false;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(189, 193);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(88, 20);
            this.dtpDate.TabIndex = 15;
            // 
            // vlrDescription
            // 
            this.vlrDescription.Location = new System.Drawing.Point(425, 217);
            this.vlrDescription.Name = "vlrDescription";
            this.vlrDescription.Size = new System.Drawing.Size(17, 35);
            this.vlrDescription.TabIndex = 14;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(189, 217);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(235, 35);
            this.txtDescription.TabIndex = 13;
            // 
            // cbxAccountType
            // 
            this.cbxAccountType.FormattingEnabled = true;
            this.cbxAccountType.Location = new System.Drawing.Point(189, 167);
            this.cbxAccountType.Name = "cbxAccountType";
            this.cbxAccountType.Size = new System.Drawing.Size(253, 21);
            this.cbxAccountType.TabIndex = 11;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(189, 36);
            this.txtAccountName.Multiline = true;
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(253, 23);
            this.txtAccountName.TabIndex = 7;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.BackColor = System.Drawing.Color.DarkCyan;
            this.txtAccountCode.Enabled = false;
            this.txtAccountCode.Location = new System.Drawing.Point(189, 8);
            this.txtAccountCode.Multiline = true;
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.ReadOnly = true;
            this.txtAccountCode.Size = new System.Drawing.Size(253, 23);
            this.txtAccountCode.TabIndex = 6;
            // 
            // lblAccountCode
            // 
            this.lblAccountCode.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountCode.ForeColor = System.Drawing.Color.Transparent;
            this.lblAccountCode.Location = new System.Drawing.Point(10, 11);
            this.lblAccountCode.Name = "lblAccountCode";
            this.lblAccountCode.Size = new System.Drawing.Size(123, 19);
            this.lblAccountCode.TabIndex = 6;
            this.lblAccountCode.Text = "Account Code";
            // 
            // lblAccountName
            // 
            this.lblAccountName.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountName.ForeColor = System.Drawing.Color.Transparent;
            this.lblAccountName.Location = new System.Drawing.Point(10, 39);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(126, 19);
            this.lblAccountName.TabIndex = 5;
            this.lblAccountName.Text = "Account Name";
            // 
            // lblAccountType
            // 
            this.lblAccountType.BackColor = System.Drawing.Color.Transparent;
            this.lblAccountType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountType.ForeColor = System.Drawing.Color.Transparent;
            this.lblAccountType.Location = new System.Drawing.Point(9, 170);
            this.lblAccountType.Name = "lblAccountType";
            this.lblAccountType.Size = new System.Drawing.Size(122, 19);
            this.lblAccountType.TabIndex = 3;
            this.lblAccountType.Text = "Account Type";
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Transparent;
            this.lblDate.Location = new System.Drawing.Point(9, 195);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(70, 19);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Date";
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Transparent;
            this.lblDescription.Location = new System.Drawing.Point(9, 220);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(107, 19);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(261, 502);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 40);
            this.btnExit.TabIndex = 320;
            this.btnExit.Text = " E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(135, 502);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 40);
            this.btnSave.TabIndex = 319;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoEllipsis = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblTitle.Image = ((System.Drawing.Image)(resources.GetObject("lblTitle.Image")));
            this.lblTitle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblTitle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(509, 50);
            this.lblTitle.TabIndex = 314;
            this.lblTitle.Text = "Manage Parent Account";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.lblCreateChild);
            this.groupBox1.Location = new System.Drawing.Point(24, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(461, 440);
            this.groupBox1.TabIndex = 321;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parent Account";
            // 
            // ManageParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(509, 549);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnSave);
            this.Name = "ManageParent";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::Parent Account:::...";
            this.Load += new System.EventHandler(this.EditParentAccount_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ManageParentAccount_Paint);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtDeleteParent;
        private System.Windows.Forms.RadioButton rbtCreateChild;
        private System.Windows.Forms.RadioButton rbtEditParent;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtParentCode;
        private System.Windows.Forms.Label lblParentName;
        private System.Windows.Forms.Label lblParentCode;
        private System.Windows.Forms.Label lblCreateChild;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblAccountCode;
        private System.Windows.Forms.Label lblAccountName;
        private System.Windows.Forms.Label lblOpeningBalance;
        private System.Windows.Forms.Label lblAccountType;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RadioButton rbtDr;
        private System.Windows.Forms.RadioButton rbtCr;
        private System.Windows.Forms.TextBox txtOpeningBalance;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.ComboBox cbxAccountType;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.VScrollBar vlrDescription;
        private System.Windows.Forms.TextBox txtParentName;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rbtCreateParent;
        internal System.Windows.Forms.Label lblTitle;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOpGold;
        private System.Windows.Forms.RadioButton rbtOpDebit;
        private System.Windows.Forms.RadioButton rbtOpCredit;
    }
}