using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;
using BusinesEntities;
namespace DAL
{
    public class StockDAL
    {

        //public DataRow[] select(int id);
        //DataSet myDataSet;
        private string AllKarat = "select * from Karat";

        public int GetMaxIndexNo()
        {
            string querry = "Select MAX(IndexNo) as [MaxIndex] from Stock";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int indexNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["MaxIndex"] == DBNull.Value)
                        indexNo = 0;
                    else
                        indexNo = Convert.ToInt32(dr["MaxIndex"]);
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
            return indexNo;
        }

        public void AddStock(Stock stock)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddStock", con);

            // SqlCommand cmdStone = new SqlCommand("Insert_StonesDetail", con);
            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdStone.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@ItemId", stock.ItemName.ItemId));
            cmd.Parameters.Add(new SqlParameter("@IndexNo", this.GetMaxIndexNo() + 1));
            cmd.Parameters.Add(new SqlParameter("@TagNo", stock.TagNo));
            cmd.Parameters.Add(new SqlParameter("@BarCode", stock.BarCode));
            cmd.Parameters.Add(new SqlParameter("@IType", stock.ItemType.ToString()));
            cmd.Parameters.Add(new SqlParameter("@ItemFor", stock.ItemFor.ToString()));
            cmd.Parameters.Add(new SqlParameter("@OrderNo", stock.OrderNo));
            if (stock.ItemFor.ToString() == "Order")
                cmd.Parameters.Add(new SqlParameter("@OItemId", stock.OItemId));

            cmd.Parameters.Add("@SubGItmId", SqlDbType.Int);
            cmd.Parameters["@SubGItmId"].Value = DBNull.Value;            
            cmd.Parameters.Add("@SubItemId", SqlDbType.Int);
            cmd.Parameters["@SubItemId"].Value = DBNull.Value;
           
            cmd.Parameters.Add(new SqlParameter("@BQuantity", stock.BQuantity));
            cmd.Parameters.Add(new SqlParameter("@BWeight", stock.BWeight));
            cmd.Parameters.Add(new SqlParameter("@Bstatus", stock.BStatus));
            cmd.Parameters.Add(new SqlParameter("@DesNo", stock.DesNo));
            cmd.Parameters.Add(new SqlParameter("@NetWeight", stock.NetWeight));
            cmd.Parameters.Add(new SqlParameter("@RateA", stock.Silver.RateA));
            cmd.Parameters.Add(new SqlParameter("@PriceA", stock.Silver.PriceA));
            cmd.Parameters.Add(new SqlParameter("@RateD", stock.Silver.RateD));
            cmd.Parameters.Add(new SqlParameter("@PriceD", stock.Silver.PriceD));
            cmd.Parameters.Add(new SqlParameter("@SilverSalePrice", stock.Silver.SalePrice));
            cmd.Parameters.Add(new SqlParameter("@TotalWeight", stock.TotalWeight));
            cmd.Parameters.Add(new SqlParameter("@ItemSize", stock.ItemSize));
            cmd.Parameters.Add(new SqlParameter("@Qty", stock.Qty));
            cmd.Parameters.Add(new SqlParameter("@StQty", stock.Qty));
            cmd.Parameters.Add(new SqlParameter("@Pieces", stock.Pieces));
            cmd.Parameters.Add(new SqlParameter("@Karat", stock.Karrat));
            cmd.Parameters.Add(new SqlParameter("@Description", stock.Description));
            cmd.Parameters.Add(new SqlParameter("@StockDate", stock.StockDate));

            if (stock.WorkerName != null)
                cmd.Parameters.Add(new SqlParameter("@WorkerId", stock.WorkerName.ID));
            else
            {
                cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
                cmd.Parameters["@WorkerId"].Value = DBNull.Value;
            }

            cmd.Parameters.Add(new SqlParameter("@WasteInGm", stock.WasteInGm));
            cmd.Parameters.Add(new SqlParameter("@WastePercent", stock.WastePercent));
            cmd.Parameters.Add(new SqlParameter("@Kaat", stock.KaatInRatti));
            cmd.Parameters.Add(new SqlParameter("@PWeight", stock.PWeight));
            cmd.Parameters.Add(new SqlParameter("@LakerGm", stock.LakerGm));
            cmd.Parameters.Add(new SqlParameter("@TotalLaker", stock.TotalLaker));
            cmd.Parameters.Add(new SqlParameter("@MakingPerGm", stock.MakingPerGm));
            cmd.Parameters.Add(new SqlParameter("@TotalMaking", stock.TotalMaking));
            cmd.Parameters.Add(new SqlParameter("@WTola", stock.WTola));
            cmd.Parameters.Add(new SqlParameter("@WMasha", stock.WMasha));
            cmd.Parameters.Add(new SqlParameter("@WRatti", stock.WRatti));
            cmd.Parameters.Add(new SqlParameter("@PTola", stock.PTola));
            cmd.Parameters.Add(new SqlParameter("@PMasha", stock.PMasha));
            cmd.Parameters.Add(new SqlParameter("@PRatti", stock.PRatti));
            cmd.Parameters.Add(new SqlParameter("@TTola", stock.TTola));
            cmd.Parameters.Add(new SqlParameter("@TMasha", stock.TMasha));
            cmd.Parameters.Add(new SqlParameter("@TRatti", stock.TRatti));

            if (stock.DesignNo != null)
                cmd.Parameters.Add(new SqlParameter("@DesignId", stock.DesignNo.DesignId));
            else
            {
                cmd.Parameters.Add("@DesignId", SqlDbType.Int);
                cmd.Parameters["@DesignId"].Value = DBNull.Value;
            }
            cmd.Parameters.Add(new SqlParameter("@MakingType", stock.MakingType));
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";
            cmd.Parameters.Add(new SqlParameter("@ItemCost", stock.ItemCost));
            cmd.Parameters.Add(new SqlParameter("@SalePrice", stock.SalePrice));
            cmd.Parameters.Add(new SqlParameter("@PurchaseRate", stock.PurchaseRate));
            cmd.Parameters.Add(new SqlParameter("@UserId", DateDAL.userId));

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

            con.Open();

            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmd.ExecuteNonQuery();

