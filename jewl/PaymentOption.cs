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
using System.IO;

namespace jewl
{
    public partial class PaymentOption : Form
    {
        int test;
        int sno;
        public List<Cheques> ListOfCheques;
        public List<CreditCard> listOfCreditCards;
        public List<Gold> ListOfPureGold = new List<Gold>();
        Bitmap bitmap;
        public List<Gold> ListOfUsedGold = new List<Gold>();
        JewelPictures jp = new JewelPictures();
        MemoryStream mst;
        SaleDAL sDAL = new SaleDAL();
        BankDAL bDAL = new BankDAL();
        Banks bnk = new Banks();
        BankAccount ba = new BankAccount();
        string ccbId = "";
        private Cheques chq;
        CreditCard crdt;
        Gold gold;
        string vno = "";
        Formulas frm = new Formulas();
        GoldRateDAL grDAL = new GoldRateDAL();
        GoldRates grs = new GoldRates();
        List<Gold> GoldList;
        int count = -1;
        public decimal sum = 0;
        // public decimal crsum = 0;
        decimal rate = 0;
        public string GramTolaRate = "";
        public string GoldRatetype = "";
        public PaymentOption()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.None;
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
        }
        public PaymentOption(string vno, int sno, int i, decimal sum)
            : this()
        {
            this.test = i;
            this.sno = sno;
            this.vno = vno;
            this.sum = sum;
        }
        //public PaymentOption(string vno, int sno, int i,decimal crsum)
        //    : this()
        //{
        //    this.test = i;
        //    this.sno = sno;
        //   //this.listOfCreditCards = lst;
        //   this.vno = vno;
        //   this.crsum = crsum;
        //}
        //public PaymentOption(string vno, int sno, int i, List<Gold> lst)
        //    : this()
        //{
        //    this.test = i;
        //    this.sno = sno;
        //    this.vno=vno;
        //    if (this.test == 3)
        //        this.ListOfPureGold = lst;
        //    else
        //        this.ListOfUsedGold = lst;
        //}
        //public PaymentOption(int i, List<Gold> lstused)
        //    : this()
        //{
        //    this.test = i;
        //    this.ListOfUsedGold = lstused;
        //}
        private void PaymentOption_Load(object sender, EventArgs e)
        {
            if (Main.City == "Islamabad")
            {
                this.pnlPasaRate.Visible = true;
                grs = grDAL.GetPasaRates(DateTime.Today);
            }
            if (test == 1)
            {
                this.tbcPaymentOption.SelectedTab = tbpCreditCard;
                count = this.listOfCreditCards.Count;
                for (int i = 0; i < count - 1; i++)
                {
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value = this.listOfCreditCards[i].MachineName.ToString();
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[1].Value = this.listOfCreditCards[i].Amount.ToString("0");
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[2].Value = this.listOfCreditCards[i].DeductRate.ToString("0.0");
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[3].Value = this.listOfCreditCards[i].SwapAmount.ToString("0");
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[4].Value = this.listOfCreditCards[i].BankName.ToString();
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[5].Value = this.listOfCreditCards[i].BankDeductRate.ToString("0.0");
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[6].Value = this.listOfCreditCards[i].AmountDepositeBank.ToString("0");
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[7].Value = this.listOfCreditCards[i].DepositInAccount.ToString();
                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value = this.listOfCreditCards[i].Description.ToString();
                }
            }
            else if (test == 2)
            {
                this.tbcPaymentOption.SelectedTab = tbpCheque;
                count = this.ListOfCheques.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    this.dgvChequePaymentDetail.Rows[i].Cells[2].Value = this.ListOfCheques[i].ChequeNo.ToString();
                    this.dgvChequePaymentDetail.Rows[i].Cells[1].Value = this.ListOfCheques[i].ChequeDate.ToString();
                    this.dgvChequePaymentDetail.Rows[i].Cells[3].Value = this.ListOfCheques[i].BankName.ToString();

                    //sum += chq.Amount;
                }
                // ListOfCheques = new List<Cheques>();
            }
            else if (test == 3)
            {
                this.tbcPaymentOption.SelectedTab = tbpGold;
                count = this.ListOfPureGold.Count;
                for (int i = 0; i < count - 1; i++)
                {
                    this.dgvGoldReceivedDetail.Rows[i].Cells[3].Value = this.ListOfPureGold[i].PWeight.ToString("0.000");
                }
            }
            else
            {
                this.tbcPaymentOption.SelectedTab = tbpGold;
                count = this.ListOfUsedGold.Count;
                for (int i = 0; i < count - 1; i++)
                {
                    this.dgvGoldReceivedDetail.Rows[i].Cells[3].Value = this.ListOfUsedGold[i].PWeight.ToString("0.000");
                }
            }

