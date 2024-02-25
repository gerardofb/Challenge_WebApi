using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;

namespace TestChallengeWebApi.FactoryDbContext
{
    public class ChallengeContextFactory : IDesignTimeDbContextFactory<ChallengeContext>
    {
       
        public ChallengeContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ChallengeContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ChallengeContext(optionsBuilder.Options);
        }
    }
}
