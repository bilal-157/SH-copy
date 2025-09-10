using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Sale
    {
        private int saleNo;
       
        public int SaleNo
        {
            get { return saleNo; }
            set { saleNo = value; }
        }
        private Nullable<int> orderNo = null;

        public Nullable<int> OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }
        private Nullable<DateTime> sDate = null;

        public Nullable<DateTime> SDate
        {

            get { return sDate; }
            set { sDate = value; }
        }
        private Nullable<DateTime> odate = null;

        public Nullable<DateTime> ODate
        {

            get { return odate; }
            set { odate = value; }
        }
        private Nullable<DateTime> ddate = null;

        public Nullable<DateTime> DDate
        {

            get { return ddate; }
            set { sDate = value; }
        }
        private Nullable<decimal> totalprice = null;

        public Nullable<decimal> TotalPrice
        {

            get { return totalprice; }
            set { totalprice = value; }
        }
        private Nullable<decimal> discount = null;

        public Nullable<decimal> Discount
        {

            get { return discount; }
            set { discount = value; }
        }
        private Nullable<decimal> advance = null;

        public Nullable<decimal> Advance
        {

            get { return advance; }
            set { advance = value; }
        }
       
        //private Nullable<decimal> netamount = null;

        //public Nullable<decimal> NetAmount
        //{

        //    get { return netamount; }
        //    set { netamount = value; }
        //}
        private Nullable<decimal> cashReceive = null;

        public Nullable<decimal> CashReceive
        {
            get { return cashReceive; }
            set { cashReceive = value; }
        }

        private Nullable<decimal> orderRate = null;

        public Nullable<decimal> OrderRate
        {
            get { return orderRate; }
            set { orderRate = value; }
        }
        private Nullable<decimal> treceivedamount = null;

        public Nullable<decimal> TReceivedAmount
        {

            get { return treceivedamount; }
            set { treceivedamount = value; }
        }
        private Nullable<decimal> balance = null;

        public Nullable<decimal> Balance
        {

            get { return balance; }
            set { balance = value; }
        }
        private Nullable<decimal> custBalance = null;

        public Nullable<decimal> CustBalance
        {

            get { return custBalance; }
            set { custBalance = value; }
        }
       // private Nullable<decimal> tReceivedAmount = null;
        public SaleMan SalesMan { get; set; }
        public Customer CustName { get; set; }
        public decimal TotalMaking { get; set; }
        public decimal TotalLaker { get; set; }
        public decimal TotalOtherCharges { get; set; }
        public decimal StoneCharges { get; set; }
        public decimal TotalGoldPrice { get; set; }
        //public decimal TotalPrice { get; set; }
        public decimal TotalItemDiscount { get; set; }
        public decimal TotalNetPrice { get; set; }
        public decimal BillDiscout { get; set; }
        public decimal NetBill { get; set; }
        public decimal CashPayment { get; set; }
        public decimal epureWeight { get; set; }
        public decimal eusedWeight { get; set; }
        public decimal OtherChergesReceivedGold { get; set; }
        #region LineItem Code
        private List<SaleLineItem> lineItems;
        public List<SaleLineItem> SaleLineItem
          
        {
            get 
            {
                return this.lineItems;
            }
        }
        public void AddLineItems(SaleLineItem sli)
        {
            if (this.lineItems == null) lineItems = new List<SaleLineItem>();
            lineItems.Add(sli);
        }
        public void RemoveLineItems(SaleLineItem sli)
        {
            
            this.lineItems.Remove(sli);
        }
        //methods
        public decimal GetGrossWeight()
        {
            decimal w = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                w += sli.GrossWeight;
            }
            return w;
        }
        public decimal GetNetTotal()
        {
            decimal  sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetGrossTotal();
            }
            return sum;
        }
        public decimal  GetTotalOtherCharges()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.OtherChargesTotal();
            }
            return sum;
        }

        public decimal GetToltalOPWeight()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.SOPWeight ();
            }
            return sum;
        }
        public decimal  GetTotalGoldPrice()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GoldCharges();
            }
            return sum;
        }
        public decimal GetTotalMaking()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetMaking();
            }
            return sum;
        }
        public decimal GetTotalLaker()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetTotalLaker();
            }
            return sum;
        }
        public decimal GetOtherChargesTotal()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetOtherCharges();
            }
            return sum;
        }
        public decimal GetAllStoneCharges()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetStoneCharges();
            }
            return sum;
        }
        public decimal GetTotalDisount()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetDiscount();
            }
            return sum;
        }
        public decimal GetGrossTotal()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetGrossTotal();
            }
            return sum;
        }

        public decimal GetOGrossTotal()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetOGrossTotal();
            }
            return sum;
        }
        public decimal GetSilverGrossTotal()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += (decimal)sli.Stock .NetAmount ;
            }
            return sum;
        }
        public decimal GetTotalGolWeight()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.GetTotalGoldWeight();
            }
            return sum;
        }
        public decimal GetTotalCPuerWeight()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.ConvertedPureWeight();
            }
            return sum;
        }
        public decimal GetItemNetTotal()
        {
            decimal sum = 0;
            foreach (SaleLineItem sli in lineItems)
            {
                sum += sli.ItemNetTotal();
            }
            return sum;
        }
        #endregion
        public string SalemanName { get; set; }
        public string CusAccountNo { get; set; }
        public string BillBookNo { get; set; }
        public string CVNO { get; set; }
        public string ChVNO { get; set; }
        public string CCVNO { get; set; }
        public string KNO { get; set; }
        public string OrderTaker { set; get; }
        public string OECVno { get; set; }
        public string OEChVNO { get; set; }
        public string OECCVno { get; set; }
        public string Status { get; set; }
        public string SVNO { get; set; }
        public string AGVNo { get; set; }
        public string OEAGVNO { get; set; }
        public string OESVNO { get; set; }

        private Nullable<int> khataNo = null;

        public Nullable<int> KhataNo
        {

            get { return khataNo; }
            set { khataNo = value; }
        }
        private Nullable<DateTime> promiseDate = null;

        public Nullable<DateTime> PromiseDate
        {

            get { return promiseDate; }
            set { promiseDate = value; }
        }
        private Nullable<decimal> baddats = null;

        public Nullable<decimal> Baddats
        {

            get { return baddats; }
            set { baddats = value; }
        }

        public string OEUGVNo { get; set; }
        public string UGVno { get; set; }
        public string BillInWord { get; set; }
        private Nullable<decimal> otherChargesRecievd = null;

        public Nullable<decimal> OtherChargesRecievd
        {
            get { return otherChargesRecievd; }
            set { otherChargesRecievd = value; }
        }
        private Nullable<decimal> othrChargesGold = null;

        public Nullable<decimal> OthrChargesGold
        {
            get { return othrChargesGold; }
            set { othrChargesGold = value; }
        }
        private Nullable<decimal> goldChargesGold = null;

        public Nullable<decimal> GoldChargesGold
        {
            get { return goldChargesGold; }
            set { goldChargesGold = value; }
        }
        private Nullable<decimal> netBillGold = null;
       
        public Nullable<decimal> NetBillGold
        {
            get { return netBillGold; }
            set { netBillGold = value; }
        }
        private Nullable<decimal> totalGold = null;

        public Nullable<decimal> TotalGold
        {
            get { return totalGold; }
            set { totalGold = value; }
        }

        private Nullable<decimal> goldReceived = null;

        public Nullable<decimal> GoldReceived
        {
            get { return goldReceived; }
            set { goldReceived = value; }
        }
        private Nullable<decimal> goldBalance = null;

        public Nullable<decimal> GoldBalance
        {
            get { return goldBalance; }
            set { goldBalance = value; }
        }


        //public Nullable<decimal> OtherCharges
        //{
        //    get { return OtherCharges; }
        //    set { OtherCharges = value; }
        //}

        private Nullable<decimal> creditCard = null;

        public Nullable<decimal> CreditCard
        {
            get { return creditCard; }
            set { creditCard = value; }
        }
        private Nullable<decimal> checkCash = null;

        public Nullable<decimal> CheckCash
        {
            get { return checkCash; }
            set { checkCash = value; }
        }
        private Nullable<decimal> usedGoldCharges = null;


        public Nullable<decimal> UsedGoldCharges
        {
            get { return usedGoldCharges; }
            set { usedGoldCharges = value; }
        }
        private Nullable<decimal> pureGoldCharges = null;

        public Nullable<decimal> PureGoldCharges
        {
            get { return pureGoldCharges; }
            set { pureGoldCharges = value; }
        }

        private Nullable<decimal> cPureGold = null;

        public Nullable<decimal> CPureGold
        {
            get { return cPureGold; }
            set { cPureGold = value; }
        }

        public List<CreditCard> ListOfCreditCards { get; set; }
        public List<Cheques> ListOfChequs { get; set; }
        //public List<Gold> ListOfUsedGold { get; set; }
        //public List<Gold> ListOfPureGold { get; set; }
        private Nullable<decimal> tOtherChargesCash = null;

        public Nullable<decimal> TOtherChargesCash
        {
            get { return tOtherChargesCash; }
            set { tOtherChargesCash = value; }
        }

        private Nullable<decimal> tOtherChargesGold = null;

        public Nullable<decimal> TOtherChargesGold
        {
            get { return tOtherChargesGold; }
            set { tOtherChargesGold = value; }
        }
        private Nullable<decimal> oFixRate = null;
        public Nullable<decimal> OFixRate
        {
            get { return oFixRate; }
            set { oFixRate = value; }
        }
    }
}
 
        
   

    
