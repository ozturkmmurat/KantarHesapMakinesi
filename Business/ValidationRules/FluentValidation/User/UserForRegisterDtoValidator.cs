using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation.User
{
    public class UserForRegisterDtoValidator : AbstractValidator<UserForRegisterDto>
    {
        public UserForRegisterDtoValidator()
        {
            RuleFor(u => u.Password).NotEmpty().MinimumLength(6).MaximumLength(20).WithMessage("Şifreniz en az 6 karakter en fazla 20 karakter olabilir.").WithName("Şifre");
            RuleFor(u => u.Password).NotEmpty().Matches("[A-Z]").WithMessage("Şifreniz bir veya daha fazla büyük harf içermelidir.")
            .Matches(@"\d").WithMessage("Şifreniz bir veya daha fazla rakam içermelidir.").WithName("Şifre");
        }
    }
}
