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
                             on pmc.ModelId equals m.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ShateIronPrice = pmc.ShateIronPrice,
                                 IProfilePrice = pmc.IProfilePrice,
                                 MaterialAmount = pmc.MaterialAmount,
                                 TotalLaborCost = pmc.TotalLaborCost,
                                 TotalAmount = pmc.TotalAmount,
                                 GeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 OverheadIncluded = pmc.OverheadIncluded,
                                 ElectronicAmount = pmc.ElectronicAmount,
                                 CurrencyName = pmc.CurrencyName,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,

                                 ProfitPercentage = m.ProfitPercentage,
                                 AdditionalProfitPercentage = m.AdditionalProfitPercentage,

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
                             on pmc.ModelId equals m.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ShateIronPrice = pmc.ShateIronPrice,
                                 IProfilePrice = pmc.IProfilePrice,
                                 MaterialAmount = pmc.MaterialAmount,
                                 TotalLaborCost = pmc.TotalLaborCost,
                                 TotalAmount = pmc.TotalAmount,
                                 GeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 OverheadIncluded = pmc.OverheadIncluded,
                                 ElectronicAmount = pmc.ElectronicAmount,
                                 CurrencyName = pmc.CurrencyName,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,

                                 ProfitPercentage = m.ProfitPercentage,
                                 AdditionalProfitPercentage = m.AdditionalProfitPercentage,

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro
                             };
                return result.Where(filter).FirstOrDefault();
            }
        }

        public List<ProductModelCostDto> GetAllDto()
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmc in context.ProductModelCosts
                             join m in context.Models
                             on pmc.ModelId equals m.Id
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ProductModelCostDto
                             {
                                 ProductModelCostId = pmc.Id,
                                 ShateIronPrice = pmc.ShateIronPrice,
                                 IProfilePrice = pmc.IProfilePrice,
                                 MaterialAmount = pmc.MaterialAmount,
                                 TotalLaborCost = pmc.TotalLaborCost,
                                 TotalAmount = pmc.TotalAmount,
                                 GeneralExpenseAmount = pmc.GeneralExpenseAmount,
                                 OverheadIncluded = pmc.OverheadIncluded,
                                 ElectronicAmount = pmc.ElectronicAmount,
                                 CurrencyName = pmc.CurrencyName,

                                 ModelId = m.Id,
                                 ModelProductionTime = m.ProductionTime,

                                 ProfitPercentage = m.ProfitPercentage,
                                 AdditionalProfitPercentage = m.AdditionalProfitPercentage,

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro,
                             };
                return result.ToList();
            }
        }
    }
}
