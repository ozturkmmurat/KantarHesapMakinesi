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
    public class EfModelElectronicDetailDal : EfEntityRepositoryBase<ModelElectronicDetail, KantarHesapMakinesiContext>, IModelElectronicDetailDal
    {
        public List<ModelElectronicDetailDto> GetAllModelElectronicDetailByModelId(int modelId)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from med in context.ModelElectronicDetails
                             join m in context.Models
                             on med.ModelId equals m.Id
                             join e in context.Electronics
                             on med.ElectronicId equals e.Id
                             join p in context.Products
                             on m.ProductId  equals  p.Id
                             where med.ModelId == modelId

                             select new ModelElectronicDetailDto
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
                                 ModelElectronicDetailsId = med.Id,
                                 ModelElectronicDetailsModelId = med.ModelId,
                                 ModelElectronicDetailsElectronicId = med.ElectronicId,
                                 ModelElectronicDetailsElectronicPcs = med.ElectronicPcs,
                                 ElectronicId = e.Id,
                                 ElectronicName = e.ElectronicName,
                                 ElectronicTlPrice = e.ElectronicTlPrice,
                                 ElectronicEuroPrice = e.ElectronicEuroPrice,
                                 ProductId = p.Id
                                 
                                 
                             };

                return result.ToList();
            }

        }

        public ModelElectronicDetailDto GetModelElectronicDetailDtoById(Expression<Func<ModelElectronicDetailDto, bool>> filter)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from med in context.ModelElectronicDetails
                             join m in context.Models
                             on med.ModelId equals m.Id
                             join e in context.Electronics
                             on med.ElectronicId equals e.Id
                             join p in context.Products
                             on m.ProductId equals p.Id

                             select new ModelElectronicDetailDto
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
                                 ModelElectronicDetailsId = med.Id,
                                 ModelElectronicDetailsModelId = med.ModelId,
                                 ModelElectronicDetailsElectronicId = med.ElectronicId,
                                 ElectronicId = e.Id,
                                 ElectronicName = e.ElectronicName,
                                 ElectronicTlPrice = e.ElectronicTlPrice,
                                 ElectronicEuroPrice = e.ElectronicEuroPrice,
                                 ProductId = p.Id
                             };
                return result.FirstOrDefault(filter);
            }
        }
    }
}
