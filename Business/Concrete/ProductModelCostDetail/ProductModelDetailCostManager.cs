using Business.Abstract;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract;
using DataAccess.Abstract.ProductModelCostDetail;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.ProductModelCostDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductModelDetailCostManager : IProductModelCostDetailService
    {
        IProductModelCostDetailDal _productModelCostDetailDal;
        IProductModelCostDetailSelectListDal _productModelCostDetailSelectListDal;
        public ProductModelDetailCostManager
            (
            IProductModelCostDetailDal productModelCostDetailDal,
            IProductModelCostDetailSelectListDal productModelCostDetailSelectListDal
            )
        {
            _productModelCostDetailDal = productModelCostDetailDal;
            _productModelCostDetailSelectListDal = productModelCostDetailSelectListDal;
        }
        public IResult Add(ProductModelCostDetail productModelCostDetail)
        {
            if (productModelCostDetail != null)
            {
                _productModelCostDetailDal.Add(productModelCostDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IResult Delete(ProductModelCostDetail productModelCostDetail)
        {
            if (productModelCostDetail != null)
            {
                _productModelCostDetailDal.Delete(productModelCostDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        public IDataResult<List<ProductModelCostDetail>> GetAllInstallationCostDetail()
        {
            var result = _productModelCostDetailDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<ProductModelCostDetail>>(result);
            }
            return new ErrorDataResult<List<ProductModelCostDetail>>();
        }

        public IDataResult<ProductModelCostDetail> GetById(int id)
        {
            var result = _productModelCostDetailDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDetail>(result);
            }
            return new ErrorDataResult<ProductModelCostDetail>();
        }

        public IDataResult<ProductModelCostDetailDto> GetProductModelCostDetailLocationModelId(int locationId, int modelId)
        {
            var result = _productModelCostDetailDal.GetByIdProductModelCostDetailDto(x => x.InstallationCostLocationId == locationId && x.ProductModelCostDetailProductModelCostId == modelId);
            if (result != null)
            {
                return new SuccessDataResult<ProductModelCostDetailDto>(result);
            }
            return new ErrorDataResult<ProductModelCostDetailDto>();
        }

        public IResult Update(ProductModelCostDetail productModelCostDetail)
        {
            if (productModelCostDetail != null)
            {
                _productModelCostDetailDal.Update(productModelCostDetail);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
