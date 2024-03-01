using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    //Urun ve urunun kar yuzdesini db eklemek icin kullaniliyor.
    public class CRUDProductDto : IDto
    {
        public int ProductId { get; set; }
        public int ProductProfitId { get; set; }
        public string ProductName { get; set; }
        public decimal ProfitPercentage { get; set; }
        public decimal AdditionalProfitPercentage { get; set; }
    }
}
