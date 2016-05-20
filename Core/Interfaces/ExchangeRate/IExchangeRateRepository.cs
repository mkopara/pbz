using Core.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.ExchangeRate
{
    /// <summary>
    /// Generic Interface for Exchage Rate Lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IExchangeRateRepository<T> where T : Currency
    {
        Task<IEnumerable<T>> FetchAsync();
    }
}
