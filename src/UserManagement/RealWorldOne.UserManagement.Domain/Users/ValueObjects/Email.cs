using System;

namespace RealWorldOne.UserManagement.Domain.Users.ValueObjects
{
    public readonly struct Email : IEquatable<Email>
    {
        public Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public bool Equals(Email other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Email other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public static bool operator ==(Email left, Email right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Email left, Email right)
        {
            return !(left == right);
        }
    }
}