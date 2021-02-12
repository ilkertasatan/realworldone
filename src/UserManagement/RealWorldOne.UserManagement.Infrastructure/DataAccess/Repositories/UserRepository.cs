using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Infrastructure.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManagementDataContext _dataContext;

        public UserRepository(UserManagementDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<User> SaveAsync(User user, CancellationToken cancellationToken = default)
        {
            return (await _dataContext.AddAsync(user, cancellationToken)).Entity;
        }

        public async Task<User> SelectByIdAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users
                .SingleOrDefaultAsync(user => user.Id == userId, cancellationToken) ?? User.None;
        }

        public async Task<User> SelectByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users
                .SingleOrDefaultAsync(user => user.Email == email, cancellationToken) ?? User.None;
        }

        public async Task<IEnumerable<User>> SelectAllAsync(int offset, int limit, CancellationToken cancellationToken = default)
        {
            return await _dataContext.Users.Skip(offset * limit).Take(limit).ToListAsync(cancellationToken);
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dataContext.SaveChangesAsync(cancellationToken);
        }
    }
}