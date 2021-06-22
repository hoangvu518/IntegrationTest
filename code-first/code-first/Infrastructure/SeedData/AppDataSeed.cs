using code_first.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace code_first.Infrastructure.SeedData
{
    public class AppDataSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext, ILoggerFactory loggerFactory)
        {

                if (!await dbContext.Employees.AnyAsync())
                {
                    await dbContext.Employees.AddRangeAsync(
                        GetPreconfiguredEmployees());

                    await dbContext.SaveChangesAsync();
                }

        }

        static IEnumerable<Employee> GetPreconfiguredEmployees()
        {
            return new List<Employee>()
            {
                new Employee("Pham", "Hoang"),
                new Employee("Tan", "Hue")
            };
        }
    }
}
