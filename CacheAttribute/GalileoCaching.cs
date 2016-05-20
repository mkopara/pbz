﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CacheAttribute
{

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class GalileoCaching : ActionFilterAttribute
    {
        //storing all cached info in this variable
       // private static Dictionary<string, CacheHolder> cache = new Dictionary<string, CacheHolder>();

        protected DateTime _expirationDate;

        public async override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var key = actionContext.Request.RequestUri.ToString();

            //if cache contains element which did not expire, inject it back to response
            if (GalileoMemoryStorage.Contains(key))
            {
                var memoryItem = GalileoMemoryStorage.Get(key);
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, memoryItem.Value);

                //set cache control header for clients to know that content is cached
                var secondsLeft = Math.Round((memoryItem.ExpirationDate - DateTime.Now).TotalSeconds);
                actionContext.Response.Headers.Add("Cache-Control", string.Format("max-age={0}", secondsLeft));
                actionContext.Response.Headers.Add("ETag", string.Format("\"{0}\"", memoryItem.ETag));
                return;
            }

            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        public async override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            //if current request is GET, there is no errors, and expiration date > now
            if(actionExecutedContext.Request.Method.Method.ToUpper() == "GET" 
                && actionExecutedContext.Response.IsSuccessStatusCode && _expirationDate > DateTime.Now)
            {
                //use url as storage key for cache
                var key = actionExecutedContext.Request.RequestUri.ToString();

                //fetch Value from response as object
                var responseValue = (actionExecutedContext.Response.Content as ObjectContent).Value;

                var responseString = await actionExecutedContext.Request.Content.ReadAsStringAsync();

                //create cache holder and add it to memory list
                var cacheHolder = new CacheHolder(_expirationDate, responseValue, key, responseString);
                GalileoMemoryStorage.Add(cacheHolder, key, _expirationDate);

                //set cache control header for clients to know that content is cached
                var secondsLeft = Math.Round((GalileoMemoryStorage.Get(key).ExpirationDate - DateTime.Now).TotalSeconds);
                actionExecutedContext.Response.Headers.Add("Cache-Control", string.Format("max-age={0}", secondsLeft));
                actionExecutedContext.Response.Headers.Add("ETag", string.Format("\"{0}\"", cacheHolder.ETag));
            }
           
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
    }
}
