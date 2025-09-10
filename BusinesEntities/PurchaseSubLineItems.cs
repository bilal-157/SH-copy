using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
  public class PurchaseSubLineItems
    {
        public string PItemId { get; set; }
        public int PurchaseNo { get; set; }
        public int ItemId { get; set; }
        public string ItemDescription { get; set; }
        public int Qty { get; set; }
        public string Karat { get; set; }
        public decimal Weight { get; set; }
        public decimal Making { get; set; }
        public decimal SubWastage { get; set; }
        public decimal SubDOP { get; set; }
        public decimal SubPureGold { get; set; }
        public List<Stones> StoneList { get; set; }

        public PurchaseSubLineItems() {
            this.StoneList = new List<Stones>();
        
        }
    }
}
