using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class State
    {
        public int StateId {get;set;}
       public string StateName {get; set;}
       public Country CntId { get; set; }
       public State(int id, string name)
       {
           this.StateId = id;
           this.StateName = name; 
       }
       public State() { }
    }
}
