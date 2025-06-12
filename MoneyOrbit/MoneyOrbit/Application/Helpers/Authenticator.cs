using MoneyOrbit.Application.Data.Repository;
using MoneyOrbit.Application.Exceptions;

namespace MoneyOrbit.Application.Helpers
{
    /// <summary>
    /// This class houses everything we need to authenticat tokens, OTPs and other forms of authentication.
    /// </summary>
    internal class Authenticator
    {
        /// <summary>
        /// This method verifies the Token (One Time Password) provided by the user against, well, Terrence will figure this one out.
        /// <para> If it fails to verify the Token, it should return empty. (Unless Terrence can figure out how to uses expections
        /// without breaking the existing references to this block</para>
        /// </summary>
        /// <param name="oTP"></param>
        /// <param name="userRepository"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static async Task<string> VerifyOTP(string OTP, UserRepository userRepository)
        {
            //@Terrence, please put implementation here
            return OTP;
        }
    }
}