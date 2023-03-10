using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }
        [ValidationAspect(typeof(UserForRegisterDtoValidator))]
        public IDataResult<Core.Entities.Concrete.User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;

            if (_userService.GetByMail(userForRegisterDto.Email) != null)
            {
                return new ErrorDataResult<Core.Entities.Concrete.User>(Messages.CurrentMail);
            }

            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new Core.Entities.Concrete.User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = false
            };
            _userService.Add(user);
            return new SuccessDataResult<Core.Entities.Concrete.User>(user, "Kayıt oldu");
        }

        public IDataResult<Core.Entities.Concrete.User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);

            IResult result = BusinessRules.Run(_userService.CheckPassword(userForLoginDto.Email, userForLoginDto.Password), _userService.CheckEmail(userForLoginDto.Email), CheckStatus(userForLoginDto.Email));
            if (result == null)
            {
                return new SuccessDataResult<Core.Entities.Concrete.User>(userToCheck, "Başarılı giriş");
            }
            return new ErrorDataResult<Core.Entities.Concrete.User>("Email ve şifreyi kontrol ediniz");

        }

        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("Kullanıcı mevcut");
            }
            return new SuccessResult();
        }

        public IDataResult<AccessToken> CreateAccessToken(Core.Entities.Concrete.User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu.");
        }

        public IResult CheckStatus(string email)
        {
            var statusCheck = _userService.GetByMail(email);
            if (statusCheck.Status != false)
            {
                return new SuccessResult();
            }
            return new ErrorResult("Yetkililer tarafından hesabınızın aktif hale getirilmesi gerekmektedir.");
        }
    }
}
