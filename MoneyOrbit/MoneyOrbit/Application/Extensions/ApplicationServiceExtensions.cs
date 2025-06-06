using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Infrastructure.Services;

namespace MoneyOrbit.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IFirebaseService, FirebaseService>();

            services.AddScoped<IFirebaseService, FirebaseService>();
            services.AddScoped<IDataService, DataService>();
            services.AddScoped<IUserService, UserService>();

            // NOTE: This sets up AutoMapper in our project. It will need a Profile class that defines named configurations
            // you plan to use. See AutoMapperProfiles for clarification
            // services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

            return services;
        }
    }
}
