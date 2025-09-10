using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class ManageSalesMan : Form
    {
        SaleMan slm;
        List<SaleMan> slms;
        SaleManDAL slmDAL = new SaleManDAL();
        AccountDAL acDAL = new AccountDAL();
        int slmId = 0;
        string accountCode = "";
        public ManageSalesMan()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool cflag = false;
            bool nflag = false;

            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                slm = new SaleMan();
                slm.Name = this.txtName.Text.ToString();
                slm.Address = this.txtAddress.Text.ToString();
                slm.ContactNo = this.txtContactNo.Text.ToString();
                slm.CNIC = this.txtCNIC.Text.ToString();
                slm.DateOfBirth = this.dtpDate.Value;
                slm.AccountCode = new ChildAccount();
                slm.AccountCode.ChildCode = acDAL.CreateAccount(2, "SalesMan", slm.Name, "SalesMan",0);

                slm.Salary = FormControls.GetDecimalValue(this.txtSalary, 0);

                nflag = slmDAL.IsNameExist(slm.Name);
                if (nflag == true)
                {
                    MessageBox.Show("SaleMan Name Already Exist", Messages.Header);
                    return;
                }
                cflag = slmDAL.IsContacNoExist(slm.ContactNo);
                if (cflag == true)
                {
                    MessageBox.Show("Contact No Already Exist", Messages.Header);
                    return;
                }
                else
                {
                    slmDAL.AddSaleMan(slm);
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.RefreshRecord();
                    this.ShowDGVRecord();
                }
            }
        }

        private void RefreshRecord()
        {
            this.txtName.Text = "";
            this.txtContactNo.Text = "";
            this.txtAddress.Text = "";
            this.txtSalary.Text = "";
            this.txtCNIC.Text = "";
            FormControls.CrudRefresh(this.btnSave, this.btnEdit, this.btnDelete);
        }
        private void ShowDGVRecord()
        {
            this.dgvSaleManDetail.Rows.Clear();
            slms = slmDAL.GetAllSaleMen();
            if (slms == null)
                return;
            else
            {
                this.dgvSaleManDetail.AutoGenerateColumns = false;
                this.dgvSaleManDetail.Rows.Clear();
                int count = slms.Count;
                this.dgvSaleManDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvSaleManDetail.Rows[i].Cells[0].Value = slms[i].ID.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[1].Value = slms[i].Name.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[2].Value = slms[i].CNIC.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[3].Value = slms[i].ContactNo.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[4].Value = slms[i].Address.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[5].Value = slms[i].Salary.ToString();
                    this.dgvSaleManDetail.Rows[i].Cells[6].Value = slms[i].AccountCode.ChildCode.ToString();
                }
            }
        }

        private void ManageSalesMan_Load(object sender, EventArgs e)
        {
            this.ShowDGVRecord();
            this.txtName.Select();

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str = this.txtName.Text;
            this.txtName.Text = txtInfo.ToTitleCase(str);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Update")
            {
                if (txtName.Text == "")
                {
                    MessageBox.Show("Please Enter Sale Man Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    slm = new SaleMan();

                    slm.Name = this.txtName.Text.ToString();
                    slm.Address = this.txtAddress.Text.ToString();
                    slm.ContactNo = this.txtContactNo.Text.ToString();
                    slm.CNIC = this.txtCNIC.Text.ToString();
                    //slm.Status = "Available";
                    //slm.AccountCode = this.CreatAccount(2, "Current Liability", "SaleMan", "SaleMan", slm.Name);
                    if (this.txtSalary.Text == "")
                        slm.Salary = 0;
                    else
                        slm.Salary = FormControls.GetDecimalValue(this.txtSalary, 0);
                    slmDAL.UpdateSaleMan(slmId,slm);
                    MessageBox.Show(Messages.Updated, Messages.Header);
                    this.btnSave.Enabled = true;
                    this.btnDelete.Enabled = true;
                    this.RefreshRecord();
                    this.ShowDGVRecord();
                }
            }
        }

        private void dgvSaleManDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                else
                {
                    string str = this.dgvSaleManDetail.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (!(str == null || str == ""))
                    {
                        slmId = Convert.ToInt32(this.dgvSaleManDetail.Rows[e.RowIndex].Cells[0].Value.ToString());
                        accountCode = this.dgvSaleManDetail.Rows[e.RowIndex].Cells[6].Value.ToString();
                        this.ShowRecord(slmId);
                        this.btnEdit.Text = "&Update";
                        this.btnEdit.Enabled = true;
                        this.btnSave.Enabled = false;
                        this.btnDelete.Enabled = true;
                    }
                }
            }
            catch { }
        }
        private void ShowRecord(int id)
        {
            slm = slmDAL.GetSaleManById(id);
            if (slm == null)
                return;
            else
            {
                this.txtName.Text = slm.Name.ToString();
                this.txtContactNo.Text = slm.ContactNo.ToString();
                this.txtCNIC.Text = slm.CNIC.ToString();
                this.dtpDate.Value = slm.DateOfBirth.Value;
                this.txtAddress.Text = slm.Address.ToString();
                if (slm.Salary == 0)
                    this.txtSalary.Text = string.Empty;
                else
                    this.txtSalary.Text = slm.Salary.ToString();
                this.txtAccountCode.Text = slm.AccountCode.ChildCode.ToString();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (slmId != 0)
            {
                slmDAL.DeleteSaleMan(slmId,accountCode);
                MessageBox.Show(Messages.Deleted, Messages.Header);
                this.RefreshRecord();

                this.ShowDGVRecord();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void AddSaleMan_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void lblSaleMan_Click(object sender, EventArgs e)
        {

        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            FormControls.validate_box(sender as TextBox, e, errorProvider1, "Customer Name");
        }

        private void txtContactNo_Validating(object sender, CancelEventArgs e)
        {
            FormControls.validate_box(sender as TextBox, e, errorProvider1, "Phone No");
        }

        private void txtAddress_Validating(object sender, CancelEventArgs e)
        {
            FormControls.validate_box(sender as TextBox, e, errorProvider1, "Address");
            if (this.txtName.Text==String.Empty)
            {
                this.txtAddress.CausesValidation = false;
                this.txtName.Select();
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtContactNo);
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCNIC);
        }

        private void txtCNIC_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtAddress);
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtSalary);
        }

        private void txtSalary_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtName);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            FormControls.CrudRefresh(btnSave, btnEdit, btnDelete);
            IEnumerable<Control> list =  FormControls.GetAll(this, typeof(TextBox));
            FormControls.Refresh(list);
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
    }
}
