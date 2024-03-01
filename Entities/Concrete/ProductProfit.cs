using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductProfit : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal AdditionalProfitPercentage { get; set; }
    }
}
