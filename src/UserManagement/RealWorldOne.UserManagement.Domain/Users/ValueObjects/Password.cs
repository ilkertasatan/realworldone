using System;

namespace RealWorldOne.UserManagement.Domain.Users.ValueObjects
{
    public readonly struct Password : IEquatable<Password>
    {
        public Password(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool Equals(Password other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Password other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Password left, Password right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Password left, Password right)
        {
            return !(left == right);
        }
    }
}