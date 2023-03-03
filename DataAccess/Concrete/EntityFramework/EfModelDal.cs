using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfModelDal : EfEntityRepositoryBase<Model, KantarHesapMakinesiContext>, IModelDal
    {
        public List<ModelDto> GetAllModelDto()
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from m in context.Models
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ModelDto
                             {
                                 ModelId = m.Id,
                                 ModelCostVariableId = m.CostVariableId,
                                 ModelProductId = m.ProductId,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFire = m.Fire,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireIProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireIProfileWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 CostVariableId = cv.Id,
                                 CostVariableIProfile = cv.IProfile,
                                 CostVariableShateIron = cv.ShateIron,
                                 CostVariableFireShateIronAndIProfilePercentage = cv.FireShateIronAndIProfilePercentage,
                                 CostVariableFireTotalPercentAge = cv.FireTotalPercentAge,
                                 CostVariableFirePercentAge = cv.FirePercentAge,
                                 
                             };
                return result.ToList();
            }
        }

        public ModelDto GetModelModelDtoById(int id)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from m in context.Models
                             join cv in context.CostVariables
                             on m.CostVariableId equals cv.Id
                             where m.Id == id
                             select new ModelDto
                             {
                                 ModelId = m.Id,
                                 ModelCostVariableId = m.CostVariableId,
                                 ModelProductId = m.ProductId,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelNetWeight = m.NetWeight,
                                 ModelFire = m.Fire,
                                 ModelFirePercentage = m.FirePercentage,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireIProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireIProfileWeight,
                                 ModelGateWeight = m.GateWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 CostVariableId = cv.Id,
                                 CostVariableIProfile = cv.IProfile,
                                 CostVariableShateIron = cv.ShateIron,
                                 CostVariableFireShateIronAndIProfilePercentage = cv.FireShateIronAndIProfilePercentage,
                                 CostVariableFireTotalPercentAge = cv.FireTotalPercentAge,
                                 CostVariableFirePercentAge = cv.FirePercentAge
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
