﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class SizeContent : IEntity
    {
        public int Id { get; set; }
        public int ElectronicId { get; set; }
        public int SizeId { get; set; }
        public int ElectronicPcs { get; set; }
    }
}
