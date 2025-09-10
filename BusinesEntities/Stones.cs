 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinesEntities
{
   public  class Stones
    {
      // public int StoneId { get; set; }
        private Nullable<int> stoneId = null;

        public Nullable<int> StoneId
        {
            get { return stoneId; }
            set { stoneId = value; }
        }
        private Nullable<int> stoneTypeId = null;

        public Nullable<int> StoneTypeId
        {
            get { return stoneTypeId; }
            set { stoneTypeId = value; }
        }

      public string StoneName { get; set; }
      public string StoneTypeName { get; set; }

      private Nullable<int> workerId = null;

      public Nullable<int> WorkerId
      {
          get { return workerId; }
          set { workerId = value; }
      }
      // public decimal StoneWeight { get; set; }
      private Nullable<decimal> stoneWeight = null;

      public Nullable<decimal> StoneWeight
      {
          get { return stoneWeight; }
          set { stoneWeight = value; }
      }
     //  public decimal StonePrice { get; set; }
      private Nullable<decimal> stonePrice = null;

      public Nullable<decimal> StonePrice
      {
          get { return stonePrice; }
          set { stonePrice = value; }
      }

      private Nullable<decimal> beedsPrice = null;

      public Nullable<decimal> BeedsPrice
      {
          get { return beedsPrice; }
          set { beedsPrice = value; }
      }
      private Nullable<decimal> diamondPrice = null;

      public Nullable<decimal> DiamondPrice
      {
          get { return diamondPrice; }
          set { diamondPrice = value; }
      }
       //public int Qty { get; set; }
      private Nullable<decimal> qty = null;

      public Nullable<decimal> Qty
      {
          get { return qty; }
          set { qty = value; }
      }
      // public decimal Rate { get; set; }
      private Nullable<decimal> rate = null;

      public Nullable<decimal> Rate
      {
          get { return this .rate; }
          set { rate = value; }
      }
       //public decimal Price { get; set; }
      private Nullable<decimal> price = null;

      public Nullable<decimal> Price
      {
          get { return price; }
          set { price = value; }
      }
       //public StoneClearity ClearityName { get; set; }
      public StoneColor  ColorName { get; set; }
      public StoneCut  CutName { get; set; }
      //public StoneType StoneTypeName { get; set; }
      private StoneClearity clearityName;
      public int GiveReceive { get; set; }
       
    

public StoneClearity ClearityName
{
  get { return this . clearityName; }
  set {this . clearityName = value; }
}}
}
