using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class RepairLineItem
    {
        public int RepairId { get; set; }

        public string ItemId { get; set; }

        public decimal ReceiveWeight { get; set; }
        public int Qty { get; set; }

        public string ItemName { get; set; }

        public string ItemStatus { get; set; }

        public string RepairingStatus { get; set; }

        public string Description { get; set; }

        private Nullable<decimal> repairWeight=null;

        public Nullable<decimal> RepairWeight
        {
            get { return repairWeight; }
            set { repairWeight = value; }
        }

        public string Karat { get; set; }

        private Nullable<decimal> goldRate=null;

        public Nullable<decimal> GoldRate
        {
            get { return goldRate; }
            set { goldRate = value; }
        }

        private Nullable<decimal> lacker = null;

        public Nullable<decimal> Lacker
        {
            get { return lacker; }
            set { lacker = value; }
        }

        private Nullable<decimal> making = null;

        public Nullable<decimal> Making
        {
            get { return making; }
            set { making = value; }
        }

        private Nullable<decimal> stonePrice = null;

        public Nullable<decimal> StonePrice
        {
            get { return stonePrice; }
            set { stonePrice = value; }
        }

        private Nullable<int> workerId=null ;

        public Nullable<int> WorkerId
        {
            get { return workerId; }
            set { workerId = value; }
        }

        private Nullable<decimal> repairCharges=null;

        public decimal PerItemCost { get; set; }

        public Nullable<decimal> RepairCharges
        {
            get { return repairCharges; }
            set { repairCharges = value; }
        }

        public List<Stones> StoneList { get; set; }

        public Worker WorkerName { get; set; }
        //public decimal perItemTotal()
        //{
        //    return Convert.ToDecimal();
        //}

      
    }
}
