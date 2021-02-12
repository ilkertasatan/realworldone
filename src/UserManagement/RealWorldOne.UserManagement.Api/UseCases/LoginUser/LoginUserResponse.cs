using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.UseCases.LoginUser
{
    public sealed class LoginUserResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }
        
        [JsonProperty(PropertyName = "expires_in")]
        public int ExpiresIn { get; set; }
    }
}