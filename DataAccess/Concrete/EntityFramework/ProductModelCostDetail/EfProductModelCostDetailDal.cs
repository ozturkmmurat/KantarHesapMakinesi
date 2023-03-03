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
    public class EfProductModelCostDetailDal : EfEntityRepositoryBase<Entities.Concrete.ProductModelCostDetail, KantarHesapMakinesiContext>, IProductModelCostDetailDal
    {
        public List<ProductModelCostDetailDto> GetAllProductModelCostDetailDtos(Expression<Func<ProductModelCostDetailDto, bool>> filter = null)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmcd in context.ProductModelCostDetails
                             join m in context.Models
                             on pmcd.ProductModelCostId equals m.Id
                             join ic in context.InstallationCosts
                             on pmcd.InstallationCostId equals ic.Id
                             join l in context.Locations
                             on ic.LocationId equals l.Id

                             select new ProductModelCostDetailDto
                             {
                                 ProductModelCostDetailId = pmcd.Id,
                                 ProductModelCostDetailInstallationCostId = pmcd.InstallationCostId,
                                 ProductModelCostDetailProductModelCostId = pmcd.ProductModelCostId,
                                 ProductModelCostDetailInstallationIncluded = pmcd.InstallationIncluded,
                                 ProductModelCostDetailSalesPrice = pmcd.SalesPrice,
                                 ProductModelCostDetailTurkeySalesPrice = pmcd.TurkeySalesPrice,
                                 ProductModelCostDetailProfitPercentage = pmcd.ProfitPercentage,
                                 ProductModelCostDetailProfitPrice = pmcd.ProfitPrice,
                                 ProductModelCostDetailTurkeySalesDiscount = pmcd.TurkeySalesDiscount,
                                 ProductModelCostDetailTurkeySalesDiscountPrice = pmcd.TurkeySalesDiscountPrice,
                                 ProductModelCostDetailExportFinalDiscount = pmcd.ExportFinalDiscount,
                                 ProductModelCostDetailExportFinalDiscountPrice = pmcd.ExportFinalDiscountPrice,



                                 InstallationCostId = ic.Id,
                                 InstallationCostLocationId = ic.LocationId,
                                 InstallationCostInstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationCostInstallationEuroPrice = ic.InstallationEuroPrice,

                                 LocationId = l.Id,
                                 LocationCityName = l.CityName

                             };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }

        public ProductModelCostDetailDto GetByIdProductModelCostDetailDto(Expression<Func<ProductModelCostDetailDto, bool>> filter)
        {
            using (KantarHesapMakinesiContext context = new KantarHesapMakinesiContext())
            {
                var result = from pmcd in context.ProductModelCostDetails
                             join m in context.Models
                             on pmcd.ProductModelCostId equals m.Id
                             join ic in context.InstallationCosts
                             on pmcd.InstallationCostId equals ic.Id
                             join l in context.Locations
                             on ic.LocationId equals l.Id

                             select new ProductModelCostDetailDto
                             {
                                 ProductModelCostDetailId = pmcd.Id,
                                 ProductModelCostDetailInstallationCostId = pmcd.InstallationCostId,
                                 ProductModelCostDetailProductModelCostId = pmcd.ProductModelCostId,
                                 ProductModelCostDetailInstallationIncluded = pmcd.InstallationIncluded,
                                 ProductModelCostDetailSalesPrice = pmcd.SalesPrice,
                                 ProductModelCostDetailTurkeySalesPrice = pmcd.TurkeySalesPrice,
                                 ProductModelCostDetailProfitPercentage = pmcd.ProfitPercentage,
                                 ProductModelCostDetailProfitPrice = pmcd.ProfitPrice,
                                 ProductModelCostDetailTurkeySalesDiscount = pmcd.TurkeySalesDiscount,
                                 ProductModelCostDetailTurkeySalesDiscountPrice = pmcd.TurkeySalesDiscountPrice,
                                 ProductModelCostDetailExportFinalDiscount = pmcd.ExportFinalDiscount,
                                 ProductModelCostDetailExportFinalDiscountPrice = pmcd.ExportFinalDiscountPrice,

                                 InstallationCostId = ic.Id,
                                 InstallationCostLocationId = ic.LocationId,
                                 InstallationCostInstallationTlPrice = ic.InstallationTlPrice,
                                 InstallationCostInstallationEuroPrice = ic.InstallationEuroPrice,

                                 LocationId = l.Id,
                                 LocationCityName = l.CityName
                             };
                return result.FirstOrDefault(filter);
            }
        }
    }
}
