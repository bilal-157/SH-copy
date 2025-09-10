using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;


namespace DAL
{
    public class WorkerDAL
    {
        private string AllWorkers = "select w.*,(select isnull(sum(OpeningGold),0) from ChildAccount where ChildCode=w.AccountCode)'OpeningGold',(select isnull(sum(OpeningCash),0) from ChildAccount where ChildCode=w.AccountCode)'openingcash',(((select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='GoldGiven' and WorkerId=w.WorkerId)+(select isnull(sum(OpeningGold),0) from ChildAccount where ChildCode=w.AccountCode)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='CashToGold' and GRStatus='Given' and WorkerId=w.WorkerId)) - ((select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='GoldReceive' and WorkerId=w.WorkerId)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where Status='CashToGold' and GRStatus='Receive' and WorkerId=w.WorkerId))) as GoldBalance,(((select isnull(sum(WeightCash),0) from WorkerGold_Trans where  Status='CashGiven' and WorkerId=w.WorkerId)+(select isnull(sum(OpeningCash),0) from ChildAccount where ChildCode=w.AccountCode)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where  Status='GoldToCash' and GRStatus='Given' and WorkerId=w.WorkerId)) - ((select isnull(sum(WeightCash),0) from WorkerGold_Trans where  Status='CashReceive' and WorkerId=w.WorkerId)+(select isnull(sum(PWeight),0) from WorkerGold_Trans where Status='GoldToCash' and GRStatus='Receive' and WorkerId=w.WorkerId))) as CashBalance from Worker w";
        StonesDAL sDAL = new StonesDAL();
        public void AddWorker(Worker worker)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddWorker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AccountCode", worker.AccountCode));
            cmd.Parameters.Add(new SqlParameter("@WorkerName", worker.Name));
            cmd.Parameters.Add(new SqlParameter("@Address", worker.Address));
            cmd.Parameters.Add(new SqlParameter("@TelNo", worker.ContactNo));
            cmd.Parameters.Add(new SqlParameter("@Refrence", worker.Email));
            cmd.Parameters.Add(new SqlParameter("@Making", worker.MakingTola));
            cmd.Parameters.Add(new SqlParameter("@Cheejad", worker.Cheejad));
            cmd.Parameters.Add(new SqlParameter("@TKarrat", worker.TKarrat)); ;

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
                }
            }
        }
        public void UpdateWorker(int id, Worker wrkr)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateWorker", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = id;
            //cmd.Parameters.Add(new SqlParameter("@AccountCode", wrkr.AccountCode));
            cmd.Parameters.Add(new SqlParameter("@WorkerName", wrkr.Name));
            cmd.Parameters.Add(new SqlParameter("@Address", wrkr.Address));
            cmd.Parameters.Add(new SqlParameter("@TelNo", wrkr.ContactNo));
            cmd.Parameters.Add(new SqlParameter("@Refrence", wrkr.Email));
            cmd.Parameters.Add(new SqlParameter("@Making", wrkr.MakingTola));
            cmd.Parameters.Add(new SqlParameter("@Cheejad", wrkr.Cheejad));
            cmd.Parameters.Add(new SqlParameter("@TKarrat", wrkr.TKarrat));
            cmd.Parameters.Add(new SqlParameter("@OpeningCash", wrkr.OpeningCash));
            cmd.Parameters.Add(new SqlParameter("@OpeningGold", wrkr.OpeningGold));
            cmd.Parameters.Add(new SqlParameter("@AccountCode", wrkr.AccountCode));
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
                }
            }
        }
        public List<Worker> GetAllWorkers()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(this.AllWorkers, con);
            cmd.CommandType = CommandType.Text;
            List<Worker> wrks = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    wrks = new List<Worker>();
                    do
                    {
                        Worker wrk = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                        wrk.Address = dr["Address"].ToString();
                        wrk.ContactNo = dr["TelNo"].ToString();
                        wrk.Email = dr["Refrence"].ToString();
                        wrk.MakingTola = Convert.ToDecimal(dr["Making"]);
                        wrk.AccountCode = dr["AccountCode"].ToString();
                        wrk.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                        wrk.TKarrat = Convert.ToDecimal(dr["TKarrat"]);
                        wrk.GoldBalance = Convert.ToDecimal(dr["GoldBalance"]);
                        wrk.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                        wrks.Add(wrk);
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
            if (wrks != null)
                wrks.TrimExcess();
            return wrks;
        }
        public Worker GetWorkerById(int id)
        {
            string selectsql = "select w.*,ch.OpeningCash,ch.OpeningGold from Worker w inner join ChildAccount ch on w.AccountCode=ch.ChildCode where WorkerId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.CommandType = CommandType.Text;
            Worker wrk = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (wrk == null) wrk = new Worker();

                    wrk.ID = Convert.ToInt32(dr["WorkerId"]);
                    wrk.AccountCode = dr["AccountCode"].ToString();
                    wrk.Name = dr["WorkerName"].ToString();
                    wrk.Address = dr["Address"].ToString();
                    wrk.ContactNo = dr["TelNo"].ToString();
                    wrk.Email = dr["Refrence"].ToString();
                    wrk.MakingTola = Convert.ToDecimal(dr["Making"]);
                    wrk.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                    wrk.TKarrat = Convert.ToDecimal(dr["TKarrat"]);
                    wrk.OpeningCash = Convert.ToDecimal(dr["OpeningCash"]);
                    wrk.OpeningGold = Convert.ToDecimal(dr["OpeningGold"]);
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

            return wrk;
        }
        public int GetMaxTranId()
        {
            string querry = "select max(TranId) as [px] from WorkerGold_Trans";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int t = 0;
            try
            {
                con.Open();
                SqlDataReader me = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (me.Read())
                {
                    if (me["px"] == DBNull.Value)
                        t = 0;
                    else
                        t = Convert.ToInt32(me["px"]);
                }

                con.Close();
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
            return t;

        }

        public void AddGoldTransaction(WorkerDealingMaster Wdm, SqlConnection con, SqlTransaction tran)
        {
            SqlCommand cmdSale = new SqlCommand("WorkerDealingMasterPrc", con);
            cmdSale.CommandType = CommandType.StoredProcedure;
            cmdSale.Parameters.Add(new SqlParameter("@BillNo", Wdm.BillBookNo));
            cmdSale.Parameters.Add(new SqlParameter("@Date", Wdm.Date));
            cmdSale.Parameters.Add(new SqlParameter("@ReceiveGold", Wdm.ReceiveTotalGold));
            cmdSale.Parameters.Add(new SqlParameter("@GivenGold", Wdm.GivenGold));
            cmdSale.Parameters.Add(new SqlParameter("@Balance", Wdm.Balance));
            cmdSale.Parameters.Add(new SqlParameter("@TotalPrice", Wdm.TotalPrice));
            cmdSale.Parameters.Add(new SqlParameter("@PreveiousGoldBalance", Wdm.PreveiousGoldBalance));



            SqlCommand cmd = new SqlCommand("AddWorkerDealing", con);

            cmd.Parameters.Add("@WItemId", SqlDbType.NVarChar);
            cmd.Parameters.Add("@BillNo", SqlDbType.Int);
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
            cmd.Parameters.Add("@EntryDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@WeightCash", SqlDbType.Float);
            cmd.Parameters.Add("@ToCashGold", SqlDbType.Float);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmd.Parameters.Add("@ItemId", SqlDbType.Int);
            cmd.Parameters.Add("@Ujrat", SqlDbType.Float);
            cmd.Parameters.Add("@Ujratgiven", SqlDbType.Float);
            cmd.Parameters.Add("@Tola", SqlDbType.Float);
            cmd.Parameters.Add("@Masha", SqlDbType.Float);
            cmd.Parameters.Add("@Ratti", SqlDbType.Float);
            cmd.Parameters.Add("@Kaat", SqlDbType.Float);
            cmd.Parameters.Add("@KaatWeight", SqlDbType.Float);
            cmd.Parameters.Add("@TotalWeight", SqlDbType.Float);
            cmd.Parameters.Add("@Purity", SqlDbType.Float);
            cmd.Parameters.Add("@GoldRate", SqlDbType.Float);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@GRStatus", SqlDbType.NVarChar);
            cmd.Parameters.Add("@Cheejad", SqlDbType.Float);
            cmd.Parameters.Add("@CheejadDecided", SqlDbType.Float);
            cmd.Parameters.Add("@PWeight", SqlDbType.Float);
            cmd.Parameters.Add("@PMaking", SqlDbType.Float);
            cmd.Parameters.Add("@Karat", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CashBalance", SqlDbType.Float);
            cmd.Parameters.Add("@WtWeight", SqlDbType.Float);
            cmd.Parameters.Add("@Piece", SqlDbType.Float);
            cmd.Parameters.Add("@PieceMaking", SqlDbType.Float);
            cmd.Parameters.Add("@GoldBalance", SqlDbType.Float);
            cmd.Parameters.Add("@PCheejad", SqlDbType.Float);
            cmd.Parameters.Add("@Qty", SqlDbType.Float);
            cmd.Parameters.Add("@MakingTola", SqlDbType.Float);
            cmd.Parameters.Add("@VNO", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CVNO", SqlDbType.NVarChar);


            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdStone = new SqlCommand("AddWorekerDealingsStones", con);
            cmdStone.Parameters.Add(new SqlParameter("@TranId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
            cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
            cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
            cmdStone.Parameters.Add(new SqlParameter("@WItemId", SqlDbType.NVarChar));
            cmdStone.CommandType = CommandType.StoredProcedure;
            try
            {
                cmdSale.Transaction = tran;
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmdSale.ExecuteNonQuery();

                try
                {
                    foreach (WorkerLineItem wli in Wdm.WorkerLineItem)
                    {
                        cmd.Parameters["@WItemId"].Value = wli.WorkerDealing.WItemId;
                        cmd.Parameters["@BillNo"].Value = wli.WorkerDealing.BillBookNo;
                        cmd.Parameters["@WorkerId"].Value = wli.WorkerDealing.Worker.ID;
                        cmd.Parameters["@EntryDate"].Value = wli.WorkerDealing.EntryDate;
                        cmd.Parameters["@WeightCash"].Value = wli.WorkerDealing.WeightCash;
                        cmd.Parameters["@ToCashGold"].Value = wli.WorkerDealing.ToCashGold;
                        cmd.Parameters["@Description"].Value = wli.WorkerDealing.Description;
                        cmd.Parameters["@ItemId"].Value = wli.WorkerDealing.items.ItemId;
                        cmd.Parameters["@Ujrat"].Value = wli.WorkerDealing.Ujrat;
                        cmd.Parameters["@Ujratgiven"].Value = wli.WorkerDealing.UjratGiven;
                        cmd.Parameters["@Tola"].Value = wli.WorkerDealing.Toola;
                        cmd.Parameters["@Masha"].Value = wli.WorkerDealing.Masha;
                        cmd.Parameters["@Ratti"].Value = wli.WorkerDealing.Ratti;
                        cmd.Parameters["@Kaat"].Value = wli.WorkerDealing.Kaat;
                        cmd.Parameters["@KaatWeight"].Value = wli.WorkerDealing.KaatInRatti;
                        cmd.Parameters["@TotalWeight"].Value = wli.WorkerDealing.TotalWeight;
                        cmd.Parameters["@Purity"].Value = wli.WorkerDealing.Purity;
                        cmd.Parameters["@GoldRate"].Value = wli.WorkerDealing.GoldRate;
                        cmd.Parameters["@Status"].Value = wli.WorkerDealing.Status;
                        cmd.Parameters["@GRStatus"].Value = wli.WorkerDealing.GRStatus;
                        cmd.Parameters["@Cheejad"].Value = wli.WorkerDealing.Cheejad;
                        cmd.Parameters["@CheejadDecided"].Value = wli.WorkerDealing.CheejadDecided;
                        cmd.Parameters["@PWeight"].Value = wli.WorkerDealing.PureWeight;
                        cmd.Parameters["@PMaking"].Value = wli.WorkerDealing.LabourMakingPerPiece;
                        cmd.Parameters["@Karat"].Value = wli.WorkerDealing.Karrat;
                        cmd.Parameters["@CashBalance"].Value = wli.WorkerDealing.CashBalance;
                        cmd.Parameters["@WtWeight"].Value = wli.WorkerDealing.WasteInRatti;
                        cmd.Parameters["@Piece"].Value = wli.WorkerDealing.Piece;
                        cmd.Parameters["@PieceMaking"].Value = wli.WorkerDealing.PieceMaking;
                        cmd.Parameters["@GoldBalance"].Value = wli.WorkerDealing.GoldBalance;
                        cmd.Parameters["@PCheejad"].Value = wli.WorkerDealing.PCheejad;
                        cmd.Parameters["@Qty"].Value = wli.WorkerDealing.Qty;
                        cmd.Parameters["@MakingTola"].Value = wli.WorkerDealing.MakingTola;
                        cmd.Parameters["@VNO"].Value = wli.WorkerDealing.WVNO;
                        cmd.Parameters["@CVNO"].Value = wli.WorkerDealing.CVNO;
                        cmd.ExecuteNonQuery();

                    }
                    foreach (WorkerLineItem wli in Wdm.WorkerLineItem)
                    {
                        if (wli.WorkerDealing.WorkerStonesList == null)
                        {
                        }
                        else
                        {
                            foreach (Stones stList in wli.WorkerDealing.WorkerStonesList)
                            {
                                cmdStone.Parameters["@TranId"].Value = wli.WorkerDealing.TransId;
                                cmdStone.Parameters["@WItemId"].Value = wli.WorkerDealing.WItemId;
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
        //public void AddGoldTransaction(WorkerDealingMaster wrkd, SqlConnection con, SqlTransaction trans)
        //{
        //    // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand("AddWorkerDealing", con);
        //    cmd.Transaction = trans;
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add(new SqlParameter("@WItemId", wrkd.WItemId));
        //    cmd.Parameters.Add(new SqlParameter("@BillNo", wrkd.BillBookNo));  
        //    cmd.Parameters.Add(new SqlParameter("@WorkerId", wrkd.Worker.ID));
        //    cmd.Parameters.Add(new SqlParameter("@EntryDate", wrkd.EntryDate));
        //    cmd.Parameters.Add(new SqlParameter("@WeightCash", wrkd.WeightCash));
        //    cmd.Parameters.Add(new SqlParameter("@ToCashGold", wrkd.ToCashGold));
        //    cmd.Parameters.Add(new SqlParameter("@Description", wrkd.Description));            
        //    cmd.Parameters.Add(new SqlParameter("@ItemId", wrkd.items.ItemId));                                  
        //    cmd.Parameters.Add(new SqlParameter("@Ujrat", wrkd.Ujrat));
        //    cmd.Parameters.Add(new SqlParameter("@Ujratgiven", wrkd.UjratGiven));
        //    cmd.Parameters.Add(new SqlParameter("@Tola", wrkd.Toola));
        //    cmd.Parameters.Add(new SqlParameter("@Masha", wrkd.Masha));
        //    cmd.Parameters.Add(new SqlParameter("@Ratti", wrkd.Ratti));
        //    cmd.Parameters.Add(new SqlParameter("@Kaat", wrkd.Kaat));
        //    cmd.Parameters.Add(new SqlParameter("@KaatWeight", wrkd.KaatInRatti));
        //    if (wrkd.TotalWeight.HasValue)
        //    {
        //        cmd.Parameters.Add(new SqlParameter("@TotalWeight", wrkd.TotalWeight));
        //    }
        //    else
        //    {
        //        wrkd.TotalWeight = 0;
        //        cmd.Parameters.Add(new SqlParameter("@TotalWeight", wrkd.TotalWeight));
        //    }
            
        //    if (wrkd.Purity.HasValue)
        //    {
        //        cmd.Parameters.Add(new SqlParameter("@Purity", wrkd.Purity));
        //    }
        //    else
        //    {
        //        wrkd.Purity = 0;
        //        cmd.Parameters.Add(new SqlParameter("@Purity", wrkd.Purity));
        //    }
        //    if (wrkd.GoldRate.HasValue)
        //        cmd.Parameters.Add(new SqlParameter("@GoldRate", wrkd.GoldRate));
        //    else
        //        cmd.Parameters.Add(new SqlParameter("@GoldRate", DBNull.Value));
          
           
        //    cmd.Parameters.Add(new SqlParameter("@Status", wrkd.Status));
        //    cmd.Parameters.Add(new SqlParameter("@GRStatus", wrkd.GRStatus));           
        //    cmd.Parameters.Add(new SqlParameter("@Cheejad", wrkd.Cheejad));
        //    cmd.Parameters.Add(new SqlParameter("@CheejadDecided", wrkd.CheejadDecided));
        //    cmd.Parameters.Add(new SqlParameter("@PWeight", wrkd.PureWeight));
        //    cmd.Parameters.Add(new SqlParameter("@PMaking", wrkd.LabourMakingPerPiece));
        //    cmd.Parameters.Add(new SqlParameter("@Karat", wrkd.Karrat));
        //    cmd.Parameters.Add(new SqlParameter("@CashBalance", wrkd.CashBalance));          
        //    cmd.Parameters.Add(new SqlParameter("@WtWeight", wrkd.WasteInRatti));
        //    cmd.Parameters.Add(new SqlParameter("@Piece", wrkd.Piece));
        //    cmd.Parameters.Add(new SqlParameter("@PieceMaking", wrkd.PieceMaking));
        //    cmd.Parameters.Add(new SqlParameter("@GoldBalance", wrkd.GoldBalance));
        //    cmd.Parameters.Add(new SqlParameter("@PCheejad", wrkd.PCheejad));
        //    cmd.Parameters.Add(new SqlParameter("@Qty", wrkd.Qty));
        //    cmd.Parameters.Add(new SqlParameter("@MakingTola", wrkd.MakingTola));            
        //    if (wrkd.WVNO != null)
        //        cmd.Parameters.Add(new SqlParameter("@VNO", wrkd.WVNO));
        //    else
        //    {
        //        cmd.Parameters.Add("@VNO", SqlDbType.NVarChar);
        //        cmd.Parameters["@VNO"].Value = DBNull.Value;
        //    }
        //    if (wrkd.CVNO != null)
        //        cmd.Parameters.Add(new SqlParameter("@CVNO", wrkd.CVNO));
        //    else
        //    {
        //        cmd.Parameters.Add("@CVNO", SqlDbType.DateTime);
        //        cmd.Parameters["@CVNO"].Value = DBNull.Value;
        //    }
        //    SqlCommand cmdStone = new SqlCommand("AddWorekerDealingsStones", con);
        //    cmdStone.Transaction = trans;
        //    cmdStone.Parameters.Add(new SqlParameter("@TranId", SqlDbType.Int));
        //    cmdStone.Parameters.Add(new SqlParameter("@StoneTypeId", SqlDbType.Int));
        //    cmdStone.Parameters.Add(new SqlParameter("@StoneId", SqlDbType.Int));
        //    cmdStone.Parameters.Add(new SqlParameter("@ColorName", SqlDbType.NVarChar));
        //    cmdStone.Parameters.Add(new SqlParameter("@CutName", SqlDbType.NVarChar));
        //    cmdStone.Parameters.Add(new SqlParameter("@ClearityName", SqlDbType.NVarChar));
        //    cmdStone.Parameters.Add(new SqlParameter("@StoneWeight", SqlDbType.Float));
        //    cmdStone.Parameters.Add(new SqlParameter("@SQty", SqlDbType.Int));
        //    cmdStone.Parameters.Add(new SqlParameter("@Rate", SqlDbType.Float));
        //    cmdStone.Parameters.Add(new SqlParameter("@Price", SqlDbType.Float));
        //    cmdStone.CommandType = CommandType.StoredProcedure;
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        try
        //        {
        //            if (wrkd.WorkerStonesList == null)
        //            {

        //            }
        //            else
        //            {

        //                foreach (Stones stList in wrkd.WorkerStonesList)
        //                {
        //                    cmdStone.Parameters["@TranId"].Value = wrkd.TransId;

        //                    cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
        //                    if (stList.StoneId.HasValue)
        //                        cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
        //                    //cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
        //                    else
        //                        cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
        //                    if (stList.ColorName == null)
        //                        cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
        //                    //cmdStone.Parameters["@ColorId"].Value = DBNull.Value;
        //                    else
        //                        // cmdStone.Parameters["@ColorId"].Value = stList .ColorName .ColorId ;
        //                        cmdStone.Parameters["@ColorName"].Value = stList.ColorName.ColorName;
        //                    if (stList.CutName == null)
        //                        //cmdStone.Parameters["@CutId"].Value = DBNull.Value;
        //                        cmdStone.Parameters["@CutName"].Value = DBNull.Value;
        //                    else
        //                        //cmdStone.Parameters["@CutId"].Value = stList .CutName .CutId ;
        //                        cmdStone.Parameters["@CutName"].Value = stList.CutName.CutName;
        //                    if (stList.ClearityName == null)
        //                        // cmdStone.Parameters["@ClearityId"].Value = DBNull.Value;
        //                        cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
        //                    else
        //                        //cmdStone.Parameters["@ClearityId"].Value = stList .ClearityName .ClearityId ;
        //                        cmdStone.Parameters["@ClearityName"].Value = stList.ClearityName.ClearityName;
        //                    if (stList.StoneWeight.HasValue)
        //                        cmdStone.Parameters["@StoneWeight"].Value = stList.StoneWeight;
        //                    else
        //                        cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
        //                    if (stList.Qty.HasValue)
        //                        cmdStone.Parameters["@SQty"].Value = stList.Qty;
        //                    else
        //                        cmdStone.Parameters["@SQty"].Value = DBNull.Value;
        //                    if (stList.Rate.HasValue)
        //                        cmdStone.Parameters["@Rate"].Value = stList.Rate;
        //                    else
        //                        cmdStone.Parameters["@Rate"].Value = DBNull.Value;
        //                    if (stList.Price.HasValue)
        //                        cmdStone.Parameters["@Price"].Value = stList.Price;
        //                    else
        //                        cmdStone.Parameters["@Price"].Value = DBNull.Value;

        //                    //cmdStone.Parameters["@GivenOrReceived"].Value = stList.GiveReceive;
        //                    cmdStone.ExecuteNonQuery();

        //                }
        //            }
        //            // tran.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            // trans.Rollback();

        //            throw ex;
        //        }
        //    }


        //    finally
        //    {
        //        //if (con.State == ConnectionState.Open)
        //        //{
        //        //    con.Close();
        //        //}
        //    }


        //}
        public List<WorkerDealing> GetWorkerDealingsById(int wrkid, DateTime dt)
        {
            // string query = "select * from WorkerGold_Trans where WorkerId =  " + wrkid + "  and convert(varchar, TDate, 112) = convert(varchar,'"+dt+"' , 112)";
            string query = "GetWokerDealingByDate";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wrkid;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dt;
            List<WorkerDealing> wkd = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    wkd = new List<WorkerDealing>();
                    if (wkd == null)
                        wkd = new List<WorkerDealing>();
                    do
                    {
                        WorkerDealing wd = new WorkerDealing();
                     
                            wd.EntryDate = Convert.ToDateTime(dr["EntryDate"]);


                        if (Convert.ToDecimal(dr["WeightCash"]) == 0)
                            wd.WeightCash = 0;
                        else
                            wd.WeightCash = Convert.ToDecimal(dr["WeightCash"]);
                      
                        wd.Description = dr["Description"].ToString();
                       // wd.CashGiven = Convert.ToDecimal(dr["CashGiven"]);
                        wd.Ujrat = Convert.ToDecimal(dr["Ujrat"]);
                        wd.UjratGiven = Convert.ToDecimal(dr["UjratGiven"]);
                        wd.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                        wd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                        wd.PureWeight = Convert.ToDecimal(dr["PWeight"]);
                        wd.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                        wd.TransId = Convert.ToInt32(dr["TranId"]);
                        wd.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                        wd.CheejadDecided = Convert.ToDecimal(dr["CheejadDecided"]);
                        wd.KaatInRatti = Convert.ToDecimal(dr["KaatWeight"]);
                        wd.WasteInRatti = Convert.ToDecimal(dr["WtWeight"]);
                        wd.Piece = Convert.ToDecimal(dr["Piece"]);
                        wd.PieceMaking = Convert.ToDecimal(dr["PieceMaking"]);
                        wd.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                        //wd.GivenWeight = Convert.ToDecimal(dr["GivenWeight"]);
                        if (dr["Purity"] == DBNull.Value)
                        {
                            wd.Purity = null;
                        }
                        else
                            wd.Purity = Convert.ToDecimal(dr["Purity"]);
                        wkd.Add(wd);
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
            return wkd;
        }
        public WorkerDealing GetRecByTranId(int id)
        {
            string query = "select * from WorkerGold_Trans where TranId = " + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            WorkerDealing wdt = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    wdt = new WorkerDealing();
                    if (dr["GDate"] == DBNull.Value)
                    {
                        wdt.GDate = null;
                    }
                    else
                        wdt.GDate = Convert.ToDateTime(dr["GDate"]);
                    if (dr["AddDate"] == DBNull.Value)
                    {
                        wdt.AddDate = null;
                    }
                    else
                        wdt.AddDate = Convert.ToDateTime(dr["AddDate"]);

                    if (Convert.ToDecimal(dr["GivenWeight"]) == 0)
                        wdt.GivenWeight = 0;
                    else
                        wdt.GivenWeight = Convert.ToDecimal(dr["GivenWeight"]);
                    if (Convert.ToDecimal(dr["ReceivedWeight"]) == 0)
                    {
                        wdt.ReceivedWeight = 0;

                    }
                    else
                        wdt.ReceivedWeight = Convert.ToDecimal(dr["ReceivedWeight"]);
                    wdt.Description = dr["Description"].ToString();
                    wdt.items = new Item();
                    wdt.items.ItemId = Convert.ToInt32(dr["ItemId"]);
                    wdt.CashGiven = Convert.ToDecimal(dr["CashGiven"]);
                    wdt.Ujrat = Convert.ToDecimal(dr["Ujrat"]);
                    wdt.UjratGiven = Convert.ToDecimal(dr["UjratGiven"]);
                    wdt.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    wdt.Kaat = Convert.ToDecimal(dr["Kaat"]);
                    wdt.PureWeight = Convert.ToDecimal(dr["PWeight"]);
                    wdt.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                    wdt.TransId = Convert.ToInt32(dr["TranId"]);
                    wdt.Toola = Convert.ToDecimal(dr["Tola"]);
                    wdt.Masha = Convert.ToDecimal(dr["Masha"]);
                    wdt.Ratti = Convert.ToDecimal(dr["Ratti"]);
                    wdt.Karrat = dr["Karat"].ToString();
                    wdt.KaatInRatti = Convert.ToDecimal(dr["KaatWeight"]);
                    wdt.WasteInRatti = Convert.ToDecimal(dr["WtWeight"]);
                    wdt.Piece = Convert.ToDecimal(dr["Piece"]);
                    wdt.PieceMaking = Convert.ToDecimal(dr["PieceMaking"]);
                    wdt.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    wdt.Purity = Convert.ToDecimal(dr["Purity"]);
                    wdt.PCheejad = Convert.ToDecimal(dr["PCheejad"]);
                    wdt.MakingTola = Convert.ToDecimal(dr["MakingTola"]);
                    if (dr["VNO"] != null)
                        wdt.WVNO = dr["VNO"].ToString();
                    if (dr["CVNO"] != null)
                        wdt.CVNO = dr["CVNO"].ToString();

                    wdt.WorkerStonesList = sDAL.GetAllStonesDetailforWorker(id);


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
            return wdt;
        }
        public void UpdateWorkerDealing(int id, WorkerDealing wrkd, SqlConnection con, SqlTransaction trans)
        {
            // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateWorkerDealings", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            cmd.Parameters.Add("@oldTranId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add(new SqlParameter("@WorkerId", wrkd.Worker.ID));
            cmd.Parameters.Add(new SqlParameter("@Description", wrkd.Description));
            cmd.Parameters.Add(new SqlParameter("@GivenWeight", wrkd.GivenWeight));
            cmd.Parameters.Add(new SqlParameter("@ItemId", wrkd.items.ItemId));
            cmd.Parameters.Add(new SqlParameter("@ReceivedWeight", wrkd.ReceivedWeight));
            if (wrkd.GDate.HasValue)
                cmd.Parameters.Add(new SqlParameter("@GDate", wrkd.GDate));
            else
            {
                cmd.Parameters.Add("@GDate", SqlDbType.DateTime);
                cmd.Parameters["@GDate"].Value = DBNull.Value;
            }
            cmd.Parameters.Add(new SqlParameter("@Ujrat", wrkd.Ujrat));
            cmd.Parameters.Add(new SqlParameter("@Ujratgiven", wrkd.UjratGiven));
            cmd.Parameters.Add(new SqlParameter("@Tola", wrkd.Toola));
            cmd.Parameters.Add(new SqlParameter("@Masha", wrkd.Masha));
            cmd.Parameters.Add(new SqlParameter("@Ratti", wrkd.Ratti));
            cmd.Parameters.Add(new SqlParameter("@Kaat", wrkd.Kaat));
            cmd.Parameters.Add(new SqlParameter("@KaatWeight", wrkd.KaatInRatti));
            cmd.Parameters.Add(new SqlParameter("@TotalWeight", wrkd.TotalWeight));
            if (wrkd.AddDate.HasValue)
                cmd.Parameters.Add(new SqlParameter("@AddDate", wrkd.AddDate));
            else
            {
                cmd.Parameters.Add("@AddDate", SqlDbType.DateTime);
                cmd.Parameters["@AddDate"].Value = DBNull.Value;
            }
            //cmd.Parameters.Add(new SqlParameter("@TranId", wrkd.TransId));
            cmd.Parameters.Add(new SqlParameter("@Cheejad", wrkd.Cheejad));
            cmd.Parameters.Add(new SqlParameter("@CheejadDecided", wrkd.CheejadDecided));
            cmd.Parameters.Add(new SqlParameter("@PWeight", wrkd.PureWeight));
            cmd.Parameters.Add(new SqlParameter("@PMaking", wrkd.LabourMakingPerPiece));
            cmd.Parameters.Add(new SqlParameter("@Karat", wrkd.Karrat));
            cmd.Parameters.Add(new SqlParameter("@CashBalance", wrkd.CashBalance));
            cmd.Parameters.Add(new SqlParameter("@CashGiven", wrkd.CashGiven));
            cmd.Parameters.Add(new SqlParameter("@WtWeight", wrkd.WasteInRatti));
            cmd.Parameters.Add(new SqlParameter("@Piece", wrkd.Piece));
            cmd.Parameters.Add(new SqlParameter("@PieceMaking", wrkd.PieceMaking));
            cmd.Parameters.Add(new SqlParameter("@GoldBalance", wrkd.GoldBalance));
            cmd.Parameters.Add(new SqlParameter("@TDate", wrkd.TDate));
            SqlCommand cmdStone = new SqlCommand("AddWorekerDealingsStones", con);
            cmdStone.Transaction = trans;
            cmdStone.Parameters.Add(new SqlParameter("@TranId", SqlDbType.Int));
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
                // con.Open();
                cmd.ExecuteNonQuery();
                try
                {
                    if (wrkd.WorkerStonesList == null)
                    {

                    }
                    else
                    {

                        foreach (Stones stList in wrkd.WorkerStonesList)
                        {
                            cmdStone.Parameters["@TranId"].Value = wrkd.TransId;

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

                            //cmdStone.Parameters["@GivenOrReceived"].Value = stList.GiveReceive;
                            cmdStone.ExecuteNonQuery();

                        }
                    }
                    // tran.Commit();
                }
                catch (Exception ex)
                {
                    //trans.Rollback();
                    throw ex;
                }

            }
            catch (Exception ex)
            {
                //trans.Rollback();
                throw ex;
            }

            finally
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    con.Close();
                //}
            }
        }
        public void DeleteTransactionByBillNo(int tid,SqlConnection con,SqlTransaction trans)
        {
            string query = "delete from WorkerGold_Trans where BillNo= " + tid + "delete from WorkerDealingMaster where BillNo=" + tid + "delete from vouchers where WBillNo="+tid;           
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = trans;
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
        public void AddGoldtoWorker(Gold gld, SqlConnection con, SqlTransaction trans)
        {
            string AddGold = "AddGivenGoldToWorker";
            // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AddGold, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            cmd.Parameters.Add(new SqlParameter("@VNO", gld.VNO));
            cmd.Parameters.Add(new SqlParameter("@GoldType", gld.GoldType));
            cmd.Parameters.Add(new SqlParameter("@Weight", gld.Weight));
            cmd.Parameters.Add(new SqlParameter("@Kaat", gld.Kaat));
            cmd.Parameters.Add(new SqlParameter("@RemainingWork", gld.RemainingWork));
            cmd.Parameters.Add(new SqlParameter("@Karrat", gld.Karat));
            cmd.Parameters.Add(new SqlParameter("@Description", gld.Description));
            cmd.Parameters.Add(new SqlParameter("@PWeight", gld.PWeight));
            cmd.Parameters.Add(new SqlParameter("@PGDate", gld.PGDate));
            cmd.Parameters.Add(new SqlParameter("@WorkerId", gld.WorkerId));
            cmd.Parameters.Add(new SqlParameter("@TranId", gld.TranId));
            try
            {
                // con.Open();
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
        public decimal GetGoldByTranId(int tranid)
        {
            string query = "select * from GoldDetail where TranId =" + tranid + "";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            decimal gd = 0;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    gd = Convert.ToDecimal(dr["Weight"]);
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
            return gd;
        }
        public void DelelteGoldByTranId(int tranid)
        {
            string query = "Delete from GoldDetail where TranId =" + tranid + "";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            // SqlDataReader dr = null;
            // decimal gd = 0;
            try
            {
                con.Open();
                // dr = cmd.ExecuteReader();
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
            // return gd;
        }
        public void DelelteGoldByTranId(int tranid, SqlConnection con, SqlTransaction trans)
        {
            string query = "Delete from GoldDetail where TranId =" + tranid + "";
            // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = trans;
            // SqlDataReader dr = null;
            // decimal gd = 0;
            try
            {
                // con.Open();
                // dr = cmd.ExecuteReader();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //  con.Close();
            }
            // return gd;
        }
        public bool IsWorkerNameExist(string name)
        {
            string querry = "select WorkerName from Worker where WorkerName='" + name + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

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
        public bool IsWorkerMobileExist(string mobile)
        {
            string querry = "select TelNo from Worker where telno<>'' and TelNo='" + mobile + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool cFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    cFlag = true;


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
            return cFlag;
        }
        public bool IsWorkerAddressExist(string address)
        {
            string querry = "select Address from Worker where Address='" + address + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool dFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    dFlag = true;


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
            return dFlag;
        }
        public void DeleteWorker(string name)
        {
            string query = "delete from Worker where AccountCode  = '" + name + "';delete from ChildAccount where ChildCode='"+name+"'";
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
        public bool IsWorkernameAnytableExist(int id)
        {
            string query = "select wfj.WorkerId,cost.WorkerId,repd.WorkerId,stk.WorkerId,ord.WorkerId from WorkerDealingFAJ wfj left outer join Costing cost on cost.WorkerId=wfj.WorkerId left outer join tblRepairDetail repd  on repd.WorkerId =wfj.WorkerId left outer join Stock stk  on stk.WorkerId=wfj.WorkerId left outer join OrderEstimate ord  on ord.WorkerId=wfj.WorkerId where  wfj.WorkerId ='" + id + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            bool dFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    dFlag = true;


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
            return dFlag;
        }
        public int GetMaxBillNo()
        {
            string query = "Select isnull(MAX(BillNo),0) as [maxBillNo] from WorkerDealingMaster ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            int billno = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["maxBillNo"] == DBNull.Value)
                        billno = 0;
                    else
                        billno = Convert.ToInt32(dr["maxBillNo"]);
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
            return billno;
        }
        public void WorkerDealingMaster(WorkerDealingMaster wd, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("WorkerDealingMasterPrc", con);
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@BillNo", wd.BillBookNo));
            if (wd.Date.HasValue)
                cmd.Parameters.Add(new SqlParameter("@Date", wd.Date));
            else
            {
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DBNull.Value;

            }
            if (wd.ReceiveTotalGold.HasValue)
                cmd.Parameters.Add(new SqlParameter("@ReceiveGold", wd.ReceiveTotalGold));
            else
                cmd.Parameters.Add(new SqlParameter("@ReceiveGold", DBNull.Value));

            if (wd.GivenGold.HasValue)
                cmd.Parameters.Add(new SqlParameter("@GivenGold", wd.GivenGold));
            else
                cmd.Parameters.Add(new SqlParameter("@GivenGold", DBNull.Value));

            if (wd.Balance.HasValue)
                cmd.Parameters.Add(new SqlParameter("@Balance", wd.Balance));
            else
                cmd.Parameters.Add(new SqlParameter("@Balance", DBNull.Value));

            if (wd.TotalPrice.HasValue)
                cmd.Parameters.Add(new SqlParameter("@TotalPrice", wd.TotalPrice));
            else
                cmd.Parameters.Add(new SqlParameter("@TotalPrice", DBNull.Value));

            if (wd.PreveiousGoldBalance.HasValue)
                cmd.Parameters.Add(new SqlParameter("@PreveiousGoldBalance", wd.PreveiousGoldBalance));
            else
                cmd.Parameters.Add(new SqlParameter("@PreveiousGoldBalance", DBNull.Value));
            cmd.ExecuteNonQuery();


        }
        public Worker GetWorkerbyID(int id)
        {
            string query = AllWorkers + " where WorkerId = " + id;

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            Worker wrk = null;
            try
            {
                con.Open();
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {



                    wrk = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    wrk.Address = dr["Address"].ToString();
                    wrk.ContactNo = dr["TelNo"].ToString();
                    wrk.Email = dr["Refrence"].ToString();
                    wrk.MakingTola = Convert.ToDecimal(dr["Making"]);
                    wrk.AccountCode = dr["AccountCode"].ToString();
                    wrk.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                    wrk.TPurity = Convert.ToDecimal(dr["TPurity"]);
                    wrk.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    wrk.GoldBalance = Convert.ToDecimal(dr["GoldBalance"]);

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
                {

                    con.Close();
                }
            }

            return wrk;
        }
        public void WorkerDealingMaster(WokerDealingMaster wd, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("WorkerDealingMasterPrc", con);
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@BillNo", wd.BillBookNo));
            if (wd.Date.HasValue)
                cmd.Parameters.Add(new SqlParameter("@Date", wd.Date));
            else
            {
                cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = DBNull.Value;

            }
            if (wd.ReceiveTotalGold.HasValue)
                cmd.Parameters.Add(new SqlParameter("@ReceiveGold", wd.ReceiveTotalGold));
            else
                cmd.Parameters.Add(new SqlParameter("@ReceiveGold", DBNull.Value));

            if (wd.GivenGold.HasValue)
                cmd.Parameters.Add(new SqlParameter("@GivenGold", wd.GivenGold));
            else
                cmd.Parameters.Add(new SqlParameter("@GivenGold", DBNull.Value));

            if (wd.Balance.HasValue)
                cmd.Parameters.Add(new SqlParameter("@Balance", wd.Balance));
            else
                cmd.Parameters.Add(new SqlParameter("@Balance", DBNull.Value));

            if (wd.TotalPrice.HasValue)
                cmd.Parameters.Add(new SqlParameter("@TotalPrice", wd.TotalPrice));
            else
                cmd.Parameters.Add(new SqlParameter("@TotalPrice", DBNull.Value));

            if (wd.PreveiousGoldBalance.HasValue)
                cmd.Parameters.Add(new SqlParameter("@PreveiousGoldBalance", wd.PreveiousGoldBalance));
            else
                cmd.Parameters.Add(new SqlParameter("@PreveiousGoldBalance", DBNull.Value));
            cmd.ExecuteNonQuery();


        }
        public int GetW0rkerId(string query)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            int orderNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["WorkerId"] == DBNull.Value)
                        orderNo = 0;
                    else
                        orderNo = Convert.ToInt32(dr["WorkerId"]);
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
        public WorkerDealingMaster GetWorkerdealingByBillNo(int jpno)
        {
            //string constr = "select lsd.*,st.Name,sn.StoneName from LooseStones ls left outer join LooseStonesDetail lsd on lsd.PNo=ls.PNo left outer join StonesType st on st.StoneTypeId=lsd.StoneTypeId left outer join StonesName sn on sn.StoneId=lsd.StoneId where lsd.pno=" + jpno;
            string constr = "select wgt.*,it.ItemName from WorkerGold_Trans wgt left outer join item it on it.ItemId=wgt.ItemId where wgt.BillNo=" + jpno;

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(constr, con);
            List<WorkerDealing> lPli = null;
            WorkerDealingMaster wdm = new WorkerDealingMaster();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    lPli = new List<WorkerDealing>();
                    if (lPli == null) lPli = new List<WorkerDealing>();
                    do
                    {
                       // wdm.WorkerLineItem = new List<WorkerDealing>();
                        WorkerLineItem wli = new WorkerLineItem();
                        wli.WorkerDealing.Worker = new Worker();
                       wli.WorkerDealing.Worker.ID = Convert.ToInt32(dr["WorkerId"].ToString());
                       wli.WorkerDealing.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
                       wli.WorkerDealing.WeightCash = Convert.ToDecimal(dr["WeightCash"]);
                       wli.WorkerDealing.ToCashGold = Convert.ToDecimal(dr["ToCashGold"]);
                       wli.WorkerDealing.GRStatus = (dr["GRStatus"]).ToString();
                       wli.WorkerDealing.Status = (dr["Status"]).ToString();
                       wli.WorkerDealing.Description = (dr["Description"]).ToString();
                       wli.WorkerDealing.Ujrat = Convert.ToDecimal(dr["Ujrat"]);
                       wli.WorkerDealing.UjratGiven = Convert.ToDecimal(dr["UjratGiven"]);
                       wli.WorkerDealing.Qty = Convert.ToDecimal(dr["Qty"]);
                       wli.WorkerDealing.Kaat = Convert.ToDecimal(dr["Kaat"]);
                       wli.WorkerDealing.PureWeight = Convert.ToDecimal(dr["PWeight"]);
                       wli.WorkerDealing.Purity = Convert.ToDecimal(dr["Purity"]);
                       wli.WorkerDealing.Karrat = (dr["Karat"]).ToString();
                       wli.WorkerDealing.Purity = Convert.ToDecimal(dr["PMaking"]);
                      // wli.WorkerDealing.WtWeight = Convert.ToDecimal(dr["WtWeight"]);
                      // wli.WorkerDealing.KaatWeight = Convert.ToDecimal(dr["KaatWeight"]);
                       wli.WorkerDealing.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                       wli.WorkerDealing.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                       wli.WorkerDealing.CheejadDecided = Convert.ToDecimal(dr["CheejadDecided"]);
                       wli.WorkerDealing.Piece = Convert.ToDecimal(dr["Piece"]);
                       wli.WorkerDealing.PieceMaking = Convert.ToDecimal(dr["PieceMaking"]);
                       wli.WorkerDealing.TransId = Convert.ToInt32(dr["TranId"]);
                       wli.WorkerDealing.PCheejad = Convert.ToDecimal(dr["PCheejad"]);
                       wli.WorkerDealing.MakingTola = Convert.ToDecimal(dr["MakingTola"]);
                       wli.WorkerDealing.GoldRate = Convert.ToDecimal(dr["GoldRate"]);
                       wli.WorkerDealing.items = new Item();
                       wli.WorkerDealing.items.ItemId = Convert.ToInt32(dr["ItemId"]);
                       wli.WorkerDealing.items.ItemName = (dr["ItemName"]).ToString();
                       wli.WorkerDealing.WItemId = (dr["WItemId"]).ToString();
                       wli.WorkerDealing.BillBookNo = Convert.ToInt32(dr["BillNo"]);
                       wdm.AddLineItems(wli);
                        //lPli.Add(wrk);
                    }
                    while (dr.Read());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return wdm;
        }
        //public List<WorkerLineItem> GetWorkerdealingByBillNoLineItem(int jpno)
        //{
        //    //string constr = "select lsd.*,st.Name,sn.StoneName from LooseStones ls left outer join LooseStonesDetail lsd on lsd.PNo=ls.PNo left outer join StonesType st on st.StoneTypeId=lsd.StoneTypeId left outer join StonesName sn on sn.StoneId=lsd.StoneId where lsd.pno=" + jpno;
        //    string constr = "select wgt.*,it.ItemName from WorkerGold_Trans wgt left outer join item it on it.ItemId=wgt.ItemId where wgt.BillNo=" + jpno;

        //    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand(constr, con);
        //    List<WorkerDealingMaster> lPli = null;
        //    WorkerDealingMaster wdm = new WorkerDealingMaster();
        //    try
        //    {
        //        con.Open();
        //        SqlDataReader dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            lPli = new List<WorkerDealingMaster>();
        //            if (lPli == null) lPli = new List<WorkerDealingMaster>();
        //            do
        //            {

        //                WorkerDealing wrk = new WorkerDealing();
        //                wrk.Worker = new Worker();
        //                wrk.Worker.ID = Convert.ToInt32(dr["WorkerId"].ToString());
        //                wrk.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
        //                wrk.WeightCash = Convert.ToDecimal(dr["WeightCash"]);
        //                wrk.ToCashGold = Convert.ToDecimal(dr["ToCashGold"]);  
        //                wrk.GRStatus = (dr["GRStatus"]).ToString();
        //                wrk.Status = (dr["Status"]).ToString();
        //                wrk.Description = (dr["Description"]).ToString();
        //                wrk.Ujrat = Convert.ToDecimal(dr["Ujrat"]);
        //                wrk.UjratGiven = Convert.ToDecimal(dr["UjratGiven"]);
        //                wrk.Qty = Convert.ToDecimal(dr["Qty"]);
        //                wrk.Kaat = Convert.ToDecimal(dr["Kaat"]);
        //                wrk.PureWeight = Convert.ToDecimal(dr["PWeight"]);
        //                wrk.Purity = Convert.ToDecimal(dr["Purity"]);
        //                wrk.Karrat = (dr["Karat"]).ToString();
        //                wrk.Purity = Convert.ToDecimal(dr["PMaking"]);
        //                // wrk.WtWeight = Convert.ToDecimal(dr["WtWeight"]);
        //                //wrk.KaatWeight = Convert.ToDecimal(dr["KaatWeight"]);
        //                wrk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
        //                wrk.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
        //                wrk.CheejadDecided = Convert.ToDecimal(dr["CheejadDecided"]);
        //                wrk.Piece = Convert.ToDecimal(dr["Piece"]);
        //                wrk.PieceMaking = Convert.ToDecimal(dr["PieceMaking"]);
        //                wrk.TransId = Convert.ToInt32(dr["TranId"]);
        //                wrk.PCheejad = Convert.ToDecimal(dr["PCheejad"]);
        //                wrk.MakingTola = Convert.ToDecimal(dr["MakingTola"]);
        //                wrk.GoldRate = Convert.ToDecimal(dr["GoldRate"]);
        //                wrk.items = new Item();
        //                wrk.items.ItemId = Convert.ToInt32(dr["ItemId"]);
        //                wrk.items.ItemName = (dr["ItemName"]).ToString();
        //                wrk.WItemId = (dr["WItemId"]).ToString();
        //                wrk.BillBookNo = Convert.ToInt32(dr["BillNo"]);
        //                //wrk.ad
        //               //lPli.addl
                       
        //            }
        //            while (dr.Read());
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open) con.Close();
        //    }
        //    return lPli;
        //}
        public WorkerDealingMaster GetWorkerDealingByBillNo(int BillNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from WorkerDealingMaster where BillNo=" + BillNo, con);
            cmd.CommandType = CommandType.Text;
            WorkerDealingMaster wrk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    wrk=new WorkerDealingMaster ();                    
                    wrk.BillBookNo = Convert.ToInt32(dr["BillNo"]);
                    wrk.Date = Convert.ToDateTime(dr["Date"]);
                    wrk.ReceiveTotalGold = Convert.ToDecimal(dr["ReceiveGold"]);
                    wrk.GivenGold = Convert.ToDecimal(dr["Givengold"]);
                    wrk.Balance = Convert.ToDecimal(dr["Givengold"]);
                    wrk.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                    wrk.PreveiousGoldBalance = Convert.ToDecimal(dr["PreveiousGoldBalance"]);

                    foreach (int strTag in GetAllTranId("select tranid from WorkerGold_trans Where BillNo=" + wrk.BillBookNo))
                    {
                        wrk.AddLineItems(GetWorkerDetailByBillNo(strTag));
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

            return wrk;

        }
        public List<int> GetAllTranId(string query)
        {
            string getRecord = query;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            cmd.CommandType = CommandType.Text;
            List<int> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (records == null) records = new List<int>();

                    do
                    {
                        int str =Convert.ToInt32( dr["TranId"]);
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
        public WorkerLineItem GetWorkerDetailByBillNo(int BillNo)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select w.*,it.itemname,(select accountcode from worker where workerid=w.workerid)'AccountCode' from workergold_trans w left outer join item it on it.itemid=w.itemid where w.tranid="+BillNo+"", con);
            cmd.CommandType = CommandType.Text;

            WorkerLineItem wli = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    wli = new WorkerLineItem();            
                    wli.WorkerDealing = new WorkerDealing();
                    wli.WorkerDealing.Worker = new Worker();
                    wli.WorkerDealing.Worker.ID = Convert.ToInt32(dr["WorkerId"].ToString());
                    wli.WorkerDealing.Worker.AccountCode = (dr["AccountCode"].ToString());
                    wli.WorkerDealing.EntryDate = Convert.ToDateTime(dr["EntryDate"]);
                    wli.WorkerDealing.WeightCash = Convert.ToDecimal(dr["WeightCash"]);
                    wli.WorkerDealing.ToCashGold = Convert.ToDecimal(dr["ToCashGold"]);
                    wli.WorkerDealing.GRStatus = (dr["GRStatus"]).ToString();
                    wli.WorkerDealing.Status = (dr["Status"]).ToString();
                    wli.WorkerDealing.Description = (dr["Description"]).ToString();
                    wli.WorkerDealing.Ujrat = Convert.ToDecimal(dr["Ujrat"]);
                    wli.WorkerDealing.UjratGiven = Convert.ToDecimal(dr["UjratGiven"]);
                    wli.WorkerDealing.Qty = Convert.ToDecimal(dr["Qty"]);
                    wli.WorkerDealing.Kaat = Convert.ToDecimal(dr["Kaat"]);
                    wli.WorkerDealing.PureWeight = Convert.ToDecimal(dr["PWeight"]);
                    wli.WorkerDealing.Purity = Convert.ToDecimal(dr["Purity"]);
                    wli.WorkerDealing.Karrat = (dr["Karat"]).ToString();
                    wli.WorkerDealing.Purity = Convert.ToDecimal(dr["PMaking"]);
                    wli.WorkerDealing.WasteInRatti = Convert.ToDecimal(dr["WtWeight"]);
                    wli.WorkerDealing.KaatInRatti = Convert.ToDecimal(dr["KaatWeight"]);
                    wli.WorkerDealing.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                    wli.WorkerDealing.Cheejad = Convert.ToDecimal(dr["Cheejad"]);
                    wli.WorkerDealing.CheejadDecided = Convert.ToDecimal(dr["CheejadDecided"]);
                    wli.WorkerDealing.Piece = Convert.ToDecimal(dr["Piece"]);
                    wli.WorkerDealing.PieceMaking = Convert.ToDecimal(dr["PieceMaking"]);
                    wli.WorkerDealing.TransId = Convert.ToInt32(dr["TranId"]);
                    wli.WorkerDealing.PCheejad = Convert.ToDecimal(dr["PCheejad"]);
                    wli.WorkerDealing.MakingTola = Convert.ToDecimal(dr["MakingTola"]);
                    wli.WorkerDealing.GoldRate = Convert.ToDecimal(dr["GoldRate"]);
                    wli.WorkerDealing.items = new Item();
                    wli.WorkerDealing.items.ItemId = Convert.ToInt32(dr["ItemId"]);
                    wli.WorkerDealing.items.ItemName = (dr["ItemName"]).ToString();
                    wli.WorkerDealing.WItemId = (dr["WItemId"]).ToString();
                    wli.WorkerDealing.BillBookNo = Convert.ToInt32(dr["BillNo"]);
                    wli.WorkerDealing.CVNO = (dr["CVNO"]).ToString() ;
                    wli.WorkerDealing.WVNO = (dr["VNo"]).ToString() ;  
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

            return wli;
        }

    }
}
    
