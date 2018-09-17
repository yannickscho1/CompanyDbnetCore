using CompanyNetCore.Interface;
using CompanyNetCore.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyNetCore.Helper
{
    class DbContext : IDbContext
    {
        private readonly DbSettings _settings;
        public DbContext(IOptions<DbSettings> options)
        {
            _settings = options.Value;
        }
        public IDbConnection GetCompany()
        {
            var con = new SqlConnection(_settings.Company);
            con.Open();
            return con;
        }
    }
}
