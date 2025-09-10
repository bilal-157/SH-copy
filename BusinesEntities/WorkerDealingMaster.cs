using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class WorkerDealingMaster
    {
        private Nullable<int> billbookno = null;
        public Nullable<int> BillBookNo
        {
            get { return billbookno; }
            set { billbookno = value; }
        }

        private Nullable<decimal> previousgoldbalance = null;
        public Nullable<decimal> PreveiousGoldBalance
        {
            get { return previousgoldbalance; }
            set { previousgoldbalance = value; }
        }

        private Nullable<DateTime> date = null;

        public Nullable<DateTime> Date
        {

            get { return date; }
            set { date = value; }
        }

        private Nullable<decimal> receivetotalgold = null;
        public Nullable<decimal> ReceiveTotalGold
        {
            get { return receivetotalgold; }
            set { receivetotalgold = value; }
        }

        private Nullable<decimal> givengold = null;
        public Nullable<decimal> GivenGold
        {
            get { return givengold; }
            set { givengold = value; }
        }

        private Nullable<decimal> balance = null;
        public Nullable<decimal> Balance
        {
            get { return balance; }
            set { balance = value; }
        }

        private Nullable<decimal> totalprice = null;
        public Nullable<decimal> TotalPrice
        {
            get { return totalprice; }
            set { totalprice = value; }
        }

        private List<WorkerLineItem> lineItems;

        public List<WorkerLineItem> WorkerLineItem
        {
            get
            {
                return this.lineItems;
            }
        }
        public void AddLineItems(WorkerLineItem wli)
        {
            if (this.lineItems == null) lineItems = new List<WorkerLineItem>();
            lineItems.Add(wli);
        }
        public void RemoveLineItems(WorkerLineItem wli)
        {

            this.lineItems.Remove(wli);
        }










    }
}

