using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CacheAttribute
{
    /// <summary>
    /// Wrapper for cache items in GalileoCaching system
    /// </summary>
    public class CacheHolder
    {

        public CacheHolder(DateTime expirationDate, byte[] value, string key,  MediaTypeHeaderValue contentType)
        {
            ExpirationDate = expirationDate;
            Value = value;
            Key = key;
            ContentType = contentType;

            //calculate md5 hash for ETag attribute
            ETag = GetMD5Hash(Value);
           
        }

        public string Key { get; set; }
        public DateTime ExpirationDate { get; set; }
        public byte[] Value { get; set; }
        public string ETag { get; set; }
        public MediaTypeHeaderValue ContentType { get; set; }
        private string _responseString { get; set; }
       

        private static String GetMD5Hash(byte[] content)
        {
            //Check wether data was passed
            if ((content == null))
            {
                return String.Empty;
            }

            //Calculate MD5 hash. This requires that the string is splitted into a byte[].
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                var result = md5.ComputeHash(content);

                //Convert result back to string.
                return System.BitConverter.ToString(result).Replace("-", "");
            }
        }
    }
}
