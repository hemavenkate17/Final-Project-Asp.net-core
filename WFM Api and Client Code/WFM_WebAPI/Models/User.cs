using System.Text.Json.Serialization;

namespace WFM_WebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        public string username { get; set; }

        [JsonIgnore]
        public string password { get; set; }

        public string name { get; set; }

        public string role { get; set; }
        public string email { get; set; }
    }
}
