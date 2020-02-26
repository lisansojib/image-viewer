using FluentScheduler;
using System.IO;
using System.Web;

namespace ImageViewer.Extensions.Jobs
{
    public class JobRegistry : Registry
    {
        public JobRegistry()
        {
            var directoryPath = HttpContext.Current.Server.MapPath(Constants.TEMP_SAVE_DIRECTORY);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            Schedule(() => new DirectoryCleanUpJob(directoryPath)).ToRunNow().AndEvery(1).Hours();
        }
    }
}