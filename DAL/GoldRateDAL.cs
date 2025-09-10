using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;

//Test SVN update
namespace DAL
{
     public class GoldRateDAL
    {
         public void AddGoldRates(GoldRates gr)
         {
             //string rates = "insert into Rates";
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("AddRates",con);
             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add(new SqlParameter("@RateDate", gr.RateDate));
             cmd.Parameters.Add(new SqlParameter("@K12",gr.K12Gram));
             cmd.Parameters.Add(new SqlParameter("@K13", gr.K13Gram));
             cmd.Parameters.Add(new SqlParameter("@K14", gr.K14Gram));
             cmd.Parameters.Add(new SqlParameter("@K15", gr.K15Gram));
             cmd.Parameters.Add(new SqlParameter("@K16", gr.K16Gram));
             cmd.Parameters.Add(new SqlParameter("@K17", gr.K17Gram));
             cmd.Parameters.Add(new SqlParameter("@K18", gr.K18Gram));
             cmd.Parameters.Add(new SqlParameter("@K19", gr.K19Gram));
             cmd.Parameters.Add(new SqlParameter("@K20", gr.K20Gram));
             cmd.Parameters.Add(new SqlParameter("@K21", gr.K21Gram));
             cmd.Parameters.Add(new SqlParameter("@K22", gr.K22Gram));
             cmd.Parameters.Add(new SqlParameter("@K23", gr.K23Gram));
             cmd.Parameters.Add(new SqlParameter("@K24", gr.K24Gram));
             

             cmd.Parameters.Add(new SqlParameter("@K12Tola", gr.K12Tola));
             cmd.Parameters.Add(new SqlParameter("@K13Tola", gr.K13Tola));
             cmd.Parameters.Add(new SqlParameter("@K14Tola", gr.K14Tola));
             cmd.Parameters.Add(new SqlParameter("@K15Tola", gr.K15Tola));
             cmd.Parameters.Add(new SqlParameter("@K16Tola", gr.K16Tola));
             cmd.Parameters.Add(new SqlParameter("@K17Tola", gr.K17Tola));
             cmd.Parameters.Add(new SqlParameter("@K18Tola", gr.K18Tola));
             cmd.Parameters.Add(new SqlParameter("@K19Tola", gr.K19Tola));
             cmd.Parameters.Add(new SqlParameter("@K20Tola", gr.K20Tola));
             cmd.Parameters.Add(new SqlParameter("@K21Tola", gr.K21Tola));
             cmd.Parameters.Add(new SqlParameter("@K22Tola", gr.K22Tola));
             cmd.Parameters.Add(new SqlParameter("@K23Tola", gr.K23Tola));
             cmd.Parameters.Add(new SqlParameter("@K24Tola", gr.K24Tola));
             
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

         public void AddPasaGoldRates(GoldRates gr)
         {
             //string rates = "insert into Rates";
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("insert into PasaRate values('" + Convert.ToDateTime (gr.RateDate) + "'," + Convert.ToDecimal(gr.PoundPasa) + "," + Convert.ToDecimal(gr.SonaPasa) + ")", con);
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
                 }
             }
         }