            this.dgvGoldReceivedDetail.Columns["colGoldType"].ReadOnly = true;

            FormControls.FillCombobox(cbxAccountNo, bDAL.GetAllBankAccount(), "AccountNo", "Id");
            FormControls.FillCombobox(cbxBank, bDAL.GetAllBanks(), "BankName", "Id");
            FormControls.FillCombobox(cbxBankName, bDAL.GetAllBanks(), "BankName", "Id");

            #region Fill Credit Card Data Grid
            if (this.listOfCreditCards != null)
            {
                int count = this.listOfCreditCards.Count;
                if (count >= 1)
                {
                    this.dgvCreditCardPaymentDetail.AutoGenerateColumns = false;
                    this.dgvCreditCardPaymentDetail.Rows.Clear();
                    this.dgvCreditCardPaymentDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value = this.listOfCreditCards[i].MachineName.ToString();
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[1].Value = this.listOfCreditCards[i].Amount.ToString("0");
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[2].Value = this.listOfCreditCards[i].DeductRate.ToString("0.0");
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[3].Value = this.listOfCreditCards[i].SwapAmount.ToString("0");
                        //
                        if (!(string.IsNullOrEmpty(this.listOfCreditCards[i].BankName.Id.ToString())))
                        {
                            string sId = this.listOfCreditCards[i].BankName.Id.ToString();
                            for (int j = 0; j < this.cbxBank.Items.Count; j++)
                            {
                                Banks bnk = (Banks)cbxBank.Items[j];
                                if (sId.Equals(bnk.Id.ToString()))
                                {
                                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[4].Value = Convert.ToInt32(bnk.Id);
                                    this.cbxBankAccount.DataSource = bDAL.GetAllBankAccountByBankId(bnk.Id);
                                    this.cbxBankAccount.DisplayMember = "AccountNo";
                                    this.cbxBankAccount.ValueMember = "Id";
                                    this.dgvCreditCardPaymentDetail.Rows[i].Cells[7].Value = Convert.ToInt32(this.listOfCreditCards[i].DepositInAccount.Id);
                                }
                            }
                        }
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[5].Value = this.listOfCreditCards[i].BankDeductRate.ToString("0.0");
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[6].Value = this.listOfCreditCards[i].AmountDepositeBank.ToString("0");
                        this.dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value = this.listOfCreditCards[i].Description.ToString();
                    }
                }
            }
            #endregion
            #region Fill Cheque Data Grid
            if (this.ListOfCheques != null)
            {
                int count = this.ListOfCheques.Count;
                if (count >= 1)
                {
                    this.dgvChequePaymentDetail.AutoGenerateColumns = false;
                    this.dgvChequePaymentDetail.Rows.Clear();
                    this.dgvChequePaymentDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvChequePaymentDetail.Rows[i].Cells[0].Value = this.ListOfCheques[i].Amount.ToString("0");
                        this.dgvChequePaymentDetail.Rows[i].Cells[1].Value = this.ListOfCheques[i].ChequeDate.ToString("MM-dd-yyyy");
                        this.dgvChequePaymentDetail.Rows[i].Cells[2].Value = this.ListOfCheques[i].ChequeNo.ToString();
                        string sId = this.ListOfCheques[i].BankName.Id.ToString();
                        for (int j = 0; j < this.cbxBankName.Items.Count; j++)
                        {
                            Banks bnk = (Banks)this.cbxBankName.Items[j];
                            if (sId.Equals(bnk.Id.ToString()))
                            {
                                this.dgvChequePaymentDetail.Rows[i].Cells[3].Value = Convert.ToInt32(this.ListOfCheques[i].BankName.Id);
                                this.cbxAccountNo.DataSource = bDAL.GetAllBankAccountByBankId(bnk.Id);
                                this.cbxAccountNo.DisplayMember = "AccountNo";
                                this.cbxAccountNo.ValueMember = "Id";
                                this.dgvChequePaymentDetail.Rows[i].Cells[4].Value = Convert.ToInt32(this.ListOfCheques[i].DepositInAccount.Id);
                                break;
                            }
                        }
                        this.dgvChequePaymentDetail.Rows[i].Cells[5].Value = this.ListOfCheques[i].Description.ToString();
                    }
                }
            }
            #endregion
            #region Fill Gold Data Grid
            if (this.ListOfUsedGold != null)
            {
                int count = this.ListOfUsedGold.Count;
                if (count >= 1)
                {
                    this.dgvGoldReceivedDetail.AutoGenerateColumns = false;
                    this.dgvGoldReceivedDetail.Rows.Clear();
                    this.dgvGoldReceivedDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        GoldType gt = this.ListOfUsedGold[i].GoldType;
                        if (gt == GoldType.Pure)
                            this.dgvGoldReceivedDetail.Rows[i].Cells[0].Value = "Pure";
                        else
                            this.dgvGoldReceivedDetail.Rows[i].Cells[0].Value = "Used";

                        this.dgvGoldReceivedDetail.Rows[i].Cells[1].Value = this.ListOfUsedGold[i].Weight.ToString("0.000");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[2].Value = this.ListOfUsedGold[i].Kaat.ToString("0.0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[3].Value = this.ListOfUsedGold[i].PWeight.ToString("0.000");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[4].Value = this.ListOfUsedGold[i].Karat.ToString();
                        this.dgvGoldReceivedDetail.Rows[i].Cells[5].Value = this.ListOfUsedGold[i].Rate.ToString("0.0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value = this.ListOfUsedGold[i].Amount.ToString("0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[7].Value = this.ListOfUsedGold[i].Description.ToString();

                    }
                }
            }
            if (this.ListOfPureGold != null)
            {
                int count = this.ListOfPureGold.Count;
                if (count >= 1)
                {
                    this.dgvGoldReceivedDetail.AutoGenerateColumns = false;
                    this.dgvGoldReceivedDetail.Rows.Clear();
                    this.dgvGoldReceivedDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        GoldType gt = this.ListOfPureGold[i].GoldType;
                        if (gt == GoldType.Pure)
                            this.dgvGoldReceivedDetail.Rows[i].Cells[0].Value = "Pure";
                        else
                            this.dgvGoldReceivedDetail.Rows[i].Cells[0].Value = "Used";

                        this.dgvGoldReceivedDetail.Rows[i].Cells[1].Value = this.ListOfPureGold[i].Weight.ToString("0.000");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[2].Value = this.ListOfPureGold[i].Kaat.ToString("0.0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[3].Value = this.ListOfPureGold[i].PWeight.ToString("0.000");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[4].Value = this.ListOfPureGold[i].Karat.ToString();
                        this.dgvGoldReceivedDetail.Rows[i].Cells[5].Value = this.ListOfPureGold[i].Rate.ToString("0.0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value = this.ListOfPureGold[i].Amount.ToString("0");
                        this.dgvGoldReceivedDetail.Rows[i].Cells[7].Value = this.ListOfPureGold[i].Description.ToString();

                    }
                }
            }

            #endregion
            GramTolaRate = sDAL.GetStartupGramTolaRate();
            GoldRatetype = sDAL.GetStartupGoldRateType();
        }

        #region Gold tab
        private void dgvGoldReceivedDetail_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            // MessageBox.Show(e.RowIndex.ToString ());
            if (test == 3)
            {
                dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[0].Value = "Pure";
                dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                dgvGoldReceivedDetail.CellLeave += new DataGridViewCellEventHandler(dgvGoldReceivedDetail_CellLeave);
            }
            else
            {
                dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[0].Value = "Used";
                dgvGoldReceivedDetail.CellLeave -= new DataGridViewCellEventHandler(dgvGoldReceivedDetail_CellLeave);
            }
        }
        #endregion

        public string _textBox1
        {
            get { return txtTotalChequePaymentDetail.Text; }
        }

        #region Credit Card
        private void dgvCreditCardPaymentDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvCreditCardPaymentDetail.CurrentCell.ColumnIndex == 4 || this.dgvCreditCardPaymentDetail.CurrentCell.ColumnIndex == 7)
            {
                ComboBox cmbBox = e.Control as ComboBox;
                cmbBox.SelectedIndexChanged -= new EventHandler(cbxBanks_SelectedIndexChanged);
                if (this.dgvCreditCardPaymentDetail.CurrentCell.ColumnIndex == 4)
                {
                    cmbBox.SelectedIndexChanged += new EventHandler(cbxBanks_SelectedIndexChanged);
                }
                if (this.dgvCreditCardPaymentDetail.CurrentCell.ColumnIndex != 4)
                {
                    cmbBox.SelectedIndexChanged -= new EventHandler(cbxBanks_SelectedIndexChanged);
                }
            }
        }
        private void cbxBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgvCreditCardPaymentDetail.CurrentCell.ColumnIndex == 4)
            {
                if (((ComboBox)sender).SelectedValue != null)
                {
                    Banks bn = (Banks)((ComboBox)sender).SelectedItem;
                    ccbId = bn.Id.ToString();
                    if (!(string.IsNullOrEmpty(ccbId)))
                    {
                        this.cbxBankAccount.DataSource = bDAL.GetAllBankAccountByBankId(Convert.ToInt32(ccbId));
                        this.cbxBankAccount.DisplayMember = "AccountNo";
                        this.cbxBankAccount.ValueMember = "Id";
                    }
                }
            }
        }

        #endregion

        #region Cheque
        private void dgvChequePaymentDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvChequePaymentDetail.CurrentCell.ColumnIndex == 3)
            {
                ComboBox cmbBox = e.Control as ComboBox;
                cmbBox.SelectedIndexChanged -= new EventHandler(cmbBoxBank_SelectedIndexChanged);
                if (this.dgvChequePaymentDetail.CurrentCell.ColumnIndex == 3)
                    cmbBox.SelectedIndexChanged += new EventHandler(cmbBoxBank_SelectedIndexChanged);
                if (this.dgvChequePaymentDetail.CurrentCell.ColumnIndex != 3)
                    cmbBox.SelectedIndexChanged -= new EventHandler(cmbBoxBank_SelectedIndexChanged);
            }
        }

        private void cmbBoxBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgvChequePaymentDetail.CurrentCell.ColumnIndex == 3)
            {
                if (((ComboBox)sender).SelectedValue != null)
                {
                    Banks b = (Banks)((ComboBox)sender).SelectedItem;
                    this.cbxAccountNo.DataSource = bDAL.GetAllBankAccountByBankId(b.Id);
                    this.cbxAccountNo.DisplayMember = "AccountNo";
                    this.cbxAccountNo.ValueMember = "Id";
                }
            }
        }
        #endregion

        #region Gold Selected index changed
        private void dgvGoldReceivedDetail_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 4)
            {
                ComboBox cmb = e.Control as ComboBox;
                if (cmb != null)
                {
                    cmb.SelectedIndexChanged += new EventHandler(cmbGold_SelectedIndexChanged);
                }
            }
        }
        private void cmbGold_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 4)
            {
                if (((ComboBox)sender).SelectedValue != null)
                {
                    string sId = ((ComboBox)sender).SelectedValue.ToString();
                }
            }
        }
        #endregion

        #region dgv Credit Card cell updation
        private void dgvCreditCardPaymentDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal amount = 0;
            decimal rate = 0;
            if ((string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[1].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[1].Value == "" || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[1].Value == null)
                amount = 0;
            else
                amount = Math.Round(Convert.ToDecimal(this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[1].Value.ToString()), 0);
            if ((string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[2].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[2].Value == "" || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[2].Value == null)
            {
                rate = 0;
                this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value = string.Empty;
            }
            else
            {
                rate = Math.Round(Convert.ToDecimal(this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[2].Value.ToString()), 1);
                this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value = this.updateSwapAmoun(amount, rate).ToString("0");
                if ((string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[5].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[5].Value == "" || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[5].Value == null)
                {
                    this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[6].Value = string.Empty;
                }
                else
                {

                    decimal val = Convert.ToDecimal(this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                    decimal bRate = decimal.Parse(this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[5].Value.ToString());

                    this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[6].Value = this.updateDepositAmmount(val, bRate).ToString("0");
                }
            }
            if (!(string.IsNullOrEmpty(ccbId)))
            {
                decimal val = 0;
                Banks b = bDAL.SearchBank(Convert.ToInt32(ccbId));
                this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[5].Value = b.DRate.ToString("0.0");
                if ((string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value == "" || (string)this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value == null)
                {
                    val = 0;
                }
                else
                {
                    val = Convert.ToDecimal(this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                    this.dgvCreditCardPaymentDetail.Rows[e.RowIndex].Cells[6].Value = this.updateDepositAmmount(val, b.DRate).ToString("0");
                }
            }
        }

        private decimal updateSwapAmoun(decimal amount, decimal rate)
        {
            decimal swapAmount = Math.Round((amount + ((amount * rate) / 100)), 0);
            return swapAmount;
        }

        private decimal updateDepositAmmount(decimal amount, decimal rate)
        {
            decimal depositAmount = Math.Round((amount - ((amount * rate) / 100)), 0);
            return depositAmount;
        }

        #endregion

        #region dgv Gold Ammount Calculation
        private void dgvGoldReceivedDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            decimal pureW = 0;
            decimal ammount = 0;

            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 2)
            {
                decimal weight = 0;
                decimal kaat = 0;

                if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == "")
                    weight = 0;
                else
                    weight = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value.ToString());
                if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == "")
                    kaat = 0;
                else
                    kaat = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value.ToString());
                pureW = this.frm.PureWeight(kaat, weight);
                this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value = pureW.ToString("0.000");
            }
            if (Main.City == "Islamabad")
            {
                try
                {
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    if (rbtPoundPasa.Checked)
                    {
                        rate = Convert.ToDecimal(grs.PoundPasa);
                        if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex != 5)
                        {
                            this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                        }
                    }
                    if (rbtSonaPasa.Checked)
                    {
                        rate = Convert.ToDecimal(grs.SonaPasa);
                        if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex != 5)
                        {
                            this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                        }
                    }
                    pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                    ammount = Math.Round((pureW * (rate / Formulas.WeightInGm)), 0);

                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = ammount.ToString("0");
                }
                catch
                {


                }
            }
            else
            {
                if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 4)
                {
                    if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == "")
                    {
                        rate = 0;

                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = string.Empty;
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = string.Empty;
                        string str = this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value.ToString();
                        rate = grDAL.GetRateByKarat(str, DateTime.Today);
                        pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                        ammount = Math.Round((pureW * rate), 0);
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = ammount.ToString("0");
                    }
                    else if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value == "")
                    {
                        string str = this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value.ToString();
                        if (GoldRatetype == "Standard" && GramTolaRate == "Tola")
                        {
                            rate = grDAL.GetRateByKaratTola(str, DateTime.Today);
                            pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                            ammount = Math.Round((pureW * (rate / Formulas.WeightInGm)), 0);
                        }
                        else if (GoldRatetype == "Standard" && GramTolaRate == "Gram")
                        {
                            rate = grDAL.GetRateByKarat(str, DateTime.Today);
                            pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                            ammount = Math.Round((pureW * rate), 0);
                        }

                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = ammount.ToString("0");
                    }
                    else
                    {
                        try
                        {
                            decimal pureW1 = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                            decimal rate = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value.ToString());
                            this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = Math.Round((pureW1 * rate), 0).ToString("0");
                        }
                        catch
                        { }
                    }
                }
            }
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 5)
            {
                try
                {
                    if (Main.City != "Islamabad")
                    {
                        decimal pureW1 = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                        decimal rate = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value.ToString());
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = Math.Round((pureW1 * rate), 0).ToString("0");
                    }
                    else
                    {
                        decimal pureW1 = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                        decimal rate = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value.ToString());
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = Math.Round((pureW1 * (rate / Formulas.WeightInGm)), 0).ToString("0");
                    }
                }
                catch
                { }
            }
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 6)
            {
                this.txtTotalGoldReceivedDetail.Text = Math.Round((this.UpdateTextBox()), 0).ToString();
            }
        }

        private decimal UpdateTextBox()
        {
            decimal sum = 0;
            for (int i = 0; i < this.dgvGoldReceivedDetail.Rows.Count - 1; i++)
            {
                decimal amount = 0;

                if ((string)this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value == "" || (string)this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value == null)
                    amount = 0;
                else
                    amount = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[i].Cells[6].Value.ToString());
                sum += amount;
            }
            return sum;
        }
        #endregion

        private void btnRemoveGoldReceivedDetail_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = this.dgvGoldReceivedDetail.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    this.dgvGoldReceivedDetail.Rows.Remove(dgvGoldReceivedDetail.Rows[i]);
                }
            }
            this.txtTotalGoldReceivedDetail.Text = Math.Round((this.UpdateTextBox()), 0).ToString();
        }

        private void dgvGoldReceivedDetail_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            decimal pureW = 0;
            decimal ammount = 0;
            string str = "24";
            this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value = str;
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 2)
            {
                decimal weight = 0;
                decimal kaat = 0;

                if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value == "")
                    weight = 0;
                else
                    weight = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value.ToString());
                if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value == "")
                    kaat = 0;
                else
                    kaat = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[2].Value.ToString());
                pureW = this.frm.PureWeight(kaat, weight);
                this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value = pureW.ToString("0.000");
            }
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 4)
            {
                if ((string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == string.Empty || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == null || (string)this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].Value == "")
                {
                    rate = 0;
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = string.Empty;
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = string.Empty;
                }
                else
                {
                    rate = grDAL.GetRateByKarat(str, DateTime.Today);
                    pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                    ammount = Math.Round((pureW * rate), 0);
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 1).ToString();
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = ammount.ToString("0");
                }
            }
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 6)
            {
                this.txtTotalGoldReceivedDetail.Text = Math.Round((this.UpdateTextBox()), 0).ToString("0.000");
            }
            if (this.dgvGoldReceivedDetail.CurrentCell.ColumnIndex == 6)
            {
                if (this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value != null && this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value != null)
                {
                    decimal val, val1;
                    val = Convert.ToDecimal(FormControls.StringFormate(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value.ToString()));
                    val1 = Convert.ToDecimal(FormControls.StringFormate(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value.ToString()));
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round((val1 / val), 0);
                }
            }
            if (Main.City == "Islamabad")
            {
                try
                {
                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                    if (rbtPoundPasa.Checked)
                    {
                        rate = Convert.ToDecimal(grs.PoundPasa);
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                    }
                    if (rbtSonaPasa.Checked)
                    {
                        rate = Convert.ToDecimal(grs.SonaPasa);
                        this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(rate, 0).ToString();
                    }
                    pureW = Convert.ToDecimal(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString());
                    ammount = Math.Round((pureW * (rate / Formulas.WeightInGm)), 0);

                    this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value = ammount.ToString("0");
                }
                catch
                {


                }
            }
        }

        private void rbtPoundPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtPoundPasa.Checked)
            {
                rate = Convert.ToDecimal(grs.PoundPasa / Formulas.WeightInGm);
            }
        }

        private void rbtSonaPasa_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtSonaPasa.Checked)
            {
                rate = Convert.ToDecimal(grs.SonaPasa / Formulas.WeightInGm);
            }
        }

        private void dgvGoldReceivedDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void browse_Click(object sender, EventArgs e)
        {
            this.FillPictureBox();
        }

        private void FillPictureBox()
        {
            if (this.pbxMain.Image != null)
            {
                {
                    MessageBox.Show("No more space available for picture", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    return;
                }
            }
            else
            {
                DialogResult result = openFileDialog1.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    Image img = Image.FromFile(this.openFileDialog1.FileName);
                    bitmap = new Bitmap(img);
                    this.pbxMain.Image = bitmap;
                    this.pbxMain.BorderStyle = BorderStyle.Fixed3D;
                }
            }
        }

        private void PaymentOption_Paint(object sender, PaintEventArgs e)
        {
            FormControls.FormBorder(sender, e);
        }

        private void btnRemoveChequePaymentDetail_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = this.dgvChequePaymentDetail.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    this.dgvChequePaymentDetail.Rows.Remove(dgvChequePaymentDetail.Rows[i]);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tbcPaymentOption.SelectedTab == tbpCreditCard)
            {
                ManageSale frm = this.Owner as ManageSale;
                ManageOrder frm1 = this.Owner as ManageOrder;
                ManageSilverSale frm2 = this.Owner as ManageSilverSale;
                if (!(dgvCreditCardPaymentDetail.Rows.Count < -1))
                {
                    this.listOfCreditCards.Clear();

                    listOfCreditCards = new List<CreditCard>();
                    for (int i = 0; i < dgvCreditCardPaymentDetail.Rows.Count - 1; i++)
                    {
                        crdt = new CreditCard();
                        if ((string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value == null || (string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value == "")
                        {
                            crdt.MachineName = "";
                        }
                        else
                            crdt.MachineName = dgvCreditCardPaymentDetail.Rows[i].Cells[0].Value.ToString();
                        crdt.Amount = Convert.ToDecimal(dgvCreditCardPaymentDetail.Rows[i].Cells[1].Value.ToString());
                        crdt.DeductRate = Convert.ToDecimal(dgvCreditCardPaymentDetail.Rows[i].Cells[2].Value.ToString());
                        crdt.SwapAmount = Convert.ToDecimal(dgvCreditCardPaymentDetail.Rows[i].Cells[3].Value.ToString());
                        string id = dgvCreditCardPaymentDetail.Rows[i].Cells[4].Value.ToString();
                        crdt.BankName = new Banks();
                        crdt.BankName.Id = Convert.ToInt32(id);
                        if (string.IsNullOrEmpty(id))
                        {
                            crdt.BankName = null;
                        }
                        else
                            crdt.BankName = bDAL.SearchBank(Convert.ToInt32(id));
                        crdt.BankDeductRate = Convert.ToDecimal(dgvCreditCardPaymentDetail.Rows[i].Cells[5].Value.ToString());
                        crdt.AmountDepositeBank = Convert.ToDecimal(dgvCreditCardPaymentDetail.Rows[i].Cells[6].Value.ToString());
                        int aid = Convert.ToInt32(dgvCreditCardPaymentDetail.Rows[i].Cells[7].Value.ToString());
                        string str = aid.ToString();
                        if (string.IsNullOrEmpty(str))
                        {
                            MessageBox.Show("You Must Enter Account NO", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }

                        else
                            crdt.DepositInAccount = bDAL.SearchBankAccount(aid);
                        if ((string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value == string.Empty || (string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value == null || (string)this.dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value == "")
                            crdt.Description = "";
                        else
                            crdt.Description = dgvCreditCardPaymentDetail.Rows[i].Cells[8].Value.ToString();
                        ChildAccount cha = new ChildAccount();
                        cha = crdt.DepositInAccount.AccountCode;
                        crdt.AccountCode = cha.ChildCode;
                        crdt.VNO = this.vno;
                        crdt.SNO = sno;
                        crdt.Status = "Sale";
                        listOfCreditCards.Add(crdt);
                    }
                    if (frm != null)
                        frm.ListOfCreditCard = this.listOfCreditCards;
                    else if (frm1 != null)
                        frm1.ListOfCreditCard = this.listOfCreditCards;
                    else if (frm2 != null)
                        frm2.ListOfCreditCard = this.listOfCreditCards;
                    sum = 0;
                    foreach (CreditCard ct in this.listOfCreditCards)
                    {
                        sum += ct.Amount;
                    }
                    this.Close();
                }
                else
                    sum = 0;
            }
            else if (tbcPaymentOption.SelectedTab == tbpCheque)
            {
                ManageSale frm = this.Owner as ManageSale;
                ManageOrder frm1 = this.Owner as ManageOrder;
                ManageSilverSale frm2 = this.Owner as ManageSilverSale;
                if (!(dgvChequePaymentDetail.Rows.Count < -1))
                {
                    ListOfCheques = new List<Cheques>();

                    for (int i = 0; i < dgvChequePaymentDetail.Rows.Count - 1; i++)
                    {
                        chq = new Cheques();
                        chq.Amount = Convert.ToDecimal(dgvChequePaymentDetail.Rows[i].Cells[0].Value.ToString());
                        try
                        {
                            chq.ChequeDate = Convert.ToDateTime(dgvChequePaymentDetail.Rows[i].Cells[1].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            return;
                        }
                        chq.ChequeNo = dgvChequePaymentDetail.Rows[i].Cells[2].Value.ToString();
                        string id = dgvChequePaymentDetail.Rows[i].Cells[3].Value.ToString();
                        if (string.IsNullOrEmpty(id))
                            crdt.BankName = null;
                        else
                            chq.BankName = bDAL.SearchBank(Convert.ToInt32(id));

                        int aid = Convert.ToInt32(dgvChequePaymentDetail.Rows[i].Cells[4].Value);

                        if (string.IsNullOrEmpty(aid.ToString()))
                        {
                            MessageBox.Show("You Must Enter Account NO", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                        else
                            chq.DepositInAccount = bDAL.SearchBankAccount(aid);

                        if (dgvChequePaymentDetail.Rows[i].Cells[5].Value == null)
                            chq.Description = "";
                        else
                            chq.Description = dgvChequePaymentDetail.Rows[i].Cells[5].Value.ToString();

                        ChildAccount cha = chq.DepositInAccount.AccountCode;

                        chq.VNO = vno + sno.ToString();
                        chq.SNO = sno;
                        chq.Status = "ok";
                        this.ListOfCheques.Add(chq);
                    }
                    if (frm != null)
                        frm.ListOfChecks = this.ListOfCheques;
                    else if (frm1 != null)
                        frm1.ListOfCheques = this.ListOfCheques;
                    else if (frm2 != null)
                        frm2.ListOfChecks = this.ListOfCheques;
                    sum = 0;
                    foreach (Cheques chs in this.ListOfCheques)
                    {
                        sum += chq.Amount;
                    }
                }
                else
                    sum = 0;
                this.Close();
            }
            else if (tbcPaymentOption.SelectedTab == tbpGold)
            {
                ManageSale frm = this.Owner as ManageSale;
                ManageOrder frm1 = this.Owner as ManageOrder;
                ManageSilverSale frm2 = this.Owner as ManageSilverSale;
                if (!(dgvGoldReceivedDetail.Rows.Count < -1))
                {
                    if (test == 4)
                    {
                        ListOfUsedGold = new List<Gold>();
                        for (int i = 0; i < dgvGoldReceivedDetail.Rows.Count - 1; i++)
                        {
                            gold = new Gold();
                            string str = dgvGoldReceivedDetail.Rows[i].Cells[0].Value.ToString();
                            GoldType gt;
                            if (str.Equals("Pure"))
                                gt = GoldType.Pure;
                            else
                                gt = GoldType.Used;

                            gold.GoldType = gt;
                            gold.Weight = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[1].Value.ToString());
                            if ((string)this.dgvGoldReceivedDetail.Rows[i].Cells[2].Value == string.Empty || this.dgvGoldReceivedDetail.Rows[i].Cells[2].Value == null)
                                gold.Kaat = 0;
                            else
                                gold.Kaat = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[2].Value.ToString());
                            gold.PWeight = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[3].Value.ToString());
                            if (Main.City == "Islamabad")
                                gold.Karat = "";
                            else
                                if ((string)this.dgvGoldReceivedDetail.Rows[i].Cells[4].Value == string.Empty || this.dgvGoldReceivedDetail.Rows[i].Cells[4].Value == null)
                                    gold.Karat = "0";
                                else
                                    gold.Karat = dgvGoldReceivedDetail.Rows[i].Cells[4].Value.ToString();
                            gold.Rate = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[5].Value.ToString());
                            gold.Amount = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[6].Value.ToString());
                            if (dgvGoldReceivedDetail.Rows[i].Cells[7].Value == null)
                            { gold.Description = ""; }
                            else
                                gold.Description = dgvGoldReceivedDetail.Rows[i].Cells[7].Value.ToString();
                            gold.RemainingWork = gold.Description;
                            gold.JewelPictures = new JewelPictures();
                            if (pbxMain.Image != null)
                            {
                                mst = new MemoryStream();

                                gold.JewelPictures.ImageMemory = jp.ConvertImageToBinary(this.pbxMain.Image);
                            }
                            else
                            {
                                gold.JewelPictures.ImageMemory = null;
                            }
                            gold.PGDate = DateTime.Now;
                            gold.VNO = str + sno.ToString();
                            gold.SNO = sno;
                            if (gold.GoldType == GoldType.Used)
                                this.ListOfUsedGold.Add(gold);
                        }
                        if (frm != null)
                            frm.ListOfUsedGold = this.ListOfUsedGold;
                        else if (frm1 != null)
                            frm1.ListOfUsedGold = this.ListOfUsedGold;
                        else if (frm2 != null)
                            frm2.ListOfUsedGold = this.ListOfUsedGold;
                    }
                    if (test == 3)
                    {
                        ListOfPureGold = new List<Gold>();
                        for (int i = 0; i < dgvGoldReceivedDetail.Rows.Count - 1; i++)
                        {
                            gold = new Gold();
                            string str = dgvGoldReceivedDetail.Rows[i].Cells[0].Value.ToString();
                            GoldType gt;
                            if (str.Equals("Pure"))
                                gt = GoldType.Pure;
                            else
                                gt = GoldType.Used;

                            gold.GoldType = gt;
                            gold.Weight = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[1].Value.ToString());
                            if (dgvGoldReceivedDetail.Rows[i].Cells[2].Value != null)
                                gold.Kaat = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[2].Value.ToString());
                            else
                                gold.Kaat = 0;
                            gold.PWeight = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[3].Value.ToString());
                            gold.Karat = dgvGoldReceivedDetail.Rows[i].Cells[4].Value.ToString();
                            gold.Rate = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[5].Value.ToString());
                            gold.Amount = Convert.ToDecimal(dgvGoldReceivedDetail.Rows[i].Cells[6].Value.ToString());
                            if (dgvGoldReceivedDetail.Rows[i].Cells[7].Value == null)
                            { gold.Description = ""; }
                            else
                                gold.Description = dgvGoldReceivedDetail.Rows[i].Cells[7].Value.ToString();
                            gold.RemainingWork = gold.Description;
                            gold.PGDate = DateTime.Now;
                            gold.RemainingWork = gold.Description;
                            gold.VNO = str + sno.ToString();
                            gold.SNO = sno;

                            if (gold.GoldType == GoldType.Pure)
                                this.ListOfPureGold.Add(gold);
                        }
                        if (frm != null)
                            frm.ListOfPureGold = this.ListOfPureGold;
                        else if (frm1 != null)
                            frm1.ListOfPureGold = this.ListOfPureGold;
                        else if (frm2 != null)
                            frm2.ListOfPureGold = this.ListOfPureGold;
                    }
                    if (test == 3)
                    {
                        sum = 0;
                        foreach (Gold gld in this.ListOfPureGold)
                        {
                            sum += gld.Amount;
                        }
                    }
                    if (test == 4)
                    {
                        sum = 0;
                        foreach (Gold gld in this.ListOfUsedGold)
                        {
                            sum += gld.Amount;
                        }
                    }
                }
                else
                    sum = 0;
                this.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (tbcPaymentOption.SelectedTab == tbpCreditCard)
            {
                if (listOfCreditCards.Count == 0)
                    sum = 0;
            }
            else if (tbcPaymentOption.SelectedTab == tbpCheque)
            {
                if (ListOfCheques.Count == 0)
                    sum = 0;
            }
            else if (tbcPaymentOption.SelectedTab == tbpGold)
            {
                if (ListOfPureGold.Count == 0 && test == 3)
                    sum = 0;
                if (ListOfUsedGold.Count == 0 && test == 4)
                    sum = 0;
            }
            this.Close();
        }

        private void dgvGoldReceivedDetail_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[1].Value != null && this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value != null)
            {
                this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[5].Value = Math.Round(Convert.ToDecimal(FormControls.StringFormate(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[6].Value.ToString())) / (Convert.ToDecimal(FormControls.StringFormate(this.dgvGoldReceivedDetail.Rows[e.RowIndex].Cells[3].Value.ToString()))), 0);
            }
        }

        private void dgvCreditCardPaymentDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void btnRemoveCreditCardPaymentDetail_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = this.dgvCreditCardPaymentDetail.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                for (int i = 0; i < selectedRowCount; i++)
                {
                    this.dgvCreditCardPaymentDetail.Rows.Remove(dgvCreditCardPaymentDetail.Rows[i]);
                }
            }
        }
    }
}
