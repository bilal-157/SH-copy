using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Windows.Forms ;
 

namespace BusinesEntities
{
    public  class Formulas
    {

        public static void PureGoldIrfan( string weight,string wastage,string goodWill,string discount,TextBox pureGold )
        {
            decimal temp=((Convert.ToDecimal(weight)+Convert.ToDecimal( wastage)+Convert.ToDecimal(goodWill))-Convert.ToDecimal(discount));
            pureGold.Text = temp.ToString();
        }
        private int tola;

        public static decimal WeightInGm;

        public int Tola
        {
            get { return tola; }
           // set { tola = value; }
        }
        private int masha;

        public int Masha
        {
            get { return masha; }
           // set { masha = value; }
        }
        private int ratti;

        public int Ratti
        {
            get { return ratti; }
            //set { ratti = value; }
        }
        private decimal ratti1;

        public decimal Ratti1
        {
            get { return ratti1; }
            //set { ratti = value; }
        }
        private decimal pureWeight;
        private decimal percentWaste;
        private decimal totalGrams;
        private decimal wastePercent;
        private decimal makingPerGm;
        private decimal makingPerTola;
        private decimal totalMaking;
        private decimal totalLacker;
        private decimal lackerPerGm;
        private decimal goldPrice;
        private decimal totalPrice;
        private decimal netPrice;

        private decimal gramRate, gramRate1, gramRate2, gramRate3, gramRate4, gramRate5, gramRate6, gramRate7, gramRate8
            , gramRate9, gramRate10, gramRate11, gramRate12;
        private decimal tolaRate, tolaRate1, tolaRate2, tolaRate3, tolaRate4, tolaRate5, tolaRate6, tolaRate7, tolaRate8
            , tolaRate9, tolaRate10, tolaRate11;


        public void RatiMashaTolaGeneral1(decimal weight)
        {
            this.tola = (int)(weight / WeightInGm);
            this.masha = (int)(((weight / WeightInGm) - this.tola) * 12);
            this.ratti1 = (System.Math.Round(((((weight / WeightInGm) - this.tola) * 12) - this.masha) * 8));
        }
        public void RatiMashaTolaGeneral(decimal weight)
        {
            this.tola = (int)(weight / WeightInGm);
            this.masha = (int)(((weight / WeightInGm) - this.tola) * 12);
            this.ratti = (int)(System.Math.Round(((((weight / WeightInGm) - this.tola) * 12) - this.masha) * 8));
        }

        public void RatiMashaTola(decimal  weight ,Label lbl)
        {
           this.RatiMashaTolaGeneral(weight);            
            lbl.Text = this.tola + "T-" + this.masha + "M-" + this.ratti + "R";
        }
        public void RatiMashaTolaw(decimal weight, Label lbl)
        {
            this.RatiMashaTolaGeneral1(weight);
            lbl.Text = this.tola + "T-" + this.masha + "M-" + this.ratti1 + "R";
        }
        public void LabelZero(Label lbl, Label lbl1, Label lbl2,Label lbl3)
        {
            lbl.Text = 0 + "T-" + 0 + "M-" + 0 + "R";
            lbl1.Text = 0 + "T-" + 0 + "M-" + 0 + "R";
            lbl2.Text = 0 + "T-" + 0 + "M-" + 0 + "R";
            lbl3.Text = 0 + "T-" + 0 + "M-" + 0 + "R";

        }
        public void KaatInRatti(decimal  ratti,decimal weight ,TextBox txt, Label lbl)
        {
            this.pureWeight  = (decimal )(Math.Round((((96 - ratti) / 96) * weight), 3));
            txt.Text = this.pureWeight.ToString("0.000");
            this.RatiMashaTola(pureWeight, lbl);
        }
        public void KaatInRattiforWrk(decimal ratti, decimal weight, TextBox txt)
        {
            this.pureWeight = (decimal)(Math.Round((((96 - ratti) / 96) * weight), 3));
            this.pureWeight = weight - this.pureWeight;
            txt.Text = this.pureWeight.ToString("0.000");
        }
        public void KaatInRattiForBalance(decimal ratti, decimal weight, TextBox txt)
        {
            this.pureWeight = (decimal)(Math.Round((((96 - ratti) / 96) * weight), 3));
            txt.Text = this.pureWeight.ToString("0.000");
            
        }
        public void WasteInRattiforWrk(decimal ratti, decimal weight, TextBox txt)
        {
            this.pureWeight = (decimal)(Math.Round((((96 + ratti) / 96) * weight), 3));
            this.pureWeight = this.pureWeight - weight;
            txt.Text = this.pureWeight.ToString("0.000");
        }
        public decimal PureWeight(decimal ratti, decimal weight)
        {
            decimal purW = (decimal)(Math.Round((((96 - ratti) / 96) * weight), 3));
            return purW;
        }

