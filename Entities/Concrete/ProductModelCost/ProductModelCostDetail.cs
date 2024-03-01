using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class ProductModelCostDetail : IEntity
    {
        /// <summary>
        /// Bu entity Urun maliyetinin Şehirden şehire değişen fiyatını göstermek için kullanılıyor.
        /// Kurulum bedeli bölgeden bölgeye değişebiliyor. Bir model bir den fazla şehirde kurulabileceği için bu entity var
        /// Mssql de bu Entitynin karşılığı yok.
        /// </summary>
        /// 
        public int ModelId { get; set; }
        public int AccessoryId { get; set; }
        public int InstallationCostLocationId { get; set; }
        public string CurrencyName { get; set; }
        public decimal AccessoryPrice { get; set; }
        public decimal InstallationIncluded { get; set; }
        public decimal InstallationPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal ProfitPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal FinalDiscountPrice { get; set; }
        public bool ExportState { get; set; }
    }
}
