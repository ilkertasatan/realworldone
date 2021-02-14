namespace RealWorldOne.UserManagement.Application.Common.Security
{
    public class AuthenticationSettings
    {
        public string Secret { get; set; }
        public string Iss { get; set; }
        public string Aud { get; set; }
    }
}