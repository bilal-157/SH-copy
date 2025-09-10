using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
  public   class Banks
    {
      public int Id { get; set; }
      public string BankName { get; set; }
      public decimal DRate { get; set; }
      public ParentAccount  ParentCode { get; set; }
      public Banks(int id, string name)
      {
          this.Id = id;
          this.BankName = name;
      }
      public Banks(int id, string name,decimal drate)
      {
          this.Id = id;
          this.BankName = name;
          this.DRate = drate;
      }
      public Banks() { }

    }
}
