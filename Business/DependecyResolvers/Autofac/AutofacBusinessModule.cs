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
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Abstract.ProductModelCost;
using DataAccess.Abstract.SP;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.ProductModelCost;
using DataAccess.Concrete.EntityFramework.SP;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependecyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfAccessoryDal>().As<IAccessoryDal>().SingleInstance();
            builder.RegisterType<AccessoryManager>().As<IAccessoryService>().SingleInstance();

            builder.RegisterType<EfElectronicDal>().As<IElectronicDal>().SingleInstance();
            builder.RegisterType<ElectronicManager>().As<IElectronicService>().SingleInstance();

            builder.RegisterType<EfModelDal>().As<IModelDal>().SingleInstance();
            builder.RegisterType<ModelManager>().As<IModelService>().SingleInstance();

            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();

            builder.RegisterType<EfProductModelCostDal>().As<IProductModelCostDal>().SingleInstance();
            builder.RegisterType<ProductModelCostManager>().As<IProductModelCostService>().SingleInstance();

            builder.RegisterType<EfProductModelCostDetailDal>().As<IProductModelCostDetailDal>().SingleInstance();
            builder.RegisterType<ProductModelCostDetailManager>().As<IProductModelCostDetailService>().SingleInstance();

            builder.RegisterType<EfCostVariableDal>().As<ICostVariableDal>().SingleInstance();
            builder.RegisterType<CostVariableManager>().As<ICostVariableService>().SingleInstance();

            builder.RegisterType<EfLocationDal>().As<ILocationDal>().SingleInstance();
            builder.RegisterType<LocationManager>().As<ILocationService>().SingleInstance();

            builder.RegisterType<EfIInstallationCostDal>().As<IInstallationCostDal>().SingleInstance();
            builder.RegisterType<InstallationCostManager>().As<IInstallationCostService>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<EfUserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            builder.RegisterType<SizeManager>().As<ISizeService>().SingleInstance();
            builder.RegisterType<EfSizeDal>().As<ISizeDal>().SingleInstance();

            builder.RegisterType<SizeContentManager>().As<ISizeContentService>().SingleInstance();
            builder.RegisterType<EfSizeContent>().As<ISizeContentDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<DailyCalculationManager>().As<IDailyCalculationService>().SingleInstance();
            builder.RegisterType<EfDailyCalculationDal>().As<IDailyCalculationDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }
    }
}
