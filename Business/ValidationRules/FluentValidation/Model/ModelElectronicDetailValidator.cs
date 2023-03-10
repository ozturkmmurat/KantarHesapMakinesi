using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.Model
{
    public class ModelElectronicDetailValidator : AbstractValidator<ModelElectronicDetail>
    {
        public ModelElectronicDetailValidator()
        {
            RuleFor(med => med.ModelId).NotEmpty().When(m => m.ModelId <= 0).WithMessage("Model Seçiniz.").WithName("Model");
            RuleFor(med => med.ElectronicId).NotEmpty().When(e => e.ElectronicId <= 0).WithMessage("Elektronik Seçiniz.").WithName("Elektronik");
            RuleFor(med => med.ElectronicPcs).NotEmpty().When(e => e.ElectronicPcs <= 0).WithMessage("Elektronik adeti 0 ve 0 dan daha düşük olamaz").WithName("Elektronik Adı");
        }
    }
}
