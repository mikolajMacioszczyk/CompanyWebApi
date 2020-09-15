using System;
using Company.ASP.Models;

namespace Company.ASP.Dtos
{
    public class EmployeeReadDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public JobTitles JobTitle { get; set; }
    }
}