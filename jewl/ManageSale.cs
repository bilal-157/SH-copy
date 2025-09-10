using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BusinesEntities;
using DAL;
using System.IO;
using System.Globalization;
using System.Drawing.Drawing2D;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Shared;
using System.Collections;




namespace jewl
{

    public delegate void GetList(List<Cheques> lstChq);
    public partial class ManageSale : Form
    {
        string desc = "";
        clsSMS objclsSMS = new clsSMS();
        private int SaleQty;
        List<string> lstTagNo = new List<string>();
        int snoo = 0, saleQty;
        List<string> strl = new List<string>();
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        Gold pg = new Gold();
        OrderDAL oDAL = new OrderDAL();
        Formulas frm = new Formulas();
        private bool NonCharEnter = false;
        private decimal Scharges = 0;
        Customer cust;
        List<Customer> custs;
        CustomerDAL custDAL = new CustomerDAL();
        ItemDAL itmDAL = new ItemDAL();
        StockDAL sDAL = new StockDAL();
        Stock stk = new Stock();
        Item itm = new Item();
        DesignDAL dDAL = new DesignDAL();
        WorkerDAL wDAL = new WorkerDAL();
        StonesDAL stDAL = new StonesDAL();
        StonesDetail st = new StonesDetail();
        Sale s = new Sale();
        SaleDAL slDAL = new SaleDAL();
        PaymentsDAL payDAL = new PaymentsDAL();
        AccountDAL acDAL = new AccountDAL();
        SaleLineItem sli = new SaleLineItem();
        SaleLineItem sli1 = new SaleLineItem();
        StonesDAL  stkDAL = new StonesDAL ();
        UtilityDAL utlDAL = new UtilityDAL();
        GoldRateDAL grDAL = new GoldRateDAL();
        bool bFlag = false;
        decimal grate = 0;
        decimal trate = 0;
        decimal sum = 0;
        int orderNo;
        int custid = 0;
        DateTime oDate;
        ManageCustomer adcust = new ManageCustomer();
        PaymentOption frmpayment = new PaymentOption();
        NumberToEnglish nmb = new NumberToEnglish();
        public List<Cheques> ListOfChecks = new List<Cheques>();
        string CAccCode = "";
        DateTime SDate;
        public List<CreditCard> ListOfCreditCard = new List<CreditCard>();
        public List<Gold> ListOfPureGold = new List<Gold>();
        public List<Gold> ListOfUsedGold = new List<Gold>();
        private Voucher salv;
        private Voucher pv;
        private Voucher custv;
        int sNo = 0;
        bool eFlag = false;
        string strTagNo = "", status = "";
        private string tagNo = "";
        private string tagNoRpt = "";
        private string karat;        
        private VouchersDAL vDAL = new VouchersDAL();
        ChildAccount c;
        CustomerDAL cstDAL = new CustomerDAL();
        GoldRates grs = new GoldRates();
        GoldRates grse = new GoldRates();
        bool check = true;
        Sale sale;
        List<SalePayment> salePay;
        List<Gold> goldDet;
        decimal totalRecive = 0;
        decimal wt;
        int qut;
        decimal pureWeight = 0;
        decimal usedWeight = 0;
        private decimal ExtraMoney = 0;
        public int Check = 0;
        bool pFlag;
        int m = 0;
        SaleManDAL slmDAL = new SaleManDAL();
        public string SaleRateFix = "";
        public string GramTolaRate = "";
        public string GoldRatetype = "";
        private string tagNo1 = "";

