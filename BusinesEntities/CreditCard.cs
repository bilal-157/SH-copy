using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class CreditCard
    {
       public int SNO { get; set; }
       public string MachineName { get; set; }
       public decimal Amount { get; set; }
       public decimal DeductRate { get; set; }
       public decimal SwapAmount { get; set; }
       public Banks BankName { get; set; }
       public decimal BankDeductRate { get; set; }
       public decimal AmountDeposit { get; set; }
       public BankAccount  DepositInAccount { get; set; }
       public string Description { get; set; }
       public string VNO { get; set; }
       public int ONO { get; set; }
       public string Status { get; set; }
       public decimal AmountDepositeBank{get;set;}
       
       public string AccountCode { get; set; }
       
    }
}
