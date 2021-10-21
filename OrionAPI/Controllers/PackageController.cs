using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orion_DataAcess;
using Orion_DataAcess.Entities;
using OrionAPI.DTO;
using OrionAPI.Validators;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private OrionContext _context;
        public PackageController(OrionContext context)
        {
            _context = context;
        }
        // GET: api/<PackageController>
        [HttpGet]
        public IActionResult Get()
        {
            var packages = _context.Packages.Where(x=> x.DeletedAt == null);

            if(packages == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(packages.Select(package => new PackageDTO
                {
                    Id = package.Id,
                    Name = package.Name,
                    Category = package.Category.Name,
                    Description = package.Description,
                    Price = package.Price,
                    CategoryId = package.CategoryId
                }));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var packages = _context.Packages.Where(x=> x.Id == id && x.DeletedAt == null);

            if( packages == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(packages.Select(package => new PackageDTO
                {
                    Id = package.Id,
                    Name = package.Name,
                    Category = package.Category.Name,
                    Description = package.Description,
                    Price = package.Price,
                    CategoryId = package.CategoryId
                }));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Došlo je do greske" });
            }
        }

        // POST api/<PackageController>
        [HttpPost]
        public IActionResult Post([FromBody] PackageDTO dto, [FromServices] CreatePackageValidator validator)
        {
            try
            {
                var result = validator.Validate(dto);
                if (!result.IsValid)
                {
                    return result.Errors.ToUnprocessableEntity();
                }

                var package = new Package
                {
                    Name = dto.Name,
                    CategoryId = dto.CategoryId,
                    CreatedAt = DateTime.Now,
                    Description = dto.Description,
                    Price = dto.Price
                };

                _context.Add(package);
                _context.SaveChanges();
                return StatusCode(201);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Došlo je do greske");
            }
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]PackageDTO dto, [FromServices] UpdatePackageValidator validator)
        {
            try
            {
                var package = _context.Packages.Find(id);

                if (package == null)
                {
                    return NotFound();
                }

                dto.Id = id;

                var result = validator.Validate(dto);
                if (!result.IsValid)
                {
                    return result.Errors.ToUnprocessableEntity();
                }

                package.CategoryId = dto.CategoryId;
                package.Description = dto.Description;
                package.Name = dto.Name;
                package.Price = dto.Price;

                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Došlo je do greske");
            }

        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            try
            {
                var package = _context.Packages.Find(id);

                if (package == null)
                {
                    return NotFound();
                }

                package.DeletedAt = DateTime.Now;
                _context.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Došlo je do greske");
            }

        }
    }
}
