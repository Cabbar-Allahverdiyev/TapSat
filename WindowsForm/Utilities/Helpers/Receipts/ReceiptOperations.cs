﻿using Business.Abstract;
using Business.Constants.Dictionaries;
using Business.Constants.Messages;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Concrete.ForForms;
using Entities.DTOs.CartDtos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using WindowsForm.Core.Constants.Messages;
using WindowsForm.Forms;
using static Business.Constants.Enums.SettingEnums;

namespace WindowsForm.Utilities.Helpers.Receipts
{
    public class ReceiptOperations : IReceiptOperation
    {
        IFormSettingService _formSettingService;
        public ReceiptOperations(IFormSettingService formSettingService)
        {
            _formSettingService = formSettingService;
        }

        private IDataResult<int> IsTheReceiptWrittenTodayAndIncrease()
        {
            IDataResult<FormSetting> receiptGivenOnTheDaySetting = _formSettingService.GetByName(SettingsDictionary.Settings[SettingParameter.ReceiptGivenOnTheDay]);
            int receiptGivenOnTheDay = 0;
            if (!receiptGivenOnTheDaySetting.Success)
            {
                IResult addedSetting = _formSettingService.Add(new FormSetting() { Name = SettingsDictionary.Settings[SettingParameter.ReceiptGivenOnTheDay], Value = $"0&{DateTime.Today}" });
                if (!addedSetting.Success)
                {
                    return new ErrorDataResult<int>(0);//mesaj yarat
                }
                return new SuccessDataResult<int>(0);//


            }
            string data = receiptGivenOnTheDaySetting.Data.Value;
            int index = data.IndexOf('&');
            if (data.Substring(index + 1, data.Length - (index + 1)) == DateTime.Today.ToString())
            {
                receiptGivenOnTheDay = int.Parse(data.Substring(0, index));
                receiptGivenOnTheDay++;
            }

            receiptGivenOnTheDaySetting.Data.Value = $"{receiptGivenOnTheDay}&{DateTime.Today}";
            IResult updateSetting = _formSettingService.Update(receiptGivenOnTheDaySetting.Data);
            if (updateSetting.Success)
            {
                return new SuccessDataResult<int>(receiptGivenOnTheDay);//
            }
            return new ErrorDataResult<int>(0);//

        }

