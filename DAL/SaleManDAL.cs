using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

namespace DAL
{
    public class SaleManDAL
    {
        public void AddSaleMan(SaleMan saleMan)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddSaleMan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@AccountCode", saleMan.AccountCode.ChildCode ));
            cmd.Parameters.Add(new SqlParameter("@Name", saleMan.Name));
            cmd.Parameters.Add(new SqlParameter("@Address", saleMan.Address));
            cmd.Parameters.Add(new SqlParameter("@ContactNo", saleMan.ContactNo));
            cmd.Parameters.Add(new SqlParameter("@CNIC", saleMan.CNIC));
            cmd.Parameters.Add(new SqlParameter("@Salary", saleMan.Salary));
            cmd.Parameters.Add(new SqlParameter("@Date", saleMan.DateOfBirth));
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";
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
        public void UpdateSaleMan(int Id, SaleMan saleMan)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdate = new SqlCommand("UpdateSaleMan", con);
            cmdUpdate.CommandType = CommandType.StoredProcedure;
            cmdUpdate.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

            cmdUpdate.Parameters.Add(new SqlParameter("@Name", saleMan.Name));
            cmdUpdate.Parameters.Add(new SqlParameter("@Address", saleMan.Address));
            cmdUpdate.Parameters.Add(new SqlParameter("@ContactNo", saleMan.ContactNo));
            cmdUpdate.Parameters.Add(new SqlParameter("@CNIC", saleMan.CNIC));
            //cmdUpdate.Parameters.Add(new SqlParameter("@Status", saleMan.Status));
            cmdUpdate.Parameters.Add(new SqlParameter("@Salary", saleMan.Salary));

