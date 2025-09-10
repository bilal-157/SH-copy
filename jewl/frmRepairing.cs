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
using System.Globalization;
using System.Data.SqlClient;

namespace jewl
{
    public partial class frmRepairing : Form
    {
        JewelConnection con;
        private Voucher pv;
        private Voucher custv;
        private AccountDAL adal = new AccountDAL();
        private VouchersDAL vDAL = new VouchersDAL();
        Reparing rep = new Reparing();
        frmRepairNo load;
        RepairLineItem rli;
        ChildAccount ca = new ChildAccount();
        RepairDAL rDAL = new RepairDAL();
        GoldRateDAL grDAL = new GoldRateDAL();
        WorkerDAL wDAL = new WorkerDAL();
        StonesDAL sDAL = new StonesDAL();
        ManageCustomer adcust = new ManageCustomer();
        CustomerDAL custDAL = new CustomerDAL();
        SaleManDAL smDAL = new SaleManDAL();
        RepairLineItem repair = new RepairLineItem();
        List<Customer> custs;
        bool eFlag = false;
        Customer cust;
        int z, l = 1;
        decimal s, finalTotal;
        string str;

        public frmRepairing()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            txtDescription.Multiline = true;
            panel1.BorderStyle = BorderStyle.None;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            this.dgvCustomer.Visible = false;
            adcust = new ManageCustomer();
            adcust.Show();
            rbtExistingCustomer.Checked = true;
            txtCustomerName.Select();
        }

        private void ShowDataGrid()
        {
            this.Column13.DataSource = sDAL.GetAllStoneTypeName();
            this.Column13.DisplayMember = "TypeName";
            this.Column13.ValueMember = "TypeId";
            this.Column1.DataSource = sDAL.GetAllStoneName();
            this.Column1.DisplayMember = "StoneName";
            this.Column1.ValueMember = "StoneId";
            Column10.DataSource = sDAL.GetAllColorName();
            Column10.DisplayMember = "ColorName";
            Column10.ValueMember = "ColorId";
            Column11.DataSource = sDAL.GetAllCutName();
            Column11.DisplayMember = "CutName";
            Column11.ValueMember = "CutId";
            Column12.DataSource = sDAL.GetAllClearityName();
            Column12.DisplayMember = "ClearityName";
            Column12.ValueMember = "ClearityId";
        }

        private void Repairing_Load(object sender, EventArgs e)
        {
            this.rbtExistingCustomer.Checked = true;
            this.cbxDeliveredCustomer.Enabled = false;
            this.cbxItemId.Enabled = false;
            this.cbxRepairId.Enabled = false;
            this.cbxKarrat.SelectedIndex = 2;
            dgvCustomer.Size = new Size(500, 100);
            ShowDataGrid();
            txtRepairId.Text = Convert.ToString(rDAL.GetRepairId() + 1);
            this.txtitemId.Text = Convert.ToInt32(this.txtRepairId.Text) + "-" + 1;
            this.cbxWorkerName.DataSource = wDAL.GetAllWorkers();
            this.cbxWorkerName.DisplayMember = "Name";
            this.cbxWorkerName.ValueMember = "ID";
            this.cbxWorkerName.SelectedIndex = -1;
            this.cbxSaleManName.DataSource = smDAL.GetAllSaleMen();
            this.cbxSaleManName.DisplayMember = "Name";
            this.cbxSaleManName.ValueMember = "ID";
            this.cbxSaleManName.SelectedIndex = -1;
            this.btnRepairOut.Enabled = false;
            this.btnWorkerUpDate.Enabled = false;
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
                this.btnEdit.Enabled = false;
            DisableRepairCost();
            dtpReceiveDate.Select();
        }

