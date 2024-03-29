﻿using Business.Abstract;
using Business.Concrete;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.FormsAuthorization.User;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Core.Controllers.Concrete;
using WindowsForm.Forms.AdminForms;
using WindowsForm.Forms.UserForms;
using WindowsForm.Utilities.BarcodeScanner.BarcodeGenerate;
using WindowsForm.Utilities.Helpers.Receipts;

namespace WindowsForm.Forms
{
    public partial class LoginForm : Form
    {
        public static int UserId { get; set; }

        IUserService _userService;
        IUserOperationClaimForFormsService _userOperationClaimForFormsService;
        IOperationClaimForFormsService _operationClaimService;
        IProductService _productService;
        IBrandService _brandService;
        ICategoryService _categoryService;
        ICustomerService _customerService;
        ICustomerBalanceService _customerBalanceService;
        ICustomerPaymentService _customerPaymentService;
        ICartService _cartService;
        IDebtService _debtService;
        ISaleService _saleWinFormService;
        ISupplierService _supplierService;
        IBonusCardService _bonusCardService;
        IFormSettingService _formSettingService;
        IBonusCardOperationService _bonusCardOperationService;
        ILoggerService _loggerService;
        private readonly IBarcodeGenerator _barcodeGenerator;
        private readonly IReceiptOperation _receiptOperation;


        MacAddressManager _macAddressManager = new MacAddressManager(new EfMacAddressDal());



        public LoginForm(IUserOperationClaimForFormsService userOperationClaimForFormsService
            , IUserService userService
            , IOperationClaimForFormsService operationClaimService
            , IProductService productService
            , ICategoryService categoryService
            , ICustomerService customerService
            , ICustomerBalanceService customerBalanceService
            , ICustomerPaymentService customerPaymentService
            , ICartService cartService
            , IDebtService debtService
            , ISaleService saleWinFormService
            , ISupplierService supplierService
            , IBrandService brandService
            , IBonusCardService bonusCardService
            , IFormSettingService formSettingService
            , IBonusCardOperationService bonusCardOperationService
            , IBarcodeGenerator barcodeGenerator,
            ILoggerService loggerService,
            IReceiptOperation receiptOperation)
        {
            _userOperationClaimForFormsService = userOperationClaimForFormsService;
            _userService = userService;
            _operationClaimService = operationClaimService;
            _productService = productService;
            _categoryService = categoryService;
            _customerService = customerService;
            _customerBalanceService = customerBalanceService;
            _customerPaymentService = customerPaymentService;
            _cartService = cartService;
            _debtService = debtService;
            _saleWinFormService = saleWinFormService;
            _supplierService = supplierService;
            _brandService = brandService;
            _bonusCardService = bonusCardService;
            _formSettingService = formSettingService;
            _bonusCardOperationService = bonusCardOperationService;
            _barcodeGenerator = barcodeGenerator;
            _receiptOperation = receiptOperation;
            InitializeComponent();
            this.BackColor = Color.FromArgb(41, 128, 185);
            _loggerService = loggerService;
        }

        //Click------------------------------->

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBoxSifre_Click(object sender, EventArgs e)
        {
            ColorChangePassword();
        }

        private void textBoxEmail_Click(object sender, EventArgs e)
        {
            ColorChangeEmail();
        }

