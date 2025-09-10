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
    public partial class PasaGoldRate : Form
    {
        GoldRates grs = new GoldRates();
        GoldRateDAL grDAL = new GoldRateDAL();
        Main main = new Main();
        public DateTime dt;
        public PasaGoldRate()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            grs = new GoldRates();
            if (this.txtPoundPasa.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Pound Pasa", Messages.Header);
                return;
            }
            else
            {
                grs.RateDate = Convert.ToDateTime(this.dtpPasaGoldDate.Value);
                dt = grs.RateDate;
                bool bFlag;
                bFlag = grDAL.isPasaDateExist(grs.RateDate);

                if (bFlag)
                {
                    if (MessageBox.Show("Today rate is already exist if you want to save new rate press OK", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        grs.PoundPasa = Convert.ToDecimal(this.txtPoundPasa.Text);
                        grs.SonaPasa = Convert.ToDecimal(this.txtSonaPasa.Text);
                        grDAL.AddPasaGoldRates(grs);
                        MessageBox.Show("Record Saved Successfuly ", Messages.Header);
                        main.ShowPasaRates(this.dtpPasaGoldDate.Value);
                        this.Close();
                    }
                }
                else
                {
                    grs.PoundPasa = Convert.ToDecimal(this.txtPoundPasa.Text);
                    grs.SonaPasa = Convert.ToDecimal(this.txtSonaPasa.Text);
                    grDAL.AddPasaGoldRates(grs);
                    MessageBox.Show("Record Saved Successfuly ", Messages.Header);
                    main.ShowPasaRates(this.dtpPasaGoldDate.Value);
                    this.Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            dt = dtpPasaGoldDate.Value;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            grs = new GoldRates();
            if (this.txtPoundPasa.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Pasa Rate", Messages.Header);
                return;
            }
            else
            {
                grs.RateDate = Convert.ToDateTime(this.dtpPasaGoldDate.Value);
                dt = grs.RateDate;
                bool bFlag;
                bFlag = grDAL.isPasaDateExist(grs.RateDate);

                if (bFlag)
                {
                    if (MessageBox.Show("Today rate is already exist if you want to update the rate press OK", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        grs.PoundPasa = Convert.ToDecimal(this.txtPoundPasa.Text);
                        grs.SonaPasa = Convert.ToDecimal(this.txtSonaPasa.Text);
                        grDAL.UpdatePasaGoldRates(grs.RateDate, grs);
                        MessageBox.Show("Record updated successfuly ", Messages.Header);
                        this.Close();
                        return;

                    }

                }
            }
        }

        private void txtPoundPasa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtSonaPasa.Select();
             //   this.btnSave_Click(sender, e);
            }
        }

        private void txtSonaPasa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnSave_Click(sender, e);
            }
        }
    }
}