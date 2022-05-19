﻿using Business.Constants.Messages;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.CartDtos;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Core.Controllers.Concrete;
using WindowsForm.Core.Controllers.Concrete.ValidatorControllers;
using USB_Barcode_Scanner;
using WindowsForm.Utilities.Search.Concrete.ProductSearch;
using WindowsForm.MyControls;
using WindowsForm.Core.Constants.SelectionItem;
using WindowsForm.Core.Constants.FormsAuthorization.User;
using Business.Abstract;
using Entities.DTOs.ProductDtos;
using Entities.DTOs.BonusCardDtos;
using Business.ValidationRules.FluentValidation;

namespace WindowsForm.Forms.UserForms
{
    public partial class SalesFormForUser : Form
    {
        int staticUserId = LoginForm.UserId;
        private int CartId { get; set; }
        private int BonusCardId { get; set; }

        //public static bool QrCodeIsSuccess = false;
        IProductService _productService;
        ICartService _cartService;
        ICustomerService _customerService;
        ISaleWinFormService _saleWinFormService;
        IDebtService _debtService;
        ICategoryService _categoryService;
        IBrandService _brandService;
        ISupplierService _supplierService;
        IBonusCardService _bonusCardService;

        MyControl myControl = new MyControl();

        public SalesFormForUser(ICategoryService categoryService
                            , IBrandService brandService
                            , ISupplierService supplierService
                            , IProductService productService
                            , ICartService cartService
                            , ICustomerService customerService
                            , ISaleWinFormService saleWinFormService
                            , IDebtService debtService
                            , IBonusCardService bonusCardService)
        {
            _productService = productService;
            _cartService = cartService;
            _customerService = customerService;
            _saleWinFormService = saleWinFormService;
            _debtService = debtService;

            _categoryService = categoryService;
            _brandService = brandService;
            _supplierService = supplierService;
            _bonusCardService = bonusCardService;

            InitializeComponent();
            TotalPriceLabelWrite();
            UserAuthorization.QrCodeIsSuccess = false;

            myControl.WritePlaceholdersForTextBoxSearch(textBoxAxtar);
            //myControl.WritePlaceholdersForTextBoxSearchByProductName(textBoxAxtarBarcodeNumber);

            BarcodeScanner barcodeScanner = new BarcodeScanner(textBoxBarkodNo);
            barcodeScanner.BarcodeScanned += BarcodeScanner_BarcodeScanned;
            CartId = 0;
            BonusCardId = 0;
            BonusCardSelectForm.BonusCardId = 0;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {
            CategoryWriteComboBox();
            BrandWriteComboBox();
            SupplierWriteComboBox();
            ProductListRefesh();
            CartListRefesh();
            GroupBoxMehsulControlClear();
        }

        //CartValidationTool cartValidationTool = new CartValidationTool();
        //SaleValidationTool saleValidationTool = new SaleValidationTool();
        ProductViewDashboardDetailsSearch detailsSearch = new ProductViewDashboardDetailsSearch();

        private void ButtonX_Click(object sender, EventArgs e)
        {
            RemoveCart();
            Application.Exit();
        }


        private void buttoElaveEt_Click(object sender, EventArgs e)
        {

            try
            {
                bool isBarcodeNumberExists = false;

                if (textBoxProductId.Text == "")
                {
                    FormsMessage.ErrorMessage(ProductMessages.SureFillInFields);
                    return;
                }
                Cart cart = new Cart();
                IResult cartUpdated;
                IResult cartAdded;

                cart.ProductId = int.Parse(textBoxProductId.Text);
                cart.SoldPrice = textBoxQiymet.Text == "" ? decimal.Parse(textBoxMaxQiymet.Text) : decimal.Parse(textBoxQiymet.Text);
                cart.Quantity = int.Parse(textBoxMiqdar.Text);
                cart.TotalPrice = decimal.Parse(textBoxCem.Text);
                cart.UserId = staticUserId;
                if (!FormValidationTool.IsValid(new CartValidator(),cart))
                {
                    return;
                }

                IsBarcodeNumberExists(int.Parse(textBoxProductId.Text), out isBarcodeNumberExists);
                if (isBarcodeNumberExists == true)
                {
                    CartAddDto cartAddDto = _cartService.GetCartAddDetailByProductId(int.Parse(textBoxProductId.Text)).Data;
                    cart.Id = cartAddDto.CartId;
                    cart.Quantity = cartAddDto.Quantity + int.Parse(textBoxMiqdar.Text);
                    cart.SoldPrice = decimal.Parse(textBoxQiymet.Text == "" ? textBoxMaxQiymet.Text : textBoxQiymet.Text);
                    cart.TotalPrice = cart.SoldPrice * cart.Quantity;
                    cartUpdated = _cartService.Update(cart);
                    if (!cartUpdated.Success)
                    {
                        FormsMessage.WarningMessage(cartUpdated.Message);
                        return;
                    }
                    FormsMessage.SuccessMessage(cartUpdated.Message);
                }
                else
                {
                    cartAdded = _cartService.Add(cart);
                    if (!cartAdded.Success)
                    {
                        FormsMessage.WarningMessage(cartAdded.Message);
                        return;
                    }
                    FormsMessage.SuccessMessage(CartMessages.ProductAdded);
                }

                CartListRefesh();
                TotalPriceLabelWrite();
                GroupBoxMehsulControlClear();
            }
            catch (Exception ex)
            {

                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void ButtonSalesFormSil_Click(object sender, EventArgs e)
        {

            try
            {
                UserAuthorization.QrCodeIsSuccess = false;
                AdminValidationForm validationForm = new AdminValidationForm();
                validationForm.ShowDialog();

                if (UserAuthorization.QrCodeIsSuccess == false)
                {
                    FormsMessage.WarningMessage(AuthMessages.AuthorizationDenied);
                    return;
                }
                Cart cart = new Cart();
                CartAddDto cartAddDto = _cartService.GetCartAddDetailByBarcodeNumber(textBoxBarkodNo.Text).Data;

                if (cartAddDto != null)
                {
                    cart.Id = cartAddDto.CartId;
                    if (cartAddDto.Quantity <= 1 || textBoxMiqdar.Text == "" || cartAddDto.Quantity <= Convert.ToInt32(textBoxMiqdar.Text))
                    {
                        IResult result = _cartService.Delete(cart);
                        if (!result.Success)
                        {
                            FormsMessage.ErrorMessage(result.Message);
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
                        IResult result = _cartService.Update(cart);
                        if (!result.Success)
                        {
                            FormsMessage.WarningMessage(result.Message);
                            return;
                        }

                    }
                }

                GroupBoxMehsulControlClear();
                TotalPriceLabelWrite();
                CartListRefesh();
            }
            catch (ArgumentNullException ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex) +
                      " ( Hansısa dəyər boşdur zəhmət olmasa bütün dəyərləri yenidən yoxlayın ) ");
                return;
            }
            catch (FormatException ex)
            {

                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex) +
                     " ( Daxil edilən dəyərlərin hansısa yerləşdiyi xananın formatına uyğun deyil zəhmət olmasa bütün dəyərləri yenidən gözdən keçirin ) ");
                return;
            }
            catch (NullReferenceException ex)
            {

                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex) +
                    " ( Daxil edilən dəyərlərin hansısa yerləşdiyi xananın formatına uyğun deyil və ya referans boşdur zəhmət olmasa bütün dəyərləri yenidən gözdən keçirin ) ");
                return;
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void buttonBorcElaveEt_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxCustomerId.Text == "")
                {
                    FormsMessage.WarningMessage(BaseMessages.SelectValue);
                    return;
                }
                int customerId = int.Parse(textBoxCustomerId.Text);

