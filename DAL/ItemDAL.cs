using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using BusinesEntities;


namespace DAL
{
   public  class ItemDAL
    {
       private string AllItems = "select * from Item Order by ItemName";
       private string AllSubGroupItems = "getAllSubGroupItems";
       private string AllSubItems = "GetAllSubItems";
       private string getMaxCode = "select MAX(ItemId) as [MaxCode] from Item";


       public void AddItem(Item itm)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("AddItem", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@ItemId", itm.ItemId));
           cmd.Parameters.Add(new SqlParameter("@ItemName", itm.ItemName));
           cmd.Parameters.Add(new SqlParameter("@Abrivation", itm.Abrivation));

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
                    if (con.State == ConnectionState.Open) con.Close();
                }

           //cmd .Parameters.Add (new SqlParameter ("@TagNo",stockItem .TagNo ));
           //cmd.Parameters .Add (new SqlParameter ("@BarCode",stockItem .BarCode ));
           //cmd
           
	
       }

       public List<Item> GetAllItemsByName(string name)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("select * from Item  where ItemName like'%" + name + "%' Order by ItemName ", con);
           cmd.CommandType = CommandType.Text;

           List<Item> itms = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   itms = new List<Item>();
                   if (itms == null) itms = new List<Item>();

                   do
                   {
                       Item itm = new Item();
                       itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                       itm.ItemName = dr["ItemName"].ToString();
                       itm.Abrivation = dr["Abrivation"].ToString();
                       itms.Add(itm);
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
           if (itms != null) itms.TrimExcess();
           return itms;
       }

