using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class Voucher
    {
       public int UserId { get; set; }
       public string VNO { get; set; }
       public ChildAccount AccountCode { get; set; }
      // public TransactionType TransactionType { get; set; }
       public decimal Dr { get; set; }
       public decimal Cr { get; set; }
       public int GPNO { get; set; }
       public decimal GoldDr { get; set; }
       public decimal GoldCr { get; set; }
       public string AccountNO { get; set; }
       public string Description { get; set; }
       public int GSNO { get; set; }
       private Nullable<int> sNO = null;

       public Nullable<int> SNO
       {
           get { return sNO; }
           set { sNO = value; }
       }

       private Nullable<int> wBillNO = null;

       public Nullable<int> WBillNO
       {
           get { return wBillNO; }
           set { wBillNO = value; }
       } 

       //public int SNO { get; set; }
       // public int OrderNo { get; set; }
       private Nullable<int> rID=null;

       public Nullable<int> RID
       {
           get { return rID; }
           set { rID = value; }
       }
       private Nullable<int> rNO = null;

       public Nullable<int> RNO
       {
           get { return rNO; }
           set { rNO = value; }
       }
       private Nullable<int> pNO = null;

       public Nullable<int> PNO
       {
           get { return pNO; }
           set { pNO = value; }
       }
       private Nullable<int> orderNo = null;

       public Nullable<int> OrderNo
       {
           get { return orderNo; }
           set { orderNo = value; }
       }
       public DateTime DDate { get; set; }
       public Voucher() { }

    }
}
