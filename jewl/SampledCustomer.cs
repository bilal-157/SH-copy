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
    public partial class SampledCustomer : Form
    {
        SampleDAL smDAL = new SampleDAL();
        List<Customer> custs;
        List<SampleLineItem> slis;
        int SNo;
        public SampledCustomer()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }
        private int sampleNo;

        public int SampleNo
        {
            get { return this.sampleNo; }
            set { this.sampleNo = value; }
        }
        private void ShowCustDgv()
        {
            custs = smDAL.GetAllSampleCustomers();
            if (custs == null)
                return;
            else
            {
                this.dgvCustomerDetail.AutoGenerateColumns = false;
                this.dgvCustomerDetail.Rows.Clear();
                int count = custs.Count;
                this.dgvCustomerDetail.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvCustomerDetail.Rows[i].Cells[0].Value = custs[i].Name.ToString();


                    this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Mobile.ToString();
                    if (custs[i].Address!=null)
                    {
                        this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Address.ToString();
                    }
                    else
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = "";
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = Convert.ToInt32(custs[i].ID);
                }
            }
        }

        private void SampledCustomer_Load(object sender, EventArgs e)
        {
            this.ShowCustDgv();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCustomerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SNo = Convert.ToInt32(dgvCustomerDetail.Rows[e.RowIndex].Cells[4].Value);
            this.ShowSampleNo(SNo);
        }

        private void ShowSampleNo(int sNo)
        {
            slis = smDAL.GetSampleNoByCust(sNo);
            if (slis == null)
                return;
            else
            {
                this.dgvSampleNo.AutoGenerateColumns = false;
                this.dgvSampleNo.Rows.Clear();
                int count = slis.Count;
                this.dgvSampleNo.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvSampleNo.Rows[i].Cells[0].Value = slis[i].SampleNo.ToString();
                    this.dgvSampleNo.Rows[i].Cells[1].Value = slis[i].SampleDate.ToString();


                }
            }
        }

        private void dgvSampleNo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.SampleNo = Convert .ToInt32 (dgvSampleNo .Rows [e.RowIndex ].Cells [0].Value) ;
            //ShowRec sr = new ShowRec(smR.ShowTagNo);
            //sr(sno);
            this.Hide();
        }
    }
}
