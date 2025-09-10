using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class WorkerDealing
    {
        public string TagNo { get; set; }
        public Worker Worker { get; set; }
        public Item items { get; set; }
        public string VAT { get; set; }
        public string Description { get; set; }

        public string WItemId { get; set; }

        public int BillBookNo { get; set; }
        public DateTime EntryDate { get; set; }

        
        private Nullable<DateTime> addDate = null;
        public int WQty { get; set; }
      
        public List<Stones> WorkerStonesList { get; set; }
        public Nullable<DateTime> AddDate
        {
            get { return addDate; }
            set { addDate = value; }
        }
        private Nullable<DateTime> gDate=null;
        public Nullable<DateTime> GDate
            {
               get { return gDate; }
               set { gDate = value; }
            }
        public DateTime TDate { get; set; }
        private decimal ujrat;

        public decimal Ujrat
        {
            get { return ujrat; }
            set { ujrat = value; }
        }
        
        private Nullable<decimal> kaatInRatti=null;

        public Nullable<decimal> KaatInRatti
        {
            get { return kaatInRatti; }
            set { kaatInRatti = value; }
        }
        private Nullable<decimal> purity = null;

        public Nullable<decimal> Purity
        {
            get { return purity; }
            set { purity = value; }
        }
        private Nullable<decimal> wasteInRatti = null;

        public Nullable<decimal> WasteInRatti
        {
            get { return wasteInRatti; }
            set { wasteInRatti = value; }
        }
        
        public string Karrat { get; set; }
        private Nullable<decimal> pureWeight=null;
        public Nullable<decimal> PureWeight
        {
            get { return pureWeight; }
            set { pureWeight = value; }
        }

        private Nullable<decimal> stoneweight = null;
        public Nullable<decimal> StoneWeight
        {
            get { return stoneweight; }
            set { stoneweight = value; }
        }
        private Nullable<decimal> qty=null;

        public Nullable<decimal> Qty
        {
            get { return qty; }
            set { qty = value; }
        }
      
        private Nullable<decimal> receivedWeight=null;

        public Nullable<decimal> ReceivedWeight
        {
            get { return receivedWeight; }
            set { receivedWeight = value; }
        }
        private Nullable<decimal> givenWeight = null;

        public Nullable<decimal> GivenWeight
        {
            get { return givenWeight; }
            set { givenWeight = value; }
        }
        private Nullable<decimal> totalWeight=null;
        public Nullable<decimal> TotalWeight
        {
            get { return totalWeight; }
            set { totalWeight = value; }
        }
        public decimal UjratGiven { get; set; }
        public decimal CashGiven { get; set; }
        public decimal CashReceive { get; set; }

        public decimal CashBalance { get; set; }

        public decimal WorkerGoldBalance { get; set; }
        public decimal CashInHand { get; set; }
        public decimal GoldBalance { get; set; }
        public decimal CheejadDecided { get; set; }
        public decimal Cheejad{ get; set; }

        public decimal LabourMakingPerPiece { get; set; }
        public decimal LabourMakingPerGram { get; set; }
        public decimal LabourWeight { get; set; }
        public decimal LabourPiece { get; set; }
        public decimal Masha { get; set; }
        public decimal Toola { get; set; }
        public decimal Ratti { get; set; }
        public int TransId { get; set; }
        
        public decimal Kaat { get; set; }
        public decimal Piece { get; set; }
        public decimal PieceMaking { get; set; }
        public string WVNO { get; set; }
        public string CVNO { get; set; }
        public decimal PCheejad { get; set; }
        public decimal MakingTola { get; set; }
        public decimal StoneCharges { get; set; }
        private Nullable<decimal> tocashgold = null;

        public Nullable<decimal> ToCashGold
        {
            get { return tocashgold; }
            set { tocashgold = value; }
        }
        private Nullable<decimal> goldrate = null;
        public Nullable<decimal> GoldRate
        {
            get { return goldrate; }
            set { goldrate = value; }
        }
        public string Status { get; set; }
        public string GRStatus { get; set; }
        public decimal WeightCash { get; set; }
        public decimal GoldGivenReceive { get; set; }
    }
}
