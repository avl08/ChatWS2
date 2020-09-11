using ChatWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatWeb.Filter
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                if (HttpContext.Current.Session["User"] == null) 
                {
                    if (filterContext.Controller is HomeController==false) 
                    {
                        filterContext.HttpContext.Response.Redirect("Home/Login");
                    }
                }
            }
            catch (Exception ex) 
            {

            }
        }
    }
}