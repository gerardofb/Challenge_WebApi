using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    internal class WorkAreaConfiguration : IEntityTypeConfiguration<WorkArea>
    {
        public void Configure(EntityTypeBuilder<WorkArea> builder)
        {
            builder.HasKey(x => x.Id); 
            builder.Property(x=> x.AreaName).IsRequired().HasMaxLength(100).IsUnicode(true);
        }
    }
}
