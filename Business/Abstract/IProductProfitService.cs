using Core.Utilities.Result.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductProfitService
    {
        IDataResult<ProductProfit> GetByProductId(int productId);
        IResult Add(ProductProfit productProfit);
        IResult Update(ProductProfit productProfit);
    }
}
