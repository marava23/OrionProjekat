using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Orion_DataAcess;
using OrionAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractEditController : ControllerBase
    {
        private OrionContext _context;
        public ContractEditController(OrionContext context)
        {
            _context = context;
        }

        // GET api/<ContractEditController>/5
        [HttpGet("{id}")]
        [EnableCors("_myAllowSpecificOrigins")]
        public IActionResult Get(int id)
        {
            try
            {
                var edits= _context.ContractEdits.Where(x => x.ContractId == id);
                return Ok(edits.Select(x=> new ContractEditsDTO
                {
                    ContractId = x.Id,
                    Date = x.Date,
                    Status = x.Status,
                    TotalPrice = x.TotalPrice
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }

        }

        public string Post()
        {
            return "1,2,3";
        }
    }
}
