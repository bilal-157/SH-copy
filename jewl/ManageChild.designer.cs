namespace jewl
{
    partial class ManageChild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageChild));
            this.rbtEditAccount = new System.Windows.Forms.RadioButton();
            this.rbtDeleteAccount = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbtGoldDr = new System.Windows.Forms.RadioButton();
            this.rbtGoldCr = new System.Windows.Forms.RadioButton();
            this.txtOpeningGold = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbtDr = new System.Windows.Forms.RadioButton();
            this.rbtCr = new System.Windows.Forms.RadioButton();
            this.txtOpeningCash = new System.Windows.Forms.TextBox();
            this.cbxAccountType = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtAccountCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtEditAccount
            // 
            this.rbtEditAccount.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtEditAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtEditAccount.ForeColor = System.Drawing.Color.Transparent;
            this.rbtEditAccount.Location = new System.Drawing.Point(72, 22);
            this.rbtEditAccount.Name = "rbtEditAccount";
            this.rbtEditAccount.Size = new System.Drawing.Size(114, 30);
            this.rbtEditAccount.TabIndex = 1;
            this.rbtEditAccount.TabStop = true;
            this.rbtEditAccount.Text = "Edit Account";
            this.rbtEditAccount.UseVisualStyleBackColor = false;
            // 
            // rbtDeleteAccount
            // 
            this.rbtDeleteAccount.BackColor = System.Drawing.Color.DarkSlateGray;
            this.rbtDeleteAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDeleteAccount.ForeColor = System.Drawing.Color.Transparent;
            this.rbtDeleteAccount.Location = new System.Drawing.Point(246, 22);
            this.rbtDeleteAccount.Name = "rbtDeleteAccount";
            this.rbtDeleteAccount.Size = new System.Drawing.Size(114, 30);
            this.rbtDeleteAccount.TabIndex = 2;
            this.rbtDeleteAccount.TabStop = true;
            this.rbtDeleteAccount.Text = "Delete Account";
            this.rbtDeleteAccount.UseVisualStyleBackColor = false;
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnExit.Location = new System.Drawing.Point(254, 472);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(113, 40);
            this.btnExit.TabIndex = 322;
            this.btnExit.Text = " E&xit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSave.Location = new System.Drawing.Point(128, 472);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(121, 40);
            this.btnSave.TabIndex = 321;
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
            this.lblTitle.Size = new System.Drawing.Size(509, 63);
            this.lblTitle.TabIndex = 323;
            this.lblTitle.Text = "Manage Child Account";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.UseMnemonic = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.txtOpeningGold);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.txtOpeningCash);
            this.groupBox1.Controls.Add(this.cbxAccountType);
            this.groupBox1.Controls.Add(this.dtpDate);
            this.groupBox1.Controls.Add(this.txtDescription);
            this.groupBox1.Controls.Add(this.txtAccountName);
            this.groupBox1.Controls.Add(this.txtAccountCode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbtEditAccount);
            this.groupBox1.Controls.Add(this.rbtDeleteAccount);
            this.groupBox1.Location = new System.Drawing.Point(31, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(442, 371);
            this.groupBox1.TabIndex = 324;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Account Detail";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbtGoldDr);
            this.panel3.Controls.Add(this.rbtGoldCr);
            this.panel3.Location = new System.Drawing.Point(302, 188);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(107, 28);
            this.panel3.TabIndex = 39;
            // 
            // rbtGoldDr
            // 
            this.rbtGoldDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtGoldDr.Location = new System.Drawing.Point(8, 7);
            this.rbtGoldDr.Name = "rbtGoldDr";
            this.rbtGoldDr.Size = new System.Drawing.Size(43, 20);
            this.rbtGoldDr.TabIndex = 15;
            this.rbtGoldDr.TabStop = true;
            this.rbtGoldDr.Text = "Dr";
            this.rbtGoldDr.UseVisualStyleBackColor = true;
            // 
            // rbtGoldCr
            // 
            this.rbtGoldCr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtGoldCr.Location = new System.Drawing.Point(58, 7);
            this.rbtGoldCr.Name = "rbtGoldCr";
            this.rbtGoldCr.Size = new System.Drawing.Size(43, 20);
            this.rbtGoldCr.TabIndex = 16;
            this.rbtGoldCr.TabStop = true;
            this.rbtGoldCr.Text = "Cr";
            this.rbtGoldCr.UseVisualStyleBackColor = true;
            // 
            // txtOpeningGold
            // 
            this.txtOpeningGold.Location = new System.Drawing.Point(196, 194);
            this.txtOpeningGold.Name = "txtOpeningGold";
            this.txtOpeningGold.Size = new System.Drawing.Size(95, 20);
            this.txtOpeningGold.TabIndex = 38;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(33, 197);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(84, 13);
            this.label8.TabIndex = 37;
            this.label8.Text = "Opening Gold";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbtDr);
            this.panel1.Controls.Add(this.rbtCr);
            this.panel1.Location = new System.Drawing.Point(302, 149);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(107, 28);
            this.panel1.TabIndex = 36;
            // 
            // rbtDr
            // 
            this.rbtDr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDr.Location = new System.Drawing.Point(8, 7);
            this.rbtDr.Name = "rbtDr";
            this.rbtDr.Size = new System.Drawing.Size(43, 20);
            this.rbtDr.TabIndex = 15;
            this.rbtDr.TabStop = true;
            this.rbtDr.Text = "Dr";
            this.rbtDr.UseVisualStyleBackColor = true;
            // 
            // rbtCr
            // 
            this.rbtCr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCr.Location = new System.Drawing.Point(58, 7);
            this.rbtCr.Name = "rbtCr";
            this.rbtCr.Size = new System.Drawing.Size(43, 20);
            this.rbtCr.TabIndex = 16;
            this.rbtCr.TabStop = true;
            this.rbtCr.Text = "Cr";
            this.rbtCr.UseVisualStyleBackColor = true;
            // 
            // txtOpeningCash
            // 
            this.txtOpeningCash.Location = new System.Drawing.Point(196, 155);
            this.txtOpeningCash.Name = "txtOpeningCash";
            this.txtOpeningCash.Size = new System.Drawing.Size(95, 20);
            this.txtOpeningCash.TabIndex = 35;
            // 
            // cbxAccountType
            // 
            this.cbxAccountType.FormattingEnabled = true;
            this.cbxAccountType.Location = new System.Drawing.Point(196, 238);
            this.cbxAccountType.Name = "cbxAccountType";
            this.cbxAccountType.Size = new System.Drawing.Size(197, 21);
            this.cbxAccountType.TabIndex = 34;
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(196, 279);
            this.dtpDate.MinDate = new System.DateTime(1755, 6, 4, 0, 0, 0, 0);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(83, 20);
            this.dtpDate.TabIndex = 33;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(196, 322);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(197, 25);
            this.txtDescription.TabIndex = 32;
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(196, 109);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(197, 20);
            this.txtAccountName.TabIndex = 31;
            // 
            // txtAccountCode
            // 
            this.txtAccountCode.Location = new System.Drawing.Point(196, 69);
            this.txtAccountCode.Name = "txtAccountCode";
            this.txtAccountCode.ReadOnly = true;
            this.txtAccountCode.Size = new System.Drawing.Size(197, 20);
            this.txtAccountCode.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "Account Name";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "Opening Cash";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Account Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(33, 282);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Date";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 325);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Account Code";
            // 
            // ManageChild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(509, 524);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSave);
            this.Name = "ManageChild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::Edit Child Account:::...";
            this.Load += new System.EventHandler(this.ManageChild_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ManageChildAccount_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtEditAccount;
        private System.Windows.Forms.RadioButton rbtDeleteAccount;
        internal System.Windows.Forms.Button btnExit;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbtGoldDr;
        private System.Windows.Forms.RadioButton rbtGoldCr;
        private System.Windows.Forms.TextBox txtOpeningGold;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbtDr;
        private System.Windows.Forms.RadioButton rbtCr;
        private System.Windows.Forms.TextBox txtOpeningCash;
        private System.Windows.Forms.ComboBox cbxAccountType;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtAccountName;
        private System.Windows.Forms.TextBox txtAccountCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}