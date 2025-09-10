using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient ;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using BusinesEntities;
using DAL;
using System.Globalization;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace jewl
{
    public partial class ManageCustomer : Form
    {
        Customer cust;
        CountryDAL cDAL = new CountryDAL();
        CustomerDAL custDAL = new CustomerDAL();   
        AccountDAL adal = new AccountDAL();
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        Bitmap bitmap;
        SqlTransaction trans;
        List<Customer> custs;
        JewelPictures jp = new JewelPictures();
        private bool DeviceExist = false;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource = null;
        GroupAccount g;
        SubGroupAccount sg;
        ParentAccount p;
        ChildAccount c;
        private WebCam_Capture.WebCamCapture WebCamCapture;
        string st = "";
        string newPath, activeDir = "";
        int x = 0;
        string accountCode="";

        public ManageCustomer()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void AddCustomer_Load(object sender, EventArgs e)
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
                str = ur.GetUserRightsByUser("AddCustomer");
                if (str != "" && str != null)
                {
                    ur.AssignRights(str, btnSave, btnEdit, btnDelete);
                }
            }
            
            
            label11.Text = dateTimePicker1.Value.ToShortDateString ();

            this.cbxCountry.SelectedIndexChanged-=new EventHandler(cbxCountry_SelectedIndexChanged);
            this.btnDelete.Enabled = false ;
            this.dtpBDate.Enabled = false;
            this.dtpAnniDate.Enabled = false;
            this.ShowCustRecDgv();

            FormControls.FillCombobox(cbxCountry, cDAL.GetAllCountry(), "CountryName", "CountryId");
            FormControls.FillCombobox(cbxCity, cDAL.GetAllCityBy(), "CityName", "CityId");
            this.cbxCity.SelectedIndex = 0;
            this.cbxCountry.SelectedIndex = 0;
            this.cbxSalutation.SelectedIndex = 2;
            this.txtName.Select();
            getCamList();
            this.Location = new Point(0, 0);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "")
            {
                MessageBox.Show("First Enter Customer First Name",  Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                //con.Close();
                return;
            }
            else if (cbxCity.Text == "")
            {
                MessageBox.Show("First Enter City ", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.cbxCity.Focus();
                //con.Close();
                return;
            }
            else
            {

                con.Open();
                trans = con.BeginTransaction();
                cust = new Customer();

                string str = (string)this.cbxSalutation.SelectedItem;
                cust.Salutation = str;
                cust.Name = this.txtName.Text.ToString();
                cust.CO = this.txtCO.Text.ToString();
                if (this.txtCashBalance.Text != string.Empty)
                {
                    if (this.rbtReceiveable.Checked)
                        cust.CashBalance = Convert.ToDecimal(this.txtCashBalance.Text);
                    else if (this.rbtPayables.Checked)
                        cust.CashBalance = -Convert.ToDecimal(this.txtCashBalance.Text);
                    else
                        cust.CashBalance = 0;
                }
                else
                    cust.CashBalance = 0;
                cust.Address = " House No. " + txtHouseNo.Text + ", Block No." + txtBlockNo.Text + ", Street No." + txtStreetNo.Text + ", " + txtColony.Text;
                cust.Mobile = this.txtMobile.Text.ToString();
                cust.TelHome = this.txtTelHome.Text.ToString();
                cust.CNIC = this.txtCNIC.Text.ToString();
                cust.HouseNo = this.txtHouseNo.Text.ToString();
                cust.CityId = (City)this.cbxCity.SelectedItem;
                cust.BlockNo = this.txtBlockNo.Text.ToString();
                cust.CountryId = (Country)this.cbxCountry.SelectedItem;
                if (this.txtEmail.Text != string.Empty)
                {
                    string email = this.txtEmail.Text;
                    if (email.Contains('@'))
                        cust.Email = this.txtEmail.Text.ToString();
                    else
                    {
                        MessageBox.Show("Enter Correct Email Address", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtEmail.Focus();
                        con.Close();
                        return;
                    }
                }
                cust.Email = this.txtEmail.Text.ToString();
                cust.StreetNo = this.txtStreetNo.Text.ToString();
                cust.Near = this.txtNear.Text.ToString();
                cust.Colony = this.txtColony.Text.ToString();
                cust.Date = Convert.ToDateTime(this.dateTimePicker1.Value);
                cust.GoldBalance = 0;
                if (this.chkBirthDate.Checked == false)
                    cust.DateOfBirth = null;
                else
                    cust.DateOfBirth = Convert.ToDateTime(dtpBDate.Value);

                if (this.chkAnniversaryDate.Checked == false)
                    cust.AnniversaryDate = null;
                else
                    cust.AnniversaryDate = Convert.ToDateTime(dtpAnniDate.Value);
                bool df = true;
                cust.AccountCode = adal.CreateAccount(1, "Customer", cust.Name, "Customer", con, trans).ToString();
                ChildAccount cha = adal.GetChildByCode(cust.AccountCode, con, trans);
                adal.UpdateGoldBalance("update ChildAccount set OpeningCash=" + cust.CashBalance + ", OpeningGold=" + cust.GoldBalance + " where ChildCode='" + cha.ChildCode + "' ", con, trans);
                if (pbxMain.Image != null)
                {
                    jp.ImageMemorySmall = jp.ConvertImageToBinary(jp.resizeImage(pbxMain.Image, pbxMain.Size));
                    jp.ImageMemory = jp.ConvertImageToBinary(this.pbxMain.Image);
                    jp.CustId = (custDAL.GetMaxCustId()+1);
                }
                else
                {
                    jp.ImageMemory = null;
                    jp.ImageMemorySmall = null;
                }
               
                if (cha.ChildCode != null)
                {
                    custDAL.AddCustomer(cust);
                    if (this.pbxMain.Image != null)
                    {
                        custDAL.AddPics(jp);
                    }
                    trans.Commit();
                    con.Close();
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.RefreshRec();
                    this.ShowCustRecDgv();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Not Saved", Messages.Header);
                    return;
                }
            }
        }

        private void ShowCustRecDgv()
        {
            custs = custDAL.GetAllCustomer();
            if (custs == null)
                return;
            else
            {
                this.dgvCustomerDetail.AutoGenerateColumns = false;
                this.dgvCustomerDetail.Rows.Clear();
                int count = custs.Count;
                this.dgvCustomerDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {

                    this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Salutation .ToString() + custs[i].Name.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].CNIC.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].TelHome .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].Mobile .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[5].Value = custs[i].Email.ToString();
                    if (custs[i].DateOfBirth.HasValue )
                    {
                        DateTime dt = (DateTime)custs[i].DateOfBirth;
                        this.dgvCustomerDetail.Rows[i].Cells[6].Value = dt.ToString("dd-MM-yyy");
                    }
                    else
                        dgvCustomerDetail.Rows[i].Cells[6].Value = string.Empty;

                    if (custs[i].AnniversaryDate.HasValue)
                    {
                        DateTime dt = (DateTime)custs[i].AnniversaryDate;
                        this.dgvCustomerDetail.Rows[i].Cells[7].Value = dt.ToString("dd-MM-yyy");
                    }
                    else
                        this.dgvCustomerDetail.Rows[i].Cells[7].Value = string.Empty;
                    this.dgvCustomerDetail.Rows[i].Cells[8].Value = custs[i].CO .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[9].Value = custs[i].HouseNo .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[10].Value = custs[i].StreetNo .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[11].Value = custs[i].BlockNo.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[12].Value = custs[i].Colony.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[13].Value = custs[i].CityId.CityName .ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[14].Value = custs[i].CountryId.CountryName.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[15].Value = Convert.ToInt32(custs[i].ID);
                    this.dgvCustomerDetail.Rows[i].Cells[16].Value = custs[i].AccountCode.ToString();









                    //this.dgvCustomerDetail.Rows[i].Cells[0].Value = Convert.ToInt32(custs[i].ID);
                    ////this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Salutation.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Name.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].CO.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Mobile.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].CNIC.ToString();
                    ////this.dgvCustomerDetail.Rows[i].Cells[5].Value = custs[i].TelOffice.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[6].Value = custs[i].HouseNo.ToString();
                    ////this.dgvCustomerDetail.Rows[i].Cells[8].Value = custs[i].Address.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[7].Value = custs[i].City.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[8].Value = custs[i].BlockNo.ToString();
                    //this.dgvCustomerDetail.Rows[i].Cells[9].Value = custs[i].Country.ToString();

                }
            }
        }



        private void SearchCustomerByID(int id)
        {
            //try
            //{
            #region a
            //Customer newCust = new Customer();
            //cust = custDAL.SearchCustById(id);

            ////this.txtSalutation.Text = cust.Salutation.ToString();
            //this.txtName.Text = cust.Name.ToString();
            //this.txtCO.Text = cust.CO.ToString();
            //this.txtMobile.Text = cust.Mobile.ToString();
            //this.txtTelHome.Text = cust.CNIC.ToString();
            //this.txtCNIC.Text = cust.TelHome.ToString();
            //this.txtHouseNo.Text = cust.HouseNo.ToString();
            //this.txtStreetNo.Text = cust.StreetNo.ToString();
            //this.txtNear.Text = cust.Near.ToString();
            //this.txtColony.Text = cust.Colony.ToString();
            ////this.txtCity.Text = cust.City.ToString();
            //this.txtBlockNo.Text = cust.BlockNo.ToString();
            ////this.txtCountry.Text = cust.Country.ToString();
            //if (cust.CashBalance.HasValue)
            //{
            //    this.txtCashBalance.Text = cust.CashBalance.ToString();
            //}
            //else
            //    this.txtCashBalance.Text = "";
            ////this.txtAddress.Text = cust.Address.ToString();
            //this.txtEmail.Text = cust.Email.ToString();
            //if (cust.AnniversaryDate.HasValue)
            //{
            //    this.chkAnniversaryDate.Checked = true;
            //    this.dtpAnniDate.Enabled = true;
            //    this.dtpAnniDate.Value = Convert.ToDateTime(cust.AnniversaryDate);
            //}
            //else
            //{
            //    this.chkAnniversaryDate.Checked = false;
            //    this.dtpAnniDate.Enabled = false;
            //}

            //if (cust.DateOfBirth.HasValue)
            //{
            //    this.chkBirthDate.Checked = true;
            //    // this.dtpBDate.Enabled = true;
            //    this.dtpBDate.Value = Convert.ToDateTime(cust.DateOfBirth);
            //}
            //else
            //{
            //    this.chkBirthDate.Checked = false;
            //    this.dtpBDate.Enabled = false;
            //}
            ////this.lblCustId.Text = Convert.ToString(cust.ID);
            #endregion
            //Customer cust = new Customer();
            cust = custDAL.SearchCustById(id);
            //CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            // TextInfo txtInfo = cultureInfo.TextInfo;
            this.txtName.Text = cust.Name.ToString();
            for (int i = 0; i < cbxSalutation.Items.Count; i++)
            {
                if (cust.Salutation == (string)this.cbxSalutation.Items[i])
                    this.cbxSalutation.SelectedIndex = i;
            }
            //for(int i=0;i<cbxProfession .

            this.txtTelHome.Text = cust.TelHome.ToString();
            this.txtCNIC.Text = cust.CNIC.ToString();
            this.txtMobile.Text = cust.Mobile.ToString();
            this.txtEmail.Text = cust.Email.ToString();
            this.txtCO.Text = cust.CO.ToString();
            this.txtHouseNo.Text = cust.HouseNo.ToString();
            this.txtBlockNo.Text = cust.BlockNo.ToString();
            this.txtStreetNo.Text = cust.StreetNo.ToString();
            this.txtColony.Text = cust.Colony.ToString();
            this.txtNear.Text = cust.Near.ToString();
            if (cust.CountryId.CountryId != 0)
            {
                for (int i = 0; i < this.cbxCountry.Items.Count; i++)
                {
                    Country it = (Country)this.cbxCountry.Items[i];
                    if (cust.CountryId.CountryId == it.CountryId)
                        this.cbxCountry.SelectedIndex = i;
                }
                int k = (int)this.cbxCountry.SelectedValue;
                this.cbxCity.DataSource = cDAL.GetAllCity(k);
                this.cbxCity.DisplayMember = "CityName";
                this.cbxCity.ValueMember = "CityId";
                for (int i = 0; i < this.cbxCity.Items.Count; i++)
                {
                    City it = (City)this.cbxCity.Items[i];
                    if (cust.CityId.CityId == it.CityId)
                    {
                        this.cbxCity.SelectedIndex = i;
                        break;
                    }
                    else
                        this.cbxCity.SelectedIndex = -1;
                }
            }
            //this.dtpDate.Value = Convert.ToDateTime(cust.Date);

            if (cust.AnniversaryDate.HasValue)
            {
                this.chkAnniversaryDate.Checked = true;
                this.dtpAnniDate.Enabled = true;
                this.dtpAnniDate.Value = Convert.ToDateTime(cust.AnniversaryDate);
            }
            else
            {
                this.chkAnniversaryDate.Checked = false;
                this.dtpAnniDate.Enabled = false;
            }

            if (cust.DateOfBirth.HasValue)
            {
                this.chkBirthDate.Checked = true;
                this.dtpBDate.Enabled = true;
                this.dtpBDate.Value = Convert.ToDateTime(cust.DateOfBirth);
            }
            else
            {
                this.chkBirthDate.Checked = false;
                this.dtpBDate.Enabled = false;
            }
            if (cust.CashBalance.HasValue)
            {
                if (cust.CashBalance < 0)
                {
                    this.rbtPayables.Checked = true;
                    this.txtCashBalance.Text = (-(cust.CashBalance)).ToString();
                }
                else
                {
                    this.rbtReceiveable.Checked = true;
                    this.txtCashBalance.Text = cust.CashBalance.ToString();
                }
            }
            this.txtAccountCode.Text = cust.AccountCode.ToString();
            if (cust.ImageMemory == null)
            {
                this.pbxMain.Image = null;
                this.pbxMain.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                MemoryStream mst = new MemoryStream(cust.ImageMemory);
                Image img = Image.FromStream(mst);
                Size newSize = new Size(3800, 3300);
                Bitmap bitmap = new Bitmap(img, newSize);
                this.pbxMain.Image = bitmap;
            }
        }

        private void RefreshRec()
        {
            DateTime t = DateTime.Today;
            //this.txtSalutation.Text = "";
            this.txtName.Text = "";
            this.txtCO.Text = "";
            this.txtMobile.Text = "";
            this.pbxMain.Image = null;
            this.txtTelHome.Text = "";
            this.txtCNIC.Text = "";
            this.txtHouseNo.Text = "";
            //this.txtAddress.Text = "";
            //this.cbxCity .Text = "";
            this.txtBlockNo.Text = "";
            //this.cbxCountry .Text = "";
            this.txtEmail.Text = "";
            this.txtStreetNo.Text = "";
            this.dtpBDate.Value = t;
            this.dtpAnniDate.Value = t;
            this.chkAnniversaryDate.Checked = false;
            this.chkBirthDate.Checked = false;
            this.txtNear.Text = "";
            this.txtColony.Text = "";
            this.txtCashBalance.Text = "";
            this.txtAccountCode.Text = "";
            CloseVideoSource();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {                    
            if (btnEdit.Text == "&Edit")
            {
                if (x == 0)
                {
                    MessageBox.Show("No Record is selected", Messages.Header);
                    return;
                }
                else
                    this.SearchCustomerByID(x);
                //this.btnEdit.Text = "Update";
                //return;
            }
            if (btnEdit.Text == "&Update")
            {
                string str = (string)this.cbxSalutation.SelectedItem;
                
                cust.Salutation = str;
                cust.Name = this.txtName.Text.ToString();
                cust.CO = this.txtCO.Text.ToString();
                cust.Address = " House No. " + txtHouseNo.Text + ", Block No." + txtBlockNo.Text + ", Street No." + txtStreetNo.Text + ", " + txtColony.Text;
                cust.Mobile = this.txtMobile.Text.ToString();
                cust.CNIC = this.txtCNIC.Text.ToString();
                cust.TelHome = this.txtTelHome.Text.ToString();
                cust.HouseNo = this.txtHouseNo.Text.ToString();
                cust.CityId = (City)this.cbxCity.SelectedItem;
                cust.BlockNo = this.txtBlockNo.Text.ToString();
                cust.CountryId = (Country)this.cbxCountry.SelectedItem;
                cust.Email = this.txtEmail.Text.ToString();
                cust.StreetNo = this.txtStreetNo.Text.ToString();
                cust.Near = this.txtNear.Text.ToString();
                cust.Colony = this.txtColony.Text.ToString();
                if (this.txtCashBalance.Text != string.Empty)
                {
                    if (this.rbtReceiveable.Checked)
                        cust.CashBalance = Convert.ToDecimal(this.txtCashBalance.Text);
                    else if (this.rbtPayables.Checked)
                        cust.CashBalance = -Convert.ToDecimal(this.txtCashBalance.Text);
                    else
                        cust.CashBalance = 0;
                }
                else
                    cust.CashBalance = 0;

                if (this.chkBirthDate.Checked == false)
                {
                    cust.DateOfBirth = null;
                }
                else
                    cust.DateOfBirth = Convert.ToDateTime(dtpBDate.Value);

                if (this.chkAnniversaryDate.Checked == false)
                {
                    cust.AnniversaryDate = null;
                }
                else
                    cust.AnniversaryDate = Convert.ToDateTime(dtpAnniDate.Value);
                if (pbxMain.Image != null)
                {
                    jp.ImageMemorySmall = jp.ConvertImageToBinary(jp.resizeImage(pbxMain.Image, pbxMain.Size));
                    jp.ImageMemory = jp.ConvertImageToBinary(this.pbxMain.Image);
                    jp.CustId = x;
                }
                else
                {
                    jp.ImageMemory = null;
                    jp.ImageMemorySmall = null;
                }
                bool aFlag = false;
                if (aFlag == true)
                {
                    MessageBox.Show("You can not update customer,it is already in use", Messages.Header);
                }
                else
                {
                    custDAL.UpdateCustomer(x, cust);
                    bool pflag = custDAL.isPicCustidExist(x);
                    if (pflag == true)
                    {
                        custDAL.UpdateCustomerPics(x, jp);
                    }
                    else
                    {
                        if (this.pbxMain.Image != null)
                        {
                            custDAL.AddPics(jp);
                        }
                    }
                    MessageBox.Show(Messages.Updated, Messages.Header);
                }
                this.btnEdit.Text = "&Edit";
                this.btnSave.Enabled = true;
                this.RefreshRec();
                this.ShowCustRecDgv();
            }
        }


        private void chkBirthDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBirthDate.Checked == true)
            {
                this.dtpBDate.Enabled = true;
            }
            else
                this.dtpBDate.Enabled = false;
        }

        private void chkAnniversaryDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnniversaryDate.Checked == true)
            {
                this.dtpAnniDate.Enabled = true;
            }
            else
                this.dtpAnniDate.Enabled = false;
        }       

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Messages.Sure, Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                custDAL.DeleteCustomer(accountCode, cust);

            this.RefreshRec();
            this.ShowCustRecDgv();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.RefreshRec();
            this.btnEdit.Text = "&Edit";
            this.btnSave.Enabled = true;
        }

        private void txtSalutation_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

      private void txtCO_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str = this.txtCO.Text;
            this.txtCO.Text = txtInfo.ToTitleCase(str);
        }

        private void txtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57)&& Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;                           
        }

        private void txtTelHome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtCNIC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtHouseNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
            //    e.Handled = true;
            //else
            //    e.Handled = false;
        }

        private void txtCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtBlockNo_KeyPress(object sender, KeyPressEventArgs e)
        {


            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && (Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }
        private void txtCountry_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtState_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str = this.txtBlockNo.Text;
            this.txtBlockNo.Text = txtInfo.ToTitleCase(str);
        }        

        private void txtCashBalance_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void dtpBDate_Leave(object sender, EventArgs e)
        {
            if (dtpBDate.Value > dateTimePicker1.Value)
            {
                MessageBox.Show("Enter Correct Date ", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpBDate.Value = dateTimePicker1.Value;
            }
            return;
        }

        private void dtpAnniDate_Leave(object sender, EventArgs e)
        {
            if (dtpAnniDate.Value > dateTimePicker1.Value)
            {
                MessageBox.Show("Enter Correct Date ", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpAnniDate.Value = dateTimePicker1.Value;
            }
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.txtName.Text;
            if (string.IsNullOrEmpty(str))
            {
                dgvSearchCustomer.Rows.Clear();
                dgvSearchCustomer.Visible = false;

                return;
            }
            {
                custs = null;
                dgvSearchCustomer.Visible = true;
                custs = custDAL.GetAllCustomer("select * from CustomerInfo where Name like '%" + this.txtName.Text + "%'");
                if (custs == null)
                {
                    dgvSearchCustomer.Visible = false;
                    return;
                }
                else
                {
                    this.dgvSearchCustomer.AutoGenerateColumns = false;

                    int count = custs.Count;

                    this.dgvSearchCustomer.Rows.Clear();
                    this.dgvSearchCustomer.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        //int j = 0;
                        int k = 1;

                        this.dgvSearchCustomer.Rows[i].Cells[0].Value = i + k;
                        this.dgvSearchCustomer.Rows[i].Cells[1].Value = custs[i].Name.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[2].Value = custs[i].CNIC.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[3].Value = custs[i].TelHome.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[4].Value = custs[i].Mobile.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[5].Value = custs[i].Email.ToString();
                        if (custs[i].DateOfBirth.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[6].Value = custs[i].DateOfBirth.ToString();
                        else
                            dgvSearchCustomer.Rows[i].Cells[6].Value = string.Empty;

                        if (custs[i].AnniversaryDate.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = custs[i].AnniversaryDate.ToString();
                        else
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = string.Empty;
                        this.dgvSearchCustomer.Rows[i].Cells[8].Value = custs[i].CO.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[9].Value = custs[i].HouseNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[10].Value = custs[i].StreetNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[11].Value = custs[i].BlockNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[12].Value = custs[i].Colony.ToString();

                        //this.dgvSearchCustomer.Rows[i].Cells[13].Value = custs[i].City.ToString();
                        //this.dgvSearchCustomer.Rows[i].Cells[14].Value = custs[i].Country.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[15].Value = Convert.ToInt32(custs[i].ID);
                    }
                }
            }
        }

        private void txtCNIC_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.txtCNIC.Text;
            if (string.IsNullOrEmpty(str))
            {
                dgvSearchCustomer.Rows.Clear();
                dgvSearchCustomer.Visible = false;

                return;
            }
            {
                custs = null;
                dgvSearchCustomer.Visible = true;
                custs = custDAL.GetAllCustomer("select * from CustomerInfo where CNIC like '%" + str + "%'");
                if (custs == null)
                {
                    dgvSearchCustomer.Visible = false;
                    return;
                }
                else
                {
                    this.dgvSearchCustomer.AutoGenerateColumns = false;

                    int count = custs.Count;

                    this.dgvSearchCustomer.Rows.Clear();
                    this.dgvSearchCustomer.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        //int j = 0;
                        int k = 1;

                        this.dgvSearchCustomer.Rows[i].Cells[0].Value = i + k;
                        this.dgvSearchCustomer.Rows[i].Cells[1].Value = custs[i].Name.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[2].Value = custs[i].CNIC.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[3].Value = custs[i].TelHome.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[4].Value = custs[i].Mobile.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[5].Value = custs[i].Email.ToString();
                        if (custs[i].DateOfBirth.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[6].Value = custs[i].DateOfBirth.ToString();
                        else
                            dgvSearchCustomer.Rows[i].Cells[6].Value = string.Empty;

                        if (custs[i].AnniversaryDate.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = custs[i].AnniversaryDate.ToString();
                        else
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = string.Empty;
                        this.dgvSearchCustomer.Rows[i].Cells[8].Value = custs[i].CO.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[9].Value = custs[i].HouseNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[10].Value = custs[i].StreetNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[11].Value = custs[i].BlockNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[12].Value = custs[i].Colony.ToString();

                        //this.dgvSearchCustomer.Rows[i].Cells[13].Value = custs[i].City.ToString();
                        //this.dgvSearchCustomer.Rows[i].Cells[14].Value = custs[i].Country.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[15].Value = Convert.ToInt32(custs[i].ID);
                    }
                }
            }
        }

        private void txtMobile_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.txtMobile.Text;
            if (string.IsNullOrEmpty(str))
            {
                dgvSearchCustomer.Rows.Clear();
                dgvSearchCustomer.Visible = false;

                return;
            }
            {
                custs = null;
                dgvSearchCustomer.Visible = true;
                custs = custDAL.GetAllCustomer ("select * from CustomerInfo where Mobile like '%" + str + "%' ");
                if (custs == null)
                {
                    dgvSearchCustomer.Visible = false;
                    return;
                }
                else
                {
                    this.dgvSearchCustomer.AutoGenerateColumns = false;

                    int count = custs.Count;

                    this.dgvSearchCustomer.Rows.Clear();
                    this.dgvSearchCustomer.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        //int j = 0;
                        int k = 1;

                        this.dgvSearchCustomer.Rows[i].Cells[0].Value = i + k;
                        this.dgvSearchCustomer.Rows[i].Cells[1].Value = custs[i].Name.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[2].Value = custs[i].CNIC.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[3].Value = custs[i].TelHome.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[4].Value = custs[i].Mobile.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[5].Value = custs[i].Email.ToString();
                        if (custs[i].DateOfBirth.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[6].Value = custs[i].DateOfBirth.ToString();
                        else
                            dgvSearchCustomer.Rows[i].Cells[6].Value = string.Empty;

                        if (custs[i].AnniversaryDate.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = custs[i].AnniversaryDate.ToString();
                        else
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = string.Empty;
                        this.dgvSearchCustomer.Rows[i].Cells[8].Value = custs[i].CO.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[9].Value = custs[i].HouseNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[10].Value = custs[i].StreetNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[11].Value = custs[i].BlockNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[12].Value = custs[i].Colony.ToString();

                        //this.dgvSearchCustomer.Rows[i].Cells[13].Value = custs[i].City.ToString();
                        //this.dgvSearchCustomer.Rows[i].Cells[14].Value = custs[i].Country.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[15].Value = Convert.ToInt32(custs[i].ID);
                    }
                }
            }
        }

        private void txtTelHome_KeyUp(object sender, KeyEventArgs e)
        {
            string str = this.txtTelHome.Text;
            if (string.IsNullOrEmpty(str))
            {
                dgvSearchCustomer.Rows.Clear();
                dgvSearchCustomer.Visible = false;

                return;
            }
            else
            {
                custs = null;
                dgvSearchCustomer.Visible = true;
                custs = custDAL.GetAllCustomer("select * from CustomerInfo where TelHome like '" + str + "%' ");
                if (custs == null)
                {
                    dgvSearchCustomer.Visible = false;
                    return;
                }
                else
                {
                    this.dgvSearchCustomer.AutoGenerateColumns = false;

                    int count = custs.Count;

                    this.dgvSearchCustomer.Rows.Clear();
                    this.dgvSearchCustomer.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        //int j = 0;
                        int k = 1;

                        this.dgvSearchCustomer.Rows[i].Cells[0].Value = i + k;
                        this.dgvSearchCustomer.Rows[i].Cells[1].Value = custs[i].Name.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[2].Value = custs[i].CNIC.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[3].Value = custs[i].TelHome.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[4].Value = custs[i].Mobile.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[5].Value = custs[i].Email.ToString();
                        if (custs[i].DateOfBirth.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[6].Value = custs[i].DateOfBirth.ToString();
                        else
                            dgvSearchCustomer.Rows[i].Cells[6].Value = string.Empty;

                        if (custs[i].AnniversaryDate.HasValue)
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = custs[i].AnniversaryDate.ToString();
                        else
                            this.dgvSearchCustomer.Rows[i].Cells[7].Value = string.Empty;
                        this.dgvSearchCustomer.Rows[i].Cells[8].Value = custs[i].CO.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[9].Value = custs[i].HouseNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[10].Value = custs[i].StreetNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[11].Value = custs[i].BlockNo.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[12].Value = custs[i].Colony.ToString();

                        //this.dgvSearchCustomer.Rows[i].Cells[13].Value = custs[i].City.ToString();
                        //this.dgvSearchCustomer.Rows[i].Cells[14].Value = custs[i].Country.ToString();
                        this.dgvSearchCustomer.Rows[i].Cells[15].Value = Convert.ToInt32(custs[i].ID);
                    }
                }
            }
        }

        private void txtStreetNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtColony_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 45 && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
            if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
                e.Handled = true;
        }

        private void txtColony_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str = this.txtColony.Text;
            this.txtColony.Text = txtInfo.ToTitleCase(str);
        }

        private void txtCO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 46 && Convert.ToInt16(e.KeyChar) != 45)
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
                e.Handled = true;
        }

        private void cbxCity_Click(object sender, EventArgs e)
        {
            if (cbxCountry.Text == "")
            {
                MessageBox.Show("First Select Country ", Messages.Header);
                return;
            }           
        }

        private void dgvCustomerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                int id = Convert.ToInt32(dgvCustomerDetail.Rows[e.RowIndex].Cells[15].Value);
                accountCode = dgvCustomerDetail.Rows[e.RowIndex].Cells[16].Value.ToString();
                x = id;
               
                this.SearchCustomerByID(id);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
                this.btnDelete.Enabled = true;
            }
        }

        private void cbxCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            int x = (int)this.cbxCountry.SelectedValue;
            this.cbxCity.DataSource = cDAL.GetAllCity(x);
            this.cbxCity.DisplayMember = "CityName";
            this.cbxCity.ValueMember = "CityId";
            this.cbxCity.SelectedIndex = -1;
        }

        private void cbxCountry_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxCountry.SelectedIndexChanged += new EventHandler(cbxCountry_SelectedIndexChanged);
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 46)
            //    e.Handled = true;
            //else
            //    e.Handled = false;

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
            string str = this.txtName.Text;
            this.txtName.Text = txtInfo.ToTitleCase(str);
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
            }
        }

        private void dgvSearchCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                int id = Convert.ToInt32(dgvSearchCustomer.Rows[e.RowIndex].Cells[15].Value);
                x = id;
                this.SearchCustomerByID(id);
                this.btnEdit.Text = "&Update";
                this.btnSave.Enabled = false;
                this.btnDelete.Enabled = true;
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMobile);
        }

        private void txtMobile_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtStreetNo);
        }

        private void txtStreetNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtColony);
        }

        private void txtColony_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxCountry);
        }

        private void cbxCountry_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxCity);
        }

        private void cbxCity_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCashBalance);
        }

        private void txtCashBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                this.rbtReceiveable.Focus();
            }
        }

        private void rbtReceiveable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, e);
            }
        }

        private void rbtPayables_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, e);
            }
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            AddCountry adc = new AddCountry();
            adc.ShowDialog();
            FormControls.FillCombobox(cbxCountry, cDAL.GetAllCountry(), "CountryName", "CountryId");
        }

        private void btnAddCity_Click(object sender, EventArgs e)
        {
            AddCity adc = new AddCity();
            adc.ShowDialog();
            FormControls.FillCombobox(cbxCity, cDAL.GetAllCityBy(), "CityName", "CityId");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }
        private void FillPictureBox()
        {
            if (this.pbxMain.Image != null)
            { }
            else
            {
                DialogResult result = openFileDialog1.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Image img = Image.FromFile(this.openFileDialog1.FileName);
                    //Size newSize = new Size(3800, 3300);
                    bitmap = new Bitmap(img);
                    this.pbxMain.Image = bitmap;
                    //this.pbxZoom.Image = Image.FromFile(this.openFileDialog1.FileName);
                    //this.pbxThumbNail.Image = Image.FromFile(this.openFileDialog1.FileName);
                    //this.SetImage(this.pbxMain);
                    this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.pbxMain.Image == null)
            {
                MessageBox.Show("No Picture is selected to Remove", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                this.pbxMain.Image = null;
            this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (DeviceExist)
            {
                videoSource = new VideoCaptureDevice(videoDevices[comboBox1.SelectedIndex].MonikerString);
                videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
                CloseVideoSource();
                videoSource.DesiredFrameSize = new Size(160, 120);
                //videoSource.DesiredFrameRate = 10;
                videoSource.Start();
            }
        }

        private void getCamList()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                comboBox1.Items.Clear();
                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                DeviceExist = true;
                foreach (FilterInfo device in videoDevices)
                {
                    comboBox1.Items.Add(device.Name);
                }
                comboBox1.SelectedIndex = 0; //make dafault to first cam
            }
            catch (ApplicationException)
            {
                DeviceExist = false;
                comboBox1.Items.Add("No capture device on your system");
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap img = (Bitmap)eventArgs.Frame.Clone();
            //do processing here
            pbxMain.Image = img;
        }

        //close the device safely
        private void CloseVideoSource()
        {
            if (!(videoSource == null))
                if (videoSource.IsRunning)
                {
                    videoSource.SignalToStop();
                    videoSource = null;
                }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            // stop the video capture
            //this.WebCamCapture.Stop();
                CloseVideoSource();

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.Name != "C:\\" && d.IsReady && d.DriveType == DriveType.Fixed)
                {
                    // This is the drive you want...
                    st = d.Name;
                }
            }

            if (this.pbxMain.Image != null)
            {
                try
                {

                    using (Bitmap bitmap1 = new Bitmap(this.pbxMain.Image))//System.Drawing.Image Img = System.Drawing.Image.FromStream(mst)//Bitmap ImgThnail = new Bitmap(Img, Thumbwidth, ThumbHeight))
                    {
                        //bool t = File.Exists(st + "CamImage"+s+".Jpg");
                        //if (t == false)
                        //{

                        activeDir = st;

                        //Create a new subfolder under the current active folder
                        newPath = System.IO.Path.Combine(activeDir, "CamPics");
                        string str = DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss");
                        System.IO.Directory.CreateDirectory(newPath);

                        bitmap1.Save(newPath + "\\CamImage" + str + ".Jpg", ImageFormat.Jpeg);//(sfd.FileName, ImageFormat.Jpeg);
                        Image img = Image.FromFile(newPath + "\\CamImage" + str + ".Jpg");
                        Size newSize = new Size(3800, 3300);
                        bitmap = new Bitmap(img, newSize);
                        this.pbxMain.Image = bitmap;

                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An Error has occured: \n" + ex.Message);
                    return;
                }
            }
        }

        private void ManageCustomer_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseVideoSource();
        }
    }
}
    