using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public  class StonesDAL
    {
     private   string addStoneName = "AddStoneName";
     private string addStoneColor = "AddStoneColor";
     private string addCut = "AddCut";
     private string addClearity = "AddClearity";


       public void AddStoneName(Stones stn)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this .addStoneName ,con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@StoneTypeId", stn.StoneTypeId));
           cmd.Parameters.Add(new SqlParameter("@StoneName",stn.StoneName));
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

       public void AddStoneColor( StoneColor clr)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this .addStoneColor , con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@ColorName", clr.ColorName));
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
       public bool isStNameExist(string name, int? typeId)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select stonename from stonesname where stonename='" + name + "'and StoneTypeId=" + typeId + "";
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
       public void AddCut(StoneCut scut)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this.addCut , con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@CutName",scut .CutName ));
           try
           {
               con.Open();
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
       }

       public void AddClearity(StoneClearity clearity)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this .addClearity , con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@ClearityName", clearity.ClearityName));
           try
           {
               con.Open();
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
       }

       public List<Stones> GetAllStoneName()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd=new SqlCommand ("select * from StonesName",con );
           cmd.CommandType = CommandType.Text;
           List<Stones> stone = null;
          // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stone = new List<Stones>();
                   do
                   {
                       Stones stn = new Stones();
                       stn.StoneId = Convert.ToInt32(dr["StoneId"]);
                       stn.StoneName = dr["StoneName"].ToString();
                       stone.Add(stn);
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
           if (stone != null) stone.TrimExcess();
           return stone;
       }

       public List<StoneColor > GetAllColorName()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("select * from StonesColor", con);
           cmd.CommandType = CommandType.Text;
           List<StoneColor > stoneColor = null;
           // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stoneColor  = new List<StoneColor >();
                   do
                   {
                       StoneColor  stnColor = new StoneColor ();
                       stnColor.ColorId = Convert.ToInt32(dr["ColorId"]);
                       stnColor.ColorName = dr["ColorName"].ToString();
                       stoneColor.Add(stnColor);
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
           if (stoneColor  != null) stoneColor .TrimExcess();
           return stoneColor ;
       }
      
       public List<StoneCut> GetAllCutName()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("select * from DiamondCut", con);
           cmd.CommandType = CommandType.Text;
           List<StoneCut> stoneCut = null;
           // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stoneCut = new List<StoneCut>();
                   do
                   {
                       StoneCut stnCut = new StoneCut();
                       stnCut.CutId = Convert.ToInt32(dr["CutId"]);
                       stnCut.CutName = dr["CutName"].ToString();
                       stoneCut.Add(stnCut);
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
           if (stoneCut != null) stoneCut.TrimExcess();
           return stoneCut;
       }
    
       public List<StoneClearity> GetAllClearityName()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("select * from DiamondClearity", con);
           cmd.CommandType = CommandType.Text;
           List<StoneClearity> stoneClearity = null;
           // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stoneClearity = new List<StoneClearity>();
                   do
                   {
                       StoneClearity stnClearity = new StoneClearity();
                       stnClearity.ClearityId = Convert.ToInt32(dr["ClearityId"]);
                       stnClearity.ClearityName = dr["ClearityName"].ToString();
                       stoneClearity.Add(stnClearity);
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
           if (stoneClearity != null) stoneClearity.TrimExcess();
           return stoneClearity;
       }

       public Stones GetStNameById(int id)
       {
           string getItemById = "select * from StonesName where StoneId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           Stones stn = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   stn = new Stones();
                   stn.StoneId = Convert.ToInt32(dr["StoneId"]);
                   stn.StoneName = dr["StoneName"].ToString();
                   stn.StoneTypeId = Convert.ToInt32(dr["StoneTypeId"]);
                  
               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
           return stn;
       }

       public StoneColor GetClrNameById(int id)
       {
           string getItemById = "select * from StonesColor where ColorId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           StoneColor stclr = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   stclr = new StoneColor();
                   stclr.ColorId = Convert.ToInt32(dr["ColorId"]);
                   stclr.ColorName = dr["ColorName"].ToString();

               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
           return stclr;
       }

       public StoneCut GetCutNameById(int id)
       {
           string getItemById = "select * from DiamondCut where CutId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           StoneCut stcut = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   stcut = new StoneCut();
                   stcut.CutId = Convert.ToInt32(dr["CutId"]);
                   stcut.CutName = dr["CutName"].ToString();

               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
           return stcut;
       }

       public StoneClearity GetClearityNameById(int id)
       {
           string getItemById = "select * from DiamondClearity where ClearityId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           StoneClearity stcle = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   stcle = new StoneClearity();
                   stcle.ClearityId = Convert.ToInt32(dr["ClearityId"]);
                   stcle.ClearityName = dr["ClearityName"].ToString();

               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
           return stcle;
       }

       public void UpdateStoneName(int oldId, Stones stn)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateItem = new SqlCommand("UpdateStoneName", con);
           cmdUpdateItem.CommandType = CommandType.StoredProcedure;

           cmdUpdateItem.Parameters.Add(new SqlParameter("@StoneName", stn.StoneName));
           cmdUpdateItem.Parameters.Add(new SqlParameter("@StoneTypeId", stn.StoneTypeId));

           cmdUpdateItem.Parameters.Add(new SqlParameter("@oldStoneId", oldId));


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

       public void UpdateColorName(int oldId, StoneColor stcl)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateItem = new SqlCommand("UpdateColorName", con);
           cmdUpdateItem.CommandType = CommandType.StoredProcedure;

           cmdUpdateItem.Parameters.Add(new SqlParameter("@ColorName", stcl.ColorName));


           cmdUpdateItem.Parameters.Add(new SqlParameter("@oldColorId", oldId));


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

       public void UpdateCutName(int oldId, StoneCut stct)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateItem = new SqlCommand("UpdateCutName", con);
           cmdUpdateItem.CommandType = CommandType.StoredProcedure;

           cmdUpdateItem.Parameters.Add(new SqlParameter("@CutName", stct.CutName));


           cmdUpdateItem.Parameters.Add(new SqlParameter("@oldCutId", oldId));


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

       public void UpdateClearityName(int oldId, StoneClearity stcle)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateItem = new SqlCommand("UpdateClearityName", con);
           cmdUpdateItem.CommandType = CommandType.StoredProcedure;

           cmdUpdateItem.Parameters.Add(new SqlParameter("@ClearityName", stcle.ClearityName));


           cmdUpdateItem.Parameters.Add(new SqlParameter("@oldClearityId", oldId));


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

       public List<Stones> GetAllStonesDetail(string TagNo)
       {
          // .Replace("""",""
          // TagNo=TagNo .Replace ("""",'"");
           string GetDetail = "select stone.stoneId,stone.stoneName,sd.StoneWeight,st.stoneTypeId,st.Name 'TypeName', sd.TagNo,sd.ColorName,sd.cutName,sd.ClearityName,sd.StoneWeight,sd.SQty,sd.Rate,sd.Rate,sd.Price,sd.TranId from StonesDetail sd, stonesname stone, stonestype st where stone.stoneid=sd.stoneid and stone.stonetypeid=st.stonetypeid and TagNo = '" + TagNo + "'";
           //string GetDetail = "GetStonesDetailByTagNo";
          // string GetDetail = " GetAllStonesByTag";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("StonesDetailByTagNo", con);
           //cmd.CommandType = CommandType.StoredProcedure ;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@TagNo", TagNo));
           SqlDataReader dr = null;
           List<Stones> stDetail = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   if (stDetail == null) stDetail = new List<Stones>();
                   while (dr.Read())
                   {
                       Stones std = new Stones();
                       //if (dr["StoneId"] == DBNull.Value)
                       //StoneType st;
                       /*if (dr["StoneTypeId"].ToString() == "Beeds")
                           st = StoneType.Beeds;
                       else if (dr["StoneType"].ToString() == "Diamond")
                           st = StoneType.Diamond;
                       else// if (dr["StoneType"] == "Stones")
                           st = StoneType.Stones;
                        */
                       int typeId,stoneId;
                       int.TryParse(dr["StoneTypeId"].ToString(), out typeId);
                       std.StoneTypeId = typeId;
                       std.StoneTypeName = dr["TypeName"].ToString();
                       int.TryParse(dr["StoneId"].ToString(), out stoneId);
                       std.StoneId = stoneId;
                       std.StoneName = dr["StoneName"].ToString();

                       /*if (dr["StoneName"] == DBNull.Value)
                           std.StoneName = string .Empty   ;
                       else
                       {
                          // std.StoneId = Convert.ToInt32(dr["StoneId"]);
                           std.StoneName = dr["StoneName"].ToString();
                       }*/
                       //if (dr["ColorId"] == DBNull.Value)
                       if (dr["ColorName"] == DBNull.Value)
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = string.Empty;
                           //std.ColorName.ColorId = null;
                       }
                       else
                       //std.ColorName = new StoneColor(Convert.ToInt32(dr["ColorId"]),dr["ColorName"].ToString());
                       //std.ColorName = new StoneColor( dr["ColorName"].ToString());
                       {
                           std.ColorName = new StoneColor();
                            std.ColorName .ColorName =   (dr["ColorName"].ToString());
                       }
                       //if (dr["CutId"] == DBNull.Value)
                       if (dr["CutName"] == DBNull.Value)
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = string.Empty;
                       }
                       else
                       //std.CutName = new StoneCut(Convert.ToInt32(dr["CutId"]),dr["CutName"].ToString ());
                       {
                           std.CutName = new StoneCut();
                          std.CutName .CutName =     (dr["CutName"].ToString());
                       }
                      // if (dr["ClearityId"] == DBNull.Value)
                       if (dr["ClearityName"] == DBNull.Value)
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = string .Empty ;
                       }
                       else
                       //std.ClearityName = new StoneClearity(Convert .ToInt32 (dr["ClearityId"]),dr["ClearityName"].ToString ());
                       {
                           std.ClearityName = new StoneClearity();
                           std .ClearityName .ClearityName =    ( dr["ClearityName"].ToString());
                       }
                       if (dr["StoneWeight"] == DBNull.Value)
                           std.StoneWeight = null;
                       else 
                       std.StoneWeight = Convert.ToDecimal(dr["StoneWeight"]);
                       if (dr["SQty"] == DBNull.Value)
                           std.Qty = 0;
                       else 
                       std.Qty = Convert.ToInt32(dr["SQty"]);
                       if (dr["Rate"] == DBNull.Value)
                           std.Rate = null;
                       else 
                       std.Rate = Convert.ToDecimal(dr["Rate"]);
                       if (dr["Price"] == DBNull.Value)
                           std.Price = null;
                       else 
                       std.Price = Convert.ToDecimal(dr["Price"]);
                       std.StonePrice = Convert.ToDecimal(dr["StonesPrice"]);
                       std.BeedsPrice = Convert.ToDecimal(dr["BeedsPrice"]);
                       std.DiamondPrice = Convert.ToDecimal(dr["DiamondPrice"]);
                       stDetail.Add(std);

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
           if (stDetail != null) stDetail.TrimExcess();
           return stDetail;

       }

       public List<Stones> GetAllStonesDetailforWorker(int tranid)
       {
           // .Replace("""",""
           // TagNo=TagNo .Replace ("""",'"");
           string GetDetail = "select stone.stoneId,stone.stoneName,sd.StoneWeight,st.stoneTypeId,st.Name 'TypeName', sd.TagNo,sd.ColorName,sd.cutName,sd.ClearityName,sd.StoneWeight,sd.SQty,sd.Rate,sd.Rate,sd.Price,sd.TranId from StonesDetail sd, stonesname stone, stonestype st where stone.stoneid=sd.stoneid and stone.stonetypeid=st.stonetypeid and TranId = " + tranid ;
           //string GetDetail = "GetStonesDetailByTagNo";
           // string GetDetail = " GetAllStonesByTag";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(GetDetail, con);
           //cmd.CommandType = CommandType.StoredProcedure ;
           cmd.CommandType = CommandType.Text;
           cmd.Parameters.Add(new SqlParameter("@TranId", tranid ));
           SqlDataReader dr = null;
           List<Stones> stDetail = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   if (stDetail == null) stDetail = new List<Stones>();
                   while (dr.Read())
                   {
                       Stones std = new Stones();
                       //if (dr["StoneId"] == DBNull.Value)
                       //StoneType st;
                       /*if (dr["StoneTypeId"].ToString() == "Beeds")
                           st = StoneType.Beeds;
                       else if (dr["StoneType"].ToString() == "Diamond")
                           st = StoneType.Diamond;
                       else// if (dr["StoneType"] == "Stones")
                           st = StoneType.Stones;
                        */
                       int typeId, stoneId;
                       int.TryParse(dr["StoneTypeId"].ToString(), out typeId);
                       std.StoneTypeId = typeId;
                       std.StoneTypeName = dr["TypeName"].ToString();
                       int.TryParse(dr["StoneId"].ToString(), out stoneId);
                       std.StoneId = stoneId;
                       std.StoneName = dr["StoneName"].ToString();

                       /*if (dr["StoneName"] == DBNull.Value)
                           std.StoneName = string .Empty   ;
                       else
                       {
                          // std.StoneId = Convert.ToInt32(dr["StoneId"]);
                           std.StoneName = dr["StoneName"].ToString();
                       }*/
                       //if (dr["ColorId"] == DBNull.Value)
                       if (dr["ColorName"] == DBNull.Value)
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = string.Empty;
                           //std.ColorName.ColorId = null;
                       }
                       else
                       //std.ColorName = new StoneColor(Convert.ToInt32(dr["ColorId"]),dr["ColorName"].ToString());
                       //std.ColorName = new StoneColor( dr["ColorName"].ToString());
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = (dr["ColorName"].ToString());
                       }
                       //if (dr["CutId"] == DBNull.Value)
                       if (dr["CutName"] == DBNull.Value)
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = string.Empty;
                       }
                       else
                       //std.CutName = new StoneCut(Convert.ToInt32(dr["CutId"]),dr["CutName"].ToString ());
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = (dr["CutName"].ToString());
                       }
                       // if (dr["ClearityId"] == DBNull.Value)
                       if (dr["ClearityName"] == DBNull.Value)
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = string.Empty;
                       }
                       else
                       //std.ClearityName = new StoneClearity(Convert .ToInt32 (dr["ClearityId"]),dr["ClearityName"].ToString ());
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = (dr["ClearityName"].ToString());
                       }
                       if (dr["StoneWeight"] == DBNull.Value)
                           std.StoneWeight = null;
                       else
                           std.StoneWeight = Convert.ToDecimal(dr["StoneWeight"]);
                       if (dr["SQty"] == DBNull.Value)
                           std.Qty = null;
                       else
                           std.Qty = Convert.ToInt32(dr["SQty"]);
                       if (dr["Rate"] == DBNull.Value)
                           std.Rate = null;
                       else
                           std.Rate = Convert.ToDecimal(dr["Rate"]);
                       if (dr["Price"] == DBNull.Value)
                           std.Price = null;
                       else
                           std.Price = Convert.ToDecimal(dr["Price"]);
                       stDetail.Add(std);

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
           if (stDetail != null) stDetail.TrimExcess();
           return stDetail;

       }

       public List<Stones> GetAllCostStonesDetail(string TagNo)
       {
           // .Replace("""",""
           // TagNo=TagNo .Replace ("""",'"");
           string GetDetail = "select csd.*,st.Name 'StoneType',sn.StoneName from CostingStonesDetail csd, StonesType st, StonesName sn where csd.StoneId=sn.StoneId and st.StoneTypeId=sn.StoneTypeId and TagNo = '" + TagNo + "'";
           //string GetDetail = "GetStonesDetailByTagNo";
           // string GetDetail = " GetAllStonesByTag";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(GetDetail, con);
           //cmd.CommandType = CommandType.StoredProcedure ;
           cmd.CommandType = CommandType.Text;
           cmd.Parameters.Add(new SqlParameter("@TagNo", TagNo));
           SqlDataReader dr = null;
           List<Stones> stDetail = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   if (stDetail == null) stDetail = new List<Stones>();
                   while (dr.Read())
                   {
                       Stones std = new Stones();
                       //if (dr["StoneId"] == DBNull.Value)
                       /*StoneType st;
                       if (dr["StoneType"].ToString() == "Beeds")
                           st = StoneType.Beeds;
                       else if (dr["StoneType"].ToString() == "Diamond")
                           st = StoneType.Diamond;
                       else// if (dr["StoneType"] == "Stones")
                           st = StoneType.Stones;
                       */

                       std.StoneTypeName = dr["StoneType"].ToString();
                       std.StoneTypeId = Convert.ToInt32(dr["StoneTypeId"]);
                       if (dr["StoneName"] != DBNull.Value)
                       {
                           std.StoneId = Convert.ToInt32(dr["StoneId"]);
                           std.StoneName = dr["StoneName"].ToString();
                       }
                       //if (dr["ColorId"] == DBNull.Value)
                       if (dr["ColorName"] != DBNull.Value)
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = (dr["ColorName"].ToString());
                       }
                       //if (dr["CutId"] == DBNull.Value)
                       if (dr["CutName"] == DBNull.Value)
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = string.Empty;
                       }
                       else
                       //std.CutName = new StoneCut(Convert.ToInt32(dr["CutId"]),dr["CutName"].ToString ());
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = (dr["CutName"].ToString());
                       }
                       // if (dr["ClearityId"] == DBNull.Value)
                       if (dr["ClearityName"] == DBNull.Value)
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = string.Empty;
                       }
                       else
                       //std.ClearityName = new StoneClearity(Convert .ToInt32 (dr["ClearityId"]),dr["ClearityName"].ToString ());
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = (dr["ClearityName"].ToString());
                       }
                       if (dr["StoneWeight"] == DBNull.Value)
                           std.StoneWeight = null;
                       else
                           std.StoneWeight = Convert.ToDecimal(dr["StoneWeight"]);
                       if (dr["SQty"] == DBNull.Value)
                           std.Qty = null;
                       else
                           std.Qty = Convert.ToInt32(dr["SQty"]);
                       if (dr["Rate"] == DBNull.Value)
                           std.Rate = null;
                       else
                           std.Rate = Convert.ToDecimal(dr["Rate"]);
                       if (dr["Price"] == DBNull.Value)
                           std.Price = null;
                       else
                           std.Price = Convert.ToDecimal(dr["Price"]);
                       stDetail.Add(std);

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
           if (stDetail != null) stDetail.TrimExcess();
           return stDetail;

       }

       public void DeleteStoneName(string name)
       {
           string querry = "delete from StonesName where StoneName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public void DeleteColorName(string name)
       {
           string querry = "delete from StonesColor where ColorName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public void DeleteCutName(string name)
       {
           string querry = "delete from DiamondCut where CutName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public void DeleteClearityName(string name)
       {
           string querry = "delete from DiamondClearity where ClearityName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public void DeleteStonesByTagNo(string tagNo)
       {
           string querry = "delete from StonesDetail where TagNo='"+tagNo+"'";
           SqlConnection con = new SqlConnection(DALHelper .ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public void DeleteStonesByTagNo(string tagNo, SqlConnection con,SqlTransaction tran)
       {
           string querry = "delete from StonesDetail where TagNo='" + tagNo + "'";
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

       public void DeleteCostStonesByTagNo(string tagNo)
       {
           string querry = "delete from CostingStonesDetail where TagNo='" + tagNo + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
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
               cmd.Dispose();
           }
       }

       public bool isTagNoExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select TagNo from CostingStonesDetail where TagNo='" + name + "'";
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

       public bool isStNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select stonename from stonesname where stonename='" + name + "'";
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

       public bool isClrNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ColorName from StonesColor where ColorName='" + name + "'";
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

       public bool isCutNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select CutName from DiamondCut where CutName='" + name + "'";
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

       public bool isClearityNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ClearityName from DiamondClearity where ClearityName='" + name + "'";
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

       public bool isStoneNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select StoneName from StonesName where StoneName='" + name + "'";
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

       public bool isColorNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ColorName from StonesDetail where ColorName='" + name + "'";
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

       public bool isCuttNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select CutName from StonesDetail where CutName='" + name + "'";
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

       public bool isClearNameExist(string name)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ClearityName from StonesDetail where ClearityName='" + name + "'";
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
       public List<Stones> GetAllOrderStonesDetail(string oItmId)
       {
          
           // TagNo=TagNo .Replace ("""",'"");
           string GetDetail = "select stone.stoneId,stone.stoneName,sd.StoneWeight,st.stoneTypeId,st.Name 'TypeName', sd.OItemId,sd.ColorName,sd.cutName,sd.ClearityName,sd.StoneWeight,sd.SQty,sd.Rate,sd.Rate,sd.Price from OrderStonesDetail sd, stonesname stone, stonestype st where stone.stoneid=sd.stoneid and stone.stonetypeid=st.stonetypeid and OItemId = '" + oItmId + "'";
           //string GetDetail = "select * from OrderStonesDetail where OItemId = '" + oItmId + "'";
           
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(GetDetail, con);
           //cmd.CommandType = CommandType.StoredProcedure ;
           cmd.CommandType = CommandType.Text;
           cmd.Parameters.Add(new SqlParameter("@OItemId", oItmId));
           SqlDataReader dr = null;
           List<Stones> stDetail = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
               {
                   if (stDetail == null) stDetail = new List<Stones>();
                   while (dr.Read())
                   {
                       Stones std = new Stones();
                  

                       int typeId, stoneId;
                       int.TryParse(dr["StoneTypeId"].ToString(), out typeId);
                       std.StoneTypeId = typeId;
                       std.StoneTypeName = dr["TypeName"].ToString();
                       int.TryParse(dr["StoneId"].ToString(), out stoneId);
                       std.StoneId = stoneId;
                       std.StoneName = dr["StoneName"].ToString();


                       //if (dr["ColorId"] == DBNull.Value)
                       if (dr["ColorName"] == DBNull.Value)
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = string.Empty;
                           //std.ColorName.ColorId = null;
                       }
                       else
                       //std.ColorName = new StoneColor(Convert.ToInt32(dr["ColorId"]),dr["ColorName"].ToString());
                       //std.ColorName = new StoneColor( dr["ColorName"].ToString());
                       {
                           std.ColorName = new StoneColor();
                           std.ColorName.ColorName = (dr["ColorName"].ToString());
                       }
                       //if (dr["CutId"] == DBNull.Value)
                       if (dr["CutName"] == DBNull.Value)
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = string.Empty;
                       }
                       else
                       //std.CutName = new StoneCut(Convert.ToInt32(dr["CutId"]),dr["CutName"].ToString ());
                       {
                           std.CutName = new StoneCut();
                           std.CutName.CutName = (dr["CutName"].ToString());
                       }
                       // if (dr["ClearityId"] == DBNull.Value)
                       if (dr["ClearityName"] == DBNull.Value)
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = string.Empty;
                       }
                       else
                       //std.ClearityName = new StoneClearity(Convert .ToInt32 (dr["ClearityId"]),dr["ClearityName"].ToString ());
                       {
                           std.ClearityName = new StoneClearity();
                           std.ClearityName.ClearityName = (dr["ClearityName"].ToString());
                       }
                       if (dr["StoneWeight"] == DBNull.Value)
                           std.StoneWeight = null;
                       else
                           std.StoneWeight = Convert.ToDecimal(dr["StoneWeight"]);
                       if (dr["SQty"] == DBNull.Value)
                           std.Qty = null;
                       else
                           std.Qty = Convert.ToInt32(dr["SQty"]);
                       if (dr["Rate"] == DBNull.Value)
                           std.Rate = null;
                       else
                           std.Rate = Convert.ToDecimal(dr["Rate"]);
                       if (dr["Price"] == DBNull.Value)
                           std.Price = null;
                       else
                           std.Price = Convert.ToDecimal(dr["Price"]);
                       stDetail.Add(std);

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
           if (stDetail != null) stDetail.TrimExcess();
           return stDetail;

       }
       public List<Stone> GetAllStoneTypeName()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("select * from StonesType", con);
           cmd.CommandType = CommandType.Text;
           List<Stone> stone = null;
           // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stone = new List<Stone>();
                   do
                   {
                       Stone stn = new Stone();
                       stn.TypeId = Convert.ToInt32(dr["StoneTypeId"]);
                       stn.TypeName = dr["Name"].ToString();
                       stone.Add(stn);
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
           if (stone != null) stone.TrimExcess();
           return stone;
       }
      
       public List<Stone> GetAllStoneNamebyId(int stypeid)
       {
           string query = "Select * from StonesName where StoneTypeId= '" + stypeid + "' order by stonename";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.Text;
           List<Stone> stone = null;
           // SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();

               if (dr.Read())
               {
                   stone = new List<Stone>();
                   do
                   {
                       Stone stn = new Stone();
                       stn.Id = Convert.ToInt32(dr["StoneId"]);
                       stn.Name = dr["StoneName"].ToString();
                       stone.Add(stn);
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
           if (stone != null) stone.TrimExcess();
           return stone;
       }
       public decimal GetAvailabaleStoneweightbyStoneId(int stoneid)
       {
           string query = "Select StoneWeight from StonesName where stoneid =" + stoneid;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.Text;
           decimal stoneweight = 0;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
                   stoneweight = Convert.ToDecimal(dr["StoneWeight"]);
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
           return stoneweight;
       }
       public void UpdateStoneweight(int stoneid, decimal stoneweight)
       {
           string query = "Update StonesName set StoneWeight =" + stoneweight + " where Stoneid = " + stoneid;
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
       public void UpDateOrderEstimate(string oldItmId)
       {


           SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateEmploye = new SqlCommand("update OrderEstimate set Status='Completed' where OItemId='" + oldItmId + "'", conn);
           cmdUpdateEmploye.CommandType = CommandType.Text;

           //cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Weight", newsid.Weight));
           //cmdUpdateEmploye.Parameters.Add(new SqlParameter("@oldSubItemId", oldsid));

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

       public bool isStoneIdExistinStonesDetail(int id)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select StoneId from StonesDetail where StoneId=" +id;
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

       public bool isStoneIdExistCostingStonesDetail(int id)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select StoneId from CostingStonesDetail where StoneId=" + id;
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

       public void DeleteStonesByTransId(int transid, SqlConnection con, SqlTransaction tran)
       {
           string querry = "delete from StonesDetail where TranId=" + transid;
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

       public List<Stones> GetAllStoneNameById(int id)
       {
           string query = "Select * From StonesName where StoneTypeId= " + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);

           List<Stones> cnts = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   cnts = new List<Stones>();
                   if (cnts == null) cnts = new List<Stones>();

                   do
                   {
                       Stones cn = new Stones();
                       cn.StoneTypeId = Convert.ToInt32(dr["StoneTypeId"]);
                       cn.StoneName = dr["StoneName"].ToString();

                       cnts.Add(cn);
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
           if (cnts != null) cnts.TrimExcess();
           return cnts;
       }


    }
}
