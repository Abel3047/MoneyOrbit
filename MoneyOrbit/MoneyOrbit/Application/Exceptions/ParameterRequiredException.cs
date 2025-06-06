using System.Runtime.Serialization;

namespace MoneyOrbit.Application.Exceptions
{
    /// <summary>
    /// This will be thrown when a method requires a parameter and was not supplied that parameter.
    /// </summary>
    [Serializable]
    public class ParameterRequiredException:Exception
    {
        public ParameterRequiredException()
        {

        }

        public ParameterRequiredException(string message) : base(message)
        {
        }

        public ParameterRequiredException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ParameterRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
