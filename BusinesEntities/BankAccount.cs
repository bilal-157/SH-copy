using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class BankAccount
    {
       public int Id { get; set; }
      public  Banks BankName { get; set; }
      public ChildAccount AccountCode { get; set; }
      public decimal OpeningBalace { get; set; }
      public string AccountNo { get; set; }

      public BankAccount() {

          this.AccountCode = new ChildAccount();
          this.BankName = new Banks();
      }
    }
}
