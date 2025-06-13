using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Infrastructure.Services;

namespace MoneyOrbit.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped<IFirebaseService, FirebaseService>();
            //I used FirebaseService here instead of DataService because DataService is an abstract
            //If you wish to swap this with a different DataService, do so here
            services.AddScoped<IDataService, FirebaseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IGoalService, GoalService>();

            //Repositories
            services.AddScoped<IUserRepository<IUser>, UserRepository>();
            services.AddScoped<IAccountRepository<IAccount>, AccountRepository>();
            services.AddScoped<ITransactionRepository<ITransaction>, TransactionRepository>();
            services.AddScoped<IGoalRepository<IGoal>, GoalRepository>();

            return services;
        }
    }
}
