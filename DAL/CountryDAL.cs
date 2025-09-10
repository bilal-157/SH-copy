using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class CountryDAL
    {
        public int GetMaxCounryID()
        {
            string query = "Select Max(CountryId) as [MAXID] from Country ";
            SqlConnection con = new SqlConnection(DALHelper .ConnectionString );
            SqlCommand cmd = new SqlCommand(query, con);
            int id = 0;

            try
            {
                con.Open();
                SqlDataReader dr =cmd.ExecuteReader (CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["MAXID"] == DBNull.Value)
                        id = 0;
                    else
                        id = Convert .ToInt32 (dr["MAXID"]);
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
            return id;
        }
        public List<City> GetAllCityBy(int id)
        {
            string query = "Select * From City where StateId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            List<City> cnts = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnts = new List<City>();
                    if (cnts == null) cnts = new List<City>();

                    do
                    {
                        City cn = new City();
                        cn.CityId = Convert.ToInt32(dr["CityId"]);
                        cn.CityName = dr["CityName"].ToString();

                        cnts.Add(cn);
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
            if (cnts != null) cnts.TrimExcess();
            return cnts;
        }

     
        public void AddCountry(Country c)
        {
            string query = "insert into country values ( "+c.CountryId  +" ,'"+ c.CountryName+"' )";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

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
                if (con.State == ConnectionState.Open) con.Close();
            }

        }
        public List<State> GetAllStateBy(int id)
        {
            string query = "Select * from States where CountryId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            List<State> sts = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    sts = new List<State>();
                    if (sts == null) sts = new List<State>();

                    do
                    {
                        State st = new State();
                        st.StateId = Convert.ToInt32(dr["StateId"]);
                        st.StateName = dr["StateName"].ToString();

                        sts.Add(st);
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
            if (sts != null) sts.TrimExcess();
            return sts;
        }

        public List<Country> GetAllCountry()
        {
            string query = "Select * From Country";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            
            List<Country> cnts = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnts = new List<Country>();
                    if (cnts == null) cnts = new List<Country>();

                    do
                    {
                        Country cn = new Country();
                        cn.CountryId = Convert.ToInt32(dr["CountryId"]);
                        cn.CountryName  = dr["CountryName"].ToString();
                       
                        cnts.Add(cn);
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
            if (cnts != null) cnts.TrimExcess();
            return cnts;
        }
          public List<City> SearchCity(string query, int a)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<City> city = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    city = new List<City>();
                    if (city == null) city = new List<City>();
                    do
                    {
                        City cty = new City();
                        cty.CityId = Convert.ToInt32(dr["CityId"]);
                        cty.CityName = dr["CityName"].ToString();
                        cty.CntId.CountryId=Convert.ToInt32(dr["CountryId"]);
                        cty.CntId.CountryName=dr["CountryName"].ToString();

                        city.Add(cty);
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

            return city;
        }
        public int GetMaxCityID()
        {
            string query = "Select Max(CityId) as [MAXID] from City ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            int id = 0;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);

                if (dr.Read())
                {
                    if (dr["MAXID"] == DBNull.Value)
                        id = 0;
                    else
                        id = Convert.ToInt32(dr["MAXID"]);
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
            return id;
        }

        

        public void AddCity(City c)
        {
            string query = "insert into City values ( " + c.CntId.CountryId + " ," + c.CityId + ",'"+c.CityName+"' )";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

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
                if (con.State == ConnectionState.Open) con.Close();
            }

        }

        public List<City> GetAllCity(int id)
        {
            string query = "Select * From City where CountryId= "+id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            List<City> cnts = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnts = new List<City>();
                    if (cnts == null) cnts = new List<City>();

                    do
                    {
                        City cn = new City();
                        cn.CityId = Convert.ToInt32(dr["CityId"]);
                        cn.CityName = dr["CityName"].ToString();

                        cnts.Add(cn);
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
            if (cnts != null) cnts.TrimExcess();
            return cnts;
        }
        public List<City> GetAllCityBy()
        {
            string query = "Select * From City ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            List<City> cnts = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    cnts = new List<City>();
                    if (cnts == null) cnts = new List<City>();

                    do
                    {
                        City cn = new City();
                        cn.CityId = Convert.ToInt32(dr["CityId"]);
                        cn.CityName = dr["CityName"].ToString();

                        cnts.Add(cn);
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
            if (cnts != null) cnts.TrimExcess();
            return cnts;
        }

        public bool isCityIdExist(string querry)
        {           
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
    }
}
