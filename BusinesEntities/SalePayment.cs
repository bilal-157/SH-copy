using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class SalePayment
    {
       public int SaleNo { get; set; }
       public int ONo { get; set; }
       public string VNo { get; set; }
       public string PMode { get; set; }
       public decimal Amount { get; set; }
       public string Description { get; set; }
       public DateTime DDate { get; set; }
       public decimal DRate { get; set; }
       public string PTime { get; set; }
       public decimal Receiveable { get; set; }
       public decimal BDrate { get; set; }
       public string BankName { get; set; }
       public string DAccountCode { get; set; }
       public decimal GoldOfCash { get; set; }

       public int SaleManId { get; set; }
       public int CustId { get; set; }
    }
}
