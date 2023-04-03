using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.InstallationCost
{
    public class InstallationCostValidator : AbstractValidator<Entities.Concrete.InstallationCost>
    {
        public InstallationCostValidator()
        {
            RuleFor(ic => ic.LocationId).NotEmpty().When(ic => ic.LocationId <= 0).WithMessage("Kurulum yeri boş bırakılamaz.").WithName("Lokasyon");
            RuleFor(ic => ic.InstallationEuroPrice).NotEmpty().When(ic => ic.InstallationEuroPrice < 0).WithMessage("Kurulum Euro Ücreti 0 dan düşük girilemez.").WithName("Kurulum Ücreti");
        }
    }
}
