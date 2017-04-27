using MVC5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute(
            //    name: "Manage",
            //    url: "Manage/{action}",
            //    defaults: new { controller = "Manage", action = "Index"}
            //);

            routes.Add("ListingDetails", new SeoFriendlyRoute("property-for-auction/Details/{id}",
            new RouteValueDictionary(new { controller = "Listing", action = "Details" }),
            new MvcRouteHandler()));

            routes.MapRoute(
                name: "Listing",
                url: "property-for-auction/{action}/{id}",
                defaults: new { controller = "Listing", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "MVC5.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new []{"MVC5.Controllers"}
            );
        }
    }
}
