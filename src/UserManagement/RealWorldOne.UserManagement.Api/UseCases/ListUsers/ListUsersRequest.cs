using System.ComponentModel;

namespace RealWorldOne.UserManagement.Api.UseCases.ListUsers
{
    public sealed class ListUsersRequest
    {
        public int Offset { get; set; }
        
        [DefaultValue(10)]
        public int Limit { get; set; } = 10;
    }
}