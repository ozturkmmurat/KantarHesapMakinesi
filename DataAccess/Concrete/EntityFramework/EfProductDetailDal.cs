using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDetailDal : EfEntityRepositoryBase<ProductDetail, KantarHesapMakinesiContext>, IProductDetailDal
    {
        public List<ProductDetailDto> GetAllProductDto(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pd in context.ProductDetails
                             join p in context.Products
                             on pd.ProductId equals p.Id
                             join m in context.Models
                             on pd.ModelId equals m.Id

                             select new ProductDetailDto
                             {
                                 ProductId = p.Id,
                                 ProductName = p.ProductName,
                                 ModelName = m.MostSizeKg
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
