using Business.Abstract;
using Business.Abstract.SP;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
            MailDto mail = new MailDto()
            {
                MailTitle = "Günlük güncelleme"
            };
            while (!stoppingToken.IsCancellationRequested) // Sonsuz döngü ile metodu sürekli çalışır hale getiriyoruz.
            {
                try
                {
                    // Günün şu anki zamanını al
                    var currentTime = DateTime.Now;

                    // Eğer saat 16:30'dan önceyse, 16:30'a kadar bekleyip devam et
                    var targetTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 16, 30, 0);
                    if (currentTime < targetTime) // Eğer şu anki zaman, hedef zamandan önceyse
                    {
                        var delayTime = targetTime - currentTime; // Zaman farkını hesapla
                        await Task.Delay(delayTime, CancellationToken.None); // Zaman farkı kadar bekle
                    }

                    // Saat 16:30 olduğunda işlem yap
                    _dailyCalculationService.DailyTCMBSP();
                    _dailyCalculationService.CalculateCostSP();
                    mail.MailBody = "Günlük güncelleme başarılı.";
                    _mailService.SendMail(mail);
                    // Bir sonraki gün için 16:30'u hesapla
                    targetTime = targetTime.AddDays(1);

                    // Bir sonraki işleme kadar bekle
                    await Task.Delay(targetTime - DateTime.Now, CancellationToken.None);
                }
                catch (Exception e)
                {
                    _logger.LogError(e,"Background servisi hata verdi.");
                    mail.MailTitle = "Günlük güncelleme başarısız.";
                    mail.MailBody = "Güncelleme başarısız. </br></br> Hata Mesajı Alttaki şekildedir. </br></br> <h3>" + e.Message + "</h3>" ;
                    _mailService.SendMail(mail);
                }
              
            }
        }
    }
}
