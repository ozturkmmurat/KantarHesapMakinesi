using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.ProductModelCostDetail
{
    public class ProductModelCostDetailSelectListDto : IDto
    {
        //Product Model Cost Detail Propertyleri
        public int ProductModelCostId { get; set; }

        //InstallationCost Details Propertyleri
        public int InstallationCostId { get; set; }
        public int InstallationCostLocationId { get; set; }

        //Location Propertyleri
        public string LocationCityName { get; set; }
    }
}
