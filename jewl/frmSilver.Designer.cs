namespace jewl
{
    partial class frmSilver
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
            this.txtTotalPriceSilver = new System.Windows.Forms.TextBox();
            this.txtRateD = new System.Windows.Forms.TextBox();
            this.txtPriceD = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.txtPriceA = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtRateA = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.chkTagNo = new System.Windows.Forms.CheckBox();
            this.cbxTagNo = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTotalPriceSilver
            // 
            this.txtTotalPriceSilver.Location = new System.Drawing.Point(115, 339);
            this.txtTotalPriceSilver.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalPriceSilver.Name = "txtTotalPriceSilver";
            this.txtTotalPriceSilver.Size = new System.Drawing.Size(182, 20);
            this.txtTotalPriceSilver.TabIndex = 60;
            // 
            // txtRateD
            // 
            this.txtRateD.Location = new System.Drawing.Point(115, 267);
            this.txtRateD.Margin = new System.Windows.Forms.Padding(4);
            this.txtRateD.Name = "txtRateD";
            this.txtRateD.Size = new System.Drawing.Size(182, 20);
            this.txtRateD.TabIndex = 58;
            // 
            // txtPriceD
            // 
            this.txtPriceD.Location = new System.Drawing.Point(115, 303);
            this.txtPriceD.Margin = new System.Windows.Forms.Padding(4);
            this.txtPriceD.Name = "txtPriceD";
            this.txtPriceD.Size = new System.Drawing.Size(182, 20);
            this.txtPriceD.TabIndex = 59;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(27, 198);
            this.label41.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(40, 13);
            this.label41.TabIndex = 51;
            this.label41.Text = "Rate A";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(27, 235);
            this.label40.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(41, 13);
            this.label40.TabIndex = 52;
            this.label40.Text = "Price A";
            // 
            // txtPriceA
            // 
            this.txtPriceA.Location = new System.Drawing.Point(115, 231);
            this.txtPriceA.Margin = new System.Windows.Forms.Padding(4);
            this.txtPriceA.Name = "txtPriceA";
            this.txtPriceA.Size = new System.Drawing.Size(182, 20);
            this.txtPriceA.TabIndex = 57;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(27, 271);
            this.label39.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(41, 13);
            this.label39.TabIndex = 53;
            this.label39.Text = "Rate D";
            // 
            // txtRateA
            // 
            this.txtRateA.Location = new System.Drawing.Point(115, 195);
            this.txtRateA.Margin = new System.Windows.Forms.Padding(4);
            this.txtRateA.Name = "txtRateA";
            this.txtRateA.Size = new System.Drawing.Size(182, 20);
            this.txtRateA.TabIndex = 56;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(27, 304);
            this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(42, 13);
            this.label38.TabIndex = 54;
            this.label38.Text = "Price D";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(27, 341);
            this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(58, 13);
            this.label37.TabIndex = 55;
            this.label37.Text = "Total Price";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(323, 33);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 61;
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::jewl.Properties.Resources._1483476843_Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 30);
            this.btnSave.Text = "&Add";
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 161);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Rate A";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 159);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(182, 20);
            this.textBox1.TabIndex = 63;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(27, 49);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 18);
            this.label4.TabIndex = 151;
            this.label4.Text = "Group Item";
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(115, 49);
            this.cbxGroupItem.Margin = new System.Windows.Forms.Padding(4);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(182, 21);
            this.cbxGroupItem.TabIndex = 152;
            // 
            // chkTagNo
            // 
            this.chkTagNo.Location = new System.Drawing.Point(27, 87);
            this.chkTagNo.Margin = new System.Windows.Forms.Padding(4);
            this.chkTagNo.Name = "chkTagNo";
            this.chkTagNo.Size = new System.Drawing.Size(85, 18);
            this.chkTagNo.TabIndex = 153;
            this.chkTagNo.Text = "Tag No";
            this.chkTagNo.UseVisualStyleBackColor = true;
            // 
            // cbxTagNo
            // 
            this.cbxTagNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTagNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTagNo.FormattingEnabled = true;
            this.cbxTagNo.Location = new System.Drawing.Point(115, 86);
            this.cbxTagNo.Margin = new System.Windows.Forms.Padding(4);
            this.cbxTagNo.Name = "cbxTagNo";
            this.cbxTagNo.Size = new System.Drawing.Size(182, 21);
            this.cbxTagNo.TabIndex = 154;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(27, 123);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 18);
            this.label6.TabIndex = 155;
            this.label6.Text = "Weight";
            // 
            // txtWeight
            // 
            this.txtWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWeight.Location = new System.Drawing.Point(115, 123);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(182, 20);
            this.txtWeight.TabIndex = 156;
            // 
            // frmSilver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 377);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbxGroupItem);
            this.Controls.Add(this.chkTagNo);
            this.Controls.Add(this.cbxTagNo);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.txtTotalPriceSilver);
            this.Controls.Add(this.txtRateD);
            this.Controls.Add(this.txtPriceD);
            this.Controls.Add(this.label41);
            this.Controls.Add(this.label40);
            this.Controls.Add(this.txtPriceA);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.txtRateA);
            this.Controls.Add(this.label38);
            this.Controls.Add(this.label37);
            this.Name = "frmSilver";
            this.Text = "frmSilver";
            this.Load += new System.EventHandler(this.frmSilver_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtTotalPriceSilver;
        private System.Windows.Forms.TextBox txtRateD;
        private System.Windows.Forms.TextBox txtPriceD;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox txtPriceA;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox txtRateA;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.CheckBox chkTagNo;
        private System.Windows.Forms.ComboBox cbxTagNo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWeight;
    }
}