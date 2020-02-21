using FluentScheduler;
using ImageViewer.Extensions.Jobs;
using System.Web.Http;

namespace ImageViewer
{
    public static class InitImageViewer
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "imageviewer-api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Init JobManager
            JobManager.Initialize(new JobRegistry());
        }
    }
}
