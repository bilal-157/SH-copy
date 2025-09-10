using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public class Design
    {
      // public int DesignId { get; set; }
       //public string DesignName { get; set; }
        private Nullable<int> designId = null;

        public Nullable<int> DesignId
        {
            get { return designId; }
            set { designId = value; }
        }
       public string DesignNo { get; set; }

       public Design() { }

       public Design(int id, string no)
       {
           this.DesignId = id;
           this.DesignNo = no;

       }
       public Design(string no) { this.DesignNo  = no; }
       public Design(int id) { this.DesignId = id; }
    }
}
