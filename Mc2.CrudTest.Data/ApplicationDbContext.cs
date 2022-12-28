using System;
using Mc2.CrudTest.ApplicationServices.Models;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Customer> Customer { get; set; }
    }
}
