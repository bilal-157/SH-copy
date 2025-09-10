namespace jewl
{
    partial class EditNo
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
            this.btnEnter = new System.Windows.Forms.Button();
            this.txtEditNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbtSaleNo = new System.Windows.Forms.RadioButton();
            this.rbtBillBookNo = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnEnter
            // 
            this.btnEnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnter.Location = new System.Drawing.Point(117, 140);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(75, 23);
            this.btnEnter.TabIndex = 8;
            this.btnEnter.Text = "OK";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // txtEditNo
            // 
            this.txtEditNo.Location = new System.Drawing.Point(95, 103);
            this.txtEditNo.Name = "txtEditNo";
            this.txtEditNo.Size = new System.Drawing.Size(124, 20);
            this.txtEditNo.TabIndex = 7;
            this.txtEditNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(109, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Enter No.";
            // 
            // rbtSaleNo
            // 
            this.rbtSaleNo.AutoSize = true;
            this.rbtSaleNo.Checked = true;
            this.rbtSaleNo.Location = new System.Drawing.Point(78, 32);
            this.rbtSaleNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtSaleNo.Name = "rbtSaleNo";
            this.rbtSaleNo.Size = new System.Drawing.Size(60, 17);
            this.rbtSaleNo.TabIndex = 9;
            this.rbtSaleNo.TabStop = true;
            this.rbtSaleNo.Text = "SaleNo";
            this.rbtSaleNo.UseVisualStyleBackColor = true;
            this.rbtSaleNo.Visible = false;
            // 
            // rbtBillBookNo
            // 
            this.rbtBillBookNo.AutoSize = true;
            this.rbtBillBookNo.Location = new System.Drawing.Point(165, 32);
            this.rbtBillBookNo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtBillBookNo.Name = "rbtBillBookNo";
            this.rbtBillBookNo.Size = new System.Drawing.Size(77, 17);
            this.rbtBillBookNo.TabIndex = 10;
            this.rbtBillBookNo.Text = "BillBookNo";
            this.rbtBillBookNo.UseVisualStyleBackColor = true;
            this.rbtBillBookNo.Visible = false;
            // 
            // EditNo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 183);
            this.Controls.Add(this.rbtBillBookNo);
            this.Controls.Add(this.rbtSaleNo);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtEditNo);
            this.Controls.Add(this.label1);
            this.Name = "EditNo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit No.";
            this.Load += new System.EventHandler(this.EditNo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.TextBox txtEditNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbtSaleNo;
        private System.Windows.Forms.RadioButton rbtBillBookNo;
    }
}