namespace jewl
{
    partial class frmStonesRpt
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
            this.cbxParty = new System.Windows.Forms.ComboBox();
            this.cbxStonesType = new System.Windows.Forms.ComboBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkDateTo = new System.Windows.Forms.CheckBox();
            this.chkDateFrom = new System.Windows.Forms.CheckBox();
            this.dtpDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpDateFrom = new System.Windows.Forms.DateTimePicker();
            this.ChkParty = new System.Windows.Forms.CheckBox();
            this.ChkStonesType = new System.Windows.Forms.CheckBox();
            this.ChkDate = new System.Windows.Forms.CheckBox();
            this.ChkAll = new System.Windows.Forms.CheckBox();
            this.cbxStonesName = new System.Windows.Forms.ComboBox();
            this.ChkStoneName = new System.Windows.Forms.CheckBox();
            this.ChkStkSumary = new System.Windows.Forms.CheckBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxParty
            // 
            this.cbxParty.Enabled = false;
            this.cbxParty.FormattingEnabled = true;
            this.cbxParty.Location = new System.Drawing.Point(166, 79);
            this.cbxParty.Name = "cbxParty";
            this.cbxParty.Size = new System.Drawing.Size(140, 21);
            this.cbxParty.TabIndex = 0;
            // 
            // cbxStonesType
            // 
            this.cbxStonesType.Enabled = false;
            this.cbxStonesType.FormattingEnabled = true;
            this.cbxStonesType.Items.AddRange(new object[] {
            "Stones",
            "Beeds",
            "Diamond"});
            this.cbxStonesType.Location = new System.Drawing.Point(166, 106);
            this.cbxStonesType.Name = "cbxStonesType";
            this.cbxStonesType.Size = new System.Drawing.Size(140, 21);
            this.cbxStonesType.TabIndex = 3;
            this.cbxStonesType.SelectionChangeCommitted += new System.EventHandler(this.cbxStonesType_SelectionChangeCommitted);
            this.cbxStonesType.SelectedIndexChanged += new System.EventHandler(this.cbxStonesType_SelectedIndexChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(106, 269);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 35);
            this.btnPrint.TabIndex = 12;
            this.btnPrint.Text = "View";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(226, 269);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(80, 35);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkDateTo);
            this.panel1.Controls.Add(this.chkDateFrom);
            this.panel1.Controls.Add(this.dtpDateTo);
            this.panel1.Controls.Add(this.dtpDateFrom);
            this.panel1.Location = new System.Drawing.Point(164, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 60);
            this.panel1.TabIndex = 19;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // chkDateTo
            // 
            this.chkDateTo.AutoSize = true;
            this.chkDateTo.Location = new System.Drawing.Point(12, 32);
            this.chkDateTo.Name = "chkDateTo";
            this.chkDateTo.Size = new System.Drawing.Size(72, 17);
            this.chkDateTo.TabIndex = 21;
            this.chkDateTo.Text = "Date To";
            this.chkDateTo.UseVisualStyleBackColor = true;
            // 
            // chkDateFrom
            // 
            this.chkDateFrom.AutoSize = true;
            this.chkDateFrom.Location = new System.Drawing.Point(12, 9);
            this.chkDateFrom.Name = "chkDateFrom";
            this.chkDateFrom.Size = new System.Drawing.Size(84, 17);
            this.chkDateFrom.TabIndex = 20;
            this.chkDateFrom.Text = "Date From";
            this.chkDateFrom.UseVisualStyleBackColor = true;
            // 
            // dtpDateTo
            // 
            this.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateTo.Location = new System.Drawing.Point(106, 32);
            this.dtpDateTo.Name = "dtpDateTo";
            this.dtpDateTo.Size = new System.Drawing.Size(140, 20);
            this.dtpDateTo.TabIndex = 19;
            // 
            // dtpDateFrom
            // 
            this.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateFrom.Location = new System.Drawing.Point(106, 9);
            this.dtpDateFrom.Name = "dtpDateFrom";
            this.dtpDateFrom.Size = new System.Drawing.Size(140, 20);
            this.dtpDateFrom.TabIndex = 18;
            // 
            // ChkParty
            // 
            this.ChkParty.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkParty.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkParty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkParty.Location = new System.Drawing.Point(13, 79);
            this.ChkParty.Name = "ChkParty";
            this.ChkParty.Size = new System.Drawing.Size(137, 24);
            this.ChkParty.TabIndex = 21;
            this.ChkParty.Text = "Party";
            this.ChkParty.UseVisualStyleBackColor = false;
            this.ChkParty.CheckedChanged += new System.EventHandler(this.ChkParty_CheckedChanged);
            // 
            // ChkStonesType
            // 
            this.ChkStonesType.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkStonesType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkStonesType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkStonesType.Location = new System.Drawing.Point(13, 108);
            this.ChkStonesType.Name = "ChkStonesType";
            this.ChkStonesType.Size = new System.Drawing.Size(137, 21);
            this.ChkStonesType.TabIndex = 22;
            this.ChkStonesType.Text = "Stones Type";
            this.ChkStonesType.UseVisualStyleBackColor = false;
            this.ChkStonesType.CheckedChanged += new System.EventHandler(this.ChkStonesType_CheckedChanged);
            // 
            // ChkDate
            // 
            this.ChkDate.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkDate.Location = new System.Drawing.Point(12, 161);
            this.ChkDate.Name = "ChkDate";
            this.ChkDate.Size = new System.Drawing.Size(138, 22);
            this.ChkDate.TabIndex = 23;
            this.ChkDate.Text = "Date";
            this.ChkDate.UseVisualStyleBackColor = false;
            this.ChkDate.CheckedChanged += new System.EventHandler(this.ChkDate_CheckedChanged);
            // 
            // ChkAll
            // 
            this.ChkAll.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkAll.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkAll.Location = new System.Drawing.Point(12, 188);
            this.ChkAll.Name = "ChkAll";
            this.ChkAll.Size = new System.Drawing.Size(138, 22);
            this.ChkAll.TabIndex = 24;
            this.ChkAll.Text = "All";
            this.ChkAll.UseVisualStyleBackColor = false;
            this.ChkAll.CheckedChanged += new System.EventHandler(this.ChkAll_CheckedChanged);
            // 
            // cbxStonesName
            // 
            this.cbxStonesName.Enabled = false;
            this.cbxStonesName.FormattingEnabled = true;
            this.cbxStonesName.Items.AddRange(new object[] {
            "Stones",
            "Beeds",
            "Diamond"});
            this.cbxStonesName.Location = new System.Drawing.Point(166, 134);
            this.cbxStonesName.Name = "cbxStonesName";
            this.cbxStonesName.Size = new System.Drawing.Size(137, 21);
            this.cbxStonesName.TabIndex = 25;
            // 
            // ChkStoneName
            // 
            this.ChkStoneName.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkStoneName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkStoneName.Location = new System.Drawing.Point(12, 134);
            this.ChkStoneName.Name = "ChkStoneName";
            this.ChkStoneName.Size = new System.Drawing.Size(138, 22);
            this.ChkStoneName.TabIndex = 26;
            this.ChkStoneName.Text = "Stones Name";
            this.ChkStoneName.UseVisualStyleBackColor = false;
            this.ChkStoneName.CheckedChanged += new System.EventHandler(this.ChkStoneName_CheckedChanged);
            // 
            // ChkStkSumary
            // 
            this.ChkStkSumary.BackColor = System.Drawing.Color.DarkKhaki;
            this.ChkStkSumary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkStkSumary.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ChkStkSumary.Location = new System.Drawing.Point(12, 215);
            this.ChkStkSumary.Name = "ChkStkSumary";
            this.ChkStkSumary.Size = new System.Drawing.Size(138, 32);
            this.ChkStkSumary.TabIndex = 28;
            this.ChkStkSumary.Text = "Stone Stock Summry";
            this.ChkStkSumary.UseVisualStyleBackColor = false;
            this.ChkStkSumary.CheckedChanged += new System.EventHandler(this.ChkStkSumary_CheckedChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Comic Sans MS", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(430, 52);
            this.lblTitle.TabIndex = 29;
            this.lblTitle.Text = "Loose Stone Report";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmStonesRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(430, 325);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.ChkStkSumary);
            this.Controls.Add(this.ChkStoneName);
            this.Controls.Add(this.cbxStonesName);
            this.Controls.Add(this.ChkAll);
            this.Controls.Add(this.ChkDate);
            this.Controls.Add(this.ChkStonesType);
            this.Controls.Add(this.ChkParty);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.cbxStonesType);
            this.Controls.Add(this.cbxParty);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmStonesRpt";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmStonesRpt";
            this.Load += new System.EventHandler(this.frmStonesRpt_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmStonesRpt_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxParty;
        private System.Windows.Forms.ComboBox cbxStonesType;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkDateTo;
        private System.Windows.Forms.CheckBox chkDateFrom;
        private System.Windows.Forms.DateTimePicker dtpDateTo;
        private System.Windows.Forms.DateTimePicker dtpDateFrom;
        private System.Windows.Forms.CheckBox ChkParty;
        private System.Windows.Forms.CheckBox ChkStonesType;
        private System.Windows.Forms.CheckBox ChkDate;
        private System.Windows.Forms.CheckBox ChkAll;
        private System.Windows.Forms.ComboBox cbxStonesName;
        private System.Windows.Forms.CheckBox ChkStoneName;
        private System.Windows.Forms.CheckBox ChkStkSumary;
        private System.Windows.Forms.Label lblTitle;
    }
}