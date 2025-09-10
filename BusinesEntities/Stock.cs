 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace BusinesEntities
{
    public class Stock
    {
        public Stock() {
            this.DesignNo = new Design();
        }
        private Nullable<bool> PFlag;
        public Nullable<bool> pFlag
        {
            get { return PFlag; }
            set { PFlag = value; }
        }
        private Nullable<bool> CostFlag;
        public Nullable<bool> costFlag
        {
            get { return CostFlag; }
            set { CostFlag = value; }
        }
        public int UserId { get; set; }
        public Silver Silver { get; set; }
        public int StockId { get; set; }
        public int OrderNo { get; set; }
        public string OItemId { get; set; }
        public int BQuantity { get; set; }
        public decimal BWeight { get; set; }
        public string BStatus { get; set; }
        public ItemFor ItemFor { get; set; }
        public string TagNo { get; set; }
        public string BarCode { get; set; }
        public Item ItemName { get; set; }
        private Nullable<decimal> totalWeight;

        //public Nullable<decimal> TotalWeight
        //{
        //    get { return totalWeight; }
        //    set { totalWeight = value; }
        //}
        public decimal TotalWeight { get; set; }

        public int SaleNo { get; set; }
        public string DesNo { get; set; }
        public string CustomerName { get; set; }
        public string BillBookNo { get; set; }
        public decimal StoneCharges { get; set; }
        //public decimal OtherCharges { get; set; }
        public Nullable<DateTime> SaleDate { get; set; }

        private Nullable<decimal> netWeight = null;
        private Nullable<decimal> otherCharges = null;

        public Nullable<decimal> OtherCharges
        {
            get { return otherCharges; }
            set { otherCharges = value; }
        }


        private Nullable<decimal> grossWeight = null;

        public Nullable<decimal> GrossWeight
        {
            get { return grossWeight; }
            set { grossWeight = value; }
        }
        //public decimal GrossWeight { get; set; }

        public Nullable<decimal> NetWeight
        {
            get { return netWeight; }
            set { netWeight = value; }
        }
        public string ChWtDesc { get; set; }
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
        public string RateType { get; set; }
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
        public DateTime StockDate { get; set; }
        public Worker WorkerName { get; set; }


        #region gold convrsion
        private Nullable<decimal> cKaat = null;

        public Nullable<decimal> CKaat
        {
            get { return cKaat; }
            set { cKaat = value; }
        }
        private Nullable<decimal> cWaste = null;

        public Nullable<decimal> CWaste
        {
            get { return cWaste; }
            set { cWaste = value; }
        }
        private Nullable<decimal> cPureWeight = null;

        public Nullable<decimal> CPureWeight
        {
            get { return cPureWeight; }
            set { cPureWeight = value; }
        }
        #endregion

        private Nullable<decimal> wasteInGm = null;
        public int Ratti { get; set; }

        public Nullable<decimal> WasteInGm
        {
            get { return wasteInGm; }
            set { wasteInGm = value; }
        }
        private Nullable<decimal> saleWasteInGm = null;

        public Nullable<decimal> SaleWasteInGm
        {
            get { return saleWasteInGm; }
            set { saleWasteInGm = value; }
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
        public Design DesignNo { get; set; }

        public string MakingType { get; set; }
        private Nullable<decimal> purchaseRate = null;

        public Nullable<decimal> PurchaseRate
        {
            get { return purchaseRate; }
            set { purchaseRate = value; }
        }
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


        private Nullable<decimal> oPWeight;

        public Nullable<decimal> OPWeight
        {
            get { return oPWeight; }
            set { oPWeight = value; }
        }
        //  public string ItemType { get; set; }
        public ItemType ItemType { get; set; }
        public decimal Disount { get; set; }

        //public List <StonesDetail> StoneDetail { get; set; }

        //public StonesDetail StoneD { get; set; }
        public List<Stones> StoneList { get; set; }

        private List<WeightLineItem> lineItems;
        public List<WeightLineItem> WeightLineItem
        {
            get
            {
                return this.lineItems;
            }
            set
            {
                this.lineItems = value;
            }
        }
        public void AddLineItems(WeightLineItem wli)
        {
            if (this.lineItems == null) lineItems = new List<WeightLineItem>();
            lineItems.Add(wli);
        }
        public void RemoveLineItems(WeightLineItem wli)
        {
            this.lineItems.Remove(wli);
        }


        public decimal GetWeight()
        {
            decimal w = 0;
            foreach (WeightLineItem wli in lineItems)
            {
                w += wli.Weight;
            }
            return w;
        }


        private byte[] imageMemory;

        public byte[] ImageMemory
        {
            get { return imageMemory; }
            set { imageMemory = value; }
        }

        private byte[] imageMemoryThumb;

        public byte[] ImageMemoryThumb
        {
            get { return imageMemoryThumb; }
            set { imageMemoryThumb = value; }
        }

        public Byte[] ConvertImageToBinary(Image imag)
        {
            MemoryStream ms = new MemoryStream();
            imag.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Byte[] bytes = ms.GetBuffer();
            return bytes;
        }

        public bool GoldOfWaste { get; set; }
    }
}
