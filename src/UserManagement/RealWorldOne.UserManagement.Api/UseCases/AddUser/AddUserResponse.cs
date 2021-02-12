using System;
using Newtonsoft.Json;

namespace RealWorldOne.UserManagement.Api.UseCases.AddUser
{
    public sealed class AddUserResponse : IEquatable<AddUserResponse>
    {
        [JsonProperty(PropertyName = "user_id")]
        public Guid UserId { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        public bool Equals(AddUserResponse other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return UserId.Equals(other.UserId) && Name.Equals(other.Name) && Email.Equals(other.Email);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is AddUserResponse other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(UserId, Name, Email);
        }
    }
}