using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Accessory : IEntity
    {
        public int Id { get; set; }
        public string AccessoryName{ get; set; }
        public decimal AccessoryEuroPrice { get; set; }
        public decimal AccessoryTlPrice { get; set; }
    }
}
