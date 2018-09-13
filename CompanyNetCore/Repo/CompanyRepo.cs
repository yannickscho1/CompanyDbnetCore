using CompanyNetCore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CompanyNetCore.Repo
{
    public class CompanyRepo
    {

        public List<Company> Read()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "SELECT Id,Name,Business,Country,City,Street FROM viCompany;";
                var companyList = conn.Query<Company>(companySelect).ToList();
                return companyList;
            }
        }
        public Company ReadById(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "SELECT Id,Name,Business FROM viCompany  WHERE Id = @Id;";
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var company = conn.QueryFirstOrDefault<Company>(companySelect, param);
                return company;
            }
        }
        public Company AddOrUpdate(string Name, string Business, int Id = -1)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "spCompany";
                var param = new DynamicParameters();
                param.Add("@Name", Name);
                param.Add("@Business", Business);
                param.Add("@Id", Id);
                var company = conn.QueryFirstOrDefault<Company>(companySelect, param, null, null, CommandType.StoredProcedure);
                return company;
            }
        }

        public Company Delete(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "spDeleteCompany";
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var company = conn.QueryFirstOrDefault<Company>(companySelect, param, null, null, CommandType.StoredProcedure);
                return company;
            }
        }
    }
}