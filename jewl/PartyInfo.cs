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
    public partial class frmPartyInfo : Form
    {
        Party pt = new Party();
        List<Party> pts = new List<Party>();
        PartyDAL PDAL = new PartyDAL();
        //Customer cust;
        CountryDAL cDAL = new CountryDAL();
        //CustomerDAL custDAL = new CustomerDAL();
        //List<Customer> custs;
        GroupAccount g;
        SubGroupAccount sg;
        ParentAccount p;
        ChildAccount c=new ChildAccount();
        public bool bflag = false;
        int k;
        public frmPartyInfo()
        {
            InitializeComponent();
        }

       
        private void btnSave_Click(object sender, EventArgs e)
        {
            pt = new Party();
            if (this.txtPartyName.Text == "")
            {
                MessageBox.Show("Enter Party Name");
                return;
            }
            if (this.txtPartyAddress.Text == "")
            {
                MessageBox.Show("Enter Address");
                return;
            }
            if (this.txtMob.Text == "")
            {
                MessageBox.Show("Enter  Mobile No");
                return;
            }
            {
                pt.PCode = Convert.ToInt32(this.txtPartyCode.Text);
                pt.PName = this.txtPartyName.Text.ToString();
                pt.PAddress = this.txtPartyAddress.Text.ToString();
                pt.PtclNo = this.txtTel.Text.ToString();
                pt.PMob = this.txtMob.Text.ToString();
                pt.PEmail = this.txtEmail.Text.ToString();               
                    pt.AccountCode = new AccountDAL().CreateAccount(2, "Current Liability", pt.PName, "General Account", 0);             
                if (!String.IsNullOrEmpty(pt.AccountCode))
                {
                    PDAL.AddParty(pt);
                    MessageBox.Show("Record Saved Successfully");
                    RefreshRec();
                    this.ShowGrid();
                }
                else
                {
                    MessageBox.Show("Account Name Already Exist");
                    return;
                }
            }

        }

        private void frmPartyInfo_Load(object sender, EventArgs e)
        {
            this.ShowGrid();
        }
        private void  RefreshRec ()
        {
            this.txtPartyName.Text="";
            this.txtPartyAddress.Text="";
            this.txtTel.Text = "";
            this.txtMob.Text = "";
            this.txtEmail.Text = "";
            this.txtPartyCode.Text = Convert.ToString(PDAL.GetMaxPCode() + 1);
        }

        private void dgvPartyList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            k = Convert.ToInt32(dgvPartyList.Rows[e.RowIndex].Cells[0].Value);
            pt = new Party();
            pt = PDAL.GetRecByPartyId(k);
            this.btnEdit.Text = "Update";
            this.txtPartyCode.Text = pt.PCode.ToString();
            this.txtPartyName.Text = pt.PName.ToString();
            this.txtPartyAddress.Text = pt.PAddress.ToString();
            this.txtTel.Text = pt.PtclNo.ToString();
            this.txtMob.Text = pt.PMob.ToString();
            this.txtEmail.Text = pt.PEmail.ToString();
            this.btnDelete.Enabled = false;
            this.btnSave.Enabled = false;
            this.btnReset.Enabled = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (k == 0)
            {
                MessageBox.Show("Please Select the Record First");
                return;

            }
            if(this.btnEdit.Text=="Update")
            {
                pt = new Party();
                //pt.PCode = this.txtPartyCode.Text.ToString();
                pt.PName = this.txtPartyName.Text.ToString();
                pt.PAddress = this.txtPartyAddress.Text.ToString();
                pt.PtclNo = this.txtTel.Text.ToString();
                pt.PMob = this.txtMob.Text.ToString();
                pt.PEmail = this.txtEmail.Text.ToString();
                PDAL.UpdateParty(k, pt);
                MessageBox.Show("Record Updated");
                this.btnDelete.Enabled = true;
                this.btnSave.Enabled = true;
                this.btnReset.Enabled = true;
                this.btnEdit.Text = "&Edit";
                this.RefreshRec();
                this.ShowGrid();


            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowGrid()
        {
            this.txtPartyCode.Text = Convert.ToString(PDAL.GetMaxPCode() + 1);
            List<Party> party = PDAL.GetAllParties();
            if (party == null)
                return;
            else
            {
                this.dgvPartyList.AutoGenerateColumns = false;
                this.dgvPartyList.Rows.Clear();
                int cont = party.Count;
                this.dgvPartyList.Rows.Add(cont);
                for (int i = 0; i < cont; i++)
                {
                    this.dgvPartyList.Rows[i].Cells[0].Value = party[i].PCode.ToString();
                    this.dgvPartyList.Rows[i].Cells[1].Value = party[i].PName.ToString();
                    this.dgvPartyList.Rows[i].Cells[2].Value = party[i].PAddress.ToString();

                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (k == 0)
            {
                MessageBox.Show("Select a Record First");
                return;
            }
            else
            {
                PDAL.DeleteParty(k);
                this.ShowGrid();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.RefreshRec();
        }
        
    }
}
