using Core.DomainModels;
using Core.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PbzApi.Models
{
    //Model used to return data from WEB Api
    public class RatesModel
    {

        public RatesModel()
        {

        }

        public List<Currency> Data { get; set; }

        public List<Link> Links { get; set; }
    }
}