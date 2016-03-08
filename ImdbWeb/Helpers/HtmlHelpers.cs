using MovieDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace ImdbWeb.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString PrettyJoin(this HtmlHelper html, IEnumerable<Person> persons)
        {
            Func<Person, string> makeLink = p => html.ActionLink(p.Name, "Details", "Person", new { id = p.PersonId }, null).ToString();

            string res = null;
            int count = 0;
            foreach (var person in persons)
            {
                switch (count++)
                {
                    case 0:
                        res = makeLink(person);
                        break;

                    case 1:
                        res = makeLink(person) + " og " + res;
                        break;

                    default:
                        res = makeLink(person) + ", " + res;
                        break;
                }
            }

            return MvcHtmlString.Create(res);
        }
    }
}