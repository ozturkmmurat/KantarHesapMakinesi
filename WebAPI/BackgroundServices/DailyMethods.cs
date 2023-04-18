using Business.Abstract;
using Business.Abstract.SP;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.BackgroundServices
{
    public class DailyMethods : BackgroundService
    {
        private IDailyCalculationService _dailyCalculationService;
        private IMailService _mailService;
        private readonly ILogger<DailyMethods> _logger;
        public DailyMethods(
            IDailyCalculationService dailyCalculationService,
            ILogger<DailyMethods> logger,
            IMailService mailService)
        {
            _dailyCalculationService = dailyCalculationService;
            _mailService = mailService;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //_logger.LogInformation("Executeasync girdi");
            MailDto mail = new MailDto()
            {
                MailTitle = "Günlük güncelleme"
            };
            string message1 = " ExecuteAsync giriş yapıldı";
            while (true) // Sonsuz döngü ile metodu sürekli çalışır hale getiriyoruz.
            {
                mail.MailBody = " While giriş yapıldı";
                _mailService.SendMail(mail);
                try
                {
                    string message2 = " try giriş yapıldı";
                    // Günün şu anki zamanını al
                    var currentTime = DateTime.Now;

                    // Eğer saat 16:30'dan önceyse, 16:30'a kadar bekleyip devam et
                    var targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 16, 30, 0);
                    string message3 = " Şuan ki saat : " + currentTime + " hedeflenen saat : " + targetTime;
                    mail.MailBody  = message1 + message2 + message3;
                    if (currentTime < targetTime) // Eğer şu anki zaman, hedef zamandan önceyse
                    {
                        string message4 = " İf giriş yapıldı ";
                        mail.MailBody += message4;
                        _mailService.SendMail(mail);
                        var delayTime = targetTime - currentTime; // Zaman farkını hesapla
                        await Task.Delay(delayTime, CancellationToken.None); // Zaman farkı kadar bekle
                        _dailyCalculationService.DailyTCMBSP();
                        _dailyCalculationService.CalculateCostSP();
                        mail.MailBody = " Günlük güncelleme başarılı. ";
                        _mailService.SendMail(mail);
                    }
                    else
                    {
                        string message4 = " Else giriş yapıldı ";
                        mail.MailBody += message4;
                        _mailService.SendMail(mail);
                        var nextDayTargetTime = targetTime.AddDays(1); // Bir sonraki gün için 16:30'u hesapla
                        var delayTime = nextDayTargetTime - currentTime; // Zaman farkını hesapla
                        await Task.Delay(delayTime, CancellationToken.None); // Zaman farkı kadar bekle
                    }
                }
                catch (Exception e)
                {
                    mail.MailTitle = "Günlük güncelleme başarısız.";
                    mail.MailBody = " Güncelleme başarısız. </br></br> Hata Mesajı Alttaki şekildedir. </br></br> <h3>" + e.Message + "</h3>";
                    _mailService.SendMail(mail);
                }
            }

        }
    }
}
