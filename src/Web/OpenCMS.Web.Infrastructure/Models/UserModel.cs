using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace OpenCMS.Web.Infrastructure.Models
{
    public class UserModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("userName")]
        [Required(ErrorMessage = "UserName is Required")]
        public string UserName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("firstName")]
        [Required(ErrorMessage ="FirstName is Required")]
        public string FirstName { get; set; }

        [JsonPropertyName("middleName")]
        public string MiddleName { get; set; }

        [JsonPropertyName("lastName")]
        [Required(ErrorMessage = "lastName is Required")]
        public string LastName { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