                try
                {
                    if (stock.StoneList == null)
                    {
                    }
                    else
                    {
                        foreach (Stones stList in stock.StoneList)
                        {
                            cmdStone.Parameters["@TagNo"].Value = stock.TagNo.ToString();

                            cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                            if (stList.StoneId.HasValue)
                                cmdStone.Parameters["@StoneId"].Value = stList.StoneId;
                            else
                                cmdStone.Parameters["@StoneName"].Value = DBNull.Value;
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
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void AddWtLineItem(Stock stock)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdWt = new SqlCommand("AddWeight", con);
            cmdWt.CommandType = CommandType.StoredProcedure;

            cmdWt.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
            cmdWt.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.Int));
            cmdWt.Parameters.Add(new SqlParameter("@SubItemId", SqlDbType.Int));
            cmdWt.Parameters.Add(new SqlParameter("@Weight", SqlDbType.Float));
            con.Open();

            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmdWt.Transaction = tran;
                try
                {
                    foreach (WeightLineItem wli in stock.WeightLineItem)
                    {
                        cmdWt.Parameters["@TagNo"].Value = stock.TagNo;
                        cmdWt.Parameters["@ItemId"].Value = stock.ItemName.ItemId;
                        cmdWt.Parameters["@SubItemId"].Value = wli.SID;
                        cmdWt.Parameters["@Weight"].Value = wli.Weight;
                        cmdWt.ExecuteNonQuery();
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
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public void AddPics(JewelPictures jp)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic = new SqlCommand("AddPictures", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;

            cmdpic.Parameters.Add(new SqlParameter("@TagNo", jp.TagNo));
            if (jp.ImageMemory == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", jp.ImageMemory));

            if (jp.ImageMemory1 == null)
            {
                cmdpic.Parameters.Add("@Picture1", SqlDbType.Image);
                cmdpic.Parameters["@Picture1"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture1", jp.ImageMemory1));

            if (jp.ImageMemorySmall == null)
            {
                cmdpic.Parameters.Add("@SmallPicture", SqlDbType.Image);
                cmdpic.Parameters["@SmallPicture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@SmallPicture", jp.ImageMemorySmall));

            if (jp.ImageMemory2 == null)
            {
                cmdpic.Parameters.Add("@Picture2", SqlDbType.Image);
                cmdpic.Parameters["@Picture2"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture2", jp.ImageMemory2));
            if (jp.ImageMemory3 == null)
            {
                cmdpic.Parameters.Add("@Picture3", SqlDbType.Image);
                cmdpic.Parameters["@Picture3"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture3", jp.ImageMemory3));
            if (jp.ImageMemory4 == null)
            {
                cmdpic.Parameters.Add("@Picture4", SqlDbType.Image);
                cmdpic.Parameters["@Picture4"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture4", jp.ImageMemory4));
            if (jp.ImageMemory5 == null)
            {
                cmdpic.Parameters.Add("@Picture5", SqlDbType.Image);
                cmdpic.Parameters["@Picture5"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture5", jp.ImageMemory5));
            if (jp.ImageMemory6 == null)
            {
                cmdpic.Parameters.Add("@Picture6", SqlDbType.Image);
                cmdpic.Parameters["@Picture6"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture6", jp.ImageMemory6));
            if (jp.ImageMemory7 == null)
            {
                cmdpic.Parameters.Add("@Picture7", SqlDbType.Image);
                cmdpic.Parameters["@Picture7"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture7", jp.ImageMemory7));
            if (jp.ImageMemory8 == null)
            {
                cmdpic.Parameters.Add("@Picture8", SqlDbType.Image);
                cmdpic.Parameters["@Picture8"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture8", jp.ImageMemory8));



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

        public void UpdatePics(string TagNo, JewelPictures jp)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic = new SqlCommand("UpdatePictures", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;

            //cmdpic.Parameters.Add(new SqlParameter("@TagNo", jp.TagNo));
            cmdpic.Parameters.Add("@oldTagNo", SqlDbType.NVarChar).Value = TagNo;
            if (jp.ImageMemory == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", jp.ImageMemory));

            if (jp.ImageMemory1 == null)
            {
                cmdpic.Parameters.Add("@Picture1", SqlDbType.Image);
                cmdpic.Parameters["@Picture1"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture1", jp.ImageMemory1));


            if (jp.ImageMemorySmall == null)
            {
                cmdpic.Parameters.Add("@SmallPicture", SqlDbType.Image);
                cmdpic.Parameters["@SmallPicture"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@SmallPicture", jp.ImageMemorySmall));

            if (jp.ImageMemory2 == null)
            {
                cmdpic.Parameters.Add("@Picture2", SqlDbType.Image);
                cmdpic.Parameters["@Picture2"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture2", jp.ImageMemory2));
            if (jp.ImageMemory3 == null)
            {
                cmdpic.Parameters.Add("@Picture3", SqlDbType.Image);
                cmdpic.Parameters["@Picture3"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture3", jp.ImageMemory3));
            if (jp.ImageMemory4 == null)
            {
                cmdpic.Parameters.Add("@Picture4", SqlDbType.Image);
                cmdpic.Parameters["@Picture4"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture4", jp.ImageMemory4));
            if (jp.ImageMemory5 == null)
            {
                cmdpic.Parameters.Add("@Picture5", SqlDbType.Image);
                cmdpic.Parameters["@Picture5"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture5", jp.ImageMemory5));
            if (jp.ImageMemory6 == null)
            {
                cmdpic.Parameters.Add("@Picture6", SqlDbType.Image);
                cmdpic.Parameters["@Picture6"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture6", jp.ImageMemory6));
            if (jp.ImageMemory7 == null)
            {
                cmdpic.Parameters.Add("@Picture7", SqlDbType.Image);
                cmdpic.Parameters["@Picture7"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture7", jp.ImageMemory7));
            if (jp.ImageMemory8 == null)
            {
                cmdpic.Parameters.Add("@Picture8", SqlDbType.Image);
                cmdpic.Parameters["@Picture8"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@Picture8", jp.ImageMemory8));



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

        //public string GenrateTagNo(int id)
        //{
        //    int i = 0;
        //    string selectItemName = "Select stk.ItemId ,((Select Abrivation from Item Where ItemId = stk.ItemId) + Cast(MAX(Cast(dbo.udf_GetNumeric(TagNo) as int)) as nvarchar))'TagNo',(Select Abrivation from Item Where ItemId = stk.ItemId)'Abrivation' from Stock stk  " +
        //                               "Where stk.ItemId =" + id + " group by stk.ItemId";
        //    string selectAbri = "select Abrivation from Item where ItemId= " + id;

        //    string abrivation = "";
        //    string TagNo;
        //    // int i = 0;

        //    string tag;
        //    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand(selectItemName, con);
        //    SqlCommand cmdAbri = new SqlCommand(selectAbri, con);
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        con.Open();
        //        dr = cmd.ExecuteReader();

        //        if (dr.HasRows == true)
        //        {
        //            if (dr.Read())
        //            {
        //                tag = dr["TagNo"].ToString();
        //                abrivation = dr["Abrivation"].ToString();
        //                i = Convert.ToInt32(tag.Remove(0, abrivation.Length)) + 1;
        //            }
        //        }
        //        else
        //        {
        //            dr.Close();

        //            dr = cmdAbri.ExecuteReader();
        //            if (dr.Read())
        //            {
        //                abrivation = dr["Abrivation"].ToString();
        //                i = i + 1;
        //            }
        //        }
        //        dr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        con.Close();
        //    }

        //    tag = string.Format("{0:00000}", i);
        //    TagNo = abrivation.ToString() + tag.ToString();
        //    return TagNo;
        //}
        public string GenrateTagNo(int id, string str)
        {
            int i = 0;

            //ADORecordSetHelper Rstemp = new ADORecordSetHelper("");
            string selectItemName = "Select stk.ItemId ,stk.TagNo,(Select Abrivation from Item Where ItemId = stk.ItemId)'Abrivation' from Stock stk  " +
                                       "Where stk.ItemId =" + id;
            // string selectItemName = "SelectMaxStockId";
            //  string selectAbri = "select Abrivation from Item where ItemId= " + id;

            string abrivation = "";
            string TagNo;
            int no = 0;


            string tag;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectItemName, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            // SqlCommand cmdAbri = new SqlCommand(selectAbri, con);
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    if (dr.Read())
                    {
                        do
                        {
                            tag = dr["TagNo"].ToString();
                            abrivation = dr["Abrivation"].ToString();
                            if (no < Convert.ToInt32(tag.Remove(0, abrivation.Length)))
                                no = Convert.ToInt32(tag.Remove(0, abrivation.Length));
                        }
                        while (dr.Read());
                        no += 1;

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
                con.Close();
            }
            if (no > 9999)
                tag = string.Format("{0:00000}", no);
            else
                if (abrivation == "")
                {
                    tag = str + "00001";
                }
                else
                {
                    tag = string.Format("{0:00000}", no);
                }
            //  tag=
            TagNo = abrivation.ToString() + tag.ToString();
            return TagNo;

            //Rstemp.Close();
            //a = StringsHelper.Format(a, "0000");
            //return Abrivaton + a;


        }

        public void DeleteRecord(int id, Stock stk)
        {
            string
               deleteRecord = "Delete from Stock where StockId=" + stk.StockId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteRecord, con);
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

        #region StockSearch

        public List<Stock> GetRecordsByItemId(int id)
        {
            string selectRecord = "select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where Status = 'Available' and ItemId=" + id+"order by tagno";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
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
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                        stk.Karrat = dr["Karat"].ToString();
                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

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

        public List<Stock> GetRecordsByWeight(string selectRecord)
        {
            //string selectRecord = "select StockId,TagNo,ItemId,NetWeight,StockDate from Stock where NetWeight BETWEEN" + wt1 + "and" + wt2 +"and Status = 'Available' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@Weightf", SqlDbType.Int).Value = wt1;
            //cmd.Parameters.Add("@Weightt", SqlDbType.Int).Value = wt2;
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
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                        stk.Karrat = dr["Karat"].ToString();

                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

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

        public List<Stock> GetRecordsByDate(string selectRecord)
        {
            //string selectRecord = "select StockId,TagNo,ItemId,NetWeight,StockDate from Stock where NetWeight BETWEEN" + wt1 + "and" + wt2 +"and Status = 'Available' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@Datef", SqlDbType.DateTime).Value = dt1;
            //cmd.Parameters.Add("@Datet", SqlDbType.DateTime).Value = dt2;
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
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                        stk.Karrat = dr["Karat"].ToString();

                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

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

        public List<Stock> GetRecordsByKarat(string karat)
        {
            string selectRecord = "select StockId,TagNo,ItemId,NetWeight,StockDate,Karat from Stock where Karat ='" + karat + "'and Status = 'Available' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;

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
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                        stk.Karrat = dr["Karat"].ToString();

                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

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

        public Stock GetRecordByTagNo(string tagno)
        {
            string selectRecord = "select StockId,TagNo,NetWeight,ItemId,StockDate,Karat from Stock where Status = 'Available' and TagNo ='"+tagno+"'";
            //string selectRecord = "select StockId,TagNo,NetWeight,ItemId,StockDate,Karat from Stock where TagNo="+tagno;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            Stock stk = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    stk = new Stock();
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();

                    if (dr["NetWeight"] == DBNull.Value)
                        stk.NetWeight = null;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                    stk.Karrat = dr["Karat"].ToString();

                    stk.StockDate = Convert.ToDateTime(dr["StockDate"]);


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
            return stk;

        }
        public List<Stock> GetRecordByDesNo(string DesNo)
        {
            string selectRecord = "select StockId,TagNo,NetWeight,ItemId,StockDate,Karat from Stock where Status = 'Available' and DesNo ='" + DesNo + "'";
            //string selectRecord = "select StockId,TagNo,NetWeight,ItemId,StockDate,Karat from Stock where TagNo="+tagno;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            List<Stock> stk = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stk = new List<Stock>();
                    if (stk == null) stk = new List<Stock>();
                    do{
                        Stock stck = new Stock();
                        stck.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stck.StockId = Convert.ToInt32(dr["StockId"]);
                        stck.TagNo = dr["TagNo"].ToString();

                    if (dr["NetWeight"] == DBNull.Value)
                        stck.NetWeight = null;
                    else
                        stck.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                    stck.Karrat = dr["Karat"].ToString();

                    stck.StockDate = Convert.ToDateTime(dr["StockDate"]);
                    stk.Add(stck);
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
            //if (records != null) records.TrimExcess();
            return stk;

        }

        #endregion

        public List<Stock> GetSaleTagNoByItemId(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "GetSaleTagById";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

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

        public List<Stock> GetOrderTagNoByOrderNo(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "select * from stock where OrderNo=" + id + "and Status='Available'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;           
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
        public List<Stock> GetAllTagNoByItemId(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "GetAllTagById";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

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

        public List<Stock> GetAllTagNosByItemId(string selectRecord)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id;


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

        public List<Stock> GetTagNoByItemIdForOrder(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id + " and Status='Available' and ItemFor='Sale'";
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

        public List<Stock> GetAllSampleTagNoByItemId(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id;
            string selectRecord = "GetAllSampleTagById";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Sample";

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

        public Stock GetStockBySockIdForStock(int stkId)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecByStockId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StockId", SqlDbType.Int).Value = stkId;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["BStatus"].ToString() == "Bulk")
                        stk = null;
                    else
                    {
                        stk = new Stock();
                        ItemType it;
                        if ((dr["IType"]).ToString() == "Gold")
                            it = ItemType.Gold;
                        else if ((dr["IType"]).ToString() == "Diamond")
                            it = ItemType.Diamond;
                        else if ((dr["IType"]).ToString() == "Silver")
                            it = ItemType.Silver;
                        else if ((dr["IType"]).ToString() == "Pladium")
                            it = ItemType.Pladium;
                        else
                            it = ItemType.Platinum;
                        stk.ItemType = it;
                        stk.BStatus = dr["BStatus"].ToString();
                        stk.Karrat = dr["Karat"].ToString();
                        stk.BQuantity = Convert.ToInt32(dr["BQuantity"]);
                        stk.BWeight = Convert.ToDecimal(dr["BWeight"]);
                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);
                        try
                        {
                            stk.SaleQty = Convert.ToInt32(dr["SaleQty"]);
                            stk.SaleWeight = Convert.ToDecimal(dr["SaleWeight"]);
                        }
                        catch
                        {

                        }
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                        if (dr["SaleDate"] == DBNull.Value)
                            stk.SaleDate = null;
                        else
                            stk.SaleDate = Convert.ToDateTime(dr["SaleDate"]);


                        if (dr["SaleNo"] == DBNull.Value)
                            stk.SaleNo = 0;
                        else
                            stk.SaleNo = Convert.ToInt32(dr["SaleNo"]);

                        if (dr["OrderNo"] == DBNull.Value)
                            stk.OrderNo = 0;
                        else
                            stk.OrderNo = Convert.ToInt32(dr["OrderNo"]);

                        if (dr["DesNo"] == DBNull.Value)
                            stk.DesNo = "";
                        else
                            try
                            {
                                stk.CustomerName = dr["Name"].ToString();
                                stk.BillBookNo = dr["BillBookNo"].ToString();
                            }
                            catch
                            {


                            }

                        if (dr["DesignId"] == DBNull.Value)
                        {
                            stk.DesignNo.DesignId = 0;
                            stk.DesignNo.DesignId = -1;
                        }
                        else
                            stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                        stk.ItemSize = dr["ItemSize"].ToString();
                        stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

                        if (dr["Qty"] == DBNull.Value)
                            stk.Qty = null;
                        else
                            stk.Qty = Convert.ToInt32(dr["Qty"]);

                        if (dr["Pieces"] == DBNull.Value)
                            stk.Pieces = null;
                        else
                            stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                        stk.Karrat = dr["Karat"].ToString();
                        stk.Description = dr["Description"].ToString();

                        if (dr["WorkerId"] == DBNull.Value)
                        {
                            stk.WorkerName = new Worker();
                            stk.WorkerName.ID = null;
                        }
                        else
                            stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                        if (dr["WasteInGm"] == DBNull.Value)
                            stk.WasteInGm = null;
                        else
                            stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                        if (dr["WastePercent"] == DBNull.Value)
                            stk.WastePercent = null;
                        else
                            stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                        if (dr["Kaat"] == DBNull.Value)
                            stk.KaatInRatti = null;
                        else
                            stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                        if (dr["PWeight"] == DBNull.Value)
                            stk.PWeight = null;
                        else
                            stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                        if (dr["LakerGm"] == DBNull.Value)
                            stk.LakerGm = null;
                        else
                            stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                        if (dr["TotalLaker"] == DBNull.Value)
                            stk.TotalLaker = null;
                        else
                            stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                        if (dr["TotalWeight"] == DBNull.Value)
                            stk.TotalWeight = 0;
                        else
                            stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                        if (dr["MakingPerGm"] == DBNull.Value)
                            stk.MakingPerGm = null;
                        else
                            stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                        if (dr["TotalMaking"] == DBNull.Value)
                            stk.TotalMaking = null;
                        else
                            stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);
                        stk.Silver = new Silver();
                        if (dr["RateA"] == DBNull.Value)
                            stk.Silver.RateA = null;
                        else
                            stk.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                        if (dr["RateD"] == DBNull.Value)
                            stk.Silver.RateD = null;
                        else
                            stk.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                        if (dr["PriceD"] == DBNull.Value)
                            stk.Silver.PriceD = null;
                        else
                            stk.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);

                        if (dr["PriceA"] == DBNull.Value)
                            stk.Silver.PriceA = null;
                        else
                            stk.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                        if (dr["SilverSalePrice"] == DBNull.Value)
                            stk.Silver.SalePrice = null;
                        else
                            stk.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                        if (dr["DesNo"] == DBNull.Value)
                            stk.DesNo = null;
                        else
                            stk.DesNo = dr["DesNo"].ToString();

                        if (dr["MakingType"] == DBNull.Value)
                            stk.MakingType = null;
                        else
                            stk.MakingType = dr["MakingType"].ToString();

                        if (dr["ItemCost"] == DBNull.Value)
                            stk.ItemCost = null;
                        else
                            stk.ItemCost = Convert.ToDecimal(dr["ItemCost"]);

                        if (dr["SalePrice"] == DBNull.Value)
                            stk.SalePrice = null;
                        else
                            stk.SalePrice = Convert.ToDecimal(dr["SalePrice"]);

                        if (dr["PurchaseRate"] == DBNull.Value)
                            stk.PurchaseRate = null;
                        else
                            stk.PurchaseRate = Convert.ToDecimal(dr["PurchaseRate"]);

                        try
                        {
                            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                            stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                        }
                        catch (Exception ex)
                        {
                            stk.ImageMemory = null;
                        }

                        stk.StoneList = new List<Stones>();
                        StonesDAL sDal = new StonesDAL();
                        stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
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
            return stk;
        }

        public Stock GetStockBySockId(int stkId)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecByStockId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StockId", SqlDbType.Int).Value = stkId;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stk = new Stock();
                    ItemType it;
                    if ((dr["IType"]).ToString() == "Gold")
                        it = ItemType.Gold;
                    else if ((dr["IType"]).ToString() == "Diamond")
                        it = ItemType.Diamond;
                    else if ((dr["IType"]).ToString() == "Silver")
                        it = ItemType.Silver;
                    else if ((dr["IType"]).ToString() == "Pladium")
                        it = ItemType.Pladium;
                    else
                        it = ItemType.Platinum;
                    stk.ItemType = it;
                    stk.BStatus = dr["BStatus"].ToString();
                    stk.Karrat = dr["Karat"].ToString();
                    stk.BQuantity = Convert.ToInt32(dr["BQuantity"]);
                    stk.BWeight = Convert.ToDecimal(dr["BWeight"]);
                    stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

                    try
                    {
                        stk.SaleQty = Convert.ToInt32(dr["SaleQty"]);
                        stk.SaleWeight = Convert.ToDecimal(dr["SaleWeight"]);
                    }
                    catch
                    {

                    }
                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["NetWeight"] == DBNull.Value)
                        stk.NetWeight = null;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                    if (dr["SaleDate"] == DBNull.Value)
                        stk.SaleDate = null;
                    else
                        stk.SaleDate = Convert.ToDateTime(dr["SaleDate"]);


                    if (dr["SaleNo"] == DBNull.Value)
                        stk.SaleNo = 0;
                    else
                        stk.SaleNo = Convert.ToInt32(dr["SaleNo"]);

                    if (dr["OrderNo"] == DBNull.Value)
                        stk.OrderNo = 0;
                    else
                        stk.OrderNo = Convert.ToInt32(dr["OrderNo"]);

                    if (dr["DesNo"] == DBNull.Value)
                        stk.DesNo = "";
                    else
                        try
                        {
                            stk.CustomerName = dr["Name"].ToString();
                            stk.BillBookNo = dr["BillBookNo"].ToString();
                        }
                        catch
                        {


                        }

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        stk.DesignNo.DesignId = 0;
                        stk.DesignNo.DesignId = -1;
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    stk.ItemSize = dr["ItemSize"].ToString();
                    stk.StockDate = Convert.ToDateTime(dr["StockDate"]);

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = null;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                    stk.Karrat = dr["Karat"].ToString();
                    stk.Description = dr["Description"].ToString();

                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = null;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                    if (dr["WasteInGm"] == DBNull.Value)
                        stk.WasteInGm = null;
                    else
                        stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        stk.WastePercent = null;
                    else
                        stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Kaat"] == DBNull.Value)
                        stk.KaatInRatti = null;
                    else
                        stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                    if (dr["PWeight"] == DBNull.Value)
                        stk.PWeight = null;
                    else
                        stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                    if (dr["LakerGm"] == DBNull.Value)
                        stk.LakerGm = null;
                    else
                        stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        stk.TotalLaker = null;
                    else
                        stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                    if (dr["TotalWeight"] == DBNull.Value)
                        stk.TotalWeight = 0;
                    else
                        stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                    if (dr["MakingPerGm"] == DBNull.Value)
                        stk.MakingPerGm = null;
                    else
                        stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        stk.TotalMaking = null;
                    else
                        stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);


                    stk.Silver = new Silver();
                    if (dr["RateA"] == DBNull.Value)
                    {
                        stk.Silver.RateA = null;

                    }
                    else
                        stk.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                    if (dr["RateD"] == DBNull.Value)
                    {
                        stk.Silver.RateD = null;
                    }
                    else
                        stk.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                    if (dr["PriceD"] == DBNull.Value)
                    {
                        stk.Silver.PriceD = null;
                    }
                    else
                        stk.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);

                    if (dr["PriceA"] == DBNull.Value)
                    {
                        stk.Silver.PriceA = null;
                    }
                    else
                        stk.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                    if (dr["SilverSalePrice"] == DBNull.Value)
                    {
                        stk.Silver.SalePrice = null;
                    }
                    else
                        stk.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                    if (dr["DesNo"] == DBNull.Value)
                    {
                        stk.DesNo = null;
                    }
                    else
                        stk.DesNo = dr["DesNo"].ToString();

                    if (dr["MakingType"] == DBNull.Value)
                        stk.MakingType = null;
                    else
                        stk.MakingType = dr["MakingType"].ToString();

                    if (dr["ItemCost"] == DBNull.Value)
                        stk.ItemCost = null;
                    else
                        stk.ItemCost = Convert.ToDecimal(dr["ItemCost"]);

                    if (dr["SalePrice"] == DBNull.Value)
                        stk.SalePrice = null;
                    else
                        stk.SalePrice = Convert.ToDecimal(dr["SalePrice"]);

                    if (dr["PurchaseRate"] == DBNull.Value)
                        stk.PurchaseRate = null;
                    else
                        stk.PurchaseRate = Convert.ToDecimal(dr["PurchaseRate"]);

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        stk.ImageMemory = null;
                    }

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
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

            return stk;
        }

        public Stock GetSilverStockBySockId(int stkId)
        {
            //string GetAllStock = "select * from Stock where StockId =" +stkId ;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetSilverRecByStockId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StockId", SqlDbType.Int).Value = stkId;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    stk = new Stock();
                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    stk.BStatus = dr["BStatus"].ToString();
                    //stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = null;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                    stk.Description = dr["Description"].ToString();
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = null;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());


                    stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                    stk.Silver = new Silver();
                    if (dr["RateA"] == DBNull.Value)
                        stk.Silver.RateA = null;
                    else
                        stk.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                    if (dr["PriceA"] == DBNull.Value)
                        stk.Silver.PriceA = null;
                    else
                        stk.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                    if (dr["RateD"] == DBNull.Value)
                        stk.Silver.RateD = null;
                    else
                        stk.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                    if (dr["PriceD"] == DBNull.Value)
                        stk.Silver.PriceD = null;
                    else
                        stk.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);
                    if (dr["SilverSalePrice"] == DBNull.Value)
                        stk.Silver.SalePrice = 0;
                    else
                        stk.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        stk.DesignNo = new Design();
                        stk.DesignNo.DesignId = null;
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        stk.ImageMemory = null;
                    }

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);

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

            return stk;

        }

        public Stock GetSilverStockByTag(string tagNo)
        {
            //string GetAllStock = "select * from Stock where StockId =" +stkId ;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetSilverRecByStockTagNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = tagNo;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stk = new Stock();
                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    //stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = null;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                    if (dr["OrderNo"] == DBNull.Value)
                        stk.OrderNo = 0;
                    else
                        stk.OrderNo = Convert.ToInt32(dr["OrderNo"]);

                    stk.Description = dr["Description"].ToString();
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = null;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    
                    stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);
                    stk.Silver = new Silver();
                    if (dr["RateA"] == DBNull.Value)
                        stk.Silver.RateA = null;
                    else
                        stk.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                    if (dr["PriceA"] == DBNull.Value)
                        stk.Silver.PriceA = null;
                    else
                        stk.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                    if (dr["RateD"] == DBNull.Value)
                        stk.Silver.RateD = null;
                    else
                        stk.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                    if (dr["PriceD"] == DBNull.Value)
                        stk.Silver.PriceD = null;
                    else
                        stk.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);
                    if (dr["SilverSalePrice"] == DBNull.Value)
                        stk.Silver.SalePrice = null;
                    else
                        stk.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        stk.DesignNo = new Design();
                        stk.DesignNo.DesignId = null;
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        stk.ImageMemory = null;
                    }

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
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

            return stk;

        }

        public Stock GetStockBySockTagNo(string tagNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecByStockTagNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = tagNo;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    stk = new Stock();


                    ItemType it;
                    if (dr["IType"].ToString() == "Gold")
                        it = ItemType.Gold;
                    else if (dr["IType"].ToString() == "Diamond")
                        it = ItemType.Diamond;
                    else if (dr["IType"].ToString() == "Pladium")
                        it = ItemType.Pladium;
                    else if (dr["IType"].ToString() == "Platinum")
                        it = ItemType.Platinum;
                    else
                        it = ItemType.Silver;
                    stk.ItemType = it;

                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    stk.BarCode  = dr["BarCode"].ToString();
                    stk.BStatus = dr["BStatus"].ToString();
                    stk.BQuantity = Convert.ToInt32(dr["BQuantity"]);
                    stk.BWeight = Convert.ToDecimal(dr["BWeight"]);
                    try
                    {
                        stk.SaleQty = Convert.ToInt32(dr["SaleQty"]);
                        stk.SaleWeight = Convert.ToDecimal(dr["SaleWeight"]);
                    }
                    catch
                    {
                    }
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["NetWeight"] == DBNull.Value)
                        stk.NetWeight = null;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                    if (dr["OrderNo"] == DBNull.Value)
                        stk.OrderNo = 0;
                    else
                        stk.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                   
                    stk.ItemSize = dr["ItemSize"].ToString();
                    stk.DesNo = dr["DesNo"].ToString();

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = null;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);                   

                    stk.Karrat = dr["Karat"].ToString();
                    stk.Description = dr["Description"].ToString();
                   
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = 0;
                        stk.WorkerName.Name = "";
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                    if (dr["WasteInGm"] == DBNull.Value)
                        stk.WasteInGm = 0;
                    else
                        stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        stk.WastePercent = null;
                    else
                        stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Kaat"] == DBNull.Value)
                        stk.KaatInRatti = null;
                    else
                        stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                    if (dr["PWeight"] == DBNull.Value)
                        stk.PWeight = null;
                    else
                        stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                    if (dr["LakerGm"] == DBNull.Value)
                        stk.LakerGm = null;
                    else
                        stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        stk.TotalLaker = null;
                    else
                        stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);
                    if (dr["TotalPrice"] == DBNull.Value)
                        stk.TotalPrice = 0;
                    else
                    stk.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                    stk.TotalWeight = (decimal)stk.NetWeight + (decimal)stk.WasteInGm;

                    if (dr["MakingPerGm"] == DBNull.Value)
                        stk.MakingPerGm = null;
                    else
                        stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        stk.TotalMaking = null;
                    else
                        stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);
                    if (dr["GoldRate"] == DBNull.Value)
                        stk.RatePerGm = 0;
                    else
                        stk.RatePerGm = Convert.ToDecimal(dr["GoldRate"].ToString());

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        stk.DesignNo = new Design();
                        stk.DesignNo.DesignId = null;
                        stk.DesignNo.DesignNo = "0";
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    if (dr["MakingType"] == DBNull.Value)
                        stk.MakingType = null;
                    else
                        stk.MakingType = dr["MakingType"].ToString();

                    if (dr["ItemCost"] == DBNull.Value)
                        stk.ItemCost = null;
                    else
                        stk.ItemCost = Convert.ToDecimal(dr["ItemCost"]);

                    if (dr["SalePrice"] == DBNull.Value)
                        stk.SalePrice = null;
                    else
                        stk.SalePrice = Convert.ToDecimal(dr["SalePrice"]);
                    stk.Silver = new Silver();
                    //if (dr["RateA"] == DBNull.Value)
                    //    stk.Silver.RateA = null;
                    //else
                    //    stk.Silver.RateA = Convert.ToDecimal(dr["RateA"]);
                    //if (dr["PriceA"] == DBNull.Value)
                    //    stk.Silver.PriceA = null;
                    //else
                    //    stk.Silver.PriceA = Convert.ToDecimal(dr["PriceA"]);
                    //if (dr["RateD"] == DBNull.Value)
                    //    stk.Silver.RateD = null;
                    //else
                    //    stk.Silver.RateD = Convert.ToDecimal(dr["RateD"]);
                    //if (dr["PriceD"] == DBNull.Value)
                    //    stk.Silver.PriceD = null;
                    //else
                    //    stk.Silver.PriceD = Convert.ToDecimal(dr["PriceD"]);
                    if (dr["SilverSalePrice"] == DBNull.Value)
                        stk.Silver.SalePrice = null;
                    else
                        stk.Silver.SalePrice = Convert.ToDecimal(dr["SilverSalePrice"]);

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        stk.ImageMemory = null;
                    }

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);

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

            return stk;

        }

        public Stock GetStkByStkTagStA(string tagNo)
        {
            //string GetAllStock = "select * from Stock where StockId =" +stkId ;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecByStockTagNoStA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar).Value = tagNo;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    stk = new Stock();


                    ItemType it;
                    if (dr["IType"].ToString() == "Gold")
                        it = ItemType.Gold;
                    else if (dr["IType"].ToString() == "Diamond")
                        it = ItemType.Diamond;
                    else
                        it = ItemType.Silver;
                    stk.ItemType = it;

                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    //stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                    if (dr["NetWeight"] == DBNull.Value)
                        stk.NetWeight = null;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                    stk.ItemSize = dr["ItemSize"].ToString();

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = null;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);
                    stk.StockDate = Convert.ToDateTime(dr["StockDate"]);
                    stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);
                    stk.Karrat = dr["Karat"].ToString();
                    stk.Description = dr["Description"].ToString();
;
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        Worker wrk = new Worker();
                        wrk.ID = null;
                        stk.WorkerName = wrk;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                    if (dr["WasteInGm"] == DBNull.Value)
                        stk.WasteInGm = null;
                    else
                        stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        stk.WastePercent = null;
                    else
                        stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Kaat"] == DBNull.Value)
                        stk.KaatInRatti = null;
                    else
                        stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                    if (dr["PWeight"] == DBNull.Value)
                        stk.PWeight = null;
                    else
                        stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                    if (dr["LakerGm"] == DBNull.Value)
                        stk.LakerGm = null;
                    else
                        stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        stk.TotalLaker = null;
                    else
                        stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);


                    stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                    if (dr["MakingPerGm"] == DBNull.Value)
                        stk.MakingPerGm = null;
                    else
                        stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        stk.TotalMaking = null;
                    else
                        stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);
                    
                    if (dr["GoldRate"] == DBNull.Value)
                        stk.RatePerGm = 0;
                    else
                        stk.RatePerGm = Convert.ToDecimal(dr["GoldRate"].ToString());

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        Design d = new Design();
                        d.DesignId = null;
                        stk.DesignNo = d;
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                    if (dr["MakingType"] == DBNull.Value)
                        stk.MakingType = null;
                    else
                        stk.MakingType = dr["MakingType"].ToString();

                    if (dr["ItemCost"] == DBNull.Value)
                        stk.ItemCost = null;
                    else
                        stk.ItemCost = Convert.ToDecimal(dr["ItemCost"]);

                    if (dr["SalePrice"] == DBNull.Value)
                        stk.SalePrice = null;
                    else
                        stk.SalePrice = Convert.ToDecimal(dr["SalePrice"]);

                    try
                    {
                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT SmallPicture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + stk.TagNo + "'");
                    }
                    catch (Exception ex)
                    {
                        stk.ImageMemory = null;
                    }

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
                    //  stk.StockDate = Convert.ToDateTime(dr["StockDate"]);
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

            return stk;

        }
        public StartUpp GetStartUp()
        {            
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from StartUp", con);
            cmd.CommandType = CommandType.Text;
            StartUpp stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    stk = new StartUpp();
                    stk.FixRateStatus = dr["FixRateStatus"].ToString();
                    stk.GoldRateType = dr["GoldRateType"].ToString();
                    stk.GoldRateGram = Convert.ToDecimal(dr["GoldRateGram"]);
                    stk.BackUpPath = dr["BackUpPath"].ToString();
                    stk.JewlManagerType = dr["JewlManagerType"].ToString();
                    if (dr["ReportPassword"] == DBNull.Value)
                        stk.ReportPassword = "";
                    else
                        stk.ReportPassword = dr["ReportPassword"].ToString();
                    if (dr["SalaryCalculation"] == DBNull.Value)
                        stk.SalaryCalculation = "";
                    else
                        stk.SalaryCalculation = dr["SalaryCalculation"].ToString();
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

            return stk;

        }

        public void UpdateForSoldTag(int i, string val)
        {
            string cmd = "UpdateForSale";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdselect = new SqlCommand(cmd, con);
            cmdselect.CommandType = CommandType.StoredProcedure;

            cmdselect.Parameters.Add("@StockId", SqlDbType.Int).Value = i;
            cmdselect.Parameters.Add("@Status", SqlDbType.NVarChar).Value = val;
            try
            {
                con.Open();
                cmdselect.ExecuteNonQuery();
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

        public void UpdateStock(string TagNo, Stock stock)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateStock", con);
            // SqlCommand cmdStone = new SqlCommand("Insert_StonesDetail", con);
            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmdStone.CommandType = CommandType.StoredProcedure;

            // cmd.Parameters.Add(new SqlParameter("@BarCode",stock.BarCode));
            //stock.ItemName = new Item();
            cmd.Parameters.Add("@OldTagNo", SqlDbType.NVarChar).Value = TagNo;
            cmd.Parameters.Add(new SqlParameter("@ItemId", stock.ItemName.ItemId));

            cmd.Parameters.Add(new SqlParameter("@TagNo", stock.TagNo));
            cmd.Parameters.Add(new SqlParameter("BarCode", stock.BarCode));
            cmd.Parameters.Add(new SqlParameter("@CostFlag", stock.costFlag));
            cmd.Parameters.Add(new SqlParameter("@DesNo", stock.DesNo));
            string str1 = "";
            if (stock.ItemFor == ItemFor.Sale)
            {
                str1 = "Sale";
            }
            else if (stock.ItemFor == ItemFor.Order)
            {
                str1 = "Order";
            }
            cmd.Parameters.Add(new SqlParameter("@ItemFor", str1));
            string str = "";
            if (stock.ItemType == ItemType.Diamond)
                str = "Diamond";
            else if (stock.ItemType == ItemType.Gold)
                str = "Gold";
            else if (stock.ItemType == ItemType.Silver)
                str = "Silver";
            else if (stock.ItemType == ItemType.Pladium)
                str = "Pladium";
            else
                str = "Platinum";
            cmd.Parameters.Add(new SqlParameter("IType", str));

            cmd.Parameters.Add("@SubGItmId", SqlDbType.Int);
            cmd.Parameters["@SubGItmId"].Value = DBNull.Value;
            cmd.Parameters.Add("@SubItemId", SqlDbType.Int);
            cmd.Parameters["@SubItemId"].Value = DBNull.Value;

            if (stock.NetWeight.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@NetWeight", stock.NetWeight));
            }
            else
            {
                cmd.Parameters.Add("@NetWeight", SqlDbType.Float);
                cmd.Parameters["@NetWeight"].Value = DBNull.Value;
            }

            cmd.Parameters.Add(new SqlParameter("@TotalWeight", stock.TotalWeight));

            if (stock.Silver.RateA.HasValue)
                cmd.Parameters.Add(new SqlParameter("@RateA", stock.Silver.RateA));
            else
                cmd.Parameters.Add(new SqlParameter("@RateA", 0));

            if (stock.Silver.PriceA.HasValue)
                cmd.Parameters.Add(new SqlParameter("@PriceA", stock.Silver.PriceA));
            else
                cmd.Parameters.Add(new SqlParameter("@PriceA", 0));

            if (stock.Silver.RateD.HasValue)
                cmd.Parameters.Add(new SqlParameter("@RateD", stock.Silver.RateD));
            else
                cmd.Parameters.Add(new SqlParameter("@RateD", 0));

            if (stock.Silver.PriceD.HasValue)
                cmd.Parameters.Add(new SqlParameter("@PriceD", stock.Silver.PriceD));
            else
                cmd.Parameters.Add(new SqlParameter("@PriceD", 0));

            if (stock.Silver.SalePrice.HasValue)
                cmd.Parameters.Add(new SqlParameter("@SilverSalePrice", stock.Silver.SalePrice));
            else
                cmd.Parameters.Add(new SqlParameter("@SilverSalePrice", 0));
            // cmd.Parameters.Add(new SqlParameter("@SaleWeight", stock.SaleWeight));
            cmd.Parameters.Add(new SqlParameter("@ItemSize", stock.ItemSize));
            if (stock.Qty.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@Qty", stock.Qty));
            }
            else
            {
                cmd.Parameters.Add("@Qty", SqlDbType.Int);
                cmd.Parameters["@Qty"].Value = DBNull.Value;
            }
            if (stock.Pieces.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@Pieces", stock.Pieces));
            }
            else
            {
                cmd.Parameters.Add("@Pieces", SqlDbType.Int);
                cmd.Parameters["@Pieces"].Value = DBNull.Value;
            }

            cmd.Parameters.Add(new SqlParameter("@Karat", stock.Karrat));
            cmd.Parameters.Add(new SqlParameter("@Description", stock.Description));
            cmd.Parameters.Add(new SqlParameter("@StockDate", stock.StockDate));
            //stock.WorkerName = new Worker();
            if (stock.WorkerName != null)
                cmd.Parameters.Add(new SqlParameter("@WorkerId", stock.WorkerName.ID));
            else
            {
                cmd.Parameters.Add("@WorkerId", SqlDbType.Int);
                cmd.Parameters["@WorkerId"].Value = DBNull.Value;
            }
            if (stock.WasteInGm.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@WasteInGm", stock.WasteInGm));
            }
            else
            {
                cmd.Parameters.Add("@WasteInGm", SqlDbType.Float);
                cmd.Parameters["@WasteInGm"].Value = DBNull.Value;
            }
            if (stock.WastePercent.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@WastePercent", stock.WastePercent));
            }
            else
            {
                cmd.Parameters.Add("@WastePercent", SqlDbType.Float);
                cmd.Parameters["@WastePercent"].Value = DBNull.Value;
            }
            if (stock.KaatInRatti.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@Kaat", stock.KaatInRatti));
            }
            else
            {
                cmd.Parameters.Add("@Kaat", SqlDbType.Float);
                cmd.Parameters["@Kaat"].Value = DBNull.Value;
            }
            if (stock.PWeight.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@PWeight", stock.PWeight));
            }
            else
            {
                cmd.Parameters.Add("@PWeight", SqlDbType.Float);
                cmd.Parameters["@PWeight"].Value = DBNull.Value;
            }
            if (stock.LakerGm.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@LakerGm", stock.LakerGm));
            }
            else
            {
                cmd.Parameters.Add("@LakerGm", SqlDbType.Float);
                cmd.Parameters["@LakerGm"].Value = DBNull.Value;
            }
            if (stock.TotalLaker.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@TotalLaker", stock.TotalLaker));
            }
            else
            {
                cmd.Parameters.Add("@TotalLaker", SqlDbType.Float);
                cmd.Parameters["@TotalLaker"].Value = DBNull.Value;
            }

            if (stock.MakingPerGm.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@MakingPerGm", stock.MakingPerGm));
            }
            else
            {
                cmd.Parameters.Add("@MakingPerGm", SqlDbType.Float);
                cmd.Parameters["@MakingPerGm"].Value = DBNull.Value;
            }

            if (stock.TotalMaking.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@TotalMaking", stock.TotalMaking));
            }
            else
            {
                cmd.Parameters.Add("@TotalMaking", SqlDbType.Float);
                cmd.Parameters["@TotalMaking"].Value = DBNull.Value;
            }

            if (stock.WTola.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@WTola", stock.WTola));
            }
            else
            {
                cmd.Parameters.Add("@WTola", SqlDbType.Int);
                cmd.Parameters["@WTola"].Value = DBNull.Value;
            }
            if (stock.WMasha.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@WMasha", stock.WMasha));
            }
            else
            {
                cmd.Parameters.Add("@WMasha", SqlDbType.Int);
                cmd.Parameters["@WMasha"].Value = DBNull.Value;
            }
            if (stock.WRatti.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@WRatti", stock.WRatti));
            }
            else
            {
                cmd.Parameters.Add("@WRatti", SqlDbType.Int);
                cmd.Parameters["@WRatti"].Value = DBNull.Value;
            }
            if (stock.PTola.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@PTola", stock.PTola));
            }
            else
            {
                cmd.Parameters.Add("@PTola", SqlDbType.Int);
                cmd.Parameters["@PTola"].Value = DBNull.Value;
            }
            if (stock.PMasha.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@PMasha", stock.PMasha));
            }
            else
            {
                cmd.Parameters.Add("@PMasha", SqlDbType.Int);
                cmd.Parameters["@PMasha"].Value = DBNull.Value;
            }
            if (stock.PRatti.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@PRatti", stock.PRatti));
            }
            else
            {
                cmd.Parameters.Add("@PRatti", SqlDbType.Int);
                cmd.Parameters["@PRatti"].Value = DBNull.Value;
            }
            if (stock.TTola.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@TTola", stock.TTola));
            }
            else
            {
                cmd.Parameters.Add("@TTola", SqlDbType.Int);
                cmd.Parameters["@TTola"].Value = DBNull.Value;
            }
            if (stock.TMasha.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@TMasha", stock.TMasha));
            }
            else
            {
                cmd.Parameters.Add("@TMasha", SqlDbType.Int);
                cmd.Parameters["@TMasha"].Value = DBNull.Value;
            }
            if (stock.TRatti.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@TRatti", stock.TRatti));
            }
            else
            {
                cmd.Parameters.Add("@TRatti", SqlDbType.Int);
                cmd.Parameters["@TRatti"].Value = DBNull.Value;
            }

            if (stock.DesignNo != null)
                cmd.Parameters.Add(new SqlParameter("@DesignId", stock.DesignNo.DesignId));
            else
            {
                cmd.Parameters.Add("@DesignId", SqlDbType.Int);
                cmd.Parameters["@DesignId"].Value = DBNull.Value;
            }
            cmd.Parameters.Add(new SqlParameter("@MakingType", stock.MakingType));
            if (stock.PurchaseRate.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@PurchaseRate", stock.PurchaseRate));
            }
            else
            {
                cmd.Parameters.Add("@PurchaseRate", SqlDbType.Float);
                cmd.Parameters["@PurchaseRate"].Value = DBNull.Value;
            }
            if (stock.ItemCost.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@ItemCost", stock.ItemCost));
            }
            else
            {
                cmd.Parameters.Add("@ItemCost", SqlDbType.Float);
                cmd.Parameters["@ItemCost"].Value = DBNull.Value;
            }
            if (stock.SalePrice.HasValue)
            {
                cmd.Parameters.Add(new SqlParameter("@SalePrice", stock.SalePrice));
            }
            else
            {
                cmd.Parameters.Add("@SalePrice", SqlDbType.Float);
                cmd.Parameters["@SalePrice"].Value = DBNull.Value;
            }
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";
            cmd.Parameters.Add("@bFlag", SqlDbType.Bit).Value = false;

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
            con.Open();

            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                cmdStone.Transaction = tran;
                cmd.ExecuteNonQuery();
                try
                {
                    if (stock.StoneList == null)
                    {

                    }
                    else
                    {

                        foreach (Stones stList in stock.StoneList)
                        {
                            cmdStone.Parameters["@TagNo"].Value = stock.TagNo.ToString();

                            cmdStone.Parameters["@StoneTypeId"].Value = stList.StoneTypeId;
                            if (stList.StoneId.HasValue)
                                cmdStone.Parameters["@StoneId"].Value = stList.StoneId;

                            else
                                cmdStone.Parameters["@StoneName"].Value = DBNull.Value;
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
                if (con.State == ConnectionState.Open) con.Close();
            }
        }

        public List<string> GetAllTagsByItemId(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=" + id;
            //  string selectRecord = "GetAllTagById";
            string getRecord = "select TagNo from Stock Where Status='Available' and ItemId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            //cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

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

        public Stock GetStkBySockId(int stkId)
        {
            //string GetAllStock = "select * from Stock where StockId =" + stkId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAllStockForSample", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@StockId", SqlDbType.Int).Value = stkId;
            Stock stk = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stk = new Stock();
                    ItemType it;
                    if (dr["IType"].ToString() == "Gold")
                        it = ItemType.Gold;
                    else if (dr["IType"].ToString() == "Silver")
                        it = ItemType.Silver;
                    else if (dr["IType"].ToString() == "Diamond")
                        it = ItemType.Diamond;
                    else if (dr["IType"].ToString() == "Platinum")
                        it = ItemType.Platinum;
                    else
                        it = ItemType.Pladium;
                    stk.ItemType = it;

                    stk.StockId = Convert.ToInt32(dr["StockId"]);
                    stk.TagNo = dr["TagNo"].ToString();
                    //stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                    stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));

                    if (Convert.ToDecimal(dr["NetWeight"]) == 0)
                        stk.NetWeight = 0;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                    stk.ItemSize = dr["ItemSize"].ToString();

                    if (Convert.ToInt32(dr["Qty"]) == 0)
                        stk.Qty = 0;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    if (dr["Pieces"] == DBNull.Value)
                        stk.Pieces = 0;
                    else
                        stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                    stk.Karrat = dr["Karat"].ToString();
                    stk.Description = dr["Description"].ToString();

                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = 0;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]));

                    if (dr["WasteInGm"] == DBNull.Value)
                        stk.WasteInGm = 0;
                    else
                        stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"]==DBNull.Value)
                        stk.WastePercent = 0;
                    else
                        stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["Kaat"] == DBNull.Value)
                        stk.KaatInRatti = 0;
                    else
                        stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                    if (dr["PWeight"] == DBNull.Value)
                        stk.PWeight = 0;
                    else
                        stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                    if (dr["LakerGm"]==DBNull.Value)
                        stk.LakerGm = 0;
                    else
                        stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        stk.TotalLaker = 0;
                    else
                        stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);


                    stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                    if (dr["MakingPerGm"] == DBNull.Value)
                        stk.MakingPerGm = 0;
                    else
                        stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        stk.TotalMaking = 0;
                    else
                        stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);                    

                    if (dr["DesignId"]==DBNull.Value)
                    {
                        stk.DesignNo = new Design();
                        stk.DesignNo.DesignId = 0;
                    }
                    else
                        stk.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]));

                    stk.MakingType = dr["MakingType"].ToString();
                   
                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
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

            return stk;
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
                    return tempImage ;
                else 
                    tempImage = (byte[])cmd.ExecuteScalar();
            }
            catch(Exception ex) 
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return tempImage;
        }

        public void DeleteStock(int id)
        {

            //string deleteCustomer = "Delete from Stock where StockId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand("DeleteStock", con);
            cmdDelete.CommandType = CommandType.StoredProcedure;
            cmdDelete.Parameters.Add("@oldStockId", SqlDbType.NVarChar).Value = id;

            cmdDelete.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Deleted";
            cmdDelete.Parameters.Add("@DelDate", SqlDbType.DateTime).Value = DateTime.Now;
            try
            {
                con.Open();
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                con.Close();
            }
        }
        public void UpdateDeleteStockDescription(string query)
        {

            //string deleteCustomer = "Delete from Stock where StockId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(query, con);
            cmdDelete.CommandType = CommandType.Text;          
            try
            {
                con.Open();
                cmdDelete.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                con.Close();
            }
        }

        public bool isTagNoExist(string tagNo)
        {

            string querry = "select sFlag from Stock where sFlag='True' and TagNo like '%" + tagNo + "%'";
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

        public bool isTagNoExistInDbPics(string tagNo)
        {

            string querry = "select TagNo from JewlPictures where TagNo = '" + tagNo + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConStrPictures);
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

        public List<Stock> GetSoldTagNoByItemId(int id)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and Status=@Status" 

            string selectRecord = "GetAllTagById";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Not Available";

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

        public void UpDateOrderEstimate(string oldItmId, Stock stk)
        {
            SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateEmploye = new SqlCommand("update OrderEstimate set Status='Completed', TagNo = '" + stk.TagNo + "', ItemId = " + stk.ItemName.ItemId + ", Qty = " + stk.Qty + ", Pieces = " + stk.Pieces + ", ItemSize = '" + stk.ItemSize + "', Weight = " + stk.NetWeight + ", Waste = " + stk.WasteInGm + ", WastePercent = " + stk.WastePercent + ", TotalWeight = " + stk.TotalWeight + " where OItemId='" + oldItmId + "'", conn);
            cmdUpdateEmploye.CommandType = CommandType.Text;
            conn.Open();
            try
            {
                SqlTransaction tran = conn.BeginTransaction();
                cmdUpdateEmploye.Transaction = tran;
                try
                {
                    cmdUpdateEmploye.ExecuteNonQuery();
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
                conn.Close();
            }
        }

        public decimal sumWeight(string query)
        {
            //string querry = "Select MAX(IndexNo) as [MaxIndex] from Stock";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal  weight = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["TWeight"] == DBNull.Value)
                        weight = 0;
                    else
                    {
                        weight = Convert.ToDecimal(dr["TWeight"]);
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
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return weight;
        }

        public decimal sumQty(string query)
        {
            //string querry = "Select MAX(IndexNo) as [MaxIndex] from Stock";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal weight = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["TQty"] == DBNull.Value)
                        weight = 0;
                    else
                    {
                        weight = Convert.ToDecimal(dr["TQty"]);
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
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return weight;
        }

        public int GetMaxStockCheckId()
        {
            string querry = "select max(SessionId) as max from tblMasterScan";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int sessionId = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["max"] == DBNull.Value)
                        sessionId = 0;
                    else
                        sessionId = Convert.ToInt32(dr["max"]);
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
            return sessionId;
        }

        public int GetStockIdByTagNo(string tagNo)
        {
            string querry = "select StockId as max from Stock where TagNo='"+tagNo+"'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int sessionId = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["max"] == DBNull.Value)
                        sessionId = 0;
                    else
                        sessionId = Convert.ToInt32(dr["max"]);
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
            return sessionId;
        }

        public int  GetCountOfPicsTags(string tagF, string tagT)
        {
            string query = "select Count(TagNo)'Count' from Stock where TagNo between '"+tagF+"' and '"+tagT+"' and Status = 'Available'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            int  nFlag = 0;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    nFlag = Convert.ToInt32(dr["Count"]);
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
            return nFlag;
        }

        public bool isSessionIdExist(int id)
        {

            string querry = "select SessionId from tblMasterScan where SessionId= " + id;
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

        public bool isTagScanned(int id, string tagNo)
        {
            string querry = "select SessionId from tblStockCheck where TagNo = '" + tagNo + "' and SessionId= " + id;
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

        public bool isManualTagNoExist(string tagNo)
        {

            string querry = "select tagno from stock where tagno='" + tagNo + "'";
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

        #region Bulk

        public List<Stock> GetAllTagNoByItemIdForBulk(int id)
        {
            string selectRecord = "select StockId ,TagNo from Stock where ItemId=@ItemId and [Status]=@Status and Bstatus='Bulk' order by TagNo";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

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

        public void AddBulkStockTag(Stock stock)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddBulkStockTag", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ItemId", stock.ItemName.ItemId));
            cmd.Parameters.Add(new SqlParameter("@TagNo", stock.TagNo));
            cmd.Parameters.Add(new SqlParameter("@Date", stock.StockDate));
            cmd.Parameters.Add(new SqlParameter("@Qty", stock.BQuantity));
            cmd.Parameters.Add(new SqlParameter("@Weight", stock.BWeight));
            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.ExecuteNonQuery();
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

        public void UpdateBulkStockTag(Stock stock)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateItem = new SqlCommand("update Stock set Bweight=Bweight+" + stock.BWeight + "where tagno='" + stock.TagNo + "'; update Stock set Qty=Qty+" + stock.BQuantity + "where tagno='" + stock.TagNo + "'; update Stock set Bquantity=Bquantity+" + stock.BQuantity + "where tagno='" + stock.TagNo + "'", con);
            cmdUpdateItem.CommandType = CommandType.Text;
            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmdUpdateItem.Transaction = tran;
                try
                {
                    cmdUpdateItem.ExecuteNonQuery();
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

        #endregion
    }

}
