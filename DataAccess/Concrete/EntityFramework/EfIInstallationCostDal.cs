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
        private readonly KantarHesapMakinesiContext _context;

        public EfIInstallationCostDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
        public List<InstallationCostDto> GetAllInstallationCostDto(Expression<Func<InstallationCostDto, bool>> filter = null)
        {
                var result = from l in _context.Locations
                             join ic in _context.InstallationCosts
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

        public InstallationCostDto GetByIdProductModelCostDetailDto(Expression<Func<InstallationCostDto, bool>> filter)
        {
                var result = from l in _context.Locations
                             join ic in _context.InstallationCosts
                             on l.Id equals ic.LocationId

                             select new InstallationCostDto
                             {
                                 InstallationCostId = ic.Id,
                                 LocationId = ic.LocationId,
                                 InstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationEuroPrice = ic.InstallationEuroPrice,
                                 CityName = l.CityName
                             };
                return result.Where(filter).FirstOrDefault();
        }
    }
}
