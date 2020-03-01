﻿using FluentScheduler;
using ImageViewer.Extensions.Jobs;

namespace ImageViewer
{
    public static class InitMvcModule
    {
        public static void Start()
        {
            MvcModules.MvcModules.Start();

            // Init JobManager
            JobManager.Initialize(new JobRegistry());
        }
    }
}
