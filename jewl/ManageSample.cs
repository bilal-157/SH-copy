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
    public partial class ManageSample : Form
    {
        ItemDAL itmDAL = new ItemDAL();
        Formulas frm = new Formulas();
        StockDAL sDAL = new StockDAL();
        Stock stk = new Stock();
        DesignDAL dDAL = new DesignDAL();
        WorkerDAL wDAL = new WorkerDAL();
        SampleDAL smDAL = new SampleDAL();
        Sample sam = new Sample();
        SampleLineItem smli = new SampleLineItem();
        CustomerDAL custDAL = new CustomerDAL();
        List<Sample> tags;
        Customer cust;
        int totalQty;
        string tagNo;
        List<SampleLineItem> slis;
        public ManageSample()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void ManageSample_Load(object sender, EventArgs e)
        {
            this.tabControl1.TabPages.Remove(tbSampleGiven);
            this.cbxSearch.SelectedIndexChanged += new EventHandler(cbxSearch_SelectedIndexChanged);
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxSearch, new SampleDAL().GetAllSampleCustomers(), "Name", "ID");

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        public void ShowTagNo(int sNo)
        {
            tags = smDAL.GetAllTagsBySampleNo(sNo);
            if (tags == null)
                return;
            else
            {
                this.dgvItemAdded.AutoGenerateColumns = false;
                this.dgvItemAdded.Rows.Clear();
                int count = tags.Count;
                this.dgvItemAdded.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvItemAdded.Rows[i].Cells[0].Value = tags[i].TagNum.ToString();
                }
            }
        }

        private void ShowRecordByTag(string tagno)
        {
            smli = smDAL.GetSampleByTagNo(tagno);
            if (smli == null)
                return;
            else
            {
                this.txtSampleNo.Text = Convert.ToString(smli.SampleNo);

                string st = "";
                if (smli.Stock.ItemType == ItemType.Gold)
                    st = "Gold";
                else if (smli.Stock.ItemType == ItemType.Diamond)
                    st = "Diamond";
                else if (smli.Stock.ItemType == ItemType.Silver)
                    st = "Silver";
                else if (smli.Stock.ItemType == ItemType.Pladium)
                    st = "Pladium";
                else
                    st = "Platinum";
                for (int j = 0; j < cbxItemType.Items.Count; j++)
                {
                    string str1 = (string)this.cbxItemType.Items[j];
                    if (st.Equals(str1))
                        this.cbxItemType.SelectedIndex = j;
                }
                this.cbxGroupItem.SelectedIndex = smli.Stock.ItemName.ItemId;

                if (Convert.ToInt32(smli.SQty) - Convert.ToInt32(smli.ReturnQty) != 0)
                {
                    this.txtQty.Text = ((smli.SQty) - (smli.ReturnQty)).ToString();
                    this.txtSampleQty.Text = (smli.ReturnQty).ToString();
                }
                else
                    this.txtQty.Text = "0";

                this.txtKarrat.Text = smli.Stock.Karrat;
                this.cbxWorkerName.SelectedValue = smli.Stock.WorkerName.ID;
                this.lblCustId.Text = Convert.ToString(smli.Customer.ID);                
                this.txtDescription.Text = smli.Description.ToString();
                if (Convert.ToDecimal(smli.SampleWt) - Convert.ToDecimal(smli.ReturnWt) != 0)
                {
                    this.txtWeight.Text = Convert.ToString((smli.SampleWt) - (smli.ReturnWt));
                    this.txtSampleWt.Text = Convert.ToString(smli.ReturnWt);
                }
                else
                    this.txtWeight.Text = "0";
                if (smli.Stock.NetWeight.HasValue)
                {
                    this.txtStockWt.Text = Convert.ToString(smli.Stock.NetWeight);
                    this.txtSmStWt.Text = Convert.ToString(((smli.SampleWt) - (smli.ReturnWt)) + (smli.Stock.NetWeight));
                }
                else
                    this.txtStockWt.Text = "0";
                if (smli.Stock.Qty.HasValue)
                {
                    this.txtStockQty.Text = Convert.ToString(smli.Stock.Qty);
                    this.txtSmStQty.Text = Convert.ToString(((smli.SQty) - (smli.ReturnQty)) + (smli.Stock.Qty));
                    totalQty = Convert.ToInt32(this.txtSmStQty.Text);
                }
                else
                    this.txtStockQty.Text = "0";

                this.txtBillBookNo.Text = Convert.ToString(smli.BillBookNo);
            }
        }

        private void cbxSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = (Customer)this.cbxSearch.SelectedItem;
        }

        private void cbxSearch_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxSearch.SelectedIndexChanged +=new EventHandler(cbxSearch_SelectedIndexChanged);
        }

        private void cbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ShowSampleNo((int)cust.ID);
        }

        private void ShowSampleNo(int id)
        {
            slis = smDAL.GetSampleNoByCust(id);
            if (slis == null)
                return;
            else
            {
                this.dgvSampleNo.AutoGenerateColumns = false;
                this.dgvSampleNo.Rows.Clear();
                int count = slis.Count;
                this.dgvSampleNo.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvSampleNo.Rows[i].Cells[0].Value = slis[i].SampleNo.ToString();
                    this.dgvSampleNo.Rows[i].Cells[1].Value = slis[i].SampleDate.ToString();
                }
            }
        }

        private void dgvSampleNo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.ShowTagNo(Convert.ToInt32(dgvSampleNo.Rows[e.RowIndex].Cells[0].Value));
            this.txtSampleNo.Text = dgvSampleNo.Rows[e.RowIndex].Cells[0].Value.ToString();
        }

        private void txtSampleNo_KeyUp(object sender, KeyEventArgs e)
        {
            this.ShowTagNo(Convert.ToInt32(txtSampleNo.Text));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (totalQty == Convert.ToInt32(this.txtStockQty.Text) + Convert.ToInt32(this.txtQty.Text))
            {
                smli.SampleNo = Convert.ToInt32(this.txtSampleNo.Text);
                smli.Customer = new Customer();
                if (this.lblCustId.Text == "")
                    smli.Customer = null;
                else
                    smli.Customer.ID = Convert.ToInt32(this.lblCustId.Text);

                smli.SampleDate = Convert.ToDateTime(this.dtpDate.Value);
                if (this.txtBillBookNo.Text == "")
                    smli.BillBookNo = null;
                else
                    smli.BillBookNo = this.txtBillBookNo.Text;
                if (this.txtQty.Text == "")
                    smli.ReturnQty = 0;
                else
                    smli.ReturnQty = Convert.ToInt32(this.txtSampleQty.Text) + Convert.ToInt32(this.txtQty.Text);
                this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtStockQty.Text) + Convert.ToInt32(this.txtQty.Text));
                smli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);
                if (this.txtWeight.Text == "")
                    smli.ReturnWt = 0;
                else
                    smli.ReturnWt = Convert.ToDecimal(this.txtSampleWt.Text) + Convert.ToDecimal(this.txtWeight.Text);
                this.txtHTWt.Text = Convert.ToString(Convert.ToDecimal(this.txtStockWt.Text) + Convert.ToDecimal(this.txtWeight.Text));
                smli.Stock.NetWeight = Convert.ToDecimal(this.txtHTWt.Text);
                smli.ReturnDate = this.dtpDate.Value;
                if (this.txtDescription.Text == "")
                    smli.Description = "";
                else
                    smli.Description = this.txtDescription.Text;

                smDAL.SampleReturn(tagNo, smli);
                MessageBox.Show("Record saved successfully", Messages.Header);
                this.RefreshRecord();
            }
            else
            {
                smli.SampleNo = Convert.ToInt32(this.txtSampleNo.Text);
                smli.Customer = new Customer();
                if (this.lblCustId.Text == "")
                    smli.Customer = null;
                else
                    smli.Customer.ID = Convert.ToInt32(this.lblCustId.Text);

                smli.SampleDate = Convert.ToDateTime(this.dtpDate.Value);
                if (this.txtBillBookNo.Text == "")
                    smli.BillBookNo = null;
                else
                    smli.BillBookNo = this.txtBillBookNo.Text;

                if (this.txtQty.Text == "")
                    smli.ReturnQty = 0;
                else
                    smli.ReturnQty = Convert.ToInt32(this.txtSampleQty.Text) + Convert.ToInt32(this.txtQty.Text);

                this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtStockQty.Text) + Convert.ToInt32(this.txtQty.Text));
                smli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);

                if (this.txtWeight.Text == "")
                    smli.ReturnWt = null;
                else
                    smli.ReturnWt = Convert.ToDecimal(this.txtSampleWt.Text) + Convert.ToDecimal(this.txtWeight.Text);
                this.txtHTWt.Text = Convert.ToString(Convert.ToDecimal(this.txtStockWt.Text) + Convert.ToDecimal(this.txtWeight.Text));
                smli.Stock.NetWeight = Convert.ToDecimal(this.txtHTWt.Text);

                if (this.txtDescription.Text == "")
                    smli.Description = "";
                else
                    smli.Description = this.txtDescription.Text;

                smDAL.ReturnSampleByTagNo(tagNo, smli);
                MessageBox.Show("Record saved successfully", Messages.Header);
                this.RefreshRecord();
            }
        }

        private void RefreshRecord()
        {
            this.txtSampleNo.Text = "";
            this.txtBillBookNo.Text = "";
            this.txtWeight.Text = "";
            this.txtQty.Text = "";
            this.txtKarrat.Text = "";
            this.cbxWorkerName.SelectedIndex = -1;
            this.txtDescription.Text = "";
        }

        private void dgvItemAdded_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tagNo = dgvItemAdded.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.ShowRecordByTag(tagNo);
            this.dgvItemAdded.Rows.RemoveAt(e.RowIndex);
        }
    }
}
