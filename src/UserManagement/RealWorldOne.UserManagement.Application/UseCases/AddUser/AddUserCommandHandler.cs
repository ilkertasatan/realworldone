using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Application.Extensions;
using RealWorldOne.UserManagement.Domain;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;
using RealWorldOne.UserManagement.Infrastructure.Security;

namespace RealWorldOne.UserManagement.Application.UseCases.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ICommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public AddUserCommandHandler(
            IUserRepository userRepository,
            IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public async Task<ICommandResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var registeredUser = await _userRepository.SelectByEmailAsync(request.Email, cancellationToken);
            if (registeredUser.Exists())
                return new AddUserCommandResult(registeredUser);

            var salt = Salt.Create();
            var hash = Hash.Create(request.Password.Value, salt);
            var userEntity = _userFactory.NewUser(request.Name, request.Email, new Password(hash), new PasswordSalt(salt));
            var user = await _userRepository.SaveAsync(userEntity, cancellationToken);

            await _userRepository.CommitChangesAsync(cancellationToken);
            
            return new AddUserCommandResult(user);
        }
    }
}