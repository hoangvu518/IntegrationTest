using code_first.Infrastructure;
using code_first.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ms_test
{
    public static class MockDbContext
    {
        public static AppDbContext GetAppDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<AppDbContext>()
                            .UseInMemoryDatabase(dbName)
                            .Options;

                // Create instance of DbContext
                var dbContext = new AppDbContext(options);

                // Add entities in memory
                dbContext.Seed();

                return dbContext;
        }

        public static void Seed(this AppDbContext dbContext)
        {
            var employees = new List<Employee>() {
                new Employee("Pham", "Hoang"),
                new Employee("Tan", "Hue"),
                new Employee("La", "Tam")
            };
            dbContext.Employees.AddRange(employees);
            dbContext.SaveChanges();
        }
    }
}
