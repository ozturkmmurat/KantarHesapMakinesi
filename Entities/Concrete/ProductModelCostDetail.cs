using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductModelCostDetail : IEntity
    {
        public int Id { get; set; }
        public int InstallationCostId { get; set; }
        public int ProductModelCostId { get; set; }
        public decimal InstallationIncluded { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal TurkeySalesPrice { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal ProfitPrice { get; set; }
        public decimal TurkeySalesDiscount { get; set; }
        public decimal TurkeySalesDiscountPrice { get; set; }
        public decimal ExportFinalDiscount { get; set; }
        public decimal ExportFinalDiscountPrice { get; set; }
    }
}
