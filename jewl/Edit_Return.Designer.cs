namespace jewl
{
    partial class Edit_Return
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnVeiw = new System.Windows.Forms.Button();
            this.rbtEdit = new System.Windows.Forms.RadioButton();
            this.rbtReturn = new System.Windows.Forms.RadioButton();
            this.lbldateFrom = new System.Windows.Forms.Label();
            this.lbldateTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit_Return";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(207, 191);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 64);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnVeiw
            // 
            this.btnVeiw.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnVeiw.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVeiw.Location = new System.Drawing.Point(51, 191);
            this.btnVeiw.Name = "btnVeiw";
            this.btnVeiw.Size = new System.Drawing.Size(75, 64);
            this.btnVeiw.TabIndex = 2;
            this.btnVeiw.Text = "Veiw";
            this.btnVeiw.UseVisualStyleBackColor = false;
            this.btnVeiw.Click += new System.EventHandler(this.btnVeiw_Click);
            // 
            // rbtEdit
            // 
            this.rbtEdit.AutoSize = true;
            this.rbtEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtEdit.Location = new System.Drawing.Point(10, 72);
            this.rbtEdit.Name = "rbtEdit";
            this.rbtEdit.Size = new System.Drawing.Size(54, 21);
            this.rbtEdit.TabIndex = 3;
            this.rbtEdit.TabStop = true;
            this.rbtEdit.Text = "Edit";
            this.rbtEdit.UseVisualStyleBackColor = true;
            this.rbtEdit.CheckedChanged += new System.EventHandler(this.rbtEdit_CheckedChanged);
            // 
            // rbtReturn
            // 
            this.rbtReturn.AutoSize = true;
            this.rbtReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtReturn.Location = new System.Drawing.Point(12, 131);
            this.rbtReturn.Name = "rbtReturn";
            this.rbtReturn.Size = new System.Drawing.Size(75, 21);
            this.rbtReturn.TabIndex = 4;
            this.rbtReturn.TabStop = true;
            this.rbtReturn.Text = "Return";
            this.rbtReturn.UseVisualStyleBackColor = true;
            this.rbtReturn.CheckedChanged += new System.EventHandler(this.rbtReturn_CheckedChanged);
            // 
            // lbldateFrom
            // 
            this.lbldateFrom.AutoSize = true;
            this.lbldateFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldateFrom.Location = new System.Drawing.Point(144, 121);
            this.lbldateFrom.Name = "lbldateFrom";
            this.lbldateFrom.Size = new System.Drawing.Size(78, 17);
            this.lbldateFrom.TabIndex = 7;
            this.lbldateFrom.Text = "DateFrom";
            this.lbldateFrom.Visible = false;
            // 
            // lbldateTo
            // 
            this.lbldateTo.AutoSize = true;
            this.lbldateTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldateTo.Location = new System.Drawing.Point(146, 147);
            this.lbldateTo.Name = "lbldateTo";
            this.lbldateTo.Size = new System.Drawing.Size(61, 17);
            this.lbldateTo.TabIndex = 8;
            this.lbldateTo.Text = "DateTo";
            this.lbldateTo.Visible = false;
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(246, 143);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(106, 20);
            this.dtpTo.TabIndex = 6;
            this.dtpTo.Visible = false;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(246, 117);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(106, 20);
            this.dtpFrom.TabIndex = 5;
            this.dtpFrom.Visible = false;
            // 
            // Edit_Return
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.ClientSize = new System.Drawing.Size(373, 269);
            this.Controls.Add(this.lbldateTo);
            this.Controls.Add(this.lbldateFrom);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.rbtReturn);
            this.Controls.Add(this.rbtEdit);
            this.Controls.Add(this.btnVeiw);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label1);
            this.Name = "Edit_Return";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Edit_Return";
            this.Load += new System.EventHandler(this.Edit_Return_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnVeiw;
        private System.Windows.Forms.RadioButton rbtEdit;
        private System.Windows.Forms.RadioButton rbtReturn;
        private System.Windows.Forms.Label lbldateFrom;
        private System.Windows.Forms.Label lbldateTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
    }
}