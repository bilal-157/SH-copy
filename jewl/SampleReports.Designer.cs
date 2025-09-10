namespace jewl
{
    partial class SampleReports
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
            this.rbtSampleItmWise = new System.Windows.Forms.RadioButton();
            this.rbtDateWise = new System.Windows.Forms.RadioButton();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.pnlSampleItmWise = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.pnlSampleDateWise = new System.Windows.Forms.Panel();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtCompleteSample = new System.Windows.Forms.RadioButton();
            this.rbtSampleReturn = new System.Windows.Forms.RadioButton();
            this.pnlSampleItmWise.SuspendLayout();
            this.pnlSampleDateWise.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtSampleItmWise
            // 
            this.rbtSampleItmWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSampleItmWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSampleItmWise.Location = new System.Drawing.Point(13, 117);
            this.rbtSampleItmWise.Name = "rbtSampleItmWise";
            this.rbtSampleItmWise.Size = new System.Drawing.Size(155, 24);
            this.rbtSampleItmWise.TabIndex = 0;
            this.rbtSampleItmWise.TabStop = true;
            this.rbtSampleItmWise.Text = "Sample Item Wise";
            this.rbtSampleItmWise.UseVisualStyleBackColor = false;
            this.rbtSampleItmWise.CheckedChanged += new System.EventHandler(this.rbtSampleItmWise_CheckedChanged);
            // 
            // rbtDateWise
            // 
            this.rbtDateWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDateWise.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDateWise.Location = new System.Drawing.Point(13, 158);
            this.rbtDateWise.Name = "rbtDateWise";
            this.rbtDateWise.Size = new System.Drawing.Size(155, 24);
            this.rbtDateWise.TabIndex = 1;
            this.rbtDateWise.TabStop = true;
            this.rbtDateWise.Text = "Sample Date Wise";
            this.rbtDateWise.UseVisualStyleBackColor = false;
            this.rbtDateWise.CheckedChanged += new System.EventHandler(this.rbtDateWise_CheckedChanged);
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
            this.lblTitle.Size = new System.Drawing.Size(512, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sample Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(287, 253);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(109, 31);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(154, 253);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(103, 31);
            this.btnView.TabIndex = 1;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // pnlSampleItmWise
            // 
            this.pnlSampleItmWise.Controls.Add(this.label1);
            this.pnlSampleItmWise.Controls.Add(this.cbxGroupItem);
            this.pnlSampleItmWise.Location = new System.Drawing.Point(205, 100);
            this.pnlSampleItmWise.Name = "pnlSampleItmWise";
            this.pnlSampleItmWise.Size = new System.Drawing.Size(293, 47);
            this.pnlSampleItmWise.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Item Name";
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(98, 13);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(183, 21);
            this.cbxGroupItem.TabIndex = 5;
            // 
            // pnlSampleDateWise
            // 
            this.pnlSampleDateWise.Controls.Add(this.dtpTo);
            this.pnlSampleDateWise.Controls.Add(this.label4);
            this.pnlSampleDateWise.Controls.Add(this.dtpFrom);
            this.pnlSampleDateWise.Controls.Add(this.label3);
            this.pnlSampleDateWise.Location = new System.Drawing.Point(205, 153);
            this.pnlSampleDateWise.Name = "pnlSampleDateWise";
            this.pnlSampleDateWise.Size = new System.Drawing.Size(293, 48);
            this.pnlSampleDateWise.TabIndex = 7;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(200, 13);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(80, 20);
            this.dtpTo.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(152, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "To";
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(58, 13);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(82, 20);
            this.dtpFrom.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "From";
            // 
            // rbtCompleteSample
            // 
            this.rbtCompleteSample.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtCompleteSample.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtCompleteSample.Location = new System.Drawing.Point(13, 77);
            this.rbtCompleteSample.Name = "rbtCompleteSample";
            this.rbtCompleteSample.Size = new System.Drawing.Size(155, 24);
            this.rbtCompleteSample.TabIndex = 8;
            this.rbtCompleteSample.TabStop = true;
            this.rbtCompleteSample.Text = "Complete Sample";
            this.rbtCompleteSample.UseVisualStyleBackColor = false;
            // 
            // rbtSampleReturn
            // 
            this.rbtSampleReturn.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSampleReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSampleReturn.Location = new System.Drawing.Point(13, 203);
            this.rbtSampleReturn.Name = "rbtSampleReturn";
            this.rbtSampleReturn.Size = new System.Drawing.Size(155, 24);
            this.rbtSampleReturn.TabIndex = 9;
            this.rbtSampleReturn.TabStop = true;
            this.rbtSampleReturn.Text = "Sample Return";
            this.rbtSampleReturn.UseVisualStyleBackColor = false;
            this.rbtSampleReturn.CheckedChanged += new System.EventHandler(this.rbtSampleReturn_CheckedChanged);
            // 
            // SampleReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(512, 305);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.rbtSampleReturn);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.rbtCompleteSample);
            this.Controls.Add(this.pnlSampleDateWise);
            this.Controls.Add(this.pnlSampleItmWise);
            this.Controls.Add(this.rbtDateWise);
            this.Controls.Add(this.rbtSampleItmWise);
            this.Name = "SampleReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sample Reports";
            this.Load += new System.EventHandler(this.frmSampleReports_Load);
            this.pnlSampleItmWise.ResumeLayout(false);
            this.pnlSampleItmWise.PerformLayout();
            this.pnlSampleDateWise.ResumeLayout(false);
            this.pnlSampleDateWise.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtSampleItmWise;
        private System.Windows.Forms.RadioButton rbtDateWise;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Panel pnlSampleItmWise;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Panel pnlSampleDateWise;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.RadioButton rbtCompleteSample;
        private System.Windows.Forms.RadioButton rbtSampleReturn;
    }
}