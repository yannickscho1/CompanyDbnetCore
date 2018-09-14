using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Model
{
    public class Company : dto.CompanyDto
    {
        public int? Id { get; set; }
        public string Country { get; set; }
        public string City{ get; set; }
        public string Street { get; set; }
    }
}
