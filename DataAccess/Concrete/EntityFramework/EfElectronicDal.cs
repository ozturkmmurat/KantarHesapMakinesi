using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfElectronicDal : EfEntityRepositoryBase<Electronic, KantarHesapMakinesiContext>, IElectronicDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfElectronicDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
