using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace BusinesEntities
{
    public class OrderLineItem
    {
        #region OrderDetail
        public string OItemId { get; set; }
        public ItemFor ItemFor { get; set; }
        public Item ItemName { get; set; }
        
        private Nullable<decimal> totalWeight;

        public string DesignNo { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal StoneCharges { get; set; }
        public decimal OtherCharges { get; set; }
        public DateTime SaleDate { get; set; }

        private Nullable<decimal> netWeight = null;
        private Nullable<decimal> grossWeight = null;

        public Nullable<decimal> GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }

        public Nullable<decimal> NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }
        //public string ChWtDesc { get; set; }
        public string Status { get; set; }

        private Nullable<decimal> discount = null;

        public Nullable<decimal> Discount
        {
            get { return discount; }
            set { discount = value; }
        }
        private Nullable<decimal> netAmount = null;

        public Nullable<decimal> NetAmount
        {
            get { return netAmount; }
            set { netAmount = value; }
        }

        private Nullable<decimal> saleWeight = null;

        public Nullable<decimal> SaleWeight
        {
            get { return saleWeight; }
            set { saleWeight = value; }
        }
        public string ItemSize { get; set; }
        private Nullable<int> qty = null;

        public Nullable<int> Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        private Nullable<int> stQty;

        public Nullable<int> StQty
        {
            get { return stQty; }
            set { stQty = value; }
        }
        private Nullable<int> pieces = null;

        public Nullable<int> Pieces
        {
            get { return pieces; }
            set { pieces = value; }
        }
        private Nullable<int> saleQty = null;

        public Nullable<int> SaleQty
        {
            get { return saleQty; }
            set { saleQty = value; }
        }

        public string Karrat { get; set; }

        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public Worker WorkerName { get; set; }




        private Nullable<decimal> wasteInGm = null;
        public int Ratti { get; set; }

        public Nullable<decimal> WasteInGm
        {
            get { return wasteInGm; }
            set { wasteInGm = value; }
        }

        private Nullable<decimal> wastePercent = null;

        public Nullable<decimal> WastePercent
        {
            get { return wastePercent; }
            set { wastePercent = value; }
        }

        private Nullable<decimal> kaatInRatti;

        public Nullable<decimal> KaatInRatti
        {
            get { return kaatInRatti; }
            set { kaatInRatti = value; }
        }

        private Nullable<decimal> pWeight = null;

        public Nullable<decimal> PWeight
        {
            get { return pWeight; }
            set { pWeight = value; }
        }

        private Nullable<decimal> lakerGm = null;

        public Nullable<decimal> LakerGm
        {
            get { return lakerGm; }
            set { lakerGm = value; }
        }

        private Nullable<decimal> totalLaker = null;

        public Nullable<decimal> TotalLaker
        {
            get { return totalLaker; }
            set { totalLaker = value; }
        }

        private Nullable<decimal> ratePerGm = null;

        public Nullable<decimal> RatePerGm
        {
            get { return ratePerGm; }
            set { ratePerGm = value; }
        }

        private Nullable<decimal> makingPerGm = null;

        public Nullable<decimal> MakingPerGm
        {
            get { return makingPerGm; }
            set { makingPerGm = value; }
        }

        private Nullable<decimal> makingPerTola = null;

        public Nullable<decimal> MakingPerTola
        {
            get { return makingPerTola; }
            set { makingPerTola = value; }
        }

        private Nullable<decimal> totalMaking = null;

        public Nullable<decimal> TotalMaking
        {
            get { return totalMaking; }
            set { totalMaking = value; }
        }


        private Nullable<decimal> totalPrice = null;

        public Nullable<decimal> TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }
        private Nullable<int> wTola = null;

        public Nullable<int> WTola
        {
            get { return wTola; }
            set { wTola = value; }
        }

        private Nullable<int> wMasha = null;

        public Nullable<int> WMasha
        {
            get { return wMasha; }
            set { wMasha = value; }
        }

        private Nullable<int> wRatti = null;

        public Nullable<int> WRatti
        {
            get { return wRatti; }
            set { wRatti = value; }
        }

        private Nullable<int> pTola = null;

        public Nullable<int> PTola
        {
            get { return pTola; }
            set { pTola = value; }
        }

        private Nullable<int> pMasha = null;

        public Nullable<int> PMasha
        {
            get { return pMasha; }
            set { pMasha = value; }
        }

        private Nullable<int> pRatti = null;

        public Nullable<int> PRatti
        {
            get { return pRatti; }
            set { pRatti = value; }
        }

        private Nullable<int> tTola = null;

        public Nullable<int> TTola
        {
            get { return tTola; }
            set { tTola = value; }
        }

        private Nullable<int> tMasha = null;

        public Nullable<int> TMasha
        {
            get { return tMasha; }
            set { tMasha = value; }
        }

        private Nullable<int> tRatti = null;

        public Nullable<int> TRatti
        {
            get { return tRatti; }
            set { tRatti = value; }
        }
        //public Design DesignNo { get; set; }

        public string MakingType { get; set; }
        private Nullable<decimal> itemCost = null;

        public Nullable<decimal> ItemCost
        {
            get { return itemCost; }
            set { itemCost = value; }
        }
        private Nullable<decimal> salePrice;

        public Nullable<decimal> SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }



        //  public string ItemType { get; set; }
        public ItemType ItemType { get; set; }
        public decimal Disount { get; set; }

        //public List <StonesDetail> StoneDetail { get; set; }

        //public StonesDetail StoneD { get; set; }
        public List<Stones> StoneList { get; set; }







        private byte[] imageMemory;

        public byte[] ImageMemory
        {
            get { return imageMemory; }
            set { imageMemory = value; }
        }

        public Byte[] ConvertImageToBinary(Image imag)
        {
            MemoryStream ms = new MemoryStream();
            //imag.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            imag.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Byte[] bytes = ms.GetBuffer();
            return bytes;
        }


      
        #endregion

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
        //public decimal TotalWeight { get; set; }
        //public decimal TotalMaking { get; set; }
        //public decimal TotalLaker { get; set; }
        //public decimal StoneCharges { get; set; }
        //public decimal GrossWeight { get; set; }
        //public decimal NetTotal { get; set; }
        //public decimal Discount { get; set; }
        //public decimal grossTotal { get; set; }
       
       
       //public Stock stock;
        public OrderLineItem( ){}
        public OrderLineItem(Stock stock, decimal gRate)
        {
            this.Stock = stock;
            this.gRate = gRate;
            
        
           
        }

        public DateTime ReceiveDate { get; set; }
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
            return (Convert .ToDecimal  (this .stock .TotalLaker +this .stock .TotalMaking +this .stock .StoneCharges+this .stock .OtherCharges )) ;
        }
        public decimal GetGrossTotal()
        {
            return Convert.ToDecimal(this.stock.TotalPrice);
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
           return Convert .ToDecimal (this .stock .CPureWeight );
       }
       public decimal ItemNetTotal()
       {
           return (decimal)this.Stock .NetAmount ;
       }
       
      
    }
}
