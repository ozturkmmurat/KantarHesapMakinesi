using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Abstract.ProductModelCost;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.ProductModelCost
{
    public class EfProductModelCostDetailDal : EfEntityRepositoryBase<ProductModelCostDetail, KantarHesapMakinesiContext>, IProductModelCostDetailDal
    {
    }
}
