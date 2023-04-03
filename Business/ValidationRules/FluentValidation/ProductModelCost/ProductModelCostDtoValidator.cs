using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.ProductModelCost
{
    public class ProductModelCostDtoValidator : AbstractValidator<ProductModelCostDto>
    {
        public ProductModelCostDtoValidator()
        {
            RuleFor(pmcd => pmcd.ProductModelCostProfitPercentage).NotEmpty().When(pmcd => pmcd.ProductModelCostProfitPercentage <= 0).WithMessage("Kar yüzdesi 0 ve 0 dan düşük olamaz.");
        }
    }
}
