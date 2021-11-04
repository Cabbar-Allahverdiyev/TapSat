﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsForm.MyControls;

namespace WindowsForm.Forms
{
    public partial class Dashboard : Form
    {
        private int borderSize = 2;
        private Form activateForm;
        //private Button currentButton;
        private Size formSize;

        public Dashboard()
        {
            InitializeComponent();
            this.Padding = new Padding();
            CollapseMenu();
            this.BackColor = Color.FromArgb(98, 102, 244);


            //myDropdownMenu2.PrimaryColor = Color.SeaGreen;
            //myDropdownMenu2.MenuItemTextColor = Color.SeaGreen;

            //myDropdownMenu1.IsMainMenu = true;
            //myDropdownMenu2.IsMainMenu = true;

            //myDropdownMenu2.PrimaryColor = Color.OrangeRed;
            //myDropdownMenu2.MenuItemTextColor = Color.OrangeRed;
            DisableButton();
            //WM_NCCALCSIZE
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);


        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 10;
            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right
            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>
            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          
                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion
            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }
            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);
                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }

            base.WndProc(ref m);
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    this.Padding = new Padding(0, 8, 8, 0);
                    break;
                case FormWindowState.Normal:
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);

                    break;
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    //bura yeiden bax
                    previousBtn.BackColor = Color.FromArgb(98, 102, 244);
                    //previousBtn.BackColor = Color.FromArgb(152, 161, 155);
                    // previousBtn.BackColor = Color.FromArgb(152, 158, 161);
                   // previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.ForeColor = Color.White;
                    previousBtn.Font = new Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                }
            }
        }

        //<Exit Maximized Minimized Buttons>----------------------------->

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonMaximized_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void buttonMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }



        //Menu Items button------------------------------------->
        private void buttonMenu_Click(object sender, EventArgs e)
        {
            CollapseMenu();
        }

        private void buttonHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SalesForm(), sender);
        }

        private void buttonProducts_Click(object sender, EventArgs e)
        {
            Open_DropdownMenu(myDMProduct, sender);
           // myDMProduct.Show(buttonProducts, buttonProducts.Width, 0);
            
        }


        private void buttonSales_Click(object sender, EventArgs e)
        {
            Open_DropdownMenu(myDMSales, sender);
            // myDMSales.Show(buttonSales, myDMSales.Width - buttonProducts.Width, buttonSales.Height);

           
        }

        private void buttonCategories_Click(object sender, EventArgs e)
        {
            Open_DropdownMenu(myDMCategory, sender);
        }

        private void buttonScan_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonStatistic_Click(object sender, EventArgs e)
        {
            Open_DropdownMenu(myDMStatistic, sender);
        }

        private void buttonUsers_Click(object sender, EventArgs e)
        {
            Open_DropdownMenu(myDMUser, sender);
        }

        private void buttonSuppliers_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        //Dropdown menu---------------------------------->
        //Product--------------------------------------->
        private void məhsulSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductDeleteForm(), sender);
        }

        private void məhsullarıSıralaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProductList(), sender);
        }

        private void məhsulYeniləToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ProductUpdateForm(), sender);
        }

        private void məhsulƏlavəEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormProductAdd(), sender);
        }

        private void markalarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBrand(), sender);
        }

        //Sales-------------------------------------------->
        private void satislariSiralaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormSalesList(), sender);
        }

        //Istifadeci--------------------------------------------->
        private void istifadəçiləriSıralaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormUserListed(), sender);
        }

        private void istifadəçiƏlavəEtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormUserAdd(), sender);
        }

        private void istifadəçiləriYeniləToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserUpdateForm(), sender);
        }

        private void istifadəçiSilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new UserDeleteForm(), sender);
        }

        //Categories------------------------------------------>

        private void kateqoriyalariSiralaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormCategory(), sender);
        }


        //Scan------------------------------------------------->


        //Statistic--------------------------------------------->
        private void günlükToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenChildForm(new(), sender);
        }

        private void həftəlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // OpenChildForm(new(), sender);
        }

        private void aylıqToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // OpenChildForm(new(), sender);
        }

        private void illikToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // OpenChildForm(new(), sender);
        }




        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) //Collapse Menu
            {
                panelMenu.Width = 100;
                pictureBoxLogo.Visible = false;
                buttonMenu.Dock = DockStyle.Top;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else //Expand Menu
            {
                panelMenu.Width = 230;
                pictureBoxLogo.Visible = true;
                buttonMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "  " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }

        }

        private void Open_DropdownMenu(MyDropdownMenu dropdownMenu, object sender)
        {
            Control control = (Control)sender;
            dropdownMenu.VisibleChanged += new EventHandler((sender2, ev)
                => DropdownMenu_VisibleChanged(sender2, ev, control));
            dropdownMenu.Show(control, control.Width, 0);
        }

        private void Open_DropdownMenu2(MyDropdownMenu dropdownMenu, object sender)
        {
            Control control = (Control)sender;
            dropdownMenu.VisibleChanged += new EventHandler((sender2, ev)
                => DropdownMenu_VisibleChanged(sender2, ev, control));
            dropdownMenu.Show(control, control.Width - dropdownMenu.Width, control.Height);
        }

        private void DropdownMenu_VisibleChanged(object sender, EventArgs ev, Control ctrl)
        {
            MyDropdownMenu dropdownMenu = (MyDropdownMenu)sender;
            if (!DesignMode)
            {
                if (dropdownMenu.Visible)
                    ctrl.BackColor = Color.FromArgb(159, 161, 224);
                else ctrl.BackColor = Color.FromArgb(98, 102, 244);

                //if (dropdownMenu.Visible)
                //    ctrl.BackColor = Color.FromArgb(72, 52, 182);
                //else ctrl.BackColor = Color.FromArgb(98, 102, 244);
                //ctrl.BackColor = Color.FromArgb(72, 52, 182);
                //else ctrl.BackColor = Color.FromArgb(24, 24, 36);
            }
        }

      

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            labelTitle.Text = childForm.Text;
        }

      






        //private void ActivateButton(object btnSender)
        //{
        //    if (btnSender != null)
        //    {
        //        if (currentButton.Equals((MyDropdownMenu)btnSender))
        //        {
        //            DisableButton();
        //            //Color color = SelectThemeColor();
        //            currentButton = (Button)btnSender;
        //            //currentButton.BackColor = color;
        //           // currentButton.ForeColor = Color.White;
        //           // currentButton.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        //            //panelTitleBar.BackColor = color;
        //           // panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
        //            //ThemeColor.PrimaryColor = color;
        //            //ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
        //            buttonCloseChildForm.Visible = true;

        //        }
        //    }
        //}






    }
}