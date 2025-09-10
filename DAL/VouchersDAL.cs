using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

namespace DAL
{
   public  class VouchersDAL
    {
       public string CreateVNO(string VType)
       {
           string qury = "select VNO from Vouchers where VNO like  '" + VType + "%' order by VNO";
           SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qury, con1);
           string str = "";
           string vno = "";
           SqlDataReader dr = null;
           int no = 1;
           try
           {
               con1.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   do
                   {
                       vno = dr["VNO"].ToString();
                       if (no < Convert.ToInt32(vno.Remove(0, VType.Length)))
                           no = Convert.ToInt32(vno.Remove(0, VType.Length));
                   }
                   while (dr.Read());
                   no += 1;
               }
           }
           catch (Exception ex)
           {

               throw ex;
           }
           finally
           {
               if (con1.State == ConnectionState.Open)
                   con1.Close();
           }
           str = VType + no.ToString();
           return str;


       }
       public string CreateVNO(string VType , SqlConnection con,SqlTransaction tran)
       {
           string qury = "select VNO from Vouchers where VNO like'" + VType + "%' order by VNO";
           //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qury, con);
           cmd.Transaction = tran;
           string str = "";
           string vno = "";
           SqlDataReader dr ;
           int no = 1;
           try
           {
               //con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   do
                   {
                       vno = dr["VNO"].ToString();
                       if (no < Convert.ToInt32(vno.Remove(0, VType.Length)))
                           no = Convert.ToInt32(vno.Remove(0, VType.Length));
                   }
                   while (dr.Read());
                   no += 1;
                   
               }
           }
           catch (Exception ex)
           {
               tran.Rollback();
               throw ex;
           }
           finally
           {
               //if (con.State == ConnectionState.Open)
               //    con.Close();
           }
           str = VType + no.ToString();
           dr.Close();
           return str;
       }

       public string CreatWGVNO(string VType, SqlConnection con, SqlTransaction tran)
       {
           string qury = "select VNO from GoldDetail where VNO like'" + VType + "%' order by VNO";
           //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qury, con);
           cmd.Transaction = tran;
           string str = "";
           string vno = "";
           SqlDataReader dr;
           int no = 1;
           try
           {
               //con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   do
                   {
                       vno = dr["VNO"].ToString();
                       if (no < Convert.ToInt32(vno.Remove(0, VType.Length)))
                           no = Convert.ToInt32(vno.Remove(0, VType.Length));
                   }
                   while (dr.Read());
                   no += 1;

               }
               dr.Close();
           }
           catch (Exception ex)
           {
               tran.Rollback();
               throw ex;
           }
           finally
           {
               //if (con.State == ConnectionState.Open)
               //    con.Close();
           }
           str = VType + no.ToString();
           dr.Close();
           return str;


       }

       //public string GetMaxVoucherNo()
       //{
       //    string querry = "Select MAX(VNO) as [MaxVNO] from Vouchers";
       //    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
       //    SqlCommand cmd = new SqlCommand(querry, con);
       //    cmd.CommandType = CommandType.Text;
       //    string VNo = "";
       //    try
       //    {
       //        con.Open();
       //        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


       //        //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
       //        if (dr.Read())
       //        {
       //            if (dr["MaxVNO"] == DBNull.Value)
       //                VNo = "";
       //            else
       //                VNo = dr["MaxVNO"].ToString(); ;
       //        }

       //        dr.Close();

       //    }
       //    catch (Exception ex)
       //    {

       //        throw ex;
       //    }
       //    finally
       //    {
       //        if (con.State == ConnectionState.Open)
       //            con.Close();
       //    }
       //    return VNo;
       //}
       public void AddVoucher(Voucher v , SqlConnection con , SqlTransaction tran)
       {
           string query = "AddVoucher";
           //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.Transaction = tran;
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@VNO", v.VNO));
           cmd.Parameters.Add(new SqlParameter("@AccountCode", v.AccountCode.ChildCode));
           cmd.Parameters.Add(new SqlParameter("@AccountName", v.AccountCode.ChildName));
           cmd.Parameters.Add(new SqlParameter("@ParentCode", v.AccountCode.ParentCode));
           //cmd.Parameters.Add(new SqlParameter("@GroupCode", v.AccountCode.GroupCode));
           //cmd.Parameters.Add(new SqlParameter("@SubGroupCode", v.AccountCode.SubGroupCode));
           cmd.Parameters.Add(new SqlParameter("@HeadCode", v.AccountCode.HeadCode));
           cmd.Parameters.Add(new SqlParameter("@Dr", v.Dr));
           cmd.Parameters.Add(new SqlParameter("@Cr", v.Cr));

           if (v.GPNO == null)
           {
               cmd.Parameters.Add("@GPNO", SqlDbType.Int);
               cmd.Parameters["@GPNO"].Value = DBNull.Value;
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GPNO", v.GPNO));

           if (v.GSNO == null)
           {
               cmd.Parameters.Add("@GSNO", SqlDbType.Int);
               cmd.Parameters["@GSNO"].Value = DBNull.Value;
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GSNO", v.GSNO));


           if (v.SNO.HasValue)
               cmd.Parameters.Add(new SqlParameter("@SaleNO", v.SNO));
           else
           {
               cmd .Parameters.Add ("@SaleNo",SqlDbType .Int );
               cmd .Parameters ["@saleNo"].Value =DBNull .Value ;
           }
           if (v.WBillNO.HasValue)
               cmd.Parameters.Add(new SqlParameter("@WBillNo", v.WBillNO));
           else
           {
               cmd.Parameters.Add("@WBillNo", SqlDbType.Int);
               cmd.Parameters["@WBillNo"].Value = DBNull.Value;
           }
           if (v.OrderNo.HasValue)
               cmd.Parameters.Add(new SqlParameter("@OrderNo", v.OrderNo));
           else 
           {
               cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
               cmd.Parameters["@OrderNo"].Value = DBNull.Value;
           }
           if (v.RID.HasValue)
               cmd.Parameters.Add(new SqlParameter("@RID", v.RID));
           else
           {
               cmd.Parameters.Add("@RID", SqlDbType.Int);
               cmd.Parameters["@RID"].Value = DBNull.Value;
           }


           cmd.Parameters.Add(new SqlParameter("@DDate", v.DDate));
           cmd.Parameters.Add(new SqlParameter("@Description", v.Description));
           if (v.GoldDr == null)
           {
               v.GoldDr = 0;
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           if (v.GoldCr == null)
           {
               v.GoldCr= 0;
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           }
           else
           cmd.Parameters.Add(new SqlParameter("@GoldCr", v.GoldCr));
           try
           {
               //con.Open();
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               //trans.Rollback();
               throw ex;
           }
           finally 
           {
               //if (con.State == ConnectionState.Open)
               //    con.Close();
           }
       }
       public void AddVoucher(Voucher v)
       {
           string query = "AddVoucher";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@VNO", v.VNO));
           cmd.Parameters.Add(new SqlParameter("@AccountCode", v.AccountCode.ChildCode));
           cmd.Parameters.Add(new SqlParameter("@AccountName", v.AccountCode.ChildName));
           cmd.Parameters.Add(new SqlParameter("@ParentCode", v.AccountCode.ParentCode));
           cmd.Parameters.Add(new SqlParameter("@GroupCode", v.AccountCode.GroupCode));
           cmd.Parameters.Add(new SqlParameter("@SubGroupCode", v.AccountCode.SubGroupCode));
           cmd.Parameters.Add(new SqlParameter("@HeadCode", v.AccountCode.HeadCode));
           cmd.Parameters.Add(new SqlParameter("@Dr", v.Dr));
           cmd.Parameters.Add(new SqlParameter("@Cr", v.Cr));
           if (v.GPNO == null)
           {
               cmd.Parameters.Add("@GPNO", SqlDbType.Int);
               cmd.Parameters["@GPNO"].Value = DBNull.Value;
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GPNO", v.GPNO));

           if (v.GSNO == null)
           {
               cmd.Parameters.Add("@GSNO", SqlDbType.Int);
               cmd.Parameters["@GSNO"].Value = DBNull.Value;
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GSNO", v.GSNO));
           if (v.WBillNO.HasValue)
               cmd.Parameters.Add(new SqlParameter("@WBillNo", v.WBillNO));
           else
           {
               cmd.Parameters.Add("@WBillNo", SqlDbType.Int);
               cmd.Parameters["@WBillNo"].Value = DBNull.Value;
           }
           if (v.SNO.HasValue)
               cmd.Parameters.Add(new SqlParameter("@SaleNO", v.SNO));
           else
           {
               cmd.Parameters.Add("@SaleNo", SqlDbType.Int);
               cmd.Parameters["@saleNo"].Value = DBNull.Value;
           }
           if (v.OrderNo.HasValue)
               cmd.Parameters.Add(new SqlParameter("@OrderNo", v.OrderNo));
           else
           {
               cmd.Parameters.Add("@OrderNo", SqlDbType.Int);
               cmd.Parameters["@OrderNo"].Value = DBNull.Value;
           }
           if (v.RID.HasValue)
               cmd.Parameters.Add(new SqlParameter("@RID", v.RID));
           else
           {
               cmd.Parameters.Add("@RID", SqlDbType.Int);
               cmd.Parameters["@RID"].Value = DBNull.Value;
           }


           cmd.Parameters.Add(new SqlParameter("@DDate", v.DDate));
           cmd.Parameters.Add(new SqlParameter("@Description", v.Description));
           if (v.GoldDr == null)
           {
               v.GoldDr = 0;
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           if (v.GoldCr == null)
           {
               v.GoldCr = 0;
               cmd.Parameters.Add(new SqlParameter("@GoldDr", v.GoldDr));
           }
           else
               cmd.Parameters.Add(new SqlParameter("@GoldCr", v.GoldCr));
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

       public void DeleteVoucher(string vno)
       {

           string deleteCustomer = "Delete from Vouchers where VNO='" + vno + "'";
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

       public decimal GetGoldBalanceByAccCode(string AcCode)
       {
           string querry = "select(((Select isnull(OpeningGold,0)  from ChildAccount where ChildCode='" + AcCode + "')+(Select isnull(sum(GoldDr),0)  from vouchers where AccountCode='" + AcCode + "'))-(Select isnull(sum(GoldCr),0)  from vouchers where AccountCode='" + AcCode + "'))as GoldBalance";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
           cmd.CommandType = CommandType.Text;
           decimal saleNo = 0;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);               
               if (dr.Read())
               {
                   if (dr["GoldBalance"] == DBNull.Value)
                       saleNo = 0;
                   else
                       saleNo = Convert.ToDecimal(dr["GoldBalance"]);
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
       public decimal GetCashBalanceByAccCode(string AcCode)
       {
           string querry = "select(((Select isnull(OpeningCash,0)  from ChildAccount where ChildCode='" + AcCode + "')+(Select isnull(sum(Dr),0)  from vouchers where AccountCode='" + AcCode + "'))-(Select isnull(sum(Cr),0)  from vouchers where AccountCode='" + AcCode + "'))as CashBalance";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
           cmd.CommandType = CommandType.Text;
           decimal saleNo = 0;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
               if (dr.Read())
               {
                   if (dr["CashBalance"] == DBNull.Value)
                       saleNo = 0;
                   else
                       saleNo = Convert.ToDecimal(dr["CashBalance"]);
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

       public void DeleteVoucher(string vno, SqlConnection con , SqlTransaction tran)
       {
           string deleteCustomer = "Delete from Vouchers where VNO='" + vno + "'";
           SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
           cmdDelete.Transaction = tran;
           cmdDelete.CommandType = CommandType.Text;
           try
           {
               try
               {
                   cmdDelete.ExecuteNonQuery();
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

       public void DeleteCreditCard(string vno, SqlConnection con, SqlTransaction tran)
       {
           string deleteCustomer = "Delete from CreditCardDetail where VNO='" + vno + "'";
           SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
           cmdDelete.Transaction = tran;
           cmdDelete.CommandType = CommandType.Text;
           try
           {
               try
               {
                   cmdDelete.ExecuteNonQuery();
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

       public void DeleteCheque(string vno, SqlConnection con, SqlTransaction tran)
       {
           string deleteCustomer = "Delete from ChequeDetail where VNO='" + vno + "'";
           SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
           cmdDelete.Transaction = tran;
           cmdDelete.CommandType = CommandType.Text;
           try
           {
               try
               {
                   cmdDelete.ExecuteNonQuery();
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

       public decimal GetCash(string CPV, string ccode)
       {
           string query = "select * from Vouchers where VNO ='" + CPV + "'" + "and AccountCode ='" + ccode + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.Text;
           decimal Cash = 0;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   Cash = Convert.ToDecimal(dr["Dr"]);
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
           return Cash;
       }

       public decimal GetCashPayment(string CPV, string ccode)
       {
           string query = "select * from Vouchers where VNO ='" + CPV + "'" + "and AccountCode ='" + ccode + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.Text;
           decimal Cash = 0;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   Cash = Convert.ToDecimal(dr["Cr"]);
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
           return Cash;
       }

       public decimal GetCashPayment(string CPV, string ccode,SqlConnection con , SqlTransaction tran)
       {
           string query = "select * from Vouchers where VNO ='" + CPV + "'" + "and AccountCode ='" + ccode + "'";
           //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.Transaction = tran;
           cmd.CommandType = CommandType.Text;
           decimal Cash = 0;
           try
           {
               //con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   Cash = Convert.ToDecimal(dr["Cr"]);
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
           return Cash;
       }

       public List<Voucher> GetVoucher(DateTime dt)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dt;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }
       public List<Voucher> GetVoucherGold()
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetVoucherGold", con);
           cmd.CommandType = CommandType.StoredProcedure;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.GoldDr = Convert.ToDecimal(dr["GoldDr"]);
                       v.GoldCr = Convert.ToDecimal(dr["GoldCr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetVoucherForUpdate(string vno)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetReceiptVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure;
          // cmd.Parameters.Add("@Con", SqlDbType.Int).Value = a;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.GoldDr = Convert.ToDecimal(dr["GoldDr"]);
                       v.GoldCr = Convert.ToDecimal(dr["GoldCr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }      

       public List<Voucher> GetPaymentVoucher(DateTime dt)
       {
          // string selectSql = "select * from Vouchers where VNo Like 'CPV%' and AccountName not like 'Cash In%' and convert (varchar , DDate ,112) = convert (varchar ,'"+DateTime.Today+"',112)";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetPaymentVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure  ;
           cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dt;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }
       public List<Voucher> GetPaymentVoucherGold()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetPaymentVoucherGold", con);
           cmd.CommandType = CommandType.StoredProcedure;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do 
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.GoldDr = Convert.ToDecimal(dr["GoldDr"]);
                       v.GoldCr = Convert.ToDecimal(dr["GoldCr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetPaymentVoucherForUpdate(string vno)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(vno, con);
           cmd.CommandType = CommandType.Text ;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.GoldDr = Convert.ToDecimal(dr["GoldDr"]);
                       v.GoldCr = Convert.ToDecimal(dr["GoldCr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetBankReceiptVoucher()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankReceiptVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetBankVoucherForUpdate(string vno)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankVoucherForUpdate", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public Voucher GetBankVoucherForUp(string vno)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankVoucherForUp", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           Voucher v = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //c = new List<Voucher>();
                   //if (c == null) c = new List<Voucher>();
                   //do
                   //{
                   v = new Voucher();
                   v.DDate = Convert.ToDateTime(dr["DDate"]);
                   v.VNO = dr["VNO"].ToString();
                   v.AccountCode = new ChildAccount();
                   v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                   v.AccountCode.ChildName = dr["AccountName"].ToString();
                   v.Dr = Convert.ToDecimal(dr["Dr"]);
                   v.Cr = Convert.ToDecimal(dr["Cr"]);
                   v.Description = dr["Description"].ToString();
                   //}
                   //while (dr.Read());
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
           return v;
       }

       public List<Voucher> GetBankPaymentVoucher()
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankPaymentVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetBankPaymentVoucherForUpdate(string vno)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankPaymentVoucherForUpdate", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public Voucher GetBankPaymentVoucherForUp(string vno)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetBankPaymentForUp", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           Voucher v = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //c = new List<Voucher>();
                   //if (c == null) c = new List<Voucher>();
                   //do
                   //{
                   v = new Voucher();
                   v.DDate = Convert.ToDateTime(dr["DDate"]);
                   v.VNO = dr["VNO"].ToString();
                   v.AccountCode = new ChildAccount();
                   v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                   v.AccountCode.ChildName = dr["AccountName"].ToString();
                   v.Dr = Convert.ToDecimal(dr["Dr"]);
                   v.Cr = Convert.ToDecimal(dr["Cr"]);
                   v.Description = dr["Description"].ToString();

                   //}
                   //while (dr.Read());
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
           return v;
       }

       public List<Voucher> GetJournalVoucher()
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetJournalVoucher", con);
           cmd.CommandType = CommandType.StoredProcedure;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public List<Voucher> GetJournalVoucherForUpdate(string vno)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("GetJournalVoucherForUpdate", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@VNO", SqlDbType.NVarChar).Value = vno;
           List<Voucher> c = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   c = new List<Voucher>();
                   if (c == null) c = new List<Voucher>();
                   do
                   {
                       Voucher v = new Voucher();
                       v.DDate = Convert.ToDateTime(dr["DDate"]);
                       v.VNO = dr["VNO"].ToString();
                       v.AccountCode = new ChildAccount();
                       v.AccountCode.ChildCode = dr["AccountCode"].ToString();
                       v.AccountCode.ChildName = dr["AccountName"].ToString();
                       v.Dr = Convert.ToDecimal(dr["Dr"]);
                       v.Cr = Convert.ToDecimal(dr["Cr"]);
                       v.Description = dr["Description"].ToString();
                       c.Add(v);
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
           return c;
       }

       public string GetVoucherGeneral(string query)
       {
           //string selectSql = "select * from Vouchers where ChildName='" + name + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.Text;
           string v = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //c = new Voucher();
                   //if (c == null) c = new Voucher();
                   //do
                   //{  v = new Voucher();
                   do
                       v = dr["VNO"].ToString();
                   while (dr.Read());
                       //c.Add(v);
                   //}
                   //while (dr.Read());
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
           return v;
       }
       public List<string> GetVoucherGeneral(string query, SqlConnection con,SqlTransaction tran)
       {
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.Transaction = tran;
           cmd.CommandType = CommandType.Text;
           List<string> vs = null;
           SqlDataReader dr = null;
           try
           {
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   vs = new List<string>();
                   do
                   {
                       string v;
                       v = dr["VNO"].ToString();
                       vs.Add(v);
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
               
           }
           return vs;
       }
       public string CreatVNO(string VType, SqlConnection con, SqlTransaction tran)
       {
           string qury = "select VNO from Vouchers where VNO like'" + VType + "%' order by VNO";
           //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qury, con);
           cmd.Transaction = tran;
           string str = "";
           string vno = "";
           SqlDataReader dr;
           int no = 1;
           try
           {
               //con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   do
                   {
                       vno = dr["VNO"].ToString();
                       if (no < Convert.ToInt32(vno.Remove(0, VType.Length)))
                           no = Convert.ToInt32(vno.Remove(0, VType.Length));
                   }
                   while (dr.Read());
                   no += 1;

               }
           }
           catch (Exception ex)
           {
               tran.Rollback();
               throw ex;
           }
           finally
           {
               //if (con.State == ConnectionState.Open)
               //    con.Close();
           }
           str = VType + no.ToString();
           dr.Close();
           return str;
       }

       public void DeleteVoucherBySaleNo(int sno, SqlConnection con, SqlTransaction tran)
       {
           SqlCommand cmdDeleteV = new SqlCommand("Delete from Vouchers where SaleNO = " + sno, con);
           cmdDeleteV.Transaction = tran;
           cmdDeleteV.CommandType = CommandType.Text;

           SqlCommand cmdDeleteSP = new SqlCommand("Delete from Sale_Payment where SNO = " + sno, con);
           cmdDeleteSP.Transaction = tran;
           cmdDeleteSP.CommandType = CommandType.Text;

           SqlCommand cmdDeleteGD = new SqlCommand("Delete from GoldDetail where SNO = " + sno, con);
           cmdDeleteGD.Transaction = tran;
           cmdDeleteGD.CommandType = CommandType.Text;

           SqlCommand cmdDeleteCD = new SqlCommand("Delete from ChequeDetail where SNO = " + sno, con);
           cmdDeleteCD.Transaction = tran;
           cmdDeleteCD.CommandType = CommandType.Text;

           SqlCommand cmdDeleteCC = new SqlCommand("Delete from CreditCardDetail where SNO = " + sno, con);
           cmdDeleteCC.Transaction = tran;
           cmdDeleteCC.CommandType = CommandType.Text;
           try
           {
               cmdDeleteV.ExecuteNonQuery();
               cmdDeleteSP.ExecuteNonQuery();
               cmdDeleteGD.ExecuteNonQuery();
               cmdDeleteCD.ExecuteNonQuery();
               cmdDeleteCC.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               throw ex;
           }
           finally
           {

           }
       }
    }
}
