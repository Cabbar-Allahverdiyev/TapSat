﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Entities.DTOs.ProductDtos.ForAPI;
using Entities.DTOs.ProductDtos.ForWinForms;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, StockTrackingProjectContext>
                               , IProductDal
    {

        public List<ProductViewDetailDto> GetProductViewDetails(Expression<Func<ProductViewDetailDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             select new ProductViewDetailDto
                             {
                                 ProductId = p.Id,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 TedarikciSirket = s.CompanyName,
                                 TedarikEdenInsan = s.ContactName,
                                 TedarikciNeZamanGelir = s.WhenWillCome,
                                 StokdakiVahid = p.UnitsInStock,
                                 SifaristekiVahid = p.UnitsOnOrder,
                                 YeniSifarisEdedi = p.ReorderLevel,
                                 AlisQiymeti = p.PurchasePrice,
                                 Qiymet = p.UnitPrice,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber,
                                 Kemiyyet = p.QuantityPerUnit,
                                 Aciqlama = p.Description,
                                 SonDeyistirilmeTaixi = p.LastModifiedDate,
                                 Sonlanmis = p.Discontinued

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

        public List<ProductViewDashboardDetailDto> GetProductViewDashboardDetails(Expression<Func<ProductViewDashboardDetailDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             orderby p.Id
                             select new ProductViewDashboardDetailDto
                             {
                                 ProductId = p.Id,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 TedarikciSirket = s.CompanyName,
                                 StokdakiVahid = p.UnitsInStock,
                                 Qiymet = p.UnitPrice,
                                 AlisQiymet = p.PurchasePrice,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber,
                                 Kemiyyet = p.QuantityPerUnit,
                                 Aciqlama = p.Description,
                                 SonDeyistirilmeTaixi = p.LastModifiedDate,
                                 Sonlanmis = p.Discontinued

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

        public List<ProductViewUserDashboardDetailDto> GetProductViewUserDashboardDetails(Expression<Func<ProductViewUserDashboardDetailDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             orderby p.Id
                             select new ProductViewUserDashboardDetailDto
                             {
                                 ProductId = p.Id,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 TedarikciSirket = s.CompanyName,
                                 StokdakiVahid = p.UnitsInStock,
                                 Qiymet = p.UnitPrice,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber,
                                 Kemiyyet = p.QuantityPerUnit,
                                 Aciqlama = p.Description,
                                 SonDeyistirilmeTaixi = p.LastModifiedDate,
                                 Sonlanmis = p.Discontinued

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

        public List<ProductCompactDetailDto> GetProductCompactDetails(Expression<Func<ProductCompactDetailDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join b in context.Brands on p.BrandId equals b.Id
                             select new ProductCompactDetailDto
                             {
                                 ProductId = p.Id,
                                 Marka = b.BrandName,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }
        public ProductCompactDetailDto GetByProductCompactDetail(Expression<Func<ProductCompactDetailDto, bool>> filter)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join b in context.Brands on p.BrandId equals b.Id
                             select new ProductCompactDetailDto
                             {
                                 ProductId = p.Id,
                                 Marka = b.BrandName,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber
                             };
                return result.SingleOrDefault(filter);

            }
        }
        public ProductViewDetailDto GetProductIdProductViewDetail(int productId)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products.Where(p => p.Id == productId)
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             select new ProductViewDetailDto
                             {
                                 ProductId = p.Id,
                                 Kateqoriya = c.CategoryName,
                                 Marka = b.BrandName,
                                 TedarikciSirket = s.CompanyName,
                                 TedarikEdenInsan = s.ContactName,
                                 StokdakiVahid = p.UnitsInStock,
                                 SifaristekiVahid = p.UnitsOnOrder,
                                 YeniSifarisEdedi = p.ReorderLevel,
                                 AlisQiymeti = p.PurchasePrice,
                                 Qiymet = p.UnitPrice,
                                 MehsulAdi = p.ProductName,
                                 BarcodeNomresi = p.BarcodeNumber,
                                 Kemiyyet = p.QuantityPerUnit,
                                 Aciqlama = p.Description,
                                 SonDeyistirilmeTaixi = p.LastModifiedDate,
                                 Sonlanmis = p.Discontinued

                             };

                return result.SingleOrDefault();


            }
        }


        public ProductDetailDto GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 CategoryName = c.CategoryName,
                                 BrandName = b.BrandName,
                                 SupplierName = s.CompanyName,
                                 SupplierContactName = s.ContactName,
                                 UnitsInStock = p.UnitsInStock,
                                 UnitsOnOrder = p.UnitsOnOrder,
                                 ReorderLevel = p.ReorderLevel,
                                 PurchasePrice = p.PurchasePrice,
                                 UnitPrice = p.UnitPrice,
                                 ProductName = p.ProductName,
                                 BarcodeNumber = p.BarcodeNumber,
                                 QuantityPerUnit = p.QuantityPerUnit,
                                 Description = p.Description,
                                 LastModifiedDate = p.LastModifiedDate,
                                 Discontinued = p.Discontinued

                             };
                return result.SingleOrDefault(filter);

            }
        }

        public List<ProductDetailDto> GetAllProductDetail(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.Id
                             join b in context.Brands on p.BrandId equals b.Id
                             join s in context.Suppliers on p.SupplierId equals s.Id
                             select new ProductDetailDto
                             {
                                 Id = p.Id,
                                 CategoryName = c.CategoryName,
                                 BrandName = b.BrandName,
                                 SupplierName = s.CompanyName,
                                 SupplierContactName = s.ContactName,
                                 UnitsInStock = p.UnitsInStock,
                                 UnitsOnOrder = p.UnitsOnOrder,
                                 ReorderLevel = p.ReorderLevel,
                                 PurchasePrice = p.PurchasePrice,
                                 UnitPrice = p.UnitPrice,
                                 ProductName = p.ProductName,
                                 BarcodeNumber = p.BarcodeNumber,
                                 QuantityPerUnit = p.QuantityPerUnit,
                                 Description = p.Description,
                                 LastModifiedDate = p.LastModifiedDate,
                                 Discontinued = p.Discontinued

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }

        public List<ExcelDto> GetAllProductExcelDetail(Expression<Func<ExcelDto, bool>> filter = null)
        {
            using (StockTrackingProjectContext context = new StockTrackingProjectContext())
            {
                var result = from p in context.Products
                             select new ExcelDto
                             {
                                 Id = p.Id,
                                 UnitsInStock = p.UnitsInStock,
                                 PurchasePrice = p.PurchasePrice,
                                 UnitPrice = p.UnitPrice,
                                 ProductName = p.ProductName,
                                 BarcodeNumber = p.BarcodeNumber,
                                 Description = p.Description,
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();

            }
        }


    }
}
