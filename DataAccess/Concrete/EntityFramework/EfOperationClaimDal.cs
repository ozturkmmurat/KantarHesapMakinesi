using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, KantarHesapMakinesiContext>, IOperationClaimDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfOperationClaimDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
