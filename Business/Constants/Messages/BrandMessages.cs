﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class BrandMessages
    {
        //CRUD
        public static string BrandAdded = "Marka əlavə edildi";
        public static string BrandNotAdded = "Marka əlavə edilə bilmədi";
        public static string BrandDeleted = "Marka silindi";
        public static string BrandIsNotDeleted = "Marka silinə bilmədi";
        public static string BrandUpdated = "Marka yeniləndi";
        public static string BrandIsNotUpdating = "Marka  yenilənə bilmədi";
        public static string BrandGetAll = "Marka sıralandi";
        public static string BrandFound = "Marka tapıldı";
        public static string BrandNotFound = "Belə bir marka yoxdur";
        public static string AlreadyExistsName = "Bu Marka adı artıq istifadə edilib !";

        public static string BrandIdIsEmpty= "Marka ID-si boş və ya 0 ola bilməz!";
    }
}
