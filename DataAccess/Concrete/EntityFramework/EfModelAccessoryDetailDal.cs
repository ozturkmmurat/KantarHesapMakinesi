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
    public class EfModelAccessoryDetailDal : EfEntityRepositoryBase<ModelAccessoryDetail, KantarHesapMakinesiContext>, IModelAccessoryDetailDal
    {
        public List<ModelAccessoryDetailDto> GetAllModelAccessoryDetailByModelId(int modelId)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from mma in context.ModelAccessoryDetails
                             join m in context.Models
                             on mma.ModelId equals m.Id
                             join apd in context.AccessoryPackageDetails
                             on mma.AccessoryPackageDetailId equals apd.AccessoryPackageId
                             join p in context.Products
                             on m.ProductId equals p.Id
                             join ap in context.AccessoryPackages
                             on apd.AccessoryPackageId equals ap.Id
                             where mma.ModelId == modelId

                             select new ModelAccessoryDetailDto
                             {
                                 ModelId = m.Id,
                                 ProductName = p.ProductName,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireTotalWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 ModelAccessoryDetailsId = mma.Id,
                                 ModelDetailAccessoryDetailsAccessoryPackageDetailId = apd.Id,
                                 ModelDetailAccessoryDetailsModelId = mma.ModelId,
                                 AccessoryPackageId = ap.Id,
                                 AccessoryPackageName = ap.AccessoryPackageName,
                                 AccessoryPackageDetailId = apd.Id,
                                 AccessoryPackageDetailAccessoryPackageId = apd.AccessoryPackageId,
                                 AccessoryPackageAccessoryId = apd.AccessoryId,
                                 AccessoryPackageAccessoryPcs = apd.AccessoryPcs
                             };

                return result.ToList();
            }
        }

        public List<ModelAccessoryDetailDto> GetAllModelAccessoryDetailDto(Expression<Func<ModelAccessoryDetailDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from mma in context.ModelAccessoryDetails
                             join m in context.Models
                             on mma.ModelId equals m.Id
                             join apd in context.AccessoryPackageDetails
                             on mma.AccessoryPackageDetailId equals apd.AccessoryPackageId
                             join p in context.Products
                             on m.ProductId equals p.Id
                             join ap in context.AccessoryPackages
                             on apd.AccessoryPackageId equals ap.Id

                             select new ModelAccessoryDetailDto
                             {
                                 ModelId = m.Id,
                                 ProductName = p.ProductName,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireTotalWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 ModelAccessoryDetailsId = mma.Id,
                                 ModelDetailAccessoryDetailsAccessoryPackageDetailId = apd.Id,
                                 ModelDetailAccessoryDetailsModelId = mma.ModelId,
                                 AccessoryPackageId = ap.Id,
                                 AccessoryPackageName = ap.AccessoryPackageName,
                                 AccessoryPackageDetailId = apd.Id,
                                 AccessoryPackageDetailAccessoryPackageId = apd.AccessoryPackageId,
                                 AccessoryPackageAccessoryId = apd.AccessoryId,
                                 AccessoryPackageAccessoryPcs = apd.AccessoryPcs
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ModelAccessoryDetailDto GetModelAccessoryDetailDtoById(Expression<Func<ModelAccessoryDetailDto, bool>> filter)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from mma in context.ModelAccessoryDetails
                             join m in context.Models
                             on mma.ModelId equals m.Id
                             join apd in context.AccessoryPackageDetails
                             on mma.AccessoryPackageDetailId equals apd.AccessoryPackageId
                             join p in context.Products
                             on m.ProductId equals p.Id
                             join ap in context.AccessoryPackages
                             on apd.AccessoryPackageId equals ap.Id

                             select new ModelAccessoryDetailDto
                             {
                                 ModelId = m.Id,
                                 ProductName = p.ProductName,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireTotalWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 ModelAccessoryDetailsId = mma.Id,
                                 ModelDetailAccessoryDetailsAccessoryPackageDetailId = apd.Id,
                                 ModelDetailAccessoryDetailsModelId = mma.ModelId,
                                 AccessoryPackageId = ap.Id,
                                 AccessoryPackageName = ap.AccessoryPackageName,
                                 AccessoryPackageDetailId = apd.Id,
                                 AccessoryPackageDetailAccessoryPackageId = apd.AccessoryPackageId,
                                 AccessoryPackageAccessoryId = apd.AccessoryId,
                                 AccessoryPackageAccessoryPcs = apd.AccessoryPcs
                             };
                return result.FirstOrDefault(filter);
            }
        }
    }
}