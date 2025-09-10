using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class CustomerDAL
    {
        private string AllCustomer = "select ci.*, ct.*, ctr.*, (((select isnull(sum(OpeningCash),0) from ChildAccount where ChildCode=ci.AccountCode) + (select isnull(sum(Dr),0) from vouchers where AccountCode=ci.AccountCode)) - (select isnull(sum(Cr),0) from vouchers where AccountCode=ci.AccountCode)) as Balance from CustomerInfo ci Left outer join  City ct on ct.cityId  = ci.CityId Left Outer join Country ctr on ctr.countryId = ci.countryId";
        //private string AllCustByName = "select * from CustomerInfo where FName like="+name;
        private string updateCustomer = "updateCustomer";

        public void AddCustomer(Customer cust)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@AccountCode", cust.AccountCode));
            cmd.Parameters.Add(new SqlParameter("@Salutation", cust.Salutation));
            cmd.Parameters.Add(new SqlParameter("@Name", cust.Name));
            cmd.Parameters.Add(new SqlParameter("@CO", cust.CO));
            cmd.Parameters.Add(new SqlParameter("@Mobile", cust.Mobile));
            cmd.Parameters.Add(new SqlParameter("@CNIC", cust.CNIC));
            cmd.Parameters.Add(new SqlParameter("@TelHome", cust.TelHome));
            cmd.Parameters.Add(new SqlParameter("@HouseNo", cust.HouseNo));
            cmd.Parameters.Add(new SqlParameter("@Near", cust.Near));
            cmd.Parameters.Add(new SqlParameter("@Colony", cust.Colony));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", cust.Date));
            cmd.Parameters.Add(new SqlParameter("@CityId", cust.CityId .CityId ));
            cmd.Parameters.Add(new SqlParameter("@BlockNo", cust.BlockNo));
            cmd.Parameters.Add(new SqlParameter("@CountryId", cust.CountryId .CountryId ));
            cmd.Parameters.Add(new SqlParameter("@StreetNo", cust.StreetNo));
            cmd.Parameters.Add(new SqlParameter("@Address", cust.Address));
            cmd.Parameters.Add(new SqlParameter("@Email", cust.Email));
            if (cust.DateOfBirth == null)
            {
                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime);
                cmd.Parameters["@BirthDate"].Value = DBNull.Value;
            }
            else
            cmd.Parameters.Add(new SqlParameter("@BirthDate", cust.DateOfBirth));
            if (cust.AnniversaryDate == null)
            {
                cmd.Parameters.Add("@AniversyDate", SqlDbType.DateTime);
                cmd.Parameters["@AniversyDate"].Value = DBNull.Value;
            }
            else
            cmd.Parameters.Add(new SqlParameter("@AniversyDate", cust.AnniversaryDate));
            if (cust.CashBalance == null)
            {
                cmd.Parameters.Add("@CashBalance", SqlDbType.Float);
                cmd.Parameters["@CashBalance"].Value = DBNull.Value;
            }
            else
            cmd.Parameters.Add(new SqlParameter("@CashBalance", cust.CashBalance));
           

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

        public void AddCustomer(Customer cust, SqlConnection con, SqlTransaction trans)
        {
            SqlCommand cmd = new SqlCommand("AddCustomer", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@AccountCode", cust.AccountCode));
            cmd.Parameters.Add(new SqlParameter("@Salutation", cust.Salutation));
            cmd.Parameters.Add(new SqlParameter("@Name", cust.Name));
            cmd.Parameters.Add(new SqlParameter("@CO", cust.CO));
            cmd.Parameters.Add(new SqlParameter("@Mobile", cust.Mobile));
            cmd.Parameters.Add(new SqlParameter("@CNIC", cust.CNIC));
            cmd.Parameters.Add(new SqlParameter("@TelHome", cust.TelHome));
            cmd.Parameters.Add(new SqlParameter("@HouseNo", cust.HouseNo));
            cmd.Parameters.Add(new SqlParameter("@Near", cust.Near));
            cmd.Parameters.Add(new SqlParameter("@Colony", cust.Colony));
            cmd.Parameters.Add(new SqlParameter("@EntryDate", cust.Date));
            cmd.Parameters.Add(new SqlParameter("@CityId", cust.CityId.CityId));
            cmd.Parameters.Add(new SqlParameter("@BlockNo", cust.BlockNo));
            cmd.Parameters.Add(new SqlParameter("@CountryId", cust.CountryId.CountryId));
            cmd.Parameters.Add(new SqlParameter("@StreetNo", cust.StreetNo));
            cmd.Parameters.Add(new SqlParameter("@Address", cust.Address));
            cmd.Parameters.Add(new SqlParameter("@Email", cust.Email));
            if (cust.DateOfBirth == null)
            {
                cmd.Parameters.Add("@BirthDate", SqlDbType.DateTime);
                cmd.Parameters["@BirthDate"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@BirthDate", cust.DateOfBirth));
            if (cust.AnniversaryDate == null)
            {
                cmd.Parameters.Add("@AniversyDate", SqlDbType.DateTime);
                cmd.Parameters["@AniversyDate"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@AniversyDate", cust.AnniversaryDate));
            if (cust.CashBalance == null)
            {
                cmd.Parameters.Add("@CashBalance", SqlDbType.Float);
                cmd.Parameters["@CashBalance"].Value = DBNull.Value;
            }
            else
                cmd.Parameters.Add(new SqlParameter("@CashBalance", cust.CashBalance));
            try
            {
                cmd.Transaction = trans;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                throw ex;
            }
        }

        public List<Customer> GetAllCustomer()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(this.AllCustomer, con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustID"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.CO = dr["CO"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.TelHome  = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.Near = dr["Near"].ToString();
                        cust.CityId = new City (Convert .ToInt32 (dr["CityId"]), dr["CityName"].ToString());
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.CountryId = new Country(Convert.ToInt32(dr["CountryId"]), dr["CountryName"].ToString());
                        cust.Colony = dr["Colony"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.Address = dr["Address"].ToString();
                        cust.Email = dr["Email"].ToString();
                        //if (dr["CashBalance"] == DBNull.Value)
                        //{
                        //    cust.CashBalance = null;
                        //}
                        //else
                        //cust.CashBalance = Convert.ToDecimal (dr["CashBalance"]);
                        if (dr["Balance"] == DBNull.Value)
                        {
                            cust.CashBalance = null;
                        }
                        else
                            cust.CashBalance = Convert.ToDecimal(dr["Balance"]);
                        cust.Email = dr["Email"].ToString();
                        if (dr["BirthDate"] == DBNull.Value)
                        {
                            cust.DateOfBirth = null;
                        }
                        else
                        cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                        {
                            cust.AnniversaryDate = null;
                        }
                        else
                        cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);
                        

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

        public List<Customer> GetAllCustomerForSMS()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select ci.*, ct.*, ctr.* from CustomerInfo ci Left outer join  City ct on ct.cityId  = ci.CityId Left Outer join Country ctr on ctr.countryId = ci.countryId where ci.Mobile<>'' order by ci.name", con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustID"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.CO = dr["CO"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.TelHome = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.Near = dr["Near"].ToString();
                        cust.CityId = new City(Convert.ToInt32(dr["CityId"]), dr["CityName"].ToString());
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.CountryId = new Country(Convert.ToInt32(dr["CountryId"]), dr["CountryName"].ToString());
                        cust.Colony = dr["Colony"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.Address = dr["Address"].ToString();
                        cust.Email = dr["Email"].ToString();
                        if (dr["CashBalance"] == DBNull.Value)
                        {
                            cust.CashBalance = null;
                        }
                        else
                            cust.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                        cust.Email = dr["Email"].ToString();
                        if (dr["BirthDate"] == DBNull.Value)
                        {
                            cust.DateOfBirth = null;
                        }
                        else
                            cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                        {
                            cust.AnniversaryDate = null;
                        }
                        else
                            cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);


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

        public List<Customer> GetAllCustomerSale(string query)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from CustomerInfo where Name like '%" + query + "%'", con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.TelHome = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.Email = dr["Email"].ToString();
                        if (dr["BirthDate"] == DBNull.Value)
                            cust.DateOfBirth = null;
                        else
                            cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                            cust.AnniversaryDate = null;
                        else
                            cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);
                        cust.CO = dr["CO"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.Colony = dr["Colony"].ToString();
                        cust.Address  = dr["Address"].ToString();
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

        public List<Customer> GetAllCustomerSaleMobile(string query)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from CustomerInfo where Mobile like '%" + query + "%'", con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.TelHome = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.Email = dr["Email"].ToString();
                        if (dr["BirthDate"] == DBNull.Value)
                            cust.DateOfBirth = null;
                        else
                            cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                            cust.AnniversaryDate = null;
                        else
                            cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);
                        cust.CO = dr["CO"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.Colony = dr["Colony"].ToString();
                        cust.Address = dr["Address"].ToString();
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
        public List<Customer> GetAllCustomer(string query)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.TelHome = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.Email = dr["Email"].ToString();
                        if (dr["BirthDate"] == DBNull.Value)
                            cust.DateOfBirth = null;
                        else
                            cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                            cust.AnniversaryDate = null;
                        else
                            cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);
                        cust.CO = dr["CO"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.Colony = dr["Colony"].ToString();
                        cust.Address = dr["Address"].ToString();
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
        public void AddPics(JewelPictures jp)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic = new SqlCommand("AddCustomerPictures", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;

            cmdpic.Parameters.Add(new SqlParameter("@CustId", jp.CustId));
            if (jp.ImageMemory == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", jp.ImageMemory));


            if (jp.ImageMemorySmall == null)
            {
                cmdpic.Parameters.Add("@SmallPicture", SqlDbType.Image);
                cmdpic.Parameters["@SmallPicture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@SmallPicture", jp.ImageMemorySmall));
            try
            {

                conpic.Open();

                cmdpic.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conpic.State == ConnectionState.Open)
                    conpic.Close();
            }
        }
        public int GetMaxCustId()
        {
            string querry = "Select MAX(CustId) as [MaxSale] from CustomerInfo";
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

        public Customer GetCustByName(string name)
        {
            string AllCustByName = "select * from CustomerInfo where Name like '%" + name + "%'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCustByName, con);
            cmd.CommandType = CommandType.Text;
            Customer cust = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    
                         cust = new Customer();

                        cust.Name = dr["Name"].ToString();
                        cust.AccountCode = dr["AccountCode"].ToString();


                        cust.Mobile = dr["Mobile"].ToString();

                        //cust.Address = dr["Address"].ToString();

                        cust.CNIC = dr["CNIC"].ToString();


                       
                   


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
            
            return cust;
        }

        public void UpdateCustomer(int oldCust, Customer newCust)
        {
            SqlConnection conn = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateEmploye = new SqlCommand(this.updateCustomer, conn);
            cmdUpdateEmploye.CommandType = CommandType.StoredProcedure;

            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Salutation", newCust.Salutation));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Name", newCust.Name));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@CO", newCust.CO));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Mobile", newCust.Mobile));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@CNIC", newCust.CNIC));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@TelHome", newCust.TelHome));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@HouseNo", newCust.HouseNo));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Near", newCust.Near));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Colony", newCust.Colony));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@CityId", newCust.CityId.CityId ));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@BlockNo", newCust.BlockNo));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@CountryId", newCust.CountryId.CountryId ));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@StreetNo", newCust.StreetNo));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Address", newCust.Address));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@Email", newCust.Email));
            if (newCust.DateOfBirth == null)
            {
                cmdUpdateEmploye.Parameters.Add("@BirthDate", SqlDbType.DateTime);
                cmdUpdateEmploye.Parameters["@BirthDate"].Value = DBNull.Value;
            }
            else
                cmdUpdateEmploye.Parameters.Add(new SqlParameter("@BirthDate", newCust.DateOfBirth));
            if (newCust.AnniversaryDate == null)
            {
                cmdUpdateEmploye.Parameters.Add("@AniversyDate", SqlDbType.DateTime);
                cmdUpdateEmploye.Parameters["@AniversyDate"].Value = DBNull.Value;
            }
            else
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@AniversyDate", newCust.AnniversaryDate));
            if (newCust.CashBalance == null)
            {
                cmdUpdateEmploye.Parameters.Add("@CashBalance", SqlDbType.Float);
                cmdUpdateEmploye.Parameters["@CashBalance"].Value = 0;
            }
            else
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@CashBalance", newCust.CashBalance));

            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@AccountCode", newCust.AccountCode));
            cmdUpdateEmploye.Parameters.Add(new SqlParameter("@oldCustID", oldCust));
          
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

        public Customer  SearchCustById(int id)
        {
            string selectCust = "select ci.*, ct.*, ctr.*,ca.OpeningCash from CustomerInfo ci inner join City ct on ci.CityId=ct.CityId inner join Country ctr on ci.CountryId=ctr.CountryId" +
                " inner join ChildAccount ca on ca.ChildCode=ci.AccountCode where CustID=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectCust, con);
            cmd.CommandType = CommandType.Text;
            Customer cust =null ;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                     cust = new Customer();
                     
                     cust.ID = Convert.ToInt32(dr["CustID"]);

                     cust.Salutation = dr["Salutation"].ToString();
                     cust.Name = dr["Name"].ToString();
                     cust.CO = dr["CO"].ToString();
                     cust.CNIC = dr["CNIC"].ToString();
                     cust.TelHome = dr["TelHome"].ToString();
                     cust.Mobile = dr["Mobile"].ToString();
                     cust.HouseNo = dr["HouseNo"].ToString();
                     cust.CityId = new City(Convert.ToInt32(dr["CityId"]), dr["CityName"].ToString());
                     cust.BlockNo = dr["BlockNo"].ToString();
                     cust.CountryId = new Country(Convert.ToInt32(dr["CountryId"]), dr["CountryName"].ToString());
                     cust.Near = dr["Near"].ToString();
                     cust.Colony = dr["Colony"].ToString();
                     cust.StreetNo = dr["StreetNo"].ToString();
                     cust.Address = dr["Address"].ToString();
                     cust.Email = dr["Email"].ToString();
                     cust.AccountCode = dr["AccountCode"].ToString();
                     if (dr["OpeningCash"] == DBNull.Value)
                     {
                         cust.CashBalance = 0;
                     }
                     else
                     {
                         //cust.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                         cust.CashBalance = Convert.ToDecimal(dr["OpeningCash"]);
                     }
                     cust.Email = dr["Email"].ToString();
                     if (dr["BirthDate"] == DBNull.Value)
                     {
                         cust.DateOfBirth = null;
                     }
                     else
                         cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                     if (dr["AniversyDate"] == DBNull.Value)
                     {
                         cust.AnniversaryDate = null;
                     }
                     else
                         cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);

                     cust.ImageMemory = this.ImageRestore("SELECT Picture from StandardPicsDB.dbo.CustomerPictures where CustId='" + cust.ID + "'");
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
            return cust;
           
        }
        public void UpdateCustomerPics(int CustId, JewelPictures jp)
        {
            SqlConnection conpic = new SqlConnection(DALHelper.ConStrPictures);
            SqlCommand cmdpic = new SqlCommand("UpdateCustomerPictures", conpic);
            cmdpic.CommandType = CommandType.StoredProcedure;

            //cmdpic.Parameters.Add(new SqlParameter("@TagNo", jp.TagNo));
            cmdpic.Parameters.Add("@oldCustId", SqlDbType.NVarChar).Value = CustId;
            if (jp.ImageMemory == null)
            {
                cmdpic.Parameters.Add("@Picture", SqlDbType.Image);
                cmdpic.Parameters["@Picture"].Value = DBNull.Value;
            }
            else
                cmdpic.Parameters.Add(new SqlParameter("@Picture", jp.ImageMemory));


            if (jp.ImageMemorySmall == null)
            {
                cmdpic.Parameters.Add("@SmallPicture", SqlDbType.Image);
                cmdpic.Parameters["@SmallPicture"].Value = DBNull.Value;
            }
            else

                cmdpic.Parameters.Add(new SqlParameter("@SmallPicture", jp.ImageMemorySmall));

            
          



            try
            {

                conpic.Open();

                cmdpic.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (conpic.State == ConnectionState.Open)
                    conpic.Close();
            }
        }
        public byte[] ImageRestore(string getImage)
        {
            byte[] tempImage = null;
            //string getImage = "SELECT Picture from Stock where StockId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(getImage, con);
            con.Open();
            try
            {
                if (cmd.ExecuteScalar() == DBNull.Value)
                    return tempImage;
                else
                    tempImage = (byte[])cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            return tempImage;
        }
        public Customer SearchCustById(int id, SqlConnection con , SqlTransaction tran)
        {
            string selectCust = "select ci.*, ct.*, ctr.* from CustomerInfo ci, City ct, Country ctr where CustID=" + id;
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectCust, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            Customer cust = null;

            try
            {
                //con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cust = new Customer();

                    cust.ID = Convert.ToInt32(dr["CustID"]);

                    cust.Salutation = dr["Salutation"].ToString();
                    cust.Name = dr["Name"].ToString();
                    cust.CO = dr["CO"].ToString();
                    cust.CNIC = dr["CNIC"].ToString();
                    cust.TelHome = dr["TelHome"].ToString();
                    cust.Mobile = dr["Mobile"].ToString();
                    cust.HouseNo = dr["HouseNo"].ToString();
                    cust.CityId = new City(Convert.ToInt32(dr["CityId"]), dr["CityName"].ToString());
                    cust.BlockNo = dr["BlockNo"].ToString();
                    cust.CountryId = new Country(Convert.ToInt32(dr["CountryId"]), dr["CountryName"].ToString());
                    cust.Near = dr["Near"].ToString();
                    cust.Colony = dr["Colony"].ToString();
                    cust.StreetNo = dr["StreetNo"].ToString();
                    cust.Address = dr["Address"].ToString();
                    cust.Email = dr["Email"].ToString();
                    cust.AccountCode = dr["AccountCode"].ToString();
                    if (dr["CashBalance"] == DBNull.Value)
                    {
                        cust.CashBalance = null;
                    }
                    else
                        cust.CashBalance = Convert.ToDecimal(dr["CashBalance"]);
                    cust.Email = dr["Email"].ToString();
                    if (dr["BirthDate"] == DBNull.Value)
                    {
                        cust.DateOfBirth = null;
                    }
                    else
                        cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                    if (dr["AniversyDate"] == DBNull.Value)
                    {
                        cust.AnniversaryDate = null;
                    }
                    else
                        cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);


                }
                dr.Close();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open) con.Close();
            }
            return cust;

        }

        public void DeleteCustomer(int id,Customer cust)
        {

            string deleteCustomer = "Delete from CustomerInfo where CustID=" + id;
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
        public void DeleteCustomerPic(int id)
        {

            string deleteCustomer = "Delete from CustomerPictures where CustID=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConStrPictures);
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
        public void DeleteCustomer(string accountCode, Customer cust)
        {

            string deleteCustomer = "Delete from CustomerInfo where AccountCode='"+accountCode+"';Delete from ChildAccount where ChildCode='"+accountCode+"'";
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

        public List<Customer> GetCustByNameSample(string name)
        {
            string AllCustByName = "select CustId,Name,CNIC,Mobile,TelHome,Address,Email from CustomerInfo where Name like '%" + name + "%'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCustByName, con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID =Convert.ToInt32(dr["CustId"]);

                        cust.Name = dr["Name"].ToString();


                        cust.Mobile = dr["Mobile"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();

                        cust.Address = dr["Address"].ToString();

                        cust.Email = dr["Email"].ToString();


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

        public Customer GetCustomerById(int id)
        {
            string selectsql = "select * from CustomerInfo where CustId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.CommandType = CommandType.Text;
            Customer cust = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (cust == null) cust = new Customer();

                    //cust = new Customer();
                    cust.ID=Convert.ToInt32(dr["CustId"]);
                    cust.Name = dr["Name"].ToString();


                    cust.Mobile = dr["Mobile"].ToString();

                    cust.CNIC = dr["CNIC"].ToString();

                    cust.Address = dr["Address"].ToString();

                    cust.Email = dr["Email"].ToString();
                    cust.AccountCode = dr["AccountCode"].ToString();


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

            return cust;
        }

        public int GetCustId(string query)
        {
            //string querry = "Select MAX(OrderNo) as [MaxOrder] from OrderEstimate";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            int orderNo = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["CustId"] == DBNull.Value)
                        orderNo = 0;
                    else
                        orderNo = Convert.ToInt32(dr["CustId"]);
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
            return orderNo;
        }

        public bool isPicCustidExist(int custId)
        {

            string querry = "select CustId from CustomerPictures where custId="+custId;
            SqlConnection con = new SqlConnection(DALHelper.ConStrPictures);
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
        public List<Customer> GetAllCustByNameForSMS(string name)
        {
            string AllCustomerByName = "select ci.* from customerinfo ci where ci.mobile <> '' and len(ci.Mobile)=11 and ci.Mobile like '%03%' and ci.Name like '" + name + "%' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCustomerByName, con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null)
                        custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.Name = dr["Name"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.Name = cust.Name + " " + cust.Mobile;
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
            if (custs != null)
                custs.TrimExcess();
            return custs;
        }
        public List<Customer> GetAllCustByMobileForSMS(string mobile)
        {

            string AllCustomerByMobile = "select ci.* from Customerinfo ci where ci.mobile <> '' and len(ci.Mobile)=11 and ci.Mobile like '%03%' and ci.Mobile like '%" + mobile + "%'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCustomerByMobile, con);
            cmd.CommandType = CommandType.Text;
            List<Customer> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Customer>();
                    if (custs == null) custs = new List<Customer>();

                    do
                    {
                        Customer cust = new Customer();
                        cust.ID = Convert.ToInt32(dr["CustId"]);
                        cust.Name = dr["Name"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.Name = cust.Name + " " + cust.Mobile;


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
            if (custs != null)
                custs.TrimExcess();
            return custs;
        }

        public Customer GetAllCustomerByAccountNo(string accNo, SqlConnection con, SqlTransaction trans)
        {
            string query = "select ci.*, ct.*, ctr.*, (((select isnull(sum(OpeningCash),0) from ChildAccount where ChildCode=ci.AccountCode) + (select isnull(sum(Dr),0) from vouchers where AccountCode=ci.AccountCode)) - (select isnull(sum(Cr),0) from vouchers where AccountCode=ci.AccountCode)) as Balance from CustomerInfo ci Left outer join  City ct on ct.cityId  = ci.CityId Left Outer join Country ctr on ctr.countryId = ci.countryId where ci.AccountCode = '" + accNo + "'";
            SqlCommand cmd = new SqlCommand(query, con, trans);
            cmd.CommandType = CommandType.Text;
            Customer cust = null;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cust = new Customer();
                    do
                    {
                        cust.ID = Convert.ToInt32(dr["CustID"]);
                        cust.AccountCode = dr["AccountCode"].ToString();
                        cust.Salutation = dr["Salutation"].ToString();
                        cust.Name = dr["Name"].ToString();
                        cust.CO = dr["CO"].ToString();
                        cust.CNIC = dr["CNIC"].ToString();
                        cust.TelHome = dr["TelHome"].ToString();
                        cust.Mobile = dr["Mobile"].ToString();
                        cust.HouseNo = dr["HouseNo"].ToString();
                        cust.Near = dr["Near"].ToString();
                        cust.CityId = new City(Convert.ToInt32(dr["CityId"]), dr["CityName"].ToString());
                        cust.BlockNo = dr["BlockNo"].ToString();
                        cust.CountryId = new Country(Convert.ToInt32(dr["CountryId"]), dr["CountryName"].ToString());
                        cust.Colony = dr["Colony"].ToString();
                        cust.StreetNo = dr["StreetNo"].ToString();
                        cust.Address = dr["Address"].ToString();
                        cust.Email = dr["Email"].ToString();
                        if (dr["Balance"] == DBNull.Value)
                            cust.CashBalance = null;
                        else
                            cust.CashBalance = Convert.ToDecimal(dr["Balance"]);
                        if (dr["BirthDate"] == DBNull.Value)
                            cust.DateOfBirth = null;
                        else
                            cust.DateOfBirth = Convert.ToDateTime(dr["BirthDate"]);
                        if (dr["AniversyDate"] == DBNull.Value)
                            cust.AnniversaryDate = null;
                        else
                            cust.AnniversaryDate = Convert.ToDateTime(dr["AniversyDate"]);
                    }
                    while (dr.Read());
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                con.Close();
                throw ex;
            }
            return cust;
        }
    }
}
