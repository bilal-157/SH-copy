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
using System.IO;
using System.Globalization;

namespace jewl
{
    public partial class ManageSilverSale : Form
    {
        List<string> lstTagNo = new List<string>();
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        AccountDAL acDAL = new AccountDAL();
        SaleDAL slDAL = new SaleDAL();
        int snoo = 0;
        List<string> strl = new List<string>();
        private Voucher pv;
        private Voucher custv;
        private Voucher salv;
        SaleLineItem sli = new SaleLineItem();
        ChildAccount ca = new ChildAccount();
        private AccountDAL aDAL = new AccountDAL();
        private VouchersDAL vDAL = new VouchersDAL();
        public bool checks = false;
        PaymentsDAL payDAL = new PaymentsDAL();
        SaleDAL saleDAL = new SaleDAL();
        OrderDAL oDAL = new OrderDAL();
        ItemDAL itmDAL = new ItemDAL();
        DesignDAL desDAL = new DesignDAL();
        WorkerDAL wrkDAL = new WorkerDAL();
        Formulas frm = new Formulas();
        GoldRateDAL grDAL = new GoldRateDAL();
        StonesDAL sDAL = new StonesDAL();
        BankDAL bDAL = new BankDAL();
        Stock stk = new Stock();
        Sale sl = new Sale();
        StockDAL stkDAL = new StockDAL();
        DesignDAL dDAL = new DesignDAL();
        CustomerDAL custDAL = new CustomerDAL();
        ManageCustomer adcust = new ManageCustomer();
        OrderEstimat ordEst = new OrderEstimat();
        NumberToEnglish nmb = new NumberToEnglish();
        OrderLineItem oli;
        List<Customer> custs;
        Customer cust;
        Sale sale;
        SaleManDAL slmDAL = new SaleManDAL();
        public List<Cheques> ListOfChecks = new List<Cheques>();
        public List<CreditCard> ListOfCreditCard = new List<CreditCard>();
        public List<Gold> ListOfPureGold = new List<Gold>();
        public List<Gold> ListOfUsedGold = new List<Gold>();
        PaymentOption frmpayment = null;
        bool eFlag = false;
        private decimal ExtraMoney = 0;
        decimal u = 0, sum = 0;
        string strg = "";
        decimal s;
        int l = 1;
        int z;
        public string tagNo = "";

