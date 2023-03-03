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
        public int CategoryId { get; set; }
        public string ProductName { get; set; }

        //Category Propertyleri
        public string CategoryName { get; set; }
    }
}
