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
    public partial class SaleManAttendance : Form
    {
        int k = 0;
        SaleMan slm;
        List<SaleMan> slms = new List<SaleMan>();
        SaleManDAL slmDAL = new SaleManDAL();
        int slmId = 0;
        MyColors myColor = new MyColors();
        public SaleManAttendance()
        {
            InitializeComponent();
            FormControls.GetAllControls(this);
            //lblWorkInformation.BackColor = Color.FromArgb(myColor.SimpleLabel.Red, myColor.SimpleLabel.Green, myColor.SimpleLabel.Blue);
            //lblWorkInformation.ForeColor = Color.Black;
        }

        private void SaleManAttendance_Load(object sender, EventArgs e)
        {
            this.cbxSaleManName.SelectedIndexChanged -=new EventHandler(cbxSaleManName_SelectedIndexChanged);
            this.cbxSaleManName.DataSource = slmDAL.GetAllSaleMen();
            this.cbxSaleManName.DisplayMember = "Name";
            this.cbxSaleManName.ValueMember = "ID";
            this.cbxSaleManName.SelectedIndex = -1;

            this.cbxStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (k == 0)
            {
                MessageBox.Show("Please select Sale Man", Messages.Header);
                return;
            }
            else
            {
                slm = new SaleMan();
                slm.AttDate = Convert.ToDateTime(this.dtpDate.Value);
                bool bFlag;
                bFlag = slmDAL.isDateExist(slm.AttDate, (int)this.cbxSaleManName.SelectedValue);

                if (bFlag)
                {
                    if (MessageBox.Show("Attendence of Date " + slm.AttDate.ToString("dd-MMM-yy") + " is already exist do you want to change press OK", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        slm.ID = k;
                        slm.AttDate = this.dtpDate.Value;
                        slm.AttTime = this.dtpTime.Value;
                        slm.Status = (string)this.cbxStatus.SelectedItem;
                        slmDAL.UpdateSAttendence(slm.AttDate, slm);
                        MessageBox.Show("Record is Updated successfully");
                        this.ShowDGVRecord(k);
                    }
                }
                else
                {
                    slm.ID = k;
                    slm.AttDate = this.dtpDate.Value;
                    slm.AttTime = this.dtpTime.Value;
                    slm.Status = (string)this.cbxStatus.SelectedItem;
                    slmDAL.AddSAttendence(slm);

                    MessageBox.Show("Record is saved successfully");
                    this.ShowDGVRecord(k);
                }
            }
        }

        private void cbxSaleManName_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaleMan sm = (SaleMan)this.cbxSaleManName.SelectedItem;
            k = (int)sm.ID;
            this.txtContactNo.Text = sm.ContactNo.ToString();
            this.txtAddress.Text = sm.Address.ToString();
            this.ShowDGVRecord(k);
        }

        private void cbxSaleManName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.cbxSaleManName.SelectedIndexChanged += new EventHandler(cbxSaleManName_SelectedIndexChanged);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowDGVRecord(int k)
        {
            slms = slmDAL.GetAttendenceHist(k);
            if (slms == null)
                return;
            else
            {
                this.dgvPreviousRecord.AutoGenerateColumns = false;
                this.dgvPreviousRecord.Rows.Clear();
                int count = slms.Count;
                this.dgvPreviousRecord.Rows.Add(count);
                for (int i = 0; i < count; i++)
                {
                    this.dgvPreviousRecord.Rows[i].Cells[0].Value = slms[i].AttDate.ToString("dd-MMM-yy");
                    this.dgvPreviousRecord.Rows[i].Cells[1].Value = slms[i].AttDate.ToString("hh:mm:ss");
                    this.dgvPreviousRecord.Rows[i].Cells[2].Value = slms[i].Status.ToString();
                    this.dgvPreviousRecord.Rows[i].Cells[3].Value = slms[i].ID.ToString();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (k != 0)
            {
                bool bFlag;
                bFlag = slmDAL.isDateExist(DateTime.Today, (int)this.cbxSaleManName.SelectedValue);
                if (bFlag)
                {
                    if (MessageBox.Show("Do you want to delete Today attendence of this sale man", Messages.Header, MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        slmDAL.DeleteAttendence(slmId);
                        MessageBox.Show("Record deleted successfully", Messages.Header);
                        this.ShowDGVRecord(slmId);
                        this.btnSave.Enabled = true;
                        this.btnDelete.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("There is no today's attendence", Messages.Header, MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
            }
        }
    }
}
