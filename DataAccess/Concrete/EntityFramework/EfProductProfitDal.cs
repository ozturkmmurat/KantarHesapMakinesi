using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductProfitDal : EfEntityRepositoryBase<ProductProfit, KantarHesapMakinesiContext>, IProductProfitDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfProductProfitDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
