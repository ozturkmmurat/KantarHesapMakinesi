using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class SizeContentDto : IEntity
    {
        public int SizeContentId { get; set; }
        public int SizeId { get; set; }
        public int ElectronicId { get; set; }
        public string ElectronicName { get; set; }
        public decimal ElectronicTlPrice { get; set; }
        public decimal ElectronicEuroPrice { get; set; }
        public int ElectronicPcs { get; set; }

    }
}
