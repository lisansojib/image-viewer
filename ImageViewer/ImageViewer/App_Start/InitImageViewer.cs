using FluentScheduler;
using ImageViewer.Extensions;
using ImageViewer.Extensions.Jobs;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using System.Web.Routing;

namespace ImageViewer
{
    public static class InitImageViewer
    {
        public static void Init(HttpConfiguration config, RouteCollection routes)
        {
            #region Configure WebApi
            var corsAttr = new EnableCorsAttribute("https://mozilla.github.io", "*", "*");
            config.EnableCors(corsAttr);

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ImageViewerApi",
                routeTemplate: "imageviewer-api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            #endregion

            #region Init MVC Module
            HostingEnvironment.RegisterVirtualPathProvider(new EmbeddedVirtualPathProvider());

            routes.MapRoute(
                name: "ImageViewer",
                url: "ImageViewer/{action}/{id}",
                defaults: new { controller = "DocumentViewer", action = "Index", id = UrlParameter.Optional }
            );
            #endregion

            #region Init Job Manager
            JobManager.Initialize(new JobRegistry());
            #endregion
        }
    }
}
