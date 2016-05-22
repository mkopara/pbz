using Core.Interfaces.Security;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.Auth
{
    public class AuthApiService : IAuthApiService
    {
        public Task<bool> ValidateToken(string token)
        {
            return Task.Run(() => true);
        }

        public async Task<int> Authenticate(string email, string password)
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
                    var response = await wb.UploadValuesTaskAsync(new Uri("http://localhost:49586/api/auth/generate"), "POST", data);
                    var responseString = System.Text.Encoding.UTF8.GetString(response);
                    return 1;
                }
                catch (WebException ex)
                {
                    //var statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    return 0;
                   // return "An error occurred, status code: " + statusCode;
                }

            }
            //return Task.Run(() => 1);
        }

   
    }
}
