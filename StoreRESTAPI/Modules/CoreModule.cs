using Microsoft.EntityFrameworkCore;
using WebStore.Data.Context;
using WebStore.Service;

namespace StoreRESTAPI.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
               .FromAssembliesOf(typeof(IRequestHandler<>))
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime()
               .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                   .AsImplementedInterfaces()
                   .WithTransientLifetime());

            services.AddDbContext<WebStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("WebStore"));
            });

            return services;
        }
    }
}
