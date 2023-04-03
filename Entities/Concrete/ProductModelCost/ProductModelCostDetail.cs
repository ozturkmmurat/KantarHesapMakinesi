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
        public int ModelId { get; set; }
        public int AccessoryId { get; set; }
        public int InstallationCostLocationId { get; set; }
        public decimal AccessoryTlPrice { get; set; }
        public decimal AccessoryEuroPrice { get; set; }
        public decimal InstallationIncludedTl { get; set; }
        public decimal InstallationIncludedEuro { get; set; }
        public decimal InstallationTlPrice { get; set; }
        public decimal SalesPriceTl { get; set; }
        public decimal SalesPriceEuro { get; set; }
        public decimal TurkeySalesPrice { get; set; }
        public decimal ExportSalesPrice { get; set; }
        public decimal ProfitPriceTl { get; set; }
        public decimal ProfitPriceEuro{ get; set; }
        public decimal AdditionalProfitPercentage { get; set; }
        public decimal OfferPriceTl { get; set; }
        public decimal OfferPriceEuro { get; set; }
        public decimal TurkeySalesDiscountPrice { get; set; }
        public decimal ExportFinalDiscountPrice { get; set; }
    }
}
