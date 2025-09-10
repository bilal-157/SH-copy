using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using BusinesEntities;


namespace DAL
{
  public  class PurchaseGoldDAL
    {
      public void AddGoldDetail(Gold gld)
      {
          string AddGold = "AddGoldDetail";
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(AddGold, con);
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
          cmd.Parameters.Add(new SqlParameter("@ItemId", gld.item.ItemId));
          cmd.Parameters.Add(new SqlParameter("@SaleManId", gld.SaleMan.ID));
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
      public void AddGoldDetail(Gold gld,SqlConnection con , SqlTransaction trans)
      {
          string AddGold = "AddGoldDetail";
          //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(AddGold, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Transaction = trans;
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
          cmd.Parameters.Add(new SqlParameter("@ItemId", gld.item.ItemId));
          cmd.Parameters.Add(new SqlParameter("@SaleManId", gld.SaleMan.ID));
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
              trans.Rollback();
              throw ex;
          }
          finally
          {
              //if (con.State == ConnectionState.Open)
              //    con.Close();
          }
      }
      public List<Gold> GetAllGold(string VType)
      {
          string getGold = "select * from GoldDetail where VNO like '" + VType + '%' + "'"+" and convert(varchar, PGDate, 112) = convert(varchar, GetDate(), 112)  Order by PGDate";
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(getGold, con);
          cmd.CommandType = CommandType.Text;
          List<Gold> lstGold = null;
          //SqlDataReader dr = null;
          try
          {
              con.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  if (lstGold == null) lstGold = new List<Gold>();
                  do
                  {
                      Gold gld = new Gold();
                      gld.SNO = Convert.ToInt32(dr["SNO"]);
                      gld.VNO = dr["VNO"].ToString();
                      gld.Weight = Convert.ToDecimal(dr["Weight"]);
                      gld.Kaat= Convert.ToDecimal(dr["Kaat"]);
                      gld.PWeight = Convert.ToDecimal(dr["PWeight"]);
                      gld.RemainingWork = dr["RemainingWork"].ToString();
                      gld.Karat = dr["Karrat"].ToString();
                      gld.Rate = Convert.ToDecimal(dr["Rate"]);
                      gld.Amount = Convert.ToDecimal(dr["Amount"]);
                      gld.Description = dr["Description"].ToString();
                      gld.PGDate = Convert.ToDateTime(dr["PGDate"]);
                      if (Convert.ToInt32( dr["GoldType"]) == 0)
                          gld.GoldType = GoldType.Pure;
                      else
                          gld.GoldType = GoldType.Used;
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

      public List<Gold> GetAllGold(string VType , SqlConnection con , SqlTransaction trans)
      {
          string getGold = "select * from GoldDetail where VNO like '" + VType + '%' + "'" + " and convert(varchar, PGDate, 112) = convert(varchar, GetDate(), 112)  Order by PGDate";
          //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(getGold, con);
          cmd.Transaction = trans;
          cmd.CommandType = CommandType.Text;
          List<Gold> lstGold = null;
          //SqlDataReader dr = null;
          try
          {
              con.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  if (lstGold == null) lstGold = new List<Gold>();
                  do
                  {
                      Gold gld = new Gold();
                      gld.SNO = Convert.ToInt32(dr["SNO"]);
                      gld.VNO = dr["VNO"].ToString();
                      gld.Weight = Convert.ToDecimal(dr["Weight"]);
                      gld.Kaat = Convert.ToDecimal(dr["Kaat"]);
                      gld.PWeight = Convert.ToDecimal(dr["PWeight"]);
                      gld.RemainingWork = dr["RemainingWork"].ToString();
                      gld.Karat = dr["Karrat"].ToString();
                      gld.Rate = Convert.ToDecimal(dr["Rate"]);
                      gld.Amount = Convert.ToDecimal(dr["Amount"]);
                      gld.Description = dr["Description"].ToString();
                      //gld.PWeight = Convert.ToDecimal(dr["PWeight"]);
                      gld.PGDate = Convert.ToDateTime(dr["PGDate"]);
                      if (Convert.ToInt32(dr["GoldType"]) == 0)
                          gld.GoldType = GoldType.Pure;
                      else
                          gld.GoldType = GoldType.Used;
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
              //if (con.State == ConnectionState.Open)
              //    con.Close();
          }
          return lstGold;
      }
      public Gold GetGoldByVNO(string vno)
      {
          string query = "select * from GoldDetail where VNO ='"+vno+"'";
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(query, con);
          cmd.CommandType = CommandType.Text;
          SqlDataReader dr = null;
          Gold gd = null;
          try
          {
              con.Open();
              dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  gd = new Gold();
                  gd.Weight = Convert.ToDecimal(dr["Weight"]);
                  gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                  gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                  gd.Rate = Convert.ToDecimal(dr["Rate"]);
                  gd.VNO = dr["VNO"].ToString();
                  gd.Karat = dr["Karrat"].ToString();
                  gd.Amount = Convert.ToDecimal(dr["Amount"]);
                  gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                  gd.CPVNO = dr["CPVNO"].ToString();
                  gd.CustId = Convert.ToInt32(dr["CustId"]);
                  gd.Description = dr["Description"].ToString();
                  if (Convert.ToInt32(dr["GoldType"]) == 0)
                      gd.GoldType = GoldType.Pure;
                  else
                      gd.GoldType = GoldType.Used;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {
              dr.Close();
              if (con.State == ConnectionState.Open)
                  con.Close();
          }
          return gd;
      }

      public Gold GetGoldByVNO(string vno,SqlConnection con, SqlTransaction trans)
      {
          string query = "select * from GoldDetail where VNO ='" + vno + "'";
          //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(query, con);
          cmd.Transaction = trans;
          cmd.CommandType = CommandType.Text;
          SqlDataReader dr = null;
          Gold gd = null;
          try
          {
              //con.Open();
              dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  gd = new Gold();
                  gd.Weight = Convert.ToDecimal(dr["Weight"]);
                  gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                  gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                  gd.Rate = Convert.ToDecimal(dr["Rate"]);
                  gd.VNO = dr["VNO"].ToString();
                  gd.Karat = dr["Karrat"].ToString();
                  gd.Amount = Convert.ToDecimal(dr["Amount"]);
                  gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                  gd.CPVNO = dr["CPVNO"].ToString();
                  gd.CustId = Convert.ToInt32(dr["CustId"]);
                  gd.Description = dr["Description"].ToString();
                  if (Convert.ToInt32(dr["GoldType"]) == 0)
                      gd.GoldType = GoldType.Pure;
                  else
                      gd.GoldType = GoldType.Used;
              }
          }
          catch (Exception ex)
          {
              throw ex;
          }
          finally
          {
              dr.Close();
              //if (con.State == ConnectionState.Open)
              //    con.Close();
          }
          return gd;
      }
      public void UpdateGold(string vn, Gold gld)
      {
          string AddGold = "UpdateGold";
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(AddGold, con);
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@oldVNO", SqlDbType.NVarChar).Value = vn;
          cmd.Parameters.Add(new SqlParameter("@SNO", gld.SNO));
          // cmd .Parameters .Add (new SqlParameter ("@ONO",gld .ONO ));
          //cmd.Parameters.Add(new SqlParameter("@VNO", gld.VNO));
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
          cmd.Parameters.Add(new SqlParameter("@CPVNO", gld.CPVNO));





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

      public void UpdateGold(string vn, Gold gld , SqlConnection con , SqlTransaction trans)
      {
          string AddGold = "UpdateGold";
          //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand(AddGold, con);
          cmd.Transaction = trans;
          cmd.CommandType = CommandType.StoredProcedure;
          cmd.Parameters.Add("@oldVNO", SqlDbType.NVarChar).Value = vn;
          cmd.Parameters.Add(new SqlParameter("@SNO", gld.SNO));
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
          cmd.Parameters.Add(new SqlParameter("@CPVNO", gld.CPVNO));

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

      public int GetMaxGPNO()
      {
          string querry = "Select MAX(GPNO) as [MaxSale] from GoldDetail";
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
      public int GetMaxGSNO()
      {
          string querry = "Select MAX(GSNO) as [MaxSale] from GoldDetail";
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
      public List<Gold> GetPurchaseByPNo(int PNo)
      {
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand("select gd.*,it.itemname,(select isnull(sum(cr),0) from vouchers where accountname='Cash In Hand' and GPNO=" + PNo + ")'Cashpayment' from GoldDetail gd left outer join Item it on it.itemid=gd.itemid where gd.GPNO <>0 and gd.GPNO=" + PNo, con);
          cmd.CommandType = CommandType.Text;
          List<Gold> custs = null;
          try
          {
              con.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  custs = new List<Gold>();
                  if (custs == null) custs = new List<Gold>();

                  do
                  {
                      Gold cust = new Gold();
                      cust.Weight = Convert.ToDecimal(dr["Weight"]);
                      cust.Kaat = Convert.ToDecimal(dr["Kaat"]);
                      cust.PWeight = Convert.ToDecimal(dr["PWeight"]);
                      cust.Karat = dr["Karrat"].ToString();
                      cust.Rate = Convert.ToDecimal(dr["Rate"]);
                      cust.Amount = Convert.ToDecimal(dr["Amount"]);
                      cust.CashPR = Convert.ToDecimal(dr["Cashpayment"]);
                      cust.Description = dr["Description"].ToString();
                      cust.GType = dr["GoldType"].ToString();
                      cust.PGDate = Convert.ToDateTime(dr["PGDate"]);
                      cust.CustId = Convert.ToInt32(dr["CustId"]);
                      cust.GPNO = Convert.ToInt32(dr["GPNO"]);
                      cust.item = new Item();
                      if (dr["ItemId"] == DBNull.Value)
                      {
                          cust.item = new Item();
                          cust.item.ItemId = 0;
                          cust.item.ItemName = "";
                      }
                      else
                      {
                          cust.item = new Item();
                          cust.item.ItemId = Convert.ToInt32(dr["ItemId"]);
                          cust.item.ItemName = (dr["ItemName"]).ToString();
                      }
                      if (dr["SaleManId"] == DBNull.Value)
                          cust.SaleManId = 0;
                      else
                          cust.SaleManId = Convert.ToInt32(dr["SaleManId"]);
                     

                      custs.Add(cust);
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
          if (custs != null) custs.TrimExcess();
          return custs;
      }

      public List<Gold> GetSaleBySNo(int SNo)
      {
          SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
          SqlCommand cmd = new SqlCommand("select gd.*,it.ItemName,(select isnull(sum(Dr),0) from vouchers where accountname='Cash In Hand' and GSNO=" + SNo + ")'Cashpayment' from GoldDetail gd left outer join Item it on it.itemid=gd.itemid where gd.GSNO <>0 and gd.GSNO=" + SNo, con);
          cmd.CommandType = CommandType.Text;
          List<Gold> custs = null;
          try
          {
              con.Open();
              SqlDataReader dr = cmd.ExecuteReader();
              if (dr.Read())
              {
                  custs = new List<Gold>();
                  if (custs == null) custs = new List<Gold>();

                  do
                  {
                      Gold cust = new Gold();
                      cust.Weight = Convert.ToDecimal(dr["Weight"]);
                      cust.Kaat = Convert.ToDecimal(dr["Kaat"]);
                      cust.PWeight = Convert.ToDecimal(dr["PWeight"]);
                      cust.Karat = dr["Karrat"].ToString();
                      cust.Rate = Convert.ToDecimal(dr["Rate"]);
                      cust.Amount = Convert.ToDecimal(dr["Amount"]);
                      cust.CashPR = Convert.ToDecimal(dr["Cashpayment"]);
                      cust.Description = dr["Description"].ToString();
                      cust.GType = dr["GoldType"].ToString();
                      cust.PGDate = Convert.ToDateTime(dr["PGDate"]);
                      cust.CustId = Convert.ToInt32(dr["CustId"]);
                      if (dr["ItemId"] == DBNull.Value)
                      {
                          cust.item = new Item();
                          cust.item.ItemId = 0;
                          cust.item.ItemName = "";
                      }
                      else
                      {
                          cust.item = new Item();
                          cust.item.ItemId = Convert.ToInt32(dr["ItemId"]);
                          cust.item.ItemName = (dr["ItemName"]).ToString();
                      }
                      if (dr["SaleManId"] == DBNull.Value)
                          cust.SaleManId = 0;
                      else
                          cust.SaleManId = Convert.ToInt32(dr["SaleManId"]);

                      custs.Add(cust);
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
          if (custs != null) custs.TrimExcess();
          return custs;
      }

      public void DeletePurchaseByPNo(int id, SqlConnection con, SqlTransaction trans)
      {

          string deleteCustomer = "Delete from vouchers where GPNO=" + id + "Delete from GoldDetail where GPNO=" + id;
          SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
          cmdDelete.CommandType = CommandType.Text;
          try
          {
              cmdDelete.Transaction = trans;


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
              // con.Close();
          }
      }

      public void DeleteSaleByPNo(int id, SqlConnection con, SqlTransaction trans)
      {

          string deleteCustomer = "Delete from vouchers where GSNO=" + id + "Delete from GoldDetail where GSNO=" + id;
          SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
          cmdDelete.CommandType = CommandType.Text;
          try
          {
              cmdDelete.Transaction = trans;


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
              // con.Close();
          }
      }
    }
}
