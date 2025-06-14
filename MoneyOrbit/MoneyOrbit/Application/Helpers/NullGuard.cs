namespace MoneyOrbit.Application.Helpers
{
    public static class NullGuard
    {
        /// <summary>
        /// Checks if the object is null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns> True if null</returns>
        public static bool IsNull<T>(T obj)
        {
            return obj == null;
        }

        /// <summary>
        /// Checks if the object is not null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns>True if not null</returns>
        public static bool IsNotNull<T>(T obj)
        {
            return obj != null;
        }

        /// <summary>
        /// If the object is null, it will throw an ArgumentNullException.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ThrowIfNull<T>(T obj, string paramName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName ?? nameof(obj));
            return obj;
        }
    }

}
