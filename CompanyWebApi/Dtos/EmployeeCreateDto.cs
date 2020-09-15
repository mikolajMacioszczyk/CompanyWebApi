using System;
using System.ComponentModel.DataAnnotations;
using Company.ASP.Models;

namespace Company.ASP.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public JobTitles JobTitle { get; set; }
    }
}