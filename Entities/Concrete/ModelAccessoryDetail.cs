using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ModelAccessoryDetail : IEntity
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int AccessoryPackageDetailId{ get; set; }
    }
}
