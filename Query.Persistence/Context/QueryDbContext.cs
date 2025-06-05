using Microsoft.EntityFrameworkCore;
using Query.Domain.Entities;

namespace Query.Persistence.Context
{
    // DbContext Query
    public class QueryDbContext : DbContext
    {
        public DbSet<OrderReadModel> Orders { get; set; }
        public QueryDbContext(DbContextOptions<QueryDbContext> options) : base(options) { }
    }
}
