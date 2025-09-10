using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BusinesEntities
{
   public  class Reparing
    {

       public int RepairId { get; set; }
        //public int Advance { get; }


       public int CustId { get; set; }
       public int SaleManId { get; set; }
       public int WorkerId { get; set; }
       public string BillBookNo { get; set; }
        public string Status { get; set; }
     
        public decimal TotalRepairCost { get; set; }
        public decimal Remaining { get; set; }
        public Customer CustName { get; set; }

        private Nullable<decimal> discount = null;

        public Nullable<decimal> Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private Nullable<decimal> advance = null ;

        public Nullable<decimal> Advance
        {
            get { return advance; }
            set { advance = value; }
        }

        public DateTime ReceiveDate { get; set; }

        public DateTime GivenDate { get; set; }


        //private List<RepairLineItem> RepairlineItems;
        private List<RepairLineItem> rLineItems;
        public List<RepairLineItem> RepairlineItem
        {
            get
            {
                return this.rLineItems; 
            }
        }

        public void AddRLineItems(RepairLineItem rli)
        {
            if (this.rLineItems == null) rLineItems = new List<RepairLineItem>();
            rLineItems.Add(rli);
            
        }

        public void RemoveLineItems(RepairLineItem  rli)
        {

            this.rLineItems.Remove(rli);
        }

        //public decimal repairLineTotal()
        //{
        //    decimal sum = 0;
        //    foreach (Reparing rep in rLineItems )
        //    {
        //        sum += rep.TotalRepairCost;
        //    }
        //    return sum;
        //}


    }
}
