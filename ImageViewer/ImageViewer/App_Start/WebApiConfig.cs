using System.Web.Http;

namespace ImageViewer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "imageviewer-api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
