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


namespace jewl
{
    public partial class ManageBank : Form
    {
        Banks bnk;
        AccountDAL aDAL = new AccountDAL();
        BankDAL bDAL = new BankDAL();
        List<Banks> bnks = null;
        CultureInfo Info = CultureInfo.CurrentCulture;
        int id = 0;

        public ManageBank()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool nflag = false;
            if (this.txtName.Text == "")
            {
                MessageBox.Show("First Enter Name", Messages.Header);
                this.txtName.Select();
            }
            else if (this.txtDeductionRate.Text == "")
            {
                MessageBox.Show("First Enter Deduct Rate", Messages.Header);
                this.txtDeductionRate.Select();
            }
            else
            {

                bnk = new Banks();
                TextInfo txtInfo = Info.TextInfo;
                bnk.Id = bDAL.GetMaxCode() + 1;
                bnk.BankName = txtInfo.ToTitleCase(this.txtName.Text);

                nflag = DateDAL.IsExist("select bankname from bank where bankname='" + bnk.BankName + "'");
                if (nflag == true)
                {
                    MessageBox.Show("Bank Name Already Exist", Messages.Header);
                    return;
                }

                bnk.DRate = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtDeductionRate, 2));
                bnk.ParentCode = this.CreateParentAccount(1, bnk.BankName);
                bDAL.AddBank(bnk);
                MessageBox.Show(Messages.Saved, Messages.Header);

                this.RefreshRecord();
                this.ShowDataGrid();
                this.txtName.Select();
            }
        }

        private ParentAccount CreateParentAccount(int hcode, string pname)
        {           
            ParentAccount p;
            p = aDAL.GetParent(pname,hcode.ToString());
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = hcode;
                p.ParentName = pname;
                p.ParentCode = aDAL.CreateParentCode(p.HeadCode);
                aDAL.CreateParentAccount(p);
            }
            return p;
        }


        private void RefreshRecord()
        {
            this.txtName.Text = "";
            this.txtDeductionRate.Text = "";
            FormControls.CrudRefresh(this.btnSave, this.btnEdit, this.btnDelete);
        }
        private void ShowDataGrid()
        {
            bnks = bDAL.GetAllBanks();
            if (bnks == null)
                return;
            else
            {
                this.dgvExistingBankDetail.AutoGenerateColumns = false;
                int count = bnks.Count;
                this.dgvExistingBankDetail.Rows.Clear();
                this.dgvExistingBankDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvExistingBankDetail.Rows[i].Cells[0].Value = bnks[i].BankName.ToString();
                    this.dgvExistingBankDetail.Rows[i].Cells[1].Value = Convert.ToString(bnks[i].DRate.ToString());
                    this.dgvExistingBankDetail.Rows[i].Cells[2].Value = Convert.ToInt32(bnks[i].Id);
                }
            }

        }

        private void ManageBank_Load(object sender, EventArgs e)
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
            this.ShowDataGrid();
            this.RefreshRecord();
            this.txtName.Select();

        }

        private void dgvExistingBankDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                if (this.dgvExistingBankDetail.Rows[e.RowIndex].Cells[2].Value == null)
                    return;
                else
                {
                    id = Convert.ToInt32(this.dgvExistingBankDetail.Rows[e.RowIndex].Cells[2].Value.ToString());
                    this.searchRecord(id);
                    FormControls.CrudActive(this.btnSave, this.btnEdit, this.btnDelete);
                }
            }
        }
        private void searchRecord(int id)
        {
            Banks bn = null;
            bn = bDAL.SearchBank(id);
            if (bn == null)
                return;
            else
            {
                this.txtPCode.Text = bn.ParentCode.ParentCode;
                this.txtName.Text = bn.BankName.ToString();
                this.txtDeductionRate.Text = bn.DRate.ToString();

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.txtDeductionRate.Text == "")
            {
                MessageBox.Show("Enter Deduct Rate", Messages.Header);
                return;
            }
            if (this.txtName.Text == "")
            {
                MessageBox.Show("Enter Bank Name ", Messages.Header);
                return;
            }
            if (this.btnEdit.Text == "Edit")
            {
                if (this.txtName.Text == "" || this.id == 0)
                    return;
            }
            else
            {
                Banks bnk = new Banks();
                TextInfo txtInfo = Info.TextInfo;
                bnk.BankName = txtInfo.ToTitleCase(this.txtName.Text);
                bnk.DRate = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtDeductionRate, 2));
                bDAL.UpdateBank(id, bnk);
                aDAL.UpdateParent(this.txtPCode.Text, bnk.BankName.ToString());
                MessageBox.Show(Messages.Updated, Messages.Header);
                //this.btnDelete.Enabled = true;
                this.btnExit.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "Edit";
                this.RefreshRecord();
                this.ShowDataGrid();

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(this.txtPCode.Text)))
            {
                if (DateDAL.IsExist("select VNO from Vouchers where ParentCode='" + this.txtPCode.Text + "'"))
                {
                    MessageBox.Show("You Can not Delete This Record", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else if (DateDAL.IsExist("select AccountNo from BankAccount where BankId=" + id + ""))
                {
                    MessageBox.Show("You Can not Delete This Record, Please First Delete Account No Against this Bank", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (MessageBox.Show("Are you sure to delete this record:Press Ok To Continue", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        aDAL.DeleteParent(this.txtPCode.Text);
                        MessageBox.Show(Messages.Deleted, Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.RefreshRecord();
                        this.dgvExistingBankDetail.Rows.Clear();
                        this.ShowDataGrid();
                    }
                }
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
                e.Handled = true;
            else
                e.Handled = false;

            //if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            //{
            //    e.Handled = true;

            //}
            //if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
            //{
            //    e.Handled = true;
            //}
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str1 = this.txtName.Text.ToString();
            txtName.Text = txtInfo.ToTitleCase(str1);
        }

        private void txtDeductionRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46)
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDeductionRate);
        }



        private void txtDeductionRate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void AddBank_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void lblDedcutionRate_Click(object sender, EventArgs e)
        {

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtName.Text = "";
            this.txtDeductionRate.Text = "";
        }

        private void lblNimi_Click(object sender, EventArgs e)
        {

        }
    }
}
