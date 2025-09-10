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
    public partial class AddStones : Form
    {

        JewelConnection con;
        public AddStones()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //::::::-_-_::::::Data Ko Save Karwaney K Leay::::::-_-_:::::://
        //--------------------------------------------------------------//

        private void btnSave_Click(object sender, EventArgs e)
        {
            con = new JewelConnection();

            //::::::-_-_::::::If Country Name All Ready Exist::::::-_-_::::::// (Part 1)
            //---------------------------------------------------------------//
            try
            {
                con.MyDataSet.Tables["TableCountry"].Rows.Clear();
            }
            catch (Exception)
            { }
            try
            {
                string query;
                query = "select * from stonestype";
                con.GetDataFromJMDB(query, "TableStone");
                int count = con.MyDataSet.Tables["TableStone"].Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (txtStoneType .Text == con.MyDataSet.Tables["TableStone"].Rows[i]["Name"].ToString())
                    {
                        MessageBox.Show("This Stone Type Already Exist", Messages.Header);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
            try
            {
                if (txtStoneType.Text == "")
                {
                    MessageBox.Show("First Enter Stone Type", Messages.Header);
                }
                else
                {
                    //::::::-_-_::::::To Save Data in Database ::::::-_-_:::::://   (Part 2)
                    //---------------------------------------------------------//   
                    string SaveQuery;
                    SaveQuery = " insert into StonesType values ('" + txtStoneType.Text + "')";
                    con.SaveRecordFromJMDB(SaveQuery);
                    MessageBox.Show("Name Saved Successfully", Messages.Header);
                    this.txtStoneType.Text = "";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(this.btnEdit.Text =="&Edit")
            {
            con = new JewelConnection();
            if (this.txtStoneType.Text.Length <= 0)
            {
                MessageBox.Show("Please Select FirstStone Type", Messages.Header);
            }
            this.btnEdit.Text = "Update";

            this.cbxStonesList .Visible = true;
            this.lblStonesList .Visible = true;
            this.btnSave.Enabled = false;
            //::::::-_-_::::::if Country Name All Ready Exist::::::-_-_:::::://
            //---------------------------------------------------------------//
            try
            {
                con.MyDataSet.Tables["TableStonesType"].Rows.Clear();
            }
            catch (Exception)
            { }
            try
            {
                string query;
                query = "select * from StonesType";
                con.GetDataFromJMDB(query, "TableStonesType");
                int count = con.MyDataSet.Tables["TableStonesType"].Rows.Count;

                for (int i = 0; i < count; i++)
                {
                    if (txtStoneType.Text == con.MyDataSet.Tables["TableStonesType"].Rows[i]["StoneType"].ToString())
                    {
                        MessageBox.Show("This StoneType Already Exist", Messages.Header);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
            }
               // {
                    //::::::-_-_::::::For UpdateCountry::::::-_-_:::::://
                    //-------------------------------------------------//
            else if(this.btnEdit.Text =="Update")
            {
                    try
                    {
                        int value = (int)this.cbxStonesList.SelectedValue;
                        string UpdateQuery;
                        UpdateQuery = " UPDATE StonesType SET Name = '" + txtStoneType.Text + "' Where StoneTypeId =" + value;
                        con.SaveRecordFromJMDB(UpdateQuery);
                        MessageBox.Show("StoneType Update Successfully", Messages.Header);
                        this.cbxStonesList .SelectedIndex = -1;
                        this.btnEdit.Text = "&Edit";
                        this.txtStoneType.Text = "";
                        this.cbxStonesList.Visible = false;
                        this.lblStonesList.Visible = false;
                    }
                    catch (Exception)
                    {
                    }
                }
            
        }
        //:::::::-_-_::::::Work On Reset Button:::::::-_-_:::::://
        //------------------------------------------------------//

        private void button1_Click(object sender, EventArgs e)
        {
            this.txtStoneType.Text = string.Empty;
            this.cbxStonesList.Text = string.Empty;
            this.btnSave.Enabled = true;
            this.cbxStonesList.Visible = false;
            this.lblStonesList.Visible = false;
            this.btnEdit.Text = "&Edit";
        }
        //:::::::-_-_::::::Deleted Record From DataBase:::::::-_-_:::::://
        //------------------------------------------------------//
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.cbxStonesList.Text == "")
            {
                MessageBox.Show("Please Select StoneType ");
                return; 
            }
            {
                try
                {
                    int value = (int)this.cbxStonesList.SelectedValue;
                    string DeleteQuery;
                    DeleteQuery = " delete from StonesType where StoneTypeId =" + value;
                    con.DeleteRecordfromJMDB(DeleteQuery);
                    MessageBox.Show("Record Deleted Successfully", Messages.Header);
                    this.txtStoneType.Text = "";
                    this.cbxStonesList.Visible = false;
                    this.lblStonesList.Visible = false;
                    this.btnEdit.Text = "&Edit";
                }
                catch (Exception ex0)
                {
                    throw ex0;
                }
            }
        }
        private void cbxStonesList_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.txtStoneType.Text = this.cbxStonesList.GetItemText(cbxStonesList.SelectedItem).ToString();
        }

        private void txtStoneType_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtinfo = culInfo.TextInfo;
            string str = this.txtStoneType.Text;
            this.txtStoneType.Text = txtinfo.ToTitleCase(str);
        }

        private void txtStoneType_KeyPress(object sender, KeyPressEventArgs e)
        {
              if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 && Convert.ToInt16(e.KeyChar) < 122) && Convert.ToInt16(e.KeyChar) != 8)
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void cbxStonesList_MouseClick(object sender, MouseEventArgs e)
        {
            con = new JewelConnection();
            try
            {
                con.MyDataSet.Tables["TableStonesType"].Rows.Clear();
            }
            catch (Exception)
            {
            }
            try
            {
                string query;
                query = "select * from StonesType";
                con.GetDataFromJMDB(query, "TableStonesType");
                this.cbxStonesList.DataSource = con.MyDataSet.Tables["TableStonesType"];
                this.cbxStonesList.DisplayMember = "Name";
                this.cbxStonesList.ValueMember = "StoneTypeId";
                this.txtStoneType.Text = this.cbxStonesList.Text;
            }
            catch (Exception)
            {
                throw;
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

        private void AddStones_Load(object sender, EventArgs e)
        {
            UserRights ur = new UserRights();
            string str;
            this.txtStoneType.Select();
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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        
        }
    }

