using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;
using Entities.Dtos.User;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, KantarHesapMakinesiContext>, IUserOperationClaimDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfUserOperationClaimDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
