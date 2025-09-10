using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
  public   class Item
    {
      public int  ItemId{get;set;}
      public int DesItmid { get; set; }
      public string ItemName { get; set; }
      public string Abrivation { get; set; }
      public Design DesignItem { get; set; }


      public Item() { }
      public Item(int id,string name) 
      {
          this.ItemId = id;
          this.ItemName = name;
      }
      public Item(int id) { this.ItemId = id; }
    }
}
