using MoneyOrbit.Application.Interfaces.IServices;

namespace MoneyOrbit.Infrastructure.Services
{
    /// <summary>
    /// This class provides a base implementation for data services. It should have the connection string and authentication secret
    /// initialized when the class is instantiated. It also contains the methods that is expected of all DataServices.
    /// <para> Note: This is different from a DataTrafficService, which would choses which service is most appropriate/ available
    /// to perform the function</para>
    /// </summary>
    public abstract class DataService: IDataService
    {
        protected string connectionString;
        protected string authenticationSecret;

        public abstract Task DeleteData(string path);
        public abstract Task<IEnumerable<T>> GetCollectionOfType<T>(string path);
        public abstract Task<T> GetInstanceOfType<T>(string path);
        public abstract Task StoreData(string path, object data, bool generateKey = false);
        public abstract Task UpdateData(string path, object data);
        public abstract Task<bool> DoesPropertyExist(string nodepath, string property, string keyword);
        public abstract Task<IEnumerable<T>> GetCollectionWithIdenticalProperty<T>(string nodepath, string property, string propertyKeyword);
    }
}