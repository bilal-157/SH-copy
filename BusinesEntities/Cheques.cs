using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class Cheques
    {
        public int SNO { get; set; }
        public int ONO { get; set; }
        public decimal Amount { get; set; }
        public DateTime ChequeDate { get; set; }
        public string ChequeNo { get; set; }
        public Banks BankName { get; set; }
        public BankAccount DepositInAccount { get; set; }
        public string BankAccount { get; set; }
        public string Description { get; set; }
        public string VNO { get; set; }
        public string Status { get; set; }
        public string CustAccountCode { get; set; }
    }
}
