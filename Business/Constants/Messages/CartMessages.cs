﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class CartMessages
    {
        //CRUD
        public static string Added = "Səbət əlavə edildi";
        public static string ProductAdded = "Məhsul səbətə əlavə edildi";
        public static string NotAdded = "Səbət əlavə edilə bilmədi";
        public static string ProductNotAdded = "Məhsul səbətə əlavə edilə bilmədi";
        public static string Deleted = "Səbət silindi";
        public static string ProductDeleted = "Məhsul səbətdən silindi";
        public static string IsNotDeleted = "Səbət silinə bilmədi";
        public static string ProductNotDeleted = "Məhsul səbətdən silinə bilmədi";
        public static string Updated = "Səbət yeniləndi";
        public static string IsNotUpdating = "Səbət  yenilənə bilmədi";
        public static string GetAll = "Səbət sıralandi";
        public static string Found = "Səbət tapıldı";
        public static string NotFound = "Belə bir Səbət yoxdur";

        public static string QuantityMustBeGreaterThanZero = "Miqdar '0' dan böyük olamlıdır";
        public static string TotalPriceMustBeGreaterThanZero = "Cəm qiyməti '0' dan böyük olamlıdır";
        public static string SoldPriceMustBeGreaterThanZero = "Satış qiyməti '0' dan böyük olamlıdır";
        public static string ProductNotFound = "Səbətdə belə bir məhsul yoxdur";
        public static string ProductFound = "Məhsula uyğun səbət tapıldı";

    }
}