        public void GramsOfPercentStock(decimal percent,decimal weight,TextBox txtWaste,TextBox txtTotal)
        {
            this.percentWaste = (decimal)(Math.Round(((weight*percent)/100),3));
            txtWaste.Text = this.percentWaste.ToString("0.000");
            this.totalGrams = Convert.ToDecimal(this.percentWaste + weight);
            txtTotal.Text = this.totalGrams.ToString("0.000");
        }

        public void GramsOfPercent(decimal percent, decimal weight, TextBox txtWaste, TextBox txtTotal)
        {
            this.percentWaste = (decimal)(Math.Round(((weight * percent) / 100), 3));
            txtWaste.Text = this.percentWaste.ToString();
            this.totalGrams = Convert.ToDecimal(this.percentWaste + weight);
            txtTotal.Text = this.totalGrams.ToString();          
        }
        public void AmountOfPercent(decimal percent, decimal amount, TextBox txtDiscount, TextBox txtTotal)
        {
            this.percentWaste = (decimal)(Math.Round(((amount * percent) / 100), 3));
            if (percentWaste != 0 || percentWaste < 0)
            {
                txtDiscount.Text = "0";
                this.percentWaste = 0;
            }
            else
                txtDiscount.Text = this.percentWaste.ToString("0.0");
            this.totalGrams = Convert.ToDecimal(amount - this.percentWaste);
            txtTotal.Text = this.totalGrams.ToString("0");
        }

        public void WasteInPercent(decimal waste, decimal weight,TextBox txtWastePercent,TextBox txtTotal)
        {
            this.wastePercent = (decimal)((waste * 100) / weight);
            txtWastePercent.Text = this.wastePercent.ToString("0.0");
            this.totalGrams = Convert.ToDecimal(waste  + weight);
            txtTotal.Text = this.totalGrams.ToString("N3");
        }

        public void WasteInPersent(decimal waste, decimal weight, TextBox txtWastePercent, TextBox txtTotal)
        {
            this.wastePercent = (decimal)((waste * 100) / weight);
            txtWastePercent.Text = this.wastePercent.ToString();
            this.totalGrams = Convert.ToDecimal(waste + weight);
            txtTotal.Text = this.totalGrams.ToString();           
        }
        public void PersentOfAmount(decimal disAmount, decimal amount, TextBox txtDicountPercent, TextBox txtTotal)
        {
            this.wastePercent = (decimal)((disAmount * 100) / amount);
            if (Double.IsNaN(Convert.ToDouble(this.wastePercent)))
            {
                txtDicountPercent.Text = "0";
                this.wastePercent = 0;
            }
            else
                txtDicountPercent.Text = this.wastePercent.ToString("0.0");
            this.totalGrams = Convert.ToDecimal(amount - disAmount);
            txtTotal.Text = this.totalGrams.ToString("0");
        }

        public void MakingPerGram(decimal making, decimal tweight, TextBox txt)
        {
            this.makingPerGm = (decimal)(Math.Round((making/tweight),3));
            txt.Text = this.makingPerGm.ToString("0.0");
           
        }
        public void MakingPerGram1(decimal making, decimal tweight,TextBox txtTMaking)
        {
            this.makingPerTola = (decimal)(Math.Round((making *WeightInGm ),3));
            
            if (Double.IsNaN(Convert.ToDouble(this.totalMaking)))
                this.totalMaking = 0;
            else
                this.totalMaking = (decimal)(Math.Round((making * tweight),3));
           txtTMaking.Text = this.totalMaking.ToString("0");
        }

