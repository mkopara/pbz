using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModels
{
    
    public class Currency 
    {
        public decimal BuyRateCache { get; set; }
        public decimal BuyRateForeign { get; set; }
        public string CurrencyCode { get; set; }
        public decimal MeanRate { get; set; }
        public string Name { get; set; }
        public decimal SellRateCache { get; set; }
        public decimal SellRateForeign { get; set; }
        public int Unit { get; set; }
    }
}
