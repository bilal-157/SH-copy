using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BusinesEntities;
using System.Threading;

namespace jewl
{
    public static class FormControls
    {
        public static MyColors myColor = new MyColors();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // x-coordinate of upper-left corner
            int nTopRect, // y-coordinate of upper-left corner
            int nRightRect, // x-coordinate of lower-right corner
            int nBottomRect, // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        public static void KeyFocus(object sender, KeyEventArgs e, TextBox tx)
        {
            if (e.KeyCode == Keys.Enter)
                tx.Select();
        }

        public static void KeyFocus(object sender, KeyEventArgs e, ComboBox tx)
        {
            if (e.KeyCode == Keys.Enter)
                tx.Select();
        }

        public static String StringFormate(string str)
        {
            if (str == String.Empty)
                return "0";
            else
                return str;
        }

        public static void KeyFocus(object sender, KeyEventArgs e, RadioButton tx)
        {
            if (e.KeyCode == Keys.Enter)
                tx.Select();
        }

        public static void KeyFocus(object sender, KeyEventArgs e, Button tx)
        {
            if (e.KeyCode == Keys.Enter)
                tx.Select();
        }

        public static void KeyFocus(object sender, KeyEventArgs e, ToolStripButton tx)
        {
            if (e.KeyCode == Keys.Enter)
                tx.Select();
        }

        public static void KeyLose(object sender, KeyEventArgs e, CheckBox tx)
        {
            tx.ForeColor = Color.FromArgb(0, 188, 212);
            tx.BackColor = Color.FromArgb(204, 247, 251);
        }

        public static void KeyFocus(object sender, KeyEventArgs e, CheckBox tx)
        {
            if (e.KeyCode == Keys.Enter)
            {
                tx.ForeColor = Color.White;
                tx.BackColor = Color.Green;
                tx.Focus();
            }
        }

