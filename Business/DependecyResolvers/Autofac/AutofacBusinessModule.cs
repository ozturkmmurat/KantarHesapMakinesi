using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Abstract.ProductModelCost;
using Business.Abstract.SP;
using Business.Abstract.User;
using Business.Concrete;
using Business.Concrete.ProductModelCost;
using Business.Concrete.SP;
using Business.Concrete.User;
using Castle.DynamicProxy;
using Core.Utilities.Helpers.MailHelper;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Abstract.ProductModelCost;
using DataAccess.Abstract.SP;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.ProductModelCost;
using DataAccess.Concrete.EntityFramework.SP;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfAccessoryDal>().As<IAccessoryDal>().InstancePerLifetimeScope();
            builder.RegisterType<AccessoryManager>().As<IAccessoryService>().InstancePerLifetimeScope();

            builder.RegisterType<EfElectronicDal>().As<IElectronicDal>().InstancePerLifetimeScope();
            builder.RegisterType<ElectronicManager>().As<IElectronicService>().InstancePerLifetimeScope();

            builder.RegisterType<EfModelDal>().As<IModelDal>().InstancePerLifetimeScope();
            builder.RegisterType<ModelManager>().As<IModelService>().InstancePerLifetimeScope();

            builder.RegisterType<EfProductDal>().As<IProductDal>().InstancePerLifetimeScope();
            builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();

            builder.RegisterType<EfProductProfitDal>().As<IProductProfitDal>().InstancePerLifetimeScope();
            builder.RegisterType<ProductProfitManager>().As<IProductProfitService>().InstancePerLifetimeScope();

            builder.RegisterType<EfProductModelCostDal>().As<IProductModelCostDal>().InstancePerLifetimeScope();
            builder.RegisterType<ProductModelCostManager>().As<IProductModelCostService>().InstancePerLifetimeScope();

            builder.RegisterType<EfProductModelCostDetailDal>().As<IProductModelCostDetailDal>().InstancePerLifetimeScope();
            builder.RegisterType<ProductModelCostDetailManager>().As<IProductModelCostDetailService>().InstancePerLifetimeScope();

            builder.RegisterType<ProductModelCostCalculateManager>().As<IProductModelCostCalculateService>().InstancePerLifetimeScope();

            builder.RegisterType<EfCostVariableDal>().As<ICostVariableDal>().InstancePerLifetimeScope();
            builder.RegisterType<CostVariableManager>().As<ICostVariableService>().InstancePerLifetimeScope();

            builder.RegisterType<EfLocationDal>().As<ILocationDal>().InstancePerLifetimeScope();
            builder.RegisterType<LocationManager>().As<ILocationService>().InstancePerLifetimeScope();

            builder.RegisterType<EfIInstallationCostDal>().As<IInstallationCostDal>().InstancePerLifetimeScope();
            builder.RegisterType<InstallationCostManager>().As<IInstallationCostService>().InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().InstancePerLifetimeScope();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().InstancePerLifetimeScope();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().InstancePerLifetimeScope();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().InstancePerLifetimeScope();

            builder.RegisterType<SizeManager>().As<ISizeService>().InstancePerLifetimeScope();
            builder.RegisterType<EfSizeDal>().As<ISizeDal>().InstancePerLifetimeScope();

            builder.RegisterType<SizeContentManager>().As<ISizeContentService>().InstancePerLifetimeScope();
            builder.RegisterType<EfSizeContentDal>().As<ISizeContentDal>().InstancePerLifetimeScope();

            builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerLifetimeScope();

            builder.RegisterType<DailyCalculationManager>().As<IDailyCalculationService>().InstancePerLifetimeScope();
            builder.RegisterType<EfDailyCalculationDal>().As<IDailyCalculationDal>().InstancePerLifetimeScope();


            builder.RegisterType<PasswordResetManager>().As<IPasswordResetService>().InstancePerLifetimeScope();
            builder.RegisterType<EfPasswordResetDal>().As<IPasswordResetDal>().InstancePerLifetimeScope();

            builder.RegisterType<MailManager>().As<IMailService>().InstancePerLifetimeScope();
            builder.RegisterType<MailHelper>().As<IMailHelper>().InstancePerLifetimeScope();


            builder.RegisterType<KantarHesapMakinesiContext>().InstancePerLifetimeScope();



            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).InstancePerLifetimeScope();

        }
    }
}
