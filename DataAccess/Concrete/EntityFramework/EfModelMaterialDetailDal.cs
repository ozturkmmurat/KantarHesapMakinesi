using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.Dtos;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfModelMaterialDetailDal : EfEntityRepositoryBase<ModelMaterialDetail, KantarHesapMakinesiContext>, IModelMaterialDetailDal
    {
        public List<ModelMaterialDetailDto> GetModelDetailsByModelId(int modelId, int productId)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from m in context.Models
                             where m.Id == modelId

                             select new ModelMaterialDetailDto
                             {
                                 ModelId = m.Id,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireTotalWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime
                             };
                                 
                return result.ToList();
            }
        }
    }
}
