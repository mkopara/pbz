using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PbzApi.HelperClasses
{
    public class Paginator<T> where T : class
    {

        private IEnumerable<T> _list;

        public int CurrentPage { get; set; }
        public int? PreviousPage { get; set; }
        public int? NextPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> List
        {
            get
            {
                return _list;
            }
        }

        public Paginator(IEnumerable<T> list, int page, int pageSize)
        {

            TotalPages = (int)Math.Ceiling((decimal)list.Count() / pageSize);
            PageSize = pageSize;

            _list = list.Skip((page - 1) * pageSize).Take(pageSize);

            if (page > TotalPages)
                page = TotalPages;

            if (TotalPages > page)
                NextPage = page + 1;
            if (page > 1 && TotalPages > 1)
                PreviousPage = page - 1;

            CurrentPage = page;
        }

        public List<Link> GetPaginationLinks()
        {
            var hrefs = new List<Link>();

            if (PreviousPage.HasValue)
                hrefs.Add(new Link("prev", string.Format("http://localhost:53048/api/ExchangeRates/?page={0}&take={1}", PreviousPage, PageSize)));
            if (NextPage.HasValue)
                hrefs.Add(new Link("next", string.Format("http://localhost:53048/api/ExchangeRates/?page={0}&take={1}", NextPage, PageSize)));

            return hrefs;
        }

    }
}