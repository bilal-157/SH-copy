using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class LooseStone
    {
       public Party party { get; set; }
       //public Stone Stone.TypeName { get; set; }
       //public Stone StonesName { get; set; }
       public Stone Stone { get; set; }
       public decimal Weight { get; set; }
       public int Qty { get; set; }
       public decimal Rate { get; set; }
       public decimal Price { get; set; }
       public DateTime date { get; set; }
       public int LspId { get; set; }
       public string AccountCode { get; set; }
       public string VNO { get; set; }
    }
}
