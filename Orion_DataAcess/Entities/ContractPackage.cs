using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public class ContractPackage
    {
        public int ContractId { get; set; }
        public int PackageId { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Package Package { get; set; }
    }
}
