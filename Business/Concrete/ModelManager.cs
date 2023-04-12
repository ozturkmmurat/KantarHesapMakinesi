using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.Dtos;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Logging;

namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class ModelManager : IModelService
    {
        IModelDal _modelDal;
        ICostVariableService _costVariableService;
        public ModelManager(IModelDal modelDal, ICostVariableService costVariableService)
        {
            _modelDal = modelDal;
            _costVariableService = costVariableService;
        }
        [SecuredOperation("admin")]
        public IResult Add(ModelDto modelDto)
        {
            if (modelDto != null)
            {
                var result = ModelInFormation(modelDto);
                Model model = new Model()
                {
                    ProductId = result.ModelProductId,
                    SizeId = result.ModelSizeId,
                    CostVariableId = result.CostVariableId,
                    MostSizeKg = result.ModelMostSizeKg,
                    ShateIronWeight = result.ModelShateIronWeight,
                    IProfilWeight = result.ModelIProfilWeight,
                    FireShateIronWeight = result.ModelFireShateIronWeight,
                    FireIProfileWeight = result.ModelFireIProfileWeight,
                    FireTotalWeight = result.ModelFireTotalWeight,
                    ProductionTime = result.ModelProductionTime,
                };
                _modelDal.Add(model);
                return new SuccessResult(Messages.DataAdded);
            }
            return new ErrorResult(Messages.UnDataAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(Model model)
        {
            if (model != null)
            {
                _modelDal.Delete(model);
                return new SuccessResult(Messages.DataDeleted);
            }
            return new ErrorResult(Messages.UnDataDeleted);
        }
        public IDataResult<List<Model>> GetAllByProductId(int productId)
        {
            var result = _modelDal.GetAll(x => x.ProductId == productId);
            if (result != null)
            {
                return new SuccessDataResult<List<Model>>(result);
            }
            return new ErrorDataResult<List<Model>>(Messages.GetByAllDefault);
        }
        [SecuredOperation("admin")]
        public IDataResult<List<Model>> GetAllModel()
        {
            var result = _modelDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Model>>(result);
            }
            return new ErrorDataResult<List<Model>>();
        }
        public IDataResult<List<ModelDto>> GetAllModelDto()
        {
            var result = _modelDal.GetAllModelDto();
            if (result != null)
            {
                return new SuccessDataResult<List<ModelDto>>(result);
            }
            return new ErrorDataResult<List<ModelDto>>();
        }
        [SecuredOperation("admin")]
        public IDataResult<Model> GetById(int id)
        {
            var result = _modelDal.Get(x=> x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Model>(result);
            }
            return new ErrorDataResult<Model>();
        }

        [SecuredOperation("admin")]
        public IDataResult<ModelDto> GetModelDtoById(int id)
        {
            var result = _modelDal.GetModelModelDtoById(id);
            if (result != null)
            {
                return new SuccessDataResult<ModelDto>(result);
            }
            return new ErrorDataResult<ModelDto>();
        }
        [SecuredOperation("admin")]
        public ModelDto ModelInFormation(ModelDto modelDto)
        {
            var costVariableFirst = _costVariableService.GetAllCostVariable().Data.FirstOrDefault();

            if (modelDto.CostVariableId != 0)  // Eğer kullanıcı Model deki verilerin hesaplanması için bir değer seçtiyse CostVariable dan burası çalışacak
            {
                var getCostVariable = _costVariableService.GetById(modelDto.CostVariableId);
                modelDto.CostVariableId = getCostVariable.Data.Id; // Hata
                modelDto.ModelFireShateIronWeight = ((modelDto.ModelShateIronWeight + (modelDto.ModelShateIronWeight * getCostVariable.Data.FireShateIronAndIProfilePercentage / 100)));
                modelDto.ModelFireIProfileWeight = (modelDto.ModelIProfilWeight + (modelDto.ModelIProfilWeight * getCostVariable.Data.FireShateIronAndIProfilePercentage / 100));
                var decimalShateIronWeight = Convert.ToDecimal(modelDto.ModelShateIronWeight);
                var decimalIProfilWeight = Convert.ToDecimal(modelDto.ModelIProfilWeight);
                modelDto.ModelFireTotalWeight = (Math.Round((decimalShateIronWeight + decimalIProfilWeight) * getCostVariable.Data.FireTotalPercentAge));
                return modelDto;
            }
            else //Kullanıcı cost variable dan hesaplama için herhangi bir değer seçmediyse Cost variable tablosunda ilk bulduğu veri üzerinden hesaplama yapıyor.
            {
                
                modelDto.CostVariableId = costVariableFirst.Id;
                modelDto.ModelFireShateIronWeight = (modelDto.ModelShateIronWeight + (modelDto.ModelShateIronWeight * costVariableFirst.FireShateIronAndIProfilePercentage / 100));
                modelDto.ModelFireIProfileWeight = (modelDto.ModelIProfilWeight + (modelDto.ModelIProfilWeight * costVariableFirst.FireShateIronAndIProfilePercentage / 100));
                var decimalShateIronWeight = Convert.ToDecimal(modelDto.ModelShateIronWeight);
                var decimalIProfilWeight = Convert.ToDecimal(modelDto.ModelIProfilWeight);
                modelDto.ModelFireTotalWeight = (Math.Round((decimalShateIronWeight + decimalIProfilWeight) * costVariableFirst.FireTotalPercentAge));
                return modelDto;
            }
            return null;
          
        }
        [SecuredOperation("admin")]
        public IResult Update(ModelDto modelDto)
        {
            if (modelDto != null)
            {
                var result = ModelInFormation(modelDto);
                Model model = new Model()
                {
                    Id = result.ModelId,
                    ProductId = result.ModelProductId,
                    CostVariableId = result.CostVariableId,
                    SizeId = result.ModelSizeId,
                    MostSizeKg = result.ModelMostSizeKg,
                    ShateIronWeight = result.ModelShateIronWeight,
                    IProfilWeight = result.ModelIProfilWeight,
                    FireShateIronWeight = result.ModelFireShateIronWeight,
                    FireIProfileWeight = result.ModelFireIProfileWeight,
                    FireTotalWeight = result.ModelFireTotalWeight,
                    ProductionTime = result.ModelProductionTime,
                };
                _modelDal.Update(model);
                return new SuccessResult(Messages.DataUpdate);
            }
            return new ErrorResult(Messages.UnDataUpdate);
        }
    }
}
