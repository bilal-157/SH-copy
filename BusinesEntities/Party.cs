using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class Party
    {
       public string PName { get; set; }
       public int PCode { get; set; }
       public string PAddress { get; set; }
       public string PtclNo { get; set; }
       public string PMob { get; set; }
       public string PEmail { get; set; }
       public Party() { }

      public Party(int id,string name) 
      {
          this.PCode = id;
          this.PName = name;
      }
       public Party(int id) { this.PCode = id; }
       public string AccountCode { get; set; }




    }
}
