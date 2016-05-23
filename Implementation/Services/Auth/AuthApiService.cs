using Core.DomainModels.Security;
using Core.Interfaces.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Auth
{
      /// <summary>
      /// Token validation and user authentication via web request
      /// </summary>
    public class AuthApiService : IAuthApiService
    {
        private string _endPoint;

        public AuthApiService()
        {
            _endPoint = ConfigurationManager.AppSettings["SecurityEndpoint"];

            if (_endPoint == null)
                throw new ArgumentException("Please add SecurityEndpoint to configuration file");
        }
        public async Task<TokenInfo> ValidateToken(string token)
        {
            using (var wb = new WebClient())
            {

                var data = new NameValueCollection();
                data["token"] = token;
               
                try
                {
                    var response = await wb.UploadValuesTaskAsync(new Uri(_endPoint+"validate"), "POST", data);
                    var responseString = System.Text.Encoding.UTF8.GetString(response);
                    var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(responseString);

                    //check headers if needed
                    // WebHeaderCollection myWebHeaderCollection = wb.ResponseHeaders;

                    return tokenInfo;
                }
                catch (WebException ex)
                {
                    //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    return null;
                    //returning null if not authenticated
                }

            }
            //return Task.Run(() => 1);
        }

        public async Task<TokenInfo> Authenticate(string email, string password)
        {
            using (var wb = new WebClient())
            {
                
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(email+":"+password);
                var base64 = System.Convert.ToBase64String(plainTextBytes);

                var data = new NameValueCollection();
                data["Authorization"] = "Basic " + base64;
                //data["password"] = "myPassword";
                wb.Headers.Add(data);

                try
                {
                    var response = await wb.UploadValuesTaskAsync(new Uri(_endPoint + "generate"), "POST", new NameValueCollection());
                    var responseString = System.Text.Encoding.UTF8.GetString(response);
                    var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(responseString);

                    //check headers if needed
                   // WebHeaderCollection myWebHeaderCollection = wb.ResponseHeaders;

                    return tokenInfo;
                }
                catch (WebException ex)
                {
                    //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    return null;
                  //returning null if not authenticated
                }

            }
            //return Task.Run(() => 1);
        }

   
    }
}
