using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CompanyNetCore.Helper
{
    public static class Authentication
    {
        private static string Converting(string encodedString)
        {
            var header = encodedString.Split('.')[1];
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
                var firstName = data.SelectToken("FirstName").ToString();
                var siteId = data.SelectToken("SiteID").ToString();
                if (firstName == "Yannick" && siteId == "60038-22141")
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
