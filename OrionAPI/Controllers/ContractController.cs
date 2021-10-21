using Microsoft.AspNetCore.Mvc;
using Orion_DataAcess;
using OrionAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrionAPI.Validators;
using Orion_DataAcess.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : ControllerBase
    {
        private OrionContext _context;
        public ContractController(OrionContext context)
        {
            _context = context;
        }
        // GET: api/<ContractController>
        [HttpGet]
        public IActionResult Get([FromQuery]ContractSearchDTO dto)
        {
            try
            {
                var contracts = _context.Contracts
                    .Include(x => x.ContractPackages)
                    .Where(x => x.DeletedAt == null)
                    .AsQueryable();
                if(dto.Key == "active")
                {
                    var activeContracts = contracts.Count(x => x.Status == 1);
                    var inActiveContracts = contracts.Count(x => x.Status == 0);
                    return Ok(new ActiveContractsDTO
                    {
                        ActiveContracts = activeContracts,
                        InactiveContracts = inActiveContracts
                    });
                }
                else if (dto.Id == 5)
                {
                    return Ok(contracts.Where(x=> x.Status==1).OrderByDescending(x=> x.Id).Take(5).Select(contract => new ContractDTO
                    {
                        Id = contract.Id,
                        Date = contract.Date,
                        Duration = contract.Duration,
                        Username = contract.Username,
                    }));
                }
                else if(dto.Key == "profit")
                {
                    List<ContractDTO> contractProfits = new List<ContractDTO>();
                    decimal totalPrice = 0;
                    contracts = contracts.Where(x => x.Status == 1);
                    var activecontracts = contracts.ToList();

                    foreach (var contract in activecontracts)
                    {
                        var packages = _context.Packages.Where(x => contract.ContractPackages.Select(y => y.PackageId).Contains(x.Id)).ToList();
                        var duration = Convert.ToDecimal(contract.Duration);
                        var freeMonths = Convert.ToDecimal(contract.FreeMonths);
                        var discount = Convert.ToDecimal(contract.Discount);
                        foreach (var p in packages)
                        {
                            totalPrice += (p.Price * (duration - freeMonths)) * (100 - discount) / 100;
                        }
                        ContractDTO contractProfit = new ContractDTO {Id = contract.Id, TotalPrice = totalPrice };
                        contractProfits.Add(contractProfit);
                    }
                    return Ok(contractProfits);

                }
                else
                {
                    return Ok(contracts.Select(contract => new ContractDTO
                    {
                        Id = contract.Id,
                        Date = contract.Date,
                        Discount = contract.Discount,
                        Duration = contract.Duration,
                        FreeMonths = contract.FreeMonths,
                        Username = contract.Username,
                        Status = contract.Status,
                        PackageIds = contract.ContractPackages.Select(x => x.PackageId)
                    }));
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // GET api/<ContractController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var contract = _context.Contracts.Where(x => x.Id == id && x.DeletedAt == null);

            if (contract == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(contract.Select(x => new ContractDTO
                {
                    Id = x.Id,
                    Date = x.Date,
                    Discount = x.Discount,
                    Duration = x.Duration,
                    FreeMonths = x.FreeMonths,
                    Username = x.Username,
                    Status = x.Status,
                    PackageIds = x.ContractPackages.Select(x => x.PackageId)
                }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // POST api/<ContractController>
        [HttpPost]
        public IActionResult Post([FromBody] ContractDTO dto, [FromServices] CreateContractValidator validator)
        {
            try
            {
                var result = validator.Validate(dto);
                if (!result.IsValid)
                {
                    return result.Errors.ToUnprocessableEntity();
                }
                var lastId = _context.Contracts.Max(x => x.Id) + 1;
                var contract = new Contract
                {
                    Date = dto.Date,
                    Username = dto.Username,
                    Duration = dto.Duration,
                    Discount = dto.Discount,
                    FreeMonths = dto.FreeMonths,
                    Status = dto.Status,
                    CreatedAt = DateTime.Now
                };

                _context.Contracts.Add(contract);
                var contractPackages = new List<ContractPackage>();
                foreach (var id in dto.PackageIds)
                {
                    contractPackages.Add(new ContractPackage
                    {
                        PackageId = id,
                        ContractId = lastId
                    });
                }
                _context.contractPackages.AddRange(contractPackages);

                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // PUT api/<ContractController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ContractDTO dto, [FromServices]UpdateContractValidator validator)
        {
            try
            {
                var contract = _context.Contracts.Find(id);
                var packages = _context.Packages.Where(x=> dto.PackageIds.Contains(x.Id)).ToList();
                if(contract == null)
                {
                    return NotFound();
                }

                var result = validator.Validate(dto);
                if (!result.IsValid)
                {
                    return result.Errors.ToUnprocessableEntity();
                }
                decimal totalPrice = 0;

                var duration = Convert.ToDecimal(contract.Duration);
                var freeMonths = Convert.ToDecimal(contract.FreeMonths);
                var discount = Convert.ToDecimal(contract.Discount);

                foreach(var p in packages)
                {

                    totalPrice += (p.Price * (duration - freeMonths)) * (100 - discount) /100;
                }


                contract.Id = dto.Id;
                contract.Status = dto.Status;
                contract.Username = dto.Username;
                contract.Date = dto.Date;
                contract.Duration = dto.Duration;
                contract.Discount = dto.Discount;
                contract.FreeMonths = dto.FreeMonths;

                
                var contractEdit = new ContractEdit
                {
                    ContractId = dto.Id,
                    Date = dto.Date,
                    Status = dto.Status,
                    TotalPrice = totalPrice,
                    CreatedAt = DateTime.Now
                };

                _context.ContractEdits.Add(contractEdit);
                _context.SaveChanges();
                return NoContent();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // DELETE api/<ContractController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var contract = _context.Contracts.Find(id);

                if(contract == null)
                {
                    return NotFound();
                }

                contract.DeletedAt = DateTime.Now;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }
    }
}
