using Business.Abstract.SP;
using Microsoft.Extensions.Hosting;
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
        public DailyMethods(IDailyCalculationService dailyCalculationService)
        {
            _dailyCalculationService = dailyCalculationService;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true) // Sonsuz döngü ile metodu sürekli çalışır hale getiriyoruz.
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

                // Bir sonraki gün için 16:30'u hesapla
                targetTime = targetTime.AddDays(1);

                // Bir sonraki işleme kadar bekle
                await Task.Delay(targetTime - DateTime.Now, CancellationToken.None);
            }
        }
    }
}
