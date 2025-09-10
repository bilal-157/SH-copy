using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace BusinesEntities
{
    public class Customer : Person
    {

        public string Salutation { get; set; }
        //public string FName { get; set; }
        public string CO { get; set; }
        public string AccountCode { get; set; }
        //public ContactNoTypes ContactType { get; set; }
        public string Mobile { get; set; }
        public string Mobile1 { get; set; }
        public string CNIC { get; set; }
        public string TelHome { get; set; }
        public string HouseNo { get; set; }
        public DateTime Date { get; set; }
        private Nullable<decimal> cashBalance = null;

        public Nullable<decimal> CashBalance
        {
            get { return cashBalance; }
            set { cashBalance = value; }
        }
        public byte[] FingerPrint1 { get; set; }
        public byte[] FingerPrint { get; set; }

        private byte[] fingerPrintImg;
        public byte[] FingerPrintImg
        {
            get { return fingerPrintImg; }
            set { fingerPrintImg = value; }
        }
        private byte?[] signature = null;
        public byte?[] Signature
        {
            get { return signature; }
            set { signature = value; }
        }
        private byte[] imageMemory;
        public byte[] ImageMemory
        {
            get { return imageMemory; }
            set { imageMemory = value; }
        }

        public Byte[] ConvertImageToBinary(Image imag)
        {
            MemoryStream ms = new MemoryStream();
            imag.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            Byte[] bytes = ms.GetBuffer();
            return bytes;
        }
        public TransactionType TType { get; set; }
        public decimal GoldBalance { get; set; }

        private Nullable<DateTime> anniversaryDate = null;

        public Nullable<DateTime> AnniversaryDate
        {
            get { return anniversaryDate; }
            set { anniversaryDate = value; }
        }
        public string StreetNo { get; set; }
        public string Near { get; set; }
        public string Colony { get; set; }
        public Country CountryId { get; set; }
        public State StateId { get; set; }
        public City CityId { get; set; }
        //public string City { get; set; }
        public string BlockNo { get; set; }
        public string Society { get; set; }

        // Constructor 

        public Customer(int id) : base(id) { }
        public Customer(int id, string name) { }
        //public Customer(int id, string name, string name) { }
        public Customer(string name, string mobile, string email, string address)
        {
            Name = name;
            Mobile = mobile;
            Email = email;
            Address = address;
        }
        public Customer(int id, string name, string mobile, string telhome, string email)
            : base(id)
        {
        }

        public Customer(int id, string name, string AccountCode, string mobile, string telhome, string email)
            : base(id)
        {
        }
        //public Customer(int id, string fname, string lname, string contact,string address)
        //    : base(id)
        //{

        //}
        public Customer() {
            this.CityId = new City();
            this.CountryId = new Country();
        }
       

    }
}
