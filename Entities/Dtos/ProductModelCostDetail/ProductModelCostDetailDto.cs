using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductModelCostDetailDto : IDto
    {
        //ProductModelCostDetail Propertyleri
        public int ProductModelCostDetailId { get; set; }
        public int ProductModelCostDetailInstallationCostId { get; set; }
        public int ProductModelCostDetailProductModelCostId { get; set; }
        public decimal ProductModelCostDetailInstallationIncluded { get; set; }
        public decimal ProductModelCostDetailSalesPrice { get; set; }
        public decimal ProductModelCostDetailTurkeySalesPrice { get; set; }
        public decimal ProductModelCostDetailProfitPercentage { get; set; }
        public decimal ProductModelCostDetailProfitPrice { get; set; }
        public decimal ProductModelCostDetailTurkeySalesDiscount { get; set; }
        public decimal ProductModelCostDetailTurkeySalesDiscountPrice { get; set; }
        public decimal ProductModelCostDetailExportFinalDiscount { get; set; }
        public decimal ProductModelCostDetailExportFinalDiscountPrice { get; set; }


        //InstallationCost Details
        public int InstallationCostId { get; set; }
        public int InstallationCostLocationId { get; set; }
        public decimal InstallationCostInstallationTlPrice { get; set; }
        public decimal InstallationCostInstallationEuroPrice { get; set; }

        //Location Propertyleri
        public int LocationId { get; set; }
        public string LocationCityName { get; set; }
    }
}
