using System.Text.Json;

namespace rapsoSuiteMaster.Data
{
    public class userRapsoSuites
    {
        public string email { get; private set; }
        public string password { get; private set; }
        public userRapsoSuites(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public string jsonResponse()
        {
            return JsonSerializer.Serialize<userRapsoSuites>(this);
        }

    }
}
