using Company.ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.ASP.Data.Context
{
    public class CompanyContext : DbContext
    {
        public CompanyContext(DbContextOptions<CompanyContext> opt) : base(opt) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Models.Company> Companies { get; set; }
    }
}