        public ManageSilverSale()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void ManageSilverSale_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);
            this.cbxOrderNo.SelectedIndexChanged -= new EventHandler(cbxOrderNo_SelectedIndexChanged);
            this.cbxOrderTags.SelectedIndexChanged -= new EventHandler(cbxOrderTags_SelectedIndexChanged);

            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
            FormControls.FillCombobox(cbxSaleMan, slmDAL.GetAllSaleMen(), "Name", "ID");
            this.ShowDataGrid();
            this.txtSaleNo.Text = (saleDAL.GetMaxSaleNo() + 1).ToString();
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID"); 

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
            }
            this.txtBarCode.Select();
            if(tagNo != "")
            {
                this.ShowAllRecordByTag(tagNo);
                this.txtStonePrice.Text = this.updateSum().ToString("0");
                this.txtNetPrice.Text = (Convert.ToDecimal(this.txtTotalPriceSilver.Text) + Convert.ToDecimal(this.txtStonePrice.Text) - Convert.ToDecimal(this.txtItemDiscount.Text)).ToString("0");
                this.txtBarCode.Text = "";
                txtWeight.Select();
            }
        }

        #region keyPress
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else 
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void cbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (cbxCustomerName.SelectedIndex != -1)
                {
                    cust = (Customer)this.cbxCustomerName.SelectedItem;
                    this.lblCustId.Text = cust.ID.ToString();
                    this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                    this.cbxContactNo.SelectedValue = cust.ID;
                    this.txtAddress.Text = cust.HouseNo;
                }
                this.cbxContactNo.Select();
            }
        }
        #endregion

        #region keyChange
        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val;
            if (this.txtWeight.Text == "")
                val = 0;
            else
                val = Math.Round(Convert.ToDecimal(this.txtWeight.Text), 3);
        }

        private void txtStonePrice_TextChanged(object sender, EventArgs e)
        {
            decimal val;
            if (this.txtStonePrice.Text == "")
                val = 0;
            else
                val = Math.Round(Convert.ToDecimal(this.txtStonePrice.Text), 0);
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1, val2;
            if (this.txtGTotalPrice.Text == "")
                val = 0;
            else
                val = Convert.ToDecimal(this.txtGTotalPrice.Text);
            if (this.txtTotalReceiveAmount.Text == "")
                val1 = 0;
            else
                val1 = Convert.ToDecimal(this.txtTotalReceiveAmount.Text);
            if (this.txtDiscount.Text == "")
                val2 = 0;
            else
                val2 = Convert.ToDecimal(this.txtDiscount.Text);
            txtNetAmount.Text = (val - val2).ToString("0");
            this.txtBalance.Text = (val - (val1 + val2)).ToString("0");
        }

        private void GetReceivedAmount()
        {
            decimal cashrecieve = 0, creditcard = 0, checque = 0, ugold = 0, pgold = 0, advance = 0, rgreceived = 0, cashPayment = 0;
            advance = FormControls.GetDecimalValue(this.txtAlreadyAmount, 0);
            cashrecieve = FormControls.GetDecimalValue(this.txtCashReceive, 0);
            creditcard = FormControls.GetDecimalValue(this.txtCreditCard, 0);
            checque = FormControls.GetDecimalValue(this.txtCheck, 0);
            pgold = FormControls.GetDecimalValue(this.txtPureGold, 0);
            ugold = FormControls.GetDecimalValue(this.txtUsedGold, 0);
            cashPayment = FormControls.GetDecimalValue(this.txtCashPayment, 0);
            this.txtTotalReceiveAmount.Text = ((cashrecieve + creditcard + checque + ugold + pgold + advance + rgreceived) - cashPayment).ToString("0");
        }

        private void txtTotalReceiveAmount_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1;
            if (this.txtNetAmount.Text == "")
                val = 0;
            else
                val = Convert.ToDecimal(this.txtNetAmount.Text);
            if (this.txtTotalReceiveAmount.Text == "")
                val1 = 0;
            else
                val1 = Convert.ToDecimal(this.txtTotalReceiveAmount.Text);
            this.txtBalance.Text = (val - val1).ToString("0");
        }
        #endregion

        #region functions
        private void ShowDataGrid()
        {
            FormControls.FillCombobox(Column2, sDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            FormControls.FillCombobox(Column3, sDAL.GetAllStoneName(), "StoneName", "StoneId");
            FormControls.FillCombobox(Column8, sDAL.GetAllColorName(), "ColorName", "ColorId");
            FormControls.FillCombobox(Column9, sDAL.GetAllCutName(), "CutName", "CutId");
            FormControls.FillCombobox(Column10, sDAL.GetAllClearityName(), "ClearityName", "ClearityId");
            this.txtQty.Text = "1";
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        public decimal upDateTextBox()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvStonesDetail.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == string.Empty || dgvStonesDetail.Rows[counter].Cells[3].Value == null)
                        weight += 0;
                    else
                        weight += decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString());
                }
            }
            return weight;
        }

        public decimal updateSum()
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
            {
                if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "" || Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "0")
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[4].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        decimal k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[4].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString("0");
                        sum += k;
                    }
                }
                else
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        decimal k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString("0");
                        sum += k;
                    }
                }
            }
            return sum;
        }

        private void ShowAllRecord(int stkId)
        {
            if (stkId <= 0) 
                return;
            else
            {
                stk = stkDAL.GetSilverStockBySockId(stkId);
                if (stk == null)
                    return;
                else
                {
                    this.cbxTagNo.SelectedIndexChanged -= new System.EventHandler(this.cbxTagNo_SelectedIndexChanged);
                    cbxGroupItem.DataSource = itmDAL.GetAllItemByType("Silver");
                    cbxGroupItem.DisplayMember = "ItemName";
                    cbxGroupItem.ValueMember = "ItemId";

                    for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                    {
                        Item it = (Item)this.cbxGroupItem.Items[i];
                        if (stk.ItemName.ItemId == it.ItemId)
                            this.cbxGroupItem.SelectedIndex = i;
                    }

                    FormControls.FillCombobox(cbxTagNo, getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + stk.ItemName.ItemId), "TagNo", "StockId");
                    this.cbxTagNo.SelectedValue = stkId;

                    FormControls.FillCombobox(cbxDesignNo, dDAL.GetAllDesign(), "DesignNo", "DesignId");
                    if (stk.DesignNo.DesignId == 0)
                        this.cbxDesignNo.SelectedIndex = -1;
                    else
                        this.cbxDesignNo.SelectedValue = (int)stk.DesignNo.DesignId;

                    if (stk.Qty.HasValue)
                        this.txtQty.Text = stk.Qty.ToString();
                    else
                        this.txtQty.Text = "1";

                    if (stk.Pieces.HasValue)
                        this.txtPieces.Text = stk.Pieces.ToString();
                    else
                        this.txtPieces.Text = "";

                    this.cbxWorker.DataSource = wrkDAL.GetAllWorkers();
                    this.cbxWorker.DisplayMember = "Name";
                    this.cbxWorker.ValueMember = "ID";
                    this.cbxWorker.SelectedIndex = -1;
                    for (int i = 0; i < this.cbxWorker.Items.Count; i++)
                    {
                        Worker wrk = (Worker)this.cbxWorker.Items[i];
                        if (stk.WorkerName.ID == wrk.ID)
                            this.cbxWorker.SelectedIndex = i;
                    }
                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.NetWeight.HasValue)
                        this.txtWeight.Text = Convert.ToDecimal(stk.NetWeight).ToString("0.000");
                    else
                        this.txtWeight.Text = "";

                    if (stk.Silver.RateA.HasValue)
                        this.txtRateA.Text = Convert.ToDecimal(stk.Silver.RateA).ToString("0.0");
                    else
                        this.txtRateA.Text = "";
                    if (stk.Silver.PriceA.HasValue)
                        this.txtPriceA.Text = Convert.ToDecimal(stk.Silver.PriceA).ToString("0");
                    else
                        this.txtPriceA.Text = "";
                    if (stk.Silver.RateD.HasValue)
                        this.txtRateD.Text = Convert.ToDecimal(stk.Silver.RateD).ToString("0.0");
                    else
                        this.txtRateD.Text = "";
                    if (stk.Silver.PriceD.HasValue)
                        this.txtPriceD.Text = Convert.ToDecimal(stk.Silver.PriceD).ToString("0");
                    else
                        this.txtPriceD.Text = "";
                    this.txtDiscountPercent.Text = "";
                    if (stk.Silver.SalePrice.HasValue)
                        this.txtTotalPriceSilver.Text = Convert.ToDecimal(stk.Silver.SalePrice).ToString("0");
                    else
                        this.txtTotalPriceSilver.Text = "";
                    this.txtDiscountPercent.Text = "0";
                    if (stk.ImageMemory == null)
                    {
                        this.pbxPicture.Image = null;
                        this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        MemoryStream mst = new MemoryStream(stk.ImageMemory);
                        this.pbxPicture.Image = Image.FromStream(mst);
                    }
                    this.StonesList(stk);
                }
            }
        }

        private void ShowCustomer(int id)
        {
            cust = custDAL.SearchCustById(id);
            if (cust == null)
                return;
            else
            {
                this.cbxCustomerName.Text = cust.Salutation.ToString() + cust.Name.ToString();
                this.txtAddress.Text = cust.Address.ToString();
                string str = cust.Mobile.ToString();
                if (string.IsNullOrEmpty(str))
                    this.cbxContactNo.Text = cust.CNIC.ToString();
                else
                    this.cbxContactNo.Text = cust.Mobile.ToString();
            }
        }

        private void createSale(object sender, EventArgs e)
        {
            if (eFlag == true)
            {
                sli = new SaleLineItem();
                sli.Stock = new Stock();
                sli.Stock.TagNo = this.cbxTagNo.Text;
                if (chkOrderNo.Checked == true)
                {
                    lblOrderNo.Text = cbxOrderNo.Text;
                    sli.Stock.OrderNo = Convert.ToInt32(lblOrderNo.Text);
                    sli.Stock.TagNo = cbxOrderTags.Text;
                }
                else
                    sli.Stock.OrderNo = 0;
                sli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;

                if (this.txtQty.Text == "")
                    sli.Stock.Qty = 0;
                else
                {
                    sli.Stock.Qty = Convert.ToInt32(this.txtQty.Text);
                    sli.Stock.SaleQty = Convert.ToInt32(this.txtQty.Text);
                }

                sli.Stock.Silver = new Silver();
                if (this.txtRateA.Text == "")
                    sli.Stock.Silver.RateA = 0;
                else
                    sli.Stock.Silver.RateA = FormControls.GetDecimalValue(this.txtRateA, 1);
                if (this.txtPriceA.Text == "")
                    sli.Stock.Silver.PriceA = 0;
                else
                    sli.Stock.Silver.PriceA = FormControls.GetDecimalValue(this.txtPriceA, 0);
                if (this.txtRateD.Text == "")
                    sli.Stock.Silver.RateD = 0;
                else
                    sli.Stock.Silver.RateD = FormControls.GetDecimalValue(this.txtRateD, 1);
                if (this.txtPriceD.Text == "")
                    sli.Stock.Silver.PriceD = 0;
                else
                    sli.Stock.Silver.PriceD = FormControls.GetDecimalValue(this.txtPriceD, 0);
                if (this.txtTotalPriceSilver.Text == "")
                    sli.Stock.Silver.SalePrice = 0;
                else
                    sli.Stock.Silver.SalePrice = FormControls.GetDecimalValue(this.txtTotalPriceSilver, 0);

                sli.Stock.DesignNo = (Design)this.cbxDesignNo.SelectedItem;

                if (cbxWorker.SelectedIndex == -1)
                {
                    Worker wr = new Worker();
                    wr.ID = 0;
                    sli.Stock.WorkerName = wr;
                }
                else
                    sli.Stock.WorkerName = (Worker)this.cbxWorker.SelectedItem;

                sli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                sli.Stock.SaleWeight = FormControls.GetDecimalValue(this.txtWeight, 3);

                if (this.txtDescription.Text == "")
                    sli.Stock.Description = "";
                else
                    sli.Stock.Description = this.txtDescription.Text;

                if (txtItemDiscount.Text == "")
                    sli.Stock.Discount = 0;
                else
                    sli.Stock.Discount = FormControls.GetDecimalValue(this.txtItemDiscount, 0);
                if (this.txtNetPrice.Text == "")
                    sli.Stock.NetAmount = 0;
                else
                    sli.Stock.NetAmount = FormControls.GetDecimalValue(txtNetPrice, 0);
                sli.Stock.OtherCharges = 0;
                sli.Stock.TotalPrice = FormControls.GetDecimalValue(txtNetPrice, 0);
                sli.Stock.Status = "Not Available";
                sli.Stock.BStatus = "Standard";
                sli.Stock.StoneList = this.GetAllDetails();
                sl.AddLineItems(sli);
            }
            else
            {
                if (this.cbxGroupItem.Text == "")
                {
                    MessageBox.Show("There is no Group Item", Messages.Header);
                    return;
                }
                else if (this.txtWeight.Text == "")
                {
                    MessageBox.Show("There is no Weight", Messages.Header);
                    return;
                }
                else if (this.txtQty.Text == "")
                {
                    MessageBox.Show("There is no Qty", Messages.Header);
                    return;
                }
                else
                {
                    sli = new SaleLineItem();
                    sli.Stock = new Stock();
                    sli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                    sli.Stock.StockId = stk.StockId;

                    sli.Stock.TagNo = stk.TagNo;
                    if (chkOrderNo.Checked == true)
                    {
                        lblOrderNo.Text = cbxOrderNo.Text;
                        sli.Stock.OrderNo = Convert.ToInt32(lblOrderNo.Text);
                        sli.Stock.TagNo = cbxOrderTags.Text;
                    }
                    else
                        sli.Stock.OrderNo = 0;
                    sli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;

                    if (this.txtQty.Text == "")
                        sli.Stock.Qty = 0;
                    else
                    {
                        sli.Stock.Qty = Convert.ToInt32(this.txtQty.Text);
                        sli.Stock.SaleQty = Convert.ToInt32(this.txtQty.Text);
                    }

                    sli.Stock.Silver = new Silver();
                    if (this.txtRateA.Text == "")
                        sli.Stock.Silver.RateA = 0;
                    else
                        sli.Stock.Silver.RateA = FormControls.GetDecimalValue(this.txtRateA, 1);
                    if (this.txtPriceA.Text == "")
                        sli.Stock.Silver.PriceA = 0;
                    else
                        sli.Stock.Silver.PriceA = FormControls.GetDecimalValue(this.txtPriceA, 0);
                    if (this.txtRateD.Text == "")
                        sli.Stock.Silver.RateD = 0;
                    else
                        sli.Stock.Silver.RateD = FormControls.GetDecimalValue(this.txtRateD, 1);
                    if (this.txtPriceD.Text == "")
                        sli.Stock.Silver.PriceD = 0;
                    else
                        sli.Stock.Silver.PriceD = FormControls.GetDecimalValue(this.txtPriceD, 0);
                    if (this.txtTotalPriceSilver.Text == "")
                        sli.Stock.Silver.SalePrice = 0;
                    else
                        sli.Stock.Silver.SalePrice = FormControls.GetDecimalValue(this.txtTotalPriceSilver, 0);

                    sli.Stock.DesignNo = (Design)this.cbxDesignNo.SelectedItem;

                    if (cbxWorker.SelectedIndex == -1)
                    {
                        Worker wr = new Worker();
                        wr.ID = 0;
                        sli.Stock.WorkerName = wr;
                    }
                    else
                        sli.Stock.WorkerName = (Worker)this.cbxWorker.SelectedItem;
                    sli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                    sli.Stock.SaleWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                    if (this.txtDescription.Text == "")
                        sli.Stock.Description = "";
                    else
                        sli.Stock.Description = this.txtDescription.Text;

                    if (txtItemDiscount.Text == "")
                        sli.Stock.Discount = 0;
                    else
                        sli.Stock.Discount = FormControls.GetDecimalValue(this.txtItemDiscount, 0);
                    if (this.txtNetPrice.Text == "")
                        sli.Stock.NetAmount = 0;
                    else
                        sli.Stock.NetAmount = FormControls.GetDecimalValue(txtNetPrice, 0);
                    sli.Stock.Status = "Not Available";
                    sli.Stock.BStatus = "Standard";
                    sli.Stock.OtherCharges = 0;
                    sli.Stock.StoneList = this.GetAllDetails();
                    sli.Stock.TotalPrice = FormControls.GetDecimalValue(this.txtTotalPriceSilver, 0);
                    sl.AddLineItems(sli);
                }
            }
        }

        private List<Stones> GetAllDetails()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStonesDetail.Rows.Count - 1);
            if (j == 0)
                return stDetail;
            else
            {
                if (stDetail == null) 
                    stDetail = new List<Stones>();
                DataGridViewComboBoxColumn cbxStone = (DataGridViewComboBoxColumn)dgvStonesDetail.Columns["Column3"];
                for (int i = 0; i < dgvStonesDetail.Rows.Count - 1; i++)
                {
                    Stones std = new Stones();
                    string str = "";
                    std.StoneTypeId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[1].Value.ToString());
                    str = (string)dgvStonesDetail.Rows[i].Cells[2].Value.ToString();
                    if (string.IsNullOrEmpty(str))
                        std.StoneId = null;
                    else
                        std.StoneId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[2].Value.ToString());
                    if (string.IsNullOrEmpty(Convert.ToString(dgvStonesDetail.Rows[i].Cells["Column4"].Value)))
                        std.StoneWeight = null;
                    else
                        std.StoneWeight = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells["Column4"].Value.ToString()), 3);
                    if ((string)dgvStonesDetail.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = null;
                    else
                        std.Qty = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[4].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = null;
                    else
                        std.Rate = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[5].Value.ToString()), 1);
                    if ((string)dgvStonesDetail.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = null;
                    else
                        std.Price = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[6].Value.ToString()), 0);
                    if ((string)dgvStonesDetail.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {
                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < Column8.Items.Count; k++)
                        {
                            StoneColor stc = (StoneColor)Column8.Items[k];
                            if (stc.ColorId == std.ColorName.ColorId)
                                std.ColorName.ColorName = stc.ColorName;
                        }
                    }
                    if ((string)dgvStonesDetail.Rows[i].Cells[8].FormattedValue == "")
                        std.CutName = null;
                    else
                    {
                        std.CutName = new StoneCut();
                        std.CutName.CutId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[8].Value.ToString());
                        for (int k = 0; k < Column9.Items.Count; k++)
                        {
                            StoneCut stc = (StoneCut)Column9.Items[k];
                            if (stc.CutId == std.CutName.CutId)
                                std.CutName.CutName = stc.CutName;
                        }
                    }
                    if ((string)dgvStonesDetail.Rows[i].Cells[9].FormattedValue == "")
                        std.ClearityName = null;
                    else
                    {
                        std.ClearityName = new StoneClearity();
                        std.ClearityName.ClearityId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[9].Value.ToString());
                        for (int k = 0; k < Column10.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)Column10.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }

        private List<Stock> getTags(string str)
        {
            List<Stock> records = stkDAL.GetAllTagNosByItemId(str);
            if (dgvBookedItem.Rows.Count <= 0 || records == null)
                return records;
            else
            {
                List<Stock> lstStock = new List<Stock>();
                if (lstStock.Count > 0)
                    lstStock.Clear();

                List<Stock> lstTag = new List<Stock>();
                List<int> lstInt = new List<int>();
                for (int i = 0; i < dgvBookedItem.Rows.Count; i++)
                {
                    if (dgvBookedItem.Rows[i].Cells[1].Value != null)
                    {
                        string str1 = dgvBookedItem.Rows[i].Cells[1].Value.ToString();
                        foreach (Stock stk in records)
                        {
                            if (str1.Equals(stk.TagNo))
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
                            lstStock.Remove(st1);
                    }
                }
                return lstStock;
            }
        }

        public void RefreshPage1()
        {
            this.txtWeight.Text = "";
            this.txtQty.Text = "";
            this.cbxWorker.SelectedIndex = -1;
            this.txtDescription.Text = "";
            this.txtStonePrice.Text = "";
            this.txtRateA.Text = "";
            this.txtRateD.Text = "";
            this.txtPriceA.Text = "";
            this.txtPriceD.Text = "";
            this.txtTotalPriceSilver.Text = "";
            this.txtDiscountPercent.Text = "";
            this.txtItemDiscount.Text = "";
            this.txtNetPrice.Text = "";
        }

        public void RefreshPage2()
        {
            this.txtBillBookNo.Text = "";
            this.cbxSaleMan.SelectedIndex = -1;
            this.cbxCustomerName.Text = "";
            this.cbxContactNo.Text = "";
            this.txtAddress.Text = "";
            this.txtTotalPriceSilver.Text = "";
            this.txtNetAmount.Text = "";
            this.txtDiscount.Text = "";
            this.txtCashReceive.Text = "";
            this.txtUsedGold.Text = "";
            this.txtPureGold.Text = "";
            this.txtCheck.Text = "";
            this.txtCreditCard.Text = "";
            this.txtCashPayment.Text = "";
            this.txtGTotalPrice.Text = "";
            this.txtAlreadyAmount.Text = "";
            this.txtTotalReceiveAmount.Text = "";
            this.txtBalance.Text = "";
            this.txtNetAmount.Text = "";
            this.lblOrderNo.Text = "";
        }

        private void CalculateBill(decimal amount, decimal cons, TextBox txt)
        {
            decimal netBill = Math.Round((amount - cons), 3);
            txt.Text = netBill.ToString("0");
        }
        #endregion

        #region cbxChange
        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.SelectedIndex == -1)
                return;
            else
            {
                if (this.dgvBookedItem.Rows.Count > 0)
                {
                    int k = (int)this.cbxGroupItem.SelectedValue;
                    FormControls.FillCombobox(cbxTagNo, getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + k), "TagNo", "StockId");
                }
                else
                {
                    int k = (int)this.cbxGroupItem.SelectedValue;
                    FormControls.FillCombobox(cbxTagNo, getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + k), "TagNo", "StockId");
                }
            }
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
        }

        private void cbxTagNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxTagNo.SelectedValue == null || this.cbxTagNo.SelectedIndex == -1)
                return;
            else
            {
                Stock s = (Stock)this.cbxTagNo.SelectedItem;
                if (s.StockId == 0)
                    return;
                else
                {
                    this.lblHidden.Text = s.StockId.ToString();
                    this.ShowAllRecord(s.StockId);
                    this.txtStonePrice.Text = this.updateSum().ToString("0");
                    this.txtNetPrice.Text = (FormControls.GetDecimalValue(this.txtTotalPriceSilver, 0) + FormControls.GetDecimalValue(this.txtStonePrice, 0) - FormControls.GetDecimalValue(this.txtItemDiscount, 0)).ToString("0");
                }
            }
        }
        #endregion

        private void getOrderNo()
        {
            int _orderNo = 0;
            int count = dgvBookedItem.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (dgvBookedItem.Rows[i].Cells[3].Value != "" || dgvBookedItem.Rows[i].Cells[3].Value != null)
                {
                    _orderNo = Convert.ToInt32(dgvBookedItem.Rows[i].Cells[3].Value);
                    break;
                }
                else
                    _orderNo = 0;
            }
            if (_orderNo == 0)
                lblOrderNo.Text = "";
            else
                lblOrderNo.Text = _orderNo.ToString();
        }

        #region click
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (chkOrderNo.Checked == true)
            {
                if (lblOrderNo.Text == "")
                    lblOrderNo.Text = cbxOrderNo.Text;
                else if (lblOrderNo.Text != cbxOrderNo.Text)
                {
                    MessageBox.Show("You Can only select Order No. " + lblOrderNo.Text, Messages.Header);
                    cbxOrderNo.Select();
                    return;
                }
            }
            if (eFlag == true)
            {
                this.createSale(sender, e);
                object[] values1 = new Object[4];
                Item itm1 = cbxGroupItem.SelectedItem as Item;
                values1[0] = itm1.ItemName.ToString();
                values1[1] = sli.Stock.TagNo.ToString();
                values1[2] = sli.Stock.StockId.ToString();
                values1[3] = sli.Stock.OrderNo.ToString();
                this.dgvBookedItem.Rows.Add(values1);
                this.RefreshPage1();
                this.dgvStonesDetail.Rows.Clear();
            }
            else
            {
                this.createSale(sender, e);
                if (this.cbxGroupItem.Text == "" || this.txtWeight.Text == "" || this.txtQty.Text == "")
                    return;
                else
                {
                    object[] values = new Object[4];
                    Item itm = cbxGroupItem.SelectedItem as Item;
                    values[0] = itm.ItemName.ToString();
                    values[1] = stk.TagNo.ToString();
                    values[2] = stk.StockId.ToString();
                    values[3] = stk.OrderNo.ToString();
                    this.dgvBookedItem.Rows.Add(values);
                    l = l + 1;
                    this.cbxTagNo.DisplayMember = "TagNo";
                    this.cbxTagNo.ValueMember = "StockId";
                    this.cbxTagNo.DataSource = getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + itm.ItemId);
                    this.cbxTagNo.SelectedIndex = -1;
                    this.RefreshPage1();
                    this.dgvStonesDetail.Rows.Clear();
                    stk = new Stock();
                }
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            SaleLineItem l = new SaleLineItem();
            string tag = "";
            Int32 selectedRowCount = dgvBookedItem.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    if (this.dgvBookedItem.Rows[z].Cells[2].Value == null)
                    {
                        tag = this.dgvBookedItem.Rows[z].Cells[0].Value.ToString();
                        dgvBookedItem.Rows.Remove(dgvBookedItem.Rows[z]);
                        foreach (SaleLineItem sli in sl.SaleLineItem)
                        {
                            if (sli.Stock.TagNo == tag)
                                l = sli;
                        }
                        sl.RemoveLineItems(l);
                        this.RefreshPage1();
                        this.dgvStonesDetail.Rows.Clear();
                        strg = "";
                    }
                    else
                    {
                        tag = dgvBookedItem.Rows[z].Cells[2].Value.ToString();
                        dgvBookedItem.Rows.Remove(dgvBookedItem.Rows[z]);
                        foreach (SaleLineItem sli in sl.SaleLineItem)
                        {
                            if (stk.TagNo == tag)
                                l = sli;
                        }
                        sl.RemoveLineItems(l);
                        Item itm1 = this.cbxGroupItem.SelectedItem as Item;
                        this.cbxTagNo.DisplayMember = "TagNo";
                        this.cbxTagNo.ValueMember = "StockId";
                        this.cbxTagNo.DataSource = getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + itm1.ItemId);
                        this.cbxTagNo.SelectedIndex = -1;
                        this.RefreshPage1();
                        this.dgvStonesDetail.Rows.Clear();
                        strg = "";
                    }
                }
                getOrderNo();
            }
            else
            {
                MessageBox.Show("Plz select any row to remove", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void btnBill_Click(object sender, EventArgs e)
        {
            checks = true;
            if (this.dgvBookedItem.Rows.Count <= 0)
            {
                MessageBox.Show("Please select Item to sale", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.tabControl1.SelectedTab = tabPage2;
                this.txtGTotalPrice.Text = sl.GetSilverGrossTotal().ToString("0");
                this.txtNetAmount.Text = sl.GetSilverGrossTotal().ToString("0");
                this.txtBalance.Text = (FormControls.GetDecimalValue(txtNetAmount, 0) - FormControls.GetDecimalValue(txtTotalReceiveAmount, 0)).ToString("0");
                txtBillBookNo.Select();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int saleNoRpt = 0;
            try
            {
                if (this.txtGTotalPrice.Text == "")
                {
                    MessageBox.Show("you  cant save this record there is no item to save", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (this.cbxCustomerName.Text == "")
                {
                    MessageBox.Show("Must Enter Customer Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    #region savecustomer
                    if (this.cbxCustomerName.Text != "" && cbxCustomerName.SelectedIndex == -1 && lblOrderNo.Text != "")
                    {
                        string CustAccCode = "";
                        try
                        {
                            cust = new Customer();
                            cust.Salutation = "";
                            cust.Name = this.cbxCustomerName.Text.ToString();
                            cust.CO = "";
                            cust.CashBalance = 0;
                            cust.Address = this.txtAddress.Text.ToString();
                            cust.Mobile = this.cbxContactNo.Text.ToString();
                            cust.TelHome = "";
                            cust.CNIC = "";
                            cust.HouseNo = "";
                            cust.CityId = new City();
                            cust.CityId.CityId = 1;
                            cust.BlockNo = "";
                            cust.CountryId = new Country();
                            cust.CountryId.CountryId = 1;
                            cust.Email = "";
                            cust.StreetNo = "";
                            cust.Near = "";
                            cust.Colony = "";
                            cust.Date = Convert.ToDateTime(this.dtpDate.Value);
                            cust.GoldBalance = 0;
                            cust.DateOfBirth = null;
                            cust.AnniversaryDate = null;
                            cust.AccountCode = acDAL.CreateAccount(1, "Customer", cust.Name, "Customer", con, trans).ToString();
                            ChildAccount cha1 = acDAL.GetChildByCode(cust.AccountCode, con, trans);
                            new CustomerDAL().AddCustomer(cust, con, trans);
                            CustAccCode = cha1.ChildCode;
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            con.Close();
                            throw ex;
                        }
                        Customer cust1 = new Customer();
                        cust1 = custDAL.GetAllCustomerByAccountNo(CustAccCode, con, trans);
                        this.lblCustId.Text = cust1.ID.ToString();
                    }
                    #endregion
                    this.txtPreviousSaleNo.Text = txtSaleNo.Text;
                    sl.SaleNo = Convert.ToInt32(txtSaleNo.Text);
                    sl.SDate = Convert.ToDateTime(dtpDate.Value);
                    sl.SVNO = vDAL.CreateVNO("SAV");
                    if (lblOrderNo.Text != "")
                        sl.OrderNo = Convert.ToInt32(lblOrderNo.Text);
                    else
                        sl.OrderNo = 0;
                    sl.CustName = new Customer();
                    sl.CustName.ID = Convert.ToInt32(this.lblCustId.Text);
                    sl.CustName.Name = cbxCustomerName.Text;
                    sl.CustName.ContactNo = cbxContactNo.Text;
                    sl.CustName.Address = txtAddress.Text;
                    sl.CusAccountNo = sl.OrderNo > 0 ? sale.CustName.AccountCode : cust.AccountCode;
                    sl.SalemanName = "";

                    sl.ODate = null;
                    sl.Baddats = 0;

                    sl.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);
                    if (this.cbxSaleMan.SelectedIndex > -1)
                    {
                        sl.SalesMan = new SaleMan();
                        sl.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                    }
                    else
                    {
                        sl.SalesMan = new SaleMan();
                        sl.SalesMan.ID = 0;
                    }

                    if (this.txtBillBookNo.Text == "")
                        sl.BillBookNo = "0";
                    else
                        sl.BillBookNo = txtBillBookNo.Text;

                    sl.TotalPrice = FormControls.GetDecimalValue(txtGTotalPrice, 0);

                    if (txtDiscount.Text == "")
                        sl.BillDiscout = 0;
                    else
                        sl.BillDiscout = FormControls.GetDecimalValue(txtDiscount, 0);
                    sl.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);

                    if (this.txtTotalReceiveAmount.Text == "")
                        sl.TReceivedAmount = 0;
                    else
                        sl.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceiveAmount, 0);
                    if (this.txtBalance.Text == "")
                        sl.Balance = 0;
                    else
                        sl.Balance = FormControls.GetDecimalValue(txtBalance, 0);

                    sl.CashReceive = FormControls.GetDecimalValue(txtCashReceive, 0);
                    sl.CashPayment = FormControls.GetDecimalValue(txtCashPayment, 0);
                    sl.UsedGoldCharges = FormControls.GetDecimalValue(txtUsedGold, 0);
                    sl.PureGoldCharges = FormControls.GetDecimalValue(txtPureGold, 0);
                    sl.CheckCash = FormControls.GetDecimalValue(txtCheck, 0);
                    sl.CreditCard = FormControls.GetDecimalValue(txtCreditCard, 0);
                    if (sl.OrderNo == 0)
                        sl.Status = "Stock Silver Sale";
                    else
                        sl.Status = "Order Silver Sale";
                    sl.BillInWord = nmb.changeNumericToWords(Convert.ToDecimal(this.txtNetAmount.Text));
                    foreach (SaleLineItem sli in sl.SaleLineItem)
                    {
                        oDAL.UpdateSaleNoFromOrder(sli.Stock.TagNo, sl.SaleNo, con, trans);
                    }
                    ChildAccount cha;
                    ChildAccount child;

                    #region Vouchers

                    #region Sale Voucher
                    salv = new Voucher();
                    cha = new ChildAccount();
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    if (cha == null)
                    {
                        string Coode = acDAL.CreateAccount(4, "Income", "Sale", "General Account", con, trans);
                        cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    }
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    salv.AccountCode = cha;
                    salv.Cr = sl.NetBill;
                    salv.Dr = 0;
                    salv.DDate = (DateTime)sl.SDate;
                    salv.SNO = sl.SaleNo;
                    salv.OrderNo = sl.OrderNo;
                    salv.VNO = sl.SVNO;
                    if (lblOrderNo.Text != "")
                        salv.Description = "Bill Of Order S.No." + sl.SaleNo.ToString();
                    else
                        salv.Description = "Bill Of S.No." + sl.SaleNo.ToString();
                    vDAL.AddVoucher(salv, con, trans);

                    pv = new Voucher();
                    pv.SNO = sl.SaleNo;
                    pv.OrderNo = sl.OrderNo;
                    acDAL = new AccountDAL();
                    child = new ChildAccount();
                    child = acDAL.GetChildByCode(sl.CusAccountNo, con, trans);
                    pv.AccountCode = child;
                    pv.VNO = sl.SVNO;
                    pv.Dr = sl.NetBill;
                    pv.Cr = 0;
                    pv.DDate = (DateTime)sl.SDate;
                    pv.Description = salv.Description;
                    vDAL.AddVoucher(salv, con, trans);
                    #endregion

                    #region Cash voucher
                    if (!(this.txtCashReceive.Text == "" || Convert.ToDecimal(this.txtCashReceive.Text) == Convert.ToDecimal("0")))
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = Convert.ToDecimal(sl.CashReceive);
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Recieved From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Recieved From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = (decimal)sl.CashReceive;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)sl.CashReceive;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Payment voucher
                    if (!(this.txtCashPayment.Text == "" || Convert.ToDecimal(this.txtCashPayment.Text) == Convert.ToDecimal("0")))
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = FormControls.GetDecimalValue(txtCashPayment, 0);
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = (int)sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Paid To Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Paid To S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = 0;
                        custv.Dr = pv.Cr;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = (int)sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)pv.Cr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region CreditCard Voucher
                    foreach (CreditCard cc in this.ListOfCreditCard)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = cc.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                        string str = pv.VNO;
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Recieved By Credit Card From Order S.No" + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Recieved By Credit Card From S.No" + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        pv = new Voucher();
                        cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = ExtraMoney;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = str;
                        if (lblOrderNo.Text != "")
                            pv.Description = "Credit Card Extra Mony From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Credit Card Extra Mony From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        cha = new ChildAccount();
                        cha.HeadCode = 1;
                        cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Income", "Credit Card Extra", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Income", "Credit Card Extra", con, trans);
                        }
                        custv.AccountCode = cha;
                        custv.Cr = ExtraMoney;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = (int)sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = str;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        cc.VNO = str;
                        cc.SNO = sl.SaleNo;
                        cc.ONO = (int)sl.OrderNo;
                        slDAL.AddCreditCards(cc, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = str;
                        sp1.PMode = "Credit Card";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = (decimal)cc.SwapAmount;
                        sp1.DRate = cc.DeductRate;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = cc.BankDeductRate;
                        sp1.BankName = cc.BankName.BankName;
                        sp1.Amount = cc.Amount;
                        if (lblOrderNo.Text != "")
                            sp1.Description = "Cash Recieved By Credit Card From Order S.No." + sl.SaleNo.ToString();
                        else
                            sp1.Description = "Cash Recieved By Credit Card From S.No." + sl.SaleNo.ToString();
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Cheque Voucher
                    foreach (Cheques chq in this.ListOfChecks)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetChildByName(chq.BankAccount, con, trans);
                        pv.AccountCode = acDAL.GetChildByCode(chq.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.Dr = chq.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cheque Recieved From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cheque Recieved From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        chq.SNO = sl.SaleNo;
                        chq.ONO = (int)sl.OrderNo;
                        chq.CustAccountCode = sl.CusAccountNo;
                        slDAL.AddChecques(chq, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cheque";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = chq.BankName.BankName;
                        sp1.Amount = pv.Dr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = pv.AccountCode.ChildCode;
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Used Gold Voucher
                    foreach (Gold gld in this.ListOfUsedGold)
                    {
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = gld.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Used Gold Purchased From Order S. No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Used Gold Purchased From S. No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        gld.PGDate = (DateTime)sl.SDate;
                        gld.SNO = sl.SaleNo;
                        gld.ONO = (int)sl.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.CustId = Convert.ToInt32(lblCustId.Text);
                        gld.Description = pv.Description;
                        gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        slDAL.AddGoldDetail(gld, con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        decimal balace = cha.Balance + gld.Weight;
                        acDAL.UpdateChildBalance(balace, cha.ChildCode, con, trans);
                    }
                    #endregion

                    #region Pure Gold Voucher
                    foreach (Gold gld in this.ListOfPureGold)
                    {
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = gld.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Pure Gold Purchased From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Pure Gold Purchased From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        gld.PGDate = (DateTime)sl.SDate;
                        gld.SNO = sl.SaleNo;
                        gld.VNO = pv.VNO;
                        gld.CustId = Convert.ToInt32(lblCustId.Text);
                        gld.Description = pv.Description;
                        gld.ONO = (int)sl.OrderNo;
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        slDAL.AddGoldDetail(gld, con, trans);
                    }
                    #endregion

                    #endregion                    
                    foreach (SaleLineItem sli in sl.SaleLineItem)
                    {
                        sDAL.DeleteStonesByTagNo(sli.Stock.TagNo.ToString(), con, trans);
                    }

                    saleDAL.AddSilverSale(sl, out saleNoRpt, con, trans);
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con.Close();
                    MessageBox.Show("Record saved successfully", Messages.Header);
                    saleDAL.CompleteSaleFromBStock();
                    this.RefreshPage1();
                    this.RefreshPage2();
                    this.dgvBookedItem.Rows.Clear();
                    this.txtSaleNo.Text = (saleDAL.GetMaxSaleNo() + 1).ToString();
                    ReportViewer frm = new ReportViewer();
                    frm.isPage = 3;
                    frm.rpt = 4;
                    frm.id = 1;
                    frm.sNo = FormControls.GetIntValue(this.txtPreviousSaleNo);
                    frm.Show();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region checkedChange
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

        void ShowMaxCustomer()
        {
            try
            {
                FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
                FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");
                this.cbxCustomerName.SelectedValue = custDAL.GetCustId("select * from CustomerInfo where CustId = (select MAX(CustId) from CustomerInfo)");
                cust = (Customer)this.cbxCustomerName.SelectedItem;
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                this.cbxContactNo.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.HouseNo;
            }
            catch
            {


            }
        }

        private void rbtExistingCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtExistingCustomer.Checked == false)
            {
                cbxCustomerName.AutoCompleteMode = AutoCompleteMode.None;
                cbxCustomerName.AutoCompleteSource = AutoCompleteSource.None;
                cbxContactNo.AutoCompleteMode = AutoCompleteMode.None;
                cbxContactNo.AutoCompleteSource = AutoCompleteSource.None;
            }
            else
            {
                cbxCustomerName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbxCustomerName.AutoCompleteSource = AutoCompleteSource.ListItems;
                cbxContactNo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cbxContactNo.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }
        #endregion

        void txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 4 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 5 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
            {
                if (e.KeyChar == 46)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 6 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;

                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
            }
        }

        private void cbxTagNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxTagNo.SelectedIndexChanged += new EventHandler(cbxTagNo_SelectedIndexChanged);
        }

        private void ShowSale(int sNo, out int saleno)
        {
            sl = saleDAL.GetSilverSaleBySaleNo(sNo);
            if (sl == null)
            {
                saleno = 0;
                return;
            }
            saleno = sl.SaleNo;
            this.txtSaleNo.Text = sl.SaleNo.ToString();
            if (sl.OrderNo > 0)
                lblOrderNo.Text = sl.OrderNo.ToString();
            this.txtBillBookNo.Text = sl.BillBookNo.ToString();
            this.lblCustId.Text = sl.CustName.ID.ToString();
            this.ShowCustomer((int)sl.CustName.ID);
            this.dtpDate.Value = Convert.ToDateTime(sl.SDate);
            this.txtAlreadyAmount.Text = sl.Advance.ToString();
            cbxSaleMan.SelectedValue = sl.SalesMan.ID;
            txtGTotalPrice.Text = Convert.ToDecimal(sl.TotalPrice).ToString("0");
            txtDiscount.Text = sl.BillDiscout.ToString("0");
            txtNetAmount.Text = sl.NetBill.ToString("0");
            if (sl.CashReceive != 0)
                txtCashReceive.Text = Convert.ToDecimal(sl.CashReceive).ToString("0");
            if (sl.CashPayment != 0)
                txtCashPayment.Text = sl.CashPayment.ToString("0");
            if (sl.CheckCash != 0)
                txtCheck.Text = Convert.ToDecimal(sl.CheckCash).ToString("0");
            if (sl.CreditCard != 0)
                txtCreditCard.Text = Convert.ToDecimal(sl.CreditCard).ToString("0");
            if (sl.PureGoldCharges != 0)
                txtPureGold.Text = Convert.ToDecimal(sl.PureGoldCharges).ToString("0");
            if (sl.UsedGoldCharges != 0)
                txtUsedGold.Text = Convert.ToDecimal(sl.UsedGoldCharges).ToString("0");

            this.cbxSaleMan.SelectedValue = sl.SalesMan.ID;
            this.dgvBookedItem.AutoGenerateColumns = false;
            this.dgvBookedItem.Rows.Clear();
            if (sl.SaleLineItem != null && sl.SaleLineItem.Count > 0)
            {
                int i = 0;
                lstTagNo = new List<string>();
                foreach (SaleLineItem sli in sl.SaleLineItem)
                {
                    object[] values1 = new Object[4];
                    values1[0] = sli.Stock.ItemName.ItemName.ToString();
                    values1[1] = sli.Stock.TagNo.ToString();
                    lstTagNo.Add(sli.Stock.TagNo.ToString());
                    values1[2] = sli.Stock.StockId.ToString();
                    values1[3] = sli.Stock.OrderNo.ToString();
                    this.dgvBookedItem.Rows.Add(values1);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.btnEdit.Text == "&Edit")
                {
                    EditNo sno = new EditNo();
                    sno.Text = "SaleNo";
                    sno.LabelText = "Enter Sale No.";
                    sno.Msg = "Enter Sale No. First";
                    sno.ShowDialog();
                    if ((int)sno.EditNum == 0)
                    {
                        MessageBox.Show("There is no Sale No.", Messages.Header);
                        return;
                    }
                    else
                    {
                        int salno;
                        this.ShowSale((int)sno.EditNum, out salno);
                        if (salno != 0)
                        {
                            ListOfCreditCard = payDAL.GetCCardListBySaleNo(sl.SaleNo);
                            if (ListOfCreditCard == null)
                                ListOfCreditCard = new List<CreditCard>();
                            ListOfChecks = payDAL.GetChequeListBySaleNo(sl.SaleNo);
                            if (ListOfChecks == null)
                                ListOfChecks = new List<Cheques>();
                            ListOfUsedGold = payDAL.GetUGoldListBySaleNo(sl.SaleNo);
                            if (ListOfUsedGold == null)
                                ListOfUsedGold = new List<Gold>();
                            ListOfPureGold = payDAL.GetPGoldListBySaleNo(sl.SaleNo);
                            if (ListOfPureGold == null)
                                ListOfPureGold = new List<Gold>();
                            this.tabControl1.SelectedTab = tabPage1;
                            this.btnEdit.Text = "&Update";
                            this.btnSave.Enabled = false;
                            eFlag = true;
                            return;
                        }
                    }
                }
                if (this.btnEdit.Text == "&Update")
                {
                    if (this.dgvBookedItem.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please add Item to update", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    sl.SaleNo = Convert.ToInt32(this.txtSaleNo.Text);
                    sl.CusAccountNo = cust.AccountCode;

                    sl.SalemanName = "";
                    if (lblOrderNo.Text != "")
                        sl.OrderNo = Convert.ToInt32(lblOrderNo.Text);
                    else
                        sl.OrderNo = 0;
                    sl.ODate = null;
                    sl.Baddats = 0;
                    sl.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);

                    if (this.txtBillBookNo.Text == "")
                        sl.BillBookNo = "0";
                    else
                        sl.BillBookNo = txtBillBookNo.Text;

                    sl.TotalPrice = FormControls.GetDecimalValue(txtGTotalPrice, 0);

                    if (txtDiscount.Text == "")
                        sl.BillDiscout = 0;
                    else
                        sl.BillDiscout = FormControls.GetDecimalValue(txtDiscount, 0);
                    sl.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);


                    if (this.txtTotalReceiveAmount.Text == "")
                        sl.TReceivedAmount = 0;
                    else
                        sl.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceiveAmount, 0);

                    sl.CashReceive = FormControls.GetDecimalValue(txtCashReceive, 0);
                    sl.CashPayment = FormControls.GetDecimalValue(txtCashPayment, 0);
                    sl.UsedGoldCharges = FormControls.GetDecimalValue(txtUsedGold, 0);
                    sl.PureGoldCharges = FormControls.GetDecimalValue(txtPureGold, 0);
                    sl.CheckCash = FormControls.GetDecimalValue(txtCheck, 0);
                    sl.CreditCard = FormControls.GetDecimalValue(txtCreditCard, 0);
                    if (sl.OrderNo == 0)
                        sl.Status = "Stock Silver Sale";
                    else
                        sl.Status = "Order Silver Sale";
                    if (this.txtBalance.Text == "")
                        sl.Balance = 0;
                    else
                        sl.Balance = FormControls.GetDecimalValue(txtBalance, 0);

                    if (this.cbxSaleMan.SelectedIndex > -1)
                    {
                        sl.SalesMan = new SaleMan();
                        sl.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                    }
                    else
                    {
                        sl.SalesMan = new SaleMan();
                        sl.SalesMan.ID = 0;
                    }
                    sl.CusAccountNo = cust.AccountCode;
                    sl.BillInWord = nmb.changeNumericToWords(Convert.ToDecimal(this.txtNetAmount.Text));
                    ChildAccount cha = new ChildAccount();
                    ChildAccount child = new ChildAccount();
                    con.Open();
                    trans = con.BeginTransaction();
                    vDAL.DeleteVoucherBySaleNo(sl.SaleNo, con, trans);

                    cha = new ChildAccount();

                    #region Vouchers

                    #region Sale Voucher
                    salv = new Voucher();
                    cha = new ChildAccount();
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    if (cha == null)
                    {
                        string Coode = acDAL.CreateAccount(4, "Income", "Sale", "General Account", con, trans);
                        cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    }
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    salv.AccountCode = cha;
                    salv.Cr = sl.NetBill;
                    salv.Dr = 0;
                    salv.DDate = (DateTime)sl.SDate;
                    salv.SNO = sl.SaleNo;
                    salv.OrderNo = sl.OrderNo;
                    salv.VNO = sl.SVNO;
                    if (lblOrderNo.Text != "")
                        salv.Description = "Bill Of Order S.No." + sl.SaleNo.ToString();
                    else
                        salv.Description = "Bill Of S.No." + sl.SaleNo.ToString();
                    vDAL.AddVoucher(salv, con, trans);

                    pv = new Voucher();
                    pv.SNO = sl.SaleNo;
                    pv.OrderNo = sl.OrderNo;
                    acDAL = new AccountDAL();
                    child = new ChildAccount();
                    child = acDAL.GetChildByCode(sl.CusAccountNo, con, trans);
                    pv.AccountCode = child;
                    pv.VNO = sl.SVNO;
                    pv.Dr = sl.NetBill;
                    pv.Cr = 0;
                    pv.DDate = (DateTime)sl.SDate;
                    pv.Description = salv.Description;
                    vDAL.AddVoucher(pv, con, trans);
                    #endregion

                    #region Cash voucher
                    if (!(this.txtCashReceive.Text == "" || Convert.ToDecimal(this.txtCashReceive.Text) == Convert.ToDecimal("0")))
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = Convert.ToDecimal(sl.CashReceive);
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Recieved From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Recieved From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = (decimal)sl.CashReceive;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)sl.CashReceive;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Payment voucher
                    if (!(this.txtCashPayment.Text == "" || Convert.ToDecimal(this.txtCashPayment.Text) == Convert.ToDecimal("0")))
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = FormControls.GetDecimalValue(txtCashPayment, 0);
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = (int)sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Paid To Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Paid To S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = 0;
                        custv.Dr = pv.Cr;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = (int)sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)pv.Cr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region CreditCard Voucher
                    foreach (CreditCard cc in this.ListOfCreditCard)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = cc.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                        string str = pv.VNO;
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cash Recieved By Credit Card From Order S.No" + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cash Recieved By Credit Card From S.No" + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        pv = new Voucher();
                        cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = ExtraMoney;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = str;
                        if (lblOrderNo.Text != "")
                            pv.Description = "Credit Card Extra Mony From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Credit Card Extra Mony From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        cha = new ChildAccount();
                        cha.HeadCode = 1;
                        cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Income", "Credit Card Extra", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Income", "Credit Card Extra", con, trans);
                        }
                        custv.AccountCode = cha;
                        custv.Cr = ExtraMoney;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = (int)sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = str;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        cc.VNO = str;
                        cc.SNO = sl.SaleNo;
                        cc.ONO = (int)sl.OrderNo;
                        slDAL.AddCreditCards(cc, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = str;
                        sp1.PMode = "Credit Card";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = (decimal)cc.SwapAmount;
                        sp1.DRate = cc.DeductRate;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = cc.BankDeductRate;
                        sp1.BankName = cc.BankName.BankName;
                        sp1.Amount = cc.Amount;
                        if (lblOrderNo.Text != "")
                            sp1.Description = "Cash Recieved By Credit Card From Order S.No." + sl.SaleNo.ToString();
                        else
                            sp1.Description = "Cash Recieved By Credit Card From S.No." + sl.SaleNo.ToString();
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Cheque Voucher
                    foreach (Cheques chq in this.ListOfChecks)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetChildByName(chq.BankAccount, con, trans);
                        pv.AccountCode = acDAL.GetChildByCode(chq.DepositInAccount.AccountCode.ChildCode, con, trans);
                        pv.Dr = chq.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Cheque Recieved From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Cheque Recieved From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        chq.SNO = sl.SaleNo;
                        chq.CustAccountCode = sl.CusAccountNo;
                        chq.ONO = (int)sl.OrderNo;
                        slDAL.AddChecques(chq, con, trans);

                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = sl.SaleNo;
                        sp1.ONo = (int)sl.OrderNo;
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cheque";
                        sp1.PTime = "Sale";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = (DateTime)sl.SDate;
                        sp1.BDrate = 0;
                        sp1.BankName = chq.BankName.BankName;
                        sp1.Amount = pv.Dr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = pv.AccountCode.ChildCode;
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                    }
                    #endregion

                    #region Used Gold Voucher
                    foreach (Gold gld in this.ListOfUsedGold)
                    {
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = gld.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Used Gold Purchased From Order S. No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Used Gold Purchased From S. No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        gld.PGDate = (DateTime)sl.SDate;
                        gld.SNO = sl.SaleNo;
                        gld.ONO = (int)sl.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.CustId = Convert.ToInt32(lblCustId.Text);
                        gld.Description = pv.Description;
                        gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        slDAL.AddGoldDetail(gld, con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        decimal balace = cha.Balance + gld.Weight;
                        acDAL.UpdateChildBalance(balace, cha.ChildCode, con, trans);
                    }
                    #endregion

                    #region Pure Gold Voucher
                    foreach (Gold gld in this.ListOfPureGold)
                    {
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        pv.AccountCode = cha;
                        pv.Dr = gld.Amount;
                        pv.Cr = 0;
                        pv.DDate = (DateTime)sl.SDate;
                        pv.OrderNo = sl.OrderNo;
                        pv.SNO = sl.SaleNo;
                        pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                        if (lblOrderNo.Text != "")
                            pv.Description = "Pure Gold Purchased From Order S.No." + sl.SaleNo.ToString();
                        else
                            pv.Description = "Pure Gold Purchased From S.No." + sl.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);

                        custv = new Voucher();
                        custv.AccountCode = salv.AccountCode;
                        custv.Cr = pv.Dr;
                        custv.Dr = 0;
                        custv.DDate = (DateTime)sl.SDate;
                        custv.OrderNo = sl.OrderNo;
                        custv.SNO = sl.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);

                        gld.PGDate = (DateTime)sl.SDate;
                        gld.SNO = sl.SaleNo;
                        gld.VNO = pv.VNO;
                        gld.CustId = Convert.ToInt32(lblCustId.Text);
                        gld.Description = pv.Description;
                        gld.ONO = (int)sl.OrderNo;
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        slDAL.AddGoldDetail(gld, con, trans);
                    }
                    #endregion

                    #endregion

                    saleDAL.UpdateSilverSale(sl.SaleNo, sl, con, trans);
                    foreach (SaleLineItem sli in sl.SaleLineItem)
                    {
                        lstTagNo.Remove(sli.Stock.TagNo.ToString());
                    }
                    foreach (string str in lstTagNo)
                    {
                        SqlCommand cmd = new SqlCommand("UpdateStockAgESale", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TagNo", str));
                        cmd.Transaction = trans;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con.Close();
                    MessageBox.Show("Record update successfully", Messages.Header);
                    this.RefreshPage1();
                    this.RefreshPage2();
                    this.dgvBookedItem.Rows.Clear();
                    this.btnEdit.Text = "&Edit";
                    this.btnSave.Enabled = true;
                }
            }
        }

        private void ShowAllRecordByItemId(Stock sobj)
        {
            if (sobj == null) return;
            else
            {
                stk = new Stock();
                stk = sobj;
                this.cbxGroupItem.SelectedValue = sobj.ItemName.ItemId;
                int k = (int)this.cbxGroupItem.SelectedValue;
                FormControls.FillCombobox(cbxTagNo, getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + k), "TagNo", "StockId");
                this.cbxTagNo.SelectedValue = sobj.StockId;

                if (sobj.Qty == 0)
                    this.txtQty.Text = "";
                else
                    this.txtQty.Text = sobj.Qty.ToString();

                this.cbxWorker.DataSource = wrkDAL.GetAllWorkers();
                this.cbxWorker.DisplayMember = "Name";
                this.cbxWorker.ValueMember = "ID";
                for (int i = 0; i < this.cbxWorker.Items.Count; i++)
                {
                    Worker wrk = (Worker)this.cbxWorker.Items[i];
                    if (sobj.WorkerName.ID == wrk.ID)
                        this.cbxWorker.SelectedIndex = i;
                    else
                        this.cbxWorker.SelectedIndex = -1;
                }

                if (sobj.OrderNo > 0)
                {
                    chkOrderNo.Checked = true;
                    cbxOrderNo.Text = sobj.OrderNo.ToString();
                    cbxOrderTags.Text = sobj.TagNo;
                }
                else
                    chkOrderNo.Checked = false;

                if (sobj.Description == null)
                    this.txtDescription.Text = "";
                else
                    this.txtDescription.Text = sobj.Description;

                this.txtWeight.Text = Convert.ToDecimal(sobj.NetWeight).ToString("0.000");

                if (sobj.Silver.RateA.HasValue)
                    this.txtRateA.Text = Convert.ToDecimal(sobj.Silver.RateA).ToString("0.0");
                else
                    this.txtRateA.Text = "";
                if (sobj.Silver.PriceA.HasValue)
                    this.txtPriceA.Text = Convert.ToDecimal(sobj.Silver.PriceA).ToString("0");
                else
                    this.txtPriceA.Text = "";
                if (sobj.Silver.RateD.HasValue)
                    this.txtRateD.Text = Convert.ToDecimal(sobj.Silver.RateD).ToString("0.0");
                else
                    this.txtRateD.Text = "";
                if (sobj.Silver.PriceD.HasValue)
                    this.txtPriceD.Text = Convert.ToDecimal(sobj.Silver.PriceD).ToString("0");
                else
                    this.txtPriceD.Text = "";
                this.txtDiscountPercent.Text = "";
                if (sobj.Silver.SalePrice.HasValue)
                    this.txtTotalPriceSilver.Text = Convert.ToDecimal(sobj.Silver.SalePrice).ToString("0");
                else
                    this.txtTotalPriceSilver.Text = "";
                this.txtItemDiscount.Text = Convert.ToDecimal(sobj.Discount).ToString("0");
                this.txtNetPrice.Text = Convert.ToDecimal(sobj.NetAmount).ToString("0");
                this.StonesList(sobj);
            }
        }

        private void ShowRecordByItemId(string strg)
        {
            this.txtDiscountPercent.TextChanged -= new EventHandler(this.txtDiscountPercent_TextChanged);
            this.txtItemDiscount.TextChanged -= new EventHandler(this.txtItemDiscount_TextChanged);
            foreach (SaleLineItem sli in sl.SaleLineItem)
            {
                if (sli.Stock.TagNo == strg)
                {

                    this.cbxGroupItem.SelectedValue = sli.Stock.ItemName.ItemId;
                    int k = (int)this.cbxGroupItem.SelectedValue;

                    if (sli.Stock.TagNo == "")
                        this.cbxTagNo.Text = "";
                    else
                    {
                        FormControls.FillCombobox(cbxTagNo, saleDAL.GetTagNoByItemIdForSilverSale(sli.Stock.ItemName.ItemId), "TagNo", "StockId");
                        for (int i = 0; i < this.cbxTagNo.Items.Count; i++)
                        {
                            Stock stk1 = (Stock)this.cbxTagNo.Items[i];
                            if (sli.Stock.TagNo.Equals(stk1.TagNo))
                            {
                                this.cbxTagNo.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    if (sli.Stock.StoneCharges == 0)
                        this.txtStonePrice.Text = "";
                    else
                        this.txtStonePrice.Text = sli.Stock.StoneCharges.ToString();

                    if(sli.Stock.OrderNo > 0)
                    {
                        chkOrderNo.Checked = true;
                        cbxOrderNo.Text = sli.Stock.OrderNo.ToString();
                        cbxOrderTags.Text = sli.Stock.TagNo;
                    }
                    else
                        chkOrderNo.Checked = false;

                    if (sli.Stock.SaleQty == 0)
                        this.txtQty.Text = "";
                    else
                        this.txtQty.Text = sli.Stock.SaleQty.ToString();

                    FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
                    this.cbxWorker.SelectedValue = sli.Stock.WorkerName.ID;
                    FormControls.FillCombobox(cbxDesignNo, dDAL.GetAllDesign(), "DesignNo", "DesignId");
                    this.cbxDesignNo.SelectedValue = sli.Stock.DesignNo.DesignId;

                    if (sli.Stock.Description == null)
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = sli.Stock.Description;

                    this.txtWeight.Text = Convert.ToDecimal(sli.Stock.SaleWeight).ToString("0.000");

                    if (sli.Stock.Silver.RateA.HasValue)
                        this.txtRateA.Text = Convert.ToDecimal(sli.Stock.Silver.RateA).ToString("0.0");
                    else
                        this.txtRateA.Text = "";
                    if (sli.Stock.Silver.PriceA.HasValue)
                        this.txtPriceA.Text = Convert.ToDecimal(sli.Stock.Silver.PriceA).ToString("0");
                    else
                        this.txtPriceA.Text = "";
                    if (sli.Stock.Silver.RateD.HasValue)
                        this.txtRateD.Text = Convert.ToDecimal(sli.Stock.Silver.RateD).ToString("0.0");
                    else
                        this.txtRateD.Text = "";
                    if (sli.Stock.Silver.PriceD.HasValue)
                        this.txtPriceD.Text = Convert.ToDecimal(sli.Stock.Silver.PriceD).ToString("0");
                    else
                        this.txtPriceD.Text = "";
                    this.txtDiscountPercent.Text = "";
                    if (sli.Stock.Silver.SalePrice.HasValue)
                        this.txtTotalPriceSilver.Text = Convert.ToDecimal(sli.Stock.Silver.SalePrice).ToString("0");
                    else
                        this.txtTotalPriceSilver.Text = "";
                    this.txtItemDiscount.Text = Convert.ToDecimal(sli.Stock.Discount).ToString("0");
                    this.txtNetPrice.Text = Convert.ToDecimal(sli.Stock.NetAmount).ToString("0");

                    this.StonesList(sli.Stock);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.RefreshPage1();
            this.RefreshPage2();
            this.dgvBookedItem.Rows.Clear();
            this.dgvStonesDetail.Rows.Clear();
        }

        private void dgvBookedItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            { return; }
            else
            {
                if (eFlag == true)
                {
                    string strTagNo = this.dgvBookedItem.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int id = Convert.ToInt32(this.dgvBookedItem.Rows[e.RowIndex].Cells[2].Value.ToString());
                    this.dgvBookedItem.Rows.RemoveAt(e.RowIndex);
                    SaleLineItem sle = new SaleLineItem();
                    foreach (SaleLineItem sli in sl.SaleLineItem)
                    {
                        if (sli.Stock.TagNo == strTagNo)
                        {
                            this.ShowRecordByItemId(strTagNo);
                            sle = sli;
                        }
                    }
                    if (sle != null)
                    {
                        sl.RemoveLineItems(sle);
                    }
                }
                else
                {
                    z = e.RowIndex;
                    strg = "";
                    strg = this.dgvBookedItem.Rows[e.RowIndex].Cells[1].Value.ToString();
                    this.dgvBookedItem.Rows.RemoveAt(e.RowIndex);
                    SaleLineItem sle = new SaleLineItem();
                    foreach (SaleLineItem sli in sl.SaleLineItem)
                    {
                        if (sli.Stock.TagNo == strg)
                        {
                            this.ShowAllRecordByItemId(sli.Stock);
                            sle = sli;
                        }
                    }
                    if (sle != null)
                    {
                        sl.RemoveLineItems(sle);
                    }
                }
                getOrderNo();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            if (checks == false)
                MessageBox.Show("", "");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.pbxPicture.Image = Image.FromFile(this.openFileDialog1.FileName);
                this.pbxPicture.BorderStyle = BorderStyle.Fixed3D;
            }
        }

        private void btnPRemove_Click(object sender, EventArgs e)
        {
            if (this.pbxPicture.Image == null)
            {
                MessageBox.Show("No Picture is selected to Remove", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
                this.pbxPicture.Image = null;
            this.pbxPicture.BorderStyle = BorderStyle.Fixed3D;
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "1";
        }

        public void AssignRights(Form frm, string frmRights)
        {
            string[] a = frmRights.Split(' ');

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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtAmountCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtAmountCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtDeductRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtReceiveables_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtBankDeductRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtDepositeInBank_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPGWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPGRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPGPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtWeightUsedGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPureWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtRateUsedGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPriceUseGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && (Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 13)
                e.Handled = true;
            else
                e.Handled = false;

            string str = "";
            str = this.txtBarCode.Text;
            if (str == "")
                return;
            else if (e.KeyChar == 13)
            {
                this.ShowAllRecordByTag(str);
                this.txtStonePrice.Text = this.updateSum().ToString("0");
                this.txtBarCode.Text = "";
                txtWeight.Select();
            }
        }

        private void ShowAllRecordByTag(string tagNo)
        {
            if (string.IsNullOrEmpty(tagNo))
                return;
            else
            {
                stk = stkDAL.GetSilverStockByTag(tagNo);
                if (stk == null)
                {
                    MessageBox.Show("Record Not Found!", Messages.Header);
                    return;
                }
                else
                {
                    FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItemByType("Silver"), "ItemName", "ItemId");
                    cbxGroupItem.SelectedValue = stk.ItemName.ItemId;
                    if (stk.OrderNo > 0)
                    {
                        chkOrderNo.Checked = true;
                        this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
                        cbxOrderNo.SelectedValue = stk.OrderNo;
                        cbxOrderTags.Text = stk.TagNo;
                        cbxTagNo.SelectedIndex = -1;
                        cbxTagNo.Enabled = false;
                    }
                    else
                    {
                        chkOrderNo.Checked = false;
                        cbxTagNo.Enabled = true;
                    }
                    if (chkOrderNo.Checked == false)
                    {
                        FormControls.FillCombobox(cbxTagNo, getTags("select StockId ,TagNo from Stock where Status='Available' and IType='Silver' and ItemId=" + stk.ItemName.ItemId), "TagNo", "StockId");
                        cbxTagNo.SelectedValue = stk.StockId;
                    }
                    if (stk.Qty.HasValue)
                        this.txtQty.Text = stk.Qty.ToString();
                    else
                        this.txtQty.Text = "1";
                    if (stk.Pieces.HasValue)
                        this.txtPieces.Text = stk.Pieces.ToString();
                    else
                        this.txtPieces.Text = "";
                    FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
                    this.cbxWorker.SelectedValue = stk.WorkerName.ID;
                    FormControls.FillCombobox(cbxDesignNo, dDAL.GetAllDesign(), "DesignNo", "DesignId");
                    this.cbxDesignNo.SelectedValue = stk.DesignNo.DesignId;
                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.NetWeight.HasValue)
                        this.txtWeight.Text = Convert.ToDecimal(stk.NetWeight).ToString("0.000");
                    else
                        this.txtWeight.Text = "";
                    if (stk.Silver.RateA.HasValue)
                        this.txtRateA.Text = Convert.ToDecimal(stk.Silver.RateA).ToString("0.0");
                    else
                        this.txtRateA.Text = "";
                    if (stk.Silver.PriceA.HasValue)
                        this.txtPriceA.Text = Convert.ToDecimal(stk.Silver.PriceA).ToString("0");
                    else
                        this.txtPriceA.Text = "";
                    if (stk.Silver.RateD.HasValue)
                        this.txtRateD.Text = Convert.ToDecimal(stk.Silver.RateD).ToString("0.0");
                    else
                        this.txtRateD.Text = "";
                    if (stk.Silver.PriceD.HasValue)
                        this.txtPriceD.Text = Convert.ToDecimal(stk.Silver.PriceD).ToString("0");
                    else
                        this.txtPriceD.Text = "";
                    this.txtDiscountPercent.Text = "";
                    if (stk.Silver.SalePrice.HasValue)
                        this.txtTotalPriceSilver.Text = Convert.ToDecimal(stk.Silver.SalePrice).ToString("0");
                    else
                        this.txtTotalPriceSilver.Text = "";
                    this.txtDiscountPercent.Text = "0";

                    if (stk.ImageMemory == null)
                    {
                        this.pbxPicture.Image = null;
                        this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        MemoryStream mst = new MemoryStream(stk.ImageMemory);
                        this.pbxPicture.Image = Image.FromStream(mst);
                    }
                    this.StonesList(stk);
                }
            }
        }

        private void txtItemDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtDiscountPercent.TextChanged -= new EventHandler(this.txtDiscountPercent_TextChanged);
            this.txtItemDiscount.TextChanged += new EventHandler(this.txtItemDiscount_TextChanged);
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;

            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

            if ((ModifierKeys & Keys.Control) == Keys.Control)
                e.Handled = true;

            if (e.Handled == false)
            {
                string str;
                if (e.KeyChar == '\b')
                {
                    if (this.txtDiscount.Text == "")
                        return;
                    str = this.txtDiscount.Text;
                    int i = str.Length;
                    str = str.Remove(i - 1);
                    if (str == string.Empty)
                    {
                        decimal val1 = 0;
                        frm.NetPrice(Convert.ToDecimal(this.txtTotalPriceSilver.Text), val1, txtNetPrice);
                        return;
                    }
                    else
                    {
                        if (str == ".")
                        {
                            decimal val1 = 0;
                            frm.NetPrice(Convert.ToDecimal(this.txtTotalPriceSilver.Text), val1, txtNetPrice);
                        }
                        else
                            frm.NetPrice(Convert.ToDecimal(this.txtTotalPriceSilver.Text), Convert.ToDecimal(str), txtNetPrice);
                    }
                }
                else
                {
                    str = this.txtDiscount.Text + e.KeyChar.ToString();
                    if (str == ".")
                    {
                        decimal val = 0;
                        frm.NetPrice(Convert.ToDecimal(this.txtTotalPriceSilver.Text), val, txtNetPrice);
                    }
                    else
                    {
                        decimal val = Convert.ToDecimal(str);
                        frm.NetPrice(Convert.ToDecimal(this.txtTotalPriceSilver.Text), val, txtNetPrice);
                    }
                }
            }
        }

        private void txtDiscountPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.txtDiscountPercent.TextChanged += new EventHandler(this.txtDiscountPercent_TextChanged);
            this.txtItemDiscount.TextChanged -= new EventHandler(this.txtItemDiscount_TextChanged);
        }

        private void txtDiscountPercent_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtDiscountPercent.Text;
            string str1 = this.txtTotalPriceSilver.Text;
            if (str == "" || str1 == "")
            {
                decimal val = 0, val1 = 0;
                this.txtDiscountPercent.Text = "0";
                this.txtNetPrice.Text = this.txtTotalPriceSilver.Text;
                frm.AmountOfPercent(val, val1, txtItemDiscount, txtNetPrice);
            }
            else
            {
                decimal val = Convert.ToDecimal(str);
                decimal val1 = Convert.ToDecimal(str1);
                frm.AmountOfPercent(val, val1, txtItemDiscount, txtNetPrice);
            }
        }

        private void txtItemDiscount_TextChanged(object sender, EventArgs e)
        {
            if (txtTotalPriceSilver.Text != "")
            {
                this.txtNetPrice.Text = this.txtTotalPriceSilver.Text;
                frm.PersentOfAmount(FormControls.GetDecimalValue(this.txtItemDiscount, 0), FormControls.GetDecimalValue(txtTotalPriceSilver, 0), txtDiscountPercent, txtNetPrice);
            }
        }

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {
            int val = 0;
            if (txtBalance.Text == "" || txtBalance.Text == "0")
                txtKNo.Text = val.ToString();
            else
                txtKNo.Text = (saleDAL.GetMaxKNo() + 1).ToString();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (this.btnReturn.Text == "&Return")
            {
                EditNo sno = new EditNo();
                sno.Text = "SaleNo";
                sno.LabelText = "Enter Sale No.";
                sno.Msg = "Enter Sale No. First";
                sno.ShowDialog();
                snoo = (int)sno.EditNum;
                if (MessageBox.Show("Are You Sure to Complete Return Sale of Sale No " + snoo, Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "Update Sale set Status ='Sale Return' Where SaleNo = " + snoo;
                    strl.Add(query);
                    query = "Delete from Vouchers Where SaleNo = " + snoo;
                    strl.Add(query);
                    query = "Delete from Sale_Payment Where SNo = " + snoo;
                    strl.Add(query);
                    query = "Delete from GoldDetail Where SNo = " + snoo;
                    strl.Add(query);
                    query = "Delete from ChequeDetail Where SNo = " + snoo;
                    strl.Add(query);
                    query = "Delete from CreditCardDetail Where SNo = " + snoo;
                    strl.Add(query);
                    this.btnReturn.Text = "&Confirmed";
                    this.btnReturn.TextAlign = ContentAlignment.MiddleCenter;
                    this.btnSave.Enabled = false;
                    this.btnEdit.Enabled = false;
                }
            }
            else if (this.btnReturn.Text == "&Confirmed")
            {
                try
                {
                    string str = "select TagNo from Stock where SaleNo = " + snoo;
                    List<string> strTagNo = slDAL.GetAllTags(str);
                    foreach (string stTagNo in strTagNo)
                    {
                        SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
                        SqlCommand cmd = new SqlCommand("UpdateStockAgESale", con1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TagNo", stTagNo));
                        con1.Open();
                        cmd.ExecuteNonQuery();
                        con1.Close();
                    }
                    foreach (string strd in strl)
                    {
                        SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
                        SqlCommand cmd = new SqlCommand(strd, con1);
                        cmd.CommandType = CommandType.Text;
                        con1.Open();
                        cmd.ExecuteNonQuery();
                        con1.Close();
                    }
                    MessageBox.Show("Sale No " + snoo + " is Completely Returnd", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RefreshPage1();
                    this.RefreshPage2();
                    this.btnSave.Enabled = true;
                    this.btnEdit.Enabled = true;
                    this.btnReturn.Text = "&Return";
                    this.dgvBookedItem.Rows.Clear();
                    this.txtSaleNo.Text = (saleDAL.GetMaxSaleNo() + 1).ToString();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void txtDiscountPercent_Leave(object sender, EventArgs e)
        {
            this.txtDiscountPercent.TextChanged -= new EventHandler(this.txtDiscountPercent_TextChanged);
            this.txtItemDiscount.TextChanged -= new EventHandler(this.txtItemDiscount_TextChanged);
        }

        private void txtItemDiscount_Leave(object sender, EventArgs e)
        {
            this.txtDiscountPercent.TextChanged -= new EventHandler(this.txtDiscountPercent_TextChanged);
            this.txtItemDiscount.TextChanged -= new EventHandler(this.txtItemDiscount_TextChanged);
        }

        private void txtBarCode_Enter(object sender, EventArgs e)
        {
            this.txtBarCode.Enter += new EventHandler(txtBarCode_Enter);
        }

        private void txtBarCode_Leave(object sender, EventArgs e)
        {
            CultureInfo culInfo = CultureInfo.CurrentCulture;
            TextInfo txtInfo = culInfo.TextInfo;
            string str1 = this.txtBarCode.Text.ToString();
            this.txtBarCode.Text = txtInfo.ToTitleCase(str1);
        }

        private void dgvStonesDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
                e.Cancel = true;
        }

        private void dgvStonesDetail_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 2; i < dgvStonesDetail.ColumnCount; i++)
            {
                if (Convert.ToString(this.dgvStonesDetail.CurrentRow.Cells[i].Value) != string.Empty)
                    return;
                else if (i == dgvStonesDetail.ColumnCount)
                    dgvStonesDetail.Rows.Remove(dgvStonesDetail.CurrentRow);
            }
        }

        private void dgvStonesDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStonesDetail.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = sDAL.GetAllStoneNamebyId(sty);
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "Id";
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
            {
                TextBox txtbox = e.Control as TextBox;
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
                if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 7)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
            }
        }

        private void dgvStonesDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    string s = val.ToString("N3");
                    dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value = s.ToString();
                }
                catch { }
            }
            if (e.ColumnIndex == 1 && dgvStonesDetail.CurrentRow != null)
            { }
        }

        private void dgvStonesDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    string s = val.ToString("N3");
                    dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value = s.ToString();
                }
                catch { }
            }
            if (Convert.ToInt32(dgvStonesDetail.Rows[e.RowIndex].Cells[1].Value) == 3)
            {
                Column8.ReadOnly = false;
                Column9.ReadOnly = false;
                Column10.ReadOnly = false;
            }
            else
            {
                Column8.ReadOnly = true;
                Column9.ReadOnly = true;
                Column10.ReadOnly = true;
            }
            decimal val2;
            val2 = upDateTextBox();
            this.txtStonePrice.Text = updateSum().ToString("0");
            this.dgvStonesDetail.EndEdit();
        }

        private void StonesList(Stock stock)
        {
            if (stock.StoneList == null)
                return;
            else
            {
                this.dgvStonesDetail.AutoGenerateColumns = false;
                int count = stock.StoneList.Count;
                this.dgvStonesDetail.Rows.Add(count);
                for (int i = 0; i < stock.StoneList.Count; i++)
                {
                    this.dgvStonesDetail.Rows[i].Cells[1].Value = stock.StoneList[i].StoneTypeId;
                    this.dgvStonesDetail.Rows[i].Cells[2].Value = stock.StoneList[i].StoneId;
                    if (stock.StoneList[i].StoneWeight.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].StoneWeight), 3);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                    if (stock.StoneList[i].Qty.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stock.StoneList[i].Qty);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                    if (stock.StoneList[i].Rate.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].Rate), 1);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                    if (stock.StoneList[i].Price.HasValue)
                        this.dgvStonesDetail.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(stock.StoneList[i].Price), 0);
                    else
                        this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].ColorName.ColorName.ToString())))
                    {
                        for (int j = 0; j < this.Column8.Items.Count; j++)
                        {
                            StoneColor stc = (StoneColor)this.Column8.Items[j];
                            if (stock.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                        }
                    }
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].CutName.CutName)))
                    {
                        for (int j = 0; j < this.Column9.Items.Count; j++)
                        {
                            StoneCut stc = (StoneCut)this.Column9.Items[j];
                            if (stock.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                        }
                    }
                    if (!(string.IsNullOrEmpty(stock.StoneList[i].ClearityName.ClearityName.ToString())))
                    {
                        for (int j = 0; j < this.Column10.Items.Count; j++)
                        {
                            StoneClearity stc = (StoneClearity)this.Column10.Items[j];
                            if (stock.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                        }
                    }
                }
            }
        }

        private void txtRateD_TextChanged(object sender, EventArgs e)
        {
            this.txtPriceD.Text = (FormControls.GetDecimalValue(this.txtWeight, 3) * FormControls.GetDecimalValue(this.txtRateD, 1)).ToString("0");
            this.txtTotalPriceSilver.Text = (FormControls.GetDecimalValue(this.txtWeight, 3) * FormControls.GetDecimalValue(this.txtRateD, 1)).ToString("0");
            this.txtNetPrice.Text = ((FormControls.GetDecimalValue(this.txtWeight, 3) * FormControls.GetDecimalValue(this.txtRateD, 1)) + FormControls.GetDecimalValue(this.txtStonePrice, 0)).ToString("0");
        }

        private void txtRateA_TextChanged(object sender, EventArgs e)
        {
            this.txtPriceA.Text = (FormControls.GetDecimalValue(this.txtRateA, 1) * FormControls.GetDecimalValue(this.txtWeight, 3)).ToString("0.0");
        }

        private void txtItemDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtNetPrice.Text = this.txtTotalPriceSilver.Text;
            frm.PersentOfAmount(FormControls.GetDecimalValue(this.txtItemDiscount, 0), FormControls.GetDecimalValue(txtTotalPriceSilver, 0), txtDiscountPercent, txtNetPrice);
        }

        private void cbxContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cbxContactNo.SelectedIndex != -1)
                {
                    cust = (Customer)this.cbxContactNo.SelectedItem;
                    this.lblCustId.Text = cust.ID.ToString();
                    this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
                    this.cbxCustomerName.SelectedValue = cust.ID;
                    this.txtAddress.Text = cust.HouseNo;
                }
                this.txtAddress.Select();
            }
        }

        private void cbxGroupItem_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxTagNo);
        }

        private void cbxTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWeight);
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtQty);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtPieces);
        }

        private void txtPieces_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxDesignNo);
        }

        private void cbxDesignNo_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxWorker);
        }

        private void cbxWorker_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDescription);
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRateA);
        }

        private void txtRateA_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtPriceA);
        }

        private void txtPriceA_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtRateD);
        }

        private void txtRateD_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtPriceD);
        }

        private void txtPriceD_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalPriceSilver);
        }

        private void txtTotalPriceSilver_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDiscountPercent);
        }

        private void txtDiscountPercent_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtItemDiscount);
        }

        private void txtItemDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtNetPrice);
        }

        private void txtNetPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dgvStonesDetail.Select();
        }

        private void dgvStonesDetail_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddItem);
        }

        private void txtBillBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                dtpDate.Select();
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxSaleMan);
        }

        private void cbxSaleMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                rbtExistingCustomer.Select();
        }

        private void rbtExistingCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxCustomerName);
        }

        private void rbtNewCustomer_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxContactNo);
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDiscount);
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(sl.SVNO, sl.SaleNo, 1, sum);
            frmpayment.Owner = this;
            frmpayment.listOfCreditCards = this.ListOfCreditCard;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            decimal extra = 0;
            txtCreditCard.Text = sum.ToString("0");
            ExtraMoney = extra;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(sl.SVNO, sl.SaleNo, 2, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfCheques = this.ListOfChecks;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtCheck.Text = sum.ToString("0");
        }

        private void btnUsedGold_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(sl.SVNO, sl.SaleNo, 4, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfUsedGold = this.ListOfUsedGold;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtUsedGold.Text = sum.ToString("0");
        }

        private void btnPureGold_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(sl.SVNO, sl.SaleNo, 3, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfPureGold = this.ListOfPureGold;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtPureGold.Text = sum.ToString("0");
        }

        private void chkOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOrderNo.Checked == true)
            {
                cbxOrderNo.Enabled = true;
                FormControls.FillCombobox(cbxOrderNo, oDAL.GetAllOrderNo("select Distinct[OrderNo] from Stock where ItemFor='Order' and Status='Available' order by OrderNo"), "OrderNo", "OrderNo");
                cbxOrderTags.Enabled = true;
                cbxTagNo.Enabled = false;
                txtBarCode.ReadOnly = true;
            }
            else
            {
                this.cbxOrderNo.SelectedIndexChanged -= new EventHandler(cbxOrderNo_SelectedIndexChanged);
                cbxOrderNo.Enabled = false;
                cbxOrderTags.Enabled = false;
                cbxTagNo.Enabled = true;
                txtBarCode.ReadOnly = false;
            }
        }

        private void cbxOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
            if (this.cbxOrderNo.SelectedValue == null) 
                return;
            else
            {
                int k = (int)this.cbxOrderNo.SelectedValue;
                this.cbxOrderTags.DataSource = slDAL.GetAllTags("select TagNo from Stock Where OrderNo=" + k + " and IType = 'Silver' and Status='Available'");
                this.cbxOrderTags.DisplayMember = "TagNo";
                this.cbxOrderTags.SelectedIndex = -1;
                this.ShowSearchRecByOrderNo(k);
            }
        }

        private void ShowSearchRecByOrderNo(int orderNo)
        {
            sale = new Sale();
            sale = payDAL.GetRecordByOrderNo(orderNo);
            if (sale == null)
                return;
            else
            {
                if (sale.BillBookNo == null)
                    this.txtBillBookNo.Text = "";
                else
                    this.txtBillBookNo.Text = sale.BillBookNo.ToString();
                this.txtGTotalPrice.Text = Convert.ToDecimal(sale.TotalPrice).ToString("0");
                this.txtDiscount.Text = sale.BillDiscout.ToString("0");
                this.txtNetAmount.Text = ((decimal)sale.TotalPrice - sale.BillDiscout).ToString("0");
                this.txtAlreadyAmount.Text = Convert.ToDecimal(sale.Advance).ToString("0");
                this.txtBalance.Text = (FormControls.GetDecimalValue(txtNetAmount, 0) - (decimal)sale.Advance).ToString("0");
                this.cbxCustomerName.SelectedIndexChanged += new EventHandler(cbxCustomerName_SelectedIndexChanged);
                this.cbxCustomerName.SelectedValue = sale.CustName.ID;
                this.lblCustId.Text = sale.CustName.ID.ToString();
                cust = new Customer();
                cust.CashBalance = sale.CustBalance;
            }
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
        }

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cust = (Customer)this.cbxCustomerName.SelectedItem;
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
                this.cbxContactNo.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.HouseNo;
            }
            catch
            {
            }
        }

        private void cbxContactNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cust = (Customer)this.cbxContactNo.SelectedItem;
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
                this.cbxCustomerName.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.HouseNo;
            }
            catch
            {
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

        private void cbxOrderNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
        }

        private void cbxOrderTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshPage1();
            this.ShowAllRecordByTag(this.cbxOrderTags.Text);
        }

        private void cbxOrderTags_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOrderTags.SelectedIndexChanged += new EventHandler(cbxOrderTags_SelectedIndexChanged);
        }

        private void txtAlreadyAmount_TextChanged(object sender, EventArgs e)
        {
            GetReceivedAmount();
        }

        private void txtCashReceive_KeyUp(object sender, KeyEventArgs e)
        {
            GetReceivedAmount();
        }
    }
}
  
