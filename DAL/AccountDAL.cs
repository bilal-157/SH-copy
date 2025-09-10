using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class AccountDAL
    {
        GroupAccount g;
        SubGroupAccount sg;
        ParentAccount p;
        ChildAccount c;
        ChildAccount chld = new ChildAccount();
        //rtuytujukuiuuh
        string createParent = "AddParentAccount";
        string createChild = "AddChildAccount";

        #region Accounts3Level

        public void DeleteAccount(string query)
        {
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
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
        public List<ParentAccount> GetParentByHeadCode(int hCode)
        {
            string selectsql = "select * from ParentAccount where HeadCode=" + hCode + " order by ParentName";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            List<ParentAccount> gl = null;
            ParentAccount g = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    gl = new List<ParentAccount>();
                    do
                    {
                        g = new ParentAccount();
                        g.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                        g.ParentName = dr["ParentName"].ToString();
                        g.ParentCode = dr["ParentCode"].ToString();
                        gl.Add(g);
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
            return gl;
        }
        public List<ChildAccount> GetAllChildAccounts(string query)
        {
            //string selectSql = "select * from ChildAccount where ParentCode='" + pCode + "' order by ChildName";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            List<ChildAccount> childs = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    childs = new List<ChildAccount>();
                    do
                    {
                        ChildAccount c = new ChildAccount();
                        c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                        c.ParentCode = dr["ParentCode"].ToString();
                        c.ChildCode = dr["ChildCode"].ToString();
                        c.ChildName = dr["ChildName"].ToString();
                        if (dr["OpeningCash"] == DBNull.Value)
                            c.OpCash = 0;
                        else
                            c.OpCash = Convert.ToDecimal(dr["OpeningCash"]);
                        c.Balance = Convert.ToDecimal(dr["Balance"]);
                        childs.Add(c);

                    } while (dr.Read());
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
            return childs;
        }
        public string CreateAccount(int hcode, string pname, string chname, string type, SqlConnection con, SqlTransaction trans)
        {
           
            p = new ParentAccount();
            p = this.GetParent(pname, hcode.ToString(), con, trans);
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = hcode;
                p.ParentName = pname;
                p.ParentCode = this.CreateParentCode( p.HeadCode,con,trans);
                this.CreateParentAccount(p,con,trans);
            }            
                c = new ChildAccount();
                c.HeadCode = p.HeadCode;
                c.ParentCode = p.ParentCode;
                c.ChildName = chname;
                c.ChildCode = this.CreateChildCode(c.ParentCode, c.HeadCode, con, trans);
                c.DDate = DateTime.Today;
                c.Status = "";
                c.Description = "";
                c.AccountType = type;// "General Account";
                this.CreateChildAccount(c,true, con, trans);            
            return c.ChildCode;
        }
        public string CreateAccount(int hcode, string pname, string chname, string type,  decimal opCash)
        {
            p = new ParentAccount();
            p = this.GetParent(pname, hcode.ToString());
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = hcode;
                p.ParentName = pname;
                p.ParentCode = this.CreateParentCode(p.HeadCode);
                this.CreateParentAccount(p);
            }
            c = new ChildAccount();
            c = this.GetChild(chname, p.ParentCode);
            if (c == null)
            {
                c = new ChildAccount();
                c.HeadCode = p.HeadCode;
                c.ParentCode = p.ParentCode;
                c.ChildName = chname;
                c.ChildCode = this.CreateChildCode(c.ParentCode, c.HeadCode);
                c.DDate = DateTime.Today;
                c.Status = "";
                c.Description = "";
                c.OpCash = opCash;
                c.AccountType = type;// "General Account";
                this.CreateChildAccount(c, true);
            }
            return c.ChildCode;
        }
        public string CreateAccount(int hcode, string pname, ChildAccount chname, string type)
        {
            p = new ParentAccount();
            p = this.GetParent(pname, hcode.ToString());
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = hcode;
                p.ParentName = pname;
                p.ParentCode = this.CreateParentCode(p.HeadCode);
                this.CreateParentAccount(p);
            }
            c = new ChildAccount();
            c = this.GetChild(chname.ChildName, p.ParentCode);
            if (c == null)
            {
                c = new ChildAccount();
                c.HeadCode = p.HeadCode;
                c.ParentCode = p.ParentCode;
                c.ChildName = chname.ChildName;
                c.ChildCode = this.CreateChildCode(c.ParentCode, c.HeadCode);
                c.DDate = DateTime.Today;
                c.Status = "";
                c.Description = "";
                c.OpCash = chname.OpCash;
                c.OpGold = chname.OpGold;
                c.AccountType = type;// "General Account";
                this.CreateChildAccount(c, true);
            }
            return c.ChildCode;
        }
        public ChildAccount GetAccount(int headcode, string parent, string childname, SqlConnection con, SqlTransaction trans)
        {
            ChildAccount c1 = new ChildAccount();
            c1.ChildName = childname;
            c1.HeadCode = headcode;
            c1.ParentCode = this.GetParentCode(c1.HeadCode.ToString(), parent, con, trans);
            c1.ChildCode = this.GetChildCode(c1.ParentCode, childname, con, trans);
            c1.Balance = 0;// this.GetCashInHandBalance(childname, con, trans);
            if (!(DateDAL.IsExist("select ChildCode from ChildAccount where ChildCode='" + c1.ChildCode + "'", con, trans)))
            {
                c1 = null;
            }
            return c1;
        }
        public ChildAccount GetAccount(int headcode, string parent, string childname)
        {
            ChildAccount c1 = new ChildAccount();
            c1.ChildName = childname;
            c1.HeadCode = headcode;
            c1.ParentCode = this.GetParentCode(c1.HeadCode.ToString(), parent);
            c1.ChildCode = this.GetChildCode(c1.ParentCode, childname);
            c1.Balance = 0;// this.GetCashInHandBalance(childname, con, trans);
            if (!(DateDAL.IsExist("select ChildCode from ChildAccount where ChildCode='" + c1.ChildCode + "'")))
            {
                c1 = null;
            }
            return c1;
        }
        public string GetParentCode(string hCode, string pName)
        {
            string selectsql = "select ParentCode from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            string str = "";
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    str = dr["ParentCode"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State==ConnectionState.Open)
                {
                    con.Close();
                }
              
            }
            return str;
        }
        public bool DeleteCheck(string query)
        {
            
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            bool str = false ;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    str =Convert.ToBoolean(dr["DeleteCheck"]);
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
                }

            }
            return str;
        }
        //public string GetChildCode(string pCode, string chName)
        //{
        //    string selectsql = "select ChildCode from ChildAccount where ChildName='" + chName + "'and ParentCode='" + pCode + "'";
        //    SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
        //    SqlCommand cmd = new SqlCommand(selectsql, con);
        //    string str = "";
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        con.Open();
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            str = dr["ChildCode"].ToString();
        //        }
        //        dr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //       // trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //    }
        //    return str;
        //}
        //public string GetParentCode(string hCode, string pName, SqlConnection con, SqlTransaction trans)
        //{
        //    string selectsql = "select ParentCode from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
        //    SqlCommand cmd = new SqlCommand(selectsql, con);
        //    cmd.Transaction = trans;
        //    string str = "";
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            str = dr["ParentCode"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dr.Close();
        //    }
        //    return str;
        //}
        //public string GetChildCode(string pCode, string chName, SqlConnection con, SqlTransaction trans)
        //{
        //    string selectsql = "select ChildCode from ChildAccount where ChildName='" + chName + "'and ParentCode='" + pCode + "'";
        //    SqlCommand cmd = new SqlCommand(selectsql, con);
        //    cmd.Transaction = trans;
        //    string str = "";
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            str = dr["ChildCode"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        trans.Rollback();
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dr.Close();
        //    }
        //    return str;
        //}
        //public ChildAccount GetChild(string cName, string pCode, SqlConnection con, SqlTransaction trans)
        //{
        //    string selectSql = "select * from ChildAccount where ChildName='" + cName + "'and ParentCode='" + pCode + "'";
        //    SqlCommand cmd = new SqlCommand(selectSql, con);
        //    cmd.Transaction = trans;
        //    ChildAccount c = null;
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            if (c == null) c = new ChildAccount();
        //            c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
        //            c.ChildCode = dr["ChildCode"].ToString();
        //            c.ChildName = dr["ChildName"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dr.Close();
        //    }
        //    return c;
        //}
        //public ParentAccount GetParent(string pName, string hCode, SqlConnection con, SqlTransaction trans)
        //{
        //    string selectSql = "select * from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
        //    SqlCommand cmd = new SqlCommand(selectSql, con);
        //    cmd.Transaction = trans;
        //    ParentAccount p = null;
        //    SqlDataReader dr = null;
        //    try
        //    {
        //        dr = cmd.ExecuteReader();
        //        if (dr.Read())
        //        {
        //            if (p == null) p = new ParentAccount();
        //            p.HeadCode = Convert.ToInt32(dr["HeadCode"]);
        //            p.ParentCode = dr["ParentCode"].ToString();
        //            p.ParentName = dr["ParentName"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        dr.Close();
        //    }
        //    return p;
        //}
         public string GetChildCode(string pCode, string chName)
        {
            string selectsql = "select ChildCode from ChildAccount where ChildName='" + chName + "'and ParentCode='" + pCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            string str = "";
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    str = dr["ChildCode"].ToString();
                }
                dr.Close();
            }
            catch (Exception ex)
            {
               // trans.Rollback();
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return str;
        }
        public string GetParentCode(string hCode, string pName, SqlConnection con, SqlTransaction trans)
        {
            string selectsql = "select ParentCode from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.Transaction = trans;
            string str = "";
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    str = dr["ParentCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return str;
        }
        public string GetChildCode(string pCode, string chName, SqlConnection con, SqlTransaction trans)
        {
            string selectsql = "select ChildCode from ChildAccount where ChildName='" + chName + "'and ParentCode='" + pCode + "'";
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.Transaction = trans;
            string str = "";
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    str = dr["ChildCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return str;
        }
        public ChildAccount GetChild(string cName, string pCode, SqlConnection con, SqlTransaction trans)
        {
            string selectSql = "select * from ChildAccount where ChildName='" + cName + "'and ParentCode='" + pCode + "'";
            SqlCommand cmd = new SqlCommand(selectSql, con);
            cmd.Transaction = trans;
            ChildAccount c = null;
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (c == null) c = new ChildAccount();
                    c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    c.ChildCode = dr["ChildCode"].ToString();
                    c.ChildName = dr["ChildName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return c;
        }
        public ParentAccount GetParent(string pName, string hCode, SqlConnection con, SqlTransaction trans)
        {
            string selectSql = "select * from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
            SqlCommand cmd = new SqlCommand(selectSql, con);
            cmd.Transaction = trans;
            ParentAccount p = null;
            SqlDataReader dr = null;
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (p == null) p = new ParentAccount();
                    p.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    p.ParentCode = dr["ParentCode"].ToString();
                    p.ParentName = dr["ParentName"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dr.Close();
            }
            return p;
        }
        public ChildAccount GetChild(string cName, string pCode)
        {
            string selectSql = "select * from ChildAccount where ChildName='" + cName + "'and ParentCode='" + pCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSql, con);
            ChildAccount c = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (c == null) c = new ChildAccount();
                    c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    c.ChildCode = dr["ChildCode"].ToString();
                    c.ChildName = dr["ChildName"].ToString();
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
        public ParentAccount GetParent(string pName, string hCode)
        {
            string selectSql = "select * from ParentAccount where ParentName='" + pName + "'and HeadCode='" + hCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSql, con);
            ParentAccount p = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (p == null) p = new ParentAccount();
                    p.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    p.ParentCode = dr["ParentCode"].ToString();
                    p.ParentName = dr["ParentName"].ToString();
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
            return p;
        }
        public string CreateParentCode(int headCode)
        {
            string selectSQL = "select ParentCode from ParentAccount where HeadCode=" + headCode + " order by ParentName";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            int maxCode = 1;
            string code;
            string s = "";
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    do
                    {
                        s = dr["ParentCode"].ToString();
                        int c = Convert.ToInt32(s.Remove(0, 3));
                        if (c > maxCode)
                            maxCode = c;
                    }
                    while (dr.Read());
                    maxCode = maxCode + 1;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.Close(); }
            code = string.Format("{0:000}", maxCode);
            code = headCode + "-" + code;
            return code;
        }
        public string CreateParentCode(int headCode,SqlConnection con,SqlTransaction trans)
        {
            string selectSQL = "select ParentCode from ParentAccount where HeadCode=" + headCode + " order by ParentName";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Transaction = trans;
            int maxCode = 1;
            string code;
            string s = "";
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    do
                    {
                        s = dr["ParentCode"].ToString();
                        int c = Convert.ToInt32(s.Remove(0, 3));
                        if (c > maxCode)
                            maxCode = c;
                    }
                    while (dr.Read());
                    maxCode = maxCode + 1;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { //con.Close(); 
            }
            code = string.Format("{0:000}", maxCode);
            code = headCode + "-" + code;
            return code;
        }
        public bool CreateParentAccount(ParentAccount p)
        {
            string selectSql = "select ParentName from ParentAccount where ParentName='" + p.ParentName + "'and HeadCode=" + p.HeadCode;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(createParent, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdname = new SqlCommand(selectSql, con);
            cmdname.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@HeadCode", p.HeadCode));
            cmd.Parameters.Add(new SqlParameter("@ParentCode", p.ParentCode));
            cmd.Parameters.Add(new SqlParameter("@ParentName", p.ParentName));           
            cmd.Parameters.Add(new SqlParameter("@DeleteCheck", p.DeleteCheck));

            SqlDataReader dr = null;
            bool bFlag = true;
            try
            {
                con.Open();
                dr = cmdname.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    con.Close();
                    bFlag = false;
                    return bFlag;
                }
                else
                {
                    dr.Close();
                    cmd.ExecuteNonQuery();
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
            return bFlag;
        }
        public bool CreateParentAccount(ParentAccount p,SqlConnection con,SqlTransaction trans)
        {
            string selectSql = "select ParentName from ParentAccount where ParentName='" + p.ParentName + "'and HeadCode=" + p.HeadCode;
            SqlCommand cmd = new SqlCommand(createParent, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            SqlCommand cmdname = new SqlCommand(selectSql, con);
            cmdname.CommandType = CommandType.Text;
            cmdname.Transaction = trans;
            cmd.Parameters.Add(new SqlParameter("@HeadCode", p.HeadCode));
            cmd.Parameters.Add(new SqlParameter("@ParentCode", p.ParentCode));
            cmd.Parameters.Add(new SqlParameter("@ParentName", p.ParentName));
            SqlDataReader dr = null;
            bool bFlag = true;
            try
            {
               // con.Open();
                dr = cmdname.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Close();
                    //con.Close();
                    bFlag = false;
                    return bFlag;
                }
                else
                {
                    dr.Close();
                    cmd.ExecuteNonQuery();
                }
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
            return bFlag;
        }
        public string CreateChildCode(string pCode, int headCode)
        {
            string selectSQL = "select Max(ChildCode)as ChildCode from ChildAccount where ParentCode='" + pCode + "' and HeadCode='" + headCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            int maxCode = 1;
            string code;
            string sgcode;
            string c;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    do
                    {
                        c = dr["ChildCode"].ToString();
                        if (!string.IsNullOrEmpty(c))
                        {
                            int a = Convert.ToInt32(c.Remove(0, 6));
                            if (a > maxCode)
                                maxCode = a;
                        }
                        else
                            maxCode = 0;
                       
                    }
                    while (dr.Read());
                    maxCode = maxCode + 1;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { con.Close(); }
            code = string.Format("{0:00000}", maxCode);
            sgcode = pCode + "-" + code;
            return sgcode;
        }
        public string CreateChildCode(string pCode, int headCode,SqlConnection con,SqlTransaction trans)
        {
            string selectSQL = "select * from ChildAccount where ChildCode=(select  Max(ChildCode) from ChildAccount where ParentCode='" + pCode + "' and HeadCode='" + headCode + "')";
            SqlCommand cmd = new SqlCommand(selectSQL, con);
            cmd.Transaction = trans;
            int maxCode = 1;
            string code;
            string sgcode;
            string c;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    do
                    {
                        c = dr["ChildCode"].ToString();
                        int a = Convert.ToInt32(c.Remove(0, 6));
                        if (a > maxCode)
                            maxCode = a;
                    }
                    while (dr.Read());
                    maxCode = maxCode + 1;
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { //con.Close();
            }
            code = string.Format("{0:00000}", maxCode);
            sgcode = pCode + "-" + code;
            return sgcode;
        }
        public bool CreateChildAccount(ChildAccount c, bool b)
        {
            string selectSql = "select ChildName from ChildAccount where ChildName='" + c.ChildName + "'and HeadCode=" + c.HeadCode;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(createChild, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlCommand cmdname = new SqlCommand(selectSql, con);
            cmdname.CommandType = CommandType.Text;
            cmd.Parameters.Add(new SqlParameter("@HeadCode", c.HeadCode));
            cmd.Parameters.Add(new SqlParameter("@ParentCode", c.ParentCode));
            cmd.Parameters.Add(new SqlParameter("@ChildCode", c.ChildCode));
            cmd.Parameters.Add(new SqlParameter("@ChildName", c.ChildName));
            cmd.Parameters.Add(new SqlParameter("@Status", c.Status));
            cmd.Parameters.Add(new SqlParameter("@Balance", c.Balance));
            cmd.Parameters.Add(new SqlParameter("@Type", c.AccountType));
            cmd.Parameters.Add(new SqlParameter("@DDate", c.DDate));
            cmd.Parameters.Add(new SqlParameter("@Description", c.Description));
            cmd.Parameters.Add(new SqlParameter("@OpeningCash", c.OpCash));
            cmd.Parameters.Add(new SqlParameter("@OpeningGold", c.OpGold));
            cmd.Parameters.Add(new SqlParameter("@DeleteCheck", c.DeleteCheck));
            SqlDataReader dr = null;
            bool bFlag = true;
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
            return bFlag;
        }
        public bool CreateChildAccount(ChildAccount c, bool b, SqlConnection con, SqlTransaction trans)
        {
            string selectSql = "select ChildName from ChildAccount where ChildName='" + c.ChildName + "'and HeadCode=" + c.HeadCode;

            SqlCommand cmd = new SqlCommand(createChild, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Transaction = trans;
            SqlCommand cmdname = new SqlCommand(selectSql, con);
            cmdname.CommandType = CommandType.Text;
            cmdname.Transaction = trans;
            cmd.Parameters.Add(new SqlParameter("@HeadCode", c.HeadCode));
            cmd.Parameters.Add(new SqlParameter("@ParentCode", c.ParentCode));
            cmd.Parameters.Add(new SqlParameter("@ChildCode", c.ChildCode));
            cmd.Parameters.Add(new SqlParameter("@ChildName", c.ChildName));
            cmd.Parameters.Add(new SqlParameter("@Status", c.Status));
            cmd.Parameters.Add(new SqlParameter("@Balance", c.Balance));
            cmd.Parameters.Add(new SqlParameter("@Type", c.AccountType));
            cmd.Parameters.Add(new SqlParameter("@DDate", c.DDate));
            cmd.Parameters.Add(new SqlParameter("@Description", c.Description));
            cmd.Parameters.Add(new SqlParameter("@OpeningCash", c.OpCash));
            cmd.Parameters.Add(new SqlParameter("@OpeningGold", c.OpGold));

            SqlDataReader dr = null;
            bool bFlag = true;
            try
            {
              //  con.Open();
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
            }
            return bFlag;
        }
        public ChildAccount GetChildByCode(string ChildCode, SqlConnection con, SqlTransaction trans)
        {
            string selectSql = "select * from ChildAccount where ChildCode='" + ChildCode + "'";
            //SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSql, con);
            cmd.Transaction = trans;
            ChildAccount c = null;
            SqlDataReader dr = null;
            try
            {
                //con1.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (c == null) c = new ChildAccount();
                    c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    c.ParentCode = dr["ParentCode"].ToString();
                    c.ChildCode = dr["ChildCode"].ToString();
                    c.ChildName = dr["ChildName"].ToString();
                    c.AccountType = dr["Type"].ToString();
                    if (dr["Balance"] == DBNull.Value)
                        c.Balance = 0;
                    else
                        c.Balance = Convert.ToDecimal(dr["Balance"]);
                    c.OpCash = Convert.ToDecimal(dr["OpeningCash"]);
                    c.OpGold = Convert.ToDecimal(dr["OpeningGold"]);


                }
                dr.Close();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                //if (con1.State == ConnectionState.Open)
                //    con1.Close();
            }
            return c;
        }

        public void UpdateGoldBalance(string query, SqlConnection con, SqlTransaction tran)
        {
            // SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;

            try
            {
                cmd.Transaction = tran;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                // if (con.State == ConnectionState.Open) con.Close();
            }

        }

        public ChildAccount GetChildByCode(string ChildCode)
        {
            string selectSql = "select * from ChildAccount where ChildCode='" + ChildCode + "'";
            SqlConnection con1 = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSql, con1);
            ChildAccount c = null;
            SqlDataReader dr = null;
            try
            {
                con1.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (c == null) c = new ChildAccount();
                    c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    c.ParentCode = dr["ParentCode"].ToString();
                    c.ChildCode = dr["ChildCode"].ToString();
                    c.ChildName = dr["ChildName"].ToString();
                    c.AccountType = dr["Type"].ToString();
                    if (dr["Balance"] == DBNull.Value)
                        c.Balance = 0;
                    else
                        c.Balance = Convert.ToDecimal(dr["Balance"]);
                    c.OpCash = Convert.ToDecimal(dr["OpeningCash"]);
                    c.OpGold = Convert.ToDecimal(dr["OpeningGold"]);

                }
                dr.Close();

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
            return c;
        }
        public ChildAccount GetChildByName(string name,SqlConnection con,SqlTransaction trans)
        {
            string selectSql = "select * from ChildAccount where ChildName='" + name + "'";
            SqlCommand cmd = new SqlCommand(selectSql, con);
            cmd.Transaction = trans;
            ChildAccount c = null;
            SqlDataReader dr = null;
            try
            {
                
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (c == null) c = new ChildAccount();
                    c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                    c.ParentCode = dr["ParentCode"].ToString();
                    c.ChildCode = dr["ChildCode"].ToString();
                    c.ChildName = dr["ChildName"].ToString();
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                throw ex;
            }
            finally
            {
                
            }
            return c;
        }
        
        #endregion

        public void getChildBalance(string childCode, out decimal cashBalance, out decimal goldBalance)
        {
            string query = "select Balance,GoldBalance from ChildAccount where ChildCode='" + childCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cashBalance = Convert.ToDecimal(dr["Balance"]);
                    goldBalance = Convert.ToDecimal(dr["GoldBalance"]);
                }
                else
                {
                    cashBalance = 0;
                    goldBalance = 0;
                }
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
                    dr.Close();
                    cmd.Dispose();
                }
            }

        }
        public void getChildBalance(string childCode, out decimal cashBalance, out decimal goldBalance, SqlConnection con,SqlTransaction tran)
        {
            string query = "select Balance,GoldBalance from ChildAccount where ChildCode='" + childCode + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Transaction = tran;
            SqlDataReader dr = null;
            try
            {
                //con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cashBalance = Convert.ToDecimal(dr["Balance"]);
                    goldBalance = Convert.ToDecimal(dr["GoldBalance"]);
                }
                else
                {
                    cashBalance = 0;
                    goldBalance = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (con.State == ConnectionState.Open)
                //{
                //    //con.Close();
                dr.Close();
                //    //cmd.Dispose();
                //}
            }

        }
        public void UpdateChildCashBalance(string ccode, decimal cashBalance)//, decimal goldBalance)
        {
            string querry = "Update ChildAccount set Balance=@Balance where ChildCode='" + ccode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@oldChildCode", SqlDbType.NVarChar).Value = ccode;

            cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            //cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
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
        public void UpdateChildCashBalance(string ccode, decimal cashBalance, SqlConnection con,SqlTransaction tran)//, decimal goldBalance)
        {
            string querry = "Update ChildAccount set Balance=@Balance where ChildCode='" + ccode + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tran;
            cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            //cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
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
        public void UpdateChildBalance(string ccode, decimal cashBalance, decimal goldBalance)
        {
            string querry = "Update ChildAccount set Balance=@Balance,GoldBalance=@GoldBalance where ChildCode='" + ccode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@oldChildCode", SqlDbType.NVarChar).Value = ccode;

            cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
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
        public void UpdateChildBalance(string ccode, decimal cashBalance, decimal goldBalance, SqlConnection con, SqlTransaction tran)
        {
            string querry = "Update ChildAccount set Balance=@Balance,GoldBalance=@GoldBalance where ChildCode='" + ccode + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tran; 
            cmd.Parameters.Add(new SqlParameter("@Balance", cashBalance));
            cmd.Parameters.Add(new SqlParameter("@GoldBalance", goldBalance));
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
        public List<ChildAccount> GetAllAccounts()
        {
            string query = "select * from ChildAccount order by ChildName";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<ChildAccount> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<ChildAccount>();
                    if (custs == null) custs = new List<ChildAccount>();

                    do
                    {
                        ChildAccount cust = new ChildAccount();

                        cust.ChildCode = dr["ChildCode"].ToString();
                        cust.ChildName = dr["ChildName"].ToString();


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
        public List<ChildAccount> GetAllAccounts(string name)
        {
            string query = "select * from ChildAccount Where ChildName Like '%"+name+"%' order by ChildName";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<ChildAccount> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<ChildAccount>();
                    if (custs == null) custs = new List<ChildAccount>();

                    do
                    {
                        ChildAccount cust = new ChildAccount();

                        cust.ChildCode = dr["ChildCode"].ToString();
                        cust.ChildName = dr["ChildName"].ToString();


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
        public List<ChildAccount> GetAllAccountsForBank()
        {
            string query = "select * from ChildAccount where Type='General Account'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<ChildAccount> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<ChildAccount>();
                    if (custs == null) custs = new List<ChildAccount>();

                    do
                    {
                        ChildAccount cust = new ChildAccount();

                        cust.ChildCode = dr["ChildCode"].ToString();
                        cust.ChildName = dr["ChildName"].ToString();


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
        public List<ChildAccount> GetAllBankAccounts(string name)
        {
            string query = "select * from ChildAccount where Type='Bank Account' and ChildName like '%" + name + "%'";

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<ChildAccount> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<ChildAccount>();
                    if (custs == null) custs = new List<ChildAccount>();

                    do
                    {
                        ChildAccount cust = new ChildAccount();

                        cust.ChildCode = dr["ChildCode"].ToString();
                        cust.ChildName = dr["ChildName"].ToString();


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
        public List<ChildAccount> GetAllChildAccountsByPCode(string pCode)
        {
            string selectSql = "select * from ChildAccount where ParentCode='" + pCode + "' order by ChildName";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectSql, con);
            List<ChildAccount> childs = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    childs = new List<ChildAccount>();
                    do
                    {
                        ChildAccount c = new ChildAccount();
                        c.HeadCode = Convert.ToInt32(dr["HeadCode"]);
                        c.GroupCode = dr["GroupCode"].ToString();

                        c.SubGroupCode = dr["SubGroupCode"].ToString();
                        c.ParentCode = dr["ParentCode"].ToString();
                        c.ChildCode = dr["ChildCode"].ToString();
                        c.ChildName = dr["ChildName"].ToString();
                        if (dr["Balance"] == DBNull.Value)
                            c.Balance = 0;
                        else
                            c.Balance = Convert.ToDecimal(dr["Balance"]);
                        childs.Add(c);

                    } while (dr.Read());
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
            return childs;
        }
        public void AddBankAccount(BankAccount ba)
        {
            string qurry = "AddBankAccount";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(qurry, con);
            cmd.CommandType = CommandType.StoredProcedure;
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
                    con.Close();
            }
        }
        public void UpdateParent(string pcode, string name)
        {
            string querry = "UpdateParent";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@oldParentCode", SqlDbType.NVarChar).Value = pcode;
            cmd.Parameters.Add(new SqlParameter("@ParentName", name));
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
        public void UpdateChild(string ccode, ChildAccount cha)
        {
            string querry = "UpdateChildAccount";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@oldChildCode", SqlDbType.NVarChar).Value = ccode;
            cmd.Parameters.Add(new SqlParameter("@ChildName", cha.ChildName));
            cmd.Parameters.Add(new SqlParameter("@Type", cha.AccountType));
            cmd.Parameters.Add(new SqlParameter("@Status", cha.Status));
            cmd.Parameters.Add(new SqlParameter("@Balance", cha.Balance));
            cmd.Parameters.Add(new SqlParameter("@GoldBalance", cha.GoldBalance));
            cmd.Parameters.Add(new SqlParameter("@OpeningCash", cha.OpCash));
            cmd.Parameters.Add(new SqlParameter("@OpeningGold", cha.OpGold));
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
        public void DeleteParent(string pcode)
        {
            string querry1 = "delete from ParentAccount where ParentCode='" + pcode + "'";
            string querry = "delete from Bank where ParentCode='" + pcode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd1 = new SqlCommand(querry1, con);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                cmd1.ExecuteNonQuery();
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
        public List<AccType> AllAccountsType()
        {
            string query = "select distinct [Type] from ChildAccount";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<AccType> atl = null;
            AccType at = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                    atl = new List<AccType>();
                do
                {
                    at = new AccType();
                    at.TypeName = dr["Type"].ToString();
                    atl.Add(at);
                } while (dr.Read());
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
            return atl;
        }
        public string GetOPV(string AccountCode)
        {
            string query = "Select VNO from Vouchers where VNO Like 'OPV%' and AccountCode ='" + AccountCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            string VNO = "";
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    VNO = dr["VNO"].ToString();
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
            return VNO;
        }
        public void DeleteChild(string ccode)
        {
            string querry = "delete from ChildAccount where ChildCode='" + ccode + "'";
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
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
        public void DeleteSubGroup(string sgcode)
        {
            string querry = "delete from SubGroupAccount where SubGroupCode='" + sgcode + "'";
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
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
        public void DeleteGroup(string gcode)
        {
            string querry = "delete from GroupAccount where GroupCode='" + gcode + "'";
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
                {
                    con.Close();
                    cmd.Dispose();
                }
            }
        }
        public decimal GetCashInHandBalance()
        {
            string query = "select(select sum(dr)-sum(cr) from Vouchers where AccountName ='Cash In Hand')+(select OpeningCash from ChildAccount where ChildName ='Cash In Hand')as Balance";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    if (dr["Balance"] == DBNull.Value)
                    {
                        Balance = 0;
                    }
                    else
                        Balance = Convert.ToDecimal(dr["Balance"]);
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
            return Balance;
        }
        public decimal GetCashInHandBalance(string ChildName)
        {
            string query = "select balance from ChildAccount where ChildName ='" + ChildName + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Balance = Convert.ToDecimal(dr["Balance"]);
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
            return Balance;
        }
        public decimal GetUsedGoldBalance()
        {
            string query = "select ((Select isnull(Sum(Weight),0) from GoldDetail where GoldType = 1 and (VNo Like 'AGV%' or VNo like 'GPV%')) - (Select isnull(Sum(Weight),0) from GoldDetail where GoldType = 1 and VNo like 'GSV%'))'UsedGold'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Balance = Convert.ToDecimal(dr["UsedGold"]);
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
            return Balance;
        }
        public decimal GetPureGoldBalance()
        {
            string query = "select ((select isnull(sum(OpeningGold),0) from ChildAccount where ChildName ='Gold In Hand')+(Select isnull(Sum(Weight),0) from GoldDetail where GoldType = 0 and (VNo Like 'AGV%' or VNo like 'GPV%')) - (Select isnull(Sum(Weight),0) from GoldDetail where GoldType = 0 and (VNo like 'GSV%' or VNo like 'WGV%')))'PureGold' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Balance = Convert.ToDecimal(dr["PureGold"]);
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
            return Balance;
        }
        public decimal GetCashInHandBalance(string ChildName,SqlConnection con,SqlTransaction tran)
        {
            string query = "select balance from ChildAccount where ChildName ='" + ChildName + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Transaction = tran;
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                //con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Balance = Convert.ToDecimal(dr["Balance"]);
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
            return Balance;
        }
        public void UpdateChildBalance(decimal Balance, string ccode)
        {
            string querry = "Update ChildAccount set Balance =" + Balance + " where ChildCode ='" + ccode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("oldCustId", SqlDbType.Int).Value = id;
            //cmd.Parameters.Add(new SqlParameter("CashBalance", cst.CashBalance));
            //cmd.Parameters.Add(new SqlParameter("GoldBalance", cst.GoldBalance));
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
        public void UpdateChildBalance(decimal Balance, string ccode,SqlConnection con , SqlTransaction tran)
        {
            string querry = "Update ChildAccount set Balance =" + Balance + " where ChildCode ='" + ccode + "'";
            //SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            cmd.Transaction = tran;
            //cmd.Parameters.Add("oldCustId", SqlDbType.Int).Value = id;
            //cmd.Parameters.Add(new SqlParameter("CashBalance", cst.CashBalance));
            //cmd.Parameters.Add(new SqlParameter("GoldBalance", cst.GoldBalance));
            try
            {
                //con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                tran.Rollback();
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
        public decimal GetAccountBalance(string ChildCode)
        {
            string query = "select balance from ChildAccount where ChildCode ='" + ChildCode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            decimal Balance = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Balance = Convert.ToDecimal(dr["Balance"]);
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
            return Balance;
        }
        public void UpdateGroup(string gcode, string name)
        {
            string querry = "Update GroupAccount set GroupName ='" + name + "' where GroupCode = '" + gcode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            //cmd.Parameters.Add("@oldGroupCode", SqlDbType.NVarChar).Value = gcode;
            //cmd.Parameters.Add(new SqlParameter("@GroupName", name));
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
        public void UpdateSubGroup(string sgcode, string name)
        {
            string querry = "Update SubGroupAccount set SubGroupName ='" + name + "' where SubGroupCode ='" + sgcode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.Add("@oldsubGroupCode", SqlDbType.NVarChar).Value = sgcode;
            //cmd.Parameters.Add(new SqlParameter("@SubGroupName", name));
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
        public void DeleteBankAccount(string acccode)
        {

            string querry = "Delete from BankAccount where AccountCode='" + acccode + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            //SqlCommand cmd1 = new SqlCommand(querry1, con);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                //cmd.ExecuteNonQuery();
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
        public bool isChildExist(string code, SqlConnection con, SqlTransaction trans)
        {
            string qurry = "select ChildCode from ChildAccount where ChildCode='" + code + "'";
            SqlCommand cmd = new SqlCommand(qurry, con);
            cmd.Transaction = trans;
            bool bFlag = false;
            SqlDataReader dr = null;
            try
            {
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
            }
            return bFlag;
        }
        public string CreatAccount(int hcode, string pname, string chname, SqlConnection con, SqlTransaction trans)
        {

            p = new ParentAccount();
            p = this.GetParent(pname, hcode.ToString(), con, trans);
            if (p == null)
            {
                p = new ParentAccount();
                p.HeadCode = hcode;
                p.ParentName = pname;
                p.ParentCode = this.CreateParentCode(p.HeadCode, con, trans);
                this.CreateParentAccount(p, con, trans);
            }
            c = new ChildAccount();
            c = this.GetChild(chname, p.ParentCode, con, trans);
            if (c == null)
            {
                c = new ChildAccount();
                c.HeadCode = p.HeadCode;
                c.ParentCode = p.ParentCode;
                c.ChildName = chname;
                c.ChildCode = this.CreateChildCode(c.ParentCode, c.HeadCode, con, trans);
                c.DDate = DateTime.Today;
                c.Status = "";
                c.Description = "";
                c.AccountType = "General Account";
                this.CreateChildAccount(c, true, con, trans);
            }
            return c.ChildCode;

            // g.GroupCode =this.CreateGroupCode(g).ToString();

        }
        public bool isChildExist(string code)
        {
            string qurry = "select ChildCode from ChildAccount where ChildCode='" + code + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(qurry, con);
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
        public string GetCodeByAccountNo(string AccountNo)
        {
            string query = "select AccountCode from BankAccount where AccountNo = '" + AccountNo + "'";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            string AccountCode = "";
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    AccountCode = dr["AccountCode"].ToString();
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
            return AccountCode;
        }
    }
}
