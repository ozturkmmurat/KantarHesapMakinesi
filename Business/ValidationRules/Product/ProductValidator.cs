using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.Product
{
    public class ProductValidator : AbstractValidator<Entities.Concrete.Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty().MinimumLength(3).MaximumLength(25).WithMessage("Ürün adı en az 3 en fazla 25 karakter olmalıdır.").WithName("Ürün Adı");
        }
    }
}
