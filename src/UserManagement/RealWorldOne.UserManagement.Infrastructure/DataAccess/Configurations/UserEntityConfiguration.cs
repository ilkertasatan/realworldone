using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealWorldOne.UserManagement.Domain.Users;
using RealWorldOne.UserManagement.Domain.Users.ValueObjects;

namespace RealWorldOne.UserManagement.Infrastructure.DataAccess.Configurations
{
    public sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .HasConversion(v => v.Value, v => new UserId(v))
                .IsRequired();
            
            builder
                .Property(x => x.Name)
                .HasConversion(v => v.Value, v => new Name(v))
                .IsRequired();
            
            builder
                .Property(x => x.Email)
                .HasConversion(v => v.Value, v => new Email(v))
                .IsRequired();
            
            builder
                .Property(x => x.Password)
                .HasConversion(v => v.Value, v => new Password(v))
                .IsRequired();
        }
    }
}