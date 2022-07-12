﻿using Core.Entities.Concrete;
using Core.Utilities;
using Core.Utilities.Results;
using Entities.Concrete.ForForms;
using Entities.DTOs.UserDtos;
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
        IDataResult<User> GetById(int id);

       IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<List<OperationClaimForForms>> GetClaimsForForms(User user);
        IResult CheckUserOperationClaimIsBossByUserIdForForms(int userId);
        IResult CheckUserOperationClaimIsBossAndAdminByUserIdForForms(int userId);

        IDataResult<User> GetByMail(string email);
        IResult UserExists(string email);

       

        //DTOs
        IDataResult<List<UserDto>> GetUserDetails();
        IDataResult<List<UserDto>> GetUserDetailsByUserName(string userName);
        IDataResult<UserDto> GetUserDetailsByUserId(int userId);
       // IDataResult<UserDto> GetUserDetail(int userId);

        //For Forms
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto
            //, string passwordRepeeat
           );
        IDataResult<User> Login(UserForLoginDto userForLoginDto);

    }
}
