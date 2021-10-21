using System;

namespace OrionAPI.DTO
{
    public class ContractEditsDTO
    {
        public int ContractId { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
