using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Domain.Users
{
    public interface IUserRepository : IRepository
    {
        Task<User> SaveAsync(User user, CancellationToken cancellationToken = default);
        Task<User> SelectByIdAsync(UserId userId, CancellationToken cancellationToken = default);
        Task<User> SelectByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> SelectAllAsync(int offset, int limit, CancellationToken cancellationToken = default);
    }
}