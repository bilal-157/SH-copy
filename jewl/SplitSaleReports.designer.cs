namespace jewl
{
    partial class SplitSaleReports
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
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbtComplet = new System.Windows.Forms.RadioButton();
            this.rbtDateWise = new System.Windows.Forms.RadioButton();
            this.rbtItemWise = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Date";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dtpFromDate);
            this.panel3.Controls.Add(this.dtpToDate);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(214, 191);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 78);
            this.panel3.TabIndex = 29;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(105, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(125, 20);
            this.dtpFromDate.TabIndex = 14;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(105, 43);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(125, 20);
            this.dtpToDate.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "From";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(105, 7);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(125, 20);
            this.dtpDate.TabIndex = 15;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.dtpDate);
            this.panel4.Location = new System.Drawing.Point(214, 125);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(242, 35);
            this.panel4.TabIndex = 21;
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateRange.Location = new System.Drawing.Point(3, 3);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(94, 17);
            this.chkDateRange.TabIndex = 18;
            this.chkDateRange.Text = "Date Range";
            this.chkDateRange.UseVisualStyleBackColor = true;
            this.chkDateRange.CheckedChanged += new System.EventHandler(this.chkDateRange_CheckedChanged);
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(105, 10);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(125, 21);
            this.cbxGroupItem.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkDateRange);
            this.panel2.Location = new System.Drawing.Point(214, 163);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(99, 24);
            this.panel2.TabIndex = 28;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxGroupItem);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(214, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 41);
            this.panel1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Item Name";
            // 
            // rbtComplet
            // 
            this.rbtComplet.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtComplet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtComplet.Location = new System.Drawing.Point(16, 76);
            this.rbtComplet.Name = "rbtComplet";
            this.rbtComplet.Size = new System.Drawing.Size(188, 29);
            this.rbtComplet.TabIndex = 26;
            this.rbtComplet.TabStop = true;
            this.rbtComplet.Text = "Complet Report";
            this.rbtComplet.UseVisualStyleBackColor = false;
            // 
            // rbtDateWise
            // 
            this.rbtDateWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDateWise.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDateWise.Location = new System.Drawing.Point(16, 148);
            this.rbtDateWise.Name = "rbtDateWise";
            this.rbtDateWise.Size = new System.Drawing.Size(188, 29);
            this.rbtDateWise.TabIndex = 25;
            this.rbtDateWise.TabStop = true;
            this.rbtDateWise.Text = "Date Wise Report";
            this.rbtDateWise.UseVisualStyleBackColor = false;
            this.rbtDateWise.CheckedChanged += new System.EventHandler(this.rbtDateWise_CheckedChanged);
            // 
            // rbtItemWise
            // 
            this.rbtItemWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtItemWise.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemWise.Location = new System.Drawing.Point(16, 112);
            this.rbtItemWise.Name = "rbtItemWise";
            this.rbtItemWise.Size = new System.Drawing.Size(188, 29);
            this.rbtItemWise.TabIndex = 24;
            this.rbtItemWise.TabStop = true;
            this.rbtItemWise.Text = "Item Wise Report";
            this.rbtItemWise.UseVisualStyleBackColor = false;
            this.rbtItemWise.CheckedChanged += new System.EventHandler(this.rbtItemWise_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(245, 281);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(74, 31);
            this.btnExit.TabIndex = 23;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnView.Location = new System.Drawing.Point(130, 281);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(74, 31);
            this.btnView.TabIndex = 22;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(470, 53);
            this.lblTitle.TabIndex = 21;
            this.lblTitle.Text = "Split Sale";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SplitSaleReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(470, 327);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbtComplet);
            this.Controls.Add(this.rbtDateWise);
            this.Controls.Add(this.rbtItemWise);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblTitle);
            this.Name = "SplitSaleReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplitSaleReports";
            this.Load += new System.EventHandler(this.SplitSaleReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmSplitSaleReports_Paint);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbtComplet;
        private System.Windows.Forms.RadioButton rbtDateWise;
        private System.Windows.Forms.RadioButton rbtItemWise;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblTitle;
    }
}