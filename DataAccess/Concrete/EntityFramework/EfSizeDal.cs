﻿using Core.DataAccess.EntityFramework;
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
    public class EfSizeDal : EfEntityRepositoryBase<Size, KantarHesapMakinesiContext>, ISizeDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfSizeDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
    }
}
