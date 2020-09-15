using System.Collections.Generic;

namespace Company.ASP.Dtos
{
    public class CompanyReadDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int EstablishmentYear { get; set; }
        public IEnumerable<EmployeeReadDto> Employees { get; set; }
    }
}