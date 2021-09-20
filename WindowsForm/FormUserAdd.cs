﻿using Business.Abstract;
using Business.Concrete;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class FormUserAdd : Form
    {

        IUserService _userService;
        //BindingList<string> errors = new BindingList<string>();
        //  IAuthService authService = new AuthManager(new UserManager(new EfUserDal()),new JwtHelper());

        public FormUserAdd(IUserService userService)
        {
            InitializeComponent();

            _userService = userService;

        }

        private void ButtonFormUserAddEalveEt_Click(object sender, EventArgs e)
        {
            User user = new User();
            UserForRegisterDto userForRegisterDto = new UserForRegisterDto();
            string passwordRepeat;

            userForRegisterDto.FirstName = TextBoxFormUserAddAd.Text;
            userForRegisterDto.LastName = TextBoxFormUserAddSoyad.Text;
            userForRegisterDto.Email = TextBoxFormUserAddEmail.Text;
            userForRegisterDto.Address = TextBoxFormUserAddAddress.Text;
            userForRegisterDto.PhoneNumber = TextBoxFormUserAddPhoneNumber.Text;
            userForRegisterDto.Password = TextBoxFormUserAddSifre.Text;
            passwordRepeat = TextBoxFormUserAddSifreTekrari.Text;

            user.FirstName = userForRegisterDto.FirstName;
            user.LastName = userForRegisterDto.LastName;
            user.Email = userForRegisterDto.Email;
            user.Address = userForRegisterDto.Address;
            user.PhoneNumber = userForRegisterDto.PhoneNumber;


            UserValidator validationRules = new UserValidator();
            ValidationResult results = validationRules.Validate(user);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    MessageBox.Show(failure.ErrorMessage);
                    return;
                }
                
            }

            var userRegister = _userService.Register(userForRegisterDto, userForRegisterDto.Password, passwordRepeat);


            //var result = _authService.CreateAccessToken(registerResult.Data);
            if (userRegister.Success)
            {
                MessageBox.Show(AuthMessages.UserRegistered, AuthMessages.ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(userRegister.Message, AuthMessages.ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (Control control in this.Controls)
            {
                if (control is TextBox && userRegister.Success)
                {
                    control.Text = "";
                }
            }

        }


    }
}
