﻿using Business.Abstract;
using Business.Constants.Messages;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.SaleWinFormDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Core.Controllers.Concrete;
using WindowsForm.MyControls;
using WindowsForm.Utilities.Search.Concrete.SaleSearch;

namespace WindowsForm.Forms.AdminForms
{
    public partial class FormSalesList : Form
    {

        IProductService _productService;
        ISaleService _saleWinFormService;
        IUserService _userService;

        private List<SaleWinFormDto> _dataSaleWinFormDeatil;

        SaleWinFormDetailDtoSearch detailSearch = new SaleWinFormDetailDtoSearch();

        public FormSalesList(IProductService productService, ISaleService saleWinFormService, IUserService userService)
        {
            _productService = productService;
            _saleWinFormService = saleWinFormService;
            _userService = userService;
            _dataSaleWinFormDeatil = _saleWinFormService.GetAllSaleWinFormDtoDetails().Data;
            InitializeComponent();
        }

        private void FormSalesList_Load(object sender, EventArgs e)
        {
            ComboBoxController.WriteDaysInComboBox(comboBoxDays);
            ComboBoxController.WriteMonthsInComboBox(comboBoxMonths);
            ComboBoxController.WriteYearsInComboBox(comboBoxYears);
            SaleListRefesh();
            CalculateCountOfAllProduct();
            CalculateUnitPriceOfAllProduct();
            CalculatePurchasePriceOfAllProduct();
            checkBoxSatisLegvEdilsin.Checked = false;
        }

        //Click------------------------------------------->
        private void buttonAxtar_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedDayItem = comboBoxDays.SelectedItem != null ? int.Parse(comboBoxDays.SelectedItem.ToString())
             : DateTime.Now.Day;

                int selectedMonthItem = comboBoxMonths.SelectedItem != null ? int.Parse(comboBoxMonths.SelectedItem.ToString())
                   : DateTime.Now.Month;

                int selectedYearItem = comboBoxYears.SelectedItem != null ? int.Parse(comboBoxYears.SelectedItem.ToString())
                   : DateTime.Now.Year;
                decimal saleTotal = 0;
                decimal incomeTotal = 0;
                int staticUseraId = LoginForm.UserId;

                if (comboBoxDays.SelectedItem == null && comboBoxMonths.SelectedItem != null && comboBoxYears.SelectedItem == null)
                {
                    List<SaleWinFormDto> dataMonth = _saleWinFormService
                        .GetAllSaleWinFormDetailsSalesForMonthAndYear(selectedMonthItem, selectedYearItem).Data;

                    foreach (SaleWinFormDto item in dataMonth)
                    {
                        if (item.SatisinVeziyyeti == true)
                        {


                            saleTotal += item.Cem;

                            Product getProduct = _productService.GetById(item.ProductId).Data;
                            for (int i = 1; i < item.Miqdar + 1; i++)
                            {
                                incomeTotal += (item.SatilanQiymet - getProduct.PurchasePrice);
                            }
                        }

                    }

                    labelTotal.Text = saleTotal.ToString();
                    labelIncome.Text = incomeTotal.ToString();
                    dataGridViewSaleList.DataSource = dataMonth;
                    return;
                }

                if (comboBoxDays.SelectedItem == null && comboBoxMonths.SelectedItem == null && comboBoxYears.SelectedItem != null)
                {
                    List<SaleWinFormDto> dataYear = _saleWinFormService
                        .GetAllSaleWinFormDetailsSalesForYear(selectedYearItem).Data;

                    foreach (SaleWinFormDto item in dataYear)
                    {
                        if (item.SatisinVeziyyeti == true)
                        {
                            saleTotal += item.Cem;

                            Product getProduct = _productService.GetById(item.ProductId).Data;
                            for (int i = 1; i < item.Miqdar + 1; i++)
                            {
                                incomeTotal += (item.SatilanQiymet - getProduct.PurchasePrice);
                            }
                        }
                    }

                    labelTotal.Text = saleTotal.ToString();
                    labelIncome.Text = incomeTotal.ToString();
                    dataGridViewSaleList.DataSource = dataYear;
                    return;
                }


                if (comboBoxDays.SelectedItem == null && comboBoxMonths.SelectedItem != null && comboBoxYears.SelectedItem != null)
                {
                    List<SaleWinFormDto> dataMonth = _saleWinFormService
                        .GetAllSaleWinFormDetailsSalesForMonthAndYear(selectedMonthItem, selectedYearItem).Data;
                    foreach (SaleWinFormDto item in dataMonth)
                    {
                        if (item.SatisinVeziyyeti == true)
                        {
                            saleTotal += item.Cem;
                            Product getProduct = _productService.GetById(item.ProductId).Data;
                            for (int i = 1; i < item.Miqdar + 1; i++)
                            {
                                incomeTotal += (item.SatilanQiymet - getProduct.PurchasePrice);
                            }
                        }
                    }
                    labelTotal.Text = saleTotal.ToString();
                    IResult getUserClaims = _userService.CheckUserOperationClaimIsBossAndAdminByUserIdForForms(staticUseraId);
                    if (getUserClaims.Success)
                    {
                    //    if (staticUseraId == 3002 || staticUseraId == 2004)
                    //{
                        labelIncome.Text = incomeTotal.ToString();
                    }
                    dataGridViewSaleList.DataSource = dataMonth;
                    return;
                }