                Debt debt = new Debt();
                IDataResult<List<Cart>> carts = _cartService.GetAllByUserId(staticUserId);
                IResult debtAdded;
                IResult productUpdated;
                List<string> messages = new List<string>();
                string resultMessage = "";
                string newResultMessage = "";
                if (carts.Data.Count != 0)
                {
                    foreach (Cart cart in carts.Data)
                    {

                        Product product = _productService.GetById(cart.ProductId).Data;
                        product.UnitsInStock -= cart.Quantity;
                        debt.ProductId = cart.ProductId;
                        debt.CustomerId = customerId;
                        debt.SoldPrice = cart.SoldPrice;
                        debt.Quantity = cart.Quantity;

                        debtAdded = _debtService.Add(debt);
                        productUpdated = _productService.Update(product);
                        if (!debtAdded.Success || !productUpdated.Success)
                        {
                            messages.Add(product.BarcodeNumber + " - " + product.ProductName + " : " + debtAdded.Message + " & " + productUpdated.Message);
                            foreach (string message in messages)
                            {
                                resultMessage += $" {message} {newResultMessage} |";
                            }
                            FormsMessage.ErrorMessage(resultMessage);
                            return;
                        }
                        messages.Add(product.ProductName + " : " + debtAdded.Message + " & " + productUpdated.Message);


                    }
                    foreach (string message in messages)
                    {
                        resultMessage += $" {message} {newResultMessage} |";
                    }
                    FormsMessage.SuccessMessage(resultMessage);


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
                GroupBoxMusteriControlClear();
                GroupBoxMehsulControlClear();
                TotalPriceLabelWrite();
            }
            catch (Exception ex)
            {

                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
        }

