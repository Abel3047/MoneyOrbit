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

        /// <summary>
        /// This checks if an propertyKeyword exists in the database. 
        /// <para> Essentially, each repository should have a property that they look for. For <see cref="IUserRepository{TUser}"/>
        /// its <see cref="IUser.UserName"/>. The <paramref name="propertyKeyword"/> would then be something specific like 
        /// 'Abel37'</para>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<bool> DoesPropertyExist(string propertyKeyword);
        /// <summary>
        /// Gets collection from the database under the repository node and propertyname with the 
        /// keyword <paramref name="propertyKeyword"/>. Expect to find subrepositories to have their own overrides
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyKeyword"></param>
        /// <returns></returns>
        public Task<IEnumerable<T>> GetCollectionWithIdenticalProperty<T>(string propertyKeyword);
    }
}