                List<SaleWinFormDto> data = _saleWinFormService
                        .GetAllSaleWinFormDetailsSalesForDayAndMonthAndYear(selectedDayItem, selectedMonthItem, selectedYearItem).Data;
                foreach (SaleWinFormDto item in data)
                {
                    if (item.SatisinVeziyyeti == true)
                    {
                        saleTotal += item.Cem;
                        Product getProduct = _productService.GetById(item.ProductId).Data;
                        for (int i = 1; i < item.Miqdar + 1; i++)
                        {
                            incomeTotal += (item.SatilanQiymet - getProduct.PurchasePrice);
                        }
                    }
                }
                labelTotal.Text = saleTotal.ToString();

                labelIncome.Text = incomeTotal.ToString();

                dataGridViewSaleList.DataSource = data;
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
        }

        private void buttonTetbiqEt_Click(object sender, EventArgs e)
        {
            try
            {
                Sale sale = new Sale();
                if (textBoxSaleId.Text == "")
                {
                    FormsMessage.WarningMessage(FormsTextMessages.SaleIdBlank);
                    return;
                }
                sale.Id = int.Parse(textBoxSaleId.Text);

                IResult canceledSale;
                if (checkBoxSatisLegvEdilsin.Checked == true)
                {
                    canceledSale = _saleWinFormService.CancelSale(sale);
                    if (!canceledSale.Success)
                    {
                        FormsMessage.WarningMessage(canceledSale.Message);
                        return;
                    }

                    FormsMessage.SuccessMessage(canceledSale.Message);
                    checkBoxSatisLegvEdilsin.Checked = false;
                }

                SaleListRefesh();
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage(this.Name, MethodBase.GetCurrentMethod().Name, ex));
                return;
            }
        }


        private void pictureBoxRefresh_Click(object sender, EventArgs e)
        {
            SaleListRefesh();
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            ComboBoxController.ClearAllComboBoxByGroupBox(groupBox1);
        }

        //Cell double click ------------------->

        private void dataGridViewSaleList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewSaleList.CurrentRow == null)
            {
                FormsMessage.WarningMessage(BaseMessages.SelectedValueIsNull);
                return;
            }
            textBoxSaleId.Text = dataGridViewSaleList.CurrentRow.Cells["SaleId"].Value.ToString();
            textBoxMehsul.Text = dataGridViewSaleList.CurrentRow.Cells["MehsulAdi"].Value.ToString();
            textBoxMiqdar.Text = dataGridViewSaleList.CurrentRow.Cells["Miqdar"].Value.ToString();
            textBoxUmumiDeyer.Text = dataGridViewSaleList.CurrentRow.Cells["Cem"].Value.ToString();
            textBoxSatIici.Text = dataGridViewSaleList.CurrentRow.Cells["Istifadeci"].Value.ToString();
            textBoxTarix.Text = dataGridViewSaleList.CurrentRow.Cells["Tarix"].Value.ToString();
            checkBoxSatisLegvEdilsin.Checked = false;
        }

        //Elave Metodlar------------------------>
        private void CalculateCountOfAllProduct()
        {
            List<Product> result = _productService.GetAll().Data;
            int total = 0;
            foreach (Product product in result)
            {
                total += product.UnitsInStock;
            }
            labelCountOfAllProduct.Text = total.ToString();
        }

        private void CalculateUnitPriceOfAllProduct()
        {
            List<Product> result = _productService.GetAll().Data;
            decimal total = 0;
            foreach (Product product in result)
            {
                total += product.UnitPrice * product.UnitsInStock;
            }
            labelPriceOfAllProduct.Text = total.ToString();
        }
        private void CalculatePurchasePriceOfAllProduct()
        {
            List<Product> result = _productService.GetAll().Data;
            decimal total = 0;
            foreach (Product product in result)
            {
                total += product.PurchasePrice * product.UnitsInStock;
            }
            labelPurchasePriceOfAllProduct.Text = total.ToString();
        }


        private void SaleListRefesh()
        {
            _dataSaleWinFormDeatil= _saleWinFormService.GetAllSaleWinFormDtoDetails().Data;
            dataGridViewSaleList.DataSource=_dataSaleWinFormDeatil;

            MyControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewSaleList, "AlisQiymeti", Color.Yellow);
            MyControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewSaleList, "SatilanQiymet", Color.Green);
            MyControl.MakeDataGridViewCurrentColumnCurrentColor(dataGridViewSaleList, "Cem", Color.Red);
        }

        private void textBoxAxtar_TextChanged(object sender, EventArgs e)
        {
            
            detailSearch.GetDataWriteGridView(_dataSaleWinFormDeatil, textBoxAxtar.Text, dataGridViewSaleList);
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void groupBoxProducts_Enter(object sender, EventArgs e)
        {

        }

        private void labelOurchasePriceOfAllProduct_Click(object sender, EventArgs e)
        {

        }
    }
}
