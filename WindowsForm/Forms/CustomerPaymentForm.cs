﻿using Business.Concrete;
using Business.Constants.Messages;
using Core.Utilities.Results;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.CustomerDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Core.Controllers.Concrete;
using WindowsForm.Core.Controllers.Concrete.ValidatorControllers;
using WindowsForm.MyControls;
using WindowsForm.Core.Constants.SelectionItem;

namespace WindowsForm.Forms
{
    public partial class CustomerPaymentForm : Form
    {
        CustomerPaymentManager _paymentManager = new CustomerPaymentManager(new EfCustomerPaymentDal(), new CustomerBalanceManager(new EfCustomerBalanceDal()));
        CustomerManager _customerManager = new CustomerManager(new EfCustomerDal(), new CustomerBalanceManager(new EfCustomerBalanceDal()));
        CustomerPaymentValidationTool validationTool = new CustomerPaymentValidationTool();

        public CustomerPaymentForm()
        {
            InitializeComponent();
            CustomerPaymentListRefresh();
            //CustomerGetComboBox();
            checkBoxOdenisLegvEdilsin.Checked = false;
        }

        //Click-------------->
        private void buttoElaveEt_Click(object sender, EventArgs e)
        {
            try
            {
                CustomerPayment customerPayment = new CustomerPayment();
                customerPayment.CustomerId = Convert.ToInt32(textBoxCustomerIdInPaymentAdd.Text);
                customerPayment.Value = decimal.Parse(textBoxMeblegInPaymentAdd.Text);

                if (!validationTool.IsValid(customerPayment))
                {
                    return;
                }

                IResult result = _paymentManager.Add(customerPayment);
                if (!result.Success)
                {
                    FormsMessage.ErrorMessage(result.Message);
                    return;
                }
                FormsMessage.SuccessMessage(result.Message);
                TextBoxController.ClearAllTextBoxesByGroupBox(groupBoxPaymentAdd);
                CustomerPaymentListRefresh();
            }
            catch (Exception ex)
            {

                FormsMessage.ErrorMessage($"{BaseMessages.ErrorMessage} | { ex.Message}");
                return;
            }

        }


        //Key Press----------------------------------->
        private void textBoxMebleg_KeyPress(object sender, KeyPressEventArgs e)
        {
            MyControl.MakeTextBoxDecimalBox(sender, e);
        }


        //Elave metodlar------------------>
        private void CustomerPaymentListRefresh()
        {
            dataGridViewPaymentList.DataSource = _paymentManager.GetCustomerPaymentDetails().Data;
        }

        //private void CustomerGetComboBox()
        //{
        //    List<CustomerDto> get = _customerManager.GetCustomerDetails().Data;
        //    comboBoxMusteriInPaymentAdd.DataSource = get;
        //    comboBoxMusteriInPaymentAdd.DisplayMember = "FirstName";
        //    comboBoxMusteriInPaymentAdd.ValueMember = "CustomerId";

        //}

        private void groupBoxPayment_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridViewPaymentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxCustomerPaymentIdInCancelPayment.Text = dataGridViewPaymentList.CurrentRow.Cells["CustomerPaymentId"].Value.ToString();
            textBoxMusteriInCancelPayment.Text = dataGridViewPaymentList.CurrentRow.Cells["FullName"].Value.ToString();
            //textBoxSaticiInCancelPayment.Text = dataGridViewPaymentList.CurrentRow.Cells["UserName"].Value.ToString();
            textBoxMeblegInCancelPayment.Text = dataGridViewPaymentList.CurrentRow.Cells["Value"].Value.ToString();
            textBoxTarixInCancelPayment.Text = dataGridViewPaymentList.CurrentRow.Cells["Date"].Value.ToString();
            checkBoxOdenisLegvEdilsin.Checked = false;
        }

        private void buttonTetbiqEt_Click(object sender, EventArgs e)
        {
            CustomerPayment customerPayment = new CustomerPayment();
            if (textBoxCustomerPaymentIdInCancelPayment.Text == "")
            {
                FormsMessage.WarningMessage(FormsTextMessages.CustomerPaymentIdBlank);
                return;
            }
            customerPayment.Id = int.Parse(textBoxCustomerPaymentIdInCancelPayment.Text);

            IDataResult<CustomerPayment> getPayment = _paymentManager.GetById(customerPayment.Id);
            if (!getPayment.Success)
            {
                FormsMessage.WarningMessage(getPayment.Message);
                return;
            }

            if (checkBoxOdenisLegvEdilsin.Checked == true)
            {

                getPayment.Data.PaymentStatus = false;
                IResult result = _paymentManager.CancelPayment(getPayment.Data);
                if (!result.Success)
                {
                    FormsMessage.WarningMessage(result.Message);
                    return;
                }
                FormsMessage.SuccessMessage(result.Message);
                TextBoxController.ClearAllTextBoxesByGroupBox(groupBoxCancelPayment);
                checkBoxOdenisLegvEdilsin.Checked = false;
                CustomerPaymentListRefresh();

                //getPayment.Data.CancelDate = DateTime.Now;
                //bax
            }
            else
            {
                FormsMessage.WarningMessage(BaseMessages.NoChange);
            }
        }

        private void buttonSec_Click(object sender, EventArgs e)
        {
            CustomerListForm customerListForm = new CustomerListForm();
            customerListForm.ShowDialog();
            textBoxCustomerIdInPaymentAdd.Text = SelectedCustomerForSalesForm.Id.ToString();
            IDataResult<Customer> result = _customerManager.GetById(SelectedCustomerForSalesForm.Id);
            if (!result.Success)
            {
                FormsMessage.ErrorMessage(result.Message);
                return;
            }
            textBoxMusteriInPaymentAdd.Text = result.Data.FirstName + " " + result.Data.LastName;
            textBoxTelefonInPaymentAdd.Text = result.Data.PhoneNumber;
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            TextBoxController.ClearAllTextBoxesByGroupBox(groupBoxPaymentAdd);
        }

        private void groupBoxCancelPayment_Enter(object sender, EventArgs e)
        {

        }
    }
}
