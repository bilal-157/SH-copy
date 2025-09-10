using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

namespace DAL
{
    public class UserDAL
    {
        int uid;
        string getMaxCode = "select MAX(UserId) as [MaxCode] from Users";
        public int GetMaxUser()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(this.getMaxCode, con);
            cmd.CommandType = CommandType.Text;
            //Employee employee = null;
            int uid = 0;
            con.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    uid = Convert.ToInt32(dr["MaxCode"]);
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
            return uid;
        }
        public void AddUsers(Users u)
        {
            uid=GetMaxUser()+1;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("addUsers", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", uid));
            cmd.Parameters.Add(new SqlParameter("@UserName", u.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", u.Password));
            cmd.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Available";

            SqlCommand cmdAddUserRights = new SqlCommand("addRights", con);
            cmdAddUserRights.CommandType = CommandType.StoredProcedure;
            cmdAddUserRights.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int));
            cmdAddUserRights.Parameters.Add(new SqlParameter("@FormName", SqlDbType.NVarChar));
            cmdAddUserRights.Parameters.Add(new SqlParameter("@Rights", SqlDbType.NVarChar));

            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                cmdAddUserRights.Transaction = tran;
                try
                {
                    cmd.ExecuteNonQuery();
                    if (u.RightsLineItem == null)
                    {
                        cmdAddUserRights.Parameters["@UserId"].Value = DBNull.Value;
                        cmdAddUserRights.Parameters["@FormName"].Value = DBNull.Value;
                        cmdAddUserRights.Parameters["@Rights"].Value = DBNull.Value;
                        cmdAddUserRights.ExecuteNonQuery();
                    }
                    else
                    {
                        foreach (RightsLineItem ur in u.RightsLineItem)
                        {
                            cmdAddUserRights.Parameters["@UserId"].Value = uid;
                            cmdAddUserRights.Parameters["@FormName"].Value = ur.FormName;
                            cmdAddUserRights.Parameters["@Rights"].Value = ur.Rights;
                            cmdAddUserRights.ExecuteNonQuery();

                        }
                    }
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

        public void UpdateUsers(Users u)
        {
            //uid = GetMaxUser() + 1;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@UserId", u.UserId));
            cmd.Parameters.Add(new SqlParameter("@UserName", u.UserName));
            cmd.Parameters.Add(new SqlParameter("@Password", u.Password));


            SqlCommand cmdAddUserRights = new SqlCommand("addRights", con);
            cmdAddUserRights.CommandType = CommandType.StoredProcedure;
            cmdAddUserRights.Parameters.Add(new SqlParameter("@UserId", SqlDbType.Int));
            cmdAddUserRights.Parameters.Add(new SqlParameter("@FormName", SqlDbType.NVarChar));
            cmdAddUserRights.Parameters.Add(new SqlParameter("@Rights", SqlDbType.NVarChar));

            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                cmdAddUserRights.Transaction = tran;
                try
                {
                    this.DeleteRights(u.UserId);
                    cmd.ExecuteNonQuery();
                    foreach (RightsLineItem ur in u.RightsLineItem)
                    {
                        cmdAddUserRights.Parameters["@UserId"].Value = u.UserId;
                        cmdAddUserRights.Parameters["@FormName"].Value = ur.FormName;
                        cmdAddUserRights.Parameters["@Rights"].Value = ur.Rights;
                        cmdAddUserRights.ExecuteNonQuery();

                    }
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

        public void UpdatePassword(int oldId, string password,string oldPassword)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdatePass = new SqlCommand("Update Users set Password=@Password where UserId=@UserId and Password=@oldPassword", con);
            cmdUpdatePass.CommandType = CommandType.Text;

            cmdUpdatePass.Parameters.Add(new SqlParameter("@UserId", oldId));
            cmdUpdatePass.Parameters.Add(new SqlParameter("@Password", password));

            cmdUpdatePass.Parameters.Add(new SqlParameter("@oldPassword", oldPassword));


            con.Open();
            try
            {
                SqlTransaction tran = con.BeginTransaction();
                cmdUpdatePass.Transaction = tran;
                try
                {
                    cmdUpdatePass.ExecuteNonQuery();
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

        public List<Users> GetAllUsers()
        {
            string selectAll = "select * from Users where Status='Available'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectAll, con);
            List<Users> allusers = null;
            con.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (allusers == null) allusers = new List<Users>();
                while (dr.Read())
                {
                    Users u = new Users();
                    u.UserId = Convert.ToInt32(dr["UserId"]);
                    u.UserName = dr["UserName"].ToString();
                    u.Password = dr["Password"].ToString();
                   
                    allusers.Add(u);

                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return allusers;


        }

        public Users GetUserById(int userId)
        {
            string selectAll = "select * from Users where UserId="+userId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectAll, con);
            Users user = null;
            con.Open();
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
               
                if (dr.Read())
                {
                    user = new Users();
                    user.UserId = Convert.ToInt32(dr["UserId"]);
                    user.UserName = dr["UserName"].ToString();
                    user.Password = dr["Password"].ToString();
                    List<RightsLineItem> rlis = this.GetUserRightsByUserId(user.UserId);
                    foreach (RightsLineItem rli in rlis)
                    {
                        user.AddLineItems(rli);
                    }
                }
                dr.Close();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close();
            }
            return user;


        }
       
        public List<RightsLineItem> GetUserRightsByUserId(int userId)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from UserRights where UserId=" + userId, con);
            cmd.CommandType = CommandType.Text;
            List<RightsLineItem> rights = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(rights==null) rights = new List<RightsLineItem>();
                while (dr.Read())
                {

                    RightsLineItem right = new RightsLineItem();

                    right.UserId = Convert.ToInt32(dr["UserId"]);
                    right.FormName = dr["FormName"].ToString();
                    right.Rights = dr["Rights"].ToString();

                    rights.Add(right);
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

            return rights;

        }

        public void DeleteRights(int uid)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand("Delete from UserRights where UserId="+uid, con);
            cmdDelete.CommandType = CommandType.Text;

           try
            {
                con.Open();
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                con.Close();
            }
        }       

        public bool ValidateUser(int userid, string password)
        {
            bool result = false;

            //string query = " SELECT u.UserName,u.Password from Users u INNER JOIN Employee emp on u.EmpCode=emp.EmpCode WHERE emp.EmpName = @UserID  AND u.Password =@Password";
            string query = " SELECT * from Users WHERE UserId = @UserId  AND Password =@Password AND Status='Available'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);

            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userid;
            com.Parameters.Add("@Password", SqlDbType.NVarChar).Value = password;

            con.Open();
            SqlDataReader r = com.ExecuteReader();

            if (r.Read())
            {
                result = true;
            }

            con.Close();
            return result;
        }

        public bool isNameExist(string name)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select UserName from Users where UserName='" + name + "'";
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

        public void DeleteUser(int userId)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand("Update Users set Status=@Status where UserId=@UserId", con);
            cmdDelete.CommandType = CommandType.Text;

            cmdDelete.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userId;

            cmdDelete.Parameters.Add("@Status", SqlDbType.NVarChar).Value = "Deleted";
            
            try
            {
                con.Open();
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            finally
            {
                con.Close();
            }
        }

        public bool IsUserAdministrator(int userid)
        {
            bool result = false;
            string rights = "";
            //string query = " SELECT u.UserName,u.Password from Users u INNER JOIN UserRights ur on u.UserId=ur.UserId WHERE emp.EmpName = @UserID  AND u.Password =@Password";
            string query = " SELECT Rights from UserRights WHERE UserId = @UserId ";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);

            SqlCommand com = new SqlCommand(query, con);

            com.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = userid;

            con.Open();
            SqlDataReader dr = com.ExecuteReader();

            if (dr.Read())
            {
                rights = dr["Rights"].ToString();
                if (rights == "Administrator")
                    result = true;
            }

            con.Close();
            return result;
        }

        public bool NotDeleteJewelManager(string name)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select username from users where username='" + name+"'";
            
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

        //public 
    }
}