        private void buttonDaxilOl_Click(object sender, EventArgs e)
        {
            try
            {
                string thisComputerMacAddress = _macAddressManager.GetAll().Data.FirstOrDefault()==null?"": _macAddressManager.GetAll().Data.FirstOrDefault().Address;
                if (thisComputerMacAddress != GetMacAddress())
                {
                    FormsMessage.ErrorMessage("Əlaqə vasitələri səhifənin aşağısındadır");
                    FormsMessage.ErrorMessage("Zəhmət olmasa istehsalçı ilə əlaqə qurun");
                    FormsMessage.ErrorMessage("Bu kompyuterin icazəsi yoxdur");
                    return;
                }
                if (textBoxEmail.Text == "" || textBoxPassword.Text == "")
                {
                    FormsMessage.WarningMessage(FormsTextMessages.PasswordOrEmailIsBlank);
                    return;
                }
                UserForLoginDto userForLoginDto = new UserForLoginDto();
                userForLoginDto.Email = textBoxEmail.Text;
                userForLoginDto.Password = textBoxPassword.Text;

                IDataResult<User> userToLogin = _userService.Login(userForLoginDto);
                if (!userToLogin.Success)
                {
                    FormsMessage.ErrorMessage(userToLogin.Message);
                    return;
                }

                User user = _userService.GetByMail(userForLoginDto.Email).Data;
                UserId = user.Id;
                IResult getUserClaims = _userService.CheckUserOperationClaimIsBossAndAdminByUserIdForForms(user.Id);
                if (getUserClaims.Success)
                {
                    Dashboard dashboard = new Dashboard(_userService
                                                    , _operationClaimService
                                                    , _userOperationClaimForFormsService
                                                    , _productService
                                                    , _brandService
                                                    , _categoryService
                                                    , _customerService
                                                    , _customerBalanceService
                                                    , _customerPaymentService
                                                    , _cartService
                                                    , _debtService
                                                    , _saleWinFormService
                                                    , _supplierService
                                                    ,_bonusCardService
                                                    ,_formSettingService
                                                    ,_bonusCardOperationService
                                                    , _barcodeGenerator
                                                    ,_loggerService,
                                                    _receiptOperation);
                    this.Hide();
                    dashboard.Show();
                    return;
                }

                UserDashboard userDashboard = new UserDashboard(_userService
                                                                , _operationClaimService
                                                                , _userOperationClaimForFormsService
                                                                , _productService
                                                                , _brandService
                                                                , _categoryService
                                                                , _customerService
                                                                , _customerBalanceService
                                                                , _customerPaymentService
                                                                , _cartService
                                                                , _debtService
                                                                , _saleWinFormService
                                                                , _supplierService
                                                                ,_bonusCardService
                                                                ,_formSettingService
                                                                ,_bonusCardOperationService
                                                                , _barcodeGenerator
                                                                ,_loggerService,
                                                                _receiptOperation);
                this.Hide();
                userDashboard.Show();


            }
            catch (NullReferenceException ex)
            {
                FormsMessage.ErrorMessage("Əlaqə vasitələri səhifənin aşağısındadır");
                FormsMessage.ErrorMessage("Zəhmət olmasa istehsalçı ilə əlaqə qurun");
                FormsMessage.ErrorMessage("Bu kompyuterin icazəsi yoxdur");
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
        }
        //Text Changed------------------------------->

        private void textBoxSifre_TextChanged(object sender, EventArgs e)
        {
            ColorChangePassword();
        }

        private void textBoxEmail_TextChanged(object sender, EventArgs e)
        {
            ColorChangeEmail();
        }

        //Key down----------------------->

        private void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonDaxilOl.PerformClick();
            }
        }
        //Mouse Down ------------------------------->


        private void pictureBoxSifre_MouseDown(object sender, MouseEventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = false;

        }
        //Mouse Up ------------------------------->

        private void pictureBoxSifre_MouseUp(object sender, MouseEventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
        }
        //Mouse Move---------------------------->
        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {
            btnClose.BackColor = Color.FromArgb(255, 0, 21);
        }

        private void panelEmail_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangeEmail();
        }

        private void textBoxEmail_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangeEmail();
        }

        private void panelSifre_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangePassword();
        }

        private void pictureBoxEmail_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangeEmail();
        }

        private void pictureBoxSifre_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangePassword();
        }

        private void textBoxSifre_MouseMove(object sender, MouseEventArgs e)
        {
            ColorChangePassword();
        }

        //Mouse Leave ------------------------------->

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            btnClose.BackColor = SystemColors.Control;
        }

        private void panelEmail_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        private void panelSifre_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        private void pictureBoxEmail_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        private void pictureBoxSifre_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        private void textBoxEmail_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        private void textBoxSifre_MouseLeave(object sender, EventArgs e)
        {
            ColorChangeControl();
        }

        //Resize ------------------------------->
        private void LoginForm_Resize(object sender, EventArgs e)
        {
            FormControllers form = new FormControllers();
            form.AdjustForm(this);
        }


        //Elave Metodlar ------------------------------->
        private void ColorChangeControl()
        {
            textBoxEmail.BackColor = SystemColors.Control;
            panelEmail.BackColor = SystemColors.Control;
            textBoxPassword.BackColor = SystemColors.Control;
            panelSifre.BackColor = SystemColors.Control;
        }

        private void ColorChangeEmail()
        {
            textBoxEmail.BackColor = Color.White;
            panelEmail.BackColor = Color.White;
            textBoxPassword.BackColor = SystemColors.Control;
            panelSifre.BackColor = SystemColors.Control;
        }
        private void ColorChangePassword()
        {
            textBoxPassword.BackColor = Color.White;
            panelSifre.BackColor = Color.White;
            textBoxEmail.BackColor = SystemColors.Control;
            panelEmail.BackColor = SystemColors.Control;
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonDaxilOl.PerformClick();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Process.Start("https://cabar.allahverrdiyev@gmail.com");
            Clipboard.SetText("cabbar.allahverdiyev@gmail.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Clipboard.SetText("+994554926939");
        }

        private string GetMacAddress()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == string.Empty)
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();

                }
            }
            return sMacAddress;
        }

        private void label5_DoubleClick(object sender, EventArgs e)
        {
            UserAuthorization.IsAdminVerified = false;
            UserAuthorization.IsBossVerified = false;
            AdminVerificationForm adminVerificationForm = new AdminVerificationForm(_userService);
            adminVerificationForm.ShowDialog();
            if (UserAuthorization.IsBossVerified ==true)
            {
                MacAddressForm macAddressForm = new MacAddressForm(_userService,
                                                                    _userOperationClaimForFormsService,
                                                                    _formSettingService,
                                                                    _operationClaimService
                                                                    );
                macAddressForm.ShowDialog();
                return;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_DoubleClick(object sender, EventArgs e)
        {

        }
    }
}
