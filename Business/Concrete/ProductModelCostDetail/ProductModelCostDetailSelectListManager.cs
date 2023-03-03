using Business.Abstract.ProductModelCostDetail;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using DataAccess.Abstract.ProductModelCostDetail;
using Entities.Dtos.ProductModelCostDetail;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductModelCostDetailSelectListManager : IProductModelCostDetailSelectListService
    {
        IProductModelCostDetailSelectListDal _productModelCostDetailSelectListDal;
        public ProductModelCostDetailSelectListManager(IProductModelCostDetailSelectListDal productModelCostDetailSelectListDal)
        {
            _productModelCostDetailSelectListDal = productModelCostDetailSelectListDal;
        }
        public IDataResult<List<ProductModelCostDetailSelectListDto>> GetAllProductModelCostDetailDtoByProductModelCostId(int productModelCostId)
        {
            var result = _productModelCostDetailSelectListDal.GetAllProductModelCostDetailDtos(x => x.ProductModelCostId == productModelCostId);

            if (result != null)
            {
                return new SuccessDataResult<List<ProductModelCostDetailSelectListDto>>(result);
            }
            return new ErrorDataResult<List<ProductModelCostDetailSelectListDto>>();
        }
    }
}
