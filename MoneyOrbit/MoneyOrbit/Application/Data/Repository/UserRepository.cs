using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using MoneyOrbit.Core.Entities;

namespace MoneyOrbit.Application.Data.Repository
{
    public class UserRepository : EntityRepository, IUserRepository<IUser>
    {
        protected readonly string _nodePath;
        public UserRepository(IDataService dataService) : base(dataService, "Users") {
            _nodePath = "Users";
        }

        protected override string GetPropertyName() => "UserName";

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            // Get the property name from our override method to avoid hardcoding "UserName"
            string propertyToSearch = GetPropertyName();

            // Call the correct DataService method
            var usersFound = await _dataService.GetCollectionWithIdenticalProperty<User>(
                _nodePath,           // The node to search in, e.g., "Users"
                propertyToSearch,    // The property to filter on, e.g., "UserName"
                username             // The value we are looking for
            );

            // The method returns a collection, but we expect only one user.
            // FirstOrDefault() will return the first match, or null if the collection is empty.
            return usersFound.FirstOrDefault();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            // Call the correct DataService method to find users where the "Email" property matches.
            var usersFound = await _dataService.GetCollectionWithIdenticalProperty<User>(
                _nodePath,      // The node to search in, e.g., "Users"
                "Email",        // The specific property name to filter on
                email           // The email address we are looking for
            );

            // GetCollectionWithIdenticalProperty returns a collection.
            // Since email should be unique, we expect at most one user.
            // .FirstOrDefault() safely gets that user, or returns null if none were found.
            return usersFound.FirstOrDefault();
        }

    }
}
