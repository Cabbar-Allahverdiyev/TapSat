﻿using Core.CrossCuttingConcerns.Validation.Abstract;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.SaleValidators
{
    public class UpdateSaleValidator : AbstractValidator<Sale>, IEntityValidator
    {
        public UpdateSaleValidator()
        {
            
            RuleFor(s => s.UserId).NotEmpty();
            RuleFor(s => s.UserId).GreaterThan(0);
            RuleFor(s => s.ProductId).NotEmpty();
            RuleFor(s => s.Quantity).NotEmpty();
            RuleFor(s => s.Quantity).GreaterThan(0);
        }
    }
}
