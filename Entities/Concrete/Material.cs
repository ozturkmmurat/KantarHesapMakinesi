using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Material : IEntity
    {
        public int Id { get; set; }
        public string MaterialName { get; set; }
        public decimal MaterialEuroPrice { get; set; }
        public decimal MaterialTlPrice { get; set; }
    }
}
