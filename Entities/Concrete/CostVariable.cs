using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class CostVariable : IEntity
    {
        public int Id { get; set; }
        public string CostVariableName { get; set; }
        public decimal IProfile { get; set; }
        public decimal ShateIron { get; set; }
        public decimal FireShateIronAndIProfilePercentage { get; set; }
        public decimal FireTotalPercentAge { get; set; }
        public decimal LaborCostPerHourEuro { get; set; }
        public decimal OverheadPercentage { get; set; }
    }
}
