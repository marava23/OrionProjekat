using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public abstract class Entity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
