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
    public partial class AddCountry : Form
    {
        JewelConnection con;       
        bool checkforDelete = false;

        public AddCountry()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con = new JewelConnection();
            try
            {
                try
                {
                    con.MyDataSet.Tables["TableCountry"].Rows.Clear();
                }
                catch { }
                string query;
                query = "select * from Country";
                con.GetDataFromJMDB(query, "TableCountry");
                int count = con.MyDataSet.Tables["TableCountry"].Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (txtCountryName.Text == con.MyDataSet.Tables["TableCountry"].Rows[i]["CountryName"].ToString())
                    {
                        MessageBox.Show("This Country Already Exist", Messages.Header);
                        return;
                    }
                }
                if (txtCountryName.Text == "")
                {
                    MessageBox.Show("First Enter Country Name", Messages.Header);
                    this.txtCountryName.Focus();
                    return;
                }
                else
                {
                    string SaveQuery;
                    SaveQuery = " insert into Country values ('" + txtCountryName.Text + "')";
                    con.SaveRecordFromJMDB(SaveQuery);
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.txtCountryName.Text = "";
                    this.txtCountryName.Select();
                }
            }
            catch (Exception)
            {
                throw;
            }
            ShowRecord();
            this.txtCountryName.Focus();
        }

        private void txtCountryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 && Convert.ToInt16(e.KeyChar) < 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Update")
            {
                try
                {
                    if (txtCountryName.Text != "")
                    {
                        int value = Convert.ToInt32(this.lblCountryId.Text);
                        string UpdateQuery;
                        UpdateQuery = " UPDATE Country SET CountryName = '" + txtCountryName.Text + "' Where CountryId =" + value;
                        con.SaveRecordFromJMDB(UpdateQuery);
                        MessageBox.Show(Messages.Updated, Messages.Header);
                        this.btnEdit.Text = "&Edit";
                        this.txtCountryName.Text = "";
                        Refresh();
                    }
                    else
                    {
                        MessageBox.Show("Empty Country Name Will Not UpDated ", Messages.Header);
                        return;
                    }
                }
                catch (Exception)
                { }
            }
            ShowRecord();
        }

        public void Refresh()
        {
            this.btnSave.Enabled = true;
            this.btnEdit.Text = "&Edit";
            this.btnEdit.Enabled = false;
            this.btnDelete.Enabled = false;
            this.txtCountryName.Text = "";
            this.lblCountryId.Text = "";
            this.txtCountryName.Select();
        }

        private void cbxCountryList_MouseClick(object sender, MouseEventArgs e)
        {
            con = new JewelConnection();
            try
            {
                con.MyDataSet.Tables["TableCountry"].Rows.Clear();
            }
            catch (Exception)
            {
            }
            try
            {
                string query = "select * from Country";
                con.GetDataFromJMDB(query, "TableCountry");
                this.cbxCountryList.DataSource = con.MyDataSet.Tables["TableCountry"];
                this.cbxCountryList.DisplayMember = "CountryName";
                this.cbxCountryList.ValueMember = "CountryId";
                this.txtCountryName.Text = this.cbxCountryList.Text;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void txtCountryName_Leave_1(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtinfo = culInfo.TextInfo;
            string str = this.txtCountryName.Text;
            this.txtCountryName.Text = txtinfo.ToTitleCase(str);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = false;
            this.txtCountryName.Text = string.Empty;
            this.cbxCountryList.Text = string.Empty;
            this.btnSave.Enabled = true;
            this.cbxCountryList.Visible = false;
            this.lblCountryList.Visible = false;
            this.btnEdit.Text = "Edit";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            checkforDelete = new ItemDAL().isNameExist("select CountryId from CustomerInfo where CountryId='" + this.lblCountryId.Text + "'");
            if (this.txtCountryName.Text == "")
            {
                MessageBox.Show("Please Select Country", Messages.Header);
                return;
            }
            else if (checkforDelete == true)
            {
                MessageBox.Show(Messages.DeleteWarning, Messages.Header);
                Refresh();
                return;
            }
            else
            {
                try
                {
                    int value = Convert.ToInt32(this.lblCountryId.Text);
                    string DeleteQuery;
                    DeleteQuery = " delete from Country where CountryId =" + value;
                    con.DeleteRecordfromJMDB(DeleteQuery);
                    MessageBox.Show(Messages.Deleted, Messages.Header);
                    this.txtCountryName.Text = "";
                    this.cbxCountryList.Visible = false;
                    this.lblCountryList.Visible = false;
                    this.btnEdit.Text = "Edit";
                    Refresh();
                }
                catch (Exception ex0)
                {
                    throw ex0;
                }
            }
            ShowRecord();
        }

        private void cbxCountryList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.txtCountryName.Text = this.cbxCountryList.GetItemText(cbxCountryList.SelectedItem).ToString();
        }

        private void AddCountry_Load(object sender, EventArgs e)
        {
            this.btnDelete.Enabled = false;
            ShowRecord();
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void AddCountry_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        public void ShowRecord()
        {
            con = new JewelConnection();
            try
            {
                con.MyDataSet.Tables["TableCountry"].Rows.Clear();
            }
            catch (Exception)
            { }
            try
            {
                string query;
                query = "select * from Country";
                con.GetDataFromJMDB(query, "TableCountry");
                this.dgvCountryDetail.DataSource = con.MyDataSet.Tables["TableCountry"];
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgvCountryDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                int k = Convert.ToInt32(this.dgvCountryDetail.Rows[e.RowIndex].Cells[0].Value);
                this.txtCountryName.Text = this.dgvCountryDetail.Rows[e.RowIndex].Cells[1].Value.ToString();
                this.lblCountryId.Text = this.dgvCountryDetail.Rows[e.RowIndex].Cells[0].Value.ToString();

                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnEdit.Text = "&Update";
                this.btnEdit.Enabled = true;
            }
        }

        private void txtCountryName_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void btnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtCountryName.Focus();
        }
    }
}
