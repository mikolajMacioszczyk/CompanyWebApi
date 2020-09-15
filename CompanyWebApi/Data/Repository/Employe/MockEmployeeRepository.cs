using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.ASP.Models;

namespace Company.ASP.Data.Repository.Employe
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private IEnumerable<Employee> Employees = new[]
        {
            new Employee()
            {
                Id = 1, FirstName = "FirstName 1", LastName = "LastName 1", JobTitle = JobTitles.Administrator,
                DateOfBirth = DateTime.Now
            },
            new Employee()
            {
                Id = 2, FirstName = "FirstName 2", LastName = "LastName 2", JobTitle = JobTitles.Architect,
                DateOfBirth = DateTime.Now
            },
            new Employee()
            {
                Id = 3, FirstName = "FirstName 3", LastName = "LastName 3", JobTitle = JobTitles.Developer,
                DateOfBirth = DateTime.Now
            },
            new Employee()
            {
                Id = 4, FirstName = "FirstName 4", LastName = "LastName 4", JobTitle = JobTitles.Manager,
                DateOfBirth = DateTime.Now
            },
        };

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return new List<Employee>(Employees);
        }

        public IQueryable<Employee> GetQuerable()
        {
            throw new NotImplementedException();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return Employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task CreateAsync(Employee input)
        {
            var list = new List<Employee>(Employees);
            list.Add(input);
            Employees = list;
        }

        public void Remove(Employee toRemove)
        {
            var list = new List<Employee>(Employees);
            list.Remove(toRemove);
            Employees = list;
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var toRemove = Employees.FirstOrDefault(e => e.Id == id);
            if (toRemove == null){return false;}
            Remove(toRemove);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Employee updated)
        {
            var fromList = Employees.FirstOrDefault(e => e.Id == id);
            if (fromList == null){return false;}

            fromList.FirstName = updated.FirstName;
            fromList.LastName = updated.LastName;
            fromList.JobTitle = updated.JobTitle;
            fromList.DateOfBirth = updated.DateOfBirth;
            return true;
        }
    }
}