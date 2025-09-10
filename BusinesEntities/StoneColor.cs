using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class StoneColor
    {
       //public int ColorId { get; set; }
        private Nullable<int> colorId = null;

        public Nullable<int> ColorId
        {
            get { return colorId; }
            set { colorId = value; }
        }
       public string ColorName { get; set; }

       public StoneColor() { }
       public StoneColor(int id) { this.ColorId = id; }
       public StoneColor(int id, string name)
       {
           this.ColorId = id;
           this.ColorName = name;
       }
    }
}
