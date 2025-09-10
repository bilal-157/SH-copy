namespace jewl
{
    partial class frmProfitLossReports
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
            this.rbtSaleNo = new System.Windows.Forms.RadioButton();
            this.rbtDateWise = new System.Windows.Forms.RadioButton();
            this.rbtItemWise = new System.Windows.Forms.RadioButton();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnView = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlDateWise = new System.Windows.Forms.Panel();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSaleNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlDateWise.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtSaleNo
            // 
            this.rbtSaleNo.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtSaleNo.Font = new System.Drawing.Font("Verdana", 7.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSaleNo.Location = new System.Drawing.Point(15, 77);
            this.rbtSaleNo.Name = "rbtSaleNo";
            this.rbtSaleNo.Size = new System.Drawing.Size(185, 29);
            this.rbtSaleNo.TabIndex = 16;
            this.rbtSaleNo.TabStop = true;
            this.rbtSaleNo.Text = "Sale No";
            this.rbtSaleNo.UseVisualStyleBackColor = false;
            this.rbtSaleNo.CheckedChanged += new System.EventHandler(this.rbtSaleNo_CheckedChanged);
            // 
            // rbtDateWise
            // 
            this.rbtDateWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtDateWise.Font = new System.Drawing.Font("Verdana", 7.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtDateWise.Location = new System.Drawing.Point(15, 149);
            this.rbtDateWise.Name = "rbtDateWise";
            this.rbtDateWise.Size = new System.Drawing.Size(185, 29);
            this.rbtDateWise.TabIndex = 15;
            this.rbtDateWise.TabStop = true;
            this.rbtDateWise.Text = "Date Wise Report";
            this.rbtDateWise.UseVisualStyleBackColor = false;
            this.rbtDateWise.CheckedChanged += new System.EventHandler(this.rbtDateWise_CheckedChanged);
            // 
            // rbtItemWise
            // 
            this.rbtItemWise.BackColor = System.Drawing.Color.DarkKhaki;
            this.rbtItemWise.Font = new System.Drawing.Font("Verdana", 7.7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtItemWise.Location = new System.Drawing.Point(15, 113);
            this.rbtItemWise.Name = "rbtItemWise";
            this.rbtItemWise.Size = new System.Drawing.Size(185, 29);
            this.rbtItemWise.TabIndex = 14;
            this.rbtItemWise.TabStop = true;
            this.rbtItemWise.Text = "Item Wise Report";
            this.rbtItemWise.UseVisualStyleBackColor = false;
            this.rbtItemWise.CheckedChanged += new System.EventHandler(this.rbtItemWise_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnExit.Location = new System.Drawing.Point(215, 237);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(63, 29);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnView
            // 
            this.btnView.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnView.Location = new System.Drawing.Point(112, 237);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(63, 29);
            this.btnView.TabIndex = 12;
            this.btnView.Text = "&View";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(419, 53);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "Profit/Loss Reports";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(283, 117);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(124, 21);
            this.cbxGroupItem.TabIndex = 17;
            this.cbxGroupItem.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(210, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Item Name";
            this.label2.Visible = false;
            // 
            // pnlDateWise
            // 
            this.pnlDateWise.Controls.Add(this.dtpFromDate);
            this.pnlDateWise.Controls.Add(this.dtpToDate);
            this.pnlDateWise.Controls.Add(this.label5);
            this.pnlDateWise.Controls.Add(this.label4);
            this.pnlDateWise.Location = new System.Drawing.Point(215, 144);
            this.pnlDateWise.Name = "pnlDateWise";
            this.pnlDateWise.Size = new System.Drawing.Size(192, 78);
            this.pnlDateWise.TabIndex = 21;
            this.pnlDateWise.Visible = false;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(79, 10);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(96, 20);
            this.dtpFromDate.TabIndex = 14;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(79, 43);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(96, 20);
            this.dtpToDate.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 17;
            this.label5.Text = "To";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "From";
            // 
            // txtSaleNo
            // 
            this.txtSaleNo.Location = new System.Drawing.Point(283, 81);
            this.txtSaleNo.Name = "txtSaleNo";
            this.txtSaleNo.Size = new System.Drawing.Size(124, 20);
            this.txtSaleNo.TabIndex = 22;
            this.txtSaleNo.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(214, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Sale No";
            this.label3.Visible = false;
            // 
            // frmProfitLossReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(419, 286);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSaleNo);
            this.Controls.Add(this.pnlDateWise);
            this.Controls.Add(this.cbxGroupItem);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.rbtSaleNo);
            this.Controls.Add(this.rbtDateWise);
            this.Controls.Add(this.rbtItemWise);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.lblTitle);
            this.Name = "frmProfitLossReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmProfitLossReports";
            this.Load += new System.EventHandler(this.frmProfitLossReports_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmProfitLossReports_Paint);
            this.pnlDateWise.ResumeLayout(false);
            this.pnlDateWise.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbtSaleNo;
        private System.Windows.Forms.RadioButton rbtDateWise;
        private System.Windows.Forms.RadioButton rbtItemWise;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlDateWise;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSaleNo;
        private System.Windows.Forms.Label label3;
    }
}