using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class ParentAccount
    {
       public int HeadCode { get; set; }
       public string GroupCode { get; set; }
       public string SubGroupCode { get; set; }
       public string  ParentCode { get; set; }
       public bool DeleteCheck { get; set; }
       public string ParentName { get; set; }

    }
}
