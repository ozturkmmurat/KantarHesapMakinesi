using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class SizeDto : IDto
    {
        public int SizeId { get; set; }
        public int ElectronicId { get; set; }
        public string Aspect { get; set; }
        public string Weight { get; set; }
        public string ProductName { get; set; }
        public string ElectronicName { get; set; }
    }
}
