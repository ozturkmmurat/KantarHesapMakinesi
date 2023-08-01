using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductModelCost:IEntity
    {
        public int Id { get; set; }
        public int ModelId{ get; set; }
        public string CurrencyName { get; set; }
        public decimal ShateIronPrice { get; set; }
        public decimal IProfilePrice { get; set; }
        public decimal MaterialAmount { get; set; }
        public decimal TotalLaborCost{ get; set; }
        public decimal TotalAmount { get; set; }
        public decimal GeneralExpenseAmount { get; set; }
        public decimal OverheadIncluded { get; set; }
        public decimal ElectronicAmount { get; set; }
    }
}
