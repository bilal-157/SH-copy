using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class SaleLineItem
    {
        private int qty;
       // public ItemType IType { get; set; }
       // public Stock Stock { get; set; }
        private Stock stock;

        public Stock Stock
        {
            get { return this. stock; }
            set {this . stock = value; }
        }
        public decimal  pureWeight { get; set; }
        private decimal gRate;
        private decimal sPureWight;

        public decimal SPureWight
        {
            get { return sPureWight; }
            set { sPureWight = value; }
        }
        
        public decimal GRate
        {
            get { return gRate; }
            set { gRate = value; }
        }
        public decimal TotalWeight { get; set; }
        public decimal TotalMaking { get; set; }
        public decimal TotalLaker { get; set; }
        public decimal StoneCharges { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Discount { get; set; }
        public decimal grossTotal { get; set; }
       
       
       //public Stock stock;
        public SaleLineItem( ){}
        public SaleLineItem(Stock stock ,decimal gRate)
        {
            this.Stock = stock;
            this.gRate = gRate;                               
        }

        public bool Bool { get; set; }
        public decimal GPrice { get; set; }
        public decimal ChangeWeight { get; set; }
        public int ChangeQty { get; set; }
       
        public decimal Rate 
        {
            get { return this.gRate; }
            set { this.gRate = value; }
        }
        public decimal  GoldCharges()
        {
            return this.stock.TotalWeight *(decimal)this .Stock .RatePerGm;
        }
       //laker+making+stonecharges.......
        public decimal OtherChargesTotal()
        {
            return (Convert.ToDecimal(this.stock.TotalLaker + this.stock.TotalMaking + this.stock.StoneCharges + this.stock.OtherCharges));
        }

        public decimal SOPWeight()
        {
            return (Convert.ToDecimal(this .Stock .OPWeight ));
        }
        public decimal GetGrossTotal()
        {
            return Convert.ToDecimal(this.GetOtherCharges() + this.GoldCharges() + this.GetMaking() + GetTotalLaker() + GetStoneCharges() + GetDiscount());
        }

        public decimal GetOGrossTotal()
        {
            return Convert.ToDecimal(this.GetOtherCharges()+ this.GetMaking() + GetTotalLaker() + GetStoneCharges() + GetDiscount());
        }
        public decimal GetSilverGrossTotal()
        {
            return Convert.ToDecimal(this.stock.NetAmount);
        }
        public decimal  GetNetTotal()
        {
            return(decimal)this.Stock .NetAmount;
        }
       public decimal GetMaking()
        {
            return (Convert.ToDecimal(this.stock.TotalMaking));
            
        }
       public decimal GetTotalLaker()
       {
           return (Convert.ToDecimal(this.stock.TotalLaker));
       }
       public decimal GetOtherCharges()
       {
           return Convert.ToDecimal(this.stock.OtherCharges);
       }
       public decimal GetStoneCharges()
       {
           return Convert.ToDecimal(this.stock.StoneCharges);
       }
       public decimal GetDiscount()
       {
           return Convert.ToDecimal(this.stock .Disount );
       }
       public decimal GetTotalGoldWeight()
       {
           return Convert.ToDecimal(stock.TotalWeight);
       }
       public decimal ConvertedPureWeight()
       {
           return Convert.ToDecimal(this.stock.CPureWeight);
       }
       public decimal ItemNetTotal()
       {
           return (decimal)this.Stock.TotalPrice;
       }
    }
}
