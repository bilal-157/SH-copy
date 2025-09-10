using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
  public   class StoneClearity
    {
     // public int ClearityId { get; set; }
        private Nullable<int> clearityId = null;

        public Nullable<int> ClearityId
        {
            get { return clearityId; }
            set { clearityId = value; }
        }
      public string ClearityName { get; set; }

      public StoneClearity() { }
      public StoneClearity(int id) { this.ClearityId = id; }
      public StoneClearity(int id, string name)
      {
          this.ClearityId = id;
          this.ClearityName = name;
      }
    }
}
