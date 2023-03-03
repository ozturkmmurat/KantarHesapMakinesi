using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ModelAccessoryDetailDto : IDto
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

        //Model Aksesuar Detay Propertyleri
        public int ModelAccessoryDetailsId { get; set; }
        public int ModelDetailAccessoryDetailsModelId { get; set; }
        public int ModelDetailAccessoryDetailsAccessoryPackageDetailId { get; set; }
        public int ModelDetailAccessoryPcs { get; set; }

        //Aksesuar Paketi Propertyleri
        public int AccessoryPackageId { get; set; }
        public string AccessoryPackageName { get; set; }

        //Aksesuar Paketi Detay Propertyleri
        public int AccessoryPackageDetailId { get; set; }
        public int AccessoryPackageDetailAccessoryPackageId { get; set; }
        public int AccessoryPackageAccessoryId { get; set; }
        public int AccessoryPackageAccessoryPcs { get; set; }

        //Product Propertyleri
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        //Aksesuar Propertyleri
        public int AccessoryId { get; set; }
        public string AccessoryName { get; set; }
        public decimal AccessoryEuroPrice { get; set; }
        public decimal AccessoryTlPrice { get; set; }
       
    }
}
