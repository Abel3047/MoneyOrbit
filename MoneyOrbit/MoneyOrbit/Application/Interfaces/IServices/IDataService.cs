using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    /// <summary>
    /// This interface provides a base for data services.
    /// <para>This also works to have a contract for the <see cref="DataService"/>. When the service is instantiated, the class
    /// will have the connectionstring and authentication string required to be instantiated as well.
    /// It also works to define all the necessary information needed to connect to a database.</para>
    /// <para> Types that implement this service will have to populate the specific database information needed to connect
    /// to its database.</para>
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Get single instance of type T at path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns>Single instance of type T</returns>
        public Task<T> GetInstanceOfType<T>(string path);
        /// <summary>
        /// Gets a collection of type T at the path specified from the database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetCollectionOfType<T>(string path);
        /// <summary>
        /// Stores data with options to store at a path 
        /// and generate a unique key for the data.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns>IEnumerable of type T</returns>
        public Task StoreData(string path, object data, bool generateKey = false);
        /// <summary>
        /// Updating an object can be done at a single property of an 
        /// object or the entire object or collection.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Task UpdateData(string path, object data);
        /// <summary>
        /// Deletes the data at the directory defined by the node and path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Task DeleteData(string path);
        /// <summary>
        /// This takes the <paramref name="nodepath"/> from a repository, and then checks <paramref name="property"/>
        /// amoung what we are trying to search for using the keyword <paramref name="keyword"/>. If it finds something under these
        /// three path , it returns true, otherwise it returns false.
        /// </summary>
        /// <param name="nodepath"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        Task<bool> DoesPropertyExist(string nodepath, string property, string keyword);
    }
}
