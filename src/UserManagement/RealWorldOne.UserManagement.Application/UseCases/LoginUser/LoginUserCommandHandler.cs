using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using RealWorldOne.UserManagement.Application.Common.Interfaces;
using RealWorldOne.UserManagement.Application.Extensions;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Infrastructure.Security;

namespace RealWorldOne.UserManagement.Application.UseCases.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ICommandResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenerateAccessToken _tokenGenerator;
        private readonly AuthenticationSettings _audience;
        
        public LoginUserCommandHandler(
            IUserRepository userRepository,
            IOptions<AuthenticationSettings> settings, 
            IGenerateAccessToken tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
            _audience = settings.Value;
        }
        
        public async Task<ICommandResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository
                .SelectByEmailAsync(request.Email, cancellationToken);
            
            if (!user.Exists())
                return new UserNotFoundResult(user.Email.Value);
            
            var passwordVerified = Hash.Verify(request.Password.Value, user.PasswordSalt.Value, user.Password.Value);
            if (!passwordVerified)
                return new PasswordInCorrectResult();
            
            
            var accessToken = _tokenGenerator.CreateAccessToken(
                _audience.Secret,
                user.Email.Value,
                _audience.Iss,
                _audience.Aud);
          
            return new LoginUserCommandResult(accessToken.Token, accessToken.ExpiresIn);
        }
    }
}