using MovieDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Helpers
{
    public static class HtmlHelpers
    {
        public static string PrettyJoin(this HtmlHelper html, IEnumerable<Person> persons)
        {
            string res = null;
            int count = 0;
            foreach (var person in persons)
            {
                switch (count++)
                {
                    case 0:
                        res = person.Name;
                        break;

                    case 1:
                        res = person.Name + " og " + res;
                        break;

                    default:
                        res = person.Name + ", " + res;
                        break;
                }
            }

            return res;
        }
    }
}