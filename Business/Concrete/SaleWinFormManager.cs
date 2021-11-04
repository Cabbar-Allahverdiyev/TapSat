﻿using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SaleWinFormManager : ISaleWinFormService
    {
        ISaleWinFormDal _saleWinFormDal;
        public SaleWinFormManager(ISaleWinFormDal saleWinFormDal)
        {
            _saleWinFormDal = saleWinFormDal;
        }

        [ValidationAspect(typeof(SaleWinFormValidator))]
        [CacheRemoveAspect("ISaleWinFormService.Get")]
        public IResult Add(SaleWinForm saleWinForm)
        {
            saleWinForm.SellDate = DateTime.Now;
            _saleWinFormDal.Add(saleWinForm);
            return new SuccessResult(SaleMessages.Added);
        }

        [ValidationAspect(typeof(SaleWinFormValidator))]
        public IResult Update(SaleWinForm saleWinForm)
        {
            saleWinForm.SellDate = DateTime.Now;
            _saleWinFormDal.Update(saleWinForm);
            return new SuccessResult(SaleMessages.Updated);
        }

        [CacheRemoveAspect("ISaleWinFormService.Get")]
        public IResult Delete(SaleWinForm saleWinForm)
        {
            _saleWinFormDal.Delete(saleWinForm);
            return new SuccessResult(SaleMessages.Deleted);
        }

        [CacheAspect]
        public IDataResult<List<SaleWinForm>> GetAll()
        {
            List<SaleWinForm> get = _saleWinFormDal.GetAll();
            if (get is null)
            {
                return new ErrorDataResult<List<SaleWinForm>>(SaleMessages.NotFound);
            }
            return new SuccessDataResult<List<SaleWinForm>>(get, SaleMessages.Found);
        }


        //Metodlar----------------------->
        public IDataResult<List<SaleWinFormDto>> GetAllSaleWinFormDtoDetails()
        {
            List<SaleWinFormDto> get = _saleWinFormDal.GetAllWinFormDtoDetails();
            if (get is null)
            {
                return new ErrorDataResult<List<SaleWinFormDto>>(SaleMessages.NotFound);
            }
            return new SuccessDataResult<List<SaleWinFormDto>>(get,SaleMessages.Found);
        }
    }
}