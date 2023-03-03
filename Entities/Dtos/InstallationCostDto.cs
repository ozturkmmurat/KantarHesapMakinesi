using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class InstallationCostDto : IDto
    {
        // Installation Cost Propertyleri
        public int InstallationCostId { get; set; }
        public int LocationId { get; set; }
        public decimal InstallationTlPrice { get; set; }
        public decimal InstallationEuroPrice { get; set; }

        // Location Propertyleri
        public string CityName { get; set; }

    }
}
