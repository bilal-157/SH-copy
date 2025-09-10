using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BusinesEntities;

namespace DAL
{
   public class PurchaseDAL
    {
        int purNo;
        int RpNo;
       public int GetMaxPurchaseNo()
       {
           string querry = "Select MAX(PurchaseNo) as [MaxPurchase] from PurchaseMaster";
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
                   if (dr["MaxPurchase"] == DBNull.Value)
                       saleNo = 0;
                   else
                       saleNo = Convert.ToInt32(dr["MaxPurchase"]);
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

       public void AddPurchase(Purchase p, out int pa, SqlConnection con, SqlTransaction tran)
       {
           string addPurchase = "AddPurchaseMaster";
           string addPurchaseDetail = "AddPurchaseItemDetail";
           string addPurchaseSubDetail = "AddPurchaseSubDetail";

           SqlCommand cmdPurchase = new SqlCommand(addPurchase, con);
           cmdPurchase.CommandType = CommandType.StoredProcedure;
           SqlParameter parm = new SqlParameter("@PurchaseNum", SqlDbType.Int);
           parm.Direction = ParameterDirection.Output; // This is important!
           cmdPurchase.Parameters.Add(parm);
           cmdPurchase.Parameters.Add(new SqlParameter("@PAccountCode", p.PAccountCode));
           cmdPurchase.Parameters.Add(new SqlParameter("@PurchaseNo", p.PurchaseNo));
           cmdPurchase.Parameters.Add(new SqlParameter("@TotalWeight", p.TotalWeight));
           cmdPurchase.Parameters.Add(new SqlParameter("@TotalWastage", p.TotalWastage));
           cmdPurchase.Parameters.Add(new SqlParameter("@TotalGoodWill", p.TotalGoodWill));
           cmdPurchase.Parameters.Add(new SqlParameter("@TotalPurchaseDiscount", p.TotalPurchaseDiscount));
           cmdPurchase.Parameters.Add(new SqlParameter("@TotalPureGold", p.TotalPureGold));
           cmdPurchase.Parameters.Add(new SqlParameter("@PurchaseDate", p.PurchaseDate));
           cmdPurchase.Parameters.Add(new SqlParameter("@ReceivedBy", p.ReceivedBy));
           cmdPurchase.Parameters.Add(new SqlParameter("@Comments", p.Comments == null ? "" : p.Comments));
           cmdPurchase.Parameters.Add(new SqlParameter("@VNO", new VouchersDAL().CreateVNO("PRV")));


           SqlCommand cmd = new SqlCommand(addPurchaseSubDetail, con);
           // cmd.Parameters.Add(new SqlParameter("@BarCode",stock.BarCode));
           //stock.ItemName = new Item();
           cmd.Parameters.Add("@PItemId", SqlDbType.NVarChar);
           cmd.Parameters.Add("@ItemId", SqlDbType.Int);
           cmd.Parameters.Add("@PurchaseNo", SqlDbType.Int);
           cmd.Parameters.Add("@ItemDescription", SqlDbType.NVarChar);
           cmd.Parameters.Add("@Qty", SqlDbType.Int);
           cmd.Parameters.Add("@Karat", SqlDbType.NVarChar);
           cmd.Parameters.Add("@Weight", SqlDbType.Float);
           cmd.Parameters.Add("@Making", SqlDbType.Float);


           cmd.CommandType = CommandType.StoredProcedure;
           SqlCommand cmdPurchaseDetail = new SqlCommand(addPurchaseDetail, con);
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@PItemId", SqlDbType.NVarChar));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@PurchaseNo", SqlDbType.Int));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@Qty", SqlDbType.Int));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@Karat", SqlDbType.NVarChar));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@GrossWeight", SqlDbType.Decimal));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@Wastage", SqlDbType.Decimal));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@GoodWill", SqlDbType.Decimal));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@Discount", SqlDbType.Decimal));
           cmdPurchaseDetail.Parameters.Add(new SqlParameter("@PureGold", SqlDbType.Decimal));
           cmdPurchaseDetail.CommandType = CommandType.StoredProcedure;

           try
           {
               //con.Open();
               //SqlTransaction tran = con.BeginTransaction();
               cmdPurchase.Transaction = tran;
               cmd.Transaction = tran;
               cmdPurchaseDetail.Transaction = tran;
               cmdPurchase.ExecuteNonQuery();
               purNo = (int)parm.Value;
               pa = purNo;
               try
               {
                   int i = 1;
                   foreach (PurchaseSubLineItems pli in p.PurchaseSubLineItems)
                   {
                       cmd.Parameters["@PurchaseNo"].Value = purNo;
                       cmd.Parameters["@PItemId"].Value = purNo + "-" + i;
                       i++;
                       cmd.Parameters["@ItemId"].Value = pli.ItemId;
                       cmd.Parameters["@ItemDescription"].Value = pli.ItemDescription;
                       cmd.Parameters["@Qty"].Value = pli.Qty;
                       cmd.Parameters["@Karat"].Value = pli.Karat;
                       cmd.Parameters["@Weight"].Value = pli.Weight;
                       cmd.Parameters["@Making"].Value = pli.Making;
                       cmd.ExecuteNonQuery();

                   }
                   i = 1;
                   if (p.PurchaseLineItems == null)
                   { }
                   else
                   {
                       foreach (PurchaseLineItems pli in p.PurchaseLineItems)
                       {
                           cmdPurchaseDetail.Parameters["@PurchaseNo"].Value = purNo;
                           cmdPurchaseDetail.Parameters["@PItemId"].Value = purNo + "-" + i;
                           i++;
                           cmdPurchaseDetail.Parameters["@Description"].Value = pli.Description;
                           cmdPurchaseDetail.Parameters["@Qty"].Value = pli.Qty;
                           cmdPurchaseDetail.Parameters["@Karat"].Value = pli.Karat;
                           cmdPurchaseDetail.Parameters["@GrossWeight"].Value = pli.GrossWeight;
                           cmdPurchaseDetail.Parameters["@Wastage"].Value = pli.Wastage;
                           cmdPurchaseDetail.Parameters["@GoodWill"].Value = pli.GoodWill;
                           cmdPurchaseDetail.Parameters["@Discount"].Value = pli.Discount;
                           cmdPurchaseDetail.Parameters["@PureGold"].Value = pli.PureGold;
                           cmdPurchaseDetail.ExecuteNonQuery();
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

       public Stock GetPendingItemsBySupplier(string PAcc, int ItmId, string krt)
       {
           string constr = @"select ((select isnull(sum(psd.Qty), 0) from PurchaseItemDetail pid inner join PurchaseSubDetail psd on psd.PItemId = pid.PItemId inner join item itm on itm.ItemId = psd.ItemId where pid.PurchaseNo in (select PurchaseNo from PurchaseMaster where PAccountCode = '" + PAcc + "') and itm.ItemId = " + ItmId + " and psd.Karat = '" + krt + "')-(select isnull(sum(Qty), 0) from Stock where ItemId = " + ItmId + " and PAccountCode = '" + PAcc + "' and Karat = '" + krt + "')) As Qty," +
           "((select isnull(sum(psd.[Weight]), 0) from PurchaseItemDetail pid inner join PurchaseSubDetail psd on psd.PItemId = pid.PItemId inner join item itm on itm.ItemId = psd.ItemId where pid.PurchaseNo in (select PurchaseNo from PurchaseMaster where PAccountCode = '" + PAcc + "') and itm.ItemId = " + ItmId + " and psd.Karat = '" + krt + "')-(select isnull(sum([NetWeight]), 0) from Stock where ItemId = " + ItmId + " and PAccountCode = '" + PAcc + "' and Karat = '" + krt + "')) As Weight";

           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(constr, con);
           Stock stk = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   stk = new Stock();
                   stk.Qty = Convert.ToInt32(dr["Qty"]);
                   stk.NetWeight = Convert.ToDecimal(dr["Weight"]);
               }
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
           return stk;
       }
    }
}
