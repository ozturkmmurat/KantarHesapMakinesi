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
    public class EfProductDal : EfEntityRepositoryBase<Product, KantarHesapMakinesiContext>, IProductDal
    {
        public List<ProductDto> GetAllProductDto(Expression<Func<ProductDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             select new ProductDto
                             {
                                 ProductId = p.Id,
                                 CategoryId = c.Id,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
