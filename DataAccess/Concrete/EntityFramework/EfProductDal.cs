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
        private readonly KantarHesapMakinesiContext _context;

        public EfProductDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }

        public List<ProductDto> GetAllProductDto(Expression<Func<ProductDto, bool>> filter = null)
        {
                var result = from p in _context.Products
                             join pf in _context.ProductProfits
                             on p.Id equals pf.ProductId
                             select new ProductDto
                             {
                                 ProductId = p.Id,
                                 ProductProfitId = pf.Id,
                                 ProductName = p.ProductName,
                                 ProfitPercentage = pf.ProfitPercentage,
                                 AdditionalProfitPercentage = pf.AdditionalProfitPercentage
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
        }
    }
}