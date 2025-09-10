using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
//using System.Windows.Forms;


namespace DAL
{
    public class JewelConnection
    {
        public SqlConnection MyConnection = null;       //Connection Class Object Declartion
        public SqlCommand MyCommand = null;
        public SqlDataAdapter MyAdapter = null;
        public DataSet MyDataSet = new DataSet();       //Create Object

        //::::::-_-_:::::::Making Connection With JMDB DataBase::::::-_-_:::::://
        //---------------------------------------------------------------------//

        public void SetConnection()
        {
            try
            {
                MyConnection = new SqlConnection();
               // string constring = "Data Source=SAIM-PC\\SQLEXPRESS;Initial Catalog =JewlDB ;Integrated Security =SSPI";
                string constring = DALHelper.ConnectionString;
                MyConnection.ConnectionString = constring;
                MyConnection.Open();
            }
            catch (Exception )
            {
               // MessageBox.Show(ex.Message, ":::-_-_::: Error Message :::-_-_:::");
            }

        }

        //::::::-_-_::::::To Save Record From Data Base::::::-_-_:::::://
        //--------------------------------------------------------------//

        public void SaveRecordFromJMDB(string query)
        {
            try
            {
                SetConnection();
                MyCommand = new SqlCommand(query, MyConnection);
                MyCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception )
            {
               // MessageBox.Show(ex0.Message, ":::-_-_::: Error Message :::-_-_:::");
            }

        }

        //::::::-_-_::::::To Get Record From Database::::::-_-_:::::://
        //------------------------------------------------------------//

        public void GetDataFromJMDB(string query, string CustomTable)
        {
            try
            {
                SetConnection();
                MyCommand = new SqlCommand(query, MyConnection);
                MyAdapter = new SqlDataAdapter(MyCommand);
                MyAdapter.SelectCommand = MyCommand;
                MyAdapter.Fill(MyDataSet, CustomTable);
                MyAdapter.Dispose();
                MyConnection.Close();
            }
            catch (Exception )
            {
                //MessageBox.Show(ex0.Message, ":::-_-_::: Error Message :::-_-_:::");
            }
        }

        //::::::-_-_::::::To UpdateRecord From Database::::::-_-_:::::://
        //-------------------------------------------------------------//

        public void UpdateRecordFromJMDB(string query)
        {

            try
            {
                SetConnection();
                MyCommand = new SqlCommand(query, MyConnection);
                MyCommand.ExecuteNonQuery();
                MyConnection.Close();
            }
            catch (Exception )
            {
               // MessageBox.Show(ex0.Message, ":::-_-_::: Error Message :::-_-_:::");
            }


        }
        //::::::-_-_::::::To DeletedData From Database::::::-_-_:::::://
        //-------------------------------------------------------------//
        public void DeleteRecordfromJMDB(string query)
        {

            try
            {
                SetConnection();
                MyCommand = new SqlCommand(query, MyConnection);
                MyCommand.ExecuteNonQuery();
                MyConnection.Close();


            }

            catch (Exception )
            {
               // MessageBox.Show(ex0.Message, ":::-_-_::: Error Message :::-_-_:::");
            }


        }

    }
}
