using MoneyOrbit.Application.Interfaces.IEntities;

namespace MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository
{
    public interface IEntityRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Get single instance of type T at path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns>Single instance of type T</returns>
        public Task<T> GetInstanceOfType<T>(string path);
        /// <summary>
        /// Get collection of type T at path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns>Collection of type T</returns>
        public Task<IEnumerable<T>> GetCollectionOfType<T>(string path);

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
    }
}
