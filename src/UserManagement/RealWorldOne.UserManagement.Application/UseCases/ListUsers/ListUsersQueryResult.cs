using System.Collections.Generic;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Application.UseCases.ListUsers
{
    public class ListUsersQueryResult : IQueryResult
    {
        public IEnumerable<User> Users { get; }

        public ListUsersQueryResult(IEnumerable<User> users)
        {
            Users = users;
        }
    }
}