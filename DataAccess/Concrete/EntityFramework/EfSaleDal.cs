﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.DTOs.SaleWinFormDtos;
using Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSaleDal : EfEntityRepositoryBase<Sale, StockTrackingProjectContext>
                               , ISaleDal
    {


        public List<SaleWinFormDto> GetAllWinFormDtoDetails(Expression<Func<SaleWinFormDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from s in context.Sales
                             join p in context.Products on s.ProductId equals p.Id
                             join u in context.Users on s.UserId equals u.Id
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             orderby s.SellDate descending
                             select new SaleWinFormDto
                             {
                                 BarkodNomresi=p.BarcodeNumber,
                                 SaleId = s.Id,
                                 ProductId = s.ProductId,
                                 MehsulAdi = p.ProductName,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 Istifadeci = $"{u.FirstName} {u.LastName}",
                                 AlisQiymeti = p.PurchasePrice,
                                 SatilanQiymet = s.SoldPrice,
                                 Miqdar = s.Quantity,
                                 Cem = s.SoldPrice * s.Quantity,
                                 SatisinVeziyyeti=s.SaleStatus,
                                 Tarix = s.SellDate

                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }

        }

        public List<SaleWinFormUserDto> GetAllWinFormUserDtoDetails(Expression<Func<SaleWinFormUserDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from s in context.Sales
                             join p in context.Products on s.ProductId equals p.Id
                             join u in context.Users on s.UserId equals u.Id
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             orderby s.SellDate descending
                             select new SaleWinFormUserDto
                             {
                                 BarkodNomresi = p.BarcodeNumber,
                                 SaleId = s.Id,
                                 ProductId = s.ProductId,
                                 MehsulAdi = p.ProductName,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 Istifadeci = $"{u.FirstName} {u.LastName}",
                                 SatilanQiymet = s.SoldPrice,
                                 Miqdar = s.Quantity,
                                 Cem = s.SoldPrice * s.Quantity,
                                 SatisinVeziyyeti = s.SaleStatus,
                                 Tarix = s.SellDate

                             };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }


        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByDayAndMonthAndYear(int day, int month, int year)
        //{
        //    using (StockTrackingProjectContext context = new StockTrackingProjectContext())
        //    {

        //        var result = from s in context.SalesWinForms
        //                     join p in context.Products on s.ProductId equals p.Id
        //                     join u in context.Users on s.UserId equals u.Id
        //                     where s.SellDate.Day == day
        //                     where s.SellDate.Month == month
        //                     where s.SellDate.Year == year
        //                     orderby s.SellDate descending
        //                     select new SaleWinFormDto
        //                     {
        //                         SaleId = s.Id,
        //                         ProductId = s.ProductId,
        //                         MehsulAdi = p.ProductName,
        //                         Istifadeci = $"{u.FirstName} {u.LastName}",
        //                         AlisQiymeti = p.PurchasePrice,
        //                         SatilanQiymet = s.SoldPrice,
        //                         Miqdar = s.Quantity,
        //                         Cem = s.SoldPrice * s.Quantity,
        //                         SatisinVeziyyeti = s.SaleStatus,
        //                         Tarix = s.SellDate

        //                     };

        //        return result.ToList();
        //    }

        //}


        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByMonthAndYear(int month, int year)
        //{

        //    using (StockTrackingProjectContext context = new StockTrackingProjectContext())
        //    {

        //        var result = from s in context.SalesWinForms
        //                     join p in context.Products on s.ProductId equals p.Id
        //                     join u in context.Users on s.UserId equals u.Id
        //                     where s.SellDate.Month == month
        //                     where s.SellDate.Year == year
        //                     orderby s.SellDate descending
        //                     select new SaleWinFormDto
        //                     {
        //                         SaleId = s.Id,
        //                         ProductId = s.ProductId,
        //                         MehsulAdi = p.ProductName,
        //                         Istifadeci = $"{u.FirstName} {u.LastName}",
        //                         AlisQiymeti = p.PurchasePrice,
        //                         SatilanQiymet = s.SoldPrice,
        //                         Miqdar = s.Quantity,
        //                         Cem = s.SoldPrice * s.Quantity,
        //                         SatisinVeziyyeti = s.SaleStatus,
        //                         Tarix = s.SellDate

        //                     };

        //        return result.ToList();
        //    }
        //}

        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByDecreasingProducts(Expression<Func<SaleWinFormDto, bool>> filter = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByFinishedProducts(Expression<Func<SaleWinFormDto, bool>> filter = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByProductsThatNeverSell(Expression<Func<SaleWinFormDto, bool>> filter = null)
        //{
        //    using (StockTrackingProjectContext context = new StockTrackingProjectContext())
        //    {
        //        var result = from s in context.SalesWinForms
        //                     join p in context.Products on s.ProductId equals p.Id
        //                     join u in context.Users on s.UserId equals u.Id
        //                     orderby s.SellDate descending
        //                     select new SaleWinFormDto
        //                     {
        //                         SaleId = s.Id,
        //                         ProductId = s.ProductId,
        //                         MehsulAdi = p.ProductName,
        //                         Istifadeci = $"{u.FirstName} {u.LastName}",
        //                         SatilanQiymet = s.SoldPrice,
        //                         Miqdar = s.Quantity,
        //                         Cem = s.TotalPrice,
        //                         //s.SoldPrice * s.Quantity,
        //                         Tarix = s.SellDate

        //                     };

        //        return filter == null ? result.ToList() : result.Where(filter).ToList();
        //    }
        //}

        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByTopSelling(Expression<Func<SaleWinFormDto, bool>> filter = null)
        //{
        //    using (StockTrackingProjectContext context = new StockTrackingProjectContext())
        //    {
        //        IQueryable <SaleWinFormDto> result = from s in context.SalesWinForms
        //                     join p in context.Products on s.ProductId equals p.Id
        //                     join u in context.Users on s.UserId equals u.Id

        //                     select new SaleWinFormDto
        //                     {
        //                         SaleId = s.Id,
        //                         ProductId = s.ProductId,
        //                         MehsulAdi = p.ProductName,
        //                         Istifadeci = $"{u.FirstName} {u.LastName}",
        //                         SatilanQiymet = s.SoldPrice,
        //                         Miqdar = s.Quantity,
        //                         Cem = s.TotalPrice,
        //                         //s.SoldPrice * s.Quantity,
        //                         Tarix = s.SellDate

        //                     };


        //        return filter == null ? result.ToList() : result.Where(filter).ToList();
        //    }
        //}

        //public List<SaleWinFormDto> GetAllWinFormDtoDetailsByTopSellinQuery(Expression<Func<SaleWinFormDto, bool>> filter = null)
        //{
        //    using (StockTrackingProjectContext context = new StockTrackingProjectContext())
        //    {
        //        IQueryable<SaleWinFormDto> query = (
        //                       from s in context.SalesWinForms
        //                       join p in context.Products on s.ProductId equals p.Id
        //                       group s by p.Id into g
        //                       select new SaleWinFormDto { MehsulAdi = p.ProductName, Miqdar=g.Count() }
        //                     );

        //        return query.ToList();
        //    }
        //}
    }
}

