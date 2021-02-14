using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using RealWorldOne.UserManagement.Application.Common.Interfaces;

namespace RealWorldOne.UserManagement.Application.Common.Security
{
    public class AccessTokenGenerator : IGenerateAccessToken
    {
        private static readonly TimeSpan ExpiresIn = TimeSpan.FromMinutes(5);

        public AccessToken CreateAccessToken(string secret, string sub, string iss, string aud)
        {
            var now = DateTime.UtcNow;
            var expires = now.Add(ExpiresIn);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, sub, ClaimValueTypes.String),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var securityToken = new JwtSecurityToken(
                iss,
                aud,
                claims,
                now,
                expires,
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return new AccessToken(accessToken, (int)ExpiresIn.TotalMilliseconds);
        }
    }
}