﻿using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICartService
    {
        IDataResult<List<Cart>> GetAll();
        IDataResult<CartDto> GetCartDtoDetailByProductId(int productId);
        IDataResult<CartAddDto> GetCartAddDetailByBarcodeNumber(int barcodeNumber);
        IDataResult<CartAddDto> GetCartAddDetailByProductId(int productId);
        IResult Add(Cart cart);
        IResult Update(Cart cart);
        IResult Delete(Cart cart);

        IResult ByUserIdAllRemove(int userId);
        IDataResult<List<CartViewDto>> GetAllCartViewDetailsByUserId(int userId);

       
    }
}