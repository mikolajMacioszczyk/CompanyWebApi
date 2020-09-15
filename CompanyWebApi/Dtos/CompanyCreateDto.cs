using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Company.ASP.Dtos
{
    public class CompanyCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int? EstablishmentYear { get; set; }
        [Required]
        public IEnumerable<EmployeeCreateDto> Employees { get; set; }
    }
}