using HealthChecks.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.WebApi.Data
{
    public class ApplicationDataContext : DbContext
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
        {
        }
        public DbSet<Developer> Developers;
    }
}