        private void buttonSec_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerListForm customerListForm = new CustomerListForm(_customerService);
                customerListForm.ShowDialog();
                textBoxCustomerId.Text = SelectedCustomerForSalesForm.Id.ToString();
                IDataResult<Customer> result = _customerService.GetById(SelectedCustomerForSalesForm.Id);
                if (!result.Success)
                {
                    FormsMessage.ErrorMessage(result.Message);
                    return;
                }

                textBoxAd.Text = result.Data.FirstName;
                textBoxSoyad.Text = result.Data.LastName;
                textBoxTelefon.Text = result.Data.PhoneNumber;
                FormsMessage.SuccessMessage(result.Message);
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            GroupBoxMehsulControlClear();

        }

        private void ButtonSalesFormSatisEtmek_Click(object sender, EventArgs e)
        {
            try
            {
                SaleWinForm saleWinForm = new SaleWinForm();
                IDataResult<List<Cart>> carts = _cartService.GetAllByUserId(staticUserId);
                IResult saleWinFormAdded;
                IResult productUpdated;
                IResult bonusCardIncreased;
                List<string> messages = new List<string>();
                string resultMessage = "";
                string newResultMessage = "";
                decimal totalPrice = 0;
                if (carts.Data.Count != 0)
                {
                    foreach (Cart cart in carts.Data)
                    {

                        Product product = _productService.GetById(cart.ProductId).Data;
                        product.UnitsInStock -= cart.Quantity;
                        saleWinForm.Id = 0;
                        saleWinForm.ProductId = cart.ProductId;
                        saleWinForm.UserId = cart.UserId;
                        saleWinForm.SoldPrice = cart.SoldPrice;
                        saleWinForm.Quantity = cart.Quantity;

                        if (!FormValidationTool.IsValid(new SaleWinFormValidator(),saleWinForm))
                        {
                            return;
                        }

                        saleWinFormAdded = _saleWinFormService.Add(saleWinForm);
                        productUpdated = _productService.Update(product);
                        if (!saleWinFormAdded.Success || !productUpdated.Success)
                        {
                            messages.Add(product.BarcodeNumber + " - " + product.ProductName + " : " + saleWinFormAdded.Message + " & " + productUpdated.Message);

                        }
                        messages.Add(product.ProductName + " : " + saleWinFormAdded.Message + " & " + productUpdated.Message);
                        totalPrice += saleWinForm.SoldPrice * saleWinForm.Quantity;

                    }
                    foreach (string message in messages)
                    {
                        resultMessage += $" {message} {newResultMessage} |";
                    }

                    if (BonusCardId != 0)
                    {
                        bonusCardIncreased = _bonusCardService.IncreaseBalance(BonusCardId
                            , totalPrice);
                        if (!bonusCardIncreased.Success)
                        {
                            FormsMessage.WarningMessage(bonusCardIncreased.Message);

                        }
                        else
                        {
                            FormsMessage.SuccessMessage(bonusCardIncreased.Message);
                        }

                    }
                    FormsMessage.SuccessMessage(resultMessage);
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
                buttonTemizleBonusCard_Click(sender, e);
            }
            catch (Exception ex)
            {

                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void buttonTemizleBonusCard_Click(object sender, EventArgs e)
        {
            BonusCardId = 0;
            textBoxBonusCardCustomerName.Text = "";
        }

        private void buttonBonusCardSelect_Click(object sender, EventArgs e)
        {

            BonusCardSelectForm bonusCardSelectForm = new BonusCardSelectForm(_bonusCardService);
            bonusCardSelectForm.ShowDialog();
            IDataResult<BonusCardForFormsDto> getBonusCard =
                _bonusCardService.GetBonusCardForFormsDetailById(BonusCardSelectForm.BonusCardId);
            if (!getBonusCard.Success)
            {
                FormsMessage.WarningMessage(getBonusCard.Message);
                return;
            }
            BonusCardId = BonusCardSelectForm.BonusCardId;
            textBoxBonusCardCustomerName.Text = getBonusCard.Data.Ad + " " + getBonusCard.Data.Soyad;
        }

        private void pictureBoxRefresh_Click(object sender, EventArgs e)
        {
            ProductListRefesh();
            CartListRefesh();
        }

        private void buttonBarkodNoAxtar_Click(object sender, EventArgs e)
        {
            FindByBarcodeNumberAndAddToCart(textBoxBarkodNo.Text);
        }

        private void buttonDuzelt_Click(object sender, EventArgs e)
        {
            try
            {
                if (decimal.Parse(textBoxQiymet.Text) == 0)
                {
                    FormsMessage.WarningMessage(FormsTextMessages.UnitPriceGreaterThanZero);
                    return;
                }
                Cart cart = _cartService.GetById(CartId).Data;

                if (textBoxMiqdar.Text == "")
                {
                    FormsMessage.WarningMessage(FormsTextMessages.QuantityIsBlank);
                    return;
                }

                cart.Quantity = int.Parse(textBoxMiqdar.Text);

                cart.SoldPrice = textBoxQiymet.Text == "" ? decimal.Parse(textBoxMaxQiymet.Text) : decimal.Parse(textBoxQiymet.Text);
                cart.TotalPrice = cart.Quantity * cart.SoldPrice;

                IResult updatedCart = _cartService.Update(cart);
                if (!updatedCart.Success)
                {
                    FormsMessage.ErrorMessage(updatedCart.Message);
                    return;
                }
                FormsMessage.SuccessMessage(updatedCart.Message);

                CartListRefesh();
                TotalPriceLabelWrite();
                GroupBoxMehsulControlClear();
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void buttonAxtar_Click(object sender, EventArgs e)
        {
            SalesForm salesForm = new SalesForm(_categoryService, _brandService, _supplierService, _productService, _cartService
               , _customerService, _saleWinFormService, _debtService, _bonusCardService);

            salesForm.ComboBoxSelectedValue(dataGridViewProductList, comboBoxCategoryList.Text, comboBoxSupplierList.Text, comboBoxBrandList.Text);
        }

        //Text Changed -------------------->
        private void textBoxMiqdar_TextChanged(object sender, EventArgs e)
        {
            CemWrite();

        }

        private void textBoxQiymet_TextChanged(object sender, EventArgs e)
        {
            CemWrite();


        }


        private void textBoxAxtar_TextChanged(object sender, EventArgs e)
        {
            List<ProductViewDashboardDetailDto> data = _productService.GetAllProductViewDasboardDetails().Data;
            detailsSearch.GetDataWriteGridView(data, textBoxAxtar.Text, dataGridViewProductList);
        }

        //Checked Changed----------------------------->
        private void checkBoxBarkodNo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBarkodNo.Checked == false)
            {
                checkBoxBarkodNo.Text = "Avtomatik";
                buttonBarkodNoAxtar.Visible = false;
            }
            else
            {
                checkBoxBarkodNo.Text = "Manual";
                buttonBarkodNoAxtar.Visible = true;
            }

        }



        //Double Click----------------------------------->


        private void dataGridViewCartList_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                GroupBoxMehsulControlClear();
                CartId = 0;
                if (dataGridViewCartList.CurrentRow == null)
                {
                    FormsMessage.WarningMessage(BaseMessages.SelectedValueIsNull);
                    return;
                }

                textBoxProductId.Text = dataGridViewCartList.CurrentRow.Cells["ProductId"].Value.ToString();
                CartId = int.Parse(dataGridViewCartList.CurrentRow.Cells["Id"].Value.ToString());

                CartDto cartDto = _cartService.GetCartDtoDetailByProductId(Convert.ToInt32(textBoxProductId.Text)).Data;
                textBoxBarkodNo.Text = cartDto.BarcodeNumber.ToString();
                textBoxMehsulAdi.Text = cartDto.ProductName.ToString();
                textBoxMaxQiymet.Text = cartDto.UnitPrice.ToString();

                textBoxQiymet.Text = dataGridViewCartList.CurrentRow.Cells["Qiymet"].Value.ToString();
                textBoxMiqdar.Text = dataGridViewCartList.CurrentRow.Cells["Miqdar"].Value.ToString();
                textBoxCem.Text = dataGridViewCartList.CurrentRow.Cells["Cem"].Value.ToString();
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }

        }

        private void DataGridViewSalesForm_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            GroupBoxMehsulControlClear();
            if (dataGridViewProductList.CurrentRow == null)
            {
                FormsMessage.WarningMessage(BaseMessages.SelectedValueIsNull);
                return;
            }
            textBoxProductId.Text = dataGridViewProductList.CurrentRow.Cells["ProductId"].Value.ToString();
            textBoxBarkodNo.Text = dataGridViewProductList.CurrentRow.Cells["BarcodeNomresi"].Value.ToString();
            textBoxMehsulAdi.Text = dataGridViewProductList.CurrentRow.Cells["MehsulAdi"].Value.ToString();
            textBoxMaxQiymet.Text = dataGridViewProductList.CurrentRow.Cells["Qiymet"].Value.ToString();
            CemWrite();
        }



        //Key Press--------------------------------------->
        private void textBoxBarkodNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                //MyControl.MakeTextBoxNumberBox(e);
                if (checkBoxBarkodNo.Checked == true)
                {
                    return;
                }

                string barcodeNumber = textBoxBarkodNo.Text;
                if (barcodeNumber.Length >= 13)
                {
                    FindByBarcodeNumberAndAddToCart(barcodeNumber);
                }
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
        }


        private void textBoxMaxQiymet_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyControl.MakeTextBoxDecimalBox(sender, e);
        }

        private void textBoxQiymet_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyControl.MakeTextBoxDecimalBox(sender, e);
        }

        private void textBoxMiqdar_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyControl.MakeTextBoxNumberBox(e);
        }



        //Elave metodlar--------------------------->

        private void BarcodeScanner_BarcodeScanned(object sender, BarcodeScannerEventArgs e)
        {
            textBoxBarkodNo.Text = e.Barcode;
        }

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
            TextBoxController.ClearAllTextBoxesByGroupBox(groupBoxMehsul);
            textBoxMiqdar.Text = "1";
        }

        private void GroupBoxMusteriControlClear()
        {
            TextBoxController.ClearAllTextBoxesByGroupBox(groupBoxMusteri);
        }

        private void IsBarcodeNumberExists(int productId, out bool isBarcodeNumberExists)
        {
            IDataResult<CartAddDto> result = _cartService.GetCartAddDetailByProductId(productId);
            if (result.Success)
            {
                isBarcodeNumberExists = true;
                return;
            }
            isBarcodeNumberExists = false;
            return;
        }


        private void RemoveCart()
        {
            Cart cart = new Cart();
            cart.UserId = staticUserId;
            _cartService.ByUserIdAllRemove(cart.UserId);
        }

        private void CartListRefesh()
        {
            dataGridViewCartList.DataSource = _cartService.GetAllCartViewDetailsByUserId(staticUserId).Data;

            myControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewCartList, "AlisQiymeti", Color.Yellow);
            myControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewCartList, "Qiymet", Color.Green);
            myControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewCartList, "Cem", Color.Red);
        }

        private void ProductListRefesh()
        {
            dataGridViewProductList.DataSource = _productService.GetAllProductViewDasboardDetails().Data;
            myControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewProductList, "AlisQiymet", Color.Yellow);
            myControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewProductList, "Qiymet", Color.Green);
        }


        private decimal TotalCartPriceCalculation()
        {
            decimal tolalPrice = 0;
            decimal price;
            List<Cart> carts = _cartService.GetAllByUserId(staticUserId).Data;

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
                CalculateTotalPrice(int.Parse(textBoxMiqdar.Text), decimal.Parse(textBoxQiymet.Text == "" ? textBoxMaxQiymet.Text : textBoxQiymet.Text));
            }
            catch (Exception)
            {
                textBoxCem.Text = "";
                return;
            }
        }


        private void FindByBarcodeNumberAndAddToCart(string barcodeNumber)
        {

            bool isbarcodeExists = false;
            IResult cartAdded;
            IResult cartUpdated;

            IDataResult<Product> result = _productService.GetByProductBarcodeNumber(barcodeNumber);
            if (result.Success == false)
            {
                FormsMessage.WarningMessage(result.Message);
                GroupBoxMehsulControlClear(); //bura yeniden bax
                return;
            }
            // GroupBoxMehsulControlClear();
            Cart cart = new Cart();
            cart.ProductId = result.Data.Id;
            cart.UserId = staticUserId; //sonra dinamiklesdir
            cart.Quantity = 1;
            cart.SoldPrice = result.Data.UnitPrice;
            cart.TotalPrice = cart.Quantity * cart.SoldPrice;

            if (!FormValidationTool.IsValid(new CartValidator(),cart))
            {
                return;
            }



            IsBarcodeNumberExists(cart.ProductId, out isbarcodeExists);
            if (isbarcodeExists == true)
            {
                Cart getCart = _cartService.GetByProductId(cart.ProductId).Data;
                cart.Id = getCart.Id;
                cart.Quantity = getCart.Quantity + 1;
                cart.TotalPrice = cart.SoldPrice * cart.Quantity;
                cartUpdated = _cartService.Update(cart);
                if (!cartUpdated.Success)
                {
                    FormsMessage.WarningMessage(cartUpdated.Message);
                    return;
                }
            }
            else
            {

                cartAdded = _cartService.Add(cart);
                if (!cartAdded.Success)
                {
                    FormsMessage.WarningMessage(cartAdded.Message);
                    return;
                }
            }
            GroupBoxMehsulControlClear();
            CartListRefesh();

            TotalPriceLabelWrite();
        }

        public void CategoryWriteComboBox()
        {
            List<Category> categories = _categoryService.GetAll().Data;
            comboBoxCategoryList.DataSource = categories;
            comboBoxCategoryList.DisplayMember = "CategoryName";
            comboBoxCategoryList.ValueMember = "Id";
            comboBoxCategoryList.Text = "";
        }

        public void BrandWriteComboBox()
        {
            List<Brand> brands = _brandService.GetAll().Data;
            comboBoxBrandList.DataSource = brands;
            comboBoxBrandList.DisplayMember = "BrandName";
            comboBoxBrandList.ValueMember = "Id";
            comboBoxBrandList.Text = "";
        }

        public void SupplierWriteComboBox()
        {
            List<Supplier> suppliers = _supplierService.GetAll().Data;
            comboBoxSupplierList.DataSource = suppliers;
            comboBoxSupplierList.DisplayMember = "CompanyName";
            comboBoxSupplierList.ValueMember = "Id";
            comboBoxSupplierList.Text = "";
        }

     
    }
}
