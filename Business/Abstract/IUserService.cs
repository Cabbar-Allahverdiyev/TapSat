﻿using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Results;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);

        IDataResult<User> GetByMail(string email);
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password,string passwordRepeeat);

        //DTOs
        IDataResult<List<UserDto>> GetUserDetails();
        IDataResult<List<UserDto>> GetUserDetailsByUserId(int userId);
        IDataResult<UserDto> GetUserDetail(int userId);

    }
}
