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
    public partial class SelectCustomer : Form
    {
        CustomerDAL custDAL = new CustomerDAL();
        SampleDAL smDAL = new SampleDAL();
        List<Customer> custs;
        List<SampleLineItem> smlis = new List<SampleLineItem>();

        public SelectCustomer()
        {
            InitializeComponent();
        }
        private void ShowCustRecDgv()
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
                    this.dgvCustomerDetail.Rows[i].Cells[0].Value = Convert.ToInt32(custs[i].ID);

                    this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Name.ToString();

                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Mobile.ToString();

                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].Address.ToString();
                                       

                }
            }
        }

        private void SelectCustomer_Load(object sender, EventArgs e)
        {
            this.ShowCustRecDgv();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvCustomerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id=Convert.ToInt32(this.dgvCustomerDetail.Rows[e.RowIndex].Cells[0].Value);
            this.ShowSampleNo(id);

        }

        private void ShowSampleNo(int id)
        { 
             smlis = smDAL.GetSampleNoByCust(id);
             if (smlis == null)
                    return;
                else
                {
                    this.dgvCustomerList.AutoGenerateColumns = false;
                    this.dgvCustomerList.Rows.Clear();
                    int count = smlis.Count;
                    this.dgvCustomerList.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {
                        this.dgvCustomerList.Rows[i].Cells[0].Value = smlis[i].SampleNo.ToString();


                        this.dgvCustomerList.Rows[i].Cells[1].Value = smlis[i].SampleDate.ToString();

                    }
                }
            
        }
    }
}
