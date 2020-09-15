using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Company.ASP.Data.Repository.Company;
using Company.ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.ASP.QueryBuilder
{
    public class CompanyQueryBuilder
    {
        private readonly ICompanyRepository _companyRepository;
        private IQueryable<Models.Company> _companies;

        public CompanyQueryBuilder(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public CompanyQueryBuilder GetAll()
        {
            _companies = _companyRepository.GetQuerable();
            return this;
        }

        public CompanyQueryBuilder NameLike(string pattern)
        {
            _companies = from company in _companies
                where EF.Functions.Like(company.Name, pattern)
                select company;
            return this;
        }

        public CompanyQueryBuilder WithEmployeeAtJobs(string[] jobTitles)
        {
            var asEnum = jobTitles
                .Select(jt => (JobTitles) Enum.Parse(typeof(JobTitles), jt)).ToList();
            _companies = _companies.Where(c =>
                c.Employees.Any(e => asEnum.Contains(e.JobTitle)));
            return this;
        }

        public CompanyQueryBuilder WithEmployeeBornAfter(DateTime date)
        {
            _companies = _companies.Where(c =>
                c.Employees.Any(e => e.DateOfBirth >= date));
            return this;
        }
        
        public CompanyQueryBuilder WithEmployeeBornBefore(DateTime date)
        {
            _companies = _companies.Where(c =>
                c.Employees.Any(e => e.DateOfBirth <= date));
            return this;
        }
        
        public async Task<IEnumerable<Models.Company>> ToListAsync()
        {
            var result = await _companies.AsQueryable().ToListAsync();
            _companies = null;
            return result;
        }
    }
}