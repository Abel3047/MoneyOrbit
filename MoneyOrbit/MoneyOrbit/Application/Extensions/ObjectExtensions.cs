using Firebase.Database;
using Newtonsoft.Json;

namespace MoneyOrbit.Application.Extensions
{
    public static class ObjectExtensions
    {
        public static T ToObject<T>(this IDictionary<string, object> source)
        {
            var json = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Maps T2 to T and returns an object of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static T Map<T, T2>(this object data) where T : new()
        {
            Dictionary<string, object> valPairs = (Dictionary<string, object>)((new T()).AsDictionary());

            IDictionary<string, object> keyValuePairs = ((T2)data).AsDictionary();

            foreach (var item in keyValuePairs)
            {
                foreach (var key in valPairs)
                {
                    if (item.Key.ToLower() == key.Key.ToLower())
                    {
                        valPairs[key.Key] = item.Value;
                        continue;
                    }
                }
            }

            return valPairs.ToObject<T>();
        }
        /// <summary>
        /// Extension to make sure the enumerable returned is never null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns>IEnumerable<T></returns>
        public static IEnumerable<T> ToEnumerable<T>(this IReadOnlyCollection<FirebaseObject<T>> data)
        {
            return data.Count() == 0 ? (new T[0]).AsEnumerable() : data.Select(item => item.Object);
        }
    }
}
