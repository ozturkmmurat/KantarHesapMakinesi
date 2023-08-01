using Business.Abstract;
using Business.Abstract.SP;
using Core.Entities.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.JWT;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace WebAPI.BackgroundServices
{
    public class DailyMethods : IHostedService, IDisposable
    {
        private IProductModelCostService _productModelCostService;
        private IDailyCalculationService _dailyCalculationService;
        private IMailService _mailService;
        private readonly ILogger<DailyMethods> _logger;
        private Timer _timer;
        public DailyMethods(
            ILogger<DailyMethods> logger,
            IProductModelCostService productModelCostService,
            IDailyCalculationService dailyCalculationService,
            IMailService mailService)
        {
            _productModelCostService = productModelCostService;
            _dailyCalculationService = dailyCalculationService;
            _mailService = mailService;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync is running.");
            MailDto mailDto = new MailDto()
            {
                MailTitle = "Günlük Güncelleme başladı."
            };
            string message1 = " StartAsync giriş yapıldı ";
            TimeSpan calculateTime = CalculateTime();
            string message2 = " Kalan süre : " + calculateTime.Hours + " Saat "  + calculateTime.Minutes +  " Dakika ";
            mailDto.MailBody = message1 + message2;
            _mailService.SendMail(mailDto);
            _timer = new Timer(DailyMethod, null, calculateTime, TimeSpan.FromDays(1));
            //_timer = new Timer(DailyMethod, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Dispose();
            throw new NotImplementedException();
        }

        private void DailyMethod(object state)
        {
            MailDto mailDto = new MailDto()
            {
                MailTitle = "Günlük güncelleme"
            };
            try
            {
                List<ProductModelCostDto> productModelCostDtos = new List<ProductModelCostDto>();
                var getProductModelCost = _productModelCostService.GetAllDto();
                for (int i = 0; i < getProductModelCost.Data.Count; i++)
                {
                    ProductModelCostDto productModelCostDto = new ProductModelCostDto();
                    productModelCostDto.ProductModelCostId = getProductModelCost.Data[i].ProductModelCostId;
                    productModelCostDto.ModelId = getProductModelCost.Data[i].ModelId;
                    productModelCostDto.CurrencyName = getProductModelCost.Data[i].CurrencyName;
                    productModelCostDto.AdditionalProfitPercentage = getProductModelCost.Data[i].AdditionalProfitPercentage;
                    productModelCostDto.ProfitPercentage = getProductModelCost.Data[i].ProfitPercentage;
                    productModelCostDtos.Add(productModelCostDto);
                }
                _dailyCalculationService.DailyTCMBSP();
                _dailyCalculationService.CalculateCostSP();
                _productModelCostService.UpdateRange(productModelCostDtos);
                mailDto.MailBody = "Günlük güncelleme başarılı";
                _mailService.SendMail(mailDto);

            }
            catch (Exception error)
            {
                mailDto.MailTitle = "Günlük güncelleme başarısız.";
                mailDto.MailBody = "Günülük güncelleme sırasında hata oluştu hata mesajı alttaki şekildedir </br>" + error;
                _mailService.SendMail(mailDto);
                throw;
            }
        }

        private TimeSpan CalculateTime()
        {
            // Şu anki zamanı ve 16:30 zamanını al
            DateTime now = DateTime.Now;
            TimeSpan updateTime = new TimeSpan(16, 30, 00);

            // Eğer şu anki zaman, 16:30'dan büyükse bir sonraki günün 16:30'una kadar zaman hesapla
            if (now.TimeOfDay > updateTime)
            {
                return TimeSpan.FromDays(1) - (now.TimeOfDay - updateTime);
            }
            else
            {
                // Aksi takdirde, bugünün 16:30'una kadar zaman hesapla
                return updateTime - now.TimeOfDay;
            }
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
