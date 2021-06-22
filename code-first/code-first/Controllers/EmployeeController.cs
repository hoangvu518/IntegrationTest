using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using code_first.Infrastructure;
using code_first.Models;
using code_first.Models.dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace code_first.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly ILogger<EmployeeController> _logger;

        private readonly AppDbContext _dbContext;

        public EmployeeController(ILogger<EmployeeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //return Ok(Enumerable.Range(0, 10));
            return Ok(await _dbContext.Employees.FindAsync(id));
        }

        [HttpGet("All")]
        public async Task<IActionResult> GetAll()
        {
            //return Ok(Enumerable.Range(0, 10));
            return Ok(await _dbContext.Employees.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post(EmployeePostDto employeeDto)
        {
            var employee = new Employee(employeeDto.LastName, employeeDto.FirstName);

            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
            return Created(nameof(Get), employee);
        }
    }
}
