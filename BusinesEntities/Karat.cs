using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class  Karat
    {
        public int KaratId { get; set; }
        public string  Karatt { get; set; }

        public Karat() { }

        public Karat(int id, string  no)
        {
            this.KaratId = id;
            this.Karatt = no;

        }
        public Karat(int id) { this.KaratId = id; }
        //public Karat(decimal no) { this.Karatt = no; }
    }
}
