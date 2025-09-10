using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class GoldPurchase : Form
    {
        SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        Gold pg = new Gold();
        List<Gold> PGL = new List<Gold>();
        PurchaseGoldDAL pgDAL = new PurchaseGoldDAL();
        private AccountDAL aDAL = new AccountDAL();
        AccountDAL acDAL = new AccountDAL();
        VouchersDAL vcDAL = new VouchersDAL();
        ManageCustomer adcust = new ManageCustomer();
        List<Customer> custs = new List<Customer>();
        CustomerDAL custDAL = new CustomerDAL();
        GoldRateDAL grDAL = new GoldRateDAL();
        Customer cust = new Customer();
        Voucher vchr = new Voucher();
        Formulas frm = new Formulas();
        GroupAccount g;
        SubGroupAccount sg;
        ParentAccount p;
        ChildAccount c;
        ChildAccount chld = new ChildAccount();
        SaleManDAL smDAL = new SaleManDAL();
        ItemDAL itmDAL = new ItemDAL();
        int j = 0;
        int k = 0;
        string vn;

        public GoldPurchase()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.txtAddress.Multiline = true;
        }

        private void GoldPurchase_Load(object sender, EventArgs e)
        {
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            
            this.ShowDGV();
            this.RefreshRec();
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");
            FormControls.FillCombobox(cbxSaleMan, smDAL.GetAllSaleMen(), "Name", "ID");
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            cbxSaleMan.Select();
            this.txtPurchaseNo.Text = (pgDAL.GetMaxGPNO() + 1).ToString();
            this.rbtUsedGold.Checked = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbxCustomerName.Text == "")
                {
                    MessageBox.Show("Please Select Customer", Messages.Header);
                    this.cbxCustomerName.Select();
                    return;
                }
                if (this.dgvDetail.Rows.Count <=1)
                {
                    MessageBox.Show("first enter weight then add", Messages.Header);
                    this.txtWeight.Focus();
                    return;
                }
                con1.Open();
                trans = con1.BeginTransaction();
                for (int i = 0; i < this.dgvDetail.Rows.Count - 1; i++)
                {
                    pg.PGDate = Convert.ToDateTime(this.dtpPGDate.Value);
                    pg.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                    pg.Weight = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[1].Value), 3);
                    pg.Kaat = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[2].Value), 1);
                    pg.PWeight = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[3].Value), 3);
                    pg.Karat = this.dgvDetail.Rows[i].Cells[4].Value.ToString();
                    pg.Rate = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[5].Value), 1);
                    pg.Amount = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[6].Value), 0);                   
                    pg.Description = this.dgvDetail.Rows[i].Cells[7].Value.ToString();
                    pg.RemainingWork = "";
                    if (this.dgvDetail.Rows[i].Cells[8].Value.ToString() == "UsedGold")
                        pg.GoldType = GoldType.Used;
                    if (this.dgvDetail.Rows[i].Cells[8].Value.ToString() == "PureGold")
                        pg.GoldType = GoldType.Pure;
                    pg.item = new Item();
                    pg.item.ItemId =Convert.ToInt32( this.dgvDetail.Rows[i].Cells[9].Value);
                    pg.SaleMan = new SaleMan();
                    if (this.cbxSaleMan.SelectedIndex == -1)
                        pg.SaleMan.ID = 0;
                    else
                        pg.SaleMan = (SaleMan)cbxSaleMan.SelectedItem;
                    pg.CustId = Convert.ToInt32(this.lblCustId.Text);
                    pg.VNO = "GPV";
                    pg.PTime = "Gold Purchase";
                    pg.PMode = "Rec";
                    this.pgDAL.AddGoldDetail(pg, con1, trans);
                }
                ChildAccount cha = null;
                if (this.rbtPureGold.Checked == true)
                {
                    #region A
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                    if (cha==null)
                    {
                        pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con1, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                    }                                  
                    #endregion
                }
                if (this.rbtUsedGold.Checked == true)
                {
                    #region B
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                    if (cha == null)
                    {
                        pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con1, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                    }
                    #endregion
                }
                string str = "GPV";
                pg.ONO = 0;
                pg.SNO = 0;
                pg.VNO = vcDAL.CreateVNO(str, con1, trans);
                pg.RemainingWork = "";
                vchr.AccountCode = cha;
                vchr.SNO = pg.SNO;
                vchr.OrderNo = pg.ONO;
                vchr.VNO = pg.VNO;
                vchr.DDate = pg.PGDate;
                vchr.Description = pg.Description;
                vchr.Dr = Math.Round(upDateAmountTextBox(), 0);
                vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                vchr.Cr = 0;
                this.vcDAL.AddVoucher(vchr, con1, trans);

                vchr = new Voucher();
                Customer cst = new Customer();
                cst = custDAL.GetCustomerById(Convert.ToInt32(lblCustId.Text));
                aDAL = new AccountDAL();
                ChildAccount child = new ChildAccount();
                child = aDAL.GetChildByCode(cst.AccountCode, con1, trans);
                vchr.AccountCode = child;
                vchr.Cr = Math.Round(upDateAmountTextBox(), 0);
                vchr.Dr = 0;
                vchr.SNO = pg.SNO;
                vchr.OrderNo = pg.ONO;
                vchr.VNO = pg.VNO;
                vchr.DDate = pg.PGDate;
                vchr.Description = pg.Description;
                vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                this.vcDAL.AddVoucher(vchr, con1, trans);
               
                if (this.txtCashPayment.Text != "")
                {
                    vchr.AccountCode = child;
                    vchr.Dr = FormControls.GetDecimalValue(this.txtCashPayment, 0);
                    vchr.Cr = 0;
                    vchr.SNO = pg.SNO;
                    vchr.OrderNo = pg.ONO;
                    vchr.VNO = vcDAL.CreateVNO("CPV", con1, trans);
                    vchr.DDate = pg.PGDate;
                    vchr.Description = pg.Description;
                    vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                    vcDAL.AddVoucher(vchr, con1, trans);
                    
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con1, trans);
                    if (cha == null)
                    {
                        pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con1, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con1, trans);
                    }
                    cha.Balance = cha.Balance - FormControls.GetDecimalValue(this.txtCashPayment, 0);
                    aDAL.UpdateChildBalance(cha.Balance, cha.ChildCode, con1, trans);
                    vchr.AccountCode = cha;
                    vchr.Cr = FormControls.GetDecimalValue(this.txtCashPayment, 0);
                    vchr.Dr = 0;
                    vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                    vcDAL.AddVoucher(vchr, con1, trans);
                    pg.CPVNO = vchr.VNO;
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con1.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con1.Close();
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    frmBalanceInvoiceRpt frm = new frmBalanceInvoiceRpt();
                    frm.id = 1;
                    frm.selectQuery = "{GoldSalePurchaseBill.GPNO} = " + pg.GPNO;
                    frm.ShowDialog();
                    this.ShowDGV();
                    this.RefreshRec();
                    GoldPurchase gp = new GoldPurchase();
                    this.Dispose();
                    gp.ShowDialog();
                }
            }
        }

        public decimal upDateAmountTextBox()
        {
            decimal price = 0;
            int counter;
            if (!(this.dgvDetail.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvDetail.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvDetail.Rows[counter].Cells[6].Value) == string.Empty || dgvDetail.Rows[counter].Cells[6].Value == null)
                        price += 0;
                    else
                        price += decimal.Parse(dgvDetail.Rows[counter].Cells[6].Value.ToString());
                }
            }
            return price;
        }

        public decimal upDatePaidAmountTextBox()
        {
            decimal pPrice = 0;
            int counter;
            if (!(this.dgvDetail.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvDetail.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvDetail.Rows[counter].Cells[6].Value) == string.Empty || dgvDetail.Rows[counter].Cells[6].Value == null)
                        pPrice += 0;
                    else
                        pPrice += decimal.Parse(dgvDetail.Rows[counter].Cells[6].Value.ToString());
                }
            }
            return pPrice;
        }

        private void ShowDGV()
        {
            string VType = "GPV";
            PGL = pgDAL.GetAllGold(VType);
            this.dgvGoldPurchaseDetail.AutoGenerateColumns = false;
            this.dgvGoldPurchaseDetail.Rows.Clear();
            if (PGL == null)
                return;
            else
            {
                int cont = PGL.Count;
                this.dgvGoldPurchaseDetail.Rows.Add(cont);
                for (int i = 0; i < cont; i++)
                {
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[0].Value = PGL[i].VNO.ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[1].Value = Math.Round(PGL[i].Weight, 3).ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[2].Value = Math.Round(PGL[i].Kaat, 1).ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[3].Value = Math.Round(PGL[i].PWeight, 3).ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[4].Value = PGL[i].Karat.ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[5].Value = Math.Round(PGL[i].Rate, 1).ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[6].Value = Math.Round(PGL[i].Amount, 0).ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[7].Value = PGL[i].GoldType.ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[8].Value = PGL[i].Description.ToString();
                    this.dgvGoldPurchaseDetail.Rows[i].Cells[9].Value = PGL[i].PGDate.ToString();
                }
            }
        }

        private void rbtPureGold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtPureGold.Checked == true)
            {
                this.txtKaatIn.Text = string.Empty;
                this.txtKaatIn.ReadOnly = true;
                this.txtPureWeight.ReadOnly = true;
                this.cbxKarrat.Text = "24";
                string r = (string)this.cbxKarrat.SelectedItem;
                decimal s = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpPGDate.Value));
                this.txtRate.Text = Math.Round(s, 1).ToString();
                this.cbxKarrat.Enabled = false;
            }
            if (this.rbtPureGold.Checked == false)
            {
                this.txtKaatIn.ReadOnly = false;
                this.txtKaatIn.Enabled = true;
                this.cbxKarrat.Text = "22";
                string r = (string)this.cbxKarrat.SelectedItem;
                decimal s = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpPGDate.Value));
                this.txtRate.Text = Math.Round(s, 1).ToString();
                this.cbxKarrat.Enabled = false;
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtPureGold.Checked)
            {
                this.txtPureWeight.Text = this.txtWeight.Text;
                this.txtPrice.Text = Math.Round((FormControls.GetDecimalValue(this.txtPureWeight, 3) * FormControls.GetDecimalValue(this.txtRate, 1)), 0).ToString();
            }
            this.txtPrice.Text = Math.Round((FormControls.GetDecimalValue(this.txtPureWeight, 3) * FormControls.GetDecimalValue(this.txtRate, 1)), 0).ToString();
        }

        private void txtKaatIn_TextChanged(object sender, EventArgs e)
        {
            decimal o = FormControls.GetDecimalValue(this.txtWeight, 3);
            decimal m = FormControls.GetDecimalValue(this.txtKaatIn, 1);
            frm.KaatInRatti(m, o, txtPureWeight, lblTest);

            this.txtPrice.Text = Math.Round((FormControls.GetDecimalValue(this.txtPureWeight, 3) * FormControls.GetDecimalValue(this.txtRate, 1)), 0).ToString();
        }

        private void txtRate_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtPrice.Text = Math.Round((FormControls.GetDecimalValue(this.txtPureWeight, 3) * FormControls.GetDecimalValue(this.txtRate, 1)), 0).ToString();
        }
        private void RefreshRec()
        {
            this.txtDescription.Text = "";
            this.txtKaatIn.Text = "";
            this.txtPrice.Text = "";
            this.txtPureWeight.Text = "";
            this.txtRate.Text = "";
            this.txtWeight.Text = "";
            this.txtPureWeight.Text = "";
            this.cbxKarrat.Text = "";
            this.txtAddress.Text = "";
            this.txtCashPayment.Text = "";            
            this.txtCustomerBalace.Text = "";
            this.txtCustomerName.Text = "";
            this.dtpPGDate.Value = DateTime.Now;
            this.rbtExistingCustome.Checked = true;
            string cash = "Cash In Hand";
            this.lblCashHand.Text = Math.Round((acDAL.GetCashInHandBalance()), 0).ToString();
            this.lblUsedGold.Text = Math.Round((acDAL.GetUsedGoldBalance()), 3).ToString();
            this.lblPureGold.Text = Math.Round((acDAL.GetPureGoldBalance()), 3).ToString();
            this.lblPure.Text = "";
            this.lblUsed.Text = "";
            this.cbxKarrat.SelectedIndex = 0;
            string r = (string)this.cbxKarrat.SelectedItem;
            decimal s = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpPGDate.Value));
            this.txtRate.Text = Math.Round(s, 1).ToString();
        }

        private void RefreshTab()
        {
            this.txtDescription.Text = "";
            this.txtKaatIn.Text = "";
            this.txtPrice.Text = "";
            this.txtPureWeight.Text = "";
            this.txtRate.Text = "";
            this.txtWeight.Text = "";
            this.cbxKarrat.Text = "";
            this.cbxKarrat.SelectedIndex = 0;
            string r = (string)this.cbxKarrat.SelectedItem;
            decimal s = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpPGDate.Value));
            this.txtRate.Text = Math.Round(s, 1).ToString();           
        }

        private void dgvGoldPurchaseDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                else
                {
                    vn = this.dgvGoldPurchaseDetail.Rows[e.RowIndex].Cells[0].Value.ToString();
                    j = 1;
                    k = 1;
                    con1.Open();
                    trans = con1.BeginTransaction();
                    Gold gld = new Gold();
                    gld = pgDAL.GetGoldByVNO(vn ,con1 ,trans );
                    this.txtWeight.Text = Math.Round(gld.Weight, 3).ToString();
                    this.dtpPGDate.Value = gld.PGDate;
                    this.txtKaatIn.Text = Math.Round(gld.Kaat, 1).ToString(); ;
                    this.txtDescription.Text = gld.Description.ToString();
                    this.txtPureWeight.Text = Math.Round(gld.PWeight, 3).ToString();
                    this.txtRate.Text = Math.Round(gld.Rate, 1).ToString();
                    this.txtPrice.Text = Math.Round(gld.Amount, 0).ToString();
                    this.cbxKarrat.Text = gld.Karat.ToString();
                    this.lblCPV.Text = gld.CPVNO.ToString();
                    this.lblCustId.Text = gld.CustId.ToString();
                    Customer cstmr = new Customer();
                    cstmr = custDAL.SearchCustById(gld.CustId, con1, trans);
                    this.txtCustomerName.Text = cstmr.Name.ToString();
                    this.cbxCustomerName.SelectedValue = cstmr.ID;
                    this.cbxContactNo.SelectedValue = cstmr.ID;
                    this.txtAddress.Text = cstmr.Address.ToString();
                    this.txtCustomerBalace.Text = Math.Round((decimal)cstmr.CashBalance, 0).ToString();
                    if (this.txtKaatIn.Text == "0")
                    {
                        this.rbtPureGold.Checked = true;
                        this.rbtUsedGold.Checked = false;
                        this.lblPure.Text = Math.Round((Convert.ToDecimal(this.lblPureGold.Text) - gld.PWeight), 3).ToString();
                        this.lblUsed.Text = this.lblUsedGold.Text;
                    }
                    if (this.txtKaatIn.Text != "0")
                    {
                        this.rbtUsedGold.Checked = true;
                        this.rbtPureGold.Checked = false;
                        this.lblUsed.Text = Math.Round((Convert.ToDecimal(this.lblUsedGold.Text) - gld.PWeight), 3).ToString();
                        this.lblPure.Text = this.lblPureGold.Text;
                    }
                    this.lblVNO.Text = gld.VNO.ToString();
                    this.lblCustId.Text = gld.CustId.ToString();
                    chld = new AccountDAL().GetAccount(1, "Current Asset", "Cash In Hand", con1, trans);
                    if (this.lblCPV.Text != "")
                    {
                        this.txtCashPayment.Text = Math.Round((vcDAL.GetCashPayment(this.lblCPV.Text, chld.ChildCode,con1 ,trans )), 0).ToString();
                        this.lblCashPayment.Text = this.txtCashPayment.Text;
                        this.lblCashBalance.Text = Math.Round((Convert.ToDecimal(this.lblCashHand.Text) + Convert.ToDecimal(this.txtCashPayment.Text)), 0).ToString();
                    }
                    if (this.lblCPV.Text == "")
                    {
                        this.txtCashPayment.Text = "0";
                        this.lblCashPayment.Text = this.txtCashPayment.Text;
                        this.lblCashBalance.Text = Math.Round(Convert.ToDecimal(this.lblCashHand.Text), 0).ToString();
                    }
                    this.lblBalance.Text = (FormControls.GetDecimalValue(this.txtCustomerBalace, 0) + FormControls.GetDecimalValue(this.txtCashPayment, 0) - FormControls.GetDecimalValue(this.txtPrice, 0)).ToString();

                    this.btnEdit.Text = "&Update";
                    this.btnSave.Enabled = false;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        private void GetPurchaseGoldbyPNo(int PNo)
        {
            PGL = pgDAL.GetPurchaseByPNo(PNo);
            if (PGL == null)
            {

                MessageBox.Show("Record Not Found");
                return;
            }
            else
            {
                this.dgvDetail.AutoGenerateColumns = false;
                this.dgvDetail.Rows.Clear();
                int count = PGL.Count;
                this.dgvDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {

                    this.dgvDetail.Rows[i].Cells[0].Value = PGL[i].item.ItemName.ToString();
                    this.dgvDetail.Rows[i].Cells[1].Value = Math.Round(PGL[i].Weight, 3).ToString();
                    this.dgvDetail.Rows[i].Cells[2].Value = Math.Round(PGL[i].Kaat, 1).ToString();
                    this.dgvDetail.Rows[i].Cells[3].Value = Math.Round(PGL[i].PWeight, 3).ToString();
                    this.dgvDetail.Rows[i].Cells[4].Value = PGL[i].Karat.ToString();
                    this.dgvDetail.Rows[i].Cells[5].Value = Math.Round(PGL[i].Rate, 1).ToString();
                    this.dgvDetail.Rows[i].Cells[6].Value = Math.Round(PGL[i].Amount, 0).ToString();
                    this.dgvDetail.Rows[i].Cells[7].Value = PGL[i].Description.ToString();                    
                    this.dgvDetail.Rows[i].Cells[8].Value = PGL[i].GType.ToString();
                    this.dgvDetail.Rows[i].Cells[9].Value = PGL[i].item.ItemId.ToString();

                }
                this.cbxSaleMan.SelectedValue = PGL[0].SaleManId;
                this.lblCustId.Text = PGL[0].CustId.ToString();
                Customer cstmr = new Customer();
                con1.Open();
                trans = con1.BeginTransaction();
                cstmr = custDAL.SearchCustById(PGL[0].CustId, con1, trans);
                this.txtCustomerName.Text = cstmr.Name.ToString();
                this.cbxCustomerName.SelectedValue = cstmr.ID;
                this.cbxContactNo.SelectedValue = cstmr.ID;
                this.txtAddress.Text = cstmr.Address.ToString();
                this.txtCustomerBalace.Text = Math.Round((decimal)cstmr.CashBalance, 0).ToString();
                this.txtTAmount.Text = Math.Round(upDateAmountTextBox(), 0).ToString();
                this.txtCashPayment.Text = Math.Round(PGL[0].CashPR, 0).ToString();
                this.txtPurchaseNo.Text = PNo.ToString();
                this.dtpPGDate.Value = PGL[0].PGDate;
                this.btnEdit.Text = "Update";
                trans.Commit();
                con1.Close();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {         
            if (btnEdit.Text == "Update")
            {
                try
                {
                    if (this.cbxCustomerName.Text == "")
                    {
                        MessageBox.Show("Please Select Customer", Messages.Header);
                        this.cbxCustomerName.Select();
                        return;
                    }
                    if (this.dgvDetail.Rows.Count <= 1)
                    {
                        MessageBox.Show("first enter weight then add", Messages.Header);
                        this.txtWeight.Focus();
                        return;
                    }
                    con1.Open();
                    trans = con1.BeginTransaction();
                    pgDAL.DeletePurchaseByPNo(Convert.ToInt32(this.txtPurchaseNo.Text), con1, trans);
                    for (int i = 0; i < this.dgvDetail.Rows.Count - 1; i++)
                    {
                        pg.PGDate = Convert.ToDateTime(this.dtpPGDate.Value);
                        pg.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                        pg.Weight = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[1].Value), 3);
                        pg.Kaat = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[2].Value), 1);
                        pg.PWeight = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[3].Value), 3);
                        pg.Karat = this.dgvDetail.Rows[i].Cells[4].Value.ToString();
                        pg.Rate = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[5].Value), 1);
                        pg.Amount = Math.Round(Convert.ToDecimal(this.dgvDetail.Rows[i].Cells[6].Value), 0);
                        pg.Description = this.dgvDetail.Rows[i].Cells[7].Value.ToString();
                        pg.RemainingWork = "";
                        if (this.dgvDetail.Rows[i].Cells[8].Value == "UsedGold")
                            pg.GoldType = GoldType.Used;
                        if (this.dgvDetail.Rows[i].Cells[8].Value == "PureGold")
                            pg.GoldType = GoldType.Pure;
                        pg.item = new Item();
                        pg.item.ItemId = Convert.ToInt32(this.dgvDetail.Rows[i].Cells[9].Value);
                        pg.SaleMan = new SaleMan();
                        if (this.cbxSaleMan.SelectedIndex == -1)
                            pg.SaleMan.ID = 0;
                        else
                            pg.SaleMan = (SaleMan)cbxSaleMan.SelectedItem;
                        pg.CustId = Convert.ToInt32(this.lblCustId.Text);
                        pg.VNO = "GPV";
                        pg.PTime = "Gold Sale";
                        pg.PMode = "Pay";
                        this.pgDAL.AddGoldDetail(pg, con1, trans);
                    }
                    ChildAccount cha = null;
                    if (this.rbtPureGold.Checked == true)
                    {
                        #region A
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                        if (cha == null)
                        {
                            pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con1, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                        }
                        #endregion
                    }
                    if (this.rbtUsedGold.Checked == true)
                    {
                        #region B
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                        if (cha == null)
                        {
                            pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con1, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con1, trans);
                        }
                        #endregion
                    }
                    string str = "GPV";
                    pg.ONO = 0;
                    pg.SNO = 0;
                    pg.VNO = vcDAL.CreateVNO(str, con1, trans);
                    pg.RemainingWork = "";
                    vchr.AccountCode = cha;
                    vchr.SNO = pg.SNO;
                    vchr.OrderNo = pg.ONO;
                    vchr.VNO = pg.VNO;
                    vchr.DDate = pg.PGDate;
                    vchr.Description = pg.Description;
                    vchr.Dr = Math.Round(upDateAmountTextBox(), 0);
                    vchr.Cr = 0;
                    vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                    this.vcDAL.AddVoucher(vchr, con1, trans);
                    vchr = new Voucher();

                    Customer cst = new Customer();
                    cst = custDAL.GetCustomerById(Convert.ToInt32(lblCustId.Text)); //custDAL.GetAccountCod(Convert.ToInt32(this.lblCustId.Text));
                    aDAL = new AccountDAL();
                    ChildAccount child = new ChildAccount();
                    child = aDAL.GetChildByCode(cst.AccountCode, con1, trans);
                    vchr.AccountCode = child;
                    vchr.Cr = Math.Round(upDateAmountTextBox(), 0);
                    vchr.Dr = 0;
                    vchr.SNO = pg.SNO;
                    vchr.OrderNo = pg.ONO;
                    vchr.VNO = pg.VNO;
                    vchr.DDate = pg.PGDate;
                    vchr.Description = pg.Description;
                    vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                    this.vcDAL.AddVoucher(vchr, con1, trans);
                  
                    if (this.txtCashPayment.Text != "")
                    {
                        vchr.AccountCode = child;
                        vchr.Dr = FormControls.GetDecimalValue(this.txtCashPayment, 0);
                        vchr.Cr = 0;
                        vchr.SNO = pg.SNO;
                        vchr.OrderNo = pg.ONO;
                        vchr.VNO = vcDAL.CreateVNO("CPV", con1, trans);
                        vchr.DDate = pg.PGDate;
                        vchr.Description = pg.Description;
                        vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                        this.vcDAL.AddVoucher(vchr, con1, trans);

                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con1, trans);
                        if (cha == null)
                        {
                            pg.AccountNO = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con1, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con1, trans);
                        }
                        cha.Balance = cha.Balance - FormControls.GetDecimalValue(this.txtCashPayment, 0);
                        aDAL.UpdateChildBalance(cha.Balance, cha.ChildCode, con1, trans);
                        vchr.AccountCode = cha;
                        vchr.Cr = Convert.ToDecimal(this.txtCashPayment.Text);
                        vchr.Dr = 0;
                        vchr.GPNO = Convert.ToInt32(this.txtPurchaseNo.Text);
                        this.vcDAL.AddVoucher(vchr, con1, trans);
                        pg.CPVNO = vchr.VNO;
                    }

                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
                finally
                {
                    if (con1.State == ConnectionState.Open)
                    {
                        trans.Commit();
                        con1.Close();
                        MessageBox.Show(Messages.Updated, Messages.Header);
                        btnSave.Enabled = true;
                        GoldPurchase gp = new GoldPurchase();
                        this.Dispose();
                        gp.ShowDialog();
                    }
                }
            }
            if (btnEdit.Text == "&Edit")
            {
                EditSPNO espno = new EditSPNO();
                espno.ShowDialog();
                GetPurchaseGoldbyPNo(espno.PNO);
                btnSave.Enabled = false;
                return;
            }
        }

        private void rbtNewCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtNewCustomer.Checked == true)
            {
                adcust = new ManageCustomer();
                FormControls.FadeOut(this);
                adcust.ShowDialog();
                FormControls.FadeIn(this);
                this.ShowMaxCustomer();
            }
        }

        private void ShowCustomer(int id)
        {            
            cust = custDAL.SearchCustById(id);
            if (cust == null)
                return;
            else
            {
                this.txtCustomerName.Text = cust.Name.ToString();
                this.txtAddress.Text = cust.Address.ToString();
                if (cust.CashBalance == null)
                    this.txtCustomerBalace.Text = "0";
                else
                    this.txtCustomerBalace.Text = Math.Round((decimal)cust.CashBalance, 0).ToString();
                string str = cust.Mobile.ToString();
                if (string.IsNullOrEmpty(str))
                    this.cbxContactNo.Text= cust.TelHome.ToString();
                else
                    this.cbxContactNo.Text = cust.Mobile.ToString();
            }
        }

        private void txtCashPayment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtKaatIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 58) && (Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46))
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void rbtUsedGold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtUsedGold.Checked == true)
            {
                if (this.txtKaatIn.Text == "")
                    this.txtPureWeight.ReadOnly = false;
                if (this.txtKaatIn.Text != "")
                    this.txtPureWeight.ReadOnly = true;
                this.cbxKarrat.Enabled = true;
            }
            if (this.rbtUsedGold.Checked == false)
            {
                this.txtKaatIn.Text = string.Empty;
                this.txtKaatIn.Enabled = false;
                this.txtPureWeight.ReadOnly = true;
            }
        }

        private void txtCashPayment_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void cbxKarrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            string r = (string)this.cbxKarrat.SelectedItem;
            decimal s = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpPGDate.Value));
            this.txtRate.Text = Math.Round(s, 1).ToString();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            this.txtPrice.Text = Math.Round((FormControls.GetDecimalValue(this.txtPureWeight, 3) * FormControls.GetDecimalValue(this.txtRate, 1)), 0).ToString("0");
        }

        private void cbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbxCustomerName.SelectedIndex != -1)
                {
                    cust = (Customer)this.cbxCustomerName.SelectedItem;
                    this.lblCustId.Text = cust.ID.ToString();
                    cust = custDAL.SearchCustById(Convert.ToInt32(lblCustId.Text));
                    this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                    this.cbxContactNo.SelectedValue = cust.ID;
                    this.txtAddress.Text = cust.Address;
                    this.txtCustomerBalace.Text = Math.Round((decimal)cust.CashBalance, 0).ToString();
                }
                cbxContactNo.Select();
            }
        }

        private void cbxContactNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = (Customer)this.cbxContactNo.SelectedItem;
            this.lblCustId.Text = cust.ID.ToString();
         
            this.cbxCustomerName.SelectedValue = cust.ID;
            this.txtAddress.Text = cust.Address;
        }

        private void cbxContactNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxContactNo.SelectedIndexChanged += new EventHandler(cbxContactNo_SelectedIndexChanged);
        }

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = (Customer)this.cbxCustomerName.SelectedItem;            
            this.lblCustId.Text = cust.ID.ToString();
            cust = custDAL.SearchCustById(Convert.ToInt32(lblCustId.Text));
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            this.cbxContactNo.SelectedValue = cust.ID;
            this.txtAddress.Text = cust.Address;
            this.txtCustomerBalace.Text = Math.Round((decimal)cust.CashBalance, 0).ToString();
        }

        private void cbxCustomerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxCustomerName.SelectedIndexChanged += new EventHandler(cbxCustomerName_SelectedIndexChanged);
        }

        void ShowMaxCustomer()
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

        private void cbxContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtPureGold.Select();
        }

        private void rbtExistingCustome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxCustomerName.Select();
        }

        private void rbtNewCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxCustomerName.Select();
        }

        private void rbtPureGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpPGDate.Select();
        }

        private void rbtUsedGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpPGDate.Select();
        }

        private void dtpPGDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxGroupItem.Select();
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (rbtPureGold.Checked == true)
                    txtRate.Select();
                else
                    txtKaatIn.Select();
            }
        }

        private void txtKaatIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                cbxKarrat.Select();
        }

        private void cbxKarrat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRate.Select();
        }

        private void txtRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtPrice.Select();
        }

        private void txtPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Select();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnAddInfo.PerformClick();
        }

        private void btnAddInfo_Click(object sender, EventArgs e)
        {
            if (txtWeight.Text == "")
            {
                MessageBox.Show("Must enter Weight!", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Must enter Description!", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            object[] values1 = new Object[10];
            values1[0] = this.cbxGroupItem.Text;
            values1[1] = this.txtWeight.Text == "" ? 0 : FormControls.GetDecimalValue(this.txtWeight, 3);
            values1[2] = this.txtKaatIn.Text == "" ? 0 : FormControls.GetDecimalValue(this.txtKaatIn, 1);
            values1[3] = this.txtPureWeight.Text == "" ? 0 : FormControls.GetDecimalValue(this.txtPureWeight, 3);
            values1[4] = this.cbxKarrat.Text;
            values1[5] = FormControls.GetDecimalValue(this.txtRate, 1).ToString();
            values1[6] = FormControls.GetDecimalValue(this.txtPrice, 0).ToString();

            values1[7] = this.txtDescription.Text.ToString();
            if (this.rbtUsedGold.Checked == true)
            {
                values1[8] = "UsedGold";
            }
            else
            {
                values1[8] = "PureGold";
            }
            if (this.cbxGroupItem.SelectedIndex != -1)
                values1[9] = (int)this.cbxGroupItem.SelectedValue;
            else
                values1[9] = "0";
            this.dgvDetail.Rows.Add(values1);
            int j = this.dgvDetail.Rows.Count;
            txtTAmount.Text = Math.Round(upDateAmountTextBox(), 0).ToString();
            RefreshTab();
            txtCashPayment.Select();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.dgvDetail.SelectedRows.Count > 0)
            {
                this.txtTAmount.Text = Math.Round(upDateAmountTextBox(), 0).ToString();
                this.txtCashPayment.Text = Math.Round(upDatePaidAmountTextBox(), 0).ToString();
                dgvDetail.Rows.RemoveAt(this.dgvDetail.SelectedRows[0].Index);
            }
            else
            {
                MessageBox.Show("Plz select any row to delete", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void cbxSaleMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtExistingCustome.Select();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EditSPNO espno = new EditSPNO();
            espno.ShowDialog();
            if (MessageBox.Show("Are you sure want to delete this Record  Click Yes else Click No", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con1.Open();
                trans = con1.BeginTransaction();
                pgDAL.DeletePurchaseByPNo(espno.PNO, con1, trans);
                trans.Commit();
                con1.Close();
                MessageBox.Show(Messages.Deleted);
                GoldPurchase gp = new GoldPurchase();
                this.Dispose();
                gp.ShowDialog();
            }
        }

        private void txtPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtDescription.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmGoldSalePurchaseReports frm = new frmGoldSalePurchaseReports();
            frm.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmGoldSalePurchaseReports frm = new frmGoldSalePurchaseReports();
            frm.ShowDialog();
        }

        private void cbxGroupItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtWeight.Select();
        }
    }
}
