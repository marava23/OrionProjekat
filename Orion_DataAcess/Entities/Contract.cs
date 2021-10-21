using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public class Contract : Entity
    {
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int Duration { get; set; }
        public int? Discount { get; set; } 
        public int? FreeMonths { get; set; }
        public int Status { get; set; }

        public virtual ICollection<ContractPackage> ContractPackages { get; set; } = new HashSet<ContractPackage>();
        public virtual ICollection<ContractEdit> ContractEdits { get; set; } = new HashSet<ContractEdit>();


    }
}
