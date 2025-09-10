using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class Worker:Person 
    {
       public string AccountCode { get; set;}
       public string Refernce { get; set; }
       public decimal MakingTola { get; set; }
       public decimal TKarrat { get; set; }
       public decimal OpeningCash { get; set; }
       public decimal OpeningGold { get; set; }
       public decimal CashBalance { get; set; }
       public decimal GoldBalance { get; set; }
       public Karat TransKarat { get; set; }
       public string Karat { get; set; }
       public Worker(int id, string name):base(id,name )
           
       { }
       public Worker() { }
       public Worker(int id) { this.ID = id; }
       public override string ToString()
       {
           return "Worker ID=" + this.ID  + "," + this.Name ;
       }
       public decimal Cheejad { get; set; }
    }
}
