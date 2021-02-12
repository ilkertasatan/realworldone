using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Domain;
using RealWorldOne.UserManagement.Domain.Users;

namespace RealWorldOne.UserManagement.Application.UseCases.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ICommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserFactory _userFactory;

        public AddUserCommandHandler(IUserRepository userRepository, IUserFactory userFactory)
        {
            _userRepository = userRepository;
            _userFactory = userFactory;
        }

        public async Task<ICommandResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var registeredUser = await _userRepository.SelectByEmailAsync(request.Email, cancellationToken);
            if (registeredUser.Exists())
                return new AddUserCommandResult(registeredUser);

            var userEntity = _userFactory.NewUser(request.Name, request.Email, request.Password);
            var user = await _userRepository.SaveAsync(userEntity, cancellationToken);

            await _userRepository.CommitChangesAsync(cancellationToken);
            
            return new AddUserCommandResult(user);
        }
    }
}