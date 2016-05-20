using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CacheAttribute
{
    /// <summary>
    /// Holder for cache items
    /// </summary>
    public class CacheHolder
    {

        public CacheHolder(DateTime expirationDate, Object value, string key, string responseString)
        {
            ExpirationDate = expirationDate;
            Value = value;
            Key = key;
            _responseString = responseString;
            ETag = GetMD5Hash(_responseString + Key);
           
        }

        public string Key { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Object Value { get; set; }
        public Object ETag { get; set; }
        private string _responseString { get; set; }

        public static String GetMD5Hash(String TextToHash)
        {
            //Check wether data was passed
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return String.Empty;
            }

            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                var textToHash = Encoding.Default.GetBytes(TextToHash);
                var result = md5.ComputeHash(textToHash);

                //Convert result back to string.
                return System.BitConverter.ToString(result).Replace("-", "");
            }
        }
    }
}
