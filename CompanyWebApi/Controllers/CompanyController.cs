using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Company.ASP.Data.Repository.Company;
using Company.ASP.Data.Repository.Employe;
using Company.ASP.Dtos;
using Company.ASP.Models;
using Company.ASP.QueryBuilder;
using Microsoft.AspNetCore.Mvc;

namespace Company.ASP.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly CompanyQueryBuilder _queryBuilder;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository companyRepository, IMapper mapper, IEmployeeRepository employeeRepository, CompanyQueryBuilder queryBuilder)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _queryBuilder = queryBuilder;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyReadDto>>> Index()
        {
            var companyItems = await _companyRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CompanyReadDto>>(companyItems));
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<CompanyReadDto>> GetById(int id)
        {
            var companyItem = await _companyRepository.GetByIdAsync(id);
            return companyItem == null ? Problem() : Ok(_mapper.Map<CompanyReadDto>(companyItem));
        }

        [HttpPost("create")]
        public async Task<ActionResult<CompanyReadDto>> CreateAsync(CompanyCreateDto companyCreateDto)
        {
            if (!companyCreateDto.EstablishmentYear.HasValue)
                return ValidationProblem("THe field EstablishmentYear cannot be empty");
            var companyModel = _mapper.Map<Models.Company>(companyCreateDto);
            
            await _companyRepository.CreateAsync(companyModel);
            foreach (var modelEmployee in companyModel.Employees)
                await _employeeRepository.CreateAsync(modelEmployee);
            
            await _companyRepository.SaveChangesAsync();

            return Ok(new {id = companyModel.Id});
        }

        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<CompanyReadDto>>> SearchAsync(CompanySearchDto companySearchDto)
        {
            var query = _queryBuilder.GetAll();
            
            query = !string.IsNullOrEmpty(companySearchDto.Keyword)
                ? query.NameLike(companySearchDto.Keyword)
                : query;
            
            query = companySearchDto.EmployeeJobTitles != null && companySearchDto.EmployeeJobTitles.Any()
                ? query.WithEmployeeAtJobs(companySearchDto.EmployeeJobTitles)
                : query;
            
            query = companySearchDto.EmployeeDateOfBirthFrom.HasValue
                ? query.WithEmployeeBornAfter(companySearchDto.EmployeeDateOfBirthFrom.Value)
                : query;
            
            query = companySearchDto.EmployeeDateOfBirthTo.HasValue
                ? query.WithEmployeeBornBefore(companySearchDto.EmployeeDateOfBirthTo.Value)
                : query;
            
            return Ok(await query.ToListAsync());
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, CompanyCreateDto companyCreateDto)
        {
            var companyModel = _mapper.Map<Models.Company>(companyCreateDto);

            if (await _companyRepository.UpdateAsync(id, companyModel))
            {
                await _companyRepository.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            if (await _companyRepository.RemoveByIdAsync(id))
            {
                await _companyRepository.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}