        public void MakingPerTola(decimal making, decimal tweight, TextBox txtMakingPerGm, TextBox txtTMaking)
        {
            this.makingPerGm = (decimal)(Math.Round((making / WeightInGm ),3));
            if (Double.IsNaN(Convert.ToDouble(this.makingPerGm)))
                this.makingPerGm = 0;
            else
                txtMakingPerGm.Text = this.makingPerGm.ToString();
            if (Double.IsNaN(Convert.ToDouble(this.totalMaking)))
                this.totalMaking = 0;
            this.totalMaking = (decimal)(Math.Round((makingPerGm * tweight),3));
            txtTMaking.Text = this.totalMaking.ToString();
            
        }

        public void TotalMaking(decimal makingGm, decimal tweight, TextBox txtTMaking)
        {
            this.totalMaking = (decimal)(Math.Round((makingGm * tweight)));
            txtTMaking.Text = this.totalMaking.ToString("0");           
        }
       
        public void MakingPrTola(decimal making, decimal tweight, TextBox txtMakingPerGm, TextBox txtTMaking)
        {
            this.makingPerGm = (decimal)(Math.Round((making / WeightInGm),3));
            txtMakingPerGm.Text = this.makingPerGm.ToString();
            this.totalMaking = (decimal)(Math.Round((making * tweight),3));
            txtTMaking.Text = this.totalMaking.ToString("0.0");
        }
        public void TotalMakingForSale(decimal making, decimal tweight, TextBox txtMakingPerGm)
        {
            this.makingPerGm = Math.Round(((decimal)(making / tweight)), 1);
            if (Double.IsNaN(Convert.ToDouble(this.makingPerGm)))
                this.makingPerGm = 0;
            else
                txtMakingPerGm.Text = this.makingPerGm.ToString("0.0");
            if (Double.IsNaN(Convert.ToDouble(this.makingPerTola)))
                this.makingPerTola = 0;
            else
                this.makingPerTola = Math.Round(((decimal)(this.makingPerGm * WeightInGm)), 1);
        }

        public void TotalLacker(decimal lacker, decimal tweight, TextBox txtTLacker)
        { 
            this.totalLacker=(decimal)(Math.Round((lacker * tweight)));
            txtTLacker.Text = this.totalLacker.ToString("0");
        }
        public void LackerPerGm(decimal tlacker,decimal tweight,TextBox txtLackerPerGm)
        {
            this.lackerPerGm = (decimal)(Math.Round((tlacker / tweight),3));
            txtLackerPerGm.Text = this.lackerPerGm.ToString("0.0");
        }


        public void GoldPrice(decimal tweight, decimal grate, TextBox txtgprice)
        {
            this.goldPrice = (decimal)(tweight * grate);
            txtgprice.Text = this.goldPrice.ToString("0");
        }
        public void TotalPrice(decimal tlacker, decimal tmaking, decimal gprice, TextBox txttprice)
        {
            this.totalPrice = (decimal)(tlacker + tmaking + gprice);
            txttprice.Text = this.totalPrice.ToString("0");
        }
        public void NetPrice(decimal tprice, decimal discount, TextBox txtnetprice)
        {
            this.netPrice = Convert.ToDecimal(tprice - discount);
            txtnetprice.Text = this.netPrice.ToString("0");
        }

