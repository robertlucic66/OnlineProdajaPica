using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineProdajaPica
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddToCart",
                url: "cart/addtocart/{id}/{quantity}",
                defaults: new { controller = "Cart", action = "AddToCart", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "KategorijeRoute",
                url: "products/kategorije/{id}",
                defaults: new { controller = "Products", action = "Kategorije", id = UrlParameter.Optional }
            );
        }
    }
}
