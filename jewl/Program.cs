using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinesEntities;
using System.Globalization;
using DAL;

namespace jewl
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Costing());
            //Application.Run(new ManageStock());
            //Application.Run(new AddSample());
            //Application.Run(new SampleReturn());
            //Application.Run(new StockSearch());
            int PID = 0;
            string str = DateCheck.GetProcessorID();
            //string k = DateTime.Now.ToString("MM/dd/yyyy");
            //DateTime d = new DateTime();
            //d = DateTime.ParseExact(k,"MM/dd/yyyy",null );
            DateTime s = DateTime.Parse("06/04/2019");//, "MM/DD/YYYY", null);
            DateTime e = DateTime.Parse("11/30/2025");//, "MM/DD/YYYY", null);
            DateTime d = DateTime.Today;
            DateTime md = DateDAL.GetMaxDate();
            //if (md < s)
            //{
            //    DateDAL.AddDates(d);
            //}
            if (d >= md && d < e)
            {
                DateDAL.AddDates(d);
            }
            md = DateDAL.GetMaxDate();
            //md = md.ToShortDateString();
            //d = d.ToShortDateString();
            List<string> proId;
            bool check = false;
            #region check
            if (md == d && md < e)
            {
                proId = DateDAL.GetProcessId();
                foreach (string st in proId)
                {
                    if (str.Equals(st))
                    {
                        Application.Run(new Main());
                        check = true;
                        break;
                    }
                }
                if (!check)
                {
                    frmPassword frm = new frmPassword();
                    frm.Password = "bringoo786";
                    frm.ShowDialog();
                    PID = frm.id;
                    frm.Dispose();
                    if (PID == 1)
                    {
                        new UtilityDAL().ExceuteNonQuery("insert into tblSystemInfo values ('" + str + "')");
                    }
                }
                return;
            }
            else
            {
                   // MessageBox.Show("Please Contact your Software vender:03007660141,03217778823","Jewel Manger 2011");
                MessageBox.Show("", "汉语/漢語", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            #endregion          
        }
    }
}
