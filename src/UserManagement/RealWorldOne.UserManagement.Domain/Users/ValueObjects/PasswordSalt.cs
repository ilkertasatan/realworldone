using System;

namespace RealWorldOne.UserManagement.Domain.Users.ValueObjects
{
    public readonly struct PasswordSalt : IEquatable<PasswordSalt>
    {
        public PasswordSalt(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool Equals(PasswordSalt other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is PasswordSalt other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(PasswordSalt left, PasswordSalt right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PasswordSalt left, PasswordSalt right)
        {
            return !(left == right);
        }
    }
}