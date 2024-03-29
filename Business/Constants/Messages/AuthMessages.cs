﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants.Messages
{
    public static class AuthMessages
    {
        public static string AuthorizationDenied = "Səlahiyyətiniz yoxdur";
        public static string Alowed = "Icazə verildi";

        public static string UserRegistered = "Qeydiyyatdan kecdi";
        public static string RegistirationFailed = "Qeydiyyat uğursuz oldu";
        public static string UserNotFound = "Istifadəci tapilmadi";
        public static string PasswordError = "Şifrə yanlışdır";
        public static string SuccessfulLogin = "Uğurlu giriş";
        public static string UserAlreadyExists = "İstifadəçi mövcuddur";
        public static string AccessTokenCreated = "Token yaradıldı";

        public static string passwordAndPasswordRepeatNotEquals="Şifrə ilə şifrə təkrarı eyni deyil";
        public static string PasswordNull= "Şifrə boşdur, zəhmət olmasa şifrə yazın";

        public static string PasswordIsLessThanFourCharacters = "Şifrə 4 simvoldan azdır";


       
    }
}
