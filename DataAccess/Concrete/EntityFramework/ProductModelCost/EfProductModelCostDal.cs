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
    public class EfProductModelCostDal : EfEntityRepositoryBase<Entities.Concrete.ProductModelCost, KantarHesapMakinesiContext>, IProductModelCostDal
    {

        public void CustomAdd(Entities.Concrete.ProductModelCost product)
        {
            using (KantarHesapMakinesiContext ctx = new KantarHesapMakinesiContext())
            {
                ctx.Set<Entities.Concrete.ProductModelCost>().Add(product);
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
                             join m in context.Models
                             on pmc.Id equals m.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ProductModelCostShateIronEuroPrice = pmc.ShateIronEuroPrice,
                                 ProductModelCostIProfileEuroPrice = pmc.IProfileEuroPrice,
                                 ProductModelCostMaterialTlAmount = pmc.MaterialTlAmount,
                                 ProductModelCostMaterialEuroAmount = pmc.MaterialEuroAmount,
                                 ProductModelCostTotalLaborCostTl = pmc.TotalLaborCostTl,
                                 ProductModelCostTotalLaborCostEuro = pmc.TotalLaborCostEuro,
                                 ProductModelCostTotalAmount = pmc.TotalAmount,
                                 ProductModelCostGeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 ProductModelCostOverheadIncluded = pmc.OverheadIncluded,
                                 ProductModelCostElectronicTlAmount = pmc.ElectronicTlAmount,
                                 ProductModelCostElectronicEuroAmount = pmc.ElectronicEuroAmount,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,

                                 ProductModelCostProfitPercentage = pmc.ProfitPercentage,
                                 ProductModelCostAdditionalProfitPercentage = pmc.AdditionalProfitPercentage,

                                 ModelCostVariableId = cv.Id,
                                LaborCostPerHourEuro = cv.LaborCostPerHourEuro
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ProductModelCostDto GetProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmc in context.ProductModelCosts
                             join m in context.Models
                             on pmc.Id equals m.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ProductModelCostShateIronEuroPrice = pmc.ShateIronEuroPrice,
                                 ProductModelCostIProfileEuroPrice = pmc.IProfileEuroPrice,
                                 ProductModelCostMaterialTlAmount = pmc.MaterialTlAmount,
                                 ProductModelCostMaterialEuroAmount = pmc.MaterialEuroAmount,
                                 ProductModelCostTotalLaborCostTl = pmc.TotalLaborCostTl,
                                 ProductModelCostTotalLaborCostEuro = pmc.TotalLaborCostEuro,
                                 ProductModelCostTotalAmount = pmc.TotalAmount,
                                 ProductModelCostGeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 ProductModelCostOverheadIncluded = pmc.OverheadIncluded,
                                 ProductModelCostElectronicTlAmount = pmc.ElectronicTlAmount,
                                 ProductModelCostElectronicEuroAmount = pmc.ElectronicEuroAmount,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,

                                 ProductModelCostProfitPercentage = pmc.ProfitPercentage,
                                 ProductModelCostAdditionalProfitPercentage = pmc.AdditionalProfitPercentage,

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro
                             };
                return result.Where(filter).FirstOrDefault();
            }
        }
    }
}
