using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ModelElectronicDetailDto : IDto
    {
        //Model Propertyleri
        public int ModelId { get; set; }
        public string ModelMostSizeKg { get; set; }
        public int ModelNetWeight { get; set; }
        public decimal ModelFirePercentage { get; set; }
        public int ModelShateIronWeight { get; set; }
        public int ModelIProfilWeight { get; set; }
        public decimal ModelFireShateIronWeight { get; set; }
        public decimal ModelFireProfileWeight { get; set; }
        public decimal ModelFireTotalWeight { get; set; }
        public int ModelGateWeight { get; set; }
        public int ModelProductionTime { get; set; }

        //Model Elektronik Detay Propertyleri
        public int ModelElectronicDetailsId { get; set; }
        public int ModelElectronicDetailsModelId { get; set; }
        public int ModelElectronicDetailsElectronicId { get; set; }
        public int ModelElectronicDetailsElectronicPcs { get; set; }

        //Product Propertyleri
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        //Elektronik Propertyleri
        public int ElectronicId { get; set; }
        public string ElectronicName { get; set; }
        public decimal ElectronicEuroPrice { get; set; }
        public decimal ElectronicTlPrice { get; set; }
    }
}
