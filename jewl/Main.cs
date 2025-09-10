using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing.Drawing2D;
using BusinesEntities;
using DAL;

namespace jewl
{
    public partial class Main : Form
    {
        public static string City = "";
        StockDAL stkDAL = new StockDAL();
        GoldRateDAL grDAL = new GoldRateDAL();
        GoldRates grs;
        UserRights ur = new UserRights();
        StartUpp statp = new StartUpp();
        CountryDAL cntDAL = new CountryDAL();
        Timer t = new Timer();
        int x = 0;
        int y = 25;
        int m = 0;
        public int usrId;
        bool bFlag;
        public static bool pFlag;
        protected Graphics graphics;
        private const int CP_NOCLOSE_BUTTON = 0x200;
        string str = "";
        List<string> mainMen;
        List<string> subMen;

        public Main()
        {
            InitializeComponent();
            MainTheme();
            this.panel1.BackColor = Color.FromArgb(0, 188, 212);
            System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
            myTimer.Tick += new EventHandler(myTimer_Tick);
            myTimer.Interval = 1000;
            myTimer.Start();
        }

        public void MainTheme()
        {
            IEnumerable<Control> controls = FormControls.GetAll(this, typeof(Label));
            IEnumerable<Control> textcontrols = FormControls.GetAll(this, typeof(TextBox));

            foreach (var item in controls)
            {
                Label l1 = ((Label)item);
                l1.ForeColor = Color.FromArgb(255, 255, 255);
            }
            foreach (var item in textcontrols)
            {
                TextBox t1 = ((TextBox)item);
                t1.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddItem frmAddItem = new AddItem();
            frmAddItem.Show();
        }

        private void addStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageStock frmStock = new ManageStock();
            frmStock.ShowDialog();
        }

        private void addStoneDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StoneDetail frmStoneDetail = new StoneDetail();
            frmStoneDetail.ShowDialog();
        }

