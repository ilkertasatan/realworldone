using System;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Domain.Users
{
    public class User : Entity<UserId>
    {
        public static readonly User None = new();

        private User() { }

        public User(
            UserId userId,
            Name name,
            Email email,
            Password password,
            PasswordSalt passwordSalt)
        {
            Id = userId;
            Name = name;
            Email = email;
            Password = password;
            PasswordSalt = passwordSalt;
            CreatedAt = DateTime.UtcNow;
        }

        public Name Name { get; }
        public Email Email { get; }
        public Password Password { get; }
        public PasswordSalt PasswordSalt { get; set; }
        public DateTime CreatedAt { get; }
    }
}