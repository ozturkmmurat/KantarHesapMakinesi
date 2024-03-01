using Business.Abstract;
using Business.Constans.Html;
using Core.Entities.Concrete;
using Core.Utilities.Helpers.MailHelper;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Core.Utilities.User;
using Microsoft.AspNetCore.Http;
using Core.Utilities.IoC;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Business.Constans;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class MailManager : IMailService
    {
        Mail mailEntity;
        IMailHelper _mailHelper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IConfiguration Configuration { get; }

        public MailManager(IMailHelper mailHelper, IConfiguration config)
        {
            _mailHelper = mailHelper;
            Configuration = config;
            mailEntity = Configuration.GetSection("Mail").Get<Mail>();
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }


        public IResult ConstantSendMail(MailDto mailDto)
        {
            if (mailDto != null)
            {
                Mail sendMail = new Mail()
                {
                    MailSender = mailEntity.MailSender,
                    SenderPassword = mailEntity.SenderPassword,
                    SenderSmtp = mailEntity.SenderSmtp,
                    SenderPort = mailEntity.SenderPort,
                    MailRecipientList = mailEntity.MailRecipientList
                };

                mailEntity.MailSubject = mailDto.MailTitle;
                mailEntity.MailHtmlBody = mailDto.MailBody;
                var sendMailResult = _mailHelper.SendMail(mailEntity, mailDto);
                if (!sendMailResult.Success)
                {
                    return new ErrorResult(sendMailResult.Message);
                }

                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Register(string firstName, string lastName, string email)
        {
            MailDto mailDto = new MailDto();
            mailDto.MailTitle = "Yeni Üye";
            mailDto.MailBody = MailHtml.Register(firstName, lastName, email);
            var sendMailResult = ConstantSendMail(mailDto);
            if (sendMailResult.Success)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult ForgotPasswordCode(string email, string code)
        {
            if (email == null && email == "" && code == null && code == "")
            {
                return new ErrorResult(Messages.CodeCheck);
            }
            MailDto mailDto = new MailDto();
            mailDto.MailTitle = "Kantar Hesap Makinesi Şifre sıfırlama kodunuz";
            mailDto.MailBody = MailHtml.ForgotPasswordCode(code);
            mailDto.Email = email;
            var sendMailResult = VariableSendMail(mailDto);

            if (!sendMailResult.Success)
            {
                return new ErrorResult(sendMailResult.Message);
            }

            return new SuccessResult();
        }

        public IResult SendPaswordResetLink(string email, string link)
        {
            if (email == null && email == "")
            {
                return new ErrorResult(Messages.CheckEmail);
            }
            MailDto mailDto = new MailDto();
            mailDto.MailTitle = "Kantar Hesap Makinesi Şifre sıfırlama linki";
            mailDto.MailBody = MailHtml.ResetPasswordLink(link);
            mailDto.Email = email;
            var sendMailResult = VariableSendMail(mailDto);

            if (!sendMailResult.Success)
            {
                return new ErrorResult(sendMailResult.Message);
            }
            return new SuccessResult();
        }

        public IResult VariableSendMail(MailDto mailDto)
        {
            if (mailDto != null)
            {
                Mail sendMail = new Mail()
                {
                    MailSender = mailEntity.MailSender,
                    SenderPassword = mailEntity.SenderPassword,
                    SenderSmtp = mailEntity.SenderSmtp,
                    SenderPort = mailEntity.SenderPort,
                    MailRecipientList = new List<string>()
                };

                //sendMail.MailRecipientList.Add(ClaimHelper.GetUserEmail(_httpContextAccessor.HttpContext));
                if (mailDto.Email == null)
                {
                    sendMail.MailRecipientList.Add(mailDto.Email);
                }
                else
                {
                    sendMail.MailRecipientList.Add(mailDto.Email);
                }

                sendMail.MailSubject = mailDto.MailTitle;
                sendMail.MailHtmlBody = mailDto.MailBody;
                var sendMailResult  = _mailHelper.SendMail(sendMail, mailDto);
                if (!sendMailResult.Success)
                {
                    return new ErrorResult(sendMailResult.Message);
                }

                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
