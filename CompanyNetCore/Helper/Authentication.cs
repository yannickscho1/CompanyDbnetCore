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