         public bool isDateExist(DateTime dt)
         {
            
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("IsDateExist", con);
             //SqlCommand cmd = new SqlCommand("select RateDate from GoldRates where Convert(varchar,RateDate,112)=convert(varchar,'" + Convert .ToDateTime (dt) + "',112)", con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;
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

         public bool isPasaDateExist(DateTime dt)
         {

             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             //SqlCommand cmd = new SqlCommand("select RateDate from GoldRates where Convert(varchar,RateDate,112)=convert(varchar,'" + Convert.ToDateTime(dt) + "',112)", con);
             SqlCommand cmd = new SqlCommand("IsPasaDateExist", con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;

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

         public GoldRates GetRates(DateTime dt)
         {
            
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetRates", con);
            cmd.CommandType = CommandType.StoredProcedure ;
            cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;
            GoldRates gr = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    gr = new GoldRates();

                    //gr.RateDate = Convert.ToDateTime(dr["RateDate"]);
                    gr.K12Gram = Convert.ToDecimal(dr["K12"]);
                    gr.K13Gram = Convert.ToDecimal(dr["K13"]);
                    gr.K14Gram = Convert.ToDecimal(dr["K14"]);
                    gr.K15Gram = Convert.ToDecimal(dr["K15"]);
                    gr.K16Gram = Convert.ToDecimal(dr["K16"]);
                    gr.K17Gram = Convert.ToDecimal(dr["K17"]);
                    gr.K18Gram = Convert.ToDecimal(dr["K18"]);
                    gr.K19Gram = Convert.ToDecimal(dr["K19"]);
                    gr.K20Gram = Convert.ToDecimal(dr["K20"]);
                    gr.K21Gram = Convert.ToDecimal(dr["K21"]);
                    gr.K22Gram = Convert.ToDecimal(dr["K22"]);
                    gr.K23Gram = Convert.ToDecimal(dr["K23"]);
                    gr.K24Gram = Convert.ToDecimal(dr["K24"]);

                    gr.K12Tola = Convert.ToDecimal(dr["K12Tola"]);
                    gr.K13Tola = Convert.ToDecimal(dr["K13Tola"]);
                    gr.K14Tola = Convert.ToDecimal(dr["K14Tola"]);
                    gr.K15Tola = Convert.ToDecimal(dr["K15Tola"]);
                    gr.K16Tola = Convert.ToDecimal(dr["K16Tola"]);
                    gr.K17Tola = Convert.ToDecimal(dr["K17Tola"]);
                    gr.K18Tola = Convert.ToDecimal(dr["K18Tola"]);
                    gr.K19Tola = Convert.ToDecimal(dr["K19Tola"]);
                    gr.K20Tola = Convert.ToDecimal(dr["K20Tola"]);
                    gr.K21Tola = Convert.ToDecimal(dr["K21Tola"]);
                    gr.K22Tola = Convert.ToDecimal(dr["K22Tola"]);
                    gr.K23Tola = Convert.ToDecimal(dr["K23Tola"]);
                    gr.K24Tola = Convert.ToDecimal(dr["K24Tola"]);
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
            return gr;
         }

         public GoldRates GetPasaRates(DateTime dt)
         {

             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             //SqlCommand cmd = new SqlCommand("select * from PasaRate where RDate = (Select Max(RDate) from PasaRate) and  convert(varchar, RDate, 112) = convert(varchar," + Convert.ToDateTime(dt) + ",112)", con);
             SqlCommand cmd = new SqlCommand("GetPasaRates", con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;
             GoldRates gr = null;
             try
             {
                 con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     gr = new GoldRates();

                     //gr.RateDate = Convert.ToDateTime(dr["RateDate"]);
                     gr.PoundPasa = Convert.ToDecimal(dr["PoundPasa"]);
                     gr.SonaPasa = Convert.ToDecimal(dr["SonaPasa"]);
                    
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
             return gr;
         }

         public void UpdateGoldRates(DateTime oldDt, GoldRates gr)
         {
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmdUpdate = new SqlCommand("UpdateRates", con);
             cmdUpdate.CommandType = CommandType.StoredProcedure;
             cmdUpdate.Parameters.Add("@oldRateDate", SqlDbType.DateTime).Value = oldDt;

             //cmdUpdate.Parameters.Add(new SqlParameter("@RateDate", gr.RateDate));
             cmdUpdate.Parameters.Add(new SqlParameter("@K12", gr.K12Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K13", gr.K13Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K14", gr.K14Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K15", gr.K15Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K16", gr.K16Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K17", gr.K17Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K18", gr.K18Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K19", gr.K19Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K20", gr.K20Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K21", gr.K21Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K22", gr.K22Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K23", gr.K23Gram));
             cmdUpdate.Parameters.Add(new SqlParameter("@K24", gr.K24Gram));

             cmdUpdate.Parameters.Add(new SqlParameter("@K12Tola", gr.K12Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K13Tola", gr.K13Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K14Tola", gr.K14Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K15Tola", gr.K15Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K16Tola", gr.K16Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K17Tola", gr.K17Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K18Tola", gr.K18Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K19Tola", gr.K19Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K20Tola", gr.K20Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K21Tola", gr.K21Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K22Tola", gr.K22Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K23Tola", gr.K23Tola));
             cmdUpdate.Parameters.Add(new SqlParameter("@K24Tola", gr.K24Tola));
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

         public void UpdatePasaGoldRates(DateTime oldDt, GoldRates gr)
         {
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             //SqlCommand cmdUpdate = new SqlCommand("Update PasaRate Set PoundPasa=" + gr.PoundPasa + ",SonaPasa=" + gr.SonaPasa + " where Convert(varchar,RDate,112)=convert(varchar,'" + Convert.ToDateTime(oldDt) + "',112)", con);
             SqlCommand cmdUpdate = new SqlCommand("UpdatePasaRates", con);
             cmdUpdate.CommandType = CommandType.StoredProcedure;
             cmdUpdate.Parameters.Add("@oldRateDate", SqlDbType.DateTime).Value = oldDt;

             cmdUpdate.Parameters.Add(new SqlParameter("@PoundPasa", gr.PoundPasa));
             cmdUpdate.Parameters.Add(new SqlParameter("@SonaPasa", gr.SonaPasa));

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

         public decimal GetRateByKarat(string karat, DateTime dt)
         {
           
             string str1 = "K" + karat;
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("GetRates", con);
            
             cmd.CommandType = CommandType.StoredProcedure;
            
             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;
            
             decimal rate = 0;

             try
             {
                 con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {
                     

                     switch (str1)
                     {
                         case "K24":
                             {
                                 rate = Convert.ToDecimal(dr["K24"]);
                                 break;
                             }
                         case "K23":
                             {
                                 rate = Convert.ToDecimal(dr["K23"]);
                                 break;
                             }
                         case "K22":
                             {
                                 rate = Convert.ToDecimal(dr["K22"]);
                                 break;
                             }
                         case "K21":
                             {
                                 rate = Convert.ToDecimal(dr["K21"]);
                                 break;
                             }
                         case "K20":
                             {
                                 rate = Convert.ToDecimal(dr["K20"]);
                                 break;
                             }
                         case "K19":
                             {
                                 rate = Convert.ToDecimal(dr["K19"]);
                                 break;
                             }
                         case "K18":
                             {
                                 rate = Convert.ToDecimal(dr["K18"]);
                                 break;
                             }
                         case "K17":
                             {
                                 rate = Convert.ToDecimal(dr["K17"]);
                                 break;
                             }
                         case "K16":
                             {
                                 rate = Convert.ToDecimal(dr["K16"]);
                                 break;
                             }
                         case "K15":
                             {
                                 rate = Convert.ToDecimal(dr["K15"]);
                                 break;
                             }
                         case "K14":
                             {
                                 rate = Convert.ToDecimal(dr["K14"]);
                                 break;
                             }
                         case "K13":
                             {
                                 rate = Convert.ToDecimal(dr["K13"]);
                                 break;
                             }
                         case "K12":
                             {
                                 rate = Convert.ToDecimal(dr["K12"]);
                                 break;
                             }
                         default :
                             return rate;


                     }

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
             return rate;
         }

         public decimal GetRateByKaratTola(string karat, DateTime dt)
         {

             string str1 = "K" + karat;
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("GetRates", con);

             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;

             decimal rate = 0;

             try
             {
                 con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {


                     switch (str1)
                     {
                         case "K24":
                             {
                                 rate = Convert.ToDecimal(dr["K24Tola"]);
                                 break;
                             }
                         case "K23":
                             {
                                 rate = Convert.ToDecimal(dr["K23Tola"]);
                                 break;
                             }
                         case "K22":
                             {
                                 rate = Convert.ToDecimal(dr["K22Tola"]);
                                 break;
                             }
                         case "K21":
                             {
                                 rate = Convert.ToDecimal(dr["K21Tola"]);
                                 break;
                             }
                         case "K20":
                             {
                                 rate = Convert.ToDecimal(dr["K20Tola"]);
                                 break;
                             }
                         case "K19":
                             {
                                 rate = Convert.ToDecimal(dr["K19Tola"]);
                                 break;
                             }
                         case "K18":
                             {
                                 rate = Convert.ToDecimal(dr["K18Tola"]);
                                 break;
                             }
                         case "K17":
                             {
                                 rate = Convert.ToDecimal(dr["K17Tola"]);
                                 break;
                             }
                         case "K16":
                             {
                                 rate = Convert.ToDecimal(dr["K16Tola"]);
                                 break;
                             }
                         case "K15":
                             {
                                 rate = Convert.ToDecimal(dr["K15Tola"]);
                                 break;
                             }
                         case "K14":
                             {
                                 rate = Convert.ToDecimal(dr["K14Tola"]);
                                 break;
                             }
                         case "K13":
                             {
                                 rate = Convert.ToDecimal(dr["K13Tola"]);
                                 break;
                             }
                         case "K12":
                             {
                                 rate = Convert.ToDecimal(dr["K12Tola"]);
                                 break;
                             }
                         default:
                             return rate;


                     }

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
             return rate;
         }

         public decimal GetRateByKarrat24(string karat, DateTime dt)
         {

             string str1 = "K" + karat;
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("GetRatesforPure", con);

             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;

             decimal rate = 0;

             try
             {
                 con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {


                     switch (str1)
                     {
                         case "K24":
                             {
                                 rate = Convert.ToDecimal(dr["K24"]);
                                 break;
                             }
                         case "K23":
                             {
                                 rate = Convert.ToDecimal(dr["K23"]);
                                 break;
                             }
                         case "K22":
                             {
                                 rate = Convert.ToDecimal(dr["K22"]);
                                 break;
                             }
                         case "K21":
                             {
                                 rate = Convert.ToDecimal(dr["K21"]);
                                 break;
                             }
                         case "K20":
                             {
                                 rate = Convert.ToDecimal(dr["K20"]);
                                 break;
                             }
                         case "K19":
                             {
                                 rate = Convert.ToDecimal(dr["K19"]);
                                 break;
                             }
                         case "K18":
                             {
                                 rate = Convert.ToDecimal(dr["K18"]);
                                 break;
                             }
                         case "K17":
                             {
                                 rate = Convert.ToDecimal(dr["K17"]);
                                 break;
                             }
                         case "K16":
                             {
                                 rate = Convert.ToDecimal(dr["K16"]);
                                 break;
                             }
                         case "K15":
                             {
                                 rate = Convert.ToDecimal(dr["K15"]);
                                 break;
                             }
                         case "K14":
                             {
                                 rate = Convert.ToDecimal(dr["K14"]);
                                 break;
                             }
                         case "K13":
                             {
                                 rate = Convert.ToDecimal(dr["K13"]);
                                 break;
                             }
                         case "K12":
                             {
                                 rate = Convert.ToDecimal(dr["K12"]);
                                 break;
                             }
                         default:
                             return rate;


                     }

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
             return rate;
         }
         public decimal GetRateByTola(string karat, DateTime dt)
         {
             string str1 = "K" + karat;
             SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
             SqlCommand cmd = new SqlCommand("GetRates", con);

             cmd.CommandType = CommandType.StoredProcedure;

             cmd.Parameters.Add("@RateDate", SqlDbType.DateTime).Value = dt;

             decimal rate = 0;

             try
             {
                 con.Open();
                 SqlDataReader dr = cmd.ExecuteReader();
                 if (dr.Read())
                 {


                     switch (str1)
                     {
                         case "K12":
                             {
                                 rate = Convert.ToDecimal(dr["K12Tola"]);
                                 break;
                             }
                         case "K13":
                             {
                                 rate = Convert.ToDecimal(dr["K13Tola"]);
                                 break;
                             }
                         case "K14":
                             {
                                 rate = Convert.ToDecimal(dr["K14Tola"]);
                                 break;
                             }
                         case "K15":
                             {
                                 rate = Convert.ToDecimal(dr["K15Tola"]);
                                 break;
                             }
                         case "K16":
                             {
                                 rate = Convert.ToDecimal(dr["K16Tola"]);
                                 break;
                             }
                         case "K17":
                             {
                                 rate = Convert.ToDecimal(dr["K17Tola"]);
                                 break;
                             }
                         case "K18":
                             {
                                 rate = Convert.ToDecimal(dr["K18Tola"]);
                                 break;
                             }
                         case "K19":
                             {
                                 rate = Convert.ToDecimal(dr["K19Tola"]);
                                 break;
                             }
                         case "K20":
                             {
                                 rate = Convert.ToDecimal(dr["K20Tola"]);
                                 break;
                             }
                         case "K21":
                             {
                                 rate = Convert.ToDecimal(dr["K21Tola"]);
                                 break;
                             }
                         case "K22":
                             {
                                 rate = Convert.ToDecimal(dr["K22Tola"]);
                                 break;
                             }
                         case "K23":
                             {
                                 rate = Convert.ToDecimal(dr["K23Tola"]);
                                 break;
                             }
                         case "K24":
                             {
                                 rate = Convert.ToDecimal(dr["K24Tola"]);
                                 break;
                             }
                         default:
                             return rate;


                     }

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
             return rate;
         }
}
}
