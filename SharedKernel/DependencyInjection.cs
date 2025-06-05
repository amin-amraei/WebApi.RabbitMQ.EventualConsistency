using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;


namespace Authentication
{
    public static class DependencyInjection
    {
        public static void AddShareKernel(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"));
        }
    }
}