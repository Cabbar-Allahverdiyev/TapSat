﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class BonusCardMessages
    {
        public static string Added = "Bonus kartı əlavə edildi";
        public static string NotAdded = "Bonus kartı edilə bilmədi";
        public static string Deleted = "Bonus kartı silindi";
        public static string IsNotDeleted = "Bonus kartı silinə bilmədi";
        public static string Updated = "Bonus kartı yeniləndi";
        public static string IsNotUpdating = "Bonus kartı yenilənə bilmədi";
        public static string GetAll = "Bonus kartları sıralandi";
        public static string Found = "Bonus kartı tapıldı";
        public static string NotFound = "Belə bir Bonus kartı yoxdur";

        public static string ThisCustomerDoesNotHaveABonusCard = "Bu Müştəriyə aid bonus kart yoxdur";
        public static string ThisCustomerAlreadyExistsABonusCard = "Bu müştərinin artıq bonus kartı var";

        public static string IncreaseBalance(string customerName) => $"{customerName} balansı artırıldı";
        public static string NotIncreaseBalance (string customerName)=> $"{customerName} balansı artırıla bilmədi !";

        public static string ReduceBalance(string customerName) => $"{customerName} balansı azaldı";
        public static string NotReduceBalance(string customerName) => $"{customerName} balansı azaldıla bilmədi";

    }
}
