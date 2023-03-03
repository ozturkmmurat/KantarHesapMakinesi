using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductModelCostDto : IDto
    {
        // Product Model Cost Propertyleri
        public int ProductModelCostId { get; set; }
        public decimal ProductModelCostShateIronEuroPrice { get; set; }
        public decimal ProductModelCostIProfileEuroPrice { get; set; }
        public decimal ProductModelCostMaterialTlAmount { get; set; }
        public decimal ProductModelCostMaterialEuroAmount { get; set; }
        public decimal ProductModelCostLaborCostPerHour { get; set; }
        public decimal ProductModelCostTotalLaborCost { get; set; }
        public decimal ProductModelCostTotalAmount { get; set; }
        public int ProductModelCostOverheadPercentage { get; set; }
        public decimal ProductModelCostGeneralExpenseAmount { get; set; }
        public decimal ProductModelCostOverheadIncluded { get; set; }
        public decimal ProductModelCostElectronicTlAmount { get; set; }
        public decimal ProductModelCostElectronicEuroAmount { get; set; }
        public decimal ProductModelCostAccessoriesTlAmount { get; set; }
        public decimal ProductModelCostAccessoriesEuroAmount { get; set; }

        // Model Propertyleri
        public int ModelId { get; set; }
        public int ModelProductionTime { get; set; }

        // Product Propertyleri
        public int ProductId { get; set; }

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
