using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class PictureDAL
    {
        public Stock GetStkPics(string tagNo)
        {
            //string selectRecord = "select Picture from Stock where StockId=" + stkId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetStockPics", con);
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

                    if (dr["NetWeight"] == DBNull.Value)
                        stk.NetWeight = null;
                    else
                        stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                    if (dr["Qty"] == DBNull.Value)
                        stk.Qty = null;
                    else
                        stk.Qty = Convert.ToInt32(dr["Qty"]);

                    stk.Description = dr["Description"].ToString();
                    stk.Status = dr["Status"].ToString();

                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        stk.WorkerName = new Worker();
                        stk.WorkerName.ID = null;
                    }
                    else
                        stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                    System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                    stk.ImageMemory = this.ImageRestore("SELECT Picture from " + builder.InitialCatalog + ".dbo.JewlPictures where TagNo='" + tagNo + "'");

                    stk.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    stk.StoneList = sDal.GetAllStonesDetail(tagNo);
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

        public List<Stock> GetAllTagNoByWrkId(string selectRecord)
        {
            //string selectRecord = "select StockId ,TagNo from Stock where WorkerId='" + id +"' and ItemId="+itmId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectRecord, con);
            //SqlDataAdapter oAd = new SqlDataAdapter(cmd);
            //myDataSet = new DataSet();
            //oAd.Fill(myDataSet);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = id;
         
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

        public List<Stock> GetPicsByWrkId(int wrkId)
        {
            //string selectRecord = "select Picture from Stock where StockId=" + stkId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetPicsByWrkId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@WorkerId", SqlDbType.Int).Value = wrkId;
            List<Stock> stks = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    stks = new List<Stock>();
                    if (stks == null) stks = new List<Stock>();

                    do
                    {
                       Stock stk = new Stock();

                       stk.TagNo = dr["TagNo"].ToString();

                        if (dr["NetWeight"] == DBNull.Value)
                            stk.NetWeight = null;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                        if (dr["Qty"] == DBNull.Value)
                            stk.Qty = null;
                        else
                            stk.Qty = Convert.ToInt32(dr["Qty"]);

                        stk.Description = dr["Description"].ToString();
                        stk.Status = dr["Status"].ToString();

                        if (dr["WorkerId"] == DBNull.Value)
                            stk.WorkerName.ID = null;
                        else
                            stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                        System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(DALHelper.ConStrPictures);
                        stk.ImageMemory = this.ImageRestore("SELECT Picture from " + builder.InitialCatalog + ".dbo.JewlPictures where WorkerId = " + wrkId);

                        stk.StoneList = new List<Stones>();
                        StonesDAL sDal = new StonesDAL();
                        stk.StoneList = sDal.GetAllStonesDetail(stk.TagNo);
                        stks.Add(stk);
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
            return stks;
        }

        public List<Stock> GetAllTagNoByDate(DateTime dt1, DateTime dt2)
        {
            
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetTagNoByDate", con);
           
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Datef", SqlDbType.DateTime).Value = dt1;
            cmd.Parameters.Add("@Datet", SqlDbType.DateTime).Value = dt2;
           

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

        public List<Stock> GetAllTagNoByWeight(decimal wt1, decimal wt2)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetTagNoByWeight", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Weightf", SqlDbType.Int).Value = wt1;
            cmd.Parameters.Add("@Weightt", SqlDbType.Int).Value = wt2;


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

        public byte[] ImageRestore(string getImage)
        {
            byte[] tempImage = null;
            //string getImage = "SELECT Picture from Stock where TagNo='" + tagNo+"'";
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

        public JewelPictures GetPictures(string tagNo)
        {
           
            string getImage = "SELECT * from JewlPictures where TagNo='" + tagNo+"'";
            SqlConnection con = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmd = new SqlCommand(getImage, con);
            JewelPictures jp = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    jp = new JewelPictures();
                    if (dr["Picture"] == DBNull.Value)
                    {
                        jp.ImageMemory = null;
                    }
                    else
                        jp.ImageMemory = (byte[])dr["Picture"];

                    if (dr["SmallPicture"] == DBNull.Value)
                    {
                        jp.ImageMemorySmall = null;
                    }
                    else
                        jp.ImageMemorySmall = (byte[])dr["SmallPicture"];
                    if (dr["Picture1"] == DBNull.Value)
                    {
                        jp.ImageMemory1 = null;
                    }
                    else
                    jp.ImageMemory1 = (byte[])dr["Picture1"];
                    if (dr["Picture2"] == DBNull.Value)
                    {
                        jp.ImageMemory2 = null;
                    }
                    else
                    jp.ImageMemory2 = (byte[])dr["Picture2"];
                    if (dr["Picture3"] == DBNull.Value)
                    {
                        jp.ImageMemory3 = null;
                    }
                    else
                    jp.ImageMemory3 = (byte[])dr["Picture3"];
                    if (dr["Picture4"] == DBNull.Value)
                    {
                        jp.ImageMemory4 = null;
                    }
                    else
                    jp.ImageMemory4 = (byte[])dr["Picture4"];
                    if (dr["Picture5"] == DBNull.Value)
                    {
                        jp.ImageMemory5 = null;
                    }
                    else
                    jp.ImageMemory5 = (byte[])dr["Picture5"];
                    if (dr["Picture6"] == DBNull.Value)
                    {
                        jp.ImageMemory6 = null;
                    }
                    else
                    jp.ImageMemory6 = (byte[])dr["Picture6"];
                    if (dr["Picture7"] == DBNull.Value)
                    {
                        jp.ImageMemory7 = null;
                    }
                    else
                    jp.ImageMemory7 = (byte[])dr["Picture7"];
                    if (dr["Picture8"] == DBNull.Value)
                    {
                        jp.ImageMemory8 = null;
                    }
                    else
                    jp.ImageMemory7 = (byte[])dr["Picture8"];

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
            return jp;
        }

        public void ManagePic(byte[] jp, int val, string oid, int intVal, int from, int UStatus)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic;
            if (UStatus == 0)
                cmdpic = new SqlCommand("AddPicturesForOrder", conpic);
            else
                cmdpic = new SqlCommand("UpdateManagePictures", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;
            
            if (from == 1)
            {
                cmdpic.Parameters.Add(new SqlParameter("@OrderNo", intVal));
                cmdpic.Parameters.Add(new SqlParameter("@OitemId", oid));
            }
            if (from == 2)
            {
                cmdpic.Parameters.Add(new SqlParameter("@RepairId", intVal));
                // cmdpic.Parameters.Add(new SqlParameter("@ItemNo", jp.TagNo));
            }
            if (from == 3)
                cmdpic.Parameters.Add(new SqlParameter("@CustId", intVal));
            if (jp == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", jp));

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
