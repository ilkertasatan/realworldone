using System;

namespace RealWorldOne.UserManagement.Domain.Users.ValueObjects
{
    public readonly struct UserId : IEquatable<UserId>
    {
        public UserId(Guid value)
        {
            Value = value;
        }

        public Guid Value { get; }

        public bool Equals(UserId other)
        {
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is UserId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(UserId left, UserId right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UserId left, UserId right)
        {
            return !(left == right);
        }
    }
}