        private void addDesignToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddDesign frmDesign = new AddDesign();
            frmDesign.ShowDialog();
        }

        private void addWorkerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ManageWorker frmWorker = new ManageWorker();
            frmWorker.ShowDialog();
        }

        private void addCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageCustomer frmWorker = new ManageCustomer();
            frmWorker.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockReports frm = new StockReports();
            frm.ShowDialog();
        }


        private void directSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageSale frmdsale = new ManageSale();
            frmdsale.ShowDialog();
        }

        private void myTimer_Tick(object source, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Time " + DateTime.Now.ToString("hh:mm tt");
            toolStripStatusLabel3.Text = Login.userName.ToString();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            bool nflag = cntDAL.isCityIdExist("select * from city");
            if (nflag == false)
            {
                AddCity ct = new AddCity();
                ct.ShowDialog();
            }
            statp = stkDAL.GetStartUp();
            if (statp.JewlManagerType == "Tagging")
            {
                utilitiesToolStripMenuItem.Visible = false;
                toolStripMenuItem3.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem4.Visible = false;
                repairingToolStripMenuItem.Visible = false;
                toolStripMenuItem6.Visible = false;
                toolStripMenuItem11.Visible = false;
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem2.Visible = false;
                sToolStripMenuItem.Visible = false;
                orderToolStripMenuItem.Visible = false;
                saleToolStripMenuItem.Visible = false;
                toolStripMenuItem9.Visible = false;
                toolStripMenuItem10.Visible = false;
                toolStripMenuItem7.Visible = false;
                toolStripMenuItem8.Visible = false;
                addCustomerToolStripMenuItem.Visible = false;
                addSaleManToolStripMenuItem.Visible = false;
                seToolStripMenuItem.Visible = false;
                addSupplierToolStripMenuItem.Visible = false;
                sampleToolStripMenuItem.Visible = false;
                saleReportsToolStripMenuItem.Visible = false;
                splitSaleToolStripMenuItem.Visible = false;
                goldSalePurchaseToolStripMenuItem.Visible = false;
                workerDealingsToolStripMenuItem1.Visible = false;
                looseStonesToolStripMenuItem.Visible = false;
                orderReportsToolStripMenuItem.Visible = false;
                profitLossToolStripMenuItem.Visible = false;
                accountsReportsToolStripMenuItem.Visible = false;
                saleManToolStripMenuItem.Visible = false;
                customerToolStripMenuItem.Visible = false;
                btnStock.Visible = false;
                button1.Visible = false;
                button2.Visible = false;
            }
            if (statp.JewlManagerType == "StockSale")
            {
                utilitiesToolStripMenuItem.Visible = false;
                toolStripMenuItem1.Visible = false;
                toolStripMenuItem4.Visible = false;
                repairingToolStripMenuItem.Visible = false;
                toolStripMenuItem5.Visible = false;
                toolStripMenuItem2.Visible = false;
                sToolStripMenuItem.Visible = false;
                orderToolStripMenuItem.Visible = false;
                sampleToolStripMenuItem.Visible = false;
                goldSalePurchaseToolStripMenuItem.Visible = false;
                workerDealingsToolStripMenuItem1.Visible = false;
                looseStonesToolStripMenuItem.Visible = false;
                orderReportsToolStripMenuItem.Visible = false;
            }
            Formulas.WeightInGm = statp.GoldRateGram;
            if (statp.GoldRateType == "SonaPasa")
                City = "Islamabad";
            if (statp.GoldRateType == "Standard")
                City = "Lahore";
            Login log = new Login();
            log.ShowDialog();
            this.MenuItemsOff();
            Buttons(0);
            string str = ur.GetRightsByUser();

            if (log.lFlag == true && str == "Administrator")
            {
                this.MenuItemsOn();
                DateTime dt = DateTime.Now;
                if (City == "Islamabad")
                {
                    this.pnlGoldRate.Visible = false;
                    this.pnlPasa.Visible = true;
                    bFlag = grDAL.isPasaDateExist(dt);
                    if (bFlag == true)
                        this.ShowPasaRates(dt);
                    else
                        if (bFlag == false)
                        {
                            MessageBox.Show("There is no gold rate", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            PasaGoldRate gr = new PasaGoldRate();
                            gr.ShowDialog();
                            this.ShowPasaRates(dt);
                            gr.Close();
                        }
                }
                else
                {
                    this.pnlGoldRate.Visible = true;
                    bFlag = grDAL.isDateExist(dt);
                    if (bFlag == true)
                        this.ShowRates(dt);
                    else
                    {
                        MessageBox.Show("There is no gold rate", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GoldRate gr = new GoldRate();
                        gr.ShowDialog();
                        this.ShowRates(dt);
                        gr.Close();
                    }
                }
            }
            else if (log.lFlag == true && str == "Limited")
            {
                this.MenuItemsOnForLimitedUser();
                DateTime dt = DateTime.Now;
                if (City == "Islamabad")
                {
                    this.pnlGoldRate.Visible = false;
                    this.pnlPasa.Visible = true;
                    bFlag = grDAL.isPasaDateExist(dt);
                    if (bFlag == true)
                        this.ShowPasaRates(dt);
                    else
                        if (bFlag == false)
                        {
                            MessageBox.Show("There is no gold rate", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            PasaGoldRate gr = new PasaGoldRate();
                            gr.ShowDialog();
                            this.ShowPasaRates(dt);
                            gr.Close();
                        }
                }
                else
                {
                    this.pnlGoldRate.Visible = true;
                    bFlag = grDAL.isDateExist(dt);
                    if (bFlag == true)
                        this.ShowRates(dt);
                    else
                    {
                        MessageBox.Show("There is no gold rate", Messages.Header, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        GoldRate gr = new GoldRate();
                        gr.ShowDialog();
                        this.ShowRates(dt);
                        gr.Close();
                    }
                }
            }
            menuStrip1.Focus();
            ReportViewer frm = new ReportViewer();
            if (statp.JewlManagerType != "Tagging")
            {
                frm.isPage = 3;
                frm.rpt = 3;
                frm.id = 1;
                frm.ShowDialog();

                frm = new ReportViewer();
                frm.isPage = 2;
                frm.rpt = 2;
                frm.id = 1;
                frm.ShowDialog();
            }
            if (statp.ReportPassword != "")
            {
                this.chkPassword.Visible = true;
                reportsToolStripMenuItem.Visible = false;
            }
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
        }
        private void t_Tick(object sender, EventArgs e)
        {
            int hh = DateTime.Now.Hour;
            int mm = DateTime.Now.Minute;
            int ss = DateTime.Now.Second;
            string time = "";
            if (hh < 10)
            {
                time += "0" + hh;
            }
            else
            {
                time += hh;
            }
            time += ":";

            if (mm < 10)
            {
                time += "0" + mm;
            }
            else
            {
                time += mm;
            }
            time += ":";

            if (ss < 10)
            {
                time += "0" + ss;
            }
            else
            {
                time += ss;
            }
            this.lblTime.Text = time;
        }
        public void ShowPasaRates(DateTime dt)
        {
            grs = new GoldRates();
            grs = grDAL.GetPasaRates(dt);
            if (grs == null)
                return;
            else
            {
                this.txtSonaPasa.Text = grs.SonaPasa.ToString("0");
                this.txtPoundPasa.Text = grs.PoundPasa.ToString("0");
            }
        }
        public void ShowRates(DateTime dt)
        {
            grs = grDAL.GetRates(dt);
            if (grs == null)
                return;
            else
            {
                this.txt12KarratGram.Text = grs.K12Gram.ToString("0");
                this.txt13KarratGram.Text = grs.K13Gram.ToString("0");
                this.txt14KarratGram.Text = grs.K14Gram.ToString("0");
                this.txt15KarratGram.Text = grs.K15Gram.ToString("0");
                this.txt16KarratGram.Text = grs.K16Gram.ToString("0");
                this.txt17KarratGram.Text = grs.K17Gram.ToString("0");
                this.txt18KarratGram.Text = grs.K18Gram.ToString("0");
                this.txt19KarratGram.Text = grs.K19Gram.ToString("0");
                this.txt20KarratGram.Text = grs.K20Gram.ToString("0");
                this.txt21KarratGram.Text = grs.K21Gram.ToString("0");
                this.txt22KarratGram.Text = grs.K22Gram.ToString("0");
                this.txt23KarratGram.Text = grs.K23Gram.ToString("0");
                this.txt24KarratGram.Text = grs.K24Gram.ToString("0");

                this.txt12KarratTola.Text = grs.K12Tola.ToString("0");
                this.txt13KarratTola.Text = grs.K13Tola.ToString("0");
                this.txt14KarratTola.Text = grs.K14Tola.ToString("0");
                this.txt15KarratTola.Text = grs.K15Tola.ToString("0");
                this.txt16KarratTola.Text = grs.K16Tola.ToString("0");
                this.txt17KarratTola.Text = grs.K17Tola.ToString("0");
                this.txt18KarratTola.Text = grs.K18Tola.ToString("0");
                this.txt19KarratTola.Text = grs.K19Tola.ToString("0");
                this.txt20KarratTola.Text = grs.K20Tola.ToString("0");
                this.txt21KarratTola.Text = grs.K21Tola.ToString("0");
                this.txt22KarratTola.Text = grs.K22Tola.ToString("0");
                this.txt23KarratTola.Text = grs.K23Tola.ToString("0");
                this.txt24KarratTola.Text = grs.K24Tola.ToString("0");
            }
        }

        private void goldRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (City == "Islamabad")
            {
                PasaGoldRate pgr = new PasaGoldRate();
                pgr.ShowDialog();
                DateTime dt = DateTime.Now;
                this.ShowPasaRates(dt);
                pgr.Close();
            }
            else
            {
                GoldRate grs = new GoldRate();
                grs.ShowDialog();
                grs.Close();
                DateTime dt = DateTime.Now;
                this.ShowRates(dt);
            }
        }

        private void sampleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleReports frmsm = new SampleReports();
            frmsm.ShowDialog();
        }

        private void goldPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoldPurchase frm = new GoldPurchase();
            frm.ShowDialog();
        }

        private void goldSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoldSale frm = new GoldSale();
            frm.ShowDialog();
        }

        private void workerDealingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmWorkerDealing frm = new frmWorkerDealing();
            frm.ShowDialog();
        }

        private void chartOfAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartOfAccount frm = new ChartOfAccount();
            frm.ShowDialog();
        }

        private void goldSalePurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGoldSalePurchaseReports frm = new frmGoldSalePurchaseReports();
            frm.ShowDialog();
        }

        private void workerDealingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmWorkerDealingReports frm = new frmWorkerDealingReports();
            frm.ShowDialog();
        }

        private void looseStonesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStonesRpt frm = new frmStonesRpt();
            frm.ShowDialog();
        }

        private void stonesPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLooseStonesPurchase frm = new frmLooseStonesPurchase();
            frm.ShowDialog();
        }

        private void barCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BarCodeReports frm = new BarCodeReports();
            frm.ShowDialog();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            VeiwPictures view = new VeiwPictures();
            view.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            AddCountry reload = new AddCountry();
            reload.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            AddCity reload = new AddCity();
            reload.ShowDialog();
        }

        private void orderReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderReports frm = new OrderReports();
            frm.ShowDialog();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            ManageBank reload = new ManageBank();
            reload.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            ManageBankAccount reload = new ManageBankAccount();
            reload.ShowDialog();
        }

        private void balanceReceiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BalanceReceive frm = new BalanceReceive();
            frm.ShowDialog();
        }

        private void profitLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfitLossReports frm = new frmProfitLossReports();
            frm.ShowDialog();
        }

        private void createUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateUser cu = new CreateUser();
            cu.ShowDialog();
        }

        private void loginToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
            str = new UserRights().GetRightsByUser();
            this.MenuItemsOff();
            Buttons(0);
            if (Login.userId != 0 && str == "Administrator")
            {
                this.MenuItemsOn();
            }
            else if (Login.userId != 0 && str == "Limited")
            {
                this.MenuItemsOnForLimitedUser();
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MenuItemsOff();
        }

        private void MenuItemsOn()
        {
            for (int h = 0; h < menuStrip1.Items.Count; h++)
            {
                menuStrip1.Items[h].Enabled = true;
                foreach (ToolStripMenuItem toolItem in ((ToolStripMenuItem)menuStrip1.Items[h]).DropDownItems)
                {
                    toolItem.Enabled = true;
                }
            }
            this.loginToolStripMenuItem1.Enabled = false;
        }

        private void MenuItemsOff()
        {
            this.DisableMenuItems();
            mainMen = new List<string>();
            mainMen.Add("&Login");
            subMen = new List<string>();
            subMen.Add("loginToolStripMenuItem1");
            Buttons(1);
            this.ItemsOnByUser(mainMen, subMen);
        }

        private void MenuItemsOnForLimitedUser()
        {
            for (int h = 0; h < menuStrip1.Items.Count; h++)
            {
                menuStrip1.Items[h].Enabled = true;
                foreach (ToolStripMenuItem toolItem in ((ToolStripMenuItem)menuStrip1.Items[h]).DropDownItems)
                {
                    toolItem.Enabled = true;
                }
            }
            this.loginToolStripMenuItem.Enabled = false;
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            frmStockCheck reload = new frmStockCheck();
            reload.ShowDialog();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            bk.RunWorkerAsync();
        }

        private void accountsReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountReports frm = new AccountReports();
            frm.ShowDialog();
        }

        private void addSampleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddSample adsmp = new AddSample();
            adsmp.ShowDialog();
        }

        private void sampleReturnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SampleReturn smr = new SampleReturn();
            smr.ShowDialog();
        }

        private void repairingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmRepairing reload = new frmRepairing();
            reload.ShowDialog();
        }

        private void cashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashReceiptVoucher frm = new CashReceiptVoucher();
            frm.ShowDialog();
        }

        private void cashPaymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CashPaymentVoucher frm = new CashPaymentVoucher();
            frm.ShowDialog();
        }

        private void bankReceiptVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankReceiptVoucher reload = new BankReceiptVoucher();
            reload.ShowDialog();
        }

        private void bankPaymentVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BankPaymentVoucher frm = new BankPaymentVoucher();
            frm.ShowDialog();
        }

        private void journalVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JournalVoucher frm = new JournalVoucher();
            frm.ShowDialog();
        }

        private void saleReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleReports reload = new SaleReports();
            reload.ShowDialog();
        }

        private void tagHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTagHistory frm = new frmTagHistory();
            frm.ShowDialog();
        }

        private void damageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DamageReports frm = new DamageReports();
            frm.ShowDialog();
        }

        private void deletedItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeletedItemsReports frm = new frmDeletedItemsReports();
            frm.ShowDialog();
        }

        private void backUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackUp();
            this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.ShowDialog();
        }

        private void lblTimer_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.jewelmanager.com");
            }
            catch { }
        }

        private void silverSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageSilverSale frm = new ManageSilverSale();
            frm.ShowDialog();
        }

        private void Main_ControlAdded(object sender, ControlEventArgs e)
        {
            Form childform = e.Control as Form;
            string str = childform.Name;
        }

        private void BackUp()
        {
            UtilityDAL ud = new UtilityDAL();
            string activeDir = @"" + statp.BackUpPath + "";
            //Create a new subfolder under the current active folder
            string newPath = System.IO.Path.Combine(activeDir, "JewleryDataBaseBackUp");
            string str = DateTime.Now.ToString("dd-MMM-yyyy-hh-mm-ss");
            System.IO.Directory.CreateDirectory(newPath);
            string[] fileNames = System.IO.Directory.GetFiles(newPath, @"*.bak");
            string backupPath = newPath + "\\DBBackUp" + str + ".bak";
            string backupPath1 = newPath + "\\DBBackUpPics" + str + ".bak";
            ud.MakeBackup(backupPath, backupPath1);
            foreach (string file in fileNames)
            {
                System.Diagnostics.Debug.WriteLine(file + "will be deleted");
                System.IO.File.Delete(file);
            }
        }

        private void RefreshDB()
        {
            UtilityDAL ud = new UtilityDAL();
            ud.RefreshDatabase();
        }

        private void saleManAttendenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaleManAttendance frm = new SaleManAttendance();
            frm.ShowDialog();
        }

        private void saleManAlownceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesManAdvance frm = new SalesManAdvance();
            frm.ShowDialog();
        }

        private void saleManSalaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalesManSalary frm = new SalesManSalary();
            frm.ShowDialog();
        }

        private void addSaleManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageSalesMan frm = new ManageSalesMan();
            frm.ShowDialog();
        }

        private void salaryIncrementDecrementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SalaryIncrOrDecr frm = new SalaryIncrOrDecr();
            frm.ShowDialog();
        }

        private void saleManToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleManReport frmslm = new frmSaleManReport();
            frmslm.Show();
        }

        private void seToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendSMS sms = new SendSMS();
            sms.ShowDialog();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCustomerRpt cust = new frmCustomerRpt();
            cust.Show();
        }

        private void goldRatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoldRatesReports rate = new GoldRatesReports();
            rate.Show();
        }

        private void splitSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SplitSaleReports split = new SplitSaleReports();
            split.ShowDialog();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshDB();
        }

        private void addSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSupplier frm = new AddSupplier();
            frm.Show();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            BarCodeReports frm = new BarCodeReports();
            frm.ShowDialog();
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            StockReports frm = new StockReports();
            frm.ShowDialog();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            ManageStock frm = new ManageStock();
            frm.ShowDialog();
        }

        private void bk_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripProgressBar1.Value = e.ProgressPercentage;
            this.toolStripStatusLabel5.Text = string.Format("Backup is Completed about {0} %", e.ProgressPercentage.ToString());
        }

        private void bk_DoWork(object sender, DoWorkEventArgs e)
        {
            this.BackUp();
        }

        private void bk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Backup is Cancelled.", Messages.Header);
            }
            else if (e.Error != null)
            {
                MessageBox.Show("There is an Error While taking Bakeup of Your Database.", Messages.Header);
            }
            else
                Application.Exit();
        }

        public void ItemsOnByUser(List<string> mainMenu, List<string> subMenu)
        {
            for (int i = 0; i < mainMenu.Count; i++)
            {
                for (int h = 0; h < menuStrip1.Items.Count; h++)
                {
                    if (menuStrip1.Items[h].ToString() == mainMenu[i].ToString())
                        menuStrip1.Items[h].Enabled = true;
                    foreach (string lst in subMenu)
                    {
                        foreach (ToolStripMenuItem toolItem in ((ToolStripMenuItem)menuStrip1.Items[h]).DropDownItems)
                        {
                            if (toolItem.Name == lst)
                                toolItem.Enabled = true;
                        }
                    }
                }
            }
        }

        public void DisableMenuItems()
        {
            for (int h = 0; h < menuStrip1.Items.Count; h++)
            {
                menuStrip1.Items[h].Enabled = false;
                foreach (ToolStripMenuItem toolItem in ((ToolStripMenuItem)menuStrip1.Items[h]).DropDownItems)
                {
                    toolItem.Enabled = false;
                }
            }
        }

        void Buttons(int a)
        {
            if (a == 0)
            {
                btnPurchase.Enabled = true;
                btnStock.Enabled = true;
                btnSale.Enabled = true;
            }
            if (a == 1)
            {
                btnPurchase.Enabled = false;
                btnStock.Enabled = false;
                btnSale.Enabled = false;
            }
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ManageOrder orE = new ManageOrder();
            orE.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ManageSale frm = new ManageSale();
            frm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ManageOrder frm = new ManageOrder();
            frm.ShowDialog();
        }

        private void startupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Password p = new Password();
            p.ShowDialog();
        }

        private void setBackupPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackUpPath bp = new BackUpPath();
            bp.ShowDialog();
            statp = stkDAL.GetStartUp();
        }

        private void editReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Return edit = new Edit_Return();
            edit.ShowDialog();
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPassword.Checked == true)
            {
                ReportsPassword rp = new ReportsPassword();
                rp.ShowDialog();
                if (rp.id == 1)
                {
                    reportsToolStripMenuItem.Visible = true;
                }
            }
            if (this.chkPassword.Checked == false)
            {
                reportsToolStripMenuItem.Visible = false;
            }
        }

        private void txt24KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl24KarratGram.Text = this.txt24KarratGram.Text;
        }

        private void txt23KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl23KarratGram.Text = this.txt23KarratGram.Text;
        }

        private void txt22KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl22KarratGram.Text = this.txt22KarratGram.Text;
        }

        private void txt21KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl21KarratGram.Text = this.txt21KarratGram.Text;
        }

        private void txt20KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl20KarratGram.Text = this.txt20KarratGram.Text;
        }

        private void txt19KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl19KarratGram.Text = this.txt19KarratGram.Text;
        }

        private void txt18KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl18KarratGram.Text = this.txt18KarratGram.Text;
        }

        private void txt17KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl17KarratGram.Text = this.txt17KarratGram.Text;
        }

        private void txt16KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl16KarratGram.Text = this.txt16KarratGram.Text;
        }

        private void txt15KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl15KarratGram.Text = this.txt15KarratGram.Text;
        }

        private void txt14KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl14KarratGram.Text = this.txt14KarratGram.Text;
        }

        private void txt13KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl13KarratGram.Text = this.txt13KarratGram.Text;
        }

        private void txt13KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl13KarratTola.Text = this.txt13KarratTola.Text;
        }

        private void txt14KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl14KarratTola.Text = this.txt14KarratTola.Text;
        }

        private void txt15KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl15KarratTola.Text = this.txt15KarratTola.Text;
        }

        private void txt16KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl16KarratTola.Text = this.txt16KarratTola.Text;
        }

        private void txt17KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl17KarratTola.Text = this.txt17KarratTola.Text;
        }

        private void txt18KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl18KarratTola.Text = this.txt18KarratTola.Text;
        }

        private void txt19KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl19KarratTola.Text = this.txt19KarratTola.Text;

        }

        private void txt20KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl20KarratTola.Text = this.txt20KarratTola.Text;

        }

        private void txt21KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl21KarratTola.Text = this.txt21KarratTola.Text;
        }

        private void txt22KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl22KarratTola.Text = this.txt22KarratTola.Text;
        }

        private void txt23KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl23KarratTola.Text = this.txt23KarratTola.Text;
        }

        private void txt24KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl24KarratTola.Text = this.txt24KarratTola.Text;
        }

        private void txt12KarratTola_TextChanged(object sender, EventArgs e)
        {
            this.lbl12KarratTola.Text = this.txt12KarratTola.Text;

        }

        private void txt12KarratGram_TextChanged(object sender, EventArgs e)
        {
            this.lbl12KarratGram.Text = this.txt12KarratGram.Text;

        }

        private void txtPoundPasa_TextChanged(object sender, EventArgs e)
        {
            this.lblPoundPasa.Text = this.txtPoundPasa.Text;
        }

        private void txtSonaPasa_TextChanged(object sender, EventArgs e)
        {
            this.lblSonaPasa.Text = this.txtSonaPasa.Text;
        }

        private void addBulkStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddBulkStock adb = new AddBulkStock();
            adb.ShowDialog();
        }

        private void goldRateToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (City == "Islamabad")
            {
                PasaGoldRate pgr = new PasaGoldRate();
                pgr.ShowDialog();
                pgr.Close();
                DateTime dt = pgr.dt;
                ShowPasaRates(dt);
            }
            else
            {
                GoldRate grs = new GoldRate();
                grs.ShowDialog();
                grs.Close();
                DateTime dt = grs.dt;
                this.ShowRates(dt);
            }
        }

        private void repairingReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRepairingReport frm = new frmRepairingReport();
            frm.ShowDialog();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcontactus frm = new frmcontactus();
            frm.ShowDialog();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.ShowDialog();
            str = new UserRights().GetRightsByUser();
            this.MenuItemsOff();
            Buttons(0);
            if (Login.userId != 0 && str == "Administrator")
            {
                this.MenuItemsOn();
            }
            else if (Login.userId != 0 && str == "Limited")
            {
                this.MenuItemsOnForLimitedUser();
                this.createUserToolStripMenuItem.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm frm1 = new frm();
            frm1.ShowDialog();
        }

        private void stockRDLCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RDLCReport rpt = new RDLCReport();
            rpt.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }
    }
}
