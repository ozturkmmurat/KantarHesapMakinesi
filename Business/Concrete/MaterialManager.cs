using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Utilities.CostsCurrencyCalculation;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class MaterialManager : IMaterialService
    {
        IMaterialDal _materialDal;
        public MaterialManager(IMaterialDal materialDal)
        {
            _materialDal = materialDal;
        }
        [SecuredOperation("admin")]
        public IResult Add(Material material)
        {
            if (material != null)
            {
                material.MaterialEuroPrice = TCMBCalculation.EuroCalculation(material.MaterialTlPrice);
                _materialDal.Add(material);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [SecuredOperation("admin")]
        public IResult Delete(Material material)
        {
            if (material != null)
            {
                _materialDal.Delete(material);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<Material>> GetAllMaterial()
        {
            var result = _materialDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Material>>(result);
            }
            return new ErrorDataResult<List<Material>>();
        }

        public IDataResult<Material> GetById(int id)
        {
            var result = _materialDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<Material>(result);
            }
            return new ErrorDataResult<Material>();
        }
        [SecuredOperation("admin")]
        public IResult Update(Material material)
        {
            if (material != null)
            {
                _materialDal.Update(material);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
