﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class InstallationCost :  IEntity
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public decimal InstallationTlPrice { get; set; }
        public decimal InstallationEuroPrice { get; set; }
    }
}
