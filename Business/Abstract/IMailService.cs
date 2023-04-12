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
        IResult SendMail(MailDto mailDto);
    }
}
