using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.CostVariable
{
    public class CostVariableValidator : AbstractValidator<Entities.Concrete.CostVariable>
    {
        public CostVariableValidator()
        {
            //RuleFor(cv => cv.CostVariableName).NotEmpty().WithMessage("Maliyet adı boş bırakılamaz.").WithName("Maliyet Değişken Adı");
            //RuleFor(cv => cv.CostVariableName).MinimumLength(3).MaximumLength(20).WithMessage("Maliyet Değişkeninin adı en az 3 karakter en fazla 20 karakter olmalıdır.").WithName("Maliyet Değişken Adı");
            //RuleFor(cv => cv.IProfile).NotEmpty().WithMessage("I Profil değeri boş bırakılamaz.").WithName("I Profil");
            //RuleFor(cv => cv.ShateIron).NotEmpty().WithMessage("Saç değeri boş bırakılamaz. ").WithName("Saç");
            //RuleFor(cv => cv.FireTotalPercentAge).NotEmpty().WithMessage("Fire Toplam Ağırlık yüzdesi boş bırakılamaz.");
            //RuleFor(cv => cv.FirePercentAge).NotEmpty().WithMessage("Fire yüzdesi boş bırakılamaz").WithName("Fire Yüzdesi");
            //RuleFor(cv => cv.FireShateIronAndIProfilePercentage).NotEmpty().WithMessage("Fireli I Profil ve Saç değeri boş bırakılamaz");
        }
    }
}
