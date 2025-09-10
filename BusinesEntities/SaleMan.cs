using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
    public class SaleMan:Person
    {
        //public string AccountCode { get; set; }
        public ChildAccount AccountCode { get; set; }
        public string CNIC { get; set; }
        
        private Nullable<decimal> salary = null;

        public Nullable<decimal> Salary
        {
            get { return salary; }
            set { salary = value; }
        }
        private Nullable<decimal> alownce = null;

        public Nullable<decimal> Alownce
        {
            get { return alownce; }
            set { alownce = value; }
        }
        public string Description { get; set; }
         public SaleMan(int id) : base(id) { }
        public SaleMan(int id, string name) { }
        public SaleMan(string name, string address) 
        {
            Name = name;
            Address = address;
        }
        public SaleMan(int id, string name, string ContactNo)
            : base(id)
        {
        }

        public SaleMan(int id, string name, string AccountCode, string ContactNo)
            : base(id)
        {
        }
        public SaleMan() 
        {
            ID = 0;
            Name = "";
        }


        #region SaleMan Attendence


        public DateTime AttDate
        {
            get;
            set;
        }


        public DateTime  AttTime
        {
            get;
            set;
        }
        public string Status { get; set; }
        #endregion

        #region SaleMan Advance
        public int AdvanceNo { get; set; }
        private Nullable<decimal> advAmount = null;

        public Nullable<decimal> AdvAmount
        {
            get { return advAmount; }
            set { advAmount = value; }
        }
        private Nullable<decimal> paidAmount = null;

        public Nullable<decimal> PaidAmount
        {
            get { return paidAmount; }
            set { paidAmount = value; }
        }
        private Nullable<decimal> remainingAmount = null;

        public Nullable<decimal> RemainingAmount
        {
            get { return remainingAmount; }
            set { remainingAmount = value; }
        }
        private Nullable<int> noOfInst=null;

        public Nullable<int> NoOfInst
        {
            get { return noOfInst; }
            set { noOfInst = value; }
        }
        private Nullable<int> noOfInstPaid = null;

        public Nullable<int> NoOfInstPaid
        {
            get { return noOfInstPaid; }
            set { noOfInstPaid = value; }
        }
        private Nullable<decimal> installAmount=null;

        public Nullable<decimal> InstallAmount
        {
            get { return installAmount; }
            set { installAmount = value; }
        }
        #endregion

        #region Alownce

        private Nullable<DateTime> increDate=null;

        public Nullable<DateTime> IncreDate
        {
            get { return increDate; }
            set { increDate = value; }
        }

        private Nullable<DateTime> decreDate=null;

        public Nullable<DateTime> DecreDate
        {
            get { return decreDate; }
            set { decreDate = value; }
        }
        private Nullable<decimal> increAmount = null;

        public Nullable<decimal> IncreAmount
        {
            get { return increAmount; }
            set { increAmount = value; }
        }
        private Nullable<decimal> decreAmount = null;

        public Nullable<decimal> DecreAmount
        {
            get { return decreAmount; }
            set { decreAmount = value; }
        }
        #endregion
    }
}
