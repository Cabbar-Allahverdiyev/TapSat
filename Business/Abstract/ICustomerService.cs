﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs.CustomerDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        IDataResult<List<Customer>> GetAll();
        IResult Add(Customer customer);
        IResult Update(Customer customer);
        IResult Delete(Customer customer);
        IDataResult<Customer> GetById(int id);

        IDataResult<List<CustomerDto>> GetCustomerDetails();
        IDataResult<CustomerDto> GetCustomerDetailByCustomerId(int customerId);
    }
}