        public void TolaGramRate( decimal rate, TextBox txt24,TextBox txt23,TextBox txt22,TextBox txt21,
            TextBox txt20,TextBox txt19,TextBox txt18,TextBox txt17,TextBox txt16,TextBox txt15,TextBox txt14,
            TextBox txt13,TextBox txt12)
        {

            this.gramRate = (decimal)(Math.Round(((Convert.ToDecimal(24 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate1 = (decimal)(Math.Round(((Convert.ToDecimal(23 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate2 = (decimal)(Math.Round(((Convert.ToDecimal(22 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate3 = (decimal)(Math.Round(((Convert.ToDecimal(21 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate4 = (decimal)(Math.Round(((Convert.ToDecimal(20 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate5 = (decimal)(Math.Round(((Convert.ToDecimal(19 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate6 = (decimal)(Math.Round(((Convert.ToDecimal(18 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate7 = (decimal)(Math.Round(((Convert.ToDecimal(17 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate8 = (decimal)(Math.Round(((Convert.ToDecimal(16 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate9 = (decimal)(Math.Round(((Convert.ToDecimal(15 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate10 = (decimal)(Math.Round(((Convert.ToDecimal(14 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate11 = (decimal)(Math.Round(((Convert.ToDecimal(13 * 0.0416666) * rate) / WeightInGm), 0));
            this.gramRate12 = (decimal)(Math.Round(((Convert.ToDecimal(12 * 0.0416666) * rate) / WeightInGm), 0));
                txt24.Text = this.gramRate.ToString();
                txt23.Text = this.gramRate1.ToString();
                txt22.Text = this.gramRate2.ToString();
                txt21.Text = this.gramRate3.ToString();
                txt20.Text = this.gramRate4.ToString();
                txt19.Text = this.gramRate5.ToString();
                txt18.Text = this.gramRate6.ToString();
                txt17.Text = this.gramRate7.ToString();
                txt16.Text = this.gramRate8.ToString();
                txt15.Text = this.gramRate9.ToString();
                txt14.Text = this.gramRate10.ToString();
                txt13.Text = this.gramRate11.ToString();
                txt12.Text = this.gramRate12.ToString();
                
           
        }
        public void TolaRate(decimal rate, TextBox txt23, TextBox txt22, TextBox txt21,
            TextBox txt20, TextBox txt19, TextBox txt18, TextBox txt17, TextBox txt16, TextBox txt15, TextBox txt14,
            TextBox txt13, TextBox txt12)
        {
            this.tolaRate = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(23 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate1 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(22 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate2 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(21 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate3 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(20 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate4 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(19 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate5 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(18 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate6 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(17 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate7 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(16 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate8 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(15 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate9 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(14 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate10 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(13 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            this.tolaRate11 = (decimal)(Math.Round(((Math.Round((Convert.ToDecimal(12 * 0.0416666) * rate) / WeightInGm)) * WeightInGm), 0));
            txt23.Text = this.tolaRate.ToString();
            txt22.Text = this.tolaRate1.ToString();
            txt21.Text = this.tolaRate2.ToString();
            txt20.Text = this.tolaRate3.ToString();
            txt19.Text = this.tolaRate4.ToString();
            txt18.Text = this.tolaRate5.ToString();
            txt17.Text = this.tolaRate6.ToString();
            txt16.Text = this.tolaRate7.ToString();
            txt15.Text = this.tolaRate8.ToString();
            txt14.Text = this.tolaRate9.ToString();
            txt13.Text = this.tolaRate10.ToString();
            txt12.Text = this.tolaRate11.ToString();
        }

        public void GramRate(int karat, decimal rate, TextBox txt)
        {
            this.gramRate = (decimal)(Math.Round(((karat * (decimal)0.0416666 * rate) / WeightInGm), 0));
            txt.Text = gramRate.ToString();
        }

        public void TRate(int karat, decimal rate,TextBox txt)
        {
            this.tolaRate = (decimal)(Math.Round(((Math.Round((karat * (decimal)0.0416666 * rate)))), 0));
            txt.Text = this.tolaRate.ToString();
        }

        public void GodConversion(decimal weight,decimal rtPrGm)
        {
            decimal cash = (decimal)weight * rtPrGm;
        }

        public void GramToGram(int karrat, decimal rate, TextBox txt)
        {
            this.gramRate = (decimal)(Math.Round((Math.Round(((karrat * (decimal)0.0416666 * rate) / WeightInGm), 1) * WeightInGm), 0));
            txt.Text = this.gramRate.ToString();
        }

        public void GramToTola(int karrat, decimal rate, TextBox txt)
        {
            this.tolaRate = (decimal)(Math.Round(((karrat * (decimal)0.0416666 * rate) * WeightInGm), 0));
            txt.Text = this.tolaRate.ToString();
        }

        public void WasteInGm(decimal waste, decimal weight, TextBox txtTotal)
        {
            this.totalGrams = Convert.ToDecimal(waste + weight);
            txtTotal.Text = this.totalGrams.ToString("0.000");
        }
    }
}