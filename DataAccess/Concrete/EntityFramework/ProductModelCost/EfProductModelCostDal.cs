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
        private readonly KantarHesapMakinesiContext _context;

        public EfProductModelCostDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }

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
                var result = from pmc in _context.ProductModelCosts
                             join m in _context.Models
                             on pmc.ModelId equals m.Id
                             join pf in _context.ProductProfits
                             on  m.ProductId equals pf.ProductId
                             join cv in _context.CostVariables
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

                                 ProfitPercentage = pf.ProfitPercentage,
                                 AdditionalProfitPercentage = pf.AdditionalProfitPercentage,

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
        }

        public ProductModelCostDto GetProductModelCostDtoById(Expression<Func<ProductModelCostDto, bool>> filter = null)
        {
                var result = from pmc in _context.ProductModelCosts
                             join m in _context.Models
                             on pmc.ModelId equals m.Id
                             join cv in _context.CostVariables
                             on m.CostVariableId equals cv.Id
                             join pf in _context.ProductProfits
                             on m.ProductId equals pf.ProductId

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

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro,

                                 ProfitPercentage = pf.ProfitPercentage,
                                 AdditionalProfitPercentage = pf.AdditionalProfitPercentage
                             };
                return result.Where(filter).FirstOrDefault();
        }

        public List<ProductModelCostDto> GetAllDto()
        {
                var result = from pmc in _context.ProductModelCosts
                             join m in _context.Models
                             on pmc.ModelId equals m.Id
                             join cv in _context.CostVariables
                             on m.CostVariableId equals cv.Id
                             join pf in _context.ProductProfits
                             on m.ProductId equals pf.ProductId

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

                                 ModelCostVariableId = cv.Id,
                                 LaborCostPerHourEuro = cv.LaborCostPerHourEuro,

                                 ProfitPercentage = pf.ProfitPercentage,
                                 AdditionalProfitPercentage = pf.AdditionalProfitPercentage
                             };
                return result.ToList();
        }
    }
}
