using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //Maliyet hesaplaması yaparken kullanılıyor Veritabanında bu modele karşılık gelen bir tablo yok.
    public class CurrencyCheck
    {
        public string CurrencyName { get; set; }
        public decimal Currency { get; set; }
    }
}
