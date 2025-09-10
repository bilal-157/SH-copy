using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BusinesEntities;
namespace DAL
{
    public class PartyDAL
    {
        public void AddParty(Party part)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddParty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@PCode", part.PCode));
            cmd.Parameters.Add(new SqlParameter("@PName", part.PName));
            //cmd.Parameters.Add(new SqlParameter("PCode", part.PCode));
            cmd.Parameters.Add(new SqlParameter("@PAddress", part.PAddress));
            cmd.Parameters.Add(new SqlParameter("@PTel", part.PtclNo));
            cmd.Parameters.Add(new SqlParameter("@PMob", part.PMob));
            cmd.Parameters.Add(new SqlParameter("@PEmail", part.PEmail));
            cmd.Parameters.Add(new SqlParameter("@AccountCode", part.AccountCode));


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
        public int GetMaxPCode()
        {
            string querry = "select max(PCode) as [px] from Party";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
            cmd.CommandType = CommandType.Text;
            int t = 0;
            try
            {
                con.Open();
                SqlDataReader me = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (me.Read())
                {
                    if (me["px"] == DBNull.Value)
                        t = 0;
                    else
                        t = Convert.ToInt32(me["px"]);
                }

                con.Close();
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
            return t;

        }
       public List<Party> GetAllParties()
        {
            string constr = "select * from Party order by PCode";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(constr, con);
            List<Party> part = null;
            //SqlDataReader dr = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    part = new List<Party>();

                    do
                    {
                        Party prt = new Party();
                        prt.PCode = Convert.ToInt32(dr["PCode"].ToString());
                        prt.PName = dr["PName"].ToString();
                        prt.PAddress = dr["PAddress"].ToString();
                        prt.PtclNo = dr["PTel"].ToString();
                        prt.PMob = dr["PMob"].ToString();
                        prt.PEmail = dr["PEmail"].ToString();
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
        public Party GetRecByPartyId(int Id)
        {
            string constr = "select * from Party where PCode =" + Id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(constr, con);
            Party pty = null;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    pty = new Party();
                    pty.PCode = Convert.ToInt32(dr["PCode"].ToString());
                    pty.PName = dr["PName"].ToString();
                    pty.PAddress = dr["PAddress"].ToString();
                    pty.PtclNo = dr["PTel"].ToString();
                    pty.PMob = dr["PMob"].ToString();
                    pty.PEmail = dr["PEmail"].ToString();
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
        public void UpdateParty(int oldId,Party part)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("UpdateParty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@oldPCode", SqlDbType.Int).Value = oldId;
            cmd.Parameters.Add(new SqlParameter("@PName",part.PName));
            //cmd.Parameters.Add(new SqlParameter("PCode", part.PCode));
            cmd.Parameters.Add(new SqlParameter("@PAddress", part.PAddress));
            cmd.Parameters.Add(new SqlParameter("@PTel", part.PtclNo));
            cmd.Parameters.Add(new SqlParameter("@PMob", part.PMob));
            cmd.Parameters.Add(new SqlParameter("@PEmail", part.PEmail));


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
        public void DeleteParty(int Id)
        {
            string querry = "Delete from Party where PCode=" + Id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);
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
    }
}
