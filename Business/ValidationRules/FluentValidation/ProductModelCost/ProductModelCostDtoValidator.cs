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
            RuleFor(pmcd => pmcd.ProfitPercentage).NotEmpty().When(pmcd => pmcd.ProfitPercentage <= 0).WithMessage("Kar yüzdesi 0 ve 0 dan düşük olamaz.");
        }
    }
}
