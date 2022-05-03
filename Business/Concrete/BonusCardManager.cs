﻿using Business.Abstract;
using Business.Constants.Messages;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BonusCardManager : IBonusCardService
    {

        IBonusCardDal _bonusCardDal;
        ICustomerService _customerService;
        public BonusCardManager(IBonusCardDal bonusCardDal, ICustomerService customerService)
        {
            _bonusCardDal = bonusCardDal;
            _customerService = customerService;
        }
        //CRUD
        [ValidationAspect(typeof(BonusCardValidator))]
        [CacheRemoveAspect("IBonusCardService.Get")]
        public IResult Add(BonusCard bonusCard)
        {
            IResult result = GetByCustomerId(bonusCard.CustomerId);
            if (result.Success)
            {
                return new ErrorResult(BonusCardMessages.ThisCustomerAlreadyExistsABonusCard);
            }
          

            _bonusCardDal.Add(bonusCard);
            return new SuccessResult(BonusCardMessages.Added);
        }

        [CacheRemoveAspect("IBonusCardService.Get")]
        public IResult Delete(BonusCard bonusCard)
        {
            _bonusCardDal.Delete(bonusCard);
            return new SuccessResult(BonusCardMessages.Deleted);
        }

        [ValidationAspect(typeof(BonusCardValidator))]
        [CacheRemoveAspect("IBonusCardService.Get")]
        public IResult Update(BonusCard bonusCard   )
        {
            _bonusCardDal.Update(bonusCard);
            return new SuccessResult(BonusCardMessages.Updated);
        }

        [CacheAspect]
        public IDataResult<List<BonusCard>> GetAll()
        {
            List<BonusCard> get = _bonusCardDal.GetAll();
            return new SuccessDataResult<List<BonusCard>>(get,BonusCardMessages .GetAll);
        }

        [CacheAspect]
        public IDataResult<BonusCard> GetById(int id)
        {
            BonusCard get = _bonusCardDal.Get(b => b.Id == id);
            if (get == null)
            {
                return new ErrorDataResult<BonusCard>(BonusCardMessages.NotFound);
            }
            return new SuccessDataResult<BonusCard>(get, BonusCardMessages.Found);
        }

        //Elave--------------->
        public  IDataResult<BonusCard> GetByCustomerId(int customerId)
        {

            BonusCard get = _bonusCardDal.Get(b => b.CustomerId == customerId);
            if (get == null)
            {
                return new ErrorDataResult<BonusCard>(BonusCardMessages.NotFound);
            }
            return new SuccessDataResult<BonusCard>(get, BonusCardMessages.Found);
        }

        [CacheRemoveAspect("IBonusCardService.Get")]
        public IResult IncreaseBalance(int customerId, decimal value)
        {
            IDataResult<BonusCard> getBonusCard = GetByCustomerId(customerId);
            if (!getBonusCard.Success)
            {
                return new ErrorResult(BonusCardMessages.ThisCustomerDoesNotHaveABonusCard);
            }
            BonusCard bonusCard = getBonusCard.Data;
            bonusCard.Balance += value;
            Customer customer = _customerService.GetById(bonusCard.CustomerId).Data;
            IResult result = Update(bonusCard);
            if (!result.Success)
            {
                return new ErrorResult(BonusCardMessages.NotIncreaseBalance(customer.FirstName));
            }
           
            return new SuccessResult(BonusCardMessages.IncreaseBalance(customer.FirstName));
        }

        public IResult ReduceBalance(int customerId, decimal value)
        {
            IDataResult<BonusCard> getBonusCard = GetByCustomerId(customerId);
            if (!getBonusCard.Success)
            {
                return new ErrorResult(BonusCardMessages.ThisCustomerDoesNotHaveABonusCard);
            }
            BonusCard bonusCard = getBonusCard.Data;
            bonusCard.Balance -= value;
            Customer customer = _customerService.GetById(bonusCard.CustomerId).Data;
            IResult result = Update(bonusCard);
            if (!result.Success)
            {
                return new ErrorResult(BonusCardMessages.NotReduceBalance(customer.FirstName));
            }
            return new SuccessResult(BonusCardMessages.ReduceBalance(customer.FirstName));
        }
    }
}
