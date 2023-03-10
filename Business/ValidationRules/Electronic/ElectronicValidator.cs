using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.Electronic
{
    public class ElectronicValidator : AbstractValidator<Entities.Concrete.Electronic>
    {
        public ElectronicValidator()
        {
            RuleFor(e => e.ElectronicName).NotEmpty().MinimumLength(3).WithMessage("Elektronik Adı minimum 3 karakter olabilir").MaximumLength(20).WithMessage("Elektronik adı maksimum 20 karakter olmalıdır.").WithName("Elektronik Adı");
            RuleFor(e => e.ElectronicEuroPrice).NotEmpty().WithMessage("Elektronik Euro fiyatı boş bırakılamaz").WithName("Elektronik Euro Fiyatı");
            RuleFor(e => e.ElectronicEuroPrice).NotEmpty().When(e => e.ElectronicEuroPrice < 0).WithMessage("Elektronik Euro Fiyatı 0 dan düşük girilemez").WithName("Elektronik Euro Fiyatı");
        }
    }
}
