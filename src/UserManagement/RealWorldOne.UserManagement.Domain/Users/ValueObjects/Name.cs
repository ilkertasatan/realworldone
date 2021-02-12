using System;

namespace RealWorldOne.UserManagement.Domain.Users.ValueObjects
{
    public readonly struct Name : IEquatable<Name>
    {
        public Name(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool Equals(Name other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Name other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Name left, Name right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Name left, Name right)
        {
            return !(left == right);
        }
    }
}