using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaAPI.API.DTOs;
using PracticaAPI.API.Models;

namespace PracticaAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("applications/json")]
    public class CustomersController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public CustomersController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<CustomerDTO> GetCustomers([FromRoute] int pag, [FromRoute] int tam)
        {
            var model = _context.Customers.Skip(tam * pag - 1).Take(tam).OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<CustomerDTO>>(model);
            return dto;
        }

        [HttpGet("")]
        public IEnumerable<CustomerDTO> GetCustomer()
        {
            var model = _context.Customers.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            var dto = Mapper.Map<IEnumerable<CustomerDTO>>(model);
            return dto;
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<CustomerDTO>(customer));
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromBody] int id, [FromBody] CustomerDTO customer)
        {

            if (!ModelState.IsValid)
            {
                return Conflict(ModelState);
            }

            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Customer>(customer)).State = EntityState.Modified;

            try
            {
                var updateResult = await _context.SaveChangesAsync();

                if (updateResult == 0)
                {
                    return Conflict(new { Message = "No se pudo modificar el modelo enviado" });
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return Conflict(new { ex.Message });
                }
            }

            return NoContent();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] CustomerDTO customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var map = Mapper.Map<Customer>(customer);

            _context.Customers.Add(map);
            await _context.SaveChangesAsync();
            customer.Id = map.Id;

            return CreatedAtAction("GetCustomer", new { id = map.Id }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer([FromBody] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok(Mapper.Map<CustomerDTO>(customer));
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] dynamic credentials)
        {
            var username = (string)credentials["username"];
            var password = (string)credentials["password"];

            var employee = await _context.Employees.SingleOrDefaultAsync(m => m.UserName == username && m.Password == password);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EmployeeDTO>(employee));
        }


        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
