using Microsoft.EntityFrameworkCore;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Contexts
{
    public class ChallengeContext : DbContext
    {
        public ChallengeContext(DbContextOptions<ChallengeContext> options)
        : base(options)
        {
        }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<PermissionType>? PermissionsTypes { get; set; }
        public DbSet<PermissionsEmployee>? Permissions { get; set; }
        public DbSet<WorkArea>? WorkAreas { get; set; }
    }
}
