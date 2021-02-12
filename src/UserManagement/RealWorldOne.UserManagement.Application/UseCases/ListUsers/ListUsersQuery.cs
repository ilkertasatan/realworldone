using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;

namespace RealWorldOne.UserManagement.Application.UseCases.ListUsers
{
    public sealed class ListUsersQuery : IRequest<IQueryResult>
    {
        public ListUsersQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }
        
        public int Offset { get; }
        public int Limit { get; }
    }
}