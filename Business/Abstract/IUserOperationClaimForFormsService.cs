﻿using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.ForForms;
using Entities.DTOs.UserOperationClaimDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserOperationClaimForFormsService
    {
        IDataResult<List<UserOperationClaimForForms>> GetAll();
        IResult Add(UserOperationClaimForForms operationClaim);
        IResult Update(UserOperationClaimForForms operationClaim);
        IResult Delete(int id);
        IDataResult<UserOperationClaimForForms> GetById(int id);

        IDataResult<UserOperationClaimForForms> GetByUserId(int userId);

        IDataResult<List<UserOperationClaimDto>> GetAllUserOperationClaimDtoDetails();
        IDataResult<List<UserOperationClaimForForms>> GetAllOperationClaimByUserId(int userId);
    }
}
