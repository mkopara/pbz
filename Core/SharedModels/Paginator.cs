using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.SharedModels
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

            if (page <= 0)
                page = 1;
            

            _list = list.Skip((page-1) * pageSize).Take(pageSize);

            //if (page > TotalPages)
            //    page = TotalPages;

            //if there is next page, set it
            if (TotalPages > page)
                NextPage = page+1;

            //if there is previous page, set it
            if (page > 1 && _list.Count() > 0)
                PreviousPage = page-1;

            CurrentPage = page;
          
        }

        public List<Link> GetPaginationLinks(string prefixURL)
        {
            var hrefs = new List<Link>();

            if (PreviousPage.HasValue)
                hrefs.Add(new Link("prev", string.Format("{0}?{1}={2}&{3}={4}", 
                    prefixURL, PagingQueryInfo.PagePropertyName, PreviousPage, PagingQueryInfo.TakePropertyName,  PageSize)));
            if (NextPage.HasValue)
                hrefs.Add(new Link("next", string.Format("{0}?{1}={2}&{3}={4}",
                 prefixURL, PagingQueryInfo.PagePropertyName, NextPage, PagingQueryInfo.TakePropertyName, PageSize)));

            return hrefs;
        }
        
    }
}
