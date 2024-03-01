using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class ProductDto : IDto
    {
        //Product propertyleri
        public int ProductId { get; set; }
        public int ProductProfitId { get; set; }
        public string ProductName { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal AdditionalProfitPercentage { get; set; }
    }
}