        public static void PanelBorder(object sender, PaintEventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel.BorderStyle == BorderStyle.FixedSingle)
            {
                int thickness = myColor.PanelBorder.Radius;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.FromArgb(myColor.PanelBorder.Red, myColor.PanelBorder.Green, myColor.PanelBorder.Blue), thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel.ClientSize.Width - thickness,
                                                              panel.ClientSize.Height - thickness));
                }
            }
        }

        public static void FormBorder(object sender, PaintEventArgs e)
        {
            Form panel = sender as Form;

            if (panel.FormBorderStyle == FormBorderStyle.None)
            {
                int thickness = myColor.FormBorder.Radius;//it's up to you
                int halfThickness = thickness / 2;
                using (Pen p = new Pen(Color.FromArgb(myColor.FormBorder.Red, myColor.FormBorder.Green, myColor.FormBorder.Blue), thickness))
                {
                    e.Graphics.DrawRectangle(p, new Rectangle(halfThickness,
                                                              halfThickness,
                                                              panel.ClientSize.Width - thickness,
                                                              panel.ClientSize.Height - thickness));
                }
            }
        }

        public static System.Drawing.Region GetRoundedRegion(int controlWidth, int controlHeight)
        {
            return System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, controlWidth, controlHeight, 20, 20));
        }

        public static System.Drawing.Region GetRoundedRegionControl(int left, int top, int right, int bottom, int widthElips, int heightElips)
        {
            return System.Drawing.Region.FromHrgn(CreateRoundRectRgn(left, top, right, bottom, widthElips, heightElips));
        }

        public static void validate_box(TextBox textbox, CancelEventArgs e, ErrorProvider error, string fieldName)
        {
            if (string.IsNullOrEmpty(textbox.Text))
            {                
                textbox.Focus();
                error.SetError(textbox, fieldName + Messages.Empty);
            }
            else
                error.SetError(textbox, "");
        }

        public static int GetColor(string hexValue)
        {
            int color = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            return color;
        }

        public static void GetAllControls(Control container)
        {
            if (container is Form)
            {
                if (((Form)container).Name != "AccountDetail" && ((Form)container).Name != "StockSearch" && ((Form)container).Name != "StockReportsPics" && ((Form)container).Name != "BarCodeReports")
                {
                    ((Form)container).BackColor = Color.FromArgb(myColor.FormBackground.Red, myColor.FormBackground.Green, myColor.FormBackground.Blue);
                    ((Form)container).WindowState = FormWindowState.Normal;
                    ((Form)container).StartPosition = FormStartPosition.CenterScreen;
                }
                else
                    ((Form)container).BackColor = Color.FromArgb(myColor.FormBackground.Red, myColor.FormBackground.Green, myColor.FormBackground.Blue);
            }
            foreach (Control c in container.Controls)
            {
                GetAllControls(c);
                if (c is TextBox)
                {
                    c.BackColor = Color.FromArgb(myColor.TextBoxBackground.Red, myColor.TextBoxBackground.Green, myColor.TextBoxBackground.Blue);
                    ((TextBox)c).BorderStyle = BorderStyle.FixedSingle;
                    ((TextBox)c).Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    if (((TextBox)c).Name == "txtPassword" || ((TextBox)c).Name == "txtUserName" || ((TextBox)c).Name == "txtCHW" || ((TextBox)c).Name.Contains("Description"))
                    {
                        if (((TextBox)c).ScrollBars == ScrollBars.Vertical)
                            c.BackColor = Color.FromArgb(myColor.TextBoxBackground.Red, myColor.TextBoxBackground.Green, myColor.TextBoxBackground.Blue);
                    }
                    else 
                    {
                        ((TextBox)c).Multiline = false;
                        if (((TextBox)c).ScrollBars == ScrollBars.Vertical)
                            c.BackColor = Color.FromArgb(myColor.TextBoxBackground.Red, myColor.TextBoxBackground.Green, myColor.TextBoxBackground.Blue);
                    }
                }
                else if (c is ToolStrip)
                {
                    c.BackColor = Color.LightBlue;
                }
                else if (c is Label)
                {
                    if (((Label)c).Name == "lblTitle")
                    {
                        ((Label)c).AutoSize = false;
                        ((Label)c).Image = null;
                        ((Label)c).Padding = new Padding(0, 0, 45, 12);
                        ((Label)c).Font = new Font("Segoe UI", 18, FontStyle.Bold);
                        ((Label)c).TextAlign = ContentAlignment.TopCenter;
                    }
                    else if (((Label)c).Size.Width > 170)
                    {
                        ((Label)c).Font = new Font("Segoe UI", (float)10.5, FontStyle.Bold);
                    }
                    else if (((Label)c).Size.Width < 15)
                    {
                        ((Label)c).Font = new Font("Segoe UI", (float)8, FontStyle.Bold);
                    }
                    else if (((Label)c).Size.Height > 30)
                    {
                        ((Label)c).Font = new Font("Segoe UI", (float)20, FontStyle.Bold);
                    }
                    else 
                    {
                        ((Label)c).BorderStyle = BorderStyle.None;
                        ((Label)c).Height = 19;
                        ((Label)c).ForeColor = Color.Black;
                        if (((Label)c).Size.Width < 170)
                        {
                            ((Label)c).Font = new Font("Segoe UI", (float)myColor.SimpleLabel.FontSize, FontStyle.Bold);                           
                        }
                    }
                    ((Label)c).Font = new Font("Segoe UI", (float)myColor.SimpleLabel.FontSize, FontStyle.Bold);
                    ((Label)c).BackColor = Color.Transparent;
                    ((Label)c).ForeColor = Color.FromArgb(0, 188, 212);
                }
                else if (c is GroupBox) 
                {
                    ((GroupBox)c).ForeColor = Color.FromArgb(0, 188, 212);
                    ((GroupBox)c).Font = new Font("Segoe UI", (float)myColor.SimpleLabel.FontSize, FontStyle.Bold);                
                }
                else if (c is Button)
                {
                    if (((Button)c).Name == "btnReason")
                    {
                        ((Button)c).Size = new Size(33, 23);
                    }
                    else
                    {
                        if (((Button)c).Name == "btnSave")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476843_Save;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnEdit")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476345_edit_notes;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnReport")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.Reports;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnAddRow")
                        {
                            ((Button)c).Image = global::jewl.Properties.Resources.Addrow;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;

                        }
                        else if (((Button)c).Name == "btnRemoveRow")
                        {
                            ((Button)c).Image = global::jewl.Properties.Resources.removeRow;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnFingerPrint")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.FingerPrint1;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;

                        }
                        else if (((Button)c).Name == "btnEnter")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.Tick;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnComplete")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.completed;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnEstimate")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483816206_order;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnUpdate")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476345_edit_notes;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnDelete")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.mydelete;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnReturn")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.ResetChanges_32x32;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnExit")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476602_Cancel;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnClose")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476602_Cancel;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnPrint")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.my_print_16;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnView")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.my_print_16;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnDamage")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.rings;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnReset")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources.reset;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnSearch")
                        {
                            ((Button)c).Size = new Size(100, 35);
                            ((Button)c).Image = global::jewl.Properties.Resources._1483476751_xmag;
                            ((Button)c).ImageAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (((Button)c).Name == "btnPurchase")
                        {
                            ((Button)c).BackColor = Color.DarkMagenta;
                            ((Button)c).Font = new Font("Microsoft Sans Serif", (float)10, FontStyle.Bold);
                        }
                        else if (((Button)c).Name == "btnStock")
                        {
                            ((Button)c).BackColor = Color.DarkSlateBlue;
                            ((Button)c).Font = new Font("Microsoft Sans Serif", (float)10, FontStyle.Bold);
                        }
                        else if (((Button)c).Name == "btnSale")
                        {
                            ((Button)c).BackColor = Color.DarkOrange;
                            ((Button)c).Font = new Font("Microsoft Sans Serif", (float)10, FontStyle.Bold);
                        }
                        else
                        {
                            ((Button)c).Height = 32;
                        }
                        ((Button)c).FlatStyle = FlatStyle.Flat;
                        ((Button)c).FlatAppearance.BorderSize = myColor.BtnColor.BorderSize;
                        ((Button)c).FlatAppearance.BorderColor = Color.FromArgb(myColor.BtnBorderColor.Red, myColor.BtnBorderColor.Green, myColor.BtnBorderColor.Blue);
                        ((Button)c).FlatAppearance.MouseOverBackColor = Color.FromArgb(myColor.BtnHover.Red, myColor.BtnHover.Green, myColor.BtnHover.Blue);
                        ((Button)c).ForeColor = Color.FromArgb(myColor.BtnTextColor.Red, myColor.BtnTextColor.Green, myColor.BtnTextColor.Blue);
                        ((Button)c).Font = new Font("Segoe UI", (float)myColor.BtnTextColor.FontSize);
                        ((Button)c).BackColor = Color.LightBlue;
                    }
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).FlatStyle = FlatStyle.Standard;
                    c.BackColor = Color.FromArgb(myColor.ComboBoxBackColor.Red, myColor.ComboBoxBackColor.Green, myColor.ComboBoxBackColor.Blue);
                    ((ComboBox)c).DropDownStyle = ComboBoxStyle.DropDown;
                    ((ComboBox)c).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    ((ComboBox)c).AutoCompleteSource = AutoCompleteSource.ListItems;
                    ((ComboBox)c).AutoCompleteCustomSource = new AutoCompleteStringCollection();
                }
                else if (c is DataGridView)
                {
                    ((DataGridView)c).BackgroundColor = Color.FromArgb(myColor.GridViewBackground.Red, myColor.GridViewBackground.Green, myColor.GridViewBackground.Blue);
                    ((DataGridView)c).BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).BackColor = Color.FromArgb(myColor.BtnColor.Red, myColor.BtnColor.Green, myColor.BtnColor.Blue);
                    ((RadioButton)c).ForeColor = Color.FromArgb(myColor.BtnTextColor.Red, myColor.BtnTextColor.Green, myColor.BtnTextColor.Blue);
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).BackColor = Color.FromArgb(myColor.BtnColor.Red, myColor.BtnColor.Green, myColor.BtnColor.Blue);
                    ((CheckBox)c).ForeColor = Color.FromArgb(myColor.BtnTextColor.Red, myColor.BtnTextColor.Green, myColor.BtnTextColor.Blue);
                    if (((CheckBox)c).Width>170)
                    {
                         ((CheckBox)c).ForeColor = Color.FromArgb(myColor.Heading.Red, myColor.Heading.Green, myColor.Heading.Blue);
                    }
                }
                else if (c is DateTimePicker)
                {
                    ((DateTimePicker)c).BackColor = Color.FromArgb(myColor.BtnColor.Red, myColor.BtnColor.Green, myColor.BtnColor.Blue);
                    ((DateTimePicker)c).CustomFormat = "dd-MMM-yyyy";
                }
                else if (c is Panel)
                {
                    ((Panel)c).BackColor = Color.FromArgb(myColor.PanelBackGround.Red, myColor.PanelBackGround.Green, myColor.PanelBackGround.Blue);
                    ((Panel)c).BorderStyle = BorderStyle.FixedSingle;
                }
                else if (c is GroupBox)
                {
                    if (((GroupBox)c).Name == "GridGroupBox")
                    {
                        ((GroupBox)c).BackColor = Color.FromArgb(myColor.FormBackground.Red, myColor.FormBackground.Green, myColor.FormBackground.Blue);
                    }
                }
                else if (c is TabControl)
                { }
            }
        }

        public static void CrudActive(ToolStripButton save, ToolStripButton edit, ToolStripButton delete)
        {
            save.Enabled = false;
            edit.Text = "&Update";
            edit.Enabled = true;
            delete.Enabled = true;        
        }
        public static void CrudRefresh(ToolStripButton save, ToolStripButton edit, ToolStripButton delete)
        {
            save.Enabled = true;
            edit.Text = "&Edit";
            edit.Enabled = false;
            delete.Enabled = false;
        }

        public static string N3(string str)
        {
            decimal temp = Convert.ToDecimal(FormControls.StringFormate(str));
            return temp.ToString("N3");
        }

        public static void CheckBox(TextBox t)
        {
            if (t.Text == string.Empty)
            {
                string str = t.Name.Substring(3);
                MessageBox.Show( str + Messages.Empty,Messages.Header);
                t.Focus();
                return;
            }
            else
              return;
        }

        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type)).Concat(controls).Where(c => c.GetType() == type);
        }

        public static void Refresh(IEnumerable<Control> list)
        {
            foreach (var item in list)
            {
                if (item is TextBox)
                {
                    TextBox t = (TextBox)item;
                    t.Text = "";
                }
                else if (item is ComboBox)
                {
                    ComboBox x = (ComboBox)item;
                    x.SelectedIndex = -1;
                }
                else if (item is DataGridView)
                {
                    DataGridView x = (DataGridView)item;
                    x.Rows.Clear();
                }
            }
        }

        public static void IgnoreValidations(IEnumerable<Control> list)
        {
            foreach (var item in list)
            {
                if (item is TextBox)
                {
                    TextBox t = (TextBox)item;
                    t.CausesValidation = false;
                }
            }
        }

        public static void FillCombobox(ComboBox cbx1, object ob, string disName, string valMember)
        {
            cbx1.DataSource = ob;
            cbx1.DisplayMember = disName;
            cbx1.ValueMember = valMember;
            cbx1.SelectedIndex = -1;
        }

        public static void FillCombobox(DataGridViewComboBoxColumn cbx1, object ob, string disName, string valMember)
        {
            cbx1.DataSource = ob;
            cbx1.DisplayMember = disName;
            cbx1.ValueMember = valMember;
        }

        public static decimal GetDecimalValue(TextBox txt, int round)
        {
            if (txt.Text == "")
                return 0;
            else
                return Math.Round(Convert.ToDecimal(txt.Text), round);
        }

        public static decimal GetDecimalValue(Label lbl, int round)
        {
            if (lbl.Text == "")
                return 0;
            else
                return Math.Round(Convert.ToDecimal(lbl.Text), round);
        }

        public static int GetIntValue(TextBox txt)
        {
            if (txt.Text == "")
                return 0;
            else
                return Convert.ToInt32(txt.Text);
        }

        public static void ClearTextBox(Control container)
        {
            foreach (Control c in container.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = string.Empty;
                }
                else if (c is ComboBox)
                {
                    ((ComboBox)c).SelectedIndex = -1;
                }
                else if (c is DataGridView)
                {
                    ((DataGridView)c).Rows.Clear();
                }
            }
        }

        public static void cell_painting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                SolidBrush br = new SolidBrush(Color.FromArgb(myColor.GridViewBackground.Red, myColor.GridViewBackground.Green, myColor.GridViewBackground.Blue));
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
            else
            {
                SolidBrush br = new SolidBrush(Color.FromArgb(myColor.GridViewRow.Red, myColor.GridViewRow.Green, myColor.GridViewRow.Blue));
                e.Graphics.FillRectangle(br, e.CellBounds);
                e.PaintContent(e.ClipBounds);
                e.Handled = true;
            }
        }

        public static void tabPage_DrawItem(TabControl tabPage, object sender, DrawItemEventArgs e)
        {
            switch (e.Index)
            {
                case 0:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 1:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 2:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 3:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 4:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 5:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 6:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
                case 7:
                    e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(myColor.TabActive.Red, myColor.TabActive.Green, myColor.TabActive.Blue)), e.Bounds);
                    break;
            }
            // Then draw the current tab button text 
            Rectangle paddedBounds = e.Bounds;
            paddedBounds.Inflate(-2, -2);
            if (e.Index == tabPage.SelectedIndex)
            {
                e.Graphics.DrawString(tabPage.TabPages[e.Index].Text, new Font(tabPage.Font, FontStyle.Bold), SystemBrushes.HighlightText, paddedBounds);
            }
            else
            {
                e.Graphics.DrawString(tabPage.TabPages[e.Index].Text, tabPage.Font, SystemBrushes.HighlightText, paddedBounds);
            }
        }

        public static void dgv_CellPainting(DataGridView dgv, string column, int colIndex, object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //If there are no articles to download, then hide the button
                if (dgv[dgv.Columns[column].Index, e.RowIndex].Style.ForeColor != Color.Red)
                {

                    if (e.ColumnIndex == colIndex)
                    {
                        dgv[dgv.Columns[column].Index, e.RowIndex].ReadOnly = true;
                        Rectangle newRect = new Rectangle(e.CellBounds.X + 1,
                            e.CellBounds.Y + 1, e.CellBounds.Width - 4,
                            e.CellBounds.Height - 4);

                        using (Brush gridBrush = new SolidBrush(dgv.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor))
                        {
                            using (Pen gridLinePen = new Pen(gridBrush))
                            {
                                // Erase the cell.
                                e.Graphics.FillRectangle(backColorBrush, e.CellBounds);

                                // Draw the grid lines (only the right and bottom lines;
                                // DataGridView takes care of the others).
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left,
                                   e.CellBounds.Bottom - 1, e.CellBounds.Right - 1,
                                   e.CellBounds.Bottom - 1);
                                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1,
                                    e.CellBounds.Top, e.CellBounds.Right - 1,
                                    e.CellBounds.Bottom);

                                e.Handled = true;
                            }
                        }
                    }
                }
            }
        }

        public static void validate_textBox(TextBox _text, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar == 13)
                e.Handled = true;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',')
                e.Handled = true;
            if (e.KeyChar == (char)13)
                e.Handled = true;

            //check if '.' , ',' pressed
            char sepratorChar = 's';
            if (e.KeyChar == '.' || e.KeyChar == ',')
            {
                // check if it's in the beginning of text not accept
                if (_text.Text.Length == 0) 
                    e.Handled = true;
                // check if it's in the beginning of text not accept
                if (_text.SelectionStart == 0) 
                    e.Handled = true;
                // check if there is already exist a '.' , ','
                if (alreadyExist(_text.Text, ref sepratorChar)) 
                    e.Handled = true;
                //check if '.' or ',' is in middle of a number and after it is not a number greater than 99
                if (_text.SelectionStart != _text.Text.Length && e.Handled == false)
                {
                    // '.' or ',' is in the middle
                    string AfterDotString = _text.Text.Substring(_text.SelectionStart);

                    if (AfterDotString.Length > 2)
                    {
                        e.Handled = true;
                    }
                }
            }
            //check if a number pressed

            if (Char.IsDigit(e.KeyChar))
            {
                //check if a coma or dot exist
                if (alreadyExist(_text.Text, ref sepratorChar))
                {
                    int sepratorPosition = _text.Text.IndexOf(sepratorChar);
                    string afterSepratorString = _text.Text.Substring(sepratorPosition + 1);
                }
            }
        }
        private static bool alreadyExist(string _text, ref char KeyChar)
        {
            if (_text.IndexOf('.') > -1)
            {
                KeyChar = '.';
                return true;
            }
            if (_text.IndexOf(',') > -1)
            {
                KeyChar = ',';
                return true;
            }
            return false;
        }

        /// <summary>
        /// shows window with fade in effect
        /// </summary>
        public static void FadeIn(Form f)
        {
            while (f.Opacity < 1)
            {
                Thread.Sleep(100);
                f.Opacity += 0.50;
            }
            f.Opacity = 1;
        }

        /// <summary>
        /// closes window with fade out effect
        /// </summary>
        public static void FadeOut(Form f)
        {
            while (f.Opacity > 0.0)
            {
                Thread.Sleep(100);
                f.Opacity -= 0.50;
            }
            f.Opacity = 0; //make fully invisible       
        }
    }
}
