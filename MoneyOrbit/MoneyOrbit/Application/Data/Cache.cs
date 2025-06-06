using Microsoft.Extensions.Caching.Memory;
using MoneyOrbit.Application.Interfaces.IApplication.IData;

namespace MoneyOrbit.Application.Data
{
    /// <summary>
    /// This the is abstraction that will wrap a <typeparamref name="IDataService"/> and its functionality. It will be used for mostly implementing the cache technology
    /// over the and how the information we recieve from the <typeparamref name="IDataService"/> is retrieved. (The method calls).
    /// </summary>
    public class Cache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// In this constructor you have to input all the data source DataContexts. (<paramref name="dataServices"/>)
        /// This is neccessary so that you can make sure the methods you are trying to fire are indeed part of the <see cref="IDataService"/> Type.
        /// But with the API that's not really neccessary so we will manually input them.
        /// The <see cref="IMemoryCache"/> is really important for the Caching feature we involved
        /// </summary>
        /// <param name="dataServices"></param>
        /// <param name="memoryCache"></param>
        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        ///  This will take in the method (<paramref name="DataServiceFunctionCall"/>) you want called from the <see cref="IDataService"/> and fire it accordingly,
        ///  whilst still wraping that call with Caching. 
        ///  <para> PLEASE NOTE: If the CacheKey isn't set it will not cache anything and just invoke <paramref name="DataServiceFunctionCall"/></para>
        ///  <para> But if you do want caching functionality, you'll have to give a <paramref name="CacheKey"/>. There are also defaults for the sliding and absolute
        ///  expiration time windows for the caching, and you can make use of them if you so desire.</para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="DataServiceFunctionCall"></param>
        /// <param name="CacheKey"></param>
        /// <param name="slidingExpirationInMinutes">This is the window of time the <see cref="IMemoryCache"/> clearing timer gets reset everytime a call to the specific
        /// <paramref name="keySlice"/> is made. As long as you make the call in the window of time, it starts counting afresh. The default is set so that it exist within
        /// expected frequency levels made by the API but lower than the time it takes for a user to edit information</param>
        /// <param name="absoluteExpirationInMinutes">This is the window of time the <see cref="IMemoryCache"/> is allowed to keep reseting the timer. After this time elapses if you
        /// go above the <paramref name="slidingExpirationInMinutes"/> it will clear the cache under that key regardless. It is set to be the expected average time of a users session</param>
        /// <returns></returns>
        public async Task<TResult> GetData<TResult>
            (Func<Task<TResult>> DataServiceFunctionCall, string CacheKey = null,
            double slidingExpirationInMinutes = 1, double absoluteExpirationInMinutes = 20) where TResult : class
        {
            //Checks if its not null and fires with the Caching functionality
            if (CacheKey != null)
                return await GetCacheValue(CacheKey, DataServiceFunctionCall, slidingExpirationInMinutes, absoluteExpirationInMinutes);
            //If the cacheKey is empty it won't perform Caching functionality
            return await DataServiceFunctionCall.Invoke();
        }
        #region Support Methods

        /// <summary>
        /// This takes in a <paramref name="keySlice"/> and gets a value, whether it is from <see cref="IMemoryCache"/> or a result from <paramref name="DataServiceCall"/>.
        /// It does this by checking if there is anything under the <paramref name="keySlice"/> and returning using 'out' if true. Otherwise it will set the value in the 
        /// <see cref="IMemoryCache"/> from the result of the <paramref name="DataServiceCall"/> and return that.
        /// </summary>
        /// <typeparam name="TResult"> This is whatever it is we are trying to get from the datasource provided, else the <see cref="IMemoryCache"/></typeparam>
        /// <param name="keySlice"></param>
        /// <param name="DataServiceCall"></param>
        /// <returns> A <typeparamref name="TResult"/> whether it comes from the <see cref="IMemoryCache"/> or <paramref name="DataServiceCall"/> result</returns>
        private async Task<TResult> GetCacheValue<TResult>
            (string keySlice, Func<Task<TResult>> DataServiceCall, double slidingExpirationInMinutes, double absoluteExpirationInMinutes) where TResult : class
        {
            TResult result = null;

            //If it doesn't find any value under the keyslice within the cache
            if (_memoryCache.Get(keySlice) == null)
            {
                //Sets the result of the call to the result we are going to pass in
                result = await DataSourceCallDelegateFunc(DataServiceCall);
                _memoryCache.Set(keySlice, result,
                    new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationInMinutes))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpirationInMinutes)));
                return result;
            }
            return (TResult)_memoryCache.Get(keySlice);
        }
        /// <summary>
        /// This is supposed to be the delegate method that will depend on what you put into it and fire that. I made it so that the method we are trying to fire
        /// only fires when we want it to. Cause if you just put <paramref name="DataServiceCall"/> straight into
        ///  <see cref="GetCacheValue{TResult}(string, Func{Task{TResult}}, double, double)"/> in as a parameter, it will be called and fired before it is used
        ///  inside <see cref="GetCacheValue{TResult}(string, Func{Task{TResult}}, double, double)"/> 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="DataServiceCall"></param>
        /// <returns></returns>
        private async Task<TResult> DataSourceCallDelegateFunc<TResult>(Func<Task<TResult>> DataServiceCall) => await DataServiceCall.Invoke();

        #endregion
    }

   
}
