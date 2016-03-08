using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_CSharpTurk.Filters
{
    public class MakaleAramaAttribute: FilterAttribute,IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Do nothing ...
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string prevURL;
            try
            {
                prevURL = filterContext.HttpContext.Request.UrlReferrer.AbsoluteUri;
            }
            catch (Exception)
            {
                prevURL = "null";   
            }
            if (prevURL.Contains("/Home/AramaSonucu/"))
            {
                string aramaFiltresi = prevURL.Substring(prevURL.LastIndexOf('/') + 1);
                filterContext.RouteData.Values.Add("aramaFiltresi", aramaFiltresi);
            }
            else filterContext.RouteData.Values.Add("aramaFiltresi", "null");
        }
    }
}