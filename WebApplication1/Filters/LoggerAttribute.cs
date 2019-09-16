using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL.Utils;
using WebApplication1.Models;
using BL.BModel;
using BL.Services;
using BL.BInterfaces;
using Ninject.Modules;
using Ninject.Web.Mvc;
using WebApplication1.Modules;
using Ninject;

namespace WebApplication1.Filters
{
    public class LoggerAttribute : FilterAttribute, IActionFilter, IExceptionFilter
    {
        private ILogDetailService db;
        public LoggerAttribute()
        {
            NinjectModule Module = new ViewModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(serviceModule, Module);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            db = kernel.Get<LogDetailService>();
        }

        public void OnException(ExceptionContext filterContext)
        {

            BLogDetail logerModel = new BLogDetail()
            {
                Message = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };
            db.CreateOrUpdate(logerModel);
            filterContext.ExceptionHandled = true;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            BLogDetail logerModel = new BLogDetail()
            {
                Message = filterContext.ActionDescriptor.ActionName,
                StackTrace = filterContext.Result.ToString(),
                ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
                ActionName = filterContext.ActionDescriptor.ActionName,
                Date = DateTime.Now
            };
            db.CreateOrUpdate(logerModel);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}