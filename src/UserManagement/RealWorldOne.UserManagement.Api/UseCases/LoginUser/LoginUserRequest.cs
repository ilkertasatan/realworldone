using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.UseCases.LoginUser
{
    public sealed class LoginUserRequest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}