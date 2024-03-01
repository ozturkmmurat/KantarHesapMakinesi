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
        private readonly KantarHesapMakinesiContext _context;

        public EfModelDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
        public List<ModelDto> GetAllModelDto()
        {
                var result = from m in _context.Models
                             join cv in _context.CostVariables
                             on m.CostVariableId equals cv.Id

                             select new ModelDto
                             {
                                 ModelId = m.Id,
                                 ModelCostVariableId = m.CostVariableId,
                                 ModelProductId = m.ProductId,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireIProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireIProfileWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 CostVariableId = cv.Id,
                                 CostVariableIProfile = cv.IProfile,
                                 CostVariableShateIron = cv.ShateIron,
                                 CostVariableFireShateIronAndIProfilePercentage = cv.FireShateIronAndIProfilePercentage,
                                 CostVariableFireTotalPercentAge = cv.FireTotalPercentAge,
                                 
                             };
                return result.ToList();
        }

        public ModelDto GetModelModelDtoById(int id)
        {
                var result = from m in _context.Models
                             join cv in _context.CostVariables
                             on m.CostVariableId equals cv.Id
                             where m.Id == id
                             select new ModelDto
                             {
                                 ModelId = m.Id,
                                 ModelCostVariableId = m.CostVariableId,
                                 ModelProductId = m.ProductId,
                                 ModelMostSizeKg = m.MostSizeKg,
                                 ModelShateIronWeight = m.ShateIronWeight,
                                 ModelIProfilWeight = m.IProfilWeight,
                                 ModelFireShateIronWeight = m.FireShateIronWeight,
                                 ModelFireIProfileWeight = m.FireIProfileWeight,
                                 ModelFireTotalWeight = m.FireIProfileWeight,
                                 ModelProductionTime = m.ProductionTime,
                                 CostVariableId = cv.Id,
                                 CostVariableIProfile = cv.IProfile,
                                 CostVariableShateIron = cv.ShateIron,
                                 CostVariableFireShateIronAndIProfilePercentage = cv.FireShateIronAndIProfilePercentage,
                                 CostVariableFireTotalPercentAge = cv.FireTotalPercentAge,
                             };
                return result.FirstOrDefault();
        }
    }
}
