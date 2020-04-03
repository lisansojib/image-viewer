﻿using ImageViewer.WebR;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;

namespace ImageViewer.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            GlobalConfiguration.Configure(config => InitImageViewer.Init(config, RouteTable.Routes));

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}