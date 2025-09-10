using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;
using System.Data.SqlClient;

namespace jewl
{
    public partial class ManagePurchase : Form
    {
        Supplier supplier;
        List<Supplier> supList = new List<Supplier>();
        StonesDAL sDAL = new StonesDAL();
        Purchase purchase = new Purchase();
        PurchaseLineItems purchaseLineItem;
        PurchaseSubLineItems purchaseSubLine;
        Stones stone = new Stones();
        int lineNo = 1, SublineNo = 1;
        string karat = "";
        bool chkkarat = false;
        public ManagePurchase()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.lblPurchaseNo.ForeColor = Color.FromArgb(0, 188, 212);
            this.lblPurchaseNo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
        }



        private void Purchase_Load(object sender, EventArgs e)
        {

            this.cbxSupplier.SelectedIndexChanged -= new EventHandler(cbxSupplier_SelectedIndexChanged);

            FormControls.FillCombobox(cbxSupplier, new SupplierDAL().GetAllSuppliers(), "PAbri", "PCode");
            FormControls.FillCombobox(cbxItemCode, new ItemDAL().GetAllItems(), "Abrivation", "ItemId");
            this.cbxKarat.SelectedIndex = 1;
            this.cbxSupplier.Select();
            this.lblPurchaseNo.Text = (new PurchaseDAL().GetMaxPurchaseNo() + 1).ToString();
            this.ShowDataGrid();
        }


        private void ShowDataGrid()
        {
            int i = 0;
            int j = 1;
            FormControls.FillCombobox(dataGridViewComboBoxColumn1, sDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            FormControls.FillCombobox(dataGridViewComboBoxColumn2, sDAL.GetAllStoneName(), "StoneName", "StoneId");
            this.dgvStonesDetail.Rows[i].Cells[0].Value = i + j;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Purchase_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }


        private void cbxSupplier_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxSupplier.SelectedIndexChanged += new EventHandler(cbxSupplier_SelectedIndexChanged);
        }

