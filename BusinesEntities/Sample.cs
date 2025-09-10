using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Sample
    {
        private string tagNum;

        public string TagNum
        {
            get { return tagNum; }
            set { tagNum = value; }
        }
        public int SampleNo { get; set; }

        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public DateTime SampleDate { get; set; }

        public string BillBookNo { get; set; }
        //public int BillBookNo { get; set; }

        private List<SampleLineItem> lineItems;

        public List<SampleLineItem> SampleLineItems
        {
            get { return this.lineItems; }
           // set { lineItems = value; }
        }

        public void AddLineItems(SampleLineItem sli)
        {
            if (this.lineItems == null) lineItems = new List<SampleLineItem>();
            lineItems.Add(sli);
        }
        public void RemoveLineItems(SampleLineItem sli)
        {
            this.lineItems.Remove(sli);
        }
        
    }
}
