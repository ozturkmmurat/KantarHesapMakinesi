using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductModelCostDetailDto : IDto
    {
        //pmc --> ProductModelCostDetail
        //ProductModelCostDetail Propertyleri
        public int PmcDetailId { get; set; }
        public int PmcInstallationCostId { get; set; }
        public int PmcProductModelCostId { get; set; }
        public decimal PmcInstallationIncluded { get; set; }
        public decimal PmcSalesPrice { get; set; }
        public decimal PmcTurkeySalesPrice { get; set; }
        public decimal PmcProfitPrice { get; set; }
        public decimal PmcTurkeySalesDiscount { get; set; }
        public decimal PmcTurkeySalesDiscountPrice { get; set; }
        public decimal PmcExportFinalDiscount { get; set; }
        public decimal PmcExportFinalDiscountPrice { get; set; }

        //pm --> ProductModelCost
        //ProductModelCost
        public decimal PmProfitPercentage { get; set; }

        //Ic --> InstallationCost
        //InstallationCost Details
        public int IcId { get; set; }
        public int IcLocationId { get; set; }
        public decimal IcInstallationTlPrice { get; set; }
        public decimal IcInstallationEuroPrice { get; set; }

        //Location Propertyleri
        public int LocationId { get; set; }
        public string LocationCityName { get; set; }
    }
}
