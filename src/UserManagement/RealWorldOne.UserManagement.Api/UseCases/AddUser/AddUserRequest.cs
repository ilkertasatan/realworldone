using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    public sealed class AddUserRequest
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}