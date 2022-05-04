﻿using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs.CartDtos
{
    public class CartDto : IDto
    {
        public string BarcodeNumber { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal SoldPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CartDate { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
