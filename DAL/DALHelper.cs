using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data .OleDb;

namespace DAL
{
    public static class DALHelper
    {
        static StreamWriter log;
        static StreamReader sr;
        public static string ConnectionStringMaster { get { return ConfigurationManager.ConnectionStrings["MasterDBConnection"].ConnectionString.ToString(); } }
        public static string ConnectionString { get { return ConfigurationManager.ConnectionStrings["JewelDBConnection"].ConnectionString.ToString(); } }
        public static string ConStrPictures { get { return ConfigurationManager.ConnectionStrings["JewelPicsConnection"].ConnectionString.ToString(); } }
        public static string ReportsPath { get { return ConfigurationManager.AppSettings["ReportsPath"].ToString(); } }
        public static string PortName { get { return ConfigurationManager.AppSettings["PortName"].ToString(); } }
        public static int BaudRate { get { return Convert.ToInt32(ConfigurationManager.AppSettings["BaudRate"]); } }
        public static int DataBits { get { return Convert.ToInt32(ConfigurationManager.AppSettings["DataBits"]); } }
        public static string Parity { get { return ConfigurationManager.AppSettings["Parity"].ToString(); } }
        public static string StopBits { get { return ConfigurationManager.AppSettings["StopBits"].ToString(); } }
        public static string Handshake { get { return ConfigurationManager.AppSettings["Handshake"].ToString(); } }
        public static int HandshakeONOFF { get { return Convert.ToInt32(ConfigurationManager.AppSettings["HandshakeONOFF"]); } }
        public static bool RtsEnable { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["RtsEnable"]); } }
        public static int RtsEnableONOFF { get { return Convert.ToInt32(ConfigurationManager.AppSettings["RtsEnableONOFF"]); } }
        public static string ReadLine { get { return ConfigurationManager.AppSettings["ReadLine"].ToString(); } }

        public static void MessageFile(String msg)
        {
            try
            {
                bool t = File.Exists(@"C:\Users\Mudeel\My Documents\Message.txt");
                if (t == false)
                {
                    //File.Delete(@"C:\Users\Mudeel\My Documents\Message.txt");
                    log = new StreamWriter(@"C:\Users\Mudeel\My Documents\Message.txt", true);
                    log.AutoFlush = true;
                    log.Write(msg + " " + DateTime.Now + "\n\r");
                }
                if (t == true)
                {
                    #region test
                    //try
                    //{
                    //    Pass the file path and file name to the StreamReader constructor
                    //    sr = new StreamReader(@"C:\Users\Mudeel\My Documents\Message.txt");

                    //    Read the first line of text
                    //    line = /*File.ReadAllText(@"C:\Users\Mudeel\My Documents\Message.txt");*/ sr.ReadLine();

                    //    Continue to read until you reach end of file
                    //    /*while (line != null)
                    //    {
                    //        write the lie to console window
                    //        Console.WriteLine(line);
                    //        Read the next line
                    //        line = sr.ReadLine();
                    //    }*/

                    //    close the file
                    //    sr.Close();
                    //    Console.ReadLine();
                    //}
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine("Exception: " + e.Message);
                    //}
                    //finally
                    //{
                    //    Console.WriteLine("Executing finally block.");
                    //}
                    //log.Write("ERROR >> " + DateTime.Now + ":" + msg + "\n\r");

                    //if (line == "")
                    //{
                    //    line = line.Replace(line, msg);
                    //    log.Write(line + " " + DateTime.Now + ":\n\r");
                    //}
                    //else
                    //{
                    //    line=line.Remove(0,line.Length);                        
                    //    //line = line.Replace(line, msg);
                    //    log.WriteLine(msg);// + " " + DateTime.Now + ":\n\r");                     
                    //}
                    #endregion
                    File.Delete(@"C:\Users\Mudeel\My Documents\Message.txt");
                    log = new StreamWriter(@"C:\Users\Mudeel\My Documents\Message.txt", true);
                    log.AutoFlush = true;
                    log.Write(msg + " " + DateTime.Now + "\n\r");
                }
            }
            catch (Exception e)
            {
                string strMessage = e.ToString();
            }
        }

        public static void debug(String msg)
        {
            try
            {
                if (log == null)
                {
                    log = new StreamWriter(@"C:\Users\Mudeel\My Documents\Message.txt", true);
                    log.AutoFlush = true;
                }
                log.Write("DEBUG >> " + DateTime.Now + "." + DateTime.Now.Millisecond + ":" + msg + "\n\r");
            }
            catch (Exception e)
            {
                string strMessage = e.ToString();
                ;
            }
        }

        public static void ReplaceLineByLine(string filePath, string replaceText, string withText)
        {
            StreamReader streamReader = new StreamReader(filePath);
            StreamWriter streamWriter = new StreamWriter(filePath + ".tmp");

            while (!streamReader.EndOfStream)
            {
                string data = streamReader.ReadLine();
                data = data.Replace(replaceText, withText);
                streamWriter.WriteLine(data);
            }

            streamReader.Close();
            streamWriter.Close();
        }

        public static string ConStockString = "Data Source=TAUSEEF-PC\\SQLEXPRESS;Initial Catalog=JewlDB;Integrated Security=False;User ID=sa;Password=123; Connection TimeOut = 6000;";
    }
}
//        public void InsertRow(string connectionString, string insertSQL)
//        {
//    using (OleDbConnection connection = new OleDbConnection(connectionString))
//    {
//        // The insertSQL string contains a SQL statement that
//        // inserts a new row in the source table.
//        OleDbCommand command = new OleDbCommand(insertSQL);

//        // Set the Connection to the new OleDbConnection.
//        command.Connection = connection;

//        // Open the connection and execute the insert command.
//        try
//        {
//            connection.Open();
//            command.ExecuteNonQuery();
        
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex.Message);
//        }}}}
//    }
//}