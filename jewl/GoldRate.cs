using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class GoldRate : Form
    {
        Formulas frm = new Formulas();
        GoldRates grs = new GoldRates();
        GoldRateDAL grDAL = new GoldRateDAL();
        public int uid;
        Timer timer = new Timer();
        Main main = new Main();
        public DateTime dt;

        public GoldRate()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = (10) * (1);
            timer.Enabled = true;
            timer.Start();
        }

        private void txt24KarratTola_KeyPress(object sender, KeyPressEventArgs e)
        {
            FormControls.validate_textBox(sender as TextBox, e);
        }

        private void txt24KarratGram_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            grs = new GoldRates();
            if (this.txt24KarratTola.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Tola Rate", Messages.Header);
                return;
            }
            else
            {
                grs.RateDate = Convert.ToDateTime(this.dtpGoldRateDate.Value);
                dt = grs.RateDate;
                //if (grs.RateDate != null)
                //{
                bool bFlag;
                bFlag = grDAL.isDateExist(grs.RateDate);

                if (bFlag)
                {
                    if (MessageBox.Show("Today rate is already exist if you want to save new rate press OK", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        grs.K12Gram = Convert.ToDecimal(this.txt12KarratGram.Text);
                        grs.K13Gram = Convert.ToDecimal(this.txt13KarratGram.Text);
                        grs.K14Gram = Convert.ToDecimal(this.txt14KarratGram.Text);
                        grs.K15Gram = Convert.ToDecimal(this.txt15KarratGram.Text);
                        grs.K16Gram = Convert.ToDecimal(this.txt16KarratGram.Text);
                        grs.K17Gram = Convert.ToDecimal(this.txt17KarratGram.Text);
                        grs.K18Gram = Convert.ToDecimal(this.txt18KarratGram.Text);
                        grs.K19Gram = Convert.ToDecimal(this.txt19KarratGram.Text);
                        grs.K20Gram = Convert.ToDecimal(this.txt20KarratGram.Text);
                        grs.K21Gram = Convert.ToDecimal(this.txt21KarratGram.Text);
                        grs.K22Gram = Convert.ToDecimal(this.txt22KarratGram.Text);
                        grs.K23Gram = Convert.ToDecimal(this.txt23KarratGram.Text);
                        grs.K24Gram = Convert.ToDecimal(this.txt24KarratGram.Text);

                        grs.K12Tola = Convert.ToDecimal(this.txt12KarratTola.Text);
                        grs.K13Tola = Convert.ToDecimal(this.txt13KarratTola.Text);
                        grs.K14Tola = Convert.ToDecimal(this.txt14KarratTola.Text);
                        grs.K15Tola = Convert.ToDecimal(this.txt15KarratTola.Text);
                        grs.K16Tola = Convert.ToDecimal(this.txt16KarratTola.Text);
                        grs.K17Tola = Convert.ToDecimal(this.txt17KarratTola.Text);
                        grs.K18Tola = Convert.ToDecimal(this.txt18KarratTola.Text);
                        grs.K19Tola = Convert.ToDecimal(this.txt19KarratTola.Text);
                        grs.K20Tola = Convert.ToDecimal(this.txt20KarratTola.Text);
                        grs.K21Tola = Convert.ToDecimal(this.txt21KarratTola.Text);
                        grs.K22Tola = Convert.ToDecimal(this.txt22KarratTola.Text);
                        grs.K23Tola = Convert.ToDecimal(this.txt23KarratTola.Text);
                        grs.K24Tola = Convert.ToDecimal(this.txt24KarratTola.Text);

                        grDAL.AddGoldRates(grs);
                        MessageBox.Show("Record saved successfuly ", Messages.Header);
                        main.ShowRates(Convert.ToDateTime(this.dtpGoldRateDate.Value));
                        this.Close();
                    }

                }
                else
                {
                    grs.K12Gram = Convert.ToDecimal(this.txt12KarratGram.Text);
                    grs.K13Gram = Convert.ToDecimal(this.txt13KarratGram.Text);
                    grs.K14Gram = Convert.ToDecimal(this.txt14KarratGram.Text);
                    grs.K15Gram = Convert.ToDecimal(this.txt15KarratGram.Text);
                    grs.K16Gram = Convert.ToDecimal(this.txt16KarratGram.Text);
                    grs.K17Gram = Convert.ToDecimal(this.txt17KarratGram.Text);
                    grs.K18Gram = Convert.ToDecimal(this.txt18KarratGram.Text);
                    grs.K19Gram = Convert.ToDecimal(this.txt19KarratGram.Text);
                    grs.K20Gram = Convert.ToDecimal(this.txt20KarratGram.Text);
                    grs.K21Gram = Convert.ToDecimal(this.txt21KarratGram.Text);
                    grs.K22Gram = Convert.ToDecimal(this.txt22KarratGram.Text);
                    grs.K23Gram = Convert.ToDecimal(this.txt23KarratGram.Text);
                    grs.K24Gram = Convert.ToDecimal(this.txt24KarratGram.Text);

                    grs.K12Tola = Convert.ToDecimal(this.txt12KarratTola.Text);
                    grs.K13Tola = Convert.ToDecimal(this.txt13KarratTola.Text);
                    grs.K14Tola = Convert.ToDecimal(this.txt14KarratTola.Text);
                    grs.K15Tola = Convert.ToDecimal(this.txt15KarratTola.Text);
                    grs.K16Tola = Convert.ToDecimal(this.txt16KarratTola.Text);
                    grs.K17Tola = Convert.ToDecimal(this.txt17KarratTola.Text);
                    grs.K18Tola = Convert.ToDecimal(this.txt18KarratTola.Text);
                    grs.K19Tola = Convert.ToDecimal(this.txt19KarratTola.Text);
                    grs.K20Tola = Convert.ToDecimal(this.txt20KarratTola.Text);
                    grs.K21Tola = Convert.ToDecimal(this.txt21KarratTola.Text);
                    grs.K22Tola = Convert.ToDecimal(this.txt22KarratTola.Text);
                    grs.K23Tola = Convert.ToDecimal(this.txt23KarratTola.Text);
                    grs.K24Tola = Convert.ToDecimal(this.txt24KarratTola.Text);

                    grDAL.AddGoldRates(grs);
                    MessageBox.Show("Record saved successfuly ", Messages.Header);


                    main.ShowRates(Convert.ToDateTime(this.dtpGoldRateDate.Value));
                    this.Close();
                }
            }
        }

        private void GoldRate_Load(object sender, EventArgs e)
        {
            
            UserRights ur = new UserRights();
            string str;
            str = ur.GetRightsByUser();
            if (str == "Administrator")
            {
                this.btnSave.Enabled = true;
                this.btnUpdate.Enabled = true;
            }
            else if (str == "Limited")
            {
                this.btnSave.Enabled = true;
            }
            else
            {
                str = ur.GetUserRightsByUser("GoldRate");
                if (str != "" && str != null)
                {
                    ur.AssignRights(str, this.btnSave);
                }
            }
            this .txt24KarratTola .TextChanged -=new EventHandler(txt24KarratTola_TextChanged);
            this.txt24KarratTola.Select();
        }
        private void RefreshRecord()
        {
            this.txt12KarratGram.Text = "";
            this.txt13KarratGram.Text = "";
            this.txt14KarratGram.Text = "";
            this.txt15KarratGram.Text = "";
            this.txt16KarratGram.Text = "";
            this.txt17KarratGram.Text = "";
            this.txt18KarratGram.Text = "";
            this.txt19KarratGram.Text = "";
            this.txt20KarratGram.Text = "";
            this.txt21KarratGram.Text = "";
            this.txt22KarratGram.Text = "";
            this.txt23KarratGram.Text = "";
            this.txt24KarratGram.Text = "";

            this.txt12KarratTola.Text = "";
            this.txt13KarratTola.Text = "";
            this.txt14KarratTola.Text = "";
            this.txt15KarratTola.Text = "";
            this.txt16KarratTola.Text = "";
            this.txt17KarratTola.Text = "";
            this.txt18KarratTola.Text = "";
            this.txt19KarratTola.Text = "";
            this.txt20KarratTola.Text = "";
            this.txt21KarratTola.Text = "";
            this.txt22KarratTola.Text = "";
            this.txt23KarratTola.Text = "";
            this.txt24KarratTola.Text = "";
        }

        private void txt24KarratTola_TextChanged(object sender, EventArgs e)
        {
            decimal val = 0;
            string str = this.txt24KarratTola.Text;
            if (string.IsNullOrEmpty(str))
            {
                this.txt24KarratTola.Text = "0";
                val = 0;
            }
            else 
            {
                val = Convert.ToDecimal(str.ToString());
            }
            frm.TRate(23, val, txt23KarratTola);
            frm.TRate(22, val, txt22KarratTola);
            frm.TRate(21, val, txt21KarratTola);
            frm.TRate(20, val, txt20KarratTola);
            frm.TRate(19, val, txt19KarratTola);
            frm.TRate(18, val, txt18KarratTola);
            frm.TRate(17, val, txt17KarratTola);
            frm.TRate(16, val, txt16KarratTola);
            frm.TRate(15, val, txt15KarratTola);
            frm.TRate(14, val, txt14KarratTola);
            frm.TRate(13, val, txt13KarratTola);
            frm.TRate(12, val, txt12KarratTola);

            frm.GramRate(23, val, txt23KarratGram);
            frm.GramRate(22, val, txt22KarratGram);
            frm.GramRate(21, val, txt21KarratGram);
            frm.GramRate(20, val, txt20KarratGram);
            frm.GramRate(19, val, txt19KarratGram);
            frm.GramRate(18, val, txt18KarratGram);
            frm.GramRate(17, val, txt17KarratGram);
            frm.GramRate(16, val, txt16KarratGram);
            frm.GramRate(15, val, txt15KarratGram);
            frm.GramRate(14, val, txt14KarratGram);
            frm.GramRate(13, val, txt13KarratGram);
            frm.GramRate(12, val, txt12KarratGram);
        }

        private void txt24KarratTola_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {                
                this.btnSave_Click(sender, e);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            grs = new GoldRates();
            if (this.txt24KarratTola.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Tola Rate", Messages.Header);
                return;
            }
            else
            {
                grs.RateDate = Convert.ToDateTime(this.dtpGoldRateDate.Value);
                dt = grs.RateDate;
                bool bFlag;
                bFlag = grDAL.isDateExist(grs.RateDate);

                if (bFlag)
                {
                    if (MessageBox.Show("Today rate is already exist if you want to update the rate press OK", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        grs.K12Gram = Convert.ToDecimal(this.txt12KarratGram.Text);
                        grs.K13Gram = Convert.ToDecimal(this.txt13KarratGram.Text);
                        grs.K14Gram = Convert.ToDecimal(this.txt14KarratGram.Text);
                        grs.K15Gram = Convert.ToDecimal(this.txt15KarratGram.Text);
                        grs.K16Gram = Convert.ToDecimal(this.txt16KarratGram.Text);
                        grs.K17Gram = Convert.ToDecimal(this.txt17KarratGram.Text);
                        grs.K18Gram = Convert.ToDecimal(this.txt18KarratGram.Text);
                        grs.K19Gram = Convert.ToDecimal(this.txt19KarratGram.Text);
                        grs.K20Gram = Convert.ToDecimal(this.txt20KarratGram.Text);
                        grs.K21Gram = Convert.ToDecimal(this.txt21KarratGram.Text);
                        grs.K22Gram = Convert.ToDecimal(this.txt22KarratGram.Text);
                        grs.K23Gram = Convert.ToDecimal(this.txt23KarratGram.Text);
                        grs.K24Gram = Convert.ToDecimal(this.txt24KarratGram.Text);

                        grs.K12Tola = Convert.ToDecimal(this.txt12KarratTola.Text);
                        grs.K13Tola = Convert.ToDecimal(this.txt13KarratTola.Text);
                        grs.K14Tola = Convert.ToDecimal(this.txt14KarratTola.Text);
                        grs.K15Tola = Convert.ToDecimal(this.txt15KarratTola.Text);
                        grs.K16Tola = Convert.ToDecimal(this.txt16KarratTola.Text);
                        grs.K17Tola = Convert.ToDecimal(this.txt17KarratTola.Text);
                        grs.K18Tola = Convert.ToDecimal(this.txt18KarratTola.Text);
                        grs.K19Tola = Convert.ToDecimal(this.txt19KarratTola.Text);
                        grs.K20Tola = Convert.ToDecimal(this.txt20KarratTola.Text);
                        grs.K21Tola = Convert.ToDecimal(this.txt21KarratTola.Text);
                        grs.K22Tola = Convert.ToDecimal(this.txt22KarratTola.Text);
                        grs.K23Tola = Convert.ToDecimal(this.txt23KarratTola.Text);
                        grs.K24Tola = Convert.ToDecimal(this.txt24KarratTola.Text);

                        grDAL.UpdateGoldRates(grs.RateDate, grs);
                        MessageBox.Show("Record updated successfuly ", Messages.Header);
                        this.Close();
                        return;
                    }
                }
            }            
        }       

        private void textBox28_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Convert.ToInt16(e.KeyChar) < 48 || Convert.ToInt16(e.KeyChar) > 57) && Convert.ToInt16(e.KeyChar) != 8)
                e.Handled = true;
            else
                e.Handled = false;
        }
       
        private void lblTimer_MouseLeave(object sender, EventArgs e)
        {
            timer.Tick += new EventHandler(timer1_Tick);
        }

        private void lblTimer_MouseEnter(object sender, EventArgs e)
        {
            //timer.Tick -= new EventHandler(timer1_Tick);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (x == this.Width)
            //{
            //    m = m - 1;
            //    if (m == 0)
            //        x = m;
            //    pnlTimer.Location = new Point(m, y);
            //}
            //else if (x == 0)
            //{
            //    m = m + 1;
            //    if (m == this.Width)
            //        x = m;
            //    pnlTimer.Location = new Point(m, y);
            //}
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {
        //    try
        //    {
        //        System.Diagnostics.Process.Start("http://www.jewelmanager.com");
        //    }
        //    catch { }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            dt = dtpGoldRateDate.Value;
        }

        private void txt24KarratTola_KeyUp(object sender, KeyEventArgs e)
        {
            decimal val = FormControls.GetDecimalValue(this.txt24KarratTola, 1);
            
            frm.TRate(23, val, txt23KarratTola);
            frm.TRate(22, val, txt22KarratTola);
            frm.TRate(21, val, txt21KarratTola);
            frm.TRate(20, val, txt20KarratTola);
            frm.TRate(19, val, txt19KarratTola);
            frm.TRate(18, val, txt18KarratTola);
            frm.TRate(17, val, txt17KarratTola);
            frm.TRate(16, val, txt16KarratTola);
            frm.TRate(15, val, txt15KarratTola);
            frm.TRate(14, val, txt14KarratTola);
            frm.TRate(13, val, txt13KarratTola);
            frm.TRate(12, val, txt12KarratTola);

            frm.GramRate(24, val, txt24KarratGram);
            frm.GramRate(23, val, txt23KarratGram);
            frm.GramRate(22, val, txt22KarratGram);
            frm.GramRate(21, val, txt21KarratGram);
            frm.GramRate(20, val, txt20KarratGram);
            frm.GramRate(19, val, txt19KarratGram);
            frm.GramRate(18, val, txt18KarratGram);
            frm.GramRate(17, val, txt17KarratGram);
            frm.GramRate(16, val, txt16KarratGram);
            frm.GramRate(15, val, txt15KarratGram);
            frm.GramRate(14, val, txt14KarratGram);
            frm.GramRate(13, val, txt13KarratGram);
            frm.GramRate(12, val, txt12KarratGram);
        }
    }
}
