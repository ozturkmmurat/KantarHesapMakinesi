﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Context;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCostVariableDal : EfEntityRepositoryBase<CostVariable, KantarHesapMakinesiContext>, ICostVariableDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfCostVariableDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
