using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.AccessoryPackage
{
    public class AccessoryPackageValidator : AbstractValidator<Entities.Concrete.AccessoryPackage>
    {
        public AccessoryPackageValidator()
        {
            RuleFor(ap => ap.AccessoryPackageName).MinimumLength(4).WithMessage("Aksesuar Paket adı minimum 4 karakter olabilir").WithName("Aksesuar Paket Adı");
            RuleFor(ap => ap.AccessoryPackageName).NotEmpty().WithMessage("Aksesuar Paket adı boş olamaz").WithName("Aksesuar Paket Adı");
        }
    }
}
