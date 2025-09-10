using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Gold
    {
        public int SNO { get; set; }
        public int ONO { get; set; }
        public int CustId { get; set; }
        public int WorkerId { get; set; }
        public string VNO { get; set; }
        public string CPVNO { get; set; }
        public DateTime PGDate { get; set; }
        public int GSNO { get; set; }
        public string GType { get; set; }
        public SaleMan SaleMan { get; set; }
        public decimal PAmount { get; set; }
        public decimal RAmount { get; set; }
        public int SaleManId { get; set; }
        public decimal CashPR { get; set; }
        public decimal Weight { get; set; }
        public decimal Kaat { get; set; }
        public int GPNO { get; set; }
        public string Karat { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string PTime { get; set; }
        public string PMode { get; set; }
        public decimal PWeight { get; set; }
        public string AccountNO { get; set; }
        public string RemainingWork { get; set; }
        public GoldType GoldType { get; set; }
        public int TranId { get; set; }
        public JewelPictures JewelPictures { get; set; }
        public Item item { get; set; }
    }
}
