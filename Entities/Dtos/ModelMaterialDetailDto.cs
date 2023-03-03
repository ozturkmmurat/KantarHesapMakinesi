using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ModelMaterialDetailDto : IDto
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

        //ModelMaterialDetail Propertyleri
        public int ModelMaterialDetailId { get; set; }
        public int ModelMaterialDetailModelId { get; set; }
        public int ModelMaterialDetailMaterialId { get; set; }


        //Product Propertyleri
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        //Materyal Propertyleri
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public decimal MaterialEuroPrice { get; set; }
        public decimal MaterialTlPrice { get; set; }


        //Model Material Detay Tablosu
        public int MaterailDetailId { get; set; }
        public int MaterialPcs { get; set; }


    }
}
