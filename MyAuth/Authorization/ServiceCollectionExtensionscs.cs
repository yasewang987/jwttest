using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace jwttest.MyAuth
{
    public static class ServiceCollectionExtensionscs
    {
        public static IServiceCollection AddMyAuthorizationHandler(this IServiceCollection services)
        {
            return services.AddScoped<IAuthorizationHandler, MyAuthorizationHandler>();
        }
    }
}