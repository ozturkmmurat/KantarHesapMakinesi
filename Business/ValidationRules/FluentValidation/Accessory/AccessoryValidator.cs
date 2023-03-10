using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.Accessory
{
    public class AccessoryValidator : AbstractValidator<Entities.Concrete.Accessory>
    {
        public AccessoryValidator()
        {
            RuleFor(a => a.AccessoryEuroPrice).NotEmpty().WithMessage("Aksesuar Euro Fiyatı boş geçilemez").WithName("Aksesuar Euro Fiyatı");
            RuleFor(a => a.AccessoryEuroPrice).GreaterThan(0).When(a => a.AccessoryEuroPrice < 0).WithMessage("Aksesuar Euro Fiyatı 0 ve 0 dan düşük bir fiyat olamaz").WithName("Aksesuar Euro Fiyatı");
            RuleFor(a => a.AccessoryName).NotEmpty().WithMessage("Aksesuar adını giriniz.").WithName("Aksesuar Adı");
            RuleFor(a => a.AccessoryName).MinimumLength(4).WithMessage("Aksesuar adı en az 4 karakter olmalı.").WithName("Aksesuar Adı");
        }
    }
}
