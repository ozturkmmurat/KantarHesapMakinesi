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
        public string CurrencyName { get; set; }
        public decimal ShateIronPrice { get; set; }
        public decimal IProfilePrice { get; set; }
        public decimal MaterialAmount { get; set; }
        public decimal TotalLaborCost { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GeneralExpenseAmount { get; set; }
        public decimal OverheadIncluded { get; set; }
        public decimal ElectronicAmount { get; set; }

        // Model Propertyleri
        public int ModelId { get; set; }
        public int ModelCostVariableId { get; set; }
        public int ModelProductionTime { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal AdditionalProfitPercentage { get; set; }

        //Cost Variable Propertyleri
        public decimal LaborCostPerHourEuro { get; set; }

    }
}
