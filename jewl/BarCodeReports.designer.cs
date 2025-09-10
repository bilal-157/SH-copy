namespace jewl
{
    partial class BarCodeReports
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
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.rbtComStkRpt = new System.Windows.Forms.RadioButton();
            this.rbtTagNo = new System.Windows.Forms.RadioButton();
            this.rbtTagRange = new System.Windows.Forms.RadioButton();
            this.rbtStkSelection = new System.Windows.Forms.RadioButton();
            this.rbtManual = new System.Windows.Forms.RadioButton();
            this.pnlTagNo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTagNo = new System.Windows.Forms.TextBox();
            this.pnlTagRange = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.cbxItemName = new System.Windows.Forms.ComboBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.pnlManual = new System.Windows.Forms.Panel();
            this.txtTagNo12 = new System.Windows.Forms.TextBox();
            this.txtTagNo11 = new System.Windows.Forms.TextBox();
            this.txtTagNo10 = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTagNo9 = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtTagNo8 = new System.Windows.Forms.TextBox();
            this.txtTagNo7 = new System.Windows.Forms.TextBox();
            this.txtTagNo6 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTagNo5 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTagNo4 = new System.Windows.Forms.TextBox();
            this.txtTagNo3 = new System.Windows.Forms.TextBox();
            this.txtTagNo2 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTagNo1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlTagNo.SuspendLayout();
            this.pnlTagRange.SuspendLayout();
            this.pnlManual.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Gray;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(5, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(680, 47);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Bar Code";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(195, 359);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(95, 34);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(339, 359);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(95, 34);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // rbtComStkRpt
            // 
            this.rbtComStkRpt.AutoSize = true;
            this.rbtComStkRpt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtComStkRpt.Location = new System.Drawing.Point(67, 77);
            this.rbtComStkRpt.Name = "rbtComStkRpt";
            this.rbtComStkRpt.Size = new System.Drawing.Size(156, 17);
            this.rbtComStkRpt.TabIndex = 3;
            this.rbtComStkRpt.TabStop = true;
            this.rbtComStkRpt.Text = "Complete Stock Report";
            this.rbtComStkRpt.UseVisualStyleBackColor = true;
            this.rbtComStkRpt.CheckedChanged += new System.EventHandler(this.rbtComStkRpt_CheckedChanged);
            // 
            // rbtTagNo
            // 
            this.rbtTagNo.AutoSize = true;
            this.rbtTagNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtTagNo.Location = new System.Drawing.Point(244, 77);
            this.rbtTagNo.Name = "rbtTagNo";
            this.rbtTagNo.Size = new System.Drawing.Size(67, 17);
            this.rbtTagNo.TabIndex = 4;
            this.rbtTagNo.TabStop = true;
            this.rbtTagNo.Text = "Tag No";
            this.rbtTagNo.UseVisualStyleBackColor = true;
            this.rbtTagNo.CheckedChanged += new System.EventHandler(this.rbtTagNo_CheckedChanged);
            // 
            // rbtTagRange
            // 
            this.rbtTagRange.AutoSize = true;
            this.rbtTagRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtTagRange.Location = new System.Drawing.Point(347, 77);
            this.rbtTagRange.Name = "rbtTagRange";
            this.rbtTagRange.Size = new System.Drawing.Size(88, 17);
            this.rbtTagRange.TabIndex = 5;
            this.rbtTagRange.TabStop = true;
            this.rbtTagRange.Text = "Tag Range";
            this.rbtTagRange.UseVisualStyleBackColor = true;
            this.rbtTagRange.CheckedChanged += new System.EventHandler(this.rbtTagRange_CheckedChanged);
            // 
            // rbtStkSelection
            // 
            this.rbtStkSelection.AutoSize = true;
            this.rbtStkSelection.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtStkSelection.Location = new System.Drawing.Point(319, 32);
            this.rbtStkSelection.Name = "rbtStkSelection";
            this.rbtStkSelection.Size = new System.Drawing.Size(115, 17);
            this.rbtStkSelection.TabIndex = 6;
            this.rbtStkSelection.TabStop = true;
            this.rbtStkSelection.Text = "Stock Selection";
            this.rbtStkSelection.UseVisualStyleBackColor = true;
            // 
            // rbtManual
            // 
            this.rbtManual.AutoSize = true;
            this.rbtManual.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtManual.Location = new System.Drawing.Point(461, 77);
            this.rbtManual.Name = "rbtManual";
            this.rbtManual.Size = new System.Drawing.Size(66, 17);
            this.rbtManual.TabIndex = 7;
            this.rbtManual.TabStop = true;
            this.rbtManual.Text = "Manual";
            this.rbtManual.UseVisualStyleBackColor = true;
            this.rbtManual.CheckedChanged += new System.EventHandler(this.rbtManual_CheckedChanged);
            // 
            // pnlTagNo
            // 
            this.pnlTagNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTagNo.Controls.Add(this.label2);
            this.pnlTagNo.Controls.Add(this.txtTagNo);
            this.pnlTagNo.Location = new System.Drawing.Point(10, 116);
            this.pnlTagNo.Name = "pnlTagNo";
            this.pnlTagNo.Size = new System.Drawing.Size(580, 66);
            this.pnlTagNo.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(158, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "TagNo";
            // 
            // txtTagNo
            // 
            this.txtTagNo.Location = new System.Drawing.Point(240, 20);
            this.txtTagNo.Name = "txtTagNo";
            this.txtTagNo.Size = new System.Drawing.Size(144, 20);
            this.txtTagNo.TabIndex = 0;
            // 
            // pnlTagRange
            // 
            this.pnlTagRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTagRange.Controls.Add(this.label5);
            this.pnlTagRange.Controls.Add(this.cbxItemName);
            this.pnlTagRange.Controls.Add(this.txtTo);
            this.pnlTagRange.Controls.Add(this.label4);
            this.pnlTagRange.Controls.Add(this.label3);
            this.pnlTagRange.Controls.Add(this.txtFrom);
            this.pnlTagRange.Location = new System.Drawing.Point(10, 116);
            this.pnlTagRange.Name = "pnlTagRange";
            this.pnlTagRange.Size = new System.Drawing.Size(675, 80);
            this.pnlTagRange.TabIndex = 9;
            this.pnlTagRange.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTagRange_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Item Name";
            // 
            // cbxItemName
            // 
            this.cbxItemName.FormattingEnabled = true;
            this.cbxItemName.Location = new System.Drawing.Point(13, 38);
            this.cbxItemName.Name = "cbxItemName";
            this.cbxItemName.Size = new System.Drawing.Size(190, 21);
            this.cbxItemName.TabIndex = 4;
            this.cbxItemName.SelectedIndexChanged += new System.EventHandler(this.cbxItemName_SelectedIndexChanged);
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(563, 38);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(100, 20);
            this.txtTo.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(560, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(326, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "From";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(329, 38);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(105, 20);
            this.txtFrom.TabIndex = 0;
            // 
            // pnlManual
            // 
            this.pnlManual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlManual.Controls.Add(this.txtTagNo12);
            this.pnlManual.Controls.Add(this.txtTagNo11);
            this.pnlManual.Controls.Add(this.txtTagNo10);
            this.pnlManual.Controls.Add(this.label17);
            this.pnlManual.Controls.Add(this.label18);
            this.pnlManual.Controls.Add(this.label19);
            this.pnlManual.Controls.Add(this.txtTagNo9);
            this.pnlManual.Controls.Add(this.label20);
            this.pnlManual.Controls.Add(this.txtTagNo8);
            this.pnlManual.Controls.Add(this.txtTagNo7);
            this.pnlManual.Controls.Add(this.txtTagNo6);
            this.pnlManual.Controls.Add(this.label13);
            this.pnlManual.Controls.Add(this.label14);
            this.pnlManual.Controls.Add(this.label15);
            this.pnlManual.Controls.Add(this.txtTagNo5);
            this.pnlManual.Controls.Add(this.label16);
            this.pnlManual.Controls.Add(this.txtTagNo4);
            this.pnlManual.Controls.Add(this.txtTagNo3);
            this.pnlManual.Controls.Add(this.txtTagNo2);
            this.pnlManual.Controls.Add(this.label12);
            this.pnlManual.Controls.Add(this.label11);
            this.pnlManual.Controls.Add(this.label10);
            this.pnlManual.Controls.Add(this.txtTagNo1);
            this.pnlManual.Controls.Add(this.label9);
            this.pnlManual.Location = new System.Drawing.Point(10, 202);
            this.pnlManual.Name = "pnlManual";
            this.pnlManual.Size = new System.Drawing.Size(675, 141);
            this.pnlManual.TabIndex = 11;
            this.pnlManual.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlManual_Paint);
            // 
            // txtTagNo12
            // 
            this.txtTagNo12.Location = new System.Drawing.Point(563, 106);
            this.txtTagNo12.Name = "txtTagNo12";
            this.txtTagNo12.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo12.TabIndex = 26;
            // 
            // txtTagNo11
            // 
            this.txtTagNo11.Location = new System.Drawing.Point(331, 106);
            this.txtTagNo11.Name = "txtTagNo11";
            this.txtTagNo11.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo11.TabIndex = 25;
            // 
            // txtTagNo10
            // 
            this.txtTagNo10.Location = new System.Drawing.Point(100, 106);
            this.txtTagNo10.Name = "txtTagNo10";
            this.txtTagNo10.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo10.TabIndex = 24;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(466, 109);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(72, 13);
            this.label17.TabIndex = 23;
            this.label17.Text = "Tag No.12";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(225, 110);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(72, 13);
            this.label18.TabIndex = 22;
            this.label18.Text = "Tag No.11";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 109);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(72, 13);
            this.label19.TabIndex = 21;
            this.label19.Text = "Tag No.10";
            // 
            // txtTagNo9
            // 
            this.txtTagNo9.Location = new System.Drawing.Point(563, 75);
            this.txtTagNo9.Name = "txtTagNo9";
            this.txtTagNo9.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo9.TabIndex = 20;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(466, 78);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(64, 13);
            this.label20.TabIndex = 19;
            this.label20.Text = "Tag No.9";
            // 
            // txtTagNo8
            // 
            this.txtTagNo8.Location = new System.Drawing.Point(331, 74);
            this.txtTagNo8.Name = "txtTagNo8";
            this.txtTagNo8.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo8.TabIndex = 18;
            // 
            // txtTagNo7
            // 
            this.txtTagNo7.Location = new System.Drawing.Point(100, 74);
            this.txtTagNo7.Name = "txtTagNo7";
            this.txtTagNo7.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo7.TabIndex = 17;
            // 
            // txtTagNo6
            // 
            this.txtTagNo6.Location = new System.Drawing.Point(563, 42);
            this.txtTagNo6.Name = "txtTagNo6";
            this.txtTagNo6.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo6.TabIndex = 16;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(225, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "Tag No.8";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 78);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 13);
            this.label14.TabIndex = 14;
            this.label14.Text = "Tag No.7";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(466, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 13);
            this.label15.TabIndex = 13;
            this.label15.Text = "Tag No.6";
            // 
            // txtTagNo5
            // 
            this.txtTagNo5.Location = new System.Drawing.Point(331, 42);
            this.txtTagNo5.Name = "txtTagNo5";
            this.txtTagNo5.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo5.TabIndex = 12;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(225, 45);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Tag No.5";
            // 
            // txtTagNo4
            // 
            this.txtTagNo4.Location = new System.Drawing.Point(100, 42);
            this.txtTagNo4.Name = "txtTagNo4";
            this.txtTagNo4.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo4.TabIndex = 10;
            // 
            // txtTagNo3
            // 
            this.txtTagNo3.Location = new System.Drawing.Point(563, 12);
            this.txtTagNo3.Name = "txtTagNo3";
            this.txtTagNo3.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo3.TabIndex = 9;
            // 
            // txtTagNo2
            // 
            this.txtTagNo2.Location = new System.Drawing.Point(331, 12);
            this.txtTagNo2.Name = "txtTagNo2";
            this.txtTagNo2.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo2.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 45);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 7;
            this.label12.Text = "Tag No.4";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(466, 16);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(64, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "Tag No.3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(225, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(64, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Tag No.2";
            // 
            // txtTagNo1
            // 
            this.txtTagNo1.Location = new System.Drawing.Point(100, 11);
            this.txtTagNo1.Name = "txtTagNo1";
            this.txtTagNo1.Size = new System.Drawing.Size(103, 20);
            this.txtTagNo1.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Tag No.1";
            // 
            // BarCodeReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(689, 417);
            this.Controls.Add(this.pnlManual);
            this.Controls.Add(this.pnlTagRange);
            this.Controls.Add(this.pnlTagNo);
            this.Controls.Add(this.rbtManual);
            this.Controls.Add(this.rbtTagRange);
            this.Controls.Add(this.rbtTagNo);
            this.Controls.Add(this.rbtComStkRpt);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.rbtStkSelection);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BarCodeReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...:::::Jewel Manager:::::...";
            this.Load += new System.EventHandler(this.BarCodeReports_Load);
            this.pnlTagNo.ResumeLayout(false);
            this.pnlTagNo.PerformLayout();
            this.pnlTagRange.ResumeLayout(false);
            this.pnlTagRange.PerformLayout();
            this.pnlManual.ResumeLayout(false);
            this.pnlManual.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.RadioButton rbtComStkRpt;
        private System.Windows.Forms.RadioButton rbtTagNo;
        private System.Windows.Forms.RadioButton rbtTagRange;
        private System.Windows.Forms.RadioButton rbtStkSelection;
        private System.Windows.Forms.RadioButton rbtManual;
        private System.Windows.Forms.Panel pnlTagNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTagNo;
        private System.Windows.Forms.Panel pnlTagRange;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbxItemName;
        private System.Windows.Forms.Panel pnlManual;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTagNo1;
        private System.Windows.Forms.TextBox txtTagNo12;
        private System.Windows.Forms.TextBox txtTagNo11;
        private System.Windows.Forms.TextBox txtTagNo10;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtTagNo9;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtTagNo8;
        private System.Windows.Forms.TextBox txtTagNo7;
        private System.Windows.Forms.TextBox txtTagNo6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTagNo5;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTagNo4;
        private System.Windows.Forms.TextBox txtTagNo3;
        private System.Windows.Forms.TextBox txtTagNo2;
        private System.Windows.Forms.Label label12;
    }
}