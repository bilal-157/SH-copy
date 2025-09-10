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
    public partial class AddSample : Form
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
        ManageCustomer adcust;
        List<Customer> custs;
        List<Sample> tags;
        Customer cust;
        int sNo = 0;
        int c;
        string tagNo;
        int id;
        public AddSample()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }


        private void AddSample_Load(object sender, EventArgs e)
        {
            label2.Text = dtpSample.Text;
            this.cbxItemType.SelectedIndex = 0;
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            this.txtWeight.ReadOnly = true;
            this.cbxTagNumber.SelectedIndexChanged -= new EventHandler(cbxTagNumber_SelectedIndexChanged);
            this.txtSampleNo.Text = (smDAL.GetMaxSampleNo() + 1).ToString();
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.SelectedIndex == -1)
                return;
            else
            {
                int k = (int)this.cbxGroupItem.SelectedValue;
                this.RefreshRecord();
                FormControls.FillCombobox(cbxTagNumber, getTags(k), "TagNo", "StockId");
            }
        }

        private List<Stock> getTags(int id)
        {
            List<Stock> records = sDAL.GetAllTagNoByItemId(id);
            if (dgvItemAdded.Rows.Count <= 0 || records == null)
                return records;
            else
            {
                List<Stock> lstStock = new List<Stock>();
                if (lstStock.Count > 0)
                    lstStock.Clear();
                List<Stock> lstTag = new List<Stock>();
                List<int> lstInt = new List<int>();
                for (int i = 0; i < dgvItemAdded.Rows.Count; i++)
                {
                    string str = dgvItemAdded.Rows[i].Cells[0].Value.ToString();
                    foreach (Stock stk in records)
                    {
                        if (str.Equals(stk.TagNo))
                        {
                            lstTag.Add(stk);
                        }
                    }
                }
                foreach (Stock s in records)
                {
                    lstStock.Add(s);
                }
                foreach (Stock st1 in records)
                {
                    foreach (Stock s in lstTag)
                    {
                        if (st1.StockId == s.StockId)
                        {
                            lstStock.Remove(st1);
                        }
                    }
                }
                return lstStock;
            }
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
      && !char.IsDigit(e.KeyChar)
      && e.KeyChar != '.')
            {
                e.Handled = true;

            }
            else e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;

            }

            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;

            if (e.Handled == false)
            {
                string str;
                if (e.KeyChar == '\b')
                {
                    if (this.txtWeight.Text == "")
                        return;
                    str = this.txtWeight.Text;
                    int i = str.Length;
                    str = str.Remove(i - 1);
                    if (str == string.Empty)
                    {
                        decimal val1 = 0;
                        frm.RatiMashaTolaGeneral(val1);
                        this.lblRtmWeight.Text = "T-" + frm.Tola + "M-" + frm.Masha + "R" + frm.Ratti;
                    }
                    else
                    {
                        if (str == ".")
                        {
                            decimal val = 0;
                            frm.RatiMashaTolaGeneral(val);
                        }
                        else
                        {
                            frm.RatiMashaTolaGeneral(Convert.ToDecimal(str));
                            this.lblRtmWeight.Text = "T-" + frm.Tola + "M-" + frm.Masha + "R" + frm.Ratti;
                        }
                    }

                }
                else
                {
                    str = this.txtWeight.Text + e.KeyChar.ToString();
                    if (str == ".")
                    {
                        decimal val = 0;
                        frm.RatiMashaTolaGeneral(val);
                    }
                    else
                    {
                        decimal val = Convert.ToDecimal(str);
                        frm.RatiMashaTolaGeneral(val);// (Convert.ToInt32(this.txtWeight.Text));
                        this.lblRtmWeight.Text = "T-" + frm.Tola + "M-" + frm.Masha + "R" + frm.Ratti;
                    }
                }
            }
        }

        private void cbxTagNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxTagNumber.SelectedValue == null)
                return;
            else
            {
                Stock s = (Stock)this.cbxTagNumber.SelectedItem;
                this.ShowAllRecord(s.StockId);
            }
        }

        private void ShowAllRecord(int stkId)
        {
            if (stkId <= 0) return;
            else
            {
                stk = sDAL.GetStkBySockId(stkId);
                if (stk == null)
                    return;
                else
                {
                    string st = "";
                    if (stk.ItemType == ItemType.Gold)
                        st = "Gold";
                    else if (stk.ItemType == ItemType.Diamond)
                        st = "Diamond";
                    else if (stk.ItemType == ItemType.Silver)
                        st = "Silver";
                    else if (stk.ItemType == ItemType.Pladium)
                        st = "Pladium";
                    else
                        st = "Platinum";
                    for (int j = 0; j < cbxItemType.Items.Count; j++)
                    {
                        string str1 = (string)this.cbxItemType.Items[j];
                        if (st.Equals(str1))
                            this.cbxItemType.SelectedIndex = j;
                    }
                    cbxGroupItem.SelectedValue = stk.ItemName.ItemId;
                    if (stk.Qty.HasValue)
                    {
                        this.txtQty.Text = stk.Qty.ToString();
                        this.txtSmStQty.Text = stk.Qty.ToString();
                    }
                    else
                        this.txtQty.Text = "1";

                    txtKarrat.Text = stk.Karrat;
                    txtDescription.Text = stk.Description;
                    this.cbxWorkerName.SelectedValue = stk.WorkerName.ID;

                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.NetWeight == 0)
                        this.txtWeight.Text = "0";
                    else
                    {
                        this.txtWeight.Text = Convert.ToString(stk.NetWeight);
                        this.txtSmStWt.Text = Convert.ToString(stk.NetWeight);
                    }
                }
            }
        }

        private void ShowAllRecordByTag(string tagno)
        {

            if (tagno == "") return;
            else
            {
                stk = smDAL.GetStkByTagNo(tagno);
                if (stk == null)
                {
                    MessageBox.Show("TagNo not found", Messages.Header);
                    txtBarCode.Text = "";
                    return;
                }
                else
                {
                    if (stk.BarCode == "")
                    {
                        MessageBox.Show("TagNo not found", Messages.Header);
                        txtBarCode.Text = "";
                        return;
                    }
                    string st = "";
                    if (stk.ItemType == ItemType.Gold)
                        st = "Gold";
                    else if (stk.ItemType == ItemType.Diamond)
                        st = "Diamond";
                    else if (stk.ItemType == ItemType.Silver)
                        st = "Silver";
                    else if (stk.ItemType == ItemType.Pladium)
                        st = "Pladium";
                    else
                        st = "Platinum";
                    for (int j = 0; j < cbxItemType.Items.Count; j++)
                    {
                        string str1 = (string)this.cbxItemType.Items[j];
                        if (st.Equals(str1))
                            this.cbxItemType.SelectedIndex = j;
                    }

                    for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                    {
                        Item it = (Item)this.cbxGroupItem.Items[i];
                        if (stk.ItemName.ItemId == it.ItemId)
                            this.cbxGroupItem.SelectedIndex = i;

                    }
                    int k = (int)this.cbxGroupItem.SelectedValue;

                    if (stk.BarCode == "")
                    {
                        MessageBox.Show("TagNo not found", Messages.Header);
                        return;
                    }

                    this.cbxTagNumber.DisplayMember = "TagNo";
                    this.cbxTagNumber.ValueMember = "StockId";
                    this.cbxTagNumber.DataSource = sDAL.GetAllTagNoByItemId(stk.ItemName.ItemId);
                    this.cbxTagNumber.SelectedIndex = -1;

                    for (int i = 0; i < this.cbxTagNumber.Items.Count; i++)
                    {
                        Stock stk1 = (Stock)this.cbxTagNumber.Items[i];
                        if (stk.TagNo.Equals(stk1.TagNo))
                        {
                            this.cbxTagNumber.SelectedIndex = i;
                            break;
                        }
                    }

                    if (stk.Qty.HasValue)
                    {
                        this.txtQty.Text = stk.Qty.ToString();
                        this.txtSmStQty.Text = stk.Qty.ToString();
                    }
                    else
                        this.txtQty.Text = "1";

                    txtKarrat.Text = stk.Karrat;

                    this.cbxWorkerName.SelectedValue = stk.WorkerName.ID;

                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.NetWeight == 0)
                        this.txtWeight.Text = "";
                    else
                    {
                        this.txtWeight.Text = Convert.ToString(stk.NetWeight);
                        this.txtSmStWt.Text = Convert.ToString(stk.NetWeight);
                    }
                }
            }
        }

        private void cbxTagNumber_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxTagNumber.SelectedIndexChanged += new EventHandler(cbxTagNumber_SelectedIndexChanged);
        }

        private void btnChangeWeight_Click(object sender, EventArgs e)
        {
            this.txtWeight.ReadOnly = false;
            this.cbxTagNumber.SelectedIndexChanged += new EventHandler(cbxTagNumber_SelectedIndexChanged);
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new EventHandler(cbxGroupItem_SelectedIndexChanged);
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val;
            if (txtWeight.Text == "")
                val = 0;
            else
                val = Convert.ToDecimal(txtWeight.Text);
            frm.RatiMashaTolaGeneral(val);
            this.lblRtmWeight.Text = "T-" + frm.Tola + "M-" + frm.Masha + "R" + frm.Ratti;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (txtWeight.Text == "")
            {
                MessageBox.Show("Please select item to sample", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            for (int i = 0; i < dgvItemAdded.Rows.Count; i++)
            {
                if (!String.IsNullOrEmpty(tagNo))
                {
                    if (dgvItemAdded.Rows[i].Cells[0].Value.ToString() == this.tagNo)
                    {
                        MessageBox.Show("Duplicate TagNo", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (!String.IsNullOrEmpty(this.cbxTagNumber.Text))
                {
                    if (dgvItemAdded.Rows[i].Cells[0].Value.ToString() == this.cbxTagNumber.Text)
                    {
                        MessageBox.Show("Duplicate TagNo", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            this.CreateSample(sender, e);
            object[] values = new Object[2];
            if (cbxTagNumber.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select TagNo For Sample", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(tagNo))
                    values[0] = stk.TagNo.ToString();
                else
                    values[0] = tagNo;
            }
            tagNo = "";

            this.dgvItemAdded.Rows.Add(values);
            int j = this.dgvItemAdded.Rows.Count;
            this.RefreshRecord();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            this.txtWeight.ReadOnly = false;
        }


        private void CreateSample(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "Update")
            {
                SampleLineItem smli = new SampleLineItem();
                smli.Stock = new Stock();

                smli.SampleNo = Convert.ToInt32(this.txtSampleNo.Text);

                smli.SampleDate = Convert.ToDateTime(this.dtpSample.Value);
                if (this.txtBillBookNo.Text == "")
                    smli.BillBookNo = "";
                else
                    smli.BillBookNo = this.txtBillBookNo.Text;

                if (string.IsNullOrEmpty(tagNo))
                    smli.Stock.TagNo = stk.TagNo;
                else
                    smli.Stock.TagNo = tagNo;

                if (this.txtQty.Text == "")
                    smli.SQty = null;
                else
                {

                    smli.SQty = Convert.ToInt32(this.txtQty.Text);
                    this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtSmStQty.Text) - Convert.ToInt32(this.txtQty.Text));
                    if (Convert.ToInt32(this.txtHTQty.Text) < 0)
                    {
                        MessageBox.Show("Sample quantity is greater than Stock quantity", Messages.Header);
                        return;
                    }
                    else
                        smli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);
                }
                if (this.txtWeight.Text == "")
                    smli.SampleWt = null;
                else
                    smli.SampleWt = Convert.ToDecimal(this.txtWeight.Text);
                this.txtHTWt.Text = Convert.ToString(Convert.ToDecimal(this.txtSmStWt.Text) - Convert.ToDecimal(this.txtWeight.Text));
                smli.Stock.NetWeight = Convert.ToDecimal(this.txtHTWt.Text);

                if (this.txtDescription.Text == "")
                    smli.Description = "";
                else
                    smli.Description = this.txtDescription.Text;
                sam.AddLineItems(smli);
            }
            else
            {
                SampleLineItem sli = new SampleLineItem();
                sli.Stock = new Stock();
                sli.SampleNo = Convert.ToInt32(this.txtSampleNo.Text);
                sli.SampleDate = Convert.ToDateTime(this.dtpSample.Value);
                if (this.txtBillBookNo.Text == "")
                    sli.BillBookNo = "";
                else
                    sli.BillBookNo = this.txtBillBookNo.Text;
                if (string.IsNullOrEmpty(tagNo))
                    sli.Stock.TagNo = stk.TagNo;
                else
                    sli.Stock.TagNo = tagNo;
                if (this.txtQty.Text == "")
                    sli.SQty = 0;
                else
                {
                    sli.SQty = Convert.ToInt32(this.txtQty.Text);
                    this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtSmStQty.Text) - Convert.ToInt32(this.txtQty.Text));
                    if (Convert.ToInt32(this.txtHTQty.Text) < 0)
                    {
                        MessageBox.Show("Sample quantity is greater than Stock quantity", Messages.Header);
                        return;
                    }
                    else
                        sli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);
                }

                if (this.txtWeight.Text == "")
                    sli.SampleWt = 0;
                else
                    sli.SampleWt = Convert.ToDecimal(this.txtWeight.Text);
                this.txtHTWt.Text = Convert.ToString(Convert.ToDecimal(this.txtSmStWt.Text) - Convert.ToDecimal(this.txtWeight.Text));
                sli.Stock.NetWeight = Convert.ToDecimal(this.txtHTWt.Text);

                sli.Description = this.txtDescription.Text;
                sam.AddLineItems(sli);
            }
        }

        private void RefreshRecord()
        {
            this.txtBarCode.Text = "";
            this.cbxTagNumber.SelectedIndex = -1;
            this.txtWeight.Text = "";
            this.txtQty.Text = "";
            this.txtKarrat.Text = "";
            this.cbxWorkerName.SelectedIndex = -1;
            this.txtDescription.Text = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "&Edit")
            {
                if (this.lblCustId.Text == "")
                {
                    MessageBox.Show("Please select the customer", Messages.Header);
                    return;
                }
                else
                {
                    Customer cst = new Customer();
                    cst.ID = Convert.ToInt32(this.lblCustId.Text);
                    sam.Customer = cst;
                }
                smDAL.AddSample(sam);
                MessageBox.Show("Record saved successfully ", Messages.Header);
                this.RefreshRecord();
                this.txtBillBookNo.Text = "";
                this.txtAddress.Text = "";
                this.cbxCustomerName.SelectedIndex = -1;
                this.cbxContactNo.SelectedIndex = -1;
                this.dgvItemAdded.Rows.Clear();
                this.txtSampleNo.Text = (smDAL.GetMaxSampleNo() + 1).ToString();
            }
        }

        private void ShowTagNo(int sNo)
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
                    txtSampleNo.Text = tags[i].SampleNo.ToString();
                    txtBillBookNo.Text = tags[i].BillBookNo.ToString(); 
                    this.cbxCustomerName.SelectedIndexChanged += new EventHandler(cbxCustomerName_SelectedIndexChanged);
                    cbxCustomerName.SelectedValue = tags[i].Customer.ID;
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

                for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                {
                    Item it = (Item)this.cbxGroupItem.Items[i];
                    if (smli.Stock.ItemName.ItemId == it.ItemId)
                        this.cbxGroupItem.SelectedIndex = i;

                }
                int k = (int)this.cbxGroupItem.SelectedValue;

                this.cbxTagNumber.DataSource = sDAL.GetAllTagNoByItemId(k);
                this.cbxTagNumber.DisplayMember = "TagNo";
                this.cbxTagNumber.ValueMember = "StockId";


                for (int i = 0; i < this.cbxTagNumber.Items.Count; i++)
                {
                    Stock it = (Stock)this.cbxTagNumber.Items[i];
                    if (smli.Stock.TagNo == it.TagNo)
                        this.cbxTagNumber.SelectedIndex = i;

                }

                if (smli.SQty.HasValue)
                {
                    this.txtQty.Text = smli.SQty.ToString();
                    //this.txtSmStQty.Text = smli.SQty.ToString();
                }
                else
                    this.txtQty.Text = "1";

                this.dtpSample.Value = Convert.ToDateTime(smli.SampleDate);

                this.txtKarrat.Text = smli.Stock.Karrat;

                this.cbxWorkerName.SelectedValue = smli.Stock.WorkerName.ID;

                Customer cst = new Customer();
                int custId = Convert.ToInt32(smli.Customer.ID);
                cst = custDAL.GetCustomerById(custId);

                this.lblCustId.Text = Convert.ToString(smli.Customer.ID);

                this.txtDescription.Text = smli.Description.ToString();

                if (smli.SampleWt == 0)
                {

                    this.txtWeight.Text = "";
                }
                else
                    this.txtWeight.Text = Convert.ToString(smli.SampleWt);

                if (smli.Stock.NetWeight.HasValue)
                {
                    this.txtStockWt.Text = Convert.ToString(smli.Stock.NetWeight);
                    this.txtSmStWt.Text = Convert.ToString((smli.SampleWt) + (smli.Stock.NetWeight));
                }
                else
                    this.txtStockWt.Text = "0";
                if (smli.Stock.Qty.HasValue)
                {
                    this.txtStockQty.Text = Convert.ToString(smli.Stock.Qty);
                    this.txtSmStQty.Text = Convert.ToString((smli.SQty) + (smli.Stock.Qty));
                }
                else
                    this.txtStockQty.Text = "0";

                if (smli.BillBookNo.ToString() == "")
                    this.txtBillBookNo.Text = "";
                else
                    this.txtBillBookNo.Text = Convert.ToString(smli.BillBookNo);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                EditNo sno = new EditNo();
                sno.Text = "SampleNo";
                sno.LabelText = "Enter Sample No.";
                sno.Msg = "Enter Sample No. First";
                sno.ShowDialog();
                sNo = (int)sno.EditNum;
                this.ShowTagNo(sNo);
                this.btnEdit.Text = "&Update";
                return;
            }
            if (this.btnEdit.Text == "&Update")
            {
                sam.SampleNo = Convert.ToInt32(this.txtSampleNo.Text);

                if (this.lblCustId.Text == "")
                {
                    MessageBox.Show("Please select the customer", Messages.Header);
                    return;
                }
                else
                {
                    Customer cst = new Customer();
                    cst.ID = Convert.ToInt32(this.lblCustId.Text);
                    sam.Customer = cst;
                }
                sam.SampleDate = Convert.ToDateTime(this.dtpSample.Value);
                if (this.txtBillBookNo.Text == "")
                    sam.BillBookNo = "";
                else
                    sam.BillBookNo = this.txtBillBookNo.Text;
                smDAL.UpdateSampleByTagNo(Convert.ToInt32(this.txtSampleNo.Text), sam);
                MessageBox.Show(Messages.Updated, Messages.Header);
                this.RefreshRecord();
                this.txtBillBookNo.Text = "";
                this.txtAddress.Text = "";
                this.cbxCustomerName.SelectedIndex = -1;
                this.cbxContactNo.SelectedIndex = -1;
                this.btnSave.Enabled = true;
                this.btnEdit.Text = "&Edit";
                this.dgvItemAdded.Rows.Clear();
                this.txtSampleNo.Text = (smDAL.GetMaxSampleNo() + 1).ToString();
            }
        }

        private void RemoveSample(string tagNo)
        {
            foreach (SampleLineItem sli in sam.SampleLineItems)
            {
                if (tagNo.Equals(sli.Stock.TagNo))
                {
                    sam.RemoveLineItems(sli);
                    return;
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string tag = "";
            Int32 selectedRowCount = dgvItemAdded.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    tag = dgvItemAdded.SelectedRows[i].Cells[0].Value.ToString();
                    this.RemoveSample(tag);
                    dgvItemAdded.Rows.Remove(dgvItemAdded.Rows[i]);
                }
            }
        }

        private void txtBillBookNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;

            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 32 && Convert.ToInt16(e.KeyChar) != 46)
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            if (Convert.ToInt16(e.KeyChar) == 32 && (sender as TextBox).Text.IndexOf(' ') > -1)
                e.Handled = true;
        }

        private void txtBarCode_Enter(object sender, EventArgs e)
        {
            //CultureInfo culInfo = CultureInfo.CurrentCulture;
            //TextInfo txtInfo = culInfo.TextInfo;
            //string str = this.txtBarCode.Text;
            //this.txtBarCode.Text = txtInfo.ToUpper(str);
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && (Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ShowAllRecordByTag(txtBarCode.Text);
                this.txtBarCode.Text = "";
                //CultureInfo culInfo = CultureInfo.CurrentCulture;
                //TextInfo txtInfo = culInfo.TextInfo;
                //string str = this.txtBarCode.Text;
                //this.txtBarCode.Text = txtInfo.ToUpper(str);

            }
        }

        private void txtBarCode_Click(object sender, EventArgs e)
        {
            //this.rbtGold.Checked = true;
        }

        private void dgvItemAdded_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tagNo = "";
            this.RefreshRecord();

            if (this.btnEdit.Text == "Update")
            {
                this.cbxGroupItem.SelectedIndexChanged -= new EventHandler(cbxGroupItem_SelectedIndexChanged);
                this.cbxTagNumber.DataSource = smDAL.GetAllTagsBySampleNo(sNo);
                this.cbxTagNumber.DisplayMember = "TagNo";

                tagNo = dgvItemAdded.Rows[e.RowIndex].Cells[0].Value.ToString();
                id = Convert.ToInt32(dgvItemAdded.Rows[e.RowIndex].Cells[1].Value);
                this.ShowRecordByTag(tagNo);
                this.btnSave.Enabled = false;
                //this.btnEdit.Text = "Update";
                this.dgvItemAdded.Rows.RemoveAt(e.RowIndex);
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
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.txtAddress.Text = "";
            this.cbxWorkerName.SelectedIndex = -1;
            this.txtWeight.Text = "";
            this.txtStockWt.Text = "";
            this.txtStockQty.Text = "";
            this.txtSmStWt.Text = "";
            this.txtSmStQty.Text = "";
            this.txtQty.Text = "";
            this.txtKarrat.Text = "";
            this.txtHTWt.Text = "";
            this.txtHTQty.Text = "";
            this.txtDescription.Text = "";
            this.txtBillBookNo.Text = "";
            this.txtBarCode.Text = "";
            this.cbxGroupItem.SelectedIndex = -1;
            this.cbxTagNumber.SelectedIndex = -1;
            this.dgvItemAdded.Rows.Clear();
            this.btnEdit.Text = "Edit";
        }

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = new Customer();
            cust = (Customer)this.cbxCustomerName.SelectedItem;
            if (cust != null)
            {
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                this.cbxContactNo.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.Address;
            }
        }

        private void cbxContactNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = new Customer();
            cust = (Customer)this.cbxContactNo.SelectedItem;
            if (cust != null)
            {
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
                this.cbxCustomerName.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.Address;
            }
        }

        private void cbxCustomerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxCustomerName.SelectedIndexChanged += new EventHandler(cbxCustomerName_SelectedIndexChanged);
        }

        private void cbxContactNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxContactNo.SelectedIndexChanged += new EventHandler(cbxContactNo_SelectedIndexChanged);
        }

        void ShowCustomer()
        {
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");
            this.cbxCustomerName.SelectedValue = custDAL.GetCustId("select * from CustomerInfo where CustId = (select MAX(CustId) from CustomerInfo)");
            cust = (Customer)this.cbxCustomerName.SelectedItem;
            this.lblCustId.Text = cust.ID.ToString();
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            this.cbxContactNo.SelectedValue = cust.ID;
            this.txtAddress.Text = cust.Address;
        }

        private void cbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cust = (Customer)this.cbxCustomerName.SelectedItem;
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                this.cbxContactNo.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.Address;
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            ManageCustomer frm = new ManageCustomer();
            FormControls.FadeOut(this);
            frm.ShowDialog();
            FormControls.FadeIn(this);
            this.ShowCustomer();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            SampleReports frm = new SampleReports();
            frm.ShowDialog();
        }

        private void cbxGroupItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cbxGroupItem.SelectedIndex == -1)
                    return;
                else
                {
                    int k = (int)this.cbxGroupItem.SelectedValue;
                    this.RefreshRecord();
                    FormControls.FillCombobox(cbxTagNumber, getTags(k), "TagNo", "StockId");
                }
            }
        }

        private void cbxTagNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cbxTagNumber.SelectedValue == null)
                    return;
                else
                {
                    Stock s = (Stock)this.cbxTagNumber.SelectedItem;
                    this.ShowAllRecord(s.StockId);
                }
            }
        }
    }
}
       

        
    

