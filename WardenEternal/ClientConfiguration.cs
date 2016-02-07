using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WardenEternal
{
    static class ClientConfiguration
    {
        public static string Email { get; }
        public static string Password { get; }

        static ClientConfiguration()
        {
            JObject config = JObject.Parse(File.ReadAllText("config.json"));

            Email = config.SelectToken("login.email").ToString();
            Password = config.SelectToken("login.password").ToString();
        }
    }
}
