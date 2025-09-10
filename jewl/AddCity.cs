using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Data.SqlClient;
using DAL;
using BusinesEntities;

namespace jewl
{
    public partial class AddCity : Form
    {
        JewelConnection con;
        JewelConnection con1 = new JewelConnection();
        List<City> cityList;
        public int k;
        bool checkForDelete = false;
        public AddCity()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                checkForDelete = new ItemDAL().isNameExist("select CityId from CustomerInfo where CityId='" + this.lblCityId.Text + "'");

                if (this.txtCityName.Text == "")
                {
                    MessageBox.Show("Please Select City", Messages.Header);
                    return; //And City Then Delete");
                }
                else if (checkForDelete == true)
                {
                    MessageBox.Show(Messages.DeleteWarning, Messages.Header);
                    Refresh();
                    return;
                }
                else
                {
                    //con1  = new Connection();
                    try
                    {
                        int value = Convert.ToInt32(this.lblCityId.Text);

                        string DeleteQuery;
                        DeleteQuery = " delete from City where CityId =" + value;
                        con.DeleteRecordfromJMDB(DeleteQuery);
                        this.txtCityName.Text = "";
                        MessageBox.Show(Messages.Deleted, Messages.Header);

                    }

                    catch (Exception ex0)
                    {
                        throw ex0;
                    }
                }
                Refresh();
                ShowRecord("Select ct.CountryName ,c.* from Country ct inner join City c on c.CountryId=ct.CountryId ");

            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //::::::-_-_::::::if City Name All Ready Exist::::::-_-_:::::://
            //---------------------------------------------------------------//
            con = new JewelConnection();
            try
            {
                if (btnEdit.Text == "&Update")
                {
                    int value = Convert.ToInt32(this.lblCityId.Text);
                    string UpdateQuery;
                    UpdateQuery = " UPDATE City SET CityName = '" + txtCityName.Text + "' Where CityId =" + value;
                    con.UpdateRecordFromJMDB(UpdateQuery);
                    MessageBox.Show(Messages.Updated, Messages.Header);
                    Refresh();
                    ShowRecord("Select ct.CountryName ,c.* from Country ct inner join City c on c.CountryId=ct.CountryId ");
                    this.cbxCountryList.Select();
                }

            }
            catch (Exception)
            {

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            con = new JewelConnection();


            //::::::-_-_::::::If Country Name All Ready Exist::::::-_-_::::::// (Part 1)
            //---------------------------------------------------------------//
            if (ValidateChildren(ValidationConstraints.Enabled))
            {
                try
                {
                    con.MyDataSet.Tables["TableCity"].Rows.Clear();
                }
                catch (Exception)
                { }
                try
                {
                    string query;
                    query = "select * from City";
                    con.GetDataFromJMDB(query, "TableCity");
                    int count = con.MyDataSet.Tables["TableCity"].Rows.Count;

                    for (int i = 1; i < count; i++)
                    {
                        if (txtCityName.Text == con.MyDataSet.Tables["TableCity"].Rows[i]["CityName"].ToString())
                        {
                            MessageBox.Show("This City Already Exist", Messages.Header);
                            return;
                        }
                    }
                }
                catch (Exception)
                {
                }

                try
                {
                    if (txtCityName.Text == "")
                    {
                        MessageBox.Show("First Enter Country Name And Then City", Messages.Header);
                    }
                    else
                    {
                        //::::::-_-_::::::To Save Data in Database ::::::-_-_:::::://   (Part 2)
                        //---------------------------------------------------------//   
                        int CountryId = (int)this.cbxCountryList.SelectedValue;
                        string SaveQuery;
                        SaveQuery = " insert into City values ('" + CountryId + "','" + txtCityName.Text + "')";
                        con.SaveRecordFromJMDB(SaveQuery);
                        MessageBox.Show(Messages.Saved, Messages.Header);
                        this.txtCityName.Text = "";
                        this.cbxCountryList.Select();
                    }
                }
                catch (Exception)
                {
                    throw;
                }


            }
            ShowRecord("Select ct.CountryName ,c.* from Country ct inner join City c on c.CountryId=ct.CountryId ");
        }

        private void txtCityName_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtinfo = culInfo.TextInfo;
            string str = this.txtCityName.Text;
            this.txtCityName.Text = txtinfo.ToTitleCase(str);
        }

