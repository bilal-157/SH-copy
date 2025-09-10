using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;
namespace DAL
{
   public  class BankDAL
    {
       private string getMaxCode = "select MAX(Id) as [BankCode] from Bank";
       private string getMaxCode1 = "select MAX(AccountId) as [MaxId] from BankAccount";
       public void AddBank(Banks bnk)
       {
           string qurry = "AddBank";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qurry, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@Id", bnk.Id));
           cmd.Parameters.Add(new SqlParameter("@BankName", bnk.BankName));
           cmd.Parameters.Add(new SqlParameter("@DRate", bnk.DRate));
           cmd.Parameters.Add(new SqlParameter("@ParentCode", bnk.ParentCode.ParentCode));
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
       public List<Banks> GetAllBanks()
       {
           string querry = "select * from Bank";
           SqlConnection con = new SqlConnection(DALHelper .ConnectionString);
           SqlCommand cmd = new SqlCommand(querry ,con);
           cmd.CommandType = CommandType.Text;
           List<Banks> bnks = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   bnks = new List<Banks>();
                   do
                   {
                       Banks bnk = new Banks();
                       bnk.Id = Convert.ToInt32(dr["Id"]);
                       bnk.BankName = dr["BankName"].ToString();
                       ParentAccount pa = new ParentAccount();
                       pa.ParentCode = dr["ParentCode"].ToString();
                       bnk.ParentCode = pa;
                       bnk.DRate = Convert.ToDecimal(dr["DRate"]);
                       bnks.Add(bnk);

                   } while (dr.Read());
                   dr.Close();
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
               cmd.Dispose();
           }
           return bnks;
       }
       public void AddBankAccount(BankAccount ba)
       {
           string query = "AddBankAccount";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@AccountId", ba.Id));
           cmd.Parameters.Add(new SqlParameter("@BankId", ba.BankName .Id));
           cmd.Parameters.Add(new SqlParameter("@AccountNo", ba.AccountNo));
           cmd.Parameters.Add(new SqlParameter("@AccountCode", ba.AccountCode.ChildCode));
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
                   itemCode = Convert.ToInt32(dr["BankCode"]);
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
       public int GetMaxAccountCode()
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(this.getMaxCode1, con);
           cmd.CommandType = CommandType.Text;
           //Employee employee = null;
           int itemCode = 0;
           con.Open();
           try
           {
               SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
               if (dr.Read())
               {
                   itemCode = Convert.ToInt32(dr["MaxId"]);
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
       public List<BankAccount> GetAllBankAccount()
       {
           string qurry = "select ba.AccountId,ba.BankId,ba.AccountNo,ba.AccountCode ,b.Id,b.BankName from BankAccount ba inner join Bank b on ba.BankId=b.Id ";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qurry, con);
           cmd.CommandType = CommandType.Text;
           List<BankAccount> baccounts = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   baccounts = new List<BankAccount>();
                   do
                   {
                       BankAccount ba = new BankAccount();
                        
                       ba.AccountCode = new ChildAccount (dr["AccountCode"].ToString());
                       ba.Id = Convert.ToInt32(dr["AccountId"]);
                       ba.BankName = new Banks(Convert.ToInt32(dr["Id"]), dr["BankName"].ToString());
                       ba.AccountNo = dr["AccountNo"].ToString();
                       baccounts.Add(ba);
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
               {
                   con.Close();
                   cmd.Dispose();
               }
              
           }
           return baccounts;
       }
       public void UpdateBank(int id, Banks bnk)
       {
           string querry = "updateBank";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@oldId",SqlDbType .Int ).Value =id;
           cmd.Parameters.Add(new SqlParameter("@BankName", bnk.BankName));
           cmd.Parameters.Add(new SqlParameter("@DRate", bnk.DRate));
           
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
                   cmd.Dispose();
               }
           }

       }
       public void UpdateBankAccount(int id, BankAccount ba)
       {
           string querry = "UpdateBankAccount";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@oldAccountId",SqlDbType .Int).Value = id;
           cmd.Parameters.Add(new SqlParameter("@BankId", ba.BankName.Id));
           cmd.Parameters.Add(new SqlParameter("@AccountNo", ba.AccountNo));
           cmd.Parameters.Add(new SqlParameter("@AccountCode", ba.AccountCode.ChildCode));
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
                   cmd.Dispose();
               }
           }

       }
       public Banks SearchBank(int id)
       {
           string querry = "select * from Bank Where Id='"+id+"'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(querry, con);
           cmd.CommandType = CommandType.Text;
           SqlDataReader dr = null;
           Banks bnk = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   
                     bnk = new Banks();
                       bnk.Id = Convert.ToInt32(dr["Id"]);
                       bnk.BankName = dr["BankName"].ToString();
                       ParentAccount pa = new ParentAccount();
                       pa.ParentCode = dr["ParentCode"].ToString();
                       bnk.ParentCode = pa;
                       bnk.DRate = Convert.ToDecimal(dr["DRate"]);
                      
                  
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
                   cmd.Dispose();
               }
           }
           return bnk;
       }
       public BankAccount SearchBankAccount(int id)
       {
           string qurry = "select ba.AccountId,ba.BankId,ba.AccountNo,ba.AccountCode ,b.Id,b.BankName from BankAccount ba inner join Bank b on ba.BankId=b.Id where ba.AccountId='"+id+"'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qurry, con);
           cmd.CommandType = CommandType.Text;
          
           SqlDataReader dr = null;
           BankAccount ba = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   ba = new BankAccount();
                   ba.AccountCode = new ChildAccount(dr["AccountCode"].ToString());
                   ba.Id = Convert.ToInt32(dr["AccountId"]);
                   ba.BankName = new Banks(Convert.ToInt32(dr["Id"]), dr["BankName"].ToString());
                   ba.AccountNo = dr["AccountNo"].ToString();
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
                   cmd.Dispose();
               }
           }
           return ba;
       }
       public List<BankAccount> GetAllBankAccountByBankId(int id)
       {
           string qurry = "select ba.AccountId,ba.BankId,ba.AccountNo,ba.AccountCode ,b.Id,b.BankName from BankAccount ba inner join Bank b on ba.BankId=b.Id where ba.BankId='"+id+"'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(qurry, con);
           cmd.CommandType = CommandType.Text;
           List<BankAccount> baccounts = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   baccounts = new List<BankAccount>();
                   do
                   {
                       BankAccount ba = new BankAccount();

                       ba.AccountCode = new ChildAccount(dr["AccountCode"].ToString());
                       ba.Id = Convert.ToInt32(dr["AccountId"]);
                       ba.BankName = new Banks(Convert.ToInt32(dr["Id"]), dr["BankName"].ToString());
                       ba.AccountNo = dr["AccountNo"].ToString();
                       baccounts.Add(ba);
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
               {
                   con.Close();
                   cmd.Dispose();
               }

           }
           return baccounts;
       }
    }
}
