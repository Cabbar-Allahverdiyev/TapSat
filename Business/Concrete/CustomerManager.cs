﻿using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Business.ValidationRules.FluentValidation.BrandValidators;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.DTOs.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {

        ICustomerDal _customerDal;
        ICustomerBalanceService _balanceService;
        IBonusCardDal _bonusCardDal;
        public CustomerManager(ICustomerDal customerDal, ICustomerBalanceService balanceService
                             , IBonusCardDal bonusCardDal)
        {
            _customerDal = customerDal;
            _balanceService = balanceService;
            _bonusCardDal = bonusCardDal;
        }
        //CRUD
        [ValidationAspect(typeof(CustomerValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Add(Customer customer)
        {
            IResult result = BusinessRules.Run(IsThereFirstNameAndLastNameAvailable(customer.FirstName, customer.LastName)
                                                , IsEmailExists(customer.Email)
                                                , IsPhoneNumberExists(customer.PhoneNumber)
                                                ,CustomerFullNameToTitleCase(customer)
                                                );
            if (result != null)
            {
                return new ErrorDataResult<Customer>(result.Message);
            }

            customer.CreatedDate = DateTime.Now;
            CustomerBalance customerBalance = new CustomerBalance();
            customerBalance.Balance = 0;
            customerBalance.Debt = 0;

            _customerDal.Add(customer);

            IDataResult<Customer> get = GetByPhoneNumber(customer.PhoneNumber);
            customerBalance.CustomerId = get.Data.Id;
            _balanceService.Add(customerBalance);
            return new SuccessResult(CustomerMessages.Added);
        }



        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(Customer customer)
        {
            string message = "";
            IResult rules = BusinessRules.Run(//sGetBonusCardByCustomerIdAndDelete(customer,ref message)
                                              //,GetBalanceByCustomerIdAndDelete(customer,ref message)
                                             );
            if (rules != null)
            {
                return new ErrorResult(rules.Message);
            }

            _customerDal.Delete(customer);
            message += CustomerMessages.Deleted;
            return new SuccessResult(message);
        }

        [ValidationAspect(typeof(CreateBrandValidator))]
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            IResult result = BusinessRules.Run(IsThereFirstNameAndLastNameAvailableForUserUpdate(customer)
                                              , IsEmailExistsForUserUpdate(customer)
                                              , IsPhoneNumberExistsForUserUpdate(customer)
                                              , CustomerFullNameToTitleCase(customer)
                                              );

            if (result != null)
            {
                return new ErrorDataResult<Customer>(result.Message);
            }

            _customerDal.Update(customer);
            return new SuccessResult(CustomerMessages.Updated);
        }

        [CacheAspect]
        public IDataResult<List<Customer>> GetAll()
        {
            List<Customer> get = _customerDal.GetAll();
            return new SuccessDataResult<List<Customer>>(get, CustomerMessages.GetAll);
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int id)
        {
            Customer get = _customerDal.Get(c => c.Id == id);
            if (get == null)
            {
                return new ErrorDataResult<Customer>(CustomerMessages.NotFound);
            }
            return new SuccessDataResult<Customer>(get, CustomerMessages.Found);
        }

        [CacheAspect]
        public IDataResult<Customer> GetByPhoneNumber(string phoneNumber)
        {
            Customer get = _customerDal.Get(c => c.PhoneNumber.Equals(phoneNumber));
            if (get == null)
            {
                return new ErrorDataResult<Customer>(CustomerMessages.NotFound);
            }
            return new SuccessDataResult<Customer>(get, CustomerMessages.Found);
        }

        //Dtos---------------------------------------->
        public IDataResult<List<CustomerDto>> GetCustomerDetails()
        {
            List<CustomerDto> get = _customerDal.GetCustomerDetails();
            List<BonusCard> getCards = _bonusCardDal.GetAll();
            foreach (var customer in get)
            {
                var card = getCards.SingleOrDefault(b => b.CustomerId == customer.CustomerId);
                if (card != null)
                {
                    customer.BonusCardBalance = card.Balance;
                }
            }
            //if (get == null)
            //{
            //    return new ErrorDataResult<List<CustomerDto>>(CustomerMessages.NotFound);
            //}
            return new SuccessDataResult<List<CustomerDto>>(get, CustomerMessages.GetAll);
        }

        public IDataResult<CustomerDto> GetCustomerDetailByCustomerId(int customerId)
        {
            CustomerDto get = _customerDal.GetCustomerDetail(c=>c.CustomerId==customerId);
            if (get is null)
            {
                return new ErrorDataResult<CustomerDto>(CustomerMessages.CustomerDetailNotFound);
            }
            BonusCard bonusCard = _bonusCardDal.Get(b => b.CustomerId == get.CustomerId);
            if (bonusCard != null)
            {
                get.BonusCardBalance = bonusCard.Balance;
            }
            return new SuccessDataResult<CustomerDto>(get,CustomerMessages.CustomerDetailFound);
        }

        //Elave-------------------->
        private IResult IsThereFirstNameAndLastNameAvailable(string firstName, string lastName)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer customer in customerGetAll)
            {
                
                if (customer.FirstName.ToLower(new CultureInfo("en-Us", false)).Equals(firstName.ToLower(new CultureInfo("en-Us", false))))
                {
                    if (customer.LastName.ToLower(new CultureInfo("en-Us", false)).Equals(lastName.ToLower(new CultureInfo("en-Us", false))))
                    {
                        return new ErrorResult(CustomerMessages.FirstNameAndLastNameAvailable);
                    }
                }
            }
            return new SuccessResult();
        }

        private IResult IsThereFirstNameAndLastNameAvailableForUserUpdate(Customer customer)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer item in customerGetAll)
            {
                if (item.FirstName.ToLower().Equals(customer.FirstName.ToLower()) && item.LastName.ToLower().Equals(customer.LastName.ToLower()))
                {
                    if (item.Id != customer.Id)
                    {
                        return new ErrorResult(CustomerMessages.FirstNameAndLastNameAvailable);
                    }

                }
            }
            return new SuccessResult();
        }

        private IResult IsEmailExists(string email)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer customer in customerGetAll)
            {
                if (customer.Email.ToLower().Equals(email.ToLower()) && customer.Email != "")
                {
                    return new ErrorResult(CustomerMessages.EmailAvailable);
                }
            }
            return new SuccessResult();
        }

        private IResult IsEmailExistsForUserUpdate(Customer customer)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer item in customerGetAll)
            {
                if (item.Email.ToLower().Equals(customer.Email.ToLower()) && item.Id != customer.Id)
                {
                    return new ErrorResult(CustomerMessages.EmailAvailable);
                }
            }
            return new SuccessResult();
        }

        //private IResult PhoneNumberFormatControl(string phoneNumber)
        //{
        //    string format = @"^(0(\d{9}))$";
        //    Match match = Regex.Match(phoneNumber, format, RegexOptions.IgnoreCase);
        //    if (match.Success)
        //    {
        //        return new SuccessResult();
        //    }
        //    return new ErrorResult(UserMessages.PhoneNumberFormatIsNotSuitable);
        //}

        private IResult IsPhoneNumberExists(string phoneNumber)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer item in customerGetAll)
            {
                if (item.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()))
                {
                    return new ErrorResult(CustomerMessages.PhoneNumberAvailable);
                }
            }
            return new SuccessResult();
        }

        private IResult IsPhoneNumberExistsForUserUpdate(Customer customer)
        {
            List<Customer> customerGetAll = _customerDal.GetAll();
            foreach (Customer item in customerGetAll)
            {
                if (item.PhoneNumber.ToLower().Equals(customer.PhoneNumber.ToLower()) && item.Id != customer.Id)
                {
                    return new ErrorResult(CustomerMessages.PhoneNumberAvailable);
                }
            }
            return new SuccessResult();
        }
        private IResult GetBonusCardByCustomerIdAndDelete(Customer customer, ref string message)
        {
            var bonusCard = _bonusCardDal.Get(b => b.CustomerId == customer.Id);
            if (bonusCard != null)
            {
                _bonusCardDal.Delete(bonusCard);
                message += BonusCardMessages.Deleted(customer.FirstName + " " + customer.LastName) + " və ";
            }
            return new SuccessResult();
        }

        private IResult GetBalanceByCustomerIdAndDelete(Customer customer, ref string message)
        {
            var balance = _balanceService.GetByCustomerId(customer.Id).Data;
            if (balance != null)
            {
                IResult deletedBalance = _balanceService.Delete(balance);
                message += deletedBalance.Message + " ";
                if (!deletedBalance.Success)
                {
                    return new ErrorResult(message);

                }
            }
            return new SuccessResult();
        }

        private IResult CustomerFullNameToTitleCase(Customer customer)
        {
            customer.FirstName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(customer.FirstName);
            customer.LastName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(customer.LastName);
            return new SuccessResult();
        }


    }
}
