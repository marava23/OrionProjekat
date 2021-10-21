using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Package> Packages { get; set; }
    }
}
