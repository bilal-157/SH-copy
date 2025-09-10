using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class CashPaymentVoucher : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        List<Voucher> vouch, vouchgld, vhs;
        ChildAccount child;
        ChildAccount ca = new ChildAccount();
        AccountDAL acDAL = new AccountDAL();
        VouchersDAL vDAL = new VouchersDAL();
        DataGridViewRow dr = new DataGridViewRow();
        private Voucher pv, custv;
        string str = "", st = "";
        int r, g;
        decimal h;

        public CashPaymentVoucher()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void CashPaymentVoucher_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(Column1, acDAL.GetAllAccounts(), "ChildName", "ChildCode");
            this.ShowLedger();
            this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            int rowCount = this.dgvTransection.RowCount;
            DataGridViewRow dr = new DataGridViewRow();
            dgvTransection.Rows.Insert(rowCount, dr);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            this.dgvTransection.Rows.RemoveAt(r);
        }

        public decimal updateSum()
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvTransection.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvTransection.Rows[counter].Cells[2].Value) != string.Empty)
                {
                    decimal k;
                    k = decimal.Parse(dgvTransection.Rows[counter].Cells[2].Value.ToString());
                    sum += k;
                }
            }
            return sum;
        }

        private void dgvTransection_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.txtAmount.Text = Math.Round(this.updateSum(), 0).ToString();
            string stri = this.dgvTransection.Rows[0].Cells[1].Value.ToString();
            this.lblGoldBalance.Text = Math.Round(vDAL.GetGoldBalanceByAccCode(stri), 0).ToString("0.000");
            this.lblCashbalance.Text = Math.Round(vDAL.GetCashBalanceByAccCode(stri), 0).ToString("0");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            try
            {
                ChildAccount cha = new ChildAccount();
                dgvTransection.EndEdit();
                if (rbtCash.Checked == true)
                {
                    #region Cash voucher
                    for (int i = 0; i < this.dgvTransection.Rows.Count - 1; i++)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string Code = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = h;
                        pv.DDate = this.dtpDate.Value;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        pv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        child = new ChildAccount();
                        string stri = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                        child = acDAL.GetChildByCode(stri, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = 0;
                        custv.Dr = h;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = 0;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.AddVoucher(custv, con, trans);
                    }
                    #endregion
                }
                if (rbtGold.Checked == true)
                {
                    #region Gold voucher
                    for (int i = 0; i < this.dgvTransection.Rows.Count - 1; i++)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string Code = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                        pv.AccountCode = cha;
                        pv.GoldDr = 0;
                        pv.GoldCr = h;
                        pv.DDate = this.dtpDate.Value;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPAV", con, trans);
                        pv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        child = new ChildAccount();
                        string stri = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                        child = acDAL.GetChildByCode(stri, con, trans);
                        custv.AccountCode = child;
                        custv.GoldCr = 0;
                        custv.GoldDr = h;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = 0;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.AddVoucher(custv, con, trans);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con.Close();
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    frmBalanceInvoiceRpt frm = new frmBalanceInvoiceRpt();
                    frm.selectQuery = "{VoucherInvoice.VNo}='" + this.txtVoucherNo.Text + "'";
                    frm.ShowDialog();
                    this.dgvTransection.Rows.Clear();
                    this.txtAmount.Text = "";
                    if (rbtCash.Checked == true)
                    {
                        this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
                        this.ShowLedger();
                    }
                    if (rbtGold.Checked == true)
                    {
                        this.txtVoucherNo.Text = vDAL.CreateVNO("GPV");
                        this.ShowLedgerGold();
                    }
                }
            }
        }

        private void ShowLedger()
        {
            vouch = new List<Voucher>();
            vouch = vDAL.GetPaymentVoucher(this.dtpDate.Value);
            if (vouch == null)
            {
                this.dgvLedger.Rows.Clear();
                return;
            }
            else
            {
                this.dgvLedger.AutoGenerateColumns = false;
                this.dgvLedger.Rows.Clear();
                int count = vouch.Count;
                this.dgvLedger.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    if (vouch[i].VNO.Contains("CPV"))
                    {
                        this.dgvLedger.Rows[i].Cells[0].Value = vouch[i].DDate.ToString("dd-MMM-yy");
                        this.dgvLedger.Rows[i].Cells[1].Value = vouch[i].VNO.ToString();
                        this.dgvLedger.Rows[i].Cells[2].Value = vouch[i].AccountCode.ChildName.ToString();
                        if (rbtCash.Checked == true)
                        {
                            this.dgvLedger.Rows[i].Cells[3].Value = Math.Round(vouch[i].Dr, 0).ToString();
                            this.dgvLedger.Rows[i].Cells[4].Value = Math.Round(vouch[i].Cr, 0).ToString();
                        }
                        this.dgvLedger.Rows[i].Cells[5].Value = vouch[i].Description.ToString();
                    }
                }
            }
        }

        private void ShowLedgerGold()
        {
            vouchgld = new List<Voucher>();
            vouchgld = vDAL.GetPaymentVoucherGold();
            if (vouchgld == null)
            {
                this.dgvLedger.Rows.Clear();
                return;
            }
            else
            {
                this.dgvLedger.AutoGenerateColumns = false;
                this.dgvLedger.Rows.Clear();
                int count = vouchgld.Count;
                this.dgvLedger.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvLedger.Rows[i].Cells[0].Value = vouchgld[i].DDate.ToString("dd-MMM-yy");
                    this.dgvLedger.Rows[i].Cells[1].Value = vouchgld[i].VNO.ToString();
                    this.dgvLedger.Rows[i].Cells[2].Value = vouchgld[i].AccountCode.ChildName.ToString();
                    this.dgvLedger.Rows[i].Cells[3].Value = Math.Round(vouchgld[i].GoldDr, 0).ToString();
                    this.dgvLedger.Rows[i].Cells[4].Value = Math.Round(vouchgld[i].GoldCr, 0).ToString();
                    this.dgvLedger.Rows[i].Cells[5].Value = vouchgld[i].Description.ToString();
                }
            }
        }

        private void dgvLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            st = this.dgvLedger.Rows[e.RowIndex].Cells[1].Value.ToString();
            if (rbtCash.Checked == true)
                this.ShowTransaction("select * from Vouchers where VNo='" + st + "' and AccountName not like 'Cash In%'");
            if (rbtGold.Checked == true)
                this.ShowTransaction("select * from Vouchers where VNo='" + st + "' and AccountName not like 'Gold In%'");
            tabLedger.SelectedTab = tabPage1;
            this.btnEdit.Text = "&Update";
            this.btnSave.Enabled = false;
        }

        private void ShowTransaction(string stn)
        {
            vhs = new List<Voucher>();
            vhs = vDAL.GetPaymentVoucherForUpdate(stn);
            if (vhs == null)
            {
                this.dgvTransection.Rows.Clear();
                return;
            }
            else
            {
                this.dgvTransection.AutoGenerateColumns = false;
                this.dgvTransection.Rows.Clear();
                int count = vhs.Count;
                this.dgvTransection.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dtpDate.Value = Convert.ToDateTime(vhs[i].DDate);
                    this.txtVoucherNo.Text = vhs[i].VNO.ToString();
                    this.dgvTransection.Rows[i].Cells[0].Value = vhs[i].AccountCode.ChildCode.ToString();
                    this.dgvTransection.Rows[i].Cells[1].Value = vhs[i].AccountCode.ChildCode.ToString();
                    if (rbtCash.Checked == true)
                        this.dgvTransection.Rows[i].Cells[2].Value = Math.Round(vhs[i].Dr, 0).ToString();
                    if (rbtGold.Checked == true)
                        this.dgvTransection.Rows[i].Cells[2].Value = Math.Round(vhs[i].GoldDr, 0).ToString();
                    this.dgvTransection.Rows[i].Cells[3].Value = vhs[i].Description.ToString();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                MessageBox.Show("Please select the transection to edit", Messages.Header);
                tabLedger.SelectedTab = tabPage2;
            }
            if (this.btnEdit.Text == "&Update")
            {
                ChildAccount cha = new ChildAccount();
                dgvTransection.EndEdit();
                if (rbtCash.Checked == true)
                {
                    #region Cash voucher
                    for (int i = 0; i < this.dgvTransection.Rows.Count - 1; i++)
                    {
                        h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand");
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = h;
                        pv.DDate = this.dtpDate.Value;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = this.txtVoucherNo.Text;
                        pv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.DeleteVoucher(this.txtVoucherNo.Text);
                        vDAL.AddVoucher(pv);

                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        string stri = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                        child = acDAL.GetChildByCode(stri);
                        custv.AccountCode = child;
                        custv.Cr = 0;
                        custv.Dr = h;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = 0;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        if (this.dgvTransection.Rows[i].Cells[3].Value == null)
                            custv.Description = "";
                        else
                            custv.Description = this.dgvTransection.Rows[i].Cells[3].Value.ToString();
                        vDAL.AddVoucher(custv);
                    }
                    #endregion
                }
                if (rbtGold.Checked == true)
                {
                    #region Gold voucher
                    for (int i = 0; i < this.dgvTransection.Rows.Count - 1; i++)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand");
                        h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                        pv.AccountCode = cha;
                        pv.GoldDr = 0;
                        pv.GoldCr = h;
                        pv.DDate = this.dtpDate.Value;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = this.txtVoucherNo.Text;
                        pv.Description = this.dgvTransection.Rows[i].Cells[3].FormattedValue.ToString();
                        vDAL.DeleteVoucher(this.txtVoucherNo.Text);
                        vDAL.AddVoucher(pv);

                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        string stri = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                        child = acDAL.GetChildByCode(stri);
                        custv.AccountCode = child;
                        custv.GoldCr = 0;
                        custv.GoldDr = h;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = 0;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        if (this.dgvTransection.Rows[i].Cells[3].Value == null)
                            custv.Description = "";
                        else
                            custv.Description = this.dgvTransection.Rows[i].Cells[3].Value.ToString();
                        vDAL.AddVoucher(custv);
                    }
                    #endregion
                }
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.dgvTransection.Rows.Clear();
                this.txtAmount.Text = "";
                if (rbtCash.Checked == true)
                {
                    this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
                    this.ShowLedger();
                }
                if (rbtGold.Checked == true)
                {
                    this.txtVoucherNo.Text = vDAL.CreateVNO("GPV");
                    this.ShowLedgerGold();
                }
                this.btnSave.Enabled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (st == "")
            {
                MessageBox.Show("Please select the record to delete", Messages.Header);
                tabLedger.SelectedTab = tabPage2;
            }
            if (st != "")
            {
                if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    vDAL.DeleteVoucher(st);
                    MessageBox.Show(Messages.Deleted, Messages.Header);
                    this.dgvTransection.Rows.Clear();
                    this.dgvLedger.Rows.Clear();
                    this.txtAmount.Text = "";
                    this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
                    this.btnSave.Enabled = true;
                    this.ShowLedger();
                    this.btnEdit.Text = "&Edit";
                }
            }
        }

        public void AssignRights(Form frm, string frmRights)
        {
            string[] a;
            a = frmRights.Split(' ');
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                    this.btnSave.Enabled = false;
                else if (a[i] == "Edit")
                    this.btnEdit.Enabled = false;
            }
        }

        private void dgvTransection_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvTransection.CurrentCell.ColumnIndex == 0 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            }
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataGridViewComboBoxEditingControl dataGridViewComboBoxEditingControl = sender as DataGridViewComboBoxEditingControl;
            object value = dataGridViewComboBoxEditingControl.SelectedValue;
            if (value != null)
            {
                ChildAccount it = (ChildAccount)dataGridViewComboBoxEditingControl.SelectedItem;
                this.dgvTransection.CurrentRow.Cells[1].Value = it.ChildCode;
            }
        }

        private void rbtGold_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtGold.Checked == true)
            {
                this.dgvTransection.Rows.Clear();
                this.ShowLedgerGold();
                this.txtVoucherNo.Text = vDAL.CreateVNO("GPAV");
                dgvTransection.Rows.Insert(0, dr);
            }
            else
            {
                this.dgvTransection.Rows.Clear();
                this.ShowLedger();
                this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
            }
        }

        private void rbtCash_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtCash.Checked == true)
            {
                this.dgvTransection.Rows.Clear();
                this.ShowLedger();
                this.txtVoucherNo.Text = vDAL.CreateVNO("CPV");
                dgvTransection.Rows.Insert(0, dr);
            }
            else
            {
                this.dgvTransection.Rows.Clear();
                this.ShowLedgerGold();
                this.txtVoucherNo.Text = vDAL.CreateVNO("GPV");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CashPaymentVoucher frm = new CashPaymentVoucher();
            frm.ShowDialog();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            this.ShowLedger();
        }
    }
}
