using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Persistence.Context;

namespace Query.Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<QueryDbContext>(options =>
              options.UseSqlServer(configuration.GetConnectionString("QueryConnection")));

        }
    }
}