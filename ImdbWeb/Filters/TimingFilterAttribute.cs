using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImdbWeb.Filters
{
    public class TimingFilterAttribute : FilterAttribute, IActionFilter, IResultFilter
    {
        private Stopwatch _stopwatch;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _stopwatch = Stopwatch.StartNew();
        }


        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Timing-1:", _stopwatch.Elapsed.ToString());
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Timing-2:", _stopwatch.Elapsed.ToString());
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add("Timing-3:", _stopwatch.Elapsed.ToString());
        }

    }
}