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
            builder.HasMany<PermissionType>().WithMany(a=> a.PermisssionsEmployees);
            builder.HasOne<Employee>().WithOne(a=> a.PermissionEmployees);
            builder.Property(e=> e.LastUpdated).HasDefaultValue(DateTime.UtcNow);
            builder.Property(e=> e.Guid).HasDefaultValue(Guid.NewGuid());
            builder.Property(e=> e.PermissionTypes).IsRequired();
            builder.Property(e=> e.Employees).IsRequired();
        }
    }
}
