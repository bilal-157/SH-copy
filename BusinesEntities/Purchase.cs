using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class Purchase
    {
      
        public Supplier supplier { get; set; }
        public int PurchaseNo { get; set; }
        public List<PurchaseSubLineItems> PurchaseSubLineItems { get; set; }
        public List<PurchaseLineItems> PurchaseLineItems { get; set; }
        public string PAccountCode { get; set; }
        public decimal TotalWeight { get; set; }
        public decimal TotalMaking { get; set; }
        public decimal TotalWastage { get; set; }
        public decimal TotalGoodWill { get; set; }
        public decimal TotalPurchaseDiscount { get; set; }
        public decimal TotalPureGold { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string ReceivedBy { get; set; }
        public string Comments { get; set; }
        public string VNO { get; set; }

        public void AddSubLineItems(PurchaseSubLineItems psli)
        {
            if (PurchaseSubLineItems == null) PurchaseSubLineItems = new List<PurchaseSubLineItems>();
            PurchaseSubLineItems.Add(psli);
        }
        public void RemoveLineItems(PurchaseSubLineItems sli)
        {

            this.PurchaseSubLineItems.Remove(sli);
        }

        public void AddLineItems(PurchaseLineItems pli)
        {
            if (PurchaseLineItems == null) PurchaseLineItems = new List<PurchaseLineItems>();
            PurchaseLineItems.Add(pli);
        }
        public void RemoveLineItems(PurchaseLineItems pli)
        {

            this.PurchaseLineItems.Remove(pli);
        }
        public Purchase() {
            this.PurchaseLineItems = new List<PurchaseLineItems>();
            this.PurchaseSubLineItems = new List<PurchaseSubLineItems>();
            this.supplier = new Supplier();
        }
    }
}
