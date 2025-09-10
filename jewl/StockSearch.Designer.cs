namespace jewl
{
    partial class StockSearch
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbxKarrat = new System.Windows.Forms.ComboBox();
            this.chkKarrat = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtWTo = new System.Windows.Forms.TextBox();
            this.txtWFrom = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.chkWeightRange = new System.Windows.Forms.CheckBox();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvShowRecords = new System.Windows.Forms.DataGridView();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tagNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weight = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.karrat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StockId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label11 = new System.Windows.Forms.Label();
            this.txtTagNo = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblRowCount = new System.Windows.Forms.Label();
            this.txtDesNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gray;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitle.Location = new System.Drawing.Point(5, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(721, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = ". . : : Search Item : : . .";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Window;
            this.label2.Location = new System.Drawing.Point(5, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Search Item";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.cbxKarrat);
            this.panel1.Controls.Add(this.chkKarrat);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.chkDateRange);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtWTo);
            this.panel1.Controls.Add(this.txtWFrom);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.chkWeightRange);
            this.panel1.Controls.Add(this.cbxGroupItem);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(5, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(333, 324);
            this.panel1.TabIndex = 2;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Goldenrod;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(175, 277);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 41);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "dd-MMM-yy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(197, 163);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(103, 20);
            this.dtpTo.TabIndex = 10;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "dd-MMM-yy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(50, 163);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(99, 20);
            this.dtpFrom.TabIndex = 9;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Goldenrod;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(61, 277);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 41);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "&Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbxKarrat
            // 
            this.cbxKarrat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKarrat.FormattingEnabled = true;
            this.cbxKarrat.Items.AddRange(new object[] {
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
            this.cbxKarrat.Location = new System.Drawing.Point(50, 228);
            this.cbxKarrat.Name = "cbxKarrat";
            this.cbxKarrat.Size = new System.Drawing.Size(250, 21);
            this.cbxKarrat.TabIndex = 5;
            this.cbxKarrat.SelectedIndexChanged += new System.EventHandler(this.cbxKarrat_SelectedIndexChanged);
            // 
            // chkKarrat
            // 
            this.chkKarrat.AutoSize = true;
            this.chkKarrat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkKarrat.Location = new System.Drawing.Point(13, 201);
            this.chkKarrat.Name = "chkKarrat";
            this.chkKarrat.Size = new System.Drawing.Size(60, 17);
            this.chkKarrat.TabIndex = 3;
            this.chkKarrat.Text = "Karrat";
            this.chkKarrat.UseVisualStyleBackColor = true;
            this.chkKarrat.Click += new System.EventHandler(this.chkKarrat_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(169, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(22, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "To";
            // 
            // chkDateRange
            // 
            this.chkDateRange.AutoSize = true;
            this.chkDateRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDateRange.Location = new System.Drawing.Point(12, 133);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(94, 17);
            this.chkDateRange.TabIndex = 3;
            this.chkDateRange.Text = "Date Range";
            this.chkDateRange.UseVisualStyleBackColor = true;
            this.chkDateRange.Click += new System.EventHandler(this.chkDateRange_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "From ";
            // 
            // txtWTo
            // 
            this.txtWTo.Location = new System.Drawing.Point(197, 96);
            this.txtWTo.Name = "txtWTo";
            this.txtWTo.Size = new System.Drawing.Size(103, 20);
            this.txtWTo.TabIndex = 4;
            this.txtWTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWTo_KeyPress);
            // 
            // txtWFrom
            // 
            this.txtWFrom.Location = new System.Drawing.Point(50, 96);
            this.txtWFrom.Name = "txtWFrom";
            this.txtWFrom.Size = new System.Drawing.Size(99, 20);
            this.txtWFrom.TabIndex = 5;
            this.txtWFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWFrom_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(167, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "To";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "From ";
            // 
            // chkWeightRange
            // 
            this.chkWeightRange.AutoSize = true;
            this.chkWeightRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkWeightRange.Location = new System.Drawing.Point(13, 69);
            this.chkWeightRange.Name = "chkWeightRange";
            this.chkWeightRange.Size = new System.Drawing.Size(107, 17);
            this.chkWeightRange.TabIndex = 3;
            this.chkWeightRange.Text = "Weight Range";
            this.chkWeightRange.UseVisualStyleBackColor = true;
            this.chkWeightRange.CheckedChanged += new System.EventHandler(this.chkWeightRange_CheckedChanged);
            this.chkWeightRange.Click += new System.EventHandler(this.chkWeightRange_Click);
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(13, 36);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(288, 21);
            this.cbxGroupItem.TabIndex = 3;
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxGroupItem.SelectionChangeCommitted += new System.EventHandler(this.cbxGroupItem_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Group Item";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.dgvShowRecords);
            this.panel3.Location = new System.Drawing.Point(378, 136);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(348, 237);
            this.panel3.TabIndex = 5;
            // 
            // dgvShowRecords
            // 
            this.dgvShowRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.tagNo,
            this.weight,
            this.karrat,
            this.date,
            this.Column1,
            this.StockId});
            this.dgvShowRecords.Location = new System.Drawing.Point(3, 5);
            this.dgvShowRecords.Name = "dgvShowRecords";
            this.dgvShowRecords.ReadOnly = true;
            this.dgvShowRecords.RowHeadersVisible = false;
            this.dgvShowRecords.Size = new System.Drawing.Size(340, 254);
            this.dgvShowRecords.TabIndex = 0;
            this.dgvShowRecords.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowRecords_CellClick);
            this.dgvShowRecords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowRecords_CellContentClick);
            // 
            // No
            // 
            this.No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.No.DefaultCellStyle = dataGridViewCellStyle1;
            this.No.HeaderText = "No";
            this.No.Name = "No";
            this.No.ReadOnly = true;
            // 
            // tagNo
            // 
            this.tagNo.HeaderText = "Tag No";
            this.tagNo.Name = "tagNo";
            this.tagNo.ReadOnly = true;
            this.tagNo.Width = 80;
            // 
            // weight
            // 
            this.weight.HeaderText = "Weight";
            this.weight.Name = "weight";
            this.weight.ReadOnly = true;
            this.weight.Width = 50;
            // 
            // karrat
            // 
            this.karrat.HeaderText = "Karrat";
            this.karrat.Name = "karrat";
            this.karrat.ReadOnly = true;
            this.karrat.Width = 50;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 80;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ItemId";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 30;
            // 
            // StockId
            // 
            this.StockId.HeaderText = "StockId";
            this.StockId.Name = "StockId";
            this.StockId.ReadOnly = true;
            this.StockId.Visible = false;
            this.StockId.Width = 20;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.ControlText;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.SystemColors.Window;
            this.label11.Location = new System.Drawing.Point(471, 381);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(174, 21);
            this.label11.TabIndex = 11;
            this.label11.Text = "Total Item Found:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTagNo
            // 
            this.txtTagNo.Location = new System.Drawing.Point(503, 84);
            this.txtTagNo.Name = "txtTagNo";
            this.txtTagNo.Size = new System.Drawing.Size(219, 20);
            this.txtTagNo.TabIndex = 0;
            this.txtTagNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTagNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTagNo_KeyDown);
            this.txtTagNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTagNo_KeyPress);
            this.txtTagNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTagNo_KeyUp);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.SystemColors.Window;
            this.label13.Location = new System.Drawing.Point(379, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(347, 19);
            this.label13.TabIndex = 14;
            this.label13.Text = "Search Tag Number";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(379, 84);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(108, 20);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tag Number";
            // 
            // lblRowCount
            // 
            this.lblRowCount.BackColor = System.Drawing.SystemColors.ControlText;
            this.lblRowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowCount.ForeColor = System.Drawing.SystemColors.Window;
            this.lblRowCount.Location = new System.Drawing.Point(613, 381);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(32, 21);
            this.lblRowCount.TabIndex = 15;
            this.lblRowCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDesNo
            // 
            this.txtDesNo.Location = new System.Drawing.Point(503, 110);
            this.txtDesNo.Name = "txtDesNo";
            this.txtDesNo.Size = new System.Drawing.Size(219, 20);
            this.txtDesNo.TabIndex = 16;
            this.txtDesNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDesNo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDesNo_KeyUp);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(379, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Refrence #";
            // 
            // StockSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(735, 409);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDesNo);
            this.Controls.Add(this.lblRowCount);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtTagNo);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label2);
            this.Name = "StockSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stocksearch";
            this.Load += new System.EventHandler(this.Stocksearch_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.StockSearch_Paint);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chkWeightRange;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.TextBox txtWTo;
        private System.Windows.Forms.TextBox txtWFrom;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkKarrat;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbxKarrat;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView dgvShowRecords;
        private System.Windows.Forms.TextBox txtTagNo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblRowCount;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn tagNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn weight;
        private System.Windows.Forms.DataGridViewTextBoxColumn karrat;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn StockId;
        private System.Windows.Forms.TextBox txtDesNo;
        private System.Windows.Forms.Label label1;
    }
}