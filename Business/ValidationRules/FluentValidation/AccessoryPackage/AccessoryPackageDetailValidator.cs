using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.AccessoryPackage
{
    public class AccessoryPackageDetailValidator :  AbstractValidator<AccessoryPackageDetail>
    {
        public AccessoryPackageDetailValidator()
        {
            RuleFor(apd => apd.AccessoryId).NotEmpty().When(a => a.Id <=0).WithMessage("Aksesuar Seçiniz").WithName("Aksesuar");
            RuleFor(apd => apd.AccessoryPcs).NotEmpty().When(a => a.AccessoryPcs <= 0).WithMessage("Aksesuar Adeti 0 olamaz.").WithName("Aksesuar Adeti");
            RuleFor(apd => apd.AccessoryPackageId).NotEmpty().When(a => a.AccessoryPackageId <= 0).WithMessage("Aksesuar Paketi seçiniz.").WithName("Aksesuar Paketi");
        }
    }
}
