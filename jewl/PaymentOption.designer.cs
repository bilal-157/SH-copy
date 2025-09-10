namespace jewl
{
    partial class PaymentOption
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
            this.tbpCheque = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvChequePaymentDetail = new System.Windows.Forms.DataGridView();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxBankName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cbxAccountNo = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtTotalChequePaymentDetail = new System.Windows.Forms.TextBox();
            this.lblTotalChequePaymentDetail = new System.Windows.Forms.Label();
            this.btnRemoveChequePaymentDetail = new System.Windows.Forms.Button();
            this.tbcPaymentOption = new System.Windows.Forms.TabControl();
            this.tbpCreditCard = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvCreditCardPaymentDetail = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxBank = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxBankAccount = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveCreditCardPaymentDetail = new System.Windows.Forms.Button();
            this.txtTotalCreditCardPaymentDetail = new System.Windows.Forms.TextBox();
            this.lblTotalCreditCardPaymentDetail = new System.Windows.Forms.Label();
            this.tbpGold = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvGoldReceivedDetail = new System.Windows.Forms.DataGridView();
            this.colGoldType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxKarrat = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnRemoveGoldReceivedDetail = new System.Windows.Forms.Button();
            this.browse = new System.Windows.Forms.Button();
            this.pbxMain = new System.Windows.Forms.PictureBox();
            this.txtTotalGoldReceivedDetail = new System.Windows.Forms.TextBox();
            this.lblTotalGoldReceivedDetail = new System.Windows.Forms.Label();
            this.pnlPasaRate = new System.Windows.Forms.Panel();
            this.rbtSonaPasa = new System.Windows.Forms.RadioButton();
            this.rbtPoundPasa = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.tbpCheque.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChequePaymentDetail)).BeginInit();
            this.tbcPaymentOption.SuspendLayout();
            this.tbpCreditCard.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditCardPaymentDetail)).BeginInit();
            this.tbpGold.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoldReceivedDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).BeginInit();
            this.pnlPasaRate.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.Window;
            this.lblTitle.Location = new System.Drawing.Point(328, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(562, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Payment Option";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbpCheque
            // 
            this.tbpCheque.BackColor = System.Drawing.Color.Khaki;
            this.tbpCheque.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tbpCheque.Controls.Add(this.groupBox2);
            this.tbpCheque.Controls.Add(this.txtTotalChequePaymentDetail);
            this.tbpCheque.Controls.Add(this.lblTotalChequePaymentDetail);
            this.tbpCheque.Controls.Add(this.btnRemoveChequePaymentDetail);
            this.tbpCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpCheque.Location = new System.Drawing.Point(4, 22);
            this.tbpCheque.Margin = new System.Windows.Forms.Padding(4);
            this.tbpCheque.Name = "tbpCheque";
            this.tbpCheque.Padding = new System.Windows.Forms.Padding(4);
            this.tbpCheque.Size = new System.Drawing.Size(1185, 399);
            this.tbpCheque.TabIndex = 1;
            this.tbpCheque.Text = "Cheque";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvChequePaymentDetail);
            this.groupBox2.Location = new System.Drawing.Point(13, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1175, 253);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cheque Detail";
            // 
            // dgvChequePaymentDetail
            // 
            this.dgvChequePaymentDetail.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgvChequePaymentDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChequePaymentDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column9,
            this.Column10,
            this.Column11,
            this.cbxBankName,
            this.cbxAccountNo,
            this.Column13});
            this.dgvChequePaymentDetail.Location = new System.Drawing.Point(3, 16);
            this.dgvChequePaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.dgvChequePaymentDetail.Name = "dgvChequePaymentDetail";
            this.dgvChequePaymentDetail.Size = new System.Drawing.Size(1167, 234);
            this.dgvChequePaymentDetail.TabIndex = 1;
            this.dgvChequePaymentDetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvChequePaymentDetail_EditingControlShowing);
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column9.HeaderText = "Amount";
            this.Column9.Name = "Column9";
            this.Column9.Width = 180;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Date";
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Cheque No";
            this.Column11.Name = "Column11";
            // 
            // cbxBankName
            // 
            this.cbxBankName.HeaderText = "Bank Name ";
            this.cbxBankName.Name = "cbxBankName";
            this.cbxBankName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxBankName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cbxBankName.Width = 120;
            // 
            // cbxAccountNo
            // 
            this.cbxAccountNo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxAccountNo.HeaderText = "Acount No";
            this.cbxAccountNo.Name = "cbxAccountNo";
            this.cbxAccountNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxAccountNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Description";
            this.Column13.Name = "Column13";
            this.Column13.Width = 515;
            // 
            // txtTotalChequePaymentDetail
            // 
            this.txtTotalChequePaymentDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalChequePaymentDetail.Location = new System.Drawing.Point(948, 274);
            this.txtTotalChequePaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalChequePaymentDetail.Name = "txtTotalChequePaymentDetail";
            this.txtTotalChequePaymentDetail.Size = new System.Drawing.Size(221, 20);
            this.txtTotalChequePaymentDetail.TabIndex = 3;
            // 
            // lblTotalChequePaymentDetail
            // 
            this.lblTotalChequePaymentDetail.AutoSize = true;
            this.lblTotalChequePaymentDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalChequePaymentDetail.Location = new System.Drawing.Point(891, 278);
            this.lblTotalChequePaymentDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalChequePaymentDetail.Name = "lblTotalChequePaymentDetail";
            this.lblTotalChequePaymentDetail.Size = new System.Drawing.Size(36, 13);
            this.lblTotalChequePaymentDetail.TabIndex = 5;
            this.lblTotalChequePaymentDetail.Text = "Total";
            // 
            // btnRemoveChequePaymentDetail
            // 
            this.btnRemoveChequePaymentDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnRemoveChequePaymentDetail.BackgroundImage = global::jewl.Properties.Resources.removeRow;
            this.btnRemoveChequePaymentDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemoveChequePaymentDetail.Location = new System.Drawing.Point(535, 272);
            this.btnRemoveChequePaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveChequePaymentDetail.Name = "btnRemoveChequePaymentDetail";
            this.btnRemoveChequePaymentDetail.Size = new System.Drawing.Size(108, 42);
            this.btnRemoveChequePaymentDetail.TabIndex = 2;
            this.btnRemoveChequePaymentDetail.Text = "Remove";
            this.btnRemoveChequePaymentDetail.UseVisualStyleBackColor = false;
            this.btnRemoveChequePaymentDetail.Click += new System.EventHandler(this.btnRemoveChequePaymentDetail_Click);
            // 
            // tbcPaymentOption
            // 
            this.tbcPaymentOption.Controls.Add(this.tbpCreditCard);
            this.tbcPaymentOption.Controls.Add(this.tbpCheque);
            this.tbcPaymentOption.Controls.Add(this.tbpGold);
            this.tbcPaymentOption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcPaymentOption.ItemSize = new System.Drawing.Size(58, 18);
            this.tbcPaymentOption.Location = new System.Drawing.Point(15, 64);
            this.tbcPaymentOption.Margin = new System.Windows.Forms.Padding(4);
            this.tbcPaymentOption.Name = "tbcPaymentOption";
            this.tbcPaymentOption.Padding = new System.Drawing.Point(125, 3);
            this.tbcPaymentOption.SelectedIndex = 0;
            this.tbcPaymentOption.Size = new System.Drawing.Size(1193, 425);
            this.tbcPaymentOption.TabIndex = 0;
            // 
            // tbpCreditCard
            // 
            this.tbpCreditCard.BackColor = System.Drawing.Color.Khaki;
            this.tbpCreditCard.Controls.Add(this.groupBox1);
            this.tbpCreditCard.Controls.Add(this.btnRemoveCreditCardPaymentDetail);
            this.tbpCreditCard.Controls.Add(this.txtTotalCreditCardPaymentDetail);
            this.tbpCreditCard.Controls.Add(this.lblTotalCreditCardPaymentDetail);
            this.tbpCreditCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpCreditCard.Location = new System.Drawing.Point(4, 22);
            this.tbpCreditCard.Margin = new System.Windows.Forms.Padding(4);
            this.tbpCreditCard.Name = "tbpCreditCard";
            this.tbpCreditCard.Padding = new System.Windows.Forms.Padding(4, 4, 4, 5);
            this.tbpCreditCard.Size = new System.Drawing.Size(1185, 399);
            this.tbpCreditCard.TabIndex = 0;
            this.tbpCreditCard.Text = "Credit Card";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvCreditCardPaymentDetail);
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1158, 253);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Credit Card Detail";
            // 
            // dgvCreditCardPaymentDetail
            // 
            this.dgvCreditCardPaymentDetail.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgvCreditCardPaymentDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCreditCardPaymentDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.cbxBank,
            this.Column5,
            this.Column6,
            this.cbxBankAccount,
            this.Column8});
            this.dgvCreditCardPaymentDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvCreditCardPaymentDetail.Location = new System.Drawing.Point(3, 16);
            this.dgvCreditCardPaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.dgvCreditCardPaymentDetail.Name = "dgvCreditCardPaymentDetail";
            this.dgvCreditCardPaymentDetail.Size = new System.Drawing.Size(1152, 234);
            this.dgvCreditCardPaymentDetail.TabIndex = 1;
            this.dgvCreditCardPaymentDetail.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvCreditCardPaymentDetail_CellEndEdit);
            this.dgvCreditCardPaymentDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvCreditCardPaymentDetail_DataError);
            this.dgvCreditCardPaymentDetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvCreditCardPaymentDetail_EditingControlShowing);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Machine Name";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Amount";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Deduct Rate";
            this.Column3.Name = "Column3";
            this.Column3.Width = 45;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column4.HeaderText = "Swap Amount";
            this.Column4.Name = "Column4";
            this.Column4.Width = 90;
            // 
            // cbxBank
            // 
            this.cbxBank.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxBank.HeaderText = "Bank";
            this.cbxBank.Name = "cbxBank";
            this.cbxBank.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxBank.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column5.HeaderText = "Bank Deduct Rate";
            this.Column5.Name = "Column5";
            this.Column5.Width = 111;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Column6.HeaderText = "Amount Deposite";
            this.Column6.Name = "Column6";
            this.Column6.Width = 104;
            // 
            // cbxBankAccount
            // 
            this.cbxBankAccount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.cbxBankAccount.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cbxBankAccount.HeaderText = "Deposite In Account";
            this.cbxBankAccount.Name = "cbxBankAccount";
            this.cbxBankAccount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxBankAccount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cbxBankAccount.Width = 118;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.HeaderText = "Description";
            this.Column8.Name = "Column8";
            // 
            // btnRemoveCreditCardPaymentDetail
            // 
            this.btnRemoveCreditCardPaymentDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnRemoveCreditCardPaymentDetail.BackgroundImage = global::jewl.Properties.Resources.removeRow;
            this.btnRemoveCreditCardPaymentDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemoveCreditCardPaymentDetail.Location = new System.Drawing.Point(535, 272);
            this.btnRemoveCreditCardPaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveCreditCardPaymentDetail.Name = "btnRemoveCreditCardPaymentDetail";
            this.btnRemoveCreditCardPaymentDetail.Size = new System.Drawing.Size(108, 42);
            this.btnRemoveCreditCardPaymentDetail.TabIndex = 6;
            this.btnRemoveCreditCardPaymentDetail.Text = "Remove";
            this.btnRemoveCreditCardPaymentDetail.UseVisualStyleBackColor = false;
            this.btnRemoveCreditCardPaymentDetail.Click += new System.EventHandler(this.btnRemoveCreditCardPaymentDetail_Click);
            // 
            // txtTotalCreditCardPaymentDetail
            // 
            this.txtTotalCreditCardPaymentDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalCreditCardPaymentDetail.Location = new System.Drawing.Point(948, 274);
            this.txtTotalCreditCardPaymentDetail.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalCreditCardPaymentDetail.Name = "txtTotalCreditCardPaymentDetail";
            this.txtTotalCreditCardPaymentDetail.Size = new System.Drawing.Size(221, 20);
            this.txtTotalCreditCardPaymentDetail.TabIndex = 3;
            // 
            // lblTotalCreditCardPaymentDetail
            // 
            this.lblTotalCreditCardPaymentDetail.AutoSize = true;
            this.lblTotalCreditCardPaymentDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCreditCardPaymentDetail.Location = new System.Drawing.Point(891, 278);
            this.lblTotalCreditCardPaymentDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalCreditCardPaymentDetail.Name = "lblTotalCreditCardPaymentDetail";
            this.lblTotalCreditCardPaymentDetail.Size = new System.Drawing.Size(36, 13);
            this.lblTotalCreditCardPaymentDetail.TabIndex = 0;
            this.lblTotalCreditCardPaymentDetail.Text = "Total";
            // 
            // tbpGold
            // 
            this.tbpGold.BackColor = System.Drawing.Color.Khaki;
            this.tbpGold.Controls.Add(this.groupBox3);
            this.tbpGold.Controls.Add(this.btnRemoveGoldReceivedDetail);
            this.tbpGold.Controls.Add(this.browse);
            this.tbpGold.Controls.Add(this.pbxMain);
            this.tbpGold.Controls.Add(this.txtTotalGoldReceivedDetail);
            this.tbpGold.Controls.Add(this.lblTotalGoldReceivedDetail);
            this.tbpGold.Controls.Add(this.pnlPasaRate);
            this.tbpGold.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbpGold.Location = new System.Drawing.Point(4, 22);
            this.tbpGold.Margin = new System.Windows.Forms.Padding(4);
            this.tbpGold.Name = "tbpGold";
            this.tbpGold.Padding = new System.Windows.Forms.Padding(4);
            this.tbpGold.Size = new System.Drawing.Size(1185, 399);
            this.tbpGold.TabIndex = 2;
            this.tbpGold.Text = " Gold";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvGoldReceivedDetail);
            this.groupBox3.Location = new System.Drawing.Point(13, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1175, 253);
            this.groupBox3.TabIndex = 115;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gold Detail";
            // 
            // dgvGoldReceivedDetail
            // 
            this.dgvGoldReceivedDetail.BackgroundColor = System.Drawing.Color.Khaki;
            this.dgvGoldReceivedDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGoldReceivedDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colGoldType,
            this.Column15,
            this.Column16,
            this.Column17,
            this.cbxKarrat,
            this.Column19,
            this.Column20,
            this.Column21});
            this.dgvGoldReceivedDetail.EnableHeadersVisualStyles = false;
            this.dgvGoldReceivedDetail.Location = new System.Drawing.Point(3, 16);
            this.dgvGoldReceivedDetail.Margin = new System.Windows.Forms.Padding(4);
            this.dgvGoldReceivedDetail.Name = "dgvGoldReceivedDetail";
            this.dgvGoldReceivedDetail.Size = new System.Drawing.Size(1167, 234);
            this.dgvGoldReceivedDetail.TabIndex = 1;
            this.dgvGoldReceivedDetail.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoldReceivedDetail_CellEndEdit);
            this.dgvGoldReceivedDetail.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoldReceivedDetail_CellLeave);
            this.dgvGoldReceivedDetail.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvGoldReceivedDetail_DataError);
            this.dgvGoldReceivedDetail.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvGoldReceivedDetail_EditingControlShowing);
            this.dgvGoldReceivedDetail.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoldReceivedDetail_RowEnter);
            this.dgvGoldReceivedDetail.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGoldReceivedDetail_RowLeave);
            // 
            // colGoldType
            // 
            this.colGoldType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.colGoldType.HeaderText = "Gold Type ";
            this.colGoldType.Name = "colGoldType";
            this.colGoldType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colGoldType.Width = 190;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Weight";
            this.Column15.Name = "Column15";
            this.Column15.Width = 70;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Kaat ";
            this.Column16.Name = "Column16";
            this.Column16.Width = 70;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Pure Weight";
            this.Column17.Name = "Column17";
            // 
            // cbxKarrat
            // 
            this.cbxKarrat.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.cbxKarrat.HeaderText = "Karrat";
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
            this.cbxKarrat.Name = "cbxKarrat";
            this.cbxKarrat.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cbxKarrat.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.cbxKarrat.Width = 70;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "Rate";
            this.Column19.Name = "Column19";
            // 
            // Column20
            // 
            this.Column20.HeaderText = "Amount";
            this.Column20.Name = "Column20";
            // 
            // Column21
            // 
            this.Column21.HeaderText = "Description";
            this.Column21.Name = "Column21";
            this.Column21.Width = 422;
            // 
            // btnRemoveGoldReceivedDetail
            // 
            this.btnRemoveGoldReceivedDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.btnRemoveGoldReceivedDetail.BackgroundImage = global::jewl.Properties.Resources.removeRow;
            this.btnRemoveGoldReceivedDetail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRemoveGoldReceivedDetail.Location = new System.Drawing.Point(535, 272);
            this.btnRemoveGoldReceivedDetail.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveGoldReceivedDetail.Name = "btnRemoveGoldReceivedDetail";
            this.btnRemoveGoldReceivedDetail.Size = new System.Drawing.Size(108, 42);
            this.btnRemoveGoldReceivedDetail.TabIndex = 114;
            this.btnRemoveGoldReceivedDetail.Text = "Remove";
            this.btnRemoveGoldReceivedDetail.UseVisualStyleBackColor = false;
            this.btnRemoveGoldReceivedDetail.Click += new System.EventHandler(this.btnRemoveGoldReceivedDetail_Click);
            // 
            // browse
            // 
            this.browse.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.browse.Location = new System.Drawing.Point(930, 319);
            this.browse.Margin = new System.Windows.Forms.Padding(4);
            this.browse.Name = "browse";
            this.browse.Size = new System.Drawing.Size(103, 42);
            this.browse.TabIndex = 113;
            this.browse.Text = "Browse";
            this.browse.UseVisualStyleBackColor = true;
            this.browse.Click += new System.EventHandler(this.browse_Click);
            // 
            // pbxMain
            // 
            this.pbxMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbxMain.Location = new System.Drawing.Point(1041, 288);
            this.pbxMain.Margin = new System.Windows.Forms.Padding(4);
            this.pbxMain.Name = "pbxMain";
            this.pbxMain.Size = new System.Drawing.Size(130, 103);
            this.pbxMain.TabIndex = 112;
            this.pbxMain.TabStop = false;
            // 
            // txtTotalGoldReceivedDetail
            // 
            this.txtTotalGoldReceivedDetail.BackColor = System.Drawing.Color.DarkKhaki;
            this.txtTotalGoldReceivedDetail.Location = new System.Drawing.Point(812, 274);
            this.txtTotalGoldReceivedDetail.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotalGoldReceivedDetail.Name = "txtTotalGoldReceivedDetail";
            this.txtTotalGoldReceivedDetail.Size = new System.Drawing.Size(221, 20);
            this.txtTotalGoldReceivedDetail.TabIndex = 3;
            // 
            // lblTotalGoldReceivedDetail
            // 
            this.lblTotalGoldReceivedDetail.AutoSize = true;
            this.lblTotalGoldReceivedDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalGoldReceivedDetail.Location = new System.Drawing.Point(752, 277);
            this.lblTotalGoldReceivedDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalGoldReceivedDetail.Name = "lblTotalGoldReceivedDetail";
            this.lblTotalGoldReceivedDetail.Size = new System.Drawing.Size(36, 13);
            this.lblTotalGoldReceivedDetail.TabIndex = 9;
            this.lblTotalGoldReceivedDetail.Text = "Total";
            // 
            // pnlPasaRate
            // 
            this.pnlPasaRate.Controls.Add(this.rbtSonaPasa);
            this.pnlPasaRate.Controls.Add(this.rbtPoundPasa);
            this.pnlPasaRate.Location = new System.Drawing.Point(16, 266);
            this.pnlPasaRate.Margin = new System.Windows.Forms.Padding(4);
            this.pnlPasaRate.Name = "pnlPasaRate";
            this.pnlPasaRate.Size = new System.Drawing.Size(316, 24);
            this.pnlPasaRate.TabIndex = 111;
            this.pnlPasaRate.Visible = false;
            // 
            // rbtSonaPasa
            // 
            this.rbtSonaPasa.AutoSize = true;
            this.rbtSonaPasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtSonaPasa.Location = new System.Drawing.Point(168, 4);
            this.rbtSonaPasa.Margin = new System.Windows.Forms.Padding(4);
            this.rbtSonaPasa.Name = "rbtSonaPasa";
            this.rbtSonaPasa.Size = new System.Drawing.Size(86, 17);
            this.rbtSonaPasa.TabIndex = 1;
            this.rbtSonaPasa.Text = "Sona Pasa";
            this.rbtSonaPasa.UseVisualStyleBackColor = true;
            this.rbtSonaPasa.CheckedChanged += new System.EventHandler(this.rbtSonaPasa_CheckedChanged);
            // 
            // rbtPoundPasa
            // 
            this.rbtPoundPasa.AutoSize = true;
            this.rbtPoundPasa.Checked = true;
            this.rbtPoundPasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtPoundPasa.Location = new System.Drawing.Point(7, 4);
            this.rbtPoundPasa.Margin = new System.Windows.Forms.Padding(4);
            this.rbtPoundPasa.Name = "rbtPoundPasa";
            this.rbtPoundPasa.Size = new System.Drawing.Size(93, 17);
            this.rbtPoundPasa.TabIndex = 0;
            this.rbtPoundPasa.TabStop = true;
            this.rbtPoundPasa.Text = "Pound Pasa";
            this.rbtPoundPasa.UseVisualStyleBackColor = true;
            this.rbtPoundPasa.CheckedChanged += new System.EventHandler(this.rbtPoundPasa_CheckedChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton6,
            this.btnSave,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(15, 9);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(205, 33);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 59;
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.CheckOnClick = true;
            this.toolStripButton6.ForeColor = System.Drawing.Color.Black;
            this.toolStripButton6.Image = global::jewl.Properties.Resources.newfile;
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(62, 30);
            this.toolStripButton6.Text = "&New";
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
            // PaymentOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 497);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tbcPaymentOption);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PaymentOption";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PaymentOption_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PaymentOption_Paint);
            this.tbpCheque.ResumeLayout(false);
            this.tbpCheque.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChequePaymentDetail)).EndInit();
            this.tbcPaymentOption.ResumeLayout(false);
            this.tbpCreditCard.ResumeLayout(false);
            this.tbpCreditCard.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCreditCardPaymentDetail)).EndInit();
            this.tbpGold.ResumeLayout(false);
            this.tbpGold.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGoldReceivedDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxMain)).EndInit();
            this.pnlPasaRate.ResumeLayout(false);
            this.pnlPasaRate.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabPage tbpCheque;
        private System.Windows.Forms.TabControl tbcPaymentOption;
        private System.Windows.Forms.TabPage tbpCreditCard;
        private System.Windows.Forms.TabPage tbpGold;
        private System.Windows.Forms.DataGridView dgvCreditCardPaymentDetail;
        private System.Windows.Forms.TextBox txtTotalCreditCardPaymentDetail;
        private System.Windows.Forms.Label lblTotalCreditCardPaymentDetail;
        private System.Windows.Forms.DataGridView dgvChequePaymentDetail;
        private System.Windows.Forms.TextBox txtTotalChequePaymentDetail;
        private System.Windows.Forms.Label lblTotalChequePaymentDetail;
        private System.Windows.Forms.Button btnRemoveChequePaymentDetail;
        private System.Windows.Forms.TextBox txtTotalGoldReceivedDetail;
        private System.Windows.Forms.Label lblTotalGoldReceivedDetail;
        private System.Windows.Forms.Panel pnlPasaRate;
        private System.Windows.Forms.RadioButton rbtSonaPasa;
        private System.Windows.Forms.RadioButton rbtPoundPasa;
        private System.Windows.Forms.DataGridView dgvGoldReceivedDetail;
        private System.Windows.Forms.Button browse;
        private System.Windows.Forms.PictureBox pbxMain;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnRemoveCreditCardPaymentDetail;
        private System.Windows.Forms.Button btnRemoveGoldReceivedDetail;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxBank;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxBankAccount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxBankName;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxAccountNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGoldType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewComboBoxColumn cbxKarrat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column20;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column21;
    }
}

