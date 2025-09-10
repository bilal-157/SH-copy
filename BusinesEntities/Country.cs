using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class Country
    {
       public int CountryId {get;set;}
       public string CountryName {get; set;}

       public Country(int id, string name)
       {
           this.CountryId = id;
           this.CountryName = name; 
       }
       public Country() { }
    }
}
