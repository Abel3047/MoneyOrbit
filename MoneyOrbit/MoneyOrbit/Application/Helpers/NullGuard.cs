namespace MoneyOrbit.Application.Helpers
{
    public static class NullGuard
    {
        public static bool IsNull<T>(T obj)
        {
            return obj == null;
        }

        public static bool IsNotNull<T>(T obj)
        {
            return obj != null;
        }

        public static T ThrowIfNull<T>(T obj, string paramName = null)
        {
            if (obj == null)
                throw new ArgumentNullException(paramName ?? nameof(obj));
            return obj;
        }
    }

}
