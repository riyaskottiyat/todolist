using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TODOApp.Util
{
    /// <summary>
    /// Class CryptoUtility.
    /// </summary>
    public class CryptoUtility
    {
        /// <summary>
        /// Makes the hash.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public static string MakeHash(string value)
        {
            return Convert.ToBase64String(SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(value)));
        }

        /// <summary>
        /// Compares the hash.
        /// </summary>
        /// <param name="plainString">The plain string.</param>
        /// <param name="hashString">The hash string.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool CompareHash(string plainString, string hashString)
        {
            if (MakeHash(plainString) == hashString)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}