using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DAL;


namespace jewl
{
    public class UserRights
    {
        int userId = Login.userId;

        public string GetUserRightsByUser(string formName)
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select Rights from UserRights where UserId=" + userId + "and FormName='" + formName + "'", con);
            cmd.CommandType = CommandType.Text;
            string right = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //if (rights == null) rights = new List<RightsLineItem>();
                if (dr.Read())
                {

                    //right = new RightsLineItem();

                    //right.UserId = Convert.ToInt32(dr["UserId"]);
                    //right.FormName = dr["FormName"].ToString();
                    right = dr["Rights"].ToString();

                    //rights.Add(right);
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

            return right;

        }

        public string GetRightsByUser()
        {

            SqlConnection con = new SqlConnection(DALHelper.ConnectionString);
            SqlCommand cmd = new SqlCommand("select Rights from UserRights where UserId=" + Login.userId /*+ "and FormName=' " + formName + "'"*/, con);
            cmd.CommandType = CommandType.Text;
            string right = null;

            try
            {
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                //if (rights == null) rights = new List<RightsLineItem>();
                if (dr.Read())
                {

                    //right = new RightsLineItem();

                    //right.UserId = Convert.ToInt32(dr["UserId"]);
                    //right.FormName = dr["FormName"].ToString();
                    right = dr["Rights"].ToString();

                    //rights.Add(right);
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

            return right;

        }

        public void AssignRights(string frmRights, Button save, Button edit, Button delete, Button print, Button view)//, Button btn6, Button btn7)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                else if (a[i] == "Edit")
                {
                    edit.Enabled = true;
                }
                else if (a[i] == "Delete")
                {
                    delete.Enabled = true;
                }
                else if (a[i] == "Print")
                {
                    print.Enabled = true;
                    //btn5.Enabled = true;
                }
                else if (a[i] == "View")
                {
                    view.Enabled = true;
                }
                //else if (a[i] == "Sample")
                //{
                //    btn7.Enabled = true;
                //}
                //else if (a[i] == "Damage")
                //{
                //    btn8.Enabled = true;
                //}

            }

            //return a;
        }

        public void AssignRights(string frmRights, Button save)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                //else if (a[i] == "Edit")
                //{
                //    btn2.Enabled = true;
                //}
            }
        }

        public void AssignRights(string frmRights, ToolStripButton save)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                //else if (a[i] == "Edit")
                //{
                //    btn2.Enabled = true;
                //}
            }
        }

        public void AssignRights(string frmRights, Button save, Button edit)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                else if (a[i] == "Edit")
                {
                    edit.Enabled = true;
                }
            }
        }

        public void AssignRights(string frmRights, Button save, Button edit, Button delete)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                else if (a[i] == "Edit")
                {
                    edit.Enabled = true;
                }
                else if (a[i] == "Delete")
                {
                    delete.Enabled = true;
                }
            }
        }

        public void AssignRights(string frmRights, ToolStripButton save, ToolStripButton edit, ToolStripButton delete)
        {
            string[] a;
            a = frmRights.Split(' ');

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == "Save")
                {
                    save.Enabled = true;
                }
                else if (a[i] == "Edit")
                {
                    edit.Enabled = true;
                }
                else if (a[i] == "Delete")
                {
                    delete.Enabled = true;
                }
            }
        }
    }
}