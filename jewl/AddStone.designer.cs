namespace jewl
{
    partial class AddStones
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
            this.lblStonesList = new System.Windows.Forms.Label();
            this.cbxStonesList = new System.Windows.Forms.ComboBox();
            this.txtStoneType = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnEdit = new System.Windows.Forms.ToolStripButton();
            this.btnReset = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStonesList
            // 
            this.lblStonesList.BackColor = System.Drawing.Color.Transparent;
            this.lblStonesList.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStonesList.ForeColor = System.Drawing.Color.Black;
            this.lblStonesList.Location = new System.Drawing.Point(28, 74);
            this.lblStonesList.Name = "lblStonesList";
            this.lblStonesList.Size = new System.Drawing.Size(118, 22);
            this.lblStonesList.TabIndex = 29;
            this.lblStonesList.Text = "Stone Type List";
            this.lblStonesList.Visible = false;
            // 
            // cbxStonesList
            // 
            this.cbxStonesList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStonesList.FormattingEnabled = true;
            this.cbxStonesList.Location = new System.Drawing.Point(152, 74);
            this.cbxStonesList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbxStonesList.Name = "cbxStonesList";
            this.cbxStonesList.Size = new System.Drawing.Size(205, 24);
            this.cbxStonesList.TabIndex = 2;
            this.cbxStonesList.Visible = false;
            this.cbxStonesList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbxStonesList_MouseClick);
            this.cbxStonesList.SelectionChangeCommitted += new System.EventHandler(this.cbxStonesList_SelectionChangeCommitted);
            // 
            // txtStoneType
            // 
            this.txtStoneType.Location = new System.Drawing.Point(152, 42);
            this.txtStoneType.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtStoneType.Name = "txtStoneType";
            this.txtStoneType.Size = new System.Drawing.Size(205, 22);
            this.txtStoneType.TabIndex = 1;
            this.txtStoneType.Leave += new System.EventHandler(this.txtStoneType_Leave);
            this.txtStoneType.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtStoneType_KeyPress);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Palatino Linotype", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(28, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 22);
            this.label2.TabIndex = 31;
            this.label2.Text = "Stone Type";
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Font = new System.Drawing.Font("Palatino Linotype", 23F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Transparent;
            this.lblTitle.Location = new System.Drawing.Point(0, 39);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(630, 45);
            this.lblTitle.TabIndex = 32;
            this.lblTitle.Text = "Add Stone Type";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTitle.Click += new System.EventHandler(this.label1_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSave,
            this.btnEdit,
            this.btnReset,
            this.btnDelete,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(630, 33);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 59;
            // 
            // btnNew
            // 
            this.btnNew.CheckOnClick = true;
            this.btnNew.ForeColor = System.Drawing.Color.Black;
            this.btnNew.Image = global::jewl.Properties.Resources.newfile;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(62, 30);
            this.btnNew.Text = "&New";
            // 
            // btnSave
            // 
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = global::jewl.Properties.Resources._1483476843_Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 30);
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.CheckOnClick = true;
            this.btnEdit.ForeColor = System.Drawing.Color.Black;
            this.btnEdit.Image = global::jewl.Properties.Resources._1483476345_edit_notes;
            this.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(56, 30);
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnReset
            // 
            this.btnReset.ForeColor = System.Drawing.Color.Black;
            this.btnReset.Image = global::jewl.Properties.Resources.reset;
            this.btnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(68, 30);
            this.btnReset.Text = "&Reset";
            this.btnReset.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.ForeColor = System.Drawing.Color.Black;
            this.btnDelete.Image = global::jewl.Properties.Resources.mydelete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 30);
            this.btnDelete.Text = "&Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbxStonesList);
            this.groupBox1.Controls.Add(this.txtStoneType);
            this.groupBox1.Controls.Add(this.lblStonesList);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(419, 197);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Stones detail";
            // 
            // AddStones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(630, 402);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Palatino Linotype", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "AddStones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Stone Type";
            this.Load += new System.EventHandler(this.AddStones_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStonesList;
        private System.Windows.Forms.ComboBox cbxStonesList;
        private System.Windows.Forms.TextBox txtStoneType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnEdit;
        private System.Windows.Forms.ToolStripButton btnReset;
        private System.Windows.Forms.ToolStripButton btnDelete;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.GroupBox groupBox1;


    }
}