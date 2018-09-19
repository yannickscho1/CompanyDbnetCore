using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompanyNetCore.Helper
{
    public static class Authentication
    {
        private static string Converting(string encodedString)
        {
            var header = encodedString.Split('.')[1];
            if (header.Length % 4 == 3)
                header += "=";
            if (header.Length % 4 == 2)
                header += "==";
            byte[] data = Convert.FromBase64String(header);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
        public static bool Authenticat(string Auth)
        {
            try
            {
                // My Bearer Token 
                // Header       eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsInZlciI6MSwia2lkIjoibWRjeG1kYmQifQ.
                // Payload      eyJqdGkiOiI5Y2QyZTM0Ny1kMDBiLTRmYWItYTI1YS1hNTQ4ZmQxNjhiMmYiLCJzdWIiOiIxMzAtOTMzNDciLCJ0eXBlIjoxLCJleHAiOiIyMDE4LTA5LTIyVDA2OjM4OjI2WiIsImlhdCI6IjIwMTgtMDktMThUMDY6Mzg6MjZaIiwiTG9jYXRpb25JRCI6MTU4NzQ2LCJTaXRlSUQiOiI3Nzg5MC0yOTU2NyIsIklzQWRtaW4iOnRydWUsIlRvYml0VXNlcklEIjoxOTg4NTgwLCJQZXJzb25JRCI6IjEzMC05MzM0NyIsIkZpcnN0TmFtZSI6Illhbm5pY2siLCJMYXN0TmFtZSI6IlNjaG8ifQ.
                // Signature    E-L0hzBptyXvVY5ihiUO3YNQlDZ1xAdvOETgoiM_jlZ8FR6jlKlmwmNow2HCuj-ZkRjcdpSLAQibr6-ZP8wfSPv1bqehzbJqYfqjhkzXFpvUcutsiWKoCXKHMJs_sPkQAbJn27eIIPS9CJYJq2Z3bXqezyvAvsH7DZ9pvlYKkeX49txN3BSBq1RtXZkEceFY2eNV0tiJK2u584pSaqltp0ZJhY95Z8QGg9S7qxQQVPTCONhZkeBqBJYuRmzdb4fc9y3K0oimcwMlkgumX88viLmx4s3MO24T4CzcVyICQlpuuIRBLR33KCerJjFfCWLRHF1YENhHImpQWtp2qymdzg
                var authString = Converting(Auth);
                var data = (JObject)JsonConvert.DeserializeObject(authString);
                var personId = data.SelectToken("PersonID").ToString();
                var name = data.SelectToken("LastName").ToString();
                if (personId == "130-93347" && name == "Scho")
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
