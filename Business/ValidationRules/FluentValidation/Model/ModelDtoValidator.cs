using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.Model
{
    public class ModelDtoValidator : AbstractValidator<ModelDto>
    {
        public ModelDtoValidator()
        {
            RuleFor(md => md.CostVariableId).NotEmpty().When(cv => cv.CostVariableId <= 0).WithMessage("Maliyet değişkeni seçiniz.").WithName("Maliyet Değişkeni");
            RuleFor(md => md.ModelId).NotEmpty().When(m => m.ModelId <= 0).WithMessage("Model seçiniz.").WithName("Model");
            RuleFor(md => md.ModelMostSizeKg).NotEmpty().MaximumLength(25).MinimumLength(4).WithMessage("Model En Boy Ağırlığı en az 4 karakter en fazla 25 karakter olmalıdır.");
            RuleFor(md => md.ModelProductionTime).NotEmpty().When(m => m.ModelProductionTime <= 0).WithMessage("Model üretim süresi 0 ve 0 dan düşük olamaz").WithName("Üretim süresi");
            RuleFor(md => md.ModelShateIronWeight).NotEmpty().When(m => m.ModelShateIronWeight <= 0).WithMessage("Saç ağırlığı 0 ve 0 dan düşük olamaz.");
            RuleFor(md => md.ModelIProfilWeight).NotEmpty().When(m => m.ModelIProfilWeight <= 0).WithMessage("I Profil ağırlığı 0 ve 0 dan düşük olamaz.");
        }
    }
}
