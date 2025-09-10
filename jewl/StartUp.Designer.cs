namespace jewl
{
    partial class StartUp
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
            this.rbtManual = new System.Windows.Forms.RadioButton();
            this.rbtAuto = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxKarat = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlRateFixing = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.pnlRateTypeFixing = new System.Windows.Forms.Panel();
            this.cbxWeightInGram = new System.Windows.Forms.ComboBox();
            this.rbtSonaPasa = new System.Windows.Forms.RadioButton();
            this.rbtStandard = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.chkSaleRate = new System.Windows.Forms.CheckBox();
            this.chkRateType = new System.Windows.Forms.CheckBox();
            this.chkBackUp = new System.Windows.Forms.CheckBox();
            this.pnlBackUp = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBackUp = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pnlJewlManager = new System.Windows.Forms.Panel();
            this.rbtStockSale = new System.Windows.Forms.RadioButton();
            this.rbtTagging = new System.Windows.Forms.RadioButton();
            this.rbtComplete = new System.Windows.Forms.RadioButton();
            this.chkJewlManger = new System.Windows.Forms.CheckBox();
            this.chkReportPasword = new System.Windows.Forms.CheckBox();
            this.pnlReports = new System.Windows.Forms.Panel();
            this.txtReportPassword = new System.Windows.Forms.TextBox();
            this.chkRateTotaGram = new System.Windows.Forms.CheckBox();
            this.pnlRateTolaGram = new System.Windows.Forms.Panel();
            this.rbtTola = new System.Windows.Forms.RadioButton();
            this.rbtGram = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlSalary = new System.Windows.Forms.Panel();
            this.rbtWithoutAttendance = new System.Windows.Forms.RadioButton();
            this.rbtWithAttendance = new System.Windows.Forms.RadioButton();
            this.chkSalary = new System.Windows.Forms.CheckBox();
            this.pnlRateFixing.SuspendLayout();
            this.pnlRateTypeFixing.SuspendLayout();
            this.pnlBackUp.SuspendLayout();
            this.pnlJewlManager.SuspendLayout();
            this.pnlReports.SuspendLayout();
            this.pnlRateTolaGram.SuspendLayout();
            this.pnlSalary.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtManual
            // 
            this.rbtManual.AutoSize = true;
            this.rbtManual.Location = new System.Drawing.Point(241, 20);
            this.rbtManual.Name = "rbtManual";
            this.rbtManual.Size = new System.Drawing.Size(60, 17);
            this.rbtManual.TabIndex = 0;
            this.rbtManual.Text = "Manual";
            this.rbtManual.UseVisualStyleBackColor = true;
            this.rbtManual.CheckedChanged += new System.EventHandler(this.rbtManual_CheckedChanged_1);
            // 
            // rbtAuto
            // 
            this.rbtAuto.AutoSize = true;
            this.rbtAuto.Checked = true;
            this.rbtAuto.Location = new System.Drawing.Point(121, 20);
            this.rbtAuto.Name = "rbtAuto";
            this.rbtAuto.Size = new System.Drawing.Size(47, 17);
            this.rbtAuto.TabIndex = 1;
            this.rbtAuto.TabStop = true;
            this.rbtAuto.Text = "Auto";
            this.rbtAuto.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Sale Rate Fixing";
            // 
            // cbxKarat
            // 
            this.cbxKarat.Enabled = false;
            this.cbxKarat.FormattingEnabled = true;
            this.cbxKarat.Items.AddRange(new object[] {
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
            this.cbxKarat.Location = new System.Drawing.Point(357, 19);
            this.cbxKarat.Name = "cbxKarat";
            this.cbxKarat.Size = new System.Drawing.Size(121, 21);
            this.cbxKarat.TabIndex = 3;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(256, 511);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlRateFixing
            // 
            this.pnlRateFixing.Controls.Add(this.cbxKarat);
            this.pnlRateFixing.Controls.Add(this.rbtManual);
            this.pnlRateFixing.Controls.Add(this.rbtAuto);
            this.pnlRateFixing.Controls.Add(this.label1);
            this.pnlRateFixing.Enabled = false;
            this.pnlRateFixing.Location = new System.Drawing.Point(132, 53);
            this.pnlRateFixing.Name = "pnlRateFixing";
            this.pnlRateFixing.Size = new System.Drawing.Size(497, 58);
            this.pnlRateFixing.TabIndex = 5;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(337, 511);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // pnlRateTypeFixing
            // 
            this.pnlRateTypeFixing.Controls.Add(this.cbxWeightInGram);
            this.pnlRateTypeFixing.Controls.Add(this.rbtSonaPasa);
            this.pnlRateTypeFixing.Controls.Add(this.rbtStandard);
            this.pnlRateTypeFixing.Controls.Add(this.label2);
            this.pnlRateTypeFixing.Enabled = false;
            this.pnlRateTypeFixing.Location = new System.Drawing.Point(132, 117);
            this.pnlRateTypeFixing.Name = "pnlRateTypeFixing";
            this.pnlRateTypeFixing.Size = new System.Drawing.Size(497, 58);
            this.pnlRateTypeFixing.TabIndex = 6;
            // 
            // cbxWeightInGram
            // 
            this.cbxWeightInGram.FormattingEnabled = true;
            this.cbxWeightInGram.Items.AddRange(new object[] {
            "11.664",
            "12.150"});
            this.cbxWeightInGram.Location = new System.Drawing.Point(357, 19);
            this.cbxWeightInGram.Name = "cbxWeightInGram";
            this.cbxWeightInGram.Size = new System.Drawing.Size(121, 21);
            this.cbxWeightInGram.TabIndex = 3;
            // 
            // rbtSonaPasa
            // 
            this.rbtSonaPasa.AutoSize = true;
            this.rbtSonaPasa.Location = new System.Drawing.Point(241, 20);
            this.rbtSonaPasa.Name = "rbtSonaPasa";
            this.rbtSonaPasa.Size = new System.Drawing.Size(105, 17);
            this.rbtSonaPasa.TabIndex = 0;
            this.rbtSonaPasa.Text = "PoundSonaPasa";
            this.rbtSonaPasa.UseVisualStyleBackColor = true;
            // 
            // rbtStandard
            // 
            this.rbtStandard.AutoSize = true;
            this.rbtStandard.Checked = true;
            this.rbtStandard.Location = new System.Drawing.Point(121, 20);
            this.rbtStandard.Name = "rbtStandard";
            this.rbtStandard.Size = new System.Drawing.Size(113, 17);
            this.rbtStandard.TabIndex = 1;
            this.rbtStandard.TabStop = true;
            this.rbtStandard.Text = "StandardGoldRate";
            this.rbtStandard.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "GoldRateType";
            // 
            // chkSaleRate
            // 
            this.chkSaleRate.AutoSize = true;
            this.chkSaleRate.Location = new System.Drawing.Point(5, 72);
            this.chkSaleRate.Name = "chkSaleRate";
            this.chkSaleRate.Size = new System.Drawing.Size(89, 17);
            this.chkSaleRate.TabIndex = 7;
            this.chkSaleRate.Text = "Sale Rate Fix";
            this.chkSaleRate.UseVisualStyleBackColor = true;
            this.chkSaleRate.CheckedChanged += new System.EventHandler(this.chkSaleRate_CheckedChanged_1);
            // 
            // chkRateType
            // 
            this.chkRateType.AutoSize = true;
            this.chkRateType.Location = new System.Drawing.Point(5, 138);
            this.chkRateType.Name = "chkRateType";
            this.chkRateType.Size = new System.Drawing.Size(92, 17);
            this.chkRateType.TabIndex = 8;
            this.chkRateType.Text = "Rate Type Fix";
            this.chkRateType.UseVisualStyleBackColor = true;
            this.chkRateType.CheckedChanged += new System.EventHandler(this.chkRateType_CheckedChanged_1);
            // 
            // chkBackUp
            // 
            this.chkBackUp.AutoSize = true;
            this.chkBackUp.Location = new System.Drawing.Point(5, 201);
            this.chkBackUp.Name = "chkBackUp";
            this.chkBackUp.Size = new System.Drawing.Size(65, 17);
            this.chkBackUp.TabIndex = 9;
            this.chkBackUp.Text = "BackUp";
            this.chkBackUp.UseVisualStyleBackColor = true;
            this.chkBackUp.CheckedChanged += new System.EventHandler(this.chkBackUp_CheckedChanged);
            // 
            // pnlBackUp
            // 
            this.pnlBackUp.Controls.Add(this.label3);
            this.pnlBackUp.Controls.Add(this.txtBackUp);
            this.pnlBackUp.Enabled = false;
            this.pnlBackUp.Location = new System.Drawing.Point(132, 184);
            this.pnlBackUp.Name = "pnlBackUp";
            this.pnlBackUp.Size = new System.Drawing.Size(497, 60);
            this.pnlBackUp.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "BackUp";
            // 
            // txtBackUp
            // 
            this.txtBackUp.Location = new System.Drawing.Point(121, 17);
            this.txtBackUp.Name = "txtBackUp";
            this.txtBackUp.Size = new System.Drawing.Size(357, 20);
            this.txtBackUp.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pnlJewlManager
            // 
            this.pnlJewlManager.Controls.Add(this.rbtStockSale);
            this.pnlJewlManager.Controls.Add(this.rbtTagging);
            this.pnlJewlManager.Controls.Add(this.rbtComplete);
            this.pnlJewlManager.Enabled = false;
            this.pnlJewlManager.Location = new System.Drawing.Point(132, 253);
            this.pnlJewlManager.Name = "pnlJewlManager";
            this.pnlJewlManager.Size = new System.Drawing.Size(497, 60);
            this.pnlJewlManager.TabIndex = 11;
            // 
            // rbtStockSale
            // 
            this.rbtStockSale.AutoSize = true;
            this.rbtStockSale.Location = new System.Drawing.Point(241, 16);
            this.rbtStockSale.Name = "rbtStockSale";
            this.rbtStockSale.Size = new System.Drawing.Size(77, 17);
            this.rbtStockSale.TabIndex = 5;
            this.rbtStockSale.Text = "Stock& Sale";
            this.rbtStockSale.UseVisualStyleBackColor = true;
            // 
            // rbtTagging
            // 
            this.rbtTagging.AutoSize = true;
            this.rbtTagging.Location = new System.Drawing.Point(145, 16);
            this.rbtTagging.Name = "rbtTagging";
            this.rbtTagging.Size = new System.Drawing.Size(64, 17);
            this.rbtTagging.TabIndex = 4;
            this.rbtTagging.Text = "Tagging";
            this.rbtTagging.UseVisualStyleBackColor = true;
            // 
            // rbtComplete
            // 
            this.rbtComplete.AutoSize = true;
            this.rbtComplete.Checked = true;
            this.rbtComplete.Location = new System.Drawing.Point(34, 16);
            this.rbtComplete.Name = "rbtComplete";
            this.rbtComplete.Size = new System.Drawing.Size(69, 17);
            this.rbtComplete.TabIndex = 4;
            this.rbtComplete.TabStop = true;
            this.rbtComplete.Text = "Complete";
            this.rbtComplete.UseVisualStyleBackColor = true;
            // 
            // chkJewlManger
            // 
            this.chkJewlManger.AutoSize = true;
            this.chkJewlManger.Location = new System.Drawing.Point(5, 269);
            this.chkJewlManger.Name = "chkJewlManger";
            this.chkJewlManger.Size = new System.Drawing.Size(116, 17);
            this.chkJewlManger.TabIndex = 12;
            this.chkJewlManger.Text = "JewlManager Type";
            this.chkJewlManger.UseVisualStyleBackColor = true;
            this.chkJewlManger.CheckedChanged += new System.EventHandler(this.chkJewlManger_CheckedChanged);
            // 
            // chkReportPasword
            // 
            this.chkReportPasword.AutoSize = true;
            this.chkReportPasword.Location = new System.Drawing.Point(5, 341);
            this.chkReportPasword.Name = "chkReportPasword";
            this.chkReportPasword.Size = new System.Drawing.Size(112, 17);
            this.chkReportPasword.TabIndex = 13;
            this.chkReportPasword.Text = "Reports Password";
            this.chkReportPasword.UseVisualStyleBackColor = true;
            this.chkReportPasword.CheckedChanged += new System.EventHandler(this.chkReportPasword_CheckedChanged);
            // 
            // pnlReports
            // 
            this.pnlReports.Controls.Add(this.txtReportPassword);
            this.pnlReports.Enabled = false;
            this.pnlReports.Location = new System.Drawing.Point(132, 319);
            this.pnlReports.Name = "pnlReports";
            this.pnlReports.Size = new System.Drawing.Size(497, 60);
            this.pnlReports.TabIndex = 14;
            // 
            // txtReportPassword
            // 
            this.txtReportPassword.Location = new System.Drawing.Point(121, 20);
            this.txtReportPassword.Name = "txtReportPassword";
            this.txtReportPassword.Size = new System.Drawing.Size(357, 20);
            this.txtReportPassword.TabIndex = 5;
            // 
            // chkRateTotaGram
            // 
            this.chkRateTotaGram.AutoSize = true;
            this.chkRateTotaGram.Location = new System.Drawing.Point(5, 402);
            this.chkRateTotaGram.Name = "chkRateTotaGram";
            this.chkRateTotaGram.Size = new System.Drawing.Size(95, 17);
            this.chkRateTotaGram.TabIndex = 15;
            this.chkRateTotaGram.Text = "RateTolaGram";
            this.chkRateTotaGram.UseVisualStyleBackColor = true;
            this.chkRateTotaGram.CheckedChanged += new System.EventHandler(this.chkRateTotaGram_CheckedChanged);
            // 
            // pnlRateTolaGram
            // 
            this.pnlRateTolaGram.Controls.Add(this.rbtTola);
            this.pnlRateTolaGram.Controls.Add(this.rbtGram);
            this.pnlRateTolaGram.Enabled = false;
            this.pnlRateTolaGram.Location = new System.Drawing.Point(132, 385);
            this.pnlRateTolaGram.Name = "pnlRateTolaGram";
            this.pnlRateTolaGram.Size = new System.Drawing.Size(497, 60);
            this.pnlRateTolaGram.TabIndex = 15;
            // 
            // rbtTola
            // 
            this.rbtTola.AutoSize = true;
            this.rbtTola.Location = new System.Drawing.Point(251, 17);
            this.rbtTola.Name = "rbtTola";
            this.rbtTola.Size = new System.Drawing.Size(46, 17);
            this.rbtTola.TabIndex = 7;
            this.rbtTola.Text = "Tola";
            this.rbtTola.UseVisualStyleBackColor = true;
            // 
            // rbtGram
            // 
            this.rbtGram.AutoSize = true;
            this.rbtGram.Checked = true;
            this.rbtGram.Location = new System.Drawing.Point(155, 17);
            this.rbtGram.Name = "rbtGram";
            this.rbtGram.Size = new System.Drawing.Size(50, 17);
            this.rbtGram.TabIndex = 6;
            this.rbtGram.TabStop = true;
            this.rbtGram.Text = "Gram";
            this.rbtGram.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(241, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(159, 25);
            this.label4.TabIndex = 16;
            this.label4.Text = "StartUp Setting";
            // 
            // pnlSalary
            // 
            this.pnlSalary.Controls.Add(this.rbtWithoutAttendance);
            this.pnlSalary.Controls.Add(this.rbtWithAttendance);
            this.pnlSalary.Enabled = false;
            this.pnlSalary.Location = new System.Drawing.Point(132, 451);
            this.pnlSalary.Name = "pnlSalary";
            this.pnlSalary.Size = new System.Drawing.Size(497, 42);
            this.pnlSalary.TabIndex = 17;
            // 
            // rbtWithoutAttendance
            // 
            this.rbtWithoutAttendance.AutoSize = true;
            this.rbtWithoutAttendance.Checked = true;
            this.rbtWithoutAttendance.Location = new System.Drawing.Point(279, 11);
            this.rbtWithoutAttendance.Name = "rbtWithoutAttendance";
            this.rbtWithoutAttendance.Size = new System.Drawing.Size(120, 17);
            this.rbtWithoutAttendance.TabIndex = 7;
            this.rbtWithoutAttendance.TabStop = true;
            this.rbtWithoutAttendance.Text = "Without Attendance";
            this.rbtWithoutAttendance.UseVisualStyleBackColor = true;
            // 
            // rbtWithAttendance
            // 
            this.rbtWithAttendance.AutoSize = true;
            this.rbtWithAttendance.Location = new System.Drawing.Point(101, 11);
            this.rbtWithAttendance.Name = "rbtWithAttendance";
            this.rbtWithAttendance.Size = new System.Drawing.Size(105, 17);
            this.rbtWithAttendance.TabIndex = 6;
            this.rbtWithAttendance.Text = "With Attendance";
            this.rbtWithAttendance.UseVisualStyleBackColor = true;
            // 
            // chkSalary
            // 
            this.chkSalary.AutoSize = true;
            this.chkSalary.Location = new System.Drawing.Point(5, 465);
            this.chkSalary.Name = "chkSalary";
            this.chkSalary.Size = new System.Drawing.Size(110, 17);
            this.chkSalary.TabIndex = 18;
            this.chkSalary.Text = "Salary Calculation";
            this.chkSalary.UseVisualStyleBackColor = true;
            this.chkSalary.CheckedChanged += new System.EventHandler(this.chkSalary_CheckedChanged);
            // 
            // StartUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 550);
            this.Controls.Add(this.pnlSalary);
            this.Controls.Add(this.chkSalary);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlRateTolaGram);
            this.Controls.Add(this.chkRateTotaGram);
            this.Controls.Add(this.pnlReports);
            this.Controls.Add(this.chkReportPasword);
            this.Controls.Add(this.chkJewlManger);
            this.Controls.Add(this.pnlJewlManager);
            this.Controls.Add(this.pnlBackUp);
            this.Controls.Add(this.chkBackUp);
            this.Controls.Add(this.chkRateType);
            this.Controls.Add(this.chkSaleRate);
            this.Controls.Add(this.pnlRateTypeFixing);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pnlRateFixing);
            this.Controls.Add(this.btnSave);
            this.Name = "StartUp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartUp";
            this.pnlRateFixing.ResumeLayout(false);
            this.pnlRateFixing.PerformLayout();
            this.pnlRateTypeFixing.ResumeLayout(false);
            this.pnlRateTypeFixing.PerformLayout();
            this.pnlBackUp.ResumeLayout(false);
            this.pnlBackUp.PerformLayout();
            this.pnlJewlManager.ResumeLayout(false);
            this.pnlJewlManager.PerformLayout();
            this.pnlReports.ResumeLayout(false);
            this.pnlReports.PerformLayout();
            this.pnlRateTolaGram.ResumeLayout(false);
            this.pnlRateTolaGram.PerformLayout();
            this.pnlSalary.ResumeLayout(false);
            this.pnlSalary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtManual;
        private System.Windows.Forms.RadioButton rbtAuto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxKarat;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlRateFixing;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel pnlRateTypeFixing;
        private System.Windows.Forms.ComboBox cbxWeightInGram;
        private System.Windows.Forms.RadioButton rbtSonaPasa;
        private System.Windows.Forms.RadioButton rbtStandard;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkSaleRate;
        private System.Windows.Forms.CheckBox chkRateType;
        private System.Windows.Forms.CheckBox chkBackUp;
        private System.Windows.Forms.Panel pnlBackUp;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtBackUp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlJewlManager;
        private System.Windows.Forms.RadioButton rbtStockSale;
        private System.Windows.Forms.RadioButton rbtTagging;
        private System.Windows.Forms.RadioButton rbtComplete;
        private System.Windows.Forms.CheckBox chkJewlManger;
        private System.Windows.Forms.CheckBox chkReportPasword;
        private System.Windows.Forms.Panel pnlReports;
        private System.Windows.Forms.TextBox txtReportPassword;
        private System.Windows.Forms.CheckBox chkRateTotaGram;
        private System.Windows.Forms.Panel pnlRateTolaGram;
        private System.Windows.Forms.RadioButton rbtTola;
        private System.Windows.Forms.RadioButton rbtGram;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlSalary;
        private System.Windows.Forms.RadioButton rbtWithoutAttendance;
        private System.Windows.Forms.RadioButton rbtWithAttendance;
        private System.Windows.Forms.CheckBox chkSalary;
    }
}