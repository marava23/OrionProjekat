﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public class Package : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<ContractPackage> ContractPackages { get; set; } = new HashSet<ContractPackage>();

    }
}
