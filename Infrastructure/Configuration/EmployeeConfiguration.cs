using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired().IsUnicode(true);
            builder.Property(b => b.LastName).HasMaxLength(150).IsUnicode(true);
            builder.Property(b => b.LastUpdated).HasDefaultValue(DateTime.UtcNow);
            builder.HasIndex(b => b.LastUpdated);
        }
    }
}
