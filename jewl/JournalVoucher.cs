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
    public partial class JournalVoucher : Form
    {
        List<Voucher> vouch;
        ChildAccount ca = new ChildAccount();
        AccountDAL acDAL = new AccountDAL();
        VouchersDAL vDAL = new VouchersDAL();
        private Voucher pv;
        int r;
        string st="";
        bool isTrue = false;

        public JournalVoucher()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void JournalVoucher_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(Column1, acDAL.GetAllAccounts(), "ChildName", "ChildCode");
            this.ShowLedger();
            this.txtVoucherNo.Text = vDAL.CreateVNO("JLV");
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

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            this.dgvTransection.Rows.RemoveAt(r);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(this.txtBalance.Text) != 0)
            {
                MessageBox.Show("Debit and Credit entry is not equal ",Messages.Header);
                return;
            }
            ChildAccount cha = new ChildAccount();
            #region Cash voucher
            for (int i = 0; i < this.dgvTransection.Rows.Count-1; i++)
            {
                pv = new Voucher();
                string str = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                cha = acDAL.GetChildByCode(str);
                decimal h = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value);
                decimal j = Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[3].Value);
                pv.AccountCode = cha;
                if (h > 0)
                {
                    pv.Dr = Math.Round(h, 0);
                    pv.Cr = 0;
                }
               if(j > 0)
                {
                    pv.Dr = 0;
                    pv.Cr = Math.Round(j, 0);
                }
                pv.DDate = this.dtpDate.Value;
                pv.OrderNo = 0;
                pv.SNO = 0;
                pv.VNO = this.txtVoucherNo.Text;
                if (this.dgvTransection.Rows[i].Cells[4].Value == null)
                    pv.Description = "";
                else
                    pv.Description = this.dgvTransection.Rows[i].Cells[4].Value.ToString();
                vDAL.AddVoucher(pv);                
            }
            #endregion
            MessageBox.Show(Messages.Saved, Messages.Header);
            this.dgvTransection.Rows.Clear();
            this.txtBalance.Text = "";
            this.txtDebit.Text = "";
            this.txtCredit.Text = "";
            this.txtVoucherNo.Text = vDAL.CreateVNO("JLV");
            this.ShowLedger();
        }

        public void updateDebitSum(out bool val1,out decimal val2)
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvTransection.Rows.Count ); counter++)
            {
                if (Convert.ToString(dgvTransection.Rows[counter].Cells[2].Value) != string.Empty)
                {
                    decimal k;
                    isTrue = true;
                    k = decimal.Parse(dgvTransection.Rows[counter].Cells[2].Value.ToString());
                    sum += Math.Round(k, 0);
                }
            }
            val1 = isTrue;
            val2= Math.Round(sum, 0);
        }

        public void updateCreditSum(out bool val1, out decimal val2)
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvTransection.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvTransection.Rows[counter].Cells[3].Value) != string.Empty)
                {
                    decimal k;
                    isTrue = false;
                    k = decimal.Parse(dgvTransection.Rows[counter].Cells[3].Value.ToString());
                    sum += Math.Round(k, 0);
                }
            }
            val1 = isTrue;
            val2 = Math.Round(sum, 0);
        }

        private void ShowLedger()
        {
            vouch = vDAL.GetJournalVoucher();
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

                }
            }
        }

        private void dgvTransection_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool y;
            decimal a;
            decimal b;
            this.updateDebitSum(out y, out a);
            if (y == true)
            {
                this.txtDebit.Text = a.ToString();
                this.txtBalance.Text = a.ToString();
            }
            this.updateCreditSum(out y, out b);
            if (y == false)
            {
                this.txtCredit.Text = b.ToString();
                if (this.txtCredit.Text == "")
                    b = 0;
                else
                    this.txtBalance.Text = Math.Round((a - b), 0).ToString();
            }
        }

        private void ShowTransaction(string stn)
        {
            vouch = vDAL.GetJournalVoucherForUpdate(stn);
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
                    this.dgvTransection.Rows[i].Cells[1].Value = vouch[i].AccountCode.ChildCode.ToString();
                    this.dgvTransection.Rows[i].Cells[2].Value = Math.Round(vouch[i].Dr, 0).ToString();
                    this.dgvTransection.Rows[i].Cells[3].Value = Math.Round(vouch[i].Cr, 0).ToString();
                    this.dgvTransection.Rows[i].Cells[4].Value = vouch[i].Description.ToString();
                }
            }
        }

        private void dgvLedger_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                st = this.dgvLedger.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.ShowTransaction(st);
                tabLedger.SelectedTab = tabPage1;
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
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
                if (Convert.ToDecimal(this.txtBalance.Text) != 0)
                {
                    MessageBox.Show("Debit and Credit entry is not equal ", Messages.Header);
                    return;
                }
                ChildAccount cha = new ChildAccount();
                #region Cash voucher
                vDAL.DeleteVoucher(this.txtVoucherNo.Text);
                for (int i = 0; i < this.dgvTransection.Rows.Count; i++)
                {
                    pv = new Voucher();
                    string str = this.dgvTransection.Rows[i].Cells[1].Value.ToString();
                    cha = acDAL.GetChildByCode(str);
                    pv.AccountCode = cha;
                    if (Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value) == 0)
                        pv.Dr = 0;
                    else
                    {
                        pv.Dr = Math.Round(Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[2].Value), 0);
                        pv.Cr = 0;
                    }
                    if (Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[3].Value) == 0)
                        pv.Cr = 0;
                    else
                    {
                        pv.Dr = 0;
                        pv.Cr = Math.Round(Convert.ToDecimal(this.dgvTransection.Rows[i].Cells[3].Value), 0);
                    }                                     
                    
                    pv.DDate = this.dtpDate.Value;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = this.txtVoucherNo.Text;
                    if (this.dgvTransection.Rows[i].Cells[4].Value == null)
                        pv.Description = "";
                    else
                        pv.Description = this.dgvTransection.Rows[i].Cells[4].Value.ToString();
                    
                    vDAL.AddVoucher(pv);
                }
                #endregion
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.dgvTransection.Rows.Clear();
                this.txtBalance.Text = "";
                this.txtDebit.Text = "";
                this.txtCredit.Text = "";
                this.txtVoucherNo.Text = vDAL.CreateVNO("JLV");
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
                    MessageBox.Show(Messages.Deleted, Messages.Header);
                    this.dgvTransection.Rows.Clear();
                    this.txtBalance.Text = "";
                    this.txtDebit.Text = "";
                    this.txtCredit.Text = "";
                    this.txtVoucherNo.Text = vDAL.CreateVNO("JLV");
                    this.btnSave.Enabled = true;
                    this.ShowLedger();
                    tabLedger.SelectedTab = tabPage2;
                    this.btnEdit.Text = "&Edit";
                    this.btnSave.Enabled = true;
                    
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
            JournalVoucher frm = new JournalVoucher();
            frm.ShowDialog();
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
    }
}
