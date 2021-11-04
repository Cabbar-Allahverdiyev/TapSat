﻿using Business.Abstract;
using Business.Concrete;
using Business.Constants.Messages;
using Business.ValidationRules;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Core.Controllers;

namespace WindowsForm.Forms
{
    public partial class SalesForm : Form
    {
        public SalesForm()
        {
            InitializeComponent();
            TotalPriceLabelWrite();
        }

        ProductManager _productManager = new ProductManager(new EfProductDal());
        CartManager _cartManager = new CartManager(new EfCartDal());
        SaleWinFormManager _saleWinFormManager = new SaleWinFormManager(new EfSaleWinFormDal());
        ResultControllersMessageList resultControllersMessageList = new ResultControllersMessageList();

        bool isBarcodeNumberExists = false;


        private void SalesForm_Load(object sender, EventArgs e)
        {
            ProductListRefesh();
            CartListRefesh();
            GroupBoxMehsulControlClear();
        }



        private void ButtonSalesFormYenile_Click(object sender, EventArgs e)
        {
            ProductListRefesh();
            CartListRefesh();
        }

        private void ButtonX_Click(object sender, EventArgs e)
        {
            RemoveCart();
            Application.Exit();
        }

        private void ButtonSalesFormSil_Click(object sender, EventArgs e)
        {

            try
            {
                Cart cart = new Cart();
                CartAddDto cartAddDto = _cartManager.GetCartAddDetailByBarcodeNumber(int.Parse(textBoxBarkodNo.Text)).Data;

                if (cartAddDto!=null)
                {
                    cart.Id = cartAddDto.CartId;
                    if (cartAddDto.Quantity <= 1 || textBoxMiqdar.Text == "" || cartAddDto.Quantity <= Convert.ToInt32(textBoxMiqdar.Text))
                    {
                        IResult result = _cartManager.Delete(cart);
                        if (!result.Success)
                        {
                            ResultControllers.ResultIsSucces(result);
                            return;
                        }

                    }
                    else
                    {
                        cart.ProductId = int.Parse(textBoxProductId.Text);
                        cart.UserId = cartAddDto.UserId;
                        cart.Quantity = cartAddDto.Quantity - Convert.ToInt32(textBoxMiqdar.Text);
                        cart.SoldPrice = cartAddDto.SoldPrice;
                        CalculateTotalPrice(cart.Quantity, cart.SoldPrice);
                        cart.TotalPrice = Convert.ToDecimal(textBoxCem.Text);
                        //  CartValidation(cart);
                        IResult result = _cartManager.Update(cart);
                        if (!result.Success)
                        {
                            ResultControllers.ResultIsSucces(result);
                            return;
                        }

                    }
                }
               
                GroupBoxMehsulControlClear();
                TotalPriceLabelWrite();
                CartListRefesh();
            }
            catch (ArgumentNullException)
            {
                FormsMessage.ErrorMessage($"{ButtonMessages.SilError} {AuthMessages.ErrorMessage}");
                return;
            }
            catch (FormatException)
            {
                FormsMessage.ErrorMessage($"{ButtonMessages.SilError} {AuthMessages.ErrorMessage}");
                return;
            }
            catch (NullReferenceException)
            {
                FormsMessage.ErrorMessage($"{ButtonMessages.SilError} {AuthMessages.ErrorMessage}");
                return;
            }


        }

        private void ButonSalesFormSatisIptal_Click(object sender, EventArgs e)
        {

            RemoveCart();
            CartListRefesh();
            TotalPriceLabelWrite();
            GroupBoxMehsulControlClear();
            FormsMessage.InformationMessage(SaleMessages.SaleCancel);


        }

        private void DataGridViewSalesForm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GroupBoxMehsulControlClear();
            textBoxProductId.Text = dataGridViewProductList.CurrentRow.Cells["ProductId"].Value.ToString();
            textBoxBarkodNo.Text = dataGridViewProductList.CurrentRow.Cells["BarcodeNomresi"].Value.ToString();
            textBoxMehsulAdi.Text = dataGridViewProductList.CurrentRow.Cells["MehsulAdi"].Value.ToString();
            textBoxMaxQiymet.Text = dataGridViewProductList.CurrentRow.Cells["Qiymet"].Value.ToString();
        }

