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
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly StoreDBContext _context;

        public EmployeesController(StoreDBContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public IEnumerable<EmployeeDTO> GetEmployees()
        {
            var model = _context.Employees;
            var dto = Mapper.Map<IEnumerable<EmployeeDTO>>(model);
            return dto;
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _context.Employees.Find(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<EmployeeDTO>(employee));
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public IActionResult PutEmployee(int id, EmployeeDTO employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(Mapper.Map<Employee>(employee)).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        [HttpPost]
        public async Task<IActionResult> PostEmployee(EmployeeDTO employee)
        {

            var map = Mapper.Map<Employee>(employee);

            _context.Employees.Add(map);
            await _context.SaveChangesAsync();
            employee.Id = map.Id;

            return CreatedAtAction("GetEmployee", new { id = map.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {

            var employee = _context.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            _context.SaveChangesAsync();

            return Ok(Mapper.Map<EmployeeDTO>(employee));
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

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
