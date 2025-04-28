using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TechReserveSystem.Infrastructure.Data.Context
{
    public class AppDbContexFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            Env.Load(@"../../src/TechReserveSystem.API/.env");
            var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__Default") ??
                throw new InvalidOperationException("String de conexão vazia.");

            var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new AppDbContext(optionBuilder.Options);
        }
    }
}