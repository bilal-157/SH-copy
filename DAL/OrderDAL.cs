using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

namespace DAL
{
    public class OrderDAL
    {
        public void AddOrder(OrderEstimat o,SqlConnection con,SqlTransaction tran)
        {
            string addSaleOrd = "AddOrderMaster";
            string addOrder = "AddOrder";
            SqlCommand cmdSaleOrd = new SqlCommand(addSaleOrd, con);
            cmdSaleOrd.CommandType = CommandType.StoredProcedure;
            cmdSaleOrd.Parameters.Add(new SqlParameter("@SaleNo", o.SaleNo));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@OrderNo", o.OrderNo));
            if (o.ODate.HasValue)
                cmdSaleOrd.Parameters.Add(new SqlParameter("@ODate", o.ODate));
            else
                cmdSaleOrd.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;
            if (o.DDate.HasValue)
                cmdSaleOrd.Parameters.Add(new SqlParameter("@DDate", o.DDate));
            else
                cmdSaleOrd.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            cmdSaleOrd.Parameters.Add(new SqlParameter("@TotalPrice", o.TotalPrice));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@BillDiscount", o.BillDiscout));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@NetBill", o.NetBill));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@Balance", o.Balance));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@TReceivedAmount", o.TReceivedAmount));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@CustAccountNo", o.CusAccountNo));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@BillBookNo", o.BillBookNo));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@Status", o.Status));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@OFixRate", o.OFixRate));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@CashReceive", o.CashReceive));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@CashPayment", o.CashPayment));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@CCAmount", o.CreditCard));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@CAmount", o.CheckCash));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@PGoldAmount", o.PureGoldCharges));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@UGoldAmount", o.UsedGoldCharges));

            SqlCommand cmd = new SqlCommand(addOrder, con);
            cmd.Parameters.Add("@OItemId", SqlDbType.NVarChar);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@IType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@SubGItmId", SqlDbType.Int);
            cmd.Parameters.Add("@SubItemId", SqlDbType.Int);
            cmd.Parameters.Add("@Weight", SqlDbType.Float);
            cmd.Parameters.Add("@DesignNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Karrat", SqlDbType.NVarChar);
            cmd.Parameters.Add("@QTY", SqlDbType.Float);
            cmd.Parameters.Add("@pFlag", SqlDbType.Bit );
            cmd.Parameters.Add("@GoldRate", SqlDbType.Float);
            cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
            cmd.Parameters.Add("@WDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@WRDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@Waste", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@PTola", SqlDbType.Float);
            cmd.Parameters.Add("@PMasha", SqlDbType.Float);
            cmd.Parameters.Add("@PRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            //cmd.Parameters.Add("@GolRate", SqlDbType.Float);
            cmd.Parameters.Add("@ItemSize", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OPGold", SqlDbType.Float);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdStone = new SqlCommand("AddStoneForOrder", con);
            cmdStone.Parameters.Add(new SqlParameter("@OItemId", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.CommandType = CommandType.StoredProcedure;

            try
            {
               // con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdSaleOrd.Transaction = tran;
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSaleOrd.ExecuteNonQuery();
                try
                {
                    foreach (OrderLineItem oli in o.OrderLineItem)
                    {
                        cmd.Parameters["@OItemId"].Value = oli.OItemId;
                        cmd.Parameters["@TagNo"].Value = oli.Stock.TagNo;
                        cmd.Parameters["@IType"].Value = oli.Stock.ItemType;
                        cmd.Parameters["@ItemId"].Value = oli.Stock.ItemName.ItemId;
                        cmd.Parameters["@SubGItmId"].Value = DBNull.Value;                        
                        cmd.Parameters["@SubItemId"].Value = DBNull.Value;
                        
                        if (oli.Stock.pFlag!=null )
                            cmd.Parameters["@pFlag"].Value = oli.Stock.pFlag ;
                        else
                            cmd.Parameters["@pFlag"].Value = DBNull.Value;
                        cmd.Parameters["@GoldRate"].Value = oli.GRate ;
                        cmd.Parameters["@Weight"].Value = oli.Stock.NetWeight;
                        cmd.Parameters["@DesignNo"].Value = oli.DesignNo;
                        if (oli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = oli.Stock.Description;
                        cmd.Parameters["@Karrat"].Value = oli.Stock.Karrat;
                        cmd.Parameters["@QTY"].Value = oli.Stock.Qty;
                        cmd.Parameters["@OrderNo"].Value = o.OrderNo;
                        cmd.Parameters["@WDate"].Value = o.ODate;
                        cmd.Parameters["@WRDate"].Value = o.ODate;
                        cmd.Parameters["@WastePercent"].Value = oli.Stock.WastePercent;
                        cmd.Parameters["@LakerGm"].Value = oli.Stock.LakerGm;
                        cmd.Parameters["@TotalLaker"].Value = oli.Stock.TotalLaker;
                        cmd.Parameters["@RatePerGm"].Value = oli.Stock.RatePerGm;
                        cmd.Parameters["@MakingPerGm"].Value = oli.Stock.MakingPerGm;
                        cmd.Parameters["@TotalMaking"].Value = oli.Stock.TotalMaking;
                        cmd.Parameters["@Waste"].Value = oli.Stock.WasteInGm;
                        cmd.Parameters["@WTola"].Value = oli.Stock.WTola;
                        cmd.Parameters["@WMasha"].Value = oli.Stock.WMasha;
                        cmd.Parameters["@WRatti"].Value = oli.Stock.WRatti;
                        cmd.Parameters["@PTola"].Value = oli.Stock.PTola;
                        cmd.Parameters["@PMasha"].Value = oli.Stock.PMasha;
                        cmd.Parameters["@PRatti"].Value = oli.Stock.PRatti;
                        cmd.Parameters["@TotalWeight"].Value = oli.Stock.TotalWeight;
                        cmd.Parameters["@TTola"].Value = oli.Stock.TTola;
                        cmd.Parameters["@TMasha"].Value = oli.Stock.TMasha;
                        cmd.Parameters["@TRatti"].Value = oli.Stock.TRatti;
                        cmd.Parameters["@ItemSize"].Value = oli.Stock.ItemSize;
                        if (oli.Stock.Karrat == "")
                            cmd.Parameters["@OPGold"].Value = 0;
                        else

                            cmd.Parameters["@OPGold"].Value = (oli.Stock.NetWeight * Convert.ToDecimal(oli.Stock.Karrat)) / 24;
                        if (oli.Stock.WorkerName != null)
                            cmd.Parameters["@WorkerId"].Value = oli.Stock.WorkerName.ID;
                        else
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        cmd.Parameters["@TotalPrice"].Value = oli.Stock.TotalPrice;
                        cmd.Parameters["@Status"].Value = oli.Stock.Status;
                        cmd.ExecuteNonQuery();                        
                    }
                    foreach (OrderLineItem oli in o.OrderLineItem)
                    {
                        if (oli.Stock.StoneList == null)
                        {
                            
                        }
                        else
                        {
                            foreach (Stones stList in oli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@OItemId"].Value = oli.OItemId;
                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneName != "")
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneName"].Value = DBNull.Value;

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
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }
        }
        public List<OrderEstimat> GetOrderRecordsByAccount(string accCode)
        {
            string selectRecord = "select OrderNo,Balance from OrderMaster where status <> 'Cancelled' and CustAccountNo='" + accCode + "' order by OrderNo";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@bFlag", SqlDbType.Bit).Value = true;
            List<OrderEstimat> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<OrderEstimat>();
                    if (records == null) records = new List<OrderEstimat>();

                    do
                    {
                        OrderEstimat cstd = new OrderEstimat();
                        cstd.OrderNo = Convert.ToInt32(dr["OrderNo"]);
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

        public void UpdateSaleNoFromOrder(string tagNo, int SaleNo, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("update OrderEstimate set SaleNo= " + SaleNo + " where TagNo= '" + tagNo + "'", con, trans);
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                throw ex;
            }
        }

        public byte[] PicByOrderNo(int orderNo, string OItemId)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmd = new SqlCommand("select Picture from OtherPictures where  OrderNo=" + orderNo + " and OItemId ='" + OItemId + "'", conpic);

            cmd.CommandType = CommandType.Text;
            OrderEstimat ord = null;

            try
            {
                conpic.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ord = new OrderEstimat();
                    if (DBNull.Value != (dr["Picture"]))
                        ord.Pic = (byte[])dr["Picture"];
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conpic.State == ConnectionState.Open) conpic.Close();
            }
            return ord.Pic;
        }
        public void UpdateOrder(int oldOrderNo, OrderEstimat o,SqlConnection con,SqlTransaction tran)
        {
            string addSaleOrd = "UpdateSaleForOrder";
            string addOrder = "UpdateOrder";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdSaleOrd = new SqlCommand(addSaleOrd, con);
            cmdSaleOrd.CommandType = CommandType.StoredProcedure;
            cmdSaleOrd.Parameters.Add(new SqlParameter("@SaleNo", o.SaleNo));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@OrderNo", oldOrderNo));
            //cmdSale.Parameters.Add(new SqlParameter("@SDate", s.SDate));
            if (o.ODate.HasValue)
                cmdSaleOrd.Parameters.Add(new SqlParameter("@ODate", o.ODate));
            else
            {
                cmdSaleOrd.Parameters.Add("@ODate", SqlDbType.DateTime).Value = DBNull.Value;

            }
            if (o.DDate.HasValue)
                cmdSaleOrd.Parameters.Add(new SqlParameter("@DDate", o.DDate));
            else
            {
                cmdSaleOrd.Parameters.Add("@DDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            cmdSaleOrd.Parameters.Add(new SqlParameter("@TotalPrice", o.TotalPrice));


            cmdSaleOrd.Parameters.Add(new SqlParameter("@BillDiscount", o.BillDiscout));

            cmdSaleOrd.Parameters.Add(new SqlParameter("@NetBill", o.NetBill));


            cmdSaleOrd.Parameters.Add(new SqlParameter("@Balance", o.Balance));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@TReceivedAmount", o.TReceivedAmount));

            cmdSaleOrd.Parameters.Add(new SqlParameter("@CustAccountNo", o.CusAccountNo));
            cmdSaleOrd.Parameters.Add(new SqlParameter("@BillBookNo", o.BillBookNo));

            cmdSaleOrd.Parameters.Add(new SqlParameter("@Status", o.Status));

            SqlCommand cmd = new SqlCommand(addOrder, con);

            cmd.Parameters.Add("@OItemId", SqlDbType.NVarChar);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@IType", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@SubGItmId", SqlDbType.Int);
            cmd.Parameters.Add("@SubItemId", SqlDbType.Int);
            cmd.Parameters.Add("@Weight", SqlDbType.Float);
            cmd.Parameters.Add("@DesignNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Karrat", SqlDbType.NVarChar);
            cmd.Parameters.Add("@QTY", SqlDbType.Float);
            cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
            cmd.Parameters.Add("@WDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
            cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
            cmd.Parameters.Add("@RatePerGm", SqlDbType.Float);
            cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
            cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
            cmd.Parameters.Add("@Waste", SqlDbType.Float);
            cmd.Parameters.Add("@WTola", SqlDbType.Float);
            cmd.Parameters.Add("@WMasha", SqlDbType.Float);
            cmd.Parameters.Add("@WRatti", SqlDbType.Float);
            cmd.Parameters.Add("@PTola", SqlDbType.Float);
            cmd.Parameters.Add("@PMasha", SqlDbType.Float);
            cmd.Parameters.Add("@PRatti", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TTola", SqlDbType.Float);
            cmd.Parameters.Add("@TMasha", SqlDbType.Float);
            cmd.Parameters.Add("@TRatti", SqlDbType.Float);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@TotalPrice", SqlDbType.Float);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);



            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdStone = new SqlCommand("AddStoneForOrder", con);
            cmdStone.Parameters.Add(new SqlParameter("@OItemId", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.CommandType = CommandType.StoredProcedure;



            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdSaleOrd.Transaction = tran;
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSaleOrd.ExecuteNonQuery();
                try
                {
                    foreach (OrderLineItem oli in o.OrderLineItem)
                    {

                        cmd.Parameters["@OItemId"].Value = oli.OItemId;
                        cmd.Parameters["@TagNo"].Value = oli.Stock.TagNo;
                        cmd.Parameters["@IType"].Value = oli.Stock.ItemType;
                        cmd.Parameters["@ItemId"].Value = oli.Stock.ItemName.ItemId;
                        cmd.Parameters["@SubGItmId"].Value = DBNull.Value;
                        cmd.Parameters["@SubItemId"].Value = DBNull.Value;

                        cmd.Parameters["@Weight"].Value = oli.Stock.NetWeight;
                        cmd.Parameters["@DesignNo"].Value = oli.DesignNo;
                        if (oli.Stock.Description == null)
                            cmd.Parameters["@Description"].Value = DBNull.Value;
                        else
                            cmd.Parameters["@Description"].Value = oli.Stock.Description;
                        cmd.Parameters["@Karrat"].Value = oli.Stock.Karrat;
                        cmd.Parameters["@QTY"].Value = oli.Stock.Qty;
                        cmd.Parameters["@OrderNo"].Value = oldOrderNo;
                        cmd.Parameters["@WDate"].Value = o.ODate;
                        cmd.Parameters["@WastePercent"].Value = oli.Stock.WastePercent;
                        cmd.Parameters["@LakerGm"].Value = oli.Stock.LakerGm;
                        cmd.Parameters["@TotalLaker"].Value = oli.Stock.TotalLaker;
                        cmd.Parameters["@RatePerGm"].Value = oli.Stock.RatePerGm;
                        cmd.Parameters["@MakingPerGm"].Value = oli.Stock.MakingPerGm;
                        cmd.Parameters["@TotalMaking"].Value = oli.Stock.TotalMaking;
                        cmd.Parameters["@Waste"].Value = oli.Stock.WasteInGm;
                        cmd.Parameters["@WTola"].Value = oli.Stock.WTola;
                        cmd.Parameters["@WMasha"].Value = oli.Stock.WMasha;
                        cmd.Parameters["@WRatti"].Value = oli.Stock.WRatti;
                        cmd.Parameters["@PTola"].Value = oli.Stock.PTola;
                        cmd.Parameters["@PMasha"].Value = oli.Stock.PMasha;
                        cmd.Parameters["@PRatti"].Value = oli.Stock.PRatti;
                        cmd.Parameters["@TotalWeight"].Value = oli.Stock.TotalWeight;
                        cmd.Parameters["@TTola"].Value = oli.Stock.TTola;
                        cmd.Parameters["@TMasha"].Value = oli.Stock.TMasha;
                        cmd.Parameters["@TRatti"].Value = oli.Stock.TRatti;
                        //cmd.Parameters["@WorkerId"].Value = oli.Stock.WorkerName.ID;
                        if (oli.Stock.WorkerName != null)
                            cmd.Parameters["@WorkerId"].Value = oli.Stock.WorkerName.ID;
                        else
                        {
                            //cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
                            cmd.Parameters["@WorkerId"].Value = DBNull.Value;
                        }
                        cmd.Parameters["@TotalPrice"].Value = oli.Stock.TotalPrice;
                        // cmd.Parameters["@SalePrice"].Value = sli.Stock.SalePrice;
                        cmd.Parameters["@Status"].Value = oli.Stock.Status;


                        cmd.ExecuteNonQuery();

                    }
                    foreach (OrderLineItem oli in o.OrderLineItem)
                    {

                        if (oli.Stock.StoneList == null)
                        {

                        }
                        else
                        {
                            foreach (Stones stList in oli.Stock.StoneList)
                            {
                                cmdStone.Parameters["@OItemId"].Value = oli.OItemId;
                                cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                                if (stList.StoneName != "")
                                    cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                                else
                                    cmdStone.Parameters["@StoneName"].Value = DBNull.Value;

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
        public int GetMaxOrderNo()
        {
            string querry = "Select MAX(OrderNo) as [MaxOrder] from OrderEstimate";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int orderNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["MaxOrder"] == DBNull.Value)
                        orderNo = 0;
                    else
                        orderNo = Convert.ToInt32(dr["MaxOrder"]);
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
            return orderNo;
        }
        public List<OrderEstimat> GetAllOrderNo(string query)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = query;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            List<OrderEstimat> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<OrderEstimat>();
                    if (records == null) records = new List<OrderEstimat>();

                    do
                    {
                        OrderEstimat ord = new OrderEstimat();
                        ord.OrderNo = Convert.ToInt32(dr["OrderNo"]);




                        records.Add(ord);
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
        public List<OrderLineItem> GetAllOItemIdByOrderNo(int oNo)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "select OItemId from OrderEstimate where Status='Estimated' and OrderNo="+oNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            List<OrderLineItem> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<OrderLineItem>();
                    if (records == null) records = new List<OrderLineItem>();

                    do
                    {
                        OrderLineItem ord = new OrderLineItem();
                        ord.OItemId = dr["OItemId"].ToString();




                        records.Add(ord);
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
        public List<string> GetAllItemIdByOrderNo(int id)
        {
            string getRecord = "select oe.OItemId from OrderEstimate oe where  oe.OrderNo=" + id;//oe.Status ='Estimated' and
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            cmd.CommandType = CommandType.Text;
            List<string> records = new List<string> ();

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //if (records == null) records = new List<string>();

                    do
                    {
                        string str = dr["OItemId"].ToString();
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
        public OrderLineItem GetComRecByOItemId(string oNo)
        {
            //string gcrecord = "select * from Costing where bFlag='true'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetComRecByOItemId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OItemId", SqlDbType.NVarChar).Value = oNo;
            OrderLineItem ord = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ord = new OrderLineItem();
                    ord.Stock = new Stock();
                    ItemType it;
                    if ((dr["IType"]).ToString() == "Gold")
                        it = ItemType.Gold;
                    else if ((dr["IType"]).ToString() == "Diamond")
                        it = ItemType.Diamond;
                    else
                        it = ItemType.Silver;

                    ord.Stock.ItemType = it;
                    ord.Stock.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    ord.OItemId = dr["OItemId"].ToString();
                    ord.Stock.TagNo = dr["TagNo"].ToString();
                    ord.Stock.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    ord.Stock.NetWeight = Convert.ToDecimal(dr["Weight"]);
                    if (dr["Qty"] == DBNull.Value)
                    {
                        ord.Stock.Qty = null;
                    }
                    else
                        ord.Stock.Qty = Convert.ToInt32(dr["Qty"]);

                    ord.Stock.Karrat = dr["Karrat"].ToString();

                    if (dr["RatePerGm"] == DBNull.Value)
                    {
                        ord.Stock.RatePerGm = null;
                    }
                    else
                        ord.Stock.RatePerGm = Convert.ToDecimal(dr["RatePerGm"]);

                    ord.Stock.Description = dr["Description"].ToString();
                    ord.Stock.ItemSize = dr["ItemSize"].ToString();
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        ord.Stock.WorkerName = new Worker();
                        ord.Stock.WorkerName.ID = null;
                    }
                    else
                        ord.Stock.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                    ord.DesignNo=dr["DesignNo"].ToString();
                    //if (dr["DesignId"] == DBNull.Value)
                    //{
                    //    ord.Stock.DesignNo = new Design();
                    //    ord.Stock.DesignNo.DesignId = null;
                    //}
                    //else
                    //    ord.Stock.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    ord.Stock.StockDate = Convert.ToDateTime(dr["WDate"]);
                    if (dr["WastePercent"] == DBNull.Value)
                    {
                        ord.Stock.WastePercent = null;
                    }
                    else
                        ord.Stock.WastePercent = Convert.ToDecimal(dr["WastePercent"]);
                    if (dr["Waste"] == DBNull.Value)
                    {
                        ord.Stock.WasteInGm = null;
                    }
                    else
                        ord.Stock.WasteInGm = Convert.ToDecimal(dr["Waste"]);

                    ord.Stock.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                    if (dr["TotalPrice"] == DBNull.Value)
                    {
                        ord.Stock.TotalPrice = null;
                    }
                    else
                    ord.Stock.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);

                    if (dr["MakingPerGm"] == DBNull.Value)
                    {
                        ord.Stock.MakingPerGm = null;
                    }
                    else
                        ord.Stock.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);
                    if (dr["TotalMaking"] == DBNull.Value)
                    {
                        ord.Stock.TotalMaking = null;
                    }
                    else
                        ord.Stock.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);


                    if (dr["LakerGm"] == DBNull.Value)
                    {
                        ord.Stock.LakerGm = null;
                    }
                    else
                        ord.Stock.LakerGm = Convert.ToDecimal(dr["LakerGm"]);
                    if (dr["TotalLaker"] == DBNull.Value)
                    {
                        ord.Stock.TotalLaker = null;
                    }
                    else
                        ord.Stock.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                    if (dr["WTola"] == DBNull.Value)
                    {
                        ord.Stock.WTola = null;
                    }
                    else
                        ord.Stock.WTola = Convert.ToInt32(dr["WTola"]);
                    if (dr["WMasha"] == DBNull.Value)
                    {
                        ord.Stock.WMasha = null;
                    }
                    else
                        ord.Stock.WMasha = Convert.ToInt32(dr["WMasha"]);
                    if (dr["WRatti"] == DBNull.Value)
                    {
                        ord.Stock.WRatti = null;
                    }
                    else
                        ord.Stock.WRatti = Convert.ToInt32(dr["WRatti"]);

                    if (dr["PTola"] == DBNull.Value)
                    {
                        ord.Stock.PTola = null;
                    }
                    else
                        ord.Stock.PTola = Convert.ToInt32(dr["PTola"]);
                    if (dr["PMasha"] == DBNull.Value)
                    {
                        ord.Stock.PMasha = null;
                    }
                    else
                        ord.Stock.PMasha = Convert.ToInt32(dr["PMasha"]);
                    if (dr["PRatti"] == DBNull.Value)
                    {
                        ord.Stock.PRatti = null;
                    }
                    else
                        ord.Stock.PRatti = Convert.ToInt32(dr["PRatti"]);

                    if (dr["TTola"] == DBNull.Value)
                    {
                        ord.Stock.TTola = null;
                    }
                    else
                        ord.Stock.TTola = Convert.ToInt32(dr["TTola"]);
                    if (dr["TMasha"] == DBNull.Value)
                    {
                        ord.Stock.TMasha = null;
                    }
                    else
                        ord.Stock.TMasha = Convert.ToInt32(dr["TMasha"]);
                    if (dr["TRatti"] == DBNull.Value)
                    {
                        ord.Stock.TRatti = null;
                    }
                    else
                        ord.Stock.TRatti = Convert.ToInt32(dr["TRatti"]);

                    ord.Stock.Status = dr["Status"].ToString();

                    //if (pbxPicture.Image != null)
                    //{
                    //    mst = new MemoryStream();
                    //    ord.Stock.ImageMemory = oli.Stock.ConvertImageToBinary(this.pbxPicture.Image);
                    //}
                    //else
                    //{
                    //    ord.Stock.ImageMemory = null;
                    //}


                    //jp = new JewelPictures();
                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);

                        ord.Stock.ImageMemory = this.ImageRestore("SELECT Picture from " + builder.InitialCatalog + ".dbo.JewlPictures where OItemId='" + oNo + "'");

                    }
                    catch (Exception ex)
                    {
                        ord.Stock.ImageMemory = null;
                    }

                   // ord.Stock.ImageMemory = pictureDAL.ImageRestore("select picture from " + builder.InitialCatalog + ".dbo.JewlPictures where OrderNo=" + oli.Stock.OrderNo + " or TagNo='" + oli.Stock.TagNo + "'");
                    //if (jp.ImageMemory == null)
                    //{
                    //    this.pbxPicture.Image = null;
                    //    this.pbxPicture.BorderStyle = BorderStyle.FixedSingle;
                    //}
                    //else
                    //{

                    //    MemoryStream mst = new MemoryStream(jp.ImageMemory);
                    //    this.pbxPicture.Image = Image.FromStream(mst);
                    //    //Bitmap bitmap = new Bitmap(img);//, newSize);
                    //    //this.pbxPicture.Image = bitmap;
                    //}

                    ord.Stock.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    ord.Stock.StoneList = sDal.GetAllOrderStonesDetail(oNo);


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
            //if (records != null) records.TrimExcess();
            return ord;
        }
        public byte[] ImageRestore(string getImage)
        {
            byte[] tempImage = null;
            //string getImage = "SELECT Picture from Stock where StockId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getImage, con);
            con.Open();
            try
            {
                if (cmd.ExecuteScalar() == DBNull.Value)
                    return tempImage;
                else
                    tempImage = (byte[])cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return tempImage;
        }
        public OrderEstimat GetOrderByOrderNo(int orderNo)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select s.*,ci.* from OrderMaster s inner join CustomerInfo ci on ci.AccountCode=s.CustAccountNo where OrderNo=" + orderNo, con);
            cmd.CommandType = CommandType.Text;
            OrderEstimat ord = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    ord = new OrderEstimat();
                    if (dr["BillBookNo"] == DBNull.Value)
                        ord.BillBookNo = null;
                    else
                        ord.BillBookNo = dr["BillBookNo"].ToString();
                    //ord.SVNO = dr["VNO"].ToString();
                    ord.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    ord.ODate = Convert.ToDateTime(dr["ODate"]);
                    ord.DDate = Convert.ToDateTime(dr["DDate"]);
                    ord.CusAccountNo = dr["AccountCode"].ToString();
                    ord.CustName = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Address"].ToString());
                    ord.SalesMan = new SaleMan();
                    if (ord.SalesMan.ID == null || ord.SalesMan.ID == 0)
                    {
                        //ord.SalesMan.ID = 0;

                        //Start Qasim: Problm SaleManId Not Get Solution(02/07/2023)MM/DD/YYYY
                        ord.SalesMan.ID = 0;
                    }
                    else
                    {
                        //ord.SalesMan = new SaleMan(Convert.ToInt32(dr["Id"]), dr["Name"].ToString());
                        
                        ord.SalesMan = new SaleMan(Convert.ToInt32(dr["SaleManId"]));
                        //End
                    }
                    ord.TReceivedAmount = this.GetTReceivedAmount(orderNo);
                    foreach (string strTag in GetAllItemIdByOrderNo((int)ord.OrderNo))
                    {
                        ord.AddLineItems(GetComRecByOItemId(strTag));
                    }
                    if (dr["CashReceive"] == DBNull.Value)
                        ord.CashReceive = 0;
                    else
                        ord.CashReceive = Convert.ToInt32(dr["CashReceive"]);
                    if (dr["CashPayment"] == DBNull.Value)
                        ord.CashPayment = 0;
                    else
                        ord.CashPayment = Convert.ToInt32(dr["CashPayment"]);
                    if (dr["CCAmount"] == DBNull.Value)
                        ord.CreditCard = 0;
                    else
                        ord.CreditCard = Convert.ToInt32(dr["CCAmount"]);
                    if (dr["CAmount"] == DBNull.Value)
                        ord.CheckCash = 0;
                    else
                        ord.CheckCash = Convert.ToInt32(dr["CAmount"]);
                    if (dr["PGoldAmount"] == DBNull.Value)
                        ord.PureGoldCharges = 0;
                    else
                        ord.PureGoldCharges = Convert.ToInt32(dr["PGoldAmount"]);
                    if (dr["UGoldAmount"] == DBNull.Value)
                        ord.UsedGoldCharges = 0;
                    else
                        ord.UsedGoldCharges = Convert.ToInt32(dr["UGoldAmount"]);
                    if (dr["BillDiscount"] == DBNull.Value)
                        ord.BillDiscout = 0;
                    else
                        ord.BillDiscout = Convert.ToInt32(dr["BillDiscount"]);
                    if (dr["TotalPrice"] == DBNull.Value)
                        ord.TotalPrice = 0;
                    else
                        ord.TotalPrice = Convert.ToInt32(dr["TotalPrice"]);
                    if (dr["NetBill"] == DBNull.Value)
                        ord.NetBill = 0;
                    else
                        ord.NetBill = Convert.ToInt32(dr["NetBill"]);
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

            return ord;

        }
        public List<Stock> GetTagNoByItemIdForOrder(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id + " and Status='Available' and ItemFor='Order'";
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
        public decimal GetGoldSumByOrderNo(int id)
        {
            string getRecord = "select sum(OPGold) 'TGold' from OrderEstimate Where OrderNo=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            cmd.CommandType = CommandType.Text;
            decimal record = 0;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    record=new decimal();
                     record =Convert.ToDecimal(dr["TGold"]);
                       
                   

                  

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
            //if (records != null) records.TrimExcess();
            return record;


        }
        public decimal GetTReceivedAmount(int orderno)
        {
            string query = "Select TReceivedAmount from Sale where orderno =" + orderno;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal  orderNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["TReceivedamount"] == DBNull.Value)
                        orderNo = 0;
                    else
                        orderNo = Convert.ToDecimal(dr["TReceivedamount"]);
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
            return orderNo;

        }
        public void GetReceivedAmount(int orderno, out decimal cash, out decimal goldAmount)
        {
            string query = @"select case when (select sum(isnull(Amount,0)) from Sale_Payment where ONO=Sale.OrderNo) is null then 0 else (select sum(isnull(Amount,0)) from Sale_Payment where ONO=Sale.OrderNo and Description like 'Cash Received From Order No%') end 'Amount',
                            case when (select sum(isnull(Amount,0)) from GoldDetail where ONO=Sale.OrderNo) is null then 0 else (select sum(isnull(Amount,0)) from GoldDetail where ONO=Sale.OrderNo) end 'GoldAmount' from Sale where OrderNo=" + orderno;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Amount"] == DBNull.Value)
                        cash = 0;
                    else
                        cash = Convert.ToDecimal(dr["Amount"]);
                    if (dr["GoldAmount"] == DBNull.Value)
                        goldAmount = 0;
                    else
                        goldAmount = Convert.ToDecimal(dr["GoldAmount"]);
                }
                else
                {
                    cash = 0;
                    goldAmount = 0;
                }
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
                    dr.Close();
                    cmd.Dispose();
                }
            }

        }
        public void UpdateOrderStatus(string querry, SqlConnection con, SqlTransaction tran)//, float goldBalance)
        {

            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tran;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw ex;
            }
            finally
            {

            }
        }
        public void AddOrderItems(OrderLineItem oli)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddOrderItems", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OItemId", SqlDbType.NVarChar);
            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();

                cmd.Transaction = tran;

                try
                {
                    cmd.Parameters["@OrderNo"].Value = oli.Stock.OrderNo;
                    cmd.Parameters["@Date"].Value = DateTime.Now;
                    cmd.Parameters["@TagNo"].Value = oli.Stock.TagNo;
                    cmd.Parameters["@OItemId"].Value = oli.OItemId;

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
        public void AddOrderItems(OrderLineItem oli, string str, int ono, SqlConnection con, SqlTransaction tran)
        {
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);

            SqlCommand cmd = new SqlCommand("AddOrderItems", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@OItemId", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    cmd.Parameters["@OrderNo"].Value = ono;
                    cmd.Parameters["@Date"].Value = DateTime.Now;
                    cmd.Parameters["@TagNo"].Value = oli.Stock.TagNo;
                    cmd.Parameters["@OItemId"].Value = oli.OItemId;
                    cmd.Parameters["@Description"].Value = str;

                    cmd.ExecuteNonQuery();

                    //}                                                                                         
                    // tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }

            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //    con.Close();
            }

        }
        public void DeleteStonesByTagNo(string OitemId, SqlConnection con, SqlTransaction tran)
        {
            string querry = "delete from OrderStonesDetail where OitemId='" + OitemId + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
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
                //cmd.Dispose();
            }
        }
        public void DeleteOrderMaster(int OrderNo, SqlConnection con, SqlTransaction tran)
        {
            string querry = "delete from OrderMaster where OrderNo=" + OrderNo;
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
        }
        public void AddOrderPic(byte[] ImageMemory, string oItemID, int OrderNo)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic = new SqlCommand("AddPicturesForOrder", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;

            cmdpic.Parameters.Add(new SqlParameter("@OrderNo", OrderNo));
            cmdpic.Parameters.Add(new SqlParameter("@OItemId", oItemID));
            if (ImageMemory == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", ImageMemory));

            try
            {

                conpic.Open();

                cmdpic.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conpic.State == ConnectionState.Open)
                    conpic.Close();
            }
        }
    }
}
