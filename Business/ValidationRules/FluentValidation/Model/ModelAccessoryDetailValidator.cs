using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.Model
{
    public class ModelAccessoryDetailValidator : AbstractValidator<ModelAccessoryDetail>
    {
        public ModelAccessoryDetailValidator()
        {
            RuleFor(mad => mad.ModelId).NotEmpty().When(m => m.ModelId <= 0).WithMessage("Model Seçiniz.").WithName("Model");
            RuleFor(mad => mad.AccessoryPackageDetailId).NotEmpty().When(ap => ap.AccessoryPackageDetailId <= 0).WithMessage("Aksesuar Paketi seçiniz.").WithName("Aksesuar Paketi");
        }
    }
}
