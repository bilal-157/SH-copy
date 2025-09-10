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
    public partial class SampleReturn : Form
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
        List<Customer> custs;
        List<Sample> tags;
        Customer cust;
        int totalQty;
        string tagNo;
        public SampleReturn()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void SampleReturn_Load(object sender, EventArgs e)
        {
            
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

        private void dgvItemAdded_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            tagNo = dgvItemAdded.Rows[e.RowIndex].Cells[0].Value.ToString();
            this.ShowRecordByTag(tagNo);
            this.dgvItemAdded.Rows.RemoveAt(e.RowIndex);
        }

        private void ShowRecordByTag(string tagno)
        {
            smli = smDAL.GetSampleByTagNo(tagno);
            if (smli == null)
                return;
            else
            {
                this.txtSampleNo.Text = Convert.ToString(smli.SampleNo);

                if (smli.Stock.ItemType == ItemType.Gold)
                    this.txtItemType.Text = "Gold";
                else if (smli.Stock.ItemType == ItemType.Diamond)
                    this.txtItemType.Text = "Diamond";
                else if (smli.Stock.ItemType == ItemType.Silver)
                    this.txtItemType.Text = "Silver";
                else if (smli.Stock.ItemType == ItemType.Pladium)
                    this.txtItemType.Text = "Pladium";
                else
                    this.txtItemType.Text = "Platinum";
                this.txtGroupItem.Text = Convert.ToString(smli.Stock.ItemName.ItemName);
                if (Convert.ToInt32(smli.SQty) - Convert.ToInt32(smli.ReturnQty) != 0)
                {
                    this.txtQty.Text = ((smli.SQty) - (smli.ReturnQty)).ToString();
                    this.txtSampleQty.Text = (smli.ReturnQty).ToString();
                }
                else
                    this.txtQty.Text = "0";
                txtTagNumber.Text = smli.Stock.TagNo;
                this.txtDate.Text = Convert.ToDateTime(smli.SampleDate).ToString("dd:MMM:yy");
                this.txtKarrat.Text = smli.Stock.Karrat;
                Design d = new Design();
                int dId = Convert.ToInt32(smli.Stock.DesignNo.DesignId);
                if (dId != 0)
                {
                    d = dDAL.GetDesignById(dId);
                    txtDesign.Text = d.DesignNo.ToString();
                }               
                Worker wrk = new Worker();
                int workId = Convert.ToInt32(smli.Stock.WorkerName.ID);
                if (workId != 0)
                {
                    wrk = wDAL.GetWorkerById(workId);
                    this.txtWorkerName.Text = wrk.Name.ToString();
                }
                Customer cst = new Customer();
                int custId = Convert.ToInt32(smli.Customer.ID);
                cst = custDAL.GetCustomerById(custId);

                this.lblCustId.Text = Convert.ToString(smli.Customer.ID);
                string fn = cst.Name.ToString();
                if (string.IsNullOrEmpty(fn))
                    this.txtDetailCustomerName.Text = "";
                else
                    this.txtDetailCustomerName.Text = cst.Name.ToString();

                string st = cst.Mobile.ToString();
                if (string.IsNullOrEmpty(st))
                    this.txtContactNo.Text = cst.CNIC.ToString();
                else
                    this.txtContactNo.Text = cst.Mobile.ToString();

                string em = cst.Email.ToString();
                if (string.IsNullOrEmpty(em))
                    this.txtEmail.Text = "";
                else
                    this.txtEmail.Text = cst.Email.ToString();

                string ad = cst.Address.ToString();
                if (string.IsNullOrEmpty(ad))
                    this.txtAddress.Text = "";
                else
                    this.txtAddress.Text = cst.Address.ToString();
                txtKarrat.Text = smli.Stock.Karrat;
                this.txtDescription.Text = smli.Description.ToString();

                if (Convert.ToDecimal(smli.SampleWt) - Convert.ToDecimal(smli.ReturnWt) != 0)
                {
                    this.txtWeight.Text = Convert.ToString(Math.Round((decimal)(smli.SampleWt - smli.ReturnWt), 3));
                    this.txtSampleWt.Text = Convert.ToString(Math.Round((decimal)smli.ReturnWt, 3));
                }
                else
                    this.txtWeight.Text = "0";
                if (smli.Stock.NetWeight.HasValue)
                {
                    this.txtStockWt.Text = Convert.ToString(Math.Round((decimal)smli.Stock.NetWeight, 3));
                    this.txtSmStWt.Text = Convert.ToString(Math.Round((decimal)((smli.SampleWt - smli.ReturnWt) + smli.Stock.NetWeight), 3));
                }
                else
                    this.txtStockWt.Text = "0";
                if (smli.Stock.Qty.HasValue)
                {
                    this.txtStockQty.Text = Convert.ToString(smli.Stock.Qty);
                    this.txtSmStQty.Text = Convert.ToString(((smli.SQty)-(smli.ReturnQty)) + (smli.Stock.Qty));
                    totalQty = Convert.ToInt32(this.txtSmStQty.Text);
                }
                else
                    this.txtStockQty.Text = "0";
                if (smli.Stock.WastePercent.HasValue)
                    this.txtWasteInPercent.Text = Convert.ToString(Math.Round((decimal)smli.Stock.WastePercent, 1));
                else
                    this.txtWasteInPercent.Text = "";
                if (smli.Stock.WasteInGm.HasValue)
                    this.txtWasteInGm.Text = Convert.ToString(Math.Round((decimal)smli.Stock.WasteInGm, 3));
                else
                    this.txtWasteInGm.Text = "";
                this.txtTotalWeight.Text = Convert.ToString(Math.Round((decimal)smli.Stock.TotalWeight, 3));
                if (smli.Stock.MakingPerGm.HasValue)

                    this.txtMakingPerGm.Text = Convert.ToString(Math.Round((decimal)smli.Stock.MakingPerGm, 1));
                else
                    this.txtMakingPerGm.Text = "";
                if (smli.Stock.TotalMaking.HasValue)
                    this.txtTotalMaking.Text = Convert.ToString(Math.Round((decimal)smli.Stock.TotalMaking, 0));
                else
                    this.txtTotalMaking.Text = "";
                if (smli.Stock.LakerGm.HasValue)
                    this.txtLakerGm.Text = Convert.ToString(Math.Round((decimal)smli.Stock.LakerGm, 1));
                else
                    this.txtLakerGm.Text = "";
                if (smli.Stock.TotalLaker.HasValue)
                    this.txtTotalLacker.Text = Convert.ToString(Math.Round((decimal)smli.Stock.TotalLaker, 0));
                else
                    this.txtTotalLacker.Text = "";
                this.txtBillBookNo.Text = Convert.ToString(smli.BillBookNo);
                if (smli.Stock.StoneList == null)
                    return;
                else
                {
                    this.dgvStonesDetail.AutoGenerateColumns = false;
                    int count = smli.Stock.StoneList.Count;
                    this.dgvStonesDetail.Rows.Add(count);
                    for (int i = 0; i < smli.Stock.StoneList.Count; i++)
                    {
                        if (!(string.IsNullOrEmpty(smli.Stock.StoneList[i].StoneTypeName)))
                            dgvStonesDetail.Rows[i].Cells[1].Value = smli.Stock.StoneList[i].StoneTypeName;
                        if (!(string.IsNullOrEmpty(smli.Stock.StoneList[i].StoneName.ToString())))

                            this.dgvStonesDetail.Rows[i].Cells[0].Value = smli.Stock.StoneList[i].StoneName;
                        if (smli.Stock.StoneList[i].StoneWeight.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[1].Value = Convert.ToDecimal(smli.Stock.StoneList[i].StoneWeight);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[1].Value = string.Empty;
                        if (smli.Stock.StoneList[i].Qty.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[2].Value = Convert.ToInt32(smli.Stock.StoneList[i].Qty);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[2].Value = string.Empty;
                        if (smli.Stock.StoneList[i].Rate.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(smli.Stock.StoneList[i].Rate), 1);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                        if (smli.Stock.StoneList[i].Price.HasValue)
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = Math.Round(Convert.ToDecimal(smli.Stock.StoneList[i].Price), 0);
                        else
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                        if (!(string.IsNullOrEmpty(smli.Stock.StoneList[i].ColorName.ColorName.ToString())))
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = smli.Stock.StoneList[i].ColorName.ColorName.ToString();
                        if (!(string.IsNullOrEmpty(smli.Stock.StoneList[i].CutName.CutName)))
                            dgvStonesDetail.Rows[i].Cells[6].Value = smli.Stock.StoneList[i].CutName.CutName.ToString();
                        if (!(string.IsNullOrEmpty(smli.Stock.StoneList[i].ClearityName.ClearityName.ToString())))
                            this.dgvStonesDetail.Rows[i].Cells[7].Value = smli.Stock.StoneList[i].ClearityName.ClearityName.ToString();
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearchSamNo_Click(object sender, EventArgs e)
        {
            this.ShowTagNo(Convert.ToInt32(this.txtSampleNo.Text));
            if (this.dgvItemAdded.Rows.Count < 2)
            {
                MessageBox.Show("Please select a record from Selected Items", Messages.Header);
                return;
            }
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
                smli.BillBookNo = this.txtBillBookNo.Text;
                if (this.txtQty.Text == "")
                    smli.ReturnQty = 0;
                else
                    smli.ReturnQty =Convert.ToInt32(this.txtSampleQty.Text)+ Convert.ToInt32(this.txtQty.Text);
                this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtStockQty.Text) + Convert.ToInt32(this.txtQty.Text));
                smli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);
                if (this.txtWeight.Text == "")
                    smli.ReturnWt = 0;
                else
                    smli.ReturnWt = Math.Round(Convert.ToDecimal(this.txtSampleWt.Text) + Convert.ToDecimal(this.txtWeight.Text), 3);
                this.txtHTWt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(this.txtStockWt.Text) + Convert.ToDecimal(this.txtWeight.Text), 3));
                smli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtHTWt, 3);
                smli.ReturnDate = this.dtpDate.Value;
                if (this.txtDescription.Text == "")
                    smli.Description = "";
                else 
                    smli.Description = this.txtDescription.Text;              
                smDAL.SampleReturn(tagNo, smli);
                MessageBox.Show("Record saved successfully", Messages.Header);
                frmSampleReturn frm = new frmSampleReturn();
                frm.selectQuery = "{SampleReport.TagNo} = '" + tagNo + "' and {SampleReport.SampleNo} = " + smli.SampleNo;
                frm.ShowDialog();
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
                smli.BillBookNo = this.txtBillBookNo.Text;
                if (this.txtQty.Text == "")
                    smli.ReturnQty = 0;
                else
                    smli.ReturnQty = Convert.ToInt32(this.txtSampleQty.Text) + Convert.ToInt32(this.txtQty.Text);                
                this.txtHTQty.Text = Convert.ToString(Convert.ToInt32(this.txtStockQty.Text) +Convert.ToInt32(this.txtQty.Text));
                smli.Stock.Qty = Convert.ToInt32(this.txtHTQty.Text);
                if (this.txtWeight.Text == "")
                    smli.ReturnWt = null;
                else
                    smli.ReturnWt = Math.Round(Convert.ToDecimal(this.txtSampleWt.Text) + Convert.ToDecimal(this.txtWeight.Text), 3);
                this.txtHTWt.Text = Convert.ToString(Math.Round(Convert.ToDecimal(this.txtStockWt.Text) + Convert.ToDecimal(this.txtWeight.Text), 3));
                smli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtHTWt, 3);
                if (this.txtDescription.Text == "")
                    smli.Description = "";
                else
                    smli.Description = this.txtDescription.Text;
                smDAL.ReturnSampleByTagNo(tagNo, smli);
                MessageBox.Show("Record saved successfully", Messages.Header);
                frmSampleReturn frm = new frmSampleReturn();
                frm.selectQuery = "{SampleReport.TagNo} = '" + tagNo + "' and {SampleReport.SampleNo} = " + smli.SampleNo;
                frm.ShowDialog();
                this.RefreshRecord();
            }
        }

        private void RefreshRecord()
        {
            this.txtCustomerName.Text = "";
            this.txtSampleNo.Text = "";
            this.txtBillBookNo.Text = "";
            this.txtDetailCustomerName.Text = "";
            this.txtContactNo.Text = "";
            this.txtEmail.Text = "";
            this.txtAddress.Text = "";
            this.txtItemType.Text = "";
            this.txtGroupItem.Text = "";
            this.txtTagNumber.Text = "";
            this.txtWeight.Text = "";
            this.txtTola.Text = "";
            this.txtMasha.Text = "";
            this.txtRatti.Text = "";
            this.txtDesign.Text = "";
            this.txtQty.Text = "";
            this.txtKarrat.Text = "";
            this.txtDate.Text = "";
            this.txtWorkerName.Text = "";
            this.txtDescription.Text = "";
            this.txtWasteInPercent.Text = "";
            this.txtWasteInGm.Text = "";
            this.txtTotalWeight.Text = "";
            this.txtLakerGm.Text = "";
            this.txtTotalLacker.Text = "";
            this.txtGoldRate.Text = "";
            this.txtGoldPrice.Text = "";
            this.txtMakingPerGm.Text = "";
            this.txtTotalMaking.Text = "";
            this.txtGrossWeight.Text = "";
            this.txtTotalPrice.Text = "";
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void txtSampleNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ShowTagNo(Convert.ToInt32(this.txtSampleNo.Text));
                if (this.dgvItemAdded.Rows.Count < 2)
                {
                    MessageBox.Show("Please select a record from Selected Items", Messages.Header);
                    return;
                }
            }
        }

        private void btnCustomerName_Click(object sender, EventArgs e)
        {
            SampledCustomer smcust = new SampledCustomer();
            smcust.ShowDialog();
            int sNo = (int)smcust.SampleNo;
            this.ShowTagNo(sNo);
            this.txtSampleNo.Text = sNo.ToString();
        }

        private void txtCustomerName_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtSampleNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            txtSampleNo_KeyDown(sender, e);
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val = FormControls.GetDecimalValue(this.txtWeight, 3);
            frm.RatiMashaTolaGeneral(val);
            this.txtTola.Text = frm.Tola.ToString();
            this.txtMasha.Text = frm.Masha.ToString();
            this.txtRatti.Text = frm.Ratti.ToString();
        }

        private void txtGoldRate_KeyUp(object sender, KeyEventArgs e)
        {
            TotalPrice();
        }

        private void TotalPrice()
        {
            decimal val, val1;
            val = FormControls.GetDecimalValue(txtTotalWeight, 3) * FormControls.GetDecimalValue(txtGoldRate, 1);
            val1 = FormControls.GetDecimalValue(txtTotalLacker, 0) + FormControls.GetDecimalValue(txtTotalMaking, 0);
            txtTotalPrice.Text = Math.Round((val + val1), 0).ToString();
        }

        private void txtTotalLacker_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }

        private void txtTotalMaking_TextChanged(object sender, EventArgs e)
        {
            TotalPrice();
        }
    }
}
