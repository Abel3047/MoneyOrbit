using Firebase.Database;
using Firebase.Database.Query;
using MoneyOrbit.Application.Extensions;
using MoneyOrbit.Application.Helpers;
using MoneyOrbit.Application.Interfaces.IServices;
using Newtonsoft.Json;

namespace MoneyOrbit.Infrastructure.Services
{
    public class FirebaseService :DataService, IFirebaseService
    {
        private readonly FirebaseClient _firebaseClient;
        private Generators generators;

        public FirebaseService()
        {
            connectionString = Environment.GetEnvironmentVariable("Firebase_BasePath");
            authenticationSecret = Environment.GetEnvironmentVariable("Firebase_AuthSecret");
            _firebaseClient = new FirebaseClient(
              connectionString,
              new FirebaseOptions
              {
                  AuthTokenAsyncFactory = () => Task.FromResult(authenticationSecret)
              });
        }

        /// <summary>
        /// This directly gets a single instance of type T at the path specified from the Firebase Database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public override async Task<T> GetInstanceOfType<T>(string path) => await _firebaseClient.Child(path).OnceSingleAsync<T>();
        public override async Task<IEnumerable<T>> GetCollectionOfType<T>(string path)
        {
            var filter= new FilteringOptions { Path= path };
            return await GetCollectionOfType<T>(filter);
        }
        /// <summary>
        /// This method stores data at a path in the Firebase Database.
        /// <para>It also has implements an option on whether or not one wishes to generate a key for the path to store the
        /// object</para>
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <param name="generateKey"></param>
        /// <returns></returns>
        public override async Task StoreData(string path, object data, bool generateKey = false)
        {
            if (!generateKey)
            {
                await _firebaseClient
                    .Child(path)
                    .PutAsync(GetJSONData(data), DateTime.UtcNow.AddSeconds(5) - DateTime.UtcNow);
                return;
            }
            if (generateKey)
            {
                string generatedKey = generators.GenerateKey(DateTime.UtcNow);

                await _firebaseClient
                    .Child(path)
                    .Child(generatedKey)
                    .PutAsync(GetJSONData(data),
                    DateTime.UtcNow.AddSeconds(5) - DateTime.UtcNow);

                //Assign the auto generated path to the "ID" parameter of the object
                await UpdateData(path + "/" + generatedKey + "/ID", generatedKey);
            }
        }
        /// <summary>
        /// This overwrites what ever data is in the path with the object in the parameter in the Firebase Database
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task UpdateData(string path, object data)=> 
            await _firebaseClient
            .Child(path)
            .PutAsync(GetJSONData(data), DateTime.UtcNow.AddSeconds(5) - DateTime.UtcNow);
        /// <summary>
        /// This deletes the data at the path specified in the Firebase Database.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public override async Task DeleteData(string path) => await _firebaseClient.Child(path).DeleteAsync();

        //Method Unique to firebase
        /// <summary>
        /// This directly gets a collection of type T at the path specified from the Firebase Database.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filteringOptions"> This is set in the  repository and contains the path as well as other filtering
        /// possibilities</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> GetCollectionOfType<T>(FilteringOptions filteringOptions)
        {
            //Time Period & Limiting 
            if (filterByPeriod(filteringOptions.StartDate, filteringOptions.EndDate) &&
                filteringOptions.LimitToFirst != null)
            {
                //We have these variables so that we can make sure they are the correct type before they reach the method
                var startDate = (DateTime)filteringOptions.StartDate;
                var endDate = GetEndDate((DateTime)filteringOptions.EndDate);
                var limitToFirst = (int)filteringOptions.LimitToFirst;

                var result = await _firebaseClient
                    .Child(filteringOptions.Path)
                    .OrderByKey().StartAt(generators.GenerateKey(startDate)).EndAt(generators.GenerateKey(endDate))
                    .LimitToFirst(limitToFirst).OnceAsync<T>();

                return result.ToEnumerable();
            }
            //Time period only
            if (filterByPeriod(filteringOptions.StartDate, filteringOptions.EndDate) &&
                filteringOptions.LimitToFirst == null)
            {
                var startDate = (DateTime)filteringOptions.StartDate;
                var endDate = GetEndDate((DateTime)filteringOptions.EndDate);

                var result1 = await _firebaseClient
                    .Child(filteringOptions.Path)
                    .OrderByKey().StartAt(generators.GenerateKey(startDate))
                    .EndAt(generators.GenerateKey(endDate))
                    .OnceAsync<T>();

                return result1.ToEnumerable();
            }
            //Limiting only
            if (!filterByPeriod(filteringOptions.StartDate, filteringOptions.EndDate) &&
                filteringOptions.LimitToFirst != null)
            {
                var limitToFirst = (int)filteringOptions.LimitToFirst;

                var result2 = await _firebaseClient
                    .Child(filteringOptions.Path)
                    .OrderByKey().LimitToFirst(limitToFirst)
                    .OnceAsync<T>();

                return result2.ToEnumerable();
            }

            //This works if there is no filtering by time period or limiting
            var result3 = await _firebaseClient
                .Child(filteringOptions.Path)
                .OnceAsync<T>();

            return result3.ToEnumerable();
        }

        #region Support Methods
        private string GetJSONData(object data)=> JsonConvert.SerializeObject(data);        
        private bool filterByPeriod(DateTime? startDate, DateTime? endDate)
        {
            return startDate != null && endDate != null;
        }
        private DateTime GetEndDate(DateTime dateTime)
        {
            //We add a day and subtract a second when the time selected is 23:00:00
            //We do this so we can get the end of that day 23:59:59
            //We only do that when the time is 23:00:00
            if (dateTime.Hour == 0 &&
                dateTime.Minute == 0 &&
                dateTime.Second == 0)
                return dateTime.AddDays(1).AddSeconds(-1);

            return dateTime;
        }

        #endregion

    }
}