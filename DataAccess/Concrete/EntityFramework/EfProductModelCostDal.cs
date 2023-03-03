using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductModelCostDal : EfEntityRepositoryBase<ProductModelCost, KantarHesapMakinesiContext>, IProductModelCostDal
    {

        public void CustomAdd(ProductModelCost product)
        {
            using (KantarHesapMakinesiContext ctx = new KantarHesapMakinesiContext())
            {
                ctx.Set<ProductModelCost>().Add(product);
                ctx.SaveChanges();
            }
        }

        public void AddCustom<T>(T yourEntity)
            where T:class,new()
        {
            using (KantarHesapMakinesiContext ctx = new KantarHesapMakinesiContext())
            {
                ctx.Set<T>().Add(yourEntity); 
                ctx.SaveChanges();
            }
        }

        public List<ProductModelCostDto> GetAllProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmc in context.ProductModelCosts
                             join pmcd in context.ProductModelCostDetails
                             on pmc.Id equals pmcd.ProductModelCostId
                             join m in context.Models
                             on pmc.Id equals m.Id
                             join ic in context.InstallationCosts
                             on pmcd.InstallationCostId equals ic.Id
                             join l in context.Locations
                             on ic.LocationId equals l.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ProductModelCostShateIronEuroPrice = pmc.ShateIronEuroPrice,
                                 ProductModelCostIProfileEuroPrice = pmc.IProfileEuroPrice,
                                 ProductModelCostMaterialTlAmount = pmc.MaterialTlAmount,
                                 ProductModelCostMaterialEuroAmount = pmc.MaterialEuroAmount,
                                 ProductModelCostLaborCostPerHour = pmc.LaborCostPerHour,
                                 ProductModelCostTotalLaborCost = pmc.TotalLaborCost,
                                 ProductModelCostTotalAmount = pmc.TotalAmount,
                                 ProductModelCostOverheadPercentage = pmc.OverheadPercentage,
                                 ProductModelCostGeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 ProductModelCostOverheadIncluded = pmc.OverheadIncluded,
                                 ProductModelCostElectronicTlAmount = pmc.ElectronicTlAmount,
                                 ProductModelCostElectronicEuroAmount = pmc.ElectronicEuroAmount,
                                 ProductModelCostAccessoriesTlAmount = pmc.AccessoriesTlAmount,
                                 ProductModelCostAccessoriesEuroAmount = pmc.AccessioresEuroAmount,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,


                                 ProductModelCostDetailId = pmcd.Id,
                                 ProductModelCostDetailInstallationCostId = pmcd.InstallationCostId,
                                 ProductModelCostDetailProductModelCostId = pmcd.ProductModelCostId,
                                 ProductModelCostDetailInstallationIncluded = pmcd.InstallationIncluded,
                                 ProductModelCostDetailSalesPrice = pmcd.SalesPrice,
                                 ProductModelCostDetailTurkeySalesPrice = pmcd.TurkeySalesPrice,
                                 ProductModelCostDetailProfitPercentage = pmcd.ProfitPercentage,
                                 ProductModelCostDetailProfitPrice = pmcd.ProfitPrice,
                                 ProductModelCostDetailTurkeySalesDiscount = pmcd.TurkeySalesDiscount,
                                 ProductModelCostDetailTurkeySalesDiscountPrice = pmcd.TurkeySalesDiscountPrice,
                                 ProductModelCostDetailExportFinalDiscount = pmcd.ExportFinalDiscount,
                                 ProductModelCostDetailExportFinalDiscountPrice = pmcd.ExportFinalDiscountPrice,



                                 InstallationCostId = ic.Id,
                                 InstallationCostLocationId = ic.LocationId,
                                 InstallationCostInstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationCostInstallationEuroPrice = ic.InstallationEuroPrice,

                                 LocationId = l.Id,
                                 LocationCityName = l.CityName,

                                 
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ProductModelCostDto GetProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmc in context.ProductModelCosts
                             join pmcd in context.ProductModelCostDetails
                             on pmc.Id equals pmcd.ProductModelCostId
                             join m in context.Models
                             on pmc.Id equals m.Id
                             join ic in context.InstallationCosts
                             on pmcd.InstallationCostId equals ic.Id
                             join l in context.Locations
                             on ic.LocationId equals l.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ProductModelCostShateIronEuroPrice = pmc.ShateIronEuroPrice,
                                 ProductModelCostIProfileEuroPrice = pmc.IProfileEuroPrice,
                                 ProductModelCostMaterialTlAmount = pmc.MaterialTlAmount,
                                 ProductModelCostMaterialEuroAmount = pmc.MaterialEuroAmount,
                                 ProductModelCostLaborCostPerHour = pmc.LaborCostPerHour,
                                 ProductModelCostTotalLaborCost = pmc.TotalLaborCost,
                                 ProductModelCostTotalAmount = pmc.TotalAmount,
                                 ProductModelCostOverheadPercentage = pmc.OverheadPercentage,
                                 ProductModelCostGeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 ProductModelCostOverheadIncluded = pmc.OverheadIncluded,
                                 ProductModelCostElectronicTlAmount = pmc.ElectronicTlAmount,
                                 ProductModelCostElectronicEuroAmount = pmc.ElectronicEuroAmount,
                                 ProductModelCostAccessoriesTlAmount = pmc.AccessoriesTlAmount,
                                 ProductModelCostAccessoriesEuroAmount = pmc.AccessioresEuroAmount,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,


                                 ProductModelCostDetailId = pmcd.Id,
                                 ProductModelCostDetailInstallationCostId = pmcd.InstallationCostId,
                                 ProductModelCostDetailProductModelCostId = pmcd.ProductModelCostId,
                                 ProductModelCostDetailInstallationIncluded = pmcd.InstallationIncluded,
                                 ProductModelCostDetailSalesPrice = pmcd.SalesPrice,
                                 ProductModelCostDetailTurkeySalesPrice = pmcd.TurkeySalesPrice,
                                 ProductModelCostDetailProfitPercentage = pmcd.ProfitPercentage,
                                 ProductModelCostDetailProfitPrice = pmcd.ProfitPrice,
                                 ProductModelCostDetailTurkeySalesDiscount = pmcd.TurkeySalesDiscount,
                                 ProductModelCostDetailTurkeySalesDiscountPrice = pmcd.TurkeySalesDiscountPrice,
                                 ProductModelCostDetailExportFinalDiscount = pmcd.ExportFinalDiscount,
                                 ProductModelCostDetailExportFinalDiscountPrice = pmcd.ExportFinalDiscountPrice,



                                 InstallationCostId = ic.Id,
                                 InstallationCostLocationId = ic.LocationId,
                                 InstallationCostInstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationCostInstallationEuroPrice = ic.InstallationEuroPrice,

                                 LocationId = l.Id,
                                 LocationCityName = l.CityName,

                                 
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
