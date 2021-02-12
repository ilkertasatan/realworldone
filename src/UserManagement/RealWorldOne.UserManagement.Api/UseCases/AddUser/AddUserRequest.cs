using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    public sealed class AddUserRequest
    {
        [JsonProperty(PropertyName = "user_name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "user_email")]
        public string Email { get; set; }
        
        [JsonProperty(PropertyName = "user_password")]
        public string Password { get; set; }
    }
}