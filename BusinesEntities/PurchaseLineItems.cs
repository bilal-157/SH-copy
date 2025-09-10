using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class PurchaseLineItems
   {
       public string PItemId { get; set; }
       public int PurchaseNo { get; set; }
       public string Description { get; set; }
       public int Qty { get; set; }
       public string Karat { get; set; }
       public decimal GrossWeight { get; set; }
       public decimal Wastage { get; set; }
       public decimal GoodWill { get; set; }
       public decimal Discount { get; set; }
       public decimal PureGold { get; set; }
    }
}
