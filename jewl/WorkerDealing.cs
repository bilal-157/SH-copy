using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class frmWorkerDealing : Form
    {
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SaleDAL sDAL = new SaleDAL();
        WorkerLineItem wli = new WorkerLineItem();
        SqlTransaction trans;
        WorkerDAL wDAL = new WorkerDAL();
        Worker w;
        Formulas frm = new Formulas();
        Gold pg = new Gold();
        VouchersDAL vcDAL = new VouchersDAL();
        PurchaseGoldDAL pgDAL = new PurchaseGoldDAL();
        GoldRate gr = new GoldRate();
        GoldRates grs = new GoldRates();
        GoldRateDAL grDAL = new GoldRateDAL();
        Stock stk = new Stock();
        WorkerDealing wd = new WorkerDealing();
        WorkerDealingMaster Wdm = new WorkerDealingMaster();
        List<WorkerDealing> wdl = new List<WorkerDealing>();
        StonesDAL stDAL = new StonesDAL();
        ItemDAL itmDAL = new ItemDAL();
        Voucher wrkv = new Voucher();
        EditWorkerNo ewn = new EditWorkerNo();
        private Voucher pv;
        private Voucher wv;
        int deletetranid = 0;
        List<WorkerDealing> wdeal;
        List<WorkerDealingMaster> wdeaml;
        decimal pureWeight = 0;
        private AccountDAL acDAL = new AccountDAL();
        private VouchersDAL vDAL = new VouchersDAL();
        ChildAccount ca;
        ChildAccount cha;
        int clk = 1;
        int t;
        int cont;
        int c = -1;
        int l = 1, itemId = 0;
        int rpt;
        string WAccountCode = "";
        string sTATUS = "";
        decimal g;
        decimal total;
        public string GramTolaRate = "";
        public string GoldRatetype = "";

        public frmWorkerDealing()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void frmWorkerDealing_Load(object sender, EventArgs e)
        {
            this.cbxGroupItem.SelectedIndexChanged -= new System.EventHandler(this.cbxGroupItem_SelectedIndexChanged);
            this.cbxWorkerName.SelectedIndexChanged -= new System.EventHandler(cbxWorkerName_SelectedIndexChanged);
            this.cbxPurity.SelectedIndexChanged -= new System.EventHandler(cbxPurity_SelectedIndexChanged);
            this.txtBillBookNo.Text = (wDAL.GetMaxBillNo() + 1).ToString();
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
            FormControls.FillCombobox(dataGridViewComboBoxColumn1, stDAL.GetAllStoneTypeName(), "TypeName", "TypeId");
            this.txtId.Text = Convert.ToInt32(this.txtBillBookNo.Text) + "-" + l;

            this.rbtKaat.Checked = true;
            this.rbtWeight.Checked = true;
            this.RefreshW();
            this.ShowDataGrid();
            this.cbxWorkerName.Select();

            con.Open();
            trans = con.BeginTransaction();
            sTATUS = "GoldGiven";
            UserRights ur = new UserRights();
            string str = ur.GetRightsByUser();
            if (str == "Limited")
            {
                this.btnEdit.Enabled = false;
                this.btnDelete.Enabled = false;
            }
            GramTolaRate = sDAL.GetStartupGramTolaRate();
            GoldRatetype = sDAL.GetStartupGoldRateType();
            if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
            {
                decimal gramrate = grDAL.GetRateByKaratTola("24", DateTime.Today);
                this.txtGoldRate.Text = gramrate.ToString("0");
            }
            if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
            {
                decimal gramrate = grDAL.GetRateByKarat("24", DateTime.Today);
                this.txtGoldRate.Text = gramrate.ToString("0");
            }
            if (Main.City == "Islamabad")
            {
                this.pnlPasaRate.Visible = true;
                grs = grDAL.GetPasaRates(DateTime.Today);
                this.txtGoldRate.Text = grs.PoundPasa.ToString("0");
            }
        }

        public decimal upDateTextBox()
        {
            decimal weight = 0;
            int counter;
            for (counter = 0; counter < (dgvStonesDetail.Rows.Count - 1); counter++)
            {
                if (dgvStonesDetail.Rows[counter].Cells[3].Value == null)
                    weight += 0;
                else
                    weight += decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString());
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
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                }
                else
                {
                    if (Convert.ToString(dgvStonesDetail.Rows[counter].Cells[3].Value) != string.Empty && Convert.ToString(dgvStonesDetail.Rows[counter].Cells[5].Value) != string.Empty)
                    {
                        decimal k = (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[3].Value.ToString())) * (decimal.Parse(dgvStonesDetail.Rows[counter].Cells[5].Value.ToString()));
                        dgvStonesDetail.Rows[counter].Cells[6].Value = k.ToString();
                        sum += k;
                    }
                }
            }
            return sum;
        }

        private void cbxGroupItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxGroupItem.SelectedIndex == -1)
                return;
            else
                itemId = (int)this.cbxGroupItem.SelectedValue;
        }

        private void RefreshW()
        {
            this.txtContactNo.Text = "";
            this.txtKarrat.Text = "";
            this.txtMakingTola.Text = "";
            this.txtAddress.Text = "";
            this.txtDescription.Text = "";
            this.txtKaatIn.Text = "";
            this.txtMasha.Text = "";
            this.txtPure.Text = "";
            this.txtPurityWeight.Text = "";
            this.txtRatti.Text = "";
            this.txtTola.Text = "";
            this.txtWeight.Text = "";
            this.txtWorkerGoldBalance.Text = "";
            this.txtLabour.Text = "";
            this.txtLabourDecided.Text = "";
            this.txtStoneWeight.Text = "";
            this.txtTpriceOfStones.Text = "";
            this.lblUsedGold.Text = (acDAL.GetUsedGoldBalance()).ToString("0.000");
            this.lblPureGold.Text = (acDAL.GetPureGoldBalance()).ToString("0.000");
        }

        private void cbxWorkerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            Worker w = (Worker)(this.cbxWorkerName.SelectedItem);

            if (w == null)
                return;
            else
            {
                this.txtContactNo.Text = w.ContactNo.ToString();
                this.txtMakingTola.Text = w.MakingTola.ToString();
                this.txtCheejad.Text = w.Cheejad.ToString();
                this.txtAddress.Text = w.Address.ToString();
                this.txtGoldBalance.Text = w.GoldBalance.ToString("0.000");
                this.txtCashBalance.Text = w.CashBalance.ToString("0");
                this.txtOpeningGold.Text = w.OpeningGold.ToString("0.000");
                t = Convert.ToInt32(w.ID);
            }
        }

        private void ShowDGV(int m)
        {
            if (this.cbxWorkerName.SelectedIndex == -1)
                return;
            else
            {
                wdl = wDAL.GetWorkerDealingsById(m, Convert.ToDateTime(this.dtpDate.Value));
                this.dgvPreviousTransactionDetail.AutoGenerateColumns = false;
                this.dgvPreviousTransactionDetail.Rows.Clear();
                if (wdl == null)
                    return;
                else
                    cont = wdl.Count;
                this.dgvPreviousTransactionDetail.Rows.Add(cont);
                decimal p = 0;
                decimal y = 0;

                for (int i = 0; i < cont; i++)
                {
                    if (wdl[i].AddDate.HasValue)
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[0].Value = wdl[i].AddDate.ToString();
                    else
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[0].Value = wdl[i].GDate.ToString();
                    this.dgvPreviousTransactionDetail.Rows[i].Cells[1].Value = wdl[i].Description.ToString();
                    if (wdl[i].GivenWeight == 0)
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[2].Value = "";
                    else
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[2].Value = Convert.ToDecimal(wdl[i].GivenWeight).ToString("0.000");
                    if (wdl[i].ReceivedWeight == 0)
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[3].Value = "";
                    else
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[3].Value = Convert.ToDecimal(wdl[i].ReceivedWeight).ToString("0.000");
                    if (wdl[i].Kaat == 0)
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[4].Value = "";
                    else
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[4].Value = wdl[i].Kaat.ToString("0.0");
                    if (wdl[i].PureWeight == 0)
                    {
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(wdl[i].PureWeight).ToString("0.000");
                    }
                    else
                        this.dgvPreviousTransactionDetail.Rows[i].Cells[5].Value = Convert.ToDecimal(wdl[i].PureWeight).ToString("0.000");

                    if (wdl[i].ReceivedWeight != 0)
                        p = p + Convert.ToDecimal(wdl[i].PureWeight);
                    else
                        p = p - Convert.ToDecimal(wdl[i].PureWeight);
                    if (this.txtMakingTola.Text != "")
                    {
                        if ((wdl[i].Ujrat != 0 || wdl[i].CashGiven != 0 || wdl[i].GivenWeight != 0))
                        {
                            this.dgvPreviousTransactionDetail.Rows[i].Cells[7].Value = wdl[i].Ujrat.ToString("0.000");
                            this.dgvPreviousTransactionDetail.Rows[i].Cells[8].Value = wdl[i].UjratGiven.ToString("0.000");
                            this.dgvPreviousTransactionDetail.Rows[i].Cells[9].Value = wdl[i].CashGiven.ToString("0");
                            if (wdl[i].UjratGiven == 0)
                                y = y - wdl[i].CashGiven;
                            else
                                y = y - wdl[i].CashGiven + wdl[i].UjratGiven;
                        }
                    }
                    if (this.txtCheejad.Text != "")
                    {
                        if (wdl[i].Cheejad != 0 || wdl[i].GivenWeight != 0)
                        {
                            this.dgvPreviousTransactionDetail.Rows[i].Cells[7].Value = wdl[i].Cheejad.ToString("0.000");
                            this.dgvPreviousTransactionDetail.Rows[i].Cells[8].Value = wdl[i].CheejadDecided.ToString("0.000");

                            if (wdl[i].CheejadDecided != 0)
                            {
                                y = y + wdl[i].CheejadDecided;
                                if (wdl[i].GivenWeight == 0)
                                    this.dgvPreviousTransactionDetail.Rows[i].Cells[9].Value = Convert.ToDecimal(wdl[i].GivenWeight).ToString("0.000");
                            }
                            else if (wdl[i].GivenWeight != 0)
                            {
                                this.dgvPreviousTransactionDetail.Rows[i].Cells[9].Value = wdl[i].CheejadDecided.ToString("0.000");
                                y = y - Convert.ToDecimal(wdl[i].GivenWeight);
                            }
                        }
                    }
                    this.dgvPreviousTransactionDetail.Rows[i].Cells[10].Value = y.ToString();
                    this.dgvPreviousTransactionDetail.Rows[i].Cells[11].Value = wdl[i].TransId.ToString();
                    this.dgvPreviousTransactionDetail.Rows[i].Cells[6].Value = p.ToString();
                }
                g = p;
                total = y;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;

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
                        if (this.rbtCashGiven.Checked == false)
                        {
                            decimal val1 = 0;
                            frm.RatiMashaTolaGeneral(val1);
                            this.txtTola.Text = frm.Tola.ToString("0");
                            this.txtMasha.Text = frm.Masha.ToString("0");
                            this.txtRatti.Text = frm.Ratti.ToString("0.0");
                        }
                    }
                    else
                    {
                        if (str == ".")
                        {
                            if (this.rbtCashGiven.Checked == false)
                            {
                                decimal val = 0;
                                frm.RatiMashaTolaGeneral(val);
                            }
                        }
                        else
                        {
                            if (this.rbtCashGiven.Checked == false)
                            {
                                frm.RatiMashaTolaGeneral(Convert.ToDecimal(str));
                                this.txtTola.Text = frm.Tola.ToString("0");
                                this.txtMasha.Text = frm.Masha.ToString("0");
                                this.txtRatti.Text = frm.Ratti.ToString("0.0");
                            }
                        }
                    }
                }
                else
                {
                    str = this.txtWeight.Text + e.KeyChar.ToString();
                    if (str == ".")
                    {
                        if (this.rbtCashGiven.Checked == false)
                        {
                            decimal val = 0;
                            frm.RatiMashaTolaGeneral(val);
                        }
                    }
                    else
                    {
                        if (this.rbtCashGiven.Checked == false)
                        {
                            decimal val = Convert.ToDecimal(str);
                            frm.RatiMashaTolaGeneral(val);
                            this.txtTola.Text = frm.Tola.ToString("0");
                            this.txtMasha.Text = frm.Masha.ToString("0");
                            this.txtRatti.Text = frm.Ratti.ToString("0.0");
                        }
                    }
                }
            }
        }

        private bool KeyCheck(object sender, KeyPressEventArgs e)
        {
            bool bFlag = false;
            if (e.KeyChar == 13 || e.KeyChar == 27)
                bFlag = true;
            return bFlag;
        }

        private void txtKaatIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            if (this.txtWeight.Text != string.Empty)
            {
                if (rbtKaat.Checked == true)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                        e.Handled = true;
                    else
                        e.Handled = false;

                    bool bFlag = false;
                    bFlag = this.KeyCheck(sender, e);
                    if (bFlag == true)
                        e.Handled = true;

                    // only allow one decimal point
                    if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                        e.Handled = true;

                    if (e.Handled == false)
                    {
                        string str;
                        if (e.KeyChar == '\b')
                        {
                            if (this.txtKaatIn.Text == "")
                                return;
                            str = this.txtKaatIn.Text;
                            int i = str.Length;
                            str = str.Remove(i - 1);
                            if (str == string.Empty)
                            {
                                decimal val1 = 0;
                                frm.KaatInRattiforWrk(val1, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) - Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                            else
                            {
                                if (str == ".")
                                {
                                    decimal val = 0;
                                    frm.KaatInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                    this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) - Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                                }
                                else
                                {
                                    frm.KaatInRattiforWrk(Convert.ToDecimal(str), Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                    this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) - Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                                }
                            }
                        }
                        else
                        {
                            str = this.txtKaatIn.Text + e.KeyChar.ToString();
                            if (str == ".")
                            {
                                decimal val = 0;
                                frm.KaatInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) - Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                            else
                            {
                                decimal val = Convert.ToDecimal(str);
                                frm.KaatInRattiforWrk(Convert.ToDecimal(str), Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) - Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                        }
                    }
                }
                else if (this.rbtWaste.Checked == true)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                        e.Handled = true;
                    else
                        e.Handled = false;

                    bool bFlag = false;
                    bFlag = this.KeyCheck(sender, e);
                    if (bFlag == true)
                        e.Handled = true;

                    // only allow one decimal point
                    if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                        e.Handled = true;

                    if (e.Handled == false)
                    {
                        string str;
                        if (e.KeyChar == '\b')
                        {
                            if (this.txtKaatIn.Text == "")
                                return;
                            str = this.txtKaatIn.Text;
                            int i = str.Length;
                            str = str.Remove(i - 1);
                            if (str == string.Empty)
                            {
                                decimal val1 = 0;
                                frm.WasteInRattiforWrk(val1, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                            }
                            else
                            {
                                if (str == ".")
                                {
                                    decimal val = 0;
                                    frm.WasteInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                    this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) + Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                                }
                                else
                                    frm.WasteInRattiforWrk(Convert.ToDecimal(str), Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) + Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                        }
                        else
                        {
                            str = this.txtKaatIn.Text + e.KeyChar.ToString();
                            if (str == ".")
                            {
                                decimal val = 0;
                                frm.WasteInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) + Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                            else
                            {
                                decimal val = Convert.ToDecimal(str);
                                frm.WasteInRattiforWrk(Convert.ToDecimal(str), Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) + Convert.ToDecimal(this.txtPurityWeight.Text)).ToString("0.000");
                            }
                        }
                    }
                }
            }
        }

        private void rbtKaat_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtKaat.Checked == true)
            {
                this.txtPurityWeight.Text = "";
                this.txtKaatIn.Text = "";
                this.txtPure.Text = this.txtWeight.Text;
                this.txtKarrat.Text = "";
                this.label18.Text = "KaatIn";
                this.cbxPurity.Enabled = false;
            }
            else
            {
                this.label18.Text = "Waste";
                this.cbxPurity.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.cbxWorkerName.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Worker", Messages.Header);
                return;
            }
            if (this.dgvBookedItem.Rows.Count <= 1)
            {
                MessageBox.Show("Must Add Row in grid", Messages.Header);
                return;
            }
            try
            {
                ChildAccount cha = new ChildAccount();
                wd.Description = this.txtDescription.Text;
                Wdm.BillBookNo = Convert.ToInt32(this.txtBillBookNo.Text);
                Wdm.Date = Convert.ToDateTime(this.dtpDate.Value);

                if (this.txtReceieveTotal.Text == "")
                    Wdm.ReceiveTotalGold = 0;
                else
                    Wdm.ReceiveTotalGold = Convert.ToDecimal(this.txtReceieveTotal.Text);

                if (this.txtGivenTotal.Text == "")
                    Wdm.GivenGold = 0;
                else
                    Wdm.GivenGold = Convert.ToDecimal(this.txtGivenTotal.Text);
                if (this.txtBalance.Text == "")
                    Wdm.Balance = 0;
                else
                    Wdm.Balance = Convert.ToDecimal(this.txtBalance.Text);
                if (this.txtTotalPrice.Text == "")
                    Wdm.TotalPrice = 0;
                else
                    Wdm.TotalPrice = Convert.ToDecimal(this.txtTotalPrice.Text);
                if (this.txtGoldBalance.Text == "")
                    Wdm.PreveiousGoldBalance = 0;
                else
                    Wdm.PreveiousGoldBalance = Convert.ToDecimal(this.txtGoldBalance.Text);

                wd.EntryDate = (DateTime)Wdm.Date;
                wDAL.AddGoldTransaction(Wdm, con, trans);
                if (upDateCashGiven() > 0)
                {
                    #region CashGiven voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Dr = 0;
                    pv.Cr = upDateCashGiven();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Cash Given to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Cr = 0;
                    wv.Dr = upDateCashGiven();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateCashReceive() > 0)
                {
                    #region CashReceive voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Cr = 0;
                    pv.Dr = upDateCashReceive();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Cash Receive to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Dr = 0;
                    wv.Cr = upDateCashReceive();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateGoldGiven() > 0)
                {
                    #region GoldGiven voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Dr = 0;
                    pv.GoldCr = upDateGoldGiven();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Gold Given to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Cr = 0;
                    wv.GoldDr = upDateGoldGiven();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateGoldReceive() > 0)
                {
                    #region GoldReceive voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Cr = 0;
                    pv.GoldDr = upDateGoldReceive();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("GRV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Gold Receive to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Dr = 0;
                    wv.GoldCr = upDateGoldReceive();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateCashtoGoldGivenCash() > 0 || upDateCashtoGoldGivenWeight() > 0 || upDateCashtoGoldReceiveCash() > 0 || upDateCashtoGoldReceiveWeight() > 0)
                {
                    #region CashToGold Voucher
                    if (upDateCashtoGoldReceiveCash() > 0 && upDateCashtoGoldReceiveWeight() > 0)
                    {

                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.Dr = upDateCashtoGoldReceiveCash();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "CashToGold from worker " + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.GoldCr = upDateCashtoGoldReceiveWeight();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    if (upDateCashtoGoldGivenCash() > 0 && upDateCashtoGoldGivenWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = upDateCashtoGoldGivenCash();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "CashToGold to worker" + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.GoldDr = upDateCashtoGoldGivenWeight();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    #endregion
                }
                if (upDateGoldtoCashGivenCash() > 0 || upDateGoldtoCashGivenWeight() > 0 || upDateGoldtoCashReceiveCash() > 0 || upDateGoldtoCashReceiveWeight() > 0)
                {
                    #region GoldToCash Voucher
                    if (upDateGoldtoCashReceiveCash() > 0 || upDateGoldtoCashReceiveWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.GoldDr = upDateGoldtoCashReceiveWeight();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "GoldToCash from worker " + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Dr = 0;
                        wv.Cr = upDateGoldtoCashReceiveCash();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    if (upDateGoldtoCashGivenCash() > 0 || upDateGoldtoCashGivenWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.GoldCr = upDateGoldtoCashGivenWeight();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "GoldToCash to worker" + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.Dr = upDateGoldtoCashGivenCash();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con.Close();
                    MessageBox.Show("Record Saved Successfully", Messages.Header);
                    this.ShowDGV(t);
                    this.txtId.Text = (wDAL.GetMaxBillNo() + 1).ToString();
                    this.txtBillBookNo.Text = (wDAL.GetMaxBillNo() + 1).ToString();
                    WorkerDealingBill frm = new WorkerDealingBill();
                    frm.BillNo = (wDAL.GetMaxBillNo());
                    this.Dispose();
                    frm.ShowDialog();
                    frmWorkerDealing www = new frmWorkerDealing();
                    www.ShowDialog();
                }
            }
        }

        public decimal upDateCashGiven()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashGiven")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[2].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateCashReceive()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashReceive")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[2].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldGiven()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldGiven" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldReceive()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldReceive" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateCashtoGoldReceiveWeight()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashToGold" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Receive")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateCashtoGoldReceiveCash()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashToGold" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Receive")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[4].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateCashtoGoldGivenWeight()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashToGold" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Given")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateCashtoGoldGivenCash()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "CashToGold" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Given")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[4].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldtoCashReceiveWeight()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldToCash" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Receive")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldtoCashReceiveCash()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldToCash" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Receive")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[4].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldtoCashGivenWeight()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldToCash" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Given")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[6].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        public decimal upDateGoldtoCashGivenCash()
        {
            decimal weight = 0;
            int counter;
            if (!(this.dgvBookedItem.Rows.Count <= 1))
            {
                for (counter = 0; counter < (dgvBookedItem.Rows.Count - 1); counter++)
                {
                    if (Convert.ToString(dgvBookedItem.Rows[counter].Cells[3].Value) == "GoldToCash" && Convert.ToString(dgvBookedItem.Rows[counter].Cells[5].Value) == "Given")
                        weight += decimal.Parse(dgvBookedItem.Rows[counter].Cells[4].Value.ToString());
                    else
                        weight += 0;
                }
            }
            return weight;
        }

        private void rbtWeight_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtWeight.Checked == true)
            {
                this.label28.Text = "Making/Tola";
                this.label9.Text = "Making";
                this.label29.Text = "Making Decided";
                this.txtPiece.Text = "";
                this.txtMaking.Text = "";
                this.txtLabour.Text = "";
                this.txtLabourDecided.Text = "";
                this.txtCheejadValue.Text = "";
                this.txtCheejadDecided.Text = "";
            }
            if (rbtPiece.Checked == true)
            {
                this.label27.Text = "Piece";
                this.label28.Text = "Making/Piece";
                this.label9.Text = "Labour";
                this.label29.Text = "Labour Decided";
                this.txtPiece.Text = "";
                this.txtMaking.Text = "";
                this.txtLabour.Text = "";
                this.txtLabourDecided.Text = "";
                this.txtCheejadValue.Text = "";
                this.txtCheejadDecided.Text = "";
            }
        }

        private void rbtWaste_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtWaste.Checked == true)
            {
                this.txtPurityWeight.Text = "";
                this.txtKaatIn.Text = "";
                this.txtPure.Text = this.txtWeight.Text;
                this.txtKarrat.Text = "";
                this.label18.Text = "Waste";
                this.cbxPurity.Enabled = true;
            }
            else
            {
                this.label18.Text = "KaatIn";
                this.cbxPurity.Enabled = false;
            }
        }

        private void txtWeight_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtCashGiven.Checked == false)
            {
                decimal val = FormControls.GetDecimalValue(this.txtWeight, 3);
                this.txtPure.Text = this.txtWeight.Text;
                frm.RatiMashaTolaGeneral(val);
                this.txtTola.Text = frm.Tola.ToString("0");
                this.txtMasha.Text = frm.Masha.ToString("0");
                this.txtRatti.Text = frm.Ratti.ToString("0.0");
            }

            if (this.rbtGoldReceive.Checked == true)
            {
                if (this.cbxWorkerName.SelectedIndex != -1)
                {
                    if (this.txtWeight.Text == "")
                    {
                        this.txtCheejadValue.Text = "";
                        this.txtCheejadDecided.Text = "";
                        this.txtLabour.Text = "";
                        this.txtLabourDecided.Text = "";
                        return;
                    }
                }
            }
            else
            {
                this.txtLabour.Text = "";
                this.txtLabourDecided.Text = "";
                this.txtCheejadValue.Text = "";
                this.txtCheejadDecided.Text = "";
            }
            if (this.rbtCashGiven.Checked == true)
            {
                if (this.txtWeight.Text == "")
                    this.txtCashBalance.Text = total.ToString("0");
                else
                {
                    decimal o = 0;
                    if (this.txtCashBalance.Text != "")
                        o = total;
                    this.txtCashBalance.Text = (o - FormControls.GetDecimalValue(this.txtWeight, 3)).ToString("0");
                }
            }
            this.txtWeight.Text = this.txtWeight.Text;
        }

        private void cbxWorkerName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxWorkerName.SelectedIndexChanged += new EventHandler(cbxWorkerName_SelectedIndexChanged);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.btnEdit.Text == "Update")
            {
                if (this.dgvBookedItem.Rows.Count <= 1)
                {
                    MessageBox.Show("Must Add Row in grid", Messages.Header);
                    return;
                }
                wDAL.DeleteTransactionByBillNo(Convert.ToInt32(this.txtBillBookNo.Text), con, trans);
                ChildAccount cha = new ChildAccount();
                wd.Description = this.txtDescription.Text;
                Wdm.BillBookNo = Convert.ToInt32(this.txtBillBookNo.Text);
                Wdm.Date = Convert.ToDateTime(this.dtpDate.Value);

                if (this.txtReceieveTotal.Text == "")
                    Wdm.ReceiveTotalGold = 0;
                else
                    Wdm.ReceiveTotalGold = Convert.ToDecimal(this.txtReceieveTotal.Text);

                if (this.txtGivenTotal.Text == "")
                    Wdm.GivenGold = 0;
                else
                    Wdm.GivenGold = Convert.ToDecimal(this.txtGivenTotal.Text);
                if (this.txtBalance.Text == "")
                    Wdm.Balance = 0;
                else
                    Wdm.Balance = Convert.ToDecimal(this.txtBalance.Text);
                if (this.txtTotalPrice.Text == "")
                    Wdm.TotalPrice = 0;
                else
                    Wdm.TotalPrice = Convert.ToDecimal(this.txtTotalPrice.Text);
                if (this.txtGoldBalance.Text == "")
                    Wdm.PreveiousGoldBalance = 0;
                else
                    Wdm.PreveiousGoldBalance = Convert.ToDecimal(this.txtGoldBalance.Text);
                wd.EntryDate = (DateTime)Wdm.Date;
                wDAL.AddGoldTransaction(Wdm, con, trans);
                if (upDateCashGiven() > 0)
                {
                    #region CashGiven voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Dr = 0;
                    pv.Cr = upDateCashGiven();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Cash Given to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Cr = 0;
                    wv.Dr = upDateCashGiven();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateCashReceive() > 0)
                {
                    #region CashReceive voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Cr = 0;
                    pv.Dr = upDateCashReceive();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Cash Receive to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Dr = 0;
                    wv.Cr = upDateCashReceive();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateGoldGiven() > 0)
                {
                    #region GoldGiven voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Dr = 0;
                    pv.GoldCr = upDateGoldGiven();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Gold Given to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Cr = 0;
                    wv.GoldDr = upDateGoldGiven();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateGoldReceive() > 0)
                {
                    #region GoldReceive voucher
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                    }
                    pv.AccountCode = cha;
                    pv.Cr = 0;
                    pv.GoldDr = upDateGoldReceive();
                    pv.WBillNO = (int)Wdm.BillBookNo;
                    pv.DDate = (DateTime)wd.EntryDate;
                    pv.OrderNo = 0;
                    pv.SNO = 0;
                    pv.VNO = vDAL.CreateVNO("GRV", con, trans);
                    wd.WVNO = pv.VNO;
                    pv.Description = "Gold Receive to worker " + cbxWorkerName.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //worker account entry
                    wv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(WAccountCode, con, trans);
                    wv.AccountCode = child;
                    wv.Dr = 0;
                    wv.GoldCr = upDateGoldReceive();
                    wv.WBillNO = (int)Wdm.BillBookNo;
                    wv.DDate = (DateTime)wd.EntryDate;
                    wv.OrderNo = 0;
                    wv.SNO = 0;
                    wv.VNO = pv.VNO;
                    wv.Description = pv.Description;
                    vDAL.AddVoucher(wv, con, trans);
                    #endregion
                }
                if (upDateCashtoGoldGivenCash() > 0 || upDateCashtoGoldGivenWeight() > 0 || upDateCashtoGoldReceiveCash() > 0 || upDateCashtoGoldReceiveWeight() > 0)
                {
                    #region CashToGold Voucher
                    if (upDateCashtoGoldReceiveCash() > 0 && upDateCashtoGoldReceiveWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.Dr = upDateCashtoGoldReceiveCash();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "CashToGold to worker " + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.GoldCr = upDateCashtoGoldReceiveWeight();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    if (upDateCashtoGoldGivenCash() > 0 && upDateCashtoGoldGivenWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = 0;
                        pv.Cr = upDateCashtoGoldGivenCash();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "CashToGold to worker" + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.GoldDr = upDateCashtoGoldGivenWeight();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    #endregion
                }
                if (upDateGoldtoCashGivenCash() > 0 || upDateGoldtoCashGivenWeight() > 0 || upDateGoldtoCashReceiveCash() > 0 || upDateGoldtoCashReceiveWeight() > 0)
                {
                    #region GoldToCash Voucher
                    if (upDateGoldtoCashReceiveCash() > 0 || upDateGoldtoCashReceiveWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.GoldDr = upDateGoldtoCashReceiveWeight();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "GoldToCash to worker " + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Dr = 0;
                        wv.Cr = upDateGoldtoCashReceiveCash();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    if (upDateGoldtoCashGivenCash() > 0 || upDateGoldtoCashGivenWeight() > 0)
                    {
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Cr = 0;
                        pv.GoldCr = upDateGoldtoCashGivenWeight();
                        pv.WBillNO = (int)Wdm.BillBookNo;
                        pv.DDate = (DateTime)wd.EntryDate;
                        pv.OrderNo = 0;
                        pv.SNO = 0;
                        pv.VNO = vDAL.CreateVNO("GRV", con, trans);
                        wd.WVNO = pv.VNO;
                        pv.Description = "GoldToCash to worker" + cbxWorkerName.Text;
                        vDAL.AddVoucher(pv, con, trans);
                        //worker account entry
                        wv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(WAccountCode, con, trans);
                        wv.AccountCode = child;
                        wv.Cr = 0;
                        wv.Dr = upDateGoldtoCashGivenCash();
                        wv.WBillNO = (int)Wdm.BillBookNo;
                        wv.DDate = (DateTime)wd.EntryDate;
                        wv.OrderNo = 0;
                        wv.SNO = 0;
                        wv.VNO = pv.VNO;
                        wv.Description = pv.Description;
                        vDAL.AddVoucher(wv, con, trans);
                    }
                    #endregion
                }
                trans.Commit();
                con.Close();
                MessageBox.Show(Messages.Updated, Messages.Header);
                frmWorkerDealing wrk = new frmWorkerDealing();
                this.Dispose();
                wrk.ShowDialog();
            }
            if (this.btnEdit.Text == "&Edit")
            {
                EditWorkerNo ewn = new EditWorkerNo();
                ewn.ShowDialog();
                ShowDGVByNo(ewn.EditNum);
            }
        }

        private void RefreshAll()
        {
            this.cbxWorkerName.SelectedIndexChanged -= new System.EventHandler(cbxWorkerName_SelectedIndexChanged);
            this.txtId.Text = (wDAL.GetMaxTranId() + 1).ToString();
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
            this.rbtGoldReceive.Checked = true;
            this.rbtKaat.Checked = true;
            this.rbtWeight.Checked = true;
            this.RefreshW();
        }

        public void ShowDGVByNo(int pno)
        {
            Wdm = wDAL.GetWorkerDealingByBillNo(pno);
            if (Wdm == null)
            {
                MessageBox.Show("Record Not Found", Messages.Header);
                return;
            }
            else
            {
                this.dtpDate.Value = (DateTime)Wdm.Date;
                this.txtBillBookNo.Text = Wdm.BillBookNo.ToString();
                this.dgvBookedItem.AutoGenerateColumns = false;
                this.dgvBookedItem.Rows.Clear();
                if (Wdm.WorkerLineItem != null && Wdm.WorkerLineItem.Count > 0)
                {
                    int i = 0;
                    foreach (WorkerLineItem wli in Wdm.WorkerLineItem)
                    {
                        object[] values1 = new Object[7];
                        values1[0] = wli.WorkerDealing.WItemId;
                        values1[1] = wli.WorkerDealing.items.ItemName.ToString();
                        values1[2] = wli.WorkerDealing.WeightCash.ToString("0.000");
                        values1[3] = wli.WorkerDealing.Status.ToString();
                        values1[4] = Convert.ToDecimal(wli.WorkerDealing.ToCashGold).ToString("0.000");
                        if (wli.WorkerDealing.Status == "CashToGold" || wli.WorkerDealing.Status == "GoldToCash" || wli.WorkerDealing.Status == "CashToGold" || wli.WorkerDealing.Status == "GoldToCash")
                            values1[5] = wli.WorkerDealing.GRStatus.ToString();
                        values1[6] = Convert.ToDecimal(wli.WorkerDealing.PureWeight).ToString("0.000");
                        WAccountCode = wli.WorkerDealing.Worker.AccountCode.ToString();
                        this.dgvBookedItem.Rows.Add(values1);
                    }
                }
            }
            this.btnEdit.Text = "Update";
        }

        private void dgvPreviousTransactionDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == -1)
                    return;
                else
                {
                    c = Convert.ToInt32(this.dgvPreviousTransactionDetail.Rows[e.RowIndex].Cells[11].Value);
                    if (c != 0)
                    {
                        this.txtId.Text = c.ToString();
                        if (clk == 1)
                        {
                            wd = new WorkerDealing();
                            wd = wDAL.GetRecByTranId(c);
                            pureWeight = wDAL.GetGoldByTranId(c);
                            this.txtGiventWeight.Text = pureWeight.ToString("0.000");
                            this.btnEdit.Text = "Update";
                            this.btnDelete.Enabled = false;
                            this.btnSave.Enabled = false;
                            if (wd.GivenWeight != 0)
                            {
                                this.rbtGoldGiven.Checked = true;
                                this.txtWeight.Text = Convert.ToDecimal(wd.GivenWeight).ToString("0.000");
                                this.txtId.Text = c.ToString();
                                this.txtKaatIn.Text = wd.Kaat.ToString();
                                this.dtpDate.Value = wd.GDate.Value;
                                if (wd.KaatInRatti != 0)
                                {
                                    this.rbtKaat.Checked = true;
                                    this.txtPurityWeight.Text = Convert.ToDecimal(wd.KaatInRatti).ToString("0.0");
                                    this.txtKaatIn.Text = wd.Kaat.ToString("0.0");
                                    this.txtTola.Text = wd.Toola.ToString("0");
                                    this.txtMasha.Text = wd.Masha.ToString("0");
                                    this.txtRatti.Text = wd.Ratti.ToString("0.0");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                }
                                else if (wd.WasteInRatti != 0)
                                {
                                    this.rbtWaste.Checked = true;
                                    if (wd.Kaat == 0)
                                        this.txtKaatIn.Text = string.Empty;
                                    else
                                        this.txtKaatIn.Text = wd.Kaat.ToString("0.0");
                                    this.txtPurityWeight.Text = Convert.ToDecimal(wd.WasteInRatti).ToString("0.000");
                                    this.txtTola.Text = wd.Toola.ToString("0");
                                    this.txtMasha.Text = wd.Masha.ToString("0");
                                    this.txtRatti.Text = wd.Ratti.ToString("0.0");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                    if (this.txtKaatIn.Text == string.Empty)
                                        this.txtPure.Text = this.txtWeight.Text;
                                    else
                                        this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                }
                                else if (wd.Purity != 0)
                                    this.rbtWaste.Checked = true;
                                this.txtKarrat.Text = wd.Karrat.ToString();
                                this.txtPure.Text = this.txtWeight.Text;
                            }
                            if (wd.ReceivedWeight != 0)
                            {
                                this.rbtGoldReceive.Checked = true;
                                this.txtWeight.Text = Convert.ToDecimal(wd.ReceivedWeight).ToString("0.000");
                                this.txtMakingTola.Text = wd.MakingTola.ToString("0.0");
                                this.txtCheejad.Text = wd.PCheejad.ToString("0.000");
                                this.txtId.Text = c.ToString();
                                this.txtKaatIn.Text = wd.Kaat.ToString("0.0");
                                this.dtpDate.Value = wd.AddDate.Value;
                                if (wd.KaatInRatti != 0)
                                {
                                    this.rbtKaat.Checked = true;
                                    this.txtPurityWeight.Text = Convert.ToDecimal(wd.KaatInRatti).ToString("0.0");
                                    this.txtTola.Text = wd.Toola.ToString("0");
                                    this.txtMasha.Text = wd.Masha.ToString("0");
                                    this.txtRatti.Text = wd.Ratti.ToString("0.0");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                }
                                if (wd.WasteInRatti != 0 || wd.Purity != 0)
                                {
                                    this.rbtWaste.Checked = true;
                                    this.txtPurityWeight.Text = Convert.ToDecimal(wd.WasteInRatti).ToString("0.000");
                                    this.txtTola.Text = wd.Toola.ToString("0");
                                    this.txtMasha.Text = wd.Masha.ToString("0");
                                    this.txtRatti.Text = wd.Ratti.ToString("0.0");
                                    this.txtKaatIn.Text = wd.Kaat.ToString("0.0");
                                    this.txtPure.Text = Convert.ToDecimal(wd.PureWeight).ToString("0.000");
                                    this.cbxPurity.Text = Convert.ToDecimal(wd.Purity).ToString("0.000");
                                }
                                if (wd.Piece != 0)
                                {
                                    this.rbtPiece.Checked = true;
                                    this.txtPiece.Text = wd.Piece.ToString("0");
                                    this.txtMaking.Text = wd.PieceMaking.ToString("0.000");
                                }
                            }
                            if (wd.CashGiven != 0)
                            {
                                this.rbtCashGiven.Checked = true;
                                this.dtpDate.Value = wd.GDate.Value;
                                this.txtWeight.Text = wd.CashGiven.ToString("0");
                            }
                            if (wd.WVNO != null)
                                this.txtVNO.Text = wd.WVNO.ToString();
                            if (wd.CVNO != null)
                                this.txtCVNO.Text = wd.CVNO.ToString();
                            this.txtQty.Text = Convert.ToDecimal(wd.Qty).ToString("0");
                            if (wd.WorkerStonesList == null)
                                return;
                            else
                            {
                                this.dgvStonesDetail.AutoGenerateColumns = false;
                                int count = wd.WorkerStonesList.Count;
                                this.dgvStonesDetail.Rows.Add(count);
                                for (int i = 0; i < wd.WorkerStonesList.Count; i++)
                                {
                                    this.dgvStonesDetail.Rows[i].Cells[1].Value = wd.WorkerStonesList[i].StoneTypeId;
                                    this.dgvStonesDetail.Rows[i].Cells[2].Value = wd.WorkerStonesList[i].StoneId;
                                    if (wd.WorkerStonesList[i].StoneWeight.HasValue)
                                        this.dgvStonesDetail.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(wd.WorkerStonesList[i].StoneWeight), 3);
                                    else
                                        this.dgvStonesDetail.Rows[i].Cells[3].Value = string.Empty;
                                    if (wd.WorkerStonesList[i].Qty.HasValue)
                                        this.dgvStonesDetail.Rows[i].Cells[4].Value = Convert.ToInt32(wd.WorkerStonesList[i].Qty);
                                    else
                                        this.dgvStonesDetail.Rows[i].Cells[4].Value = string.Empty;
                                    if (wd.WorkerStonesList[i].Rate.HasValue)
                                        this.dgvStonesDetail.Rows[i].Cells[5].Value = Math.Round(Convert.ToDecimal(wd.WorkerStonesList[i].Rate), 1);
                                    else
                                        this.dgvStonesDetail.Rows[i].Cells[5].Value = string.Empty;
                                    if (wd.WorkerStonesList[i].Price.HasValue)
                                        this.dgvStonesDetail.Rows[i].Cells[6].Value = Math.Round(Convert.ToDecimal(wd.WorkerStonesList[i].Price), 0);
                                    else
                                        this.dgvStonesDetail.Rows[i].Cells[6].Value = string.Empty;
                                    if (!(string.IsNullOrEmpty(wd.WorkerStonesList[i].ColorName.ColorName.ToString())))
                                    {
                                        for (int j = 0; j < this.cbxColorName.Items.Count; j++)
                                        {
                                            StoneColor stc = (StoneColor)this.cbxColorName.Items[j];
                                            if (wd.WorkerStonesList[i].ColorName.ColorName.Equals(stc.ColorName.ToString()))
                                                this.dgvStonesDetail.Rows[i].Cells[7].Value = Convert.ToInt32(stc.ColorId);
                                        }
                                    }
                                    if (!(string.IsNullOrEmpty(wd.WorkerStonesList[i].CutName.CutName)))
                                    {
                                        for (int j = 0; j < this.cbxCutName.Items.Count; j++)
                                        {
                                            StoneCut stc = (StoneCut)this.cbxCutName.Items[j];
                                            if (wd.WorkerStonesList[i].CutName.CutName.Equals(stc.CutName.ToString()))
                                                this.dgvStonesDetail.Rows[i].Cells[8].Value = Convert.ToInt32(stc.CutId);
                                        }
                                    }
                                    if (!(string.IsNullOrEmpty(wd.WorkerStonesList[i].ClearityName.ClearityName.ToString())))
                                    {
                                        for (int j = 0; j < this.cbxClearity.Items.Count; j++)
                                        {
                                            StoneClearity stc = (StoneClearity)this.cbxClearity.Items[j];
                                            if (wd.WorkerStonesList[i].ClearityName.ClearityName.Equals(stc.ClearityName.ToString()))
                                                this.dgvStonesDetail.Rows[i].Cells[9].Value = Convert.ToInt32(stc.ClearityId);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void rbtCashGiven_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Cash";
            this.panel4.Visible = false;
            this.pnlTMR.Visible = false;
            sTATUS = "CashGiven";
            this.panel9.Visible = false;
            this.rbtCashToGold.Checked = false;
            this.rbtGoldToCash.Checked = false;
        }

        private void rbtGoldGiven_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Weight";
            this.panel4.Visible = false;
            this.pnlTMR.Visible = true;
            sTATUS = "GoldGiven";
            this.panel9.Visible = true;
        }

        private void rbtGoldReceive_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Weight";
            this.panel4.Visible = false;
            this.pnlTMR.Visible = true;
            sTATUS = "GoldReceive";
            this.panel9.Visible = true;
        }

        private void RefreshG()
        {
            this.txtKaatIn.Text = "";
            this.txtMasha.Text = "";
            this.txtPure.Text = "";
            this.txtPurityWeight.Text = "";
            this.txtRatti.Text = "";
            this.txtTola.Text = "";
            this.txtWeight.Text = "";
            this.txtGoldBalance.Text = "";
            this.txtLabour.Text = "";
            this.txtLabourDecided.Text = "";
            this.txtCheejadDecided.Text = "";
            this.txtCheejadValue.Text = "";
            this.txtPiece.Text = "";
            this.txtMaking.Text = "";
            this.txtStoneWeight.Text = "";
            this.lblUsedGold.Text = (acDAL.GetUsedGoldBalance()).ToString("0.000");
            this.lblPureGold.Text = (acDAL.GetPureGoldBalance()).ToString("0.000");
            this.txtQty.Text = string.Empty;
            this.cbxPurity.SelectedIndex = -1;
            this.dgvStonesDetail.Rows.Clear();
            this.txtContactNo.Text = "";
            this.txtAddress.Text = "";
            this.txtReceieveTotal.Text = "";
            this.txtGivenTotal.Text = "";
            this.txtBalance.Text = "";
            this.txtTotalPrice.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EditWorkerNo ewn = new EditWorkerNo();
            this.btnEdit.Enabled = false;
            this.btnSave.Enabled = false;

            if (this.btnDelete.Text == "Confirm")
            {
                wDAL.DeleteTransactionByBillNo(deletetranid, con, trans);

                trans.Commit();
                con.Close();
                MessageBox.Show("Record Deleted Successfully", Messages.Header);
                this.RefreshAll();
                this.dgvPreviousTransactionDetail.Rows.Clear();
                this.btnEdit.Enabled = true;
                this.btnSave.Enabled = true;
                frmWorkerDealing wd = new frmWorkerDealing();
                this.Dispose();
                wd.ShowDialog();
            }
            if (this.btnDelete.Text == "&Delete")
            {
                ewn.ShowDialog();
                deletetranid = ewn.editNum;
                this.btnDelete.Text = "Confirm";
            }
        }

        private void txtPiece_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtPiece.Checked == true)
            {
                decimal o, m;
                if (this.txtPiece.Text == "")
                    o = 0;
                else
                    o = Convert.ToDecimal(this.txtPiece.Text);
                if (this.txtMaking.Text == "")
                    m = 0;
                else
                    m = Convert.ToDecimal(this.txtMaking.Text);
                this.txtLabour.Text = (o * m).ToString();
                this.txtLabourDecided.Text = (o * m).ToString();
            }
        }

        private void txtMaking_TextChanged(object sender, EventArgs e)
        {
            decimal o, m;
            if (this.rbtPiece.Checked == true)
            {
                o = FormControls.GetDecimalValue(txtPiece, 0);
                m = FormControls.GetDecimalValue(txtMaking, 0);
                this.txtLabour.Text = (o * m).ToString("0");
                this.txtLabourDecided.Text = (o * m).ToString("0");
            }
            else if (this.rbtWeight.Checked == true)
            {
                o = FormControls.GetDecimalValue(txtWeight, 3) / Formulas.WeightInGm;
                m = FormControls.GetDecimalValue(txtMaking, 0);
                this.txtLabour.Text = (o * m).ToString("0");
                this.txtLabourDecided.Text = (o * m).ToString("0");
            }
        }

        private void txtLabourDecided_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtCashGiven.Checked == false)
            {
                decimal o, m;
                if (this.txtCashBalance.Text == string.Empty)
                    o = 0;
                else
                    o = total;
                if (this.txtLabourDecided.Text == string.Empty)
                {
                    this.txtCashBalance.Text = total.ToString("0");
                    m = 0;
                }
                else
                {
                    m = Convert.ToDecimal(this.txtLabourDecided.Text);
                    this.txtCashBalance.Text = Math.Round((o + m), 3).ToString();
                }
            }
        }

        private void txtWeight_Leave(object sender, EventArgs e)
        {
            if (this.txtWeight.Text != string.Empty)
            {
                decimal val1 = Convert.ToDecimal(this.txtWeight.Text);
                string s = val1.ToString("N3");
                this.txtWeight.Text = s;
                if (this.rbtCheejat.Checked)
                {
                    if (this.txtCheejad.Text != string.Empty)
                    {
                        this.txtCheejadValue.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                        this.txtCheejadDecided.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                    }
                }
            }
        }

        private void ShowDataGrid()
        {
            this.DataGridViewComboBoxColumn2.DataSource = stDAL.GetAllStoneName();
            this.DataGridViewComboBoxColumn2.DisplayMember = "StoneName";
            this.DataGridViewComboBoxColumn2.ValueMember = "StoneId";

            cbxColorName.DataSource = stDAL.GetAllColorName();
            cbxColorName.DisplayMember = "ColorName";
            cbxColorName.ValueMember = "ColorId";

            cbxCutName.DataSource = stDAL.GetAllCutName();
            cbxCutName.DisplayMember = "CutName";
            cbxCutName.ValueMember = "CutId";

            cbxClearity.DataSource = stDAL.GetAllClearityName();
            cbxClearity.DisplayMember = "ClearityName";
            cbxClearity.ValueMember = "ClearityId";
        }

        private List<Stones> getAllStones()
        {
            List<Stones> stDetail = null;
            int j = Convert.ToInt32(this.dgvStonesDetail.Rows.Count - 1);
            if (j == 0)
                return stDetail;
            else
            {
                if (stDetail == null) stDetail = new List<Stones>();
                DataGridViewComboBoxColumn cbxStone = (DataGridViewComboBoxColumn)dgvStonesDetail.Columns["SronesWeight"];
                this.dgvStonesDetail.CellEndEdit -= new DataGridViewCellEventHandler(dgvStonesDetail_CellEndEdit);
                for (int i = 0; i < dgvStonesDetail.Rows.Count - 1; i++)
                {
                    Stones std = new Stones();
                    string str = (string)dgvStonesDetail.Rows[i].Cells[2].FormattedValue.ToString();
                    if (string.IsNullOrEmpty(str))
                        std.StoneId = null;
                    else
                    {
                        std.StoneId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[2].Value.ToString());
                        for (int k = 0; k < this.DataGridViewComboBoxColumn2.Items.Count; k++)
                        {
                            Stones stc = (Stones)this.DataGridViewComboBoxColumn2.Items[k];
                            if (stc.StoneId == std.StoneId)
                                std.StoneName = stc.StoneName;
                        }
                    }
                    if (string.IsNullOrEmpty(Convert.ToString(dgvStonesDetail.Rows[i].Cells["Qty"].Value)))
                        std.StoneWeight = 0;
                    else
                        std.StoneWeight = decimal.Parse(dgvStonesDetail.Rows[i].Cells["Qty"].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = 0;
                    else
                        std.Qty = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[4].Value.ToString());

                    if ((string)dgvStonesDetail.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = 0;
                    else
                        std.Rate = Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[5].Value.ToString());

                    if ((string)dgvStonesDetail.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = 0;
                    else
                        std.Price = Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[6].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[7].FormattedValue == "")
                        std.ColorName = null;
                    else
                    {
                        std.ColorName = new StoneColor();
                        std.ColorName.ColorId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[7].Value.ToString());
                        for (int k = 0; k < cbxColorName.Items.Count; k++)
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
                        for (int k = 0; k < cbxCutName.Items.Count; k++)
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
                        for (int k = 0; k < cbxClearity.Items.Count; k++)
                        {
                            StoneClearity stc = (StoneClearity)cbxClearity.Items[k];
                            if (stc.ClearityId == std.ClearityName.ClearityId)
                                std.ClearityName.ClearityName = stc.ClearityName;
                        }
                    }
                    stDetail.Add(std);
                }
                this.dgvStonesDetail.CellEndEdit += new DataGridViewCellEventHandler(dgvStonesDetail_CellEndEdit);
            }
            return stDetail;
        }

        private void dgvStonesDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
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
            if (this.txtWeight.Text == string.Empty)
                val1 = 0;
            else
                val1 = Convert.ToDecimal(this.txtWeight.Text);
            val2 = upDateTextBox();
            this.txtStoneWeight.Text = val2.ToString("N3");
            this.txtTpriceOfStones.Text = updateSum().ToString("0");
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
                    if ((string)dgvStonesDetail.Rows[i].Cells[2].FormattedValue == string.Empty)
                    {
                        std.StoneId = null;
                        std.StoneTypeId = null;
                    }
                    else
                    {
                        std.StoneId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[2].Value.ToString());
                        std.StoneTypeId = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[1].Value.ToString());
                    }
                    if ((string)dgvStonesDetail.Rows[i].Cells[3].FormattedValue == "")
                        std.StoneWeight = null;
                    else
                        std.StoneWeight = Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[3].Value.ToString());
                    if ((string)dgvStonesDetail.Rows[i].Cells[4].FormattedValue == "")
                        std.Qty = null;
                    else
                        std.Qty = Convert.ToInt32(dgvStonesDetail.Rows[i].Cells[4].Value.ToString());

                    if ((string)dgvStonesDetail.Rows[i].Cells[5].FormattedValue == "")
                        std.Rate = null;
                    else
                        std.Rate = Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[5].Value.ToString());

                    if ((string)dgvStonesDetail.Rows[i].Cells[6].FormattedValue == "")
                        std.Price = null;
                    else
                        std.Price = Convert.ToDecimal(dgvStonesDetail.Rows[i].Cells[6].Value.ToString());

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
                    if (this.rbtGoldGiven.Checked == true)
                        std.GiveReceive = 1;
                    else if (this.rbtGoldReceive.Checked == true)
                        std.GiveReceive = 0;
                    else
                        std.GiveReceive = 2;
                    stDetail.Add(std);
                }
            }
            return stDetail;
        }

        private void txtMakingTola_TextChanged(object sender, EventArgs e)
        {
            string str = this.txtWeight.Text;
            if (str == "." || str == "")
            {
                if (this.rbtCashGiven.Checked == false)
                {
                    decimal val = 0;
                    frm.RatiMashaTolaGeneral(val);
                }
            }
            else
            {
                if (this.rbtCashGiven.Checked == false)
                {
                    decimal val = Convert.ToDecimal(str);
                    frm.RatiMashaTolaGeneral(val);
                    this.txtTola.Text = frm.Tola.ToString("0");
                    this.txtMasha.Text = frm.Masha.ToString("0");
                    this.txtRatti.Text = frm.Ratti.ToString("0.0");
                }
            }
            if (this.rbtGoldReceive.Checked == true)
            {
                if (this.cbxWorkerName.SelectedIndex != -1)
                {
                    if (this.txtWeight.Text == "")
                    {
                        this.txtCheejadValue.Text = "";
                        this.txtCheejadDecided.Text = "";
                        this.txtLabour.Text = "";
                        this.txtLabourDecided.Text = "";
                        return;
                    }
                    if (this.txtMakingTola.Text != "")
                    {
                        if (this.rbtWeight.Checked == true)
                        {
                            this.txtLabour.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtMakingTola.Text)).ToString();
                            this.txtLabourDecided.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtMakingTola.Text)).ToString();
                            this.txtCheejadValue.Text = "";
                            this.txtCheejadDecided.Text = "";
                        }
                    }
                    else
                    {
                        if (this.txtMakingTola.Text != "")
                        {
                            this.txtCheejadValue.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString();
                            this.txtCheejadDecided.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString();
                            this.txtLabour.Text = "";
                            this.txtLabourDecided.Text = "";
                        }
                    }
                    if (this.rbtPiece.Checked == true)
                    {
                        decimal o, m;
                        if (this.txtPiece.Text == "")
                            o = 0;
                        else
                            o = Convert.ToDecimal(this.txtPiece.Text);
                        if (this.txtMaking.Text == "")
                            m = 0;
                        else
                            m = Convert.ToDecimal(this.txtMaking.Text);
                        this.txtLabour.Text = (o * m).ToString("0");
                        this.txtLabourDecided.Text = (o * m).ToString("0");
                    }
                }
            }
            else
            {
                this.txtLabour.Text = "";
                this.txtLabourDecided.Text = "";
                this.txtCheejadValue.Text = "";
                this.txtCheejadDecided.Text = "";
            }
            if (this.rbtCashGiven.Checked == true)
            {
                if (this.txtWeight.Text == "")
                    this.txtCashBalance.Text = total.ToString("0");
                else
                {
                    decimal o, m;
                    if (this.txtCashBalance.Text == "")
                        o = 0;
                    else
                        o = total;
                    m = Convert.ToDecimal(this.txtWeight.Text);
                    this.txtCashBalance.Text = (o - m).ToString("0");
                }
            }
        }

        private void txtPiece_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void txtMaking_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void dgvStonesDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Error here");
        }

        private void cbxPurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = this.cbxPurity.Text;
            str = str.Substring(str.IndexOf(')') + 1);
            if (this.txtWeight.Text != string.Empty)
            {
                this.txtPure.Text = (Convert.ToDecimal(this.txtWeight.Text) * Convert.ToDecimal(str)).ToString();
                if (this.cbxPurity.Text == "0.927")
                    this.txtKarrat.Text = "22.25";
                else if (this.cbxPurity.Text == "0.917")
                    this.txtKarrat.Text = "22";
                else if (this.cbxPurity.Text == "0.917")
                    this.txtKarrat.Text = "21.75";
                else if (this.cbxPurity.Text == "0.916")
                    this.txtKarrat.Text = "21";
                else if (this.cbxPurity.Text == "0.896")
                    this.txtKarrat.Text = "21.5";
                else if (this.cbxPurity.Text == "0.885")
                    this.txtKarrat.Text = "21.25";
                else if (this.cbxPurity.Text == "0.875")
                    this.txtKarrat.Text = "21";
                else if (this.cbxPurity.Text == "0.864")
                    this.txtKarrat.Text = "20.75";
                else if (this.cbxPurity.Text == "0.854")
                    this.txtKarrat.Text = "20.5";
                else if (this.cbxPurity.Text == "0.844")
                    this.txtKarrat.Text = "20.25";
                else if (this.cbxPurity.Text == "0.833")
                    this.txtKarrat.Text = "20";
                else if (this.cbxPurity.Text == "0.823")
                    this.txtKarrat.Text = "19.75";
                else if (this.cbxPurity.Text == "0.812")
                    this.txtKarrat.Text = "19.5";
                else if (this.cbxPurity.Text == "0.802")
                    this.txtKarrat.Text = "19.25";
                else if (this.cbxPurity.Text == "0.792")
                    this.txtKarrat.Text = "19";
                else if (this.cbxPurity.Text == "0.781")
                    this.txtKarrat.Text = "18.75";
                else if (this.cbxPurity.Text == "0.771")
                    this.txtKarrat.Text = "18.5";
                else if (this.cbxPurity.Text == "0.76")
                    this.txtKarrat.Text = "18.25";
                else if (this.cbxPurity.Text == "0.75")
                    this.txtKarrat.Text = "18";
                else if (this.cbxPurity.Text == "0.739")
                    this.txtKarrat.Text = "17.75";
                else if (this.cbxPurity.Text == "0.729")
                    this.txtKarrat.Text = "17.5";
                else if (this.cbxPurity.Text == "0.719")
                    this.txtKarrat.Text = "17.25";
                else if (this.cbxPurity.Text == "0.708")
                    this.txtKarrat.Text = "17";
                else if (this.cbxPurity.Text == "0.688")
                    this.txtKarrat.Text = "16.75";
                else if (this.cbxPurity.Text == "0.687")
                    this.txtKarrat.Text = "16.5";
                else if (this.cbxPurity.Text == "(9/12)0.9722")
                    this.txtKarrat.Text = "23.33";
                else if (this.cbxPurity.Text == "(5/16)0.9528")
                    this.txtKarrat.Text = "22.86";
                else if (this.cbxPurity.Text == "(6/12)0.9444")
                    this.txtKarrat.Text = "22.66";
                else if (this.cbxPurity.Text == "(6/13.5)0.9315")
                    this.txtKarrat.Text = "22.35";
                else if (this.cbxPurity.Text == "(9/13.5)0.9589")
                    this.txtKarrat.Text = "23.01";
                else if (this.cbxPurity.Text == "(15/13.5)0.92237")
                    this.txtKarrat.Text = "22.13";
                else
                    this.txtKarrat.Text = "";
            }
            if (str == "0.00")
            {
                this.txtPure.Text = this.txtWeight.Text;
            }
        }

        private void cbxPurity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxPurity.SelectedIndexChanged += new System.EventHandler(cbxPurity_SelectedIndexChanged);
        }

        private void txtCheejadValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 58) && (Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46))
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtCheejadDecided_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 58) && (Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46))
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtLabour_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 58) && (Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46))
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtLabourDecided_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 58) && (Convert.ToInt16(e.KeyChar) != 8 && Convert.ToInt16(e.KeyChar) != 46))
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPure_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void txtPurityWeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
        }

        private void dgvStonesDetail_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // cancel editing if the ParentID has not been set
            if (e.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].FormattedValue.Equals(string.Empty))
            {
                e.Cancel = true;
            }
        }

        private void dgvStonesDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvStonesDetail.CurrentCell.ColumnIndex == 2 && this.dgvStonesDetail.CurrentRow.Cells[1].Value != null)
            {
                int sty = (int)this.dgvStonesDetail.CurrentRow.Cells[1].Value;
                ComboBox cmb = e.Control as ComboBox;
                cmb.DataSource = stDAL.GetAllStoneNamebyId(sty);
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

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void rbtCheejat_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCheejat.Checked)
            {
                if (this.txtCheejad.Text != string.Empty && this.txtWeight.Text != string.Empty)
                {
                    this.txtCheejadValue.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                    this.txtCheejadDecided.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                }
            }
        }

        private void txtCheejad_TextChanged(object sender, EventArgs e)
        {
            if (this.rbtCheejat.Checked)
            {
                if (this.txtCheejad.Text != string.Empty && this.txtWeight.Text != string.Empty)
                {
                    this.txtCheejadValue.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                    this.txtCheejadDecided.Text = (Convert.ToDecimal(this.txtWeight.Text) / (decimal)11.664 * Convert.ToDecimal(this.txtCheejad.Text)).ToString("N3");
                }
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            if (this.cbxWorkerName.SelectedIndex != -1)
            {
                this.ShowDGV((int)this.cbxWorkerName.SelectedValue);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            FormControls.PanelBorder(sender, e);
        }

        private void CreateWorkerLine(object sender, EventArgs e)
        {
            if (this.txtWeight.Text == "")
            {
                MessageBox.Show("Please Insert Weight", Messages.Header);
                return;
            }
            wli = new WorkerLineItem();
            wli.WorkerDealing = new WorkerDealing();
            wli.WorkerDealing.WeightCash = this.txtWeight.Text == "" ? 0 : Convert.ToDecimal(this.txtWeight.Text);
            wli.WorkerDealing.GoldRate = this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text);
            wli.WorkerDealing.ToCashGold = this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text);
            wli.WorkerDealing.EntryDate = Convert.ToDateTime(this.dtpDate.Value);
            wli.WorkerDealing.BillBookNo = Convert.ToInt32(this.txtBillBookNo.Text);
            Worker wdc = (Worker)this.cbxWorkerName.SelectedItem;
            string str = "";
            if (this.btnEdit.Text == "Update")
                str = this.txtBillBookNo.Text;
            else
                str = (wDAL.GetMaxBillNo() + 1).ToString();
            //string str = (wDAL.GetMaxBillNo() + 1).ToString();
            wli.WorkerDealing.TransId = Convert.ToInt32(this.txtBillBookNo.Text);
            wli.WorkerDealing.WItemId = (str + "-" + this.dgvBookedItem.Rows.Count).ToString();
            this.txtId.Text = wli.WorkerDealing.WItemId.ToString();
            WAccountCode = (string)wdc.AccountCode;
            if (this.rbtCashToGold.Checked == true)
            {
                wli.WorkerDealing.Status = "CashToGold";
                if (this.rbtGoldGiven.Checked == true)
                    wli.WorkerDealing.GRStatus = "Given";
                if (this.rbtGoldReceive.Checked == true)
                    wli.WorkerDealing.GRStatus = "Receive";
            }
            if (this.rbtGoldToCash.Checked == true)
            {
                wli.WorkerDealing.Status = "GoldToCash";
                if (this.rbtGoldGiven.Checked == true)
                    wli.WorkerDealing.GRStatus = "Given";
                if (this.rbtGoldReceive.Checked == true)
                    wli.WorkerDealing.GRStatus = "Receive";
            }
            if (this.rbtCashGiven.Checked == true)
            {
                wli.WorkerDealing.Status = "CashGiven";
                wli.WorkerDealing.GRStatus = "Given";
            }
            if (this.rbtCashReceive.Checked == true)
            {
                wli.WorkerDealing.Status = "CashReceive";
                wli.WorkerDealing.GRStatus = "Receive";
            }
            if (this.rbtGoldGiven.Checked == true && this.rbtCashToGold.Checked == false && this.rbtGoldToCash.Checked == false)
            {
                wli.WorkerDealing.Status = "GoldGiven";
                wli.WorkerDealing.GRStatus = "Given";
            }
            if (this.rbtGoldReceive.Checked == true && this.rbtCashToGold.Checked == false && this.rbtGoldToCash.Checked == false)
            {
                wli.WorkerDealing.Status = "GoldReceive";
                wli.WorkerDealing.GRStatus = "Receive";
            }
            if (cbxGroupItem.SelectedIndex == -1)
            {
                wli.WorkerDealing.items = new Item();
                wli.WorkerDealing.items.ItemId = 0;
            }
            else
                wli.WorkerDealing.items = (Item)this.cbxGroupItem.SelectedItem;
            wli.WorkerDealing.Worker = (Worker)this.cbxWorkerName.SelectedItem;
            wli.WorkerDealing.AddDate = Convert.ToDateTime(this.dtpDate.Value);
            wli.WorkerDealing.TDate = Convert.ToDateTime(this.dtpDate.Value);
            wli.WorkerDealing.GDate = null;
            wli.WorkerDealing.GivenWeight = 0;
            wli.WorkerDealing.Toola = FormControls.GetDecimalValue(this.txtTola, 0);
            wli.WorkerDealing.Ratti = FormControls.GetDecimalValue(this.txtRatti, 0);
            wli.WorkerDealing.Masha = FormControls.GetDecimalValue(this.txtMasha, 0);
            wli.WorkerDealing.PureWeight = FormControls.GetDecimalValue(this.txtPure, 3);
            wli.WorkerDealing.Description = this.txtDescription.Text;
            wli.WorkerDealing.CashBalance = 0;
            wli.WorkerDealing.CashGiven = 0;
            wli.WorkerDealing.GoldBalance = 0;
            wli.WorkerDealing.WVNO = "";
            wli.WorkerDealing.CVNO = "";
            if (this.txtGoldRate.Text == "")
                wli.WorkerDealing.GoldRate = 0;
            else
                wli.WorkerDealing.GoldRate = Convert.ToDecimal(this.txtGoldRate.Text);
            if (this.txtQty.Text == "")
            {
                wli.WorkerDealing.Qty = 0;
                wli.WorkerDealing.Cheejad = FormControls.GetDecimalValue(this.txtCheejadValue, 3);
                wli.WorkerDealing.CheejadDecided = FormControls.GetDecimalValue(this.txtCheejadDecided, 3);
            }
            else
                if (txtCheejadDecided.Text == string.Empty || txtCheejadDecided.Text == "0")
                {
                    wli.WorkerDealing.Qty = FormControls.GetIntValue(this.txtQty);
                    wli.WorkerDealing.Cheejad = 0;
                    wli.WorkerDealing.CheejadDecided = FormControls.GetDecimalValue(this.txtCheejadDecided, 3);
                }
            if (this.rbtKaat.Checked == true)
            {
                if (this.txtPurityWeight.Text == "")
                    wli.WorkerDealing.KaatInRatti = 0;
                else
                    wli.WorkerDealing.KaatInRatti = FormControls.GetDecimalValue(this.txtPurityWeight, 3);
                if (this.txtKaatIn.Text == "")
                    wli.WorkerDealing.Kaat = 0;
                else
                    wli.WorkerDealing.Kaat = Convert.ToDecimal(this.txtKaatIn.Text);
                wli.WorkerDealing.PureWeight = FormControls.GetDecimalValue(this.txtPure, 3);
                wli.WorkerDealing.TotalWeight = 0;
                wli.WorkerDealing.WasteInRatti = 0;
                wli.WorkerDealing.Purity = 0;
            }
            else if (this.rbtWaste.Checked == true)
            {
                wli.WorkerDealing.WasteInRatti = FormControls.GetDecimalValue(this.txtPurityWeight, 3);
                wli.WorkerDealing.TotalWeight = FormControls.GetDecimalValue(this.txtPure, 3);
                wli.WorkerDealing.PureWeight = FormControls.GetDecimalValue(this.txtPure, 3);
                wli.WorkerDealing.KaatInRatti = 0;
                string purity = this.cbxPurity.Text;
                if (purity == "")
                    purity = "0";
                if (purity != "0.00" || purity != "" || purity != null)
                {
                    purity = purity.Substring(purity.IndexOf(')') + 1);
                    wli.WorkerDealing.Purity = Convert.ToDecimal(purity);
                }
            }
            else
            {
                wli.WorkerDealing.KaatInRatti = 0;
                wli.WorkerDealing.WasteInRatti = 0;
            }
            wli.WorkerDealing.Karrat = this.txtKarrat.Text;

            if (this.rbtWeight.Checked == true)
            {
                wli.WorkerDealing.Ujrat = FormControls.GetDecimalValue(this.txtLabour, 0);
                wli.WorkerDealing.UjratGiven = FormControls.GetDecimalValue(this.txtLabourDecided, 0);
                wli.WorkerDealing.PieceMaking = 0;
                wli.WorkerDealing.Piece = 0;
                wli.WorkerDealing.PieceMaking = 0;
                wli.WorkerDealing.Piece = 0;
                wli.WorkerDealing.CashGiven = 0;
                wli.WorkerDealing.CashBalance = 0;
                wli.WorkerDealing.PieceMaking = 0;
                wli.WorkerDealing.Piece = 0;
            }
            else
            {
                wli.WorkerDealing.Piece = FormControls.GetIntValue(this.txtPiece);
                wli.WorkerDealing.PieceMaking = FormControls.GetDecimalValue(this.txtMaking, 0);
                wli.WorkerDealing.Ujrat = FormControls.GetDecimalValue(this.txtLabour, 0);
                wli.WorkerDealing.UjratGiven = FormControls.GetDecimalValue(this.txtLabourDecided, 0);
                wli.WorkerDealing.Cheejad = FormControls.GetDecimalValue(this.txtCheejadValue, 3);
                wli.WorkerDealing.CheejadDecided = FormControls.GetDecimalValue(this.txtCheejadDecided, 3);
            }
            wli.WorkerDealing.WorkerStonesList = this.GetAllDetails();
            Wdm.AddLineItems(wli);
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (this.cbxWorkerName.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Worker", Messages.Header);
                return;
            }
            CreateWorkerLine(sender, e);
            if (this.txtWeight.Text == "")
                return;
            else
            {
                object[] values = new Object[7];
                values[0] = this.txtId.Text.ToString();
                if (cbxGroupItem.SelectedIndex == -1)
                    values[1] = wli.WorkerDealing.items.ItemName;
                else
                    values[1] = wli.WorkerDealing.items.ItemName.ToString();
                if (this.rbtGoldReceive.Checked == true || this.rbtGoldGiven.Checked == true || this.rbtCashToGold.Checked == true)
                    values[2] = this.txtWeight.Text;
                if (this.rbtCashReceive.Checked == true || this.rbtCashGiven.Checked == true || this.rbtGoldToCash.Checked == true)
                    values[2] = (this.txtWeight.Text == "" ? 0 : Convert.ToDecimal(this.txtWeight.Text)).ToString("0");
                values[3] = sTATUS;
                values[4] = this.txtCashGold.Text.ToString();
                if (this.rbtGoldGiven.Checked == true && (this.rbtCashToGold.Checked == true || this.rbtGoldToCash.Checked == true))
                    values[5] = "Given";
                if (this.rbtGoldReceive.Checked == true && (this.rbtCashToGold.Checked == true || this.rbtGoldToCash.Checked == true))
                    values[5] = "Receive";
                values[6] = this.txtPure.Text.ToString();
                l = l + 1;
                //this.txtId.Text = Convert.ToInt32(this.txtBillBookNo.Text) + "-" + l;
                this.dgvBookedItem.Rows.Add(values);
                this.txtId.Text = Convert.ToInt32(this.txtBillBookNo.Text) + "-" + this.dgvBookedItem.Rows.Count;
            }
            this.RefershInfo();
        }

        public void RefershInfo()
        {
            this.cbxGroupItem.SelectedIndex = -1;
            this.txtQty.Text = "";
            this.txtWeight.Text = "";
            this.txtKaatIn.Text = "";
            this.txtKarrat.Text = "";
            this.txtPurityWeight.Text = "";
            this.txtPure.Text = "";
            this.dgvStonesDetail.Rows.Clear();
            this.txtTpriceOfStones.Text = "";
            this.txtStoneWeight.Text = "";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.dgvBookedItem.SelectedRows.Count > 0)
            {
                string strTagNo = this.dgvBookedItem.SelectedRows[0].Cells[0].Value.ToString();
                dgvBookedItem.Rows.RemoveAt(this.dgvBookedItem.SelectedRows[0].Index);
                WorkerLineItem sl = new WorkerLineItem();
                foreach (WorkerLineItem sli in Wdm.WorkerLineItem)
                {
                    if (sli.WorkerDealing.WItemId == strTagNo)
                        sl = sli;
                }
                if (sl != null)
                    Wdm.RemoveLineItems(sl);
            }
            else
            {
                MessageBox.Show("Plz select any row to delete", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void cbxGroupItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWeight.Select();
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtWeight.Select();
            }
        }

        private void txtWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtKaatIn.Select();
            }
        }

        private void txtStone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtKaatIn.Select();
            }
        }

        private void txtKaatIn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtKarrat.Select();
            }
        }

        private void txtKarrat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtDescription.Select();
            }
        }

        private void cbxWorkerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Worker w = (Worker)(this.cbxWorkerName.SelectedItem);

                if (w == null)
                    return;
                else
                {
                    this.txtContactNo.Text = w.ContactNo.ToString();
                    this.txtMakingTola.Text = w.MakingTola.ToString();
                    this.txtCheejad.Text = w.Cheejad.ToString();
                    this.txtAddress.Text = w.Address.ToString();
                    this.txtGoldBalance.Text = w.GoldBalance.ToString("N3");
                    this.txtCashBalance.Text = w.CashBalance.ToString("0");
                    this.txtOpeningGold.Text = w.OpeningGold.ToString("N3");
                    t = Convert.ToInt32(w.ID);
                }
                this.cbxGroupItem.Select();
            }
        }

        private void txtGivenTotal_TextChanged(object sender, EventArgs e)
        {
            this.txtBalance.Text = ((this.txtGivenTotal.Text == "" ? 0 : Convert.ToDecimal(this.txtGivenTotal.Text)) - (this.txtReceieveTotal.Text == "" ? 0 : Convert.ToDecimal(this.txtReceieveTotal.Text))).ToString("0.000");
        }

        private void txtReceieveTotal_TextChanged(object sender, EventArgs e)
        {
            this.txtBalance.Text = ((this.txtGivenTotal.Text == "" ? 0 : Convert.ToDecimal(this.txtGivenTotal.Text)) - (this.txtReceieveTotal.Text == "" ? 0 : Convert.ToDecimal(this.txtReceieveTotal.Text))).ToString("0.000");
        }

        void ShowWorker()
        {
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
            this.cbxWorkerName.SelectedValue = wDAL.GetW0rkerId("select * from Worker where WorkerId = (select MAX(WorkerId) from Worker)");
            w = (Worker)this.cbxWorkerName.SelectedItem;
            this.txtContactNo.Text = w.ContactNo;
            this.txtAddress.Text = w.Address;
        }

        private void btnAddWorker_Click(object sender, EventArgs e)
        {
            ManageWorker frm = new ManageWorker();
            FormControls.FadeOut(this);
            frm.ShowDialog();
            FormControls.FadeIn(this);
            this.ShowWorker();
        }

        private void txtKarrat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 58) && e.KeyChar != 8 && e.KeyChar != '.')
                e.Handled = true;
            else
                e.Handled = false;
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                e.Handled = true;
            if (this.txtWeight.Text != string.Empty)
            {
                if (rbtKaat.Checked == true)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                        e.Handled = true;
                    else
                        e.Handled = false;

                    bool bFlag = false;
                    bFlag = this.KeyCheck(sender, e);
                    if (bFlag == true)
                        e.Handled = true;

                    // only allow one decimal point
                    if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
                        e.Handled = true;
                    if (e.Handled == false)
                    {
                        string str;
                        if (e.KeyChar == '\b')
                        {
                            if (this.txtKaatIn.Text == "")
                                return;
                            str = this.txtKaatIn.Text;
                            int i = str.Length;
                            str = str.Remove(i - 1);
                            if (str == string.Empty)
                            {
                                decimal val1 = 0;
                                frm.KaatInRattiforWrk(val1, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = ((Convert.ToDecimal(this.txtWeight.Text) / 24) * Convert.ToDecimal(val1)).ToString("N3");
                            }
                            else
                            {
                                if (str == ".")
                                {
                                    decimal val = 0;
                                    frm.KaatInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                    this.txtPure.Text = ((Convert.ToDecimal(this.txtWeight.Text) / 24) * Convert.ToDecimal(val)).ToString("N3");

                                }
                                else
                                {
                                    frm.KaatInRattiforWrk(Convert.ToDecimal(str), Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                    this.txtPure.Text = ((Convert.ToDecimal(this.txtWeight.Text) / 24) * Convert.ToDecimal(this.txtKarrat.Text)).ToString("N3");
                                }
                            }
                        }
                        else
                        {
                            str = this.txtKarrat.Text + e.KeyChar.ToString();
                            if (str == ".")
                            {
                                decimal val = 0;
                                frm.KaatInRattiforWrk(val, Convert.ToDecimal(this.txtWeight.Text), txtPurityWeight);
                                this.txtPure.Text = ((Convert.ToDecimal(this.txtWeight.Text) / 24) * Convert.ToDecimal(val)).ToString("N3");
                            }
                            else
                            {
                                decimal val = Convert.ToDecimal(str);
                                this.txtPure.Text = (((Convert.ToDecimal(this.txtWeight.Text) * Convert.ToDecimal(val)) / 24)).ToString();
                            }
                        }
                    }
                }
            }
        }

        private void rbtCashReceive_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Cash";
            this.panel4.Visible = false;
            this.pnlTMR.Visible = false;
            sTATUS = "CashReceive";
            this.panel9.Visible = false;
            this.rbtCashToGold.Checked = false;
            this.rbtGoldToCash.Checked = false;
        }

        private void rbtCashToGold_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Cash";
            this.label4.Text = "CashToGold";
            this.panel4.Visible = true;
            this.pnlTMR.Visible = true;
            sTATUS = "CashToGold";
            if (this.rbtCashToGold.Checked == true)
                this.rbtGoldToCash.Checked = false;
        }

        private void rbtGoldToCash_CheckedChanged(object sender, EventArgs e)
        {
            this.label12.Text = "Gold";
            this.label4.Text = "GoldToCash";
            this.panel4.Visible = true;
            this.pnlTMR.Visible = false;
            sTATUS = "GoldToCash";
            if (this.rbtGoldToCash.Checked == true)
                this.rbtCashToGold.Checked = false;
        }

        private void txtGoldRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola")
                    this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) / ((this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text)) / Formulas.WeightInGm)) * (decimal)11.664).ToString("0.000");
                if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                    this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) / (this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text))) * (decimal)11.664).ToString("0.000");
            }
        }

        private void txtCashGold_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.rbtCashToGold.Checked == true)
                {
                    if ((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola")
                        this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) / ((this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text)) / Formulas.WeightInGm))).ToString("0.000");
                    if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                        this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) / (this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text)))).ToString("0.000");
                }
                if (this.rbtGoldToCash.Checked == true)
                {
                    if ((GoldRatetype == "Standard" || GoldRatetype == "SonaPasa") && GramTolaRate == "Tola")
                        this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) * ((this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text)) / Formulas.WeightInGm))).ToString("0.000");
                    if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                        this.txtWeight.Text = (((this.txtCashGold.Text == "" ? 0 : Convert.ToDecimal(this.txtCashGold.Text)) * (this.txtGoldRate.Text == "" ? 0 : Convert.ToDecimal(this.txtGoldRate.Text)))).ToString("0.000");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddItem frmAddItem = new AddItem();
            frmAddItem.ShowDialog();
            FormControls.FillCombobox(cbxGroupItem, itmDAL.GetAllItems(), "ItemName", "ItemId");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageWorker frmAddItem = new ManageWorker();
            frmAddItem.ShowDialog();
            FormControls.FillCombobox(cbxWorkerName, wDAL.GetAllWorkers(), "Name", "ID");
        }

        private void rbtSonaPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSonaPasa.Checked)
            {
                this.txtGoldRate.Text = grs.SonaPasa.ToString("0");
            }
        }

        private void rbtPoundPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSonaPasa.Checked)
            {
                this.txtGoldRate.Text = grs.PoundPasa.ToString("0");
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmWorkerDealingReports frm = new frmWorkerDealingReports();
            frm.ShowDialog();
        }

        private void txtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnAddItem_Click(sender, e);
            }
        }
    }
}