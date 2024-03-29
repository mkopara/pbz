﻿using AuthAtrributes;
using CacheAttribute;
using Core.DomainModels;
using Core.Interfaces;
using Core.Interfaces.ExchangeRate;
using Core.SharedModels;
using Implementation.Repositories;
using PbzApi.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace PbzApi.Controllers
{
    public class ExchangeRatesController : ApiController
    {

        IPBZExchangeRateRepository _pbzRepository;

        public ExchangeRatesController(IPBZExchangeRateRepository pbzRepository)
        {
            _pbzRepository = pbzRepository;
        }

        // GET api/ExchangeRates
        [HttpGet]
        [TokenAuthorizationFilter]
        [GalileoDailyCache(23,59,59)]
        [Route("api/ExchangeRates/")]
        public async Task<HttpResponseMessage> Get([FromUri]PagingQueryInfo pagingInfo, string name = null, string code = null)
        {
            if (pagingInfo == null) pagingInfo = new PagingQueryInfo();

            //prevent large data sets
            if (pagingInfo.Take > 50)
                pagingInfo.Take = 50;

            //fetch data from PBZ api
            var data = await _pbzRepository.FetchAsync();

            //apply filters if set
            if (!string.IsNullOrEmpty(name))
                data = data.Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(code))
                data = data.Where(m => m.CurrencyCode.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            //provide data to helper class, to page it and create link information about previous and next page
            var paginator = new Paginator<Currency>(data, pagingInfo.Page, pagingInfo.Take);

            //paged data
            data = paginator.List;

            return Request.CreateResponse(HttpStatusCode.OK, new RatesModel
            {
                Links = paginator.GetPaginationLinks(Request.RequestUri.GetLeftPart(UriPartial.Path)),
                Data = data.ToList()
            });
        }

        [HttpGet]
        [BasicAuthenticationFilter(true)]
        [Route("api/ExchangeRates/memorycache")]
        [GalileoDailyCache(23, 59, 59)]
        public async Task<HttpResponseMessage> FullCached([FromUri]PagingQueryInfo pagingInfo, string name = null, string code = null)
        {
            if (pagingInfo == null) pagingInfo = new PagingQueryInfo();
            //prevent large data sets
            if (pagingInfo.Take > 50)
                pagingInfo.Take = 50;


            var now = DateTime.Now;
            var midnightToday = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999, DateTimeKind.Local);
            var secondsUntilMidnight = (int)Math.Round((midnightToday - DateTime.Now).TotalSeconds);

            //fetch data from PBZ api or from cache if exists
            var data = await InMemoryCache.GetOrSet("PbzRates", 
                async () => await _pbzRepository.FetchAsync(), secondsUntilMidnight);

            //apply filters if set
            if (!string.IsNullOrEmpty(name))
                data = data.Where(m => m.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
            if (!string.IsNullOrEmpty(code))
                data = data.Where(m => m.CurrencyCode.Equals(code, StringComparison.CurrentCultureIgnoreCase));

            //provide data to helper class, to page it and create link information about previous and next page
            var paginator = new Paginator<Currency>(data, pagingInfo.Page, pagingInfo.Take);
            //paged data
            data = paginator.List;

            var response =  Request.CreateResponse(HttpStatusCode.OK, new RatesModel
            {
                Links = paginator.GetPaginationLinks(Request.RequestUri.GetLeftPart(UriPartial.Path)),
                Data = data.ToList()
            });

            response.Headers.Add("Cache-Control", string.Format("max-age={0}", secondsUntilMidnight));

            return response;
        }
    }
}
