using MoneyOrbit.Application.Exceptions;
using MoneyOrbit.Application.Interfaces.IApplication.IData.IRepository;
using MoneyOrbit.Application.Interfaces.IEntities;
using MoneyOrbit.Application.Interfaces.IServices;
using System.IO;

namespace MoneyOrbit.Application.Data.Repository
{
    /// <summary>
    /// <para>
    /// This abstract class serves as a base for all propertyKeyword repositories. It implements the IEntityRepository interface
    /// and provides the contracts for how all other repositories should behave. It also contains a protected string _nodepath 
    /// that is used to ensure that all information about this repository is taken from the right place in the database.
    /// </para>
    /// A Repository should take in IDataService as a dependency, which is used to interact with the database.
    /// And then using this IDataService, perform CRUD functions on the entities that are being managed by this repository.
    /// </summary>
    public abstract class EntityRepository : IEntityRepository<IEntity>
    {
        /// <summary>This is the _nodepath for this repository. This ensures that all information about this is taken from the right
        /// place in the database. </summary>
        protected string _nodepath { get; set; }
        protected abstract string GetPropertyName();
        protected IDataService _dataService;

        public EntityRepository(IDataService dataService, string nodepath = "Entity")
        {
            _dataService = dataService;
            _nodepath = nodepath;
        }

        public virtual async Task<T> GetInstanceOfType<T>(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ParameterRequiredException("The path parameter is required to update data.");
            //Makes sure that the string appends the _nodepath for this respective repository
            path = _nodepath + '/' + path;
            return await _dataService.GetInstanceOfType<T>(path);
        }
        public virtual async Task<IEnumerable<T>> GetCollectionOfType<T>(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ParameterRequiredException("The path parameter is required to update data.");
            //Makes sure that the string appends the _nodepath for this respective repository
            path = _nodepath + '/' + path;

            return await _dataService.GetCollectionOfType<T>(path);
        }
        public virtual Task UpdateData(string path, object data)
        {
            if (string.IsNullOrEmpty(path)) throw new ParameterRequiredException("The path parameter is required to update data.");
            //Makes sure that the string appends the _nodepath for this respective repository
            path = _nodepath + '/' + path;

            return _dataService.UpdateData(path, data);
        }
        public virtual Task DeleteData(string path)
        {
            if (string.IsNullOrEmpty(path)) throw new ParameterRequiredException("The path parameter is required to update data.");
            //Makes sure that the string appends the _nodepath for this respective repository
            path = _nodepath + '/' + path;

            return _dataService.DeleteData(path);
        }

        public async Task<bool> DoesPropertyExist(string propertyKeyword)=>
            await _dataService.DoesPropertyExist(_nodepath, GetPropertyName(),propertyKeyword);
    }
}
