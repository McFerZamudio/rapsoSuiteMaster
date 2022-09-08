using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace rapsoSuiteMaster.Data
{
    public partial class basicNetUserInfo
    {
        [Key]

        public string? id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string userName { get; set; }

        public string jsonResponse()
        {
            return JsonSerializer.Serialize(this);
        }
    }



}
