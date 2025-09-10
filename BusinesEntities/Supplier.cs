using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Supplier
    {
        public string PName { get; set; }
        public int PCode { get; set; }
        public string PAbri { get; set; }
        public string PAddress { get; set; }
        public string PtclNo1 { get; set; }
        public string PtclNo2 { get; set; }
        public string PtclNo3 { get; set; }
        public string PtclNo4 { get; set; }
        public string PtclNo5 { get; set; }
        public string PMaking { get; set; }
        public string PWastage { get; set; }
        public string PGoodWill { get; set; }
        public string PDiscount { get; set; }
        public DateTime PDate { get; set; }
        public decimal PrvCashBal { get; set; }
        public decimal PrvGoldBal { get; set; }

        public Supplier()
        {
            this.PhoneList = new List<Phone>();
        }

        public Supplier(int id, string name)
        {
            this.PCode = id;
            this.PName = name;
        }
        public Supplier(int id) { this.PCode = id; }
        public string AccountCode { get; set; }
        public List<Phone> PhoneList { get; set; }
    }

    public class Phone
    {
        public string PartyId { get; set; }
        public string PhoneNo { get; set; }
    }
}
