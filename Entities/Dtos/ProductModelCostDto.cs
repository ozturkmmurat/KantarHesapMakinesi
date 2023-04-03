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
        public decimal ProductModelCostTotalLaborCostTl { get; set; }
        public decimal ProductModelCostTotalLaborCostEuro { get; set; }
        public decimal ProductModelCostTotalAmount { get; set; }
        public decimal ProductModelCostGeneralExpenseAmount { get; set; }
        public decimal ProductModelCostOverheadIncluded { get; set; }
        public decimal ProductModelCostElectronicTlAmount { get; set; }
        public decimal ProductModelCostElectronicEuroAmount { get; set; }
        public decimal ProductModelCostProfitPercentage { get; set; }
        public decimal ProductModelCostAdditionalProfitPercentage { get; set; }

        // Model Propertyleri
        public int ModelId { get; set; }
        public int ModelCostVariableId { get; set; }
        public int ModelProductionTime { get; set; }

        //Cost Variable Propertyleri
        public decimal LaborCostPerHourEuro { get; set; }

    }
}
