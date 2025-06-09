using MoneyOrbit.Application.Data.Repository;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Infrastructure.Services;

namespace MoneyOrbit.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFirebaseService, FirebaseService>();

            //I used FirebaseService here instead of DataService because DataService is an abstract
            //If you wish to swap this with a different DataService, do so here
            services.AddScoped<IDataService, FirebaseService>();
            services.AddScoped<IUserRepository<IUser>, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}
