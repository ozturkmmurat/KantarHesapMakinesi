using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ModelDto : IDto
    {
        // Model Propertyleri 
        public int ModelId { get; set; }
        public int ModelProductId { get; set; }
        public int ModelCostVariableId { get; set; }
        public string ModelMostSizeKg { get; set; }
        public decimal ModelFirePercentage { get; set; }
        public int ModelShateIronWeight { get; set; }
        public int ModelIProfilWeight { get; set; }
        public decimal ModelFireShateIronWeight { get; set; }
        public decimal ModelFireIProfileWeight { get; set; }
        public decimal ModelFireTotalWeight { get; set; }
        public int ModelProductionTime { get; set; }


        // Cost Variable Propertyleri
        public int CostVariableId { get; set; }
        public decimal CostVariableIProfile { get; set; }
        public decimal CostVariableShateIron { get; set; }
        public decimal CostVariableFireShateIronAndIProfilePercentage { get; set; }
        public decimal CostVariableFireTotalPercentAge { get; set; }
        public decimal CostVariableFirePercentAge { get; set; }
    }
}
