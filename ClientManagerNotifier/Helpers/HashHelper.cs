using System;
using System.Security.Cryptography;
using System.Text;

namespace ClientManagerNotifier.Helpers
{
    /// <summary>
    /// The hash helper.
    /// </summary>
    public static class HashHelper
    {
        /// <summary>
        /// Calculates the hash.
        /// </summary>
        /// <param name="original">The original string for hash calculation.</param>
        /// <returns>Hash of the original string.</returns>
        public static string CalcHash(string original)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            var originalBytes = Encoding.Default.GetBytes(original);
            var encodedBytes = sha1.ComputeHash(originalBytes);
            var hash = BitConverter.ToString(encodedBytes);

            return hash;
        }
    }
}