using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MVC5.Common
{
    public class ActionLog : ActionFilterAttribute, IExceptionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string mesg = "\n"+filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " ->" 
                + filterContext.ActionDescriptor.ActionName + "-> onExecuting \t" + DateTime.Now;

            logExecutionTime(mesg);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string mesg = "\n" + filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + " ->"
                 + filterContext.ActionDescriptor.ActionName + "-> onExecuted \t" + DateTime.Now;

            logExecutionTime(mesg);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            string mesg = "\n" + filterContext.RouteData.Values["controller"].ToString() + " ->"
                + filterContext.RouteData.Values["action"].ToString() + "-> onResultExecuting \t" + DateTime.Now;

            logExecutionTime(mesg);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            string mesg = "\n" + filterContext.RouteData.Values["controller"].ToString() + " ->"
                + filterContext.RouteData.Values["action"].ToString() + "-> onResultExecuted \t" + DateTime.Now;

            logExecutionTime(mesg +"----------------------------");
        }

        public void OnException(ExceptionContext filterContext)
        {
            string mesg = "\n" + filterContext.RouteData.Values["controller"].ToString() + " ->"
               + filterContext.RouteData.Values["action"].ToString() + "-> onException \t" 
               + filterContext.Exception.Message + DateTime.Now;

            logExecutionTime(mesg + "----------------------------");
        }

        private void logExecutionTime(string data)
        {
           
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/App_Data/Data.txt"), data);
        }

    }
}