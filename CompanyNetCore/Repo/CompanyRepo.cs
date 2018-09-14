using CompanyNetCore.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using CompanyNetCore.Helper;

namespace CompanyNetCore.Repo
{
    public class CompanyRepo
    {
        static CompanyRepo _companyRepo;
        
        public static CompanyRepo GetInstance()
        {
            if (_companyRepo == null)
                _companyRepo = new CompanyRepo();

            return _companyRepo;
        }

        private CompanyRepo()
        {

        }

        public List<Company> Read()
        {
            List<Company> retVal;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
                {
                    conn.Open();
                    string companySelect = "SELECT Id,Name,Business,Country,City,Street FROM viCompany;";
                    retVal = conn.Query<Company>(companySelect).ToList();
                }
            }
            catch (Exception)
            {
                //Logging in Kibana
                throw new Helper.RepoException<ReadResultType>(ReadResultType.SQLERROR);
            }
            if (retVal == null)
                throw new RepoException<ReadResultType>(ReadResultType.SQLERROR);
            return retVal;
        }

        public Company ReadById(int id)
        {
            Company retVal;
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
                {
                    conn.Open();
                    string companySelect = "SELECT Id,Name,Business,Country,City,Street FROM viCompany  WHERE id = @Id;";
                    var param = new DynamicParameters();
                    param.Add("@Id", id);
                    retVal= conn.QueryFirstOrDefault<Company>(companySelect, param);
                }
            }
            catch (Exception)
            {
                //Logging in Kibana
                throw new RepoException<ReadResultType>(ReadResultType.SQLERROR);
            }
            //if (retVal == null)
            //    throw new RepoException<ReadResultType>(ReadResultType.NOTFOUND);
            return retVal;
        }

        public Company Create(Model.dto.CompanyDto company)
        {
            return AddOrUpdate(company);
        }
        public Company Update(Model.dto.CompanyDto company,int id)
        {
            if (ReadById(id) == null)
            {
                throw new RepoException<UpdateResultType>(UpdateResultType.INVALIDEARGUMENT);
            }
            return AddOrUpdate(company,id);
        }
        private Company AddOrUpdate(Model.dto.CompanyDto company, int id = -1)
        {
            Company retVal;
            if (id < -1)
                throw new RepoException<UpdateResultType>(UpdateResultType.INVALIDEARGUMENT);
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
                {
                    conn.Open();
                    string companySelect = "spCompany";
                    var param = new DynamicParameters();
                    param.Add("@Name", company.Name);
                    param.Add("@Business", company.Business);
                    param.Add("@Id", id);
                    retVal = conn.QueryFirstOrDefault<Company>(companySelect, param, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                //Logging in Kibana
                throw new RepoException<UpdateResultType>(UpdateResultType.SQLERROR);
            }
            if(retVal == null)
                throw new RepoException<UpdateResultType>(UpdateResultType.INVALIDEARGUMENT);
            return retVal;
        }

        public Company Delete(int Id)
        {
            Company retVal;
            if (Id < -1)
                throw new RepoException<DeleteResultType>(DeleteResultType.INVALIDEARGUMENT);
           
            try
            {
                using (SqlConnection conn = new SqlConnection(Properties.Resources.conString))
                {
                    conn.Open();
                    string companySelect = "spDeleteCompany";
                    var param = new DynamicParameters();
                    param.Add("@Id", Id);
                    retVal = conn.QueryFirstOrDefault<Company>(companySelect, param, null, null, CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                //Logging in Kibana
                throw new Helper.RepoException<DeleteResultType>(DeleteResultType.SQLERROR);
            }
            if (retVal == null)
                throw new Helper.RepoException<DeleteResultType>(DeleteResultType.NOTFOUND);
            return retVal;
        }
    }
}