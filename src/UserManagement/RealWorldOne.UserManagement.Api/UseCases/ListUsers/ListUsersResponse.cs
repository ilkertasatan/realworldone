using System;

namespace RealWorldOne.UserManagement.Api.UseCases.ListUsers
{
    public sealed class ListUsersResponse : IEquatable<ListUsersResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public bool Equals(ListUsersResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && Name == other.Name && Email == other.Email;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ListUsersResponse)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Email);
        }
    }
}