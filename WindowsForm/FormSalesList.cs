﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class FormSalesList : Form
    {
        SaleManager _saleManager = new SaleManager(new EfSaleDal());
        public FormSalesList()
        {
            InitializeComponent();
        }

        private void FormSalesList_Load(object sender, EventArgs e)
        {
            SaleListRefesh();
        }
        private void SaleListRefesh()
        {
            dataGridViewSaleList.DataSource = _saleManager.GetAll().Data;   
        }
    }
}
