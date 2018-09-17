using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Interface
{
    public interface IDbContext
    {
        IDbConnection GetCompany();
    }
}