            try
            {
                con.Open();
                cmdUpdate.ExecuteNonQuery();
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
        public void AddSAttendence(SaleMan saleMan)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("insert into SaleMan_Attendance values ( " + saleMan.ID + " ,'" + saleMan.AttDate + "','" + saleMan.AttTime + "','" + saleMan.Status + "' )", con);
           
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
        public void UpdateSAttendence(DateTime oldDt, SaleMan saleMan)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            //SqlCommand cmdUpdate = new SqlCommand("update SaleMan_Attendance set ADate = '" + saleMan.AttDate + "', Atime = '" + saleMan.AttTime + "', Status = '" + saleMan.Status + "' where Id = " + (int)saleMan.ID + " and convert (varchar ,ADate,112) =  convert (varchar ,"+oldDt+",112)", con);
            SqlCommand cmdUpdate = new SqlCommand("UpdateSAttendence", con);
            cmdUpdate.CommandType = CommandType.StoredProcedure;
            cmdUpdate.Parameters.Add("@AttDate", SqlDbType.DateTime).Value = oldDt;
            cmdUpdate.Parameters.Add("@OldId", SqlDbType.Int).Value = saleMan.ID;

            cmdUpdate.Parameters.Add(new SqlParameter("@AttTime", saleMan.AttTime));
            cmdUpdate.Parameters.Add(new SqlParameter("@Status", saleMan.Status));

            try
            {
                con.Open();
                cmdUpdate.ExecuteNonQuery();
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
        public void AddAdvance(SaleMan saleMan, SqlConnection con, SqlTransaction trans)
        {
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddAdvance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            cmd.Parameters.Add(new SqlParameter("@SmId", saleMan.ID));
            cmd.Parameters.Add(new SqlParameter("@AdvanceNo", saleMan.AdvanceNo));
            cmd.Parameters.Add(new SqlParameter("@GDate", saleMan.AttDate));
            cmd.Parameters.Add(new SqlParameter("@AdvanceAmount", saleMan.AdvAmount));
            cmd.Parameters.Add(new SqlParameter("@NoOfInstallment", saleMan.NoOfInst));
            cmd.Parameters.Add(new SqlParameter("@InstallmentAmount", saleMan.InstallAmount));
            cmd.Parameters.Add(new SqlParameter("@Status", saleMan.Status));
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
                //}
            }
        }
        public void AddSalary(SaleMan saleMan, SqlConnection con, SqlTransaction trans)
        {
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddSalary", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            cmd.Parameters.Add(new SqlParameter("@SmId", saleMan.ID));
            cmd.Parameters.Add(new SqlParameter("@AdvanceNo", saleMan.AdvanceNo));
            cmd.Parameters.Add(new SqlParameter("@Salary", saleMan.Salary));
            cmd.Parameters.Add(new SqlParameter("@GDate", saleMan.AttDate));
            cmd.Parameters.Add(new SqlParameter("@InstallmentAmount", saleMan.InstallAmount));
            cmd.Parameters.Add(new SqlParameter("@Alownce", saleMan.Alownce));
            cmd.Parameters.Add(new SqlParameter("@Description", saleMan.Description));
            //cmd.Parameters.Add(new SqlParameter("@Status", saleMan.Status));
            try
            {
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
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    //con.Close();
                //}
            }
        }
        public void AddIncreOrDecre(SaleMan saleMan)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddIncreOrDecre", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@SmId", saleMan.ID));
            cmd.Parameters.Add(new SqlParameter("@Salary", saleMan.Salary));
            if (saleMan.IncreDate != null)
            {
                cmd.Parameters.Add(new SqlParameter("@Date", saleMan.IncreDate));
                cmd.Parameters.Add(new SqlParameter("@IncrDate", saleMan.IncreDate));
                cmd.Parameters.Add(new SqlParameter("@Increment", saleMan.IncreAmount));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@IncrDate", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Increment", DBNull.Value));
            }
            if (saleMan.DecreDate != null)
            {
                cmd.Parameters.Add(new SqlParameter("@Date", saleMan.DecreDate));
                cmd.Parameters.Add(new SqlParameter("@DecrDate", saleMan.DecreDate));
                cmd.Parameters.Add(new SqlParameter("@Decrement", saleMan.DecreAmount));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@DecrDate", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@Decrement", DBNull.Value));
            }
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
        public SaleMan GetSaleManById(int id)
        {
            string selectsql = "select * from SaleMan where Id=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.CommandType = CommandType.Text;
            SaleMan slm = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (slm == null) slm = new SaleMan();

                    slm.ID = Convert.ToInt32(dr["Id"]);
                    slm.Name = dr["Name"].ToString();
                    slm.CNIC = dr["CNIC"].ToString();
                    slm.Address = dr["Address"].ToString();
                    slm.ContactNo = dr["ContactNo"].ToString();
                    slm.Salary = Convert.ToDecimal(dr["Salary"]);
                    slm.DateOfBirth = Convert.ToDateTime(dr["Date"]);
                    slm.AccountCode = new ChildAccount(dr["AccountCode"].ToString());
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

            return slm;
        }
        public List<SaleMan> GetAllSaleMen()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT  slm.Id, slm.Name, slm.ContactNo, slm.CNIC, slm.Address, slm.AccountCode, slm.Status, slm.Date, " +
            "(select Salary from SalaryRecord where smid = slm.id and id = (Select max(id) from SalaryRecord where smid = slm.id))'Salary' FROM SaleMan  slm   where Status='Available'", con);
            cmd.CommandType = CommandType.Text;
            List<SaleMan> slms = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    slms = new List<SaleMan>();

                    do
                    {
                        SaleMan slm = new SaleMan();
                        slm.ID = Convert.ToInt32(dr["Id"]);
                        slm.Name = dr["Name"].ToString();
                        slm.Address = dr["Address"].ToString();
                        slm.ContactNo = dr["ContactNo"].ToString();
                        slm.CNIC = dr["CNIC"].ToString();
                        slm.Salary = Convert.ToDecimal(dr["Salary"]);
                        slm.AccountCode = new ChildAccount( dr["AccountCode"].ToString());

                        slms.Add(slm);
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
            if (slms != null)
                slms.TrimExcess();
            return slms;
        }
        public List<SaleMan> GetAttendenceHist(int k)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("Select * from SaleMan_Attendance where Id="+k, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@Id", SqlDbType.Int).Value = k;
            List<SaleMan> slms = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    slms = new List<SaleMan>();

                    do
                    {
                        SaleMan slm = new SaleMan();
                        slm.ID = Convert.ToInt32(dr["Id"]);
                        slm.AttDate =Convert.ToDateTime(dr["ADate"]);
                        slm.AttTime = Convert.ToDateTime(dr["ATime"]);
                        slm.Status = dr["Status"].ToString();
                       

                        slms.Add(slm);
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
            if (slms != null)
                slms.TrimExcess();
            return slms;
        }
        public bool isDateExist(DateTime dt, int smid)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("IsAttDateExist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@AttDate", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = smid;
            bool bFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bFlag = true;
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
            return bFlag;

        }
        public bool IsNameExist(string name)
        {
            string querry = "select Name from SaleMan where Name='" + name + "'";
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
        public bool IsContacNoExist(string contactNo)
        {
            string querry = "select ContactNo from SaleMan where ContactNo='" + contactNo + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool cFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    cFlag = true;


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
            return cFlag;
        }
        public void DeleteSaleMan(int Id,string accountCode)
        {
            string query = "update SaleMan set Status ='Deleted' where Id=" +Id ;
            string query1 = "delete from ChildAccount where ChildCode='"+accountCode+"'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            con.Open();
            SqlTransaction trans = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction=trans;
               
            SqlCommand cmd2 = new SqlCommand(query1, con);
            cmd2.CommandType = CommandType.Text;
            cmd2.Transaction=trans;
           
            try
            {
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    trans.Commit();
                    con.Close();
                }
            }
        }
        public void DeleteAttendence(int Id)
        {
            string query = "delete from SaleMan_Attendance where Id=" + Id +" and Convert(varchar,ADate, 112)= convert(varchar,getdate(), 112)";
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
        public int GetMaxAdvanceNo()
        {
            string querry = "Select MAX(AdvanceNo) as [MaxNo] from SaleMan_Advance";
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
                    if (dr["MaxNo"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["MaxNo"]);
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
        public int GetDaysOfAttendence(DateTime dt,int id)
        {
            //string querry = "select COUNT(Id) as Count from SaleMan_Attendance where Status='Present' and MONTH(ADate)=MONTH("+dt+")";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetDaysOfAttendence", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            int saleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["Count"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["Count"]);
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
        public int GetDaysOfHalfAttendence(DateTime dt, int id)
        {
            //string querry = "select COUNT(Id) as Count from SaleMan_Attendance where Status='Present' and MONTH(ADate)=MONTH("+dt+")";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select COUNT(Id) as Count from SaleMan_Attendance where Status='HalfDay' and Month(ADate)=Month('" + dt + "') and Id=" + id, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = dt;
            //cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
            int saleNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);


                //SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["Count"] == DBNull.Value)
                        saleNo = 0;
                    else
                        saleNo = Convert.ToInt32(dr["Count"]);
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
        public List<SaleMan> GetAdvanceHistByEmp(int k)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("Select sa.*,(select (ISNULL (SUM (InstAmount),0)) from SaleMan_Inst where AdvanceNo = sa.AdvanceNo  and SmId = sa.SmId )'AmountPaid' from SaleMan_Advance sa where sa.Status = 'Pending' and  sa.SmId=" + k, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@Id", SqlDbType.Int).Value = k;
            List<SaleMan> slms = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    slms = new List<SaleMan>();

                    do
                    {
                        SaleMan slm = new SaleMan();
                        slm.ID = Convert.ToInt32(dr["SmId"]);
                        slm.AttDate =Convert.ToDateTime(dr["GDate"]);
                        slm.AdvanceNo = Convert.ToInt32(dr["AdvanceNo"]);
                        slm.AdvAmount = Convert.ToDecimal(dr["AdvanceAmount"]);
                        slm.NoOfInst = Convert.ToInt32(dr["NoOfInstallment"]);
                        slm.PaidAmount = Convert.ToDecimal(dr["AmountPaid"]);
                        slm.InstallAmount = Convert.ToDecimal(dr["InstallmentAmount"]);
                        try
                        {
                            slm.NoOfInstPaid = Convert.ToInt32((decimal)slm.PaidAmount / (decimal)slm.InstallAmount);
                        }
                        catch { slm.NoOfInstPaid = 0; }
                       
                        slm.RemainingAmount = slm.AdvAmount - slm.PaidAmount;
                        slm.Status = dr["Status"].ToString();
                        slms.Add(slm);
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
            if (slms != null)
                slms.TrimExcess();
            return slms;
        }
        public List<SaleMan> GetAllAdvanceNosById(string query)
        {
            //string getRecord = "select AdvanceNo from SaleMan_Advance Where SmId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<SaleMan> records = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (records == null) records = new List<SaleMan>();

                    do
                    {
                        SaleMan slm = new SaleMan();
                        slm.AdvanceNo = Convert.ToInt32(dr["AdvanceNo"]);
                        records.Add(slm);
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
        public SaleMan GetInstallAmount(int advNo)
        {
            string query = "Select sa.*,(select (ISNULL (SUM (InstAmount),0)) from SaleMan_Inst where AdvanceNo = sa.AdvanceNo  and SmId = sa.SmId )'AmountPaid' from SaleMan_Advance sa where AdvanceNo =" + advNo;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SaleMan slm = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (slm == null) slm = new SaleMan();
                    slm.ID = Convert.ToInt32(dr["SmId"]);
                    slm.AttDate = Convert.ToDateTime(dr["GDate"]);
                    slm.AdvanceNo = Convert.ToInt32(dr["AdvanceNo"]);
                    slm.AdvAmount = Convert.ToDecimal(dr["AdvanceAmount"]);
                    slm.NoOfInst = Convert.ToInt32(dr["NoOfInstallment"]);
                    slm.PaidAmount = Convert.ToDecimal(dr["AmountPaid"]);
                    slm.InstallAmount = Convert.ToDecimal(dr["InstallmentAmount"]);
                    if (slm.NoOfInst != 0)
                        slm.NoOfInstPaid = Convert.ToInt32((decimal)slm.PaidAmount / (decimal)slm.InstallAmount);
                    else
                        slm.NoOfInstPaid = 0;
                    slm.RemainingAmount = slm.AdvAmount - slm.PaidAmount;
                    slm.Status = dr["Status"].ToString();
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
            return slm;
        }

        public bool isSalaryMonthExist(DateTime dt,int smid)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("IsSalaryMonthExist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = dt;
            cmd.Parameters.Add("@SmId", SqlDbType.Int).Value = smid;
            bool bFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    bFlag = true;
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
            return bFlag;
        }

        public string GetSalaryCalculation()
        {
            string querry = "Select SalaryCalculation from StartUp";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            string salaryCalculation = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["SalaryCalculation"] == DBNull.Value)
                        salaryCalculation = "";
                    else
                        salaryCalculation = Convert.ToString(dr["SalaryCalculation"]);
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
            return salaryCalculation;
        }
    }
}
