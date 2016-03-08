using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading;
using System.Globalization;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class PublicController : Controller
    {
        public PublicController()
        {
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (User.Identity.GetUserId() == null) ViewBag.kullaniciId = "undefined";
            else ViewBag.kullaniciId = User.Identity.GetUserId();
            base.OnActionExecuted(filterContext);
        }
    }
}
