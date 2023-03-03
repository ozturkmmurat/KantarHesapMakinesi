using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class AccessoryPackageDetail : IEntity
    {
        public int Id { get; set; }
        public int AccessoryId { get; set; }
        public int AccessoryPackageId { get; set; }
        public int AccessoryPcs { get; set; }
    }
}