       public List<Item> GetAllItems()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this.AllItems ,con);
           cmd.CommandType = CommandType.Text;
          
           List<Item> itms = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   itms = new List<Item>();
                   if (itms == null) itms = new List<Item>();

                   do
                   {
                       Item itm = new Item();
                       itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                       itm.ItemName = dr["ItemName"].ToString();
                       itm.Abrivation = dr["Abrivation"].ToString();
                       itms.Add(itm);
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
           if (itms != null) itms.TrimExcess();
           return itms;
       }

       public void GetItemById(int id)
       {
           string getItemById = "select * from Item where ItemId="+id ;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           Item itm = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   itm = new Item(Convert.ToInt32(dr["ItemId"]), dr["ItemName"].ToString());


               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
 
       }
       
       public int GetMaxCode()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this.getMaxCode, con);
           cmd.CommandType = CommandType.Text;
           //Employee employee = null;
           int itemCode = 0;
           con.Open();
           try
           {
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
               if (dr.Read())
               {
                   itemCode = Convert.ToInt32(dr["MaxCode"]);
               }
               dr.Close();
           }
           catch (Exception ex)
           {
               string a = ex.Message;
           }
           finally
           {
               con.Close();
           }
           return itemCode;
       }
       public List<Item> GetAllItemByType(string type)
       {
         // const  string query = "select ItemId from Stock Where IType like'"+type+'"";
           string query = "GetAllItemByType";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.StoredProcedure ;
           cmd.Parameters.Add("@IType", SqlDbType.NVarChar).Value = type;
           List<Item> itms = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   itms = new List<Item>();
                   if (itms == null) itms = new List<Item>();

                   do
                   {
                       Item itm = new Item();
                       itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                       itm.ItemName = dr["ItemName"].ToString();
                       //itm.Abrivation = dr["Abrivation"].ToString();
                       itms.Add(itm);
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
           if (itms != null) itms.TrimExcess();
           return itms;

       }
       
       public List<WeightLineItem> GetAllWeights(int sitmid, int itmid)
       {
           string getWeight = "select * from Weight where ItemId='" + itmid + "'and SubItemId='" + sitmid + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getWeight, con);
           cmd.CommandType = CommandType.Text;
           List<WeightLineItem> wlis = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //  subItms  = new List<SubItem>();
                   if (wlis == null) wlis = new List<WeightLineItem>();

                   do
                   {
                       WeightLineItem wli = new WeightLineItem();
                       wli.ItemId = new Item(Convert.ToInt32(dr["ItemId"]));
                       wli.TagNo = dr["TagNo"].ToString();
                       if (dr["Weight"] == DBNull.Value)
                           wli.Weight = 0;
                       else 
                       wli.Weight= Convert.ToDecimal( dr["Weight"]);

                       wlis.Add(wli);
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
           if (wlis != null) wlis.TrimExcess();
           return wlis;


       }
       
       public List<WeightLineItem> GetAllWeightByTagNo(string tagNo,int itmid)
       {
           string getWeight = "select Weight from Weight where TagNo='" + tagNo + "'and ItemId='" + itmid + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getWeight, con);
           cmd.CommandType = CommandType.Text;
           List<WeightLineItem> wlis = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //  subItms  = new List<SubItem>();
                   if (wlis == null) wlis = new List<WeightLineItem>();

                   do
                   {
                       WeightLineItem wli = new WeightLineItem();
                       if (dr["Weight"] == DBNull.Value)
                           wli.Weight = 0;
                       else
                           wli.Weight = Convert.ToDecimal(dr["Weight"]);

                       wlis.Add(wli);
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
           if (wlis != null) wlis.TrimExcess();
           return wlis;


       }
       public WeightLineItem GetWeightByTagNo(string tagNo, int sitmid)
       {
           string getWeight = "select Weight from Weight where TagNo='" + tagNo + "'and SubItemId='" + sitmid + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getWeight, con);
           cmd.CommandType = CommandType.Text;
          WeightLineItem wli = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //  subItms  = new List<SubItem>();
                   if (wli == null) wli = new WeightLineItem();

               
                      
                       //wli.ItemId = new Item(Convert.ToInt32(dr["ItemId"]));
                       ////SubGroupItem sgItm = new SubGroupItem();
                       //wli.TagNo = dr["TagNo"].ToString();
                       //wli.SID = Convert.ToInt32(dr["SubItemId"]);
                       if (dr["Weight"] == DBNull.Value)
                           wli.Weight = 0;
                       else
                           wli.Weight = Convert.ToDecimal(dr["Weight"]);

                     
                   


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
          // if (wli != null) 
           return wli;


       }
       public Item GetItmById(int id)
       {
           string getItemById = "select * from Item where ItemId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(getItemById, con);
           cmd.CommandType = CommandType.Text;
           Item itm = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   itm = new Item();
                   itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                   itm.ItemName= dr["ItemName"].ToString();
                   itm.Abrivation = dr["Abrivation"].ToString();


               }
               if (dr != null) dr.Close();

           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally { if (con.State == ConnectionState.Open)con.Close(); }
           return itm;
       }
       public void UpdateItem(int oldId, Item itm)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmdUpdateItem= new SqlCommand("UpdateItem", con);
           cmdUpdateItem.CommandType = CommandType.StoredProcedure;

           cmdUpdateItem.Parameters.Add(new SqlParameter("@ItemName",itm.ItemName));
           cmdUpdateItem.Parameters.Add(new SqlParameter("@Abrivation", itm.Abrivation));

           cmdUpdateItem.Parameters.Add(new SqlParameter("@oldItemId", oldId));


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
       public void DeleteItem(int id, Item itm)
       {

           string deleteCustomer = "Delete from Item where ItemId=" + id;
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

       public bool isNameExist(string querry)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           //string querry = "select ItemName from Item where ItemName='" + name + "'";
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

       public bool isAbriExist(string abri)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select Abrivation from Item where Abrivation='" + abri + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);

           bool aFlag = false;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
                   aFlag = true;


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
           return aFlag;
       }

       public bool isItemIdExist(int itemId)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ItemId from Stock where ItemId=" + itemId;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);

           bool iFlag = false;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
                   iFlag = true;


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
           return iFlag;
       }

       public bool isItemExist(int itemId)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ItemId from Stock where ItemId=" + itemId;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);

           bool sFlag = false;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
                   sFlag = true;
               else
                   sFlag = false;

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
           return sFlag;
       }

       public bool isItemExistInCost(int itemId)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ItemId from Costing where ItemId=" + itemId;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);

           bool sFlag = false;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
                   sFlag = true;


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
           return sFlag;
       }

       public bool isItemAndDesignExist(int itemId,int desId)
       {
           //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
           string querry = "select ItemId,DesignId from DesignItem where ItemId=" + itemId + "and DesignId=" + desId;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);

           bool sFlag = false;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.HasRows)
                   sFlag = true;
               else
                   sFlag = false;

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
           return sFlag;
       }
    }
}
