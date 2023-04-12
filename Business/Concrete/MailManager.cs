using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Helpers.MailHelper;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MailManager : IMailService
    {
        Mail _mail;
        IMailHelper _mailHelper;
        public IConfiguration Configuration { get; }

        public MailManager(IMailHelper mailHelper, IConfiguration config)
        {
            _mailHelper = mailHelper;
            Configuration = config;
            _mail = Configuration.GetSection("Mail").Get<Mail>();
        }

        public IResult SendMail(MailDto mailDto)
        {
            if (mailDto != null)
            {
                Mail sendMail = new Mail()
                {
                    MailSender = _mail.MailSender,
                    SenderPassword = _mail.SenderPassword,
                    SenderSmtp = _mail.SenderSmtp,
                    SenderPort = _mail.SenderPort,
                    MailRecipientList = _mail.MailRecipientList
                };
                _mail.MailSubject = mailDto.MailTitle;
                _mail.MailHtmlBody = $"<h5> kantarhesap.esit.com.tr Web Sitesinden mail var. </h5> </br>" + mailDto.MailBody;
                _mailHelper.SendMail(_mail, mailDto);

                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
