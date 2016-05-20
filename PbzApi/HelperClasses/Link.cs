using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PbzApi.HelperClasses
{
    public class Link
    {
        public string Rel { get; set; }
        public string Href { get; set; }
        public string Title { get; set; }

        public Link()
        { }
        public Link(string relation, string href, string title = null)
        {
            //Ensure.Argument.NotNullOrEmpty(relation, "relation");
            // Ensure.Argument.NotNullOrEmpty(href, "href");
            Rel = relation;
            Href = href;
            Title = title;
        }
    }
}