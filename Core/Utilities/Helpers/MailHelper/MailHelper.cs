using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Entities.Concrete;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core.Utilities.Helpers.MailHelper
{
    [LogAspect(typeof(FileLogger))]

    public class MailHelper : IMailHelper
    {
        IResult IMailHelper.SendMail(Mail sendMail, UserMail userMail)
        {
            try
            {
                SmtpClient sc = new SmtpClient(sendMail.SenderSmtp, sendMail.SenderPort);
                sc.UseDefaultCredentials = true;
                sc.Credentials = new NetworkCredential(sendMail.MailSender, sendMail.SenderPassword);
                sc.EnableSsl = false;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(sendMail.MailSender, userMail.FirstNameLastName);
                foreach (var item in sendMail.MailRecipientList)
                {
                    mail.To.Add(item);
                }
                mail.Subject = sendMail.MailSubject;
                mail.IsBodyHtml = true;
                string htmlString = sendMail.MailHtmlBody;
                mail.Body = htmlString;
                mail.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");
                mail.SubjectEncoding = System.Text.Encoding.GetEncoding("utf-8");
                sc.Send(mail);
                return new SuccessResult();
            }
            catch (Exception ex)
            {
                return new ErrorResult("Mail gönderilemedi");
            }
           
        }
    }
}
