namespace MoneyOrbit.Application.Interfaces.IApplication.IData
{
    public interface ICache
    {
        /// <summary>
        ///  This will take in the method (<paramref name="DataServiceGetDataCall"/>) you want called from the <see cref="IDataService"/> and fire it accordingly,
        ///  whilst still wraping that call with extrenous functionality. In our case it was the need for Caching. 
        ///  <para> PLEASE NOTE: If the CacheKey isn't set it will not cache anything and just invoke <paramref name="DataServiceGetDataCall"/></para>
        ///  <para> But if you do want caching functionality, you'll have to give a <paramref name="CacheKey"/>. There are also defaults for the sliding and absolute
        ///  expiration time windows for the caching, and you can make use of them if you so desire.</para>
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="DataServiceGetDataCall"></param>
        /// <param name="CacheKey"></param>
        /// <param name="slidingExpirationInMinutes">This is the window of time the <see cref="IMemoryCache"/> clearing timer gets reset everytime a call to the specific
        /// <paramref name="keySlice"/> is made. As long as you make the call in the window of time, it starts counting afresh. The default is set so that it exist within
        /// expected frequency levels made by the API but lower than the time it takes for a user to edit information</param>
        /// <param name="absoluteExpirationInMinutes">This is the window of time the <see cref="IMemoryCache"/> is allowed to keep reseting the timer. After this time elapses if you
        /// go above the <paramref name="slidingExpirationInMinutes"/> it will clear the cache under that key regardless. It is set to be the expected average time of a users session</param>
        /// <returns></returns>
        public Task<TResult> GetData<TResult>
            (Func<Task<TResult>> DataServiceGetDataCall, string CacheKey = null,
            double slidingExpirationInMinutes = 1, double absoluteExpirationInMinutes = 20) where TResult : class;
    }
}
