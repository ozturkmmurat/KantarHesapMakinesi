using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class AccessoryPackageDetailDto : IDto
    {
        // Accessory Package Detail Propertyleri
        public int AccessoryPackageDetailId { get; set; }
        public int AccessoryPackageDetailAccessoryPackageId { get; set; }
        public int AccessoryPackageDetailAccessoryId { 
            get; set; }
        public int AccessoryPackageDetailAccessoryPcs { get; set; }

        //Accessory Propertyleri
        public int AccessoryId { get; set; }
        public string AccessoryName  { get; set; }
        public decimal AccessoryTlPrice { get; set; }
        public decimal AccessoryEuroPrice { get; set; }

        //Accessory Package Propertyleri
        public int AccessoryPackageId { get; set; }
        public string AccessoryPackageName { get; set; }
    }
}
