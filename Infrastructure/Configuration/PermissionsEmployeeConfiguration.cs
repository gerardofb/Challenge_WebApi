using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Configuration
{
    internal class PermissionsEmployeeConfiguration : IEntityTypeConfiguration<PermissionsEmployee>
    {
        public void Configure(EntityTypeBuilder<PermissionsEmployee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => new { e.Guid, e.LastUpdated });
            builder.HasOne(a=> a.PermissionTypes).WithOne(b=> b.PermisssionsEmployees);
            builder.Property(e=> e.PermissionTypes).IsRequired();
            builder.Property(e=> e.Employees).IsRequired();
        }
    }
}
