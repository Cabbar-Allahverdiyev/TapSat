﻿using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs.SaleWinFormDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Utilities.Search.Abstract;

namespace WindowsForm.Utilities.Search.Concrete.SaleSearch
{
    public class SaleWinFormDetailDtoSearch : IWinFormsSearch<SaleWinFormDto>
    {
        public void GetDataWriteGridView(List<SaleWinFormDto> data, string seachText, DataGridView dataGridView)
        {
            dataGridView.DataSource = Search(data, seachText);
        }

        public List<SaleWinFormDto> Search(List<SaleWinFormDto> data, string searchText)
        {
            return data.Where(s => $"{s.SaleId} {s.ProductId} {s.MehsulAdi} {s.Kateqoriya} {s.Marka} {s.Istifadeci} {s.AlisQiymeti} {s.SatilanQiymet} {s.Miqdar} {s.Cem} {s.Tarix} {s.SatisinVeziyyeti}"
            .ToLower().Contains(searchText.ToLower())).ToList();
        }
    }
}
