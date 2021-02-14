using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace RealWorldOne.UserManagement.Application.Common.Security
{
    public static class Hash
    {
        public static string Create(string password, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(  
                password: password,  
                salt: Encoding.UTF8.GetBytes(salt),  
                prf: KeyDerivationPrf.HMACSHA512,  
                iterationCount: 10000,  
                numBytesRequested: 256 / 8);  

            return Convert.ToBase64String(valueBytes); 
        }

        public static bool Verify(string password, string salt, string hashedPassword)
        {
            return Create(password, salt) == hashedPassword; 
        }
    }
}