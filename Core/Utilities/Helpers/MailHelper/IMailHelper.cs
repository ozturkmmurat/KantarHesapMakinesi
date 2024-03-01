using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Utilities.Helpers.MailHelper
{
    public interface IMailHelper
    {
        IResult SendMail(Mail sendMail, UserMail userMail);

    }
}
