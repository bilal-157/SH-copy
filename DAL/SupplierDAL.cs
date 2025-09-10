using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
   public class SupplierDAL
    {
       public bool isExist(string query)
       {
           //string query = "select Abrivation from Item where Abrivation='" + abri + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(query, con);

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
       public void AddSupplier(Supplier part)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           con.Open();
           SqlCommand cmd = new SqlCommand("AddSupplier", con);
           cmd.CommandType = CommandType.StoredProcedure;
           //cmd.Parameters.Add(new SqlParameter("@PCode", part.PCode));
           cmd.Parameters.Add(new SqlParameter("@PAbri", part.PAbri));
           cmd.Parameters.Add(new SqlParameter("@PName", part.PName));
           cmd.Parameters.Add(new SqlParameter("@PAddress", part.PAddress));
           cmd.Parameters.Add(new SqlParameter("@PTel1", part.PtclNo1));
           //cmd.Parameters.Add(new SqlParameter("@PTel2", part.PtclNo2));
           //cmd.Parameters.Add(new SqlParameter("@PTel3", part.PtclNo3));
           //cmd.Parameters.Add(new SqlParameter("@PTel4", part.PtclNo4));
           //cmd.Parameters.Add(new SqlParameter("@PTel5", part.PtclNo5));
           cmd.Parameters.Add(new SqlParameter("@Date", part.PDate));
           cmd.Parameters.Add(new SqlParameter("@Making", part.PMaking));
           cmd.Parameters.Add(new SqlParameter("@Wastage", part.PWastage));
           cmd.Parameters.Add(new SqlParameter("@GoodWill", part.PGoodWill));
           cmd.Parameters.Add(new SqlParameter("@Discount", part.PDiscount));
           SqlTransaction tran = con.BeginTransaction();
           cmd.Transaction = tran;
           part.AccountCode = new AccountDAL().CreateAccount(2, "Supplier", part.PName, "Supplier", con, tran);
           cmd.Parameters.Add(new SqlParameter("@AccountCode", part.AccountCode));
           try
           {
               if (part.PhoneList.Count()>0)
               {
                   foreach (var item in part.PhoneList)
                   {
                       AddSupplierMobile(item);
                   }
               }
               cmd.ExecuteNonQuery();
           }
           catch (Exception ex)
           {
               tran.Rollback();
               throw ex;

           }
           finally
           {
               tran.Commit();
               if (con.State == ConnectionState.Open)
                   con.Close();
           }
       }

       public void AddSupplierMobile(Phone p)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("AddCustMobile", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@Mobile", p.PhoneNo));
           cmd.Parameters.Add(new SqlParameter("@CustId", p.PartyId));
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
               }
           }

       }

       public List<Supplier> GetAllSuppliers()
       {
           string constr = "select * from Supplier order by PCode";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(constr, con);
           List<Supplier> part = null;
           //SqlDataReader dr = null;
           try
           {
               con.Open();
               SqlDataReader dr = cmd.ExecuteReader();
               if (dr.Read())
               {

                   part = new List<Supplier>();

                   do
                   {
                       Supplier prt = new Supplier();
                       prt.PCode = Convert.ToInt32(dr["PCode"].ToString());
                       prt.PAbri = dr["PAbri"].ToString();
                       prt.PName = dr["PName"].ToString();
                       prt.PAddress = dr["PAddress"].ToString();
                       prt.PtclNo1 = dr["PTel1"].ToString();
                       prt.PDate = Convert.ToDateTime(dr["Date"].ToString());
                       prt.PMaking = dr["Making"].ToString();
                       prt.PWastage = dr["Wastage"].ToString();
                       prt.PGoodWill = dr["GoodWill"].ToString();
                       prt.PDiscount = dr["Discount"].ToString();
                       prt.AccountCode = dr["AccountCode"].ToString();
                       part.Add(prt);
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
           }
           return part;

       }

       public Supplier GetRecByAbri(string Abri)
       {
           string constr = @"select p.*, (((select isnull(sum(OpeningCash), 0) from ChildAccount where ChildCode = p.AccountCode) + (select isnull(sum(Dr), 0) from Vouchers where AccountCode = p.AccountCode)) - (select isnull(sum(Cr), 0) from Vouchers where AccountCode = p.AccountCode)) As PrvCashBal,
                            (((select isnull(sum(OpeningGold) , 0) from ChildAccount where ChildCode = p.AccountCode) + (select isnull(sum(GoldDr), 0) from Vouchers where AccountCode = p.AccountCode)) - (select isnull(sum(GoldCr), 0) from Vouchers where AccountCode = p.AccountCode)) As PrvGoldBal from Supplier p where p.PAbri ='" + Abri + "'";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(constr, con);
           Supplier pty = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   pty = new Supplier();
                   pty.PCode = Convert.ToInt32(dr["PCode"]);
                   pty.PAbri = dr["PAbri"].ToString();
                   pty.PName = dr["PName"].ToString();
                   pty.AccountCode = dr["AccountCode"].ToString();
                   pty.PAddress = dr["PAddress"].ToString();
                   pty.PMaking = dr["Making"].ToString();
                   pty.PWastage = dr["Wastage"].ToString();
                   pty.PDiscount = dr["Discount"].ToString();
                   pty.PrvCashBal = Convert.ToDecimal(dr["PrvCashBal"]);
                   pty.PrvGoldBal = Convert.ToDecimal(dr["PrvGoldBal"]);
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
           return pty;
       }
    }
}
