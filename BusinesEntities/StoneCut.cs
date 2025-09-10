using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
  public   class StoneCut
    {
     // public int CutId { get; set; }
     
        private Nullable<int> cutId = null;

        public Nullable<int> CutId
        {
            get { return cutId; }
            set { cutId = value; }
        }
        public string CutName { get; set; }

      public StoneCut() { }
      public StoneCut(int id) { this.CutId = id; }
      public StoneCut(int id, string name)
      {
          this.CutId = id;
          this.CutName = name;
      }
    }
}