        private void ShowRecord(string str)
        {
            cityList = new CountryDAL().SearchCity(str, 0);
            if (cityList == null)
            {
                this.dgvDetail.Rows.Clear();
                return;
            }
            else
            {
                this.dgvDetail.AutoGenerateColumns = false;
                this.dgvDetail.Rows.Clear();
                int count = cityList.Count;
                this.dgvDetail.Rows.Add(count);

                for (int i = 0; i < count; i++)
                {
                    this.dgvDetail.Rows[i].Cells[0].Value = cityList[i].CntId.CountryId.ToString();
                    this.dgvDetail.Rows[i].Cells[1].Value = cityList[i].CntId.CountryName.ToString();
                    this.dgvDetail.Rows[i].Cells[2].Value = cityList[i].CityId.ToString(); ;
                    this.dgvDetail.Rows[i].Cells[3].Value = cityList[i].CityName.ToString();

                }
            }

        }

        private void cbxSCountryList_MouseClick(object sender, MouseEventArgs e)
        {
           
        }
        private void cbxCityNameList_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void cbxSCountryList_SelectionChangeCommitted(object sender, EventArgs e)
        {
           
        }
        private void AddCity_Load(object sender, EventArgs e)
        {
            FormControls.FillCombobox(cbxCountryList, new CountryDAL().GetAllCountry(), "CountryName", "CountryId");

            ShowRecord("Select ct.CountryName ,c.* from Country ct inner join City c on c.CountryId=ct.CountryId ");
            Refresh();
            
            this.txtCityName.Select();
            this.cbxCountryList.SelectedIndex =0;
            UserRights ur = new UserRights();
           string  str = ur.GetRightsByUser();
           if (str == "Limited")
           {
               this.btnEdit.Enabled = false;
               this.btnDelete.Enabled = false;
           }
            //foreach (ComboBox tb in this.Controls.OfType<ComboBox>())
            //{

            //    tb.Validating -= textBox_Validating;
            //}
        }
        //private void textBox_Validating(object sender, CancelEventArgs e)
        //{
        //    ComboBox currenttb = (ComboBox)sender;
        //    if (currenttb.Text == "")
        //    {
        //        MessageBox.Show(string.Format("Empty field {0 }", currenttb.Name.Substring(3)));
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        e.Cancel = false;
        //    }
        //}

        public void Refresh()
        {
            this.btnEdit.Enabled = false;
            this.btnEdit.Text = "&Edit";
            this.btnDelete.Enabled = false;
            this.btnSave.Enabled = true;
            this.cbxCountryList.SelectedIndex = -1;
            this.txtCityName.Text = "";
            this.lblCityId.Text = "";
        }

        private void cbxCityNameList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.txtCityName.Text = this.cbxCityNameList.Text;
        }

        private void cbxCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.cbxCountryList.Text = this.cbxSCountryList.Text;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //this.cbxSCountryList.Visible = false;
            //this.cbxCityNameList.Visible = false;
            //this.lblSelectCountryName.Visible = false;
            //this.lblCityNameList.Visible = false;
            this.txtCityName.Text = string.Empty;
            this.txtCityName.Visible = true;
            this.lblCityName.Visible = true;
            this.cbxCountryList.Visible = true;
            this.lblCountryList.Visible = true;
            btnEdit.Text = "Edit";



        }

        private void txtCityName_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void cbxSCountryList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddCity_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void dgvDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                k = Convert.ToInt32(this.dgvDetail.Rows[e.RowIndex].Cells[2].Value);
            }
            foreach (City city in cityList)
            {
                if (city.CityId == k)
                {
                    this.lblCityId.Text = city.CityId.ToString();
                    this.txtCityName.Text = city.CityName;
                    this.cbxCountryList.SelectedValue = city.CntId.CountryId;
                }
            }
            this.btnSave.Enabled = false;
            this.btnEdit.Text = "&Update";
            this.btnEdit.Enabled = true;
            this.btnDelete.Enabled = true;
        }

        private void cbxCountryList_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(cbxCountryList.Text))
            {
                e.Cancel = true;
                cbxCountryList.Focus();
                errorProvider1.SetError(cbxCountryList, "Country Name" + Messages.Empty);
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(cbxCountryList, "");
            }
            // textBox_Validating(sender, e);
        }

        private void txtCityName_Validating(object sender, CancelEventArgs e)
        {
            //FormControls.validate_box(txtCityName, e, errorProvider1, "City Name");
            // textBox_Validating(sender, e);
        }

        private void cbxCountryList_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCityName);
        }

        private void txtCityName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxCountryList);
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            ShowRecord("Select ct.CountryName ,c.* from Country ct inner join City c on c.CountryId=ct.CountryId and c.CityName like '%" + this.txtSearch.Text + "%'");
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxCountryList);
        }

        private void lblCityName_Click(object sender, EventArgs e)
        {

        }
    }


}


