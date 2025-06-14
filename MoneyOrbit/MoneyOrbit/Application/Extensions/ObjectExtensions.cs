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
