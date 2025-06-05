using Command.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Command.Persistence.Context
{
    // DbContext Command
    public class CommandDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public CommandDbContext(DbContextOptions<CommandDbContext> options) : base(options) { }
    }
}
