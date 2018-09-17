using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyNetCore.Helper
{
    public static class Authentication
    {
        private static string username = "hi";
        private static string password = "ho";
        private static string Converting(string encodedString)
        {
            byte[] data = Convert.FromBase64String(encodedString);
            string decodedString = Encoding.UTF8.GetString(data);
            return decodedString;
        }
        public static bool Authenticat(string Auth)
        {
            var authString = Converting(Auth);
            var user = authString.Split(':')[0];
            var pw = authString.Split(':')[1];
            if (user == username && pw == password)
            {
                return true;
            }
            else return false;
        }
    }
}
