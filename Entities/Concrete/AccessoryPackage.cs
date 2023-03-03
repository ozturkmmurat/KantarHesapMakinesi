using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AccessoryPackage : IEntity
    {
        public int Id { get; set; }
        public string AccessoryPackageName { get; set; }
    }
}
