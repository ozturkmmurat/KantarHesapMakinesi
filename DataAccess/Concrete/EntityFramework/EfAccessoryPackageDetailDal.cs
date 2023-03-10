using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAccessoryPackageDetailDal : EfEntityRepositoryBase<AccessoryPackageDetail, KantarHesapMakinesiContext>, IAccessoryPackageDetailDal
    {
        public AccessoryPackageDetailDto GetAccessoryPackageDetailById(Expression<Func<AccessoryPackageDetailDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from apd in context.AccessoryPackageDetails
                             join a in context.Accessories
                             on apd.AccessoryId equals a.Id
                             into accessoryTemp
                             from act in accessoryTemp.DefaultIfEmpty()
                             join ap in context.AccessoryPackages
                             on apd.AccessoryPackageId equals ap.Id
                             
                             select new AccessoryPackageDetailDto
                             {
                                 AccessoryPackageDetailId = apd.Id,
                                 AccessoryPackageDetailAccessoryPackageId = apd.AccessoryPackageId,
                                 AccessoryPackageDetailAccessoryId = apd.AccessoryId,
                                 AccessoryPackageDetailAccessoryPcs = apd.AccessoryPcs,
                                 AccessoryId = act.Id,
                                 AccessoryName = act.AccessoryName,
                                 AccessoryTlPrice = act.AccessoryTlPrice,
                                 AccessoryEuroPrice = act.AccessoryEuroPrice,
                                 AccessoryPackageId = ap.Id,
                                 AccessoryPackageName = ap.AccessoryPackageName
                             };
                return result.FirstOrDefault();
            }
        }

        public List<AccessoryPackageDetailDto> GetAllModelAccessoryDetailDto(Expression<Func<AccessoryPackageDetailDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from apd in context.AccessoryPackageDetails
                             join a in context.Accessories
                             on apd.AccessoryId equals a.Id
                             join ap in context.AccessoryPackages
                             on apd.AccessoryPackageId equals ap.Id
                             into accessoryPackageTemp
                             from acpt in accessoryPackageTemp.DefaultIfEmpty()
                             select new AccessoryPackageDetailDto
                             {  
                                 AccessoryPackageDetailId = apd.Id,
                                 AccessoryPackageDetailAccessoryPackageId = apd.AccessoryPackageId,
                                 AccessoryPackageDetailAccessoryId = apd.AccessoryId,
                                 AccessoryPackageDetailAccessoryPcs = apd.AccessoryPcs,
                                 AccessoryId = a.Id,
                                 AccessoryName = a.AccessoryName,
                                 AccessoryTlPrice = a.AccessoryTlPrice,
                                 AccessoryEuroPrice = a.AccessoryEuroPrice,
                                 AccessoryPackageId = acpt.Id,
                                 AccessoryPackageName = acpt.AccessoryPackageName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }

        }
    }
}