        public ManageSale()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.lblRate.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
            this.label9.Font = new Font("Seogo UI", 14);
            this.WindowState = FormWindowState.Maximized;
            this.label104.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
            this.label104.Size = new Size(46, 30);
            this.label103.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Bold);
            this.label102.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Bold);
            this.label101.Font = new Font("Microsoft Sans Serif", 7, FontStyle.Bold);
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        }
        #region sale
        public string _textBox
        {
            set { txtCheck.Text = value; }
        }
        private void ShowDataGrid()
        {
            FormControls.FillCombobox(cbxStoneType, stDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            FormControls.FillCombobox(cbxStoneName, stDAL.GetAllStoneName(), "StoneName", "StoneId");
            FormControls.FillCombobox(cbxColorName, stDAL.GetAllColorName(), "ColorName", "ColorId");
            FormControls.FillCombobox(cbxCutName, stDAL.GetAllCutName(), "CutName", "CutId");
            FormControls.FillCombobox(cbxClearity, stDAL.GetAllClearityName(), "ClearityName", "ClearityId");
        }
        private void ManageSale_Load(object sender, EventArgs e)
        {            
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnDelete.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else if (str == "Limited")
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                str = ur.GetUserRightsByUser("DirectSale");
                if (str != "" && str != null)
                {

                }
            }          
            lbldate.Text = this.dtpDate.Text;
            this.txtBarCode.Select();
            this.cbxItemType.SelectedIndex = 0;
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            this.cbxItemType.Height = 50;
            this.cbxOrderNo.SelectedIndexChanged -= new EventHandler(cbxOrderNo_SelectedIndexChanged);
            this.cbxOrderTags.SelectedIndexChanged -= new EventHandler(cbxOrderTags_SelectedIndexChanged);
            this.cbxTagNo.SelectedIndexChanged -= new System.EventHandler(this.cbxTagNo_SelectedIndexChanged);
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.txtTotalMaking.TextChanged -= new EventHandler(txtTotalMaking_TextChanged);
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");
            FormControls.FillCombobox(cbxSaleMan, slmDAL.GetAllSaleMen(), "Name", "ID");
            FormControls.FillCombobox(cbxDesignNo, dDAL.GetAllDesign(), "DesignNo", "DesignId");
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
            ShowDataGrid();
            this.txtStoneWeight.Text = "0";
            this.txtTpriceOfStones.Text = "0";
            this.txtSaleNo.Text = (slDAL.GetMaxSaleNo() + 1).ToString();
            if (Main.City == "Islamabad")
            {
                this.pnlPasaRate.Visible = true;
                this.txtKarat.Enabled = false;
                this.label9.Visible = false;
                this.lblRate.Visible = false;
                this.rbtPoundPasa.Checked = true;
                grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                this.txtGoldRates.Text = grs.PoundPasa.ToString("0");
            }
            else
            {
                grs.PoundPasa = 0;
                grs.SonaPasa = 0;
            }
            grate = grDAL.GetRateByKarat("24", DateTime.Today);
            lblRate.Text = grate.ToString("0");
            this.dtpPromiseDate.Enabled = false;
            this.chkPromiseDate.Checked = false;
            this.cbxTagNo.Items.Clear();
            txtNetWeight.ReadOnly = true;            
            this.txtQty.ReadOnly = true;
            this.txtPieces.ReadOnly = true;
            s.ListOfChequs = new List<Cheques>();
            s.ListOfCreditCards = new List<CreditCard>();
           slDAL.CompleteSaleFromBStock();
            SaleRateFix = slDAL.GetSaleRateFixStatus();
            GramTolaRate = slDAL.GetStartupGramTolaRate();
            GoldRatetype = slDAL.GetStartupGoldRateType();
            if (GramTolaRate == "Gram" || GramTolaRate == "")
            { }
            else
            { }
            str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
                this.btnDamage.Enabled = false;
                this.btnReturn.Enabled = false;
                this.btnReports.Enabled = false;
            }
        }

        #region keypress
        private void txtWasteInPercent_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
        private void txtWasteInGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
        private void txtMakingPerGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
        private void txtLackerPerGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);            
        }
        private void txtTotalLacker_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);            
        }
        private void txtMakingPerTola_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }
        private void txtBillDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
        #endregion

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxTagNo.SelectedIndexChanged -= new System.EventHandler(this.cbxTagNo_SelectedIndexChanged);            
            if (this.cbxGroupItem.SelectedIndex == -1)
                return;
            else
            {
                int k = (int)this.cbxGroupItem.SelectedValue;
                this.RefreshRecord();
                this.cbxTagNo.DisplayMember = "TagNo";
                this.cbxTagNo.ValueMember = "StockId";
                this.cbxTagNo.DataSource = getTags(k);
                this.cbxTagNo.SelectedIndex = -1;
            }
        }
        private void cbxTagNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxTagNo.SelectedValue == null || this.cbxTagNo.SelectedIndex == -1)
                return;
            else
            {                
                Stock s = (Stock)this.cbxTagNo.SelectedItem;
                if (s.StockId == null)
                    return;
                else
                {
                    this.dgvStonesDetail.Rows.Clear();
                    this.lblHidden.Text = s.StockId.ToString();
                    this.ShowAllRecord(s.StockId);
                    this.rbtPoundPasa_CheckedChanged(sender, e);
                }
            }
        }
        private void ShowAllRecord(int stkId)
        {
            if (stkId <= 0) return;
            else
            {
                stk = new Stock();
                stk = sDAL.GetStockBySockId(stkId);                
                if (stk == null)
                    return;
                else
                {
                    status = stk.BStatus;
                    if (stk.BStatus != "Bulk")
                    {
                        this.pnlBulk.Visible = false;
                    }
                    if (stk.BStatus == "Bulk")
                    {
                        this.txtNetWeight.ReadOnly = false;
                        if (stk.SaleQty == null)
                            stk.SaleQty = 0;
                        status = stk.BStatus;
                        SaleQty = (int)stk.SaleQty;
                        this.txtAvailableQty.Text = Convert.ToInt32(stk.BQuantity - stk.SaleQty).ToString();
                        if (stk.SaleWeight == null)
                            stk.SaleWeight = 0;
                        this.txtAvailableWeight.Text = (stk.BWeight - (decimal)stk.SaleWeight).ToString("0.000");
                        this.pnlBulk.Visible = true;
                    }
                    if (stk.Qty.HasValue)
                    {
                        this.txtQty.Text = stk.Qty.ToString();
                        qut = (int)stk.Qty;
                    }
                    else
                        this.txtQty.Text = "1";
                    if (stk.Pieces.HasValue)
                        this.txtPieces.Text = stk.Pieces.ToString();
                    else
                        this.txtPieces.Text = "";
                    if (stk.KaatInRatti.HasValue)
                        this.txtkaat.Text = Convert.ToDecimal(stk.KaatInRatti).ToString("0.0");
                    else
                        this.txtkaat.Text = "0";
                    this.txtKarat.Text = stk.Karrat;
                    try
                    {
                        this.cbxWorkerName.SelectedValue = stk.WorkerName.ID;
                    }
                    catch 
                    {
                    }
                    this.txtSize.Text = stk.ItemSize;
                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.BStatus != "Bulk")
                    {
                        if (stk.NetWeight.HasValue)
                        {
                            this.txtNetWeight.Text = Convert.ToDecimal(stk.NetWeight).ToString("0.000");
                            wt = FormControls.GetDecimalValue(txtNetWeight, 3);
                        }
                        else
                        {
                            this.txtNetWeight.Text = "";
                            wt = 0;
                        }
                    }

                    txtGoldRates.ReadOnly = false;
                    txtFixRate.Text = "";

                    grate = grDAL.GetRateByKarat("24", DateTime.Today);
                    txtGoldRates.Text = grate.ToString("0");

                    if (stk.BStatus == "Bulk")
                    {
                        this.txtNetWeight.Text = this.txtAvailableWeight.Text;
                    }
                    if (stk.WastePercent.HasValue)
                        this.txtWasteInPercent.Text = Convert.ToDecimal(stk.WastePercent).ToString("0.0");
                    else
                        this.txtWasteInPercent.Text = "10";

                    if (stk.WasteInGm.HasValue)
                        this.txtWasteInGm.Text = Convert.ToDecimal(stk.WasteInGm).ToString("0.000");
                    else
                        this.txtWasteInGm.Text = "0";

                    if (stk.MakingPerGm.HasValue)
                        this.txtMakingPerGm.Text = Convert.ToDecimal(stk.MakingPerGm).ToString("0.0");
                    else
                        this.txtMakingPerGm.Text = "0";

                    if (stk.TotalMaking.HasValue)
                        this.txtTotalMaking.Text = Convert.ToDecimal(stk.TotalMaking).ToString("0");
                    else
                        this.txtTotalMaking.Text = "0";
                    if (stk.LakerGm.HasValue)
                        this.txtLackerPerGm.Text = Convert.ToDecimal(stk.LakerGm).ToString("0.0");
                    else
                        this.txtLackerPerGm.Text = "";
                    if (stk.TotalLaker.HasValue)
                        this.txtTotalLacker.Text = Convert.ToDecimal(stk.TotalLaker).ToString("0");
                    else
                        this.txtTotalLacker.Text = "";

                    this.txtTotalWeight.Text = Convert.ToDecimal(stk.NetWeight + stk.WasteInGm).ToString("0.000");
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
                    if (stk.StoneList == null)
                    { this.dgvStonesDetail.Rows.Clear(); }
                    else
                    {
                        this.dgvStonesDetail.AutoGenerateColumns = false;
                        int count = stk.StoneList.Count;
                        this.dgvStonesDetail.Rows.Add(count);
                        for (int i = 0; i < stk.StoneList.Count; i++)
                        {
                            this.dgvStonesDetail.Rows[i].Cells[1].Value = stk.StoneList[i].StoneTypeId;

                            this.dgvStonesDetail.Rows[i].Cells[2].Value = stk.StoneList[i].StoneId;


                            if (stk.StoneList[i].StoneWeight.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[3].Value = Convert.ToDecimal(stk.StoneList[i].StoneWeight).ToString("0.000");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                            if (stk.StoneList[i].Qty.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stk.StoneList[i].Qty);
                            else
                                this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                            if (stk.StoneList[i].Rate.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(stk.StoneList[i].Rate).ToString("0.0");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                            if (stk.StoneList[i].Price.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[6].Value = Convert.ToDecimal(stk.StoneList[i].Price).ToString("0");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                            //if (stk.StoneList[i].ColorName.ColorId.HasValue)
                            //{
                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ColorName.ColorName.ToString())))
                            {
                                for (int j = 0; j < this.cbxColorName.Items.Count; j++)
                                {
                                    StoneColor stc = (StoneColor)this.cbxColorName.Items[j];
                                    if (stk.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                        this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                                }
                            }
                            if (!(string.IsNullOrEmpty(stk.StoneList[i].CutName.CutName)))
                            {
                                for (int j = 0; j < this.cbxCutName.Items.Count; j++)
                                {
                                    StoneCut stc = (StoneCut)this.cbxCutName.Items[j];
                                    if (stk.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                        this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                                }
                            }

                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ClearityName.ClearityName.ToString())))
                            {
                                for (int j = 0; j < this.cbxClearity.Items.Count; j++)
                                {
                                    StoneClearity stc = (StoneClearity)this.cbxClearity.Items[j];
                                    if (stk.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                        this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                                }
                            }
                        }
                    }
                    decimal val1;
                    decimal val2;
                    val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                    val2 = upDateTextBox();
                    this.txtStoneWeight.Text = val2.ToString();
                    this.grossweight(val1, val2, txtGrossWeight);
                    this.txtTpriceOfStones.Text = updateSum().ToString("0");
                }
            }
            if (this.txtFixRate.Text != "" && this.txtFixRate.Text != "0")
            {
                decimal grate = FormControls.GetDecimalValue(this.txtFixRate, 1);
                grate = grate * Convert.ToDecimal(stk.Karrat) / 24;
                grate = Math.Round(grate / (decimal)Formulas.WeightInGm, 0);
                this.txtGoldRates.Text = grate.ToString("0.0");
            }
            else
            {
                if (SaleRateFix == "Auto")
                {
                    if (GramTolaRate == "Gram" || GramTolaRate == "")
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(stk.Karrat.ToString(), DateTime.Today).ToString("0.0");
                    }
                    else
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(stk.Karrat.ToString(), DateTime.Today).ToString("0.0");
                    }
                }
                else
                {
                    if (GramTolaRate == "Gram" || GramTolaRate == "")
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(SaleRateFix, DateTime.Today).ToString("0.0");
                    }
                    else
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(SaleRateFix, DateTime.Today).ToString("0.0");
                    }
                }
            }
        }

        private void ShowAllRecordByTag(string tagNo)
        {
            if (string.IsNullOrEmpty(tagNo)) return;
            else
            {
                this.txtTpriceOfStones.Text = "0";
                this.txtStoneWeight.Text = "0";
                stk = sDAL.GetStockBySockTagNo(tagNo);
                if (stk == null)
                {
                    MessageBox.Show("Record Not Found", Messages.Header);
                    return;
                }
                else
                {
                    if (stk.BStatus != "Bulk")
                    {
                        this.pnlBulk.Visible = false;
                    }
                    if (stk.BStatus == "Bulk")
                    {
                        this.txtNetWeight.ReadOnly = false;
                        if (stk.SaleQty == null)
                            stk.SaleQty = 0;
                        status = stk.BStatus;
                        SaleQty = (int)stk.SaleQty;
                        this.txtAvailableQty.Text = Convert.ToInt32(stk.BQuantity - stk.SaleQty).ToString();
                        if (stk.SaleWeight == null)
                            stk.SaleWeight = 0;

                        this.txtAvailableWeight.Text = (stk.BWeight - (decimal)stk.SaleWeight).ToString("0.000");
                        this.pnlBulk.Visible = true;
                    }
                    tagNo1 = "";
                    string st = "";
                    if (stk.ItemType == ItemType.Gold)
                        st = "Gold";
                    else if (stk.ItemType == ItemType.Diamond)
                        st = "Diamond";
                    else if (stk.ItemType == ItemType.Silver)
                    {
                        tagNo1 = stk.TagNo;
                        st = "Silver";
                    }
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
                    string strn = (string)cbxItemType.SelectedItem;
                    cbxGroupItem.DataSource = itmDAL.GetAllItemByType(strn);
                    cbxGroupItem.DisplayMember = "ItemName";
                    cbxGroupItem.ValueMember = "ItemId";

                    for (int i = 0; i < this.cbxGroupItem.Items.Count; i++)
                    {
                        Item it = (Item)this.cbxGroupItem.Items[i];
                        if (stk.ItemName.ItemId == it.ItemId)
                            this.cbxGroupItem.SelectedIndex = i;
                    }
                    int k = (int)this.cbxGroupItem.SelectedValue;

                    this.cbxTagNo.DisplayMember = "TagNo";
                    this.cbxTagNo.ValueMember = "StockId";
                    this.cbxTagNo.DataSource = getTags(k);
                    this.cbxTagNo.SelectedValue = new StockDAL().GetStockIdByTagNo(this.txtBarCode.Text);
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

                    this.txtKarat.Text = stk.Karrat;                    

                    if (stk.Qty.HasValue)
                    {
                        this.txtQty.Text = stk.Qty.ToString();
                        qut = (int)stk.Qty;
                    }
                    else
                        this.txtQty.Text = "1";

                    txtGoldRates.ReadOnly = false;
                    txtFixRate.Text = "";

                    grate = grDAL.GetRateByKarat("24", DateTime.Today);
                    txtGoldRates.Text = grate.ToString("0.0");

                    if (stk.BStatus != "Bulk")
                    {
                        if (stk.NetWeight.HasValue)
                        {
                            this.txtNetWeight.Text = Convert.ToString(stk.NetWeight);
                            wt = FormControls.GetDecimalValue(txtNetWeight, 3);
                        }
                        else
                        {
                            this.txtNetWeight.Text = "";
                            wt = 0;
                        }
                    }
                    if (stk.BStatus == "Bulk")
                    {
                        this.txtNetWeight.Text = this.txtAvailableWeight.Text;
                    }
                    if (stk.Pieces.HasValue)
                        this.txtPieces.Text = stk.Pieces.ToString();
                    else
                        this.txtPieces.Text = "";

                    //this.cbxKarat.DataSource = sDAL.GetAllKarat();
                    this.txtKarat.Text = stk.Karrat;
                    if (stk.DesignNo.DesignId != null) 
                    this.cbxDesignNo.SelectedValue = stk.DesignNo.DesignId;
                    
                    this.cbxWorkerName.SelectedValue = stk.WorkerName.ID;

                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.WastePercent.HasValue)
                        this.txtWasteInPercent.Text = Convert.ToDecimal(stk.WastePercent).ToString("0.0");
                    else
                        this.txtWasteInPercent.Text = "0";
                    if (stk.WasteInGm.HasValue)
                        this.txtWasteInGm.Text = Convert.ToDecimal(stk.WasteInGm).ToString("0.000");
                    else
                        this.txtWasteInGm.Text = "0";
                  
                    if (stk.MakingPerGm.HasValue)
                        this.txtMakingPerGm.Text = Convert.ToDecimal(stk.MakingPerGm).ToString("0.0");
                    else
                        this.txtMakingPerGm.Text = "0";
                    if (stk.KaatInRatti.HasValue)
                        this.txtkaat.Text = Convert.ToDecimal(stk.KaatInRatti).ToString("0.0");
                    else
                        this.txtkaat.Text = "0";

                
                    if (stk.TotalMaking.HasValue)
                        this.txtTotalMaking.Text = Convert.ToDecimal(stk.TotalMaking).ToString("0");
                    else
                        this.txtTotalMaking.Text = "0";
                    if (stk.LakerGm.HasValue)
                        this.txtLackerPerGm.Text = Convert.ToDecimal(stk.LakerGm).ToString("0.00");
                    else
                        this.txtLackerPerGm.Text = "";
                    if (stk.TotalLaker.HasValue)
                        this.txtTotalLacker.Text = Convert.ToDecimal(stk.TotalLaker).ToString("0");
                    else
                        this.txtTotalLacker.Text = "";
                    this.txtTotalWeight.Text = Convert.ToDecimal(stk.WasteInGm + stk.NetWeight).ToString("0.000");

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
                    if (stk.StoneList == null)
                    {
                        this.dgvStonesDetail.Rows.Clear();
                    }
                    else
                    {
                        this.dgvStonesDetail.AutoGenerateColumns = false;
                        this.dgvStonesDetail.Rows.Clear();
                        int count = stk.StoneList.Count;
                        this.dgvStonesDetail.Rows.Add(count);
                        for (int i = 0; i < stk.StoneList.Count; i++)
                        {
                            this.dgvStonesDetail.Rows[i].Cells[1].Value = stk.StoneList[i].StoneTypeId;

                            this.dgvStonesDetail.Rows[i].Cells[2].Value = stk.StoneList[i].StoneId;


                            if (stk.StoneList[i].StoneWeight.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[3].Value = Convert.ToDecimal(stk.StoneList[i].StoneWeight).ToString("0.000");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                            if (stk.StoneList[i].Qty.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stk.StoneList[i].Qty);
                            else
                                this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                            if (stk.StoneList[i].Rate.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(stk.StoneList[i].Rate).ToString("0.0");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                            if (stk.StoneList[i].Price.HasValue)
                                this.dgvStonesDetail.Rows[i].Cells[6].Value = Convert.ToDecimal(stk.StoneList[i].Price).ToString("0");
                            else
                                this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;

                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ColorName.ColorName.ToString())))
                            {
                                for (int j = 0; j < this.cbxColorName.Items.Count; j++)
                                {
                                    StoneColor stc = (StoneColor)this.cbxColorName.Items[j];
                                    if (stk.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                        this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                                }
                            }

                            if (!(string.IsNullOrEmpty(stk.StoneList[i].CutName.CutName)))
                            {
                                for (int j = 0; j < this.cbxCutName.Items.Count; j++)
                                {
                                    StoneCut stc = (StoneCut)this.cbxCutName.Items[j];
                                    if (stk.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                                }
                            }

                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ClearityName.ClearityName.ToString())))
                            {
                                for (int j = 0; j < this.cbxClearity.Items.Count; j++)
                                {
                                    StoneClearity stc = (StoneClearity)this.cbxClearity.Items[j];
                                    if (stk.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                        this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                                }
                            }

                        }
                        decimal val1;
                        decimal val2;
                        val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                        val2 = upDateTextBox();
                        this.txtStoneWeight.Text = val2.ToString("0.000");
                        this.grossweight(val1, val2, txtGrossWeight);
                        this.txtTpriceOfStones.Text = updateSum().ToString("0");
                    }
                }
            }
            if (this.txtFixRate.Text != "" && Convert.ToDecimal(this.txtFixRate.Text) != 0)
            {
                decimal grate = FormControls.GetDecimalValue(this.txtFixRate, 0);
                grate = grate * Convert.ToDecimal(stk.Karrat) / 24;
                if (GramTolaRate == "Tola")
                {
                    grate = Math.Round(grate / (decimal)Formulas.WeightInGm, 0);
                }
                
                this.txtGoldRates.Text = grate.ToString("0.0");
            }
            else
            {
                //if (SaleRateFix == "Auto")
                //{
                //    if (GramTolaRate == "Gram" || GramTolaRate == "")
                //    {
                //        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(stk.Karrat.ToString(), DateTime.Today).ToString("0.0");
                //    }
                //    else
                //    {
                //        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(stk.Karrat.ToString(), DateTime.Today).ToString("0.0");
                //    }
                //}
                //else
                //{
                //    if (GramTolaRate == "Gram" || GramTolaRate == "")
                //    {
                //        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(SaleRateFix, DateTime.Today).ToString("0.0");
                //    }
                //    else
                //    {
                //        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(SaleRateFix, DateTime.Today).ToString("0.0");
                //    }
                //}

                //Start Qasim: Pound Pasa and Sona Pasa Work In Auto Generated Rates
                if (SaleRateFix == "Auto")
                {
                    if (GramTolaRate == "Gram" || GramTolaRate == "")
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(stk.Karrat.ToString(), DateTime.Today).ToString();
                    }
                    else if (Main.City == "Islamabad")
                    {
                        grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                        this.txtGoldRates.Text = grs.PoundPasa.ToString();
                    }
                    else
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(stk.Karrat.ToString(), DateTime.Today).ToString();
                    }
                }
                else
                {
                    if (GramTolaRate == "Gram" || GramTolaRate == "")
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKarat(SaleRateFix, DateTime.Today).ToString();
                    }
                    else
                    {
                        this.txtGoldRates.Text = this.grDAL.GetRateByKaratTola(SaleRateFix, DateTime.Today).ToString();
                    }
                }
                //End Qasim: Pound Pasa and Sona Pasa Work In Auto Generated Rates
            }
        }

        private void ShowAllRecordByStockId(Stock sobj)
        {

            if (sobj == null) return;
            else
            {
                stk = new Stock();
                stk = sobj;

                for (int k = 0; k < this.cbxGroupItem.Items.Count; k++)
                {
                    Item it = (Item)cbxGroupItem.Items[k];
                    if (sobj.ItemName.ItemId == it.ItemId)
                    {
                        cbxGroupItem.SelectedIndex = k;
                        break;
                    }
                }

                int val = (int)this.cbxGroupItem.SelectedValue;
                FormControls.FillCombobox(cbxTagNo, getTags(val), "TagNo", "StockId");
                this.cbxTagNo.SelectedValue = sobj.StockId;
                if(sobj.OrderNo > 0)
                {
                    chkOrderNo.Checked = true;
                    cbxOrderNo.Text = sobj.OrderNo.ToString();
                    cbxOrderTags.Text = sobj.TagNo;
                }
                else
                    chkOrderNo.Checked = false;

                if (sobj.SaleQty.HasValue)
                    this.txtQty.Text = sobj.SaleQty.ToString();
                else
                    this.txtQty.Text = "1";

                if (sobj.Pieces.HasValue)
                    this.txtPieces.Text = sobj.Pieces.ToString();
                else
                    this.txtPieces.Text = "";
                //this.cbxKarat.DataSource = sDAL.GetAllKarat();
                this.txtKarat.Text = stk.Karrat;
                if (sobj.RatePerGm == 0)
                {
                    decimal gramrate = grDAL.GetRateByKarat(sobj.Karrat, DateTime.Today);
                    this.txtGoldRates.Text = gramrate.ToString("0.0");
                }
                else
                    this.txtGoldRates.Text = ((decimal)sobj.RatePerGm).ToString("0.0");

                if (sobj.DesignNo!=null)
                this.cbxDesignNo.SelectedValue = sobj.DesignNo.DesignId;

                if (sobj.WorkerName != null)
                this.cbxWorkerName.SelectedValue = sobj.WorkerName.ID;

                this.txtKarat.Text = stk.Karrat;
                
                this.txtDescription.Text = sobj.Description.ToString();
                if (sobj.NetWeight.HasValue)
                    this.txtNetWeight.Text = Convert.ToDecimal(sobj.NetWeight).ToString("0.000");
                else
                    this.txtNetWeight.Text = "";

                if (sobj.WastePercent.HasValue)
                    this.txtWasteInPercent.Text = Convert.ToDecimal(sobj.WastePercent).ToString("0.0");
                else
                    this.txtWasteInPercent.Text = "";
                if (sobj.WasteInGm.HasValue)
                    this.txtWasteInGm.Text = Convert.ToDecimal(sobj.WasteInGm).ToString("0.000");
                else
                    this.txtWasteInGm.Text = "";
                this.txtTotalWeight.Text = Convert.ToDecimal(sobj.TotalWeight).ToString("0.000");
                if (sobj.MakingPerGm.HasValue)

                    this.txtMakingPerGm.Text = Convert.ToDecimal(sobj.MakingPerGm).ToString("0.0");
                else
                    this.txtMakingPerGm.Text = "";
               
                if (sobj.TotalMaking.HasValue)
                    this.txtTotalMaking.Text = Convert.ToDecimal(sobj.TotalMaking).ToString("0");
                else
                    this.txtTotalMaking.Text = "";
                if (sobj.LakerGm.HasValue)
                    this.txtLackerPerGm.Text = Convert.ToDecimal(sobj.LakerGm).ToString("0.0");
                else
                    this.txtLackerPerGm.Text = "";
                if (sobj.TotalLaker.HasValue)
                    this.txtTotalLacker.Text = Convert.ToDecimal(sobj.TotalLaker).ToString("0");
                else
                    this.txtTotalLacker.Text = "";
                if (sobj.OtherCharges.HasValue)
                    this.txtOtherCharges.Text = Convert.ToDecimal(sobj.OtherCharges).ToString("0");
                else
                    this.txtOtherCharges.Text = "";
                                
                if(Main.City == "Islamabad")
                {
                    if (pFlag == true)
                        this.txtGoldRates.Text = grs.PoundPasa.ToString("0");
                    else
                        this.txtGoldRates.Text = grs.SonaPasa.ToString("0");
                }
                if (sobj.TotalPrice.HasValue)
                    this.txtNetPrice.Text = Convert.ToDecimal(sobj.TotalPrice).ToString("0");
                else
                    this.txtNetPrice.Text = "";

                if (sobj.StoneList == null)
                {
                    decimal val1;
                    decimal val2;
                    val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                    val2 = upDateTextBox();
                    this.txtStoneWeight.Text = val2.ToString("0.000");
                    this.grossweight(val1, val2, txtGrossWeight);
                    this.txtTpriceOfStones.Text = updateSum().ToString("0");
                    return;
                }
                else
                {                   
                    this.dgvStonesDetail.AutoGenerateColumns = false;
                    this.dgvStonesDetail.Rows.Clear();
                    int count = sobj.StoneList.Count;
                    this.dgvStonesDetail.Rows.Add(count);
                    for (int i = 0; i < sobj.StoneList.Count; i++)
                    {                        
                        this.dgvStonesDetail.Rows[i].Cells[1].Value = sobj.StoneList[i].StoneTypeId;

                        this.cbxStoneName.DataSource = stDAL.GetAllStoneNamebyId(Convert.ToInt32(sobj.StoneList[i].StoneTypeId));
                        this.cbxStoneName.DisplayMember = "Name";
                        this.cbxStoneName.ValueMember = "Id";

                        this.dgvStonesDetail.Rows[i].Cells[2].Value = sobj.StoneList[i].StoneId;

                        if (sobj.StoneList[i].StoneWeight == 0)
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = Convert.ToDecimal(sobj.StoneList[i].StoneWeight).ToString("0.000");

                        if (sobj.StoneList[i].Qty == 0)
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(sobj.StoneList[i].Qty);

                        if (sobj.StoneList[i].Rate == 0)
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(sobj.StoneList[i].Rate).ToString("0.0");

                        if (sobj.StoneList[i].Price == 0)
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = Convert.ToDecimal(sobj.StoneList[i].Price).ToString("0");

                        if (sobj.StoneList[i].ColorName != null)                        
                        {
                            for (int j = 0; j < this.cbxColorName.Items.Count; j++)
                            {
                                StoneColor stc = (StoneColor)this.cbxColorName.Items[j];
                                if (sobj.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                            }
                        }
                        if (sobj.StoneList[i].CutName != null)
                        {
                            for (int j = 0; j < this.cbxCutName.Items.Count; j++)
                            {
                                StoneCut stc = (StoneCut)this.cbxCutName.Items[j];
                                if (sobj.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                            }
                        }
                        if (sobj.StoneList[i].ClearityName != null)
                        {
                            for (int j = 0; j < this.cbxClearity.Items.Count; j++)
                            {
                                StoneClearity stc = (StoneClearity)this.cbxClearity.Items[j];
                                if (sobj.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                            }
                        }
                    }
                }
                decimal val3, val4;
                val3 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                val4 = upDateTextBox();
                this.txtStoneWeight.Text = val4.ToString("0.000");
                this.grossweight(val3, val4, txtGrossWeight);
                this.txtTpriceOfStones.Text = updateSum().ToString("0");
            }
        }

        private void RefreshRecord()
        {
            this.txtPieces.Text = "";
            this.txtSize.Text = "";
            this.txtKarat.Text = "";
            this.txtQty.Text = "1";
            this.cbxDesignNo.SelectedIndex = -1;
            this.cbxWorkerName.SelectedIndex = -1;
            this.cbxTagNo.SelectedIndex = -1;            
            this.txtDescription.Text = "";
            this.txtNetWeight.Text = "";
            this.txtWasteInPercent.Text = "";
            this.txtWasteInGm.Text = "";
            this.txtTotalWeight.Text = "";
            this.txtMakingPerGm.Text = "";
            this.txtTotalMaking.Text = "";
            this.txtLackerPerGm.Text = "";
            this.txtTotalLacker.Text = "";           
            this.txtOtherCharges.Text = "";
            this.txtGoldRates.Text = "";
            this.txtGoldPrice.Text = "";
            this.txtGrossWeight.Text = "";
            this.txtTotalPrice.Text = "";           
            this.txtNetPrice.Text = "";
            this.txtTpriceOfStones.Text = "";
            this.txtStoneWeight.Text = "";
            this.lblHidden.Text = "";
            this.pbxPicture.Image = null;
        }

        private void cbxGroupItem_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged += new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
        }

        private void cbxTagNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.txtStoneWeight.Text = "0";
            this.txtTpriceOfStones.Text = "0";
            this.txtGrossWeight.Text = "0";
            this.cbxTagNo.SelectedIndexChanged += new System.EventHandler(this.cbxTagNo_SelectedIndexChanged);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (status == "Bulk")
            {
                if (this.txtSaleQ.Text == "")
                {
                    MessageBox.Show("Must Enter Bulk Sale Qty", Messages.Header);
                    return;
                }
            }
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
                if (txtNetWeight.Text == "")
                {
                    MessageBox.Show("Please select GroupItem to sale", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int i = 1;
                this.createSale(sender, e);

                object[] values1 = new Object[11];
                values1[0] = i.ToString();
                values1[1] = this.cbxTagNo.Text;

                Item itm = cbxGroupItem.SelectedItem as Item;
                values1[2] = itm.ItemName.ToString();
                values1[3] = this.cbxTagNo.SelectedValue.ToString();
                values1[4] = this.txtNetWeight.Text.ToString();
                values1[5] = this.txtWasteInGm.Text.ToString();
                values1[6] = this.txtTotalWeight.Text.ToString();
                values1[7] = this.txtTotalMaking.Text.ToString();
                values1[8] = this.txtTotalLacker.Text.ToString();
                values1[9] = this.txtTpriceOfStones.Text.ToString();
                values1[10] = this.cbxOrderNo.Text.ToString();

                int TEMP = values1.Length;
                this.dgvItemAdded.Rows.Add(values1);
                this.RefreshRecord();
                this.cbxGroupItem.SelectedIndex = -1;
                this.dgvStonesDetail.Rows.Clear();
            }
            else
            {
                if (txtNetWeight.Text == "")
                {
                    MessageBox.Show("Please select item to sale", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int i = 1;
                this.createSale(sender, e);

                object[] values = new Object[11];
                values[0] = i.ToString();
                if (cbxTagNo.SelectedIndex == -1 && this.chkOrderNo.Checked == false)
                {
                    MessageBox.Show("Please Select Tag No For Sale", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    bool sFlag = false;
                    sFlag = sDAL.isTagNoExist(stk.TagNo.ToString());
                    if (sFlag == true)
                    {
                        MessageBox.Show("You can not sale this Tag becuse it is sampled", Messages.Header);
                        return;
                    }
                    else
                    {
                        values[1] = stk.TagNo.ToString();

                        tagNoRpt = stk.TagNo.ToString();
                        Item itm = cbxGroupItem.SelectedItem as Item;
                        values[2] = itm.ItemName.ToString();
                        values[3] = stk.StockId.ToString();
                        values[4] = this.txtNetWeight.Text.ToString();
                        values[5] = this.txtWasteInGm.Text.ToString();
                        values[6] = this.txtTotalWeight.Text.ToString();
                        values[7] = this.txtTotalMaking.Text.ToString();
                        values[8] = this.txtTotalLacker.Text.ToString();
                        values[9] = this.txtTpriceOfStones.Text.ToString();
                        values[10] = this.cbxOrderNo.Text.ToString();
                        this.dgvItemAdded.Rows.Add(values);

                        bFlag = true;
                        int j = this.dgvItemAdded.Rows.Count;
                        this.txtKNo.Text = (slDAL.GetMaxKNo() + 1).ToString();
                        FormControls.FillCombobox(cbxTagNo, getTags(itm.ItemId), "TagNo", "StockId");
                        if (this.cbxOrderNo.SelectedIndex != -1)
                        {
                            this.cbxOrderTags.SelectedIndexChanged -= cbxOrderTags_SelectedIndexChanged;
                            FormControls.FillCombobox(cbxOrderTags, getTagsForOrder(Convert.ToInt32(this.cbxOrderNo.Text)), "TagNo", "StockId");
                        }
                        this.RefreshRecord();
                        this.dgvStonesDetail.Rows.Clear();
                        this.cbxOrderTags.Text = "";
                    }
                }
                this.RefreshRecord();
            }
            this.lblTNetWeight.Text = upDateTextBoxNetWeight().ToString("0.000");
            this.lblTotalWaste.Text = upDateTextBoxWaste().ToString("0.000");
            this.lblTotalWeight.Text = upDateTextBoxNetTotalWeight().ToString("0.000");
            this.lblTotalMaking.Text = upDateTextBoxTotalMaking().ToString("0");
            this.lblTotalLaker.Text = upDateTextBoxTotalLaker().ToString("0");
            this.lblStonePrice.Text = upDateTextBoxStonePrice().ToString("0");
            btnBill_Click(sender, e);
        }

        private void UpdateTexBox()
        {
            decimal sum = 0;
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (this.dgvStonesDetail.Rows.Count); counter++)
            {
                string str1 = (string)dgvStonesDetail.Rows[counter].Cells[3].Value.ToString();
                string str2 = dgvStonesDetail.Rows[counter].Cells[6].Value.ToString();
                if (!(string.IsNullOrEmpty(str1)))
                {
                    weight += Convert.ToDecimal(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString());
                }

                if (!(string.IsNullOrEmpty(str2)))
                {
                    sum += Convert.ToDecimal(dgvStonesDetail.Rows[counter].Cells[6].Value.ToString());
                }
            }
            this.txtTpriceOfStones.Text = sum.ToString("0");
            this.txtStoneWeight.Text = weight.ToString("0.000");
        }

        private void createSale(object sender, EventArgs e)
        {
            SaleLineItem sli = new SaleLineItem();
            sli.GPrice = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            sli.Stock = new Stock();
            if (pnlPasaRate.Visible == true)
            {
                if (rbtPoundPasa.Checked == true)
                    sli.Stock.pFlag = true;
                if (rbtSonaPasa.Checked == true)
                    sli.Stock.pFlag = false;
            }
            else
                sli.Stock.pFlag = null;
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

            string str = (string)this.cbxItemType.SelectedItem;
            if (string.IsNullOrEmpty(str))
                return;
            if (str.Equals("Gold"))
                sli.Stock.ItemType = ItemType.Gold;
            else if (str.Equals("Diamond"))
                sli.Stock.ItemType = ItemType.Diamond;
            else if (str.Equals("Silver"))
                sli.Stock.ItemType = ItemType.Silver;
            else if (str.Equals("Platinum"))
                sli.Stock.ItemType = ItemType.Platinum;
            else
                sli.Stock.ItemType = ItemType.Pladium;
            if (cbxGroupItem.SelectedIndex == -1)
            {
                MessageBox.Show("No Group Item Is Selected For Sale", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            sli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
            sli.Stock.Karrat = this.txtKarat.Text;

            if (Main.City == "Islamabad")
            {
                sli.GRate = FormControls.GetDecimalValue(this.txtGoldRates, 1);
                sli.Stock.RatePerGm = (decimal)(FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm);
                sli.Stock.RateType = "PoundPasa";
            }
            else
            {
                sli.GRate = 0;
                sli.Stock.RatePerGm = FormControls.GetDecimalValue(this.txtGoldRates, 1);
                sli.Stock.RateType = "Standerd";
            }
            sli.Stock.StoneCharges = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            if (stk.BStatus == "Bulk")
                sli.Stock.SaleQty = FormControls.GetIntValue(this.txtSaleQ);
            else
                sli.Stock.SaleQty = FormControls.GetIntValue(this.txtQty);
            sli.Stock.SaleDate = this.dtpSaleDate.Value;
            if (txtPieces.Text == "")
                sli.Stock.Pieces = 1;
            else
                sli.Stock.Pieces = FormControls.GetIntValue(this.txtPieces);
            if (this.txtSize.Text == "")
                sli.Stock.ItemSize = null;
            else sli.Stock.ItemSize = this.txtSize.Text;
            if (this.cbxDesignNo.SelectedIndex == -1)
            {
                sli.Stock.DesignNo.DesignId = 0;
            }
            else
            sli.Stock.DesignNo = (Design)this.cbxDesignNo.SelectedItem;
            if (this.cbxWorkerName.SelectedIndex == -1)
            {
                sli.Stock.WorkerName = new Worker();
                sli.Stock.WorkerName.ID = 0;
            }
            else
            sli.Stock.WorkerName = (Worker)this.cbxWorkerName.SelectedItem;
            if (txtDescription.Text == "")
                sli.Stock.Description = null;
            else
                sli.Stock.Description = this.txtDescription.Text;
            sli.Stock.SaleWeight = FormControls.GetDecimalValue(this.txtNetWeight, 3);
            if (desc.Length == 0)
                sli.Stock.ChWtDesc = "";
            else
                sli.Stock.ChWtDesc = desc;
            sli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtNetWeight, 3);
            sli.Stock.WastePercent = FormControls.GetDecimalValue(this.txtWasteInPercent, 1);
            sli.Stock.SaleWasteInGm = FormControls.GetDecimalValue(this.txtWasteInGm, 3);
            sli.Stock.WasteInGm = FormControls.GetDecimalValue(this.txtWasteInGm, 3);
            frm.RatiMashaTolaGeneral(Convert.ToDecimal(sli.Stock.WasteInGm));
            sli.Stock.WTola = frm.Tola;
            sli.Stock.WMasha = frm.Masha;
            sli.Stock.WRatti = frm.Ratti;

            sli.Stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            sli.Stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtTotalWeight, 3));
            sli.Stock.TTola = frm.Tola;
            sli.Stock.TMasha = frm.Masha;
            sli.Stock.TRatti = frm.Ratti;
            sli.Stock.BStatus = stk.BStatus;

            if (stk.BStatus == "Bulk" && saleQty < stk.BQuantity)
            { sli.Stock.Status = "Available"; }
            else if (stk.BStatus == "Bulk" && saleQty == stk.BQuantity)
            { sli.Stock.Status = "Not Available"; }
            else
            { sli.Stock.Status = "Not Available"; }
            if (this.txtDescription.Text == "")
                sli.Stock.Description = "";
            else
                sli.Stock.Description = this.txtDescription.Text;

            sli.Stock.OtherCharges = FormControls.GetDecimalValue(this.txtOtherCharges, 0);
            sli.Stock.MakingPerGm = FormControls.GetDecimalValue(this.txtMakingPerGm, 1);
            sli.Stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            sli.Stock.LakerGm = FormControls.GetDecimalValue(this.txtLackerPerGm, 1);
            sli.Stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            sli.Stock.GrossWeight = FormControls.GetDecimalValue(this.txtGrossWeight, 3);

            sli.Stock.KaatInRatti = FormControls.GetDecimalValue(this.txtkaat, 0); ;
            sli.Stock.CKaat = 0;
            sli.Stock.CWaste = 0;
            sli.Stock.CPureWeight = 0;
            sli.Stock.Discount = 0;

            sli.Stock.StoneList = this.GetAllDetails();
            sli.Stock.TotalPrice = FormControls.GetDecimalValue(this.txtTotalPrice, 0);
            sli.Stock.NetAmount = FormControls.GetDecimalValue(this.txtNetPrice, 0);

            decimal nwt = FormControls.GetDecimalValue(this.txtNetWeight, 3);
            sli.Stock.OPWeight = 0;
            sli.Bool = false;
            if (wt != nwt)
            {
                sli.Bool = true;
                sli.Stock.TagNo = stk.TagNo;
                sli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                if (string.IsNullOrEmpty(str))
                    return;
                if (str.Equals("Gold"))
                    sli.Stock.ItemType = ItemType.Gold;
                else if (str.Equals("Diamond"))
                    sli.Stock.ItemType = ItemType.Diamond;
                else if (str.Equals("Silver"))
                    sli.Stock.ItemType = ItemType.Silver;
                else if (str.Equals("Platinum"))
                    sli.Stock.ItemType = ItemType.Platinum;
                else
                    sli.Stock.ItemType = ItemType.Pladium;

                sli.ChangeWeight = wt - nwt;
                sli.ChangeQty = qut - FormControls.GetIntValue(this.txtQty);
            }
            s.AddLineItems(sli);
        }

        private List<Stones> GetAllDetails()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStonesDetail.Rows.Count) - 1;
            if (j == 0)
                return stDetail;
            else
            {
                stDetail = new List<Stones>();
                DataGridViewComboBoxColumn cbxStone = (DataGridViewComboBoxColumn)dgvStonesDetail.Columns["Column3"];

                for (int i = 0; i < j; i++)
                {
                    Stones std = new Stones();
                    if ((string)dgvStonesDetail.Rows[i].Cells[1].FormattedValue == string.Empty)
                        std.StoneId = null;
                    else
                        std.StoneTypeId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[1].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[2].FormattedValue == string.Empty)
                        std.StoneId = null;
                    else
                        std.StoneId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[2].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[3].FormattedValue == "")
                        std.StoneWeight = 0;
                    else
                        std.StoneWeight = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[3].Value.ToString()), 3);
                    if ((string)dgvStonesDetail.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = 0;
                    else
                        std.Qty = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[4].Value.ToString());

                    if ((string)dgvStonesDetail.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = 0;
                    else
                        std.Rate = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[5].Value.ToString()), 1);

                    if ((string)dgvStonesDetail.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = 0;
                    else
                        std.Price = Math.Round(Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[6].Value.ToString()), 0);

                    if ((string)dgvStonesDetail.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {
                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < this.cbxColorName.Items.Count; k++)
                        {
                            StoneColor stc = (StoneColor)cbxColorName.Items[k];
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
                        for (int k = 0; k < this.cbxCutName.Items.Count; k++)
                        {
                            StoneCut stc = (StoneCut)cbxCutName.Items[k];
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
                        for (int k = 0; k < this.cbxClearity.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)cbxClearity.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }
        #region TextChange

        private void txtGoldRates_TextChanged(object sender, EventArgs e)
        {
            if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 0) / Formulas.WeightInGm), txtGoldPrice);
            else
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 0), txtGoldPrice);
        }

        private void txtGoldPrice_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtTotalWeight_TextChanged(object sender, EventArgs e)
        {
            if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
            else
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);
        }
        #endregion

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    if (e.KeyCode == Keys.Decimal)
                    {
                        if (e.KeyCode != Keys.Back)
                        {
                            NonCharEnter = true;
                        }
                    }
                }
            }
        }

        private void grossweight(decimal val1, decimal val2, TextBox textTotal)
        {
            textTotal.Text = (val1 + val2).ToString("0.000");
        }

        private void goldprice(decimal val1, decimal val2, TextBox textGoldPrice)
        {
            if (val1 == grs.PoundPasa || val1 == grs.SonaPasa)
                textGoldPrice.Text = (val1 / (decimal)11.664 * val2).ToString("0");
            else
                textGoldPrice.Text = (val1 * val2).ToString("0");
        }

        private void cbxItemType_SelectedValueChanged(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            string str = (string)cbxItemType.SelectedItem;
            if (str == "Silver")
            {
                this.Hide();
                ManageSilverSale frm = new ManageSilverSale();
                frm.tagNo = tagNo1;
                frm.ShowDialog();                
            }
            else
                cbxGroupItem.DataSource = itmDAL.GetAllItemByType(str);
            cbxGroupItem.DisplayMember = "ItemName";
            cbxGroupItem.ValueMember = "ItemId";
            cbxGroupItem.SelectedIndex = -1;
        }

        private void dgvStonesDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    decimal val = Convert.ToDecimal(dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value);
                    string s = val.ToString("N3");
                    dgvStonesDetail.Rows[e.RowIndex].Cells["Column4"].Value = s.ToString();
                }
                catch { }
            }
            if (Convert.ToString(dgvStonesDetail.Rows[e.RowIndex].Cells[1].Value) == "Diamond")
            {
                cbxColorName.ReadOnly = false;
                cbxCutName.ReadOnly = false;
                cbxClearity.ReadOnly = false;
            }
            else
            {
                cbxColorName.ReadOnly = true;
                cbxCutName.ReadOnly = true;
                cbxClearity.ReadOnly = true;
            }
            decimal val1;
            decimal val2;
            val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            val2 = upDateTextBox();
            this.txtStoneWeight.Text = val2.ToString("0.000");
            this.grossweight(val1, val2, txtGrossWeight);
            this.txtTpriceOfStones.Text = updateSum().ToString("0");

            this.dgvStonesDetail.RefreshEdit();
        }

        private List<Stock> GetAllTags(int id)
        {
            List<Stock> stoks = new List<Stock>();
            stoks = sDAL.GetAllTagNoByItemId(id);

            Stock newItem = new Stock();
            newItem.TagNo = "";
            stoks.Add(newItem);

            if (bFlag == true)
            {
                foreach (Stock s in stoks)
                {
                    if (s == null)
                        return stoks;
                    for (int i = 0; i < this.dgvItemAdded.Rows.Count; i++)
                    {
                        int stkid = Convert.ToInt32(this.dgvItemAdded.Rows[i].Cells[3].Value.ToString());
                    }
                }
            }
            return stoks;
        }

        private bool RemoveOneTag(Stock stock)
        {
            if (s.SaleLineItem[s.SaleLineItem.Count - 1].Stock.StockId == stock.StockId)
                return true;
            else
                return false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int saleNoRpt = 0;
            if (this.cbxCustomerName.Text == "")
            {
                MessageBox.Show("Must Enter Customer Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (this.dgvItemAdded.Rows.Count < 1)
            {
                MessageBox.Show("There is No any Item to Added", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            con.Open();
            trans = con.BeginTransaction();
            this.txtPreviousSaleNo.Text = txtSaleNo.Text;
            #region savecustomer
            if (this.cbxCustomerName.Text != "" && cbxCustomerName.SelectedIndex == -1 && (lblOrderNo.Text == "" || lblOrderNo.Text == "0"))
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
                    cust.Date = Convert.ToDateTime(this.dtpSaleDate.Value);
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
            try
            {
                s.SVNO = vDAL.CreateVNO("SAV", con, trans);
                s.SaleNo = Convert.ToInt32(txtSaleNo.Text);
                s.SDate = Convert.ToDateTime(dtpDate.Value);
                if (lblOrderNo.Text != "")
                {
                    s.ODate = Convert.ToDateTime(dtpOrderDate.Value);
                    s.OrderNo = Convert.ToInt32(this.lblOrderNo.Text);
                    s.Status = "Order Sale";
                }
                else
                {
                    s.ODate = null;
                    s.OrderNo = 0;
                    s.Status = "Stock Sale";
                }
                s.CustName = new Customer();
                s.CustName.ID = Convert.ToInt32(this.lblCustId.Text);
                s.CustName.Name = cbxCustomerName.Text;
                s.CustName.ContactNo = cbxContactNo.Text;
                s.CustName.Address = txtAddress.Text;
                s.CusAccountNo = s.OrderNo > 0 ? sale.CustName.AccountCode : cust.AccountCode;
                s.TotalMaking = FormControls.GetDecimalValue(txtTMaking, 0);
                s.TotalLaker = FormControls.GetDecimalValue(txtTLacker, 0);
                s.TotalOtherCharges = FormControls.GetDecimalValue(txtTOtherCharges, 0);
                s.StoneCharges = FormControls.GetDecimalValue(txtTStoneCharges, 0);
                if (this.txtFixRate.Text != "" && this.txtFixRate.Text != "0")
                {
                    s.TotalGoldPrice = 0;
                    s.TotalPrice = FormControls.GetDecimalValue(txtTPrice, 0);
                }
                else
                {
                    s.TotalGoldPrice = 0;
                    s.OrderRate = FormControls.GetDecimalValue(this.txtOrderRate, 1);
                    s.TotalPrice = FormControls.GetDecimalValue(txtTPrice, 0);
                }
                s.TotalItemDiscount = FormControls.GetDecimalValue(txtTItemDisc, 0);
                s.OrderTaker = "";
                if (this.cbxSaleMan.SelectedIndex > -1)
                {
                    s.SalesMan = new SaleMan();
                    s.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                }
                else
                {
                    s.SalesMan = new SaleMan();
                    s.SalesMan.ID = 0;
                }
                s.Baddats = 0;
                s.TotalNetPrice = FormControls.GetDecimalValue(txtNetBill, 0);
                s.BillDiscout = FormControls.GetDecimalValue(txtBillDiscount, 0);
                s.NetBill = FormControls.GetDecimalValue(txtNetBill, 0);
                s.CashReceive = FormControls.GetDecimalValue(txtCashReceive, 0);
                s.CashPayment = FormControls.GetDecimalValue(txtCashPayment, 0);
                s.CreditCard = FormControls.GetDecimalValue(txtCreditCard, 0);
                s.CheckCash = FormControls.GetDecimalValue(txtCheck, 0);
                s.PureGoldCharges = FormControls.GetDecimalValue(txtPureGold, 0);
                s.UsedGoldCharges = FormControls.GetDecimalValue(txtUsedGold, 0);
                s.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceive, 0);
                s.Balance = FormControls.GetDecimalValue(txtBalance, 0);
                s.TotalGold = 0;
                s.GoldReceived = 0;
                s.GoldBalance = 0;
                if (this.txtBillBookNo.Text == "")
                    s.BillBookNo = "0";
                else
                    s.BillBookNo = txtBillBookNo.Text;
                if (this.txtKNo.Text == "")
                    s.KhataNo = 0;
                s.KhataNo = Convert.ToInt32(txtKNo.Text);
                if (this.chkPromiseDate.Checked == true)
                    s.PromiseDate = Convert.ToDateTime(this.dtpPromiseDate.Text);

                if (this.cbxSaleMan.SelectedIndex > -1)
                {
                    s.SalesMan = new SaleMan();
                    s.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                }
                else
                {
                    s.SalesMan = new SaleMan();
                    s.SalesMan.ID = 0;
                }
                s.OthrChargesGold = 0;
                s.GoldChargesGold = 0;
                s.NetBillGold = 0;
                s.TotalGold = 0;
                s.GoldReceived = 0;
                s.GoldBalance = 0;
                s.OFixRate = 0;
                s.BillInWord = nmb.changeNumericToWords(FormControls.GetDecimalValue(this.txtNetBill, 0));
                s.ListOfChequs = this.ListOfChecks;
                s.ListOfCreditCards = this.ListOfCreditCard;

                foreach (SaleLineItem sl in s.SaleLineItem)
                {
                    stDAL.DeleteStonesByTagNo(sl.Stock.TagNo.ToString(), con, trans);
                }
                slDAL.AddSale(s, out saleNoRpt, con, trans);
                string saletagno = "";
                for (int i = 0; i <= this.dgvItemAdded.Rows.Count - 1; i++)
                {
                    saletagno = saletagno + this.dgvItemAdded.Rows[i].Cells[1].Value.ToString() + " ";
                }

                #region Vouchers

                #region Sale Voucher
                salv = new Voucher();
                ChildAccount cha = new ChildAccount();
                cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                if (cha == null)
                {
                    string Coode = acDAL.CreateAccount(4, "Income", "Sale", "General Account", con, trans);
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                }
                cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                salv.AccountCode = cha;
                salv.Cr = s.NetBill;
                salv.Dr = 0;
                salv.DDate = (DateTime)s.SDate;
                salv.SNO = saleNoRpt;
                salv.OrderNo = s.OrderNo;
                salv.VNO = s.SVNO;
                if (s.OrderNo > 0)
                    salv.Description = "Bill Of Order S.No." + s.SaleNo.ToString();
                else
                    salv.Description = "Bill Of S.No." + s.SaleNo.ToString();
                vDAL.AddVoucher(salv, con, trans);

                pv = new Voucher();
                pv.SNO = saleNoRpt;
                pv.OrderNo = s.OrderNo;
                acDAL = new AccountDAL();
                ChildAccount child = new ChildAccount();
                child = acDAL.GetChildByCode(s.CusAccountNo, con, trans);
                pv.AccountCode = child;
                pv.VNO = s.SVNO;
                pv.Dr = s.NetBill;
                pv.Cr = 0;
                pv.DDate = (DateTime)s.SDate;
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
                    pv.Dr = Convert.ToDecimal(s.CashReceive);
                    pv.Cr = 0;
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                    if (s.OrderNo > 0)
                        pv.Description = "Cash Recieved From Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Cash Recieved From S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    custv = new Voucher();
                    custv.AccountCode = child;
                    custv.Cr = (decimal)s.CashReceive;
                    custv.Dr = 0;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = saleNoRpt;
                    sp1.ONo = (int)s.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cash";
                    sp1.PTime = "Sale";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = (DateTime)s.SDate;
                    sp1.BDrate = 0;
                    sp1.BankName = "";
                    sp1.Amount = (decimal)s.CashReceive;
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
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = (int)s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                    if (s.OrderNo > 0)
                        pv.Description = "Cash Paid To Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Cash Paid To S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    custv = new Voucher();
                    custv.AccountCode = salv.AccountCode;
                    custv.Cr = 0;
                    custv.Dr = pv.Cr;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = (int)s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = saleNoRpt;
                    sp1.ONo = (int)s.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cash";
                    sp1.PTime = "Sale";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = (DateTime)s.SDate;
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
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                    string str = pv.VNO;
                    if (s.OrderNo > 0)
                        pv.Description = "Cash Recieved By Credit Card From Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Cash Recieved By Credit Card From S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                    custv = new Voucher();
                    custv.AccountCode = salv.AccountCode;
                    custv.Cr = pv.Dr;
                    custv.Dr = 0;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    pv = new Voucher();
                    cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                    pv.AccountCode = cha;
                    pv.Dr = ExtraMoney;
                    pv.Cr = 0;
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = str;
                    pv.Description = custv.Description;
                    vDAL.AddVoucher(pv, con, trans);

                    pv = new Voucher();
                    cha = new ChildAccount();
                    cha.HeadCode = 1;
                    cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                    if (cha == null)
                    {
                        string Coode = acDAL.CreateAccount(1, "Income", "Credit Card Extra", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Income", "Credit Card Extra", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Cr = ExtraMoney;
                    pv.Dr = 0;
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = (int)s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = str;
                    pv.Description = custv.Description;
                    vDAL.AddVoucher(pv, con, trans);

                    cc.VNO = str;
                    cc.SNO = saleNoRpt;
                    cc.ONO = (int)s.OrderNo;
                    slDAL.AddCreditCards(cc, con, trans);

                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = saleNoRpt;
                    sp1.ONo = (int)s.OrderNo;
                    sp1.VNo = str;
                    sp1.PMode = "Credit Card";
                    sp1.PTime = "Sale";
                    sp1.Receiveable = (decimal)cc.SwapAmount;
                    sp1.DRate = cc.DeductRate;
                    sp1.DDate = (DateTime)s.SDate;
                    sp1.BDrate = cc.BankDeductRate;
                    sp1.BankName = cc.BankName.BankName;
                    sp1.Amount = cc.Amount;
                    sp1.Description = custv.Description;
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
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                    if (s.OrderNo > 0)
                        pv.Description = "Cheque Recieved From Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Cheque Recieved From S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    custv = new Voucher();
                    custv.AccountCode = salv.AccountCode;
                    custv.Cr = pv.Dr;
                    custv.Dr = 0;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    chq.SNO = s.SaleNo;
                    chq.ONO = (int)s.OrderNo;
                    chq.CustAccountCode = s.CusAccountNo;
                    slDAL.AddChecques(chq, con, trans);

                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = saleNoRpt;
                    sp1.ONo = (int)s.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cheque";
                    sp1.PTime = "Sale";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = (DateTime)s.SDate;
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
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                    if (s.OrderNo > 0)
                        pv.Description = "Used Gold Purchased From Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Used Gold Purchased From S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    custv = new Voucher();
                    custv.AccountCode = salv.AccountCode;
                    custv.Cr = pv.Dr;
                    custv.Dr = 0;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    gld.PGDate = (DateTime)s.SDate;
                    gld.SNO = saleNoRpt;
                    gld.ONO = (int)s.OrderNo;
                    gld.VNO = pv.VNO;
                    gld.CustId = custid;
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
                    pv.DDate = (DateTime)s.SDate;
                    pv.OrderNo = s.OrderNo;
                    pv.SNO = saleNoRpt;
                    pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                    if (s.OrderNo > 0)
                        pv.Description = "Pure Gold Purchased From Order S.No." + s.SaleNo.ToString();
                    else
                        pv.Description = "Pure Gold Purchased From S.No." + s.SaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);

                    custv = new Voucher();
                    custv.AccountCode = salv.AccountCode;
                    custv.Cr = pv.Dr;
                    custv.Dr = 0;
                    custv.DDate = (DateTime)s.SDate;
                    custv.OrderNo = s.OrderNo;
                    custv.SNO = saleNoRpt;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);

                    gld.PGDate = (DateTime)s.SDate;
                    gld.SNO = saleNoRpt;
                    gld.VNO = pv.VNO;
                    gld.CustId = custid;
                    gld.Description = pv.Description;
                    gld.ONO = (int)s.OrderNo;
                    gld.PTime = "Sale";
                    gld.PMode = "Rec";
                    gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                    slDAL.AddGoldDetail(gld, con, trans);
                }
                #endregion

                #endregion
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
                    MessageBox.Show(Messages.Saved, Messages.Header);
                    this.RefreshBillTab();
                    this.dgvItemAdded.Rows.Clear();
                    this.txtTROrderAmount.Text = "";
                    slDAL.CompleteSaleFromBStock();
                    ReportViewer frm = new ReportViewer();
                    frm.isPage = 3;
                    frm.rpt = 4;
                    frm.sNo = saleNoRpt;
                    frm.ShowDialog();
                    this.Dispose();
                    ManageSale f = new ManageSale();
                    f.ShowDialog();
                }
            }
        }

        private void CalculateBill(decimal amount, decimal cons, decimal alcash, TextBox txt)
        {
            decimal netBill = Math.Round((amount - (cons + alcash)), 0);
            txt.Text = netBill.ToString("0");
        }

        private decimal calculatePayment()
        {
            decimal cashrecieve = 0, creditcard = 0, checque = 0, ugold = 0, pgold = 0, cashrecieved = 0, rgreceived = 0, cashPayment = 0;
            cashrecieved = FormControls.GetDecimalValue(this.txtCashReceived, 0);
            rgreceived = FormControls.GetDecimalValue(this.txtRGPrice, 0);
            cashrecieve = FormControls.GetDecimalValue(this.txtCashReceive, 0);
            creditcard = FormControls.GetDecimalValue(this.txtCreditCard, 0);
            checque = FormControls.GetDecimalValue(this.txtCheck, 0);
            pgold = FormControls.GetDecimalValue(this.txtPureGold, 0);
            ugold = FormControls.GetDecimalValue(this.txtUsedGold, 0);
            cashPayment = FormControls.GetDecimalValue(this.txtCashPayment, 0);
            return (cashrecieve + creditcard + checque + ugold + pgold + cashrecieved + rgreceived) - cashPayment;
        }
        private decimal calculatePaymentforcash()
        {
            decimal creditcard = 0, checque = 0, ugold = 0, pgold = 0;
            creditcard = FormControls.GetDecimalValue(this.txtCreditCard, 0);
            checque = FormControls.GetDecimalValue(this.txtCheck, 0);
            pgold = FormControls.GetDecimalValue(this.txtPureGold, 0);
            ugold = FormControls.GetDecimalValue(this.txtUsedGold, 0);
            return creditcard + checque + ugold + pgold;
        }
        private void TotalRecieve(decimal amount, decimal amount1, TextBox txt)
        {
            decimal tReciev = Math.Round((amount + amount1), 0);
            txt.Text = tReciev.ToString();
        }

        private void Gold(decimal rate, decimal cash, TextBox txt, Label lbl)
        {
            decimal gold = (decimal)Math.Round((cash / rate), 0);
            txt.Text = gold.ToString();
            frm.RatiMashaTola(Convert.ToDecimal(gold), lbl);
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
                this.tabControl1.SelectedTab = tabPage2;
                this.txtTMaking.Text = Math.Round(s.GetTotalMaking(), 0).ToString();
                this.txtTLacker.Text = Math.Round(s.GetTotalLaker(), 0).ToString();
                this.txtTOtherCharges.Text = Math.Round(s.GetOtherChargesTotal(), 0).ToString();
                this.txtTStoneCharges.Text = Math.Round(s.GetAllStoneCharges(), 0).ToString();
                this.txtTPrice.Text = (Math.Round(s.GetGrossTotal(), 0)).ToString();
                this.txtTItemDisc.Text = (Math.Round(s.GetTotalDisount(), 0)).ToString();
                this.txtOTotalCash.Text = Math.Round(s.GetTotalOtherCharges(), 0).ToString();

                decimal a = 0;
                a = Math.Round(s.GetItemNetTotal());
                if (this.txtCashReceived.Text != "" && FormControls.GetDecimalValue(this.txtCashReceived, 0) != 0)
                {
                    this.txtTPrice.Text = a.ToString("0");
                    this.txtTROrderAmount.Text = (FormControls.GetDecimalValue(this.txtCashReceived, 0) + FormControls.GetDecimalValue(this.txtRGPrice, 0)).ToString();
                }
                else if (this.txtCashReceived.Text != "")
                {
                    this.txtTPrice.Text = a.ToString("0");
                    this.txtTROrderAmount.Text = (FormControls.GetDecimalValue(this.txtCashReceived, 0)).ToString();
                }
                else if (this.txtRGPrice.Text != "" && this.txtRGPrice.Text != "0")
                {
                    this.txtTPrice.Text = a.ToString("0");
                    this.txtTROrderAmount.Text = (FormControls.GetDecimalValue(this.txtRGPrice, 0)).ToString();
                }
                else
                    this.txtTPrice.Text = a.ToString("0");
                if (this.txtTROrderAmount.Text != "")
                    totalRecive = FormControls.GetDecimalValue(this.txtTROrderAmount, 0);
                if (this.txtCashReceived.Text != "" && this.txtRGPrice.Text != "")
                {
                    this.txtNetBill.Text = this.txtTPrice.Text;
                    this.txtTPrice.Text = this.txtNetBill.Text;
                }
                else if (this.txtCashReceived.Text != "")
                {
                    this.txtNetBill.Text = this.txtTPrice.Text;
                    this.txtTPrice.Text = this.txtNetBill.Text;
                }
                else if (this.txtRGPrice.Text != "")
                {
                    this.txtNetBill.Text = this.txtTPrice.Text;
                    this.txtTPrice.Text = this.txtNetBill.Text;
                }
                else
                    this.txtNetBill.Text = s.GetItemNetTotal().ToString("0");
                this.txtBillDiscount.Text = s.BillDiscout.ToString("0");
                s.OthrChargesGold = 0;

                bool gFlag = true;
                if (gFlag == true)
                    this.txtTCPWeight.Text = s.GetTotalCPuerWeight().ToString("0.000");
                else
                    this.txtTCPWeight.Text = "0";

                if (eFlag)
                {
                    if (this.txtTPrice.Text == "0")
                    {
                        this.txtTPrice.Text = s.GetGrossTotal().ToString("0");
                        this.txtNetBill.Text = (Convert.ToDecimal(txtTPrice.Text) - Convert.ToDecimal(txtBillDiscount.Text)).ToString("0");
                    }
                    this.txtBalance.Text = (FormControls.GetDecimalValue(this.txtNetBill, 0) - FormControls.GetDecimalValue(this.txtTotalReceive, 0)).ToString();
                }
                decimal ans = decimal.Parse(txtTCPWeight.Text);
                if (this.chkOrderNo.Checked == true)
                    this.pnlOrderPayment.Visible = true;
                else
                    this.pnlOrderPayment.Visible = false;
                if (this.chkOrderNo.Checked && (this.txtFixRate.Text == "" || this.txtFixRate.Text == "0"))
                {
                    decimal orderGold = 0, orderPrice = 0;
                    orderGold = Math.Round(s.GetToltalOPWeight(), 3);
                    this.txtRWeight.Text = (s.GetToltalOPWeight() - (this.SumGoldofCash() + upTextBox())).ToString("N3");
                    orderPrice = (((grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpSaleDate.Value))) * FormControls.GetDecimalValue(this.txtRWeight, 3)) + (FormControls.GetDecimalValue(this.txtReceivedAmount, 0)));
                    if (orderGold == 0)
                        this.txtOrderRate.Text = "";
                    else
                        this.txtOrderRate.Text = (orderPrice / orderGold).ToString("0.0");
                    this.txtNetBill.Text = this.txtTPrice.Text;
                    this.txtTPrice.Text = ((this.txtTPrice.Text == "" ? 0 : Convert.ToDecimal(this.txtTPrice.Text)) - (this.txtBillDiscount.Text == "" ? 0 : Convert.ToDecimal(this.txtBillDiscount.Text))).ToString("0");
                    this.txtNetBill.Text = ((this.txtTPrice.Text == "" ? 0 : Convert.ToDecimal(this.txtTPrice.Text)) - (this.txtBillDiscount.Text == "" ? 0 : Convert.ToDecimal(this.txtBillDiscount.Text))).ToString("0");
                    decimal val = 0;
                    if (this.chkOrderNo.Checked == true)
                        val = FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + this.calculatePaymentforcash() + FormControls.GetDecimalValue(this.txtCashReceive, 0);
                    else
                        val = this.calculatePaymentforcash();
                    this.txtTotalReceive.Text = val.ToString();
                }
            this.txtNetBill.BackColor = Color.Azure;
            TotalPayment();
            this.txtBillBookNo.Select();
        }

        public void TotalPayment()
        {
            this.txtTotalReceive.Text = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + (FormControls.GetDecimalValue(this.txtCashReceive, 0) + FormControls.GetDecimalValue(this.txtCreditCard, 0) + FormControls.GetDecimalValue(this.txtCheck, 0) + FormControls.GetDecimalValue(this.txtUsedGold, 0) + FormControls.GetDecimalValue(this.txtPureGold, 0))).ToString("0");
            this.txtBalance.Text = (FormControls.GetDecimalValue(this.txtNetBill, 0) - FormControls.GetDecimalValue(this.txtTotalReceive, 0)).ToString();
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(s.SVNO, s.SaleNo, 1, sum);
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
            frmpayment = new PaymentOption(s.SVNO, s.SaleNo, 2, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfCheques = this.ListOfChecks;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtCheck.Text = sum.ToString("0");
        }

        public void GetList(List<Cheques> lst)
        {
            decimal sum = 0;
            foreach (Cheques chq in lst)
            {
                sum += chq.Amount;
            }
        }

        private void btnUsedGold_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption(s.SVNO, s.SaleNo, 4, sum);
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
            frmpayment = new PaymentOption(s.SVNO, s.SaleNo, 3, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfPureGold = this.ListOfPureGold;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtPureGold.Text = sum.ToString("0");
        }

        private void getOrderNo()
        {
            int _orderNo = 0;
            int count = dgvItemAdded.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                if (!(String.IsNullOrEmpty(dgvItemAdded.Rows[i].Cells[10].Value.ToString())))
                {
                    _orderNo = Convert.ToInt32(dgvItemAdded.Rows[i].Cells[10].Value);
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

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.dgvItemAdded.SelectedRows.Count > 0)
            {
                strTagNo = this.dgvItemAdded.SelectedRows[0].Cells[1].Value.ToString();
                dgvItemAdded.Rows.RemoveAt(this.dgvItemAdded.SelectedRows[0].Index);
                SaleLineItem sl = new SaleLineItem();
                foreach (SaleLineItem sli in s.SaleLineItem)
                {
                    if (sli.Stock.TagNo == strTagNo)
                        sl = sli;
                }
                if (sl != null)
                    s.RemoveLineItems(sl);
                getOrderNo();
                Item itm1 = this.cbxGroupItem.SelectedItem as Item;
                FormControls.FillCombobox(cbxTagNo, getTags(itm1.ItemId), "TagNo", "StockId");
                btnBill_Click(sender, e);
                this.RefreshRecord();
                this.dgvStonesDetail.Rows.Clear();
               
            }      
            else
            {
                MessageBox.Show("Plz select any row to delete", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.lblTNetWeight.Text = upDateTextBoxNetWeight().ToString("0.000");
            this.lblTotalWaste.Text = upDateTextBoxWaste().ToString("0.000");
            this.lblTotalWeight.Text = upDateTextBoxNetTotalWeight().ToString("0.000");
            this.lblTotalMaking.Text = upDateTextBoxTotalMaking().ToString("0");
            this.lblTotalLaker.Text = upDateTextBoxTotalLaker().ToString("0");
            this.lblStonePrice.Text = upDateTextBoxStonePrice().ToString("0");
        }

        private void RemoveSale(string tagNo)
        {
            foreach (SaleLineItem sli in s.SaleLineItem)
            {
                if (tagNo.Equals(sli.Stock.TagNo))
                {
                    s.RemoveLineItems(sli);
                    return;
                }
            }
        }

        private void txtNetBill_TextChanged(object sender, EventArgs e)
        {
            this.CalculateBill(FormControls.GetDecimalValue(this.txtNetBill, 0), FormControls.GetDecimalValue(this.txtTotalReceive, 0), 0, txtBalance);
        }

        private void updateCashReceive(decimal amount, decimal val, TextBox txt)
        {
            if (this.chkOrderNo.Checked == false)
            {
                decimal cashReceive = amount + val;
                txt.Text = cashReceive.ToString("0");
            }
            else if (this.chkOrderNo.Checked)
            {
                decimal cashReceive = amount + val + Convert.ToDecimal(this.txtReceivedAmount.Text);
                txt.Text = cashReceive.ToString("0");
            }
        }

        private void txtCashReceive_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTotalReceive_TextChanged(object sender, EventArgs e)
        {
            this.CalculateBill(FormControls.GetDecimalValue(this.txtNetBill, 0), FormControls.GetDecimalValue(this.txtTotalReceive, 0), 0, txtBalance);
        }

        private void txtCreditCard_TextChanged(object sender, EventArgs e)
        {
            this.updateCashReceive(this.calculatePayment(), 0, txtTotalReceive);
        }

        private void txtCheck_TextChanged(object sender, EventArgs e)
        {
            this.updateCashReceive(this.calculatePayment(), 0, txtTotalReceive);            
        }

        private void txtUsedGold_TextChanged(object sender, EventArgs e)
        {
            this.updateCashReceive(this.calculatePayment(), 0, txtTotalReceive);            
        }

        private void txtPureGold_TextChanged(object sender, EventArgs e)
        {
            this.updateCashReceive(this.calculatePayment(), 0, txtTotalReceive);            
        }

        private void txtBalance_TextChanged(object sender, EventArgs e)
        {
            int val = 0;
            if (txtBalance.Text == "" || txtBalance.Text == "0")
                txtKNo.Text = val.ToString();
            else
                txtKNo.Text = (slDAL.GetMaxKNo() + 1).ToString();
        }

        private void dgvItemAdded_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int k = Convert.ToInt32(e.RowIndex) + 1;
            this.dgvItemAdded.Rows[e.RowIndex].HeaderCell.Value = k.ToString();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void calculateStonePrice(decimal val, decimal val1, TextBox txt)
        {
            decimal sum = val + val1;
            txt.Text = Math.Round((sum), 0).ToString();
        }

        private List<Stock> getTags(int id)
        {
            List<Stock> records = sDAL.GetSaleTagNoByItemId(id);
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
                    string str = dgvItemAdded.Rows[i].Cells[1].Value.ToString();
                    foreach (Stock stk in records)
                    {
                        if (str.Equals(stk.TagNo))
                            lstTag.Add(stk);
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

        private List<Stock> getTagsForOrder(int id)
        {
            List<Stock> records = sDAL.GetOrderTagNoByOrderNo(id);
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
                    string str = dgvItemAdded.Rows[i].Cells[1].Value.ToString();
                    foreach (Stock stk in records)
                    {
                        if (str.Equals(stk.TagNo))
                            lstTag.Add(stk);
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

        private void txtTotalMaking_KeyPress(object sender, KeyPressEventArgs e)
        {            
            this.txtTotalMaking.TextChanged += new EventHandler(txtTotalMaking_TextChanged);
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
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
            decimal sum = 0, k;
            int counter;
            for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
            {
                if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "" || Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) == "0")
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[4].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[4].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                    else
                    {
                        if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[6].Value) != string.Empty)
                        {
                            k = decimal.Parse(dgvStonesDetail.Rows[counter].Cells[6].Value.ToString());
                            sum += k;
                        }
                    }
                }
                else
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                    else
                    {
                        if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[6].Value) != string.Empty)
                        {
                            k = decimal.Parse(dgvStonesDetail.Rows[counter].Cells[6].Value.ToString());
                            sum += k;
                        }
                    }
                }
            }
            return sum;
        }

        public decimal upTextBox()
        {
            decimal weight = 0;

            int counter;
            for (counter = 0; counter < (dgvPreviousReciveGold.Rows.Count - 1); counter++)
            {
                if (dgvPreviousReciveGold.Rows[counter].Cells[2].Value.ToString() == string.Empty || dgvPreviousReciveGold.Rows[counter].Cells[2].Value == null)
                    weight += 0;
                else
                    weight += Convert.ToDecimal(dgvPreviousReciveGold.Rows[counter].Cells[2].Value.ToString());
            }
            return weight;
        }

        public decimal upSumGPrice()
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvPreviousReciveGold.Rows.Count - 1); counter++)
            {
                if (dgvPreviousReciveGold.Rows[counter].Cells[4].Value.ToString() == string.Empty || dgvPreviousReciveGold.Rows[counter].Cells[4].Value == null)
                    sum += 0;
                else
                    sum += Convert.ToDecimal(dgvPreviousReciveGold.Rows[counter].Cells[4].Value.ToString());
            }
            return sum;
        }

        public decimal upSum()
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvPreviousReceivedAmount.Rows.Count - 1); counter++)
            {
                if (dgvPreviousReceivedAmount.Rows[counter].Cells[2].Value.ToString() == string.Empty || dgvPreviousReceivedAmount.Rows[counter].Cells[2].Value == null)
                    sum += 0;
                else
                    sum += Convert.ToDecimal(dgvPreviousReceivedAmount.Rows[counter].Cells[2].Value.ToString());
            }
            return sum;
        }
       
        private void GoldBalance(decimal totalgold, decimal receive, TextBox txt, Label lblrecieve, Label bal)
        {
            decimal gold = 0;
            frm.RatiMashaTola(receive, lblrecieve);
            gold = Math.Round((totalgold - receive), 3);

            txt.Text = gold.ToString("N3");
            frm.RatiMashaTola(gold, bal);
        }

        private void chkPromiseDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPromiseDate.Checked == true)
                dtpPromiseDate.Enabled = true;
            if (chkPromiseDate.Checked == false)
                dtpPromiseDate.Enabled = false;
        }

        private void btnReason_Click(object sender, EventArgs e)
        {
            frmSplit frm = new frmSplit();
            frm.ShowDialog();
            desc = frm.desc;
            if (desc.Length == 0)
            {
                this.txtNetWeight.ReadOnly = true;
                this.txtQty.ReadOnly = true;
                this.txtPieces.ReadOnly = false;
            }
            else
            {
                this.txtNetWeight.ReadOnly = false;
                this.txtNetWeight.BackColor = Color.White;
                this.txtPieces.ReadOnly = false;
                this.txtQty.ReadOnly = false;
            }
        }

        private void txtNetWeight_Enter(object sender, EventArgs e)
        {
            if (this.txtNetWeight.ReadOnly == true)
                this.txtNetWeight.BackColor = Color.FromArgb(224,255,255);
            else
                this.txtNetWeight.BackColor = Color.White;
        }

        private void txtNetWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void RefreshBillTab()
        {         
            this.cbxCustomerName.SelectedIndex = -1;
            tagNo1 = "";
            this.txtAddress.Text = "";
            this.cbxContactNo.Text = "";
            this.txtCashPayment.Text = "";
            this.txtTMaking.Text = "";
            this.txtTLacker.Text = "";
            this.txtTOtherCharges.Text = "";
            this.txtTStoneCharges.Text = "";
            this.txtOTotalCash.Text = "";
            this.txtCashReceive.Text = "0";
            this.txtCheck.Text = "0";
            this.txtCreditCard.Text = "0";
            this.txtUsedGold.Text = "0";
            this.txtPureGold.Text = "0";
            this.txtTotalReceive.Text = "0";
            this.txtBalance.Text = "0";
            this.txtTPrice.Text = "0";
            this.txtTItemDisc.Text = "0";
            this.txtBillDiscount.Text = "0";
            this.txtNetBill.Text = "0";
            this.txtSaleNo.Text = (slDAL.GetMaxSaleNo() + 1).ToString();
            this.btnReturn.Text = "&Return";
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnDelete.Enabled = true;
                this.btnEdit.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            else if (str == "Limited")
            {
                this.btnDelete.Enabled = true;
            }
            else
            {
                str = ur.GetUserRightsByUser("DirectSale");
                if (str != "" && str != null)
                { }
            }
            s = new Sale();
        }

        private void dgvItemAdded_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (eFlag == true)
                {
                    strTagNo = this.dgvItemAdded.Rows[e.RowIndex].Cells[1].Value.ToString();
                    int id = Convert.ToInt32(this.dgvItemAdded.Rows[e.RowIndex].Cells[3].Value.ToString());
                    this.dgvItemAdded.Rows.RemoveAt(e.RowIndex);
                    SaleLineItem sl = new SaleLineItem();
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        if (sli.Stock.TagNo == strTagNo)
                        {
                            this.ShowAllRecordByTagNo(sli.Stock);
                            sl = sli;
                        }
                    }
                    if (sl != null)
                    {
                        s.RemoveLineItems(sl);
                    }
                }
                else
                {
                    if (txtNetWeight.Text == "")
                    {
                        strTagNo = this.dgvItemAdded.Rows[e.RowIndex].Cells[1].Value.ToString();
                        int id = Convert.ToInt32(this.dgvItemAdded.Rows[e.RowIndex].Cells[3].Value.ToString());
                        this.dgvItemAdded.Rows.RemoveAt(e.RowIndex);
                        SaleLineItem sl = new SaleLineItem();
                        foreach (SaleLineItem sli in s.SaleLineItem)
                        {
                            if (sli.Stock.StockId == id)
                            {
                                this.ShowAllRecordByStockId(sli.Stock);
                                sl = sli;
                            }
                        }
                        if (sl != null)
                        {
                            s.RemoveLineItems(sl);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                getOrderNo();
                btnBill_Click(sender, e);
            }
        }

        private void txtTotalMaking_TextChanged(object sender, EventArgs e)
        {
            //if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
            //    frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
            //else
            //    frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);
        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {
            decimal val = 0;
            val = FormControls.GetDecimalValue(this.txtTotalPrice, 0);
            this.txtNetPrice.Text = val.ToString("0");
        }

        private void txtOtherCharges_TextChanged(object sender, EventArgs e)
        {
            if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
            else
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);
        }

        private void txtNetWeight_Leave(object sender, EventArgs e)
        {
            this.txtNetWeight.ReadOnly = true;
            this.txtNetWeight.BackColor = Color.FromArgb(224,255,255);
        }

        private void txtRemaingWeight_TextChanged(object sender, EventArgs e)
        {
            decimal waste = 0;
            if (this.txtWasteInGm.Text == "")
                waste = 0;
            else
                waste = FormControls.GetDecimalValue(this.txtWasteInGm, 3);
        }

        private void txtRemaingTotalWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val1 = 0, val3 = 0, val2 = 0;
            val2 = FormControls.GetDecimalValue(this.txtStoneWeight, 3);
            this.grossweight(val1, val2, txtGrossWeight);
            val3 = FormControls.GetDecimalValue(this.txtGoldRates, 1);
            this.goldprice(val3, val1, txtGoldPrice);
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && (Convert.ToInt16(e.KeyChar) < 65 || Convert.ToInt16(e.KeyChar) > 90) && (Convert.ToInt16(e.KeyChar) < 97 || Convert.ToInt16(e.KeyChar) > 122) && Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 13)
                e.Handled = true;
            else
                e.Handled = false;

            string str = "";
            str = this.txtBarCode.Text;
            if (e.KeyChar == 13)
            {
                this.ShowAllRecordByTag(str);
                this.rbtPoundPasa_CheckedChanged(sender, e);
                this.txtBarCode.Text = "";              
            }
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void txtPieces_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtOtherCharges_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void txtGoldRates_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            bFlag = this.KeyCheck(sender, e);
            if (bFlag == true)
                e.Handled = true;
        }

        private void dgvStonesDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStonesDetail.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = stkDAL.GetAllStoneNamebyId(sty);
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "Id";
            }
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 4 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 5)
            {
                if (e.KeyChar == 46)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 3 || this.dgvStonesDetail.CurrentCell.ColumnIndex == 6)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
            }
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                EditNo sno = new EditNo();
                sno.Text = "SaleNo";
                sno.LabelText = "Enter Sale No.";
                sno.Msg = "Enter Sale No. First";
                sno.ShowDialog();
                sNo = (int)sno.EditNum;
                bool nflag = slDAL.isSaleNoExist(sNo);
                if (nflag == false)
                {
                    MessageBox.Show("Record Not Found", Messages.Header);
                    return;
                }
                this.ShowSale(sNo);
                if (check == true)
                {
                    ListOfCreditCard = payDAL.GetCCardListBySaleNo(s.SaleNo);
                    if (ListOfCreditCard == null)
                        ListOfCreditCard = new List<CreditCard>();
                    ListOfChecks = payDAL.GetChequeListBySaleNo(s.SaleNo);
                    if (ListOfChecks == null)
                        ListOfChecks = new List<Cheques>();
                    ListOfUsedGold = payDAL.GetUGoldListBySaleNo(s.SaleNo);
                    if (ListOfUsedGold == null)
                        ListOfUsedGold = new List<Gold>();
                    ListOfPureGold = payDAL.GetPGoldListBySaleNo(s.SaleNo);
                    if (ListOfPureGold == null)
                        ListOfPureGold = new List<Gold>();
                    this.tabControl1.SelectedTab = tabPage1;
                    this.btnEdit.Text = "&Update";
                    this.btnSave.Enabled = false;
                    this.btnDelete.Enabled = false;
                    this.btnDamage.Enabled = false;
                    eFlag = true;
                    return;
                }
                check = true;
            }
            if (this.btnEdit.Text == "&Update")
            {
                if (this.dgvItemAdded.Rows.Count < 1)
                {
                    MessageBox.Show("There is No any Item to Added", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                con.Open();
                trans = con.BeginTransaction();
                if (Check == 1)
                { }
                else if (Check == 0)
                {
                    try
                    {
                        ChildAccount cha = new ChildAccount();
                        ChildAccount child = new ChildAccount();
                        s.SaleNo = Convert.ToInt32(txtSaleNo.Text);
                        s.SDate = Convert.ToDateTime(dtpDate.Value);
                        if (lblOrderNo.Text != "")
                            s.OrderNo = Convert.ToInt32(lblOrderNo.Text);
                        else
                            s.OrderNo = 0;
                        s.TotalMaking = FormControls.GetDecimalValue(txtTMaking, 0);
                        s.TotalLaker = FormControls.GetDecimalValue(txtTLacker, 0);
                        s.TotalOtherCharges = FormControls.GetDecimalValue(txtTOtherCharges, 0);
                        s.StoneCharges = FormControls.GetDecimalValue(txtTStoneCharges, 0);
                        if (this.txtFixRate.Text != "" && this.txtFixRate.Text != "0")
                        {
                            s.TotalGoldPrice = 0;
                            s.TotalPrice = FormControls.GetDecimalValue(txtTPrice, 0);
                        }
                        else
                        {
                            s.TotalGoldPrice = 0;
                            s.OrderRate = FormControls.GetDecimalValue(txtOrderRate, 1);
                            s.TotalPrice = FormControls.GetDecimalValue(txtTPrice, 0);
                        }
                        s.CusAccountNo = chkOrderNo.Checked ? sale.CustName.AccountCode : cust.AccountCode;
                        s.TotalItemDiscount = FormControls.GetDecimalValue(txtTItemDisc, 0);
                        s.OrderTaker = "";
                        s.CashPayment = FormControls.GetDecimalValue(txtCashPayment, 0);
                        if (this.cbxSaleMan.SelectedIndex > -1)
                        {
                            s.SalesMan = new SaleMan();
                            s.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                        }
                        else
                        {
                            s.SalesMan = new SaleMan();
                            s.SalesMan.ID = 0;
                        }
                        if (s.OrderNo != 0)
                        {
                            s.Status = "Order Sale";
                            s.ODate = oDate;
                        }
                        else
                        {
                            s.Status = "Stock Sale";
                            s.ODate = null;
                        }
                        s.Baddats = 0;
                        s.TotalNetPrice = Convert.ToDecimal(FormControls.GetDecimalValue(txtTPrice, 0));
                        s.BillDiscout = FormControls.GetDecimalValue(txtBillDiscount, 0);
                        s.NetBill = FormControls.GetDecimalValue(txtNetBill, 0);
                        s.CashReceive = FormControls.GetDecimalValue(txtCashReceive, 0);
                        s.CreditCard = FormControls.GetDecimalValue(txtCreditCard, 0);
                        s.CheckCash = FormControls.GetDecimalValue(txtCheck, 0);
                        s.PureGoldCharges = FormControls.GetDecimalValue(txtPureGold, 0);
                        s.UsedGoldCharges = FormControls.GetDecimalValue(txtUsedGold, 0);
                        s.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceive, 0);
                        s.Balance = FormControls.GetDecimalValue(txtBalance, 0);
                        s.TotalGold = 0;
                        s.GoldReceived = 0;

                        vDAL.DeleteVoucherBySaleNo(s.SaleNo, con, trans);
                        string saletagno = "";
                        for (int i = 0; i <= this.dgvItemAdded.Rows.Count - 1; i++)
                        {
                            saletagno = saletagno + this.dgvItemAdded.Rows[i].Cells[1].Value.ToString() + " ";
                        }

                        #region Sale Voucher
                        salv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);

                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(4, "Income", "Sale", "General Account", con, trans);
                            cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                        }
                        salv.AccountCode = cha;
                        salv.Cr = s.NetBill;
                        salv.Dr = 0;
                        salv.DDate = (DateTime)s.SDate;
                        salv.SNO = s.SaleNo;
                        salv.VNO = vDAL.CreateVNO("SAV", con, trans);
                        salv.Description = "Bill Of S.No" + s.SaleNo.ToString();
                        vDAL.AddVoucher(salv, con, trans);

                        salv = new Voucher();
                        salv.SNO = s.SaleNo;
                        salv.OrderNo = 0;
                        acDAL = new AccountDAL();
                        child = new ChildAccount();
                        child = acDAL.GetChildByCode(cust.AccountCode, con, trans);
                        salv.AccountCode = child;
                        salv.VNO = vDAL.CreateVNO("SAV", con, trans);
                        salv.Dr = s.NetBill;
                        salv.Cr = 0;
                        salv.DDate = (DateTime)s.SDate;
                        salv.Description = "Bill Of S.No" + s.SaleNo.ToString();
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
                            pv.AccountCode = cha;
                            pv.Dr = Convert.ToDecimal(s.CashReceive);
                            pv.Cr = 0;
                            pv.DDate = (DateTime)s.SDate;
                            pv.OrderNo = 0;
                            pv.SNO = s.SaleNo;
                            pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                            pv.Description = "Cash Recieved From S.No" + s.SaleNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);

                            custv = new Voucher();
                            custv.AccountCode = salv.AccountCode;
                            custv.Cr = (decimal)s.CashReceive;
                            custv.Dr = 0;
                            custv.DDate = (DateTime)s.SDate;
                            custv.OrderNo = 0;
                            custv.SNO = s.SaleNo;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);

                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = s.SaleNo;
                            sp1.ONo = 0;
                            sp1.VNo = pv.VNO;
                            sp1.PMode = "Cash";
                            sp1.PTime = "Sale";
                            sp1.Receiveable = 0;
                            sp1.DRate = 0;
                            sp1.DDate = (DateTime)s.SDate;
                            sp1.BDrate = 0;
                            sp1.BankName = "";
                            sp1.Amount = (decimal)s.CashReceive;
                            sp1.Description = pv.Description;
                            sp1.DAccountCode = "";
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            sp1.CustId = Convert.ToInt32(lblCustId.Text);
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
                            pv.DDate = (DateTime)s.SDate;
                            pv.OrderNo = 0;
                            pv.SNO = s.SaleNo;
                            pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                            pv.Description = "Cash Paid From S.No." + s.SaleNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);

                            custv = new Voucher();
                            custv.AccountCode = salv.AccountCode;
                            custv.Cr = 0;
                            custv.Dr = pv.Cr;
                            custv.DDate = (DateTime)s.SDate;
                            custv.OrderNo = 0;
                            custv.SNO = s.SaleNo;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);

                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = s.SaleNo;
                            sp1.ONo = 0;
                            sp1.VNo = pv.VNO;
                            sp1.PMode = "Cash";
                            sp1.PTime = "Sale";
                            sp1.Receiveable = 0;
                            sp1.DRate = 0;
                            sp1.DDate = (DateTime)s.SDate;
                            sp1.BDrate = 0;
                            sp1.BankName = "";
                            sp1.Amount = (decimal)pv.Cr;
                            sp1.Description = pv.Description;
                            sp1.DAccountCode = "";
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            sp1.CustId = Convert.ToInt32(lblCustId.Text);
                            payDAL.AddSalePayment(sp1, con, trans);
                        }
                        #endregion

                        #region CreditCard Voucher
                        if (this.ListOfCreditCard.Count > 0)
                        {
                            if (!(this.txtCreditCard.Text == "" || this.txtCreditCard.Text == "0"))
                            {
                                foreach (CreditCard cc in this.ListOfCreditCard)
                                {
                                    pv = new Voucher();
                                    string accountCode = acDAL.GetCodeByAccountNo(cc.DepositInAccount.AccountNo);
                                    cha = acDAL.GetChildByCode(accountCode, con, trans);
                                    pv.AccountCode = cha;
                                    pv.Dr = Convert.ToDecimal(s.CreditCard);
                                    pv.Cr = 0;
                                    pv.DDate = (DateTime)s.SDate;
                                    pv.OrderNo = 0;
                                    pv.SNO = s.SaleNo;
                                    pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                                    string str = pv.VNO;
                                    pv.Description = "Cash Recieved By Credit Card From S.No" + s.SaleNo.ToString();
                                    vDAL.AddVoucher(pv, con, trans);

                                    ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                                    custv = new Voucher();
                                    custv.AccountCode = salv.AccountCode;
                                    custv.Cr = (decimal)s.CreditCard;
                                    custv.Dr = 0;
                                    custv.DDate = (DateTime)s.SDate;
                                    custv.OrderNo = 0;
                                    custv.SNO = s.SaleNo;
                                    custv.VNO = pv.VNO;
                                    custv.Description = pv.Description;
                                    vDAL.AddVoucher(custv, con, trans);

                                    pv = new Voucher();
                                    cha = acDAL.GetChildByCode(accountCode, con, trans);
                                    pv.AccountCode = cha;
                                    pv.Dr = ExtraMoney;
                                    pv.Cr = 0;
                                    pv.DDate = (DateTime)s.SDate;
                                    pv.OrderNo = 0;
                                    pv.SNO = s.SaleNo;
                                    pv.VNO = str;
                                    pv.Description = "Credit Card Extra Mony From S.No" + s.SaleNo.ToString();
                                    vDAL.AddVoucher(pv, con, trans);

                                    pv = new Voucher();
                                    cha = new ChildAccount();
                                    cha.HeadCode = 1;
                                    cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                                    if (cha == null)
                                    {
                                        cha = new ChildAccount();
                                        cha.ChildCode = acDAL.CreateAccount(1, "Income", "Credit Card Extra", "General Account", con, trans);
                                    }
                                    pv.AccountCode = cha;
                                    pv.Cr = ExtraMoney;
                                    pv.Dr = 0;
                                    pv.DDate = (DateTime)s.SDate;
                                    pv.OrderNo = 0;
                                    pv.SNO = s.SaleNo;
                                    pv.VNO = str;
                                    pv.Description = "Credit Card Extra Mony From S.No" + s.SaleNo.ToString();
                                    vDAL.AddVoucher(pv, con, trans);

                                    cc.VNO = str;
                                    cc.SNO = s.SaleNo;
                                    cc.ONO = (int)s.OrderNo;
                                    cc.AccountCode = accountCode;
                                    cc.Status = "Sale";
                                    slDAL.AddCreditCards(cc, con, trans);

                                    SalePayment sp1 = new SalePayment();
                                    sp1.SaleNo = s.SaleNo;
                                    sp1.ONo = 0;
                                    sp1.VNo = str;
                                    sp1.PMode = "Credit Card";
                                    sp1.PTime = "Sale";
                                    sp1.Receiveable = (decimal)cc.SwapAmount;
                                    sp1.DRate = cc.DeductRate;
                                    sp1.DDate = (DateTime)s.SDate;
                                    sp1.BDrate = cc.BankDeductRate;
                                    sp1.BankName = cc.BankName.BankName;
                                    sp1.Amount = (decimal)s.CreditCard;
                                    sp1.Description = "Cash Recieved By Credit Card From S.No" + s.SaleNo.ToString();
                                    sp1.DAccountCode = "";
                                    sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                                    sp1.CustId = Convert.ToInt32(lblCustId.Text);
                                    payDAL.AddSalePayment(sp1, con, trans);
                                }
                            }
                        }
                        #endregion

                        #region Cheque Voucher
                        if (this.ListOfChecks.Count > 0)
                        {
                            if (!(this.txtCheck.Text == "" || this.txtCheck.Text == "0"))
                            {
                                foreach (Cheques chq in this.ListOfChecks)
                                {
                                    pv = new Voucher();
                                    cha = acDAL.GetChildByName(chq.BankAccount, con, trans);
                                    pv.AccountCode = acDAL.GetChildByCode(chq.DepositInAccount.AccountCode.ChildCode, con, trans);
                                    pv.Dr = Convert.ToDecimal(s.CheckCash);
                                    pv.Cr = 0;
                                    pv.DDate = (DateTime)s.SDate;
                                    pv.OrderNo = 0;
                                    pv.SNO = s.SaleNo;
                                    pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                                    pv.Description = "Cheque Recieved From S.No" + s.SaleNo.ToString();
                                    vDAL.AddVoucher(pv, con, trans);

                                    custv = new Voucher();
                                    custv.AccountCode = salv.AccountCode;
                                    custv.Cr = (decimal)s.CheckCash;
                                    custv.Dr = 0;
                                    custv.DDate = (DateTime)s.SDate;
                                    custv.OrderNo = 0;
                                    custv.SNO = s.SaleNo;
                                    custv.VNO = pv.VNO;
                                    custv.Description = pv.Description;
                                    vDAL.AddVoucher(custv, con, trans);

                                    chq.SNO = s.SaleNo;
                                    chq.ONO = (int)s.OrderNo;
                                    chq.VNO = pv.VNO;
                                    chq.CustAccountCode = s.CusAccountNo;
                                    chq.Status = "ok";
                                    slDAL.AddChecques(chq, con, trans);

                                    SalePayment sp1 = new SalePayment();
                                    sp1.SaleNo = s.SaleNo;
                                    sp1.ONo = 0;
                                    sp1.VNo = pv.VNO;
                                    sp1.PMode = "Cheque";
                                    sp1.PTime = "Sale";
                                    sp1.Receiveable = 0;
                                    sp1.DRate = 0;
                                    sp1.DDate = (DateTime)s.SDate;
                                    sp1.BDrate = 0;
                                    sp1.BankName = chq.BankName.BankName;
                                    sp1.Amount = (decimal)s.CheckCash;
                                    sp1.Description = pv.Description;
                                    sp1.DAccountCode = pv.AccountCode.ChildCode;
                                    sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                                    sp1.CustId = Convert.ToInt32(lblCustId.Text);
                                    payDAL.AddSalePayment(sp1, con, trans);
                                }
                            }
                        }
                        #endregion

                        #region Used Gold Voucher
                        if (this.ListOfUsedGold.Count > 0)
                        {
                            foreach (Gold gld in this.ListOfUsedGold)
                            {
                                pv = new Voucher();
                                cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                                if (cha == null)
                                {
                                    string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                                }
                                pv.AccountCode = acDAL.GetChildByCode(cha.ChildCode, con, trans);
                                pv.Dr = Convert.ToDecimal(s.UsedGoldCharges);
                                pv.Cr = 0;
                                pv.DDate = (DateTime)s.SDate;
                                pv.OrderNo = 0;
                                pv.SNO = s.SaleNo;
                                pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                                pv.Description = "Used Gold Purchased From Sale No " + s.SaleNo.ToString();
                                vDAL.AddVoucher(pv, con, trans);

                                custv = new Voucher();
                                custv.AccountCode = salv.AccountCode;
                                custv.Cr = (decimal)s.UsedGoldCharges;
                                custv.Dr = 0;
                                custv.DDate = (DateTime)s.SDate;
                                custv.OrderNo = 0;
                                custv.SNO = s.SaleNo;
                                custv.VNO = pv.VNO;
                                custv.Description = pv.Description;
                                vDAL.AddVoucher(custv, con, trans);

                                gld.PGDate = (DateTime)s.SDate;
                                gld.SNO = s.SaleNo;
                                gld.VNO = pv.VNO;
                                gld.Description = pv.Description;
                                gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                                gld.CustId = Convert.ToInt32(lblCustId.Text);
                                gld.PTime = "Sale";
                                gld.PMode = "Rec";
                                slDAL.AddGoldDetail(gld, con, trans);
                                cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                                decimal balace = cha.Balance + gld.Weight;
                                acDAL.UpdateChildBalance(balace, cha.ChildCode, con, trans);
                            }
                        }
                        #endregion

                        #region Pure Gold Voucher
                        if (this.ListOfPureGold.Count > 0)
                        {
                            foreach (Gold gld in this.ListOfPureGold)
                            {
                                pv = new Voucher();
                                cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                                if (cha == null)
                                {
                                    string Coode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                                }
                                pv.AccountCode = acDAL.GetChildByCode(cha.ChildCode, con, trans);
                                pv.Dr = Convert.ToDecimal(s.PureGoldCharges);
                                pv.Cr = 0;
                                pv.DDate = (DateTime)s.SDate;
                                pv.OrderNo = 0;
                                pv.SNO = s.SaleNo;
                                pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                                pv.Description = "Pure Gold Purchased From S.No" + s.SaleNo.ToString();
                                vDAL.AddVoucher(pv, con, trans);

                                custv = new Voucher();
                                custv.AccountCode = salv.AccountCode;
                                custv.Cr = (decimal)s.PureGoldCharges;
                                custv.Dr = 0;
                                custv.DDate = (DateTime)s.SDate;
                                custv.OrderNo = 0;
                                custv.SNO = s.SaleNo;
                                custv.VNO = pv.VNO;
                                custv.Description = pv.Description;
                                vDAL.AddVoucher(custv, con, trans);

                                gld.PGDate = (DateTime)s.SDate;
                                gld.SNO = s.SaleNo;
                                gld.VNO = pv.VNO;
                                gld.Description = pv.Description;
                                gld.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                                gld.CustId = Convert.ToInt32(lblCustId.Text);
                                gld.PTime = "Sale";
                                gld.PMode = "Rec";
                                slDAL.AddGoldDetail(gld, con, trans);
                                cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                                decimal balace = cha.Balance + gld.Weight;
                                acDAL.UpdateChildBalance(balace, cha.ChildCode, con, trans);
                            }
                        }
                        #endregion

                        s.GoldBalance = 0;
                        if (this.txtBillBookNo.Text == "")
                            s.BillBookNo = "0";
                        else
                            s.BillBookNo = txtBillBookNo.Text;
                        s.KhataNo = FormControls.GetIntValue(txtKNo);
                        s.OthrChargesGold = 0;
                        s.GoldChargesGold = 0;
                        s.NetBillGold = 0;
                        s.TotalGold = 0;
                        s.GoldReceived = 0;
                        s.GoldBalance = 0;
                        s.BillInWord = nmb.changeNumericToWords(FormControls.GetDecimalValue(this.txtNetBill, 0));
                        cust.CashBalance = FormControls.GetDecimalValue(this.txtBalance, 0);
                        cust.GoldBalance = 0;
                        foreach (SaleLineItem sli in s.SaleLineItem)
                        {
                            stDAL.DeleteStonesByTagNo(sli.Stock.TagNo.ToString(), con, trans);
                        }
                        slDAL.UpdateSale(sNo, s, con, trans);
                        foreach (SaleLineItem sl in s.SaleLineItem)
                        {
                            lstTagNo.Remove(sl.Stock.TagNo.ToString());
                        }
                        foreach (string str in lstTagNo)
                        {
                            SqlCommand cmd = new SqlCommand("UpdateStockAgESale", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add(new SqlParameter("@TagNo", str));
                            cmd.Transaction = trans;
                            cmd.ExecuteNonQuery();
                            slDAL.AddEdit_Return((string)s.CusAccountNo, Convert.ToInt16(this.txtSaleNo.Text), str, (DateTime)s.SDate, "Edit", con, trans);
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show(ex.ToString());
                        return;
                    }
                    finally
                    {
                        if (con.State == ConnectionState.Open)
                        {
                            trans.Commit();
                            con.Close();
                            MessageBox.Show(Messages.Updated, Messages.Header);
                            this.dtpDate.Value = DateTime.Now;
                            this.dtpSaleDate.Value = DateTime.Now;
                            this.btnEdit.Text = "&Edit";
                            this.RefreshBillTab();
                            this.dgvItemAdded.Rows.Clear();
                        }
                    }
                }
            }
        }

        private void txtMakingPerGm_Leave(object sender, EventArgs e)
        {
            this.txtMakingPerGm.Text = FormControls.GetDecimalValue(this.txtMakingPerGm, 1).ToString("N3");
        }       
        #endregion

        private void ShowSale(int sNo)
        {
            s = slDAL.GetSaleBySaleNo(sNo);
            if (s == null)
            {
                MessageBox.Show("Sale No Not Exist", Messages.Header);
                check = false;
                return;
            }
            else
            {
                if (s.OrderNo != 0 && s.OrderNo != null)
                {
                    orderNo = (int)s.OrderNo;
                    lblOrderNo.Text = orderNo.ToString();
                    oDate = (DateTime)s.ODate;
                    txtReceivedAmount.Visible = true;
                    label62.Visible = true;
                    this.ShowRecByOrderNo((int)s.OrderNo);
                    this.txtGoldReceived.Text = this.upTextBox().ToString("0.000");
                    this.txtRGPrice.Text = this.upSumGPrice().ToString("0");
                    this.txtCashReceived.Text = this.upSum().ToString("0");
                    this.txtOPWeight.Text = (this.SumGoldofCash() + upTextBox()).ToString("0.000");
                    this.txtReceivedAmount.Text = (this.upSum() + this.upSumGPrice()).ToString("0");
                    this.txtFixRate.Text = Convert.ToDecimal(s.OFixRate).ToString("0.0");
                }
                else
                    orderNo = 0;
                this.txtSaleNo.Text = s.SaleNo.ToString();
                this.dtpDate.Value = Convert.ToDateTime(s.SDate);
                this.cbxSaleMan.Text = s.SalesMan.Name.ToString();
                this.txtBillBookNo.Text = s.BillBookNo.ToString();
                this.lblCustId.Text = s.CustName.ID.ToString();
                this.ShowCustomer((int)s.CustName.ID);
                this.txtTotalReceive.Text = Convert.ToDecimal(s.TReceivedAmount).ToString("0");
                this.txtNetBill.Text = Convert.ToDecimal(s.NetBill).ToString("0");
                this.txtCashPayment.Text = Convert.ToDecimal(s.CashPayment).ToString("0");
                this.txtCashReceive.Text = Convert.ToDecimal(s.CashReceive).ToString("0");
                this.txtCheck.Text = Convert.ToDecimal(s.CheckCash).ToString("0");
                this.txtCreditCard.Text = Convert.ToDecimal(s.CreditCard).ToString("0");
                this.txtUsedGold.Text = Convert.ToDecimal(s.UsedGoldCharges).ToString("0");
                this.txtPureGold.Text = Convert.ToDecimal(s.PureGoldCharges).ToString("0");
                this.txtEPureWeight.Text = Convert.ToDecimal(s.epureWeight).ToString("0.000");
                this.txtEUsedWeight.Text = Convert.ToDecimal(s.eusedWeight).ToString("0.000");
                this.txtOtherChargesGold.Text = Convert.ToDecimal(s.OtherChergesReceivedGold).ToString("0.000");
                this.txtTPrice.Text = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtNetBill, 0) + s.BillDiscout).ToString("0");
                this.txtBillDiscount.Text = Convert.ToDecimal(s.BillDiscout).ToString("0");
                string voucherNo1 = vDAL.GetVoucherGeneral("select vno from Vouchers where vno like 'SAV%' and Description like 'Converted Gold Bill Of Sale No%' and SaleNo=" + s.SaleNo);
                if (voucherNo1 != null)
                    Check = 1;
                else
                    Check = 0;             
                this.dgvItemAdded.AutoGenerateColumns = false;
                this.dgvItemAdded.Rows.Clear();
                if (s.SaleLineItem != null && s.SaleLineItem.Count > 0)
                {
                    int i = 0;
                    decimal Weight = 0, Waste = 0, TotalWeight = 0, TotalLacker = 0, StonePrice = 0, TotalGoldPrice = 0, TotalMaking = 0;
                    lstTagNo = new List<string>();
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        object[] values1 = new Object[11];
                        values1[0] = (i + 1).ToString();
                        values1[1] = sli.Stock.TagNo.ToString();
                        Item itm = cbxGroupItem.SelectedItem as Item;
                        values1[2] = sli.Stock.ItemName.ItemName;
                        values1[3] = sli.Stock.StockId.ToString();
                        values1[4] = sli.Stock.SaleWeight.ToString();
                        values1[5] = sli.Stock.WasteInGm.ToString();
                        values1[6] = sli.Stock.TotalWeight.ToString();
                        values1[7] = sli.Stock.TotalMaking.ToString();
                        values1[8] = sli.Stock.TotalLaker.ToString();
                        values1[9] = sli.Stock.StoneCharges.ToString();
                        values1[10] = sli.Stock.OrderNo.ToString();
                        lstTagNo.Add(sli.Stock.TagNo);
                        this.dgvItemAdded.Rows.Add(values1);
                        Weight += (decimal)sli.Stock.SaleWeight;
                        Waste += (decimal)sli.Stock.WasteInGm;
                        TotalWeight += Convert.ToDecimal(sli.Stock.SaleWeight + sli.Stock.WasteInGm);
                        TotalLacker += (decimal)sli.Stock.TotalLaker;
                        StonePrice += (decimal)sli.Stock.StoneCharges;
                        TotalGoldPrice += (decimal)sli.Stock.TotalPrice;
                        TotalMaking += (decimal)sli.Stock.TotalMaking;
                    }
                    lblTNetWeight.Text = Weight.ToString("0.000");
                    lblTotalWaste.Text = Waste.ToString("0.000");
                    lblTotalWeight.Text = TotalWeight.ToString("0.000");
                    lblTotalLaker.Text = TotalLacker.ToString("0");
                    lblStonePrice.Text = StonePrice.ToString("0");
                    lblTotalMaking.Text = TotalMaking.ToString("0");
                    txtTMaking.Text = TotalMaking.ToString("0");
                }
            }
        }
        private void ShowAllRecordByTagNo(Stock stc)
        {
            if (stc == null) return;
            else
            {
                stk.StockId = stc.StockId;
                stk.TagNo = stc.TagNo;

                string st = "";
                if (stc.ItemType == ItemType.Gold)
                    st = "Gold";
                if (stc.ItemType == ItemType.Diamond)
                    st = "Diamond";
                if (stc.ItemType == ItemType.Silver)
                    st = "Silver";
                if (stc.ItemType == ItemType.Pladium)
                    st = "Pladium";
                if (stc.ItemType == ItemType.Platinum)
                    st = "Platinum";
                this.cbxItemType.Text = st;
                
                cbxGroupItem.DataSource = itmDAL.GetAllItemByType(st);               
                cbxGroupItem.DisplayMember = "ItemName";
                cbxGroupItem.ValueMember = "ItemId";               

                this.cbxGroupItem.SelectedValue = stc.ItemName.ItemId;

                int k = (int)this.cbxGroupItem.SelectedValue;
                int n = (int)this.cbxGroupItem.SelectedValue;

                this.cbxTagNo.DataSource = sDAL.GetSoldTagNoByItemId(stc.ItemName.ItemId);
                this.cbxTagNo.DisplayMember = "TagNo";
                this.cbxTagNo.ValueMember = "StockId";
                for (int i = 0; i < this.cbxTagNo.Items.Count; i++)
                {
                    Stock stk1 = (Stock)this.cbxTagNo.Items[i];
                    if (stc.TagNo.Equals(stk1.TagNo))
                    {
                        this.cbxTagNo.SelectedIndex = i;
                        break;
                    }
                }
                if (stc.OrderNo > 0)
                {
                    chkOrderNo.Checked = true;
                    cbxOrderNo.Text = stc.OrderNo.ToString();
                    cbxOrderTags.Text = stc.TagNo;
                }
                else
                    chkOrderNo.Checked = false;
                if (stc.SaleQty.HasValue)
                    this.txtQty.Text = stc.SaleQty.ToString();
                else
                    this.txtQty.Text = "1";

                if (stc.Pieces.HasValue)
                    this.txtPieces.Text = stc.Pieces.ToString();
                else
                    this.txtPieces.Text = "";
                this.txtKarat.Text = stc.Karrat.ToString();

                if (stc.RatePerGm == 0)
                {
                    decimal gramrate = grDAL.GetRateByKarat(stc.Karrat, DateTime.Today);
                    this.txtGoldRates.Text = gramrate.ToString("0.0");
                }
                else
                {
                    if (Main.City == "Islamabad")
                    {
                        grse = grDAL.GetPasaRates(Convert.ToDateTime(stc.SaleDate));
                        if (stc.pFlag == true)
                        {
                            this.rbtPoundPasa.Checked = true;
                            this.txtGoldRates.Text = (grse.PoundPasa).ToString();
                        }
                        else
                        {
                            this.rbtSonaPasa.Checked = true;
                            this.txtGoldRates.Text = (grse.SonaPasa).ToString();
                        }
                    }
                    else
                    {
                        this.txtGoldRates.Text = Convert.ToDecimal(stc.RatePerGm).ToString("0.0");
                        if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
                            frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
                        else
                            frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);
                        decimal val, val1, val2, val3 = 0, val4 = 0;
                        val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
                        val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                        val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
                        val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
                        val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

                        frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
                    }
                }

                this.cbxDesignNo.DataSource = dDAL.GetAllDesign();
                this.cbxDesignNo.DisplayMember = "DesignNo";
                this.cbxDesignNo.ValueMember = "DesignId";
                for (int i = 0; i < this.cbxDesignNo.Items.Count; i++)
                {
                    Design d = (Design)this.cbxDesignNo.Items[i];
                    if (stc.DesignNo.DesignId == d.DesignId)
                        this.cbxDesignNo.SelectedIndex = i;
                }
                this.cbxWorkerName.DataSource = wDAL.GetAllWorkers();
                this.cbxWorkerName.DisplayMember = "Name";
                this.cbxWorkerName.ValueMember = "ID";
                for (int i = 0; i < this.cbxWorkerName.Items.Count; i++)
                {
                    Worker wrk = (Worker)this.cbxWorkerName.Items[i];
                    if (stc.WorkerName.ID == wrk.ID)
                        this.cbxWorkerName.SelectedIndex = i;
                }
               
                this.txtDescription.Text = stc.Description.ToString();

                if (stc.SaleWeight.HasValue)
                    this.txtNetWeight.Text = Convert.ToDecimal(stc.SaleWeight).ToString("0.000");
                else
                    this.txtNetWeight.Text = "";

                if (stc.WastePercent.HasValue)
                {
                    this.txtWasteInPercent.Text = Convert.ToDecimal(stc.WastePercent).ToString("0.0");
                    frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWasteInPercent, 1), FormControls.GetDecimalValue(this.txtNetWeight, 3), txtWasteInGm, txtTotalWeight);
                }
                else
                    this.txtWasteInPercent.Text = "";
                if (stc.OtherCharges.HasValue)
                    this.txtOtherCharges.Text = Convert.ToDecimal(stc.OtherCharges).ToString("0");
                else
                    this.txtOtherCharges.Text = "";

                if (stc.MakingPerGm.HasValue)

                    this.txtMakingPerGm.Text = Convert.ToDecimal(stc.MakingPerGm).ToString("0.0");
                else
                    this.txtMakingPerGm.Text = "";
               
                if (stc.TotalMaking.HasValue)
                    this.txtTotalMaking.Text = Convert.ToDecimal(stc.TotalMaking).ToString("0");
                else
                    this.txtTotalMaking.Text = "";
                if (stc.LakerGm.HasValue)
                    this.txtLackerPerGm.Text = Convert.ToDecimal(stc.LakerGm).ToString("0.0");
                else
                    this.txtLackerPerGm.Text = "";
                if (stc.TotalPrice.HasValue)
                    this.txtTotalPrice.Text = Convert.ToDecimal(stc.TotalPrice).ToString("0");
                else
                    this.txtTotalPrice.Text = "";

                if (stc.TotalLaker.HasValue)
                    this.txtTotalLacker.Text = Convert.ToDecimal(stc.TotalLaker).ToString("0");
                else
                    this.txtTotalLacker.Text = "";
                if (stc.ImageMemory == null)
                {
                    this.pbxPicture.Image = null;
                    this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    MemoryStream mst = new MemoryStream(stc.ImageMemory);
                    this.pbxPicture.Image = Image.FromStream(mst);
                }
               
                if (stc.StoneList == null)
                {
                    decimal val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                    decimal val2 = upDateTextBox();
                    this.txtStoneWeight.Text = val2.ToString("0.000");
                    this.grossweight(val1, val2, txtGrossWeight);
                    this.txtTpriceOfStones.Text = updateSum().ToString("0");
                    return;
                }
                else
                {
                    this.dgvStonesDetail.AutoGenerateColumns = false;
                    int count = stc.StoneList.Count;
                    this.dgvStonesDetail.Rows.Add(count);
                    for (int i = 0; i < stc.StoneList.Count; i++)
                    {
                        this.dgvStonesDetail.Rows[i].Cells[1].Value = stc.StoneList[i].StoneTypeId;
                        this.cbxStoneName.DataSource = stDAL.GetAllStoneNamebyId(Convert.ToInt32(stc.StoneList[i].StoneTypeId));
                        this.cbxStoneName.DisplayMember = "Name";
                        this.cbxStoneName.ValueMember = "Id";
                        this.dgvStonesDetail.Rows[i].Cells[2].Value = stc.StoneList[i].StoneId;

                        if (stc.StoneList[i].StoneWeight == 0)
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(stc.StoneList[i].StoneWeight), 3);

                        if (stc.StoneList[i].Qty == 0)
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(stc.StoneList[i].Qty);

                        if (stc.StoneList[i].Rate == 0)
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(stc.StoneList[i].Rate), 1);

                        if (stc.StoneList[i].Price == 0)
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                        else
                            this.dgvStonesDetail.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(stc.StoneList[i].Price), 0);
                        if (stc.StoneList[i].ColorName != null)
                        {
                            for (int j = 0; j < this.cbxColorName.Items.Count; j++)
                            {
                                StoneColor stcl = (StoneColor)this.cbxColorName.Items[j];
                                if (stc.StoneList[i].ColorName.ColorName.Equals(stcl.ColorName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stcl.ColorId);
                            }
                        }
                        if (stc.StoneList[i].CutName != null)
                       
                        {
                            for (int j = 0; j < this.cbxCutName.Items.Count; j++)
                            {
                                StoneCut stcl = (StoneCut)this.cbxCutName.Items[j];
                                if (stc.StoneList[i].CutName.CutName.Equals(stcl.CutName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stcl.CutId);
                            }
                        }

                        if (stc.StoneList[i].ClearityName != null)
                        {
                            for (int j = 0; j < this.cbxClearity.Items.Count; j++)
                            {
                                StoneClearity stcl = (StoneClearity)this.cbxClearity.Items[j];
                                if (stc.StoneList[i].ClearityName.ClearityName.Equals(stcl.ClearityName.ToString()))
                                    this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stcl.ClearityId);
                            }
                        }
                    }
                }
                decimal val33 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                decimal val44 = upDateTextBox();
                this.txtStoneWeight.Text = val44.ToString("0.000");
                this.grossweight(val33, val44, txtGrossWeight);
                this.txtTpriceOfStones.Text = updateSum().ToString("0");            
            }
        }

        private void cbxOrderNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
            if (this.cbxOrderNo.SelectedValue == null) return;
            else
            {
                int k = (int)this.cbxOrderNo.SelectedValue;
                this.cbxOrderTags.DataSource = slDAL.GetAllTags("select TagNo from Stock Where OrderNo=" + k + " and Status='Available'");
                this.cbxOrderTags.DisplayMember = "TagNo";
                this.cbxOrderTags.SelectedIndex = -1;
                txtReceivedAmount.Visible = true;
                label62.Visible = true;
                this.ShowSearchRecByOrderNo(k);
                this.txtGoldReceived.Text = this.upTextBox().ToString("0.000");
                this.txtRGPrice.Text = this.upSumGPrice().ToString("0");
                this.txtCashReceived.Text = this.upSum().ToString("0");
                this.txtOPWeight.Text = (this.SumGoldofCash() + upTextBox()).ToString("0.000");
                this.txtReceivedAmount.Text = (this.upSum() + this.upSumGPrice()).ToString("0");
            }
        }

        private void cbxOrderNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOrderNo.SelectedIndexChanged += new EventHandler(cbxOrderNo_SelectedIndexChanged);
        }

        private void cbxOrderTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.RefreshRecord();
            this.ShowAllRecordByTag(this.cbxOrderTags.Text);
        }

        private void cbxOrderTags_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxOrderTags.SelectedIndexChanged += new EventHandler(cbxOrderTags_SelectedIndexChanged);
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (this.txtQty.Text == "")
                this.txtQty.Text = "1";
        }

        private void chkOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkOrderNo.Checked == true)
            {
                this.cbxOrderNo.Enabled = true;
                FormControls.FillCombobox(cbxOrderNo, oDAL.GetAllOrderNo("select Distinct[OrderNo] from Stock where ItemFor='Order' and Status='Available' order by OrderNo"), "OrderNo", "OrderNo");
                this.cbxOrderTags.Enabled = true;
                this.cbxTagNo.Enabled = false;

            }
            else
            {
                this.cbxOrderNo.SelectedIndexChanged -= new EventHandler(cbxOrderNo_SelectedIndexChanged);
                this.cbxOrderNo.Enabled = false;
                this.cbxOrderTags.Enabled = false;
                this.cbxOrderNo.SelectedIndex = -1;
                this.cbxTagNo.Enabled = true;
            }
        }

        private void ShowSearchRecByOrderNo(int orderNo)
        {
            sale = new Sale();
            sale = payDAL.GetRecordByOrderNo(orderNo);
            salePay = payDAL.GetSalePaymentByOrderNo(orderNo);
            goldDet = payDAL.GetGoldByOrderNo(orderNo);
            if (sale == null)
                return;
            else
            {
                if (sale.BillBookNo == null)
                    this.txtBillBookNo.Text = "";
                else
                    this.txtBillBookNo.Text = sale.BillBookNo.ToString();
                this.txtTotalPrice.Text = Convert.ToDecimal(sale.TotalPrice).ToString("0");
                this.cbxCustomerName.SelectedIndexChanged += new EventHandler(cbxCustomerName_SelectedIndexChanged);
                this.cbxCustomerName.SelectedValue = sale.CustName.ID;
                //this.txtBillDiscount.Text = sale.BillDiscout.ToString("0");
                this.lblCustId.Text = sale.CustName.ID.ToString();
                dtpOrderDate.Value = Convert.ToDateTime(sale.ODate);
                this.txtFixRate.Text = Convert.ToDecimal(sale.OFixRate).ToString("0.0");
                cust = new Customer();
                cust.CashBalance = sale.CustBalance;
                if (salePay == null)
                {
                    if (goldDet == null)
                        return;
                    else
                    {
                        this.dgvPreviousReciveGold.AutoGenerateColumns = false;
                        this.dgvPreviousReciveGold.Rows.Clear();
                        int count = goldDet.Count;
                        this.dgvPreviousReciveGold.Rows.Add(count);
                        for (int i = 0; i < count; i++)
                        {
                            this.dgvPreviousReciveGold.Rows[i].Cells[0].Value = goldDet[i].VNO.ToString();
                            this.dgvPreviousReciveGold.Rows[i].Cells[1].Value = goldDet[i].PGDate.ToString("dd-MMM-yy");
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Convert.ToDecimal(goldDet[i].PWeight).ToString("0.000");
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Convert.ToDecimal(goldDet[i].Rate).ToString("0.0");
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Convert.ToDecimal(goldDet[i].Amount).ToString("0");
                        }
                    }
                }
                else
                {
                    this.dgvPreviousReceivedAmount.AutoGenerateColumns = false;
                    this.dgvPreviousReceivedAmount.Rows.Clear();
                    int count = salePay.Count;
                    this.dgvPreviousReceivedAmount.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[0].Value = salePay[i].VNo.ToString();
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[1].Value = salePay[i].DDate.ToString("dd-MMM-yy");
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[2].Value = Convert.ToDecimal(salePay[i].Amount).ToString("0");
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[3].Value = salePay[i].PMode.ToString();
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[4].Value = salePay[i].PTime.ToString();
                    }
                    if (goldDet == null)
                        return;
                    else
                    {
                        this.dgvPreviousReciveGold.AutoGenerateColumns = false;
                        this.dgvPreviousReciveGold.Rows.Clear();
                        int count1 = goldDet.Count;
                        this.dgvPreviousReciveGold.Rows.Add(count1);
                        for (int i = 0; i < count1; i++)
                        {
                            this.dgvPreviousReciveGold.Rows[i].Cells[0].Value = goldDet[i].VNO.ToString();
                            this.dgvPreviousReciveGold.Rows[i].Cells[1].Value = goldDet[i].PGDate.ToString("dd-MMM-yy");
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Convert.ToDecimal(goldDet[i].PWeight).ToString("0.000");
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Convert.ToDecimal(goldDet[i].Rate).ToString("0.0");
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Convert.ToDecimal(goldDet[i].Amount).ToString("0");
                        }
                    }
                }             
            }
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
        }

        private void ShowRecByOrderNo(int orderNo)
        {
            sale = new Sale();
            sale = payDAL.GetRecordByOrderNo(orderNo);
            salePay = payDAL.GetSalePaymentByOrderNo(orderNo);
            goldDet = payDAL.GetGoldByOrderNo(orderNo);
            if (sale == null)
                return;
            else
            {                
                if (salePay == null)
                {
                    if (goldDet == null)
                        return;
                    else
                    {
                        this.dgvPreviousReciveGold.AutoGenerateColumns = false;
                        this.dgvPreviousReciveGold.Rows.Clear();
                        int count = goldDet.Count;
                        this.dgvPreviousReciveGold.Rows.Add(count);
                        for (int i = 0; i < count; i++)
                        {
                            this.dgvPreviousReciveGold.Rows[i].Cells[0].Value = goldDet[i].VNO.ToString();

                            this.dgvPreviousReciveGold.Rows[i].Cells[1].Value = goldDet[i].PGDate.ToString("dd-MMM-yy");
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Convert.ToDecimal(goldDet[i].PWeight).ToString("0.000");
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Convert.ToDecimal(goldDet[i].Rate).ToString("0.0");
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Convert.ToDecimal(goldDet[i].Amount).ToString("0");
                        }
                    }
                }
                else
                {
                    this.dgvPreviousReceivedAmount.AutoGenerateColumns = false;
                    this.dgvPreviousReceivedAmount.Rows.Clear();
                    int count = salePay.Count;
                    this.dgvPreviousReceivedAmount.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[0].Value = salePay[i].VNo.ToString();
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[1].Value = salePay[i].DDate.ToString("dd-MMM-yy");
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[2].Value = Convert.ToDecimal(salePay[i].Amount).ToString("0");
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[3].Value = salePay[i].PMode.ToString();
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[4].Value = salePay[i].PTime.ToString();
                    }
                    if (goldDet == null)
                        return;
                    else
                    {
                        this.dgvPreviousReciveGold.AutoGenerateColumns = false;
                        this.dgvPreviousReciveGold.Rows.Clear();
                        int count1 = goldDet.Count;
                        this.dgvPreviousReciveGold.Rows.Add(count1);
                        for (int i = 0; i < count1; i++)
                        {
                            this.dgvPreviousReciveGold.Rows[i].Cells[0].Value = goldDet[i].VNO.ToString();
                            this.dgvPreviousReciveGold.Rows[i].Cells[1].Value = goldDet[i].PGDate.ToString("dd-MMM-yy");
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Convert.ToDecimal(goldDet[i].PWeight).ToString("0.000");
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Convert.ToDecimal(goldDet[i].Rate).ToString("0.0");
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Convert.ToDecimal(goldDet[i].Amount).ToString("0");
                        }
                    }
                }
            }
        }

        private decimal SumGoldofCash()
        {
            decimal sum = 0;
            if (salePay != null)
            {
                foreach (SalePayment sp in salePay)
                {
                    sum += sp.GoldOfCash;
                }
            }
            return sum;
        }

        private void btnNewCustomer_Click(object sender, EventArgs e)
        {
            adcust = new ManageCustomer();
            adcust.Show();
        }

        private void btnDamage_Click(object sender, EventArgs e)
        {
            slDAL.DamageStock(s);
            MessageBox.Show("Damage saved successfully", Messages.Header);
            this.RefreshBillTab();
            this.dgvItemAdded.Rows.Clear();
            this.dgvPreviousReceivedAmount.Rows.Clear();
            this.dgvPreviousReciveGold.Rows.Clear();
            this.txtTROrderAmount.Text = "";
            this.txtCashReceived.Text = "";
            this.txtRGPrice.Text = "";
        }

        private void rbtPoundPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPoundPasa.Checked)
            {
                this.txtGoldRates.Text = grs.PoundPasa.ToString("0");
                this.txtGoldPrice.Text = (FormControls.GetDecimalValue(this.txtTotalWeight, 3) * (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm)).ToString("0");
            }
        }

        private void rbtSonaPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSonaPasa.Checked)
            {
                this.txtGoldRates.Text = grs.SonaPasa.ToString("0");
                this.txtGoldPrice.Text = (FormControls.GetDecimalValue(this.txtTotalWeight, 3) * (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm)).ToString("0");
            }
        }

        private void dgvStonesDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
            {
                e.Cancel = true;
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
        }

        private void dtpSaleDate_ValueChanged(object sender, EventArgs e)
        {
            this.dtpDate.Value = this.dtpSaleDate.Value;
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
                snoo = sno.EditNum;
                sale = slDAL.GetSaleBySaleNo(sno.EditNum);
                if (sale == null)
                {
                    MessageBox.Show("Sale Not Found", Messages.Header);
                    return;
                }
                if (MessageBox.Show("Are U Sure to Complete Return Sale of Sale No. " + sno.EditNum, Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string query = "Update Sale set Status ='Sale Return' Where SaleNo = " + sno.EditNum;
                    strl.Add(query);
                    CAccCode = slDAL.GetCustAccBySNO(sno.EditNum);
                    SDate = slDAL.GetSDateBySNO(sno.EditNum);
                    query = "Delete from Sale_Payment Where SNo = " + sno.EditNum;
                    strl.Add(query);
                    query = "Delete from GoldDetail Where SNo = " + sno.EditNum;
                    strl.Add(query);
                    this.btnReturn.Text = "Confirmed";
                    this.btnReturn.TextAlign = ContentAlignment.MiddleCenter;
                    this.btnDelete.Enabled = false;
                    this.btnEdit.Enabled = false;
                    this.btnDelete.Enabled = false;
                }
            }
            else if (this.btnReturn.Text == "Confirmed")
            {
                try
                {
                    string str = "select TagNo from Stock where SaleNo =" + snoo;
                    List<string> strTagNo = slDAL.GetAllTags(str);
                    foreach (string stTagNo in strTagNo)
                    {
                        SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
                        SqlCommand cmd = new SqlCommand("UpdateStockAgESale", con1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@TagNo", stTagNo));
                        con1.Open();
                        slDAL.insertReturnStock(stTagNo, con1);
                        slDAL.AddEdit_Return(CAccCode, snoo, stTagNo, SDate, "Sale Return", con1, trans);
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

                    #region Return Voucher
                    #region Sale Voucher
                    //voucher entry in sale accoutn;
                    con.Open();
                    trans = con.BeginTransaction();
                    salv = new Voucher();
                    ChildAccount cha = new ChildAccount();
                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);

                    if (cha == null)
                    {
                        string Coode = acDAL.CreateAccount(4, "Income", "Sale", "General Account", con, trans);
                        cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    }

                    cha = acDAL.GetAccount(4, "Income", "Sale", con, trans);
                    salv.AccountCode = cha;
                    salv.Dr = sale.NetBill;
                    salv.Cr = 0;
                    salv.DDate = (DateTime)sale.SDate;
                    salv.SNO = sale.SaleNo;
                    salv.OrderNo = sale.OrderNo;
                    salv.VNO = sale.SVNO;
                    salv.Description = "Bill Of S.Return No." + sale.SaleNo.ToString();
                    vDAL.AddVoucher(salv, con, trans);
                    //voucher entry in customer account;
                    salv = new Voucher();
                    salv.SNO = sale.SaleNo;
                    salv.OrderNo = sale.OrderNo;
                    acDAL = new AccountDAL();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                    salv.AccountCode = child;
                    salv.VNO = sale.SVNO;
                    salv.Cr = sale.NetBill;
                    salv.Dr = 0;
                    salv.DDate = (DateTime)sale.SDate;
                    salv.Description = "Bill Of S.Return No." + sale.SaleNo.ToString();
                    vDAL.AddVoucher(salv, con, trans);
                    #endregion
                    #region Cash voucher
                    //if (!(this.txtCashReceive.Text == "" || this.txtCashReceive.Text == "0"))
                    {
                        //cash in hand entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);

                        pv.AccountCode = cha;
                        pv.Cr = Convert.ToDecimal(sale.TReceivedAmount);
                        pv.Dr = 0;
                        pv.DDate = (DateTime)sale.SDate;
                        pv.OrderNo = sale.OrderNo;
                        pv.SNO = sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        pv.Description = "Cash Return for S.No." + sale.SaleNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        //custv.AccountCode.ChildCode =(string) sale.CusAccountNo;
                        custv.Dr = (decimal)sale.TReceivedAmount;
                        custv.Cr = 0;
                        custv.DDate = (DateTime)sale.SDate;
                        custv.OrderNo = sale.OrderNo;
                        custv.SNO = sale.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);                        
                    }
                    #endregion
                    #endregion
                    trans.Commit();
                    con.Close();

                    MessageBox.Show("Sale No " + snoo + " is Completely Returnd", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.RefreshBillTab();
                    this.RefreshRecord();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }          
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ManageSale frm = new ManageSale();
            frm.ShowDialog();
        }

        private void txtFixRate_TextChanged(object sender, EventArgs e)
        {
            if (this.txtFixRate.Text != "" && this.txtFixRate.Text != "0" && chkOrderNo.Checked == true)
                this.txtGoldRates.ReadOnly = true;
            else
                this.txtGoldRates.ReadOnly = false;
        }

        public void ExitForm()
        {
            this.Close();
        }
      
        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void txtCashReceive_KeyUp(object sender, KeyEventArgs e)
        {
            this.CalculateBill(FormControls.GetDecimalValue(this.txtTPrice, 0), FormControls.GetDecimalValue(this.txtBillDiscount, 0) + FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + FormControls.GetDecimalValue(this.txtCashReceive, 0) + FormControls.GetDecimalValue(this.txtUsedGold, 0) + FormControls.GetDecimalValue(this.txtPureGold, 0) + FormControls.GetDecimalValue(this.txtCheck, 0) + FormControls.GetDecimalValue(this.txtCreditCard, 0), 0, txtBalance);
            this.txtTotalReceive.Text = (FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + FormControls.GetDecimalValue(this.txtCashReceive, 0) + FormControls.GetDecimalValue(this.txtUsedGold, 0) + FormControls.GetDecimalValue(this.txtPureGold, 0) + FormControls.GetDecimalValue(this.txtCheck, 0) + FormControls.GetDecimalValue(this.txtCreditCard, 0)).ToString("0");
        }

        private void txtNetWeight_KeyUp(object sender, KeyEventArgs e)
        {
            frm.WasteInGm(FormControls.GetDecimalValue(this.txtWasteInGm, 3), FormControls.GetDecimalValue(this.txtNetWeight, 3), txtTotalWeight);
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

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            ManageCustomer frm = new ManageCustomer();
            FormControls.FadeOut(this);
            frm.ShowDialog();
            FormControls.FadeIn(this);
            this.ShowMaxCustomer();
        }

        private void txtWasteInPercent_KeyUp(object sender, KeyEventArgs e)
        {
            frm.GramsOfPercentStock(FormControls.GetDecimalValue(this.txtWasteInPercent, 1), FormControls.GetDecimalValue(this.txtNetWeight, 3), txtWasteInGm, txtTotalWeight);
        }

        private void txtWasteInGm_KeyUp(object sender, KeyEventArgs e)
        {
            frm.WasteInPercent(FormControls.GetDecimalValue(this.txtWasteInGm, 3), FormControls.GetDecimalValue(this.txtNetWeight, 3), txtWasteInPercent, txtTotalWeight);
        }

        private void txtMakingPerGm_KeyUp(object sender, KeyEventArgs e)
        {
            frm.MakingPerGram1(FormControls.GetDecimalValue(this.txtMakingPerGm, 1), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtTotalMaking);
            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtTotalMaking_KeyUp(object sender, KeyEventArgs e)
        {
            frm.TotalMakingForSale(FormControls.GetDecimalValue(this.txtTotalMaking, 0), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtMakingPerGm);

            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtLackerPerGm_KeyUp(object sender, KeyEventArgs e)
        {
            frm.TotalLacker(FormControls.GetDecimalValue(this.txtLackerPerGm, 1), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtTotalLacker);
            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtOtherCharges_KeyUp(object sender, KeyEventArgs e)
        {
            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtTotalLacker_KeyUp(object sender, KeyEventArgs e)
        {
            frm.LackerPerGm(FormControls.GetDecimalValue(this.txtTotalLacker, 0), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtLackerPerGm);

            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtGoldRates_KeyUp(object sender, KeyEventArgs e)
        {
            if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
            else
                frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 3), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);

            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void cbxCustomerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void txtBillDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            this.CalculateBill(FormControls.GetDecimalValue(this.txtTPrice, 0), FormControls.GetDecimalValue(this.txtBillDiscount, 0) + FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + FormControls.GetDecimalValue(this.txtCashReceive, 0) + FormControls.GetDecimalValue(this.txtUsedGold, 0) + FormControls.GetDecimalValue(this.txtPureGold, 0) + FormControls.GetDecimalValue(this.txtCheck, 0) + FormControls.GetDecimalValue(this.txtCreditCard, 0), 0, txtBalance);
            this.txtTotalReceive.Text = (FormControls.GetDecimalValue(this.txtReceivedAmount, 0) + FormControls.GetDecimalValue(this.txtCashReceive, 0) + FormControls.GetDecimalValue(this.txtUsedGold, 0) + FormControls.GetDecimalValue(this.txtPureGold, 0) + FormControls.GetDecimalValue(this.txtCheck, 0) + FormControls.GetDecimalValue(this.txtCreditCard, 0)).ToString("0");            
        }

        private void txtCashPayment_KeyUp(object sender, KeyEventArgs e)
        {
            this.txtBalance.Text = (((this.txtTPrice.Text == "" ? 0 : Convert.ToDecimal(this.txtTPrice.Text)) + (this.txtCashPayment.Text == "" ? 0 : Convert.ToDecimal(this.txtCashPayment.Text))) + -((this.txtBillDiscount.Text == "" ? 0 : Convert.ToDecimal(this.txtBillDiscount.Text)) + (this.txtTotalReceive.Text == "" ? 0 : Convert.ToDecimal(this.txtTotalReceive.Text)))).ToString("0");
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            SaleReports frm = new SaleReports();
            frm.ShowDialog();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMakingPerGm_TextChanged(object sender, EventArgs e)
        {
            //if (GoldRatetype == "SonaPasa" || GramTolaRate == "Tola")
            //    frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 0), (FormControls.GetDecimalValue(this.txtGoldRates, 1) / Formulas.WeightInGm), txtGoldPrice);
            //else
            //    frm.GoldPrice(FormControls.GetDecimalValue(this.txtTotalWeight, 0), FormControls.GetDecimalValue(this.txtGoldRates, 1), txtGoldPrice);
        }

        private void txtBillDiscount_TextChanged(object sender, EventArgs e)
        {
            this.txtNetBill.Text = ((this.txtTPrice.Text == "" ? 0 : Convert.ToDecimal(this.txtTPrice.Text)) - (this.txtBillDiscount.Text == "" ? 0 : Convert.ToDecimal(this.txtBillDiscount.Text))).ToString("0");
        }

        public decimal upDateTextBoxNetWeight()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[4].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[4].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[4].Value.ToString());
            }
            return weight;
        }

        public decimal upDateTextBoxWaste()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[5].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[5].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[5].Value.ToString());
            }
            return weight;
        }

        public decimal upDateTextBoxNetTotalWeight()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[6].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[6].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[6].Value.ToString());
            }
            return weight;
        }

        public decimal upDateTextBoxTotalMaking()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[7].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[7].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[7].Value.ToString());
            }
            return weight;
        }

        public decimal upDateTextBoxTotalLaker()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[8].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[8].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[8].Value.ToString());
            }
            return weight;
        }

        public decimal upDateTextBoxStonePrice()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvItemAdded.Rows.Count); counter++)
            {
                if (Convert.ToString(dgvItemAdded.Rows[counter].Cells[9].Value) == string.Empty || dgvItemAdded.Rows[counter].Cells[9].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvItemAdded.Rows[counter].Cells[9].Value.ToString());
            }
            return weight;
        }

        private void chkPromiseDate_CheckedChanged_1(object sender, EventArgs e)
        {
            chkPromiseDate_CheckedChanged(sender, e);
        }

        private void txtReceivedAmount_TextChanged(object sender, EventArgs e)
        {
            this.txtTotalReceive.Text = this.txtReceivedAmount.Text;
        }

        private void txtWasteInPercent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWasteInGm.Select();
            }
        }

        private void txtTotalWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtMakingPerGm.Select();
            }
        }

        private void txtWasteInGm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTotalWeight.Select();
            }
        }

        private void txtMakingPerGm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTotalMaking.Select();
            }
        }

        private void txtTotalMaking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtOtherCharges.Select();
            }
        }

        private void txtOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtLackerPerGm.Select();
            }
        }

        private void txtLackerPerGm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTotalLacker.Select();
            }
        }

        private void txtTotalLacker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtGoldRates.Select();
            }
        }

        private void txtGrossWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtGoldRates.Select();
            }
        }

        private void cbxItemType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxGroupItem.Select();
            }
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
                    this.cbxTagNo.DisplayMember = "TagNo";
                    this.cbxTagNo.ValueMember = "StockId";
                    this.cbxTagNo.DataSource = getTags(k);
                    this.cbxTagNo.SelectedIndex = -1;
                }
                this.txtBarCode.Select();
            }
        }

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxTagNo.Select();
            }
        }

        private void cbxTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cbxTagNo.SelectedValue == null || this.cbxTagNo.SelectedIndex == -1)
                    return;
                else
                {
                    Stock s = (Stock)this.cbxTagNo.SelectedItem;
                    if (s.StockId == null)
                        return;
                    else
                    {
                        this.dgvStonesDetail.Rows.Clear();
                        this.lblHidden.Text = s.StockId.ToString();
                        this.ShowAllRecord(s.StockId);
                        this.rbtPoundPasa_CheckedChanged(sender, e);
                    }
                }
                this.txtSize.Select();
            }
        }

        private void txtSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPieces.Select();
            }
        }

        private void txtPieces_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtKarat.Select();
            }
        }

        private void txtKarat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtQty.Select();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxDesignNo.Select();
            }
        }

        private void cbxDesignNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxWorkerName.Select();
            }
        }

        private void cbxWorkerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDescription.Select();
            }
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNetWeight.Select();
            }
        }

        private void txtNetWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWasteInPercent.Select();
            }
        }

        private void txtGoldRates_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtGoldPrice.Select();
            }
        }

        private void txtFixRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtGoldPrice.Select();
            }
        }

        private void txtGoldPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNetPrice.Select();
            }
        }

        private void txtNetPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTotalPrice.Select();
            }
        }

        private void txtTotalPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnAddItem.Select();
            }
        }

        private void cbxContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtAddress.Select();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTMaking.Select();
            }
        }

        private void txtTMaking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTLacker.Select();
            }
        }

        private void txtTLacker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTOtherCharges.Select();
            }
        }

        private void txtTOtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTStoneCharges.Select();
            }
        }

        private void txtTStoneCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTPrice.Select();
            }
        }

        private void txtTPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtBillDiscount.Select();
            }
        }

        private void txtBillDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNetBill.Select();
            }
        }

        private void txtNetBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtpDate.Select();
            }
        }

        private void txtTItemDisc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTPrice.Select();
            }
        }

        private void txtTNetPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.dtpDate.Select();
            }
        }

        private void txtCashReceive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCreditCard.Select();
            }
        }

        private void txtCreditCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCheck.Select();
            }
        }

        private void txtCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtUsedGold.Select();
            }
        }

        private void txtUsedGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPureGold.Select();
            }
        }

        private void txtPureGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtTotalReceive.Select();
            }
        }

        private void btnAddItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnBill.Focus();
            }
        }

        private void txtTotalReceive_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void txtBillBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxSaleMan.Select();
            }
        }

        private void cbxSaleMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbxCustomerName.Select();
            }
        }

        private void dtpDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtCashReceive.Select();
            }
        }

        private void toolStripbtnReturn_Click(object sender, EventArgs e)
        {
            btnReturn_Click(sender, e);
        }

        private void toolStripbtnDamage_Click(object sender, EventArgs e)
        {
            btnDamage_Click(sender, e);
        }

        private void btnAddCustomer_Click_1(object sender, EventArgs e)
        {
            ManageCustomer cust = new ManageCustomer();
            FormControls.FadeOut(this);
            cust.ShowDialog();
            FormControls.FadeIn(this);
            this.ShowMaxCustomer();
        }

        private void dgvStonesDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            decimal val1 = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            decimal val2 = upDateTextBox();
            this.txtStoneWeight.Text = val2.ToString("0.000");
            this.grossweight(val1, val2, txtGrossWeight);
            this.txtTpriceOfStones.Text = updateSum().ToString("0");
        }

        private void txtNetPrice_TextChanged(object sender, EventArgs e)
        {
            this.txtTotalPrice.Text = this.txtNetPrice.Text;
        }

        private void txtTStoneCharges_TextChanged(object sender, EventArgs e)
        {
            //decimal val, val1, val2, val3 = 0, val4 = 0;
            //val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            //val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            //val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            //val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            //val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            //frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void txtTpriceOfStones_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1, val2, val3 = 0, val4 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtTpriceOfStones, 0);
            val4 = FormControls.GetDecimalValue(this.txtOtherCharges, 0);

            frm.TotalPrice(val, (val1 + val4), (val2 + val3), txtNetPrice);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void txtKarat_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
