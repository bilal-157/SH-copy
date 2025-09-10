using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class StonesDetail
    {
        public Stock TagNo { get; set; }
        public Stones StoneName{get;set;}
        public StoneCut CutName { get; set; }
        public StoneColor ColorName { get; set; }
        public StoneClearity ClearityName { get; set; }
    }
}
