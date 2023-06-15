using Microservices.IDP.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Microservices.IDP.Infrastructure.Entities.Configuration
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
     
        
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permission", SystemConstants.IdentitySchema).HasKey(x=>x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(c=>new {c.RoleId,c.Function,c.Command})
                .IsUnique();

        }

    }
}