        private void buttoElaveEt_Click(object sender, EventArgs e)
        {
            try
            {
                Cart cart = new Cart();
                IResult cartUpdated;
                IResult cartAdded;
                if (textBoxProductId.Text == "")
                {
                    FormsMessage.ErrorMessage(ProductMessages.SureFillInFields);
                    return;
                }
                cart.ProductId = int.Parse(textBoxProductId.Text);
                cart.SoldPrice = decimal.Parse(textBoxQiymet.Text);
                cart.Quantity = int.Parse(textBoxMiqdar.Text);
                cart.TotalPrice = decimal.Parse(textBoxCem.Text);
                cart.UserId = 2;

                CartValidator validationRules = new CartValidator();
                ValidationResult results = validationRules.Validate(cart);
                if (!results.IsValid)
                {
                    foreach (ValidationFailure validationFailure in results.Errors)
                    {
                        FormsMessage.ErrorMessage(validationFailure.ErrorMessage);
                        return;
                    }
                }
                // CartValidation(cart);

                IsBarcodeNumberExists();
                if (isBarcodeNumberExists == true)
                {
                    CartAddDto cartAddDto = _cartManager.GetCartAddDetailByBarcodeNumber(int.Parse(textBoxBarkodNo.Text)).Data;
                    cart.Id = cartAddDto.CartId;
                    cart.Quantity = cartAddDto.Quantity + int.Parse(textBoxMiqdar.Text);
                    cart.SoldPrice = decimal.Parse(textBoxQiymet.Text);
                    cart.TotalPrice = cart.SoldPrice * cart.Quantity;
                    cartUpdated = _cartManager.Update(cart);
                    if (!cartUpdated.Success)
                    {
                        ResultControllers.ResultIsSucces(cartUpdated);
                        return;
                    }
                    FormsMessage.InformationMessage(cartUpdated.Message);
                }
                else
                {
                    cartAdded = _cartManager.Add(cart);
                    if (!cartAdded.Success)
                    {
                        ResultControllers.ResultIsSucces(cartAdded);
                        return;
                    }
                }

                CartListRefesh();
                TotalPriceLabelWrite();
                GroupBoxMehsulControlClear();
            }
            catch (Exception)
            {

                FormsMessage.ErrorMessage($"{ButtonMessages.ElaveEtError} {AuthMessages.ErrorMessage}");
                return;
            }

        }


        private void ButtonSalesFormSatisEtmek_Click(object sender, EventArgs e)
        {
            try
            {
                SaleWinForm saleWinForm = new SaleWinForm();
                IDataResult<List<Cart>> carts = _cartManager.GetAllByUserId(2);
                IResult saleWinFormAdded;
                IResult productUpdated;
                List<string> messages = new List<string>();
                string resultMessage = "";
                string newResultMessage = "";
                if (carts.Data.Count != 0)
                {
                    foreach (Cart cart in carts.Data)
                    {

                        Product product = _productManager.GetByProductId(cart.ProductId).Data;
                        product.UnitsOnOrder += cart.Quantity;
                        saleWinForm.Id = 0;
                        saleWinForm.ProductId = cart.ProductId;
                        saleWinForm.UserId = cart.UserId;
                        saleWinForm.SoldPrice = cart.SoldPrice;
                        saleWinForm.Quantity = cart.Quantity;

                        SaleWinFormValidator validationRules = new SaleWinFormValidator();
                        ValidationResult results = validationRules.Validate(saleWinForm);
                        if (!results.IsValid)
                        {
                            foreach (ValidationFailure validationFailure in results.Errors)
                            {
                                FormsMessage.ErrorMessage(validationFailure.ErrorMessage);
                                return;
                            }
                        }
                        //SaleWinFormValidation(saleWinForm);
                        saleWinFormAdded = _saleWinFormManager.Add(saleWinForm);
                        productUpdated = _productManager.Update(product);
                        messages.Add(product.ProductName + resultControllersMessageList.ResultIsSuccesMessage(saleWinFormAdded));
                        messages.Add(product.ProductName + resultControllersMessageList.ResultIsSuccesMessage(productUpdated));

                    }
                    foreach (string message in messages)
                    {
                        resultMessage += $"// {message} {newResultMessage}//       ";

                    }
                    FormsMessage.InformationMessage(resultMessage);


                    //GroupBoxIstifadeciControlClear();
                }
                else
                {
                    FormsMessage.InformationMessage(CartMessages.ProductNotFound);
                    return;
                }
                RemoveCart();
                CartListRefesh();
                ProductListRefesh();
                GroupBoxMehsulControlClear();
                TotalPriceLabelWrite();
            }
            catch (Exception)
            {

                FormsMessage.ErrorMessage($"{ButtonMessages.SatisEtmekError} {AuthMessages.ErrorMessage}");
                return;
            }

        }

