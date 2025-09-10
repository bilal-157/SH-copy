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
    public partial class AddSupplier : Form
    {
        Supplier supplier = new Supplier();
        List<Phone> contactList=new List<Phone>();
        List<Supplier> slms = new List<Supplier>();
        public AddSupplier()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void AddSupplier_Load(object sender, EventArgs e)
        {

            FormControls.CrudRefresh(btnSave, btnEdit, btnDelete);
            this.txtSupplierCode.Select();
            this.ShowDGVRecord();
          
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }

        }

        private void ShowDGVRecord()
        {
            slms = new SupplierDAL().GetAllSuppliers();
            if (slms == null)
                return;
            else
            {
                this.dgvSupplierDetail.AutoGenerateColumns = false;
                this.dgvSupplierDetail.Rows.Clear();
                int count = slms.Count;
                this.dgvSupplierDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvSupplierDetail.Rows[i].Cells[0].Value = slms[i].PCode.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[1].Value = slms[i].PAbri.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[2].Value = slms[i].PName.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[3].Value = slms[i].PtclNo1.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[4].Value = slms[i].PAddress.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[5].Value = slms[i].PWastage.ToString();
                    this.dgvSupplierDetail.Rows[i].Cells[6].Value = slms[i].PDiscount.ToString();
                    
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                bool exist = new SupplierDAL().isExist("select PAbri from Supplier where PAbri ='" + this.txtSupplierCode.Text + "'");
                if (exist == true)
                {
                    MessageBox.Show( this.txtSupplierCode.Text + " is already exist", Messages.Header);
                    this.txtSupplierCode.Focus();
                    return;
                }
                else
                {

                    supplier.PAbri = this.txtSupplierCode.Text;
                    supplier.PName = this.txtName.Text;
                    supplier.PtclNo1 = this.txtContactNo.Text;
                    supplier.PAddress = this.txtAddress.Text;
                    supplier.PDate = DateTime.Now;
                    supplier.PWastage = this.txtWastage.Text;
                    supplier.PMaking = this.txtMaking.Text;
                    supplier.PDiscount = this.txtDOP.Text;
                    supplier.PhoneList = this.contactList;
                    new SupplierDAL().AddSupplier(supplier);
                    RefreshRecord();
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.ShowDGVRecord();
                    this.txtSupplierCode.Select();
                }
            }
        }

        public void RefreshRecord()
        {
            IEnumerable<Control> list = FormControls.GetAll(this, typeof(TextBox));
            FormControls.Refresh(list);
            this.cbxContactList.Text = "";
            FormControls.CrudRefresh(btnSave, btnEdit, btnDelete);
        }

        private void AddSupplier_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

            IEnumerable<Control> list = FormControls.GetAll(this, typeof(TextBox));
            FormControls.IgnoreValidations(list);
            this.Close();
        }

        private void txtSupplierCode_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtName);
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtContactNo);
        }

        private void txtContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                this.txtAddress.Select();
            }
            else if (e.KeyCode==Keys.Space)
            {
                this.btnAddContact.Select();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWastage);
        }

        private void txtWastage_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMaking);
        }

        private void txtMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDOP);
        }

        private void txtDOP_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtSupplierCode_Validating(object sender, CancelEventArgs e)
        {
            FormControls.validate_box(sender as TextBox, e, errorProvider1, "Supplier Code");
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            FormControls.validate_box(sender as TextBox, e, errorProvider1, "Name");
        }

        private void dgvCustMobile_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            String str = this.txtContactNo.Text;
            string supplierId = this.txtSupplierCode.Text;
            contactList.Add(new Phone() { PhoneNo=str,PartyId=supplierId});
            this.cbxContactList.DataSource = contactList;
            cbxContactList.DisplayMember = "PhoneNo";
            cbxContactList.ValueMember = "PhoneNo";
            cbxContactList.SelectedIndex = 0;
            this.txtContactNo.Text = "";
            this.txtContactNo.Select();
            
        }

        private void txtContactNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void lblSaleManInfo_Click(object sender, EventArgs e)
        {

        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                bool exist = new SupplierDAL().isExist("select PAbri from Supplier where PAbri ='" + this.txtSupplierCode.Text + "'");
                if (exist == true)
                {
                    MessageBox.Show(this.txtSupplierCode.Text + " is already exist", Messages.Header);
                    this.txtSupplierCode.Focus();
                    return;
                }
                else
                {

                    supplier.PAbri = this.txtSupplierCode.Text;
                    supplier.PName = this.txtName.Text;
                    supplier.PtclNo1 = this.txtContactNo.Text;
                    supplier.PAddress = this.txtAddress.Text;
                    supplier.PDate = DateTime.Now;
                    supplier.PWastage = this.txtWastage.Text;
                    supplier.PMaking = this.txtMaking.Text;
                    supplier.PDiscount = this.txtDOP.Text;
                    supplier.PhoneList = this.contactList;
                    new SupplierDAL().AddSupplier(supplier);
                    RefreshRecord();
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.ShowDGVRecord();
                    this.txtSupplierCode.Select();
                }
            }
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {

            IEnumerable<Control> list = FormControls.GetAll(this, typeof(TextBox));
            FormControls.IgnoreValidations(list);
            this.Close();
        }

       

       
    }
}
