using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using BusinesEntities;
using System.Drawing.Drawing2D;
using DAL;

namespace jewl
{
    public partial class ManageOrder : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        MyColors mycolor = new MyColors();
        SqlTransaction trans;
        private Voucher pv;
        private Voucher custv;
        AccountDAL acDAL = new AccountDAL();
        ChildAccount ca = new ChildAccount();
        private AccountDAL adal = new AccountDAL();
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
        GoldRates grs = new GoldRates();
        StonesDAL sDAL = new StonesDAL();
        BankDAL bDAL = new BankDAL();
        Stock stk = new Stock();
        //Sale ordEst = new Sale();
        StockDAL stkDAL = new StockDAL();
        CustomerDAL custDAL = new CustomerDAL();
        ManageCustomer adcust = new ManageCustomer();
        OrderEstimat ordEst = new OrderEstimat();
        SaleManDAL slmDAL = new SaleManDAL();
        OrderLineItem oli;//=new OrderLineItem();
        JewelPictures jp = new JewelPictures();
        PictureDAL picDAL = new PictureDAL();
        MemoryStream mst;
        List<Customer> custs;
        List<string> lstOItemId = new List<string>();
        List<string> lstOItemId1 = new List<string>();
        PaymentOption frmpayment = null;
        public List<Gold> ListOfPureGold = new List<Gold>();
        public List<Gold> ListOfUsedGold = new List<Gold>();
        public List<Cheques> ListOfCheques = new List<Cheques>();
        public List<CreditCard> ListOfCreditCard = new List<CreditCard>();
        Customer cust;
        public string GramTolaRate = "";
        public string GoldRatetype = "";
        bool eFlag = false;
        private decimal ExtraMoney = 0;
        decimal u = 0;
        string strg = "";
        decimal s;
        int l = 1, ordId = 0;
        int z;
        int SpONo = 0;
        int oNo = 0;
        public decimal sum = 0;

