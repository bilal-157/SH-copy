using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using BusinesEntities;
namespace DAL
{
    public class WorkerDealingMasterDALcs
    {
        public int GetMaxBillNo()
        {
            string query = "select MAX(BillNo) MaxBillNo from WorkerDealingMaster";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            int billno = 0;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (dr.Read())
                {
                    if (dr["MaxBillNo"] == DBNull.Value)
                        billno = 0;
                    else
                        billno = Convert.ToInt32(dr["MaxBillNo"]);
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
            return billno;
        }
    }
}
