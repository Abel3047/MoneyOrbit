using MoneyOrbit.Core.Entities;
using System.Security.Cryptography;
using System.Text;

namespace MoneyOrbit.Application.Helpers
{
    /// <summary>
    /// This is a class that houses the generators that feed the application. eg randomgenerators, key generators etc.
    /// </summary>
    public class Generators
    {
        internal static Tuple<byte[], byte[]> PasswordEncryptor(string Password)
        {
            using (var hmac = new HMACSHA512())
            {
                var PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                var PasswordSalt = hmac.Key;
                var result= Tuple(PasswordHash, PasswordSalt);
            }
            
        }

        /// <summary>
        /// This generates a key based on the current date and time. Its typically used in the Firebase database but can be used
        /// else where
        /// <para> It takes the ticks of DateTime and turns them to string</para>
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public string GenerateKey(DateTime dateTime)
        {
            long ticks = dateTime.Ticks;

            return ticks.ToString();
        }
    }
}
