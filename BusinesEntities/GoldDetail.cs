using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class GoldDetail
    {
        public int SaleNo { get; set; }
        public int ONo { get; set; }
        public string VNo { get; set; }
        public DateTime Date { get; set; }
        public string GType { get; set; }
        public decimal Weight { get; set; }
        public decimal Kaat { get; set; }
        public string Karat { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }        
    }
}
