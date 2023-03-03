using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductModelCost:IEntity
    {
        public int Id { get; set; }
        public decimal ShateIronEuroPrice { get; set; }
        public decimal IProfileEuroPrice { get; set; }
        public decimal MaterialTlAmount { get; set; }
        public decimal MaterialEuroAmount { get; set; }
        public decimal LaborCostPerHour { get; set; }
        public decimal TotalLaborCost { get; set; }
        public decimal TotalAmount { get; set; }
        public int OverheadPercentage { get; set; }
        public decimal GeneralExpenseAmount { get; set; }
        public decimal OverheadIncluded { get; set; }
        public decimal ElectronicTlAmount { get; set; }
        public decimal ElectronicEuroAmount { get; set; }
        public decimal AccessoriesTlAmount { get; set; }
        public decimal AccessioresEuroAmount { get; set; }
    }
}
