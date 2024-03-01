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
    public class EfSizeContentDal : EfEntityRepositoryBase<SizeContent, KantarHesapMakinesiContext>, ISizeContentDal
    {
        private readonly KantarHesapMakinesiContext _context;

        public EfSizeContentDal(KantarHesapMakinesiContext context) : base(context)
        {
            _context = context;
        }
        public List<SizeContentDto> GetAllSizeContentDto(Expression<Func<SizeContentDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from sc in context.SizeContents
                             join s in context.Sizes
                             on sc.SizeId equals s.Id
                             join e in context.Electronics
                             on sc.ElectronicId equals e.Id

                             select new SizeContentDto
                             {
                                 SizeContentId = sc.Id,
                                 SizeId = s.Id,
                                 ElectronicId = e.Id,
                                 ElectronicName = e.ElectronicName,
                                 ElectronicTlPrice = e.ElectronicTlPrice,
                                 ElectronicEuroPrice = e.ElectronicEuroPrice,
                                 ElectronicPcs = sc.ElectronicPcs
                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public SizeContentDto GetBySizeContentDto(Expression<Func<SizeContentDto, bool>> filter)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from sc in context.SizeContents
                             join s in context.Sizes
                             on sc.SizeId equals s.Id
                             join e in context.Electronics
                             on sc.ElectronicId equals e.Id

                             select new SizeContentDto
                             {
                                 SizeContentId = sc.Id,
                                 SizeId = s.Id,
                                 ElectronicId = e.Id,
                                 ElectronicName = e.ElectronicName,
                                 ElectronicTlPrice = e.ElectronicTlPrice,
                                 ElectronicEuroPrice = e.ElectronicEuroPrice,
                                 ElectronicPcs = sc.ElectronicPcs
                             };
                return result.FirstOrDefault(filter);
            }
        }
    }
}
