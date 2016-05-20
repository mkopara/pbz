﻿using Core.DomainModels;
using Core.Interfaces;
using Core.Interfaces.ExchangeRate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Implementation.Repositories
{
    public class PBZExchangeRateRepository : IPBZExchangeRateRepository
    {
        public async Task<IEnumerable<Currency>> FetchAsync()
        {
            using (var client = new HttpClient())
            {
                //fetch XML from PBZ api
                var xml = await client.GetStringAsync("https://www.pbz.hr/Tecajnice/Danasnja/pbzteclist.xml");

                var doc = XElement.Parse(xml);

                //parse XML to object
                var rates = doc.Element("ExchRate").Elements("Currency").Select(x => new Currency
                {
                    CurrencyCode = x.Attribute("Code").Value,
                    Name = x.Element("Name").Value,
                    Unit = int.Parse(x.Element("Unit").Value),
                    BuyRateCache = decimal.Parse(x.Element("BuyRateCache").Value.Replace(',', '.')),
                    BuyRateForeign = decimal.Parse(x.Element("BuyRateForeign").Value.Replace(',', '.')),
                    MeanRate = decimal.Parse(x.Element("MeanRate").Value.Replace(',', '.')),
                    SellRateForeign = decimal.Parse(x.Element("SellRateForeign").Value.Replace(',', '.')),
                    SellRateCache = decimal.Parse(x.Element("SellRateCache").Value.Replace(',', '.'))
                });

                return rates;
            }
        }
    }
}
