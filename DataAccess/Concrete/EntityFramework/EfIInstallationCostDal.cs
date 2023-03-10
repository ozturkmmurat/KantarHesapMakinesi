using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfIInstallationCostDal : EfEntityRepositoryBase<InstallationCost, KantarHesapMakinesiContext>, IInstallationCostDal
    {
        public List<InstallationCostDto> GetAllInstallationCostDto(Expression<Func<InstallationCostDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from l in context.Locations
                             join ic in context.InstallationCosts
                             on l.Id equals ic.LocationId

                             select new InstallationCostDto
                             {
                                 InstallationCostId = ic.Id,
                                 LocationId = ic.LocationId,
                                 InstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationEuroPrice = ic.InstallationEuroPrice,
                                 CityName = l.CityName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public InstallationCostDto GetByIdProductModelCostDetailDto(Expression<Func<InstallationCostDto, bool>> filter)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from l in context.Locations
                             join ic in context.InstallationCosts
                             on l.Id equals ic.LocationId

                             select new InstallationCostDto
                             {
                                 InstallationCostId = ic.Id,
                                 LocationId = ic.LocationId,
                                 InstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationEuroPrice = ic.InstallationEuroPrice,
                                 CityName = l.CityName
                             };
                return result.FirstOrDefault(filter);
            }
        }
    }
}
