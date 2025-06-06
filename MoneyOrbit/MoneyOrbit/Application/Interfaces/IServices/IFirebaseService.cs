using MoneyOrbit.Application.Helpers;

namespace MoneyOrbit.Application.Interfaces.IServices
{
    public interface IFirebaseService:IDataService
    {       
        /// <summary>
        /// This is a FirebaseService method that is specific to firebase. And therefore needs to be defined on its own.
        /// It is what is called when <see cref="IDataService.GetCollectionOfType{T}(string)"/> is called.
        /// <para> But typically, its called when you want to filter so you have to use the direct capabilities of 
        /// firebase to do so.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filteringOptions"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetCollectionOfType<T>(FilteringOptions filteringOptions);
    }
}