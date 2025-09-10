using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class WeightLineItem
    {
        public string TagNo { get; set; }
        public Item ItemId { get; set; }
        private Nullable<int> sID = null;

        public Nullable<int> SID
        {
            get { return sID; }
            set { sID = value; }
        }

        //private Nullable<decimal> weight = null;

        //public Nullable<decimal> Weight
        //{
        //    get { return weight; }
        //    set { weight = value; }
        //}
        public decimal Weight { get; set; }

        public decimal GetTotalWeight()
        {
            return Convert.ToDecimal(this.Weight);
        }

    }
}
