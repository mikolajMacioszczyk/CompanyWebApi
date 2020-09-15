using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.ASP.Data.Context;
using Company.ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.ASP.Data.Repository.Employe
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyContext _db;

        public EmployeeRepository(CompanyContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _db.Employees.ToListAsync();
        }

        public IQueryable<Employee> GetQuerable()
        {
            return _db.Employees.AsQueryable();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _db.Employees.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Employee input)
        {
            await _db.AddAsync(input);
        }

        public void Remove(Employee toRemove)
        {
            _db.Employees.Remove(toRemove);
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var toRemove = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (toRemove == null) { return false; }
            Remove(toRemove);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Employee updated)
        {
            var fromDb = await _db.Employees.FirstOrDefaultAsync(e => e.Id == id);
            if (fromDb == null) { return false; }

            fromDb.FirstName = updated.FirstName;
            fromDb.LastName = updated.LastName;
            fromDb.JobTitle = updated.JobTitle;
            fromDb.DateOfBirth = updated.DateOfBirth;
            return true;
        }
    }
}