        public ManageOrder()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.chkPureGold.BackColor = Color.FromArgb(204, 247, 251);
            this.chkUseGold.BackColor = Color.FromArgb(204, 247, 251);
            this.chkCreditCard.BackColor = Color.FromArgb(204, 247, 251);
            this.chkCheque.BackColor = Color.FromArgb(204, 247, 251);
            this.chkCash.BackColor = Color.FromArgb(204, 247, 251);
            this.WindowState = FormWindowState.Maximized;
            this.tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
        }

        private void OrderEstimate_Load(object sender, EventArgs e)
        {
            tabControl1.ItemSize = new Size(tabControl1.Width / tabControl1.TabCount, 0);
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);
            this.cbxBank.SelectedIndexChanged -= new EventHandler(cbxBank_SelectedIndexChanged);
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);

            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
            FormControls.FillCombobox(cbxSaleMan, slmDAL.GetAllSaleMen(), "Name", "ID");
            FormControls.FillCombobox(cbxDepositeAccount, bDAL.GetAllBankAccount(), "AccountNo", "AccountNo");
            FormControls.FillCombobox(cbxBank, bDAL.GetAllBanks(), "BankName", "Id");
            FormControls.FillCombobox(cbxCustomerName, custDAL.GetAllCustomer(), "Name", "ID");
            FormControls.FillCombobox(cbxContactNo, custDAL.GetAllCustomer(), "Mobile", "ID");

            this.ShowDataGrid();

            this.cbxTagNo.Enabled = false;
            this.cbxItemType.SelectedIndex = 0;
            this.txtOrderNo.Text = (oDAL.GetMaxOrderNo() + 1).ToString();
            this.dtpOrderDate.Enabled = true;

            if (Main.City == "Islamabad")
            {
                this.cbxKarrat.SelectedIndexChanged -= new EventHandler(cbxKarrat_SelectedIndexChanged);
                this.pnlPasaRate.Visible = true;
                grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpOrderDate.Value));
                this.rbtPoundPasa.Checked = true;
                this.txtGoldRate.Text = grs.PoundPasa.ToString("0");
            }
            else
            {
                grs.PoundPasa = 0;
                grs.SonaPasa = 0;
                this.cbxKarrat.SelectedIndexChanged += new EventHandler(cbxKarrat_SelectedIndexChanged);
                this.cbxKarrat.SelectedIndex = 3;
                string r = (string)this.cbxKarrat.SelectedItem;
                s = grDAL.GetRateByKarat(r, Convert.ToDateTime(dtpOrderDate.Value));
                this.txtGoldRate.Text = s.ToString("0");
                this.txtPGRate.Text = grDAL.GetRateByKarat("24", Convert.ToDateTime(dtpOrderDate.Value)).ToString("0");
            }
            this.cbxCustomerName.Select();

            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnRemove.Enabled = false;
                this.btnOrderCancel.Enabled = false;
                this.btnReports.Enabled = false;
            }
            GramTolaRate = saleDAL.GetStartupGramTolaRate();
            GoldRatetype = saleDAL.GetStartupGoldRateType();
            if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
            {
                decimal gramrate = grDAL.GetRateByKaratTola("24", DateTime.Today);
                this.txtGoldRate.Text = gramrate.ToString("0");
                label3.Text = gramrate.ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                decimal gramrate = grDAL.GetRateByKarat("24", DateTime.Today);
                this.txtGoldRate.Text = gramrate.ToString("0");
                label3.Text = gramrate.ToString("0");
            }
            if (Main.City == "Islamabad")
            {
                this.pnlPasaRate.Visible = true;
                grs = grDAL.GetPasaRates(DateTime.Today);
                this.txtGoldRate.Text = grs.PoundPasa.ToString("0");
                label3.Text = grs.PoundPasa.ToString("0");
            }
        }

        #region keyPress
        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtWasteIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtWasteage_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtLackerGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTotalLacker_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtMakingPerGm_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtTotalMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }
        #endregion

        #region keyChange
        private void txtGoldRate_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1;
            val = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            val1 = FormControls.GetDecimalValue(this.txtGoldRate, 3);
            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
            {
                this.txtGoldPrice.Text = (val * (val1 / Formulas.WeightInGm)).ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                this.txtGoldPrice.Text = (val * val1).ToString("0");
            }
        }

        private void calculatePrice()
        {
            decimal val, val1, val2, val3;
            val = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
            val2 = FormControls.GetDecimalValue(this.txtGoldPrice, 0);
            val3 = FormControls.GetDecimalValue(this.txtStonePrice, 0);

            frm.TotalPrice(val, val1, (val2 + val3), txtTotalPrice);
        }

        private void txtGoldPrice_TextChanged(object sender, EventArgs e)
        {
            calculatePrice();
        }

        private void txtTotalLacker_TextChanged(object sender, EventArgs e)
        {
            calculatePrice();
        }

        private void txtTotalMaking_TextChanged(object sender, EventArgs e)
        {
            calculatePrice();
        }

        private void txtStonePrice_TextChanged(object sender, EventArgs e)
        {
            calculatePrice();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            this.txtNetAmount.Text = (FormControls.GetDecimalValue(this.txtGTotalPrice, 0) - FormControls.GetDecimalValue(this.txtDiscount, 0)).ToString("0");
        }

        private void txtWeight_KeyUp(object sender, KeyEventArgs e)
        {
            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtWeight, 3));
            this.lblRtmWeight.Text = frm.Tola + "T-" + frm.Masha + "M-" + frm.Ratti + "R";
            this.txtWasteIn.Text = "0";
            string str = this.txtWasteIn.Text;
            decimal val = Convert.ToDecimal(str);
            frm.GramsOfPercent(val, FormControls.GetDecimalValue(this.txtWeight, 3), txtWasteage, txtTotalWeight);
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAmountCheck_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtAmountCreditCard_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPGPrice_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void Calculation()
        {
            decimal val, val1, val2, val3, val4, val5;
            val = FormControls.GetDecimalValue(this.txtCashReceive, 0);
            val1 = FormControls.GetDecimalValue(this.txtCheque, 0);
            val2 = FormControls.GetDecimalValue(this.txtCreditCard, 0);
            val3 = FormControls.GetDecimalValue(this.txtPureGold, 0);
            val4 = FormControls.GetDecimalValue(this.txtUsedGold, 0);
            val5 = FormControls.GetDecimalValue(this.txtCashPayment, 0);

            this.txtTotalReceiveAmount.Text = ((val + val1 + val2 + val3 + val4) - val5).ToString("0");
            txtBalance.Text = Convert.ToString(FormControls.GetDecimalValue(txtGTotalPrice, 0) - (FormControls.GetDecimalValue(txtTotalReceiveAmount, 0) + FormControls.GetDecimalValue(txtDiscount, 0)));
        }

        private void txtPriceUseGold_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtBankDeductRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtReceiveables, 0);
            b = FormControls.GetDecimalValue(this.txtBankDeductRate, 1);
            decimal q = (a * b) / 100;
            this.txtDepositeInBank.Text = (a - q).ToString("0");
        }

        private void txtPGRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            decimal q = 0;
            a = FormControls.GetDecimalValue(this.txtPGWeight, 3);
            b = FormControls.GetDecimalValue(this.txtPGRate, 3);
            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
            {
                 q = a * (b / Formulas.WeightInGm);
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                 q = a * b;
            }
            this.txtPureGold.Text = q.ToString("0");
        }

        private void txtRateUsedGold_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            decimal q = 0;
            a = FormControls.GetDecimalValue(this.txtPureWeight, 3);
            b = FormControls.GetDecimalValue(this.txtRateUsedGold, 3);
            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
            {
                q = a * (b / Formulas.WeightInGm);
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                q = a * b;
            }             
            this.txtUsedGold.Text = q.ToString("0");
        }

        private void txtTotalReceiveAmount_TextChanged(object sender, EventArgs e)
        {
            decimal val;
            decimal val1;
            val = FormControls.GetDecimalValue(this.txtNetAmount, 0);
            val1 = FormControls.GetDecimalValue(this.txtTotalReceiveAmount, 0);
            this.txtBalance.Text = (val - val1).ToString("0");
            this.txtAlreadyAmount.Text = this.txtTotalReceiveAmount.Text;
        }
        #endregion

        #region functions
        private void ShowDataGrid()
        {
            FormControls.FillCombobox(Column13, sDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            FormControls.FillCombobox(Column1, sDAL.GetAllStoneName(), "StoneName", "StoneId");
            FormControls.FillCombobox(Column10, sDAL.GetAllColorName(), "ColorName", "ColorId");
            FormControls.FillCombobox(Column11, sDAL.GetAllCutName(), "CutName", "CutId");
            FormControls.FillCombobox(Column12, sDAL.GetAllClearityName(), "ClearityName", "ClearityId");
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
            if (!(this.dgvStoneInformation.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvStoneInformation.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvStoneInformation.Rows[counter].Cells[3].Value) == string.Empty || dgvStoneInformation.Rows[counter].Cells[3].Value == null)
                        weight += 0;
                    else
                        weight += decimal.Parse(dgvStoneInformation.Rows[counter].Cells[3].Value.ToString());
                }
            }
            return weight;
        }

        public decimal updateSum()
        {
            decimal sum = 0;
            int counter;
            for (counter = 0; counter < (dgvStoneInformation.Rows.Count - 1); counter++)
            {
                if (Convert.ToString(dgvStoneInformation.Rows[counter].Cells[3].Value) == "" || Convert.ToString(dgvStoneInformation.Rows[counter].Cells[3].Value) == "0")
                {
                    if (Convert.ToString(dgvStoneInformation.Rows[counter].Cells[4].Value) != string.Empty && Convert.ToString(dgvStoneInformation.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        decimal k;

                        k = (decimal.Parse(dgvStoneInformation.Rows[counter].Cells[4].Value.ToString())) * (decimal.Parse(dgvStoneInformation.Rows[counter].Cells[5].Value.ToString()));
                        dgvStoneInformation.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                }
                else
                {
                    if (Convert.ToString(dgvStoneInformation.Rows[counter].Cells[3].Value) != string.Empty && Convert.ToString(dgvStoneInformation.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        decimal k;

                        k = (decimal.Parse(dgvStoneInformation.Rows[counter].Cells[3].Value.ToString())) * (decimal.Parse(dgvStoneInformation.Rows[counter].Cells[5].Value.ToString()));
                        dgvStoneInformation.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                }
            }
            return sum;
        }

        private void totalweight(decimal val1, decimal val2, TextBox textTotal)
        {
            textTotal.Text = (val1 + val2).ToString("0.000");
        }

        private void ShowAllRecord(int stkId)
        {
            if (stkId <= 0) return;
            else
            {
                stk = stkDAL.GetStockBySockId(stkId);
                if (stk == null)
                    return;
                else
                {
                    if (stk.ItemType == ItemType.Gold)
                        this.cbxItemType.SelectedIndex = 0;
                    else if (stk.ItemType == ItemType.Diamond)
                        this.cbxItemType.SelectedIndex = 1;
                    else if (stk.ItemType == ItemType.Silver)
                        this.cbxItemType.SelectedIndex = 2;
                    else if (stk.ItemType == ItemType.Platinum)
                        this.cbxItemType.SelectedIndex = 3;
                    else
                        this.cbxItemType.SelectedIndex = 4;

                    if (stk.Qty.HasValue)
                        this.txtQty.Text = stk.Qty.ToString();
                    else
                        this.txtQty.Text = "1";
                    if (stk.DesignNo!=null)
                    this.txtDesign.Text = stk.DesignNo.DesignNo;

                    for (int i = 0; i < this.cbxKarrat.Items.Count; i++)
                    {
                        string str = (string)this.cbxKarrat.Items[i];
                        if (stk.Karrat.Equals(str))
                        {
                            this.cbxKarrat.SelectedIndex = i;
                            break;
                        }
                        else
                            this.cbxKarrat.SelectedIndex = 2;
                    }

                    if (stk.WorkerName.ID != null)
                        this.cbxWorker.SelectedValue = stk.WorkerName.ID;

                    this.cbxUsedGoldKarat.DisplayMember = "Name";

                    this.txtDescription.Text = stk.Description.ToString();
                    if (stk.NetWeight.HasValue)
                        this.txtWeight.Text = Convert.ToString(Math.Round((decimal)stk.NetWeight, 3));
                    else
                        this.txtWeight.Text = "";
                    if (stk.WastePercent.HasValue)
                        this.txtWasteIn.Text = Convert.ToString(Math.Round((decimal)stk.WastePercent, 1));
                    else
                        this.txtWasteIn.Text = "16";
                    if (stk.WasteInGm.HasValue)
                        this.txtWasteage.Text = Convert.ToString(Math.Round((decimal)stk.WasteInGm, 3));
                    else
                        this.txtWasteage.Text = "";

                    if (stk.TotalWeight != 0)
                        this.txtTotalWeight.Text = (Math.Round((decimal)stk.TotalWeight, 3)).ToString("0.000");
                    else
                        this.txtTotalWeight.Text = "";

                    if (stk.MakingPerGm.HasValue)

                        this.txtMakingPerGm.Text = Math.Round((decimal)stk.MakingPerGm, 3).ToString("0.0");
                    else
                        this.txtMakingPerGm.Text = "";
                    if (stk.TotalMaking.HasValue)

                        this.txtTotalMaking.Text = Convert.ToDecimal(stk.TotalMaking).ToString("0");
                    else
                        this.txtTotalMaking.Text = "";

                    if (stk.TotalLaker.HasValue)
                        this.txtTotalLacker.Text = Convert.ToDecimal(stk.TotalLaker).ToString("0");
                    else
                        this.txtTotalLacker.Text = "";
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
                    this.txtGrossWeight.Text = this.txtWeight.Text;
                    if (stk.StoneList == null)
                        return;
                    else
                    {
                        this.dgvStoneInformation.Rows.Clear();
                        this.dgvStoneInformation.Refresh();
                        this.dgvStoneInformation.AutoGenerateColumns = false;
                        int count = stk.StoneList.Count;
                        this.dgvStoneInformation.Rows.Add(count);
                        for (int i = 0; i < stk.StoneList.Count; i++)
                        {

                            this.dgvStoneInformation.Rows[i].Cells[1].Value = stk.StoneList[i].StoneTypeId;
                            this.Column1.DataSource = sDAL.GetAllStoneNamebyId(Convert.ToInt32(stk.StoneList[i].StoneTypeId));
                            this.Column1.DisplayMember = "Name";
                            this.Column1.ValueMember = "Id";
                            this.dgvStoneInformation.Rows[i].Cells[2].Value = stk.StoneList[i].StoneId;

                            if (stk.StoneList[i].StoneWeight.HasValue)
                            {                                
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = Convert.ToDecimal(stk.StoneList[i].StoneWeight).ToString("0.000");
                                this.txtGrossWeight.Text = (Convert.ToDecimal(FormControls.StringFormate(this.txtGrossWeight.Text)) + Convert.ToDecimal(stk.StoneList[i].StoneWeight)).ToString("0.000");
                            }
                            else
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = string.Empty;
                            if (stk.StoneList[i].Qty.HasValue)
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = Convert.ToInt32(stk.StoneList[i].Qty);
                            else
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = string.Empty;
                            if (stk.StoneList[i].Rate.HasValue)
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = Convert.ToDecimal(stk.StoneList[i].Rate).ToString("0.0");
                            else
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = string.Empty;
                            if (stk.StoneList[i].Price.HasValue)
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = Convert.ToDecimal(stk.StoneList[i].Price).ToString("0");
                            else
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = string.Empty;
                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ColorName.ColorName.ToString())))
                            {
                                for (int j = 0; j < this.Column10.Items.Count; j++)
                                {
                                    StoneColor stc = (StoneColor)this.Column10.Items[j];
                                    if (stk.StoneList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                                }
                            }
                            if (!(string.IsNullOrEmpty(stk.StoneList[i].CutName.CutName)))
                            {
                                for (int j = 0; j < this.Column11.Items.Count; j++)
                                {
                                    StoneCut stc = (StoneCut)this.Column11.Items[j];
                                    if (stk.StoneList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                                }
                            }
                            if (!(string.IsNullOrEmpty(stk.StoneList[i].ClearityName.ClearityName.ToString())))
                            {
                                for (int j = 0; j < this.Column12.Items.Count; j++)
                                {
                                    StoneClearity stc = (StoneClearity)this.Column12.Items[j];
                                    if (stk.StoneList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void createOrder(object sender, EventArgs e)
        {
            if (eFlag == true)
            {
                oli = new OrderLineItem();
                jp = new JewelPictures();
                if (Main.City == "Islamabad")
                {
                    oli.GRate = FormControls.GetDecimalValue(this.txtGoldRate, 1);
                }
                else
                    oli.GRate = 0;
                oli.OItemId = this.txtId.Text;
                oli.Stock = new Stock();
                oli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                oli.Stock.ItemSize = this.txtItemSize.Text.ToString();
                if (this.chkTagNo.Checked == true)
                    oli.Stock.TagNo = this.cbxTagNo.Text;
                else
                    oli.Stock.TagNo = "";
                if (this.cbxItemType.Text == "Gold")
                    oli.Stock.ItemType = ItemType.Gold;
                else if (this.cbxItemType.Text == "Gold")
                    oli.Stock.ItemType = ItemType.Diamond;
                else if (this.cbxItemType.Text == "Silver")
                    oli.Stock.ItemType = ItemType.Silver;
                else if (this.cbxItemType.Text == "Platinum")
                    oli.Stock.ItemType = ItemType.Platinum;
                else
                    oli.Stock.ItemType = ItemType.Pladium;

                oli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                oli.Stock.GrossWeight = FormControls.GetDecimalValue(this.txtGrossWeight, 3);
                oli.Stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                oli.Stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacker, 0);
                if (Main.City == "Islamabad")
                {
                    oli.Stock.Karrat = "";
                    if (this.pnlPasaRate.Visible == true)
                    {
                        if (rbtPoundPasa.Checked == true)
                            oli.Stock.pFlag = true;
                        if (rbtSonaPasa.Checked == true)
                            oli.Stock.pFlag = false;
                    }
                }
                else
                {
                    oli.Stock.Karrat = this.cbxKarrat.SelectedItem.ToString();
                    oli.Stock.pFlag = null;
                }
                if (Main.City == "Islamabad")
                    oli.Stock.RatePerGm = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtGoldRate, 1) / Formulas.WeightInGm);
                else
                    oli.Stock.RatePerGm = FormControls.GetDecimalValue(this.txtGoldRate, 1);
                oli.Stock.StoneCharges = FormControls.GetDecimalValue(this.txtStonePrice, 0);

                if (this.txtQty.Text == "")
                    oli.Stock.Qty = 1;
                else
                    oli.Stock.Qty = FormControls.GetIntValue(this.txtQty);
                oli.Stock.StockDate = this.dtpOrderDate.Value;
                oli.ReceiveDate = this.dtpOrderDate.Value;
                oli.DesignNo = this.txtDesign.Text;
                if (cbxWorker.SelectedIndex == -1)
                {
                    Worker wr = new Worker();
                    wr.ID = 0;
                    oli.Stock.WorkerName = wr;
                }
                else
                    oli.Stock.WorkerName = (Worker)this.cbxWorker.SelectedItem;
                if (txtDescription.Text == "")
                    oli.Stock.Description = null;
                else
                    oli.Stock.Description = this.txtDescription.Text;

                oli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                frm.RatiMashaTolaGeneral(Convert.ToDecimal(oli.Stock.NetWeight));
                oli.Stock.WTola = frm.Tola;
                oli.Stock.WMasha = frm.Masha;
                oli.Stock.WRatti = frm.Ratti;

                oli.Stock.WastePercent = FormControls.GetDecimalValue(this.txtWasteIn, 1);
                oli.Stock.WasteInGm = FormControls.GetDecimalValue(this.txtWasteage, 3);
                frm.RatiMashaTolaGeneral(Convert.ToDecimal(oli.Stock.WasteInGm));
                oli.Stock.PTola = frm.Tola;
                oli.Stock.PMasha = frm.Masha;
                oli.Stock.PRatti = frm.Ratti;

                oli.Stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(txtTotalWeight, 3));
                oli.Stock.TTola = frm.Tola;
                oli.Stock.TMasha = frm.Masha;
                oli.Stock.TRatti = frm.Ratti;

                if (this.chkTagNo.Checked == true)
                    oli.Stock.Status = "Completed";
                else
                    oli.Stock.Status = "Estimated";
                if (this.txtDescription.Text == "")
                    oli.Stock.Description = "";
                else
                    oli.Stock.Description = this.txtDescription.Text;

                oli.Stock.MakingPerGm = FormControls.GetDecimalValue(this.txtMakingPerGm, 1);
                oli.Stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                oli.Stock.LakerGm = 0;
                oli.Stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacker, 0);

                oli.Stock.StoneList = this.GetAllDetails();
                oli.Stock.TotalPrice = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtTotalPrice, 0));
                oli.ReceiveDate = DateTime.Today;
                if (pbxPicture.Image != null)
                {
                    mst = new MemoryStream();
                    oli.Stock.ImageMemory = oli.Stock.ConvertImageToBinary(this.pbxPicture.Image);
                }
                else
                {
                    oli.Stock.ImageMemory = null;
                }
                ordEst.AddLineItems(oli);
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
                    oli = new OrderLineItem();
                    if (Main.City == "Islamabad")
                    {
                        oli.GRate = FormControls.GetDecimalValue(this.txtGoldRate, 1);
                    }
                    else
                        oli.GRate = 0;
                    oli.OItemId = this.txtId.Text;
                    oli.Stock = new Stock();

                    oli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                    if (this.chkTagNo.Checked == true)
                    {
                        oli.Stock.StockId = stk.StockId;
                        oli.Stock.TagNo = stk.TagNo;
                    }
                    if (this.chkTagNo.Checked == false)
                    {
                        oli.Stock.TagNo = "";
                    }

                    if (this.cbxItemType.Text == "Gold")
                        oli.Stock.ItemType = ItemType.Gold;
                    else if (this.cbxItemType.Text == "Gold")
                        oli.Stock.ItemType = ItemType.Diamond;
                    else if (this.cbxItemType.Text == "Silver")
                        oli.Stock.ItemType = ItemType.Silver;
                    else if (this.cbxItemType.Text == "Platinum")
                        oli.Stock.ItemType = ItemType.Platinum;
                    else
                        oli.Stock.ItemType = ItemType.Pladium;

                    oli.Stock.ItemName = (Item)this.cbxGroupItem.SelectedItem;
                    oli.Stock.ItemSize = this.txtItemSize.Text.ToString();
                    oli.Stock.GrossWeight = FormControls.GetDecimalValue(this.txtGrossWeight, 3);
                    oli.Stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                    oli.Stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacker, 0);

                    if (Main.City == "Islamabad")
                    {
                        oli.Stock.Karrat = "";
                        if (this.pnlPasaRate.Visible == true)
                        {
                            if (rbtPoundPasa.Checked == true)
                                oli.Stock.pFlag = true;
                            if (rbtSonaPasa.Checked == true)
                                oli.Stock.pFlag = false;
                        }
                    }
                    else
                    {
                        oli.Stock.Karrat = this.cbxKarrat.SelectedItem.ToString();
                        oli.Stock.pFlag = null;
                    }

                    if (Main.City == "Islamabad")
                        oli.Stock.RatePerGm = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtGoldRate, 1) / Formulas.WeightInGm);
                    else
                        oli.Stock.RatePerGm = FormControls.GetDecimalValue(this.txtGoldRate, 1);
                    oli.Stock.StoneCharges = FormControls.GetDecimalValue(this.txtStonePrice, 0);
                    if (this.txtQty.Text == "")
                        oli.Stock.Qty = 1;
                    else
                        oli.Stock.Qty = Convert.ToInt32(this.txtQty.Text);
                    oli.Stock.StockDate = this.dtpOrderDate.Value;
                    oli.ReceiveDate = this.dtpOrderDate.Value;
                    oli.DesignNo = this.txtDesign.Text;
                    if (cbxWorker.SelectedIndex == -1)
                    {
                        Worker wr = new Worker();
                        wr.ID = 0;
                        oli.Stock.WorkerName = wr;
                    }
                    else
                        oli.Stock.WorkerName = (Worker)this.cbxWorker.SelectedItem;
                    if (txtDescription.Text == "")
                        oli.Stock.Description = null;
                    else
                        oli.Stock.Description = this.txtDescription.Text;
                    oli.Stock.NetWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                    frm.RatiMashaTolaGeneral(Convert.ToDecimal(oli.Stock.NetWeight));
                    oli.Stock.WTola = frm.Tola;
                    oli.Stock.WMasha = frm.Masha;
                    oli.Stock.WRatti = frm.Ratti;
                    oli.Stock.WastePercent = FormControls.GetDecimalValue(this.txtWasteIn, 0);
                    oli.Stock.WasteInGm = FormControls.GetDecimalValue(this.txtWasteage, 3);
                    frm.RatiMashaTolaGeneral(Convert.ToDecimal(oli.Stock.WasteInGm));
                    oli.Stock.PTola = frm.Tola;
                    oli.Stock.PMasha = frm.Masha;
                    oli.Stock.PRatti = frm.Ratti;
                    oli.Stock.TotalWeight = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
                    frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(txtTotalWeight, 3));
                    oli.Stock.TTola = frm.Tola;
                    oli.Stock.TMasha = frm.Masha;
                    oli.Stock.TRatti = frm.Ratti;
                    if (this.chkTagNo.Checked == true)
                        oli.Stock.Status = "Completed";
                    else
                        oli.Stock.Status = "Estimated";
                    if (this.txtDescription.Text == "")
                        oli.Stock.Description = "";
                    else
                        oli.Stock.Description = this.txtDescription.Text;

                    oli.Stock.MakingPerGm = FormControls.GetDecimalValue(this.txtMakingPerGm, 1);
                    oli.Stock.TotalMaking = FormControls.GetDecimalValue(this.txtTotalMaking, 0);
                    if (txtTotalWeight.Text == "" || txtTotalWeight.Text == "0")
                        oli.Stock.LakerGm = 0;
                    else
                        oli.Stock.LakerGm = Math.Round(FormControls.GetDecimalValue(this.txtTotalLacker, 0) / FormControls.GetDecimalValue(this.txtTotalWeight, 3), 0);
                    oli.Stock.TotalLaker = FormControls.GetDecimalValue(this.txtTotalLacker, 0);

                    oli.Stock.StoneList = this.GetAllDetails();
                    oli.Stock.TotalPrice = Convert.ToDecimal(FormControls.GetDecimalValue(this.txtTotalPrice, 0));
                    if (pbxPicture.Image != null)
                    {
                        mst = new MemoryStream();
                        oli.Stock.ImageMemory = oli.Stock.ConvertImageToBinary(this.pbxPicture.Image);
                    }
                    else
                        oli.Stock.ImageMemory = null;
                    ordEst.AddLineItems(oli);
                }
            }
        }

        private List<Stones> GetAllDetails()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStoneInformation.Rows.Count) - 1;
            if (j == 0)
            {
                return stDetail;
            }
            else
            {
                stDetail = new List<Stones>();
                for (int i = 0; i < j; i++)
                {
                    Stones std = new Stones();
                    std.StoneTypeId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[1].Value.ToString());
                    std.StoneId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[2].Value.ToString());
                    if ((string)dgvStoneInformation.Rows[i].Cells[3].FormattedValue == "")
                        std.StoneWeight = 0;
                    else
                        std.StoneWeight = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[3].Value.ToString()), 3);
                    if ((string)dgvStoneInformation.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = 0;

                    else
                        std.Qty = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[4].Value.ToString());

                    if ((string)dgvStoneInformation.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = 0;

                    else
                        std.Rate = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[5].Value.ToString()), 1);

                    if ((string)dgvStoneInformation.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = 0;

                    else
                        std.Price = Math.Round(Convert.ToDecimal(dgvStoneInformation.Rows[i].Cells[6].Value.ToString()), 0);


                    if ((string)dgvStoneInformation.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {

                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < Column10.Items.Count; k++)
                        {
                            StoneColor stc = (StoneColor)Column10.Items[k];
                            if (stc.ColorId == std.ColorName.ColorId)
                                std.ColorName.ColorName = stc.ColorName;
                        }
                    }
                    if ((string)dgvStoneInformation.Rows[i].Cells[8].FormattedValue == "")
                        std.CutName = null;
                    else
                    {
                        std.CutName = new StoneCut();
                        std.CutName.CutId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[8].Value.ToString());
                        for (int k = 0; k < Column11.Items.Count; k++)
                        {
                            StoneCut stc = (StoneCut)Column11.Items[k];
                            if (stc.CutId == std.CutName.CutId)
                                std.CutName.CutName = stc.CutName;
                        }
                    }
                    if ((string)dgvStoneInformation.Rows[i].Cells[9].FormattedValue == "")
                        std.ClearityName = null;
                    else
                    {
                        std.ClearityName = new StoneClearity();
                        std.ClearityName.ClearityId = Convert.ToInt32(dgvStoneInformation.Rows[i].Cells[9].Value.ToString());
                        for (int k = 0; k < Column12.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)Column12.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }

        private List<Stock> getTags(int id)
        {
            List<Stock> records = stkDAL.GetTagNoByItemIdForOrder(id);
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
                    if (dgvBookedItem.Rows[i].Cells[2].Value != null)
                    {
                        string str = dgvBookedItem.Rows[i].Cells[2].Value.ToString();

                        foreach (Stock stk in records)
                        {
                            if (str.Equals(stk.TagNo))
                            {
                                lstTag.Add(stk);
                            }
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

        public void RefreshPage1()
        {
            this.txtWeight.Text = "";
            this.txtDesign.Text = "";
            this.cbxWorker.SelectedIndex = -1;
            this.txtDescription.Text = "";
            this.txtStonePrice.Text = "";
            this.txtWasteIn.Text = "";
            this.txtWasteage.Text = "";
            this.txtTotalWeight.Text = "";
            this.txtTotalLacker.Text = "";
            this.txtGoldPrice.Text = "";
            this.txtMakingPerGm.Text = "";
            this.txtTotalMaking.Text = "";
            this.txtGrossWeight.Text = "";
            this.txtTotalPrice.Text = "";
            this.chkTagNo.Checked = false;
            this.pbxPicture.Image = null;
        }

        public void RefreshPage2()
        {
            this.txtBillBookNo.Text = "";
            this.cbxSaleMan.SelectedIndex = -1;
            this.cbxCustomerName.Text = "";
            this.cbxContactNo.Text = "";
            this.txtAddress.Text = "";
            this.txtTotalPrice.Text = "";
            this.txtNetAmount.Text = "";
            this.txtDiscount.Text = "";
            this.txtAmount.Text = "";
            this.txtGTotalPrice.Text = "";
            this.txtCheque.Text = "";
            this.txtBankDeductRate.Text = "";
            this.txtDepositeInBank.Text = "";
            this.cbxDepositeAccount.SelectedIndex = -1;
            this.txtChequeDescription.Text = "";
            this.txtCreditCard.Text = "";
            this.txtDeductRate.Text = "";
            this.txtReceiveables.Text = "";
            this.cbxBank.SelectedIndex = -1;
            this.txtBankDeductRate.Text = "";
            this.txtDepositeInBank.Text = "";
            this.cbxDepositeAccount.Text = "";
            this.txtPureGold.Text = "";
            this.txtPGRate.Text = "";
            this.txtPGWeight.Text = "";
            this.txtWeightUsedGold.Text = "";
            this.txtKaat.Text = "";
            this.txtPureWeight.Text = "";
            this.txtRateUsedGold.Text = "";
            this.txtUsedGold.Text = "";
            this.txtTotalReceiveAmount.Text = "";
            this.txtBalance.Text = "";
            this.txtNetAmount.Text = "";
        }

        private void CalculateBill(decimal amount, decimal cons, TextBox txt)
        {
            decimal netBill = Math.Round((amount - cons), 0);
            txt.Text = netBill.ToString();
        }

        #endregion

        #region cbxChange
        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.SelectedIndex == -1)
            {
                return;
            }
            else
            {
                if (this.dgvBookedItem.Rows.Count > 0)
                {
                    int k = (int)this.cbxGroupItem.SelectedValue;

                    this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);

                    this.cbxTagNo.DisplayMember = "TagNo";
                    this.cbxTagNo.ValueMember = "StockId";
                    this.cbxTagNo.DataSource = getTags(k);
                    this.cbxTagNo.SelectedIndex = -1;

                    this.cbxKarrat.SelectedIndex = 2;
                }
                else
                {
                    if (eFlag == true && this.dgvBookedItem.Rows.Count == 0)
                        l = 1;
                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;

                    int k = (int)this.cbxGroupItem.SelectedValue;
                    this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);

                    this.cbxTagNo.DisplayMember = "TagNo";
                    this.cbxTagNo.ValueMember = "StockId";
                    this.cbxTagNo.DataSource = getTags(k);
                    this.cbxTagNo.SelectedIndex = -1;

                    this.cbxKarrat.SelectedIndex = 2;
                }
            }
        }

        private void cbxKarrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                string r = (string)this.cbxKarrat.SelectedItem;
                decimal s = grDAL.GetRateByKarat(r, Convert.ToDateTime(dtpOrderDate.Value));
                this.txtGoldRate.Text = s.ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
            {
                string r = (string)this.cbxKarrat.SelectedItem;
                decimal s = grDAL.GetRateByKaratTola(r, Convert.ToDateTime(dtpOrderDate.Value));
                this.txtGoldRate.Text = s.ToString("0");
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
                if (s.StockId == null)
                    return;
                else
                {
                    this.lblHidden.Text = s.StockId.ToString();
                    this.ShowAllRecord(s.StockId);
                }
            }
        }

        private void cbxBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal val;

            if (this.txtBankDeductRate.Text != "")
                val = 0;
            else
            {
                try
                {
                    Banks bnk = (Banks)this.cbxBank.SelectedItem;
                    val = bnk.DRate;
                    this.txtBankDeductRate.Text = val.ToString("0.0");
                    FormControls.FillCombobox(cbxDepositeAccountCreditCard, bDAL.GetAllBankAccountByBankId(bnk.Id), "AccountNo", "Id");
                }
                catch 
                { }
            }
        }
        #endregion

        #region click
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Want To add more entries", Messages.Header, MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Yes)
            {
                if (eFlag == true)
                {
                    this.createOrder(sender, e);
                    object[] values1 = new Object[4];
                    values1[0] = this.txtId.Text.ToString();
                    Item itm1 = cbxGroupItem.SelectedItem as Item;
                    values1[1] = itm1.ItemName.ToString();
                    values1[2] = oli.Stock.TagNo.ToString();
                    values1[3] = oli.Stock.StockId.ToString();
                    this.dgvBookedItem.Rows.Add(values1);
                    l = this.dgvBookedItem.Rows.Count + 1;
                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                    this.RefreshPage1();
                    this.dgvStoneInformation.Rows.Clear();
                }
                else
                {
                    this.createOrder(sender, e);
                    if (strg != "")
                    {
                        foreach (OrderLineItem oli in ordEst.OrderLineItem)
                        {
                            if (oli.OItemId == strg)
                            {
                                object[] values1 = new Object[4];
                                values1[0] = this.txtId.Text.ToString();
                                Item itm1 = cbxGroupItem.SelectedItem as Item;
                                values1[1] = itm1.ItemName.ToString();
                                if (stk.TagNo == null)
                                {
                                    values1[2] = null;
                                    values1[3] = null;
                                    this.dgvBookedItem.Rows.Add(values1);
                                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                    this.RefreshPage1();
                                    this.dgvStoneInformation.Rows.Clear();
                                }
                                else
                                {
                                    values1[2] = stk.TagNo.ToString();
                                    values1[3] = stk.StockId.ToString();
                                    this.dgvBookedItem.Rows.Add(values1);
                                    this.cbxTagNo.DisplayMember = "TagNo";
                                    this.cbxTagNo.ValueMember = "StockId";
                                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                    this.cbxTagNo.DataSource = getTags(itm1.ItemId);
                                    this.cbxTagNo.SelectedIndex = -1;
                                    this.RefreshPage1();
                                    this.dgvStoneInformation.Rows.Clear();
                                    stk = new Stock();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.cbxGroupItem.Text == "" || this.txtWeight.Text == "" || this.txtQty.Text == "")
                        {
                            return;
                        }
                        else
                        {
                            object[] values = new Object[4];
                            values[0] = this.txtId.Text.ToString();
                            Item itm = cbxGroupItem.SelectedItem as Item;
                            values[1] = itm.ItemName.ToString();
                            if (stk.TagNo == null)
                            {
                                values[2] = null;
                                values[3] = null;
                                this.dgvBookedItem.Rows.Add(values);
                                l = l + 1;
                                this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                this.RefreshPage1();
                                this.dgvStoneInformation.Rows.Clear();
                            }
                            else
                            {
                                values[2] = stk.TagNo.ToString();
                                values[3] = stk.StockId.ToString();
                                this.dgvBookedItem.Rows.Add(values);
                                l = l + 1;
                                this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                this.cbxTagNo.DisplayMember = "TagNo";
                                this.cbxTagNo.ValueMember = "StockId";
                                this.cbxTagNo.DataSource = getTags(itm.ItemId);
                                this.cbxTagNo.SelectedIndex = -1;
                                this.RefreshPage1();
                                this.dgvStoneInformation.Rows.Clear();
                                stk = new Stock();
                            }
                        }
                    }
                }
                cbxItemType.Select();
            }
            else if (dr == DialogResult.No)
            {
                if (eFlag == true)
                {
                    this.createOrder(sender, e);
                    object[] values1 = new Object[4];
                    values1[0] = this.txtId.Text.ToString();
                    Item itm1 = cbxGroupItem.SelectedItem as Item;
                    values1[1] = itm1.ItemName.ToString();
                    values1[2] = oli.Stock.TagNo.ToString();
                    values1[3] = oli.Stock.StockId.ToString();
                    this.dgvBookedItem.Rows.Add(values1);
                    l = this.dgvBookedItem.Rows.Count + 1;
                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                    this.RefreshPage1();
                    this.dgvStoneInformation.Rows.Clear();
                }
                else
                {
                    this.createOrder(sender, e);
                    if (strg != "")
                    {
                        foreach (OrderLineItem oli in ordEst.OrderLineItem)
                        {
                            if (oli.OItemId == strg)
                            {
                                object[] values1 = new Object[4];
                                values1[0] = this.txtId.Text.ToString();
                                Item itm1 = cbxGroupItem.SelectedItem as Item;
                                values1[1] = itm1.ItemName.ToString();
                                if (stk.TagNo == null)
                                {
                                    values1[2] = null;
                                    values1[3] = null;
                                    this.dgvBookedItem.Rows.Add(values1);
                                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                    this.RefreshPage1();
                                    this.dgvStoneInformation.Rows.Clear();
                                }
                                else
                                {
                                    values1[2] = stk.TagNo.ToString();
                                    values1[3] = stk.StockId.ToString();
                                    this.dgvBookedItem.Rows.Add(values1);
                                    this.cbxTagNo.DisplayMember = "TagNo";
                                    this.cbxTagNo.ValueMember = "StockId";
                                    this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                    this.cbxTagNo.DataSource = getTags(itm1.ItemId);
                                    this.cbxTagNo.SelectedIndex = -1;
                                    this.RefreshPage1();
                                    this.dgvStoneInformation.Rows.Clear();
                                    stk = new Stock();
                                }
                            }
                        }
                    }
                    else
                    {
                        if (this.cbxGroupItem.Text == "" || this.txtWeight.Text == "" || this.txtQty.Text == "")
                            return;
                        else
                        {
                            object[] values = new Object[4];
                            values[0] = this.txtId.Text.ToString();
                            Item itm = cbxGroupItem.SelectedItem as Item;
                            values[1] = itm.ItemName.ToString();
                            if (stk.TagNo == null)
                            {
                                values[2] = null;
                                values[3] = null;
                                this.dgvBookedItem.Rows.Add(values);
                                l = l + 1;
                                this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                this.RefreshPage1();
                                this.dgvStoneInformation.Rows.Clear();
                            }
                            else
                            {
                                values[2] = stk.TagNo.ToString();
                                values[3] = stk.StockId.ToString();
                                this.dgvBookedItem.Rows.Add(values);
                                l = l + 1;
                                this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                                this.cbxTagNo.DisplayMember = "TagNo";
                                this.cbxTagNo.ValueMember = "StockId";
                                this.cbxTagNo.DataSource = getTags(itm.ItemId);
                                this.cbxTagNo.SelectedIndex = -1;
                                this.RefreshPage1();
                                this.dgvStoneInformation.Rows.Clear();
                                stk = new Stock();
                            }
                        }
                    }
                }
                btnBill_Click(sender, e);
            }
            else if (dr == DialogResult.Cancel)
            {
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OrderLineItem ol = new OrderLineItem();
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
                        l = l - 1;
                        foreach (OrderLineItem oli in ordEst.OrderLineItem)
                        {
                            if (oli.OItemId == tag)
                            {
                                this.txtId.Text = oli.OItemId.ToString();
                                ol = oli;
                            }
                        }
                        ordEst.RemoveLineItems(ol);
                        this.RefreshPage1();
                        this.dgvStoneInformation.Rows.Clear();
                        strg = "";
                    }
                    else
                    {
                        tag = dgvBookedItem.Rows[z].Cells[2].Value.ToString();
                        dgvBookedItem.Rows.Remove(dgvBookedItem.Rows[z]);
                        l = l - 1;
                        foreach (OrderLineItem oli in ordEst.OrderLineItem)
                        {
                            if (oli.Stock.TagNo == tag)
                            {
                                this.txtId.Text = oli.OItemId.ToString();
                                ol = oli;
                            }
                        }
                        ordEst.RemoveLineItems(ol);
                        this.RefreshPage1();
                        this.dgvStoneInformation.Rows.Clear();
                        strg = "";
                    }
                }
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
                MessageBox.Show("Please select Item to order", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                this.tabControl1.SelectedTab = tabPage2;
                this.dtpOrderDate.Value = this.dtpOrderDate.Value;
                this.txtGTotalPrice.Text = (Math.Round(ordEst.GetGrossTotal(), 0)).ToString("0");
                this.txtNetAmount.Text = (Math.Round(ordEst.GetGrossTotal(), 0) - FormControls.GetDecimalValue(this.txtDiscount, 0)).ToString("0");
                Calculation();
                this.chkFixRate.Select();
            }
        }
        #endregion

        #region checkedChange
        private void chkTagNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkTagNo.Checked == true)
                this.cbxTagNo.Enabled = true;
            else
            {
                this.cbxTagNo.SelectedIndex = -1;
                this.RefreshPage1();
                this.cbxTagNo.Enabled = false;
            }
        }

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCash.Checked == true)
            {
                this.txtAmount.Enabled = true;
                this.txtAmount.BackColor = Color.White;
            }
            else
            {
                this.txtAmount.Enabled = false;
                this.txtAmount.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
            }
        }

        private void chkCheque_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCheque.Checked == true)
            {
                this.txtCheque.Enabled = true;
                this.txtCheque.BackColor = Color.White;
                this.cbxDepositeAccount.Enabled = true;
                this.cbxDepositeAccount.BackColor = Color.White;
                this.txtDescription.Enabled = true;
                this.txtDescription.BackColor = Color.White;
            }
            else
            {
                this.txtCheque.Enabled = false;
                this.txtCheque.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.cbxDepositeAccount.Enabled = false;
                this.cbxDepositeAccount.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtDescription.Enabled = false;
                this.txtDescription.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
            }
        }

        private void chkCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCreditCard.Checked == true)
            {
                this.txtCreditCard.Enabled = true;
                this.txtCreditCard.BackColor = Color.White;
                this.txtDeductRate.Enabled = true;
                this.txtDeductRate.BackColor = Color.White;
                this.txtReceiveables.Enabled = true;
                this.txtReceiveables.BackColor = Color.White;
                this.cbxBank.Enabled = true;
                this.cbxBank.BackColor = Color.White;
                this.txtBankDeductRate.Enabled = true;
                this.txtBankDeductRate.BackColor = Color.White;
                this.txtDepositeInBank.Enabled = true;
                this.txtDepositeInBank.BackColor = Color.White;
                this.cbxDepositeAccountCreditCard.Enabled = true;
                this.cbxDepositeAccountCreditCard.BackColor = Color.White;
            }
            else
            {
                this.txtCreditCard.Enabled = false;
                this.txtCreditCard.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtDeductRate.Enabled = false;
                this.txtDeductRate.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtReceiveables.Enabled = false;
                this.txtReceiveables.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.cbxBank.Enabled = false;
                this.cbxBank.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtBankDeductRate.Enabled = false;
                this.txtBankDeductRate.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtDepositeInBank.Enabled = false;
                this.txtDepositeInBank.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.cbxDepositeAccountCreditCard.Enabled = false;
                this.cbxDepositeAccountCreditCard.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
            }
        }

        private void chkPureGold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPureGold.Checked == true)
            {
                this.txtPGWeight.Enabled = true;
                this.txtPGWeight.BackColor = Color.White;
                this.txtPGRate.Enabled = true;
                this.txtPGRate.BackColor = Color.White;
                this.txtPureGold.Enabled = true;
                this.txtPureGold.BackColor = Color.White;
            }
            else
            {
                this.txtPGWeight.Enabled = false;
                this.txtPGWeight.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtPGRate.Enabled = false;
                this.txtPGRate.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtPureGold.Enabled = false;
                this.txtPureGold.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
            }
        }

        private void chkUseGold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUseGold.Checked == true)
            {
                this.txtWeightUsedGold.Enabled = true;
                this.txtWeightUsedGold.BackColor = Color.White;
                this.txtKaat.Enabled = true;
                this.txtKaat.BackColor = Color.White;
                this.cbxUsedGoldKarat.Enabled = true;
                this.cbxUsedGoldKarat.BackColor = Color.White;
                this.txtPureWeight.Enabled = true;
                this.txtPureWeight.BackColor = Color.White;
                this.txtRateUsedGold.Enabled = true;
                this.txtRateUsedGold.BackColor = Color.White;
                this.txtUsedGold.Enabled = true;
                this.txtUsedGold.BackColor = Color.White;
            }
            else
            {
                this.txtWeightUsedGold.Enabled = false;
                this.txtWeightUsedGold.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtKaat.Enabled = false;
                this.txtKaat.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.cbxUsedGoldKarat.Enabled = false;
                this.cbxUsedGoldKarat.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtPureWeight.Enabled = false;
                this.txtPureWeight.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtRateUsedGold.Enabled = false;
                this.txtRateUsedGold.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
                this.txtUsedGold.Enabled = false;
                this.txtUsedGold.BackColor = Color.FromArgb(mycolor.GridViewBackground.Red, mycolor.GridViewBackground.Green, mycolor.GridViewBackground.Blue);
            }
        }
        #endregion

        private void dgvStoneInformation_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 3)
            {
                TextBox txtbox = e.Control as TextBox;
                if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 3)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
                if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 7)
                    txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
            }
            if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 2 && this.dgvStoneInformation.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStoneInformation.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = sDAL.GetAllStoneNamebyId(sty);
                cmb.DisplayMember = "Name";
                cmb.ValueMember = "Id";
            }
        }

        void txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 4 || this.dgvStoneInformation.CurrentCell.ColumnIndex == 6)
            {
                if (e.KeyChar == 46)
                    e.Handled = true;
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                    e.Handled = true;
            }
            if (this.dgvStoneInformation.CurrentCell.ColumnIndex == 3 || this.dgvStoneInformation.CurrentCell.ColumnIndex == 5)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    e.Handled = true;
                if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                    e.Handled = true;
            }
        }

        private void dgvStoneInformation_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    string s = val.ToString("N3");
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value = s.ToString();
                }
                catch { }
            }
            decimal val1;
            decimal val2;
            if (this.txtTotalWeight.Text == "")
                val1 = 0;
            else
                val1 = FormControls.GetDecimalValue(txtTotalWeight, 3);
            val2 = upDateTextBox();
            this.totalweight(val1, val2, txtGrossWeight);
            this.txtStonePrice.Text = updateSum().ToString("0");
        }

        private void cbxTagNo_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxTagNo.SelectedIndexChanged += new EventHandler(cbxTagNo_SelectedIndexChanged);
        }

        private void txtDeductRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0, b = 0;
            if (this.txtCreditCard.Text == "")
                a = 0;
            else
                a = FormControls.GetDecimalValue(this.txtCreditCard, 0);
            if (this.txtDeductRate.Text == "")
                b = 0;
            else
                b = FormControls.GetDecimalValue(this.txtDeductRate, 1);
            decimal q = (a * b) / 100;
            this.txtReceiveables.Text = (q + a).ToString("0");
        }

        private void cbxBank_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxBank.SelectedIndexChanged += new EventHandler(cbxBank_SelectedIndexChanged);
        }

        private void ShowOrder(int oNo)
        {
            decimal cash, gldCash;
            ordEst = oDAL.GetOrderByOrderNo(oNo);
            if (ordEst == null)
            {
                MessageBox.Show("Order Not Found...!!!", Messages.Header);
                return;
            }
            this.txtOrderNo.Text = ordEst.OrderNo.ToString();
            this.txtBillBookNo.Text = ordEst.BillBookNo.ToString();
            this.lblCustId.Text = ordEst.CustName.ID.ToString();
            this.cbxCustomerName.SelectedValue = (int)ordEst.CustName.ID;
            cust = (Customer) this.cbxCustomerName.SelectedItem;
            this.cbxSaleMan.SelectedValue = ordEst.SalesMan.ID;
            frm.KaatInRattiForBalance(FormControls.GetDecimalValue(this.txtKaat, 1), FormControls.GetDecimalValue(this.txtWeightUsedGold, 3), txtPureWeight);
            oDAL.GetReceivedAmount((int)ordEst.OrderNo, out cash, out gldCash);
            this.txtGTotalPrice.Text = ((decimal)ordEst.TotalPrice).ToString("0");
            this.txtNetAmount.Text = ordEst.NetBill.ToString("0");
            this.txtAlreadyAmount.Text = cash.ToString("0");
            txtDiscount.Text = ordEst.BillDiscout.ToString("0");
            if (ordEst.CashReceive > 0)
                this.txtCashReceive.Text = ((decimal)ordEst.CashReceive).ToString("0");
            if (ordEst.CashPayment > 0)
                this.txtCashPayment.Text = ordEst.CashPayment.ToString("0");
            if (ordEst.PureGoldCharges > 0)
                this.txtPureGold.Text = ((decimal)ordEst.PureGoldCharges).ToString("0");
            if (ordEst.UsedGoldCharges > 0)
                this.txtUsedGold.Text = ((decimal)ordEst.UsedGoldCharges).ToString("0");
            if (ordEst.CreditCard > 0)
                this.txtCreditCard.Text = ((decimal)ordEst.CreditCard).ToString("0");
            if (ordEst.CheckCash > 0)
                this.txtCheque.Text = ((decimal)ordEst.CheckCash).ToString("0");

            this.dtpOrderDate.Value = Convert.ToDateTime(ordEst.ODate);
            this.dtpDeliveryDate.Value = Convert.ToDateTime(ordEst.DDate);
            this.txtDiscount.Text = ordEst.BillDiscout.ToString("0");
            this.dgvBookedItem.AutoGenerateColumns = false;
            this.dgvBookedItem.Rows.Clear();
            if (ordEst.OrderLineItem != null && ordEst.OrderLineItem.Count > 0)
            {
                int i = 0;
                lstOItemId = new List<string>();
                foreach (OrderLineItem oli in ordEst.OrderLineItem)
                {
                    object[] values1 = new Object[4];
                    values1[0] = oli.OItemId.ToString();
                    lstOItemId.Add(oli.OItemId);
                    values1[1] = oli.Stock.ItemName.ItemName.ToString();
                    values1[2] = oli.Stock.TagNo.ToString();
                    values1[3] = oli.Stock.StockId.ToString();
                    this.dgvBookedItem.Rows.Add(values1);
                }
                l = ordEst.OrderLineItem.Count + 1;
                this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
            }
            else 
                return;
        }

        private void ShowAllRecordByItemId(string strg)
        {
            foreach (OrderLineItem oli in ordEst.OrderLineItem)
            {
                if (oli.OItemId == strg)
                {
                    this.txtGoldRate.Text = oli.GRate.ToString("0.0");
                    this.txtId.Text = oli.OItemId.ToString();

                    if (oli.Stock.ItemType == ItemType.Gold)
                        this.cbxItemType.SelectedIndex = 0;
                    else if (oli.Stock.ItemType == ItemType.Diamond)
                        this.cbxItemType.SelectedIndex = 1;
                    else if (oli.Stock.ItemType == ItemType.Silver)
                        this.cbxItemType.SelectedIndex = 2;
                    else if (oli.Stock.ItemType == ItemType.Platinum)
                        this.cbxItemType.SelectedIndex = 3;
                    else
                        this.cbxItemType.SelectedIndex = 4;

                    this.cbxGroupItem.SelectedValue = oli.Stock.ItemName.ItemId;
                    int k = (int)this.cbxGroupItem.SelectedValue;

                    FormControls.FillCombobox(cbxTagNo, getTags(k), "TagNo", "StockId");
                    this.cbxTagNo.SelectedValue = oli.Stock.StockId;

                    if (oli.Stock.GrossWeight == 0)
                        this.txtGrossWeight.Text = "";
                    else
                        this.txtGrossWeight.Text = ((decimal)oli.Stock.GrossWeight).ToString("0.000");
                    if (oli.Stock.TotalMaking == 0)
                        this.txtTotalMaking.Text = "";
                    else
                        this.txtTotalMaking.Text = ((decimal)oli.Stock.TotalMaking).ToString("0");
                    if (oli.Stock.TotalLaker == 0)
                        this.txtTotalLacker.Text = "";
                    else
                        this.txtTotalLacker.Text = ((decimal)oli.Stock.TotalLaker).ToString("0");

                    for (int i = 0; i < this.cbxKarrat.Items.Count; i++)
                    {
                        string str1 = (string)this.cbxKarrat.Items[i];
                        if (oli.Stock.Karrat.Equals(str1))
                        {
                            this.cbxKarrat.SelectedIndex = i;
                            break;
                        }
                        else
                            this.cbxKarrat.SelectedIndex = 2;
                    }
                    if (oli.Stock.StoneCharges == 0)
                        this.txtStonePrice.Text = "";
                    else
                        this.txtStonePrice.Text = oli.Stock.StoneCharges.ToString("0");

                    if (oli.Stock.Qty == 0)
                        this.txtQty.Text = "";
                    else
                        this.txtQty.Text = oli.Stock.Qty.ToString();

                    this.dtpOrderDate.Value = Convert.ToDateTime(oli.Stock.StockDate);

                    this.txtDesign.Text = oli.DesignNo;

                    this.txtItemSize.Text = oli.Stock.ItemSize.ToString();

                    FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
                    this.cbxWorker.SelectedValue = oli.Stock.WorkerName.ID;


                    if (oli.Stock.Description == null)
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = oli.Stock.Description;

                    this.txtWeight.Text = ((decimal)oli.Stock.NetWeight).ToString("0.000");
                    if (Main.City == "Islamabad")
                        this.txtGoldRate.Text = ((decimal)oli.Stock.RatePerGm * Formulas.WeightInGm).ToString("0");
                    else
                        this.txtGoldRate.Text = ((decimal)oli.Stock.RatePerGm).ToString("0");
                    this.txtTotalPrice.Text = ((decimal)oli.Stock.TotalPrice).ToString("0");

                    if (oli.Stock.WastePercent == 0)
                        this.txtWasteIn.Text = "";
                    else
                        this.txtWasteIn.Text = ((decimal)oli.Stock.WastePercent).ToString("0.0");

                    if (oli.Stock.WasteInGm == 0)
                        this.txtWasteage.Text = "";
                    else
                        this.txtWasteage.Text = ((decimal)oli.Stock.WasteInGm).ToString("0.000");

                    this.txtTotalWeight.Text = oli.Stock.TotalWeight.ToString("0.000");

                    if (oli.Stock.Description == "")
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = oli.Stock.Description;


                    if (oli.Stock.MakingPerGm == 0)
                        this.txtMakingPerGm.Text = "";
                    else
                        this.txtMakingPerGm.Text = ((decimal)oli.Stock.MakingPerGm).ToString("0.0");

                    if (oli.Stock.TotalMaking == 0)
                        this.txtTotalMaking.Text = "";
                    else
                        this.txtTotalMaking.Text = ((decimal)oli.Stock.TotalMaking).ToString("0");

                    if (oli.Stock.TotalLaker == 0)
                        this.txtTotalLacker.Text = "";
                    else
                        this.txtTotalLacker.Text = ((decimal)oli.Stock.TotalLaker).ToString("0");
                    this.txtTotalPrice.Text = ((decimal)oli.Stock.TotalPrice).ToString("0");
                    if (oli.Stock.ImageMemory == null)
                    {
                        this.pbxPicture.Image = null;
                        this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        MemoryStream mst = new MemoryStream(oli.Stock.ImageMemory);
                        this.pbxPicture.Image = Image.FromStream(mst);
                    }
                    if (oli.Stock.StoneList == null)
                    {
                        return;
                    }
                    else
                    {
                        this.dgvStoneInformation.AutoGenerateColumns = false;
                        int count = oli.Stock.StoneList.Count;
                        this.dgvStoneInformation.Rows.Add(count);
                        for (int i = 0; i < oli.Stock.StoneList.Count; i++)
                        {
                            this.dgvStoneInformation.Rows[i].Cells[1].Value = oli.Stock.StoneList[i].StoneTypeId;
                            this.Column1.DataSource = sDAL.GetAllStoneNamebyId(Convert.ToInt32(oli.Stock.StoneList[i].StoneTypeId));
                            this.Column1.DisplayMember = "Name";
                            this.Column1.ValueMember = "Id";

                            this.dgvStoneInformation.Rows[i].Cells[2].Value = oli.Stock.StoneList[i].StoneId;
                            if (oli.Stock.StoneList[i].StoneWeight == 0)
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].StoneWeight), 3);
                            if (oli.Stock.StoneList[i].Qty == 0)
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = Convert.ToInt32(oli.Stock.StoneList[i].Qty);
                            if (oli.Stock.StoneList[i].Rate == 0)
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].Rate), 1);
                            if (oli.Stock.StoneList[i].Price == 0)
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].Price), 0);
                            if (oli.Stock.StoneList[i].ColorName != null)
                            {
                                for (int j = 0; j < this.Column10.Items.Count; j++)
                                {
                                    StoneColor stcl = (StoneColor)this.Column10.Items[j];
                                    if (oli.Stock.StoneList[i].ColorName.ColorName.Equals(stcl.ColorName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[7].Value = Convert.ToInt32(stcl.ColorId);
                                }
                            }
                            if (oli.Stock.StoneList[i].CutName != null)
                            {
                                for (int j = 0; j < this.Column11.Items.Count; j++)
                                {
                                    StoneCut stcl = (StoneCut)this.Column11.Items[j];
                                    if (oli.Stock.StoneList[i].CutName.CutName.Equals(stcl.CutName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[8].Value = Convert.ToInt32(stcl.CutId);
                                }
                            }
                            if (oli.Stock.StoneList[i].ClearityName != null)
                            {
                                for (int j = 0; j < this.Column12.Items.Count; j++)
                                {
                                    StoneClearity stcl = (StoneClearity)this.Column12.Items[j];
                                    if (oli.Stock.StoneList[i].ClearityName.ClearityName.Equals(stcl.ClearityName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[9].Value = Convert.ToInt32(stcl.ClearityId);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ShowRecordByItemId(string strg)
        {
            foreach (OrderLineItem oli in ordEst.OrderLineItem)
            {
                if (oli.OItemId == strg)
                {
                    this.txtGoldRate.Text = oli.GRate.ToString();
                    this.txtId.Text = oli.OItemId.ToString();

                    if (oli.Stock.ItemType == ItemType.Gold)
                        this.cbxItemType.SelectedIndex = 0;
                    else if (oli.Stock.ItemType == ItemType.Diamond)
                        this.cbxItemType.SelectedIndex = 1;
                    else if (oli.Stock.ItemType == ItemType.Silver)
                        this.cbxItemType.SelectedIndex = 2;
                    else if (oli.Stock.ItemType == ItemType.Platinum)
                        this.cbxItemType.SelectedIndex = 3;
                    else
                        this.cbxItemType.SelectedIndex = 4;

                    this.cbxGroupItem.SelectedValue = oli.Stock.ItemName.ItemId;

                    if (oli.Stock.TagNo == "")
                        this.cbxTagNo.Text = "";
                    else
                    {
                        this.chkTagNo.Checked = true;
                        FormControls.FillCombobox(cbxTagNo, oDAL.GetTagNoByItemIdForOrder(oli.Stock.ItemName.ItemId), "TagNo", "StockId");

                        for (int i = 0; i < this.cbxTagNo.Items.Count; i++)
                        {
                            Stock stk1 = (Stock)this.cbxTagNo.Items[i];
                            if (oli.Stock.TagNo.Equals(stk1.TagNo))
                            {
                                this.cbxTagNo.SelectedIndex = i;
                                break;
                            }
                        }
                    }

                    this.txtItemSize.Text = oli.Stock.ItemSize.ToString();

                
                    if (oli.Stock.TotalMaking == 0)
                        this.txtTotalMaking.Text = "";
                    else
                        this.txtTotalMaking.Text = ((decimal)oli.Stock.TotalMaking).ToString("0");
                    if (oli.Stock.TotalLaker == 0)
                        this.txtTotalLacker.Text = "";
                    else
                        this.txtTotalLacker.Text = ((decimal)oli.Stock.TotalLaker).ToString("0");


                    for (int i = 0; i < this.cbxKarrat.Items.Count; i++)
                    {
                        string str1 = (string)this.cbxKarrat.Items[i];
                        if (oli.Stock.Karrat.Equals(str1))
                        {
                            this.cbxKarrat.SelectedIndex = i;
                            break;
                        }
                        else
                            this.cbxKarrat.SelectedIndex = 2;
                    }
                    if (Main.City == "Islamabad")
                        this.txtGoldRate.Text = ((decimal)(oli.Stock.RatePerGm * Formulas.WeightInGm)).ToString("0");
                    else
                        this.txtGoldRate.Text = ((decimal)oli.Stock.RatePerGm).ToString("0");
                    if (oli.Stock.StoneCharges == 0)
                        this.txtStonePrice.Text = "";
                    else
                        this.txtStonePrice.Text = ((decimal)oli.Stock.StoneCharges).ToString("0");

                    if (oli.Stock.Qty == 0)
                        this.txtQty.Text = "";
                    else
                        this.txtQty.Text = oli.Stock.Qty.ToString();

                    this.dtpOrderDate.Value = Convert.ToDateTime(oli.Stock.StockDate);

                    this.txtDesign.Text = oli.DesignNo;

                    FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
                    this.cbxWorker.SelectedValue = oli.Stock.WorkerName.ID;

                    if (oli.Stock.Description == null)
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = oli.Stock.Description;

                    if (oli.DesignNo == null)
                        this.txtDesign.Text = "";
                    else
                        this.txtDesign.Text = oli.DesignNo;

                    this.txtWeight.Text = ((decimal)oli.Stock.NetWeight).ToString("0.000");
                    if (Main.City == "Islamabad")
                        this.txtGoldRate.Text = ((decimal)(oli.Stock.RatePerGm * Formulas.WeightInGm)).ToString("0");
                    else
                        this.txtGoldRate.Text = oli.Stock.RatePerGm.ToString();
                    this.txtTotalPrice.Text = ((decimal)oli.Stock.TotalPrice).ToString("0");

                    if (oli.Stock.WastePercent == 0)
                        this.txtWasteIn.Text = "";
                    else
                        this.txtWasteIn.Text = ((decimal)oli.Stock.WastePercent).ToString("0.0");

                    if (oli.Stock.WasteInGm == 0)
                        this.txtWasteage.Text = "";
                    else
                        this.txtWasteage.Text = ((decimal)oli.Stock.WasteInGm).ToString("0.000");

                    this.txtTotalWeight.Text = ((decimal)oli.Stock.TotalWeight).ToString("0.000");

                    if (oli.Stock.Description == "")
                        this.txtDescription.Text = "";
                    else
                        this.txtDescription.Text = oli.Stock.Description;

                    if (oli.Stock.MakingPerGm == 0)
                        this.txtMakingPerGm.Text = "";
                    else
                        this.txtMakingPerGm.Text = ((decimal)oli.Stock.MakingPerGm).ToString("0");

                    if (oli.Stock.TotalMaking == 0)
                        this.txtTotalMaking.Text = "";
                    else
                        this.txtTotalMaking.Text = ((decimal)oli.Stock.TotalMaking).ToString("0");

                    if (oli.Stock.TotalLaker == 0)
                        this.txtTotalLacker.Text = "";
                    else
                        this.txtTotalLacker.Text = ((decimal)oli.Stock.TotalLaker).ToString("0");
                    this.txtTotalPrice.Text = ((decimal)oli.Stock.TotalPrice).ToString("0");
                    if (oli.Stock.ImageMemory == null)
                    {
                        this.pbxPicture.Image = null;
                        this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                    }
                    else
                    {
                        MemoryStream mst = new MemoryStream(oli.Stock.ImageMemory);
                        this.pbxPicture.Image = Image.FromStream(mst);
                    }
                    this.txtGrossWeight.Text = this.txtWeight.Text;
                    if (oli.Stock.StoneList == null)
                        return;
                    else
                    {
                        this.dgvStoneInformation.AutoGenerateColumns = false;
                        int count = oli.Stock.StoneList.Count;
                        this.dgvStoneInformation.Rows.Add(count);
                        for (int i = 0; i < oli.Stock.StoneList.Count; i++)
                        {
                            this.dgvStoneInformation.Rows[i].Cells[1].Value = oli.Stock.StoneList[i].StoneTypeId;
                            this.dgvStoneInformation.Rows[i].Cells[2].Value = oli.Stock.StoneList[i].StoneId;

                            if (oli.Stock.StoneList[i].StoneWeight == 0)
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].StoneWeight), 3);

                            if (oli.Stock.StoneList[i].Qty == 0)
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[4].Value = Convert.ToInt32(oli.Stock.StoneList[i].Qty);

                            if (oli.Stock.StoneList[i].Rate == 0)
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].Rate), 1);

                            if (oli.Stock.StoneList[i].Price == 0)
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = string.Empty;
                            else
                                this.dgvStoneInformation.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(oli.Stock.StoneList[i].Price), 0);

                            if (oli.Stock.StoneList[i].ColorName != null)
                            {
                                for (int j = 0; j < this.Column10.Items.Count; j++)
                                {
                                    StoneColor stcl = (StoneColor)this.Column10.Items[j];
                                    if (oli.Stock.StoneList[i].ColorName.ColorName.Equals(stcl.ColorName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[7].Value = Convert.ToInt32(stcl.ColorId);
                                }
                            }
                            if (oli.Stock.StoneList[i].CutName != null)
                            {
                                for (int j = 0; j < this.Column11.Items.Count; j++)
                                {
                                    StoneCut stcl = (StoneCut)this.Column11.Items[j];
                                    if (oli.Stock.StoneList[i].CutName.CutName.Equals(stcl.CutName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[8].Value = Convert.ToInt32(stcl.CutId);
                                }
                            }
                            if (oli.Stock.StoneList[i].ClearityName != null)
                            {
                                for (int j = 0; j < this.Column12.Items.Count; j++)
                                {
                                    StoneClearity stcl = (StoneClearity)this.Column12.Items[j];
                                    if (oli.Stock.StoneList[i].ClearityName.ClearityName.Equals(stcl.ClearityName.ToString()))
                                        this.dgvStoneInformation.Rows[i].Cells[9].Value = Convert.ToInt32(stcl.ClearityId);
                                }
                            }
                        }
                    }
                    this.txtGrossWeight.Text = Math.Round((Convert.ToDecimal(this.txtWeight.Text) + upDateTextBox()), 3).ToString();
                }
            }
        }

        private void dgvBookedItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;
            else
            {
                if (eFlag == true)
                {
                    string strTagNo = this.dgvBookedItem.Rows[e.RowIndex].Cells[0].Value.ToString();
    
                    int id = Convert.ToInt32(this.dgvBookedItem.Rows[e.RowIndex].Cells[3].Value.ToString());
                    this.dgvBookedItem.Rows.RemoveAt(e.RowIndex);
                    OrderLineItem sl = new OrderLineItem();
                    foreach (OrderLineItem oli in ordEst.OrderLineItem)
                    {
                        if (oli.OItemId == strTagNo)
                        {
                            this.ShowRecordByItemId(strTagNo);
                            MemoryStream mst;
                            if (ordEst.Pic != null)
                            {
                                mst = new MemoryStream(ordEst.Pic);
                                this.pbxPicture.Image = Image.FromStream(mst);
                            }
                            sl = oli;
                        }
                    }
                    if (sl != null)
                    {
                        ordEst.RemoveLineItems(sl);
                        lstOItemId.Remove(sl.OItemId);
                        lstOItemId1.Add(sl.OItemId);
                    }
                }
                else
                {
                    z = e.RowIndex;
                    strg = "";
                    strg = this.dgvBookedItem.Rows[e.RowIndex].Cells[0].Value.ToString();
                    this.dgvBookedItem.Rows.RemoveAt(e.RowIndex);
                    OrderLineItem sl = new OrderLineItem();
                    foreach (OrderLineItem oli in ordEst.OrderLineItem)
                    {
                        if (oli.OItemId == strg)
                        {
                            this.ShowAllRecordByItemId(strg);
                            sl = oli;
                        }
                    }
                    if (sl != null)
                    {
                        ordEst.RemoveLineItems(sl);
                        lstOItemId.Remove(sl.OItemId);
                        lstOItemId1.Add(sl.OItemId);
                    }
                }
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
                this.btnAddItem.Select();
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

        private void txtPGWeight_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0, b = 0, q = 0;
            if (this.txtPGWeight.Text == "")
                a = 0;
            else
                a = FormControls.GetDecimalValue(this.txtPGWeight, 3);
            if (this.txtPGRate.Text == "")
                b = 0;
            else
                b = FormControls.GetDecimalValue(this.txtPGRate, 1);
            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
                 q = a * (b/Formulas.WeightInGm);
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                 q = a * b;            
            this.txtPureGold.Text = q.ToString("0");
        }

        private void txtQty_Leave(object sender, EventArgs e)
        {
            if (txtQty.Text == "")
                txtQty.Text = "1";
        }

        private void dgvStoneInformation_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                try
                {
                    string txtvalue = Convert.ToString(dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value);
                    decimal val = Convert.ToDecimal(txtvalue);
                    string s = val.ToString("N3");
                    dgvStoneInformation.Rows[e.RowIndex].Cells["Column2"].Value = s.ToString();
                }
                catch { }
            }
            if (e.ColumnIndex == 1 && dgvStoneInformation.CurrentRow != null)
            { }
        }

        private void dgvStoneInformation_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 2 && this.dgvStoneInformation.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
                e.Cancel = true;
        }

        public void AssignRights(Form frm, string frmRights)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                    this.btnSave.Enabled = false;
                else if (a[i] == "Edit")
                    this.btnEdit.Enabled = false;
            }
        }

        private void cbxCustomerName_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtAmountCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtAmountCreditCard_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtDeductRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtReceiveables_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtBankDeductRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtDepositeInBank_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPGWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPGRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPGPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtWeightUsedGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPureWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtRateUsedGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtPriceUseGold_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void rbtPoundPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPoundPasa.Checked)
            {
                grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpOrderDate.Value));
                this.txtGoldRate.Text = grs.PoundPasa.ToString("0.0");
                this.txtGoldPrice.Text = Math.Round(((FormControls.GetDecimalValue(this.txtTotalWeight, 3) * (FormControls.GetDecimalValue(this.txtGoldRate, 1) / Formulas.WeightInGm))), 0).ToString("0");
            }
        }

        private void rbtSonaPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSonaPasa.Checked)
            {
                grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpOrderDate.Value));
                this.txtGoldRate.Text = grs.SonaPasa.ToString("0.0");
                this.txtGoldPrice.Text = Math.Round(((FormControls.GetDecimalValue(this.txtTotalWeight, 3) * (FormControls.GetDecimalValue(this.txtGoldRate, 1) / Formulas.WeightInGm))), 0).ToString("0");
            }
        }

        private void btnRowRemove_Click(object sender, EventArgs e)
        {
            this.Hide();
            StoneDetail frm = new StoneDetail();
            frm.ShowDialog();
            this.Show();
        }

        private void dgvStoneInformation_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            for (i = 2; i < dgvStoneInformation.ColumnCount; i++)
            {
                if (Convert.ToString(this.dgvStoneInformation.CurrentRow.Cells[i].Value) != string.Empty)
                    return;
                else if (i == dgvStoneInformation.ColumnCount) 
                    dgvStoneInformation.Rows.Remove(dgvStoneInformation.CurrentRow);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (Main.City == "Islamabad")
                {
                    this.cbxKarrat.SelectedIndexChanged -= new EventHandler(cbxKarrat_SelectedIndexChanged);
                    this.pnlPasaRate.Visible = true;
                    grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpOrderDate.Value));
                    if (this.rbtPoundPasa.Checked == true)
                        this.txtGoldRate.Text = grs.PoundPasa.ToString("0.0");
                    else if (this.rbtSonaPasa.Checked == true)
                        this.txtGoldRate.Text = grs.SonaPasa.ToString("0.0");
                }
                else
                {
                    grs.PoundPasa = 0;
                    grs.SonaPasa = 0;
                    this.cbxKarrat.SelectedIndexChanged += new EventHandler(cbxKarrat_SelectedIndexChanged);
                    this.cbxKarrat.SelectedIndex = 3;
                    string r = (string)this.cbxKarrat.SelectedItem;
                    s = grDAL.GetRateByKarat(r, Convert.ToDateTime(dtpOrderDate.Value));
                    this.txtGoldRate.Text = s.ToString("0.0");
                }
            }
            catch
            { }
        }

        private void chkFixRate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkFixRate.Checked)
                this.txtFixRate.Enabled = true;
            else
            {
                this.txtFixRate.Text = "";
                this.txtFixRate.Enabled = false;
            }
        }

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.tabControl1.SelectedIndex == 1)
            {
                if (this.dgvBookedItem.Rows.Count == 0 && this.tabControl1.SelectedTab == tabPage2)
                    this.tabControl1.SelectedTab = tabPage1;
            }
            if (this.dgvBookedItem.Rows.Count > 0 && this.tabControl1.SelectedTab == tabPage2)
            {
                this.tabControl1.SelectedTab = tabPage1;
                MessageBox.Show("Press the Button Bill", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
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

        private void cbxCustomerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = (Customer)this.cbxCustomerName.SelectedItem;
            this.lblCustId.Text = cust.ID.ToString();
            this.cbxContactNo.SelectedIndexChanged -= new EventHandler(cbxContactNo_SelectedIndexChanged);
            this.cbxContactNo.SelectedValue = cust.ID;
            this.txtAddress.Text = cust.Address;
        }

        private void cbxContactNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            cust = (Customer)this.cbxContactNo.SelectedItem;
            this.lblCustId.Text = cust.ID.ToString();
            this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
            this.cbxCustomerName.SelectedValue = cust.ID;
            this.txtAddress.Text = cust.Address;
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

        private void txtWasteIn_KeyUp(object sender, KeyEventArgs e)
        {
            frm.GramsOfPercent(FormControls.GetDecimalValue(this.txtWasteIn, 1), FormControls.GetDecimalValue(this.txtWeight, 3), txtWasteage, txtTotalWeight);
            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtWasteage, 3));
            this.lblRtmWWeight.Text = frm.Tola + "T-" + frm.Masha + "M-" + frm.Ratti + "R";

            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtTotalWeight, 3));
            this.lblRtmTWeight.Text = frm.Tola + "T-" + frm.Masha + "M-" + frm.Ratti + "R";
            decimal val = 0, val1 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalWeight, 3);           
            val1 = FormControls.GetDecimalValue(this.txtGoldRate, 1);
            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
            {
                this.txtGoldPrice.Text = (val * (val1 / Formulas.WeightInGm)).ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                this.txtGoldPrice.Text = (val * val1).ToString("0");
            }
        }

        private void txtWasteage_KeyUp(object sender, KeyEventArgs e)
        {
            frm.WasteInPersent(FormControls.GetDecimalValue(this.txtWasteage, 3), FormControls.GetDecimalValue(this.txtWeight, 3), txtWasteIn, txtTotalWeight);
            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtWasteage, 3));
            this.lblRtmWWeight.Text = frm.Tola + "T-" + frm.Masha + "M-" + frm.Ratti + "R";

            frm.RatiMashaTolaGeneral(FormControls.GetDecimalValue(this.txtTotalWeight, 3));
            this.lblRtmTWeight.Text = frm.Tola + "T-" + frm.Masha + "M-" + frm.Ratti + "R";
            decimal val = 0, val1 = 0;
            val = FormControls.GetDecimalValue(this.txtTotalWeight, 3);           
            val1 = FormControls.GetDecimalValue(this.txtGoldRate, 1);

            if (((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola"))
            {
                this.txtGoldPrice.Text = (val * (val1 / Formulas.WeightInGm)).ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                this.txtGoldPrice.Text = (val * val1).ToString("0");
            }
            this.txtGrossWeight.Text = this.txtWeight.Text;
        }

        private void txtMakingPerGm_KeyUp(object sender, KeyEventArgs e)
        {
            frm.TotalMaking(FormControls.GetDecimalValue(this.txtMakingPerGm, 1), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtTotalMaking);
        }

        private void txtTotalMaking_KeyUp(object sender, KeyEventArgs e)
        {
            frm.MakingPerGram(FormControls.GetDecimalValue(this.txtTotalMaking, 0), FormControls.GetDecimalValue(this.txtTotalWeight, 3), txtMakingPerGm);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            con.Open();
            trans = con.BeginTransaction();
            if (this.cbxCustomerName.Text == "")
            {
                MessageBox.Show("Must Enter Customer Name", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                trans.Rollback();
                con.Close();
                return;
            }
            else
            {
                try
                {
                    #region savecustomer
                    if (this.cbxCustomerName.Text != "" && cbxCustomerName.SelectedIndex == -1)
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
                            cust.Date = Convert.ToDateTime(this.dtpOrderDate.Value);
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
                    ordEst.SaleNo = 0;
                    ordEst.ODate = Convert.ToDateTime(this.dtpOrderDate.Value);
                    if (this.dtpDeliveryDate.Value <= this.dtpOrderDate.Value)
                    {
                        MessageBox.Show("Delivery date must be greater than order date", Messages.Header);
                        trans.Rollback();
                        con.Close();
                        return;
                    }
                    else
                        ordEst.DDate = Convert.ToDateTime(this.dtpDeliveryDate.Value);
                    ordEst.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                    ordEst.CustName = new Customer();
                    ordEst.CustName.ID = Convert.ToInt32(this.lblCustId.Text);
                    ordEst.CustName.Name = cbxCustomerName.Text;
                    ordEst.CustName.ContactNo = cbxContactNo.Text;
                    ordEst.CustName.Address = txtAddress.Text;
                    ordEst.CusAccountNo = cust.AccountCode;

                    if (this.txtBillBookNo.Text == "")
                        ordEst.BillBookNo = "0";
                    else
                        ordEst.BillBookNo = txtBillBookNo.Text;

                    ordEst.TotalPrice = FormControls.GetDecimalValue(txtGTotalPrice, 0);
                    ordEst.BillDiscout = FormControls.GetDecimalValue(txtDiscount, 0);
                    ordEst.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);
                    ordEst.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceiveAmount, 0);
                    ordEst.Balance = FormControls.GetDecimalValue(txtBalance, 0);
                    ordEst.OFixRate = FormControls.GetDecimalValue(this.txtFixRate, 1);
                    ordEst.CashReceive = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                    ordEst.CashPayment = FormControls.GetDecimalValue(this.txtCashPayment, 0);
                    ordEst.CreditCard = FormControls.GetDecimalValue(this.txtCreditCard, 0);
                    ordEst.CheckCash = FormControls.GetDecimalValue(this.txtCheque, 0);
                    ordEst.UsedGoldCharges = FormControls.GetDecimalValue(this.txtUsedGold, 0);
                    ordEst.PureGoldCharges = FormControls.GetDecimalValue(this.txtPureGold, 0);
                    
                    ordEst.Status = "Order Estimate";
                    oDAL.AddOrder(ordEst, con, trans);
                    foreach (OrderLineItem oli in ordEst.OrderLineItem)
                    {
                        oDAL.AddOrderPic(oli.Stock.ImageMemory, oli.OItemId, Convert.ToInt32(ordEst.OrderNo));
                    }
                    ChildAccount cha;
                    if (this.txtCashReceive.Text != "" && this.txtCashReceive.Text != "0")
                    {
                        #region Cash voucher
                        //cash in hand entry
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        pv.Description = "Cash Received From Order No." + this.txtOrderNo.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //Customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = 0;
                        sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash Received";
                        sp1.PTime = "Order Form";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = this.dtpOrderDate.Value;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(lblCustId.Text);
                        if (Main.City == "Islamabad")
                        {
                            grs = grDAL.GetPasaRates(Convert.ToDateTime(DateTime.Now));
                            sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtCashReceive, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                        }
                        else
                            sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtCashReceive, 0) / Convert.ToDecimal(grDAL.GetRateByKarrat24("24", DateTime.Now))), 3);
                        payDAL.AddSalePayment(sp1, con, trans);

                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        #endregion
                    }
                    if (this.txtCheque.Text != "" && this.txtCheque.Text != "0")
                    {
                        #region Cheque voucher
                        foreach (Cheques chq in ListOfCheques)
                        {
                            pv = new Voucher();
                            cha = acDAL.GetChildByName(chq.BankAccount, con, trans);
                            pv.AccountCode = acDAL.GetChildByCode(chq.DepositInAccount.AccountCode.ChildCode, con, trans);
                            //pv.AccountCode = chq.DepositInAccount .AccountCode;
                            pv.Dr = chq.Amount; //Convert.ToDecimal(s.CheckCash);
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = ordEst.OrderNo;
                            pv.SNO = 0;
                            pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                            //pv.VNO = chq.VNO;
                            pv.Description = "Cheque Recieved From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            //customer entry
                            custv = new Voucher();
                            custv.AccountCode = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                            custv.Cr = pv.Dr; //(decimal)s.CheckCash;
                            custv.Dr = 0;
                            custv.DDate = (DateTime)ordEst.ODate;
                            custv.OrderNo = ordEst.OrderNo;
                            custv.SNO = 0;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);
                            chq.SNO = 0;
                            chq.ONO = Convert.ToInt32(txtOrderNo.Text);
                            chq.VNO = pv.VNO;
                            chq.CustAccountCode = ordEst.CusAccountNo;
                            saleDAL.AddChecques(chq, con, trans);
                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = 0;
                            sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                            sp1.VNo = chq.VNO;
                            sp1.PMode = "Cheque";
                            sp1.PTime = "Order Form";
                            sp1.Receiveable = 0;
                            sp1.DRate = 0;
                            sp1.DDate = DateTime.Today;
                            sp1.BDrate = 0;
                            sp1.BankName = chq.BankName.BankName;
                            sp1.Amount = chq.Amount;
                            sp1.Description = "Cheque Recieved From Order No." + ordEst.OrderNo.ToString();
                            sp1.DAccountCode = chq.DepositInAccount.AccountCode.ChildCode.ToString();
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            sp1.CustId = Convert.ToInt32(lblCustId.Text);
                            payDAL.AddSalePayment(sp1, con, trans);
                        }
                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCheque, 0);
                        #endregion
                    }
                    if (this.txtCreditCard.Text != "" && this.txtCreditCard.Text != "0")
                    {
                        #region CreditCard Voucher
                        foreach (CreditCard cc in this.ListOfCreditCard)
                        {
                            pv = new Voucher();
                            cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                            pv.AccountCode = cha;
                            pv.Dr = cc.Amount;
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                            string str = pv.VNO;
                            pv.Description = "Cash Recieved By Credit Card From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            //customer account voucher entry
                            ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                            custv = new Voucher();
                            ChildAccount child = new ChildAccount();
                            child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                            custv.AccountCode = child;
                            custv.Cr = pv.Dr;
                            custv.Dr = 0;
                            custv.DDate = (DateTime)ordEst.ODate;
                            custv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            custv.SNO = 0;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);
                            //for extra money;
                            pv = new Voucher();
                            cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                            pv.AccountCode = cha;
                            pv.Dr = ExtraMoney;
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = str;
                            pv.Description = "Credit Card Extra Mony From Order No." + ordEst.OrderNo.ToString();
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
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = str;
                            pv.Description = "Credit Card Extra Mony From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            cc.VNO = str;
                            cc.SNO = 0;
                            cc.ONO = Convert.ToInt32(ordEst.OrderNo);
                            saleDAL.AddCreditCards(cc, con, trans);
                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = 0;
                            sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                            sp1.VNo = str;
                            sp1.PMode = "Credit Card";
                            sp1.PTime = "Order Form";
                            sp1.Receiveable = (decimal)cc.SwapAmount;
                            sp1.DRate = cc.DeductRate;
                            sp1.DDate = DateTime.Today;
                            sp1.BDrate = cc.BankDeductRate;
                            sp1.BankName = cc.BankName.BankName;
                            sp1.Amount = cc.Amount; //(decimal)s.CreditCard;
                            sp1.Description = "Cash Recieved By Credit Card From Order No." + ordEst.OrderNo.ToString();
                            sp1.DAccountCode = "";
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            payDAL.AddSalePayment(sp1, con, trans);
                        }
                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCreditCard, 0);
                        #endregion
                    }
                    if (this.txtPureGold.Text != "" && this.txtPureGold.Text != "0")
                    {
                        #region Pure Gold Voucher
                        //gold accunt entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(txtPureGold, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        pv.Description = "Pure Gold Purchased From Order No." + pv.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(txtPureGold, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        foreach (Gold gld in ListOfPureGold)
                        {
                            gld.ONO = (int)ordEst.OrderNo;
                            gld.SNO = 0;
                            gld.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                            gld.Description = pv.Description;
                            gld.VNO = pv.VNO;
                            if (this.cbxSaleMan.SelectedIndex > -1)
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = (int)this.cbxSaleMan.SelectedValue;
                            }
                            else
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = 0;
                            }
                            gld.PTime = "Order Form";
                            gld.PTime = "Order Recieved";
                            saleDAL.AddGoldDetail(gld, con, trans);
                        }
                        #endregion
                    }
                    if (this.txtUsedGold.Text != "" && this.txtUsedGold.Text != "0")
                    {
                        #region Used Gold Voucher
                        //gold account entry
                        pv = new Voucher();

                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(txtUsedGold, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        pv.Description = "Used Gold Purchased From Order No." + pv.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(txtUsedGold, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = pv.OrderNo;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        foreach (Gold gld in ListOfUsedGold)
                        {
                            gld.ONO = (int)ordEst.OrderNo;
                            gld.SNO = 0;
                            gld.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                            gld.Description = pv.Description;
                            gld.VNO = pv.VNO;
                            if (this.cbxSaleMan.SelectedIndex > -1)
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = (int)this.cbxSaleMan.SelectedValue;
                            }
                            else
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = 0;
                            }
                            gld.PTime = "Order Form";
                            gld.PMode = "Order Recieved";
                            saleDAL.AddGoldDetail(gld, con, trans);
                        }
                        #endregion
                    }
                    if (this.txtCashPayment.Text != "" && this.txtCashPayment.Text != "0")
                    {
                        #region Payment voucher
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
                        pv.Dr = 0;
                        pv.Cr = FormControls.GetDecimalValue(txtCashPayment, 0);
                        pv.DDate = (DateTime)ordEst.ODate;
                        pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        pv.Description = "Cash Paid From Order No." + ordEst.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv = new Voucher();
                        custv.AccountCode = child;
                        custv.Cr = 0;
                        custv.Dr = pv.Cr;
                        custv.DDate = (DateTime)ordEst.ODate;
                        custv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        //sale payment entry
                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = 0;
                        sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash Payment";
                        sp1.PTime = "Order Form";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = DateTime.Today;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)pv.Cr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                        #endregion
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
                        MessageBox.Show(Messages.Saved, Messages.Header);
                        this.RefreshPage1();
                        this.RefreshPage2();
                        this.txtId.Text = "";
                        l = 1;
                        this.dgvBookedItem.Rows.Clear();
                        this.txtOrderNo.Text = (oDAL.GetMaxOrderNo() + 1).ToString();
                        this.txtId.Text = FormControls.GetIntValue(this.txtOrderNo) + "-" + l;
                        this.txtQty.Text = "1";
                        string selectQuery = "";
                        selectQuery = "{OrderEstimateBill.OrderNo}=" + ordEst.OrderNo;
                        ReportViewer frm = new ReportViewer();
                        frm.isPage = 2;
                        frm.rpt = 1;
                        frm.selectQuery = selectQuery;
                        frm.ShowDialog();
                        selectQuery = "";
                        selectQuery = "{sp_WorkerOrder.OrderNo}=" + ordEst.OrderNo;
                        frm = new ReportViewer();
                        frm.isPage = 2;
                        frm.rpt = 9;
                        frm.selectQuery = selectQuery;
                        frm.ShowDialog();
                        selectQuery = "";
                        selectQuery = "{sp_JobCard.OrderNo}=" + ordEst.OrderNo;
                        frm = new ReportViewer();
                        frm.isPage = 2;
                        frm.rpt = 4;
                        frm.selectQuery = selectQuery;
                        frm.ShowDialog();
                        ordEst = new OrderEstimat();
                        this.Dispose();
                        ManageOrder frm2 = new ManageOrder();
                        frm2.ShowDialog();
                    }
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "&Edit")
            {
                EditOrdderNo ono = new EditOrdderNo();
                ono.Text = "OrderNo";
                ono.LabelText = "Enter Order No.";
                ono.Msg = "Enter Order No. First";
                ono.ShowDialog();

                if ((int)ono.EditNum == 0)
                {
                    MessageBox.Show("There Is No Order No. ", Messages.Header);
                    return;
                }
                else
                {
                    this.ShowOrder((int)ono.EditNum);
                    if (this.dgvBookedItem.Rows.Count != 0)
                    {
                        l = this.dgvBookedItem.Rows.Count + 1;
                        this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                        this.tabControl1.SelectedTab = tabPage1;
                        this.btnEdit.Text = "&Update";
                        this.btnSave.Enabled = false;
                        ListOfCreditCard = payDAL.GetCCardListByOrderNo((int)ono.EditNum);
                        if (ListOfCreditCard == null)
                            ListOfCreditCard = new List<CreditCard>();
                        ListOfCheques = payDAL.GetChequeListByOrderNo((int)ono.EditNum);
                        if (ListOfCheques == null)
                            ListOfCheques = new List<Cheques>();
                        ListOfUsedGold = payDAL.GetUGoldListByOrderNo((int)ono.EditNum);
                        if (ListOfUsedGold == null)
                            ListOfUsedGold = new List<Gold>();
                        ListOfPureGold = payDAL.GetPGoldListByOrderNo((int)ono.EditNum);
                        if (ListOfPureGold == null)
                            ListOfPureGold = new List<Gold>();
                        eFlag = true;
                        ordId = 0;
                        return;
                    }
                }
            }
            if (this.btnEdit.Text == "&Update")
            {
                try
                {
                    if (this.dgvBookedItem.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please add Item to update", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ordEst.SaleNo = 0;
                    ordEst.ODate = Convert.ToDateTime(this.dtpOrderDate.Value);
                    ordEst.DDate = Convert.ToDateTime(this.dtpDeliveryDate.Value);
                    ordEst.OrderNo = Convert.ToInt32(this.txtOrderNo.Text);
                    ordEst.CusAccountNo = cust.AccountCode;
                    if (this.cbxSaleMan.SelectedIndex > -1)
                    {
                        ordEst.SalesMan = new SaleMan();
                        ordEst.SalesMan.ID = (int)this.cbxSaleMan.SelectedValue;
                    }
                    else
                    {
                        ordEst.SalesMan = new SaleMan();
                        ordEst.SalesMan.ID = 0;
                    }
                    if (this.txtAmount.Text == "")
                        ordEst.Amount = 0;
                    else
                        ordEst.Amount = FormControls.GetDecimalValue(txtAmount, 0);

                    if (this.txtBillBookNo.Text == "")
                        ordEst.BillBookNo = "0";
                    else
                        ordEst.BillBookNo = txtBillBookNo.Text;

                    ordEst.TotalPrice = FormControls.GetDecimalValue(txtGTotalPrice, 0);
                    ordEst.BillDiscout = FormControls.GetDecimalValue(txtDiscount, 0);
                    ordEst.NetBill = FormControls.GetDecimalValue(txtNetAmount, 0);
                    ordEst.CashReceive = FormControls.GetDecimalValue(txtCashReceive, 0);
                    ordEst.CashPayment = FormControls.GetDecimalValue(txtCashPayment, 0);
                    ordEst.CheckCash = FormControls.GetDecimalValue(txtCheque, 0);
                    ordEst.CreditCard = FormControls.GetDecimalValue(txtCreditCard, 0);
                    ordEst.UsedGoldCharges = FormControls.GetDecimalValue(txtUsedGold, 0);
                    ordEst.PureGoldCharges = FormControls.GetDecimalValue(txtPureGold, 0);
                    ordEst.TReceivedAmount = FormControls.GetDecimalValue(txtTotalReceiveAmount, 0);
                    ordEst.Balance = FormControls.GetDecimalValue(txtBalance, 0);
                    if (this.chkFixRate.Checked && this.txtFixRate.Text != "")
                        ordEst.OFixRate = FormControls.GetDecimalValue(this.txtFixRate, 1);
                    else
                        ordEst.OFixRate = 0;
                    ordEst.Status = "Order Estimate";

                    ChildAccount cha = new ChildAccount();
                    con.Open();
                    trans = con.BeginTransaction();

                    SqlCommand cmd = new SqlCommand(@"delete from orderestimate where orderno=" + Convert.ToInt32(ordEst.OrderNo), con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = trans;
                    cmd.ExecuteNonQuery();
                    foreach (string str in lstOItemId1)
                    { 
                        if (str == "")
                        { }
                        else
                        {             
                            SqlCommand cmd2 = new SqlCommand(@"update Stock set OrderNo=0,OItemId=Null,ItemFor='Sale' where OItemId='" + str + "'", con);
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Transaction = trans;
                            cmd2.ExecuteNonQuery();
                        }
                    }
                    SqlCommand cmd1 = new SqlCommand(@"delete from OrderMaster where orderno=" + Convert.ToInt32(ordEst.OrderNo), con);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Transaction = trans;
                    cmd1.ExecuteNonQuery();
                    SqlCommand cmd11 = new SqlCommand(@"delete from StandardPicsDB.dbo.JewlPictures where orderno = " + Convert.ToInt32(ordEst.OrderNo), con);
                    cmd11.CommandType = CommandType.Text;
                    cmd11.Transaction = trans;
                    cmd11.ExecuteNonQuery();

                    List<string> voucherNo = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'CRV%' and Description Like 'Cash Received From Order No%' and OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo != null)
                    {
                        for (int i = 0; i < voucherNo.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo[i], con, trans);
                            payDAL.DeleteFromSalePayment(Convert.ToInt32(ordEst.OrderNo), voucherNo[i], con, trans);
                        }
                    }
                    List<string> voucherNo5 = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'CPV%' and Description Like 'Cash Paid From Order No%' and OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo5 != null)
                    {
                        for (int i = 0; i < voucherNo5.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo5[i], con, trans);
                            payDAL.DeleteFromSalePayment(Convert.ToInt32(ordEst.OrderNo), voucherNo5[i], con, trans);
                        }
                    }
                    List<string> voucherNo1 = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'CHV%' and Description Like 'Cheque Recieved From Order No%' and OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo1 != null)
                    {
                        for (int i = 0; i < voucherNo1.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo1[i], con, trans);
                            payDAL.DeleteFromSalePayment(Convert.ToInt32(ordEst.OrderNo), voucherNo1[i], con, trans);
                            vDAL.DeleteCheque(voucherNo1[i], con, trans);
                        }
                    }
                    List<string> voucherNo2 = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'CCV%' and Description Like 'Cash Recieved By Credit Card From Order No%' and OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo2 != null)
                    {
                        for (int i = 0; i < voucherNo2.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo2[i], con, trans);
                            payDAL.DeleteFromSalePayment(Convert.ToInt32(ordEst.OrderNo), voucherNo2[i], con, trans);
                            vDAL.DeleteCreditCard(voucherNo2[i], con, trans);
                        }
                    }
                    List<string> voucherNo3 = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'GPV%' and Description Like 'Pure Gold Purchased From Order No%' and  OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo3 != null)
                    {
                        for (int i = 0; i < voucherNo3.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo3[i], con, trans);
                            saleDAL.DeleteFromGoldDetail(Convert.ToInt32(ordEst.OrderNo), voucherNo3[i], con, trans);
                        }
                    }
                    List<string> voucherNo4 = vDAL.GetVoucherGeneral("select distinct vno from Vouchers where vno like 'GPV%' and Description Like 'Used Gold Purchased From Order No%' and OrderNo=" + ordEst.OrderNo, con, trans);
                    if (voucherNo4 != null)
                    {
                        for (int i = 0; i < voucherNo4.Count; i++)
                        {
                            vDAL.DeleteVoucher(voucherNo4[i], con, trans);
                            saleDAL.DeleteFromGoldDetail(Convert.ToInt32(ordEst.OrderNo), voucherNo4[i], con, trans);
                        }
                    }
                    cha = new ChildAccount();
                    if (this.txtCashReceive.Text != "" && this.txtCashReceive.Text != "0")
                    {
                        #region Cash voucher
                        //cash in hand entry
                        pv = new Voucher();
                        cha = new ChildAccount();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        pv.Description = "Cash Received From Order No." + this.txtOrderNo.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //Customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = 0;
                        sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash Received";
                        sp1.PTime = "Order Form";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = this.dtpOrderDate.Value;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        sp1.CustId = Convert.ToInt32(lblCustId.Text);
                        if (Main.City == "Islamabad")
                        {
                            grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpOrderDate.Value));
                            sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtCashReceive, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                        }
                        else
                            sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtCashReceive, 0) / Convert.ToDecimal(grDAL.GetRateByKarrat24("24", this.dtpOrderDate.Value))), 3);
                        payDAL.AddSalePayment(sp1, con, trans);

                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCashReceive, 0);
                        #endregion
                    }
                    if (this.txtCheque.Text != "" && this.txtCheque.Text != "0")
                    {
                        #region Cheque voucher
                        foreach (Cheques chq in ListOfCheques)
                        {
                            pv = new Voucher();
                            cha = acDAL.GetChildByName(chq.BankAccount, con, trans);
                            pv.AccountCode = acDAL.GetChildByCode(chq.DepositInAccount.AccountCode.ChildCode, con, trans);
                            //pv.AccountCode = chq.DepositInAccount .AccountCode;
                            pv.Dr = chq.Amount; //Convert.ToDecimal(s.CheckCash);
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = ordEst.OrderNo;
                            pv.SNO = 0;
                            pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                            //pv.VNO = chq.VNO;
                            pv.Description = "Cheque Recieved From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            //customer entry
                            custv = new Voucher();
                            custv.AccountCode = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                            custv.Cr = pv.Dr; //(decimal)s.CheckCash;
                            custv.Dr = 0;
                            custv.DDate = (DateTime)ordEst.ODate;
                            custv.OrderNo = ordEst.OrderNo;
                            custv.SNO = 0;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);
                            chq.SNO = 0;
                            chq.ONO = Convert.ToInt32(txtOrderNo.Text);
                            chq.VNO = pv.VNO;
                            chq.Status = "ok";
                            chq.CustAccountCode = ordEst.CusAccountNo;
                            saleDAL.AddChecques(chq, con, trans);
                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = 0;
                            sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                            sp1.VNo = chq.VNO;
                            sp1.PMode = "Cheque";
                            sp1.PTime = "Order Form";
                            sp1.Receiveable = 0;
                            sp1.DRate = 0;
                            sp1.DDate = DateTime.Today;
                            sp1.BDrate = 0;
                            sp1.BankName = chq.BankName.BankName;
                            sp1.Amount = chq.Amount;
                            sp1.Description = "Cheque Recieved From Order No." + ordEst.OrderNo.ToString();
                            sp1.DAccountCode = chq.DepositInAccount.AccountCode.ChildCode.ToString();
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            sp1.CustId = Convert.ToInt32(lblCustId.Text);
                            payDAL.AddSalePayment(sp1, con, trans);
                        }
                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCheque, 0);
                        #endregion
                    }
                    if (this.txtCreditCard.Text != "" && this.txtCreditCard.Text != "0")
                    {
                        #region CreditCard Voucher
                        foreach (CreditCard cc in this.ListOfCreditCard)
                        {
                            pv = new Voucher();
                            cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                            pv.AccountCode = cha;
                            pv.Dr = cc.Amount;
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                            string str = pv.VNO;
                            pv.Description = "Cash Recieved By Credit Card From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            //customer account voucher entry
                            ExtraMoney = cc.AmountDepositeBank - cc.Amount;
                            custv = new Voucher();
                            ChildAccount child = new ChildAccount();
                            child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                            custv.AccountCode = child;
                            custv.Cr = pv.Dr;
                            custv.Dr = 0;
                            custv.DDate = (DateTime)ordEst.ODate;
                            custv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            custv.SNO = 0;
                            custv.VNO = pv.VNO;
                            custv.Description = pv.Description;
                            vDAL.AddVoucher(custv, con, trans);
                            //for extra money;
                            pv = new Voucher();
                            cha = acDAL.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode, con, trans);
                            pv.AccountCode = cha;
                            pv.Dr = ExtraMoney;
                            pv.Cr = 0;
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = str;
                            pv.Description = "Credit Card Extra Mony From Order No." + ordEst.OrderNo.ToString();
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
                            pv.DDate = (DateTime)ordEst.ODate;
                            pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                            pv.SNO = 0;
                            pv.VNO = str;
                            pv.Description = "Credit Card Extra Mony From Order No." + ordEst.OrderNo.ToString();
                            vDAL.AddVoucher(pv, con, trans);
                            cc.VNO = str;
                            cc.SNO = 0;
                            cc.ONO = Convert.ToInt32(ordEst.OrderNo);
                            cc.Status = "Order";
                            saleDAL.AddCreditCards(cc, con, trans);
                            SalePayment sp1 = new SalePayment();
                            sp1.SaleNo = 0;
                            sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                            sp1.VNo = str;
                            sp1.PMode = "Credit Card";
                            sp1.PTime = "Order Form";
                            sp1.Receiveable = (decimal)cc.SwapAmount;
                            sp1.DRate = cc.DeductRate;
                            sp1.DDate = DateTime.Today;
                            sp1.BDrate = cc.BankDeductRate;
                            sp1.BankName = cc.BankName.BankName;
                            sp1.Amount = cc.Amount; //(decimal)s.CreditCard;
                            sp1.Description = "Cash Recieved By Credit Card From Order No." + ordEst.OrderNo.ToString();
                            sp1.DAccountCode = "";
                            sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                            payDAL.AddSalePayment(sp1, con, trans);
                        }
                        ordEst.CustName.CashBalance -= FormControls.GetDecimalValue(this.txtCreditCard, 0);
                        #endregion
                    }
                    if (this.txtPureGold.Text != "" && this.txtPureGold.Text != "0")
                    {
                        #region Pure Gold Voucher
                        //gold accunt entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(txtPureGold, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        pv.Description = "Pure Gold Purchased From Order No." + pv.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(txtPureGold, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        foreach (Gold gld in ListOfPureGold)
                        {
                            gld.ONO = (int)ordEst.OrderNo;
                            gld.SNO = 0;
                            gld.VNO = custv.VNO;
                            gld.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                            gld.Description = pv.Description;
                            if (this.cbxSaleMan.SelectedIndex > -1)
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = (int)this.cbxSaleMan.SelectedValue;
                            }
                            else
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = 0;
                            }
                            gld.PTime = "Order Form";
                            saleDAL.AddGoldDetail(gld, con, trans);
                        }
                        #endregion
                    }
                    if (this.txtUsedGold.Text != "" && this.txtUsedGold.Text != "0")
                    {
                        #region Used Gold Voucher
                        //gold account entry
                        pv = new Voucher();

                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            string sr = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(txtUsedGold, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpOrderDate.Value;
                        pv.OrderNo = FormControls.GetIntValue(this.txtOrderNo);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        pv.Description = "Used Gold Purchased From Order No." + pv.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(txtUsedGold, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpOrderDate.Value;
                        custv.OrderNo = pv.OrderNo;
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        foreach (Gold gld in ListOfUsedGold)
                        {
                            gld.ONO = (int)ordEst.OrderNo;
                            gld.SNO = 0;
                            gld.VNO = custv.VNO;
                            gld.CustId = Convert.ToInt32(cbxCustomerName.SelectedValue);
                            gld.Description = pv.Description;
                            if (this.cbxSaleMan.SelectedIndex > -1)
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = (int)this.cbxSaleMan.SelectedValue;
                            }
                            else
                            {
                                gld.SaleMan = new SaleMan();
                                gld.SaleMan.ID = 0;
                            }
                            gld.PTime = "Order Form";
                            saleDAL.AddGoldDetail(gld, con, trans);
                        }
                        #endregion
                    }
                    if (txtCashPayment.Text != "" && txtCashPayment.Text != "0")
                    {
                        #region Payment voucher
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
                        pv.Dr = 0;
                        pv.Cr = FormControls.GetDecimalValue(txtCashPayment, 0);
                        pv.DDate = (DateTime)ordEst.ODate;
                        pv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        pv.Description = "Cash Paid From Order No." + ordEst.OrderNo.ToString();
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(ordEst.CusAccountNo, con, trans);
                        custv = new Voucher();
                        custv.AccountCode = child;
                        custv.Cr = 0;
                        custv.Dr = pv.Cr;
                        custv.DDate = (DateTime)ordEst.ODate;
                        custv.OrderNo = Convert.ToInt32(txtOrderNo.Text);
                        custv.SNO = 0;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        //sale payment entry
                        SalePayment sp1 = new SalePayment();
                        sp1.SaleNo = 0;
                        sp1.ONo = Convert.ToInt32(txtOrderNo.Text);
                        sp1.VNo = pv.VNO;
                        sp1.PMode = "Cash Payment";
                        sp1.PTime = "Order Form";
                        sp1.Receiveable = 0;
                        sp1.DRate = 0;
                        sp1.DDate = DateTime.Today;
                        sp1.BDrate = 0;
                        sp1.BankName = "";
                        sp1.Amount = (decimal)pv.Cr;
                        sp1.Description = pv.Description;
                        sp1.DAccountCode = "";
                        sp1.SaleManId = Convert.ToInt32(cbxSaleMan.SelectedValue);
                        payDAL.AddSalePayment(sp1, con, trans);
                        #endregion
                    }
                    foreach (OrderLineItem oli in ordEst.OrderLineItem)
                    {
                        oDAL.DeleteStonesByTagNo((oli.OItemId).ToString(), con, trans);
                    }
                    oDAL.AddOrder(ordEst, con, trans);
                    foreach (OrderLineItem oli in ordEst.OrderLineItem)
                    {
                        oDAL.AddOrderPic(oli.Stock.ImageMemory, oli.OItemId, Convert.ToInt32(ordEst.OrderNo));
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
                        MessageBox.Show(Messages.Updated, Messages.Header);
                        this.btnSave.Enabled = true;
                        ManageOrder ofrm = new ManageOrder();
                        this.Dispose();
                        ofrm.ShowDialog();
                        this.txtOrderNo.Text = (oDAL.GetMaxOrderNo() + 1).ToString();
                        this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                        this.txtQty.Text = "1";
                        this.dgvBookedItem.Rows.Clear();
                        this.btnEdit.Text = "&Edit";
                    }
                }
            }
        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.Dispose();
            ManageOrder frm = new ManageOrder();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            OrderReports frm = new OrderReports();
            FormControls.FadeOut(this);
            frm.ShowDialog();
            FormControls.FadeIn(this);
        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            if (this.btnOrderCancel.Text == "Or&der Cancel")
            {
                EditNo ono = new EditNo();
                ono.ShowDialog();
                if ((int)ono.EditNum == 0)
                {
                    MessageBox.Show("There Is No Order No ", Messages.Header);
                    return;
                }
                else
                {
                    this.ShowOrder((int)ono.EditNum);
                    if (ordEst != null)
                    {
                        SpONo = (int)ono.EditNum;

                        this.tabControl1.SelectedTab = tabPage1;
                        this.btnOrderCancel.Text = "Confirm";
                        this.btnEdit.Enabled = false;
                        this.btnSave.Enabled = false;
                        this.txtCancelDescription.Visible = true;
                        this.lblCancel.Visible = true;
                        return;
                    }
                }
            }
            if (this.btnOrderCancel.Text == "Confirm")
            {
                if (MessageBox.Show("Are you sure to cancel this record:Press Yes to continue", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (SpONo != 0)
                    {
                        foreach (OrderLineItem oli in ordEst.OrderLineItem)
                        {
                            try
                            {
                                con.Open();
                                trans = con.BeginTransaction();
                                oDAL.UpdateOrderStatus("update OrderMaster set Status = 'Cancelled' where OrderNo = " + ordEst.OrderNo + " and SaleNo = 0 and SDate is null", con, trans);
                                oDAL.UpdateOrderStatus("update OrderEstimate set Status = 'Cancelled' where OrderNo = " + ordEst.OrderNo + "and OItemId = '" + oli.OItemId + "'", con, trans);
                                oDAL.AddOrderItems(oli, this.txtCancelDescription.Text, SpONo, con, trans);
                                oDAL.UpdateOrderStatus("update stock set OrderNo = 0, OItemId = null, SaleQty = 0, SaleDate = null, ItemFor = 'Sale', Status = 'Available' Where OrderNo = " + ordEst.OrderNo + "", con, trans);
                                #region Payment voucher
                                decimal cap = saleDAL.GetReturnAmountByONo((int)ordEst.OrderNo, con, trans);
                                string cann = saleDAL.GetCustAccountNoByONo((int)ordEst.OrderNo, con, trans);
                                if (cap > 0)
                                {
                                    //cash in hand entry
                                    pv = new Voucher();
                                    ca = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                                    if (ca == null)
                                    {
                                        string Coode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                                        ca = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                                    }
                                    ca = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                                    pv.AccountCode = ca;
                                    pv.Dr = 0;
                                    pv.Cr = Math.Round(cap, 0);
                                    pv.DDate = DateTime.Now;
                                    pv.OrderNo = (int)ordEst.OrderNo;
                                    pv.SNO = 0;
                                    pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                                    pv.Description = "Cash Return To Order No." + ordEst.OrderNo.ToString();
                                    vDAL.AddVoucher(pv, con, trans);
                                    //customer account entry
                                    custv = new Voucher();
                                    custv.AccountCode = acDAL.GetChildByCode(cann, con, trans);
                                    custv.Cr = 0;
                                    custv.Dr = pv.Cr;
                                    custv.DDate = DateTime.Now;
                                    custv.OrderNo = (int)ordEst.OrderNo;
                                    custv.SNO = 0;
                                    custv.VNO = pv.VNO;
                                    custv.Description = pv.Description;
                                    vDAL.AddVoucher(custv, con, trans);
                                    //sale payment entry
                                    SalePayment sp1 = new SalePayment();
                                    sp1.SaleNo = 0;
                                    sp1.ONo = (int)ordEst.OrderNo;
                                    sp1.VNo = pv.VNO;
                                    sp1.PMode = "Cash Paid";
                                    sp1.PTime = "Order Form";
                                    sp1.Receiveable = 0;
                                    sp1.DRate = 0;
                                    sp1.DDate = DateTime.Today;
                                    sp1.BDrate = 0;
                                    sp1.BankName = "";
                                    sp1.Amount = pv.Cr;
                                    sp1.Description = pv.Description;
                                    sp1.DAccountCode = "";
                                    payDAL.AddSalePayment(sp1, con, trans);
                                }
                                #endregion
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
                                    MessageBox.Show("Order return successfully", Messages.Header);
                                    this.txtCancelDescription.Visible = false;
                                    this.lblCancel.Visible = false;
                                    this.txtCancelDescription.Text = "";
                                    ManageOrder fr = new ManageOrder();
                                    this.Dispose();
                                    fr.ShowDialog();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
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
                this.cbxContactNo.Select();
            }
        }

        private void cbxContactNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cust = (Customer)this.cbxContactNo.SelectedItem;
                this.lblCustId.Text = cust.ID.ToString();
                this.cbxCustomerName.SelectedIndexChanged -= new EventHandler(cbxCustomerName_SelectedIndexChanged);
                this.cbxContactNo.SelectedValue = cust.ID;
                this.txtAddress.Text = cust.Address;
                txtBillBookNo.Select();
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
                    if (this.dgvBookedItem.Rows.Count > 0)
                    {
                        int k = (int)this.cbxGroupItem.SelectedValue;
                        this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);
                        this.cbxTagNo.DisplayMember = "TagNo";
                        this.cbxTagNo.ValueMember = "StockId";
                        this.cbxTagNo.DataSource = getTags(k);
                        this.cbxTagNo.SelectedIndex = -1;
                        this.cbxKarrat.SelectedIndex = 2;
                    }
                    else
                    {
                        if (eFlag == true && this.dgvBookedItem.Rows.Count == 0)
                            l = 1;
                        this.txtId.Text = Convert.ToInt32(this.txtOrderNo.Text) + "-" + l;
                        int k = (int)this.cbxGroupItem.SelectedValue;
                        this.cbxTagNo.SelectedIndexChanged -= new EventHandler(cbxTagNo_SelectedIndexChanged);
                        this.cbxTagNo.DisplayMember = "TagNo";
                        this.cbxTagNo.ValueMember = "StockId";
                        this.cbxTagNo.DataSource = getTags(k);
                        this.cbxTagNo.SelectedIndex = -1;
                        this.cbxKarrat.SelectedIndex = 2;
                    }
                }
                this.chkTagNo.ForeColor = Color.Green;
                this.chkTagNo.Focus();
            }
        }

        private void chkTagNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.chkTagNo.Checked == true)
            {
                this.cbxTagNo.Select();
                this.chkTagNo.ForeColor = Color.Black;
            }
            else if (e.KeyCode == Keys.Enter && this.chkTagNo.Checked == false)
            {
                this.txtWeight.Select();
                this.chkTagNo.ForeColor = Color.Black;
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDesign);
        }

        private void txtDesign_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtItemSize);
        }

        private void cbxKarrat_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxWorker);
        }

        private void cbxWorker_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDescription);
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWasteIn);
        }

        private void txtWasteIn_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtWasteage);
        }

        private void txtWasteage_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalLacker);
        }

        private void txtTotalLacker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (pnlPasaRate.Visible == true)
                    rbtPoundPasa.Select();
                else
                    txtGoldRate.Select();
            }
        }

        private void txtMakingPerGm_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtTotalMaking);
        }

        private void txtTotalMaking_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnBrowse);
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
                        this.lblHidden.Text = s.StockId.ToString();
                        this.ShowAllRecord(s.StockId);
                        this.txtWeight.Select();
                    }
                }
            }
        }

        private void cbxSaleMan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.dtpOrderDate.Select();
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtCashReceive);
        }

        private void chkUseGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && chkUseGold.Checked == true)
                this.txtWeightUsedGold.Select();
            else if (e.KeyCode == Keys.Enter && chkUseGold.Checked == false)
            {
                FormControls.KeyLose(sender, e, chkUseGold);
                FormControls.KeyFocus(sender, e, chkCheque);
            }
        }

        private void chkCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.chkCheque.Checked == true)
                this.txtCheque.Select();
            else if (e.KeyCode == Keys.Enter && this.chkCheque.Checked == false)
            {
                FormControls.KeyLose(sender, e, chkCheque);
                FormControls.KeyFocus(sender, e, chkCreditCard);
            }
        }

        private void chkCreditCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.chkCreditCard.Checked == true)
                this.txtCreditCard.Select();
            else if (e.KeyCode == Keys.Enter && this.chkCreditCard.Checked == false)
            {
                FormControls.KeyLose(sender, e, chkCreditCard);
                FormControls.KeyFocus(sender, e, chkPureGold);
            }
        }

        private void chkPureGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.chkPureGold.Checked == true)
                this.txtPGWeight.Select();
            else if (e.KeyCode == Keys.Enter && this.chkPureGold.Checked == false)
            {
                FormControls.KeyLose(sender, e, chkPureGold);
                FormControls.KeyFocus(sender, e, chkCash);
            }
        }

        private void chkCash_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.chkCash.Checked == true)
                this.txtBillBookNo.Select();
            else if (e.KeyCode == Keys.Enter && this.chkCash.Checked == false)
            {
                chkCash.ForeColor = Color.FromArgb(0,188,212);
                chkCash.BackColor = Color.FromArgb(204,247,251);
                txtBillBookNo.Select();
            }
        }

        private void txtWeightUsedGold_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtKaat);
        }

        private void txtKaat_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtPureWeight);
        }

        private void txtPureWeight_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, cbxUsedGoldKarat);
        }

        private void cbxKarat_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtRateUsedGold);
        }

        private void txtRateUsedGold_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtUsedGoldDesc);
        }

        private void txtUsedGoldDesc_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtUsedGoldDesc);
        }

        private void txtAmountCheck_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, cbxDepositeAccount);
        }

        private void cbxDepositeAccount_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtChequeDescription);
        }

        private void txtAmountCreditCard_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtDeductRate);
        }

        private void txtDeductRate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtReceiveables);
        }

        private void txtReceiveables_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, cbxBank);
        }

        private void cbxBank_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtBankDeductRate);
        }

        private void txtBankDeductRate_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtDepositeInBank);
        }

        private void txtDepositeInBank_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, cbxDepositeAccountCreditCard);
        }

        private void txtPGWeight_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtPGRate);
        }

        private void txtPGRate_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtPureGold);
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {

            FormControls.KeyFocus(sender, e, txtAlreadyAmount);
        }

        private void txtTotalWeight_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1;
            val = FormControls.GetDecimalValue(this.txtTotalWeight, 3);
            val1 = FormControls.GetDecimalValue(this.txtGoldRate, 1);

            if (GoldRatetype == "SonaPasa" && (GramTolaRate == "Tola" || GramTolaRate == "Gram"))
                this.txtGoldPrice.Text = (val * (val1 / Formulas.WeightInGm)).ToString("0");
            if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
                this.txtGoldPrice.Text = (val * (val1 / Formulas.WeightInGm)).ToString("0");
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                this.txtGoldPrice.Text = (val * val1).ToString("0");
        }

        private void txtKaat_TextChanged(object sender, EventArgs e)
        {
            frm.KaatInRattiForBalance(FormControls.GetDecimalValue(this.txtKaat, 1), FormControls.GetDecimalValue(this.txtWeightUsedGold, 3), txtPureWeight);
        }

        private void btnAddCountry_Click(object sender, EventArgs e)
        {
            AddItem frmi = new AddItem();
            frmi.ShowDialog();
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageWorker mw = new ManageWorker();
            mw.ShowDialog();
            FormControls.FillCombobox(cbxWorker, wrkDAL.GetAllWorkers(), "Name", "ID");
        }

        private void btnAddStone_Click(object sender, EventArgs e)
        {
            StoneDetail sd = new StoneDetail();
            sd.ShowDialog();
            this.ShowDataGrid();
        }

        private void cbxUsedGoldKarat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                string r = (string)this.cbxUsedGoldKarat.SelectedItem;
                decimal s = grDAL.GetRateByKarat(r, Convert.ToDateTime(dtpOrderDate.Value));
                this.txtRateUsedGold.Text = Math.Round(s, 1).ToString();
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
            {
                string r = (string)this.cbxUsedGoldKarat.SelectedItem;
                decimal s = grDAL.GetRateByKaratTola(r, Convert.ToDateTime(dtpOrderDate.Value));
                this.txtRateUsedGold.Text = Math.Round(s, 1).ToString();
            }
        }

        private void txtGoldRate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtGoldPrice);
        }

        private void txtGoldPrice_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtMakingPerGm);
        }

        private void txtBillBookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.cbxSaleMan.Select();
        }

        private void dtpOrderDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.dtpDeliveryDate.Select();
        }

        private void dtpDeliveryDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.cbxItemType.Select();
        }

        private void btnCreditCard_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption("", Convert.ToInt32(txtOrderNo.Text), 1, sum);
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
            frmpayment = new PaymentOption("", Convert.ToInt32(txtOrderNo.Text), 2, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfCheques = this.ListOfCheques;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtCheque.Text = sum.ToString("0");
        }

        private void btnPureGold_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption("", Convert.ToInt32(txtOrderNo.Text), 3, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfPureGold = this.ListOfPureGold;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtPureGold.Text = sum.ToString("0");
        }

        private void btnUsedGold_Click(object sender, EventArgs e)
        {
            frmpayment = new PaymentOption("", Convert.ToInt32(txtOrderNo.Text), 4, sum);
            frmpayment.Owner = this;
            frmpayment.ListOfUsedGold = this.ListOfUsedGold;
            FormControls.FadeOut(this);
            frmpayment.ShowDialog();
            FormControls.FadeIn(this);
            this.sum = frmpayment.sum;
            txtUsedGold.Text = sum.ToString("0");  
        }

        private void txtCashReceive_KeyUp(object sender, KeyEventArgs e)
        {
            Calculation();
        }

        private void txtCashPayment_KeyUp(object sender, KeyEventArgs e)
        {
            Calculation();
        }

        private void cbxItemType_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxGroupItem);
        }

        private void txtItemSize_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtQty);
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, cbxKarrat);
        }

        private void rbtPoundPasa_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtGoldRate);
        }

        private void rbtSonaPasa_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtGoldRate);
        }

        private void btnBrowse_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnAddItem);
        }

        private void chkFixRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkFixRate.Checked == true)
                    txtFixRate.Select();
                else
                    txtDiscount.Select();
            }
        }

        private void txtFixRate_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, txtDiscount);
        }

        private void txtCashReceive_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnCreditCard);
        }

        private void txtCashPayment_KeyDown(object sender, KeyEventArgs e)
        {
            FormControls.KeyFocus(sender, e, btnSave);
        }

        private void txtCreditCard_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtUsedGold_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtPureGold_TextChanged(object sender, EventArgs e)
        {
            Calculation();
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            Calculation();
        }

        private void btnAddGItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddItem frm = new AddItem();
            frm.ShowDialog();
            this.Show();
            if (frm.ItemId != 0)
            {
                FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
                cbxGroupItem.SelectedValue = frm.ItemId;
            }
        }

        private void dgvStoneInformation_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            decimal val1;
            decimal val2;
            if (this.txtTotalWeight.Text == "")
                val1 = 0;
            else
                val1 = FormControls.GetDecimalValue(txtTotalWeight, 3);
            val2 = upDateTextBox();
            this.totalweight(val1, val2, txtGrossWeight);
            this.txtStonePrice.Text = updateSum().ToString("0");
        }
    }
}