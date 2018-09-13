using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Business { get; set; }
        public string Country { get; set; }
        public string City{ get; set; }
        public string Street { get; set; }
    }
}
