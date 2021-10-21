using Microsoft.AspNetCore.Mvc;
using Orion_DataAcess;
using Orion_DataAcess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Orion_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly OrionContext _context;

        public TestController(OrionContext context)
        {
            _context = context;
        }

        // POST api/<TestController>
        [HttpPost]
        public IActionResult Post()
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "NET",
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Name = "IPTV",
                    CreatedAt = DateTime.Now
                },
                new Category
                {
                    Name = "VOICE",
                    CreatedAt = DateTime.Now
                }
            };
            var packages = new List<Package>
            {
                new Package
                {
                    Name = "Internet",
                    Description = "Internet paket brzine do 100 Mbps",
                    Price = 1500,
                    Category = categories.Last(),
                    CreatedAt = DateTime.Now
                },
                new Package
                {
                    Name = "IPTV Sport",
                    Description = "Sportski kanali, Arena, SportKlub, Euro Sport",
                    Price = 450,
                    Category = categories.First(),
                    CreatedAt = DateTime.Now
                }
            };
            var contracts = new List<Contract>
            {
                new Contract
                {
                    Username = "Pera",
                    Duration = 12,
                    Date = new DateTime(2021,8,20),
                    Status = 1,
                    CreatedAt = DateTime.Now
                },
                new Contract
                {
                    Username = "Mika",
                    Duration = 24,
                    Date = new DateTime(2020,6,14),
                    Status = 1,
                    CreatedAt = DateTime.Now
                }
            };
            _context.Categories.AddRange(categories);
            _context.Packages.AddRange(packages);
            _context.Contracts.AddRange(contracts);
            try
            {
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = ex.InnerException});
            }
            

        }
    }
}
