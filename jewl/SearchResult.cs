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
    public partial class SearchResult : Form
    {
        private int searchNo;

        public int SearchNo
        {
            get { return searchNo; }
            set { searchNo = value; }
        }
        public string serStr;
        public string serStr1;
        public string serStr2;
        public string serStr3;
        public string serStr4;
        public string serStr5;
        public bool isOrder = false;
        List<OrderEstimat> order;
        OrderDAL orderDAL = new OrderDAL();
        List<Customer> custs;
        CustomerDAL custDAL = new CustomerDAL();
        List<Sale> sale;
        SaleDAL saleDAL = new SaleDAL();
        public SearchResult()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            this.WindowState = FormWindowState.Normal;
        }

        private void SearchResult_Load(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(serStr)))
            {
                BalanceReceive balRece = new BalanceReceive();
                //serStr = (string)balRece.SearchStr;
                this.ShowCustRecByName(serStr);
            }
            else if (!(string.IsNullOrEmpty(serStr1)))
            {
                BalanceReceive balRece = new BalanceReceive();
                //serStr = (string)balRece.SearchStr;
                this.ShowCustRecByMob(serStr1);
            }
            else if (!(string.IsNullOrEmpty(serStr2)))
            {
                BalanceReceive balRece = new BalanceReceive();
                //serStr = (string)balRece.SearchStr;
                this.ShowCustRecByTelHome(serStr2);
            }
            //else if (!(string.IsNullOrEmpty(serStr3)))
            //{
            //    BalanceReceive balRece = new BalanceReceive();
            //    //serStr = (string)balRece.SearchStr;
            //    this.ShowCustRecByTelOffice(serStr3);
            //}
            else if (!(string.IsNullOrEmpty(serStr4)))
            {
                BalanceReceive balRece = new BalanceReceive();
                //serStr = (string)balRece.SearchStr;
                this.ShowCustRecByEmail(serStr4);
            }
            else if (!(string.IsNullOrEmpty(serStr5)))
            {
                BalanceReceive balRece = new BalanceReceive();
                //serStr = (string)balRece.SearchStr;
                this.ShowCustRecByAddress(serStr5);
            }
            else
            this.ShowCustRecDgv();
        }
        private void ShowCustRecDgv()
        {
            custs = custDAL.GetAllCustomer();
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
                    if (string.IsNullOrEmpty(custs[i].Mobile) && string.IsNullOrEmpty(custs[i].TelHome))
                        this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].CNIC.ToString();
                    else if (string.IsNullOrEmpty(custs[i].CNIC) && string.IsNullOrEmpty(custs[i].Mobile))
                        this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].TelHome.ToString();
                    else
                    this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].Mobile.ToString();                   
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();
                   

                }
            }
        }

        private void ShowCustRecByName(string name)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where Name like '%" + name + "%'");
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
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();


                }
            }
        }

        private void ShowCustRecByMob(string mob)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where Mobile like '" + mob + "%'");
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
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();


                }
            }
        }

        private void ShowCustRecByTelHome(string telhome)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where TelHome like '" + telhome + "%'");
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
                    this.dgvCustomerDetail.Rows[i].Cells[1].Value = custs[i].CNIC.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();


                }
            }
        }

        private void ShowCustRecByEmail(string email)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where Email like '%" + email + "%'");
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
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();


                }
            }
        }

        private void ShowCustRecByAddress(string address)
        {
            custs = custDAL.GetAllCustomer("select * from CustomerInfo where Address like '%" + address + "%'");
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
                    this.dgvCustomerDetail.Rows[i].Cells[2].Value = custs[i].Email.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[3].Value = custs[i].Address.ToString();
                    this.dgvCustomerDetail.Rows[i].Cells[4].Value = custs[i].AccountCode.ToString();


                }
            }
        }

        private void dgvCustomerDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string accCode = this.dgvCustomerDetail.Rows[e.RowIndex].Cells[4].Value.ToString();
            this.ShowSaleRecByCust(accCode);
        }
        private void ShowSaleRecByCust(string account)
        {
            sale =saleDAL.GetRecordsByAccount(account);
            order = orderDAL.GetOrderRecordsByAccount(account);
            if (sale == null && order == null)
                return;
            else
            {
                if (sale == null)
                {
                }
                else
                {
                    this.dgvSaleDetail.AutoGenerateColumns = false;
                    this.dgvSaleDetail.Rows.Clear();
                    int count = sale.Count;
                    this.dgvSaleDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {

                        this.dgvSaleDetail.Rows[i].Cells[0].Value = sale[i].SaleNo.ToString();
                        this.dgvSaleDetail.Rows[i].Cells[1].Value = sale[i].Balance.ToString();

                    }
                }

        
                if (order == null)
                { }
                else
                {
                    this.dgvOrderDetail.AutoGenerateColumns = false;
                    this.dgvOrderDetail.Rows.Clear();
                    int count = order.Count;
                    this.dgvOrderDetail.Rows.Add(count);
                    for (int i = 0; i < count; i++)
                    {

                        this.dgvOrderDetail.Rows[i].Cells[0].Value = order[i].OrderNo.ToString();
                        this.dgvOrderDetail.Rows[i].Cells[1].Value = order[i].Balance.ToString();

                    }
                }
               
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvSaleDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.searchNo= Convert.ToInt32(this.dgvSaleDetail.Rows[e.RowIndex].Cells[0].Value);
            this.Close();
        }

        private void dgvOrderDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.searchNo = Convert.ToInt32(this.dgvOrderDetail.Rows[e.RowIndex].Cells[0].Value);
            isOrder = true;
            this.Close();
        }
    }
}
