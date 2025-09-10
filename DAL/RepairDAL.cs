using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;



namespace DAL
{
    public class RepairDAL
    {
        //Reparing rep = new Reparing();
        JewelConnection conn;
        public void AddOrder(Reparing rep)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdRepaird = new SqlCommand("", con);
            cmdRepaird.CommandType = CommandType.StoredProcedure;
        }

        public int GetRepairId()
        {
            conn = new JewelConnection();
            int RepairId = 0;
            try
            {
                conn.MyDataSet.Tables["TableRepair"].Rows.Clear();
            }
            catch (Exception)
            { }
            try
            {
                string querry = "Select MAX(RepairId) as [RepairId] from tblRepair";
                conn.GetDataFromJMDB(querry, "TableRepair");
                RepairId = int.Parse(conn.MyDataSet.Tables["TableRepair"].Rows[0]["RepairId"].ToString());
            }
            catch (Exception)
            { }
            return RepairId;
        }
            //------------- for saving in database------------------------///////////
        public void AddReparing(Reparing rep)
            
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);

            string SaveQuery = " insert into tblRepair values ('" + rep.RepairId
                                                   + "','" + rep.CustId
                                                   + "','" + rep.ReceiveDate
                                                   + "','" + rep.GivenDate
                                                   + "','" + rep.TotalRepairCost
                                                   + "','" + rep.Advance
                                                   + "','" + rep.Status 
                                                   + "','" + "Reparing"
                                                   + "','" + rep.Discount
                                                   + "','" + rep.Remaining
                                                   + "','" + rep.BillBookNo
                                                   + "','" + rep.WorkerId
                                                   + "','" + rep.SaleManId
                                                   + "')";
            SqlCommand cmdRepair = new SqlCommand(SaveQuery, con);
            cmdRepair.CommandType = CommandType.Text;

            SqlCommand cmd = new SqlCommand("AddRepairDetail", con);

            /// / repair detail parameters daclartion ////////////////////////////
            cmd.Parameters.Add(new SqlParameter("@RepairId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ReceiveWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@RepairWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Karat", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@GoldRate", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Lacker", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Making", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@WorkerId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@StonePrice", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairCharges", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@PerItemCost", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairingStatus", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ItemStatus", SqlDbType.NVarChar));
            cmd.CommandType = CommandType.StoredProcedure;
            //----------------------------------------------------------------///
            ///  stone par///

            SqlCommand cmdStone = new SqlCommand("AddStoneDetail", con);
            cmdStone.Parameters.Add(new SqlParameter("@TagNo", SqlDbType.NVarChar));
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
            //-            end //






            //-/////Execution area ///////////////////////////////

            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            cmd.Transaction = tran;
            cmdRepair.Transaction = tran;
            cmdStone.Transaction = tran;
            try
            {
                
                //cmd             SaleOrd.ExecuteNonQuery();

                cmdRepair.ExecuteNonQuery();

                foreach (RepairLineItem rli in rep.RepairlineItem)
                {
                    cmd.Parameters["@RepairId"].Value = rli.RepairId;
                    cmd.Parameters["@ItemId"].Value = rli.ItemId;
                    cmd.Parameters["@ReceiveWeight"].Value = rli.ReceiveWeight;
                    cmd.Parameters["@Qty"].Value = rli.Qty;
                    cmd.Parameters["@ItemName"].Value = rli.ItemName;
                    cmd.Parameters["@Description"].Value = rli.Description;
                    cmd.Parameters["@RepairWeight"].Value = rli.RepairWeight;
                    cmd.Parameters["@Karat"].Value = rli.Karat;
                    cmd.Parameters["@GoldRate"].Value = rli.GoldRate;
                    cmd.Parameters["@Lacker"].Value = rli.Lacker;
                    cmd.Parameters["@RepairingStatus"].Value = rli.RepairingStatus;
                    cmd.Parameters["@ItemStatus"].Value = rli.ItemStatus;
                    cmd.Parameters["@Making"].Value = rli.Making;
                    if (rli.WorkerId == null)
                        cmdStone.Parameters["@WorkerId"].Value = 0;
                    else
                        cmd.Parameters["@WorkerId"].Value = rli.WorkerId;
                    cmd.Parameters["@StonePrice"].Value = rli.StonePrice;
                    cmd.Parameters["@RepairCharges"].Value = rli.RepairCharges;
                    cmd.Parameters["@PerItemCost"].Value = rli.PerItemCost;

                    cmd.ExecuteNonQuery();
                    if (rli.StoneList == null)
                    {
                        //cmdStone.Parameters["@TagNo"].Value = DBNull.Value;
                        //cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                        //cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                        //cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                        //cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                        //cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                        //cmdStone.Parameters["@Price"].Value = DBNull.Value;
                        //cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                        //cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                        //cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                        //cmdStone.ExecuteNonQuery();

                    }
                    else
                    {

                        foreach (Stones st in rli.StoneList)
                        {
                            cmdStone.Parameters["@TagNo"].Value = rli.ItemId;
                            cmdStone.Parameters["@StoneTypeId"].Value = st.StoneTypeId;
                            cmdStone.Parameters["@StoneId"].Value = st.StoneId;
                            cmdStone.Parameters["@StoneWeight"].Value = st.StoneWeight;
                            cmdStone.Parameters["@SQty"].Value = st.Qty;
                            cmdStone.Parameters["@Rate"].Value = st.Rate;
                            cmdStone.Parameters["@Price"].Value = st.Price;
                            if (st.ColorName == null)
                                cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ColorName"].Value = st.ColorName;
                            if (st.CutName == null)
                                cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@CutName"].Value = st.CutName;
                            if (st.ClearityName == null)
                                cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ClearityName"].Value = st.ClearityName;
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
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            //-------------------------end of Execution area-----------------------------------------//
        }

        public List<string> GetAllItemIdByRepairId(int id)
        {
            string getRecord = "select ItemId from tblRepairdetail where RepairId = " + id;
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
                        string str = dr["ItemId"].ToString();
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

        public RepairLineItem GetComRecByRItemId(string rNo)
        {
            //string gcrecord = "select * from Costing where bFlag='true'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select  repd.*,Wrk.WorkerName from tblRepairDetail repd,Worker wrk where  repd.itemId='"+rNo+"' and repd.WorkerId = wrk.WorkerId", con);

            cmd.CommandType = CommandType.Text;
            RepairLineItem rep = null;
            
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    rep = new RepairLineItem();
                    rep.RepairId = Convert.ToInt32(dr["RepairId"]);
                    rep.ItemId = dr["ItemId"].ToString();
                    rep.ReceiveWeight = Convert.ToDecimal(dr["ReceiveWeight"]);
                    rep.Qty = Convert.ToInt32(dr["Qty"]);
                    rep.ItemName = dr["ItemName"].ToString();
                    rep.Description = dr["Description"].ToString();
                    rep.RepairWeight = Convert.ToDecimal(dr["RepairWeight"]);
                    rep.Karat = dr["Karat"].ToString();
                    rep.GoldRate = Convert.ToDecimal(dr["GoldRate"]);
                    rep.Lacker = Convert.ToDecimal(dr["Lacker"]);
                    rep.Making = Convert.ToDecimal(dr["Making"]);
                    rep.StonePrice = Convert.ToDecimal(dr["StonePrice"]);
                    rep.WorkerId = Convert.ToInt32(dr["WorkerId"]);
                    if (dr["WorkerId"] == DBNull.Value)
                    {
                        rep.WorkerName = new Worker();
                        rep.WorkerName.ID = null;
                    }
                    //else
                        rep.WorkerName = new Worker(Convert.ToInt32(dr["WorkerId"]), dr["WorkerName"].ToString());
                    rep .RepairCharges =Convert .ToDecimal (dr ["RepairCharges"]);
                    rep.PerItemCost = Convert.ToDecimal(dr["PerItemCost"]);

                    rep.StoneList = new List<Stones>();
                    StonesDAL sDal = new StonesDAL();

                    rep.StoneList = sDal.GetAllStonesDetail(rep.ItemId);
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
            return rep;
        }

        public Reparing GetRepairByRepairNo(int repairNo)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select cus.*,rep.* from tblrepair rep inner join CustomerInfo cus on rep.CustId = cus.CustId where repairId =" + repairNo, con);
            cmd.CommandType = CommandType.Text;
            Reparing rep = null;
            //OrderEstimat ord = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    rep = new Reparing();// new OrderEstimat();
                    //if (dr["BillBookNo"] == DBNull.Value)
                      //  ord.BillBookNo = null;
                    //else
                      //  ord.BillBookNo = dr["BillBookNo"].ToString();
                    //ord.SVNO = dr["VNO"].ToString();
                    rep.RepairId = Convert.ToInt32(dr["RepairId"]);
                    rep.ReceiveDate = Convert.ToDateTime(dr["ReceivedDate"]);
                    rep.GivenDate = Convert.ToDateTime(dr["DeliveryDate"]);
                    rep.TotalRepairCost = Convert.ToDecimal(dr["TotalRepairCost"]);
                    rep.Advance = Convert.ToDecimal(dr["Advance"]);
                    rep.CustId = Convert.ToInt32(dr["CustId"]);
                    rep.CustName = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["AccountCode"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Address"].ToString());

                    //string strTag = ItemId;

                    foreach (string strTag in GetAllItemIdByRepairId((int) rep.RepairId ))
                    {

                        rep.AddRLineItems(GetComRecByRItemId(strTag));
                      //ord.AddLineItems(GetComRecByOItemId(strTag));
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

            return rep;

        }

        public Reparing GetRepairByRepairNo1(int repairNo, string ItemId)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select cus.*,rep.* from tblrepair rep inner join CustomerInfo cus on rep.CustId = cus.CustId where repairId =" + repairNo, con);
            cmd.CommandType = CommandType.Text;
            Reparing rep = null;
            //OrderEstimat ord = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    rep = new Reparing();// new OrderEstimat();
                    //if (dr["BillBookNo"] == DBNull.Value)
                    //  ord.BillBookNo = null;
                    //else
                    //  ord.BillBookNo = dr["BillBookNo"].ToString();
                    //ord.SVNO = dr["VNO"].ToString();
                    rep.RepairId = Convert.ToInt32(dr["RepairId"]);
                    rep.ReceiveDate = Convert.ToDateTime(dr["ReceivedDate"]);
                    rep.GivenDate = Convert.ToDateTime(dr["DeliveryDate"]);
                    rep.TotalRepairCost = Convert.ToDecimal(dr["TotalRepairCost"]);
                    rep.Advance = Convert.ToDecimal(dr["Advance"]);
                    rep.CustId = Convert.ToInt32(dr["CustId"]);
                    rep.BillBookNo = dr["BillBookNo"].ToString();
                    rep.SaleManId = Convert.ToInt32(dr["SaleManId"]);
                    rep.WorkerId = Convert.ToInt32(dr["WorkerId"]);
                    rep.CustName = new Customer(Convert.ToInt32(dr["CustId"]), dr["Name"].ToString(), dr["AccountCode"].ToString(), dr["Mobile"].ToString(), dr["TelHome"].ToString(), dr["Address"].ToString());

                    string strTag = ItemId;

                    //foreach (string strTag in GetAllItemIdByRepairId((int) rep.RepairId ))
                    //{

                    rep.AddRLineItems(GetComRecByRItemId(strTag));
                    //ord.AddLineItems(GetComRecByOItemId(strTag));
                    //}

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

            return rep;

        }

        public void UpDateReparing(Reparing rep)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);

            //string SaveQuery = " insert into tblRepair values ('" + rep.CustId
            //                                       + "','" + rep.ReceiveDate
            //                                       + "','" + rep.GivenDate
            //                                       + "','" + rep.TotalRepairCost
            //                                       + "','" + rep.Advance
            //                                       + "','" + rep.Status
            //                                       + "')";

            string UpdateQuery = "update  tblRepair set CustId = '" + rep.CustId
                                              + "', ReceivedDate ='" + rep.ReceiveDate
                                              + "' , DeliveryDate ='" + rep.GivenDate
                                              + "' , TotalRepairCost = '" + rep.TotalRepairCost
                                              + "',Advance ='" + rep.Advance
                                              + "', Status = 'Deliverd' where RepairId = '" + rep.RepairId + "'";




            SqlCommand cmdRepair = new SqlCommand(UpdateQuery, con);
            cmdRepair.CommandType = CommandType.Text;

            SqlCommand cmd = new SqlCommand("UpdateRepairDetail", con);

            /// / repair detail parameters daclartion ////////////////////////////
            //cmd.Parameters.Add(new SqlParameter("@RepairId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ReceiveWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@RepairWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Karat", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@GoldRate", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Lacker", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Making", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@WorkerId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@StonePrice", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairCharges", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@PerItemCost", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairingStatus", SqlDbType.NVarChar));
            cmd.CommandType = CommandType.StoredProcedure;
            //----------------------------------------------------------------///

            ///  stone par///

            SqlCommand cmdStone = new SqlCommand("UpdateStone", con);
            cmdStone.Parameters.Add(new SqlParameter("@OldTagNo", SqlDbType.NVarChar));
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
            //-            end //






            //-/////Execution area ///////////////////////////////

            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            cmd.Transaction = tran;
            cmdRepair.Transaction = tran;
            cmdStone.Transaction = tran;
            try
            {

                //cmd             SaleOrd.ExecuteNonQuery();

                cmdRepair.ExecuteNonQuery();

                foreach (RepairLineItem rli in rep.RepairlineItem)
                {
                    //cmd.Parameters["@RepairId"].Value = rli.RepairId;
                    cmd.Parameters["@ItemId"].Value = rli.ItemId;
                    cmd.Parameters["@ReceiveWeight"].Value = rli.ReceiveWeight;
                    cmd.Parameters["@ItemName"].Value = rli.ItemName;
                    cmd.Parameters["@Description"].Value = rli.Description;
                    cmd.Parameters["@RepairWeight"].Value = rli.RepairWeight;
                    cmd.Parameters["@Karat"].Value = rli.Karat;
                    cmd.Parameters["@GoldRate"].Value = rli.GoldRate;
                    cmd.Parameters["@Lacker"].Value = rli.Lacker;
                    cmd.Parameters["@RepairingStatus"].Value = "Repaired";
                    cmd.Parameters["@Making"].Value = rli.Making;
                    if (rli.WorkerId == null)
                        cmdStone.Parameters["@WorkerId"].Value = 0;
                    else
                        cmd.Parameters["@WorkerId"].Value = rli.WorkerId;
                    cmd.Parameters["@StonePrice"].Value = rli.StonePrice;
                    cmd.Parameters["@RepairCharges"].Value = rli.RepairCharges;
                    cmd.Parameters["@PerItemCost"].Value = rli.PerItemCost;

                    cmd.ExecuteNonQuery();
                    if (rli.StoneList == null)
                    {
                        cmdStone.Parameters["@OldTagNo"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                        cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                        cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                        cmdStone.Parameters["@Price"].Value = DBNull.Value;
                        cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                        cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                        cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                        cmdStone.ExecuteNonQuery();

                    }
                    else
                    {

                        foreach (Stones st in rli.StoneList)
                        {
                            cmdStone.Parameters["@OldTagNo"].Value = rli.ItemId;
                            cmdStone.Parameters["@StoneTypeId"].Value = st.StoneTypeId;
                            cmdStone.Parameters["@StoneId"].Value = st.StoneId;
                            cmdStone.Parameters["@StoneWeight"].Value = st.StoneWeight;
                            cmdStone.Parameters["@SQty"].Value = st.Qty;
                            cmdStone.Parameters["@Rate"].Value = st.Rate;
                            cmdStone.Parameters["@Price"].Value = st.Price;
                            if (st.ColorName == null)
                                cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ColorName"].Value = st.ColorName.ColorName ;
                            if (st.CutName == null)
                                cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@CutName"].Value = st.CutName.CutName ;
                            if (st.ClearityName == null)
                                cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ClearityName"].Value = st.ClearityName.ClearityName ;
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
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            //-------------------------end of Execution area-----------------------------------------//
        }

        public void ReciveFromWorker(Reparing rep)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
      
            string UpdateQuery = "update  tblRepair set CustId = '" + rep.CustId
                                              + "', ReceivedDate ='" + rep.ReceiveDate
                                              + "' , DeliveryDate ='" + rep.GivenDate
                                              + "' , TotalRepairCost = '" + rep.TotalRepairCost
                                              + "',Advance ='" + rep.Advance
                                              + "', Status = 'Not Deliverd' where RepairId = '" + rep.RepairId + "'";




            SqlCommand cmdRepair = new SqlCommand(UpdateQuery, con);
            cmdRepair.CommandType = CommandType.Text;

            SqlCommand cmd = new SqlCommand("UpdateRepairDetail", con);

            /// / repair detail parameters daclartion ////////////////////////////
            //cmd.Parameters.Add(new SqlParameter("@RepairId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ReceiveWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@RepairWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Karat", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@GoldRate", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Lacker", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Making", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@WorkerId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@StonePrice", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairCharges", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@PerItemCost", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairingStatus", SqlDbType.NVarChar));
            cmd.CommandType = CommandType.StoredProcedure;
            //----------------------------------------------------------------///

            ///  stone par///

            SqlCommand cmdStone = new SqlCommand("UpdateStone", con);
            cmdStone.Parameters.Add(new SqlParameter("@OldTagNo", SqlDbType.NVarChar));
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
            //-            end //






            //-/////Execution area ///////////////////////////////

            con.Open();
            SqlTransaction tran = con.BeginTransaction();

            cmd.Transaction = tran;
            cmdRepair.Transaction = tran;
            cmdStone.Transaction = tran;
            try
            {

                //cmd             SaleOrd.ExecuteNonQuery();

                cmdRepair.ExecuteNonQuery();

                foreach (RepairLineItem rli in rep.RepairlineItem)
                
                {
                    //cmd.Parameters["@RepairId"].Value = rli.RepairId;
                    cmd.Parameters["@ItemId"].Value = rli.ItemId;
                    cmd.Parameters["@ReceiveWeight"].Value = rli.ReceiveWeight;
                    cmd.Parameters["@ItemName"].Value = rli.ItemName;
                    cmd.Parameters["@Description"].Value = rli.Description;
                    cmd.Parameters["@RepairWeight"].Value = rli.RepairWeight;
                    cmd.Parameters["@Karat"].Value = rli.Karat;
                    cmd.Parameters["@GoldRate"].Value = rli.GoldRate;
                    cmd.Parameters["@Lacker"].Value = rli.Lacker;
                    cmd.Parameters["@RepairingStatus"].Value = "Repaired";
                    cmd.Parameters["@Making"].Value = rli.Making;
                    if (rli.WorkerId == null)
                        cmdStone.Parameters["@WorkerId"].Value = 0;
                    else
                        cmd.Parameters["@WorkerId"].Value = rli.WorkerId;
                    cmd.Parameters["@StonePrice"].Value = rli.StonePrice;
                    cmd.Parameters["@RepairCharges"].Value = rli.RepairCharges;
                    cmd.Parameters["@PerItemCost"].Value = rli.PerItemCost;

                    cmd.ExecuteNonQuery();
                    if (rli.StoneList == null)
                    {
                        cmdStone.Parameters["@OldTagNo"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                        cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                        cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                        cmdStone.Parameters["@Price"].Value = DBNull.Value;
                        cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                        cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                        cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                        cmdStone.ExecuteNonQuery();

                    }
                    else
                    {

                        foreach (Stones st in rli.StoneList)
                        {
                            cmdStone.Parameters["@OldTagNo"].Value = rli.ItemId;
                            cmdStone.Parameters["@StoneTypeId"].Value = st.StoneTypeId;
                            cmdStone.Parameters["@StoneId"].Value = st.StoneId;
                            cmdStone.Parameters["@StoneWeight"].Value = st.StoneWeight;
                            cmdStone.Parameters["@SQty"].Value = st.Qty;
                            cmdStone.Parameters["@Rate"].Value = st.Rate;
                            cmdStone.Parameters["@Price"].Value = st.Price;
                            if (st.ColorName == null)
                                cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ColorName"].Value = st.ColorName.ColorName ;
                            if (st.CutName == null)
                                cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@CutName"].Value = st.CutName.CutName ;
                            if (st.ClearityName == null)
                                cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ClearityName"].Value = st.ClearityName.ClearityName ;
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
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            //-------------------------end of Execution area-----------------------------------------//
        }

        public void DeliverToCustomer(Reparing rep, SqlConnection con, SqlTransaction tran)
        {
            string UpdateQuery = "update  tblRepair set CustId = '" + rep.CustId
                                              + "', ReceivedDate ='" + rep.ReceiveDate
                                              + "' , DeliveryDate ='" + rep.GivenDate
                                              + "' , TotalRepairCost = '" + rep.TotalRepairCost
                                              + "' , Discount = '" + rep.Discount
                                              + "' , Remaining = '" + rep.Remaining
                                              + "',Advance ='" + rep.Advance
                                              + "', Status = 'Deliverd' where RepairId = '" + rep.RepairId + "'";

            SqlCommand cmdRepair = new SqlCommand(UpdateQuery, con);
            cmdRepair.CommandType = CommandType.Text;

            SqlCommand cmd = new SqlCommand("UpdateRepairDetail", con);
            cmd.Parameters.Add(new SqlParameter("@ItemId", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@ReceiveWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@RepairWeight", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Karat", SqlDbType.NVarChar));
            cmd.Parameters.Add(new SqlParameter("@GoldRate", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Lacker", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@Making", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@WorkerId", SqlDbType.Int));
            cmd.Parameters.Add(new SqlParameter("@StonePrice", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairCharges", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@PerItemCost", SqlDbType.Float));
            cmd.Parameters.Add(new SqlParameter("@RepairingStatus", SqlDbType.NVarChar));
            cmd.CommandType = CommandType.StoredProcedure;
            
            SqlCommand cmdStone = new SqlCommand("UpdateStone", con);
            cmdStone.Parameters.Add(new SqlParameter("@OldTagNo", SqlDbType.NVarChar));
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
            
            cmd.Transaction = tran;
            cmdRepair.Transaction = tran;
            cmdStone.Transaction = tran;
            try
            {
                cmdRepair.ExecuteNonQuery();
                foreach (RepairLineItem rli in rep.RepairlineItem)
                {
                    cmd.Parameters["@ItemId"].Value = rli.ItemId;
                    cmd.Parameters["@ReceiveWeight"].Value = rli.ReceiveWeight;
                    cmd.Parameters["@ItemName"].Value = rli.ItemName;
                    cmd.Parameters["@Description"].Value = rli.Description;
                    cmd.Parameters["@RepairWeight"].Value = rli.RepairWeight;
                    cmd.Parameters["@Karat"].Value = rli.Karat;
                    cmd.Parameters["@GoldRate"].Value = rli.GoldRate;
                    cmd.Parameters["@Lacker"].Value = rli.Lacker;
                    cmd.Parameters["@RepairingStatus"].Value = "Repaired";
                    cmd.Parameters["@Making"].Value = rli.Making;
                    if (rli.WorkerId == null)
                        cmdStone.Parameters["@WorkerId"].Value = 0;
                    else
                        cmd.Parameters["@WorkerId"].Value = rli.WorkerId;
                    cmd.Parameters["@StonePrice"].Value = rli.StonePrice;
                    cmd.Parameters["@RepairCharges"].Value = rli.RepairCharges;
                    cmd.Parameters["@PerItemCost"].Value = rli.PerItemCost;
                    cmd.ExecuteNonQuery();

                    if (rli.StoneList == null)
                    {
                        cmdStone.Parameters["@OldTagNo"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneTypeId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneId"].Value = DBNull.Value;
                        cmdStone.Parameters["@StoneWeight"].Value = DBNull.Value;
                        cmdStone.Parameters["@SQty"].Value = DBNull.Value;
                        cmdStone.Parameters["@Rate"].Value = DBNull.Value;
                        cmdStone.Parameters["@Price"].Value = DBNull.Value;
                        cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                        cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                        cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                        cmdStone.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (Stones st in rli.StoneList)
                        {
                            cmdStone.Parameters["@OldTagNo"].Value = rli.ItemId;
                            cmdStone.Parameters["@StoneTypeId"].Value = st.StoneTypeId;
                            cmdStone.Parameters["@StoneId"].Value = st.StoneId;
                            cmdStone.Parameters["@StoneWeight"].Value = st.StoneWeight;
                            cmdStone.Parameters["@SQty"].Value = st.Qty;
                            cmdStone.Parameters["@Rate"].Value = st.Rate;
                            cmdStone.Parameters["@Price"].Value = st.Price;
                            if (st.ColorName == null)
                                cmdStone.Parameters["@ColorName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ColorName"].Value = st.ColorName.ColorName;
                            if (st.CutName == null)
                                cmdStone.Parameters["@CutName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@CutName"].Value = st.CutName.CutName;
                            if (st.ClearityName == null)
                                cmdStone.Parameters["@ClearityName"].Value = DBNull.Value;
                            else
                                cmdStone.Parameters["@ClearityName"].Value = st.ClearityName.ClearityName;
                            cmdStone.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tran.Rollback();
                con.Close();
                throw ex;
            }
        }

        public void GetRepaired()
        {
        }

        public bool CheckRepairId(int repId)
        { 
            string query ="select repairid from tblrepairdetail where repairingstatus ='Reparing' and Repairid = "+repId+"";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            bool bflag = true;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                    bflag = false;
                else
                    bflag = true;
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
            return bflag;
        }

        public void UpdateRepairIdStatus(int repId)
        {
            string query = "update tblrepair set RStatus ='Repaired'  where Repairid = " + repId + "";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            //bool bflag = true;
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




    }
}

    