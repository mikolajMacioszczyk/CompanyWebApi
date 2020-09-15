using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.ASP.Data.Repository.Company
{
    public class MockCompanyRepository : ICompanyRepository
    {
        private IEnumerable<Models.Company> Companies = new[]
        {
            new Models.Company() {Id = 1, Name = "Company1", EstablishmentYear = 2001},
            new Models.Company() {Id = 2, Name = "Company2", EstablishmentYear = 2002},
            new Models.Company() {Id = 3, Name = "Company3", EstablishmentYear = 2003},
            new Models.Company() {Id = 4, Name = "Company4", EstablishmentYear = 2004},
        };

        public async Task<bool> SaveChangesAsync()
        {
            return true;
        }

        public async Task<IEnumerable<Models.Company>> GetAllAsync()
        {
            return new List<Models.Company>(Companies);
        }

        public IQueryable<Models.Company> GetQuerable()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Models.Company> GetByIdAsync(int id)
        {
            return Companies.FirstOrDefault(c => c.Id == id);
        }

        public async Task CreateAsync(Models.Company input)
        {
            var list = new List<Models.Company>(Companies);
            list.Add(input);
            Companies = list;
        }

        public void Remove(Models.Company toRemove)
        {
            var list = new List<Models.Company>(Companies);
            list.Remove(toRemove);
            Companies = list;
        }

        public async Task<bool> RemoveByIdAsync(int id)
        {
            var toRemove = Companies.FirstOrDefault(c => c.Id == id);
            if (toRemove == null){return false;}
            Remove(toRemove);
            return true;
        }

        public async Task<bool> UpdateAsync(int id, Models.Company updated)
        {
            var fromDb = Companies.FirstOrDefault(c => c.Id == id);
            if (fromDb == null) {return false;}

            fromDb.Name = updated.Name;
            fromDb.EstablishmentYear = updated.EstablishmentYear;
            return true;
        }
    }
}