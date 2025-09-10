using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class ChildAccount
   {
       public int HeadCode { get; set; }
       public string GroupCode { get; set; }
       public string SubGroupCode { get; set; }
       public string ParentCode { get; set; }
       public string ChildCode { get; set; }
       public string VNO { get; set; }
       public string ChildName { get; set; }
       public string AccountType { get; set; }
       public string Status { get; set; }
       public decimal Balance { get; set; }
       public decimal GoldBalance { get; set; }
       public decimal OpCash { get; set; }
       public decimal OpGold { get; set; }
       public DateTime DDate { get; set; }
       public string Description { get; set; }
       public bool DeleteCheck { get; set; }
       public AccType Atype { get; set; }
       public ChildAccount(string chcode)
       {
           this.ChildCode = chcode;
       }
       public ChildAccount() { }
    }
}
