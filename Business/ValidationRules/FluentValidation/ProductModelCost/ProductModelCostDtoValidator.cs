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
            //RuleFor(pmcd => pmcd.InstallationCostId).NotEmpty().When(i => i.InstallationCostId <= 0).WithMessage("Kurulum yeri seçiniz.").WithName("");
            RuleFor(pmcd => pmcd.ProductModelCostOverheadPercentage).NotEmpty().When(pmcd => pmcd.ProductModelCostOverheadPercentage <= 0).WithMessage("Genel gider yüzdesi 0 ve 0 dan düşük olamaz.");
            RuleFor(pmcd => pmcd.ProductModelCostLaborCostPerHour).NotEmpty().When(pmcd => pmcd.ProductModelCostLaborCostPerHour <= 0).WithMessage("Saatlik işçilik ücreti 0 ve 0 dan düşük olamaz.");
            RuleFor(pmcd => pmcd.ProductModelCostDetailProfitPercentage).NotEmpty().When(pmcd => pmcd.ProductModelCostDetailProfitPercentage <= 0).WithMessage("Kar yüzdesi 0 ve 0 dan düşük olamaz.");
            RuleFor(pmcd => pmcd.ProductModelCostDetailTurkeySalesDiscount).NotEmpty().When(pmcd => pmcd.ProductModelCostDetailTurkeySalesDiscount < 0).WithMessage("Türkiye satış indirimi 0 dan düşük olamaz.");
            RuleFor(pmcd => pmcd.ProductModelCostDetailExportFinalDiscount).NotEmpty().When(pmcd => pmcd.ProductModelCostDetailExportFinalDiscount < 0).WithMessage("İhracat satış indirimi 0 dan düşük olamaz.");
        }
    }
}