        private void textBoxMiqdar_TextChanged(object sender, EventArgs e)
        {
            CemWrite();

        }

        private void textBoxQiymet_TextChanged(object sender, EventArgs e)
        {
            CemWrite();


        }

        private void dataGridViewCartList_DoubleClick(object sender, EventArgs e)
        {
            GroupBoxMehsulControlClear();

            textBoxProductId.Text = dataGridViewCartList.CurrentRow.Cells["ProductId"].Value.ToString();

            CartDto cartDto = _cartManager.GetCartDtoDetailByProductId(Convert.ToInt32(textBoxProductId.Text)).Data;
            textBoxBarkodNo.Text = cartDto.BarcodeNumber.ToString();
            textBoxMehsulAdi.Text = cartDto.ProductName.ToString();
            textBoxMaxQiymet.Text = cartDto.UnitPrice.ToString();

            textBoxQiymet.Text = dataGridViewCartList.CurrentRow.Cells["Qiymet"].Value.ToString();
            textBoxMiqdar.Text = dataGridViewCartList.CurrentRow.Cells["Miqdar"].Value.ToString();
            textBoxCem.Text = dataGridViewCartList.CurrentRow.Cells["Cem"].Value.ToString();
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            GroupBoxMehsulControlClear();

        }




        //Elave metodlar--------------------------->
        private void CalculateTotalPrice(int quantity, decimal price)
        {
            if (quantity <= 0 || price <= 0)
            {
                textBoxCem.Text = "";
                return;
            }
            else
            {

                textBoxCem.Text = (price * quantity).ToString();

                return;
            }
        }


        private void GroupBoxMehsulControlClear()
        {
            foreach (Control control in groupBoxMehsul.Controls)
            {
                if (control is TextBox)
                {
                    control.Text = "";
                }

            }
            textBoxMiqdar.Text = "1";
        }

        private void IsBarcodeNumberExists()
        {
            IDataResult<CartAddDto> result = _cartManager.GetCartAddDetailByBarcodeNumber(Convert.ToInt32(textBoxBarkodNo.Text));
            if (result.Success)
            {
                isBarcodeNumberExists = true;
                return;
            }
            isBarcodeNumberExists = false;
            return;
        }

        private void CartValidation(Cart cart)
        {
            CartValidator validationRules = new CartValidator();
            ValidationResult results = validationRules.Validate(cart);
            if (!results.IsValid)
            {
                foreach (ValidationFailure validationFailure in results.Errors)
                {
                    FormsMessage.ErrorMessage(validationFailure.ErrorMessage);
                    return;
                }
            }
        }

        private void SaleWinFormValidation(SaleWinForm saleWinForm)
        {
            SaleWinFormValidator validationRules = new SaleWinFormValidator();
            ValidationResult results = validationRules.Validate(saleWinForm);
            if (!results.IsValid)
            {
                foreach (ValidationFailure validationFailure in results.Errors)
                {
                    MessageBox.Show(validationFailure.ErrorMessage, AuthMessages.ErrorMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void RemoveCart()
        {
            Cart cart = new Cart();
            cart.UserId = 2;    //Mutleq Dinamiklesdir
            _cartManager.ByUserIdAllRemove(cart.UserId);



        }

        private void CartListRefesh()
        {
            dataGridViewCartList.DataSource = _cartManager.GetAllCartViewDetailsByUserId(2).Data;
        }

        private void ProductListRefesh()
        {
            dataGridViewProductList.DataSource = _productManager.GetAllProductViewDasgboardDetails().Data;
        }


        private decimal TotalCartPriceCalculation()
        {
            decimal tolalPrice = 0;
            decimal price;
            List<Cart> carts = _cartManager.GetAll().Data;

            foreach (Cart cart in carts)
            {
                price = cart.TotalPrice;
                tolalPrice += price;
            }
            return tolalPrice;
        }
        private void TotalPriceLabelWrite()
        {
            LabelSalesFormUmuniCem_qiymet.Text = TotalCartPriceCalculation().ToString();
        }

        private void CemWrite()
        {
            try
            {
                CalculateTotalPrice(int.Parse(textBoxMiqdar.Text), decimal.Parse(textBoxQiymet.Text));
            }
            catch (Exception)
            {
                textBoxCem.Text = "";
                return;
            }
        }


    }
}