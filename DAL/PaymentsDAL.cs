using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using BusinesEntities;
using DAL;

namespace DAL
{
    public class PaymentsDAL
    {
        public void AddSalePayment(SalePayment sp)
        {
            string query = "AddSalePayment";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SNO", sp.SaleNo));
            cmd.Parameters.Add(new SqlParameter("@ONO", sp.ONo));
            cmd.Parameters.Add(new SqlParameter("@VNO", sp.VNo));
            cmd.Parameters.Add(new SqlParameter("@PMode", sp.PMode));
            cmd.Parameters.Add(new SqlParameter("@Amount", sp.Amount));
            cmd.Parameters.Add(new SqlParameter("@Description", sp.Description));
            cmd.Parameters.Add(new SqlParameter("@DDate", sp.DDate));
            cmd.Parameters.Add(new SqlParameter("@DRate", sp.DRate));
            cmd.Parameters.Add(new SqlParameter("@PTime", sp.PTime));
            cmd.Parameters.Add(new SqlParameter("@Receiveables", sp.Receiveable));
            cmd.Parameters.Add(new SqlParameter("@BDRate", sp.BDrate));
            cmd.Parameters.Add(new SqlParameter("@BankName", sp.BankName));
            cmd.Parameters.Add(new SqlParameter("@DAccountCode", sp.DAccountCode));
            //if (sp.GoldOfCash == null )
            //{
            //    cmd.Parameters.Add(new SqlParameter("@GoldOfCash", 0));
            //}
            //else
            //    cmd.Parameters.Add(new SqlParameter("@GoldOfCash", sp.GoldOfCash));

            cmd.Parameters.Add("@GoldOfCash", SqlDbType.Float);
            cmd.Parameters["@GoldOfCash"].Value = DBNull.Value;
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
        public void AddSalePayment(SalePayment sp, SqlConnection con, SqlTransaction trans)
        {
            string query = "AddSalePayment";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@SNO", sp.SaleNo));
            cmd.Parameters.Add(new SqlParameter("@ONO", sp.ONo));
            cmd.Parameters.Add(new SqlParameter("@VNO", sp.VNo));
            cmd.Parameters.Add(new SqlParameter("@PMode", sp.PMode));
            cmd.Parameters.Add(new SqlParameter("@Amount", sp.Amount));
            cmd.Parameters.Add(new SqlParameter("@Description", sp.Description));
            cmd.Parameters.Add(new SqlParameter("@DDate", sp.DDate));
            cmd.Parameters.Add(new SqlParameter("@DRate", sp.DRate));
            cmd.Parameters.Add(new SqlParameter("@PTime", sp.PTime));
            cmd.Parameters.Add(new SqlParameter("@Receiveables", sp.Receiveable));
            cmd.Parameters.Add(new SqlParameter("@BDRate", sp.BDrate));
            cmd.Parameters.Add(new SqlParameter("@BankName", sp.BankName));
            cmd.Parameters.Add(new SqlParameter("@DAccountCode", sp.DAccountCode));
            //if (sp.GoldOfCash == null)
            //{
            //    cmd.Parameters.Add(new SqlParameter("@GoldOfCash", 0));
            //}
            //else
            //    cmd.Parameters.Add(new SqlParameter("@GoldOfCash", 0));
            if (sp.SaleManId == 0)
            {
                cmd.Parameters.Add("@SaleManId", SqlDbType.Int);
                cmd.Parameters["@SaleManId"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@SaleManId", sp.SaleManId));
            cmd.Parameters.Add("@GoldOfCash", SqlDbType.Float);
            cmd.Parameters["@GoldOfCash"].Value = DBNull.Value;
            cmd.Parameters.Add(new SqlParameter("@CustId", sp.CustId));
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
                //{
                //    con.Close();
                //    cmd.Dispose();

                //}
            }


        }
        public Sale GetRecordBySaleNo(int saleNo)
        {
            //string query = "select * from Sale where SaleNo="+saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecForBalanceRece", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            Sale sal = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sal = new Sale();
                    sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);
                    sal.OrderNo = Convert.ToInt32(dr["OrderNo"]);

                    if (dr["BillBookNo"] == DBNull.Value)
                    {
                        sal.BillBookNo = null;
                    }
                    else
                        sal.BillBookNo = (dr["BillBookNo"]).ToString();

                    if (dr["TotalPrice"] == DBNull.Value)
                    {
                        sal.TotalPrice = null;
                    }
                    else
                        sal.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);

                    sal.CusAccountNo = dr["CustAccountNo"].ToString();
                    if (dr["TNetAmount"] == DBNull.Value)
                    {
                        sal.TotalNetPrice = 0;
                    }
                    else
                        sal.TotalNetPrice = Convert.ToDecimal(dr["TNetAmount"]);

                    sal.Balance = Convert.ToDecimal(dr["Balance"]);
                    sal.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    if (dr["KhataNo"] == DBNull.Value)
                    {
                        sal.KhataNo = 0;
                    }
                    else
                        sal.KhataNo = Convert.ToInt32(dr["KhataNo"]);
                    if (Convert.ToDecimal(dr["BillDiscount"]) == 0)
                    {
                        sal.BillDiscout = 0;
                    }
                    else
                        sal.BillDiscout = Convert.ToDecimal(dr["BillDiscount"]);

                    sal.TReceivedAmount = Convert.ToDecimal(dr["TReceivedAmount"]);
                    sal.SDate = Convert.ToDateTime(dr["SDate"]);
                    sal.CustName = new Customer();
                    sal.CustName.Name = dr["Name"].ToString();
                    sal.CustName.ID = Convert.ToInt32(dr["CustId"]);
                    sal.CustName.Mobile = dr["Mobile"].ToString();
                    sal.CustName.Email = dr["Email"].ToString();
                    sal.CustName.Address = dr["Address"].ToString();
                    if (dr["CashBalance"] == DBNull.Value)
                    {
                        sal.CustName.CashBalance = 0;
                    }
                    else
                        sal.CustName.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    if (dr["GoldBalance"] == DBNull.Value)
                    {
                        sal.CustName.GoldBalance = 0;
                    }
                    else
                        sal.CustName.GoldBalance = Convert.ToDecimal(dr["GoldBalance"]);
                    //sal.CustName = new Customer(dr["FName"].ToString(), dr["Mobile"].ToString(), dr["Email"].ToString(), dr["Address"].ToString());
                    if (dr["CustBalance"] == DBNull.Value)
                    {
                        sal.CustBalance = 0;
                    }
                    else
                        sal.CustBalance = Convert.ToDecimal(dr["CustBalance"]);
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
            return sal;
        }
        public void DeleteSalePayment(int saleNo, string vno)
        {

            string deleteCustomer = "Delete from Sale_Payment where VNO='" + vno + "' and SNO=" + saleNo;
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
        public void DeleteSalePayment(int saleNo, string vno, SqlConnection con,SqlTransaction tran)
        {

            string deleteCustomer = "Delete from Sale_Payment where VNO='" + vno + "' and SNO=" + saleNo;
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();
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
                //con.Close();
            }
        }
        public void DeleteFromSalePayment(int orderNo, string vno)
        {

            string deleteCustomer = "Delete from Sale_Payment where VNO='" + vno + "' and ONO=" + orderNo;
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
        public void DeleteFromSalePayment(int orderNo, string vno, SqlConnection con,SqlTransaction tran)
        {
            string deleteCustomer = "Delete from Sale_Payment where VNO='" + vno +"'";
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                cmdDelete.Transaction = tran;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromGoldDetail(int orderNo, string vno, SqlConnection con, SqlTransaction tran)
        {
            string deleteCustomer = "Delete from GoldDetail where VNO='" + vno + "'";
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                cmdDelete.Transaction = tran;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFromCheque(string vno, SqlConnection con, SqlTransaction tran)
        {

            string deleteCustomer = "Delete from ChequeDetail where VNO='" + vno + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();
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
                //con.Close();
            }
        }
        public void DeleteFromCreditCard(string vno, SqlConnection con, SqlTransaction tran)
        {

            string deleteCustomer = "Delete from CreditCardDetail where VNO='" + vno + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand(deleteCustomer, con);
            cmdDelete.CommandType = CommandType.Text;
            try
            {
                //con.Open();
                //SqlTransaction tran = con.BeginTransaction();
                cmdDelete.Transaction = tran;


                try
                {
                    cmdDelete.ExecuteNonQuery();
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
                //con.Close();
            }
        }
        public List<SalePayment> GetSalePaymentBySaleNo(int saleNo)
        {
            string query = "select * from Sale_Payment where SNO="+saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<SalePayment> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                     records = new List<SalePayment>();
                    if (records == null) records = new List<SalePayment>();

                    do
                    {
                        SalePayment sal = new SalePayment();
                        sal.VNo = (dr["VNO"]).ToString();
                        sal.DDate = Convert.ToDateTime(dr["DDate"]);
                        sal.Amount = Convert.ToDecimal(dr["Amount"]);
                        sal.PMode = (dr["PMode"]).ToString();
                        sal.PTime = (dr["PTime"]).ToString();
                        sal.Description = (dr["Description"]).ToString();
                        records.Add(sal);
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
            return records;
        }

        public List<Gold> GetGoldBySaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd = new Gold();


                        //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                        gd.VNO = (dr["VNO"]).ToString();

                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        if (dr["CustId"] == DBNull.Value)
                            gd.CustId = 0;
                        else
                            gd.CustId = Convert.ToInt32(dr["CustId"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        
                        records.Add(gd);
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
            return records;
        }
        public Sale GetRecordByOrderNo(int orderNo)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRecByOrderNoForBalance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@OrderNo", SqlDbType.Int).Value = orderNo;
            Sale sal = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sal = new Sale();
                    sal.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    if (dr["BillBookNo"] == DBNull.Value)
                        sal.BillBookNo = null;
                    else
                        sal.BillBookNo = (dr["BillBookNo"]).ToString();
                    if (dr["TotalPrice"] == DBNull.Value)
                        sal.TotalPrice = null;
                    else
                        sal.TotalPrice = Convert.ToDecimal(dr["TotalPrice"]);
                    sal.CusAccountNo = dr["CustAccountNo"].ToString();
                    sal.TotalNetPrice = Convert.ToDecimal(dr["NetBill"]);
                    sal.TReceivedAmount = Convert.ToDecimal(dr["TReceivedAmount"]);
                    sal.Advance = Convert.ToDecimal(dr["Advance"]);
                    sal.Balance = Convert.ToDecimal(dr["Balance"]);
                    sal.OrderNo = Convert.ToInt32(dr["OrderNo"]);
                    if (dr["OFixRate"] == DBNull.Value)
                        sal.OFixRate = 0;
                    else
                        sal.OFixRate = Convert.ToDecimal(dr["OFixRate"]);
                    if (Convert.ToDecimal(dr["BillDiscount"]) == 0)
                        sal.BillDiscout = 0;
                    else
                        sal.BillDiscout = Convert.ToDecimal(dr["BillDiscount"]);
                    sal.TReceivedAmount = Convert.ToDecimal(dr["TReceivedAmount"]);
                    sal.GoldReceived = Convert.ToDecimal(dr["GoldReceive"]);
                    sal.ODate = Convert.ToDateTime(dr["ODate"]);
                    sal.CustName = new Customer();
                    sal.CustName.AccountCode = dr["AccountCode"].ToString();
                    sal.CustName.ID= Convert.ToInt32(dr["CustId"].ToString());
                    sal.CustName.Name = dr["Name"].ToString();
                    sal.CustName.Mobile = dr["Mobile"].ToString();
                    sal.CustName.Email = dr["Email"].ToString();
                    sal.CustName.Address = dr["Address"].ToString();
                    sal.CustName.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    sal.CustName.GoldBalance = Convert.ToDecimal(dr["GoldBalance"]);
                    sal.CustBalance = Convert.ToDecimal(dr["CustBalance"]);
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
            return sal;
        }
        public List<SalePayment> GetSalePaymentByOrderNo(int orderNo)
        {
            string query = "select * from Sale_Payment where SNO=0 and ONO=" + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<SalePayment> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<SalePayment>();
                    if (records == null) records = new List<SalePayment>();

                    do
                    {
                        SalePayment sal = new SalePayment();
                        sal = new SalePayment();
                        sal.VNo = (dr["VNO"]).ToString();
                        sal.DDate = Convert.ToDateTime(dr["DDate"]);
                        sal.Amount = Convert.ToDecimal(dr["Amount"]);
                        sal.PMode = (dr["PMode"]).ToString();
                        sal.PTime = (dr["PTime"]).ToString();
                        sal.Description = (dr["Description"]).ToString();
                        if (dr["GoldOfCash"] == DBNull.Value)
                            sal.GoldOfCash = 0;
                        else
                            sal.GoldOfCash = Convert.ToDecimal(dr["GoldOfCash"]);
                        sal.Description = (dr["Description"]).ToString();
                        records.Add(sal);
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
            return records;
        }
        public List<Gold> GetGoldByOrderNo(int orderNo)
        {
            string query = "select * from GoldDetail where SNO = 0 and ONO=" + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd.VNO = (dr["VNO"]).ToString();
                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        records.Add(gd);
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
            return records;
        }
        public SalePayment GetCashRceiveBySaleNo(int saleNo)
        {
            string query = "select IsNULL(Sum(Amount), 0)'Amount' from Sale_Payment where VNO Like 'CRV%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sal = new SalePayment();
                    sal.Amount = Convert.ToDecimal(dr["Amount"]);
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
            return sal;
        }
        public SalePayment GetCashPaymentBySaleNo(int saleNo)
        {
            string query = "select IsNULL(Sum(Amount), 0)'Amount' from Sale_Payment where VNO Like 'CPV%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sal = new SalePayment();
                    sal.Amount = Convert.ToDecimal(dr["Amount"]);
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
            return sal;
        }
        public SalePayment GetCheckRceiveBySaleNo(int saleNo)
        {
            string query = "select * from Sale_Payment where VNO Like 'CHV%' and  SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<SalePayment> records = null;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<SalePayment>();
                    //if (records == null) records = new List<SalePayment>();

                    //do
                    //{

                    sal = new SalePayment();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    sal.VNo = (dr["VNO"]).ToString();

                    sal.DDate = Convert.ToDateTime(dr["DDate"]);

                    sal.Amount = Convert.ToDecimal(dr["Amount"]);

                    sal.PMode = (dr["PMode"]).ToString();
                    sal.PTime = (dr["PTime"]).ToString();
                    //    records.Add(sal);
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
                if (con.State == ConnectionState.Open) con.Close();
            }
            //if (records != null) records.TrimExcess();
            return sal;
        }
        public SalePayment GetCCardRceiveBySaleNo(int saleNo)
        {
            string query = "select * from Sale_Payment where VNO Like 'CCV%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<SalePayment> records = null;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<SalePayment>();
                    //if (records == null) records = new List<SalePayment>();

                    //do
                    //{

                    sal = new SalePayment();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    sal.VNo = (dr["VNO"]).ToString();

                    sal.DDate = Convert.ToDateTime(dr["DDate"]);

                    sal.Amount = Convert.ToDecimal(dr["Amount"]);

                    sal.PMode = (dr["PMode"]).ToString();
                    sal.PTime = (dr["PTime"]).ToString();
                    //    records.Add(sal);
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
                if (con.State == ConnectionState.Open) con.Close();
            }
            //if (records != null) records.TrimExcess();
            return sal;
        }
        public Gold GetUsedGoldBySaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where  GoldType ='1' and Description Like'Used Gold Purchased%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            Gold gd = new Gold();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<Gold>();
                    // if (records == null) records = new List<Gold>();

                    //  do
                    //  {

                    gd = new Gold();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    gd.VNO = (dr["VNO"]).ToString();

                    gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    gd.Weight = Convert.ToDecimal(dr["Weight"]);
                    gd.Rate = Convert.ToDecimal(dr["Rate"]);

                    gd.Amount = Convert.ToDecimal(dr["Amount"]);

                    //   records.Add(gd);
                    //  }
                    while (dr.Read()) ;
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
            return gd;
        }
        public Gold GetPureGoldBySaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where GoldType ='0' and Description Like 'Pure Gold Purchased%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            Gold gd = new Gold();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<Gold>();
                    // if (records == null) records = new List<Gold>();

                    //  do
                    //  {

                    gd = new Gold();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    gd.VNO = (dr["VNO"]).ToString();

                    gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    gd.Weight = Convert.ToDecimal(dr["Weight"]);
                    gd.Rate = Convert.ToDecimal(dr["Rate"]);
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);

                    //   records.Add(gd);
                    //  }
                    while (dr.Read()) ;
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
            return gd;
        }
        public SalePayment GetCashRceiveByOrderNo(int OrderNo)
        {
            string query = "select * from Sale_Payment where VNO Like 'CRV%' and  Description Like 'Cash Recieved%' and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<SalePayment> records = null;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<SalePayment>();
                    //if (records == null) records = new List<SalePayment>();

                    //do
                    //{

                    sal = new SalePayment();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    sal.VNo = (dr["VNO"]).ToString();

                    sal.DDate = Convert.ToDateTime(dr["DDate"]);

                    sal.Amount = Convert.ToDecimal(dr["Amount"]);

                    sal.PMode = (dr["PMode"]).ToString();
                    sal.PTime = (dr["PTime"]).ToString();
                    //    records.Add(sal);
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
                if (con.State == ConnectionState.Open) con.Close();
            }
            //if (records != null) records.TrimExcess();
            return sal;
        }
        public SalePayment GetCheckRceiveByOrderNo(int OrderNo)
        {
            string query = "select * from Sale_Payment where VNO Like 'CHV%' and Description Like 'Cheque Recieved%' and  ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<SalePayment> records = null;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<SalePayment>();
                    //if (records == null) records = new List<SalePayment>();

                    //do
                    //{

                    sal = new SalePayment();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    sal.VNo = (dr["VNO"]).ToString();

                    sal.DDate = Convert.ToDateTime(dr["DDate"]);

                    sal.Amount = Convert.ToDecimal(dr["Amount"]);

                    sal.PMode = (dr["PMode"]).ToString();
                    sal.PTime = (dr["PTime"]).ToString();
                    //    records.Add(sal);
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
                if (con.State == ConnectionState.Open) con.Close();
            }
            //if (records != null) records.TrimExcess();
            return sal;
        }
        public SalePayment GetCCardRceiveByOrderNo(int OrderNo)
        {
            string query = "select * from Sale_Payment where VNO Like 'CCV%' and Description Like 'Cash Recieved By Credit Card%' and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<SalePayment> records = null;
            SalePayment sal = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<SalePayment>();
                    //if (records == null) records = new List<SalePayment>();

                    //do
                    //{

                    sal = new SalePayment();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    sal.VNo = (dr["VNO"]).ToString();

                    sal.DDate = Convert.ToDateTime(dr["DDate"]);

                    sal.Amount = Convert.ToDecimal(dr["Amount"]);

                    sal.PMode = (dr["PMode"]).ToString();
                    sal.PTime = (dr["PTime"]).ToString();
                    //    records.Add(sal);
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
                if (con.State == ConnectionState.Open) con.Close();
            }
            //if (records != null) records.TrimExcess();
            return sal;
        }
        public SalePayment GetCheckDetailByOrderNo(int OrderNo)
        {
            string query = "select Max(sp.Description)'Description', Sum(sp.Amount)'Amount',(select MAx(accountname) from vouchers where OrderNO=" + OrderNo + " and VNO like '%CHV%' and dr>0)as AccountCode from Sale_Payment sp where  sp.PMode ='Cheque'  and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            SalePayment gd = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    gd = new SalePayment();
                    //gd.VNO = (dr["VNO"]).ToString();
                    gd.Description = (dr["Description"]).ToString();
                    gd.DAccountCode = (dr["AccountCode"]).ToString();
                   // gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    //gd.Weight = Convert.ToDecimal(dr["Weight"]);
                   // gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                   // gd.Rate = Convert.ToDecimal(dr["Rate"]);
                   // gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);
                  //  gd.Karat = dr["Karrat"].ToString();
                    while (dr.Read()) ;
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
            return gd;
        }
        public SalePayment GetCreditCardDetailByOrderNo(int OrderNo)
        {
            string query = "select Sum(sp.Amount)'Amount', Max(sp.DRate)'DRate', Max(sp.Receiveables)'Receiveables', Max(sp.BankName)'BankName', Max(sp.BDrate)'BDrate',(select max(accountname) from vouchers where OrderNO=" + OrderNo + " and VNO like '%CCV%' and dr>0)as AccountCode from Sale_Payment sp where  sp.PMode ='Credit Card'  and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            SalePayment gd = new SalePayment();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    gd = new SalePayment();
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);
                    gd.DRate = Convert.ToDecimal(dr["DRate"]);
                    gd.Receiveable = Convert.ToDecimal(dr["Receiveables"]);
                    gd.BankName = (dr["BankName"]).ToString();
                    gd.BDrate = Convert.ToDecimal(dr["BDrate"]);
                    gd.DAccountCode = (dr["AccountCode"]).ToString();                  
                    while (dr.Read()) ;
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
            return gd;
        }
        public Gold GetUsedGoldByOrderNo(int OrderNo)
        {
            string query = "select Max(VNO)'VNO', Max(PGDate)'PGDate', Sum(Weight)'Weight', Sum(PWeight)'PWeight', Max(Rate)'Rate', Max(Kaat)'Kaat', Sum(Amount)'Amount', Max(Karrat)'Karrat' from GoldDetail where  GoldType ='1' and Description Like'Used Gold Purchased%' and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            Gold gd = new Gold();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {   
               
                    gd = new Gold();
                    gd.VNO = (dr["VNO"]).ToString();

                    gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    gd.Weight = Convert.ToDecimal(dr["Weight"]);
                    gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                    gd.Rate = Convert.ToDecimal(dr["Rate"]);
                    gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);
                    gd.Karat = dr["Karrat"].ToString();
                    while (dr.Read()) ;
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
            return gd;
        }
        public Gold GetPureGoldByOrderNo(int OrderNo)
        {
            string query = "select Max(VNO)'VNO', Max(PGDate)'PGDate', Sum(Weight)'Weight', Max(Rate)'Rate', Sum(Amount)'Amount' from GoldDetail where  GoldType ='0'   and Description Like 'Pure Gold Purchased%' and ONO=" + OrderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            Gold gd = new Gold();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<Gold>();
                    // if (records == null) records = new List<Gold>();

                    //  do
                    //  {

                    gd = new Gold();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    gd.VNO = (dr["VNO"]).ToString();

                    gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    gd.Weight = Convert.ToDecimal(dr["Weight"]);
                    gd.Rate = Convert.ToDecimal(dr["Rate"]);
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);

                    //   records.Add(gd);
                    //  }
                    while (dr.Read()) ;
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
            return gd;
        }
        public Gold GetPureGoldByOtherChergesSaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where  GoldType ='0'   and Description Like 'Pure Gold Purchased From Other Charges%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@SaleNo", SqlDbType.Int).Value = saleNo;
            // List<Gold> records = null;
            Gold gd = new Gold();
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    // records = new List<Gold>();
                    // if (records == null) records = new List<Gold>();

                    //  do
                    //  {

                    gd = new Gold();


                    //sal.SaleNo = Convert.ToInt32(dr["SaleNo"]);


                    gd.VNO = (dr["VNO"]).ToString();

                    gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                    gd.Weight = Convert.ToDecimal(dr["Weight"]);
                    gd.Rate = Convert.ToDecimal(dr["Rate"]);
                    gd.Amount = Convert.ToDecimal(dr["Amount"]);

                    //   records.Add(gd);
                    //  }
                    while (dr.Read()) ;
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
            return gd;
        }
        public List<Gold> GetUGoldListBySaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where GoldType = '1' and Description Like 'Used Gold Purchased%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd.SNO = Convert.ToInt32(dr["SNo"]);
                        gd.VNO = (dr["VNO"]).ToString();
                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                        gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                        gd.RemainingWork = dr["RemainingWork"].ToString();
                        gd.Description = gd.RemainingWork;
                        gd.Karat = (dr["Karrat"]).ToString();
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        if (dr["GoldType"].ToString() == "0")
                            gd.GoldType = GoldType.Pure;
                        else
                            gd.GoldType = GoldType.Used;
                        records.Add(gd);
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
        public List<Gold> GetPGoldListBySaleNo(int saleNo)
        {
            string query = "select * from GoldDetail where GoldType = '0' and Description Like 'Pure Gold Purchased%' and SNO=" + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd.SNO = Convert.ToInt32(dr["SNo"]);
                        gd.VNO = (dr["VNO"]).ToString();
                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                        gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                        gd.RemainingWork = (dr["RemainingWork"]).ToString();
                        gd.Description = gd.RemainingWork;
                        gd.Karat = (dr["Karrat"]).ToString();
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        if (dr["GoldType"].ToString() == "0")
                            gd.GoldType = GoldType.Pure;
                        else
                            gd.GoldType = GoldType.Used;
                        records.Add(gd);
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
        public List<Cheques> GetChequeListBySaleNo(int saleNo)
        {
            string query = "select ch.*, b.AccountId, b.AccountCode from ChequeDetail ch inner join BankAccount b on ch.AccountNo=b.AccountNo where SNO = " + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Cheques> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Cheques>();
                    if (records == null) records = new List<Cheques>();

                    do
                    {
                        Cheques ch = new Cheques();
                        ch.SNO = Convert.ToInt32(dr["SNo"]);
                        ch.VNO = (dr["VNO"]).ToString();
                        ch.Amount = Convert.ToDecimal(dr["Amount"]);
                        ch.ChequeDate = Convert.ToDateTime(dr["DDate"]);
                        ch.ChequeNo = (dr["ChequeNo"]).ToString();
                        ch.Description = (dr["Description"]).ToString();
                        ch.BankName = new Banks();
                        ch.BankName.Id = Convert.ToInt32(dr["BankId"]);
                        ch.BankName.BankName = (dr["BankName"]).ToString();
                        ch.DepositInAccount = new BankAccount();
                        ch.DepositInAccount.AccountNo = (dr["AccountNo"]).ToString();
                        ch.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                        ch.DepositInAccount.AccountCode = new ChildAccount();
                        ch.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
                        ch.BankAccount = Convert.ToString(dr["AccountNo"]);
                        ch.Status = Convert.ToString(dr["Status"]);
                        ch.CustAccountCode = Convert.ToString(dr["CusAccountCode"]);
                        records.Add(ch);
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
        public List<CreditCard> GetCCardListBySaleNo(int saleNo)
        {
            string query = "select cd.*, b.AccountId, (Select BankName from Bank where Id = cd.BankId)'BankName' from CreditCardDetail cd inner join BankAccount b on cd.DepositeInaccount=b.AccountNo where SNO = " + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<CreditCard> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<CreditCard>();
                    if (records == null) records = new List<CreditCard>();

                    do
                    {
                        CreditCard cc = new CreditCard();
                        cc.SNO = Convert.ToInt32(dr["SNo"]);
                        cc.VNO = dr["VNO"].ToString();
                        cc.MachineName = dr["MachineName"].ToString();
                        cc.Amount = Convert.ToDecimal(dr["Amount"]);
                        cc.DeductRate = Convert.ToDecimal(dr["DeductRate"]);
                        cc.SwapAmount = Convert.ToDecimal(dr["SwapAmount"]);
                        cc.BankName = new Banks();
                        cc.BankName.Id = Convert.ToInt32(dr["BankId"]);
                        cc.BankName.BankName = Convert.ToString(dr["BankName"]);
                        cc.BankDeductRate = Convert.ToDecimal(dr["BankDeductRate"]);
                        cc.AmountDepositeBank = Convert.ToDecimal(dr["AmountDepositeBank"]);
                        cc.DepositInAccount = new BankAccount();
                        cc.DepositInAccount.AccountNo = dr["DepositeInaccount"].ToString();
                        cc.DepositInAccount.AccountCode = new ChildAccount();
                        cc.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
                        cc.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                        cc.Description = dr["Description"].ToString();
                        cc.Status = dr["Status"].ToString();
                        records.Add(cc);
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
        public List<Gold> GetUGoldListByOrderNo(int orderNo)
        {
            string query = "select * from GoldDetail where GoldType = '1' and Description Like 'Used Gold Purchased%' and SNO = 0 and ONO=" + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd.ONO = Convert.ToInt32(dr["ONo"]);
                        gd.VNO = (dr["VNO"]).ToString();
                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                        gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                        gd.RemainingWork = dr["RemainingWork"].ToString();
                        gd.Description = gd.RemainingWork;
                        gd.Karat = (dr["Karrat"]).ToString();
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        if (dr["GoldType"].ToString() == "0")
                            gd.GoldType = GoldType.Pure;
                        else
                            gd.GoldType = GoldType.Used;
                        records.Add(gd);
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
        public List<Gold> GetPGoldListByOrderNo(int orderNo)
        {
            string query = "select * from GoldDetail where GoldType = '0' and Description Like 'Pure Gold Purchased%' and SNO = 0 and ONO=" + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Gold> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Gold>();
                    if (records == null) records = new List<Gold>();

                    do
                    {
                        Gold gd = new Gold();
                        gd.ONO = Convert.ToInt32(dr["ONo"]);
                        gd.VNO = (dr["VNO"]).ToString();
                        gd.PGDate = Convert.ToDateTime(dr["PGDate"]);
                        gd.Weight = Convert.ToDecimal(dr["Weight"]);
                        gd.Kaat = Convert.ToDecimal(dr["Kaat"]);
                        gd.PWeight = Convert.ToDecimal(dr["PWeight"]);
                        gd.RemainingWork = (dr["RemainingWork"]).ToString();
                        gd.Description = gd.RemainingWork;
                        gd.Karat = (dr["Karrat"]).ToString();
                        gd.Rate = Convert.ToDecimal(dr["Rate"]);
                        gd.Amount = Convert.ToDecimal(dr["Amount"]);
                        if (dr["GoldType"].ToString() == "0")
                            gd.GoldType = GoldType.Pure;
                        else
                            gd.GoldType = GoldType.Used;
                        records.Add(gd);
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
        public List<Cheques> GetChequeListByOrderNo(int orderNo)
        {
            string query = "select ch.*, b.AccountId, b.AccountCode from ChequeDetail ch inner join BankAccount b on ch.AccountNo=b.AccountNo where SNO = 0 and ONO = " + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Cheques> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<Cheques>();
                    if (records == null) records = new List<Cheques>();

                    do
                    {
                        Cheques ch = new Cheques();
                        ch.ONO = Convert.ToInt32(dr["ONo"]);
                        ch.VNO = (dr["VNO"]).ToString();
                        ch.Amount = Convert.ToDecimal(dr["Amount"]);
                        ch.ChequeDate = Convert.ToDateTime(dr["DDate"]);
                        ch.ChequeNo = (dr["ChequeNo"]).ToString();
                        ch.Description = (dr["Description"]).ToString();
                        ch.BankName = new Banks();
                        ch.BankName.Id = Convert.ToInt32(dr["BankId"]);
                        ch.BankName.BankName = (dr["BankName"]).ToString();
                        ch.DepositInAccount = new BankAccount();
                        ch.DepositInAccount.AccountNo = (dr["AccountNo"]).ToString();
                        ch.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                        ch.DepositInAccount.AccountCode = new ChildAccount();
                        ch.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
                        ch.BankAccount = Convert.ToString(dr["AccountNo"]);
                        records.Add(ch);
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
        public List<CreditCard> GetCCardListByOrderNo(int orderNo)
        {
            string query = "select cd.*, b.AccountId, (Select BankName from Bank where Id = cd.BankId)'BankName' from CreditCardDetail cd inner join BankAccount b on cd.DepositeInaccount=b.AccountNo where SNO = 0 and ONO = " + orderNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<CreditCard> records = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    records = new List<CreditCard>();
                    if (records == null) records = new List<CreditCard>();

                    do
                    {
                        CreditCard cc = new CreditCard();
                        cc.ONO = Convert.ToInt32(dr["ONo"]);
                        cc.VNO = dr["VNO"].ToString();
                        cc.MachineName = dr["MachineName"].ToString();
                        cc.Amount = Convert.ToDecimal(dr["Amount"]);
                        cc.DeductRate = Convert.ToDecimal(dr["DeductRate"]);
                        cc.SwapAmount = Convert.ToDecimal(dr["SwapAmount"]);
                        cc.BankName = new Banks();
                        cc.BankName.Id = Convert.ToInt32(dr["BankId"]);
                        cc.BankName.BankName = Convert.ToString(dr["BankName"]);
                        cc.BankDeductRate = Convert.ToDecimal(dr["BankDeductRate"]);
                        cc.AmountDepositeBank = Convert.ToDecimal(dr["AmountDepositeBank"]);
                        cc.DepositInAccount = new BankAccount();
                        cc.DepositInAccount.AccountNo = dr["DepositeInaccount"].ToString();
                        cc.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                        cc.Description = dr["Description"].ToString();
                        cc.DepositInAccount.AccountCode = new ChildAccount();
                        cc.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
                        records.Add(cc);
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
        public Cheques GetChequeBySaleNo(int saleNo)
        {
            string query = "select ch.*, b.AccountId, b.AccountCode from ChequeDetail ch inner join BankAccount b on ch.AccountNo=b.AccountNo where ONO = 0 and SNO = " + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            Cheques ch = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ch = new Cheques();
                    ch.SNO = Convert.ToInt32(dr["SNo"]);
                    ch.VNO = (dr["VNO"]).ToString();
                    ch.Amount = Convert.ToDecimal(dr["Amount"]);
                    ch.ChequeDate = Convert.ToDateTime(dr["DDate"]);
                    ch.ChequeNo = (dr["ChequeNo"]).ToString();
                    ch.Description = (dr["Description"]).ToString();
                    ch.BankName = new Banks();
                    ch.BankName.Id = Convert.ToInt32(dr["BankId"]);
                    ch.BankName.BankName = (dr["BankName"]).ToString();
                    ch.DepositInAccount = new BankAccount();
                    ch.DepositInAccount.AccountNo = (dr["AccountNo"]).ToString();
                    ch.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                    ch.DepositInAccount.AccountCode = new ChildAccount();
                    ch.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
                    ch.BankAccount = Convert.ToString(dr["AccountNo"]);
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
            return ch;
        }
        public CreditCard GetCCardBySaleNo(int saleNo)
        {
            string query = "select cd.*, b.AccountId, (Select BankName from Bank where Id = cd.BankId)'BankName' from CreditCardDetail cd inner join BankAccount b on cd.DepositeInaccount=b.AccountNo where ONO = 0 and SNO = " + saleNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            CreditCard cc = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                        cc = new CreditCard();
                        cc.SNO = Convert.ToInt32(dr["SNo"]);
                        cc.VNO = dr["VNO"].ToString();
                        cc.MachineName = dr["MachineName"].ToString();
                        cc.Amount = Convert.ToDecimal(dr["Amount"]);
                        cc.DeductRate = Convert.ToDecimal(dr["DeductRate"]);
                        cc.SwapAmount = Convert.ToDecimal(dr["SwapAmount"]);
                        cc.BankName = new Banks();
                        cc.BankName.Id = Convert.ToInt32(dr["BankId"]);
                        cc.BankName.BankName = Convert.ToString(dr["BankName"]);
                        cc.BankDeductRate = Convert.ToDecimal(dr["BankDeductRate"]);
                        cc.AmountDepositeBank = Convert.ToDecimal(dr["AmountDepositeBank"]);
                        cc.DepositInAccount = new BankAccount();
                        cc.DepositInAccount.AccountNo = dr["DepositeInaccount"].ToString();
                        cc.DepositInAccount.Id = Convert.ToInt32(dr["AccountId"]);
                        cc.Description = dr["Description"].ToString();
                        cc.DepositInAccount.AccountCode = new ChildAccount();
                        cc.DepositInAccount.AccountCode.ChildCode = dr["AccountCode"].ToString();
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
            return cc;
        }
    }
}
