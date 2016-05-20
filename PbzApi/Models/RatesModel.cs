using Core.DomainModels;
using Core.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PbzApi.Models
{
    public class RatesModel
    {
        public List<Currency> Data { get; set; }

        public List<Link> Links { get; set; }
    }
}