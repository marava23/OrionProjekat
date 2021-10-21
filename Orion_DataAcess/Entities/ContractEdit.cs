using System;
using System.Collections.Generic;
using System.Text;

namespace Orion_DataAcess.Entities
{
    public class ContractEdit : Entity
    {
        public int ContractId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual Contract Contract { get; set; }
    }
}