        private void cbxSupplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxItemCode.Select();
            }
        }

        private void cbxItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDescription.Select();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtQty);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxKarat);
        }

        private void cbxKarat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWeight.Select();
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWastage);
        }

        private void txtWastage_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMaking);
        }

        private void txtDOP_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRemarks);
        }

        private void txtMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDOP);
        }

        private void dgvStoneDetail_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddRow);
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtWastage_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtDOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void dgvStonesDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
            {
                e.Cancel = true;
            }
        }

        private void dgvStonesDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                try
                {
                    decimal weight = Convert.ToDecimal(dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value);
                    int qty = Convert.ToInt32(dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn3"].Value);
                    decimal rate = Convert.ToDecimal(dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn4"].Value);
                    if (weight != 0)
                    {
                        dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value = (weight * rate).ToString();
                    }
                    else
                        dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn5"].Value = (qty * rate).ToString();
                }
                catch { }
            }
        }

        private void dgvStonesDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    //decimal number = decimal.Parse(val.ToString("#0.00"));
                    string s = val.ToString("N3");
                    dgvStonesDetail.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].Value = s.ToString(); ;
                }
                catch { }
            }
            if (e.ColumnIndex == 1 && dgvStonesDetail.CurrentRow != null)
            {
                //this.dgvStonesDetail.CurrentRow.Cells[2].Value = DBNull.Value;
            }
        }

        private void dgvStonesDetail_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            for (i = 2; i < dgvStonesDetail.ColumnCount; i++)
            {
                if (Convert.ToString(this.dgvStonesDetail.CurrentRow.Cells[i].Value) != string.Empty)
                    return;
                else if (i == dgvStonesDetail.ColumnCount) { dgvStonesDetail.Rows.Remove(dgvStonesDetail.CurrentRow); }// MessageBox.Show("Row is empty"); return; }
            }
        }

        private void dgvStonesDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStonesDetail.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = sDAL.GetAllStoneNamebyId(sty);
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "Id";
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
            {
                TextBox txtbox = e.Control as TextBox;
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
                {
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
                }
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
                {
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
                }
            }
        }

        void txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void btnAddrow_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.cbxItemCode.Text))
            {
                MessageBox.Show("Item must be selected before Add Row", Messages.Header);
                this.cbxItemCode.Select();
                return;
            }
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                AddRow();
                this.refreshRecord();
                this.cbxItemCode.Select();
            }
        }
        public void ShowSubline(List<PurchaseSubLineItems> subList)
        {
            if (subList == null)
                return;
            else
            {
                this.dgvItemDetail.AutoGenerateColumns = false;
                this.dgvItemDetail.Rows.Clear();
                int count = subList.Count;
                this.dgvItemDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvItemDetail.Rows[i].Cells[0].Value = subList[i].ItemId;
                    this.dgvItemDetail.Rows[i].Cells[1].Value = subList[i].ItemDescription.ToString();
                    this.dgvItemDetail.Rows[i].Cells[2].Value = subList[i].Qty.ToString();
                    this.dgvItemDetail.Rows[i].Cells[3].Value = subList[i].Karat.ToString();
                }
            }
        }

        public void Showline(List<PurchaseLineItems> LineList)
        {
            if (LineList == null)
                return;
            else
            {
                this.dgvMasterDetail.AutoGenerateColumns = false;
                this.dgvMasterDetail.Rows.Clear();
                int count = LineList.Count;
                this.dgvMasterDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvMasterDetail.Rows[i].Cells[0].Value = LineList[i].Description.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[1].Value = LineList[i].Qty.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[2].Value = LineList[i].Karat.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[3].Value = LineList[i].GrossWeight.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[4].Value = LineList[i].Wastage.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[5].Value = LineList[i].GoodWill.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[6].Value = LineList[i].Discount.ToString();
                    this.dgvMasterDetail.Rows[i].Cells[7].Value = LineList[i].PureGold.ToString();

                }
            }
        }

        public void AddRow()
        {
            purchase.PurchaseNo = Convert.ToInt32(this.lblPurchaseNo.Text);
            purchase.PurchaseDate = this.dtpDate.Value;
            purchase.supplier.PCode = Convert.ToInt32(this.cbxSupplier.SelectedValue);
            purchase.ReceivedBy = Login.userId.ToString();
            AddSubLineItem(purchase);
            AddLineItem(purchase);
            ShowGridData(purchase);
        }

        public void ShowGridData(Purchase purchase)
        {
            this.ShowSubline(purchase.PurchaseSubLineItems);
            this.Showline(purchase.PurchaseLineItems);
            this.txtTWeight.Text = purchase.TotalWeight.ToString();
            this.txtTWastage.Text = purchase.TotalWastage.ToString();
            this.txtTDOP.Text = purchase.TotalPurchaseDiscount.ToString();
            this.txtTPureGold.Text = purchase.TotalPureGold.ToString();
            this.txtTMaking.Text = purchase.TotalGoodWill.ToString();
        }

        public void AddSubLineItem(Purchase p)
        {
            purchaseSubLine = new PurchaseSubLineItems();
            purchaseSubLine.PurchaseNo = p.PurchaseNo;
            purchaseSubLine.ItemId = Convert.ToInt32(this.cbxItemCode.SelectedValue);
            purchaseSubLine.ItemDescription = this.txtDescription.Text;
            purchaseSubLine.Qty = Convert.ToInt32(this.txtQty.Text);
            purchaseSubLine.Karat = this.cbxKarat.Text;
            purchaseSubLine.Weight = Convert.ToDecimal(FormControls.StringFormate(this.txtWeight.Text));
            purchaseSubLine.Making = Convert.ToDecimal(FormControls.StringFormate(this.txtMaking.Text));
            purchaseSubLine.SubWastage = Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
            purchaseSubLine.SubDOP = Convert.ToDecimal(FormControls.StringFormate(this.txtDOP.Text));
            purchaseSubLine.SubPureGold = Convert.ToDecimal(FormControls.StringFormate(this.txtPureGold.Text));
            purchaseSubLine.PItemId = p.PurchaseNo + "-" + lineNo + "-" + SublineNo;
            p.PurchaseSubLineItems.Add(purchaseSubLine);
            SublineNo = SublineNo + 1;
            karat = this.cbxKarat.Text;
            p.TotalMaking = p.TotalMaking + purchaseSubLine.Making;
        }

        public void AddLineItem(Purchase p)
        {
            foreach (var item in p.PurchaseLineItems)
            {
                if (item.Karat == this.cbxKarat.Text)
                {
                    karat = item.Karat;
                    chkkarat = true;
                }
            }
            if (chkkarat == true)
            {
                foreach (var item in p.PurchaseLineItems)
                {
                    if (item.Karat == this.cbxKarat.Text)
                    {
                        item.Qty = item.Qty + Convert.ToInt32(FormControls.StringFormate(this.txtQty.Text));
                        item.GrossWeight = item.GrossWeight + Convert.ToDecimal(FormControls.StringFormate(this.txtWeight.Text));
                        item.Wastage = item.Wastage + Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
                        item.Discount = item.Discount + Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
                        item.GoodWill = item.GoodWill + Convert.ToDecimal(FormControls.StringFormate(this.txtMaking.Text));
                        item.PureGold = item.PureGold + Convert.ToDecimal(FormControls.StringFormate(this.txtPureGold.Text));
                        chkkarat = false;

                        p.TotalPurchaseDiscount = p.TotalPurchaseDiscount + Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
                        p.TotalWeight = p.TotalWeight + Convert.ToDecimal(FormControls.StringFormate(this.txtWeight.Text));
                        p.TotalWastage = p.TotalWastage + Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
                        p.TotalGoodWill = p.TotalGoodWill + Convert.ToDecimal(FormControls.StringFormate(this.txtMaking.Text));
                        p.TotalPureGold = p.TotalPureGold + Convert.ToDecimal(FormControls.StringFormate(this.txtPureGold.Text));
                    }
                }
            }
            else
            {
                purchaseLineItem = new PurchaseLineItems();
                purchaseLineItem.PItemId = (this.lblPurchaseNo + "-" + lineNo).ToString();
                purchaseLineItem.Qty = Convert.ToInt32(FormControls.StringFormate(this.txtQty.Text));
                purchaseLineItem.Karat = this.cbxKarat.Text;
                purchaseLineItem.GrossWeight = Convert.ToDecimal(FormControls.StringFormate(this.txtWeight.Text));
                purchaseLineItem.Wastage = Convert.ToDecimal(FormControls.StringFormate(this.txtWastage.Text));
                purchaseLineItem.Discount = Convert.ToDecimal(FormControls.StringFormate(this.txtDOP.Text));
                purchaseLineItem.Description = this.txtRemarks.Text;
                purchaseLineItem.GoodWill = Convert.ToDecimal(FormControls.StringFormate(this.txtMaking.Text));
                purchaseLineItem.PureGold = Convert.ToDecimal(FormControls.StringFormate(this.txtPureGold.Text));
                p.PurchaseLineItems.Add(purchaseLineItem);
                lineNo = lineNo + 1;
                p.TotalPurchaseDiscount = p.TotalPurchaseDiscount + purchaseLineItem.Discount;
                p.TotalWeight = p.TotalWeight + purchaseLineItem.GrossWeight;
                p.TotalWastage = p.TotalWastage + purchaseLineItem.Wastage;
                p.TotalGoodWill = p.TotalGoodWill + purchaseLineItem.GoodWill;
                p.TotalPureGold = p.TotalPureGold + purchaseLineItem.PureGold;
            }
        }

        public void refreshRecord()
        {
            this.cbxItemCode.SelectedIndex = -1;
            this.txtDescription.Text = "";
            this.txtDOP.Text = "";
            this.txtMaking.Text = "";
            this.txtWeight.Text = "";
            this.txtWastage.Text = "";
            this.txtMaking.Text = "";
            this.txtRemarks.Text = "";
            this.txtPureGold.Text = "";
            this.dgvStonesDetail.Rows.Clear();
        }
        private void txtRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dgvStonesDetail.Focus();
            }
        }

        private void dgvStonesDetail_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddRow);
        }

        private void cbxSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            supplier = new Supplier();
            supplier = new SupplierDAL().GetRecByAbri(this.cbxSupplier.Text);
            if (supplier != null)
            {
                this.txtMaking.Text = supplier.PMaking.ToString();
                this.txtWastage.Text = supplier.PWastage.ToString();
                this.txtDOP.Text = supplier.PDiscount.ToString();
                this.txtPreviousAmount.Text = supplier.PrvCashBal.ToString();
                this.txtPreviousWeight.Text = supplier.PrvGoldBal.ToString();
                this.txtName.Text = supplier.PName.ToString();
            }
        }

       

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtWeight.Text), FormControls.StringFormate(this.txtWastage.Text),
               FormControls.StringFormate(this.txtMaking.Text), FormControls.StringFormate(this.txtDOP.Text), this.txtPureGold);
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {

        }

        private void txtWastage_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtWeight.Text), FormControls.StringFormate(this.txtWastage.Text),
               FormControls.StringFormate(this.txtMaking.Text), FormControls.StringFormate(this.txtDOP.Text), this.txtPureGold);
        }

        private void txtMaking_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtWeight.Text), FormControls.StringFormate(this.txtWastage.Text),
               FormControls.StringFormate(this.txtMaking.Text), FormControls.StringFormate(this.txtDOP.Text), this.txtPureGold);
        }

        private void txtDOP_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtWeight.Text), FormControls.StringFormate(this.txtWastage.Text),
               FormControls.StringFormate(this.txtMaking.Text), FormControls.StringFormate(this.txtDOP.Text), this.txtPureGold);
        }

        private void txtWastage_Leave(object sender, EventArgs e)
        {
            this.txtWastage.Text = FormControls.N3(this.txtWastage.Text);
        }

        private void txtMaking_Leave(object sender, EventArgs e)
        {
            this.txtMaking.Text = FormControls.N3(this.txtMaking.Text);
        }

        private void txtDOP_Leave(object sender, EventArgs e)
        {
            this.txtDOP.Text = FormControls.N3(this.txtDOP.Text);
        }

        private void cbxSupplier_Validating(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(cbxSupplier.Text))
            {
                e.Cancel = true;
                cbxSupplier.Focus();

                errorProvider1.SetError(cbxSupplier, "Supplier must be selected before Purchase");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbxSupplier, "");
            }
        }

        private void cbxItemCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbxItemCode_Validating(object sender, CancelEventArgs e)
        {
            //if (String.IsNullOrEmpty(cbxItemCode.Text))
            //{
            //    e.Cancel = true;
            //    cbxItemCode.Focus();

            //    errorProvider1.SetError(cbxItemCode, "Item must be selected before Add Row");
            //}
            //else
            //{
            //    e.Cancel = false;
            //    errorProvider1.SetError(cbxItemCode, "");
            //}
        }

        private void txtWeight_Validating(object sender, CancelEventArgs e)
        {
            decimal temp = Convert.ToDecimal(FormControls.StringFormate(txtWeight.Text));
            if (string.IsNullOrEmpty(txtWeight.Text) || temp == 0)
            {
                e.Cancel = true;
                txtWeight.Focus();
                errorProvider1.SetError(txtWeight, "Positive Weight must be Added before Add Row");
            }
            else
            {
                this.txtWeight.Text = FormControls.N3(this.txtWeight.Text);
                e.Cancel = false;
                errorProvider1.SetError(txtWeight, "");
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.cbxSupplier.CausesValidation = false;
            this.cbxItemCode.CausesValidation = false;
            this.txtWeight.CausesValidation = false;
            this.Close();
        }

        private void txtTWeight_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtTWeight.Text), FormControls.StringFormate(this.txtTWastage.Text),
               FormControls.StringFormate(this.txtTMaking.Text), FormControls.StringFormate(this.txtTDOP.Text), this.txtTPureGold);
        }

        private void txtTWastage_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtTWeight.Text), FormControls.StringFormate(this.txtTWastage.Text),
              FormControls.StringFormate(this.txtTMaking.Text), FormControls.StringFormate(this.txtTDOP.Text), this.txtTPureGold);
        }

        private void txtTMaking_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtTWeight.Text), FormControls.StringFormate(this.txtTWastage.Text),
              FormControls.StringFormate(this.txtTMaking.Text), FormControls.StringFormate(this.txtTDOP.Text), this.txtTPureGold);
        }

        private void txtTDOP_KeyUp(object sender, KeyEventArgs e)
        {
            Formulas.PureGoldIrfan(FormControls.StringFormate(this.txtTWeight.Text), FormControls.StringFormate(this.txtTWastage.Text),
              FormControls.StringFormate(this.txtTMaking.Text), FormControls.StringFormate(this.txtTDOP.Text), this.txtTPureGold);
        }

        private void txtTWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTWastage_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTDOP_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTPureGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ManagePurchase frm = new ManagePurchase();
            frm.Show();
        }

        public void Reset()
        {
            this.cbxSupplier.SelectedIndex = -1;
            this.cbxItemCode.SelectedIndex = -1;
            this.txtDescription.Text = "";
            this.txtDOP.Text = "";
            this.txtMaking.Text = "";
            this.txtWeight.Text = "";
            this.txtWastage.Text = "";
            this.txtMaking.Text = "";
            this.txtRemarks.Text = "";
            this.txtPureGold.Text = "";
            this.dgvStonesDetail.Rows.Clear();
            this.dgvMasterDetail.Rows.Clear();
            this.dgvItemDetail.Rows.Clear();
            this.txtTWeight.Text = "";
            this.txtTWastage.Text = "";
            this.txtTPureGold.Text = "";
            this.txtTMaking.Text = "";
            this.txtTDOP.Text = "";
            this.txtPreviousAmount.Text = "";
            this.txtPreviousWeight.Text = "";
            this.txtPureGold.Text = "";
            this.cbxSupplier.Select();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void txtTWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTWastage);
        }

        private void txtTWastage_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTMaking);
        }

        private void txtTMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTDOP);
        }

        private void txtTDOP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave.Select();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.dgvItemDetail.Rows.Count <= 1)
            {
                MessageBox.Show("Please First Enter Some Record to Save ...", Messages.Header);
                return;
            }
            if (MessageBox.Show(Messages.Sure, Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                SqlTransaction trans;
                int purNoRpt = 0;
                con.Open();
                trans = con.BeginTransaction();

                ChildAccount cha = new ChildAccount();
                ChildAccount child = new ChildAccount();
                Voucher custv = new Voucher();
                Voucher pv = new Voucher();
                VouchersDAL vDAL = new VouchersDAL();
                PaymentsDAL payDAL = new PaymentsDAL();
                PurchaseDAL pDAL = new PurchaseDAL();
                purchase.PurchaseNo = pDAL.GetMaxPurchaseNo() + 1;
                Supplier sp = (Supplier)this.cbxSupplier.SelectedItem;
                purchase.PAccountCode = sp.AccountCode;

                try
                {
                    new PurchaseDAL().AddPurchase(purchase, out purNoRpt, con, trans);

                    #region Vouchers

                    #region Pure Gold Voucher

                    //gold accunt entry
                    pv = new Voucher();
                    cha = new ChildAccount();
                    cha = new AccountDAL().GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    if (cha == null)
                    {
                        //cha = new ChildAccount();
                        string Code = new AccountDAL().CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                        cha = new AccountDAL().GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    }
                    pv.AccountCode = new AccountDAL().GetChildByCode(cha.ChildCode, con, trans);
                    pv.GoldDr = (decimal)purchase.TotalPureGold; //Convert.ToDecimal(s.PureGoldCharges);
                    pv.GoldCr = 0;
                    pv.DDate = (DateTime)purchase.PurchaseDate;
                    pv.RNO = purNoRpt;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.PNO = purNoRpt;
                    pv.VNO = new VouchersDAL().CreateVNO("PRV", con, trans);
                    //pv.VNO = chq.VNO;
                    pv.Description = "Pure Gold Purchase From PurchaseNo" + purNoRpt.ToString();
                    new VouchersDAL().AddVoucher(pv, con, trans);
                    //customer account entry
                    custv = new Voucher();
                    custv.AccountCode = new AccountDAL().GetChildByCode(purchase.PAccountCode, con, trans);
                    custv.GoldCr = (decimal)purchase.TotalPureGold;//s.PureGoldCharges;
                    custv.GoldDr = 0;
                    custv.DDate = (DateTime)purchase.PurchaseDate;
                    custv.RNO = purNoRpt;
                    custv.OrderNo = 0;
                    custv.SNO = 0;
                    custv.VNO = pv.VNO;
                    custv.PNO = pv.PNO;
                    custv.Description = pv.Description;
                    new VouchersDAL().AddVoucher(custv, con, trans);


                    #endregion

                    #endregion

                }
                catch (Exception ex)
                {

                    trans.Rollback();
                    con.Close();
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        trans.Commit();
                        con.Close();
                        MessageBox.Show(Messages.Saved, Messages.Header);
                        this.Dispose();
                        ManagePurchase frm = new ManagePurchase();
                        frm.ShowDialog();
                    }
                }
            }
            else
            {

                return;
            }
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dgvItemDetail.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (this.dgvItemDetail.Rows.Count == 1)
            {
                return;
            }
            if (selectedRowCount > 0)
            {


                for (int i = 0; i < selectedRowCount; i++)
                {

                    string karat = dgvItemDetail.Rows[i].Cells[3].Value.ToString();
                    int count = purchase.PurchaseSubLineItems.Count(x => x.Karat == karat);
                    if (count >= 1)
                    {
                        dgvItemDetail.Rows.Remove(dgvItemDetail.Rows[i]);                       
                        int itemId = Convert.ToInt32(dgvItemDetail.Rows[i].Cells[0].Value);
                        PurchaseSubLineItems pr = purchase.PurchaseSubLineItems.FirstOrDefault(x => x.ItemId == itemId);
                        purchase.PurchaseSubLineItems.Remove(pr);
                        RemoveRowData(pr);
                        ShowGridData(purchase);
                    }
                    else
                    {

                        dgvItemDetail.Rows.Remove(dgvItemDetail.Rows[i]);
                        int itemId = Convert.ToInt32(dgvItemDetail.Rows[i].Cells[0].Value);
                        PurchaseSubLineItems pr = purchase.PurchaseSubLineItems.FirstOrDefault(x => x.ItemId == itemId);
                        purchase.PurchaseSubLineItems.Remove(pr);
                        PurchaseLineItems prL = purchase.PurchaseLineItems.FirstOrDefault(x => x.Karat == karat);
                        purchase.PurchaseLineItems.Remove(prL);
                        ShowGridData(purchase);
                    }
                }

            }
            else
                MessageBox.Show("Please Select Row", "Jewel Manager 2018");
        }

        public void RemoveRowData(PurchaseSubLineItems prs)
        {
            foreach (var item in purchase.PurchaseLineItems)
            {
                if (item.Karat == prs.Karat)
                {
                    item.Qty = item.Qty - prs.Qty;
                    item.GrossWeight = item.GrossWeight - prs.Weight;
                    item.Wastage = item.Wastage - prs.SubWastage;
                    item.Discount = item.Discount - prs.SubDOP;
                    item.GoodWill = item.GoodWill - prs.Making;
                    item.PureGold = item.PureGold - prs.SubPureGold;

                    purchase.TotalPurchaseDiscount = purchase.TotalPurchaseDiscount - prs.SubDOP;
                    purchase.TotalWeight = purchase.TotalWeight - prs.Weight;
                    purchase.TotalWastage = purchase.TotalWastage - prs.SubWastage;
                    purchase.TotalGoodWill = purchase.TotalGoodWill - prs.Making;
                    purchase.TotalPureGold = purchase.TotalPureGold - prs.SubPureGold;
                }
            }


        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            PurchaseReports frm = new PurchaseReports();
            frm.ShowDialog();
        }
    }
}