        private IDataResult<int> RecipeNumberExistsAndIncrease()
        {
            IDataResult<FormSetting> getRecipeNumberSetting = _formSettingService.GetByName(SettingsDictionary.Settings[SettingParameter.ReceiptNumber]);
            if (getRecipeNumberSetting.Success)
            {
                getRecipeNumberSetting.Data.Value = (int.Parse(getRecipeNumberSetting.Data.Value) + 1).ToString();
                _formSettingService.Update(getRecipeNumberSetting.Data);
                return new SuccessDataResult<int>(int.Parse(getRecipeNumberSetting.Data.Value));
            }

            IResult addedSetting = _formSettingService.Add(new FormSetting()
            {
                Name = SettingsDictionary.Settings[SettingParameter.ReceiptNumber],
                Value = 0.ToString()
            });
            return new SuccessDataResult<int>(0);


        }
        public IDataResult<PrintDocument> PrepareAReceipt(System.Drawing.Printing.PrintPageEventArgs e,
                                                          PrintDocument printDocReceipt,
                                                          IDataResult<CartListDtoForReceipt> carts,
                                                          ReceiptDto receiptDto)
        {
            List<FormSetting> settings = _formSettingService.GetAll().Data;
            string shopName = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopName]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopName]).Value,
                   address = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopAddress]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopAddress]).Value,
                   shopeCode = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopNumber]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ShopNumber]).Value,
                   shopNumber = "",
                   voen = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.Voen]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.Voen]).Value,
                   cashier = $"{receiptDto.UserFirstName} {receiptDto.UserLastName}",
                   bonusType = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.BonusType]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.BonusType]).Value,
                   bonusCardNumber = receiptDto.BonusCardBarcode,
                   bonusEarned = receiptDto.EarnedBonus.ToString(),
                   remainingBonus = receiptDto.RemainingBonus.ToString(),
                   vaultModel = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.VaultModel]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.VaultModel]).Value,
                   vaultSerialNumber = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.VaultSerialNumber]).Value == null ? "" : settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.VaultSerialNumber]).Value;

            // FormSetting recipeNumberSetting = settings.SingleOrDefault(s => s.Name == SettingsDictionary.Settings[SettingParameter.ReceiptNumber]);

            int receiptNumber = this.RecipeNumberExistsAndIncrease().Data;
            IDataResult<int> receiptGivenOnTheDayResult = this.IsTheReceiptWrittenTodayAndIncrease();
            if (!receiptGivenOnTheDayResult.Success)
            {
                FormsMessage.ErrorMessage(receiptGivenOnTheDayResult.Message);
            }
            int receiptGivenOnTheDay = receiptGivenOnTheDayResult.Data;
            receiptNumber++;

            List<ReceiptText> receiptTexts = new List<ReceiptText>()
            {
                new ReceiptText("TapSat System",fontSize: 18,style: FontStyle.Bold,x:40),
                new ReceiptText("",value:shopName,fontSize: 18,style: FontStyle.Bold,x:40),
                new ReceiptText ("",value:address),
                new ReceiptText("Obyektin kodu:",value:shopeCode),
                new ReceiptText("VÖEN:" ,value:voen),
                new ReceiptText("Satış çeki",x:100,style:FontStyle.Bold),
                new ReceiptText("-----------------------------------------------------------------------"),
                new ReceiptText("Çek nömrəsi № ",value:receiptNumber.ToString()),
                new ReceiptText("Mağaza:",value:shopNumber,style:FontStyle.Bold),
                new ReceiptText("Kassir:",value:cashier),
            };

            //products
            int y = 180, x = 160;
            receiptTexts.Add(new ReceiptText("Tarix:", value: DateTime.Today.ToString("dd/MM/yyyy"), y: y, x: x));
            y += 20;
            receiptTexts.Add(new ReceiptText("Saat:", value: DateTime.Now.ToString("HH/mm/ss"), y: y, x: x));

            y = 240;
            x = 0;
            receiptTexts.Add(new ReceiptText("Malın adı", style: FontStyle.Underline, x: 10, y: y));
            receiptTexts.Add(new ReceiptText("Miqdar", style: FontStyle.Underline, x: 160, y: y));
            receiptTexts.Add(new ReceiptText("Qiymət", style: FontStyle.Underline, x: 200, y: y));
            receiptTexts.Add(new ReceiptText("Toplam", style: FontStyle.Underline, x: 240, y: y));
            foreach (var cart in carts.Data.Carts)
            {
                y += 20;
                receiptTexts.Add(new ReceiptText(cart.ProductName, x: 10, y: y));
                receiptTexts.Add(new ReceiptText(cart.Quantity.ToString(), x: 160, y: y));
                receiptTexts.Add(new ReceiptText(cart.SoldPrice.ToString(), x: 200, y: y));
                receiptTexts.Add(new ReceiptText(cart.TotalPrice.ToString(), x: 240, y: y));
            }
            x = 240;
            y += 20;
            receiptTexts.Add(new ReceiptText("-----------------------------------------------------------------------"));
            y += 10;
            receiptTexts.Add(new ReceiptText("YEKUN MƏBLƏĞ", style: FontStyle.Bold, fontSize: 16, y: y));

            receiptTexts.Add(new ReceiptText("*" + carts.Data.Total.ToString(), style: FontStyle.Bold, fontSize: 14, x: 200, y: y));
            y += 20;
            receiptTexts.Add(new ReceiptText("-----------------------------------------------------------------------", y: y));

            y += 20;
            receiptTexts.Add(new ReceiptText("Nağd", y: y));
            receiptTexts.Add(new ReceiptText(carts.Data.Total.ToString(), y: y, x: x));
            y += 20;
            receiptTexts.Add(new ReceiptText("Ödəndi", y: y));
            receiptTexts.Add(new ReceiptText(carts.Data.Total.ToString(), y: y, x: x));
            y += 20;
            receiptTexts.Add(new ReceiptText("Pulun qalığı", y: y));
            receiptTexts.Add(new ReceiptText("0.00", y: y, x: x));

            receiptTexts.Add(new ReceiptText("Müştəri kart nömrəsi:", value: bonusCardNumber));
            //barkod yerlesdir
            receiptTexts.Add(new ReceiptText($"Qazanılan bonus ({bonusType}):", value: "*" + bonusEarned));
            receiptTexts.Add(new ReceiptText($"Bonus qalığı ({bonusType}):", value: "*" + remainingBonus));
            receiptTexts.Add(new ReceiptText("Gün ərzində vurulan çek:", value: receiptGivenOnTheDay.ToString()));

            receiptTexts.Add(new ReceiptText("Kassa aparatının modeli:", value: vaultModel));


            receiptTexts.Add(new ReceiptText("Kassa aparatının zavod nömrəsi:", value: vaultSerialNumber));//tap ve duzelt
            //receiptTexts.Add(new ReceiptText("")); qr kod qoy
            receiptTexts.Add(new ReceiptText("Karabakh is Azerbaijan", fontSize: 12, style: FontStyle.Bold, x: 40));
            //receiptTexts.Add(new ReceiptText(""));


            y = 0;
            x = 0;
            for (int i = 0; i < receiptTexts.Count; i++)
            {
                ReceiptText receiptText = receiptTexts[i];

                if (i == 0) y = 10;
                // else if (i != 0) receiptText.Y = y;

                if (receiptTexts[i].FontSize > 8 && i != 0) y += 30;
                if (receiptTexts[i].FontSize <= 8 && i != 0 && receiptTexts[i - 1].FontSize <= 8) y += 20;
                if (receiptTexts[i].FontSize <= 8 && i != 0 && receiptTexts[i - 1].FontSize > 8) y += 40;
                if (receiptTexts[i].Y != 0) y = receiptText.Y;
                if (receiptTexts[i].Y == 0) receiptText.Y = y;

                if (receiptText.X == 10) receiptText.X = x;
                e.Graphics.DrawString(receiptText.Text + " " + receiptText.Value,
                                      new Font(receiptText.FontType,
                                               receiptText.FontSize,
                                               receiptText.Style),
                                      Brushes.Black,
                                      new Point(receiptText.X, receiptText.Y));
            }

            foreach (var item in receiptTexts)
            {
                item.Y = 0;
            }
            return new SuccessDataResult<PrintDocument>(printDocReceipt);
        }

        public IResult PrintReceipt(PrintPreviewDialog printPreviewDialog,
                                    PrintDocument printDocReceipt)
        {
            IDataResult<FormSetting> printerSetting = _formSettingService.GetByName(
                        SettingsDictionary.Settings[SettingParameter.ReceiptPrinterModel]);
            if (printerSetting.Success && printerSetting.Data.Value != "")
            {
                printDocReceipt.DefaultPageSettings.PrinterSettings.PrinterName = printerSetting.Data.Value;
                try { printDocReceipt.Print(); }
                catch (InvalidPrinterException ex)
                {
                    return new ErrorResult(PrinterMessages.PrinterNameIsNotFound(printerSetting.Data.Value));
                }

            }

            else this.PrintShowDialog(printDocReceipt);
            return new SuccessResult();
        }

        public void PrintShowDialog(PrintDocument printDocReceipt)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocReceipt;
            if (printDialog.ShowDialog() == DialogResult.OK)
            {

                printDocReceipt.Print();
            }
        }

        public IResult PrintReceiptCheckedIsTrue(IUserService userService,
                                                IBonusCardService bonusCardService,
                                                int userId,
                                                int bonusCardId,
                                                bool situation,
                                                decimal bonusIcreasedValue,
                                                ref ReceiptDto receiptDto,
                                                PrintDocument printDocReceipt,
                                                PrintPreviewDialog printPreviewDialogReceipt)
        {
            try
            {
                if (situation == true)
                {
                    decimal? value;
                    try { value = bonusIcreasedValue; }
                    catch (NullReferenceException)
                    {
                        value = 0;
                    }
                    receiptDto = new ReceiptDto(userService, bonusCardService,
                                      userId,
                                      bonusCardId,
                                      value
                                     );
                    IResult printReceipt = this.PrintReceipt(printPreviewDialogReceipt, printDocReceipt);
                    if (!printReceipt.Success)
                    {
                        FormsMessage.WarningMessage(printReceipt.Message);
                        return new ErrorResult(printReceipt.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                FormsMessage.ErrorMessage(BaseMessages.ExceptionMessage("ReceiptOperations.PrintReceiptCheckedIsTrue", MethodBase.GetCurrentMethod().Name, ex));
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
