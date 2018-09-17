using CompanyNetCore.Model;
using CompanyNetCore.Model.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Interface
{
    public interface CompanyRepository
    {
        List<Company> Read();
        Company ReadById(int id);
        Company Create(CompanyDto company);
        Company Update(CompanyDto company, int id);
        Company Delete(int id);
    }
}
