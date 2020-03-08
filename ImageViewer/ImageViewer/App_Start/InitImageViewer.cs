//using FluentScheduler;
using ImageViewer.Extensions.Jobs;
using System;
using System.Web.Http;

namespace ImageViewer
{
    public static class InitImageViewer
    {
        public static void Register(HttpConfiguration config)
        {
            Type jobManager = CustomLoader.GetType("FluentScheduler.JobManager");
            Type registry = CustomLoader.GetType("FluentScheduler.Registry");

            var initializeMethod = jobManager.GetMethod("Initialize");

            var tt = registry.GetType();
            var v = new JobRegistry() as JobRegistry;
            initializeMethod.Invoke(null, new[] { new JobRegistry() });

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "imageviewer-api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Init JobManager
            //JobManager.Initialize(new JobRegistry());
        }
    }
}
