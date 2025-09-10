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
    public partial class ManageBankAccount : Form
    {
        BankDAL bDAL = null;
        BankAccount ba = null;
        AccountDAL aDAL = new AccountDAL();
        Banks bnk = new Banks();
        VouchersDAL vDAL = null;
        string accCode;
        int id = 0;
        public ManageBankAccount()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            bDAL = new BankDAL();
            ba = new BankAccount();            
            vDAL = new VouchersDAL();
        }


        private void ManageBankAccount_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else if (str == "Limited")
            {
                this.btnSave.Enabled = true;
            }
            else
            {
                str = ur.GetUserRightsByUser("AddItem");
                if (str != "" && str != null)
                {
                    ur.AssignRights(str, btnSave, btnEdit, btnDelete);
                }
            }
            FormControls.FillCombobox(cbxBankName, bDAL.GetAllBanks(), "BankName", "Id");
            this.ShowDataGrid();
            FormControls.CrudRefresh(this.btnSave, this.btnEdit, this.btnDelete);
            this.cbxBankName.Select();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool nflag = false;
          
            if (this.cbxBankName.Text == "")
            {
                MessageBox.Show("Select First Bank Name", Messages.Header);
                return;
            }
            else if (this.txtAccountNo.Text == "")
            {
                MessageBox.Show("Enter Account No", Messages.Header);
                return;
            }
            //else if (this.txtOpeningBalance.Text == "")
            //{
            //    MessageBox.Show("Enter OpeningBalanc", Messages.Header);
            //    return;                
            //}
            else
            {
                ba = new BankAccount();
                nflag = DateDAL.IsExist("select * from BankAccount where accountno='" + this.txtAccountNo.Text + "'");
                //nflag = ba.IsBankNameExist(bnk.BankName);
                if (nflag == true)
                {
                    MessageBox.Show("Account No Already Exist", Messages.Header);
                    return;
                }
                ba.Id = bDAL.GetMaxAccountCode() + 1;
                ba.AccountNo = this.txtAccountNo.Text;
                ba.BankName = (Banks)this.cbxBankName.SelectedItem;
                ba.OpeningBalace = Convert.ToDecimal(FormControls.StringFormate(this.txtOpeningBalance.Text));
                ba.AccountCode = aDAL.GetAccount(1, ba.BankName.BankName, ba.AccountNo.ToString());
                if (ba.AccountCode==null)
                {
                    ba.AccountCode = new ChildAccount();
                    ba.AccountCode.ChildCode = aDAL.CreateAccount(1, ba.BankName.BankName, ba.AccountNo.ToString(), "Bank Account", ba.OpeningBalace);
                }
                //string str=(string )this .txtOpeningBalance .Text ;
                //if(string .IsNullOrEmpty(str))
                //    ba.OpeningBalace =0;
                //else 
               
                // ba.OpeningBalace = ba.AccountCode.Balance;
                bDAL.AddBankAccount(ba);
                MessageBox.Show(Messages.Saved, Messages.Header);
                this.ShowDataGrid();
                this.RefreshRecord();
            }
        }

        private void ShowDataGrid()
        {
            List<BankAccount> bnac = null ;
            bnac = bDAL.GetAllBankAccount();
            if (bnac == null)
                return;
            else 
            {
                this.dgvExistingAccountDetail.AutoGenerateColumns = false;
                int count =bnac.Count;
                this.dgvExistingAccountDetail.Rows.Clear();
                this.dgvExistingAccountDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvExistingAccountDetail.Rows[i].Cells[0].Value = bnac[i].BankName.BankName.ToString();
                    this.dgvExistingAccountDetail.Rows[i].Cells[1].Value = bnac[i].AccountNo.ToString();
                    this.dgvExistingAccountDetail.Rows[i].Cells[2].Value =Convert .ToInt32 (bnac[i].Id.ToString ());
                }
            }
        }
        private void RefreshRecord()
        {
            this.txtAccountNo.Text = "";
            this.txtOpeningBalance.Text = "";
            this.cbxBankName.SelectedIndex = -1;
            FormControls.CrudRefresh(this.btnSave, this.btnEdit, this.btnDelete);
                this.cbxBankName.Select();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                MessageBox.Show("Please select record to Edit", "Jewl", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (this.btnEdit.Text == "&Update")
            {
                ba = new BankAccount();
                ChildAccount cha = new ChildAccount();
              
                cha .ChildCode =this .txtAccountCode .Text ;
                ba.AccountNo = this.txtAccountNo.Text;
                ba.AccountCode = cha;
                ba.BankName = (Banks)this.cbxBankName.SelectedItem;
                string str = ba.BankName.BankName.ToString();
                cha.ChildName = str;
                str = "";
                str = (string)this.txtOpeningBalance.Text;
                if (string.IsNullOrEmpty(str))
                    ba.OpeningBalace = 0;
                else 
                ba.OpeningBalace = Convert .ToDecimal (this.txtOpeningBalance.Text);
                cha.Balance = ba.OpeningBalace;
                cha.OpCash = ba.OpeningBalace;
                cha.Status = "Dr";
                cha.AccountType = "Bank Account";
                bDAL.UpdateBankAccount(id, ba);
                aDAL.UpdateChild(cha.ChildCode, cha);
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.btnDelete.Enabled = false;
                this.btnExit.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "Edit";
                this.btnEdit.Enabled = false;
                this.RefreshRecord();
                this.ShowDataGrid(); 
            }
        }
        private void SearchRecord(int id)
        {
            ba = bDAL.SearchBankAccount(id);
            if (ba == null)
                return;
            else 
            {
                this.txtAccountNo.Text  = ba.AccountNo;
                this.txtAccountCode.Text  = ba.AccountCode.ChildCode;
                accCode = this.txtAccountCode.Text;
                for (int i = 0; i < this.cbxBankName.Items.Count; i++)
                {
                    Banks bnk = (Banks)this.cbxBankName.Items[i];
                    if (ba.BankName.Id == bnk.Id)
                    {
                        this.cbxBankName.SelectedIndex = i;
                        break;
                    }
                }
                ChildAccount ch = aDAL.GetChildByCode(this.txtAccountCode.Text);
                this.txtOpeningBalance.Text = ch.OpCash.ToString();
                this.btnEdit.Text = "Update";
                this.btnExit.Enabled = true ;
                this.btnSave.Enabled = false;
                this.btnDelete.Enabled = true;
            }
        }

        private void dgvExistingAccountDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                if (this.dgvExistingAccountDetail.Rows[e.RowIndex].Cells[2].Value == null)
                {
                    return;
                }
                else
                {
                    id = Convert.ToInt32(this.dgvExistingAccountDetail.Rows[e.RowIndex].Cells[2].Value.ToString());
                    //accCode =this.dgvExistingAccountDetail.Rows[e.RowIndex].Cells[1].Value.ToString();
                    this.SearchRecord(id);
                    FormControls.CrudActive(this.btnSave, this.btnEdit, this.btnDelete);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvExistingAccountDetail_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                    this.btnEdit .Enabled = false;
                }
                //else if (a(I) == "Delete")
                //{
                //    frm.cmdDelete.Enabled = true;
                //}
                //else if (a(I) == "Print")
                //{
                //    frm.CmdPrint.Enabled = true;
                //    frm.CmdReport.Enabled = true;
                //}
                //else if (a(I) == "Estimate")
                //{
                //    frm.CmdEstimated.Enabled = true;
                //}
                //else if (a(I) == "Sample")
                //{
                //    frm.CmdSample.Enabled = true;
                //}
                //else if (a(I) == "Damage")
                //{
                //    frm.CmdDamage.Enabled = true;
                //}

            }


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool nFlag = false;
            if (accCode == "")
            {
                MessageBox.Show("", "");
            }
            else
            {
                nFlag = DateDAL.IsExist("select VNO from Vouchers where AccountCode ='" + accCode + "'");

                if (nFlag == true)
                {
                    MessageBox.Show("this is already in use", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    aDAL.DeleteBankAccount (this.txtAccountCode.Text);
                    aDAL.DeleteChild(this.txtAccountCode.Text);
                    //string voucherNo = vDAL.GetVoucherGeneral("select vno from Vouchers where vno like 'OBV%' and Description Like 'Opening Balance' and AccountCode='" + this.txtAccountCode.Text+"'");
                    //vDAL.DeleteVoucher(voucherNo);

                    MessageBox.Show(Messages.Deleted, Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RefreshRecord();
                    this.dgvExistingAccountDetail .Rows.Clear();
                    this.ShowDataGrid();

                }
            }
        }

        private void txtAccountNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
                
        }

        private void txtOpeningBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
                
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtOpeningBalance.Text = "";
            this.txtAccountNo.Text = "";
            this.txtAccountCode.Text = "";
            FormControls.CrudRefresh(this.btnSave, this.btnEdit, this.btnDelete);
        }

        private void AddBankAccount_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void cbxBankName_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void txtAccountNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtOpeningBalance);
        }

        private void txtOpeningBalance_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void cbxBankName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtAccountNo);
        }
    }
}
