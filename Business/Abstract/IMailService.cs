using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IMailService
    {
        IResult ConstantSendMail(MailDto mailDto);
        IResult VariableSendMail(MailDto mailDto);
        IResult Register(string firstName, string lastName, string email);
        IResult ForgotPasswordCode(string email, string code);
        IResult SendPaswordResetLink(string email, string link);
    }
}
