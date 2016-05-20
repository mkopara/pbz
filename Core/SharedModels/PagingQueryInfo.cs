using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Core.SharedModels
{
    public class PagingQueryInfo
    {
        public int Page { get; set; }

        private int take = 10;
        public int Take
        {
            get
            {
                return take;
            }

            set
            {
                take = value;
            }
        }


        public static string PagePropertyName
        {
            get
            {
                return nameof(Page);
            }
        }
        public static string TakePropertyName
        {
            get
            {
                return nameof(Take);
            }
        }


    }
}