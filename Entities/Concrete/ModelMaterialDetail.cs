using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ModelMaterialDetail : IEntity
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int MaterialId { get; set; }
        public int MaterialPcs { get; set; }
    }
}
