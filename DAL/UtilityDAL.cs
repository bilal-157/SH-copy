using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Security.Principal;
using CrystalDecisions.Shared;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Configuration;
using System.ComponentModel;
using CrystalDecisions.CrystalReports.Engine;

namespace DAL
{
    public class UtilityDAL
    {
        BackgroundWorker bk;
        public void BackupDatabase(String databaseName, String userName, String password, String destinationPath, BackgroundWorker bk)
        {
            this.bk = bk;
            Backup sqlBackup = new Backup();
            sqlBackup.PercentComplete += new PercentCompleteEventHandler(sqlBackup_PercentComplete);
            sqlBackup.Action = BackupActionType.Database;
            sqlBackup.BackupSetDescription = "ArchiveDataBase:" +
                                             DateTime.Now.ToShortDateString();
            sqlBackup.BackupSetName = "Archive";

            sqlBackup.Database = databaseName;
            BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath, DeviceType.File);
            SqlConnection sqlCon = new SqlConnection(DALHelper.ConnectionString);
            ServerConnection connection = new ServerConnection(sqlCon);
            connection.StatementTimeout = 0;
            Server sqlServer = new Server(connection);

            Microsoft.SqlServer.Management.Smo.Database db = sqlServer.Databases[databaseName];

            sqlBackup.Initialize = true;
            sqlBackup.Checksum = true;
            sqlBackup.ContinueAfterError = true;

            sqlBackup.Devices.Add(deviceItem);
            sqlBackup.Incremental = false;

            sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
            sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

            sqlBackup.FormatMedia = false;
            sqlBackup.PercentCompleteNotification = 1;
            
            sqlBackup.SqlBackup(sqlServer);
        }
        private void sqlBackup_PercentComplete(object sender, PercentCompleteEventArgs e)
        {
            bk.ReportProgress(e.Percent);
        }

        public void BackupDatabase(String databaseName, String userName, String password, String destinationPath)
        {
            Backup sqlBackup = new Backup();

            sqlBackup.Action = BackupActionType.Database;
            sqlBackup.BackupSetDescription = "ArchiveDataBase:" +
                                             DateTime.Now.ToShortDateString();
            sqlBackup.BackupSetName = "Archive";

            sqlBackup.Database = databaseName;
            BackupDeviceItem deviceItem = new BackupDeviceItem(destinationPath, DeviceType.File);
            SqlConnection sqlCon = new SqlConnection(DALHelper.ConnectionString);
            ServerConnection connection = new ServerConnection(sqlCon);
            connection.StatementTimeout = 0;
            Server sqlServer = new Server(connection);
            
            Microsoft.SqlServer.Management.Smo.Database db = sqlServer.Databases[databaseName];

            sqlBackup.Initialize = true;
            sqlBackup.Checksum = true;
            sqlBackup.ContinueAfterError = true;

            sqlBackup.Devices.Add(deviceItem);
            sqlBackup.Incremental = false;

            sqlBackup.ExpirationDate = DateTime.Now.AddDays(3);
            sqlBackup.LogTruncation = BackupTruncateLogType.Truncate;

            sqlBackup.FormatMedia = false;

            sqlBackup.SqlBackup(sqlServer);
        }

        public void BackUp(string str)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand(str, con);

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
        public void RefreshDatabase()
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdDelete = new SqlCommand("RefreshDB", con);
            cmdDelete.CommandType = CommandType.StoredProcedure;
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

        public void MakeBackup(string backupPath, string backupPath1)
        {
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmdBackup = new SqlCommand("MakeBackup", con);
            cmdBackup.CommandType = CommandType.StoredProcedure;
            cmdBackup.Parameters.Add("@BackupPath", SqlDbType.NVarChar).Value = backupPath;
            cmdBackup.Parameters.Add("@BackupPathPics", SqlDbType.NVarChar).Value = backupPath1;
            try
            {
                con.Open();
                cmdBackup.ExecuteNonQuery();
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

        public string EditionCheck()
        {
            //string str = DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss");
            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT SERVERPROPERTY ('edition')'Edition'", con);
            cmd.CommandType = CommandType.Text;
            SqlDataReader dr = null;
            string st = null;
            try
            {
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    if (dr.Read())
                    {
                        st = dr["Edition"].ToString();
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
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return st;
        }

        public void ExceuteNonQuery(string querry)
        {
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

        public void VerifyReports(string reportPath, ReportDocument report)
        {
            string str = this.EditionCheck();
            System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
            builder.ConnectionString = DALHelper.ConnectionString;
            string nombre = WindowsIdentity.GetCurrent().Name.ToString().Split('\\')[0];
            TableLogOnInfos crtableLogoninfos = new TableLogOnInfos();
            TableLogOnInfo crtableLogoninfo = new TableLogOnInfo();
            ConnectionInfo crConnectionInfo = new ConnectionInfo();
            Tables CrTables;
            report.Load(reportPath);
           
            crConnectionInfo.ServerName = builder.DataSource;
            crConnectionInfo.IntegratedSecurity = builder.IntegratedSecurity;
            crConnectionInfo.UserID = builder.UserID;
            crConnectionInfo.Password = builder.Password;
            crConnectionInfo.DatabaseName = builder.InitialCatalog;
            CrTables = report.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
            {
                crtableLogoninfo = CrTable.LogOnInfo;
                crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                CrTable.ApplyLogOnInfo(crtableLogoninfo);
            }
            if (report.Subreports.Count > 0)
            {
                CrTables = report.Subreports[0].Database.Tables ;
                foreach (CrystalDecisions.CrystalReports.Engine.Table CrTable in CrTables)
                {
                    crtableLogoninfo = CrTable.LogOnInfo;
                    crtableLogoninfo.ConnectionInfo = crConnectionInfo;
                    CrTable.ApplyLogOnInfo(crtableLogoninfo);
                }
            }
            if (builder.DataSource.Contains("sqlexpress") || builder.DataSource.Contains("SQLEXPRESS"))
            {
                if (builder.IntegratedSecurity.ToString() == "True" || builder.IntegratedSecurity.ToString() == "true")
                {
                    //report.SetDatabaseLogon("sa", "123");
                }
                else
                    report.SetDatabaseLogon("sa", "123");
            }
            else
            {
                if (builder.IntegratedSecurity.ToString() == "True" || builder.IntegratedSecurity.ToString() == "true")
                {
                    //report.SetDatabaseLogon("sa", "123");
                }
                else
                    report.SetDatabaseLogon("sa", "123");
            }
            //http://stackoverflow.com/questions/3315283/crystal-reports-not-chaging-the-server-programatically
            //SELECT SERVERPROPERTY('productversion')'Version', SERVERPROPERTY ('productlevel')'ServicePack', SERVERPROPERTY ('edition')'Edition'
            //http://stackoverflow.com/questions/141154/how-can-i-determine-installed-sql-server-instances-and-their-versions
        }
    }
}
