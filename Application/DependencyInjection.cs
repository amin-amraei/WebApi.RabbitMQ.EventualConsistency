using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Query.Application.Orders.SubscribeOrder;

namespace Command.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<OrderEventListenerService>();
        }
    }
}