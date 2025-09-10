using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class SampleLineItem
    {
        private Stock stock;

        public Stock Stock
        {
            get { return stock; }
            set { stock = value; }
        }

        private Nullable<int> sQty=null;

        public Nullable<int> SQty
        {
            get { return sQty; }
            set { sQty = value; }
        }
        private Nullable<decimal> sampleWt=null;

        public Nullable<decimal> SampleWt
        {
            get { return sampleWt; }
            set { sampleWt = value; }
        }
        public string Description { get; set; }

        public int SampleNo { get; set; }

        private Customer customer;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public DateTime SampleDate { get; set; }

        public DateTime ReturnDate { get; set; }

        public string BillBookNo { get; set; }

        private Nullable<int> returnQty=null;

        public Nullable<int> ReturnQty
        {
            get { return returnQty; }
            set { returnQty = value; }
        }
        private Nullable<decimal> returnWt=null;

        public Nullable<decimal> ReturnWt
        {
            get { return returnWt; }
            set { returnWt = value; }
        }

    }
}
