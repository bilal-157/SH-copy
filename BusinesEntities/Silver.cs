using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Silver
    {
        private Nullable<decimal> rateA=null;

        public Nullable<decimal> RateA
        {
            get { return rateA; }
            set { rateA = value; }
        }

        private Nullable<decimal> priceA = null;

        public Nullable<decimal> PriceA
        {
            get { return priceA; }
            set { priceA = value; }
        }
        private Nullable<decimal> rateD = null;

        public Nullable<decimal> RateD
        {
            get { return rateD; }
            set { rateD = value; }
        }
        private Nullable<decimal> priceD = null;

        public Nullable<decimal> PriceD
        {
            get { return priceD; }
            set { priceD = value; }
        }
        private Nullable<decimal> salePrice = null;

        public Nullable<decimal> SalePrice
        {
            get { return salePrice; }
            set { salePrice = value; }
        }
    }
}
