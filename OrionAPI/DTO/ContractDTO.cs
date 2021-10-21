using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionAPI.DTO
{
    public class ContractDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }
        public int Duration { get; set; }
        public int? Discount { get; set; }
        public int? FreeMonths { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public IEnumerable<int> PackageIds { get; set; } = new List<int>();
    }
    
}
