namespace jewl
{
    partial class AddBulkStock
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
            this.cbxTagNo = new System.Windows.Forms.ComboBox();
            this.lblTagNo = new System.Windows.Forms.Label();
            this.lblGroupItem = new System.Windows.Forms.Label();
            this.cbxGroupItem = new System.Windows.Forms.ComboBox();
            this.lblMain = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbxTagNo
            // 
            this.cbxTagNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTagNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTagNo.FormattingEnabled = true;
            this.cbxTagNo.Location = new System.Drawing.Point(16, 186);
            this.cbxTagNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxTagNo.Name = "cbxTagNo";
            this.cbxTagNo.Size = new System.Drawing.Size(128, 26);
            this.cbxTagNo.TabIndex = 96;
            // 
            // lblTagNo
            // 
            this.lblTagNo.AutoSize = true;
            this.lblTagNo.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTagNo.Location = new System.Drawing.Point(11, 153);
            this.lblTagNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTagNo.Name = "lblTagNo";
            this.lblTagNo.Size = new System.Drawing.Size(87, 14);
            this.lblTagNo.TabIndex = 95;
            this.lblTagNo.Text = "Tag Number";
            // 
            // lblGroupItem
            // 
            this.lblGroupItem.AutoSize = true;
            this.lblGroupItem.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupItem.Location = new System.Drawing.Point(12, 79);
            this.lblGroupItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupItem.Name = "lblGroupItem";
            this.lblGroupItem.Size = new System.Drawing.Size(82, 14);
            this.lblGroupItem.TabIndex = 94;
            this.lblGroupItem.Text = "Group Item";
            this.lblGroupItem.Click += new System.EventHandler(this.lblGroupItem_Click);
            // 
            // cbxGroupItem
            // 
            this.cbxGroupItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupItem.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxGroupItem.FormattingEnabled = true;
            this.cbxGroupItem.Location = new System.Drawing.Point(16, 110);
            this.cbxGroupItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxGroupItem.Name = "cbxGroupItem";
            this.cbxGroupItem.Size = new System.Drawing.Size(337, 26);
            this.cbxGroupItem.TabIndex = 93;
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxGroupItem.SelectionChangeCommitted += new System.EventHandler(this.cbxGroupItem_SelectionChangeCommitted);
            // 
            // lblMain
            // 
            this.lblMain.BackColor = System.Drawing.Color.White;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lblMain.Location = new System.Drawing.Point(4, 2);
            this.lblMain.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(485, 54);
            this.lblMain.TabIndex = 97;
            this.lblMain.Text = ". . : :Gold Stock: : . .";
            this.lblMain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtWeight);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtQty);
            this.panel1.Location = new System.Drawing.Point(5, 228);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(432, 178);
            this.panel1.TabIndex = 98;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(35, 23);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 14);
            this.label2.TabIndex = 101;
            this.label2.Text = "Weight";
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(29, 57);
            this.txtWeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(132, 22);
            this.txtWeight.TabIndex = 100;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 100);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 14);
            this.label1.TabIndex = 99;
            this.label1.Text = "Qty";
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(29, 138);
            this.txtQty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(132, 22);
            this.txtQty.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(255, 412);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 64);
            this.btnExit.TabIndex = 99;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(121, 412);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 64);
            this.btnSave.TabIndex = 100;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(225, 15);
            this.dtpDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(128, 22);
            this.dtpDate.TabIndex = 101;
            // 
            // AddBulkStock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(537, 491);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.cbxTagNo);
            this.Controls.Add(this.lblTagNo);
            this.Controls.Add(this.lblGroupItem);
            this.Controls.Add(this.cbxGroupItem);
            this.Controls.Add(this.dtpDate);
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "AddBulkStock";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddBulkStock";
            this.Load += new System.EventHandler(this.AddBulkStock_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxTagNo;
        private System.Windows.Forms.Label lblTagNo;
        private System.Windows.Forms.Label lblGroupItem;
        private System.Windows.Forms.ComboBox cbxGroupItem;
        private System.Windows.Forms.Label lblMain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtQty;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DateTimePicker dtpDate;
    }
}