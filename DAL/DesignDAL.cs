using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinesEntities;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class DesignDAL
    {
        private string AllDesign = "select * from Design";
        public void DesignItem(Design des)
        {
            string designItem = "select * from Design where DesignId= " + des.DesignId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(designItem, con);
            cmd.CommandType = CommandType.Text;
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                des = new Design();
                des.DesignId = Convert.ToInt32(dr["DesignId"]);
                des.DesignNo = dr["DesignNo"].ToString();
                dr.Close();

            }

            catch (Exception ex)
            {

                throw ex;
            }
            finally { if (con.State == ConnectionState.Open)con.Close(); }

        }

        public void AddDesignNo(Design des)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddDesignNo", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@DesignNo", des.DesignNo));

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

        public void UpdateDesignNo(int oldId, Design des)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateItem = new SqlCommand("UpdateDesignNo", con);
            cmdUpdateItem.CommandType = CommandType.StoredProcedure;

            cmdUpdateItem.Parameters.Add("@oldDesignId", SqlDbType.Int).Value = oldId;
            cmdUpdateItem.Parameters.Add(new SqlParameter("@DesignNo", des.DesignNo));

            try
            {
                con.Open();
                cmdUpdateItem.ExecuteNonQuery();
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

        public void DeleteDesign(int id, Design itm)
        {

            string deleteCustomer = "Delete from Design where DesignId=" + id;
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

        public List<Design> GetAllDesign()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(this.AllDesign, con);
            cmd.CommandType = CommandType.Text;
            List<Design> desi = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    desi = new List<Design>();
                    if (desi == null) desi = new List<Design>();

                    do
                    {
                        Design des = new Design();

                        des.DesignId = Convert.ToInt32(dr["DesignId"]);
                        des.DesignNo = dr["DesignNo"].ToString();

                        desi.Add(des);
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
            if (desi != null) desi.TrimExcess();
            return desi;
        }


        public List<Design> GetAllDesignfromStock()
        {
            string query = "Select DesNo from stock group by Designno";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            List<Design> desi = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    desi = new List<Design>();
                    if (desi == null) desi = new List<Design>();

                    do
                    {
                        Design des = new Design();

                        // des.DesignId = Convert.ToInt32(dr["DesignId"]);
                        des.DesignNo = dr["DesignNo"].ToString();

                        desi.Add(des);
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
            if (desi != null) desi.TrimExcess();
            return desi;
        }

        public List<Design> GetAllDesignByItemId(int id)
        {
            //string getItem = "select * from DesignItem where ItemId="+id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAllDesignByItmId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ItemId", SqlDbType.Int).Value = id;
            List<Design> lstDesign = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    if (lstDesign == null) lstDesign = new List<Design>();

                    do
                    {


                        Design dsg = new Design();


                        dsg.DesignId = Convert.ToInt32(dr["DesignId"]);
                        dsg.DesignNo = dr["DesignNo"].ToString();

                        lstDesign.Add(dsg);
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
            if (lstDesign != null) lstDesign.TrimExcess();
            return lstDesign;
        }

        public void AddDesignItem(Item itm)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("AddDesignItem", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@ItemId", itm.ItemId));
            cmd.Parameters.Add(new SqlParameter("@DesignId", itm.DesignItem.DesignId));

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { if (con.State == ConnectionState.Open)con.Close(); }

        }

        public void UpdateDesignItem(int oldId, Item itm)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdUpdateItem = new SqlCommand("UpdateDesignItem", con);
            cmdUpdateItem.CommandType = CommandType.StoredProcedure;

            cmdUpdateItem.Parameters.Add("@oldDesItmId", SqlDbType.Int).Value = oldId;

            cmdUpdateItem.Parameters.Add(new SqlParameter("@ItemId", itm.ItemId));
            cmdUpdateItem.Parameters.Add(new SqlParameter("@DesignId", itm.DesignItem.DesignId));

            try
            {
                con.Open();
                cmdUpdateItem.ExecuteNonQuery();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { if (con.State == ConnectionState.Open)con.Close(); }

        }

        public void DeleteDesignItem(int id, Item itm)
        {

            string deleteCustomer = "Delete from DesignItem where DesItmId=" + id;
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

        public Design GetDesignById(int id)
        {
            string selectsql = "select * from Design where DesignId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(selectsql, con);
            cmd.CommandType = CommandType.Text;
            Design desi = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    if (desi == null) desi = new Design();




                    desi.DesignId = Convert.ToInt32(dr["DesignId"]);
                    desi.DesignNo = dr["DesignNo"].ToString();






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
            //if (desi != null)
            return desi;
        }

        public List<Item> GetAllDesignAndItem()
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetAllDesignAndItm", con);
            cmd.CommandType = CommandType.StoredProcedure;
            List<Item> itms = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    itms = new List<Item>();
                    if (itms == null) itms = new List<Item>();

                    do
                    {




                        Item itm = new Item();
                        itm.DesItmid = Convert.ToInt32(dr["DesItmId"]);
                        itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                        itm.ItemName = dr["ItemName"].ToString();
                        //itm.DesignItem = this.GetAllDesignByItem(itm);
                        // itm .DesignItem =this .GetAllDesign 
                        itm.DesignItem = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());

                        itms.Add(itm);
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
            if (itms != null) itms.TrimExcess();
            return itms;
        }

        public Item GetDesItmById(int id)
        {
            //string selectsql = "select * from DesignItem where DesItmId=" + id;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("GetDesItmById", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@DesItmId", SqlDbType.Int).Value = id;
            Item itm = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    if (itm == null) itm = new Item();




                    // itm.DesItmid = Convert.ToInt32(dr["DesItmId"]);
                    itm.ItemId = Convert.ToInt32(dr["ItemId"]);
                    itm.ItemName = dr["ItemName"].ToString();

                    itm.DesignItem = new Design(Convert.ToInt32(dr["DesignId"]), dr["DesignNo"].ToString());







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
            //if (desi != null)
            return itm;
        }

        public bool isNameExist(string name)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select DesignNo from Design where DesignNo='" + name + "'";
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

        public bool isDesignIdExist(int desId)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select DesignId from Stock where DesignId=" + desId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool dFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    dFlag = true;


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
            return dFlag;
        }

        public bool isDesinIdExist(int desId)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select DesignId from Costing where DesignId=" + desId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool dFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    dFlag = true;


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
            return dFlag;
        }

        public bool isDesignExist(int desId)
        {
            //string querry = "select ItemName,Abbrivation from Item where ItemName='" + name + "' or Abrivation='" + abri + "' ";
            string querry = "select DesignId from DesignItem where DesignId=" + desId;
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(querry, con);

            bool dFlag = false;
            SqlDataReader dr = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                    dFlag = true;


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
            return dFlag;
        }

        public void UpdateStockofID(List<Design> ld)
        {
            for (int i = 0; i < ld.Count; i++)
            {
                string query = "Update Stock Set DesignId =" + ld[i].DesignId + " where DesNo='" + ld[i].DesignNo + "'";
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
        }

        public void UpdateForItemId(List<Item> ldl)
        {
            for (int i = 0; i < ldl.Count; i++)
            {
                string query = "Update Stock Set ItemId =" + ldl[i].ItemId + " where ItmName='" + ldl[i].ItemName + "'";
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
        }

        public void UpdateWorkerIdsInStock(List<Worker> lwl)
        {
            for (int i = 0; i < lwl.Count; i++)
            {
                string query = "Update Stock Set WorkerId =" + lwl[i].ID + " where WkName='" + lwl[i].Name + "'";
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
        }

        public List<string> GetAllTagNobers()
        {
            string query = "select TagNo from stonesdetail where TagNo not in (Select TagNo from Stock)";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            List<string> ls = new List<string>();
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string str = dr["TagNo"].ToString();
                    ls.Add(str);
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
            return ls;
        }

        public void DeleteStones(string tg)
        {
            string query = "Delete from StonesDetail where TagNo ='" + tg + "'";
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

        public List<Design> GetAllDesignByDesign(string Design)
        {

            string AllCustomerByCnic = "select * from Design where DesignNo like '%" + Design + "%' ";
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(AllCustomerByCnic, con);
            cmd.CommandType = CommandType.Text;
            List<Design> custs = null;
            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    custs = new List<Design>();
                    if (custs == null) custs = new List<Design>();

                    do
                    {
                        Design cust = new Design();
                        cust.DesignId = Convert.ToInt32(dr["DesignId"]);
                        cust.DesignNo = dr["DesignNo"].ToString();

                        //cust.Near=dr["Near"].ToString();
                        //cust.City = dr["City"].ToString();

                        //cust.Country = dr["Country"].ToString();


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

    }
}
