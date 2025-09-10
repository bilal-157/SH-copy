using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

namespace DAL
{
    public class SampleDAL
    {
        public void AddSample(Sample sample)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddSample", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SampleNo", SqlDbType.Int);
            cmd.Parameters.Add("@TagNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@CustId", SqlDbType.Int);
            cmd.Parameters.Add("@SampleDate", SqlDbType.DateTime);
            cmd.Parameters.Add("@BillBookNo", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SampleFlag", SqlDbType.Bit);
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar);
            cmd.Parameters.Add("@SampleQty", SqlDbType.Int);
            cmd.Parameters.Add("@sQty", SqlDbType.Int);
            cmd.Parameters.Add("@SampleWt", SqlDbType.Float);
            cmd.Parameters.Add("@sWt", SqlDbType.Float);
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar);
            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    foreach (SampleLineItem sli in sample.SampleLineItems)
                    {
                        cmd.Parameters["@SampleNo"].Value = sli.SampleNo;
                        cmd.Parameters["@TagNo"].Value = sli.Stock.TagNo;
                        cmd.Parameters["@CustId"].Value = sample.Customer.ID;
                        cmd.Parameters["@SampleDate"].Value = sli.SampleDate;
                        cmd.Parameters["@BillBookNo"].Value = sli.BillBookNo;
                        cmd.Parameters["@SampleFlag"].Value = true;                       
                        cmd.Parameters["@SampleQty"].Value = sli.SQty;
                        cmd.Parameters["@sQty"].Value = sli.Stock.Qty;
                        if (sli.Stock.Qty == 0)
                            cmd.Parameters["@Status"].Value = "Sample";
                        else
                            cmd.Parameters["@Status"].Value = "Available";
                        cmd.Parameters["@SampleWt"].Value = sli.SampleWt;
                        cmd.Parameters["@sWt"].Value = sli.Stock.NetWeight;
                        cmd.Parameters["@Description"].Value = sli.Description;
                        cmd.ExecuteNonQuery();
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
        public int GetMaxSampleNo()
        {
            string querry = "Select MAX(SampleNo) as [MaxSample] from Sample";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int sampleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["MaxSample"] == DBNull.Value)
                        sampleNo = 0;
                    else
                        sampleNo = Convert.ToInt32(dr["MaxSample"]);
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
            return sampleNo;
        }
        public void UpdateSampleByTagNo(int oldSampleNo, Sample newSam)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateSample = new SqlCommand("UpdateSamples", con);
            cmdUpdateSample.CommandType = CommandType.StoredProcedure;

            cmdUpdateSample.Parameters.Add("@oldSampleNo", SqlDbType.Int);
            cmdUpdateSample.Parameters.Add("@CustId", SqlDbType.Int);
            cmdUpdateSample.Parameters.Add("@SampleDate", SqlDbType.DateTime);
            cmdUpdateSample.Parameters.Add("@BillBookNo", SqlDbType.NVarChar);
            cmdUpdateSample.Parameters.Add("@SampleFlag", SqlDbType.Bit);
            cmdUpdateSample.Parameters.Add("@oldTagNo", SqlDbType.NVarChar);
            cmdUpdateSample.Parameters.Add("@SampleQty", SqlDbType.Int);
            cmdUpdateSample.Parameters.Add("@tQty", SqlDbType.Int);
            cmdUpdateSample.Parameters.Add("@SampleWt", SqlDbType.Float);
            cmdUpdateSample.Parameters.Add("@sNetWeight", SqlDbType.Float);
            cmdUpdateSample.Parameters.Add("@Description", SqlDbType.NVarChar);
            cmdUpdateSample.Parameters.Add("@Status", SqlDbType.NVarChar);
            try
            {
                con.Open();
                SqlTransaction tran = con.BeginTransaction();
                cmdUpdateSample.Transaction = tran;
                try
                {
                    if (newSam.SampleLineItems != null)
                    {
                        foreach (SampleLineItem sli in newSam.SampleLineItems)
                        {
                            cmdUpdateSample.Parameters["@oldSampleNo"].Value = oldSampleNo;
                            cmdUpdateSample.Parameters["@oldTagNo"].Value = sli.Stock.TagNo;
                            cmdUpdateSample.Parameters["@CustId"].Value = newSam.Customer.ID;
                            cmdUpdateSample.Parameters["@SampleDate"].Value = newSam.SampleDate;
                            cmdUpdateSample.Parameters["@BillBookNo"].Value = newSam.BillBookNo;
                            cmdUpdateSample.Parameters["@SampleFlag"].Value = true;
                            cmdUpdateSample.Parameters["@SampleQty"].Value = sli.SQty;
                            if (sli.Stock.Qty == 0)
                                cmdUpdateSample.Parameters["@Status"].Value = "Sample";
                            else
                                cmdUpdateSample.Parameters["@Status"].Value = "Available";
                            cmdUpdateSample.Parameters["@tQty"].Value = sli.Stock.Qty;
                            cmdUpdateSample.Parameters["@SampleWt"].Value = sli.SampleWt;
                            cmdUpdateSample.Parameters["@sNetWeight"].Value = sli.Stock.NetWeight;
                            cmdUpdateSample.Parameters["@Description"].Value = sli.Description;
                            cmdUpdateSample.ExecuteNonQuery();
                        }
                        tran.Commit();
                    }
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

        public List<Sample> GetAllTagsBySampleNo(int id)
        {
            string getRecord = "select * from Sample  Where SampleFlag='True' and SampleNo=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getRecord, con);
            cmd.CommandType = CommandType.Text;
            List<Sample> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (records == null) records = new List<Sample>();
                    do
                    {
                        Sample smp = new Sample();
                        smp.TagNum = dr["TagNo"].ToString();
                        smp.BillBookNo = dr["BillBookNo"].ToString();
                        smp.SampleDate = Convert.ToDateTime(dr["SampleDate"]);
                        smp.SampleNo = Convert.ToInt32(dr["SampleNo"]);
                        smp.Customer = new Customer();
                        smp.Customer.ID = Convert.ToInt32(dr["CustId"]);
                        records.Add(smp);
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
        public List<string> GetAllTagsByCustId(int id)
        {
            string getRecord = "select TagNo from Sample Where SampleFlag='True' and CustId=" + id;
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

        public SampleLineItem GetSampleByTagNo(string tagNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetSampleByTagNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@tagNo", SqlDbType.NVarChar).Value = tagNo;
            SampleLineItem smp = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    smp = new SampleLineItem();
                    smp.Stock = new Stock();

                    ItemType it;
                    if (Convert.ToString(dr["IType"]) == "Gold")
                        it = ItemType.Gold;
                    else if (Convert.ToString(dr["IType"]) == "Diamond")
                        it = ItemType.Diamond;
                    else if (Convert.ToString(dr["IType"]) == "Silver")
                        it = ItemType.Silver;
                    else if (Convert.ToString(dr["IType"]) == "Pladium")
                        it = ItemType.Pladium;
                    else
                        it = ItemType.Platinum;
                    smp.Stock.ItemType = it;
                    smp.Stock.StockId = Convert.ToInt32(dr["StockId"]);
                    smp.SampleNo = Convert.ToInt32(dr["SampleNo"]);
                    smp.Stock.TagNo = dr["TagNo"].ToString();
                    smp.Stock.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());
                    smp.Stock.Karrat = dr["Karat"].ToString();
                    if (dr["SampleWt"] == DBNull.Value)
                        smp.SampleWt = null;
                    else
                        smp.SampleWt = Convert.ToDecimal(dr["SampleWt"]);

                    if (dr["ReturnWt"] == DBNull.Value)
                        smp.ReturnWt = 0;
                    else
                        smp.ReturnWt = Convert.ToDecimal(dr["ReturnWt"]);

                    if (dr["NetWeight"] == DBNull.Value)
                        smp.Stock.NetWeight = null;
                    else
                        smp.Stock.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                    smp.Stock.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                    if (dr["SampleQty"] == DBNull.Value)
                        smp.SQty = null;
                    else
                        smp.SQty = Convert.ToInt32(dr["SampleQty"]);

                    if (dr["ReturnQty"] == DBNull.Value)
                        smp.ReturnQty = 0;
                    else
                        smp.ReturnQty = Convert.ToInt32(dr["ReturnQty"]);

                    if (dr["Qty"] == DBNull.Value)
                        smp.Stock.Qty = null;
                    else
                        smp.Stock.Qty = Convert.ToInt32(dr["Qty"]);

                    smp.Description = dr["Description"].ToString();

                    smp.SampleDate = Convert.ToDateTime(dr["SampleDate"]);
                    //if (Convert.ToInt32(dr["WorkerId"]) == 0)
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        Worker wrk = new Worker();
                        wrk.ID = 0;
                        smp.Stock.WorkerName = wrk;
                    }
                    else
                        smp.Stock.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    //stk.WorkerName=new Worker( Convert.ToInt32(dr["WorkerId"]));

                    if (dr["WasteInGm"] == null)
                        smp.Stock.WasteInGm = null;
                    else
                        smp.Stock.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                    if (dr["WastePercent"] == DBNull.Value)
                        smp.Stock.WastePercent = null;
                    else
                        smp.Stock.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                    if (dr["LakerGm"] == DBNull.Value)
                        smp.Stock.LakerGm = null;
                    else
                        smp.Stock.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                    if (dr["TotalLaker"] == DBNull.Value)
                        smp.Stock.TotalLaker = null;
                    else
                        smp.Stock.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);

                    if (dr["MakingPerGm"] == DBNull.Value)
                        smp.Stock.MakingPerGm = null;
                    else
                        smp.Stock.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                    if (dr["TotalMaking"] == DBNull.Value)
                        smp.Stock.TotalMaking = null;
                    else
                        smp.Stock.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);

                    if (dr["TotalPrice"] == DBNull.Value)
                        smp.Stock.TotalPrice = null;
                    else
                        smp.Stock.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);

                    if (dr["DesignId"] == DBNull.Value)
                    {
                        smp.Stock.DesignNo = new Design();
                        smp.Stock.DesignNo.DesignId = 0;
                    }
                    else
                        smp.Stock.DesignNo = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());
                    //stk.DesignNo = new Design (Convert.ToInt32(dr["DesignId"]));

                    if (dr["BillBookNo"] == DBNull.Value)
                        smp.BillBookNo = null;
                    else
                        smp.BillBookNo = dr["BillBookNo"].ToString();

                    smp.SampleNo = Convert.ToInt32(dr["SampleNo"]);

                    smp.Customer = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Email"].ToString());

                    smp.Stock.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();
                    smp.Stock.StoneList = sDal.GetAllStonesDetail(smp.Stock.TagNo);
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

            return smp;
        }

        public void SampleReturn(string oldTagNo, SampleLineItem retSam)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdSampleReturn = new SqlCommand("SampleReturn", con);
            SqlCommand cmdUpStock = new SqlCommand("UpStockFromSample", con);
            cmdSampleReturn.CommandType = CommandType.StoredProcedure;
            cmdUpStock.CommandType = CommandType.StoredProcedure;

            cmdSampleReturn.Parameters.Add("@oldTagNo", SqlDbType.NVarChar).Value = oldTagNo;
            cmdUpStock.Parameters.Add("@oldTagNo", SqlDbType.NVarChar).Value = oldTagNo;

            //cmdUpdateSample.Parameters.Add(new SqlParameter("@SampleNo", newSam.SampleNo));
            //newSam.Customer = new Customer();
            cmdSampleReturn.Parameters.Add(new SqlParameter("@CustId", retSam.Customer.ID));
            if (retSam.SampleDate == null)
            {
                cmdSampleReturn.Parameters.Add("@SampleDate", SqlDbType.DateTime);
                cmdSampleReturn.Parameters["@SampleDate"].Value = DBNull.Value;
            }
            else
                cmdSampleReturn.Parameters.Add(new SqlParameter("@SampleDate", retSam.SampleDate));

            cmdSampleReturn.Parameters.Add(new SqlParameter("@BillBookNo", retSam.BillBookNo));

            if (retSam.ReturnWt.HasValue)
                cmdSampleReturn.Parameters.Add(new SqlParameter("@ReturnWt", retSam.ReturnWt));
            else
            {
                cmdSampleReturn.Parameters.Add("@ReturnWt", SqlDbType.Float);
                cmdSampleReturn.Parameters["@ReturnWt"].Value = DBNull.Value;
            }
            if (retSam.ReturnQty.HasValue)
                cmdSampleReturn.Parameters.Add(new SqlParameter("@ReturnQty", retSam.ReturnQty));
            else
            {
                cmdSampleReturn.Parameters.Add("@ReturnQty", SqlDbType.Int);
                cmdSampleReturn.Parameters["@ReturnQty"].Value = DBNull.Value;
            }
            if (retSam.ReturnDate != null )
                cmdSampleReturn.Parameters.Add(new SqlParameter("@ReturnDate", retSam.ReturnDate));
            else
            {
                cmdSampleReturn.Parameters.Add("@ReturnDate", SqlDbType.DateTime);
                cmdSampleReturn.Parameters["@ReturnDate"].Value = DBNull.Value;
            }

            //cmdSampleReturn.Parameters.Add("@SampleFlag", SqlDbType.Bit).Value = false;


            if (retSam.Stock.NetWeight.HasValue)
                cmdUpStock.Parameters.Add(new SqlParameter("@NetWeight", retSam.Stock.NetWeight));
            else
            {
                cmdUpStock.Parameters.Add("@NetWeight", SqlDbType.Float);
                cmdUpStock.Parameters["@NetWeight"].Value = DBNull.Value;
            }
            if (retSam.Stock.Qty.HasValue)
                cmdUpStock.Parameters.Add(new SqlParameter("@Qty", retSam.Stock.Qty));
            else
            {
                cmdUpStock.Parameters.Add("@Qty", SqlDbType.Float);
                cmdUpStock.Parameters["@Qty"].Value = DBNull.Value;
            }

            //if (retSam.Stock.NetWeight.HasValue)
            //    cmdUpStock.Parameters.Add(new SqlParameter("@NetWeight", retSam.Stock.NetWeight));
            //else
            //{
            //    cmdUpStock.Parameters.Add("@NetWeight", SqlDbType.Float);
            //    cmdUpStock.Parameters["@NetWeight"].Value = DBNull.Value;
            //}
            //if (retSam.Stock.Qty.HasValue)
            //    cmdUpStock.Parameters.Add(new SqlParameter("@Qty", retSam.Stock.Qty));
            //else
            //{
            //    cmdUpStock.Parameters.Add("@Qty", SqlDbType.Float);
            //    cmdUpStock.Parameters["@Qty"].Value = DBNull.Value;
            //}

            
            cmdSampleReturn.Parameters.Add(new SqlParameter("@Description", retSam.Description));

            //cmdSampleReturn.Parameters.Add(new SqlParameter("@oldTagNo", oldTagNo));





            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmdSampleReturn.Transaction = tran;
                cmdUpStock.Transaction = tran;
                try
                {

                    cmdSampleReturn.ExecuteNonQuery();
                    cmdUpStock.ExecuteNonQuery();
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
        public void ReturnSampleByTagNo(string oldTagNo, SampleLineItem newSam)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateSample = new SqlCommand("UpdateSampleFromSample", con);
            SqlCommand cmdUpStock = new SqlCommand("UpdateStkFromSample", con);
            cmdUpdateSample.CommandType = CommandType.StoredProcedure;
            cmdUpStock.CommandType = CommandType.StoredProcedure;

            cmdUpdateSample.Parameters.Add("@oldTagNo", SqlDbType.NVarChar).Value = oldTagNo;
            cmdUpStock.Parameters.Add("@oldTagNo", SqlDbType.NVarChar).Value = oldTagNo;

            //cmdUpdateSample.Parameters.Add(new SqlParameter("@SampleNo", newSam.SampleNo));
            //newSam.Customer = new Customer();
            cmdUpdateSample.Parameters.Add(new SqlParameter("@CustId", newSam.Customer.ID));
            if (newSam.SampleDate == null)
            {
                cmdUpdateSample.Parameters.Add("@SampleDate", SqlDbType.DateTime);
                cmdUpdateSample.Parameters["@SampleDate"].Value = DBNull.Value;
            }
            else
                cmdUpdateSample.Parameters.Add(new SqlParameter("@SampleDate", newSam.SampleDate));

            cmdUpdateSample.Parameters.Add(new SqlParameter("@BillBookNo", newSam.BillBookNo));

            if (newSam.ReturnWt.HasValue)
                cmdUpdateSample.Parameters.Add(new SqlParameter("@ReturnWt", newSam.ReturnWt));
            else
            {
                cmdUpdateSample.Parameters.Add("@ReturnWt", SqlDbType.Float);
                cmdUpdateSample.Parameters["@ReturnWt"].Value = DBNull.Value;
            }
            if (newSam.ReturnQty.HasValue)
                cmdUpdateSample.Parameters.Add(new SqlParameter("@ReturnQty", newSam.ReturnQty));
            else
            {
                cmdUpdateSample.Parameters.Add("@ReturnQty", SqlDbType.Int);
                cmdUpdateSample.Parameters["@ReturnQty"].Value = DBNull.Value;
            }



            if (newSam.Stock.NetWeight.HasValue)
                cmdUpStock.Parameters.Add(new SqlParameter("@NetWeight", newSam.Stock.NetWeight));
            else
            {
                cmdUpStock.Parameters.Add("@NetWeight", SqlDbType.Float);
                cmdUpStock.Parameters["@NetWeight"].Value = DBNull.Value;
            }
            if (newSam.Stock.Qty.HasValue)
                cmdUpStock.Parameters.Add(new SqlParameter("@Qty", newSam.Stock.Qty));
            else
            {
                cmdUpStock.Parameters.Add("@Qty", SqlDbType.Float);
                cmdUpStock.Parameters["@Qty"].Value = DBNull.Value;
            }

            //cmdUpdateSample.Parameters.Add(new SqlParameter("@TagNo", newSam.Stock.TagNo));
            cmdUpdateSample.Parameters.Add(new SqlParameter("@Description", newSam.Description));

            //  cmdUpdateSample.Parameters.Add(new SqlParameter("@oldTagNo", oldTagNo));





            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmdUpdateSample.Transaction = tran;
                cmdUpStock.Transaction = tran;
                try
                {

                    cmdUpdateSample.ExecuteNonQuery();
                    cmdUpStock.ExecuteNonQuery();
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
        public List<Customer> GetAllSampleCustomers()
        {
            string customers = "select smp.CustId,cst.CustId,cst.Name,cst.Mobile,cst.Address,cst.Email from  Sample smp inner join CustomerInfo cst on smp.CustId=cst.CustId and smp.SampleFlag='True'";
            SqlConnection con=new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd=new SqlCommand(customers,con);
            cmd.CommandType=CommandType.Text;

            List<Customer> custs = null;

            try
            {
                con.Open();
                SqlDataReader dr=cmd.ExecuteReader();
                if(dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();
                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.Name = dr["Name"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();

                        //cust.Address = dr["Address"].ToString();
                        cust.Email = dr["Email"].ToString();
                        //cust.Customer = new Customer(Convert.ToInt32(dr["CustId"]), dr["FName"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Email"].ToString(), dr["Address"].ToString());
                        custs.Add(cust);
                    }
                    while (dr.Read());
                }
                dr.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
             finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            if(custs != null) custs.TrimExcess();
            return custs;
        }
        public List<SampleLineItem> GetSampleNoByCust(int id)
        {
            string sampleNo = "select SampleNo,SampleDate from Sample where CustId="+id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(sampleNo, con);
            cmd.CommandType = CommandType.Text;

            List<SampleLineItem> slims = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    slims = new List<SampleLineItem>();
                    if (slims == null) slims = new List<SampleLineItem>();
                    do
                    {
                        SampleLineItem sli = new SampleLineItem();

                        sli.SampleNo = Convert.ToInt32(dr["SampleNo"]);
                        sli.SampleDate = Convert.ToDateTime(dr["SampleDate"]);

                        slims.Add(sli);
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
            if (slims != null) slims.TrimExcess();
            return slims;
        }
        public Stock GetStkByTagNo(string tagno)
        {
            string GetAllStock = "select stk.*,it.ItemName,sgi.SGItmName,si.SubItmName,wr.WorkerName from Stock stk inner join Item it on it.itemId = stk.itemId left outer join SubGroupItem sgi on stk.SubGItmId=sgi.SubGItmId left outer join SubItems si on stk.SubItemId=si.SubItemId left outer join Worker wr on wr.workerid= stk.workerid where stk.Status = 'Available' and stk.TagNo='" + tagno + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(GetAllStock, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@TagNo", SqlDbType.Int).Value = tagno;
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
                    //stk.BarCode = dr["BarCode"].ToString();
                    if (dr["BarCode"] == DBNull.Value)
                    {
                        stk.BarCode = "";

                    }
                    else
                    {
                        stk.StockId = Convert.ToInt32(dr["StockId"]);
                        stk.TagNo = dr["TagNo"].ToString();
                        //stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]));
                        stk.ItemName = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());

                        if (Convert.ToDecimal(dr["NetWeight"]) == 0)
                            stk.NetWeight = 0;
                        else
                            stk.NetWeight = Convert.ToDecimal(dr["NetWeight"]);

                        stk.ItemSize = dr["ItemSize"].ToString();

                        if (Convert.ToInt32(dr["Qty"]) == 0)
                            stk.Qty = 0;
                        else
                            stk.Qty = Convert.ToInt32(dr["Qty"]);

                        if (Convert.ToInt32(dr["Pieces"]) == 0)
                            stk.Pieces = 0;
                        else
                            stk.Pieces = Convert.ToInt32(dr["Pieces"]);

                        stk.Karrat = dr["Karat"].ToString();
                        stk.Description = dr["Description"].ToString();

                        if (Convert.ToInt32(dr["WorkerId"]) == 0)
                        {
                            stk.WorkerName = new Worker();
                            stk.WorkerName.ID = 0;
                        }
                        else
                            stk.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());

                        if (Convert.ToDecimal(dr["WasteInGm"]) == 0)
                            stk.WasteInGm = 0;
                        else
                            stk.WasteInGm = Convert.ToDecimal(dr["WasteInGm"]);

                        if (Convert.ToDecimal(dr["WastePercent"]) == 0)
                            stk.WastePercent = 0;
                        else
                            stk.WastePercent = Convert.ToDecimal(dr["WastePercent"]);

                        if (Convert.ToInt32(dr["Kaat"]) == 0)
                            stk.KaatInRatti = 0;
                        else
                            stk.KaatInRatti = Convert.ToInt32(dr["Kaat"]);

                        if (Convert.ToDecimal(dr["PWeight"]) == 0)
                            stk.PWeight = 0;
                        else
                            stk.PWeight = Convert.ToDecimal(dr["PWeight"]);

                        if (Convert.ToDecimal(dr["LakerGm"]) == 0)
                            stk.LakerGm = 0;
                        else
                            stk.LakerGm = Convert.ToDecimal(dr["LakerGm"]);

                        if (Convert.ToDecimal(dr["TotalLaker"]) == 0)
                            stk.TotalLaker = 0;
                        else
                            stk.TotalLaker = Convert.ToDecimal(dr["TotalLaker"]);


                        stk.TotalWeight = Convert.ToDecimal(dr["TotalWeight"]);

                        if (Convert.ToDecimal(dr["MakingPerGm"]) == 0)
                            stk.MakingPerGm = 0;
                        else
                            stk.MakingPerGm = Convert.ToDecimal(dr["MakingPerGm"]);

                        if (Convert.ToDecimal(dr["TotalMaking"]) == 0)
                            stk.TotalMaking = 0;
                        else
                            stk.TotalMaking = Convert.ToDecimal(dr["TotalMaking"]);

                        if (Convert.ToInt32(dr["DesignId"]) == 0)
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
    }
}
