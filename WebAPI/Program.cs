using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependecyResolvers.Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.BackgroundServices;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())
             .ConfigureContainer<ContainerBuilder>(builder =>
             {
                 builder.RegisterModule(new AutofacBusinessModule());
             })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .UseSerilog((context, services, configuration) =>  {
                configuration.ReadFrom.Configuration(context.Configuration)
         .ReadFrom.Services(services)
         .Enrich.FromLogContext()
         .MinimumLevel.Information() // En düþük log seviyesi belirlenir
         .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information) // Konsola sadece Information ve üstü seviyedeki loglar yazdýrýlýr
         .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(evt => evt.Level == Serilog.Events.LogEventLevel.Error)
             .WriteTo.File("logs/logstart.txt", rollingInterval: RollingInterval.Day)); // Error seviyesindeki loglar logstart.txt dosyasýna yazdýrýlýr
            });
    }
}


