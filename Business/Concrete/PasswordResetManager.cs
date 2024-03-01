using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Business;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Core.Utilities.Security.Hashing;
using Core.Utilities.UniqueCode;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos.User;
using Entities.EntityParameter;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class PasswordResetManager : IPasswordResetService
    {
        IPasswordResetDal _passwordResetDal;
        IUserService _userService;
        IUserDal _userDal;
        IMailService _mailService;
        public PasswordResetManager(
            IPasswordResetDal passwordResetDal,
            IUserService userService,
            IMailService mailService,
            IUserDal userDal)
        {
            _passwordResetDal = passwordResetDal;
            _userService=userService;
            _mailService=mailService;
            _userDal=userDal;
        }
        public IResult Add(PasswordReset passwordReset)
        {
            if (passwordReset != null && !GetByUserId(passwordReset.UserId).Success)
            {
                _passwordResetDal.Add(passwordReset);
                return new SuccessResult(Messages.PasswordResetCode);
            }
            return new ErrorResult(Messages.FailedEmailCheck);
        }

        public IResult CheckCodeExpired(PasswordReset passwordReset)
        {
            if (passwordReset != null)
            {
                if (DateTime.Now < passwordReset.ResetEndDate)
                {
                    return new SuccessResult();
                }
                else
                {
                    return new ErrorResult(Messages.CodeHasExpired);
                }

            }
            return new ErrorResult();
        }

        public IResult Delete(PasswordReset passwordReset)
        {
            if (passwordReset != null)
            {
                _passwordResetDal.Delete(passwordReset);
                return new SuccessResult(Messages.PasswordResetCode);
            }
            return new ErrorResult();
        }

        public IDataResult<List<PasswordReset>> GetAll()
        {
            var result = _passwordResetDal.GetAll(x => x.Code != null && x.Code != "" && DateTime.Now < x.ResetEndDate);
            if (result != null)
            {
                return new SuccessDataResult<List<PasswordReset>>(result);
            }
            return new ErrorDataResult<List<PasswordReset>>();
        }

        public IDataResult<PasswordReset> GetByCode(string code)
        {
            if (code != "" && code != null)
            {
                var result = _passwordResetDal.Get(x => x.Code == code);
                if (result != null)
                {

                    return new SuccessDataResult<PasswordReset>(result);
                }
            }
            return new ErrorDataResult<PasswordReset>();
        }

        public IDataResult<PasswordReset> GetByCodeAndUrl(string code, string codeUrl)
        {
            if (code != "" && code != null && codeUrl != "" && codeUrl != null)
            {
                var result = _passwordResetDal.Get(x => x.Code == code && x.CodeUrl == codeUrl);
                if (result != null)
                {
                    return new SuccessDataResult<PasswordReset>(result);
                }
            }
            return new ErrorDataResult<PasswordReset>();
        }

        public IDataResult<PasswordReset> GetByCodeUrl(string codeUrl)
        {
            if (codeUrl != "" && codeUrl != null)
            {
                var result = _passwordResetDal.Get(x => x.CodeUrl == codeUrl);
                if (result != null)
                {
                    return new SuccessDataResult<PasswordReset>(result);
                }
            }
            return new ErrorDataResult<PasswordReset>();
        }

        public IDataResult<PasswordReset> GetByUrl(string url)
        {
            if (url != "" && url != null)
            {
                var result = _passwordResetDal.Get(x => x.Url == url);
                if (result != null)
                {
                    return new SuccessDataResult<PasswordReset>(result);
                }
            }

            return new ErrorDataResult<PasswordReset>();
        }

        public IDataResult<PasswordReset> GetByUserId(int userId)
        {
            var result = _passwordResetDal.Get(x => x.UserId == userId);
            if (result != null)
            {
                return new SuccessDataResult<PasswordReset>(result);
            }
            return new ErrorDataResult<PasswordReset>();
        }

        [ValidationAspect(typeof(UserPasswordResetDtoValidator))]
        public IResult PasswordReset(UserPasswordResetDto userPasswordResetDto)
        {
            var checkLink = GetByUrl(userPasswordResetDto.Link);
            if (checkLink.Success)
            {
                var checkExpiration = CheckCodeExpired(checkLink.Data);
                IResult rulesResult = BusinessRules.Run(checkExpiration);
                if (rulesResult != null)
                {
                    return new ErrorResult(rulesResult.Message);
                }

                var user = _userService.GetById(checkLink.Data.UserId);
                if (user.Success)
                {
                    UserForUpdateDto userForUpdateDto = new UserForUpdateDto()
                    {
                        UserId = user.Data.Id,
                        FirstName = user.Data.FirstName,
                        LastName = user.Data.LastName,
                        Email = user.Data.Email,
                        NewPassword = userPasswordResetDto.NewPassword,
                        Status = user.Data.Status,
                    };

                    byte[] passwordHash, passwordSalt;
                    HashingHelper.CreatePasswordHash(userForUpdateDto.NewPassword, out passwordHash, out passwordSalt);
                    user.Data.PasswordHash = passwordHash;
                    user.Data.PasswordSalt = passwordSalt;

                    var passwordResetResult = UpdatePassword(user.Data);
                    if (passwordResetResult.Success)
                    {
                        checkLink.Data.Url = "";
                        checkLink.Data.Code = "";
                        checkLink.Data.UserId = checkLink.Data.UserId;
                        checkLink.Data.Status = 0;
                        Update(checkLink.Data);

                        return new SuccessResult(passwordResetResult.Message);
                    }
                }
            }
            return new ErrorResult(Messages.UnSuccessUserPasswordReset);
        }

        public IResult UpdatePassword(Core.Entities.Concrete.User user)
        {
            if (user != null)
            {
                _userDal.Update(user);
                return new SuccessResult(Messages.SuccessUserPasswordReset);
            }
            return new ErrorResult(Messages.UnSuccessUserPasswordReset);
        }

        [TransactionScopeAspect]
        public IDataResult<string> SendPasswordResetCode(PasswordResetParameter passwordResetParameter)
        {
            var user = _userService.GetByMail(passwordResetParameter.Email);
            if (user != null)
            {
                var uniqueCode = UniqueCodeGenerator.GenerateUniqueCode(8);
                var updateResetPassword = GetByUserId(user.Id);


                if (!updateResetPassword.Success)
                {
                    PasswordReset passwordReset = new PasswordReset()
                    {
                        UserId = user.Id,
                        Code = uniqueCode.ToUpper(),
                        Url = "",
                        ResetEndDate = DateTime.Now.AddMinutes(10),
                        CodeUrl = UniqueCodeGenerator.GenerateUniqueLink().ToLower()
                    };

                    Add(passwordReset);

                    var mailResult = _mailService.ForgotPasswordCode(passwordResetParameter.Email, uniqueCode);
                    if (!mailResult.Success)
                    {
                        return new ErrorDataResult<string>(mailResult.Message);
                    }
                    return new SuccessDataResult<string>(data: passwordReset.CodeUrl, Messages.PasswordResetCode);

                }
                else
                {
                    updateResetPassword.Data.UserId = user.Id;
                    updateResetPassword.Data.Code = uniqueCode.ToUpper();
                    updateResetPassword.Data.Url = "";
                    updateResetPassword.Data.ResetEndDate = DateTime.Now.AddMinutes(10);
                    updateResetPassword.Data.CodeUrl = UniqueCodeGenerator.GenerateUniqueLink().ToLower();

                    Update(updateResetPassword.Data);

                    var mailResult = _mailService.ForgotPasswordCode(passwordResetParameter.Email, uniqueCode);
                    if (!mailResult.Success)
                    {
                        return new ErrorDataResult<string>(mailResult.Message);
                    }

                    return new SuccessDataResult<string>(data: updateResetPassword.Data.CodeUrl, Messages.PasswordResetCode);

                }


            }
            return new ErrorDataResult<string>(message: Messages.FailedCodeCheck);
        }

        [TransactionScopeAspect]
        public IResult SendPasswordResetLink(PasswordResetParameter passwordResetParameter)
        {
            var result = GetByCodeAndUrl(passwordResetParameter.Code, passwordResetParameter.CodeUrl);
            if (result.Data != null)
            {
                if (DateTime.Now > result.Data.ResetEndDate)
                {
                    return new ErrorResult();
                }
                var user = _userService.GetById(result.Data.UserId);

                if (user != null)
                {
                    var uniqueLink = UniqueCodeGenerator.GenerateUniqueLink();

                    result.Data.UserId = result.Data.UserId;
                    result.Data.Code = "";
                    result.Data.Url = uniqueLink.ToLower();
                    result.Data.ResetEndDate = result.Data.ResetEndDate;

                    Update(result.Data);
                    var mailResult =_mailService.SendPaswordResetLink(user.Data.Email, uniqueLink);

                    if (!mailResult.Success)
                    {
                        return new ErrorResult(mailResult.Message);
                    }


                    return new SuccessResult(Messages.PasswordResetCode);
                }
                else
                {
                    return new ErrorResult(Messages.FindFailedUser);
                }
            }
            return new ErrorResult(Messages.FailedEmailCheck);
        }

        public IResult Update(PasswordReset passwordReset)
        {
            if (passwordReset != null)
            {
                _passwordResetDal.Update(passwordReset);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult UpdateList(List<PasswordReset> passwordResets)
        {
            if (passwordResets != null & passwordResets.Count > 0)
            {
                _passwordResetDal.UpdateRange(passwordResets);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}

