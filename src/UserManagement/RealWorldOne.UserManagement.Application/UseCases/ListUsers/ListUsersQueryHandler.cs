using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Application.UseCases.ListUsers
{
    public class ListUsersQueryHandler :IRequestHandler<ListUsersQuery, IQueryResult>
    {
        private readonly IUserRepository _userRepository;

        public ListUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        
        public async Task<IQueryResult> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.SelectAllAsync(request.Offset, request.Limit, cancellationToken);
            return new ListUsersQueryResult(users);
        }
    }
}