        private void txtCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dgvCustomer.Visible = false;
                this.cbxWorkerName.Select();
            }
            else
            {
                this.dgvCustomer.Visible = true;
                this.dgvCustomer.Height = 100;
            }
        }

        private void txtCustomerName_KeyUp(object sender, KeyEventArgs e)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where Name like '%" + this.txtCustomerName.Text + "%'");
            if (custs == null)
                return;
            else
            {
                this.dgvCustomer.AutoGenerateColumns = false;
                this.dgvCustomer.Rows.Clear();
                int count = custs.Count;
                this.dgvCustomer.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvCustomer.Rows[i].Cells[0].Value = custs[i].Salutation.ToString() + custs[i].Name.ToString();
                    this.dgvCustomer.Rows[i].Cells[1].Value = custs[i].Mobile.ToString();
                    this.dgvCustomer.Rows[i].Cells[2].Value = custs[i].Address.ToString();
                    this.dgvCustomer.Rows[i].Cells[3].Value = custs[i].ID.ToString();
                }
            }
        }

        private void ShowCustomer(int id)
        {
            cust = custDAL.SearchCustById(id);
            if (cust == null)
                return;
            else
            {
                this.txtCustomerName.Text = cust.Salutation.ToString() + cust.Name.ToString();
                this.txtAddress.Text = cust.Address.ToString();
                string str = cust.Mobile.ToString();
                if (string.IsNullOrEmpty(str))
                    this.txtContactNo.Text = cust.CNIC.ToString();
                else
                    this.txtContactNo.Text = cust.Mobile.ToString();
            }
        }

        #region DgvInformation
        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            int custid = Convert.ToInt32(dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString());
            this.lblCustId.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();
            this.ShowCustomer(custid);
            this.dgvCustomer.Visible = false;
        }

        private void dgvStoneInformation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStoneInformation.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
                e.Cancel = true;
        }

        private void dgvStoneInformation_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                decimal weight, Qty, Rate;
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    weight = 0;
                else
                    weight = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value == null)
                    Qty = 0;
                else
                    Qty = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value == null)
                    Rate = 0;
                else
                    Rate = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((Qty * Rate), 0));
                else
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((weight * Rate), 0));
                int count = 0;
                count = dgvStoneInformation.Rows.Count;
                decimal Total = 0;
                for (int i = 0; i < count - 1; i++)
                {
                    Total += decimal.Parse(dgvStoneInformation.Rows[i].Cells["Column5"].Value.ToString());
                }
                txtStonePrice.Text = Math.Round(Total, 0).ToString();
            }
            if (e.ColumnIndex == 4)
            {
                decimal weight, Qty, Rate;
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    weight = 0;
                else
                    weight = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value == null)
                    Qty = 0;
                else
                    Qty = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value == null)
                    Rate = 0;
                else
                    Rate = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((Qty * Rate), 0));
                else
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((weight * Rate), 0));
                int count = 0;
                count = dgvStoneInformation.Rows.Count;
                decimal Total = 0;
                for (int i = 0; i < count - 1; i++)
                {
                    Total += decimal.Parse(dgvStoneInformation.Rows[i].Cells["Column5"].Value.ToString());
                }
                txtStonePrice.Text = Math.Round(Total, 0).ToString();
            }
            if (e.ColumnIndex == 3)
            {
                decimal weight, Qty, Rate;
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    weight = 0;
                else
                    weight = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value == null)
                    Qty = 0;
                else
                    Qty = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value == null)
                    Rate = 0;
                else
                    Rate = decimal.Parse(dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value.ToString());
                if (dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value == null)
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((Qty * Rate), 0));
                else
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column5"].Value = Convert.ToString(Math.Round((weight * Rate), 0));
                int count = 0;
                count = dgvStoneInformation.Rows.Count;
                decimal Total = 0;
                for (int i = 0; i < count - 1; i++)
                {
                    Total += decimal.Parse(dgvStoneInformation.Rows[i].Cells["Column5"].Value.ToString());
                }
                txtStonePrice.Text = Math.Round(Total, 0).ToString();
            }
            if (e.ColumnIndex == 6)
            {
                dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value = null;
                dgvStoneInformation.Rows[e.RowIndex].Cells["Column3"].Value = null;
                dgvStoneInformation.Rows[e.RowIndex].Cells["Column4"].Value = null;
                int count = 0;
                count = dgvStoneInformation.Rows.Count;
                decimal Total = 0;
                for (int i = 0; i < count - 1; i++)
                {
                    Total += decimal.Parse(dgvStoneInformation.Rows[i].Cells["Column5"].Value.ToString());
                }
                txtStonePrice.Text = Math.Round(Total, 0).ToString();
            }
        }
        #endregion

        #region KeyEvent
        private void cbxKarrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string r = (string)this.cbxKarrat.SelectedItem;
            s = grDAL.GetRateByKarat(r, Convert.ToDateTime(dtpReceiveDate.Value));
            this.txtGoldRate.Text = s.ToString();
            {
                if (txtGoldRate.Text == "")
                    txtGoldRate.Text = "0";
                else
                    return;
                this.txtGoldPrice.Text = Math.Round((Convert.ToDecimal(txtRepairWeight.Text) * Convert.ToDecimal(txtGoldRate.Text)), 0).ToString("0");
            }
        }

        private void txtRepairWeight_TextChanged(object sender, EventArgs e)
        {
            GoldPrice();
        }

        private void txtLacker_TextChanged(object sender, EventArgs e)
        {
            SumCost();
        }

        private void txtMaking_TextChanged(object sender, EventArgs e)
        {
            SumCost();
        }

        private void txtStonePrice_TextChanged(object sender, EventArgs e)
        {
            SumCost();
        }

        private void txtRepairCharges_TextChanged(object sender, EventArgs e)
        {
            SumCost();
        }

        private void SumCost()
        {
            decimal val1, val2, val3, val4, val5, val6;
            if (this.txtGoldPrice.Text == "")
                val1 = 0;
            else
                val1 = Convert.ToDecimal(this.txtGoldPrice.Text);
            if (this.txtLacker.Text == "")
                val2 = 0;
            else
                val2 = Convert.ToDecimal(this.txtLacker.Text);
            if (this.txtMaking.Text == "")
                val3 = 0;
            else
                val3 = Convert.ToDecimal(this.txtMaking.Text);
            if (this.txtStonePrice.Text == "")
                val4 = 0;
            else
                val4 = Convert.ToDecimal(this.txtStonePrice.Text);
            if (this.txtRepairCharges.Text == "")
                val5 = 0;
            else
                val5 = Convert.ToDecimal(this.txtRepairCharges.Text);
            this.txtPerItemRTotal.Text = Math.Round((val1 + val2 + val3 + val4 + val5), 0).ToString();
            if (txtRepairTotal.Text == "")
                val6 = 0;
            else
                val6 = finalTotal;
            txtRepairTotal.Text = Math.Round((val6 + Convert.ToDecimal(txtPerItemRTotal.Text)), 0).ToString("0");
        }
        #endregion

        private void EnableItemInfo()
        {
            txtReciveWeight.Enabled = true;
            txtQty.Enabled = true;
            txtItemName.Enabled = true;
            txtDescription.Enabled = true;
        }

        private void DisableItemInfo()
        {
            txtReciveWeight.Enabled = false;
            txtQty.Enabled = false;
            txtItemName.Enabled = false;
            txtDescription.Enabled = false;
        }

        private void EnableRepairCost()
        {
            txtRepairWeight.Enabled = true;
            cbxKarrat.Enabled = true;
            txtGoldRate.Enabled = true;
            txtGoldPrice.Enabled = true;
            txtLacker.Enabled = true;
            txtMaking.Enabled = true;
            txtRepairCharges.Enabled = true;
        }

        private void DisableRepairCost()
        {
            txtRepairWeight.Enabled = false;
            cbxKarrat.Enabled = false;
            txtGoldRate.Enabled = false;
            txtGoldPrice.Enabled = false;
            txtLacker.Enabled = false;
            txtMaking.Enabled = false;
            txtRepairCharges.Enabled = false;
        }

        private void EnableCalculation()
        {
            txtDiscount.Enabled = true;
            txtAdvance.Enabled = true;
            txtRemaining.Enabled = true;
        }

        private void DisableCalculation()
        {
            txtDiscount.Enabled = false;
            txtAdvance.Enabled = false;
            txtRemaining.Enabled = false;
        }

        #region ConvertValue
        public string IfEmpty4String(string txtValue)
        {
            string a;
            if (txtValue == "")
                a = "NULL";
            else
                a = txtValue;
            return a;
        }

        public decimal IfEmpty4Float(string txtValue)
        {
            decimal a;
            if (txtValue == "")
                a = 0;
            else
                a = decimal.Parse(txtValue);
            return a;
        }

        public int IfEmpty4Int(string txtValue)
        {
            int a;
            if (txtValue == "")
                a = 0;
            else
                a = int.Parse(txtValue);
            return a;
        }
        #endregion

        private List<Stones> GetStoneDetails()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStoneInformation.Rows.Count) - 1;
            if (j == 0)
                return stDetail;
            else
            {
                stDetail = new List<Stones>();
                for (int i = 0; i < j; i++)
                {
                    Stones std = new Stones();
                    std.StoneTypeId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[1].Value.ToString());
                    std.StoneId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[2].Value.ToString());
                    if ((string)dgvStoneInformation.Rows[i].Cells[3].FormattedValue == "")
                        std.StoneWeight = 0;
                    else
                        std.StoneWeight = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[3].Value.ToString()), 3);
                    if ((string)dgvStoneInformation.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = 0;

                    else
                        std.Qty = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[4].Value.ToString());
                    if ((string)dgvStoneInformation.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = 0;
                    else
                        std.Rate = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[5].Value.ToString()), 1);
                    if ((string)dgvStoneInformation.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = 0;
                    else
                        std.Price = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[6].Value.ToString()), 0);
                    if ((string)dgvStoneInformation.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {
                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < Column10.Items.Count; k++)
                        {
                            StoneColor stc = (StoneColor)Column10.Items[k];
                            if (stc.ColorId == std.ColorName.ColorId)
                                std.ColorName.ColorName = stc.ColorName;
                        }
                    }
                    if ((string)dgvStoneInformation.Rows[i].Cells[8].FormattedValue == "")
                        std.CutName = null;
                    else
                    {
                        std.CutName = new StoneCut();
                        std.CutName.CutId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[8].Value.ToString());
                        for (int k = 0; k < Column11.Items.Count; k++)
                        {
                            StoneCut stc = (StoneCut)Column11.Items[k];
                            if (stc.CutId == std.CutName.CutId)
                                std.CutName.CutName = stc.CutName;
                        }
                    }
                    if ((string)dgvStoneInformation.Rows[i].Cells[9].FormattedValue == "")
                        std.ClearityName = null;
                    else
                    {
                        std.ClearityName = new StoneClearity();
                        std.ClearityName.ClearityId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[9].Value.ToString());
                        for (int k = 0; k < Column12.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)Column12.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }

        private void createRepair(object sender, EventArgs e)
        {
            rli = new RepairLineItem();
            rli.RepairId = int.Parse(txtRepairId.Text);
            rli.ItemId = txtitemId.Text;
            rli.ReceiveWeight = Math.Round(IfEmpty4Float(txtReciveWeight.Text), 3);
            rli.Qty = IfEmpty4Int(txtQty.Text);
            rli.ItemName = txtItemName.Text;
            rli.Description = IfEmpty4String(txtDescription.Text);
            rli.RepairWeight = Math.Round(IfEmpty4Float(txtRepairWeight.Text), 3);
            rli.Karat = cbxKarrat.GetItemText(cbxKarrat.SelectedItem);
            rli.GoldRate = Math.Round(IfEmpty4Float(txtGoldRate.Text), 1);
            rli.Lacker = Math.Round(IfEmpty4Float(txtLacker.Text), 0);
            rli.Making = Math.Round(IfEmpty4Float(txtMaking.Text), 0);
            Worker wrk = (Worker)cbxWorkerName.SelectedItem;
            if (wrk == null)
                rli.WorkerId = null;
            else
                rli.WorkerId = wrk.ID;
            rli.StonePrice = Math.Round(IfEmpty4Float(txtStonePrice.Text), 0);
            rli.RepairCharges = Math.Round(IfEmpty4Float(txtRepairCharges.Text), 0);
            rli.PerItemCost = Math.Round(IfEmpty4Float(txtPerItemRTotal.Text), 0);
            rli.RepairingStatus = "Reparing";
            rli.ItemStatus = "Not Deliverd";
            rli.StoneList = this.GetStoneDetails();
            rep.AddRLineItems(rli);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (this.txtItemName.Text == "")
                MessageBox.Show("First Enter ItemName", Messages.Header);
            else if (this.txtReciveWeight.Text == "")
                MessageBox.Show("First Enter ReceiveWeight", Messages.Header);
            else if (this.txtPerItemRTotal.Text == "")
                MessageBox.Show("first enter Repairing Detail", Messages.Header);
            else if (this.cbxWorkerName.Text == "")
                MessageBox.Show("first select Worker", Messages.Header);
            else
            {
                createRepair(sender, e);
                object[] values1 = new Object[2];
                values1[0] = this.txtitemId.Text;
                values1[1] = txtItemName.Text;
                this.dgvRepairItemlist.Rows.Add(values1);
                l = l + 1;
                this.txtitemId.Text = Convert.ToInt32(this.txtRepairId.Text) + "-" + l;
                this.RefresgRepairngForm();
                finalTotal = Math.Round(Convert.ToDecimal(this.txtRepairTotal.Text), 0);
            }
        }

        public void RefresgRepairngForm()
        {
            string advance = this.txtAdvance.Text;
            string balance = this.txtBalance.Text;
            string total = txtRepairTotal.Text;
            this.txtItemName.Text = "";
            this.dgvCustomer.Rows.Clear();
            this.txtReciveWeight.Text = "";
            this.txtQty.Text = "";
            this.txtDescription.Text = "";
            this.txtRepairWeight.Text = "";
            this.cbxKarrat.SelectedIndex = 2;
            this.txtGoldPrice.Text = "";
            this.txtLacker.Text = "";
            this.txtMaking.Text = "";
            this.txtStonePrice.Text = "";
            this.txtRepairCharges.Text = "";
            this.txtBalance.Text = "";
            this.txtAdvance.TextChanged -= new EventHandler(txtAdvance_TextChanged);
            txtRepairTotal.Text = total;
            this.txtAdvance.Text = advance;
            this.txtBalance.Text = balance;
            this.dgvStoneInformation.Rows.Clear();
            EnableItemInfo();
            DisableRepairCost();
            EnableCalculation();
        }

        #region Key Press
        private void txtReciveWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtAdvance.TextChanged += new EventHandler(txtAdvance_TextChanged);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtRepairWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtItemName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 && (Convert.ToInt16(e.KeyChar) < 122) && Convert.ToInt16(e.KeyChar) != 8) && (Convert.ToInt16(e.KeyChar) != 32))
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }
        private void txtItemName_Leave(object sender, EventArgs e)
        {
            //CultureInfo culInfo = CultureInfo.CurrentCulture;
            //TextInfo txtinfo = culInfo.TextInfo;
            //string str = this.txtItemName.Text;
            //this.txtItemName.Text = txtinfo.ToTitleCase(str);
        }
        private void txtGoldPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtLacker_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtRepairCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtRepairTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtAdvance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        #endregion

        private void txtAdvance_TextChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void txtRepairTotal_TextChanged(object sender, EventArgs e)
        {
            this.txtBalance.Text = this.txtRepairTotal.Text;
        }

        private void dgvStoneInformation_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (e.ColumnIndex == 2)
                {
                    if (Convert.ToString(dgvStoneInformation.Rows[e.RowIndex].Cells[1].Value) == "")
                        MessageBox.Show("First Select Type", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvRepairItemlist_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (eFlag == true)
                {
                    string strTagNo = this.dgvRepairItemlist.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.dgvRepairItemlist.Rows.RemoveAt(e.RowIndex);
                    RepairLineItem rl = new RepairLineItem();
                    foreach (RepairLineItem rli in rep.RepairlineItem)
                    {
                        if (rli.ItemId == strTagNo)
                        {
                            this.ShowRecordByItemId(strTagNo);
                            rl = rli;
                        }
                    }
                    if (rl != null)
                        rep.RemoveLineItems(rl);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.txtCustomerName.Text == "")
                MessageBox.Show("First Enter CustomerName", Messages.Header);
            else if (this.txtRepairTotal.Text == "")
                MessageBox.Show("First Enter Repairing", Messages.Header);
            else
            {
                rep.RepairId = Convert.ToInt32(txtRepairId.Text);
                rep.CustId = IfEmpty4Int(lblCustId.Text);
                rep.ReceiveDate = Convert.ToDateTime(dtpReceiveDate.Text);
                rep.GivenDate = Convert.ToDateTime(dtpDeliveryDate.Text);
                rep.TotalRepairCost = IfEmpty4Float(txtRepairTotal.Text);
                rep.Advance = IfEmpty4Float(txtAdvance.Text);
                rep.Discount = IfEmpty4Float(txtDiscount.Text);
                rep.Status = "Not Deliverd";
                rep.SaleManId = Convert.ToInt32(cbxSaleManName.SelectedValue);
                rep.WorkerId = Convert.ToInt32(cbxWorkerName.SelectedValue);
                rep.BillBookNo = txtBillBookNo.Text;
                rDAL.AddReparing(rep);
                this.dgvRepairItemlist.Rows.Clear();
                if (txtAdvance.Text != "")
                {
                    #region Cash voucher
                    ChildAccount cha;
                    //cash in hand entry
                    pv = new Voucher();
                    cha = adal.GetAccount(1, "Current Asset", "Cash In Hand");
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        string Code = adal.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", 0);
                        cha = adal.GetAccount(1, "Current Asset", "Cash In Hand");
                    }
                    pv.AccountCode = cha;
                    if (txtAdvance.Text == "")
                        pv.Dr = 0;
                    else
                        pv.Dr = Convert.ToDecimal(this.txtAdvance.Text);
                    pv.Cr = 0;
                    pv.DDate = this.dtpReceiveDate.Value;
                    pv.RID = Convert.ToInt32(this.txtRepairId.Text);
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("CRV");
                    pv.Description = "Cash Receive for Repair No" + this.txtRepairId.Text;
                    vDAL.AddVoucher(pv);
                    //Customer account entry
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    cust = new Customer();
                    cust = custDAL.SearchCustById(rep.CustId);
                    child = adal.GetChildByCode(cust.AccountCode);

                    custv.AccountCode = child;
                    if (this.txtAdvance.Text == "")
                        custv.Cr = 0;
                    else
                        custv.Cr = Convert.ToDecimal(this.txtAdvance.Text);
                    custv.Dr = 0;
                    custv.DDate = this.dtpReceiveDate.Value;
                    custv.RID = Convert.ToInt32(this.txtRepairId.Text);
                    custv.SNO = 0;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv);
                    #endregion
                }
                if (this.txtAdvance.Text == "")
                    cust.CashBalance -= 0;
                else
                    cust.CashBalance -= Convert.ToDecimal(this.txtAdvance.Text);
                MessageBox.Show("Record Saved Successfully", Messages.Header);
                frmRepairingRpt reload1 = new frmRepairingRpt();
                string selectQuery = "select rep.*, repd.*, ci.* from tblrepair rep inner join CustomerInfo ci on ci.CustId=rep.CustId inner join tblRepairDetail repd on repd.RepairId=rep.RepairId where rep.RepairId = '" + int.Parse(txtRepairId.Text) + "'";
                reload1.selectQuery = selectQuery;
                reload1.ShowDialog();
                frmRepairing frm = new frmRepairing();
                this.Dispose();
                frm.ShowDialog();
                EnableItemInfo();
                DisableRepairCost();
                EnableCalculation();
            }
        }

        private void ShowRepair(int rNo)
        {
            if (rep == null)
            {
                MessageBox.Show("There Is No Repair No. ", Messages.Header);
                return;
            }
            else
            {
                rep = rDAL.GetRepairByRepairNo(rNo);
                this.txtRepairId.Text = rep.RepairId.ToString();
                this.lblCustId.Text = rep.CustId.ToString();
                this.txtRepairTotal.Text = rep.TotalRepairCost.ToString();
                this.txtAdvance.Text = rep.Advance.ToString();
                this.ShowCustomer((int)rep.CustName.ID);
                this.dgvRepairItemlist.AutoGenerateColumns = false;
                this.dgvRepairItemlist.Rows.Clear();
                if (rep.RepairlineItem != null && rep.RepairlineItem.Count > 0)
                {
                    int i = 0;
                    foreach (RepairLineItem rli in rep.RepairlineItem)
                    {
                        object[] values1 = new Object[2];
                        values1[0] = rli.ItemId.ToString();
                        values1[1] = rli.ItemName.ToString();
                        this.dgvRepairItemlist.Rows.Add(values1);
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                frmRepairNo rNo = new frmRepairNo();
                rNo.ShowDialog();

                if ((int)rNo.RepairNo == 0)
                {
                    MessageBox.Show("There Is No Repair No ", Messages.Header);
                    return;
                }
                else
                {
                    this.ShowRepair((int)rNo.RepairNo);
                    this.btnEdit.Text = "Update";
                    eFlag = true;
                    return;
                }
            }
            if (this.btnEdit.Text == "Update")
            {
                if (this.dgvRepairItemlist.Rows.Count <= 0)
                {
                    MessageBox.Show("Please add Item to update", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                rep.CustId = IfEmpty4Int(lblCustId.Text);
                rep.ReceiveDate = Convert.ToDateTime(dtpReceiveDate.Text);
                rep.GivenDate = Convert.ToDateTime(dtpDeliveryDate.Text);
                rep.TotalRepairCost = IfEmpty4Float(txtRepairTotal.Text);
                rep.Advance = IfEmpty4Float(txtAdvance.Text);
                rDAL.UpDateReparing(rep);
                MessageBox.Show("Record Updated Successfully");
                this.dgvRepairItemlist.Rows.Clear();
                this.btnEdit.Text = "&Edit";
            }
        }

        private void ShowRecordByItemId(string strg)
        {
            foreach (RepairLineItem rli in rep.RepairlineItem)
            {
                if (rli.ItemId == strg)
                {
                    this.txtGoldRate.Text = rli.GoldRate.ToString();
                    this.txtitemId.Text = rli.ItemId.ToString();
                    if (rli.ReceiveWeight == 0)
                        this.txtReciveWeight.Text = "";
                    else
                        this.txtReciveWeight.Text = rli.ReceiveWeight.ToString();
                    if (rli.Qty == 0)
                        this.txtQty.Text = "";
                    else
                        this.txtQty.Text = rli.Qty.ToString();
                    this.txtItemName.Text = rli.ItemName;
                    if (rli.Description == null || rli.Description == "")
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = rli.Description.ToString();
                    if (rli.RepairWeight == 0)
                        this.txtRepairWeight.Text = "";
                    else
                        this.txtRepairWeight.Text = rli.RepairWeight.ToString();
                    for (int i = 0; i < this.cbxKarrat.Items.Count; i++)
                    {
                        string str1 = (string)this.cbxKarrat.Items[i];
                        if (rli.Karat.Equals(str1))
                        {
                            this.cbxKarrat.SelectedIndex = i;
                            break;
                        }
                        else
                            this.cbxKarrat.SelectedIndex = 2;
                    }
                    this.txtGoldPrice.Text = rli.GoldRate.ToString();
                    if (rli.Lacker == 0)
                        this.txtLacker.Text = "";
                    else
                        this.txtLacker.Text = rli.Lacker.ToString();
                    if (rli.Making == 0)
                        this.txtMaking.Text = "";
                    else
                        this.txtMaking.Text = rli.Making.ToString();
                    if (rli.StonePrice == 0)
                        this.txtStonePrice.Text = "";
                    else
                        this.txtStonePrice.Text = rli.StonePrice.ToString();
                    if (rli.RepairCharges == 0)
                        this.txtRepairCharges.Text = "";
                    else
                        this.txtRepairCharges.Text = rli.RepairCharges.ToString();

                    this.txtPerItemRTotal.Text = rli.PerItemCost.ToString();
                    if (rli.StoneList == null)
                        return;
                    else
                    {
                        this.dgvStoneInformation.AutoGenerateColumns = false;
                        int count = rli.StoneList.Count;
                        this.dgvStoneInformation.Rows.Add(count);
                        for (int i = 0; i < rli.StoneList.Count; i++)
                        {
                            this.dgvStoneInformation.Rows[i].Cells[1].Value = rli.StoneList[i].StoneTypeId;
                            this.dgvStoneInformation.Rows[i].Cells[2].Value = rli.StoneList[i].StoneId;
                            if (rli.StoneList[i].StoneWeight == 0)
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = Convert.ToDecimal(rli.StoneList[i].StoneWeight);
                            if (rli.StoneList[i].Qty == 0)
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = Convert.ToInt32(rli.StoneList[i].Qty);
                            if (rli.StoneList[i].Rate == 0)
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = Convert.ToDecimal(rli.StoneList[i].Rate);
                            if (rli.StoneList[i].Price == 0)
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = Convert.ToDecimal(rli.StoneList[i].Price);
                            if (rli.StoneList[i].ColorName != null)
                            {
                                for (int j = 0; j < this.Column10.Items.Count; j++)
                                {
                                    StoneColor stcl = (StoneColor)this.Column10.Items[j];
                                    if (rli.StoneList[i].ColorName.ColorName.Equals(stcl.ColorName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[7].Value = Convert.ToInt32(stcl.ColorId);
                                }
                            }
                            if (rli.StoneList[i].CutName != null)
                            {
                                for (int j = 0; j < this.Column11.Items.Count; j++)
                                {
                                    StoneCut stcl = (StoneCut)this.Column11.Items[j];
                                    if (rli.StoneList[i].CutName.CutName.Equals(stcl.CutName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[8].Value = Convert.ToInt32(stcl.CutId);
                                }
                            }
                            if (rli.StoneList[i].ClearityName != null)
                            {
                                for (int j = 0; j < this.Column12.Items.Count; j++)
                                {
                                    StoneClearity stcl = (StoneClearity)this.Column12.Items[j];
                                    if (rli.StoneList[i].ClearityName.ClearityName.Equals(stcl.ClearityName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[9].Value = Convert.ToInt32(stcl.ClearityId);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void cbxRepairId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            con = new JewelConnection();
            try
            {
                con.MyDataSet.Tables["Table1"].Rows.Clear();
            }
            catch (Exception)
            {
                //throw ex;
            }
            try
            {
                string getidquery;
                int repairid = int.Parse(this.cbxRepairId.GetItemText(cbxRepairId.SelectedItem).ToString());
                getidquery = "select itemid from tblRepairDetail where repairid = '" + repairid + "'and tblRepairDetail.RepairingStatus = 'Reparing'";
                con.GetDataFromJMDB(getidquery, "Table1");
                this.cbxItemId.DataSource = con.MyDataSet.Tables["Table1"];
                this.cbxItemId.DisplayMember = "ItemId";
            }
            catch (Exception ex0)
            {
                throw ex0;
            }
        }

        private void cbxItemId_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string tagno = "", strTagNo = "";
            tagno = cbxWorkerName.GetItemText(cbxWorkerName.SelectedItem).ToString();
            strTagNo = cbxItemId.GetItemText(cbxItemId.SelectedItem).ToString();
            int RepairedId = int.Parse(cbxRepairId.GetItemText(cbxRepairId.SelectedItem).ToString());
            rep = rDAL.GetRepairByRepairNo1(RepairedId, strTagNo);
            this.txtRepairId.Text = rep.RepairId.ToString();
            this.lblCustId.Text = rep.CustId.ToString();
            this.txtRepairTotal.Text = rep.TotalRepairCost.ToString();
            this.txtAdvance.Text = rep.Advance.ToString();
            this.txtBillBookNo.Text = rep.BillBookNo;
            this.cbxSaleManName.SelectedValue = rep.SaleManId;
            this.cbxWorkerName.SelectedValue = rep.WorkerId;
            this.ShowCustomer((int)rep.CustName.ID);
            if (this.cbxItemId.Text == "")
                return;
            else
            {
                RepairLineItem rl = new RepairLineItem();
                foreach (RepairLineItem rli in rep.RepairlineItem)
                {
                    if (rli.ItemId == strTagNo)
                    {
                        this.ShowRecordByItemId(strTagNo);
                        rl = rli;
                    }
                }
                if (rl != null)
                    rep.RemoveLineItems(rl);
            }
        }

        private void txtRemaining_TextChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void btnWorkerUpDate_Click(object sender, EventArgs e)
        {
            if (this.dgvRepairItemlist.Rows.Count <= 0)
            {
                MessageBox.Show("Please add Item to update", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            rep.CustId = IfEmpty4Int(lblCustId.Text);
            rep.ReceiveDate = Convert.ToDateTime(dtpReceiveDate.Text);
            rep.GivenDate = Convert.ToDateTime(dtpDeliveryDate.Text);
            rep.TotalRepairCost = IfEmpty4Float(txtRepairTotal.Text);
            rep.Advance = IfEmpty4Float(txtAdvance.Text);
            rDAL.ReciveFromWorker(rep);
            MessageBox.Show("Recive From Worker Successfully");
            this.dgvRepairItemlist.Rows.Clear();
            this.RefresgRepairngForm();
            this.txtRepairTotal.Text = "";
            this.txtBalance.Text = "";
            bool Bool = rDAL.CheckRepairId(rep.RepairId);
            if (Bool == true)
                rDAL.UpdateRepairIdStatus(rep.RepairId);
            EnableItemInfo();
            DisableRepairCost();
            EnableCalculation();
            frmRepairing frm = new frmRepairing();
            this.Dispose();
            frm.ShowDialog();
        }

        private void btnRepairOut_Click(object sender, EventArgs e)
        {
            if (this.txtRemaining.Text == "")
            {
                MessageBox.Show("Firest Enter Remaining Cash", Messages.Header);
                return;
            }
            else
            {
                if (this.dgvRepairItemlist.Rows.Count <= 0)
                {
                    MessageBox.Show("Please add Item to update", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                rep.CustId = IfEmpty4Int(lblCustId.Text);
                rep.ReceiveDate = Convert.ToDateTime(dtpReceiveDate.Text);
                rep.GivenDate = Convert.ToDateTime(dtpDeliveryDate.Text);
                rep.TotalRepairCost = IfEmpty4Float(txtRepairTotal.Text);
                rep.Advance = IfEmpty4Float(txtAdvance.Text);
                rep.Discount = IfEmpty4Float(txtDiscount.Text);
                rep.Remaining = IfEmpty4Float(txtRemaining.Text);
                rDAL.DeliverToCustomer(rep, con, tran);
                int repId = Convert.ToInt32(txtRepairId.Text);

                #region Repairing Voucher
                ChildAccount cha;
                //voucher entry in sale accoutn;
                pv = new Voucher();
                cha = new ChildAccount();
                cha = adal.GetAccount(4, "Income", "Repairing", con, tran);
                if (cha == null)
                {
                    string Coode = adal.CreateAccount(4, "Income", "Repairing", "General Account", con, tran);
                }
                cha = adal.GetAccount(4, "Income", "Repairing", con, tran);
                pv.AccountCode = cha;
                pv.Cr = Convert.ToDecimal(rep.TotalRepairCost - rep.Discount);
                pv.Dr = 0;
                pv.DDate = (DateTime)rep.ReceiveDate;
                pv.RID = repId;
                pv.VNO = vDAL.CreateVNO("RPV", con, tran);
                pv.Description = "Bill Of R.No." + repId.ToString();
                vDAL.AddVoucher(pv, con, tran);

                //voucher entry in customer account;
                custv = new Voucher();
                custv.RID = repId;
                custv.OrderNo = 0;
                adal = new AccountDAL();
                cha = new ChildAccount();
                cha = adal.GetChildByCode(cust.AccountCode, con, tran);
                custv.AccountCode = cha;
                custv.VNO = pv.VNO;
                custv.Dr = pv.Cr;
                custv.Cr = 0;
                custv.DDate = (DateTime)rep.ReceiveDate;
                custv.Description = "Bill Of R.No." + repId.ToString();
                vDAL.AddVoucher(custv, con, tran);
                #endregion
                #region Cash voucher
                //cash in hand entry
                pv = new Voucher();
                cha = adal.GetAccount(1, "Current Asset", "Cash In Hand", con, tran);
                if (cha == null)
                {
                    cha = new ChildAccount();
                    cha.ChildCode = adal.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, tran);
                    cha = adal.GetAccount(1, "Current Asset", "Cash In Hand", con, tran);
                }
                pv.AccountCode = cha;
                pv.Dr = Convert.ToDecimal(this.txtRemaining.Text);
                pv.Cr = 0;
                pv.DDate = this.dtpReceiveDate.Value;
                pv.RID = Convert.ToInt32(this.txtRepairId.Text);
                pv.SNO = 0;
                pv.VNO = vDAL.CreateVNO("CRV", con, tran);
                pv.Description = "Cash Receive for Repair No" + this.txtRepairId.Text;
                vDAL.AddVoucher(pv, con, tran);

                //Customer account entry
                custv = new Voucher();
                ChildAccount child = new ChildAccount();
                cust = new Customer();
                cust = custDAL.SearchCustById(rep.CustId, con, tran);
                child = adal.GetChildByCode(cust.AccountCode, con, tran);
                custv.AccountCode = child;
                custv.Cr = Convert.ToDecimal(this.txtRemaining.Text);
                custv.Dr = 0;
                custv.DDate = this.dtpReceiveDate.Value;
                custv.RID = Convert.ToInt32(this.txtRepairId.Text);
                custv.SNO = 0;
                custv.VNO = pv.VNO;
                custv.Description = pv.Description;
                vDAL.AddVoucher(custv, con, tran);
                cust.CashBalance -= Convert.ToDecimal(this.txtRemaining.Text);
                #endregion

                MessageBox.Show("Deliver To Customer Successfully", Messages.Header);
                tran.Commit();
                con.Close();
                this.RefresgRepairngForm();
                frmRepairing frm = new frmRepairing();
                this.Dispose();
                frm.ShowDialog();
            }
        }

        private void cbxDeliveredCustomer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnEdit.Enabled = false;
            this.btnReset.Enabled = false;
            this.btnWorkerUpDate.Enabled = false;
            this.btnRepairOut.Enabled = true;
            this.ShowRepair(int.Parse(cbxDeliveredCustomer.Text));
            eFlag = true;
        }

        private void chkReceiveFromWorker_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkReceiveFromWorker.Checked == true)
            {
                this.chkDeleiverToCustomer.Checked = false;
                this.btnSave.Enabled = false;
                this.btnEdit.Enabled = false;
                this.btnRepairOut.Enabled = false;
                this.btnWorkerUpDate.Enabled = true;
                this.cbxRepairId.Enabled = true;
                this.cbxItemId.Enabled = true;
                DisableItemInfo();
                DisableCalculation();
                EnableRepairCost();
            }
            else
            {
                this.btnWorkerUpDate.Enabled = false;
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = true;
                this.cbxRepairId.Enabled = false;
                this.cbxItemId.Enabled = false;
            }
            if (chkDeleiverToCustomer.Checked == false && chkReceiveFromWorker.Checked == false)
            {
                EnableItemInfo();
                DisableRepairCost();
                EnableCalculation();
            }
        }

        private void chkDeleiverToCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkDeleiverToCustomer.Checked == true)
            {
                this.chkReceiveFromWorker.Checked = false;
                this.btnReset.Enabled = true;
                this.btnRepairOut.Enabled = true;
                this.btnEdit.Enabled = false;
                this.btnSave.Enabled = false;
                this.btnWorkerUpDate.Enabled = false;
                this.cbxDeliveredCustomer.Enabled = true;
                con = new JewelConnection();
                try
                {
                    con.MyDataSet.Tables["Table2"].Rows.Clear();
                }
                catch (Exception)
                {
                }
                try
                {
                    string getidquery;
                    getidquery = "select Distinct repairid from tblRepair Where Rstatus= 'Repaired' and Status = 'Not Deliverd'";
                    con.GetDataFromJMDB(getidquery, "Table2");
                    this.cbxDeliveredCustomer.DataSource = con.MyDataSet.Tables["Table2"];
                    this.cbxDeliveredCustomer.DisplayMember = "RepairId";
                }
                catch (Exception ex0)
                {
                    throw ex0;
                }
                DisableItemInfo();
                DisableRepairCost();
                EnableCalculation();
                txtAdvance.Enabled = false;
            }
            else
            {
                this.btnRepairOut.Enabled = false;
                this.btnEdit.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnWorkerUpDate.Enabled = false;
                this.cbxDeliveredCustomer.Enabled = false;
            }
            if (chkDeleiverToCustomer.Checked == false && chkReceiveFromWorker.Checked == false)
            {
                EnableItemInfo();
                DisableRepairCost();
                EnableCalculation();
            }
        }

        private void cbxKarrat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GoldPrice();
        }

        private void txtGoldRate_TextChanged(object sender, EventArgs e)
        {
            GoldPrice();
        }

        private void GoldPrice()
        {
            decimal rWeight, gRate;
            if (this.txtRepairWeight.Text == "")
                rWeight = 0;
            else
                rWeight = Convert.ToDecimal(txtRepairWeight.Text);
            if (txtGoldRate.Text == "")
                gRate = 0;
            else
                gRate = Convert.ToDecimal(txtGoldRate.Text);
            this.txtGoldPrice.Text = (rWeight * gRate).ToString("0");
            SumCost();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.RefresgRepairngForm();
            this.txtPerItemRTotal.Text = "";
            this.txtRepairTotal.Text = "";
            this.txtAdvance.Text = "";
            this.cbxRepairId.Text = "";
            this.cbxItemId.Text = "";
            this.txtRepairId.Text = this.txtitemId.Text;
            this.cbxDeliveredCustomer.Text = "";
            this.chkDeleiverToCustomer.Checked = false;
            this.chkReceiveFromWorker.Checked = false;
            EnableItemInfo();
            DisableRepairCost();
            EnableCalculation();
        }

        private void txtCustomerName_TextChanged(object sender, EventArgs e)
        {
            if (this.txtCustomerName.Text == "")
                this.dgvCustomer.Visible = false;
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculatePrice();
        }

        private void CalculatePrice()
        {
            decimal Discount, Advance, Remaining, Repairtotal, Balance;
            if (this.txtDiscount.Text == "")
                Discount = 0;
            else
                Discount = Convert.ToDecimal(this.txtDiscount.Text);
            if (this.txtAdvance.Text == "")
                Advance = 0;
            else
                Advance = Convert.ToDecimal(this.txtAdvance.Text);
            if (this.txtRemaining.Text == "")
                Remaining = 0;
            else
                Remaining = Convert.ToDecimal(this.txtRemaining.Text);
            if (this.txtRepairTotal.Text == "")
                Repairtotal = 0;
            else
                Repairtotal = Convert.ToDecimal(this.txtRepairTotal.Text);
            if (this.txtBalance.Text == "")
                Balance = 0;
            else
                Balance = Convert.ToDecimal(this.txtBalance.Text);
            this.txtBalance.Text = Math.Round((Repairtotal - (Discount + Advance + Remaining)), 0).ToString("0");
        }

        private void dtpReceiveDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDeliveryDate.Select();
        }

        private void dtpDeliveryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                chkReceiveFromWorker.Select();
            }
        }

        private void cbxRepairId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxItemId.Select();
        }

        private void cbxItemId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRepairWeight.Select();
        }

        private void chkDeleiverToCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkDeleiverToCustomer.Checked == true)
                    cbxDeliveredCustomer.Select();
                else
                    rbtNewCustomer.Select();
            }
        }

        private void cbxDeliveredCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDiscount.Select();
        }

        private void rbtNewCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCustomerName);
        }

        private void rbtExistingCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCustomerName);
        }

        private void cbxWorkerName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxSaleManName);
        }

        private void cbxSaleManName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtBillBookNo);
        }

        private void txtBillBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtItemName);
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtReciveWeight);
        }

        private void txtReciveWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtQty);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDescription);
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDiscount);
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtAdvance);
        }

        private void txtAdvance_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRemaining);
        }

        private void txtRemaining_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddItem);
        }

        private void txtRepairWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxKarrat);
        }

        private void cbxKarrat_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtGoldPrice);
        }

        private void txtGoldPrice_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtLacker);
        }

        private void txtLacker_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMaking);
        }

        private void txtMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRepairCharges);
        }

        private void txtRepairCharges_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddItem);
        }

        private void cbxRepairId_Click(object sender, EventArgs e)
        {
            con = new JewelConnection();
            try
            {
                con.MyDataSet.Tables["Table"].Rows.Clear();
            }
            catch (Exception)
            {
            }
            try
            {
                string getidquery = "select distinct repairid from tblrepairdetail where repairingstatus= 'Reparing'";
                con.GetDataFromJMDB(getidquery, "Table");
                this.cbxRepairId.DataSource = con.MyDataSet.Tables["Table"];
                this.cbxRepairId.DisplayMember = "RepairId";
            }
            catch (Exception ex0)
            {
                throw ex0;
            }
        }

        private void chkReceiveFromWorker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkReceiveFromWorker.Checked == true)
                    cbxRepairId.Select();
                else
                    chkDeleiverToCustomer.Select();
            }
        }

        private void txtGoldPrice_KeyUp(object sender, KeyEventArgs e)
        {
            decimal val, val1;
            val = FormControls.GetDecimalValue(txtRepairWeight, 3);
            val1 = FormControls.GetDecimalValue(txtGoldPrice, 0);
            txtGoldRate.Text = Math.Round((val1 / val), 1).ToString();
            SumCost();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.dgvRepairItemlist.SelectedRows.Count > 0)
            {
                str = this.dgvRepairItemlist.SelectedRows[0].Cells[0].Value.ToString();
                dgvRepairItemlist.Rows.RemoveAt(this.dgvRepairItemlist.SelectedRows[0].Index);
                RepairLineItem rl = new RepairLineItem();
                foreach (RepairLineItem rli in rep.RepairlineItem)
                {
                    if (rli.ItemId == str)
                        rl = rli;
                }
                if (rl != null)
                    rep.RemoveLineItems(rl);
                txtitemId.Text = str;
            }
            else
            {
                MessageBox.Show("Plz select any row to delete", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}