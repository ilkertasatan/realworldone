using System.Threading;
using System.Threading.Tasks;

namespace RealWorldOne.UserManagement.Domain
{
    public interface IRepository
    {
        Task CommitChangesAsync(CancellationToken cancellationToken = default);
    }
}