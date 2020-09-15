using System;
using Company.ASP.Models;

namespace Company.ASP.Dtos
{
    public class CompanySearchDto
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public string[] EmployeeJobTitles { get; set; }
    }
}