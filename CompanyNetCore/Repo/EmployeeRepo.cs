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
    public class EmployeeRepo
    {
        public List<Employee> Get()
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string employeeSelect = "SELECT Id,Vorname,Name,Salary,Gender FROM Employee;";
                var employeeList = conn.Query<Employee>(employeeSelect).ToList();
                return employeeList;
            }
        }
        public Employee GetById(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "SELECT Id,Vorname,Name,Salary,Gender FROM Employee WHERE Id = @Id;";
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var employee = conn.QueryFirstOrDefault<Employee>(companySelect, param);
                return employee;
            }
        }
        public Employee AddOrUpdate(string Vorname, string Name, decimal Salary, int Gender, int Id = -1)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "spEmployee";
                var param = new DynamicParameters();
                param.Add("@Vorname", Vorname);
                param.Add("@Name", Name);
                param.Add("@Salary", Salary);
                param.Add("@Gender", Gender);
                param.Add("@Id", Id);
                var employee = conn.QueryFirstOrDefault<Employee>(companySelect, param, null, null, CommandType.StoredProcedure);
                return employee;
            }
        }

        public Employee Delete(int Id)
        {
            using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
            {
                conn.Open();
                string companySelect = "spDeleteEmployee";
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var employee = conn.QueryFirstOrDefault<Employee>(companySelect, param, null, null, CommandType.StoredProcedure);
                return employee;
            }
        }
    }
}
