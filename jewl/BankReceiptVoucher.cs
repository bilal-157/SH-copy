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

namespace jewl
{
    public partial class BankReceiptVoucher : Form
    {
        List<ChildAccount> childs;
        List<Voucher> vouch;
        Voucher vouch1;
        ChildAccount child;
        ChildAccount ca = new ChildAccount();
        AccountDAL acDAL = new AccountDAL();
        VouchersDAL vDAL = new VouchersDAL();
        private Voucher pv;
        private Voucher custv;
        string str="";
        int r;
        string st = "";
        decimal h;
        public BankReceiptVoucher()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void BankReceiptVoucher_Load(object sender, EventArgs e)
        {
            this.ShowAllAccounts();
            this.ShowLedger();
            this.txtVoucherNo.Text = vDAL.CreateVNO("BRV");
            DataGridViewRow dr = new DataGridViewRow();
            dgvTransection.Rows.Insert(0, dr);

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void dgvTransection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                r = e.RowIndex;
                if (this.dgvTransection.Rows[e.RowIndex].Cells[1].Selected == true)
                    this.dgvAllAccounts.Visible = true;
                else
                    this.dgvAllAccounts.Visible = false;
            }
        }

        private void ShowAllAccounts()
        {
            childs = acDAL.GetAllAccountsForBank();
            if (childs == null)
                return;
            else
            {
                this.dgvAllAccounts.AutoGenerateColumns = false;
                this.dgvAllAccounts.Rows.Clear();
                int count = childs.Count;
                this.dgvAllAccounts.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvAllAccounts.Rows[i].Cells[0].Value = childs[i].ChildCode.ToString();
                    this.dgvAllAccounts.Rows[i].Cells[1].Value = childs[i].ChildName.ToString();
                }
            }
        }

        private void ShowAllBankAccounts(string name)
        {
            childs = acDAL.GetAllBankAccounts(name);
            if (childs == null)
                return;
            else
            {
                this.dgvBankAccounts.AutoGenerateColumns = false;
                this.dgvBankAccounts.Rows.Clear();
                int count = childs.Count;
                this.dgvBankAccounts.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvBankAccounts.Rows[i].Cells[0].Value = childs[i].ChildCode.ToString();
                    this.dgvBankAccounts.Rows[i].Cells[1].Value = childs[i].ChildName.ToString();
                }
            }
        }

        private void dgvAllAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            str = this.dgvAllAccounts.Rows[e.RowIndex].Cells[0].Value.ToString();
           
            this.ShowAccount(str);
            this.dgvAllAccounts.Visible = false;
        }

        private void ShowAccount(string str)
        {
            child = acDAL.GetChildByCode(str);
            if (child == null)
                return;
            else
            {
                this.dgvTransection.AutoGenerateColumns = false;
                this.txtAccountCode.Text = child.ChildCode;
                this.txtAccountName.Text = child.ChildName;
                    this.dgvTransection.Rows[r].Cells[0].Value = child.ChildCode.ToString();
                    this.dgvTransection.Rows[r].Cells[1].Value = child.ChildName.ToString();                             
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
            for (counter = 0; counter < (dgvTransection.Rows.Count ); counter++)
            {                
                    if (Convert.ToString(dgvTransection.Rows[counter].Cells[2].Value) != string.Empty )
                    {
                        decimal k = decimal.Parse(dgvTransection.Rows[counter].Cells[2].Value.ToString());                       
                        sum += k;
                    }              
            }
            return sum;
        }

        private void dgvTransection_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            this.txtAmount.Text = Math.Round(this.updateSum(), 0).ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ChildAccount cha = new ChildAccount();
            #region Cash voucher
            //if (!(this.txtCashReceive.Text == "" || this.txtCashReceive.Text == "0"))
            //{
            //cash in hand entry
            pv = new Voucher();
            cha = acDAL.GetChildByCode(this.txtAccountCode.Text);
            //cha = this.GetAccount(1, "Current Asset", "Cash In Hand", "Cash In Hand", "Cash In Hand");
            //if (cha == null)
            //{
            //    cha = new ChildAccount();
            //    cha.ChildCode = this.CreatAccount(1, "Current Asset", "Cash In Hand", "Cash In Hand", "Cash In Hand");
            //}
            pv.AccountCode = cha;
            pv.Dr = Convert.ToDecimal(this.txtAmount.Text);
            pv.Cr = 0;
            pv.DDate = this.dtpDate.Value;
            pv.OrderNo = 0;
            pv.SNO = 0;
            pv.VNO = vDAL.CreateVNO("BRV");
            if (this.txtDescription.Text == "")
                pv.Description = "";
            else
                pv.Description = this.txtDescription.Text ;
            vDAL.AddVoucher(pv);
            //Customer account entry
            for (int i=0; i < this.dgvTransection.Rows.Count; i++)
            {
                custv = new Voucher();
                ChildAccount child = new ChildAccount();
                string stri = this.dgvTransection.Rows[i].Cells[0].Value.ToString();
                child = acDAL.GetChildByCode(stri);
                h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                custv.AccountCode = child;
                custv.Cr = h;
                custv.Dr = 0;
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
            MessageBox.Show("Record saved successfully",Messages.Header);
            frmBalanceInvoiceRpt frm = new frmBalanceInvoiceRpt();
            frm.selectQuery = "{VoucherInvoice.VNo}='" + this.txtVoucherNo.Text + "'";
            frm.ShowDialog();
            this.dgvTransection.Rows.Clear();
            this.dgvAllAccounts.Rows.Clear();
            this.txtAccountCode.Text = "";
            this.txtAccountName.Text = "";
            this.txtAmount.Text = "";
            this.txtDescription.Text = "";
            this.txtVoucherNo.Text = vDAL.CreateVNO("BRV");
            this.ShowLedger();
        }
     
        private void ShowLedger()
        {
            vouch = vDAL.GetBankReceiptVoucher();
            if (vouch == null)
                return;
            else
            {
                this.dgvLedger.AutoGenerateColumns = false;
                this.dgvLedger.Rows.Clear();
                int count = vouch.Count;
                this.dgvLedger.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvLedger.Rows[i].Cells[0].Value = vouch[i].DDate.ToString("dd-MMM-yy");

                    this.dgvLedger.Rows[i].Cells[1].Value = vouch[i].VNO.ToString();
                    this.dgvLedger.Rows[i].Cells[2].Value = vouch[i].AccountCode.ChildName.ToString();
                    this.dgvLedger.Rows[i].Cells[3].Value = Math.Round(vouch[i].Dr, 0).ToString();
                    this.dgvLedger.Rows[i].Cells[4].Value = Math.Round(vouch[i].Cr, 0).ToString();
                    this.dgvLedger.Rows[i].Cells[5].Value = vouch[i].Description.ToString();
                    this.dgvLedger.Rows[i].Cells[6].Value = vouch[i].AccountCode.ToString();
                }
            }
        }

        private void txtAccountName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtAccountName.Text))
            {
                this.dgvBankAccounts.Visible = true;
                this.ShowAllBankAccounts(this.txtAccountName.Text);
            }
            else
                this.dgvBankAccounts.Visible = false;
        }

        private void dgvBankAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            str = this.dgvBankAccounts.Rows[e.RowIndex].Cells[0].Value.ToString();
            child = acDAL.GetChildByCode(str);

            this.txtAccountCode.Text = child.ChildCode.ToString();
            this.txtAccountName.Text = child.ChildName.ToString();
            this.dgvBankAccounts.Visible = false;
        }

        private void dgvLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            st = this.dgvLedger.Rows[e.RowIndex].Cells[1].Value.ToString();
            vouch1 = vDAL.GetBankVoucherForUp(st);
            this.txtAccountCode.Text = vouch1.AccountCode.ChildCode.ToString();
            this.txtAccountName.Text = vouch1.AccountCode.ChildName.ToString();
            this.txtAmount.Text = Math.Round(vouch1.Dr, 0).ToString();
            this.txtDescription.Text = vouch1.Description.ToString();
            this.ShowTransaction(st);
            tabLedger.SelectedTab = tabPage1;
            this.btnEdit.Text = "Update";
            this.btnSave.Enabled = false;
        }
        private void ShowTransaction(string stn)
        {
            vouch = vDAL.GetBankVoucherForUpdate(stn);
            
            if (vouch == null)
                return;
            else
            {
                this.dgvTransection.AutoGenerateColumns = false;
                this.dgvTransection.Rows.Clear();
                int count = vouch.Count;
                this.dgvTransection.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dtpDate.Value = Convert.ToDateTime(vouch[i].DDate);
                    this.txtVoucherNo.Text = vouch[i].VNO.ToString();                    
                    this.dgvTransection.Rows[i].Cells[0].Value = vouch[i].AccountCode.ChildCode.ToString();
                    this.dgvTransection.Rows[i].Cells[1].Value = vouch[i].AccountCode.ChildName.ToString();
                    this.dgvTransection.Rows[i].Cells[2].Value = Math.Round(vouch[i].Cr, 0).ToString();
                    this.dgvTransection.Rows[i].Cells[3].Value = vouch[i].Description.ToString();

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
            if (this.btnEdit.Text == "Update")
            {
                ChildAccount cha = new ChildAccount();
                #region Cash voucher
                //if (!(this.txtCashReceive.Text == "" || this.txtCashReceive.Text == "0"))
                //{
                //cash in hand entry
                pv = new Voucher();
                cha = acDAL.GetChildByCode(this.txtAccountCode.Text);
                //if (cha == null)
                //{
                //    cha = new ChildAccount();
                //    cha.ChildCode = this.CreatAccount(1, "Current Asset", "Cash In Hand", "Cash In Hand", "Cash In Hand");
                //}
                pv.AccountCode = cha;
                pv.Dr = Convert.ToDecimal(this.txtAmount.Text);
                pv.Cr = 0;
                pv.DDate = this.dtpDate.Value;
                pv.OrderNo = 0;
                pv.SNO = 0;
                pv.VNO = this.txtVoucherNo.Text;
                if (this.txtDescription.Text == "")
                    pv.Description = "";
                else
                    pv.Description = this.txtDescription.Text;
                vDAL.DeleteVoucher(this.txtVoucherNo.Text);
                vDAL.AddVoucher(pv);
                //vDAL.UpdateVoucher(this.txtVoucherNo.Text, pv);
                //Customer account entry
                for (int i = 0; i < this.dgvTransection.Rows.Count; i++)
                {
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    string stri = this.dgvTransection.Rows[i].Cells[0].Value.ToString();
                    child = acDAL.GetChildByCode(stri);
                    h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                    custv.AccountCode = child;
                    custv.Cr = h;
                    custv.Dr = 0;
                    custv.DDate = this.dtpDate.Value;
                    custv.OrderNo = 0;
                    custv.SNO = 0;
                    custv.VNO = pv.VNO;
                    if (this.dgvTransection.Rows[i].Cells[3].Value == null)
                        custv.Description = "";
                    else
                        custv.Description = this.dgvTransection.Rows[i].Cells[3].Value.ToString();
                    //vDAL.DeleteVoucher(this.txtVoucherNo.Text, pv);
                    //vDAL.UpdateVoucher(pv.VNO,custv);
                    vDAL.AddVoucher(custv);
                }

                #endregion
                MessageBox.Show("Record Update successfully", Messages.Header);
                this.dgvTransection.Rows.Clear();
                this.dgvAllAccounts.Rows.Clear();
                this.txtAccountCode.Text = "";
                this.txtAccountName.Text = "";
                this.txtAmount.Text = "";
                this.txtDescription.Text = "";
                this.txtVoucherNo.Text = vDAL.CreateVNO("BRV");
                this.btnSave.Enabled = true;
                this.ShowLedger();
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
                    MessageBox.Show("Record deleted successfully", Messages.Header);
                    this.dgvTransection.Rows.Clear();
                    this.dgvAllAccounts.Rows.Clear();
                    this.txtAccountCode.Text = "";
                    this.txtAccountName.Text = "";
                    this.txtAmount.Text = "";
                    this.txtDescription.Text = "";
                    this.txtVoucherNo.Text = vDAL.CreateVNO("BRV");
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
                {
                    this.btnSave.Enabled = false;
                }
                else if (a[i] == "Edit")
                {
                    this.btnEdit.Enabled = false;
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            BankReceiptVoucher frm = new BankReceiptVoucher();
            frm.ShowDialog();
        }
    }
}
