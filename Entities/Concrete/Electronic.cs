using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Electronic : IEntity
    {
        public int Id { get; set; }
        public string ElectronicName { get; set; }
        public decimal ElectronicEuroPrice { get; set; }
        public decimal ElectronicTlPrice { get; set; }
    }
}
