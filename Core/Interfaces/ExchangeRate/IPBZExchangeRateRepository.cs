using Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.ExchangeRate
{
    /// <summary>
    /// Interface representing PBZ api call
    /// </summary>
    public interface IPBZExchangeRateRepository : IExchangeRateRepository<Currency>
    {
    }
}
