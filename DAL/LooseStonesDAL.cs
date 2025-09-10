using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using BusinesEntities;

namespace DAL
{
   public class LooseStonesDAL
    {
       public void AddLooseStonse(LooseStone lsp)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("AddLooseStones", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add(new SqlParameter("@PCode",lsp.party.PCode));
           cmd.Parameters.Add(new SqlParameter("@StoneId", lsp.Stone.Id));
           cmd.Parameters.Add(new SqlParameter("@StoneTypeId", lsp.Stone.TypeId));
           //cmd.Parameters.Add(new SqlParameter("@StoneType", lsp.Stone.TypeName));
           cmd.Parameters.Add(new SqlParameter("@StQty", lsp.Qty));
           cmd.Parameters.Add(new SqlParameter("@StWeight", lsp.Weight));
           cmd.Parameters.Add(new SqlParameter("@StRate", lsp.Rate));
           cmd.Parameters.Add(new SqlParameter("@StPrice", lsp.Price));
           cmd.Parameters.Add(new SqlParameter("@SDate", lsp.date));
           cmd.Parameters.Add(new SqlParameter("@VNO", lsp.VNO ));

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
       public List<LooseStone> GetAllStonesbyParty(int pcode)
       {
           string constr = "select l.*,p.PName,st.[Name] as StoneType,sn.StoneName from LooseStones l " +
"inner Join Party p on p.PCode = l.PCode " +
"inner join StonesType  st on st.StoneTypeId = l.StoneTypeId " +
"inner join StonesName  sn on sn.StoneId = l.StoneId  where l.PCode=" + pcode + " and convert(varchar, SDate, 112) = convert(varchar, GetDate(), 112)";
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(constr, con);
           List<LooseStone> llsp = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               //if (dr.Read())
               //{
                   llsp = new List<LooseStone>();
                while (dr.Read())
                   //do
                   {
                       LooseStone lsp = new LooseStone();
                       lsp.party = new Party();
                       lsp.party.PCode = Convert.ToInt32(dr["PCode"]);
                       lsp.Stone = new Stone();
                       lsp.Stone.TypeName = dr["StoneType"].ToString();
                       lsp.Stone.Name = dr["StoneName"].ToString();
                       //lsp.Stone.TypeName = new Stone();
                       //lsp.Stone.TypeName.StTypeName = dr["Stone.TypeName"].ToString();
                       //lsp.StonesName = new Stone();
                       //lsp.StonesName.StonesName = dr["StonesName"].ToString();
                       lsp.Weight = Convert.ToDecimal(dr["StWeight"]);
                       lsp.Price = Convert.ToDecimal(dr["StPrice"]);
                       lsp.date = Convert.ToDateTime(dr["SDate"]);
                       lsp.Qty = Convert.ToInt32(dr["StQty"]);
                       lsp.Rate = Convert.ToDecimal(dr["StRate"]);
                       lsp.LspId = Convert.ToInt32(dr["SlpId"]);
                       llsp.Add(lsp);
                   } //while (dr.Read());
                   dr.Close();
               //}
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
           return llsp;
       }
       public LooseStone GetLooseStonesbySlpId(int id)
       {
           string constr = "select ls.*,st.[Name],sn.StoneName from LooseStones ls inner join  stonestype st on st.stonetypeid = ls.stonetypeid inner join  StonesName sn on sn.stoneid = ls.stoneid where SlpId=" + id;
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand(constr, con);
           //List<LooseStone> llsp = null;
           LooseStone lsp = null;
           SqlDataReader dr = null;
           try
           {
               con.Open();
               dr = cmd.ExecuteReader();
               if (dr.Read())
               {
                   //llsp = new List<LooseStone>();
                   //do
                   //{
                       lsp = new LooseStone();
                       lsp.party = new Party();
                       lsp.party.PCode = Convert.ToInt32(dr["PCode"]);
                       lsp.Stone = new Stone();
                       lsp.Stone.TypeId=Convert.ToInt32(dr["StoneTypeId"]);
                       lsp.Stone.TypeName = dr["Name"].ToString();
                       lsp.Weight = Convert.ToDecimal(dr["StWeight"]);
                       lsp.Price = Convert.ToDecimal(dr["StPrice"]);
                       lsp.date = Convert.ToDateTime(dr["SDate"]);
                       //lsp.Name = new Stone();
                       lsp.Stone.Id = Convert.ToInt32(dr["StoneId"]);
                       lsp.Stone.Name = dr["StoneName"].ToString();
                       lsp.Qty = Convert.ToInt32(dr["StQty"]);
                       lsp.Rate = Convert.ToDecimal(dr["StRate"]);
                       lsp.LspId = Convert.ToInt32(dr["SlpId"]);
                       lsp.VNO = dr["VNO"].ToString();
                       //llsp.Add(lsp);
                   //} while (dr.Read());
                   //dr.Close();
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
           return lsp;
       }
       public void UpdateLooseStones(int id, LooseStone lsp)
       {
           SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
           SqlCommand cmd = new SqlCommand("UpdateLooseStones", con);
           cmd.CommandType = CommandType.StoredProcedure;
           cmd.Parameters.Add("@oldSlpId", SqlDbType.Int).Value = id; ;
           cmd.Parameters.Add(new SqlParameter("@PCode", lsp.party.PCode));
           cmd.Parameters.Add(new SqlParameter("@StoneId", lsp.Stone.Id));
           //cmd.Parameters.Add(new SqlParameter("@StonesName",lsp.Stone.Name));
           cmd.Parameters.Add(new SqlParameter("@StoneTypeId", lsp.Stone.TypeId));
           //cmd.Parameters.Add(new SqlParameter("@StoneTypeName", lsp.Stone.TypeName));
           cmd.Parameters.Add(new SqlParameter("@StQty", lsp.Qty));
           cmd.Parameters.Add(new SqlParameter("@StWeight", lsp.Weight));
           cmd.Parameters.Add(new SqlParameter("@StRate", lsp.Rate));
           cmd.Parameters.Add(new SqlParameter("@StPrice", lsp.Price));
           cmd.Parameters.Add(new SqlParameter("@SDate", lsp.date));


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
       public void DeleteLooseStones(int id)
       {
           string query = "Delete from LooseStones where SlpId =" + id;
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
}
