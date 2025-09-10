using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DAL
{
    public class SaleDAL
    {
        StockDAL stkDAl = new StockDAL();
        int saleNo;

        public void AddSale(Sale s,out int sa, SqlConnection con, SqlTransaction tran)
        {
            string addSale = "AddSale";
            string UpdateStockAfterSale = "UpdateStockForSale";

            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            SqlParameter parm = new SqlParameter("@SaleNum", SqlDbType.Int);
            parm.Direction = ParameterDirection.Output; // This is important!
            cmdSale.Parameters.Add(parm); 
            cmdSale.Parameters.Add(new SqlParameter("@SaleNo", s.SaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", s.OrderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (s.ODate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            else
                cmdSale.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;
            if (s.DDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@DDate", s.DDate));
            else
                cmdSale.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));
            cmdSale.Parameters.Add(new SqlParameter("@Cashpayment", s.CashPayment));
            cmdSale.Parameters.Add(new SqlParameter("@TotalItmDiscount", s.TotalItemDiscount));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", s.TotalNetPrice));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", s.TotalGold));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", s.OthrChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", s.GoldChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", s.NetBillGold));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", s.GoldReceived));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", s.GoldBalance));
            cmdSale.Parameters.Add(new SqlParameter("@SaleManId", s.SalesMan.ID));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", s.OrderTaker));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", s.KhataNo));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));

            SqlCommand cmd = new SqlCommand(UpdateStockAfterSale, con);
            cmd.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@GoldRate", SqlDbType.Float);
            cmd.Parameters.Add("@pFlag", SqlDbType.Bit);
            cmd.Parameters.Add("@ChWtDesc", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CPWeight", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RateType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DesignId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Pieces", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@SaleQTY", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Discount", SqlDbType.Float);
            cmd.Parameters.Add("@NetAmount", SqlDbType.Float);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@CKaat", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerTola", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@OtherCharges", SqlDbType.Float);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BStatus", SqlDbType.NVarChar);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("update OrderEstimate set SaleNo=@SaleNo where TagNo=@TagNo", con);
            cmd1.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd1.Parameters.Add("@TagNo", SqlDbType.NVarChar);

            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;
            try
            {
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmd1.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                saleNo = (int)parm.Value;
                sa = saleNo;
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Parameters["@SaleNo"].Value = saleNo;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        if (sli.GRate != 0)
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = sli.Stock.pFlag;
                        }
                        else
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = DBNull.Value;
                        }
                        cmd.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;
                        if (sli.Bool == true && sli.Stock.BStatus == "Standard")
                        {
                            this.AddSplitSale(sli);
                        }
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;
                        cmd.Parameters["@ChWtDesc"].Value = sli.Stock.ChWtDesc;
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@CPWeight"].Value = sli.Stock.CPureWeight;
                        if (sli.Stock.ItemSize == null)
                            cmd.Parameters["@ItemSize"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@ItemSize"].Value = sli.Stock.ItemSize;
                        if (sli.Stock.RateType == null)
                            cmd.Parameters["@RateType"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@RateType"].Value = sli.Stock.RateType;
                        if (sli.Stock.DesignNo != null) cmd.Parameters["@DesignId"].Value = sli.Stock.DesignNo.DesignId;
                        else cmd.Parameters["@DesignId"].Value = 0;

                        if (sli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = sli.Stock.Description;
                        cmd.Parameters["@Pieces"].Value = sli.Stock.Pieces;
                        cmd.Parameters["@SaleQTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@Discount"].Value = sli.Stock.Discount;
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;
                        cmd.Parameters["@WasteInGm"].Value = sli.Stock.WasteInGm;
                        cmd.Parameters["@WastePercent"].Value = sli.Stock.WastePercent;
                        cmd.Parameters["@Kaat"].Value = sli.Stock.KaatInRatti;
                        cmd.Parameters["@CKaat"].Value = sli.Stock.CKaat;
                        cmd.Parameters["@LakerGm"].Value = sli.Stock.LakerGm;
                        cmd.Parameters["@TotalLaker"].Value = sli.Stock.TotalLaker;
                        cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;
                        cmd.Parameters["@MakingPerGm"].Value = sli.Stock.MakingPerGm;
                        cmd.Parameters["@MakingPerTola"].Value = sli.Stock.MakingPerGm;
                        cmd.Parameters["@TotalMaking"].Value = sli.Stock.TotalMaking;
                        cmd.Parameters["@WTola"].Value = sli.Stock.WTola;
                        cmd.Parameters["@WMasha"].Value = sli.Stock.WMasha;
                        cmd.Parameters["@WRatti"].Value = sli.Stock.WRatti;
                        cmd.Parameters["@TotalWeight"].Value = sli.Stock.TotalWeight;
                        cmd.Parameters["@TTola"].Value = sli.Stock.TTola;
                        cmd.Parameters["@TMasha"].Value = sli.Stock.TMasha;
                        cmd.Parameters["@TRatti"].Value = sli.Stock.TRatti;
                        if (sli.Stock.WorkerName == null)
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@WorkerId"].Value = sli.Stock.WorkerName.ID;
                        cmd.Parameters["@BStatus"].Value = sli.Stock.BStatus;
                        cmd.Parameters["@Status"].Value = sli.Stock.Status;
                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        cmd.Parameters["@OtherCharges"].Value = sli.Stock.OtherCharges;
                        cmd.ExecuteNonQuery();

                        cmd1.Parameters["@SaleNo"].Value = s.SaleNo;
                        cmd1.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                        cmd1.ExecuteNonQuery();
                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        if (sli.Stock.StoneList == null)
                        {
                        }
                        else
                        {
                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();


                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            finally
            {
            }
        }
        public void insertReturnStock(string tagId, SqlConnection con)
        {
            string insertStockt = "insert into Edit_Return select GETDATE() from stock where TagNo='" + tagId + "'";

            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDamage = new SqlCommand(insertStockt, con);
            cmdDamage.CommandType = CommandType.Text;
            try
            {
                //cmdDamage.Transaction = trans;
                try
                {
                    cmdDamage.ExecuteNonQuery();
                }
                catch
                {
                    //tran.Rollback();

                    //throw ex;
                }

            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }





        }

        private void AddSplitSale(SaleLineItem sli)
        {
            //SqlCommand cmd = new SqlCommand("insert into SampleInfo values ('" + sample.SampleNo + "','" + sample.SampleDate + "','" + sample.Customer.ID + "','" + sample.BillBookNo + "')", con);
            //string addSale = "insert into SplitSale values ('"+s.SaleNo +"','"+sli.+"','"++"','"++"','"++"','"++"',)";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddSplitSale", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@IType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Weight", SqlDbType.Float);
            cmd.Parameters.Add("@Qty", SqlDbType.Int);

            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                cmd.Transaction = tran;

                try
                {
                    //foreach (SaleLineItem sli in s.SaleLineItem)
                    //{



                    cmd.Parameters["@SaleNo"].Value = saleNo;
                    cmd.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                    cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;
                    cmd.Parameters["@IType"].Value = sli.Stock.ItemType;
                    cmd.Parameters["@Weight"].Value = sli.ChangeWeight;
                    cmd.Parameters["@Qty"].Value = sli.ChangeQty;


                    cmd.ExecuteNonQuery();

                    //}                                                                                         
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

        }

        public void AddSilverSale(Sale s, out int sa, SqlConnection con, SqlTransaction tran)
        {
            string addSale = "AddSale";
            string UpdateStockAfterSale = "UpdateStockForSale";
            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            SqlParameter parm = new SqlParameter("@SaleNum", SqlDbType.Int);
            parm.Direction = ParameterDirection.Output; // This is important!
            cmdSale.Parameters.Add(parm); 
            cmdSale.Parameters.Add(new SqlParameter("@SaleNo", s.SaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", s.OrderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (s.ODate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            else
                cmdSale.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;
            if (s.DDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@DDate", s.DDate));
            else
                cmdSale.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));

            cmdSale.Parameters.Add(new SqlParameter("@TotalItmDiscount", s.TotalItemDiscount));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            if(s.CashReceive.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CashReceive", DBNull.Value));
            if(s.CreditCard.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CCAmount", DBNull.Value));
            if(s.CheckCash.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CAmount", DBNull.Value));
            if (s.PureGoldCharges.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            else
                cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", DBNull.Value));
            if (s.UsedGoldCharges.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));
            else
                cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", DBNull.Value));

            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", DBNull.Value));

            cmdSale.Parameters.Add(new SqlParameter("@SaleManId", s.SalesMan.ID));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", ""));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", DBNull.Value));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));
            cmdSale.Parameters.Add(new SqlParameter("@CashPayment", s.CashPayment));

            SqlCommand cmd = new SqlCommand(UpdateStockAfterSale, con);
            cmd.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@ChWtDesc", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CPWeight", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RateType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DesignId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@pFlag", SqlDbType.Bit);
            cmd.Parameters.Add("@Pieces", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@SaleQty", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Discount", SqlDbType.Float);
            cmd.Parameters.Add("@NetAmount", SqlDbType.Float);
            cmd.Parameters.Add("@GoldRate", SqlDbType.Float);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@CKaat", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerTola", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@BStatus", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OtherCharges", SqlDbType.Float);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;
            try
            {
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                saleNo = (int)parm.Value;
                sa = saleNo;
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Parameters["@SaleNo"].Value = s.SaleNo;
                        saleNo = s.SaleNo;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        cmd.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;
                        cmd.Parameters["@ChWtDesc"].Value = "";
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@CPWeight"].Value = DBNull.Value;
                        if (sli.Stock.ItemSize == null)

                            cmd.Parameters["@ItemSize"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@ItemSize"].Value = sli.Stock.ItemSize;
                        if (sli.Stock.DesignNo != null) cmd.Parameters["@DesignId"].Value = sli.Stock.DesignNo.DesignId;
                        else cmd.Parameters["@DesignId"].Value = 0;

                        if (sli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = sli.Stock.Description;
                        if (sli.Stock.RateType  == null)
                            cmd.Parameters["@RateType"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@RateType"].Value = sli.Stock.RateType;
                        if(sli.Stock.Pieces.HasValue)
                            cmd.Parameters["@Pieces"].Value = sli.Stock.Pieces;
                        else
                            cmd.Parameters["@Pieces"].Value = DBNull.Value;
                        cmd.Parameters["@SaleQty"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@Discount"].Value = sli.Stock.Discount;
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;
                        if (sli.GRate != 0)
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = sli.Stock.pFlag;
                        }
                        else
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = DBNull.Value;
                        }
                        cmd.Parameters["@WasteInGm"].Value = DBNull.Value;
                        cmd.Parameters["@WastePercent"].Value = DBNull.Value;
                        cmd.Parameters["@Kaat"].Value = DBNull.Value;
                        cmd.Parameters["@CKaat"].Value = DBNull.Value;
                        cmd.Parameters["@LakerGm"].Value = DBNull.Value;
                        cmd.Parameters["@TotalLaker"].Value = DBNull.Value;
                        cmd.Parameters["@RatePerGm"].Value = DBNull.Value;
                        cmd.Parameters["@MakingPerGm"].Value = DBNull.Value;
                        cmd.Parameters["@MakingPerTola"].Value = DBNull.Value;
                        cmd.Parameters["@TotalMaking"].Value = DBNull.Value;
                        cmd.Parameters["@WTola"].Value = DBNull.Value;
                        cmd.Parameters["@WMasha"].Value = DBNull.Value;
                        cmd.Parameters["@WRatti"].Value = DBNull.Value;
                        cmd.Parameters["@TotalWeight"].Value = DBNull.Value;
                        cmd.Parameters["@TTola"].Value = DBNull.Value;
                        cmd.Parameters["@TMasha"].Value = DBNull.Value;
                        cmd.Parameters["@TRatti"].Value = DBNull.Value;
                        if (sli.Stock.WorkerName == null)
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@WorkerId"].Value = sli.Stock.WorkerName.ID;
                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        cmd.Parameters["@Status"].Value = sli.Stock.Status;
                        cmd.Parameters["@BStatus"].Value = sli.Stock.BStatus;
                        cmd.Parameters["@OtherCharges"].Value = sli.Stock.OtherCharges;

                        cmd.ExecuteNonQuery();
                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        if (sli.Stock.StoneList == null)
                        { }
                        else
                        {
                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateSale(int oldSaleNo, Sale s)
        {
            string addSale = "UpdateSale";
            string UpdateStockAfterSale = "UpdateSaleFromSale";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            cmdSale.Parameters.Add(new SqlParameter("@oldSaleNo", oldSaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", s.OrderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (s.ODate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            else
            {
                cmdSale.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;

            }
            if (s.DDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@DDate", s.DDate));
            else
            {
                cmdSale.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));

            cmdSale.Parameters.Add(new SqlParameter("@TotalItmDiscount", s.TotalItemDiscount));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", s.TotalNetPrice));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));

            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", s.TotalGold));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", s.OthrChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", s.GoldChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", s.NetBillGold));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", s.GoldReceived));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", s.GoldBalance));

            cmdSale.Parameters.Add(new SqlParameter("@SaleManName", s.SalemanName));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", s.OrderTaker));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", s.KhataNo));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));
            // //cmd.CommandType = CommandType.StoredProcedure;
            //cmdStone.CommandType = CommandType.StoredProcedure;
            SqlCommand cmd = new SqlCommand(UpdateStockAfterSale, con);
            // cmd.Parameters.Add(new SqlParameter("@BarCode",stock.BarCode));
            //stock.ItemName = new Item();
            cmd.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            //cmd.Parameters .Add ("@ItemId",SqlDbType .Int );
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);


            // cmd.Parameters.Add("@NetWeight", SqlDbType .Float );
            //ChWtDesc
            cmd.Parameters.Add("@ChWtDesc", SqlDbType.NVarChar);

            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@CPWeight", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DesignId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);

            cmd.Parameters.Add("@Pieces", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleQTY", SqlDbType.Int);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Discount", SqlDbType.Float);
            cmd.Parameters.Add("@NetAmount", SqlDbType.Float);






            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);

            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@CKaat", SqlDbType.Float);
            // cmd.Parameters.Add(new SqlParameter("@PWeight", SqlDbType .Float ));


            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);


            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);



            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerTola", SqlDbType.Float);



            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);

            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);

            cmd.Parameters.Add("@WTola", SqlDbType.Float);


            cmd.Parameters.Add("@WMasha", SqlDbType.Float);

            cmd.Parameters.Add("@WRatti", SqlDbType.Float);


            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);

            cmd.Parameters.Add("@TTola", SqlDbType.Float);


            cmd.Parameters.Add("@TMasha", SqlDbType.Float);

            cmd.Parameters.Add("@TRatti", SqlDbType.Float);

            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);

            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            // cmd.Parameters.Add("@SalePrice", SqlDbType.Float);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            //cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            //cmd.Parameters.Add("@IType", SqlDbType.NVarChar);
            //cmd.Parameters.Add("@Weight", SqlDbType.Float );
            //cmd.Parameters.Add("@Qty", SqlDbType.Int);




            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            //cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            //cmdStone.Parameters.Add(new SqlParameter("@ColorId", SqlDbType.Int));
            //cmdStone.Parameters.Add(new SqlParameter("@CutId", SqlDbType.Int));
            //cmdStone.Parameters.Add(new SqlParameter("@ClearityId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;



            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Parameters["@SaleNo"].Value = oldSaleNo;
                        saleNo = s.SaleNo;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        cmd.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;

                        if (sli.Bool == true)
                        {
                            this.AddSplitSale(sli);
                        }

                        //  cmd.Parameters["@NetWeight"].Value =sli.Stock .NetWeight ;

                        cmd.Parameters["@ChWtDesc"].Value = sli.Stock.ChWtDesc;
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@CPWeight"].Value = sli.Stock.CPureWeight;
                        if (sli.Stock.ItemSize == null)

                            cmd.Parameters["@ItemSize"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@ItemSize"].Value = sli.Stock.ItemSize;
                        cmd.Parameters["@DesignId"].Value = sli.Stock.DesignNo.DesignId;
                        if (sli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = sli.Stock.Description;
                        cmd.Parameters["@Pieces"].Value = sli.Stock.Pieces;
                        cmd.Parameters["@SaleQTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@Discount"].Value = sli.Stock.Discount;
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;


                        cmd.Parameters["@WasteInGm"].Value = sli.Stock.WasteInGm;


                        cmd.Parameters["@WastePercent"].Value = sli.Stock.WastePercent;
                        cmd.Parameters["@Kaat"].Value = sli.Stock.KaatInRatti;

                        cmd.Parameters["@CKaat"].Value = sli.Stock.CKaat;
                        //  cmd.Parameters["@PWeight"].Value =sli .Stock .PWeight ;


                        cmd.Parameters["@LakerGm"].Value = sli.Stock.LakerGm;


                        cmd.Parameters["@TotalLaker"].Value = sli.Stock.TotalLaker;
                        cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;



                        cmd.Parameters["@MakingPerGm"].Value = sli.Stock.MakingPerGm;
                        cmd.Parameters["@MakingPerTola"].Value = sli.Stock.MakingPerGm;



                        cmd.Parameters["@TotalMaking"].Value = sli.Stock.TotalMaking;



                        cmd.Parameters["@WTola"].Value = sli.Stock.WTola;


                        cmd.Parameters["@WMasha"].Value = sli.Stock.WMasha;

                        cmd.Parameters["@WRatti"].Value = sli.Stock.WRatti;


                        cmd.Parameters["@TotalWeight"].Value = sli.Stock.TotalWeight;

                        cmd.Parameters["@TTola"].Value = sli.Stock.TTola;


                        cmd.Parameters["@TMasha"].Value = sli.Stock.TMasha;

                        cmd.Parameters["@TRatti"].Value = sli.Stock.TRatti;
                        cmd.Parameters["@WorkerId"].Value = sli.Stock.WorkerName.ID;

                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        // cmd.Parameters["@SalePrice"].Value = sli.Stock.SalePrice;
                        cmd.Parameters["@Status"].Value = sli.Stock.Status;


                        cmd.ExecuteNonQuery();

                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {

                        if (sli.Stock.StoneList == null)
                        {

                            //cmdStone.Parameters["@TagNo"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            //cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            //cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@CutId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                            //cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                            //cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                            //cmdStone.Parameters["@Price"].Value = DBNull.Value;
                            //cmdStone.ExecuteNonQuery();

                        }
                        else
                        {

                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                //cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                //cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
                                else
                                    // cmdStone.Parameters["@ColorId"].Value = stList .ColorName .ColorId ;
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    //cmdStone.Parameters["@CutId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@CutId"].Value = stList .CutName .CutId ;
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    // cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@ClearityId"].Value = stList .ClearityName .ClearityId ;
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();


                            }
                        }
                    }
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }





        }

        public void UpdateSale(int oldSaleNo, Sale s, SqlConnection con, SqlTransaction tran)
        {
            string addSale = "UpdateSale";
            string UpdateStockAfterSale = "UpdateSaleFromSale";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            cmdSale.Parameters.Add(new SqlParameter("@oldSaleNo", oldSaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", s.OrderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (s.OrderNo > 0)
                cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            else
                cmdSale.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;

            if (s.DDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@DDate", s.DDate));
            else
                cmdSale.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;

            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));

            cmdSale.Parameters.Add(new SqlParameter("@TotalItmDiscount", s.TotalItemDiscount));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", s.TotalNetPrice));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@CashPayment", s.CashPayment));
            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", s.TotalGold));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", s.OthrChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", s.GoldChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", s.NetBillGold));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", s.GoldReceived));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", s.GoldBalance));
            ////cmdSale.Parameters.Add(new SqlParameter("@OtherCharges", s.OtherCharges));
            if (s.SalemanName=="")
                cmdSale.Parameters.Add("@SaleManId", SqlDbType.Int).Value = DBNull.Value;            
            else
                cmdSale.Parameters.Add(new SqlParameter("@SaleManId", s.SalesMan.ID));
            //cmdSale.Parameters.Add(new SqlParameter("@SmId", s.SalesMan.ID ));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", s.OrderTaker));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", s.KhataNo));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));

            SqlCommand cmd = new SqlCommand(UpdateStockAfterSale, con);
            cmd.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@ChWtDesc", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@CPWeight", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DesignId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Pieces", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleQty", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Discount", SqlDbType.Float);
            cmd.Parameters.Add("@NetAmount", SqlDbType.Float);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@CKaat", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerTola", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@OtherCharges", SqlDbType.Float);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("update OrderEstimate set SaleNo=@SaleNo where TagNo=@TagNo", con);
            cmd1.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd1.Parameters.Add("@TagNo", SqlDbType.NVarChar);           
          
            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;



            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdSale.Transaction = tran;
                cmd1.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Transaction = tran;
                        cmd.Parameters["@SaleNo"].Value = oldSaleNo;
                        saleNo = s.SaleNo;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        cmd.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;

                        if (sli.Bool == true)
                        {
                            //this.AddSplitSale(sli);
                        }

                        //  cmd.Parameters["@NetWeight"].Value =sli.Stock .NetWeight ;

                        cmd.Parameters["@ChWtDesc"].Value = sli.Stock.ChWtDesc;
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        if (sli.Stock .CPureWeight == null )
                            cmd.Parameters["@CPWeight"].Value = 0;
                        else
                            cmd.Parameters["@CPWeight"].Value = sli.Stock.CPureWeight;
                         if (sli.Stock.ItemSize == null)

                            cmd.Parameters["@ItemSize"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@ItemSize"].Value = sli.Stock.ItemSize;
                        cmd.Parameters["@DesignId"].Value = sli.Stock.DesignNo.DesignId;
                        if (sli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = sli.Stock.Description;
                        cmd.Parameters["@Pieces"].Value = sli.Stock.Pieces;
                        cmd.Parameters["@SaleQTY"].Value = sli.Stock.SaleQty;
                        if (sli .Stock .Discount == null )
                            cmd.Parameters["@Discount"].Value = 0;
                        else
                            cmd.Parameters["@Discount"].Value = sli.Stock.Discount;
                        
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;


                        cmd.Parameters["@WasteInGm"].Value = sli.Stock.WasteInGm;


                        cmd.Parameters["@WastePercent"].Value = sli.Stock.WastePercent;
                        cmd.Parameters["@Kaat"].Value = sli.Stock.KaatInRatti;

                        if (sli.Stock.CKaat == null)
                            cmd.Parameters["@CKaat"].Value = 0;
                        else
                            cmd.Parameters["@CKaat"].Value = sli.Stock.CKaat;
                        cmd.Parameters["@LakerGm"].Value = sli.Stock.LakerGm;
                        cmd.Parameters["@TotalLaker"].Value = sli.Stock.TotalLaker;
                        if (s.OrderNo != 0 && (s.OrderRate != null && s.OrderRate != 0))
                            cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;
                        else
                            cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;
                        cmd.Parameters["@MakingPerGm"].Value = sli.Stock.MakingPerGm;
                        cmd.Parameters["@MakingPerTola"].Value = sli.Stock.MakingPerGm;
                        cmd.Parameters["@TotalMaking"].Value = sli.Stock.TotalMaking;
                        cmd.Parameters["@WTola"].Value = sli.Stock.WTola;
                        cmd.Parameters["@WMasha"].Value = sli.Stock.WMasha;
                        cmd.Parameters["@WRatti"].Value = sli.Stock.WRatti;
                        cmd.Parameters["@TotalWeight"].Value = sli.Stock.TotalWeight;
                        cmd.Parameters["@TTola"].Value = sli.Stock.TTola;
                        cmd.Parameters["@TMasha"].Value = sli.Stock.TMasha;
                        cmd.Parameters["@TRatti"].Value = sli.Stock.TRatti;
                        if (sli.Stock.WorkerName.ID == 0)
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@WorkerId"].Value = sli.Stock.WorkerName.ID;
                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        cmd.Parameters["@OtherCharges"].Value = sli.Stock.OtherCharges;
                        cmd.ExecuteNonQuery();

                        cmd1.Parameters["@SaleNo"].Value = s.SaleNo;
                        cmd1.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                        cmd1.ExecuteNonQuery();
                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        if (sli.Stock.StoneList == null)
                        { }
                        else
                        {
                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                //cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                //cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
                                else
                                    // cmdStone.Parameters["@ColorId"].Value = stList .ColorName .ColorId ;
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    //cmdStone.Parameters["@CutId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@CutId"].Value = stList .CutName .CutId ;
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    // cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@ClearityId"].Value = stList .ClearityName .ClearityId ;
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();


                            }
                        }
                    }
                    //tran.Commit();

                }
                catch (Exception ex)
                {
                   // tran.Rollback();
                    throw ex;
                }

            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }

        public void UpdateSilverSale(int oldSaleNo, Sale s, SqlConnection con,SqlTransaction tran)
        {
            string addSale = "UpdateSale";
            string UpdateStockAfterSale = "UpdateStockForSale";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            cmdSale.Parameters.Add(new SqlParameter("@oldSaleNo", oldSaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", s.OrderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (s.ODate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            else
            {
                cmdSale.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;

            }
            if (s.DDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@DDate", s.DDate));
            else
            {
                cmdSale.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));

            cmdSale.Parameters.Add(new SqlParameter("@TotalItmDiscount", s.TotalItemDiscount));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            if (s.CashReceive.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CashReceive", DBNull.Value));
            if (s.CreditCard.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CCAmount", DBNull.Value));
            if (s.CheckCash.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            else
                cmdSale.Parameters.Add(new SqlParameter("@CAmount", DBNull.Value));
            if (s.PureGoldCharges.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            else
                cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", DBNull.Value));
            if (s.UsedGoldCharges.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));
            else
                cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", DBNull.Value));

            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", DBNull.Value));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", DBNull.Value));

            cmdSale.Parameters.Add(new SqlParameter("@SaleManId", s.SalesMan.ID));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", ""));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", DBNull.Value));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));
            cmdSale.Parameters.Add(new SqlParameter("@CashPayment", s.CashPayment));
          
            SqlCommand cmd = new SqlCommand(UpdateStockAfterSale, con);
            cmd.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            //cmd.Parameters .Add ("@ItemId",SqlDbType .Int );
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@ChWtDesc", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@CPWeight", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@DesignId", SqlDbType.Int);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Pieces", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SaleQTY", SqlDbType.Int);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@Discount", SqlDbType.Float);
            cmd.Parameters.Add("@NetAmount", SqlDbType.Float);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@CKaat", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerTola", SqlDbType.Float);
            cmd.Parameters.Add("@GoldRate", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@RateType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@pFlag", SqlDbType.Bit );
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BStatus", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OtherCharges", SqlDbType.Float);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlCommand cmd1 = new SqlCommand("update OrderEstimate set SaleNo=@SaleNo where TagNo=@TagNo", con);
            cmd1.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd1.Parameters.Add("@TagNo", SqlDbType.NVarChar);

            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;

            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmd1.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Parameters["@SaleNo"].Value = s.SaleNo;
                        saleNo = s.SaleNo;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        cmd.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@ItemId"].Value = sli.Stock.ItemName.ItemId;

                        cmd.Parameters["@ChWtDesc"].Value = "";
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@CPWeight"].Value = DBNull.Value;
                        if (sli.Stock.ItemSize == null)

                            cmd.Parameters["@ItemSize"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@ItemSize"].Value = sli.Stock.ItemSize;
                        if (sli.Stock.DesignNo != null) cmd.Parameters["@DesignId"].Value = sli.Stock.DesignNo.DesignId;
                        else cmd.Parameters["@DesignId"].Value = 0;

                        if (sli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = sli.Stock.Description;
                        if (sli.Stock.Pieces.HasValue)
                            cmd.Parameters["@Pieces"].Value = sli.Stock.Pieces;
                        else
                            cmd.Parameters["@Pieces"].Value = DBNull.Value;
                        cmd.Parameters["@SaleQTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@Discount"].Value = sli.Stock.Discount;
                        cmd.Parameters["@NetAmount"].Value = sli.Stock.NetAmount;
                        if (sli.GRate != 0)
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = DBNull.Value;
                        }
                        else
                        {
                            cmd.Parameters["@GoldRate"].Value = sli.GRate;
                            cmd.Parameters["@pFlag"].Value = DBNull.Value;
                        }
                        cmd.Parameters["@WasteInGm"].Value = DBNull.Value;
                        cmd.Parameters["@WastePercent"].Value = DBNull.Value;
                        cmd.Parameters["@Kaat"].Value = DBNull.Value;
                        cmd.Parameters["@CKaat"].Value = DBNull.Value;
                        cmd.Parameters["@LakerGm"].Value = DBNull.Value;
                        cmd.Parameters["@TotalLaker"].Value = DBNull.Value;
                        cmd.Parameters["@RatePerGm"].Value = DBNull.Value;
                        cmd.Parameters["@MakingPerGm"].Value = DBNull.Value;
                        cmd.Parameters["@MakingPerTola"].Value = DBNull.Value;
                        cmd.Parameters["@TotalMaking"].Value = DBNull.Value;
                        cmd.Parameters["@WTola"].Value = DBNull.Value;
                        cmd.Parameters["@WMasha"].Value = DBNull.Value;
                        cmd.Parameters["@WRatti"].Value = DBNull.Value;
                        cmd.Parameters["@TotalWeight"].Value = DBNull.Value;
                        cmd.Parameters["@TTola"].Value = DBNull.Value;
                        cmd.Parameters["@TMasha"].Value = DBNull.Value;
                        cmd.Parameters["@TRatti"].Value = DBNull.Value;
                        cmd.Parameters["@RateType"].Value = DBNull.Value;
                    
                        if (sli.Stock.WorkerName == null)
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@WorkerId"].Value = sli.Stock.WorkerName.ID;

                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        cmd.Parameters["@Status"].Value = sli.Stock.Status;
                        cmd.Parameters["@Bstatus"].Value = sli.Stock.BStatus;
                        cmd.Parameters["@OtherCharges"].Value = sli.Stock.OtherCharges;
                        cmd.ExecuteNonQuery();

                        cmd1.Parameters["@SaleNo"].Value = s.SaleNo;
                        cmd1.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                        cmd1.ExecuteNonQuery();
                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {

                        if (sli.Stock.StoneList == null)
                        {

                            //cmdStone.Parameters["@TagNo"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            //cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            //cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@CutId"].Value = DBNull.Value;
                            ////cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
                            //cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                            //cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                            //cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                            //cmdStone.Parameters["@Price"].Value = DBNull.Value;
                            //cmdStone.ExecuteNonQuery();

                        }
                        else
                        {

                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                //cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                //cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
                                else
                                    // cmdStone.Parameters["@ColorId"].Value = stList .ColorName .ColorId ;
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    //cmdStone.Parameters["@CutId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@CutId"].Value = stList .CutName .CutId ;
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    // cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    //cmdStone.Parameters["@ClearityId"].Value = stList .ClearityName .ClearityId ;
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();


                            }
                        }
                    }
                    //tran.Commit();

                }
                catch (Exception ex)
                {
                    //tran.Rollback();

                    throw ex;
                }

            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }





        }
        public void AddEdit_Return(string CACode, int saleNo, string tagNo, DateTime date, string status, SqlConnection con, SqlTransaction tran)
        {
            SqlCommand cmdSale = new SqlCommand("AddEdit_Return", con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            cmdSale.Parameters.Add(new SqlParameter("@SaleNo", saleNo));
            cmdSale.Parameters.Add(new SqlParameter("@Date", date));
            cmdSale.Parameters.Add(new SqlParameter("@TagNo", tagNo));
            cmdSale.Parameters.Add(new SqlParameter("@CACode", CACode));
            cmdSale.Parameters.Add(new SqlParameter("@status", status));
            try
            {
                cmdSale.Transaction = tran;
                cmdSale.ExecuteNonQuery();
            }
            finally
            {
            }
        }
        public void UpdateSaleForOrderSale(int orderNo, out int sa, Sale s, SqlConnection con, SqlTransaction tran)
        {
            string addSale = "AddSaleForOrder";
            SqlCommand cmdSale = new SqlCommand(addSale, con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            //SqlParameter parm = new SqlParameter("@SaleNum", SqlDbType.Int);
            //parm.Direction = ParameterDirection.Output; // This is important!
            //cmdSale.Parameters.Add(parm);
            cmdSale.Parameters.Add(new SqlParameter("@SaleNo", s.SaleNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderNo", orderNo));
            cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            cmdSale.Parameters.Add(new SqlParameter("@ODate", s.ODate));
            cmdSale.Parameters.Add(new SqlParameter("@DDate", s.SDate));

            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", s.TotalPrice));
            cmdSale.Parameters.Add(new SqlParameter("@BillDiscount", s.BillDiscout));
            cmdSale.Parameters.Add(new SqlParameter("@TNetAmount", s.TotalNetPrice));
            cmdSale.Parameters.Add(new SqlParameter("@NetBill", s.NetBill));
            cmdSale.Parameters.Add(new SqlParameter("@CashReceive", s.CashReceive));
            cmdSale.Parameters.Add(new SqlParameter("@CCAmount", s.CreditCard));
            cmdSale.Parameters.Add(new SqlParameter("@CAmount", s.CheckCash));
            cmdSale.Parameters.Add(new SqlParameter("@PGoldAmount", s.PureGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@UGoldAmount", s.UsedGoldCharges));
            cmdSale.Parameters.Add(new SqlParameter("@CashPayment", s.CashPayment));

            cmdSale.Parameters.Add(new SqlParameter("@TReceivedAmount", s.TReceivedAmount));
            cmdSale.Parameters.Add(new SqlParameter("@Balance", s.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TotalGold", s.TotalGold));
            cmdSale.Parameters.Add(new SqlParameter("@OGold", s.OthrChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@GGold", s.GoldChargesGold));
            cmdSale.Parameters.Add(new SqlParameter("@BillGold", s.NetBillGold));
            cmdSale.Parameters.Add(new SqlParameter("@GoldReceive", s.GoldReceived));
            cmdSale.Parameters.Add(new SqlParameter("@GoldBalance", s.GoldBalance));

            cmdSale.Parameters.Add(new SqlParameter("@SaleManId", s.SalesMan.ID));
            cmdSale.Parameters.Add(new SqlParameter("@BillBookNo", s.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@OrderTaker", s.OrderTaker));
            cmdSale.Parameters.Add(new SqlParameter("@Status", s.Status));
            cmdSale.Parameters.Add(new SqlParameter("@VNO", s.SVNO));
            cmdSale.Parameters.Add(new SqlParameter("@KhataNo", s.KhataNo));
            if (s.PromiseDate.HasValue)
                cmdSale.Parameters.Add(new SqlParameter("@PromiseDate", s.PromiseDate));
            else
                cmdSale.Parameters.Add("@PromiseDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSale.Parameters.Add(new SqlParameter("@BadDats", s.Baddats));
            cmdSale.Parameters.Add(new SqlParameter("@BillInWord", s.BillInWord));
            cmdSale.Parameters.Add(new SqlParameter("@CustAccountNo", s.CusAccountNo));
            cmdSale.Parameters.Add(new SqlParameter("@OFixRate", s.OFixRate));

            SqlCommand cmd = new SqlCommand("update Stock set SaleNo=@SaleNo,Status=@Status,RatePerGm=@RatePerGm,NetWeight=@SaleWeight,WasteInGm=@WasteInGm,WastePercent=@WastePercent,TotalLaker=@TotalLaker,LakerGm=@LakerGm,OtherCharges=@OtherCharges,SaleWeight=@SaleWeight,TotalWeight=@TotalWeight,StQTY=@SaleQTY,QTY=@SaleQTY,SaleQTY=@SaleQTY,SaleDate=@SaleDate,TotalMaking=@TotalMaking,TotalPrice=@TotalPrice where TagNo=@TagNo", con);
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@SaleWeight", SqlDbType.Float);
            cmd.Parameters.Add("@NetWeight", SqlDbType.Float);
            cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@OtherCharges", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@SaleQTY", SqlDbType.Int);
            cmd.Parameters.Add("@QTY", SqlDbType.Int);
            cmd.Parameters.Add("@StQTY", SqlDbType.Int);
            cmd.Parameters.Add("@SaleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);

            SqlCommand cmd1 = new SqlCommand("update OrderEstimate set SaleNo=@SaleNo where TagNo=@TagNo", con);
            cmd1.Parameters.Add("@SaleNo", SqlDbType.Int);
            cmd1.Parameters.Add("@TagNo", SqlDbType.NVarChar);

            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.CommandType = CommandType.StoredProcedure;

            try
            {
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmd1.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();
                sa = s.SaleNo;
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        cmd.Parameters["@SaleNo"].Value = s.SaleNo;
                        cmd.Parameters["@OrderNo"].Value = orderNo;
                        cmd.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                        if (orderNo != 0  && s.OrderRate != null )
                            cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;
                        else
                            cmd.Parameters["@RatePerGm"].Value = sli.Stock.RatePerGm;
                        if (sli.Bool == true)
                        {
                            this.AddSplitSale(sli);
                        }
                        cmd.Parameters["@SaleWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@NetWeight"].Value = sli.Stock.SaleWeight;
                        cmd.Parameters["@WasteInGm"].Value = sli.Stock.WasteInGm;
                        cmd.Parameters["@WastePercent"].Value = sli.Stock.WastePercent;
                        cmd.Parameters["@LakerGm"].Value = sli.Stock.LakerGm;
                        cmd.Parameters["@TotalLaker"].Value = sli.Stock.TotalLaker;
                        cmd.Parameters["@OtherCharges"].Value = sli.Stock.OtherCharges;                        
                        cmd.Parameters["@TotalWeight"].Value = sli.Stock.TotalWeight;
                        cmd.Parameters["@SaleQTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@QTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@StQTY"].Value = sli.Stock.SaleQty;
                        cmd.Parameters["@SaleDate"].Value = s.SDate;
                        cmd.Parameters["@Status"].Value = sli.Stock.Status;
                        cmd.Parameters["@TotalPrice"].Value = sli.Stock.TotalPrice;
                        cmd.Parameters["@TotalMaking"].Value = sli.Stock.TotalMaking;
                        cmd.ExecuteNonQuery();
                        if (s.OrderNo > 0)
                        {
                            cmd1.Parameters["@SaleNo"].Value = s.SaleNo;
                            cmd1.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                            cmd1.ExecuteNonQuery();
                        }
                    }
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                        if (sli.Stock.StoneList == null)
                        { }
                        else
                        {
                            foreach (Stones stList in sli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@TagNo"].Value = sli.Stock.TagNo.ToString();

                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneId.HasValue)
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                                if (stList.ColorName == null)
                                    cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
                                if (stList.CutName == null)
                                    cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
                                if (stList.ClearityName == null)
                                    cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                                else
                                    cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
                                if (stList.StoneWeight.HasValue)
                                    cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
                                else
                                    cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                                if (stList.Qty.HasValue)
                                    cmdStone.Parameters["@SQty"].Value = stList.Qty;
                                else
                                    cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                                if (stList.Rate.HasValue)
                                    cmdStone.Parameters["@Rate"].Value = stList.Rate;
                                else
                                    cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                                if (stList.Price.HasValue)
                                    cmdStone.Parameters["@Price"].Value = stList.Price;
                                else
                                    cmdStone.Parameters["@Price"].Value = DBNull.Value;
                                cmdStone.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            finally
            { }
        }

        public int GetMaxSaleNo()
        {
            string querry = "Select MAX(SaleNo) as [MaxSale] from sale";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int saleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["MaxSale"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["MaxSale"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }

        public int GetSaleNoByBillBookNo(string  BillBookNo)
        {
            string querry = "Select SaleNo from sale where BillbookNo='" + BillBookNo + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int saleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["SaleNo"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["SaleNo"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }

        //public int GetSaleNoByBillBookNo(string BillBookNo)
        //{
        //    string querry = "Select OrderNo from sale where OrderNo=" + BillBookNo;
        //    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand(querry, con);
        //    cmd.CommandType = CommandType.Text;
        //    int saleNo = 0;
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


        //        //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
        //        if (dr.Read())
        //        {
        //            if (dr["SaleNo"] == DBNull.Value)
        //                saleNo = 0;
        //            else
        //                saleNo = Convert.ToInt32(dr["SaleNo"]);
        //        }

        //        dr.Close();

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //    }
        //    return saleNo;
        //}

        public decimal GetReturnAmountByONo(int Ono,SqlConnection con,SqlTransaction tran)
        {
            string querry = "select(select isnull(sum(amount),0) from Sale_Payment where ono=" + Ono + ") +(select isnull(sum(amount),0) from GoldDetail where ono=" + Ono + ")as maxSale";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            decimal saleNo = 0;
            try
            {
               // con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);                
                if (dr.Read())
                {
                    if (dr["MaxSale"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToDecimal(dr["MaxSale"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
            return saleNo;
        }
        public string GetCustAccountNoByONo(int Ono, SqlConnection con, SqlTransaction tran)
        {
            string querry = "select CustAccountNo from OrderMaster where orderno=" + Ono;
           // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            string saleNo = "";
            try
            {
               // con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["CustAccountNo"] == DBNull.Value)
                        saleNo = "";
                    else
                        saleNo = Convert.ToString(dr["CustAccountNo"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
            return saleNo;
        }

        public int GetSaleNoByONO(int OrderNo)
        {
            string querry = "Select SaleNo  from sale where OrderNo="+OrderNo ;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int saleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["SaleNo"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["SaleNo"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }
        
        public void AddChecques(Cheques chq)
        {
            string addCheque = "AddChequeDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(addCheque, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO ", chq.SNO));
            cmd.Parameters.Add(new SqlParameter("@ONO", chq.SNO));
            cmd.Parameters.Add(new SqlParameter("@VNO", chq.VNO));
            cmd.Parameters.Add(new SqlParameter("@Amount", chq.Amount));
            cmd.Parameters.Add(new SqlParameter("@DDate", chq.ChequeDate));
            cmd.Parameters.Add(new SqlParameter("@ChequeNo", chq.ChequeNo));
            cmd.Parameters.Add(new SqlParameter("@BankName", chq.BankName.BankName));
            cmd.Parameters.Add(new SqlParameter("@Description", chq.Description));
            cmd.Parameters.Add(new SqlParameter("@Status", chq.Status));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void AddChecques(Cheques chq,SqlConnection con,SqlTransaction tran)
        {
            string addCheque = "AddChequeDetail";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(addCheque, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO ", chq.SNO));
            cmd.Parameters.Add(new SqlParameter("@ONO", chq.ONO));
            cmd.Parameters.Add(new SqlParameter("@VNO", chq.VNO));
            cmd.Parameters.Add(new SqlParameter("@Amount", chq.Amount));
            cmd.Parameters.Add(new SqlParameter("@DDate", chq.ChequeDate));
            cmd.Parameters.Add(new SqlParameter("@ChequeNo", chq.ChequeNo));
            cmd.Parameters.Add(new SqlParameter("@BankName", chq.BankName.BankName));
            cmd.Parameters.Add(new SqlParameter("@BankId", chq.BankName.Id));
            cmd.Parameters.Add(new SqlParameter("@Description", chq.Description));
            cmd.Parameters.Add(new SqlParameter("@Status", chq.Status));
            cmd.Parameters.Add(new SqlParameter("@AccountNo", chq.DepositInAccount.AccountNo));
            cmd.Parameters.Add(new SqlParameter("@CustAccountCode", chq.CustAccountCode));
            try
            {
                //con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }
        public void AddCreditCards(CreditCard card)
        {
            string AddCard = "AddCreditCardsDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AddCard, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO", card.SNO));
            cmd.Parameters.Add(new SqlParameter("@ONO", card.ONO));
            cmd.Parameters.Add(new SqlParameter("@VNO", card.VNO));
            cmd.Parameters.Add(new SqlParameter("@MachineName", card.MachineName));
            cmd.Parameters.Add(new SqlParameter("@Amount", card.Amount));
            cmd.Parameters.Add(new SqlParameter("@DeductRate", card.DeductRate));
            cmd.Parameters.Add(new SqlParameter("@SwapAmount", card.SwapAmount));
            cmd.Parameters.Add(new SqlParameter("@BankDeductRate", card.BankDeductRate));
            cmd.Parameters.Add(new SqlParameter("@AmountDepositeBank", card.AmountDepositeBank));
            cmd.Parameters.Add(new SqlParameter("@DepositeInaccount", card.DepositInAccount.AccountNo));
            cmd.Parameters.Add(new SqlParameter("@AccountCode", card.AccountCode));
            cmd.Parameters.Add(new SqlParameter("@Description", card.Description));
            cmd.Parameters.Add(new SqlParameter("@Status", card.Status));
            cmd.Parameters.Add(new SqlParameter("@BankId", card.BankName.Id));
            
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void AddCreditCards(CreditCard card, SqlConnection con,SqlTransaction tran)
        {
            string AddCard = "AddCreditCardsDetail";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AddCard, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO", card.SNO));
            cmd.Parameters.Add(new SqlParameter("@ONO", card.ONO));
            cmd.Parameters.Add(new SqlParameter("@VNO", card.VNO));
            cmd.Parameters.Add(new SqlParameter("@MachineName", card.MachineName));
            cmd.Parameters.Add(new SqlParameter("@Amount", card.Amount));
            cmd.Parameters.Add(new SqlParameter("@DeductRate", card.DeductRate));
            cmd.Parameters.Add(new SqlParameter("@SwapAmount", card.SwapAmount));
            cmd.Parameters.Add(new SqlParameter("@BankDeductRate", card.BankDeductRate));
            cmd.Parameters.Add(new SqlParameter("@AmountDepositeBank", card.AmountDepositeBank));
            cmd.Parameters.Add(new SqlParameter("@DepositeInaccount", card.DepositInAccount.AccountNo));
            cmd.Parameters.Add(new SqlParameter("@AccountCode", card.DepositInAccount.AccountCode.ChildCode));
            cmd.Parameters.Add(new SqlParameter("@Description", card.Description));
            cmd.Parameters.Add(new SqlParameter("@Status", card.Status));
            cmd.Parameters.Add(new SqlParameter("@BankId", card.BankName.Id));

            try
            {
                //con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }

        public void AddGoldDetail(Gold gld)
        {
            string AddGold = "AddGoldDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AddGold, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO", gld.SNO));
            if (gld.ONO == 0 )
            {
                cmd.Parameters.Add("@ONO", SqlDbType.Int);
                cmd.Parameters["@ONO"].Value = 0;
            }
            else    
            cmd.Parameters.Add(new SqlParameter("@ONO", gld.ONO));
            cmd.Parameters.Add(new SqlParameter("@VNO", gld.VNO));
            cmd.Parameters.Add(new SqlParameter("@GoldType", gld.GoldType));
            cmd.Parameters.Add(new SqlParameter("@Weight", gld.Weight));
            cmd.Parameters.Add(new SqlParameter("@Kaat", gld.Kaat));
            cmd.Parameters.Add(new SqlParameter("@RemainingWork", gld.RemainingWork));
            cmd.Parameters.Add(new SqlParameter("@Karrat", gld.Karat));
            cmd.Parameters.Add(new SqlParameter("@Rate", gld.Rate));
            cmd.Parameters.Add(new SqlParameter("@Amount", gld.Amount));
            cmd.Parameters.Add(new SqlParameter("@Description", gld.Description));
            cmd.Parameters.Add(new SqlParameter("@PWeight", gld.PWeight));
            cmd.Parameters.Add(new SqlParameter("@PGDate", gld.PGDate));
            if (gld.CPVNO == null)
            {
                cmd.Parameters.Add("@CPVNO", SqlDbType.NVarChar);
                cmd.Parameters["@CPVNO"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CPVNO", gld.CPVNO));
            cmd.Parameters.Add(new SqlParameter("@CustId", gld.CustId));
            cmd.Parameters.Add(new SqlParameter("@PTime", gld.PTime));
            cmd.Parameters.Add(new SqlParameter("@PMode", gld.PMode));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void AddGoldDetail(Gold gld, SqlConnection con, SqlTransaction tran)
        {
            string AddGold = "AddGoldDetail";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AddGold, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SNO", gld.SNO));
            if (gld.ONO == null)
            {
                cmd.Parameters.Add("@ONO", SqlDbType.Int);
                cmd.Parameters["@ONO"].Value = 0;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@ONO", gld.ONO));
            cmd.Parameters.Add(new SqlParameter("@VNO", gld.VNO));
            cmd.Parameters.Add(new SqlParameter("@GoldType", gld.GoldType));
            cmd.Parameters.Add(new SqlParameter("@Weight", gld.Weight));
            cmd.Parameters.Add(new SqlParameter("@Kaat", gld.Kaat));
            cmd.Parameters.Add(new SqlParameter("@RemainingWork", gld.RemainingWork));
            cmd.Parameters.Add(new SqlParameter("@Karrat", gld.Karat));
            cmd.Parameters.Add(new SqlParameter("@Rate", gld.Rate));
            cmd.Parameters.Add(new SqlParameter("@Amount", gld.Amount));
            cmd.Parameters.Add(new SqlParameter("@Description", gld.Description));
            cmd.Parameters.Add(new SqlParameter("@PWeight", gld.PWeight));
            cmd.Parameters.Add(new SqlParameter("@PGDate", gld.PGDate));
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters["@ItemId"].Value = DBNull.Value;
            if (gld.SaleManId == 0)
            {
                cmd.Parameters.Add("@SaleManId", SqlDbType.Int);
                cmd.Parameters["@SaleManId"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@SaleManId", gld.SaleManId));
            
            if (gld.GPNO == null)
            {
                cmd.Parameters.Add("@GPNO", SqlDbType.Int);
                cmd.Parameters["@GPNO"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@GPNO", gld.GPNO));

            if (gld.GSNO == null)
            {
                cmd.Parameters.Add("@GSNO", SqlDbType.Int);
                cmd.Parameters["@GSNO"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@GSNO", gld.GSNO));

            if (gld.CPVNO == null)
            {
                cmd.Parameters.Add("@CPVNO", SqlDbType.NVarChar);
                cmd.Parameters["@CPVNO"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CPVNO", gld.CPVNO));
            if (gld.CustId == null || gld.CustId == 0)
            {
                cmd.Parameters.Add("@CustId", SqlDbType.NVarChar);
                cmd.Parameters["@CustId"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CustId", gld.CustId));
            cmd.Parameters.Add(new SqlParameter("@PTime", gld.PTime));
            cmd.Parameters.Add(new SqlParameter("@PMode", gld.PMode));
            try
            {
                //con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }

        public void DeleteGoldDetail(int saleNo, string vno)
        {
            string deleteCustomer = "Delete from GoldDetail where VNO='" + vno + "' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            finally
            {
                con.Close();
            }
        }
        public void DeleteGoldDetail(int saleNo, string vno, SqlConnection con, SqlTransaction tran)
        {

            string deleteCustomer = "Delete from GoldDetail where VNO='" + vno + "' and SNO=" + saleNo;
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();
                    //tran.Commit();
                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    //throw ex;
                }
            }
            finally
            {
                //con.Close();
            }
        }

        public void DeleteFromGoldDetail(int orderNo, string vno)
        {

            string deleteCustomer = "Delete from GoldDetail where VNO='" + vno + "' and ONO=" + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            finally
            {
                con.Close();
            }
        }
        public void DeleteFromGoldDetail(int orderNo, string vno, SqlConnection con, SqlTransaction tran)
        {

            string deleteCustomer = "Delete from GoldDetail where VNO='" + vno + "' and ONO=" + orderNo;
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;            
            try
            {
                //con.Open();

                //SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();

                   // tran.Commit();
                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    throw ex;
                }
            }
            finally
            {
                //con.Close();
            }
        }
        public List<Cheques> GetAllCheques()
        {
            string AllCheques = "select * from ChequeDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCheques, con);
            cmd.CommandType = CommandType.Text;

            List<Cheques> chqs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    chqs = new List<Cheques>();
                    if (chqs == null) chqs = new List<Cheques>();

                    do
                    {
                        Cheques chq = new Cheques();
                        chq.SNO = Convert.ToInt32(dr["SNO"]);
                        chq.VNO = dr["ONO"].ToString();
                        chq.Amount = Convert.ToDecimal(dr["Amount"]);
                        chq.ChequeDate = Convert.ToDateTime(dr["DDate"]);
                        chq.ChequeNo = dr["ChequeNo"].ToString();
                        // chq.BankName=dr["BankName"].ToString();
                        chq.Description = dr["Description"].ToString();
                        chq.Status = dr["Status"].ToString();

                        chqs.Add(chq);
                    }
                    while (dr.Read());


                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (chqs != null) chqs.TrimExcess();
            return chqs;
        }
        public List<CreditCard> GetAllCreditCards()
        {
            string AllCheques = "select * from ChequeDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCheques, con);
            cmd.CommandType = CommandType.Text;

            List<CreditCard> crds = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    crds = new List<CreditCard>();
                    if (crds == null) crds = new List<CreditCard>();

                    do
                    {
                        CreditCard crd = new CreditCard();
                        crd.SNO = Convert.ToInt32(dr["SNO"]);
                        crd.ONO = Convert.ToInt32(dr["ONO"]);
                        crd.VNO = dr["VNO"].ToString();
                        crd.MachineName = dr["MachineName"].ToString();
                        crd.Amount = Convert.ToDecimal(dr["Amount"]);
                        crd.DeductRate = Convert.ToDecimal(dr["DeductRate"]);
                        crd.SwapAmount = Convert.ToDecimal(dr["SwapAmount"]);
                        crd.BankDeductRate = Convert.ToDecimal(dr["BankDeductRate"]);
                        crd.AmountDepositeBank = Convert.ToDecimal(dr["AmountDepositeBank"]);
                        ChildAccount cha = new ChildAccount();
                        cha.ChildCode = dr["DepositInAccount"].ToString();
                        crd.DepositInAccount.AccountCode = cha;
                        crd.AccountCode = dr["AccountCode"].ToString();
                        crd.Description = dr["Description"].ToString();
                        crd.Status = dr["Status"].ToString();

                        crds.Add(crd);
                    }
                    while (dr.Read());


                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (crds != null) crds.TrimExcess();
            return crds;
        }
        public List<Gold> GetAllGold()
        {
            string getGold = "select * from GoldDetail";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getGold, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> lstGold = null;
            //SqlDataReader dr = null;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (lstGold == null) lstGold = new List<Gold>();
                    do
                    {
                        Gold gld = new Gold();
                        gld.SNO = Convert.ToInt32(dr["SNO"]);
                        gld.VNO = dr["VNO"].ToString();


                        if (dr["GoldType"].ToString() == "Pure")
                            gld.GoldType = GoldType.Pure;
                        else
                            gld.GoldType = GoldType.Used;
                        gld.Weight = Convert.ToDecimal(dr["Weight"]);
                        gld.Weight = Convert.ToDecimal(dr["Kaat"]);
                        gld.RemainingWork = dr["RemainingWork"].ToString();
                        gld.Karat = dr["Karrat"].ToString();
                        gld.Rate = Convert.ToDecimal(dr["Rate"]);
                        gld.Amount = Convert.ToDecimal(dr["Amount"]);
                        gld.Description = dr["Description"].ToString();
                        lstGold.Add(gld);
                    }
                    while (dr.Read());
                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return lstGold;
        }
        public int GetMaxKNo()
        {
            string querry = "Select MAX(KhataNo) as [MaxKNo] from sale";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int KNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["MaxKNo"] == DBNull.Value)
                        KNo = 0;
                    else
                        KNo = Convert.ToInt32(dr["MaxKNo"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return KNo;
        }

        public List<Sale> GetRecordsByAccount(string accCode)
        {
            string selectRecord = "select SaleNo,Balance from Sale where status <> 'Sale Return' and CustAccountNo='" + accCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@bFlag", SqlDbType.Bit).Value = true;
            List<Sale> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Sale>();
                    if (records == null) records = new List<Sale>();

                    do
                    {
                        Sale cstd = new Sale();
                        cstd.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                        cstd.Balance = Convert.ToDecimal(dr["Balance"]);

                        records.Add(cstd);
                    }
                    while (dr.Read());
                }
                dr.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (records != null) records.TrimExcess();
            return records;

        }

        public void UpdateSaleBalance(string query)//, decimal cashBalance)//, decimal goldBalance)
        {
            //string querry = "Update Sale set Balance=@Balance where SaleNo=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@oldChildCode", SqlDbType.NVarChar).Value = ccode;

            //cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            //cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
        public void UpdateSaleBalance(string query, SqlConnection con,SqlTransaction tran)//, decimal goldBalance)
        {
            //string querry = "Update Sale set Balance=@Balance where SaleNo=" + saleNo;
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tran;
            //cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            //cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
            try
            {
                //con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //    cmd.Dispose();
                //}
            }
        }
        public List<string> GetAllTags(string query)
        {
            string getRecord = query;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            cmd.CommandType = CommandType.Text;
            List<string> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (records == null) records = new List<string>();

                    do
                    {
                        string str = dr["TagNo"].ToString();
                        records.Add(str);
                    }

                    while (dr.Read());

                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (records != null) records.TrimExcess();
            return records;


        }

       
        public SaleLineItem GetSaleByTagNo(string tagNo)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetSaleByTagNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = tagNo;
            SaleLineItem sli = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    sli = new SaleLineItem();
                    //smp.StockId = Convert.ToInt32(dr["StockId"]);

                    sli.Stock = new Stock();

                    ItemType it;
                    if (Convert.ToString(dr["IType"]) == "Gold")
                        it = ItemType.Gold;
                    else if (Convert.ToString(dr["IType"]) == "Diamond")
                        it = ItemType.Diamond;
                    else if (Convert.ToString(dr["IType"]) == "Pladium")
                        it = ItemType.Pladium;
                    else if (Convert.ToString(dr["IType"]) == "Platinum")
                        it = ItemType.Platinum;
                    else
                        it = ItemType.Silver;
                    sli.Stock.ItemType = it;
                    sli.Stock.StockId = Convert.ToInt32(dr["StockId"]);

                    if (Convert.ToInt32(dr["SaleNo"]) != 0 || dr["SaleNo"] != DBNull.Value)
                        sli.Stock.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                    else
                        sli.Stock.SaleNo = 0;

                    if (dr["OrderNo"] != DBNull.Value)
                        sli.Stock.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    else
                        sli.Stock.OrderNo = 0;

                    sli.Stock.TagNo = dr["TagNo"].ToString();
                    sli.Stock.BStatus = dr["BStatus"].ToString();
                    sli.Stock.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["SaleWeight"] == DBNull.Value)
                        sli.Stock.SaleWeight = null;
                    else
                        sli.Stock.SaleWeight = Convert.ToDecimal(dr["SaleWeight"]);
                    if (dr["TotalWeight"] == DBNull.Value)
                        sli.Stock.TotalWeight = 0;
                    else
                        sli.Stock.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                    if (dr["CPWeight"] == DBNull.Value)
                        sli.Stock.CPureWeight = null;
                    else
                        sli.Stock.CPureWeight = Convert.ToDecimal(dr["CPWeight"]);

                    if (dr["OtherCharges"] == DBNull.Value)
                        sli.Stock.OtherCharges = 0;
                    else
                        sli.Stock.OtherCharges = Convert.ToDecimal(dr["OtherCharges"]);

                    if (dr["SaleDate"] == DBNull.Value)
                        sli.Stock.SaleDate = null;
                    else
                        sli.Stock.SaleDate = Convert.ToDateTime(dr["SaleDate"]);

                    sli.Stock.Karrat = Convert.ToString(dr["Karat"]);

                    if (dr["SaleQTY"] == DBNull.Value)
                        sli.Stock.SaleQty = null;
                    else
                        sli.Stock.SaleQty = Convert.ToInt32(dr["SaleQTY"]);

                    if (dr["NetAmount"] == DBNull.Value)
                        sli.Stock.NetAmount = null;
                    else
                        sli.Stock.NetAmount = Convert.ToInt32(dr["NetAmount"]);

                    sli.Stock.Karrat = dr["Karat"].ToString();

                    if (dr["RatePerGm"] == DBNull.Value)
                        sli.Stock.RatePerGm = null;
                    else
                        sli.Stock.RatePerGm = Convert.ToInt32(dr["RatePerGm"]);

                    sli.Stock.Description = dr["Description"].ToString();
                    sli.Stock.ChWtDesc = dr["ChWtDesc"].ToString();

                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        Worker wrk = new Worker();
                        wrk.ID = 0;
                        sli.Stock.WorkerName = wrk;
                    }
                    else
                        sli.Stock.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    //stk.WorkerName=new Worker( Convert.ToInt32(dr["WorkerId"]));

                    if (dr["WasteInGm"] == DBNull.Value)
                        sli.Stock.WasteInGm = null;
                    else
                        sli.Stock.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        sli.Stock.WastePercent = null;
                    else
                        sli.Stock.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Pieces"] == DBNull.Value)
                        sli.Stock.Pieces = null;
                    else
                        sli.Stock.Pieces = Convert.ToInt32(dr["Pieces"]);

                    if (dr["Discount"] == DBNull.Value)
                        sli.Stock.Discount = null;
                    else
                        sli.Stock.Discount = Convert.ToDecimal(dr["Discount"]);

                    if (dr["Kaat"] == DBNull.Value)
                        sli.Stock.KaatInRatti = null;
                    else
                        sli.Stock.KaatInRatti = Convert.ToDecimal(dr["Kaat"]);


                    if (dr["LakerGm"] == DBNull.Value)
                        sli.Stock.LakerGm = null;
                    else
                        sli.Stock.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        sli.Stock.TotalLaker = null;
                    else
                        sli.Stock.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                    if (dr["CKaat"] == DBNull.Value)
                        sli.Stock.CKaat = null;
                    else
                        sli.Stock.CKaat = Convert.ToDecimal(dr["CKaat"]);

                    if (dr["CWaste"] == DBNull.Value)
                        sli.Stock.CWaste = null;
                    else
                        sli.Stock.CWaste = Convert.ToDecimal(dr["CWaste"]);

                    if (sli.Stock.CKaat.HasValue || sli.Stock.CWaste.HasValue)
                    {
                    }

                    if (dr["MakingPerGm"] == DBNull.Value)
                        sli.Stock.MakingPerGm = null;
                    else
                        sli.Stock.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        sli.Stock.TotalMaking = 0;
                    else
                        sli.Stock.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);

                    if (dr["TPrice"] == DBNull.Value)
                        sli.Stock.TotalPrice = null;
                    else
                        sli.Stock.TotalPrice = Convert.ToDecimal(dr["TPrice"]);

                    if (dr["OrderNo"] == DBNull.Value)
                        sli.Stock.OrderNo = 0;
                    else
                        sli.Stock.OrderNo = Convert.ToInt32(dr["OrderNo"]);

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        sli.Stock.DesignNo = new Design();
                        sli.Stock.DesignNo.DesignId = 0;
                    }
                    else
                        sli.Stock.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    if (dr["WTola"] == DBNull.Value)
                        sli.Stock.WTola = null;
                    else
                        sli.Stock.WTola = Convert.ToInt32(dr["WTola"]);

                    if (dr["WMasha"] == DBNull.Value)
                        sli.Stock.WMasha = null;
                    else
                        sli.Stock.WMasha = Convert.ToInt32(dr["WMasha"]);

                    if (dr["WRatti"] == DBNull.Value)
                        sli.Stock.WRatti = null;
                    else
                        sli.Stock.WRatti = Convert.ToInt32(dr["WRatti"]);

                    if (dr["PTola"] == DBNull.Value)
                        sli.Stock.PTola = null;
                    else
                        sli.Stock.PTola = Convert.ToInt32(dr["PTola"]);

                    if (dr["PMasha"] == DBNull.Value)
                        sli.Stock.PMasha = null;
                    else
                        sli.Stock.PMasha = Convert.ToInt32(dr["PMasha"]);

                    if (dr["PRatti"] == DBNull.Value)
                        sli.Stock.PRatti = null;
                    else
                        sli.Stock.PRatti = Convert.ToInt32(dr["PRatti"]);

                    if (dr["TTola"] == DBNull.Value)
                        sli.Stock.TTola = null;
                    else
                        sli.Stock.TTola = Convert.ToInt32(dr["TTola"]);

                    if (dr["TMasha"] == DBNull.Value)
                        sli.Stock.TMasha = null;
                    else
                        sli.Stock.TMasha = Convert.ToInt32(dr["TMasha"]);

                    if (dr["TRatti"] == DBNull.Value)
                        sli.Stock.TRatti = null;
                    else
                        sli.Stock.TRatti = Convert.ToInt32(dr["TRatti"]);

                    if (dr["Status"] == DBNull.Value)
                        sli.Stock.Status = null;
                    else
                        sli.Stock.Status = dr["Status"].ToString();

                    if (dr["GoldOfWaste"] == DBNull.Value)
                        sli.Stock.GoldOfWaste = false;
                    else
                        sli.Stock.GoldOfWaste = true;

                    sli.Stock.Silver = new Silver();
                    if (dr["RateA"] == DBNull.Value)
                        sli.Stock.Silver.RateA = null;
                    else
                        sli.Stock.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                    if (dr["PriceA"] == DBNull.Value)
                        sli.Stock.Silver.PriceA = null;
                    else
                        sli.Stock.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                    if (dr["RateD"] == DBNull.Value)
                        sli.Stock.Silver.RateD = null;
                    else
                        sli.Stock.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                    if (dr["PriceD"] == DBNull.Value)
                        sli.Stock.Silver.PriceD = null;
                    else
                        sli.Stock.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);
                    if (dr["SilverSalePrice"] == DBNull.Value)
                        sli.Stock.Silver.SalePrice = null;
                    else
                        sli.Stock.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                    if (dr["GoldRate"] == DBNull.Value)
                        sli.GRate = 0;
                    else
                        sli.GRate = Convert.ToDecimal(dr["SilverSalePrice"]);

                    if ((dr["pFlag"]) != DBNull.Value)
                        sli.Stock.pFlag = Convert.ToBoolean(dr["pFlag"]);

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        sli.Stock.ImageMemory = stkDAl.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + sli.Stock.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        sli.Stock.ImageMemory = null;
                    }

                    sli.Stock.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    sli.Stock.StoneList = sDal.GetAllStonesDetail(sli.Stock.TagNo);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return sli;
        }

        public SaleLineItem GetOrderSaleByTagNo(string tagNo)
        {
            //string query = "Select * from Stock where itemfor ='Order' and Status ='Not Available' and TagNo ='"+tagNo+"'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetOrderSaleByTagNo", con);
            //SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = tagNo;
            SaleLineItem sli = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    sli = new SaleLineItem();
                    //smp.StockId = Convert.ToInt32(dr["StockId"]);

                    sli.Stock = new Stock();

                    ItemType it;
                    if (Convert.ToString(dr["IType"]) == "Gold")
                        it = ItemType.Gold;
                    else if (Convert.ToString(dr["IType"]) == "Diamond")
                        it = ItemType.Diamond;
                    else
                        it = ItemType.Silver;
                    sli.Stock.ItemType = it;
                    sli.Stock.StockId = Convert.ToInt32(dr["StockId"]);

                    if ((dr["SaleNo"]).ToString() != "0" || (dr["SaleNo"]) != DBNull.Value)
                        sli.Stock.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                    else
                        sli.Stock.SaleNo = 0;

                    sli.Stock.TagNo = dr["TagNo"].ToString();
                    sli.Stock.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["SaleWeight"] == DBNull.Value)
                        sli.Stock.SaleWeight = null;
                    else
                        sli.Stock.SaleWeight = Convert.ToDecimal(dr["SaleWeight"]);

                    if (dr["OtherCharges"] == DBNull.Value)
                        sli.Stock.OtherCharges = 0;
                    else
                        sli.Stock.OtherCharges = Convert.ToDecimal(dr["OtherCharges"]);

                    if (dr["CPWeight"] == DBNull.Value)
                        sli.Stock.CPureWeight = null;
                    else
                        sli.Stock.CPureWeight = Convert.ToDecimal(dr["CPWeight"]);


                    sli.Stock.Karrat = Convert.ToString(dr["Karat"]);
                   
                    if (dr["SaleQTY"] == DBNull.Value)
                        sli.Stock.SaleQty = null;
                    else
                        sli.Stock.SaleQty = Convert.ToInt32(dr["SaleQTY"]);

                    if (dr["NetAmount"] == DBNull.Value)
                        sli.Stock.NetAmount = 0;
                    else
                        sli.Stock.NetAmount = Convert.ToInt32(dr["NetAmount"]);

                    sli.Stock.Karrat = dr["Karat"].ToString();

                    if (dr["RatePerGm"] == DBNull.Value)
                        sli.Stock.RatePerGm = null;
                    else
                        sli.Stock.RatePerGm = Convert.ToInt32(dr["RatePerGm"]);

                    sli.Stock.Description = dr["Description"].ToString();
                    sli.Stock.ChWtDesc = dr["ChWtDesc"].ToString();

                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        Worker wrk = new Worker();
                        wrk.ID = 0;
                        sli.Stock.WorkerName = wrk;
                    }
                    else
                        sli.Stock.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    //stk.WorkerName=new Worker( Convert.ToInt32(dr["WorkerId"]));

                    if (dr["WasteInGm"] == null)
                        sli.Stock.WasteInGm = null;
                    else
                        sli.Stock.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        sli.Stock.WastePercent = null;
                    else
                        sli.Stock.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Pieces"] == DBNull.Value)
                        sli.Stock.Pieces = null;
                    else
                        sli.Stock.Pieces = Convert.ToInt32(dr["Pieces"]);

                    if (dr["Discount"] == DBNull.Value)
                        sli.Stock.Discount = null;
                    else
                        sli.Stock.Discount = Convert.ToDecimal(dr["Discount"]);

                    if (dr["Kaat"] == DBNull.Value)
                        sli.Stock.KaatInRatti = null;
                    else
                        sli.Stock.KaatInRatti = Convert.ToDecimal(dr["Kaat"]);


                    if (dr["LakerGm"] == DBNull.Value)
                        sli.Stock.LakerGm = null;
                    else
                        sli.Stock.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        sli.Stock.TotalLaker = null;
                    else
                        sli.Stock.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                    if (dr["CKaat"] == DBNull.Value)
                        sli.Stock.CKaat = null;
                    else
                        sli.Stock.CKaat = Convert.ToDecimal(dr["CKaat"]);

                    if (dr["CWaste"] == DBNull.Value)
                        sli.Stock.CWaste = null;
                    else
                        sli.Stock.CWaste = Convert.ToDecimal(dr["CWaste"]);

                    if (sli.Stock.CKaat.HasValue || sli.Stock.CWaste.HasValue)
                    {
                    }

                    if (dr["MakingPerGm"] == DBNull.Value)
                        sli.Stock.MakingPerGm = null;
                    else
                        sli.Stock.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        sli.Stock.TotalMaking = null;
                    else
                        sli.Stock.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);

                    if (dr["TotalPrice"] == DBNull.Value)
                        sli.Stock.TotalPrice = 0;
                    else
                        sli.Stock.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                    if (dr["DesignId"] == DBNull.Value)
                    {
                        sli.Stock.DesignNo = new Design();
                        sli.Stock.DesignNo.DesignId = 0;
                    }
                    else
                        sli.Stock.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());
                    if (dr["WTola"] == DBNull.Value)
                        sli.Stock.WTola = null;
                    else
                        sli.Stock.WTola = Convert.ToInt32(dr["WTola"]);

                    if (dr["WMasha"] == DBNull.Value)
                        sli.Stock.WMasha = null;
                    else
                        sli.Stock.WMasha = Convert.ToInt32(dr["WMasha"]);

                    if (dr["WRatti"] == DBNull.Value)
                        sli.Stock.WRatti = null;
                    else
                        sli.Stock.WRatti = Convert.ToInt32(dr["WRatti"]);

                    if (dr["PTola"] == DBNull.Value)
                        sli.Stock.PTola = null;
                    else
                        sli.Stock.PTola = Convert.ToInt32(dr["PTola"]);

                    if (dr["PMasha"] == DBNull.Value)
                        sli.Stock.PMasha = null;
                    else
                        sli.Stock.PMasha = Convert.ToInt32(dr["PMasha"]);

                    if (dr["PRatti"] == DBNull.Value)
                        sli.Stock.PRatti = null;
                    else
                        sli.Stock.PRatti = Convert.ToInt32(dr["PRatti"]);

                    if (dr["TTola"] == DBNull.Value)
                        sli.Stock.TTola = null;
                    else
                        sli.Stock.TTola = Convert.ToInt32(dr["TTola"]);

                    if (dr["TMasha"] == DBNull.Value)
                        sli.Stock.TMasha = null;
                    else
                        sli.Stock.TMasha = Convert.ToInt32(dr["TMasha"]);

                    if (dr["TRatti"] == DBNull.Value)
                        sli.Stock.TRatti = null;
                    else
                        sli.Stock.TRatti = Convert.ToInt32(dr["TRatti"]);

                    if (dr["Status"] == DBNull.Value)
                        sli.Stock.Status = null;
                    else
                        sli.Stock.Status = dr["Status"].ToString();

                    if (dr["GoldOfWaste"] == DBNull.Value)
                        sli.Stock.GoldOfWaste = false;
                    else
                        sli.Stock.GoldOfWaste = true;
                    if (dr["TotalWeight"] == DBNull.Value)
                        sli.Stock.TotalWeight = 0;
                    else
                        sli.Stock.TotalWeight = Convert .ToDecimal (dr["TotalWeight"]);
                    sli.Stock.OPWeight = sli.Stock.TotalWeight * Convert.ToInt16(sli.Stock.Karrat) / 24;
                    sli.Stock.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    sli.Stock.StoneList = sDal.GetAllStonesDetail(sli.Stock.TagNo);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }

            return sli;
        }

        public Sale GetSaleBySaleNo(int saleNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select s.*,sm.[Name],sm.id,ci.* from Sale s inner join CustomerInfo ci on ci.AccountCode=s.CustAccountNo left outer join SaleMan sm on s.SaleManId= sm.id where s.Status<>'Sale Return' and  SaleNo=" + saleNo, con);
            cmd.CommandType = CommandType.Text;
            Sale sale = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (saleNo > 0)
                {
                    if (dr.Read())
                    {
                        sale = new Sale();
                        if (dr["BillBookNo"] == DBNull.Value)
                            sale.BillBookNo = null;
                        else
                            sale.BillBookNo = dr["BillBookNo"].ToString();
                        sale.SVNO = dr["VNO"].ToString();
                        sale.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                        sale.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                        sale.SDate = Convert.ToDateTime(dr["SDate"]);
                        if (dr["ODate"] == DBNull.Value)
                            sale.ODate = null;
                        else
                            sale.ODate = Convert.ToDateTime(dr["ODate"]);
                        sale.CusAccountNo = dr["CustAccountNo"].ToString();
                        sale.BillDiscout = Convert.ToDecimal(dr["BillDiscount"]);
                        sale.NetBill = Convert.ToDecimal(dr["NetBill"]);
                        if (dr["CashPayment"] == DBNull.Value)
                            sale.CashPayment = 0;
                        else
                            sale.CashPayment = Convert.ToDecimal(dr["CashPayment"]);
                        sale.SalesMan = new SaleMan();
                        if (sale.SalesMan.ID == null || sale.SalesMan.ID == 0)
                            sale.SalesMan.ID = 0;
                        else
                            sale.SalesMan.ID = (Convert.ToInt32(dr["Id"]));
                        sale.SalesMan.Name = (dr["Name"]).ToString();
                        sale.TReceivedAmount = Convert.ToDecimal(dr["TReceivedAmount"]);
                        if (dr["OFixRate"] == DBNull.Value)
                            sale.OFixRate = 0;
                        else
                            sale.OFixRate = Convert.ToDecimal(dr["OFixRate"]);
                        sale.CustName = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Address"].ToString());

                        //if (Convert.ToInt32(dr["OrderNo"]) != 0)
                        //{
                        //    sale.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                        //    sale.ODate = Convert.ToDateTime(dr["ODate"]);
                        //    foreach (string strTag in GetAllTags("select TagNo from Stock Where SaleNo=" + sale.SaleNo))
                        //    {
                        //        sale.AddLineItems(GetOrderSaleByTagNo(strTag));
                        //    }
                        //}
                        //else
                        //{
                            foreach (string strTag in GetAllTags("select TagNo from bulkStock Where SaleNo=" + sale.SaleNo))
                            {
                                sale.AddLineItems(GetSaleByTagNo(strTag));
                            }
                        //}
                        SalePayment sp = new SalePayment();
                        PaymentsDAL pDAL = new PaymentsDAL();
                        Gold gld = new Gold();

                        sp = pDAL.GetCashRceiveBySaleNo(sale.SaleNo);
                        if (sp.Amount != null)
                            sale.CashReceive = sp.Amount;
                        sp = pDAL.GetCheckRceiveBySaleNo(sale.SaleNo);
                        if (sp.Amount != null)
                            sale.CheckCash = sp.Amount;
                        sp = pDAL.GetCCardRceiveBySaleNo(sale.SaleNo);
                        if (sp.Amount != null)
                            sale.CreditCard = sp.Amount;
                        gld = pDAL.GetPureGoldBySaleNo(sale.SaleNo);
                        if (gld.Amount != null)
                        {
                            sale.PureGoldCharges = gld.Amount;
                            sale.epureWeight = gld.Weight;
                        }
                        gld = pDAL.GetUsedGoldBySaleNo(sale.SaleNo);
                        if (gld.Amount != null)
                        {
                            sale.UsedGoldCharges = gld.Amount;
                            sale.eusedWeight = gld.Weight;
                        }
                        gld = pDAL.GetPureGoldByOtherChergesSaleNo(sale.SaleNo);
                        if (gld.Weight != null)
                        {
                            sale.OtherChergesReceivedGold = gld.Weight;
                        }
                    }
                }                
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return sale;
        }

        public void UpdateTreceivedbyOrderNo(int ono, decimal treceived)
        {
            string query = "Update sale set TReceivedAmount ="+treceived +" where OrderNo ="+ono;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void UpdateTreceivedbySaleNO(int sno, decimal treceived)
        {
            string query = "Update sale set TReceivedAmount =" + treceived + " where SaleNo =" + sno;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        /*public void DamageStock(int id)
        {

            string damageStock = "update Stock set Status='Damage' where StockId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDamage = new SqlCommand(damageStock, con);
            cmdDamage.CommandType = CommandType.Text;

            
            try
            {
                con.Open();
                cmdDamage.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                con.Close();
            }
        }*/
        public List<Stock> GetTagNoByItemIdForSilverSale(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id + " and Status='Not Available' and ItemFor='Sale'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            //cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

            List<Stock> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Stock>();
                    if (records == null) records = new List<Stock>();

                    do
                    {
                        Stock stk = new Stock();
                        // stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();




                        records.Add(stk);
                    }

                    while (dr.Read());

                }
                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if (records != null) records.TrimExcess();
            return records;


        }

        public void DamageStock(Sale s)
        {
            string damageStock = "update Stock set Status=@Status,DamDate=@DamDate where TagNo=@oldTagNo";
            
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDamage = new SqlCommand(damageStock, con);
            cmdDamage.CommandType = CommandType.Text;


            cmdDamage.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);

            cmdDamage.Parameters.Add("@DamDate", SqlDbType.DateTime);
            cmdDamage.Parameters.Add("@Status", SqlDbType.NVarChar);
           

                    

            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmdDamage.Transaction = tran;
                
               
                try
                {
                    foreach (SaleLineItem sli in s.SaleLineItem)
                    {
                       
                        cmdDamage.Parameters["@DamDate"].Value = DateTime.Now;

                        cmdDamage.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                        cmdDamage.Parameters["@Status"].Value = "Damage";


                        cmdDamage.ExecuteNonQuery();

                    }
                  
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();

                    throw ex;
                }

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }

        public void UpdateSaleNoForOrderSale(Sale s,SqlConnection con , SqlTransaction trans)
        {
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateSaleNoForOrderSale", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderNo", SqlDbType.Int ).Value = s.OrderNo ;
            cmd.Parameters.Add(new SqlParameter ("@SaleNo", SqlDbType.Int )).Value = (int)s.SaleNo ;
            try
            {
                cmd.Transaction = trans;
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    //tran.Rollback();

                    //throw ex;
                }

            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }

        public Sale GetSilverSaleBySaleNo(int saleNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select s.*, ci.*, (((Select ISNULL(SUM(Amount), 0) from Sale_Payment where ONO = s.OrderNo and SNO = 0 and VNO not like 'CPV%') + (Select ISNULL(SUM(Amount), 0) from GoldDetail where ONO = s.OrderNo and SNO = 0)) - (Select ISNULL(SUM(Amount), 0) from Sale_Payment where ONO = s.OrderNo and SNO = 0 and VNO like 'CPV%'))'Advance' from Sale s inner join CustomerInfo ci on ci.AccountCode=s.CustAccountNo where s.Status = 'Stock Silver Sale' and  s.SaleNo=" + saleNo, con);
            cmd.CommandType = CommandType.Text;
            Sale sale = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sale = new Sale();
                    if (dr["BillBookNo"] == DBNull.Value)
                        sale.BillBookNo = null;
                    else
                        sale.BillBookNo = dr["BillBookNo"].ToString();
                    if (dr["SaleManId"] == DBNull.Value)
                    {
                        sale.SalesMan = new SaleMan();
                        sale.SalesMan.ID = 0;
                    }
                    else
                    {
                        sale.SalesMan = new SaleMan();
                        sale.SalesMan.ID =Convert.ToInt32( dr["SaleManId"]);
                    }

                    sale.SVNO = dr["VNO"].ToString();
                    sale.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                    sale.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    sale.SDate = Convert.ToDateTime(dr["SDate"]);
                    sale.CusAccountNo = dr["CustAccountNo"].ToString();
                    sale.BillDiscout = Convert.ToDecimal(dr["BillDiscount"]);
                    sale.TReceivedAmount = Convert.ToDecimal(dr["TReceivedAmount"]);
                    sale.Advance = Convert.ToDecimal(dr["Advance"]);
                    sale.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                    sale.NetBill = Convert.ToDecimal(dr["NetBill"]);
                    if (dr["OFixRate"] == DBNull.Value)
                        sale.OFixRate = 0;
                    else
                        sale.OFixRate = Convert.ToDecimal(dr["OFixRate"]);
                    sale.CustName = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Address"].ToString());

                    //if (Convert.ToInt32(dr["OrderNo"]) != 0)
                    //{
                    //    sale.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    //    //sale.ODate = Convert.ToDateTime(dr["ODate"]);
                    //    foreach (string strTag in GetAllTags("select TagNo from Stock Where OrderNo=" + sale.OrderNo))
                    //    {
                    //        sale.AddLineItems(GetOrderSaleByTagNo(strTag));
                    //    }
                    //}
                    //else
                    {
                        foreach (string strTag in GetAllTags("select TagNo from Stock Where SaleNo=" + sale.SaleNo))
                        {
                            sale.AddLineItems(GetSaleByTagNo(strTag));
                        }
                    }
                    SalePayment sp = new SalePayment();
                    PaymentsDAL pDAL = new PaymentsDAL();
                    Gold gld = new Gold();

                    sp = pDAL.GetCashRceiveBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.CashReceive = sp.Amount;
                    sp = pDAL.GetCashPaymentBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.CashPayment = sp.Amount;
                    sp = pDAL.GetCheckRceiveBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.CheckCash = sp.Amount;
                    sp = pDAL.GetCCardRceiveBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.CreditCard = sp.Amount;
                    gld = pDAL.GetUsedGoldBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.UsedGoldCharges = gld.Amount;
                    gld = pDAL.GetPureGoldBySaleNo(sale.SaleNo);
                    if (sp.Amount != null)
                        sale.PureGoldCharges = gld.Amount;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return sale;
        }

        public void CompleteSaleFromBStock()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(@"truncate table bulkstock insert into BulkStock 
            select * from Stock insert into BulkStock select StockId, TagNo, BarCode, ItemId, ItmName, SubGItmId, SubItemId, Subitem, NetWeight, SaleWeight, TotalWeight,
            ItemSize, Qty, StQty, Pieces, SaleQty, Karat, Description, StockDate, WorkerId, WasteInGm, WastePercent, Kaat, PWeight, CPWeight, CKaat, CWaste, LakerGm, 
            TotalLaker, RatePerGm, MakingPerGm, MakingPerTola, TotalMaking, TotalPrice, WTola, WMasha, WRatti, PTola, PMasha, PRatti, TTola, TMasha, TRatti, RateType, 
            GoldRate, RateA, PriceA, RateD, PriceD, DesignId, IType, MakingType, SaleDate, Status, OrderNo, OItemId, IndexNo, PicName, ItemCost, SalePrice, SaleNo, ItemFor, 
            OldTagNo, ChWtDesc, OtherCharges, SampleNo, SampleReturnDate, StWtTotal, StWtWorker, bFlag, sFlag, Picture, Discount, SaleWasteInGm, GoldOfWaste, DelDate, 
            DamDate, SilverSalePrice, pFlag, NetAmount, ThumbNail, WkName, DesNo, BQuantity, BWeight, BStatus, UserId, PAccountCode,PurchaseRate ,DelDescription  from BSale", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                { con.Open(); }

                cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public void DamageStock(string tagNo)
        {
            string damageStock = "update Stock set Status='Available',DamDate=null where TagNo='" + tagNo + "'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDamage = new SqlCommand(damageStock, con);
            cmdDamage.CommandType = CommandType.Text;
            try
            {
                con.Open();
                cmdDamage.ExecuteNonQuery();
                try
                {

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
        public void DeleteStock(string tagNo)
        {
            string damageStock = "update Stock set Status='Available',DelDate=null where TagNo='" + tagNo + "'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDamage = new SqlCommand(damageStock, con);
            cmdDamage.CommandType = CommandType.Text;
            try
            {
                con.Open();

                cmdDamage.ExecuteNonQuery();


                try
                {




                }
                catch (Exception ex)
                {
                    //  tran.Rollback();

                    throw ex;
                }

            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }





        }
        public void StartUpSaleRateFix(string query)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(query, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                con.Open();

                SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            finally
            {
                con.Close();
            }
        }
        public string GetSaleRateFixStatus()
        {
            string querry = "Select FixRateStatus as [FixRateStatus] from StartUp";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            string saleNo = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["FixRateStatus"] == DBNull.Value)
                        saleNo = "";
                    else
                        saleNo = Convert.ToString(dr["FixRateStatus"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }
        public string GetStartupGramTolaRate()
        {
            string querry = "Select GramTolaRate as [GramTolaRate] from StartUp";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            string saleNo = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["GramTolaRate"] == DBNull.Value)
                        saleNo = "";
                    else
                        saleNo = Convert.ToString(dr["GramTolaRate"]);
                }

                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }

        public string GetStartupGoldRateType()
        {
            string querry = "Select GoldRateType as [GoldRateType] from StartUp";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            string saleNo = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["GoldRateType"] == DBNull.Value)
                        saleNo = "";
                    else
                        saleNo = Convert.ToString(dr["GoldRateType"]);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return saleNo;
        }

        public bool isSaleNoExist(int SNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from BulkStock where SaleNo = " + SNo, con);
            cmd.CommandType = CommandType.Text;
            bool nFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    nFlag = true;


                dr.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return nFlag;
        }
        public string GetCustAccBySNO(int SaleNo)
        {
            string querry = "Select CustAccountNo,SDate from sale where SaleNo=" + SaleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            string CustAccountNo = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["CustAccountNo"] == DBNull.Value)
                        saleNo = 0;
                    else
                        CustAccountNo = dr["CustAccountNo"].ToString();
                }

                dr.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return CustAccountNo;
        }
        public DateTime GetSDateBySNO(int SaleNo)
        {
            string querry = "Select SDate from sale where SaleNo=" + SaleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            DateTime SDate = DateTime.Now;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["SDate"] == DBNull.Value)
                        saleNo = 0;
                    else
                        SDate = Convert.ToDateTime(dr["SDate"]);
                }

                dr.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return SDate;
        }
        public Banks GetBankByAccountNo(string str)
        {
            string querry = "select BankId, (Select BankName from Bank where Id = BankId)'BankName' from BankAccount where AccountNo = '" + str + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            Banks ba = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    ba = new Banks();
                    ba.Id = Convert.ToInt32(dr["BankId"]);
                    ba.BankName = dr["BankName"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return ba;
        }
    }
}
