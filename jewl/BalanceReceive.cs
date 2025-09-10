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
    public partial class BalanceReceive : Form
    {
        int keyCheck = 0;
        SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        SqlTransaction trans;
        private Voucher pv;
        private Voucher custv;
        int searchNo, dis = 0;
        Sale sale;
        List<SalePayment> salePay, osalePay;
        List<Gold> goldDet, ogoldDet;
        Customer cust;
        PaymentsDAL payDAL = new PaymentsDAL();
        CustomerDAL custDAL = new CustomerDAL();
        AccountDAL acDAL = new AccountDAL();
        ChildAccount ca = new ChildAccount();
        bool isOrder = false;
        //private AccountDAL acDAL = new AccountDAL();
        private VouchersDAL vDAL = new VouchersDAL();
        CustomerDAL cstDAL = new CustomerDAL();
        SaleDAL saleDAL = new SaleDAL();
        BankDAL bDAL = new BankDAL();
        Formulas frm = new Formulas();
        GoldRateDAL grDAL = new GoldRateDAL();
        GoldRates grs = new GoldRates();
        decimal TReceived = 0;
        private decimal ExtraMoney = 0;
        SaleManDAL slmDAL = new SaleManDAL();
        bool eflag = false;
        public BalanceReceive()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.rbtSaleNo.Checked == true)
            {
                this.RefreshForm();
                if (this.txtSearch.Text == "")
                    MessageBox.Show("Please enter the sale no", Messages.Header);
                else
                {
                    this.ShowSearchRec(FormControls.GetIntValue(this.txtSearch));
                }
            }
            if (this.rbtOrderNo.Checked == true)
            {
                this.RefreshForm();
                if (this.txtSearch.Text == "")
                    MessageBox.Show("Please enter the order no", Messages.Header);
                else
                {
                    this.ShowSearchRecByOrderNo(FormControls.GetIntValue(this.txtSearch));
                }
            }
            if (this.rbtCustomerName.Checked == true)
            {
                this.RefreshForm();
                string str = this.txtSearch.Text.Trim();
                decimal num;
                bool isNum = decimal.TryParse(str, out num);
                if (isNum)
                {
                    MessageBox.Show("Please enter search criteria in charaters", Messages.Header);
                    return;
                }
                else
                {
                    if (this.txtSearch.Text == "")
                    {
                        SearchResult serRes = new SearchResult();
                        serRes.ShowDialog();
                    }
                    else
                    {
                        bool result = false;
                        result = DateDAL.IsExist("select Name from CustomerInfo where Name like '%" + this.txtSearch.Text.ToString() + "%'");
                        if (result == true)
                        {
                            SearchResult serRes = new SearchResult();
                            serRes.serStr = this.txtSearch.Text.ToString();
                            serRes.ShowDialog();
                            this.RefreshForm();
                            searchNo = (int)serRes.SearchNo;
                            isOrder = (bool)serRes.isOrder;
                            if (searchNo == 0)
                                return;
                            else
                            {
                                if (isOrder == false)
                                {
                                    this.ShowSearchRec(searchNo);
                                }
                                else
                                {
                                    this.ShowSearchRecByOrderNo(searchNo);

                                  
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Customer found", Messages.Header);
                            return;
                        }
                    }
                }
            }
            if (this.rbtContactNo.Checked == true)
            {
                this.RefreshForm();
                if (this.txtSearch.Text == "")
                {
                    SearchResult serRes = new SearchResult();
                    serRes.ShowDialog();
                }
                else
                {
                    bool result = false;
                    bool result1 = false;
                    result = DateDAL.IsExist("select Mobile from CustomerInfo where Mobile like '%" + this.txtSearch.Text.ToString() + "%'");
                    result1 = DateDAL.IsExist("select TelHome from CustomerInfo where TelHome like '%" + this.txtSearch.Text.ToString() + "%'");
                    if (result == true)
                    {
                        SearchResult serRes = new SearchResult();
                        serRes.serStr1 = this.txtSearch.Text.ToString();
                        serRes.ShowDialog();
                        this.RefreshForm();
                        searchNo = (int)serRes.SearchNo;
                        this.ShowSearchRec(searchNo);
                    }
                    else if (result1 == true)
                    {
                        SearchResult serRes = new SearchResult();
                        serRes.serStr2 = this.txtSearch.Text.ToString();
                        serRes.ShowDialog();
                        this.RefreshForm();
                        searchNo = (int)serRes.SearchNo;
                        this.ShowSearchRec(searchNo);
                    }
                    else
                    {
                        MessageBox.Show("No Customer found", Messages.Header);
                        return;
                    }
                }

            }
            if (this.rbtEmailAddress.Checked == true)
            {
                this.RefreshForm();
                if (this.txtSearch.Text == "")
                {
                    SearchResult serRes = new SearchResult();
                    serRes.ShowDialog();
                }
                else
                {
                    bool result = false;
                    result = DateDAL.IsExist("select Email from CustomerInfo where Email like '%" + this.txtSearch.Text.ToString() + "%'");
                    if (result == true)
                    {
                        SearchResult serRes = new SearchResult();
                        serRes.serStr4 = this.txtSearch.Text.ToString();
                        serRes.ShowDialog();
                        this.RefreshForm();
                        searchNo = (int)serRes.SearchNo;
                        this.ShowSearchRec(searchNo);
                    }
                    else
                    {
                        MessageBox.Show("No Customer found", Messages.Header);
                        return;
                    }
                }
            }
            if (this.rbtAddress.Checked == true)
            {
                this.RefreshForm();
                if (this.txtSearch.Text == "")
                {
                    SearchResult serRes = new SearchResult();
                    serRes.ShowDialog();
                }
                else
                {
                    bool result = false;
                    result = DateDAL.IsExist("select Address from CustomerInfo where Email like '%" + this.txtSearch.Text + "%'");
                    if (result == true)
                    {
                        SearchResult serRes = new SearchResult();
                        serRes.serStr5 = this.txtSearch.Text;
                        serRes.ShowDialog();
                        this.RefreshForm();
                        searchNo = (int)serRes.SearchNo;
                        this.ShowSearchRec(searchNo);
                    }
                    else
                    {
                        MessageBox.Show("No Customer Found", Messages.Header);
                        return;
                    }
                }
            }
            this.txtNetAmount.Text = (FormControls.GetDecimalValue(this.txtTotalPrice, 0) - FormControls.GetDecimalValue(this.txtDiscount, 0)).ToString("0");
            //this.txtBalance.Text = (FormControls.GetDecimalValue(this.txtNetAmount, 0) - FormControls.GetDecimalValue(this.txtReceived, 0)).ToString("0");
            dis = 1;
        }

        private void ShowSearchRec(int saleNo)
        {
            sale = new Sale();
            sale = payDAL.GetRecordBySaleNo(saleNo);
            if (sale.OrderNo == 0)
            {
                salePay = payDAL.GetSalePaymentBySaleNo(saleNo);
                goldDet = payDAL.GetGoldBySaleNo(saleNo);
            }
            else
            {
                salePay = payDAL.GetSalePaymentByOrderNo((int)sale.OrderNo);
                goldDet = payDAL.GetGoldByOrderNo((int)sale.OrderNo);
            }
            if (sale == null)
                return;
            else
            {
                this.txtSaleNo.Text = sale.SaleNo.ToString();
                this.txtOrderNo.Text = sale.OrderNo.ToString();
                if (sale.BillBookNo == null)
                    this.txtBillBook.Text = "";
                else
                    this.txtBillBook.Text = sale.BillBookNo.ToString();
                this.txtTotalPrice.Text = Math.Round((decimal)sale.TotalPrice, 0).ToString();
                this.txtNetAmount.Text = Math.Round((decimal)sale.TotalNetPrice, 0).ToString();
                this.txtBalance.Text = Math.Round((decimal)sale.Balance, 0).ToString();
                //this.txtOrderNo.Text = sale.OrderNo.ToString();
                this.txtKhataNo.Text = sale.KhataNo.ToString();
                if (sale.BillDiscout == 0)
                    this.txtDiscount.Text = "";
                else
                    this.txtDiscount.Text = Math.Round((decimal)sale.BillDiscout, 0).ToString();
                this.txtReceived.Text = Math.Round(this.ReceivedAmount(salePay, goldDet), 0).ToString();
                this.txtSaleDate.Text = Convert.ToDateTime(sale.SDate).ToString("dd:MMM:yy");
                this.txtCustomerName.Text = sale.CustName.Name.ToString();
                this.txtContactNo.Text = sale.CustName.Mobile.ToString();
                this.txtEmailaddress.Text = sale.CustName.Email.ToString();
                this.txtAddress.Text = sale.CustName.Address.ToString();
                this.txtCustId.Text = sale.CustName.ID.ToString();
                this.txtCustomerBalance.Text = Math.Round((decimal)sale.CustBalance, 0).ToString();
                cust = new Customer();
                cust.CashBalance = Math.Round((decimal)sale.CustBalance, 0);
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
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(goldDet[i].Weight), 3);
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(goldDet[i].Rate), 1);
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Math.Round(Convert.ToDecimal(goldDet[i].Amount), 0);
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
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(salePay[i].Amount), 0);
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
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(goldDet[i].Weight), 3);
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(goldDet[i].Rate), 1);
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Math.Round(Convert.ToDecimal(goldDet[i].Amount), 0);
                        }
                    }
                }
            }
        }

        private decimal ReceivedAmount(List<SalePayment> lsp, List<Gold> lg)
        {
            //decimal a = 0;
            //if (lsp != null)
            //{
            //    foreach (SalePayment sp in lsp)
            //    {
            //        a += sp.Amount;
            //    }
            //}
            //if (lg != null)
            //{
            //    foreach (Gold gd in lg)
            //    {
            //        a += gd.Amount;
            //    }
            //}
            //return a;

            //Start Qasim: Correct here Cash Paid Vouchers Add Problem Solution (02/07/2023)MM/DD/YYYY
            decimal a = 0;
            if (lsp != null)
            {
                foreach (SalePayment sp in lsp)
                {
                    //if (sp.Description == "Cash Paid To S.No."+sp.SaleNo)
                    if (!(sp.Description.Contains("Cash Paid")))
                    {
                        a += sp.Amount;
                    }
                    else
                    {
                        a -= sp.Amount;
                    }
                }
            }
            if (lg != null)
            {
                foreach (Gold gd in lg)
                {
                    a += gd.Amount;
                }
            }
            return a;
            //End
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
                //this.txtSaleNo.Text = sale.SaleNo.ToString();
                if (sale.BillBookNo == null)
                    this.txtBillBook.Text = "";
                else
                    this.txtBillBook.Text = sale.BillBookNo.ToString();
                this.txtTotalPrice.Text = Math.Round((decimal)sale.TotalPrice, 0).ToString();
                this.txtNetAmount.Text = Math.Round((decimal)sale.TotalNetPrice, 0).ToString();
                this.txtBalance.Text = Math.Round((decimal)sale.Balance, 0).ToString();
                this.txtOrderNo.Text = sale.OrderNo.ToString();
                this.txtKhataNo.Text = sale.KhataNo.ToString();
                if (sale.BillDiscout == 0)
                    this.txtDiscount.Text = "";
                else
                    this.txtDiscount.Text = Math.Round((decimal)sale.BillDiscout, 0).ToString();
                this.txtReceived.Text = Math.Round(this.ReceivedAmount(salePay, goldDet), 0).ToString();
                this.txtSaleDate.Text = Convert.ToDateTime(sale.SDate).ToString("dd:MMM:yy");
                this.txtCustomerName.Text = sale.CustName.Name.ToString();
                this.txtContactNo.Text = sale.CustName.Mobile.ToString();
                this.txtEmailaddress.Text = sale.CustName.Email.ToString();
                this.txtCustId.Text = sale.CustName.ID.ToString();
                //this.txtAddress.Text = sale.CustName.Address.ToString();
                this.txtCustomerBalance.Text = Math.Round((decimal)sale.CustBalance, 0).ToString();
                cust = new Customer();
                cust.CashBalance = Math.Round((decimal)sale.CustBalance, 0);
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
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(goldDet[i].PWeight), 3);
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(goldDet[i].Rate), 1);
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Math.Round(Convert.ToDecimal(goldDet[i].Amount), 0);

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
                        this.dgvPreviousReceivedAmount.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(salePay[i].Amount), 0);
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
                            this.dgvPreviousReciveGold.Rows[i].Cells[2].Value = Math.Round(Convert.ToDecimal(goldDet[i].Weight), 3);
                            this.dgvPreviousReciveGold.Rows[i].Cells[3].Value = Math.Round(Convert.ToDecimal(goldDet[i].Rate), 1);
                            this.dgvPreviousReciveGold.Rows[i].Cells[4].Value = Math.Round(Convert.ToDecimal(goldDet[i].Amount), 0);
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BalanceReceive_Load(object sender, EventArgs e)
        {
            this.cbxBank.SelectedIndexChanged -= new EventHandler(cbxBank_SelectedIndexChanged);

            this.rbtSaleNo.Checked = true;
            this.txtAmount.Enabled = false;
            FormControls.FillCombobox(cbxSaleMan, slmDAL.GetAllSaleMen(), "Name", "ID");
            this.txtAmountCheck.Enabled = false;
            this.cbxDepositeAccount.Enabled = false;
            this.txtDescription.Enabled = false;

            this.txtAmountCreditCard.Enabled = false;
            this.txtDeductRate.Enabled = false;
            this.txtReceiveables.Enabled = false;
            this.cbxBank.Enabled = false;
            this.txtBankDeductRate.Enabled = false;
            this.txtDepositeInBank.Enabled = false;
            this.cbxDepositeAccountCreditCard.Enabled = false;

            this.txtWeight.Enabled = false;
            this.txtRate.Enabled = false;
            this.txtPrice.Enabled = false;

            this.txtWeightUsedGold.Enabled = false;
            this.txtKaat.Enabled = false;
            this.cbxKarat.Enabled = false;
            this.txtPureWeight.Enabled = false;
            this.txtRateUsedGold.Enabled = false;
            this.txtPriceUseGold.Enabled = false;

            FormControls.FillCombobox(cbxDepositeAccount, bDAL.GetAllBankAccount(), "AccountNo", "Id");
            FormControls.FillCombobox(cbxBank, bDAL.GetAllBanks(), "BankName", "Id");

        }

        private void chkCash_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCash.Checked == true)
            {
                this.txtAmount.Enabled = true;
                this.txtAmount.BackColor = Color.White;
                TotalReceive();
            }
            else
            {
                this.txtAmount.Enabled = false;
                this.txtAmount.BackColor = Color.DarkKhaki;
            }
        }

        private void chkCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCheque.Checked == true)
            {
                this.txtAmountCheck.Enabled = true;
                this.txtAmountCheck.BackColor = Color.White;
                this.cbxDepositeAccount.Enabled = true;
                this.cbxDepositeAccount.BackColor = Color.White;
                this.txtDescription.Enabled = true;
                this.txtDescription.BackColor = Color.White;
                TotalReceive();
            }
            else
            {
                this.txtAmountCheck.Enabled = false;
                this.txtAmountCheck.BackColor = Color.DarkKhaki;
                this.cbxDepositeAccount.Enabled = false;
                this.cbxDepositeAccount.BackColor = Color.DarkKhaki;
                this.txtDescription.Enabled = false;
                this.txtDescription.BackColor = Color.DarkKhaki;
            }
        }

        private void chkCreditCard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkCreditCard.Checked == true)
            {
                this.txtAmountCreditCard.Enabled = true;
                this.txtAmountCreditCard.BackColor = Color.White;
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
                TotalReceive();
            }
            else
            {
                this.txtAmountCreditCard.Enabled = false;
                this.txtAmountCreditCard.BackColor = Color.DarkKhaki;
                this.txtDeductRate.Enabled = false;
                this.txtDeductRate.BackColor = Color.DarkKhaki;
                this.txtReceiveables.Enabled = false;
                this.txtReceiveables.BackColor = Color.DarkKhaki;
                this.cbxBank.Enabled = false;
                this.cbxBank.BackColor = Color.DarkKhaki;
                this.txtBankDeductRate.Enabled = false;
                this.txtBankDeductRate.BackColor = Color.DarkKhaki;
                this.txtDepositeInBank.Enabled = false;
                this.txtDepositeInBank.BackColor = Color.DarkKhaki;
                this.cbxDepositeAccountCreditCard.Enabled = false;
                this.cbxDepositeAccountCreditCard.BackColor = Color.DarkKhaki;
            }
        }

        private void chkPureGold_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPureGold.Checked == true)
            {
                this.txtWeight.Enabled = true;
                this.txtWeight.BackColor = Color.White;
                this.txtRate.Enabled = true;
                this.txtRate.BackColor = Color.White;
                this.txtPrice.Enabled = true;
                this.txtPrice.BackColor = Color.White;
                TotalReceive();
            }
            else
            {
                this.txtWeight.Enabled = false;
                this.txtWeight.BackColor = Color.DarkKhaki;
                this.txtRate.Enabled = false;
                this.txtRate.BackColor = Color.DarkKhaki;
                this.txtPrice.Enabled = false;
                this.txtPrice.BackColor = Color.DarkKhaki;
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
                this.cbxKarat.Enabled = true;
                this.cbxKarat.BackColor = Color.White;
                this.txtPureWeight.Enabled = true;
                this.txtPureWeight.BackColor = Color.White;
                this.txtRateUsedGold.Enabled = true;
                this.txtRateUsedGold.BackColor = Color.White;
                this.txtPriceUseGold.Enabled = true;
                this.txtPriceUseGold.BackColor = Color.White;
                TotalReceive();
            }
            else
            {
                this.txtWeightUsedGold.Enabled = false;
                this.txtWeightUsedGold.BackColor = Color.DarkKhaki;
                this.txtKaat.Enabled = false;
                this.txtKaat.BackColor = Color.DarkKhaki;
                this.cbxKarat.Enabled = false;
                this.cbxKarat.BackColor = Color.DarkKhaki;
                this.txtPureWeight.Enabled = false;
                this.txtPureWeight.BackColor = Color.DarkKhaki;
                this.txtRateUsedGold.Enabled = false;
                this.txtRateUsedGold.BackColor = Color.DarkKhaki;
                this.txtPriceUseGold.Enabled = false;
                this.txtPriceUseGold.BackColor = Color.DarkKhaki;
            }
        }

        private void RefreshForm()
        {
            this.dgvPreviousReceivedAmount.Rows.Clear();
            this.dgvPreviousReciveGold.Rows.Clear();
            this.txtCustomerName.Text = "";
            this.txtContactNo.Text = "";
            this.txtEmailaddress.Text = "";
            this.txtAddress.Text = "";
            this.txtCustomerBalance.Text = "";
            //this.txtSearch.Text = "";
            this.txtSaleNo.Text = "";
            this.txtBillBook.Text = "";
            this.txtTotalPrice.Text = "";
            this.txtNetAmount.Text = "";
            this.txtBalance.Text = "";
            this.txtSaleMan.Text = "";
            this.txtOrderNo.Text = "";
            this.txtKhataNo.Text = "";
            this.txtDiscount.Text = "";
            this.txtReceived.Text = "";
            this.txtSaleDate.Text = "";
            this.txtAmount.Text = "";
            this.txtAmountCheck.Text = "";
            this.txtDescription.Text = "";
            this.txtAmountCreditCard.Text = "";
            this.txtDeductRate.Text = "";
            this.txtReceiveables.Text = "";
            this.txtBankDeductRate.Text = "";
            this.txtDepositeInBank.Text = "";
            this.txtWeight.Text = "";
            this.txtRate.Text = "";
            this.txtPrice.Text = "";
            this.txtWeightUsedGold.Text = "";
            this.txtKaat.Text = "";
            this.txtPureWeight.Text = "";
            this.txtRateUsedGold.Text = "";
            this.txtPriceUseGold.Text = "";
            this.txtTotalReceive.Text = "";
            this.txtTotalBalance.Text = "";
            this.chkCash.Checked = false;
            this.chkCheque.Checked = false;
            this.chkCreditCard.Checked = false;
            this.chkPureGold.Checked = false;
            this.chkUseGold.Checked = false;
            this.txtAmountCreditCard.Text = "";
            this.txtDeductRate.Text = "";
            this.txtReceiveables.Text = "";
            this.cbxBank.SelectedIndex = -1;
            this.txtBankDeductRate.Text = "";
            this.txtDepositeInBank.Text = "";
            this.cbxDepositeAccountCreditCard.Text = "";

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                TReceived = FormControls.GetDecimalValue(txtReceived, 0);
                con.Open();
                trans = con.BeginTransaction();
                ChildAccount cha = new ChildAccount();
                if (this.chkCash.Checked == true)
                {
                    #region Cash voucher
                    //if (!(this.txtCashReceive.Text == "" || this.txtCashReceive.Text == "0"))
                    //{
                    //cash in hand entry
                    pv = new Voucher();
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Cash In Hand", "General Account", con, trans);
                    }
                    cha = acDAL.GetAccount(1, "Current Asset", "Cash In Hand", con, trans);
                    pv.AccountCode = cha;
                    if (FormControls.GetDecimalValue(this.txtAmount, 0) < 0)
                    {
                        pv.Dr = 0;
                        pv.Cr = -FormControls.GetDecimalValue(this.txtAmount, 0);
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Cash Paid from Balance Receive for Order No." + this.txtOrderNo.Text;
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Cash Paid from Balance Receive Sale No." + this.txtSaleNo.Text;
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CPV", con, trans);
                    }
                    else
                    {
                        pv.Dr = FormControls.GetDecimalValue(this.txtAmount, 0);
                        pv.Cr = 0;
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Cash Received from Balance Receive for Order No." + this.txtOrderNo.Text;
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Cash Received from Balance Receive Sale No." + this.txtSaleNo.Text;
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("CRV", con, trans);
                    }
                    pv.DDate = this.dtpDate.Value;
                    TReceived = TReceived + FormControls.GetDecimalValue(this.txtAmount, 0);

                    //pv.Description = "Cash Receive for Sale No" + this.txtSaleNo.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //Customer account entry
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                    custv.AccountCode = child;
                    if (FormControls.GetDecimalValue(this.txtAmount, 0) < 0)
                    {
                        custv.Cr = 0;
                        custv.Dr = -FormControls.GetDecimalValue(this.txtAmount, 0);
                    }
                    else
                    {
                        custv.Cr = FormControls.GetDecimalValue(this.txtAmount, 0);
                        custv.Dr = 0;
                    }
                    custv.DDate = this.dtpDate.Value;
                    custv.OrderNo = (int)sale.OrderNo;
                    custv.SNO = (int)sale.SaleNo;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);
                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = (int)sale.SaleNo;
                    sp1.ONo = (int)sale.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cash";
                    if (FormControls.GetDecimalValue(this.txtAmount, 0) < 0)
                        sp1.PTime = "Balance Paid";
                    else
                        sp1.PTime = "Balance Received";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = this.dtpDate.Value;
                    sp1.BDrate = 0;
                    sp1.BankName = "";
                    if (FormControls.GetDecimalValue(this.txtAmount, 0) < 0)
                        sp1.Amount = -FormControls.GetDecimalValue(this.txtAmount, 0);
                    else
                        sp1.Amount = FormControls.GetDecimalValue(this.txtAmount, 0);
                    sp1.Description = pv.Description;
                    sp1.DAccountCode = "";
                    if (this.cbxSaleMan.SelectedIndex != -1)
                    {
                        sp1.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                    }
                    if (Main.City == "Islamabad")
                    {
                        grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmount, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                    }
                    else
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmount, 0) / Convert.ToDecimal(grDAL.GetRateByKarat("24", this.dtpDate.Value))), 3);
                    payDAL.AddSalePayment(sp1, con, trans);                    
                    #endregion
                }
                if (this.chkCheque.Checked == true)
                {
                    #region Cheque voucher
                    //Cheques chq = new Cheques();
                    pv = new Voucher();
                    BankAccount bAcc = (BankAccount)this.cbxDepositeAccount.SelectedItem;
                    cha = acDAL.GetChildByName(bAcc.AccountNo, con, trans);
                    pv.AccountCode = acDAL.GetChildByCode(cha.ChildCode, con, trans);
                    //pv.AccountCode = chq.DepositInAccount .AccountCode;
                    pv.Dr = FormControls.GetDecimalValue(this.txtAmountCheck, 0);
                    pv.Cr = 0;
                    TReceived = TReceived + FormControls.GetDecimalValue(this.txtAmountCheck, 0);
                    pv.DDate = this.dtpDate.Value; ;
                    if (this.txtOrderNo.Text != "")
                    {
                        pv.Description = "Cheque Received from Balance Receive for Order No." + this.txtOrderNo.Text;
                    }
                    if (this.txtSaleNo.Text != "")
                    {
                        pv.Description = "Cheque Received from Balance Receive for Sale No." + this.txtSaleNo.Text;
                    }
                    pv.OrderNo = (int)sale.OrderNo;
                    pv.SNO = (int)sale.SaleNo;
                    pv.VNO = vDAL.CreateVNO("CHV", con, trans);
                    //pv.VNO = chq.VNO;
                    vDAL.AddVoucher(pv, con, trans);
                    //Customer account entry
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                    custv.AccountCode = child;
                    custv.Cr = FormControls.GetDecimalValue(this.txtAmountCheck, 0);
                    custv.Dr = 0;
                    custv.DDate = this.dtpDate.Value;
                    custv.OrderNo = sale.OrderNo;
                    custv.SNO = sale.SaleNo;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);
                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = (int)sale.SaleNo;
                    sp1.ONo = (int)sale.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cheque";
                    sp1.PTime = "Balance Received";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = this.dtpDate.Value;
                    sp1.BDrate = 0;
                    sp1.BankName = "";
                    sp1.Amount = FormControls.GetDecimalValue(this.txtAmountCheck, 0);
                    sp1.Description = pv.Description; ;
                    sp1.DAccountCode = "";
                    if (this.cbxSaleMan.SelectedIndex != -1)
                    {
                        sp1.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                    }
                    if (Main.City == "Islamabad")
                    {
                        grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmount, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                    }
                    else
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmountCheck, 0) / Convert.ToDecimal(grDAL.GetRateByKarat("24", this.dtpDate.Value))), 3);
                    payDAL.AddSalePayment(sp1, con, trans);                   
                    #endregion
                }
                if (this.chkCreditCard.Checked == true)
                {
                    CreditCard cc;
                    #region CreditCard Voucher
                    pv = new Voucher();
                    BankAccount bAcc = (BankAccount)this.cbxDepositeAccountCreditCard.SelectedItem;
                    cha = acDAL.GetChildByCode(bAcc.AccountCode.ChildCode, con, trans);
                    cc = new CreditCard();
                    //cha = adal.GetChildByCode(cc.DepositInAccount.AccountCode.ChildCode);
                    //if (cha == null)
                    //{
                    //    cha = new ChildAccount();
                    //    cha.ChildCode = this.CreatAccount(1, "Current Asset", "Cash In Hand", "Cash In Hand", "Cash In Hand");
                    //}
                    pv.AccountCode = cha;
                    pv.Dr = FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
                    pv.Cr = 0;
                    TReceived = TReceived + FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
                    pv.DDate = this.dtpDate.Value;
                    if (this.txtOrderNo.Text != "")
                    {
                        pv.Description = "Credit Card from Balance Receive For Order No." + pv.OrderNo.ToString();
                    }
                    if (this.txtSaleNo.Text != "")
                    {
                        pv.Description = "Credit Card from Balance Receive For Sale No." + pv.SNO.ToString();
                    }
                    pv.OrderNo = (int)sale.OrderNo;
                    pv.SNO = (int)sale.SaleNo;
                    pv.VNO = vDAL.CreateVNO("CCV", con, trans);
                    string str = pv.VNO;
                    vDAL.AddVoucher(pv, con, trans);
                    //customer account voucher entry
                    ExtraMoney = FormControls.GetDecimalValue(this.txtDepositeInBank, 0) - FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                    custv.AccountCode = child;
                    custv.Cr = FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
                    custv.Dr = 0;
                    custv.DDate = this.dtpDate.Value;
                    custv.OrderNo = (int)sale.OrderNo;
                    custv.SNO = (int)sale.SaleNo;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);
                    //for extra money;
                    pv = new Voucher();
                    cha = acDAL.GetChildByCode(bAcc.AccountCode.ChildCode, con, trans);
                    pv.AccountCode = cha;
                    pv.Dr = ExtraMoney;
                    pv.Cr = 0;
                    pv.DDate = this.dtpDate.Value;
                    if (this.txtOrderNo.Text != "")
                    {
                        pv.Description = "Credit Card Extra Money For Order No." + pv.OrderNo.ToString();
                    }
                    if (this.txtSaleNo.Text != "")
                    {
                        pv.Description = "Credit Card Extra Money For Sale No." + pv.SNO.ToString();
                    }
                    pv.OrderNo = (int)sale.OrderNo;
                    pv.SNO = (int)sale.SaleNo;
                    pv.VNO = str;
                    //pv.Description = "Credit Card Extra Money For Sale No" + this.txtSaleNo.ToString();
                    vDAL.AddVoucher(pv, con, trans);
                    pv = new Voucher();
                    cha = new ChildAccount();
                    cha.HeadCode = 1;
                    // cha .GroupCode=adal .
                    cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        string str1 = acDAL.CreateAccount(1, "Income", "Credit Card Extra", "General Account", con, trans);
                    }
                    cha = acDAL.GetChildByName("Credit Card Extra", con, trans);
                    pv.AccountCode = cha;
                    pv.Cr = ExtraMoney;
                    pv.Dr = 0;
                    pv.DDate = this.dtpDate.Value;
                    if (this.txtOrderNo.Text != "")
                    {
                        pv.Description = "Credit Card Extra Money For Order No." + pv.OrderNo.ToString();
                    }
                    if (this.txtSaleNo.Text != "")
                    {
                        pv.Description = "Credit Card Extra Money For Sale No." + pv.SNO.ToString();
                    }
                    pv.OrderNo = (int)sale.OrderNo;
                    pv.SNO = (int)sale.SaleNo;
                    pv.VNO = str;
                    //pv.Description = "Credit Card Extra Money For Sale No" + this.txtSaleNo.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = (int)sale.SaleNo;
                    sp1.ONo = (int)sale.OrderNo;
                    sp1.VNo = str;
                    sp1.PMode = "Credit Card";
                    sp1.PTime = "Balance Received";
                    sp1.Receiveable = FormControls.GetDecimalValue(this.txtReceiveables, 0);
                    sp1.DRate = FormControls.GetDecimalValue(this.txtDeductRate, 1);
                    sp1.DDate = DateTime.Today;
                    sp1.BDrate = bAcc.BankName.DRate;
                    sp1.BankName = bAcc.BankName.BankName;
                    sp1.Amount = FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
                    sp1.Description = "Credit Card from Balance Receive For Sale No." + pv.SNO.ToString(); ;
                    sp1.DAccountCode = "";
                    if (this.cbxSaleMan.SelectedIndex != -1)
                    {
                        sp1.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                    }
                    if (Main.City == "Islamabad")
                    {
                        grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmount, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                    }
                    else
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtAmountCreditCard, 0) / Convert.ToDecimal(grDAL.GetRateByKarat("24", this.dtpDate.Value))), 3);
                    payDAL.AddSalePayment(sp1, con, trans);
                    cc.AccountCode = custv.AccountCode.ChildCode;
                    cc.VNO = str;
                    cc.ONO = (int)sale.OrderNo;
                    cc.SNO = (int)sale.SaleNo;
                    cc.Amount = sp1.Amount;
                    cc.AmountDeposit = FormControls.GetDecimalValue(this.txtDepositeInBank, 0);
                    cc.BankDeductRate = sp1.BDrate;
                    cc.SwapAmount = FormControls.GetDecimalValue(this.txtReceiveables, 0);
                    cc.DepositInAccount = new BankAccount();
                    cc.DepositInAccount.AccountNo = bAcc.AccountNo;
                    cc.DepositInAccount.AccountCode = new ChildAccount();
                    cc.DepositInAccount.AccountCode = bAcc.AccountCode;
                    cc.BankName = new Banks();
                    cc.BankName.BankName = cbxBank.Text;
                    cc.MachineName = "";
                    cc.Description = "";
                    cc.Status = "";
                    saleDAL.AddCreditCards(cc, con, trans);                    
                    #endregion
                }
                if (this.chkPureGold.Checked == true)
                {
                    #region Pure Gold Voucher
                    if (this.chkPureGoldBalance.Checked == false)
                    {
                        //gold accunt entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(this.txtPrice, 0);
                        TReceived = TReceived + FormControls.GetDecimalValue(this.txtPrice, 0);
                        pv.Cr = 0;
                        pv.DDate = this.dtpDate.Value;
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Pure Gold Purchased from Balance Received For Order No." + pv.OrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Pure Gold Purchased from Balance Received For Sale No." + pv.SNO.ToString();
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        //pv.VNO = chq.VNO;
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(this.txtPrice, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = (int)sale.OrderNo;
                        custv.SNO = (int)sale.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        Gold gld = new Gold();
                        if (this.txtOrderNo.Text != "")
                        {
                            gld.Description = "Pure Gold Purchased from Balance Received For Order No." + this.txtOrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            gld.Description = "Pure Gold Purchased from Balance Received For Sale No." + this.txtSaleNo.ToString();
                        }
                        gld.SNO = (int)sale.SaleNo;
                        gld.ONO = (int)sale.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.PGDate = this.dtpDate.Value;
                        gld.GoldType = GoldType.Pure;
                        gld.Weight = FormControls.GetDecimalValue(this.txtWeight, 3);
                        gld.PWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                        gld.Kaat = 0;
                        gld.RemainingWork = "";
                        gld.Description = pv.Description;
                        gld.Karat = "24";
                        gld.Rate = FormControls.GetDecimalValue(this.txtRate, 1);
                        gld.Amount = FormControls.GetDecimalValue(this.txtPrice, 0);
                        gld.CustId = FormControls.GetIntValue(this.txtCustId);
                        if (this.cbxSaleMan.SelectedIndex != -1)
                        {
                            gld.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                        }
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        saleDAL.AddGoldDetail(gld, con, trans);                        
                    }
                    else if (this.chkPureGoldBalance.Checked == true)
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
                        pv.GoldDr = FormControls.GetDecimalValue(this.txtWeight, 3);
                        TReceived = TReceived + FormControls.GetDecimalValue(this.txtPrice, 0);
                        pv.GoldCr = 0;
                        pv.DDate = this.dtpDate.Value;
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Pure Gold Purchased from Balance Received For Order No." + pv.OrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Pure Gold Purchased from Balance Received For Sale No." + pv.SNO.ToString();
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("GPV", con, trans);
                        //pv.VNO = chq.VNO;
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.GoldCr = FormControls.GetDecimalValue(this.txtWeight, 3);
                        custv.GoldDr = 0;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = (int)sale.OrderNo;
                        custv.SNO = (int)sale.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        Gold gld = new Gold();
                        if (this.txtOrderNo.Text != "")
                        {
                            gld.Description = "Pure Gold Purchased from Balance Received For Order No." + this.txtOrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            gld.Description = "Pure Gold Purchased from Balance Received For Sale No." + this.txtSaleNo.ToString();
                        }
                        gld.SNO = (int)sale.SaleNo;
                        gld.ONO = (int)sale.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.PGDate = this.dtpDate.Value;
                        gld.GoldType = GoldType.Pure;
                        gld.Weight = FormControls.GetDecimalValue(this.txtWeight, 3);
                        gld.PWeight = FormControls.GetDecimalValue(this.txtWeight, 3);
                        gld.Kaat = 0;
                        gld.RemainingWork = "";
                        gld.Description = pv.Description;
                        gld.Karat = "24";
                        gld.Rate = FormControls.GetDecimalValue(this.txtRate, 1);
                        gld.Amount = FormControls.GetDecimalValue(this.txtPrice, 0);
                        gld.CustId = FormControls.GetIntValue(this.txtCustId);
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        saleDAL.AddGoldDetail(gld, con, trans);                        
                    }

                    #endregion
                }
                if (this.chkUseGold.Checked == true)
                {
                    #region Used Gold Voucher
                    if (this.chkUsedGoldBalance.Checked == false)
                    {
                        //gold account entry
                        pv = new Voucher();
                        cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        if (cha == null)
                        {
                            cha = new ChildAccount();
                            cha.ChildCode = acDAL.CreateAccount(1, "Current Asset", "Gold In Hand", "General Account", con, trans);
                            cha = acDAL.GetAccount(1, "Current Asset", "Gold In Hand", con, trans);
                        }
                        pv.AccountCode = cha;
                        pv.Dr = FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        pv.Cr = 0;
                        TReceived = TReceived + FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        pv.DDate = this.dtpDate.Value;
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Used Gold Purchased from Balance Received For Order No." + pv.OrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Used Gold Purchased from Balance Received For Sale No." + pv.SNO.ToString();
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                        // pv.VNO = chq.VNO;
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.Cr = FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        custv.Dr = 0;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = (int)sale.OrderNo;
                        custv.SNO = (int)sale.SaleNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        Gold gld = new Gold();
                        if (this.txtOrderNo.Text != "")
                        {
                            gld.Description = "Used Gold Purchased from Balance Received For Order No." + this.txtOrderNo.Text;
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            gld.Description = "Used Gold Purchased from Balance Received For Sale No." + this.txtSaleNo.Text;
                        }
                        gld.SNO = (int)sale.SaleNo;
                        gld.ONO = (int)sale.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.PGDate = this.dtpDate.Value;
                        gld.GoldType = GoldType.Used;
                        gld.Weight = FormControls.GetDecimalValue(this.txtWeightUsedGold, 3);
                        gld.Kaat = FormControls.GetDecimalValue(this.txtKaat, 1);
                        gld.PWeight = FormControls.GetDecimalValue(this.txtPureWeight, 3);
                        gld.RemainingWork = "";
                        gld.Karat = (string)this.cbxKarat.SelectedItem;
                        gld.Rate = FormControls.GetDecimalValue(this.txtRateUsedGold, 1);
                        gld.Amount = FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        gld.CustId = FormControls.GetIntValue(this.txtCustId);
                        gld.Description = pv.Description;
                        if (this.cbxSaleMan.SelectedIndex != -1)
                        {
                            gld.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                        }
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        saleDAL.AddGoldDetail(gld, con, trans);                        
                    }
                    else if (this.chkUsedGoldBalance.Checked == true)
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
                        pv.GoldDr = FormControls.GetDecimalValue(this.txtPureWeight, 3);
                        pv.GoldCr = 0;
                        TReceived = TReceived + FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        pv.DDate = this.dtpDate.Value;
                        if (this.txtOrderNo.Text != "")
                        {
                            pv.Description = "Used Gold Purchased from Balance Received as Pure For Order No." + pv.OrderNo.ToString();
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            pv.Description = "Used Gold Purchased from Balance Received as Pure For Sale No." + pv.SNO.ToString();
                        }
                        pv.OrderNo = (int)sale.OrderNo;
                        pv.SNO = (int)sale.SaleNo;
                        pv.VNO = vDAL.CreateVNO("AGV", con, trans);
                        // pv.VNO = chq.VNO;
                        vDAL.AddVoucher(pv, con, trans);
                        //customer account entry
                        custv = new Voucher();
                        ChildAccount child = new ChildAccount();
                        child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                        custv.AccountCode = child;
                        custv.GoldCr = FormControls.GetDecimalValue(this.txtPureWeight, 3);
                        custv.GoldDr = 0;
                        custv.DDate = this.dtpDate.Value;
                        custv.OrderNo = (int)sale.OrderNo;
                        custv.SNO = (int)sale.OrderNo;
                        custv.VNO = pv.VNO;
                        custv.Description = pv.Description;
                        vDAL.AddVoucher(custv, con, trans);
                        Gold gld = new Gold();
                        if (this.txtOrderNo.Text != "")
                        {
                            gld.Description = "Used Gold Purchased from Balance Received as Pure For Order No." + this.txtOrderNo.Text;
                        }
                        if (this.txtSaleNo.Text != "")
                        {
                            gld.Description = "Used Gold Purchased from Balance Received as Pure For Sale No." + this.txtSaleNo.Text;
                        }
                        gld.SNO = (int)sale.SaleNo;
                        gld.ONO = (int)sale.OrderNo;
                        gld.VNO = pv.VNO;
                        gld.PGDate = this.dtpDate.Value;
                        gld.GoldType = GoldType.Used;
                        gld.Weight = FormControls.GetDecimalValue(this.txtWeightUsedGold, 3);
                        gld.PWeight = FormControls.GetDecimalValue(this.txtPureWeight, 3);
                        gld.Kaat = FormControls.GetDecimalValue(this.txtKaat, 1);
                        gld.RemainingWork = "";
                        gld.Karat = (string)this.cbxKarat.SelectedItem;
                        gld.Rate = FormControls.GetDecimalValue(this.txtRateUsedGold, 1);
                        gld.Amount = FormControls.GetDecimalValue(this.txtPriceUseGold, 0);
                        gld.CustId = FormControls.GetIntValue(this.txtCustId);
                        gld.Description = pv.Description;
                        gld.PTime = "Sale";
                        gld.PMode = "Rec";
                        saleDAL.AddGoldDetail(gld, con, trans);                        
                    }
                    #endregion
                }
                if (this.chkBadDats.Checked == true)
                {
                    #region Bad Dats
                    pv = new Voucher();
                    cha = acDAL.GetAccount(3, "Sale Expense", "Bad Dats", con, trans);
                    if (cha == null)
                    {
                        cha = new ChildAccount();
                        cha.ChildCode = acDAL.CreateAccount(3, "Sale Expense", "Bad Dats", "General Account", con, trans);
                    }
                    cha = acDAL.GetAccount(3, "Sale Expense", "Bad Dats", con, trans);
                    pv.AccountCode = cha;

                    pv.Dr = FormControls.GetDecimalValue(this.txtBadDats, 0);
                    pv.Cr = 0;
                    if (this.txtOrderNo.Text != "")
                    {
                        pv.Description = "Bad Dats of Order No" + this.txtOrderNo.Text;
                    }
                    if (this.txtSaleNo.Text != "")
                    {
                        pv.Description = "Bad Dats of Sale No" + this.txtSaleNo.Text;
                    }
                    pv.VNO = vDAL.CreateVNO("BDV", con, trans);

                    pv.DDate = this.dtpDate.Value;
                    TReceived = TReceived + FormControls.GetDecimalValue(this.txtBadDats, 0);

                    //pv.Description = "Cash Receive for Sale No" + this.txtSaleNo.Text;
                    vDAL.AddVoucher(pv, con, trans);
                    //Customer account entry
                    custv = new Voucher();
                    ChildAccount child = new ChildAccount();
                    child = acDAL.GetChildByCode(sale.CusAccountNo, con, trans);
                    custv.AccountCode = child;

                    custv.Cr = FormControls.GetDecimalValue(this.txtBadDats, 0);
                    custv.Dr = 0;

                    custv.DDate = this.dtpDate.Value;
                    custv.OrderNo = (int)sale.OrderNo;
                    custv.SNO = (int)sale.SaleNo;
                    custv.VNO = pv.VNO;
                    custv.Description = pv.Description;
                    vDAL.AddVoucher(custv, con, trans);
                    SalePayment sp1 = new SalePayment();
                    sp1.SaleNo = (int)sale.SaleNo;
                    sp1.ONo = (int)sale.OrderNo;
                    sp1.VNo = pv.VNO;
                    sp1.PMode = "Cash";
                    sp1.PTime = "Balance Received";
                    sp1.Receiveable = 0;
                    sp1.DRate = 0;
                    sp1.DDate = this.dtpDate.Value;
                    sp1.BDrate = 0;
                    sp1.BankName = "";
                    sp1.Amount = FormControls.GetDecimalValue(this.txtBadDats, 0);
                    sp1.Description = pv.Description;
                    sp1.DAccountCode = "";
                    if (this.cbxSaleMan.SelectedIndex != -1)
                    {
                        sp1.SaleManId = (int)this.cbxSaleMan.SelectedValue;
                    }
                    if (Main.City == "Islamabad")
                    {
                        grs = grDAL.GetPasaRates(Convert.ToDateTime(dtpDate.Value));
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtBadDats, 0) / (Convert.ToDecimal(grs.PoundPasa) / Formulas.WeightInGm)), 3);
                    }
                    else
                        sp1.GoldOfCash = (decimal)Math.Round((FormControls.GetDecimalValue(this.txtBadDats, 0) / Convert.ToDecimal(grDAL.GetRateByKarat("24", this.dtpDate.Value))), 3);
                    payDAL.AddSalePayment(sp1, con, trans);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    decimal val = 0, cashRec = 0, cashPay = 0;
                    if (chkCash.Checked == true)
                    {
                        if (FormControls.GetDecimalValue(txtAmount, 0) > 0)
                            cashRec = FormControls.GetDecimalValue(txtAmount, 0);
                        else if (FormControls.GetDecimalValue(txtAmount, 0) < 0)
                            cashPay = -FormControls.GetDecimalValue(txtAmount, 0);
                    }
                    val = (cashRec + FormControls.GetDecimalValue(txtAmountCheck, 0) + FormControls.GetDecimalValue(txtAmountCreditCard, 0) + FormControls.GetDecimalValue(txtPrice, 0) + FormControls.GetDecimalValue(txtPriceUseGold, 0)) - cashPay;
                    
                    if (txtSaleNo.Text != "")
                    {
                        saleDAL.UpdateSaleBalance((@"Update Sale Set NetBill = " + this.txtNetAmount.Text + 
                        ", CashReceive = CashReceive + " + cashRec + ", CashPayment = CashPayment + " + cashPay +
                        ", CAmount = CAmount + " + FormControls.GetDecimalValue(txtAmountCreditCard, 0) +
                        ", CCAmount = CCAmount + " + FormControls.GetDecimalValue(txtAmountCheck, 0) +
                        ", UGoldAmount = UGoldAmount + " + FormControls.GetDecimalValue(txtPureWeight, 3) +
                        ", PGoldAmount = PGoldAmount + " + FormControls.GetDecimalValue(txtWeight, 3) +
                        ", TReceivedAmount = TReceivedAmount + " + val + ", BillDiscount = " + FormControls.GetDecimalValue(txtDiscount, 0) +
                        ", Balance = " + (FormControls.GetDecimalValue(txtNetAmount, 0) - (val + FormControls.GetDecimalValue(txtReceived, 0))) + 
                        " Where SaleNo = " + FormControls.GetIntValue(this.txtSaleNo)), con, trans);
                    }
                    else if (txtOrderNo.Text != "" && FormControls.GetIntValue(this.txtSaleNo) == 0)
                    {
                        saleDAL.UpdateSaleBalance((@"Update OrderMaster Set NetBill = " + this.txtNetAmount.Text + 
                        ", CashReceive = CashReceive + " + cashRec + ", CashPayment = CashPayment + " + cashPay +
                        ", CAmount = CAmount + " + FormControls.GetDecimalValue(txtAmountCreditCard, 0) + 
                        ", CCAmount = CCAmount + " + FormControls.GetDecimalValue(txtAmountCheck, 0) + 
                        ", UGoldAmount = UGoldAmount + " + FormControls.GetDecimalValue(txtPureWeight, 3) + 
                        ", PGoldAmount = PGoldAmount + " + FormControls.GetDecimalValue(txtWeight, 3) +
                        ", TReceivedAmount = TReceivedAmount + " + val + ", BillDiscount = " + FormControls.GetDecimalValue(txtDiscount, 0) +
                        ", Balance = " + (FormControls.GetDecimalValue(txtNetAmount, 0) - (val + FormControls.GetDecimalValue(txtReceived, 0))) + 
                        " Where OrderNo = " + FormControls.GetIntValue(this.txtOrderNo)), con, trans);
                    }

                    MessageBox.Show("Record saved successfully", Messages.Header);
                    trans.Commit();
                    con.Close();
                    if (MessageBox.Show("Click Yes to parchi Or No to Invoice", Messages.Header, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (this.txtSaleNo.Text != "" && this.txtSaleNo.Text != "0")
                        {
                            ReportViewer frm = new ReportViewer();
                            frm.isPage = 3;
                            frm.rpt = 11;
                            frm.SaleNo = Convert.ToInt32(txtSaleNo.Text);
                            frm.Show();
                        }
                        if (this.txtOrderNo.Text != "" && this.txtOrderNo.Text != "0")
                        {
                            ReportViewer frm = new ReportViewer();
                            frm.isPage = 3;
                            frm.rpt = 11;
                            frm.oNo = Convert.ToInt32(txtOrderNo.Text);
                            frm.Show();
                        }
                    }
                    else
                    {
                        if (this.txtSaleNo.Text != "" && this.txtSaleNo.Text != "0")
                        {
                            ReportViewer frm = new ReportViewer();
                            frm.isPage = 3;
                            frm.rpt = 4;
                            frm.sNo = Convert.ToInt32(txtSaleNo.Text);
                            frm.ShowDialog();
                        }
                        if (this.txtOrderNo.Text != "" && this.txtOrderNo.Text != "0")
                        {
                            ReportViewer frm = new ReportViewer();
                            string selectQuery = "{OrderEstimateBill.OrderNo}= " + Convert.ToInt32(this.txtOrderNo.Text);
                            frm.selectQuery = selectQuery;
                            frm.isPage = 2;
                            frm.rpt = 1;
                            frm.ShowDialog();
                        }
                    }
                    this.txtSearch.Text = "";
                    this.RefreshForm();
                }
            }
        }

        private void txtDeductRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtAmountCreditCard, 0);
            b = FormControls.GetDecimalValue(this.txtDeductRate, 1);
            decimal q = (a * b) / 100;
            this.txtReceiveables.Text = Math.Round((q + a), 0).ToString();
        }

        private void cbxBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cbxBank.SelectedIndex != -1)
            {
                Banks bnk = (Banks)this.cbxBank.SelectedItem;
                this.txtBankDeductRate.Text = Math.Round(bnk.DRate, 1).ToString();
                FormControls.FillCombobox(cbxDepositeAccountCreditCard, bDAL.GetAllBankAccountByBankId(bnk.Id), "AccountNo", "Id");
            }
        }

        private void cbxBank_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxBank.SelectedIndexChanged += new EventHandler(cbxBank_SelectedIndexChanged);
        }

        private void txtBankDeductRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtReceiveables, 0);
            b = FormControls.GetDecimalValue(this.txtBankDeductRate, 1);
            decimal q = (a * b) / 100;
            this.txtDepositeInBank.Text = Math.Round((a - q), 0).ToString();
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtWeight, 3);
            b = FormControls.GetDecimalValue(this.txtRate, 1);
            decimal q = a * b;
            this.txtPrice.Text = q.ToString("0");
            TotalReceive();
        }

        private void txtKaat_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txtRateUseGold_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtPureWeight, 3);
            b = FormControls.GetDecimalValue(this.txtRateUsedGold, 1);
            decimal q = Math.Round((a * b), 0);
            this.txtPriceUseGold.Text = q.ToString();
            this.txtTotalReceive.Text = q.ToString();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSearch_Click(sender, e);
                this.txtCustomerBalance.Text = this.txtBalance.Text;
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtNetAmount.Text = (FormControls.GetDecimalValue(this.txtTotalPrice, 0) - FormControls.GetDecimalValue(this.txtDiscount, 0)).ToString("0");
                this.txtBalance.Text = (FormControls.GetDecimalValue(this.txtNetAmount, 0) - FormControls.GetDecimalValue(this.txtReceived, 0)).ToString("0");
                dis = 1;
            }
        }

        private void txtKaat_KeyUp(object sender, KeyEventArgs e)
        {
            frm.KaatInRattiForBalance(FormControls.GetDecimalValue(this.txtKaat, 1), FormControls.GetDecimalValue(this.txtWeightUsedGold, 3), txtPureWeight);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (keyCheck == 1)
            {
                FormControls.validate_textBox(sender as TextBox, e);
               
            }
        }

        private void rbtSaleNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtSaleNo.Checked)           
                keyCheck = 1;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
        }

        private void rbtOrderNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtOrderNo.Checked)
                keyCheck = 1;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
        }

        private void rbtContactNo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtContactNo.Checked)
                keyCheck = 1;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
        }

        private void rbtCustomerName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtCustomerName.Checked)
                keyCheck = 0;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
            
        }

        private void rbtAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtAddress.Checked)
                keyCheck = 0;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
            
        }

        private void rbtEmailAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtEmailAddress.Checked)
                keyCheck = 0;
            this.txtSearch.Text = "";
            this.txtSearch.Select();
            
        }

        private void txtSearch_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        private void txtRate_TextChanged_1(object sender, EventArgs e)
        {
            txtRate_TextChanged(sender, e);
        }

        private void txtRateUsedGold_TextChanged(object sender, EventArgs e)
        {
            decimal a = 0;
            decimal b = 0;
            a = FormControls.GetDecimalValue(this.txtPureWeight, 3);
            b = FormControls.GetDecimalValue(this.txtRateUsedGold, 1);
            decimal q = Math.Round((a * b), 0);
            this.txtPriceUseGold.Text = q.ToString("0");
            TotalReceive();
        }

        private void cbxKarat_SelectionChangeCommitted(object sender, EventArgs e)
        {
          decimal  grate = grDAL.GetRateByKarat("24", DateTime.Today);
          this.txtRateUsedGold.Text = Math.Round(grate, 1).ToString("0");

        }

        private void ckbPromiseDate_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbPromiseDate.Checked == true)
                dtpPromiseDate.Enabled = true;
            if (ckbPromiseDate.Checked == false)
                dtpPromiseDate.Enabled = false;
        }

        private void cbxBank_SelectedIndexChanged_1(object sender, EventArgs e)
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
                    this.txtBankDeductRate.Text = Math.Round(val, 1).ToString();
                    FormControls.FillCombobox(cbxDepositeAccountCreditCard, bDAL.GetAllBankAccountByBankId(bnk.Id), "AccountNo", "Id");
                }
                catch
                { }
            }
        }

        private void cbxBank_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            this.cbxBank.SelectedIndexChanged += new EventHandler(cbxBank_SelectedIndexChanged);
        }

        private void TotalReceive()
        {
            decimal val, val1, val2, val3, val4;
            int result = 0;
            string str = txtAmount.Text;
            if (chkCash.Checked == true)
            {
                if (this.txtAmount.Text == "-")
                    val = 0;
                else
                {
                    if (FormControls.GetDecimalValue(txtAmount, 0) < 0)
                        val = FormControls.GetDecimalValue(txtAmount, 0);
                    else
                        val = FormControls.GetDecimalValue(txtAmount, 0);
                }
            }
            else
                val = 0;
            if (chkCheque.Checked == true)
                val1 = FormControls.GetDecimalValue(txtAmountCheck, 0);
            else
                val1 = 0;
            if (chkCreditCard.Checked == true)
                val2 = FormControls.GetDecimalValue(txtAmountCreditCard, 0);
            else
                val2 = 0;
            if (chkPureGold.Checked == true)
                val3 = FormControls.GetDecimalValue(txtPrice, 0);
            else
                val3 = 0;
            if (chkUseGold.Checked == true)
                val4 = FormControls.GetDecimalValue(txtPriceUseGold, 0);
            else
                val4 = 0;
            txtTotalReceive.Text = Math.Round((val + val1 + val2 + val3 + val4), 0).ToString();
        }

        private void txtAmount_KeyUp(object sender, KeyEventArgs e)
        {
            TotalReceive();
        }

        private void txtAmountCheck_KeyUp(object sender, KeyEventArgs e)
        {
            TotalReceive();
        }

        private void txtAmountCreditCard_KeyUp(object sender, KeyEventArgs e)
        {
            TotalReceive();
        }

        private void txtPrice_KeyUp(object sender, KeyEventArgs e)
        {
            TotalReceive();
        }

        private void txtPriceUseGold_KeyUp(object sender, KeyEventArgs e)
        {
            TotalReceive();
        }

        private void txtTotalReceive_TextChanged(object sender, EventArgs e)
        {
            decimal val, val1, val2, val3;
            val = FormControls.GetDecimalValue(txtTotalPrice, 0);
            val1 = FormControls.GetDecimalValue(txtDiscount, 0);
            val2 = FormControls.GetDecimalValue(txtReceived, 0);
            val3 = FormControls.GetDecimalValue(txtTotalReceive, 0);
            txtTotalBalance.Text = Math.Round(val - (val1 + val2 + val3), 0).ToString();
        }
    }
}
