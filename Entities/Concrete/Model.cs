using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ProductId { get; set; }
        public int CostVariableId { get; set; }
        public string MostSizeKg { get; set; }
        public int ShateIronWeight { get; set; }
        public int IProfilWeight { get; set; }
        public decimal FireShateIronWeight { get; set; }
        public decimal FireIProfileWeight { get; set; }
        public decimal FireTotalWeight { get; set; }
        public int ProductionTime { get; set; }
    }
}
