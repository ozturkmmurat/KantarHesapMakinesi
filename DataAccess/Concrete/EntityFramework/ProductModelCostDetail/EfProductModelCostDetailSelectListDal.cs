using Core.DataAccess.EntityFramework;
using DataAccess.Abstract.ProductModelCostDetail;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Entities.Dtos.ProductModelCostDetail;

namespace DataAccess.Concrete.EntityFramework.ProductModelCostDetail
{
    public class EfProductModelCostDetailSelectListDal : EfEntityRepositoryBase<Entities.Concrete.ProductModelCostDetail, KantarHesapMakinesiContext>, IProductModelCostDetailSelectListDal
    {
        public List<ProductModelCostDetailSelectListDto> GetAllProductModelCostDetailDtos(Expression<Func<ProductModelCostDetailSelectListDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmcd in context.ProductModelCostDetails
                             join ic in context.InstallationCosts
                             on pmcd.InstallationCostId equals ic.Id
                             join l in context.Locations
                             on ic.LocationId equals l.Id
                             select new ProductModelCostDetailSelectListDto
                             {
                                 ProductModelCostId = pmcd.ProductModelCostId,
                                 InstallationCostId = ic.Id,
                                 InstallationCostLocationId = ic.LocationId,
                                 LocationCityName = l.CityName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
