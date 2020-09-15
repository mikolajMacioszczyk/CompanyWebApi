using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.ASP.Data.Context;
using Company.ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.ASP.Data.Repository.Company
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanyContext _db;

        public CompanyRepository(CompanyContext db)
        {
            _db = db;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Models.Company>> GetAllAsync()
        {
            return await _db.Companies.Include(c => c.Employees).ToListAsync();
        }

        public IQueryable<Models.Company> GetQuerable()
        {
            return _db.Companies.Include(c => c.Employees).AsQueryable();
        }

        public async Task<Models.Company> GetByIdAsync(int id)
        {
            return await _db.Companies.Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task CreateAsync(Models.Company input)
        {
            await _db.AddAsync(input);
        }

        public void Remove(Models.Company toRemove)
        {
            if (toRemove.Employees!= null)
            {
                foreach (var employee in toRemove.Employees)
                {
                    _db.Employees.Remove(employee);
                }   
            }
            _db.Companies.Remove(toRemove);
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var toRemove = await _db.Companies.Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == id);
            if (toRemove == null){return false;}
            Remove(toRemove);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Company updated)
        {
            var fromDb = await _db.Companies.Include(c => c.Employees).FirstOrDefaultAsync(c => c.Id == id);
            if (fromDb == null){return false;}

            if (fromDb.Employees != null)
            {
                foreach (var employee in fromDb.Employees)
                {
                    _db.Remove(employee);
                }
            }
            foreach (var employee in updated.Employees)
            {
                await _db.AddAsync(employee);
            }
            fromDb.Employees = updated.Employees;

            fromDb.Name = updated.Name;
            fromDb.EstablishmentYear = updated.EstablishmentYear;
            return true;
        }
    }
}