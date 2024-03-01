using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPasswordResetDal : EfEntityRepositoryBase<PasswordReset, KantarHesapMakinesiContext>, IPasswordResetDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfPasswordResetDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
